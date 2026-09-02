Imports MySql.Data.MySqlClient

Public Class frmStudentManagement
    Private Sub LoadStudents()
        Call connection()
        sql = "SELECT * FROM tblstudents WHERE Status = 'Active'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dgvStudents.Rows.Clear()
        While dr.Read()
            dgvStudents.Rows.Add(
                dr("StudentID").ToString(),
                dr("LRN").ToString(),
                dr("LastName").ToString(),
                dr("FirstName").ToString(),
                dr("MiddleName").ToString(),
                dr("Course").ToString(),
                dr("YearLevel").ToString(),
                dr("Section").ToString(),
                dr("ContactNo").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub
    Private Function IsStudentIDExists() As Boolean
        Call connection()
        sql = "SELECT COUNT(*) FROM tblstudents WHERE StudentID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())
        IsStudentIDExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        cn.Close()
    End Function
    Private Function IsValidInput() As Boolean
        If String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            MsgBox("Fill in Student ID", vbExclamation, "Student Management")
            Return False
        ElseIf txtStudentID.Text.Trim().Length <> 7 OrElse txtStudentID.Text.Trim().Chars(4) <> "-"c Then
            MsgBox("Student ID must be exactly 7 characters with a hyphen (-) as the 5th character", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtLRN.Text) Then
            MsgBox("Fill in LRN", vbExclamation, "Student Management")
            Return False
        ElseIf txtLRN.Text.Trim().Length <> 12 OrElse Not IsNumeric(txtLRN.Text.Trim()) Then
            MsgBox("LRN must be exactly 12 digits", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtLastName.Text) Then
            MsgBox("Fill in Last Name", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            MsgBox("Fill in First Name", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(cboCourse.Text) Then
            MsgBox("Fill in Course", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(cboYearLevel.Text) Then
            MsgBox("Fill in Year Level", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtSection.Text) Then
            MsgBox("Fill in Section", vbExclamation, "Student Management")
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtContactNo.Text) Then
            MsgBox("Fill in Contact No.", vbExclamation, "Student Management")
            Return False
        ElseIf txtContactNo.Text.Trim().Length <> 11 OrElse Not IsNumeric(txtContactNo.Text.Trim()) Then
            MsgBox("Contact No. must be exactly 11 digits", vbExclamation, "Student Management")
            Return False
        End If

        Return True
    End Function
    Private Sub dgvStudents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudents.CellClick
        If e.RowIndex >= 0 Then
            txtStudentID.Text = dgvStudents.Rows(e.RowIndex).Cells(0).Value.ToString()
            txtLRN.Text = dgvStudents.Rows(e.RowIndex).Cells(1).Value.ToString()
            txtLastName.Text = dgvStudents.Rows(e.RowIndex).Cells(2).Value.ToString()
            txtFirstName.Text = dgvStudents.Rows(e.RowIndex).Cells(3).Value.ToString()
            txtMiddleName.Text = dgvStudents.Rows(e.RowIndex).Cells(4).Value.ToString()
            cboCourse.Text = dgvStudents.Rows(e.RowIndex).Cells(5).Value.ToString()
            cboYearLevel.Text = dgvStudents.Rows(e.RowIndex).Cells(6).Value.ToString()
            txtSection.Text = dgvStudents.Rows(e.RowIndex).Cells(7).Value.ToString()
            txtContactNo.Text = dgvStudents.Rows(e.RowIndex).Cells(8).Value.ToString()
        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not IsValidInput() Then Exit Sub
        If Not IsValidInput() Then Exit Sub
        If IsStudentIDExists() Then
            MsgBox("A student with that Student ID already exists.", vbExclamation, "Student Management")
            Exit Sub
        End If
        Call connection()
        sql = "INSERT INTO tblstudents (StudentID, LRN, LastName, FirstName, MiddleName, Course, YearLevel, Section, ContactNo) " &
                  "VALUES (@id,@lrn,@ln,@fn,@mn,@course,@year,@section,@contact)"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())
        cmd.Parameters.AddWithValue("@lrn", txtLRN.Text.Trim())
        cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim())
        cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim())
        cmd.Parameters.AddWithValue("@mn", txtMiddleName.Text.Trim())
        cmd.Parameters.AddWithValue("@course", cboCourse.Text.Trim())
        cmd.Parameters.AddWithValue("@year", cboYearLevel.Text.Trim())
        cmd.Parameters.AddWithValue("@section", txtSection.Text.Trim())
        cmd.Parameters.AddWithValue("@contact", txtContactNo.Text.Trim())
        cmd.ExecuteNonQuery()
        cn.Close()
        LoadStudents()
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not IsValidInput() Then Exit Sub
        Call connection()
        sql = "UPDATE tblstudents SET LRN=@lrn, LastName=@ln, FirstName=@fn, MiddleName=@mn, " &
                  "Course=@course, YearLevel=@year, Section=@section, ContactNo=@contact WHERE StudentID=@id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())
        cmd.Parameters.AddWithValue("@lrn", txtLRN.Text.Trim())
        cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim())
        cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim())
        cmd.Parameters.AddWithValue("@mn", txtMiddleName.Text.Trim())
        cmd.Parameters.AddWithValue("@course", cboCourse.Text.Trim())
        cmd.Parameters.AddWithValue("@year", cboYearLevel.Text.Trim())
        cmd.Parameters.AddWithValue("@section", txtSection.Text.Trim())
        cmd.Parameters.AddWithValue("@contact", txtContactNo.Text.Trim())

        If cmd.ExecuteNonQuery() = 0 Then
            MsgBox("No student record found with the specified Student ID.", vbInformation, "Update Failed")
        Else
            MsgBox("Student details updated successfully!", vbInformation, "Success")
        End If
        cn.Close()

        LoadStudents()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            MsgBox("Please select or enter a Student ID to delete.", vbInformation, "Validation Error")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this student record?", vbYesNo + vbQuestion, "Confirm Deactivation") <> vbYes Then
            Exit Sub
        End If

        Call connection()
        sql = "UPDATE tblstudents SET Status = 'Inactive' WHERE StudentID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())

            If cmd.ExecuteNonQuery() > 0 Then
                MsgBox("Student record successfully deleted.", vbInformation, "Success")
            Else
                MsgBox("No matching Student ID found.", vbExclamation, "Record Not Found")
            End If
            cn.Close()
        LoadStudents()
    End Sub

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        frmMainMenu.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentManagement_Click(sender As Object, e As EventArgs) Handles btnDocumentManagement.Click
        frmDocumentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub frmStudentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Call connection()
        sql = "Select * from tblstudents where Status = 'Active' and (studentID like '%" & txtSearch.Text & "%' or lastname like '%" & txtSearch.Text & "%')"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dgvStudents.Rows.Clear()
        While dr.Read()
            dgvStudents.Rows.Add(
                dr("StudentID").ToString(),
                dr("LRN").ToString(),
                dr("LastName").ToString(),
                dr("FirstName").ToString(),
                dr("MiddleName").ToString(),
                dr("Course").ToString(),
                dr("YearLevel").ToString(),
                dr("Section").ToString(),
                dr("ContactNo").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub
End Class