Imports MySql.Data.MySqlClient

Public Class frmStudentManagement
    Private Sub LoadStudents()
        Call connection()
        sql = "SELECT * FROM tblstudents"
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
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
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

        If MsgBox("Are you sure you want to delete this student record?", vbInformation, "Confirm Deactivation") <> DialogResult.Yes Then
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
        sql = "Select * from tblstudents where studentID like '%" & txtSearch.Text & "%' or lastname like '%" & txtSearch.Text & "%'"
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