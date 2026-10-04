Imports MySql.Data.MySqlClient

Public Class frmUserManagement
    Private _originalPassword As String = ""
    Private Function EnsureAdminAccess() As Boolean
        If Not CurrentUser.IsAdmin Then
            MsgBox("You don't have permission to access User Management.", vbExclamation, "Access Denied")
            frmMainMenu.Show()
            Me.Hide()
            Return False
        End If
        Return True
    End Function

    Private Sub frmUserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not EnsureAdminAccess() Then Exit Sub

        RefreshUserSession()

        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()
        cboRoles.DropDownStyle = ComboBoxStyle.DropDownList
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
        txtPassword.UseSystemPasswordChar = True
        txtConfirmPassword.UseSystemPasswordChar = True

        If Not dgvUsers.Columns.Contains("colRealPassword") Then
            Dim hiddenCol As New DataGridViewTextBoxColumn()
            hiddenCol.Name = "colRealPassword"
            hiddenCol.Visible = False
            dgvUsers.Columns.Add(hiddenCol)
        End If

        LoadUsers()

        SetAddMode()
    End Sub

    Private Sub frmUserManagement_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Not EnsureAdminAccess() Then Exit Sub

        RefreshUserSession()
    End Sub

    Private Sub RefreshUserSession()

        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub


    Private Sub SetAddMode()
        txtUserID.ReadOnly = False
        txtUserID.BackColor = Color.White
        cboStatus.Text = "Active"
        cboStatus.Enabled = False
    End Sub


    Private Sub SetEditMode()
        txtUserID.ReadOnly = True
        txtUserID.BackColor = Color.Gainsboro
        cboStatus.Enabled = True
    End Sub

    Private Sub ClearFields()
        txtUserID.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPassword.Clear()
        txtFirstName.Clear()
        txtLastName.Clear()
        cboRoles.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        _originalPassword = ""
    End Sub

    Private Sub LoadUsers()
        Call connection()
        sql = "SELECT * FROM tblusers"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dgvUsers.Rows.Clear()
        While dr.Read()
            Dim fullName As String = dr("FullName").ToString()
            Dim firstName As String = ""
            Dim lastName As String = ""
            SplitFullName(fullName, firstName, lastName)

            dgvUsers.Rows.Add(
                dr("UserID").ToString(),
                dr("Username").ToString(),
                PasswordHelper.GetDisplayHash(dr("Password").ToString()),
                firstName,
                lastName,
                dr("Role").ToString(),
                dr("Status").ToString(),
                dr("Password").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub


    Private Function BuildFullName() As String
        Return (txtFirstName.Text.Trim() & " " & txtLastName.Text.Trim()).Trim()
    End Function


    Private Sub SplitFullName(fullName As String, ByRef firstName As String, ByRef lastName As String)
        Dim spaceIndex As Integer = fullName.IndexOf(" "c)
        If spaceIndex = -1 Then
            firstName = fullName
            lastName = ""
        Else
            firstName = fullName.Substring(0, spaceIndex)
            lastName = fullName.Substring(spaceIndex + 1)
        End If
    End Sub

    Private Function IsUserIDExists() As Boolean
        Call connection()
        sql = "SELECT COUNT(*) FROM tblusers WHERE UserID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtUserID.Text.Trim())
        IsUserIDExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        cn.Close()
    End Function

    Private Function IsUsernameTaken() As Boolean
        Call connection()
        sql = "SELECT COUNT(*) FROM tblusers WHERE Username = @uname AND UserID <> @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
        cmd.Parameters.AddWithValue("@id", If(txtUserID.Text.Trim() = "", "0", txtUserID.Text.Trim()))
        IsUsernameTaken = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        cn.Close()
    End Function

    Private Function IsValidInput() As Boolean
        If String.IsNullOrWhiteSpace(txtUserID.Text) Then
            MsgBox("Fill in User ID", vbExclamation, "User Management")
            Return False
        ElseIf Not IsNumeric(txtUserID.Text.Trim()) Then
            MsgBox("User ID must be numeric", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtUsername.Text) Then
            MsgBox("Fill in Username", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MsgBox("Fill in Password", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtConfirmPassword.Text) Then
            MsgBox("Fill in Confirm Password", vbExclamation, "User Management")
            Return False
        ElseIf txtPassword.Text.Trim() <> txtConfirmPassword.Text.Trim() Then
            MsgBox("Password and Confirm Password do not match", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            MsgBox("Fill in First Name", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtLastName.Text) Then
            MsgBox("Fill in Last Name", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(cboRoles.Text) Then
            MsgBox("Select a Role", vbExclamation, "User Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(cboStatus.Text) Then
            MsgBox("Select a Status", vbExclamation, "User Management")
            Return False
        End If

        Return True
    End Function

    Private Sub dgvUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        If e.RowIndex >= 0 Then
            txtUserID.Text = dgvUsers.Rows(e.RowIndex).Cells(0).Value.ToString()
            txtUsername.Text = dgvUsers.Rows(e.RowIndex).Cells(1).Value.ToString()
            txtPassword.Text = dgvUsers.Rows(e.RowIndex).Cells(2).Value.ToString()
            txtConfirmPassword.Text = dgvUsers.Rows(e.RowIndex).Cells(2).Value.ToString()
            txtFirstName.Text = dgvUsers.Rows(e.RowIndex).Cells(3).Value.ToString()
            txtLastName.Text = dgvUsers.Rows(e.RowIndex).Cells(4).Value.ToString()
            cboRoles.Text = dgvUsers.Rows(e.RowIndex).Cells(5).Value.ToString()
            cboStatus.Text = dgvUsers.Rows(e.RowIndex).Cells(6).Value.ToString()
            _originalPassword = dgvUsers.Rows(e.RowIndex).Cells("colRealPassword").Value.ToString()

            SetEditMode()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtUserID.ReadOnly Then
            MsgBox("Cannot add: a record is currently selected for editing. Clear the form first.", vbExclamation, "User Management")
            Exit Sub
        End If

        If Not IsValidInput() Then Exit Sub
        If IsUserIDExists() Then
            MsgBox("A user with that User ID already exists.", vbExclamation, "User Management")
            Exit Sub
        End If
        If IsUsernameTaken() Then
            MsgBox("That Username is already taken.", vbExclamation, "User Management")
            Exit Sub
        End If

        Call connection()
        sql = "INSERT INTO tblusers (UserID, Username, Password, FullName, Role, Status) " &
              "VALUES (@id,@uname,@pass,@fullname,@role,@status)"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtUserID.Text.Trim())
        cmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
        cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim())
        cmd.Parameters.AddWithValue("@fullname", BuildFullName())
        cmd.Parameters.AddWithValue("@role", cboRoles.Text.Trim())
        cmd.Parameters.AddWithValue("@status", cboStatus.Text.Trim())
        cmd.ExecuteNonQuery()
        cn.Close()

        MsgBox("User added successfully!", vbInformation, "Success")

        LoadUsers()
        ClearFields()
        SetAddMode()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not txtUserID.ReadOnly Then
            MsgBox("Please select a user from the list to edit.", vbExclamation, "User Management")
            Exit Sub
        End If

        If Not IsValidInput() Then Exit Sub
        If IsUsernameTaken() Then
            MsgBox("That Username is already taken.", vbExclamation, "User Management")
            Exit Sub
        End If

        Call connection()
        sql = "UPDATE tblusers SET Username=@uname, Password=@pass, FullName=@fullname, " &
              "Role=@role, Status=@status WHERE UserID=@id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtUserID.Text.Trim())
        Dim passwordToSave As String = txtPassword.Text.Trim()
        If passwordToSave = PasswordHelper.GetDisplayHash(_originalPassword) Then
            passwordToSave = _originalPassword
        End If

        cmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
        cmd.Parameters.AddWithValue("@pass", passwordToSave)
        cmd.Parameters.AddWithValue("@fullname", BuildFullName())
        cmd.Parameters.AddWithValue("@role", cboRoles.Text.Trim())
        cmd.Parameters.AddWithValue("@status", cboStatus.Text.Trim())

        If cmd.ExecuteNonQuery() = 0 Then
            MsgBox("No user record found with the specified User ID.", vbInformation, "Update Failed")
        Else
            MsgBox("User details updated successfully!", vbInformation, "Success")
        End If
        cn.Close()

        LoadUsers()
        ClearFields()
        SetAddMode()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtUserID.Text) Then
            MsgBox("Please select or enter a User ID to delete.", vbInformation, "Validation Error")
            Exit Sub
        End If

        If txtUserID.Text.Trim() = CurrentUser.UserID.ToString() Then
            MsgBox("You cannot deactivate your own account while logged in.", vbExclamation, "User Management")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to deactivate this user account?", vbYesNo + vbQuestion, "Confirm Deactivation") <> vbYes Then
            Exit Sub
        End If

        Call connection()
        sql = "UPDATE tblusers SET Status = 'Inactive' WHERE UserID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtUserID.Text.Trim())

        If cmd.ExecuteNonQuery() > 0 Then
            MsgBox("User account successfully deactivated.", vbInformation, "Success")
        Else
            MsgBox("No matching User ID found.", vbExclamation, "Record Not Found")
        End If
        cn.Close()

        LoadUsers()
        ClearFields()
        SetAddMode()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Call connection()
        sql = "SELECT * FROM tblusers WHERE UserID LIKE @search OR Username LIKE @search"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
        dr = cmd.ExecuteReader()

        dgvUsers.Rows.Clear()
        While dr.Read()
            Dim fullName As String = dr("FullName").ToString()
            Dim firstName As String = ""
            Dim lastName As String = ""
            SplitFullName(fullName, firstName, lastName)

            dgvUsers.Rows.Add(
                dr("UserID").ToString(),
                dr("Username").ToString(),
                PasswordHelper.GetDisplayHash(dr("Password").ToString()),
                firstName,
                lastName,
                dr("Role").ToString(),
                dr("Status").ToString(),
                dr("Password").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
        SetAddMode()
    End Sub
End Class