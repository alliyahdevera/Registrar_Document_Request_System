Imports MySql.Data.MySqlClient

Public Class frmRequestList

    Private Sub frmRequestList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Force whole row highlight on click
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        If CurrentUser.Role <> "Administrator" Then
            btnUserManagement.Visible = False
        End If

        LoadRequests()
    End Sub

    Public Sub LoadRequests(Optional searchTerm As String = "")
        Call connection()

        ' MIN() guarantees only ONE document name is selected without slashes or commas
        sql = "SELECT r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, " &
          "IFNULL( " &
          "  MIN(d.DocumentName), " &
          "  (SELECT doc.DocumentName FROM tbldocuments doc WHERE doc.Fee = r.TotalAmount OR (r.TotalAmount % doc.Fee = 0 AND doc.Fee > 0) LIMIT 1) " &
          ") AS DocumentsRequested, " &
          "r.TotalAmount, r.Status " &
          "FROM tblrequest r " &
          "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
          "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
          "LEFT JOIN tbldocuments d ON rd.DocumentID = d.DocumentID "

        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            sql &= "WHERE r.RequestNo LIKE @search OR r.StudentID LIKE @search " &
               "OR s.FirstName LIKE @search OR s.LastName LIKE @search OR r.Status LIKE @search "
        End If

        sql &= "GROUP BY r.RequestID, r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, r.TotalAmount, r.Status " &
           "ORDER BY r.RequestDate DESC, r.RequestNo DESC"

        cmd = New MySqlCommand(sql, cn)

        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            cmd.Parameters.AddWithValue("@search", "%" & searchTerm.Trim() & "%")
        End If

        dr = cmd.ExecuteReader()

        dgvReqDoc.Rows.Clear()
        While dr.Read()
            Dim formattedDate As String = Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd")
            Dim formattedTotal As String = Convert.ToDecimal(dr("TotalAmount")).ToString("N2")

            dgvReqDoc.Rows.Add(
            dr("RequestNo").ToString(),
            formattedDate,
            dr("StudentID").ToString(),
            dr("FirstName").ToString(),
            dr("LastName").ToString(),
            If(IsDBNull(dr("DocumentsRequested")), "Unmapped Request", dr("DocumentsRequested").ToString()),
            formattedTotal,
            dr("Status").ToString()
        )
        End While

        dr.Close()
        cn.Close()
    End Sub

    ' Search box filter handler
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadRequests(txtSearch.Text)
    End Sub

    ' View Details button logic
    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        OpenSelectedRequestDetails()
    End Sub

    ' Double-clicking a grid row
    Private Sub dgvReqDoc_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReqDoc.CellDoubleClick
        If e.RowIndex >= 0 Then
            OpenSelectedRequestDetails()
        End If
    End Sub

    Private Sub OpenSelectedRequestDetails()
        If dgvReqDoc.CurrentRow Is Nothing OrElse dgvReqDoc.CurrentRow.IsNewRow Then
            MsgBox("Please select a request from the list.", vbExclamation, "Request List")
            Exit Sub
        End If

        Dim selectedRequestNo As String = dgvReqDoc.CurrentRow.Cells(0).Value.ToString()

        ' Pass selected RequestNo to frmRequestDetails and navigate
        frmRequestDetails.SelectedRequestNo = selectedRequestNo
        frmRequestDetails.Show()
        Me.Hide()
    End Sub

    ' Navigation Menu Handlers
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

    Private Sub btnDocumentRequests_Click(sender As Object, e As EventArgs) Handles btnDocumentRequests.Click
        LoadRequests()
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