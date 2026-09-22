Imports MySql.Data.MySqlClient

Public Class frmDocumentManagement

    Private Sub frmDocumentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.Role <> "Administrator" Then
            btnUserManagement.Visible = False
        End If

        ' Set bottom footer values
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        LoadDocuments()
    End Sub

    ' Real-time date/time clock event
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    Private Sub LoadDocuments()
        Call connection()
        sql = "SELECT * FROM tbldocuments"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        dgvDocument.Rows.Clear()
        While dr.Read()
            dgvDocument.Rows.Add(
                dr("DocumentID").ToString(),
                dr("DocumentName").ToString(),
                dr("Description").ToString(),
                dr("Fee").ToString(),
                dr("Status").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub

    Private Function IsDocumentIDExists() As Boolean
        Call connection()
        sql = "SELECT COUNT(*) FROM tbldocuments WHERE DocumentID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
        IsDocumentIDExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        cn.Close()
    End Function

    Private Function IsDocumentNameExists(Optional currentDocumentId As String = "") As Boolean
        Call connection()
        If String.IsNullOrEmpty(currentDocumentId) Then
            sql = "SELECT COUNT(*) FROM tbldocuments WHERE DocumentName = @name"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
        Else
            sql = "SELECT COUNT(*) FROM tbldocuments WHERE DocumentName = @name AND DocumentID <> @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@id", currentDocumentId)
        End If

        IsDocumentNameExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        cn.Close()
    End Function

    Private Function IsValidInput(Optional isEditMode As Boolean = False) As Boolean
        If String.IsNullOrWhiteSpace(txtDocumentID.Text) Then
            MsgBox("Fill in Document ID", vbExclamation, "Document Management")
            txtDocumentID.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtName.Text) Then
            MsgBox("Fill in Document Name", vbExclamation, "Document Management")
            txtName.Focus()
            Return False
        ElseIf IsDocumentNameExists(If(isEditMode, txtDocumentID.Text.Trim(), "")) Then
            MsgBox("Document Name already exists", vbExclamation, "Document Management")
            txtName.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtDescription.Text) Then
            MsgBox("Fill in Description", vbExclamation, "Document Management")
            txtDescription.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtFee.Text) Then
            MsgBox("Fill in Fee", vbExclamation, "Document Management")
            txtFee.Focus()
            Return False
        ElseIf Not IsNumeric(txtFee.Text.Trim()) Then
            MsgBox("Fee must be a valid numeric amount", vbExclamation, "Document Management")
            txtFee.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtStatus.Text) Then
            MsgBox("Fill in Status", vbExclamation, "Document Management")
            txtStatus.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub dgvDocument_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDocument.CellClick
        If e.RowIndex >= 0 Then
            txtDocumentID.Text = dgvDocument.Rows(e.RowIndex).Cells(0).Value.ToString()
            txtName.Text = dgvDocument.Rows(e.RowIndex).Cells(1).Value.ToString()
            txtDescription.Text = dgvDocument.Rows(e.RowIndex).Cells(2).Value.ToString()
            txtFee.Text = dgvDocument.Rows(e.RowIndex).Cells(3).Value.ToString()
            txtStatus.Text = dgvDocument.Rows(e.RowIndex).Cells(4).Value.ToString()
        End If
    End Sub

    Private Sub btnAddDocument_Click(sender As Object, e As EventArgs) Handles btnAddDocument.Click
        If Not IsValidInput(isEditMode:=False) Then Exit Sub

        If IsDocumentIDExists() Then
            MsgBox("A document with that Document ID already exists.", vbExclamation, "Document Management")
            Exit Sub
        End If

        Call connection()
        sql = "INSERT INTO tbldocuments (DocumentID, DocumentName, Description, Fee, Status) VALUES (@id, @name, @desc, @fee, @status)"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
        cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
        cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())
        cmd.ExecuteNonQuery()
        cn.Close()

        LoadDocuments()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not IsValidInput(isEditMode:=True) Then Exit Sub

        Call connection()
        sql = "UPDATE tbldocuments SET DocumentName=@name, Description=@desc, Fee=@fee, Status=@status WHERE DocumentID=@id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
        cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
        cmd.Parameters.AddWithValue("@status", txtStatus.Text.Trim())

        If cmd.ExecuteNonQuery() = 0 Then
            MsgBox("No document found with the specified Document ID.", vbExclamation, "Record Not Found")
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

        If MsgBox("Are you sure you want to delete this document record?", vbYesNo + vbQuestion, "Confirm Deactivation") <> MsgBoxResult.Yes Then
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

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Call connection()
        sql = "SELECT * FROM tbldocuments WHERE DocumentID LIKE @search OR DocumentName LIKE @search"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@search", "%" & txtsearch.Text.Trim() & "%")
        dr = cmd.ExecuteReader()

        dgvDocument.Rows.Clear()
        While dr.Read()
            dgvDocument.Rows.Add(
                dr("DocumentID").ToString(),
                dr("DocumentName").ToString(),
                dr("Description").ToString(),
                dr("Fee").ToString(),
                dr("Status").ToString())
        End While

        dr.Close()
        cn.Close()
    End Sub

    ' Navigation Handlers
    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        frmMainMenu.Show()
        Me.Hide()
    End Sub

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnReqList_Click(sender As Object, e As EventArgs) Handles btnReqList.Click
        frmRequestList.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentRequests_Click(sender As Object, e As EventArgs) Handles btnDocumentRequests.Click
        frmNewRequest.Show()
        Me.Hide()
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

End Class