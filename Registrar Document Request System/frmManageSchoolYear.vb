Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class frmManageSchoolYear
    Inherits Form

    Private WithEvents dgv As New DataGridView()
    Private WithEvents btnAdd As New Button()
    Private WithEvents btnEdit As New Button()
    Private WithEvents btnDelete As New Button()
    Private WithEvents btnClear As New Button()
    Private txtName As New TextBox()
    Private dtpStart As New DateTimePicker()
    Private dtpEnd As New DateTimePicker()
    Private _selectedID As Integer = 0

    Public Sub New()
        Me.Text = "Manage School Year"
        Me.ClientSize = New Size(640, 490)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        BuildUI()
    End Sub

    Private Sub BuildUI()
        Dim lblName As New Label With {.Text = "School Year Name", .Location = New Point(20, 15), .AutoSize = True}
        Dim lblStart As New Label With {.Text = "Start Date", .Location = New Point(320, 15), .AutoSize = True}
        Dim lblEnd As New Label With {.Text = "End Date", .Location = New Point(475, 15), .AutoSize = True}

        txtName.Location = New Point(20, 38)
        txtName.Width = 285
        txtName.MaxLength = 30

        dtpStart.Location = New Point(320, 38)
        dtpStart.Width = 140
        dtpStart.Format = DateTimePickerFormat.Custom
        dtpStart.CustomFormat = "MMM d, yyyy"

        dtpEnd.Location = New Point(475, 38)
        dtpEnd.Width = 140
        dtpEnd.Format = DateTimePickerFormat.Custom
        dtpEnd.CustomFormat = "MMM d, yyyy"

        btnAdd.Text = "Add" : btnAdd.Location = New Point(20, 78) : btnAdd.Size = New Size(90, 32)
        btnEdit.Text = "Edit" : btnEdit.Location = New Point(120, 78) : btnEdit.Size = New Size(90, 32)
        btnDelete.Text = "Delete" : btnDelete.Location = New Point(220, 78) : btnDelete.Size = New Size(90, 32)
        btnClear.Text = "Clear" : btnClear.Location = New Point(320, 78) : btnClear.Size = New Size(90, 32)

        dgv.Location = New Point(20, 125)
        dgv.Size = New Size(595, 345)
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.BackgroundColor = Color.White
        dgv.Columns.Add("colID", "ID")
        dgv.Columns("colID").Visible = False
        dgv.Columns.Add("colName", "School Year")
        dgv.Columns.Add("colStart", "Start Date (yyyy-MM-dd)")
        dgv.Columns.Add("colEnd", "End Date (yyyy-MM-dd)")

        Me.Controls.AddRange(New Control() {lblName, txtName, lblStart, dtpStart, lblEnd, dtpEnd,
                                            btnAdd, btnEdit, btnDelete, btnClear, dgv})
    End Sub

    Private Sub frmManageSchoolYear_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Call connection()
            sql = "SELECT * FROM tblschoolyear ORDER BY StartDate DESC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            dgv.Rows.Clear()
            While dr.Read()
                dgv.Rows.Add(
                    dr("SchoolYearID").ToString(),
                    dr("SchoolYearName").ToString(),
                    Convert.ToDateTime(dr("StartDate")).ToString("yyyy-MM-dd"),
                    Convert.ToDateTime(dr("EndDate")).ToString("yyyy-MM-dd"))
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading school years: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub ClearFields()
        _selectedID = 0
        txtName.Clear()
        dtpStart.Value = DateTime.Today
        dtpEnd.Value = DateTime.Today
        dgv.ClearSelection()
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex < 0 Then Exit Sub
        Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
        _selectedID = Convert.ToInt32(row.Cells("colID").Value)
        txtName.Text = row.Cells("colName").Value.ToString()
        dtpStart.Value = Date.ParseExact(row.Cells("colStart").Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)
        dtpEnd.Value = Date.ParseExact(row.Cells("colEnd").Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)
    End Sub

    Private Function IsValidInput() As Boolean
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MsgBox("Enter the school year name (example: SY 2025-2026).", vbExclamation, "Manage School Year")
            txtName.Focus()
            Return False
        End If
        If dtpEnd.Value.Date <= dtpStart.Value.Date Then
            MsgBox("End Date must be later than Start Date.", vbExclamation, "Manage School Year")
            Return False
        End If

        Try
            ' unique name
            Call connection()
            sql = "SELECT COUNT(*) FROM tblschoolyear WHERE SchoolYearName = @n AND SchoolYearID <> @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@n", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@id", _selectedID)
            Dim nameTaken As Boolean = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            cn.Close()
            If nameTaken Then
                MsgBox("A school year with that name already exists.", vbExclamation, "Manage School Year")
                Return False
            End If

            ' no overlapping dates with another school year
            Call connection()
            sql = "SELECT COUNT(*) FROM tblschoolyear WHERE StartDate <= @e AND EndDate >= @s AND SchoolYearID <> @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@s", dtpStart.Value.Date)
            cmd.Parameters.AddWithValue("@e", dtpEnd.Value.Date)
            cmd.Parameters.AddWithValue("@id", _selectedID)
            Dim overlaps As Boolean = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            cn.Close()
            If overlaps Then
                MsgBox("These dates overlap another school year.", vbExclamation, "Manage School Year")
                Return False
            End If
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error: " & ex.Message, vbCritical, "Error")
            Return False
        End Try

        Return True
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If _selectedID <> 0 Then
            MsgBox("A school year is selected. Press Clear first to add a new one.", vbExclamation, "Manage School Year")
            Exit Sub
        End If
        If Not IsValidInput() Then Exit Sub
        If Not ConfirmAction("Add this school year?", "Confirm Add") Then Exit Sub

        Try
            Call connection()
            sql = "INSERT INTO tblschoolyear (SchoolYearName, StartDate, EndDate) VALUES (@n, @s, @e)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@n", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@s", dtpStart.Value.Date)
            cmd.Parameters.AddWithValue("@e", dtpEnd.Value.Date)
            cmd.ExecuteNonQuery()
            cn.Close()

            MsgBox("School year added successfully!", vbInformation, "Success")
            LoadData()
            ClearFields()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error adding school year: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If _selectedID = 0 Then
            MsgBox("Please select a school year from the list to edit.", vbExclamation, "Manage School Year")
            Exit Sub
        End If
        If Not IsValidInput() Then Exit Sub
        If Not ConfirmAction("Save changes to this school year?", "Confirm Edit") Then Exit Sub

        Try
            Call connection()
            sql = "UPDATE tblschoolyear SET SchoolYearName = @n, StartDate = @s, EndDate = @e WHERE SchoolYearID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@n", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@s", dtpStart.Value.Date)
            cmd.Parameters.AddWithValue("@e", dtpEnd.Value.Date)
            cmd.Parameters.AddWithValue("@id", _selectedID)
            cmd.ExecuteNonQuery()
            cn.Close()

            ' keep the active filter in sync if this is the year currently selected
            If Not SchoolYear.IsAllTime AndAlso SchoolYear.SchoolYearID = _selectedID Then
                SchoolYear.SelectYear(_selectedID, txtName.Text.Trim(), dtpStart.Value.Date, dtpEnd.Value.Date)
            End If

            MsgBox("School year updated successfully!", vbInformation, "Success")
            LoadData()
            ClearFields()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error updating school year: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedID = 0 Then
            MsgBox("Please select a school year from the list to delete.", vbExclamation, "Manage School Year")
            Exit Sub
        End If
        If Not ConfirmAction("Delete this school year?" & vbCrLf & "(Requests are NOT deleted. They stay under All Time.)", "Confirm Delete") Then Exit Sub

        Try
            Call connection()
            sql = "DELETE FROM tblschoolyear WHERE SchoolYearID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", _selectedID)
            cmd.ExecuteNonQuery()
            cn.Close()

            If Not SchoolYear.IsAllTime AndAlso SchoolYear.SchoolYearID = _selectedID Then
                SchoolYear.SelectAllTime()
            End If

            MsgBox("School year deleted.", vbInformation, "Success")
            LoadData()
            ClearFields()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error deleting school year: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If txtName.Text.Trim() <> "" Then
            If Not ConfirmAction("Clear all fields?", "Confirm Clear") Then Exit Sub
        End If
        ClearFields()
    End Sub

End Class