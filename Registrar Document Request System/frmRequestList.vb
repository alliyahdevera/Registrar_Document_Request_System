Imports System.Data.SqlTypes
Imports MySql.Data.MySqlClient

Public Class frmRequestList

    Private Sub frmRequestList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set bottom footer values
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        ' Start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Enable full row selection
        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        LoadRequests()
    End Sub

    ' Real-time date/time update event
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    Public Sub LoadRequests(Optional searchTerm As String = "")
        Call connection()

        ' SQL Query with CAST to handle DocumentID type/formatting mismatches
        sql = "SELECT r.RequestNo, r.RequestDate, r.StudentID, " &
              "s.FirstName, s.LastName, " &
              "IFNULL(GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', '), 'N/A') AS DocumentRequested, " &
              "r.TotalAmount, r.Status, " &
              "IFNULL(u1.FullName, '-') AS CreatedBy, " &
              "IFNULL(u2.FullName, '-') AS ProcessedBy, " &
              "IFNULL(u3.FullName, '-') AS ReleasedBy " &
              "FROM tblrequest r " &
              "JOIN tblstudents s ON r.StudentID = s.StudentID " &
              "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
              "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
              "LEFT JOIN tblusers u1 ON r.CreatedBy = u1.UserID " &
              "LEFT JOIN tblusers u2 ON r.ProcessedBy = u2.UserID " &
              "LEFT JOIN tblusers u3 ON r.ReleasedBy = u3.UserID "

        ' Apply search filter if search term is provided
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            sql &= "WHERE r.RequestNo LIKE @search OR r.StudentID LIKE @search " &
                   "OR s.FirstName LIKE @search OR s.LastName LIKE @search OR r.Status LIKE @search "
        End If

        ' Group by RequestID to aggregate documents
        sql &= " GROUP BY r.RequestID, r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, r.TotalAmount, r.Status, u1.FullName, u2.FullName, u3.FullName " &
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
                currentStatus,
                dr("CreatedBy").ToString(),
                dr("ProcessedBy").ToString(),
                dr("ReleasedBy").ToString()
            )
        End While

        dr.Close()
        cn.Close()
    End Sub

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
        frmNewRequest.Show()
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