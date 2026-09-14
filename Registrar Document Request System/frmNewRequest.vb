Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Cmp

Public Class frmNewRequest
    Private currentDocFee As Decimal = 0

    Private Sub frmNewRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.Role <> "Administrator" Then
            btnUserManagement.Visible = False
        End If

        LoadDocumentsCombo()
        LoadCourseCombo()
        LoadYearLevelCombo()

        dtpRequestDate.Value = Today

        txtCreatedBy.Text = CurrentUser.FullName
        txtCreatedBy.ReadOnly = True

        ClearDocumentEntryFields()
        RecalculateTotal()
    End Sub
    Private Sub frmNewRequest_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        GenerateRequestNo()
    End Sub

    Private Sub LoadDocumentsCombo()
        Call connection()
        sql = "SELECT DocumentID, DocumentName, Fee FROM tbldocuments WHERE Status = 'Active' ORDER BY DocumentName"
        Dim da As New MySqlDataAdapter(sql, cn)
        Dim dt As New DataTable()
        da.Fill(dt)
        cn.Close()

        cboDocument.DataSource = dt
        cboDocument.DisplayMember = "DocumentName"
        cboDocument.ValueMember = "DocumentID"
        cboDocument.SelectedIndex = -1
    End Sub

    Private Sub LoadCourseCombo()
        cboCourse.Items.Clear()
        cboCourse.Items.Add("Bachelor of Science in Information Technology")
        cboCourse.Items.Add("Bachelor of Science in Computer Science")
        cboCourse.Items.Add("Bachelor of Science in Business Administration")
    End Sub

    Private Sub LoadYearLevelCombo()
        cboYearLevel.Items.Clear()
        cboYearLevel.Items.AddRange(New Object() {"1st Year", "2nd Year", "3rd Year", "4th Year"})
    End Sub
    Private Sub GenerateRequestNo()
        Dim year As String = Now.Year.ToString()
        Dim nextNumber As Integer = 1

        Call connection()
        sql = "SELECT MAX(CAST(SUBSTRING_INDEX(RequestNo, '-', -1) AS UNSIGNED)) " &
              "FROM tblrequest WHERE RequestNo LIKE @pattern"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@pattern", "REQ-" & year & "-%")
        Dim result As Object = cmd.ExecuteScalar()
        cn.Close()

        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
            nextNumber = Convert.ToInt32(result) + 1
        End If

        txtRequestNo.Text = "REQ-" & year & "-" & nextNumber.ToString("000")
        txtRequestNo.ReadOnly = True
    End Sub

    Private Sub picSearchStudent_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        SearchStudent()
    End Sub

    Private Sub txtStudentID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStudentID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchStudent()
        End If
    End Sub

    Private Sub SearchStudent()
        If String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            MsgBox("Please enter a Student ID to search.", vbExclamation, "New Document Request")
            Exit Sub
        End If

        Call connection()
        sql = "SELECT * FROM tblstudents WHERE StudentID = @id"
        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@id", txtStudentID.Text.Trim())
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            txtFirstName.Text = dr("FirstName").ToString()
            txtMiddleName.Text = dr("MiddleName").ToString()
            txtLastName.Text = dr("LastName").ToString()
            cboCourse.Text = dr("Course").ToString()
            cboYearLevel.Text = dr("YearLevel").ToString()
        Else
            MsgBox("No student found with that Student ID.", vbExclamation, "New Document Request")
            ClearStudentFields()
        End If

        dr.Close()
        cn.Close()
    End Sub

    Private Sub ClearStudentFields()
        txtFirstName.Clear()
        txtMiddleName.Clear()
        txtLastName.Clear()
        cboCourse.SelectedIndex = -1
        cboYearLevel.SelectedIndex = -1
    End Sub

    Private Sub cboDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocument.SelectedIndexChanged
        If cboDocument.SelectedIndex = -1 Then
            currentDocFee = 0
            txtFee.Clear()
        Else
            Dim row As DataRowView = CType(cboDocument.SelectedItem, DataRowView)
            currentDocFee = Convert.ToDecimal(row("Fee"))
            txtFee.Text = currentDocFee.ToString("N2")
        End If
        txtQuantity.Text = "1"
        CalculateSubtotal()
    End Sub

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged
        CalculateSubtotal()
    End Sub

    Private Sub CalculateSubtotal()
        If cboDocument.SelectedIndex = -1 Then
            txtSubtotal.Clear()
            Exit Sub
        End If

        Dim qty As Integer
        If Integer.TryParse(txtQuantity.Text.Trim(), qty) AndAlso qty > 0 Then
            Dim subtotal As Decimal = currentDocFee * qty
            txtSubtotal.Text = subtotal.ToString("N2")
        Else
            txtSubtotal.Clear()
        End If
    End Sub

    Private Sub btnAddToList_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cboDocument.SelectedIndex = -1 Then
            MsgBox("Please select a document.", vbExclamation, "New Document Request")
            Exit Sub
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtQuantity.Text.Trim(), qty) OrElse qty <= 0 Then
            MsgBox("Please enter a valid quantity.", vbExclamation, "New Document Request")
            txtQuantity.Focus()
            Exit Sub
        End If

        Dim docRow As DataRowView = CType(cboDocument.SelectedItem, DataRowView)
        Dim docId As String = docRow("DocumentID").ToString()
        Dim docName As String = docRow("DocumentName").ToString()
        Dim fee As Decimal = currentDocFee
        Dim subtotal As Decimal = fee * qty

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For

            If row.Cells("DocumentID").Value IsNot Nothing AndAlso row.Cells("DocumentID").Value.ToString() = docId Then
                Dim newQty As Integer = Convert.ToInt32(row.Cells("Quantity").Value) + qty
                row.Cells("Quantity").Value = newQty
                row.Cells("Subtotal").Value = (fee * newQty).ToString("N2")
                RecalculateTotal()
                ClearDocumentEntryFields()
                Exit Sub
            End If
        Next

        Dim rowIndex As Integer = DataGridView1.Rows.Add()
        With DataGridView1.Rows(rowIndex)
            .Cells("DocumentID").Value = docId
            .Cells("DocumentName").Value = docName
            .Cells("Fee").Value = fee.ToString("N2")
            .Cells("Quantity").Value = qty
            .Cells("Subtotal").Value = subtotal.ToString("N2")
            .Cells("Action").Value = "Remove"
        End With

        RecalculateTotal()
        ClearDocumentEntryFields()
    End Sub

    Private Sub ClearDocumentEntryFields()
        cboDocument.SelectedIndex = -1
        txtQuantity.Clear()
        txtFee.Clear()
        txtSubtotal.Clear()
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If DataGridView1.Rows(e.RowIndex).IsNewRow Then Exit Sub

        If DataGridView1.Columns(e.ColumnIndex).Name = "Action" Then
            DataGridView1.Rows.RemoveAt(e.RowIndex)
            RecalculateTotal()
        End If
    End Sub

    Private Sub RecalculateTotal()
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For

            If row.Cells("Subtotal").Value IsNot Nothing Then
                Dim lineTotal As Decimal
                If Decimal.TryParse(row.Cells("Subtotal").Value.ToString(), lineTotal) Then
                    total += lineTotal
                End If
            End If
        Next
        txtTotalAmount.Text = total.ToString("N2")
    End Sub

    Private Function IsValidRequest() As Boolean
        If String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            MsgBox("Please search and select a student.", vbExclamation, "New Document Request")
            txtStudentID.Focus()
            Return False
        ElseIf String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            MsgBox("Student not found. Please search a valid Student ID.", vbExclamation, "New Document Request")
            txtStudentID.Focus()
            Return False
        ElseIf DataGridView1.Rows.Count = 0 OrElse
               (DataGridView1.Rows.Count = 1 AndAlso DataGridView1.Rows(0).IsNewRow) Then
            MsgBox("Please add at least one document to the request.", vbExclamation, "New Document Request")
            Return False
        ElseIf cboPaymentStatus.SelectedIndex = -1 Then
            MsgBox("Please select a Payment Status.", vbExclamation, "New Document Request")
            Return False
        ElseIf cboStatus.SelectedIndex = -1 Then
            MsgBox("Please select a Request Status.", vbExclamation, "New Document Request")
            Return False
        End If

        Return True
    End Function

    Private Sub btnSaveRequest_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not IsValidRequest() Then Exit Sub

        If MsgBox("Save this document request?", vbYesNo + vbQuestion, "Confirm Save") <> MsgBoxResult.Yes Then
            Exit Sub
        End If

        Call connection()

        Try
            sql = "INSERT INTO tblrequest (RequestNo, StudentID, RequestDate, TotalAmount, PaymentStatus, Status, CreatedBy) " &
                  "VALUES (@reqno, @studid, @reqdate, @total, @paystat, @status, @createdby)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@reqno", txtRequestNo.Text.Trim())
            cmd.Parameters.AddWithValue("@studid", txtStudentID.Text.Trim())
            cmd.Parameters.AddWithValue("@reqdate", dtpRequestDate.Value.Date)
            cmd.Parameters.AddWithValue("@total", CDec(txtTotalAmount.Text))
            cmd.Parameters.AddWithValue("@paystat", cboPaymentStatus.Text)
            cmd.Parameters.AddWithValue("@status", cboStatus.Text)
            cmd.Parameters.AddWithValue("@createdby", CurrentUser.UserID)
            cmd.ExecuteNonQuery()

            Dim newRequestId As Long = cmd.LastInsertedId

            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.IsNewRow Then Continue For

                sql = "INSERT INTO tblrequestdetails (RequestID, DocumentID, Quantity, Amount, SubTotal) " &
                      "VALUES (@reqid, @docid, @qty, @amount, @subtotal)"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@reqid", newRequestId)
                cmd.Parameters.AddWithValue("@docid", row.Cells("DocumentID").Value)
                cmd.Parameters.AddWithValue("@qty", row.Cells("Quantity").Value)
                cmd.Parameters.AddWithValue("@amount", CDec(row.Cells("Fee").Value))
                cmd.Parameters.AddWithValue("@subtotal", CDec(row.Cells("Subtotal").Value))
                cmd.ExecuteNonQuery()
            Next

            MsgBox("Document request saved successfully!" & vbCrLf & "Request No: " & txtRequestNo.Text, vbInformation, "Success")
            ClearForm()

        Catch ex As Exception
            MsgBox("Failed to save request: " & ex.Message, vbCritical, "Error")
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Clear all fields?", vbYesNo + vbQuestion, "Confirm Clear") = MsgBoxResult.Yes Then
            ClearForm()
        End If
    End Sub

    Private Sub ClearForm()
        txtStudentID.Clear()
        ClearStudentFields()
        DataGridView1.Rows.Clear()
        ClearDocumentEntryFields()
        cboPaymentStatus.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        dtpRequestDate.Value = Today
        RecalculateTotal()
        GenerateRequestNo()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("Discard this request and go back?", vbYesNo + vbQuestion, "Confirm Cancel") = MsgBoxResult.Yes Then
            frmMainMenu.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        frmMainMenu.Show()
        Me.Hide()
    End Sub

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        frmStudentManagement.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentManagement_Click(sender As Object, e As EventArgs) Handles btnDocumentManagement.Click
        frmDocumentManagement.Show()
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