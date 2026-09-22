Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text

Public Class frmReports

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load footer details from CurrentUser global class
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role

        ' Initialize and start real-time timer
        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        ' Default date range: today to today
        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Today

        dgvReqDoc.Rows.Clear()
        lbltotalrecords.Text = "-"
    End Sub

    ' Real-time date/time clock event
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateFooterDateTime()
    End Sub

    Private Sub UpdateFooterDateTime()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    ' Pulls requests within the selected date range (and optional search term),
    ' with the document tracking trail: Created By, Processed By, Released By.
    Private Sub LoadReports()
        Call connection()
        sql = "SELECT r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, " &
              "GROUP_CONCAT(d.DocumentName SEPARATOR ', ') AS Documents, " &
              "r.TotalAmount, r.Status, " &
              "uc.FullName AS CreatedByName, up.FullName AS ProcessedByName, ur.FullName AS ReleasedByName " &
              "FROM tblrequest r " &
              "JOIN tblstudents s ON r.StudentID = s.StudentID " &
              "LEFT JOIN tblrequestdetails rd ON rd.RequestID = r.RequestID " &
              "LEFT JOIN tbldocuments d ON d.DocumentID = rd.DocumentID " &
              "LEFT JOIN tblusers uc ON uc.UserID = r.CreatedBy " &
              "LEFT JOIN tblusers up ON up.UserID = r.ProcessedBy " &
              "LEFT JOIN tblusers ur ON ur.UserID = r.ReleasedBy " &
              "WHERE r.RequestDate BETWEEN @from AND @to " &
              "AND (r.RequestNo LIKE @search OR r.StudentID LIKE @search OR s.LastName LIKE @search) " &
              "GROUP BY r.RequestID " &
              "ORDER BY r.RequestDate DESC"

        cmd = New MySqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@from", DateTimePicker1.Value.Date)
        cmd.Parameters.AddWithValue("@to", DateTimePicker2.Value.Date)
        cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
        dr = cmd.ExecuteReader()

        dgvReqDoc.Rows.Clear()
        While dr.Read()
            dgvReqDoc.Rows.Add(
                dr("RequestNo").ToString(),
                Convert.ToDateTime(dr("RequestDate")).ToString("MMM d, yyyy"),
                dr("StudentID").ToString(),
                dr("FirstName").ToString(),
                dr("LastName").ToString(),
                If(IsDBNull(dr("Documents")), "", dr("Documents").ToString()),
                Convert.ToDecimal(dr("TotalAmount")).ToString("N2"),
                dr("Status").ToString(),
                If(IsDBNull(dr("CreatedByName")), "", dr("CreatedByName").ToString()),
                If(IsDBNull(dr("ProcessedByName")), "", dr("ProcessedByName").ToString()),
                If(IsDBNull(dr("ReleasedByName")), "", dr("ReleasedByName").ToString()))
        End While

        dr.Close()
        cn.Close()

        lbltotalrecords.Text = dgvReqDoc.Rows.Count.ToString()
    End Sub

    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        If DateTimePicker1.Value.Date > DateTimePicker2.Value.Date Then
            MsgBox("'Date From' cannot be later than 'Date To'.", vbExclamation, "Reports")
            Exit Sub
        End If

        LoadReports()
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        If dgvReqDoc.Rows.Count = 0 Then
            MsgBox("Generate a report first before exporting.", vbExclamation, "Reports")
            Exit Sub
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel (CSV) File|*.csv"
            sfd.FileName = "RequestedDocuments_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".csv"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim sb As New StringBuilder()

                    ' Header row
                    Dim headers As New List(Of String)
                    For Each col As DataGridViewColumn In dgvReqDoc.Columns
                        headers.Add(EscapeCsv(col.HeaderText))
                    Next
                    sb.AppendLine(String.Join(",", headers))

                    ' Data rows
                    For Each row As DataGridViewRow In dgvReqDoc.Rows
                        If row.IsNewRow Then Continue For
                        Dim fields As New List(Of String)
                        For Each cell As DataGridViewCell In row.Cells
                            fields.Add(EscapeCsv(If(cell.Value Is Nothing, "", cell.Value.ToString())))
                        Next
                        sb.AppendLine(String.Join(",", fields))
                    Next

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8)
                    MsgBox("Report exported successfully!", vbInformation, "Export to Excel")

                Catch ex As Exception
                    MsgBox("Failed to export report: " & ex.Message, vbCritical, "Export to Excel")
                End Try
            End If
        End Using
    End Sub

    Private Function EscapeCsv(value As String) As String
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCrLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

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

    Private Sub btnReqList_Click(sender As Object, e As EventArgs) Handles btnReqList.Click
        frmRequestList.Show()
        Me.Hide()
    End Sub

    Private Sub btnDocumentRequests_Click(sender As Object, e As EventArgs) Handles btnDocumentRequests.Click
        frmNewRequest.Show()
        Me.Hide()
    End Sub

    Private Sub btnUserManagement_Click(sender As Object, e As EventArgs) Handles btnUserManagement.Click
        frmUserManagement.Show()
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