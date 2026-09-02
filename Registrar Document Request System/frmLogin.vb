Imports MySql.Data.MySqlClient 
 
Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
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
                frmMainMenu.Show()
                Me.Hide()
            Else
                dr.Close()
                cn.Close()
                MessageBox.Show("Invalid username or password.")
            End If

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        End
    End Sub
End Class

