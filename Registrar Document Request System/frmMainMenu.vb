Imports MySql.Data.MySqlClient

Public Class frmMainMenu

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.Role <> "Administrator" Then
            btnReport.Visible = False
        End If

        ' Set bottom footer values
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Load dashboard counts
        TotalStudents()
        TotalRequest()
        PendingRequest()
        CompletedRequest()
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
        sql = "SELECT COUNT(RequestID) FROM tblrequest"
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
        sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Pending'"
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
        sql = "SELECT COUNT(RequestID) FROM tblrequest WHERE Status = 'Released' OR Status = 'Completed'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            lblcompleted.Text = dr(0).ToString()
        End If

        dr.Close()
        cn.Close()
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

End Class