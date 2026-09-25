Imports MySql.Data.MySqlClient

Public Class frmLogin

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        txtPassword.PasswordChar = ControlChars.NullChar
    End Sub

    ' Fires every time the login screen becomes visible again - including
    ' right after a logout (since the form is reused via Show(), not recreated).
    Private Sub frmLogin_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        txtUsername.Clear()
        txtPassword.Clear()
        txtUsername.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Call connection()
            sql = "SELECT * FROM tblusers WHERE Username=@u AND Password=@p AND Status='Active'"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim())
            cmd.Parameters.AddWithValue("@p", txtPassword.Text.Trim())
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                CurrentUser.UserID = Convert.ToInt32(dr("UserID"))
                CurrentUser.FullName = dr("FullName").ToString()
                CurrentUser.Role = dr("Role").ToString()
                dr.Close()
                cn.Close()
                MsgBox("Welcome to Registrar Document Request System!", vbInformation, "Registrar Document Request System")
                frmMainMenu.Show()
                Me.Hide()
            Else
                dr.Close()
                cn.Close()
                MsgBox("Invalid Username or Password", vbExclamation, "Registrar Document Request System")
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)

        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MsgBox("Are you sure you want to exit system?", vbQuestion + vbYesNo, "Registrar Document Request System") = vbYes Then
            MsgBox("Thank you for using Registrar Document Request System!", vbInformation, "Registrar Document Request System")
            End
        End If
    End Sub

    Private Sub btnShowPass_Click(sender As Object, e As EventArgs) Handles btnShowPass.Click
        txtPassword.UseSystemPasswordChar = Not txtPassword.UseSystemPasswordChar
    End Sub

    Private Sub lnklblForgotPass_Click(sender As Object, e As EventArgs) Handles lnklblForgotPass.Click
        If MsgBox("Do you want to change your password?", vbQuestion + vbYesNo, "Registrar Document Request System") = vbYes Then
            MsgBox("Please contact the Admin to change your password!", vbInformation, "Registrar Document Request System")
        End If
    End Sub
End Class