Imports MySql.Data.MySqlClient

Public Class frmMainMenu

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Auto-cancel pending requests older than 7 days prior to dashboard refresh
        AutoCancelPendingRequests()

        ' Load all dashboard counts and widgets
        RefreshDashboard()
    End Sub

    ' Re-applies the logged-in user's info AND refreshes the dashboard data
    ' every time this form becomes visible again.
    Private Sub frmMainMenu_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshUserSession()
        AutoCancelPendingRequests()
        RefreshDashboard()
    End Sub
    Private Sub AutoCancelPendingRequests()
        Try
            Call connection()

            sql = "UPDATE tblrequest " &
              "SET Status = 'Cancelled' " &
              "WHERE (Status = 'Pending' OR Status IS NULL OR Status = '') " &
              "AND RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Centralizes all dashboard data reloading methods into a single call.
    ''' </summary>
    Private Sub RefreshDashboard()
        TotalStudents()
        TotalRequest()
        PendingRequest()
        CompletedRequest()

        LoadRecentRequests()
        LoadOverdueRequests()
        LoadMostRequestedDocuments()
        LoadDocReqPerMonth()
    End Sub

    Private Sub RefreshUserSession()
        btnReport.Visible = (CurrentUser.Role = "Administrator")

        ' Set bottom footer values
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
    End Sub

    ' Real-time date/time clock event
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    Private Sub TotalStudents()
        Try
            Call connection()
            sql = "SELECT COUNT(StudentID) FROM tblstudents"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lbltotalstudents.Text = dr(0).ToString()
            End If

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading total students: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub TotalRequest()
        Try
            Call connection()
            sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE PaymentStatus = 'Paid'"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lbltotrequests.Text = dr(0).ToString()
            End If

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading total requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub PendingRequest()
        Try
            Call connection()
            sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Pending' AND PaymentStatus = 'Paid'"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lblpendingrequests.Text = dr(0).ToString()
            End If

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading pending requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub CompletedRequest()
        Try
            Call connection()
            sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE (Status = 'Released' OR Status = 'Completed') AND PaymentStatus = 'Paid'"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lblcompleted.Text = dr(0).ToString()
            End If

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading completed requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates dgvRecentReqDoc with requests that are Paid and currently
    ''' being worked on (Processing or Ready for Release).
    ''' </summary>
    Public Sub LoadRecentRequests()
        Try
            Call connection()

            sql = "SELECT r.RequestNo, " &
                  "CONCAT(s.FirstName, ' ', s.LastName) AS StudentName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.Status, r.RequestDate " &
                  "FROM tblrequest r " &
                  "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
                  "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "WHERE r.PaymentStatus = 'Paid' " &
                  "AND r.Status IN ('Processing', 'Ready for Release') " &
                  "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
                  "ORDER BY r.RequestID DESC " &
                  "LIMIT 10"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            dgvrecentreqdoc.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))

                dgvrecentreqdoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    If(IsDBNull(dr("StudentName")), "-", dr("StudentName").ToString()),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "-", dr("Status").ToString()),
                    reqDateStr
                )
            End While

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading dashboard requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates the Overdue Request grid including Cancelled requests older than 7 days.
    ''' </summary>
    Private Sub LoadOverdueRequests()
        Try
            Call connection()

            sql = "SELECT r.RequestNo, " &
                  "CONCAT(s.FirstName, ' ', s.LastName) AS StudentName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.Status, r.RequestDate " &
                  "FROM tblrequest r " &
                  "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
                  "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "WHERE r.Status NOT IN ('Completed', 'Released') " &
                  "AND r.RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY) " &
                  "GROUP BY r.RequestID, r.RequestNo, StudentName, r.Status, r.RequestDate " &
                  "ORDER BY r.RequestDate ASC"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            DataGridView1.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))

                DataGridView1.Rows.Add(
                    dr("RequestNo").ToString(),
                    If(IsDBNull(dr("StudentName")), "-", dr("StudentName").ToString()),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "-", dr("Status").ToString()),
                    reqDateStr
                )
            End While

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading overdue requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates the Most Requested Documents chart using the entire request history.
    ''' </summary>
    Private Sub LoadMostRequestedDocuments()
        Try
            Call connection()

            sql = "SELECT d.DocumentName, SUM(rd.Quantity) AS TotalQty " &
                  "FROM tblrequestdetails rd " &
                  "JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "GROUP BY d.DocumentName " &
                  "ORDER BY TotalQty DESC"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            chtMostreqdoc.Series("Series1").Points.Clear()
            While dr.Read()
                chtMostreqdoc.Series("Series1").Points.AddXY(dr("DocumentName").ToString(), Convert.ToInt32(dr("TotalQty")))
            End While

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading most requested documents chart: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Redraws the Document Request per Month chart for all 12 months of the current year.
    ''' </summary>
    Private Sub LoadDocReqPerMonth()
        Dim currentYear As Integer = DateTime.Today.Year

        Try
            Call connection()
            chtdocreqpermonth.Series("Series1").Points.Clear()

            ' Array to store total requests per month (1 = Jan, 12 = Dec)
            Dim counts(12) As Integer

            sql = "SELECT MONTH(RequestDate) AS m, COUNT(*) AS cnt " &
                  "FROM tblrequest WHERE YEAR(RequestDate) = @year " &
                  "GROUP BY MONTH(RequestDate)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@year", currentYear)
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim m As Integer = Convert.ToInt32(dr("m"))
                counts(m) = Convert.ToInt32(dr("cnt"))
            End While
            dr.Close()

            ' Plot all 12 months onto the chart
            For m As Integer = 1 To 12
                chtdocreqpermonth.Series("Series1").Points.AddXY(MonthName(m, True), counts(m))
            Next

            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading Document Request per Month chart: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ' Navigation Handlers
    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentManagement_Click(sender As Object, e As EventArgs) Handles btnDocumentManagement.Click
        frmDocumentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentRequests_Click(sender As Object, e As EventArgs) Handles btnDocumentRequests.Click
        frmNewRequest.Show()
        Me.Hide()
    End Sub

    Private Sub btnReqList_Click(sender As Object, e As EventArgs) Handles btnReqList.Click
        frmRequestList.Show()
        Me.Hide()
    End Sub

    Private Sub btnViewReq_Click(sender As Object, e As EventArgs) Handles btnViewReq.Click
        frmRequestList.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to logout?", vbYesNo + vbQuestion, "Confirm Logout") = MsgBoxResult.Yes Then
            CurrentUser.UserID = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            frmLogin.Show()
            Me.Close()
        End If
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        frmReports.Show()
        Me.Hide()
    End Sub

    Private Sub btnUserManagement_Click(sender As Object, e As EventArgs) Handles btnUserManagement.Click
        frmUserManagement.Show()
        Me.Hide()
    End Sub

End Class