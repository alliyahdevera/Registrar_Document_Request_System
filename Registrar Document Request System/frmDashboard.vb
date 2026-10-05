Imports MySql.Data.MySqlClient

' ============================================================================
'  frmDashboard  =  the "Main Menu" page (hosted inside frmMainMenu.pnlmain)
'  All the dashboard code that used to live in frmMainMenu is now here.
' ============================================================================
Public Class frmDashboard

    Private WithEvents tmrClock As New System.Windows.Forms.Timer With {.Interval = 1000}

#Region "Lifecycle"

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        UpdateClock()
        tmrClock.Start()

        chtdocreqpermonth.Legends(0).Enabled = False

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

#Region "Data loading"

    ' One connection for the whole refresh (the old code opened/closed it 8 times).
    Private Sub RefreshDashboard()
        Try
            If Not connection() Then Exit Sub

            lbltotalstudents.Text = GetScalar("SELECT COUNT(StudentID) FROM tblstudents")
            lbltotrequests.Text = GetScalar("SELECT COUNT(RequestID) FROM tblrequest")
            lblpendingrequests.Text = GetScalar("SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Pending'")
            lblcompleted.Text = GetScalar("SELECT COUNT(RequestID) FROM tblrequest WHERE Status IN ('Released', 'Completed')")

            LoadRecentRequests()
            LoadOverdueRequests()
            LoadMostRequestedDocuments()
            LoadDocReqPerMonth()
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
            "WHERE r.PaymentStatus = 'Paid' " &
            "AND YEAR(r.RequestDate) <> 2025 " &
            "AND r.Status IN ('Processing', 'Ready for Release') " &
            "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
            "ORDER BY r.RequestID DESC " &
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
            "WHERE r.Status NOT IN ('Completed', 'Released', 'Cancelled') " &
            "AND r.RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY) " &
            "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
            "ORDER BY r.RequestDate ASC"
        FillRequestGrid(dgvOverdueReq, query)
    End Sub

    ' Both grids have the same 5 columns: RequestNo, Student, Document, Status, Date.
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
            "JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
            "GROUP BY d.DocumentName " &
            "ORDER BY TotalQty DESC"

        chtMostreqdoc.Series("Series1").Points.Clear()
        Using c As New MySqlCommand(query, cn)
            Using rdr As MySqlDataReader = c.ExecuteReader()
                While rdr.Read()
                    chtMostreqdoc.Series("Series1").Points.AddXY(rdr("DocumentName").ToString(), Convert.ToInt32(rdr("TotalQty")))
                End While
            End Using
        End Using
    End Sub

    Private Sub LoadDocReqPerMonth()
        Dim counts(12) As Integer
        Dim query As String =
            "SELECT MONTH(RequestDate) AS m, COUNT(*) AS cnt " &
            "FROM tblrequest WHERE YEAR(RequestDate) = @year " &
            "GROUP BY MONTH(RequestDate)"

        Using c As New MySqlCommand(query, cn)
            c.Parameters.AddWithValue("@year", DateTime.Today.Year)
            Using rdr As MySqlDataReader = c.ExecuteReader()
                While rdr.Read()
                    counts(Convert.ToInt32(rdr("m"))) = Convert.ToInt32(rdr("cnt"))
                End While
            End Using
        End Using

        chtdocreqpermonth.Series("Series1").Points.Clear()
        For m As Integer = 1 To 12
            chtdocreqpermonth.Series("Series1").Points.AddXY(MonthName(m, True), counts(m))
        Next
    End Sub

#End Region

    ' "View All Requests" -> swap the page inside the shell.
    ' (The old code had no Handles clause here, so this button did nothing.)
    Private Sub btnViewReq_Click(sender As Object, e As EventArgs) Handles btnViewReq.Click
        frmMainMenu.OpenRequestList()
    End Sub

End Class