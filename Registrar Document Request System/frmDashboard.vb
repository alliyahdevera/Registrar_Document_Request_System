Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.DataVisualization.Charting

' ============================================================================
'  frmDashboard  =  the "Main Menu" page (hosted inside frmMainMenu.pnlmain)
' ============================================================================
Public Class frmDashboard

    Private WithEvents tmrClock As New System.Windows.Forms.Timer With {.Interval = 1000}
    Private _loadingMonth As Boolean = False
    Private _loadingStaffFilter As Boolean = False

    ' Counts of the Top 5 documents (drawn outside the graph by chtMostreqdoc_PostPaint)
    Private ReadOnly _topQtys As New List(Of Integer)

    ' OVERDUE = PAID, still Pending/Processing, and 7+ days since payment
    ' (an unpaid request is not "overdue": it is auto-cancelled after 7 days).
    ' Used by BOTH the overdue grid and the overdue card so they always match.
    Private Const OVERDUE_WHERE As String =
        "r.Status IN ('Pending', 'Processing') " &
        "AND r.PaymentStatus = 'Paid' " &
        "AND COALESCE(r.ORDate, r.RequestDate) <= DATE_SUB(CURDATE(), INTERVAL 7 DAY) "

#Region "Lifecycle"

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
        Label1.Text = "Dashboard  -  " & SchoolYear.DisplayName

        UpdateClock()
        tmrClock.Start()

        SetupCharts()
        SetupGrids()
        LoadMonthCombo()
        LoadStaffPerfCombo()

        RefreshDashboard()
    End Sub

    Private Sub frmDashboard_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        tmrClock.Stop()
        tmrClock.Dispose()
    End Sub

    Private Sub tmrClock_Tick(sender As Object, e As EventArgs) Handles tmrClock.Tick
        UpdateClock()
    End Sub

    Private Sub UpdateClock()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

#End Region

#Region "Setup (charts, grids, combos)"

    Private Sub SetupCharts()
        ' ---- TOP 5 REQUESTED DOCUMENTS ----
        With chtMostreqdoc
            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Bottom
            .Legends(0).Alignment = StringAlignment.Near          ' legend on the left

            With .Series("Series1")
                .LegendText = "Number of Requests"
                .IsValueShownAsLabel = False                      ' numbers are drawn by chtMostreqdoc_PostPaint
            End With

            With .ChartAreas(0)
                .AxisY.LabelStyle.Enabled = False
                .AxisY.MajorGrid.Enabled = True                   ' vertical lines stay
                .AxisX.MajorGrid.Enabled = True                   ' horizontal lines stay
                .AxisX.Interval = 1
                .AxisX.IsReversed = True
                .Position = New ElementPosition(0, 2, 100, 88)
                ' The graph ends at 37 + 54 = 91% of the width; the free strip on the right holds the numbers.
                ' Lower the 37 to move the graph further left.
                .InnerPlotPosition = New ElementPosition(37, 3, 54, 85)
            End With
        End With

        ' ---- DOCUMENT REQUEST PER MONTH ----
        With chtdocreqpermonth
            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Bottom
            With .ChartAreas(0)
                .AxisX.Interval = 1
                .AxisX.LabelStyle.IsStaggered = False
                .AxisY.Minimum = 0
                .AxisY.LabelStyle.Format = "0"
            End With
        End With
    End Sub

    Private Sub SetupGrids()
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub LoadMonthCombo()
        _loadingMonth = True
        cbomonth.Items.Clear()
        cbomonth.Items.Add("All Months")
        For m As Integer = 1 To 12
            cbomonth.Items.Add(MonthName(m, False))
        Next
        cbomonth.SelectedIndex = 0
        _loadingMonth = False
    End Sub

    Private Sub LoadStaffPerfCombo()
        _loadingStaffFilter = True
        cbostaffperfdates.Items.Clear()
        cbostaffperfdates.Items.Add("All Time")
        cbostaffperfdates.Items.Add("This Day")
        cbostaffperfdates.Items.Add("This Week")
        cbostaffperfdates.Items.Add("This Month")
        cbostaffperfdates.SelectedIndex = 0
        _loadingStaffFilter = False
    End Sub

#End Region

