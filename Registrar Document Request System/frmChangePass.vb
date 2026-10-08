Imports MySql.Data.MySqlClient

Public Class frmChangePass

    Private Sub frmChangePass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.KeyPreview = True
        Me.AcceptButton = btnLogin          ' Enter = Update

        txtold.PasswordChar = "*"c
        txtnew.PasswordChar = "*"c
        txtconfirm.PasswordChar = "*"c
        txtold.Clear()
        txtnew.Clear()
        txtconfirm.Clear()
        txtold.Focus()
    End Sub

    ' Esc = close without changing anything
    Private Sub frmChangePass_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    ' ---- show / hide password buttons ----
    Private Sub TogglePassword(tb As TextBox)
        tb.PasswordChar = If(tb.PasswordChar = ControlChars.NullChar, "*"c, ControlChars.NullChar)
    End Sub

    Private Sub btnnewp_Click(sender As Object, e As EventArgs) Handles btnnewp.Click
        TogglePassword(txtnew)
    End Sub

    Private Sub btncshow_Click(sender As Object, e As EventArgs) Handles btncshow.Click
        TogglePassword(txtconfirm)
    End Sub

    ' ---- Update ----
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim oldPass As String = txtold.Text.Trim()
        Dim newPass As String = txtnew.Text.Trim()
        Dim confirmPass As String = txtconfirm.Text.Trim()

        If oldPass = "" Then
            MsgBox("Enter your old password.", vbExclamation, "Change Password")
            txtold.Focus()
            Exit Sub
        End If
        If newPass = "" Then
            MsgBox("Enter your new password.", vbExclamation, "Change Password")
            txtnew.Focus()
            Exit Sub
        End If
        If newPass.Length < 6 Then
            MsgBox("New password must be at least 6 characters.", vbExclamation, "Change Password")
            txtnew.Focus()
            Exit Sub
        End If
        If confirmPass = "" Then
            MsgBox("Confirm your new password.", vbExclamation, "Change Password")
            txtconfirm.Focus()
            Exit Sub
        End If
        If newPass <> confirmPass Then
            MsgBox("New Password and Confirm Password do not match.", vbExclamation, "Change Password")
            txtconfirm.Clear()
            txtconfirm.Focus()
            Exit Sub
        End If
        If newPass = oldPass Then
            MsgBox("New password must be different from the old password.", vbExclamation, "Change Password")
            txtnew.Focus()
            Exit Sub
        End If

        Try
            ' 1) Verify the old password for the LOGGED-IN user
            Call connection()
            sql = "SELECT COUNT(*) FROM tblusers WHERE UserID = @id AND Password = @p AND Status = 'Active'"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", CurrentUser.UserID)
            cmd.Parameters.AddWithValue("@p", oldPass)
            Dim ok As Boolean = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            cn.Close()

            If Not ok Then
                MsgBox("Old password is incorrect.", vbExclamation, "Change Password")
                txtold.Clear()
                txtold.Focus()
                Exit Sub
            End If

            ' 2) Confirm
            If Not ConfirmAction("Change your password?", "Confirm Password Change") Then Exit Sub

            ' 3) Update ONLY this user's own password (ID comes from the session, not a textbox)
            Call connection()
            sql = "UPDATE tblusers SET Password = @new WHERE UserID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@new", newPass)
            cmd.Parameters.AddWithValue("@id", CurrentUser.UserID)
            cmd.ExecuteNonQuery()
            cn.Close()

            MsgBox("Your password was changed successfully!", vbInformation, "Success")
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error changing password: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

End Class