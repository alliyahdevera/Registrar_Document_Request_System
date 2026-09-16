Imports MySql.Data.MySqlClient

Public Class frmRequestList

    Private Sub frmRequestList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable full row selection
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        LoadRequests()
    End Sub
    Public Sub LoadRequests(Optional searchTerm As String = "")
        Call connection()

        sql = "SELECT r.RequestNo, r.RequestDate, r.StudentID, " &
          "s.FirstName, s.LastName, " &
          "IFNULL(MIN(d.DocumentName), 'N/A') AS DocumentRequested, " &
          "r.TotalAmount, r.Status " &
          "FROM tblrequest r " &
          "JOIN tblstudents s ON r.StudentID = s.StudentID " &
          "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
          "LEFT JOIN tbldocuments d ON rd.DocumentID = d.DocumentID "

        ' Apply search filter if search term is provided
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            sql &= "WHERE r.RequestNo LIKE @search OR r.StudentID LIKE @search " &
               "OR s.FirstName LIKE @search OR s.LastName LIKE @search OR r.Status LIKE @search "
        End If

        ' Append GROUP BY and ORDER BY once at the end
        sql &= " GROUP BY r.RequestID, r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, r.TotalAmount, r.Status " &
           " ORDER BY r.RequestDate DESC, r.RequestNo DESC"

        cmd = New MySqlCommand(sql, cn)

        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            cmd.Parameters.AddWithValue("@search", "%" & searchTerm.Trim() & "%")
        End If

        dr = cmd.ExecuteReader()
        dgvReqDoc.Rows.Clear()

        While dr.Read()
            Dim formattedDate As String = Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd")
            Dim formattedTotal As String = Convert.ToDecimal(dr("TotalAmount")).ToString("N2")

            Dim currentStatus As String = dr("Status").ToString()
            If String.IsNullOrWhiteSpace(currentStatus) Then currentStatus = "Pending"

            dgvReqDoc.Rows.Add(
            dr("RequestNo").ToString(),
            formattedDate,
            dr("StudentID").ToString(),
            dr("FirstName").ToString(),
            dr("LastName").ToString(),
            dr("DocumentRequested").ToString(),
            formattedTotal,
            currentStatus
        )
        End While

        dr.Close()
        cn.Close()
    End Sub

    ' Search functionality
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadRequests(txtSearch.Text.Trim())
    End Sub

    ' View Details Button Navigation
    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        OpenSelectedDetails()
    End Sub

    ' Double-Click Row Navigation
    Private Sub dgvReqDoc_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReqDoc.CellDoubleClick
        If e.RowIndex >= 0 Then
            OpenSelectedDetails()
        End If
    End Sub

    Private Sub OpenSelectedDetails()
        If dgvReqDoc.SelectedRows.Count > 0 Then
            Dim reqNo As String = dgvReqDoc.SelectedRows(0).Cells(0).Value.ToString()

            Dim detailsForm As New frmRequestDetails()
            detailsForm.SelectedRequestNo = reqNo
            detailsForm.Show()
            Me.Hide()
        Else
            MessageBox.Show("Please select a request from the list first.", "Select Request", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class