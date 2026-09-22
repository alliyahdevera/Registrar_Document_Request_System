Imports MySql.Data.MySqlClient

Public Class frmRequestList

    Private Sub frmRequestList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set bottom footer values
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        ' Initialize and start real-time clock timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Automatically cancel unpaid requests past 7 days before loading grid
        AutoCancelUnpaidRequests()

        ' Load request data
        LoadRequests()
    End Sub

    ' Real-time date/time clock event
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    ''' <summary>
    ''' Automatically updates status to 'Cancelled' for any unpaid requests older than 7 days.
    ''' </summary>
    Private Sub AutoCancelUnpaidRequests()
        Try
            Call connection()
            sql = "UPDATE tblrequest " &
                  "SET Status = 'Cancelled' " &
                  "WHERE PaymentStatus = 'Unpaid' " &
                  "AND Status != 'Cancelled' " &
                  "AND RequestDate <= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Fetches requests and maps them directly to the DataGridView columns using '-' for null fields.
    ''' </summary>
    Public Sub LoadRequests()
        Try
            Call connection()

            sql = "SELECT r.RequestID, r.RequestNo, r.StudentID, " &
                  "s.FirstName, s.LastName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.RequestDate, r.TotalAmount, r.Status, " &
                  "u1.FullName AS CreatedByStaff, " &
                  "u2.FullName AS ProcessedByStaff, " &
                  "u3.FullName AS ReleasedByStaff " &
                  "FROM tblrequest r " &
                  "LEFT JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON r.RequestID = rd.RequestID " &
                  "LEFT JOIN tbldocuments d ON CAST(rd.DocumentID AS CHAR) = CAST(d.DocumentID AS CHAR) " &
                  "LEFT JOIN tblusers u1 ON r.CreatedBy = u1.UserID " &
                  "LEFT JOIN tblusers u2 ON r.ProcessedBy = u2.UserID " &
                  "LEFT JOIN tblusers u3 ON r.ReleasedBy = u3.UserID " &
                  "GROUP BY r.RequestID, r.RequestNo, r.StudentID, s.FirstName, s.LastName, " &
                  "r.RequestDate, r.TotalAmount, r.Status, " &
                  "CreatedByStaff, ProcessedByStaff, ReleasedByStaff " &
                  "ORDER BY r.RequestID DESC"

            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                Dim totalAmtStr As String = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))

                dgvReqDoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    reqDateStr,
                    dr("StudentID").ToString(),
                    dr("FirstName").ToString(),
                    dr("LastName").ToString(),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    totalAmtStr,
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

    ' Real-time search filter
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Call connection()

            sql = "SELECT r.RequestID, r.RequestNo, r.StudentID, " &
                  "s.FirstName, s.LastName, " &
                  "GROUP_CONCAT(DISTINCT d.DocumentName SEPARATOR ', ') AS DocumentNames, " &
                  "r.RequestDate, r.TotalAmount, r.Status, " &
                  "u1.FullName AS CreatedByStaff, " &
                  "u2.FullName AS ProcessedByStaff, " &
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
                  "r.RequestDate, r.TotalAmount, r.Status, " &
                  "CreatedByStaff, ProcessedByStaff, ReleasedByStaff " &
                  "ORDER BY r.RequestID DESC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            While dr.Read()
                Dim reqDateStr As String = If(IsDBNull(dr("RequestDate")), "-", Convert.ToDateTime(dr("RequestDate")).ToString("yyyy-MM-dd"))
                Dim totalAmtStr As String = If(IsDBNull(dr("TotalAmount")), "0.00", Convert.ToDecimal(dr("TotalAmount")).ToString("N2"))

                dgvReqDoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    reqDateStr,
                    dr("StudentID").ToString(),
                    dr("FirstName").ToString(),
                    dr("LastName").ToString(),
                    If(IsDBNull(dr("DocumentNames")), "-", dr("DocumentNames").ToString()),
                    totalAmtStr,
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

    ' Navigation Handlers
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