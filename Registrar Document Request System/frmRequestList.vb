Imports MySql.Data.MySqlClient

Public Class frmRequestList

    Private Sub frmRequestList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        dgvReqDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReqDoc.MultiSelect = False

        AutoCancelUnpaidRequests()

        AutoCancelUnclaimedReadyForRelease()

        FixMissingProcessedByData()

        LoadRequests()
    End Sub

    Private Sub frmRequestList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshUserSession()
        AutoCancelUnpaidRequests()
        AutoCancelUnclaimedReadyForRelease()
        FixMissingProcessedByData()
        LoadRequests()
    End Sub

    Private Sub RefreshUserSession()
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub
    Private Sub AutoCancelUnclaimedReadyForRelease()
        Try
            Call connection()

            sql = "UPDATE tblrequest " &
              "SET Status = 'Cancelled', ReadyForReleaseDate = NULL " &
              "WHERE Status = 'Ready for Release' " &
              "AND ReadyForReleaseDate IS NOT NULL " &
              "AND ReadyForReleaseDate <= DATE_SUB(NOW(), INTERVAL 1 MONTH)"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
    Private Sub AutoCancelUnpaidRequests()
        Try
            Call connection()

            sql = "UPDATE tblrequest " &
                  "SET Status = 'Cancelled' " &
                  "WHERE (Status = 'Pending' OR Status IS NULL OR Status = '') " &
                  "AND RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
    Private Sub FixMissingProcessedByData()
        Try
            Call connection()

            sql = "UPDATE tblrequest " &
                  "SET ProcessedBy = CreatedBy " &
                  "WHERE Status IN ('Processing', 'Ready for Release', 'Released') " &
                  "AND (ProcessedBy IS NULL OR ProcessedBy = 0)"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
    Public Sub LoadRequests()
        Try
            Call connection()

            sql = "SELECT r.RequestID, r.RequestNo, r.StudentID, " &
                  "s.FirstName, s.LastName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.RequestDate, r.TotalAmount, r.PaymentStatus, r.AmountPaid, " &
                  "r.ORNo, r.ORDate, r.Status, " &
                  "u1.FullName AS CreatedByStaff, " &
                  "CASE " &
                  "  WHEN r.Status IN ('Processing', 'Ready for Release', 'Released') THEN COALESCE(u2.FullName, u1.FullName) " &
                  "  ELSE NULL " &
                  "END AS ProcessedByStaff, " &
                  "u3.FullName AS ReleasedByStaff " &
                  "FROM tblrequest r " &
                  "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
                  "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "LEFT JOIN tblusers u1 ON r.CreatedBy = u1.UserID " &
                  "LEFT JOIN tblusers u2 ON r.ProcessedBy = u2.UserID " &
                  "LEFT JOIN tblusers u3 ON r.ReleasedBy = u3.UserID " &
                  "GROUP BY r.RequestID, r.RequestNo, r.StudentID, s.FirstName, s.LastName, " &
                  "r.RequestDate, r.TotalAmount, r.PaymentStatus, r.AmountPaid, " &
                  "r.ORNo, r.ORDate, r.Status, " &
                  "u1.FullName, u2.FullName, u3.FullName " &
                  "ORDER BY r.RequestID DESC"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                Dim orDateStr As String = If(IsDBNull(dr("ORDate")), "-", Convert.ToDateTime(dr("ORDate")).ToString("yyyy-MM-dd"))
                Dim totalAmtStr As String = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))
                Dim amtPaidStr As String = If(IsDBNull(dr("AmountPaid")), "0.00", Convert.ToDecimal(dr("AmountPaid")).ToString("N2"))

                dgvReqDoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    reqDateStr,
                    dr("StudentID").ToString(),
                    dr("FirstName").ToString(),
                    dr("LastName").ToString(),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    totalAmtStr,
                    If(IsDBNull(dr("PaymentStatus")), "-", dr("PaymentStatus").ToString()),
                    amtPaidStr,
                    If(IsDBNull(dr("ORNo")), "-", dr("ORNo").ToString()),
                    orDateStr,
                    If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "-", dr("Status").ToString()),
                    If(IsDBNull(dr("CreatedByStaff")), "-", dr("CreatedByStaff").ToString()),
                    If(IsDBNull(dr("ProcessedByStaff")), "-", dr("ProcessedByStaff").ToString()),
                    If(IsDBNull(dr("ReleasedByStaff")), "-", dr("ReleasedByStaff").ToString())
                )
            End While

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading requests: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Call connection()

            sql = "SELECT r.RequestID, r.RequestNo, r.StudentID, " &
                  "s.FirstName, s.LastName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.RequestDate, r.TotalAmount, r.PaymentStatus, r.AmountPaid, " &
                  "r.ORNo, r.ORDate, r.Status, " &
                  "u1.FullName AS CreatedByStaff, " &
                  "CASE " &
                  "  WHEN r.Status IN ('Processing', 'Ready for Release', 'Released') THEN COALESCE(u2.FullName, u1.FullName) " &
                  "  ELSE NULL " &
                  "END AS ProcessedByStaff, " &
                  "u3.FullName AS ReleasedByStaff " &
                  "FROM tblrequest r " &
                  "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
                  "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "LEFT JOIN tblusers u1 ON r.CreatedBy = u1.UserID " &
                  "LEFT JOIN tblusers u2 ON r.ProcessedBy = u2.UserID " &
                  "LEFT JOIN tblusers u3 ON r.ReleasedBy = u3.UserID " &
                  "WHERE r.RequestNo LIKE @search OR r.StudentID LIKE @search OR s.LastName LIKE @search OR s.FirstName LIKE @search " &
                  "GROUP BY r.RequestID, r.RequestNo, r.StudentID, s.FirstName, s.LastName, " &
                  "r.RequestDate, r.TotalAmount, r.PaymentStatus, r.AmountPaid, " &
                  "r.ORNo, r.ORDate, r.Status, " &
                  "u1.FullName, u2.FullName, u3.FullName " &
                  "ORDER BY r.RequestID DESC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                Dim orDateStr As String = If(IsDBNull(dr("ORDate")), "-", Convert.ToDateTime(dr("ORDate")).ToString("yyyy-MM-dd"))
                Dim totalAmtStr As String = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))
                Dim amtPaidStr As String = If(IsDBNull(dr("AmountPaid")), "0.00", Convert.ToDecimal(dr("AmountPaid")).ToString("N2"))

                dgvReqDoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    reqDateStr,
                    dr("StudentID").ToString(),
                    dr("FirstName").ToString(),
                    dr("LastName").ToString(),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    totalAmtStr,
                    If(IsDBNull(dr("PaymentStatus")), "-", dr("PaymentStatus").ToString()),
                    amtPaidStr,
                    If(IsDBNull(dr("ORNo")), "-", dr("ORNo").ToString()),
                    orDateStr,
                    If(IsDBNull(dr("Status")) OrElse String.IsNullOrWhiteSpace(dr("Status").ToString()), "-", dr("Status").ToString()),
                    If(IsDBNull(dr("CreatedByStaff")), "-", dr("CreatedByStaff").ToString()),
                    If(IsDBNull(dr("ProcessedByStaff")), "-", dr("ProcessedByStaff").ToString()),
                    If(IsDBNull(dr("ReleasedByStaff")), "-", dr("ReleasedByStaff").ToString())
                )
            End While

            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub
    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        OpenSelectedRequestDetails()
    End Sub

    Private Sub dgvReqDoc_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReqDoc.CellDoubleClick
        If e.RowIndex >= 0 Then
            OpenSelectedRequestDetails()
        End If
    End Sub

    Private Sub OpenSelectedRequestDetails()
        If dgvReqDoc.SelectedRows.Count > 0 Then
            Dim selectedReqNo As String = dgvReqDoc.SelectedRows(0).Cells(0).Value.ToString()

            frmRequestDetails.SelectedRequestNo = selectedReqNo
            frmRequestDetails.LoadRequestDetailsInfo(selectedReqNo)
            frmRequestDetails.Show()
            Me.Hide()
        Else
            MsgBox("Please select a request row from the list first.", vbInformation, "No Selection")
        End If
    End Sub
End Class