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
        Call connection()
        sql = "SELECT * FROM tbldocuments"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dvgDocument.Rows.Clear()
        While dr.Read()
            dvgDocument.Rows.Add(
                dr("DocumentID").ToString(),
                dr("DocumentName").ToString(),
                dr("Description").ToString(),
                dr("Fee").ToString(),
                dr("Status").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not IsNumeric(txtFee.Text.Trim()) Then
            MsgBox("Fee must be a valid number.", vbExclamation, "Validation Error")
            Exit Sub
        End If

        Call connection()
        sql = "UPDATE tbldocuments SET DocumentName=@name, Description=@desc, Fee=@fee, Status=@status WHERE DocumentID=@id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
            cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
            cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())

            If cmd.ExecuteNonQuery() = 0 Then
                MsgBox("No document found with the specified Document ID.", vbInformation, "Update Failed")
            Else
                MsgBox("Document details updated successfully!", vbInformation, "Success")
            End If
            cn.Close()

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

        Call connection()
        sql = "UPDATE tbldocuments SET Status = 'Inactive' WHERE DocumentID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())

        If cmd.ExecuteNonQuery() > 0 Then
            MsgBox("Document record successfully deleted.", vbInformation, "Success")
        Else
            MsgBox("No matching Document ID found.", vbExclamation, "Record Not Found")
        End If
        cn.Close()

        LoadDocuments()
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

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        frmMainMenu.Show()
        Me.Hide()
    End Sub

    Private Sub btnStudentManagement_Click_1(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
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

    Private Sub btnAddDocument_Click(sender As Object, e As EventArgs) Handles btnAddDocument.Click
        If Not IsNumeric(txtFee.Text.Trim()) Then
            MsgBox("Fee must be a valid number.", vbExclamation, "Validation Error")
            Exit Sub
        End If

        Call connection()
        sql = "INSERT INTO tbldocuments (DocumentName, Description, Fee, Status) VALUES (@name,@desc,@fee,@status)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
            cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
            cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())
            cmd.ExecuteNonQuery()
        cn.Close()
        LoadDocuments()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Call connection()
        sql = "Select * from tbldocuments where documentID like '%" & txtsearch.Text & "%' or documentname like '%" & txtsearch.Text & "%'"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dvgDocument.Rows.Clear()
        While dr.Read()
            dvgDocument.Rows.Add(
            dr("DocumentID").ToString(),
            dr("DocumentName").ToString(),
            dr("Description").ToString(),
            dr("Fee").ToString(),
            dr("Status").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub
End Class