Imports MySql.Data.MySqlClient 
 
Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Using conn As MySqlConnection = GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM tblusers WHERE Username=@u AND Password=@p AND Status='Active'"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim())
                cmd.Parameters.AddWithValue("@p", txtPassword.Text.Trim())

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    CurrentUser.UserID = Convert.ToInt32(reader("UserID"))
                    CurrentUser.FullName = reader("FullName").ToString()
                    CurrentUser.Role = reader("Role").ToString()

                    Dim frm As New frmMainMenu()
                    frm.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Invalid username or password.")
                End If
            Catch ex As Exception
                MessageBox.Show("Connection error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        End
    End Sub
End Class

