Imports MySql.Data.MySqlClient

Public Class frmManageSY

    Private selectedID As Integer = 0

    Private Sub frmManageSY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvsy.AllowUserToAddRows = False
        dgvsy.ReadOnly = True
        dgvsy.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvsy.MultiSelect = False
        dgvsy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker2.Format = DateTimePickerFormat.Short
        txtStudentID.MaxLength = 30

        LoadYears()
        ClearFields()
    End Sub

    Private Sub LoadYears()
        dgvsy.Rows.Clear()
        Try
            If Not connection() Then Exit Sub
            sql = "SELECT SchoolYearID, SchoolYearName, StartDate, EndDate FROM tblschoolyear ORDER BY StartDate DESC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim sd As Date = Convert.ToDateTime(dr("StartDate"))
                Dim ed As Date = Convert.ToDateTime(dr("EndDate"))
                Dim idx As Integer = dgvsy.Rows.Add(dr("SchoolYearName").ToString(),
                                                    sd.ToString("MMM d, yyyy"),
                                                    ed.ToString("MMM d, yyyy"))
                dgvsy.Rows(idx).Tag = Tuple.Create(Convert.ToInt32(dr("SchoolYearID")), sd, ed)
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error loading school years: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
        dgvsy.ClearSelection()
    End Sub

    Private Sub ClearFields()
        selectedID = 0
        txtStudentID.Clear()
        DateTimePicker1.Value = Date.Today
        DateTimePicker2.Value = Date.Today
        dgvsy.ClearSelection()
        txtStudentID.Focus()
    End Sub

    Private Sub dgvsy_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvsy.CellClick
        If e.RowIndex < 0 Then Exit Sub
        Dim info = CType(dgvsy.Rows(e.RowIndex).Tag, Tuple(Of Integer, Date, Date))
        selectedID = info.Item1
        txtStudentID.Text = dgvsy.Rows(e.RowIndex).Cells(0).Value.ToString()
        DateTimePicker1.Value = info.Item2
        DateTimePicker2.Value = info.Item3
    End Sub

    ' Checks name, dates, duplicate name and overlapping dates.
    ' excludeID = the row being edited (0 when adding).
    Private Function IsValidInput(excludeID As Integer) As Boolean
        Dim name As String = txtStudentID.Text.Trim()

        If name = "" Then
            MsgBox("Please enter the school year name (e.g. SY 2027-2028).", vbExclamation, "Manage School Year")
            txtStudentID.Focus()
            Return False
        End If

        If DateTimePicker2.Value.Date <= DateTimePicker1.Value.Date Then
            MsgBox("End Date must be later than Start Date.", vbExclamation, "Manage School Year")
            DateTimePicker2.Focus()
            Return False
        End If

        Try
            If Not connection() Then Return False

            cmd = New MySqlCommand("SELECT COUNT(*) FROM tblschoolyear WHERE SchoolYearName = @n AND SchoolYearID <> @id", cn)
            cmd.Parameters.AddWithValue("@n", name)
            cmd.Parameters.AddWithValue("@id", excludeID)
            If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then
                MsgBox("A school year named '" & name & "' already exists.", vbExclamation, "Manage School Year")
                Return False
            End If

            cmd = New MySqlCommand("SELECT COUNT(*) FROM tblschoolyear " &
                                   "WHERE StartDate <= @e AND EndDate >= @s AND SchoolYearID <> @id", cn)
            cmd.Parameters.AddWithValue("@s", DateTimePicker1.Value.Date)
            cmd.Parameters.AddWithValue("@e", DateTimePicker2.Value.Date)
            cmd.Parameters.AddWithValue("@id", excludeID)
            If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then
                MsgBox("These dates overlap with another school year.", vbExclamation, "Manage School Year")
                Return False
            End If
        Catch ex As Exception
            MsgBox("Error validating: " & ex.Message, vbCritical, "Error")
            Return False
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try

        Return True
    End Function

    ' ADD
    Private Sub btnsavereq_Click(sender As Object, e As EventArgs) Handles btnsavereq.Click
        If Not IsValidInput(0) Then Exit Sub
        If Not ConfirmAction("Add this school year?", "Confirm Add") Then Exit Sub

        Try
            If Not connection() Then Exit Sub
            sql = "INSERT INTO tblschoolyear (SchoolYearName, StartDate, EndDate) VALUES (@n, @s, @e)"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@n", txtStudentID.Text.Trim())
            cmd.Parameters.AddWithValue("@s", DateTimePicker1.Value.Date)
            cmd.Parameters.AddWithValue("@e", DateTimePicker2.Value.Date)
            cmd.ExecuteNonQuery()
            MsgBox("School year added successfully!", vbInformation, "Success")
        Catch ex As Exception
            MsgBox("Error adding school year: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try

        LoadYears()
        ClearFields()
    End Sub

    ' EDIT
    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        If selectedID = 0 Then
            MsgBox("Please click a school year in the list first.", vbExclamation, "Manage School Year")
            Exit Sub
        End If
        If Not IsValidInput(selectedID) Then Exit Sub
        If Not ConfirmAction("Save the changes to this school year?", "Confirm Edit") Then Exit Sub

        Try
            If Not connection() Then Exit Sub
            sql = "UPDATE tblschoolyear SET SchoolYearName = @n, StartDate = @s, EndDate = @e WHERE SchoolYearID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@n", txtStudentID.Text.Trim())
            cmd.Parameters.AddWithValue("@s", DateTimePicker1.Value.Date)
            cmd.Parameters.AddWithValue("@e", DateTimePicker2.Value.Date)
            cmd.Parameters.AddWithValue("@id", selectedID)
            cmd.ExecuteNonQuery()
            MsgBox("School year updated successfully!", vbInformation, "Success")
        Catch ex As Exception
            MsgBox("Error updating school year: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try

        LoadYears()
        ClearFields()
    End Sub

    ' DELETE
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selectedID = 0 Then
            MsgBox("Please click a school year in the list first.", vbExclamation, "Manage School Year")
            Exit Sub
        End If
        If Not ConfirmAction("Delete '" & txtStudentID.Text.Trim() & "'?", "Confirm Delete") Then Exit Sub

        Try
            If Not connection() Then Exit Sub
            cmd = New MySqlCommand("DELETE FROM tblschoolyear WHERE SchoolYearID = @id", cn)
            cmd.Parameters.AddWithValue("@id", selectedID)
            cmd.ExecuteNonQuery()
            MsgBox("School year deleted.", vbInformation, "Success")
        Catch ex As Exception
            MsgBox("Error deleting school year: " & ex.Message, vbCritical, "Error")
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try

        LoadYears()
        ClearFields()
    End Sub

    ' CLEAR
    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        ClearFields()
    End Sub

End Class