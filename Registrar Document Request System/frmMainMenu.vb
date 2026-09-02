Imports MySql.Data.MySqlClient

Public Class frmMainMenu

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.Role <> "Administrator" Then
            btnUserManagement.Visible = False
        End If
        TotalStudents()
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs)
        If MsgBox("Are you sure you want to logout?", vbYesNo + vbQuestion, "Confirm Logout") = MsgBoxResult.Yes Then
            CurrentUser.UserID = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            frmLogin.Show()
            Me.Close()
        End If
    End Sub

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs)
        frmStudentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentManagement_Click(sender As Object, e As EventArgs)
        frmDocumentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnStudentManagement_Click_1(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
        Me.Hide()

    End Sub

    Private Sub btnDocumentManagement_Click_1(sender As Object, e As EventArgs) Handles btnDocumentManagement.Click
        frmDocumentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click_1(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to logout?", vbYesNo + vbQuestion, "Confirm Logout") = MsgBoxResult.Yes Then
            CurrentUser.UserID = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            frmLogin.Show()
            Me.Close()
        End If
    End Sub

    Private Sub TotalStudents()
        Call connection()
        sql = "Select count(StudentID) from tblstudents"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lbltotalstudents.Text = dr(0).ToString()
            End If

            dr.Close()
            cn.Close()

    End Sub
End Class