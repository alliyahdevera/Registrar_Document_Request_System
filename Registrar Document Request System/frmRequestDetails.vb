Imports MySql.Data.MySqlClient

Public Class frmRequestDetails

    Public SelectedRequestNo As String = ""
    Private currentRequestID As Integer = 0

    Private currentDbStatus As String = ""
    Private _loading As Boolean = False
    Private dbPaymentVerified As Boolean = False

    Private Const OR_DIGITS As Integer = 5          ' OR-10001 -> 5 digits (change to 6 if needed)
    Private _fixing As Boolean = False
    Private Function IsValidOR(s As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(s.Trim(), "^OR-\d{" & OR_DIGITS & "}$")
    End Function

    Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        txtReleasedBy.Text = CurrentUser.FullName
        txtReleasedBy.ReadOnly = True
        txtRequestDate.ReadOnly = True
        txtRequestDate.TabStop = False
        txtORNo.MaxLength = 3 + OR_DIGITS     ' "OR-" + digits
        txtAmountPaid.ReadOnly = True        ' automatic = Total Amount
        txtAmountPaid.TabStop = False
        cboPaymentStatus.Enabled = False     ' automatic (Paid when OR is complete)

        If Not String.IsNullOrEmpty(SelectedRequestNo) Then
            LoadRequestDetailsInfo(SelectedRequestNo)
        End If
    End Sub

    Public Sub LoadRequestDetailsInfo(ByVal reqNo As String)
        SelectedRequestNo = reqNo
        LoadRequestHeaderAndStudent()
        LoadRequestedDocuments()
    End Sub

    Private Sub LoadRequestHeaderAndStudent()
        Try
            _loading = True

            Call connection()

            sql = "SELECT r.RequestID, r.RequestNo, r.RequestDate, r.TotalAmount, r.Status, r.PaymentStatus, " &
                  "r.ORNo, r.ORDate, r.AmountPaid, r.ReleasedBy, " &
                  "s.StudentID, CONCAT(s.FirstName, ' ', IFNULL(s.MiddleName, ''), ' ', s.LastName) AS StudentName, " &
                  "s.Course, s.YearLevel, u.FullName AS ReleasedByName " &
                  "FROM tblrequest r " &
                  "INNER JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblusers u ON r.ReleasedBy = u.UserID " &
                  "WHERE r.RequestNo = @reqno"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@reqno", SelectedRequestNo)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                currentRequestID = Convert.ToInt32(dr("RequestID"))

                txtRequestNo.Text = dr("RequestNo").ToString()
                txtRequestDate.Text = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                txtStudentID.Text = dr("StudentID").ToString()
                txtStudentName.Text = dr("StudentName").ToString()
                txtCourse.Text = If(IsDBNull(dr("Course")), "-", dr("Course").ToString())
                txtYearLevel.Text = If(IsDBNull(dr("YearLevel")), "-", dr("YearLevel").ToString())
                txtTotalAmount.Text = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))

                txtRequestID.Text = currentRequestID.ToString()
                currentDbStatus = If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "Pending", dr("Status").ToString())
                cboStatus.Text = currentDbStatus

                txtORNo.Text = If(IsDBNull(dr("ORNo")), "", dr("ORNo").ToString())
                If Not IsDBNull(dr("ORDate")) Then
                    dtpORDate.Value = Convert.ToDateTime(dr("ORDate"))
                Else
                    dtpORDate.Value = DateTime.Now
                End If
                txtAmountPaid.Text = If(IsDBNull(dr("AmountPaid")), "0.00", Convert.ToDecimal(dr("AmountPaid")).ToString("N2"))
                cboPaymentStatus.Text = If(IsDBNull(dr("PaymentStatus")) OrElse String.IsNullOrWhiteSpace(dr("PaymentStatus").ToString()), "Unpaid", dr("PaymentStatus").ToString())
                dbPaymentVerified = (cboPaymentStatus.Text = "Paid" AndAlso txtORNo.Text.Trim() <> "")

                If Not IsDBNull(dr("ReleasedByName")) AndAlso Not String.IsNullOrWhiteSpace(dr("ReleasedByName").ToString()) Then
                    txtReleasedBy.Text = dr("ReleasedByName").ToString()
                Else
                    txtReleasedBy.Text = CurrentUser.FullName
                End If
            End If

            dr.Close()
            cn.Close()

            _loading = False
            ApplyStatusLock()
        Catch ex As Exception
            _loading = False
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading request details: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub LoadRequestedDocuments()
        Try
            Call connection()

            sql = "SELECT d.DocumentName, rd.Quantity, rd.Amount, rd.SubTotal " &
                  "FROM tblrequestdetails rd " &
                  "INNER JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "WHERE rd.RequestID = @rid"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@rid", currentRequestID)
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            Dim hasRows As Boolean = False

            While dr.Read()
                hasRows = True
                Dim docName As String = dr("DocumentName").ToString()
                Dim qty As String = dr("Quantity").ToString()
                Dim fee As String = Convert.ToDecimal(dr("Amount")).ToString("N2")
                Dim subtotal As String = Convert.ToDecimal(dr("SubTotal")).ToString("N2")

                dgvReqDoc.Rows.Add(docName, qty, fee, subtotal)
            End While

            dr.Close()

            If Not hasRows Then
                Dim totalAmt As Decimal = 0
                Decimal.TryParse(txtTotalAmount.Text, totalAmt)

                sql = "SELECT DocumentName, Fee FROM tbldocuments WHERE Fee = @fee OR (@fee % Fee = 0 AND Fee > 0) LIMIT 1"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@fee", totalAmt)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    Dim docName As String = dr("DocumentName").ToString()
                    Dim feeVal As Decimal = Convert.ToDecimal(dr("Fee"))
                    Dim qtyVal As Integer = If(feeVal > 0, Convert.ToInt32(totalAmt / feeVal), 1)

                    dgvReqDoc.Rows.Add(docName, qtyVal, feeVal.ToString("N2"), totalAmt.ToString("N2"))
                End If
                dr.Close()
            End If

            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading requested documents: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Function IsPaymentVerified() As Boolean
        Return dbPaymentVerified
    End Function

    Private Sub ApplyStatusLock()
        If Not IsPaymentVerified() Then
            If cboStatus.Text <> "Cancelled" Then
                cboStatus.Text = "Pending"
            End If
        End If

        cboStatus.Enabled = True
        btnUpdateStatus.Enabled = True
    End Sub

    Private Function IsValidStatusTransition(fromStatus As String, toStatus As String) As Boolean
        If fromStatus = toStatus Then Return True ' no-op, nothing to validate

        Select Case fromStatus
            Case "Pending"
                Return toStatus = "Processing" OrElse toStatus = "Cancelled"
            Case "Processing"
                Return toStatus = "Ready for Release" OrElse toStatus = "Cancelled"
            Case "Ready for Release"
                Return toStatus = "Released" OrElse toStatus = "Processing"
            Case "Released"
                Return toStatus = "Processing"
            Case "Cancelled"
                Return toStatus = "Processing" ' allow reopening a cancelled request
            Case Else
                Return False
        End Select
    End Function

    Private Sub btnSavePayment_Click(sender As Object, e As EventArgs) Handles btnSavePayment.Click
        If currentRequestID = 0 Then
            MsgBox("No active request loaded to save payment.", vbExclamation, "Error")
            Return
        End If

        Dim orNo As String = txtORNo.Text.Trim().ToUpper()
        If Not IsValidOR(orNo) Then
            MsgBox("OR Number must be in the format OR-" & New String("0"c, OR_DIGITS).Replace("0", "1") &
                   " ('OR-' followed by " & OR_DIGITS & " digits).", vbExclamation, "Validation Error")
            txtORNo.Focus()
            Return
        End If

        Dim totalAmt As Decimal = 0
        Decimal.TryParse(txtTotalAmount.Text.Trim(), totalAmt)
        If totalAmt <= 0 Then
            MsgBox("This request has no total amount.", vbExclamation, "Validation Error")
            Return
        End If

        If Not ConfirmAction("Save payment of " & totalAmt.ToString("N2") & " with OR No. " & orNo & "?", "Confirm Payment") Then Exit Sub

        Try
            Call connection()
            ' Amount Paid = Total Amount, PaymentStatus = Paid. Status is NOT changed (stays Pending).
            sql = "UPDATE tblrequest SET ORNo = @orno, ORDate = @ordate, AmountPaid = @amt, " &
                  "PaymentStatus = 'Paid' WHERE RequestID = @rid"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@orno", orNo)
            cmd.Parameters.AddWithValue("@ordate", dtpORDate.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@amt", totalAmt)
            cmd.Parameters.AddWithValue("@rid", currentRequestID)
            cmd.ExecuteNonQuery()
            cn.Close()

            LogActivity("Save Payment", txtRequestNo.Text & " - OR " & orNo & " - " & totalAmt.ToString("N2"))
            MsgBox("Payment details saved successfully!", vbInformation, "Success")
            LoadRequestHeaderAndStudent()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error saving payment details: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        If currentRequestID = 0 Then
            MsgBox("No active request loaded to update status.", vbExclamation, "Error")
            Return
        End If

        If String.IsNullOrWhiteSpace(cboStatus.Text) Then
            MsgBox("Please select a valid Request Status.", vbExclamation, "Validation Error")
            Return
        End If

        Dim selectedStatus As String = cboStatus.Text.Trim()

        If selectedStatus = "Cancelled" AndAlso cboPaymentStatus.Text.Trim() = "Paid" Then
            MsgBox("This request has already been paid and cannot be cancelled." & vbCrLf &
           "Please continue processing it through to 'Released', or reopen it back to 'Processing' instead.",
           vbExclamation, "Cannot Cancel Paid Request")
            cboStatus.Text = currentDbStatus
            Return
        End If

        If Not IsPaymentVerified() AndAlso selectedStatus <> "Pending" AndAlso selectedStatus <> "Cancelled" Then
            MsgBox("This request cannot move past 'Pending' until Payment Status is 'Paid' AND an OR Number is recorded." & vbCrLf &
           "(You can still Cancel an unpaid request.)", vbExclamation, "Payment Not Verified")
            cboStatus.Text = currentDbStatus
            Return
        End If

        If Not IsValidStatusTransition(currentDbStatus, selectedStatus) Then
            MsgBox("Invalid status change: '" & currentDbStatus & "' cannot move directly to '" & selectedStatus & "'." &
           vbCrLf & "Statuses must follow: Pending -> Processing -> Ready for Release -> Released" &
           vbCrLf & "(Ready for Release, Released, or Cancelled requests may only be reopened back to Processing.)",
           vbExclamation, "Invalid Status Transition")
            cboStatus.Text = currentDbStatus
            Return
        End If

        If selectedStatus = currentDbStatus Then
            MsgBox("Status is unchanged.", vbInformation, "No Changes")
            Return
        End If

        If Not ConfirmAction("Change status from '" & currentDbStatus & "' to '" & selectedStatus & "'?", "Confirm Status Change") Then
            cboStatus.Text = currentDbStatus
            Exit Sub
        End If

        Try
            Call connection()

            Select Case selectedStatus
                Case "Processing"
                    sql = "UPDATE tblrequest SET Status = @status, ProcessedBy = @processedby, ReleasedBy = NULL, ReadyForReleaseDate = NULL WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@processedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case "Ready for Release"
                    sql = "UPDATE tblrequest SET Status = @status, ProcessedBy = @processedby, ReadyForReleaseDate = NOW() WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@processedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case "Released"
                    sql = "UPDATE tblrequest SET Status = @status, ReleasedBy = @releasedby, ReadyForReleaseDate = NULL WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@releasedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case Else
                    sql = "UPDATE tblrequest SET Status = @status, ReadyForReleaseDate = NULL WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)
            End Select

            cmd.ExecuteNonQuery()
            cn.Close()

            LogActivity("Update Status", txtRequestNo.Text & ": " & currentDbStatus & " -> " & selectedStatus)
            MsgBox("Request status updated successfully!", vbInformation, "Success")
            LoadRequestHeaderAndStudent()

        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error updating status: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ' only digits can be typed; "OR-" is added by the system
    Private Sub txtORNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtORNo.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then e.Handled = True
    End Sub

    ' clicking the empty box shows the "OR-" prefix
    Private Sub txtORNo_Enter(sender As Object, e As EventArgs) Handles txtORNo.Enter
        If txtORNo.Text.Trim() = "" Then
            txtORNo.Text = "OR-"
            txtORNo.SelectionStart = txtORNo.Text.Length
        End If
    End Sub

    ' leaving it with just the prefix clears it again
    Private Sub txtORNo_Leave(sender As Object, e As EventArgs) Handles txtORNo.Leave
        If txtORNo.Text.Trim() = "OR-" Then txtORNo.Text = ""
    End Sub

    Private Sub txtORNo_TextChanged(sender As Object, e As EventArgs) Handles txtORNo.TextChanged
        If _loading OrElse _fixing Then Exit Sub    ' skip while loading old records
        ' keep the text always in the form OR-digits (also fixes pasted text)
        Dim s As String = txtORNo.Text.ToUpper()
        If s <> "" Then
            Dim body As String = If(s.StartsWith("OR-"), s.Substring(3), s)
            Dim digits As String = System.Text.RegularExpressions.Regex.Replace(body, "\D", "")
            If digits.Length > OR_DIGITS Then digits = digits.Substring(0, OR_DIGITS)
            Dim fixedText As String = "OR-" & digits
            If fixedText <> txtORNo.Text Then
                _fixing = True
                txtORNo.Text = fixedText
                txtORNo.SelectionStart = fixedText.Length
                _fixing = False
            End If
        End If
        ' complete OR number -> Amount Paid = Total Amount, Payment Status = Paid
        If IsValidOR(txtORNo.Text) Then
            txtAmountPaid.Text = txtTotalAmount.Text
            cboPaymentStatus.Text = "Paid"
        Else
            txtAmountPaid.Text = "0.00"
            cboPaymentStatus.Text = "Unpaid"
        End If
    End Sub

    Private Sub btnBackToRequestList_Click(sender As Object, e As EventArgs) Handles btnBacktoRequestList.Click
        frmMainMenu.OpenRequestList()
    End Sub

End Class