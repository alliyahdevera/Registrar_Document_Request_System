Imports MySql.Data.MySqlClient

Public Class frmRequestDetails

    Public SelectedRequestNo As String = ""

    Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       Private Sub frmRequestDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Force full row selection across all columns
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
        If Controls.Find("txtLRN", True).Length > 0 Then CType(Controls.Find("txtLRN", True)(0), TextBox).ReadOnly = True
        txtStudentName.ReadOnly = True
        txtCourse.ReadOnly = True
        txtYearLevel.ReadOnly = True
        txtTotalAmount.ReadOnly = True
    End Sub

    ' Fetches request and student info
    Private Sub LoadRequestHeaderAndStudent()
        Call connection()

        sql = "SELECT r.RequestNo, r.RequestDate, r.TotalAmount, " &
              "s.StudentID, s.LRN, CONCAT(s.FirstName, ' ', IFNULL(s.MiddleName, ''), ' ', s.LastName) AS StudentName, " &
              "s.Course, s.YearLevel " &
              "FROM tblrequest r " &
              "INNER JOIN tblstudents s ON r.StudentID = s.StudentID " &
              "WHERE r.RequestNo = @reqno"

        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@reqno", SelectedRequestNo)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            txtRequestNo.Text = dr("RequestNo").ToString()
            txtRequestDate.Text = Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd")
            txtStudentID.Text = dr("StudentID").ToString()

            If Controls.Find("txtLRN", True).Length > 0 Then
                Controls.Find("txtLRN", True)(0).Text = dr("LRN").ToString()
            End If

            txtStudentName.Text = dr("StudentName").ToString()
            txtCourse.Text = dr("Course").ToString()
            txtYearLevel.Text = dr("YearLevel").ToString()
            txtTotalAmount.Text = Convert.ToDecimal(dr("TotalAmount")).ToString("N2")
        End If

        dr.Close()
        cn.Close()
    End Sub

    ' Fetches document details by joining tblrequest directly on RequestNo
    Private Sub LoadRequestedDocuments()
        Call connection()

        sql = "SELECT d.DocumentName, rd.Quantity, rd.Amount, rd.SubTotal " &
              "FROM tblrequestdetails rd " &
              "INNER JOIN tblrequest r ON rd.RequestID = r.RequestID " &
              "INNER JOIN tbldocuments d ON rd.DocumentID = d.DocumentID " &
              "WHERE r.RequestNo = @reqno"

        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@reqno", SelectedRequestNo)
        dr = cmd.ExecuteReader()

        dgvReqDoc.Rows.Clear()

        While dr.Read()
            Dim docName As String = dr("DocumentName").ToString()
            Dim qty As String = dr("Quantity").ToString()
            Dim fee As String = Convert.ToDecimal(dr("Amount")).ToString("N2")
            Dim subtotal As String = Convert.ToDecimal(dr("SubTotal")).ToString("N2")

            dgvReqDoc.Rows.Add(docName, qty, fee, subtotal)
        End While

        dr.Close()
        cn.Close()
    End Sub

    Private Sub btnBackToRequestList_Click(sender As Object, e As EventArgs) Handles btnBacktoRequestList.Click
        frmRequestList.Show()
        Me.Close()
    End Sub

End Class