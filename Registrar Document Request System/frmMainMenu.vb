Imports MySql.Data.MySqlClient

Public Class frmMainMenu

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Load all dashboard counts and widgets
        RefreshDashboard()

        chtdocreqpermonth.Legends(0).Enabled = False
        chtMostreqdoc.Legends(0).Enabled = False
    End Sub

    ' Re-applies the logged-in user's info AND refreshes the dashboard data
    ' every time this form becomes visible again.
    Private Sub frmMainMenu_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshUserSession()
        RefreshDashboard()
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
            Dim studentSql As String = "SELECT COUNT(StudentID) FROM tblstudents"
            Using localCmd As New MySqlCommand(studentSql, cn)
                Dim result As Object = localCmd.ExecuteScalar()
                lbltotalstudents.Text = If(result IsNot Nothing, result.ToString(), "0")
            End Using
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading total students: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub TotalRequest()
        Try
            Call connection()
            Dim reqSql As String = "SELECT COUNT(RequestID) FROM tblrequest"
            Using localCmd As New MySqlCommand(reqSql, cn)
                Dim result As Object = localCmd.ExecuteScalar()
                lbltotrequests.Text = If(result IsNot Nothing, result.ToString(), "0")
            End Using
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading total requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub PendingRequest()
        Try
            Call connection()
            Dim pendingSql As String = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Pending'"
            Using localCmd As New MySqlCommand(pendingSql, cn)
                Dim result As Object = localCmd.ExecuteScalar()
                lblpendingrequests.Text = If(result IsNot Nothing, result.ToString(), "0")
            End Using
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading pending requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub CompletedRequest()
        Try
            Call connection()
            Dim completedSql As String = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status IN ('Released', 'Completed')"
            Using localCmd As New MySqlCommand(completedSql, cn)
                Dim result As Object = localCmd.ExecuteScalar()
                lblcompleted.Text = If(result IsNot Nothing, result.ToString(), "0")
            End Using
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading completed requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates dgvRecentReqDoc with active requests (excluding Released and 2025 records).
    ''' </summary>
    Public Sub LoadRecentRequests()
        Try
            Call connection()

            Dim recentSql As String = "SELECT r.RequestNo, " &
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

            Using localCmd As New MySqlCommand(recentSql, cn)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    dgvrecentreqdoc.Rows.Clear()
                    While localDr.Read()
                        Dim reqDateStr As String = If(IsDBNull(localDr("RequestDate")), "-", Convert.ToDateTime(localDr("RequestDate")).ToString("yyyy-MM-dd"))

                        dgvrecentreqdoc.Rows.Add(
                            localDr("RequestNo").ToString(),
                            If(IsDBNull(localDr("StudentName")), "-", localDr("StudentName").ToString()),
                            If(IsDBNull(localDr("DocumentNames")), "-", localDr("DocumentNames").ToString()),
                            If(IsDBNull(localDr("Status")) OrElse String.IsNullOrWhiteSpace(localDr("Status").ToString()), "-", localDr("Status").ToString()),
                            reqDateStr
                        )
                    End While
                End Using
            End Using

            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading dashboard requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates the Overdue Request grid including requests older than 7 days that are not completed/released.
    ''' </summary>
    Private Sub LoadOverdueRequests()
        Try
            Call connection()

            Dim overdueSql As String = "SELECT r.RequestNo, " &
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

            Using localCmd As New MySqlCommand(overdueSql, cn)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    dgvOverdueReq.Rows.Clear()
                    While localDr.Read()
                        Dim reqDateStr As String = If(IsDBNull(localDr("RequestDate")), "-", Convert.ToDateTime(localDr("RequestDate")).ToString("yyyy-MM-dd"))

                        dgvOverdueReq.Rows.Add(
                            localDr("RequestNo").ToString(),
                            If(IsDBNull(localDr("StudentName")), "-", localDr("StudentName").ToString()),
                            If(IsDBNull(localDr("DocumentNames")), "-", localDr("DocumentNames").ToString()),
                            If(IsDBNull(localDr("Status")) OrElse String.IsNullOrWhiteSpace(localDr("Status").ToString()), "-", localDr("Status").ToString()),
                            reqDateStr
                        )
                    End While
                End Using
            End Using

            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading overdue requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Populates the Most Requested Documents chart without modifying UI label controls.
    ''' </summary>
    Public Sub LoadMostRequestedDocuments()
        Try
            Call connection()

            Dim chartSql As String = "SELECT d.DocumentName, SUM(rd.Quantity) AS TotalQty " &
                                     "FROM tblrequestdetails rd " &
                                     "JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                                     "GROUP BY d.DocumentName " &
                                     "ORDER BY TotalQty DESC"

            Using localCmd As New MySqlCommand(chartSql, cn)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    chtMostreqdoc.Series("Series1").Points.Clear()
                    While localDr.Read()
                        chtMostreqdoc.Series("Series1").Points.AddXY(localDr("DocumentName").ToString(), Convert.ToInt32(localDr("TotalQty")))
                    End While
                End Using
            End Using

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

            Dim counts(12) As Integer

            Dim monthSql As String = "SELECT MONTH(RequestDate) AS m, COUNT(*) AS cnt " &
                                     "FROM tblrequest WHERE YEAR(RequestDate) = @year " &
                                     "GROUP BY MONTH(RequestDate)"

            Using localCmd As New MySqlCommand(monthSql, cn)
                localCmd.Parameters.AddWithValue("@year", currentYear)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    While localDr.Read()
                        Dim m As Integer = Convert.ToInt32(localDr("m"))
                        counts(m) = Convert.ToInt32(localDr("cnt"))
                    End While
                End Using
            End Using

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