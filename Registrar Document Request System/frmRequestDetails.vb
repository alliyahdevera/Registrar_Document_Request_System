Imports MySql.Data.MySqlClient

Public Class frmRequestDetails

    Public SelectedRequestNo As String = ""
    Private currentRequestID As Integer = 0

    ' Tracks the status as it currently exists in the database (separate from
    ' cboStatus.Text, which the user changes to select a *new* status).
    Private currentDbStatus As String = ""

    Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable full row selection on the DataGridView
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        ' Set Released By field to currently logged-in user and make it read-only
        txtReleasedBy.Text = CurrentUser.FullName
        txtReleasedBy.ReadOnly = True

        If Not String.IsNullOrEmpty(SelectedRequestNo) Then
            LoadRequestDetailsInfo(SelectedRequestNo)
        End If
    End Sub

    ''' <summary>
    ''' Public method accessible from external forms (e.g., frmRequestList)
    ''' to load request header details, student info, payment info, and document line items.
    ''' </summary>
    Public Sub LoadRequestDetailsInfo(ByVal reqNo As String)
        SelectedRequestNo = reqNo
        LoadRequestHeaderAndStudent()
        LoadRequestedDocuments()
    End Sub

    ' Loads student, request header, payment details, and request status
    Private Sub LoadRequestHeaderAndStudent()
        Try
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

                ' Request Information
                txtRequestNo.Text = dr("RequestNo").ToString()
                txtRequestDate.Text = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                txtStudentID.Text = dr("StudentID").ToString()
                txtStudentName.Text = dr("StudentName").ToString()
                txtCourse.Text = If(IsDBNull(dr("Course")), "-", dr("Course").ToString())
                txtYearLevel.Text = If(IsDBNull(dr("YearLevel")), "-", dr("YearLevel").ToString())
                txtTotalAmount.Text = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))

                ' Request Status Section
                txtRequestID.Text = currentRequestID.ToString()
                currentDbStatus = If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "Pending", dr("Status").ToString())
                cboStatus.Text = currentDbStatus

                ' Payment Information Section
                txtORNo.Text = If(IsDBNull(dr("ORNo")), "", dr("ORNo").ToString())
                If Not IsDBNull(dr("ORDate")) Then
                    dtpORDate.Value = Convert.ToDateTime(dr("ORDate"))
                Else
                    dtpORDate.Value = DateTime.Now
                End If
                txtAmountPaid.Text = If(IsDBNull(dr("AmountPaid")), "0.00", Convert.ToDecimal(dr("AmountPaid")).ToString("N2"))
                cboPaymentStatus.Text = If(IsDBNull(dr("PaymentStatus")) OrElse String.IsNullOrWhiteSpace(dr("PaymentStatus").ToString()), "Unpaid", dr("PaymentStatus").ToString())

                ' Display assigned ReleasedBy staff if already saved; otherwise default to active user
                If Not IsDBNull(dr("ReleasedByName")) AndAlso Not String.IsNullOrWhiteSpace(dr("ReleasedByName").ToString()) Then
                    txtReleasedBy.Text = dr("ReleasedByName").ToString()
                Else
                    txtReleasedBy.Text = CurrentUser.FullName
                End If
            End If

            dr.Close()
            cn.Close()

            ' Re-evaluate whether status changes are allowed, based on the just-reloaded,
            ' actually-saved payment data (this is what makes the button open right after
            ' a successful Save Payment, not while the user is merely typing).
            ApplyStatusLock()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading request details: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ' Loads itemized document line items with fallbacks for legacy records
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

            ' Fallback lookup if no rows exist in tblrequestdetails
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

#Region "Fix #2 / #4 - Lock status button until payment is fully verified"

    ''' <summary>
    ''' Payment is "verified" only when BOTH conditions are met:
    '''   - Payment Status = Paid
    '''   - An OR Number has been recorded
    ''' Until both are true (as actually saved in the DB), the request must stay "Pending"
    ''' and the Status button stays locked. This is only re-checked when data is (re)loaded
    ''' from the database - i.e. on form load, and right after a successful Save Payment -
    ''' not live while the user is just typing into the fields.
    ''' </summary>
    Private Function IsPaymentVerified() As Boolean
        Dim payStatus As String = cboPaymentStatus.Text.Trim()
        Dim hasORNo As Boolean = Not String.IsNullOrWhiteSpace(txtORNo.Text)

        Return payStatus = "Paid" AndAlso hasORNo
    End Function

    ''' <summary>
    ''' Locks the Status combo/button from advancing past "Pending" until payment is
    ''' verified (see IsPaymentVerified). Does not force-override a terminal "Cancelled"
    ''' status. The controls stay enabled either way - even an unpaid request must still
    ''' be cancellable - btnUpdateStatus_Click is what actually blocks any transition
    ''' other than "Cancelled" while payment is unverified.
    ''' </summary>
    Private Sub ApplyStatusLock()
        If Not IsPaymentVerified() Then
            If cboStatus.Text <> "Cancelled" Then
                cboStatus.Text = "Pending"
            End If
        End If

        cboStatus.Enabled = True
        btnUpdateStatus.Enabled = True
    End Sub

#End Region

