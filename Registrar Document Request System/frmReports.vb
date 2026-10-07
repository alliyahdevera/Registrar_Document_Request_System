Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text

Public Class frmReports

    Private _loadingFilter As Boolean = False

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshUserSession()

        Timer1.Interval = 1000
        Timer1.Start()
        UpdateFooterDateTime()

        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Today

        _loadingFilter = True
        cbofilter.DropDownStyle = ComboBoxStyle.DropDownList
        If Not cbofilter.Items.Contains("All Requests") Then cbofilter.Items.Insert(0, "All Requests")
        cbofilter.SelectedIndex = 0
        _loadingFilter = False

        dgvReqDoc.Rows.Clear()
        lbltotalrecords.Text = "0"
        lbltotalamount.Text = "0.00"
    End Sub

    Private Sub frmReports_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshUserSession()
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

    Private Sub LoadReports()
        Dim statusFilter As String = GetSelectedFilter()

        Try
            Call connection()

            sql = "SELECT r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, " &
                  "GROUP_CONCAT(d.DocumentName SEPARATOR ', ') AS Documents, " &
                  "r.TotalAmount, r.Status, r.AmountPaid, r.PaymentStatus, " &
                  "uc.FullName AS CreatedByName, up.FullName AS ProcessedByName, ur.FullName AS ReleasedByName " &
                  "FROM tblrequest r " &
                  "JOIN tblstudents s ON r.StudentID = s.StudentID " &
                  "LEFT JOIN tblrequestdetails rd ON rd.RequestID = r.RequestID " &
                  "LEFT JOIN tbldocuments d ON d.DocumentID = rd.DocumentID " &
                  "LEFT JOIN tblusers uc ON uc.UserID = r.CreatedBy " &
                  "LEFT JOIN tblusers up ON up.UserID = r.ProcessedBy " &
                  "LEFT JOIN tblusers ur ON ur.UserID = r.ReleasedBy " &
                  "WHERE r.RequestDate >= @from AND r.RequestDate < @to " &
                  "AND (r.StudentID LIKE @search OR s.LastName LIKE @search) "

            If statusFilter = "Pending" Then
                sql &= "AND r.Status = 'Pending' "
            ElseIf statusFilter = "Released" Then
                sql &= "AND (r.Status = 'Released' OR r.Status = 'Completed') "
            End If

            sql &= "GROUP BY r.RequestID, r.RequestNo, r.RequestDate, r.StudentID, s.FirstName, s.LastName, " &
                   "r.TotalAmount, r.Status, r.AmountPaid, r.PaymentStatus, CreatedByName, ProcessedByName, ReleasedByName "

            If statusFilter = "ByDocType" Then
                sql &= "ORDER BY Documents ASC, r.RequestDate DESC"
            Else
                sql &= "ORDER BY r.RequestDate DESC"
            End If

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@from", DateTimePicker1.Value.Date)
            cmd.Parameters.AddWithValue("@to", DateTimePicker2.Value.Date.AddDays(1))
            cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
            dr = cmd.ExecuteReader()

            dgvReqDoc.Rows.Clear()
            Dim grandTotalAmount As Decimal = 0

            While dr.Read()
                Dim amount As Decimal = 0
                If Not IsDBNull(dr("TotalAmount")) Then
                    Decimal.TryParse(dr("TotalAmount").ToString(), amount)
                End If
                grandTotalAmount += amount

                Dim amountPaid As Decimal = 0
                If Not IsDBNull(dr("AmountPaid")) Then
                    Decimal.TryParse(dr("AmountPaid").ToString(), amountPaid)
                End If

                dgvReqDoc.Rows.Add(
                    dr("RequestNo").ToString(),
                    Convert.ToDateTime(dr("RequestDate")).ToString("MMM d, yyyy"),
                    dr("StudentID").ToString(),
                    dr("FirstName").ToString(),
                    dr("LastName").ToString(),
                    If(IsDBNull(dr("Documents")), "", dr("Documents").ToString()),
                    amount.ToString("N2"),
                    dr("Status").ToString(),
                    amountPaid.ToString("N2"),
                    If(IsDBNull(dr("PaymentStatus")) OrElse String.IsNullOrWhiteSpace(dr("PaymentStatus").ToString()), "Unpaid", dr("PaymentStatus").ToString()),
                    If(IsDBNull(dr("CreatedByName")), "", dr("CreatedByName").ToString()),
                    If(IsDBNull(dr("ProcessedByName")), "", dr("ProcessedByName").ToString()),
                    If(IsDBNull(dr("ReleasedByName")), "", dr("ReleasedByName").ToString())
                )
            End While

            dr.Close()
            cn.Close()

            Dim totalRows As Integer = dgvReqDoc.Rows.Cast(Of DataGridViewRow)().Count(Function(r) Not r.IsNewRow)
            lbltotalrecords.Text = totalRows.ToString()
            lbltotalamount.Text = grandTotalAmount.ToString("N2")

        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading report: " & ex.Message, vbCritical, "Reports")
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadReports()
    End Sub

    Private Function GetSelectedFilter() As String
        Select Case cbofilter.Text
            Case "Pending Request" : Return "Pending"
            Case "Released Request" : Return "Released"
            Case "Request By Document Type" : Return "ByDocType"
            Case Else : Return ""
        End Select
    End Function

    Private Function DatesAreValid() As Boolean
        If DateTimePicker1.Value.Date > DateTimePicker2.Value.Date Then
            MsgBox("'Date From' cannot be later than 'Date To'.", vbExclamation, "Reports")
            Return False
        End If
        Return True
    End Function

    Private Sub cbofilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbofilter.SelectedIndexChanged
        If _loadingFilter Then Exit Sub
        If Not DatesAreValid() Then Exit Sub
        LoadReports()
    End Sub

    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        If Not DatesAreValid() Then Exit Sub
        LoadReports()
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Dim totalRows As Integer = dgvReqDoc.Rows.Cast(Of DataGridViewRow)().Count(Function(r) Not r.IsNewRow)
        If totalRows = 0 Then
            MsgBox("Generate a report first before exporting.", vbExclamation, "Reports")
            Exit Sub
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel (CSV) File|*.csv"
            sfd.FileName = "RequestedDocuments_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".csv"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim sb As New StringBuilder()

                    Dim headers As New List(Of String)
                    For Each col As DataGridViewColumn In dgvReqDoc.Columns
                        headers.Add(EscapeCsv(col.HeaderText))
                    Next
                    sb.AppendLine(String.Join(",", headers))

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

End Class