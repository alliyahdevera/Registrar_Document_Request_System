Imports MySql.Data.MySqlClient

Public Class frmMainMenu

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Load dashboard counts
        TotalStudents()
        TotalRequest()
        PendingRequest()
        CompletedRequest()

        ' Load recent paid requests grid
        LoadRecentRequests()
    End Sub

    ' Re-applies the logged-in user's info to this form every time it becomes
    ' visible again. Navigation between screens uses Show()/Hide() rather than
    ' Close(), so a form that was already created keeps showing whoever was
    ' logged in when it first loaded unless we also refresh here.
    Private Sub frmMainMenu_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshUserSession()
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
        Call connection()
        sql = "SELECT COUNT(StudentID) FROM tblstudents"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            lbltotalstudents.Text = dr(0).ToString()
        End If

        dr.Close()
        cn.Close()
    End Sub

    Private Sub TotalRequest()
        Call connection()
        sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE PaymentStatus = 'Paid'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            lbltotrequests.Text = dr(0).ToString()
        End If

        dr.Close()
        cn.Close()
    End Sub

    Private Sub PendingRequest()
        Call connection()
        sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Pending' AND PaymentStatus = 'Paid'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            lblpendingrequests.Text = dr(0).ToString()
        End If

        dr.Close()
        cn.Close()
    End Sub

    Private Sub CompletedRequest()
        Call connection()
        sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE (Status = 'Released' OR Status = 'Completed') AND PaymentStatus = 'Paid'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            lblcompleted.Text = dr(0).ToString()
        End If

        dr.Close()
        cn.Close()
    End Sub

    ''' <summary>
    ''' Populates dgvRecentReqDoc with recent document requests that are Paid.
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