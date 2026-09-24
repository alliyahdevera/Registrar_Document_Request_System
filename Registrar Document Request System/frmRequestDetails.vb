Imports MySql.Data.MySqlClient

Public Class frmRequestDetails

    Public SelectedRequestNo As String = ""
    Private currentRequestID As Integer = 0

    Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable full row selection on the DataGridView
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

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
                  "r.ORNo, r.ORDate, r.AmountPaid, " &
                  "s.StudentID, CONCAT(s.FirstName, ' ', IFNULL(s.MiddleName, ''), ' ', s.LastName) AS StudentName, " &
                  "s.Course, s.YearLevel " &
                  "FROM tblrequest r " &
                  "INNER JOIN tblstudents s ON r.StudentID = s.StudentID " &
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
                cboStatus.Text = If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "Pending", dr("Status").ToString())

                ' Payment Information Section
                txtORNo.Text = If(IsDBNull(dr("ORNo")), "", dr("ORNo").ToString())
                If Not IsDBNull(dr("ORDate")) Then
                    dtpORDate.Value = Convert.ToDateTime(dr("ORDate"))
                Else
                    dtpORDate.Value = DateTime.Now
                End If
                txtAmountPaid.Text = If(IsDBNull(dr("AmountPaid")), "0.00", Convert.ToDecimal(dr("AmountPaid")).ToString("N2"))
                cboPaymentStatus.Text = If(IsDBNull(dr("PaymentStatus")) OrElse String.IsNullOrWhiteSpace(dr("PaymentStatus").ToString()), "Unpaid", dr("PaymentStatus").ToString())
            End If

            dr.Close()
            cn.Close()
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

    Private Sub btnBackToRequestList_Click(sender As Object, e As EventArgs) Handles btnBacktoRequestList.Click
        frmRequestList.Show()
        Me.Close()
    End Sub
End Class