#Region "Data loading"

    Private Sub RefreshDashboard()
        ApplyAutoRules()      ' auto-cancel rules run before reading any numbers

        Try
            If Not connection() Then Exit Sub

            Dim f As String = SchoolYear.AndRequestDate()     ' school year filter ("" for All Time)

            lbltotrequests.Text = GetScalar("SELECT COUNT(r.RequestID) FROM tblrequest r WHERE 1=1 " & f)
            lblpendingrequests.Text = GetScalar("SELECT COUNT(r.RequestID) FROM tblrequest r WHERE r.Status = 'Pending' " & f)
            lblcompleted.Text = GetScalar("SELECT COUNT(r.RequestID) FROM tblrequest r WHERE r.Status IN ('Released', 'Completed') " & f)

            ' The 4th card's label control is still named lbltotalstudents,
            ' but it is the "Overdue Request" card.
            lbltotalstudents.Text = GetScalar("SELECT COUNT(r.RequestID) FROM tblrequest r WHERE " & OVERDUE_WHERE & f)

            LoadRecentRequests()
            LoadOverdueRequests()
            LoadMostRequestedDocuments()
            LoadDocReqPerMonth()
            LoadStaffPerformance()
        Catch ex As Exception
            MsgBox("Error loading dashboard: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Function GetScalar(query As String) As String
        Using c As New MySqlCommand(query, cn)
            Dim result As Object = c.ExecuteScalar()
            Return If(result Is Nothing OrElse IsDBNull(result), "0", result.ToString())
        End Using
    End Function

    ' RECENT = the newest requests of ALL time (not cancelled), newest first.
    Private Sub LoadRecentRequests()
        Dim query As String =
        "SELECT r.RequestNo, " &
        "CONCAT(s.FirstName, ' ', s.LastName) AS StudentName, " &
        "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
        "r.Status, r.RequestDate " &
        "FROM tblrequest r " &
        "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
        "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
        "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
        "WHERE r.Status <> 'Cancelled' " &
        SchoolYear.AndRequestDate() &
        "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
        "ORDER BY r.RequestDate DESC, r.RequestID DESC " &
        "LIMIT 10"
        FillRequestGrid(dgvrecentreqdoc, query)
    End Sub

    Private Sub LoadOverdueRequests()
        Dim query As String =
            "SELECT r.RequestNo, " &
            "CONCAT(s.FirstName, ' ', s.LastName) AS StudentName, " &
            "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
            "r.Status, r.RequestDate " &
            "FROM tblrequest r " &
            "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
            "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
            "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
            "WHERE " & OVERDUE_WHERE & SchoolYear.AndRequestDate() &
            "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
            "ORDER BY r.RequestDate ASC"
        FillRequestGrid(dgvOverdueReq, query)
    End Sub

    Private Sub FillRequestGrid(grid As DataGridView, query As String)
        grid.Rows.Clear()
        Using c As New MySqlCommand(query, cn)
            Using rdr As MySqlDataReader = c.ExecuteReader()
                While rdr.Read()
                    Dim reqDateStr As String = If(IsDBNull(rdr("RequestDate")), "-",
                                                  Convert.ToDateTime(rdr("RequestDate")).ToString("yyyy-MM-dd"))
                    grid.Rows.Add(
                        rdr("RequestNo").ToString(),
                        If(IsDBNull(rdr("StudentName")), "-", rdr("StudentName").ToString()),
                        If(IsDBNull(rdr("DocumentNames")), "-", rdr("DocumentNames").ToString()),
                        If(IsDBNull(rdr("Status")) OrElse String.IsNullOrWhiteSpace(rdr("Status").ToString()), "-", rdr("Status").ToString()),
                        reqDateStr)
                End While
            End Using
        End Using
    End Sub

    Private Sub LoadMostRequestedDocuments()
        Dim query As String =
            "SELECT d.DocumentName, SUM(rd.Quantity) AS TotalQty " &
            "FROM tblrequestdetails rd " &
            "JOIN tblrequest r ON r.RequestID = rd.RequestID " &
            "JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
            "WHERE d.DocumentName IS NOT NULL AND d.DocumentName <> '' " &
            SchoolYear.AndRequestDate() &
            "GROUP BY d.DocumentName " &
            "HAVING SUM(rd.Quantity) > 0 " &
            "ORDER BY TotalQty DESC " &
            "LIMIT 5"

        Dim s As Series = chtMostreqdoc.Series("Series1")
        s.Points.Clear()
        _topQtys.Clear()

        Using c As New MySqlCommand(query, cn)
            Using rdr As MySqlDataReader = c.ExecuteReader()
                While rdr.Read()
                    Dim qty As Integer = Convert.ToInt32(rdr("TotalQty"))
                    s.Points.AddXY(rdr("DocumentName").ToString(), qty)
                    _topQtys.Add(qty)
                End While
            End Using
        End Using

        Dim maxQty As Integer = If(_topQtys.Count > 0, _topQtys.Max(), 0)
        With chtMostreqdoc.ChartAreas(0).AxisY
            .Minimum = 0
            .Maximum = Math.Max(5, Math.Ceiling(maxQty * 1.1))
            .Interval = Math.Max(1, Math.Ceiling(.Maximum / 5))
        End With

        chtMostreqdoc.Invalidate()
    End Sub

    ' Draws each count just outside the right edge of the graph, centered on its bar's row
    Private Sub chtMostreqdoc_PostPaint(sender As Object, e As ChartPaintEventArgs) Handles chtMostreqdoc.PostPaint
        If Not TypeOf e.ChartElement Is ChartArea Then Exit Sub
        If _topQtys.Count = 0 Then Exit Sub

        Dim area As ChartArea = chtMostreqdoc.ChartAreas(0)
        Dim g As Graphics = e.ChartGraphics.Graphics
        Dim xRight As Single = CSng(area.AxisY.ValueToPixelPosition(area.AxisY.Maximum))

        Using f As New Font("Segoe UI", 9, FontStyle.Bold), br As New SolidBrush(Color.Black)
            Using sf As New StringFormat()
                sf.Alignment = StringAlignment.Near
                sf.LineAlignment = StringAlignment.Center
                For i As Integer = 0 To _topQtys.Count - 1
                    Dim y As Single = CSng(area.AxisX.ValueToPixelPosition(i + 1))
                    g.DrawString(_topQtys(i).ToString(), f, br, xRight + 10, y, sf)
                Next
            End Using
        End Using
    End Sub

    ' School year selected  -> its 12 months (e.g. Jun ... May) or the days of one month
    ' All Time              -> Jan ... Dec with all years combined, or days of one month combined
    Private Sub LoadDocReqPerMonth()
        Dim s As Series = chtdocreqpermonth.Series("Series1")
        s.Points.Clear()

        Dim f As String = SchoolYear.AndRequestDate()
        Dim maxVal As Integer = 0

        If cbomonth.SelectedIndex <= 0 Then
            ' ----- all months -----
            Dim counts(12) As Integer
            Dim query As String =
                "SELECT MONTH(r.RequestDate) AS m, COUNT(*) AS cnt " &
                "FROM tblrequest r WHERE 1=1 " & f &
                "GROUP BY MONTH(r.RequestDate)"
            Using c As New MySqlCommand(query, cn)
                Using rdr As MySqlDataReader = c.ExecuteReader()
                    While rdr.Read()
                        counts(Convert.ToInt32(rdr("m"))) = Convert.ToInt32(rdr("cnt"))
                    End While
                End Using
            End Using

            Dim firstMonth As Integer = If(SchoolYear.IsAllTime, 1, SchoolYear.StartDate.Month)
            For i As Integer = 0 To 11
                Dim m As Integer = ((firstMonth - 1 + i) Mod 12) + 1
                s.Points.AddXY(MonthName(m, True), counts(m))
                If counts(m) > maxVal Then maxVal = counts(m)
            Next

            s.LegendText = "Requests per month (" & SchoolYear.DisplayName & ")"
            chtdocreqpermonth.ChartAreas(0).AxisX.Interval = 1
        Else
            ' ----- one month, day by day -----
            Dim monthNo As Integer = cbomonth.SelectedIndex          ' 1 = January ... 12 = December

            Dim days As Integer
            If SchoolYear.IsAllTime Then
                days = DateTime.DaysInMonth(2024, monthNo)           ' 2024 = leap year, so Feb has 29
            Else
                Dim yr As Integer = If(monthNo >= SchoolYear.StartDate.Month, SchoolYear.StartDate.Year, SchoolYear.EndDate.Year)
                days = DateTime.DaysInMonth(yr, monthNo)
            End If

            Dim counts(days) As Integer
            Dim query As String =
                "SELECT DAY(r.RequestDate) AS d, COUNT(*) AS cnt " &
                "FROM tblrequest r WHERE MONTH(r.RequestDate) = @month " & f &
                "GROUP BY DAY(r.RequestDate)"
            Using c As New MySqlCommand(query, cn)
                c.Parameters.AddWithValue("@month", monthNo)
                Using rdr As MySqlDataReader = c.ExecuteReader()
                    While rdr.Read()
                        Dim dayNo As Integer = Convert.ToInt32(rdr("d"))
                        If dayNo <= days Then counts(dayNo) = Convert.ToInt32(rdr("cnt"))
                    End While
                End Using
            End Using

            For d As Integer = 1 To days
                s.Points.AddXY(d.ToString(), counts(d))
                If counts(d) > maxVal Then maxVal = counts(d)
            Next

            s.LegendText = "Requests per day (" & MonthName(monthNo, False) & ", " & SchoolYear.DisplayName & ")"
            chtdocreqpermonth.ChartAreas(0).AxisX.Interval = If(days > 16, 2, 1)
        End If

        chtdocreqpermonth.ChartAreas(0).AxisY.Interval = Math.Max(1, Math.Ceiling(maxVal / 5.0))
    End Sub

    Private Sub cbomonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbomonth.SelectedIndexChanged
        If _loadingMonth Then Exit Sub
        Try
            If Not connection() Then Exit Sub
            LoadDocReqPerMonth()
        Catch ex As Exception
            MsgBox("Error loading chart: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    ' Extra date condition for the staff performance table (works together with the school year filter)
    Private Function StaffDateFilter() As String
        Select Case cbostaffperfdates.SelectedIndex
            Case 1  ' This Day
                Return " AND r.RequestDate = CURDATE() "
            Case 2  ' This Week (Monday to Sunday)
                Return " AND YEARWEEK(r.RequestDate, 1) = YEARWEEK(CURDATE(), 1) "
            Case 3  ' This Month
                Return " AND YEAR(r.RequestDate) = YEAR(CURDATE()) AND MONTH(r.RequestDate) = MONTH(CURDATE()) "
            Case Else  ' All Time
                Return ""
        End Select
    End Function

    Private Sub cbostaffperfdates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbostaffperfdates.SelectedIndexChanged
        If _loadingStaffFilter Then Exit Sub
        Try
            If Not connection() Then Exit Sub
            LoadStaffPerformance()
        Catch ex As Exception
            MsgBox("Error loading staff performance: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    ' STAFF PERFORMANCE (school year filter + This Day / This Week / This Month filter)
    Private Sub LoadStaffPerformance()
        Dim f As String = SchoolYear.AndRequestDate() & StaffDateFilter()
        Dim query As String =
            "SELECT u.FullName, " &
            "(SELECT COUNT(*) FROM tblrequest r WHERE r.CreatedBy = u.UserID AND r.Status = 'Pending'" & f & ") AS PendingCnt, " &
            "(SELECT COUNT(*) FROM tblrequest r WHERE r.CreatedBy = u.UserID" & f & ") AS CreatedCnt, " &
            "(SELECT COUNT(*) FROM tblrequest r WHERE r.ProcessedBy = u.UserID " &
            "    AND r.Status IN ('Processing', 'Ready for Release', 'Released')" & f & ") AS ProcessedCnt, " &
            "(SELECT COUNT(*) FROM tblrequest r WHERE r.ReleasedBy = u.UserID AND r.Status = 'Released'" & f & ") AS ReleasedCnt, " &
            "(SELECT COUNT(*) FROM tblrequest r WHERE (r.CreatedBy = u.UserID " &
            "    OR r.ProcessedBy = u.UserID OR r.ReleasedBy = u.UserID)" & f & ") AS TotalCnt " &
            "FROM tblusers u WHERE u.Status = 'Active' " &
            "ORDER BY u.FullName"

        DataGridView1.Rows.Clear()
        Using c As New MySqlCommand(query, cn)
            Using rdr As MySqlDataReader = c.ExecuteReader()
                While rdr.Read()
                    DataGridView1.Rows.Add(
                        rdr("FullName").ToString(),
                        rdr("PendingCnt").ToString(),
                        rdr("CreatedCnt").ToString(),
                        rdr("ProcessedCnt").ToString(),
                        rdr("ReleasedCnt").ToString(),
                        rdr("TotalCnt").ToString())
                End While
            End Using
        End Using
    End Sub

#End Region

    Private Sub btnViewReq_Click(sender As Object, e As EventArgs) Handles btnViewReq.Click
        frmMainMenu.OpenRequestList()
    End Sub

End Class