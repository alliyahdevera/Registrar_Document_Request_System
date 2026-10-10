Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient

Public Class frmNewRequest
    Private currentDocFee As Decimal = 0
    Private _oldQty As Integer = 1
    Private slipItems As New List(Of String())
    Private slipReqNo, slipDate, slipStudentID, slipStudentName, slipCourse, slipYear As String
    Private slipTotalQty, slipTotalAmt, slipPay, slipStatus, slipBy As String

    Private Sub frmNewRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        LoadDocumentsCombo()

        txtreqdate.TabStop = False
        txtreqdate.Text = Date.Today.ToString("yyyy-MM-dd")
        txtCreatedBy.Text = CurrentUser.FullName
        txtPaymentStatus.Text = "Unpaid"
        txtStatus.Text = "Pending"
        SetupGridEditing()

        ClearDocumentEntryFields()
        RecalculateTotal()

        GenerateRequestNo()
    End Sub

    Private Sub RefreshUserSession()
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
        txtreqdate.Text = Date.Today.ToString("yyyy-MM-dd")
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
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

    Private Sub SetupGridEditing()
        dgvReqDoc.ReadOnly = False
        dgvReqDoc.AllowUserToAddRows = False
        dgvReqDoc.EditMode = DataGridViewEditMode.EditOnEnter
        For Each col As DataGridViewColumn In dgvReqDoc.Columns
            col.ReadOnly = (col.Name <> "Quantity")   ' only Quantity is editable
        Next
    End Sub

    Private Sub dgvReqDoc_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvReqDoc.CellBeginEdit
        If dgvReqDoc.Columns(e.ColumnIndex).Name = "Quantity" Then
            If Not Integer.TryParse(Convert.ToString(dgvReqDoc.Rows(e.RowIndex).Cells("Quantity").Value), _oldQty) Then _oldQty = 1
        End If
    End Sub

    Private Sub dgvReqDoc_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReqDoc.CellEndEdit
        If e.RowIndex < 0 OrElse dgvReqDoc.Columns(e.ColumnIndex).Name <> "Quantity" Then Exit Sub
        Dim row As DataGridViewRow = dgvReqDoc.Rows(e.RowIndex)
        Dim qty As Integer
        If Not Integer.TryParse(Convert.ToString(row.Cells("Quantity").Value).Trim(), qty) OrElse qty < 1 OrElse qty > 99 Then
            MsgBox("Quantity must be a whole number from 1 to 99.", vbExclamation, "New Document Request")
            qty = _oldQty
        End If
        Dim fee As Decimal = 0
        Decimal.TryParse(Convert.ToString(row.Cells("Fee").Value), fee)
        row.Cells("Quantity").Value = qty
        row.Cells("Subtotal").Value = (fee * qty).ToString("N2")
        RecalculateTotal()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Using frm As New frmStudentList()
            If frm.ShowDialog(frmMainMenu) = DialogResult.OK Then
                txtStudentID.Text = frm.SelectedStudentID
                SearchStudent()
            End If
        End Using
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
            txtCourse.Text = dr("Course").ToString()
            txtYearLevel.Text = dr("YearLevel").ToString()
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
        txtCourse.Clear()
        txtYearLevel.Clear()
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

    Private Function IsDocumentAlreadyActiveForStudent(studentId As String, docId As String) As Boolean
        Dim result As Boolean = False
        Try
            Call connection()
            sql = "SELECT COUNT(*) FROM tblrequestdetails rd " &
                  "INNER JOIN tblrequest r ON rd.RequestID = r.RequestID " &
                  "WHERE r.StudentID = @sid AND rd.DocumentID = @docid " &
                  "AND r.Status NOT IN ('Released', 'Cancelled')"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@sid", studentId)
            cmd.Parameters.AddWithValue("@docid", docId)
            Dim cnt As Long = Convert.ToInt64(cmd.ExecuteScalar())
            result = cnt > 0
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error checking existing requests: " & ex.Message, vbCritical, "Error")
        End Try
        Return result
    End Function

    Private Sub btnAddToList_Click(sender As Object, e As EventArgs) Handles btnaddtolist.Click
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

        If Not String.IsNullOrWhiteSpace(txtStudentID.Text) Then
            Dim alreadyInThisGrid As Boolean = False
            For Each row As DataGridViewRow In dgvReqDoc.Rows
                If row.IsNewRow Then Continue For
                If row.Cells("DocumentID").Value IsNot Nothing AndAlso row.Cells("DocumentID").Value.ToString() = docId Then
                    alreadyInThisGrid = True
                    Exit For
                End If
            Next

            If Not alreadyInThisGrid AndAlso IsDocumentAlreadyActiveForStudent(txtStudentID.Text.Trim(), docId) Then
                MsgBox("This student already has an active request for '" & docName & "' that hasn't been Released or Cancelled yet." & vbCrLf &
                   "It must reach 'Released' or 'Cancelled' status before it can be requested again.", vbExclamation, "Duplicate Document Request")
                Exit Sub
            End If
        End If

        For Each row As DataGridViewRow In dgvReqDoc.Rows
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

        Dim rowIndex As Integer = dgvReqDoc.Rows.Add()
        With dgvReqDoc.Rows(rowIndex)
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

    Private Sub dgvReqDoc_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReqDoc.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If dgvReqDoc.Rows(e.RowIndex).IsNewRow Then Exit Sub

        If dgvReqDoc.Columns(e.ColumnIndex).Name = "Action" Then
            dgvReqDoc.Rows.RemoveAt(e.RowIndex)
            RecalculateTotal()
        End If
    End Sub

    Private Sub RecalculateTotal()
        Dim totalAmount As Decimal = 0
        Dim totalQty As Integer = 0

        For Each row As DataGridViewRow In dgvReqDoc.Rows
            If row.IsNewRow Then Continue For

            If row.Cells("Subtotal").Value IsNot Nothing Then
                Dim lineTotal As Decimal
                If Decimal.TryParse(row.Cells("Subtotal").Value.ToString(), lineTotal) Then
                    totalAmount += lineTotal
                End If
            End If

            If row.Cells("Quantity").Value IsNot Nothing Then
                Dim qty As Integer
                If Integer.TryParse(row.Cells("Quantity").Value.ToString(), qty) Then
                    totalQty += qty
                End If
            End If
        Next

        txtTotalAmount.Text = totalAmount.ToString("N2")
        txtTotalQuantity.Text = totalQty.ToString()
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
        ElseIf dgvReqDoc.Rows.Count = 0 OrElse
           (dgvReqDoc.Rows.Count = 1 AndAlso dgvReqDoc.Rows(0).IsNewRow) Then
            MsgBox("Please add at least one document to the request.", vbExclamation, "New Document Request")
            Return False
        End If

        For Each row As DataGridViewRow In dgvReqDoc.Rows
            If row.IsNewRow Then Continue For

            Dim rowDocId As String = row.Cells("DocumentID").Value.ToString()
            Dim rowDocName As String = row.Cells("DocumentName").Value.ToString()

            If IsDocumentAlreadyActiveForStudent(txtStudentID.Text.Trim(), rowDocId) Then
                MsgBox("This student already has an active request for '" & rowDocName & "' that hasn't been Released or Cancelled yet." & vbCrLf &
                   "Please remove it from this request.", vbExclamation, "Duplicate Document Request")
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub btnSaveRequest_Click(sender As Object, e As EventArgs) Handles btnsavereq.Click
        If Not IsValidRequest() Then Exit Sub
        If MsgBox("Save this document request?", vbYesNo + vbQuestion, "Confirm Save") <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        Dim savedOk As Boolean = False
        Call connection()
        Try
            sql = "INSERT INTO tblrequest (RequestNo, StudentID, RequestDate, TotalAmount, PaymentStatus, Status, CreatedBy) " &
                  "VALUES (@reqno, @studid, @reqdate, @total, 'Unpaid', 'Pending', @createdby)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@reqno", txtRequestNo.Text.Trim())
            cmd.Parameters.AddWithValue("@studid", txtStudentID.Text.Trim())
            cmd.Parameters.AddWithValue("@reqdate", Date.Today)
            cmd.Parameters.AddWithValue("@total", CDec(txtTotalAmount.Text))
            cmd.Parameters.AddWithValue("@createdby", CurrentUser.UserID)
            cmd.ExecuteNonQuery()
            Dim newRequestId As Long = cmd.LastInsertedId
            slipItems.Clear()
            For Each row As DataGridViewRow In dgvReqDoc.Rows
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
                slipItems.Add({row.Cells("DocumentName").Value.ToString(), row.Cells("Quantity").Value.ToString(),
                               row.Cells("Fee").Value.ToString(), row.Cells("Subtotal").Value.ToString()})
            Next
            slipReqNo = txtRequestNo.Text
            slipDate = txtreqdate.Text
            slipStudentID = txtStudentID.Text.Trim()
            slipStudentName = (txtFirstName.Text & " " & txtMiddleName.Text & " " & txtLastName.Text).Replace("  ", " ")
            slipCourse = txtCourse.Text
            slipYear = txtYearLevel.Text
            slipTotalQty = txtTotalQuantity.Text
            slipTotalAmt = txtTotalAmount.Text
            slipPay = "Unpaid"
            slipStatus = "Pending"
            slipBy = txtCreatedBy.Text
            savedOk = True
        Catch ex As Exception
            MsgBox("Failed to save request: " & ex.Message, vbCritical, "Error")
        Finally
            cn.Close()
        End Try
        If savedOk Then
            LogActivity("New Request", slipReqNo & " - " & slipStudentID & " - Total " & slipTotalAmt)
            MsgBox("Document request saved successfully!" & vbCrLf & "Request No: " & slipReqNo, vbInformation, "Success")
            ClearForm()
            ShowSlipPreview()
            frmMainMenu.OpenRequestList()
        End If
    End Sub

    Private Sub ShowSlipPreview()
        Dim pd As New PrintDocument()
        AddHandler pd.PrintPage, AddressOf SlipPrintPage
        Using dlg As New PrintPreviewDialog()
            dlg.Document = pd
            dlg.UseAntiAlias = True
            dlg.Width = 900
            dlg.Height = 700
            dlg.StartPosition = FormStartPosition.CenterScreen
            dlg.ShowDialog(frmMainMenu)
        End Using
    End Sub

    Private Sub SlipPrintPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim xL As Single = e.MarginBounds.Left
        Dim xR As Single = e.MarginBounds.Right
        Dim w As Single = e.MarginBounds.Width
        Dim y As Single = e.MarginBounds.Top
        Dim ctr As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim rgt As New StringFormat() With {.Alignment = StringAlignment.Far}
        Using fTitle As New Font("Segoe UI", 15, FontStyle.Bold),
              fHead As New Font("Segoe UI", 10, FontStyle.Bold),
              fBody As New Font("Segoe UI", 10),
              fSmall As New Font("Segoe UI", 8, FontStyle.Italic),
              pn As New Pen(Color.Black, 1)
            Dim lineH As Single = fBody.GetHeight(g) + 5
            g.DrawString("Lyceum of Alabang", fTitle, Brushes.Black, New RectangleF(xL, y, w, 30), ctr)
            y += 30
            g.DrawString("Document Request Slip", fHead, Brushes.Black, New RectangleF(xL, y, w, 22), ctr)
            y += 28
            g.DrawLine(pn, xL, y, xR, y)
            y += 10
            Dim kv = Sub(k As String, v As String)
                         g.DrawString(k, fHead, Brushes.Black, xL, y)
                         g.DrawString(v, fBody, Brushes.Black, xL + 130, y)
                         y += lineH
                     End Sub
            kv("Request No.:", slipReqNo)
            kv("Date:", slipDate)
            kv("Student ID:", slipStudentID)
            kv("Name:", slipStudentName)
            kv("Course:", slipCourse)
            kv("Year Level:", slipYear)
            y += 6
            g.DrawLine(pn, xL, y, xR, y)
            y += 6
            Dim cQty As Single = xL + w * 0.55F
            Dim cFee As Single = xL + w * 0.64F
            Dim cSub As Single = xL + w * 0.82F
            g.DrawString("Document", fHead, Brushes.Black, xL, y)
            g.DrawString("Qty", fHead, Brushes.Black, cQty, y)
            g.DrawString("Fee", fHead, Brushes.Black, New RectangleF(cFee, y, w * 0.16F, lineH), rgt)
            g.DrawString("Subtotal", fHead, Brushes.Black, New RectangleF(cSub, y, xR - cSub, lineH), rgt)
            y += lineH
            g.DrawLine(pn, xL, y, xR, y)
            y += 4
            For Each it As String() In slipItems
                g.DrawString(it(0), fBody, Brushes.Black, New RectangleF(xL, y, w * 0.53F, lineH))
                g.DrawString(it(1), fBody, Brushes.Black, cQty, y)
                g.DrawString(it(2), fBody, Brushes.Black, New RectangleF(cFee, y, w * 0.16F, lineH), rgt)
                g.DrawString(it(3), fBody, Brushes.Black, New RectangleF(cSub, y, xR - cSub, lineH), rgt)
                y += lineH
            Next
            g.DrawLine(pn, xL, y + 2, xR, y + 2)
            y += 8
            g.DrawString("Total Quantity: " & slipTotalQty, fHead, Brushes.Black, xL, y)
            g.DrawString("TOTAL: PHP " & slipTotalAmt, fHead, Brushes.Black, New RectangleF(xL, y, w, lineH), rgt)
            y += lineH + 6
            kv("Payment Status:", slipPay)
            kv("Request Status:", slipStatus)
            kv("Processed by:", slipBy)
            y += 10
            g.DrawString("Please pay at the cashier, then present your Official Receipt (OR) number" & vbCrLf &
                         "to the Registrar's Office. Unpaid requests are automatically cancelled after 7 days.",
                         fSmall, Brushes.Black, New RectangleF(xL, y, w, lineH * 2.5F))
            y += lineH * 3
            g.DrawLine(pn, xR - 200, y + 20, xR, y + 20)
            g.DrawString("Registrar Staff Signature", fSmall, Brushes.Black, New RectangleF(xR - 200, y + 22, 200, 18), ctr)
        End Using
        e.HasMorePages = False
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        If MsgBox("Clear all fields?", vbYesNo + vbQuestion, "Confirm Clear") = MsgBoxResult.Yes Then
            ClearForm()
        End If
    End Sub

    Private Sub ClearForm()
        txtStudentID.Clear()
        ClearStudentFields()
        dgvReqDoc.Rows.Clear()
        ClearDocumentEntryFields()

        txtPaymentStatus.Text = "Unpaid"
        txtStatus.Text = "Pending"

        txtreqdate.Text = Date.Today.ToString("yyyy-MM-dd")
        RecalculateTotal()
        GenerateRequestNo()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If MsgBox("Discard this request and go back?", vbYesNo + vbQuestion, "Confirm Cancel") = MsgBoxResult.Yes Then
            frmMainMenu.OpenDashboard()
        End If
    End Sub
End Class