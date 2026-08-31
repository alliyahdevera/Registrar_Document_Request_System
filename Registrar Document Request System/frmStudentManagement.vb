Imports MySql.Data.MySqlClient

Public Class frmStudentManagement
    Private Sub LoadStudents()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim da As New MySqlDataAdapter("SELECT * FROM tblstudents", conn)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvStudents.DataSource = dt
        End Using
    End Sub
    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "SELECT * FROM tblstudents WHERE StudentID LIKE @kw OR LastName LIKE @kw OR FirstName LIKE @kw"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@kw", "%" & txtSearch.Text.Trim() & "%")
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvStudents.DataSource = dt
        End Using
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "INSERT INTO tblstudents (StudentID, LRN, LastName, FirstName, MiddleName, Course, YearLevel, Section, ContactNo) " & "VALUES (@id,@lrn,@ln,@fn,@mn,@course,@year,@section,@contact)"
            Dim cmd As New MySqlCommand(query, conn)
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
        End Using
        LoadStudents()
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim query As String = "UPDATE tblstudents SET " & "LRN = @lrn, " & "LastName = @ln, " & "FirstName = @fn, " & "MiddleName = @mn, " & "Course = @course, " & "YearLevel = @year, " & "Section = @section, " &
            "ContactNo = @contact " & "WHERE StudentID = @id"

        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())
                cmd.Parameters.AddWithValue("@lrn", txtLRN.Text.Trim())
                cmd.Parameters.AddWithValue("@ln", txtLastName.Text.Trim())
                cmd.Parameters.AddWithValue("@fn", txtFirstName.Text.Trim())
                cmd.Parameters.AddWithValue("@mn", txtMiddleName.Text.Trim())
                cmd.Parameters.AddWithValue("@course", cboCourse.Text.Trim())
                cmd.Parameters.AddWithValue("@year", cboYearLevel.Text.Trim())
                cmd.Parameters.AddWithValue("@section", txtSection.Text.Trim())
                cmd.Parameters.AddWithValue("@contact", txtContactNo.Text.Trim())

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    MsgBox("No student record found with the specified Student ID.", vbInformation, "Update Failed")
                Else
                    MsgBox("Student details updated successfully!", vbInformation, "Success")
                End If
            End Using
        End Using

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

        Dim query As String = "UPDATE tblstudents SET Status = 'Inactive' WHERE StudentID = @id"

        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Student record successfully deleted.", vbInformation, "Success")
                Else
                    MsgBox("No matching Student ID found.", vbExclamation, "Record Not Found")
                End If
            End Using
        End Using

        LoadStudents()
    End Sub

End Class