#Region "Fix #3 / #5 - Enforce sequential status flow, with reopening back to Processing"

    ''' <summary>
    ''' Defines the only allowed status transitions:
    '''   Pending -> Processing -> Ready for Release -> Released   (forward, one step at a time)
    '''   Ready for Release / Released / Cancelled -> Processing   (any of these may be reopened
    '''                                                              back to Processing)
    '''   Pending / Processing -> Cancelled                        (cancel before it's ready/released)
    ''' Anything else (skipping a stage, other invalid backward moves) is rejected.
    ''' </summary>
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

#End Region

    ''' <summary>
    ''' Saves payment details. Automatically advances a Pending request to Processing
    ''' once payment/OR info is recorded, and records the staff member who processed it.
    ''' Reloading the record afterward re-evaluates the status lock (Fix #4).
    ''' </summary>
    Private Sub btnSavePayment_Click(sender As Object, e As EventArgs) Handles btnSavePayment.Click
        If currentRequestID = 0 Then
            MsgBox("No active request loaded to save payment.", vbExclamation, "Error")
            Return
        End If

        If String.IsNullOrWhiteSpace(txtORNo.Text) Then
            MsgBox("Please enter an Official Receipt (OR) Number.", vbExclamation, "Validation Error")
            txtORNo.Focus()
            Return
        End If

        Dim amtPaid As Decimal = 0
        If Not Decimal.TryParse(txtAmountPaid.Text.Trim(), amtPaid) OrElse amtPaid <= 0 Then
            MsgBox("Please enter a valid Amount Paid.", vbExclamation, "Validation Error")
            txtAmountPaid.Focus()
            Return
        End If

        Try
            Call connection()

            ' Automatically advance Pending requests to Processing upon receiving payment
            Dim newStatus As String = currentDbStatus
            Dim advancedToProcessing As Boolean = False
            If currentDbStatus = "Pending" Then
                newStatus = "Processing"
                advancedToProcessing = True
            End If

            Dim payStatus As String = If(String.IsNullOrWhiteSpace(cboPaymentStatus.Text), "Paid", cboPaymentStatus.Text)

            If advancedToProcessing Then
                ' Record who processed the request now that payment came in
                sql = "UPDATE tblrequest " &
                      "SET ORNo = @orno, " &
                      "    ORDate = @ordate, " &
                      "    AmountPaid = @amt, " &
                      "    PaymentStatus = @paystatus, " &
                      "    Status = @status, " &
                      "    ProcessedBy = @processedby " &
                      "WHERE RequestID = @rid"

                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@orno", txtORNo.Text.Trim())
                cmd.Parameters.AddWithValue("@ordate", dtpORDate.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@amt", amtPaid)
                cmd.Parameters.AddWithValue("@paystatus", payStatus)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@processedby", CurrentUser.UserID)
                cmd.Parameters.AddWithValue("@rid", currentRequestID)
            Else
                sql = "UPDATE tblrequest " &
                      "SET ORNo = @orno, " &
                      "    ORDate = @ordate, " &
                      "    AmountPaid = @amt, " &
                      "    PaymentStatus = @paystatus, " &
                      "    Status = @status " &
                      "WHERE RequestID = @rid"

                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@orno", txtORNo.Text.Trim())
                cmd.Parameters.AddWithValue("@ordate", dtpORDate.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@amt", amtPaid)
                cmd.Parameters.AddWithValue("@paystatus", payStatus)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@rid", currentRequestID)
            End If

            cmd.ExecuteNonQuery()
            cn.Close()

            MsgBox("Payment details saved successfully!", vbInformation, "Success")

            ' Refresh form data from the DB - this is what unlocks the Status button
            ' (Fix #4), since ApplyStatusLock() runs at the end of this call using the
            ' freshly-saved OR No. / Payment Status.
            LoadRequestHeaderAndStudent()

        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error saving payment details: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Updates request status from the cboStatus dropdown, enforcing:
    '''  - payment must be fully verified (Paid AND has an OR No.) before leaving Pending
    '''  - only the allowed sequential/backward transitions are permitted
    '''  - CreatedBy / ProcessedBy / ReleasedBy are kept in sync with the new status, so
    '''    frmRequestList's grid ("Processed Request" / "Released By" columns) always
    '''    reflects the correct staff member for whatever status the request is currently in
    ''' </summary>
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

        ' Guard: payment must be fully verified before the request can leave Pending -
        ' except that an unpaid request can always be Cancelled.
        If Not IsPaymentVerified() AndAlso selectedStatus <> "Pending" AndAlso selectedStatus <> "Cancelled" Then
            MsgBox("This request cannot move past 'Pending' until Payment Status is 'Paid' AND an OR Number is recorded." & vbCrLf &
                   "(You can still Cancel an unpaid request.)", vbExclamation, "Payment Not Verified")
            cboStatus.Text = currentDbStatus
            Return
        End If

        ' Guard: no skipping stages, only the allowed transitions
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

        Try
            Call connection()

            Select Case selectedStatus
                Case "Processing"
                    ' Covers Pending -> Processing (normal) as well as reopening from
                    ' Ready for Release / Released / Cancelled. Reopening clears ReleasedBy
                    ' since the request is no longer considered released.
                    sql = "UPDATE tblrequest SET Status = @status, ProcessedBy = @processedby, ReleasedBy = NULL WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@processedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case "Ready for Release"
                    sql = "UPDATE tblrequest SET Status = @status, ProcessedBy = @processedby WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@processedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case "Released"
                    sql = "UPDATE tblrequest SET Status = @status, ReleasedBy = @releasedby WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@releasedby", CurrentUser.UserID)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)

                Case Else ' Cancelled (or Pending, though that shouldn't be reachable here)
                    sql = "UPDATE tblrequest SET Status = @status WHERE RequestID = @rid"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@status", selectedStatus)
                    cmd.Parameters.AddWithValue("@rid", currentRequestID)
            End Select

            cmd.ExecuteNonQuery()
            cn.Close()

            MsgBox("Request status updated successfully!", vbInformation, "Success")
            LoadRequestHeaderAndStudent()

        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error updating status: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub btnBackToRequestList_Click(sender As Object, e As EventArgs) Handles btnBacktoRequestList.Click
        frmRequestList.Show()
        Me.Close()
    End Sub

End Class