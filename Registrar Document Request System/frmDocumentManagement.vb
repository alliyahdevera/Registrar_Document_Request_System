Imports MySql.Data.MySqlClient

Public Class frmDocumentManagement

    ' --- TASK 17: GRID PAGER INITIALIZATION ---
    Private pager As GridPager

    Private Function EnsureAdminAccess() As Boolean
        If Not CurrentUser.IsAdmin Then
            MsgBox("You don't have permission to access Document Management.", vbExclamation, "Access Denied")
            Me.BeginInvoke(New MethodInvoker(Sub() frmMainMenu.OpenDashboard()))
            Return False
        End If
        Return True
    End Function

    Private Sub frmDocumentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- TASK 17: Hook pager to dgvDocument ---
        pager = New GridPager(dgvDocument)

        If Not EnsureAdminAccess() Then Exit Sub

        RefreshUserSession()

        dgvDocument.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDocument.MultiSelect = False

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList

        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        LoadDocuments()

        SetAddMode()
    End Sub

    Private Sub frmDocumentManagement_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
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
        txtDocumentID.ReadOnly = True
        txtDocumentID.BackColor = Color.Gainsboro
        SetNextDocumentID()

        cboStatus.Text = "Active"
        cboStatus.Enabled = False
    End Sub

    Private Sub SetEditMode()
        txtDocumentID.ReadOnly = True
        txtDocumentID.BackColor = Color.Gainsboro

        cboStatus.Enabled = True
    End Sub

    Private Sub SetNextDocumentID()
        Try
            Call connection()
            sql = "SELECT IFNULL(MAX(CAST(DocumentID AS UNSIGNED)), 0) + 1 FROM tbldocuments"
            cmd = New MySqlCommand(sql, cn)
            txtDocumentID.Text = cmd.ExecuteScalar().ToString()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
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
        ElseIf CDec(txtFee.Text.Trim()) < 0 Then
            MsgBox("Fee cannot be a negative number", vbExclamation, "Document Management")
            txtFee.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(cboStatus.Text) Then
            MsgBox("Select a Status", vbExclamation, "Document Management")
            cboStatus.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub dgvDocument_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDocument.CellClick
        If e.RowIndex < 0 Then Exit Sub

        If e.RowIndex = dgvDocument.NewRowIndex OrElse dgvDocument.Rows(e.RowIndex).IsNewRow Then
            ClearFields()
            SetAddMode()
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvDocument.Rows(e.RowIndex)
        txtDocumentID.Text = If(row.Cells(0).Value IsNot Nothing, row.Cells(0).Value.ToString(), "")
        txtName.Text = If(row.Cells(1).Value IsNot Nothing, row.Cells(1).Value.ToString(), "")
        txtDescription.Text = If(row.Cells(2).Value IsNot Nothing, row.Cells(2).Value.ToString(), "")
        txtFee.Text = If(row.Cells(3).Value IsNot Nothing, row.Cells(3).Value.ToString(), "")
        cboStatus.Text = If(row.Cells(4).Value IsNot Nothing, row.Cells(4).Value.ToString(), "")

        SetEditMode()
    End Sub

    Private Sub btnAddDocument_Click(sender As Object, e As EventArgs) Handles btnAddDocument.Click
        If Not txtDocumentID.ReadOnly Then
            MsgBox("Cannot add: a record is currently selected for editing. Clear the form first.", vbExclamation, "Document Management")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtDocumentID.Text) Then
            SetNextDocumentID()
        End If

        If Not IsValidInput(isEditMode:=False) Then Exit Sub

        If Not ConfirmAction("Add this document record?", "Confirm Add") Then Exit Sub

        Call connection()
        sql = "INSERT INTO tbldocuments (DocumentID, DocumentName, Description, Fee, Status) VALUES (@id, @name, @desc, @fee, @status)"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
        cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
        cmd.Parameters.AddWithValue("@status", cboStatus.Text.Trim())
        cmd.ExecuteNonQuery()
        cn.Close()

        MsgBox("Document added successfully!", vbInformation, "Success")

        ClearFields()
        LoadDocuments()
        SetAddMode()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not txtDocumentID.ReadOnly OrElse Not cboStatus.Enabled Then
            MsgBox("Please select a document from the list to edit.", vbExclamation, "Document Management")
            Exit Sub
        End If

        If Not IsValidInput(isEditMode:=True) Then Exit Sub

        If Not ConfirmAction("Save changes to this document record?", "Confirm Edit") Then Exit Sub

        Call connection()
        sql = "UPDATE tbldocuments SET DocumentName=@name, Description=@desc, Fee=@fee, Status=@status WHERE DocumentID=@id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtDocumentID.Text.Trim())
        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
        cmd.Parameters.AddWithValue("@fee", CDec(txtFee.Text.Trim()))
        cmd.Parameters.AddWithValue("@status", cboStatus.Text.Trim())

        If cmd.ExecuteNonQuery() = 0 Then
            MsgBox("No document found with the specified Document ID.", vbExclamation, "Record Not Found")
        Else
            MsgBox("Document details updated successfully!", vbInformation, "Success")
        End If
        cn.Close()

        ClearFields()
        LoadDocuments()
        SetAddMode()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtDocumentID.Text) Then
            MsgBox("Please select a Document to delete.", vbInformation, "Validation Error")
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
            MsgBox("Document record successfully set to Inactive.", vbInformation, "Success")
        Else
            MsgBox("No matching Document ID found.", vbExclamation, "Record Not Found")
        End If
        cn.Close()

        ClearFields()
        LoadDocuments()
        SetAddMode()
    End Sub

    Private Sub ClearFields()
        txtDocumentID.Clear()
        txtName.Clear()
        txtDescription.Clear()
        txtFee.Clear()
        cboStatus.SelectedIndex = -1
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If Not String.IsNullOrWhiteSpace(txtName.Text & txtDescription.Text & txtFee.Text) Then
            If Not ConfirmAction("Clear all fields?", "Confirm Clear") Then Exit Sub
        End If

        ClearFields()
        SetAddMode()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ' --- TASK 17: Reset pager to Page 1 on search ---
        pager.FirstPage()

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

    Private Sub txtFee_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFee.KeyPress
        If Char.IsControl(e.KeyChar) Then Exit Sub

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
            Exit Sub
        End If

        If e.KeyChar = "."c AndAlso txtFee.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

End Class