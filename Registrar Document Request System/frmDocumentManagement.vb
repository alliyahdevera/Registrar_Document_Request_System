Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class frmDocumentManagement
    Private Sub frmDocumentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.Role <> "Administrator" Then
            btnUserManagement.Visible = False
        End If
        LoadDocuments()
    End Sub

    Private Sub LoadDocuments()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim da As New MySqlDataAdapter("SELECT * FROM tbldocuments", conn)
            Dim dt As New DataTable()
            da.Fill(dt)
            dvgDocument.AutoGenerateColumns = False
            DocumentID.DataPropertyName = "DocumentID"
            DocumentName.DataPropertyName = "DocumentName"
            Description.DataPropertyName = "Description"
            Fee.DataPropertyName = "Fee"
            Status.DataPropertyName = "Status"
            dvgDocument.DataSource = dt
        End Using
    End Sub

    Private Sub btnAddStudent_Click(sender As Object, e As EventArgs) Handles btnAddStudent.Click
        If Not IsNumeric(txtFee.Text.Trim()) Then
            MsgBox("Fee must be a valid number.", vbExclamation, "Validation Error")
            Exit Sub
        End If
        Dim fee As Decimal = CDec(txtFee.Text.Trim())

        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "INSERT INTO tbldocuments (DocumentName, Description, Fee, Status) " & "VALUES (@name,@desc,@fee,@status)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
            cmd.Parameters.AddWithValue("@fee", fee)
            cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())
            cmd.ExecuteNonQuery()
        End Using
        LoadDocuments()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not IsNumeric(txtFee.Text.Trim()) Then
            MsgBox("Fee must be a valid number.", vbExclamation, "Validation Error")
            Exit Sub
        End If
        Dim fee As Decimal = CDec(txtFee.Text.Trim())

        Dim query As String = "UPDATE tbldocuments SET " & "DocumentName = @name, " & "Description = @desc, " & "Fee = @fee, " & "Status = @status " & "WHERE DocumentID = @id"

        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
                cmd.Parameters.AddWithValue("@fee", fee)
                cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    MsgBox("No document found with the specified Document ID.", vbInformation, "Update Failed")
                Else
                    MsgBox("Document details updated successfully!", vbInformation, "Success")
                End If
            End Using
        End Using

        LoadDocuments()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtDocumentID.Text) Then
            MsgBox("Please select or enter a Document ID to delete.", vbInformation, "Validation Error")
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this document record?", vbInformation, "Confirm Deactivation") <> DialogResult.Yes Then
            Exit Sub
        End If

        Dim query As String = "UPDATE tbldocuments SET Status = 'Inactive' WHERE DocumentID = @id"

        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Document record successfully deleted.", vbInformation, "Success")
                Else
                    MsgBox("No matching Document ID found.", vbExclamation, "Record Not Found")
                End If
            End Using
        End Using

        LoadDocuments()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "SELECT * FROM tbldocuments WHERE DocumentID LIKE @kw OR DocumentName LIKE @kw"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@kw", "%" & txtsearch.Text.Trim() & "%")
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dvgDocument.AutoGenerateColumns = False
            DocumentID.DataPropertyName = "DocumentID"
            DocumentName.DataPropertyName = "DocumentName"
            Description.DataPropertyName = "Description"
            Fee.DataPropertyName = "Fee"
            Status.DataPropertyName = "Status"
            dvgDocument.DataSource = dt
        End Using
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

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
        Me.Hide()
    End Sub



End Class