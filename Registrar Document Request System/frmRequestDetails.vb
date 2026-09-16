Imports MySql.Data.MySqlClient

Public Class frmRequestDetails

    Public SelectedRequestNo As String = ""
    Private currentRequestID As Integer = 0

    Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable full row selection on the DataGridView
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        SetFieldsReadOnly()

        If Not String.IsNullOrEmpty(SelectedRequestNo) Then
            LoadRequestHeaderAndStudent()
            LoadRequestedDocuments()
        End If
    End Sub

    Private Sub SetFieldsReadOnly()
        txtRequestNo.ReadOnly = True
        txtRequestDate.ReadOnly = True
        txtStudentID.ReadOnly = True
        txtStudentName.ReadOnly = True
        txtCourse.ReadOnly = True
        txtYearLevel.ReadOnly = True
        txtTotalAmount.ReadOnly = True
    End Sub

    ' Loads student and request header details
    Private Sub LoadRequestHeaderAndStudent()
        Call connection()

        sql = "SELECT r.RequestID, r.RequestNo, r.RequestDate, r.TotalAmount, r.Status, r.PaymentStatus, " &
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
            txtRequestNo.Text = dr("RequestNo").ToString()
            txtRequestDate.Text = Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd")
            txtStudentID.Text = dr("StudentID").ToString()
            txtStudentName.Text = dr("StudentName").ToString()
            txtCourse.Text = dr("Course").ToString()
            txtYearLevel.Text = dr("YearLevel").ToString()
            txtTotalAmount.Text = Convert.ToDecimal(dr("TotalAmount")).ToString("N2")
        End If

        dr.Close()
        cn.Close()
    End Sub

    ' Loads itemized document line items with fallbacks for legacy records
    Private Sub LoadRequestedDocuments()
        Call connection()

        sql = "SELECT d.DocumentName, rd.Quantity, rd.Amount, rd.SubTotal " &
              "FROM tblrequestdetails rd " &
              "INNER JOIN tbldocuments d ON rd.DocumentID = d.DocumentID " &
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
    End Sub

    Private Sub btnBackToRequestList_Click(sender As Object, e As EventArgs) Handles btnBacktoRequestList.Click
        frmRequestList.Show()
        Me.Close()
    End Sub

End Class