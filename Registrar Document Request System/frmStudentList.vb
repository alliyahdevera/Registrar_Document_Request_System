Imports MySql.Data.MySqlClient

Public Class frmStudentList

    ' --- TASK 17: GRID PAGER INITIALIZATION ---
    Private pager As GridPager

    ' The student the registrar picked (read by frmNewRequest after the form closes)
    Public Property SelectedStudentID As String = ""

    Private _loading As Boolean = False

    Private Sub frmStudentList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- TASK 17: Hook pager to dgvStudents ---
        pager = New GridPager(dgvStudents)

        Me.StartPosition = FormStartPosition.CenterParent
        Me.KeyPreview = True

        dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStudents.MultiSelect = False
        dgvStudents.ReadOnly = True
        dgvStudents.AllowUserToAddRows = False

        LoadYearLevels()
        LoadStudents()
    End Sub

    ' Esc = close without choosing anyone
    Private Sub frmStudentList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    ' Fills the filter combo with the year levels that really exist in tblstudents
    Private Sub LoadYearLevels()
        _loading = True
        cboGradeLevel.Items.Clear()
        cboGradeLevel.Items.Add("All")

        Call connection()
        sql = "SELECT DISTINCT YearLevel FROM tblstudents " &
              "WHERE Status = 'Active' AND YearLevel IS NOT NULL AND YearLevel <> '' ORDER BY YearLevel"
        cmd = New MySqlCommand(sql, cn)
        dr = cmd.ExecuteReader()
        While dr.Read()
            cboGradeLevel.Items.Add(dr(0).ToString())
        End While
        dr.Close()
        cn.Close()

        cboGradeLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cboGradeLevel.SelectedIndex = 0
        _loading = False
    End Sub

    Private Sub LoadStudents()
        If _loading Then Exit Sub

        Try
            Call connection()

            sql = "SELECT * FROM tblstudents WHERE Status = 'Active' " &
                  "AND (StudentID LIKE @s OR LRN LIKE @s OR LastName LIKE @s OR FirstName LIKE @s " &
                  "OR CONCAT(FirstName, ' ', LastName) LIKE @s) "

            Dim filterYear As Boolean = (cboGradeLevel.SelectedIndex > 0)
            If filterYear Then sql &= "AND YearLevel = @yl "
            sql &= "ORDER BY LastName, FirstName"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@s", "%" & txtSearch.Text.Trim() & "%")
            If filterYear Then cmd.Parameters.AddWithValue("@yl", cboGradeLevel.Text)
            dr = cmd.ExecuteReader()

            dgvStudents.Rows.Clear()
            While dr.Read()
                dgvStudents.Rows.Add(
                    dr("StudentID").ToString(),
                    dr("LRN").ToString(),
                    dr("LastName").ToString(),
                    dr("FirstName").ToString(),
                    dr("MiddleName").ToString(),
                    dr("Course").ToString(),
                    dr("YearLevel").ToString(),
                    dr("Section").ToString(),
                    dr("ContactNo").ToString())
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading students: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' --- TASK 17: Reset pager to Page 1 on search ---
        pager.FirstPage()
        LoadStudents()
    End Sub

    Private Sub cboGradeLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGradeLevel.SelectedIndexChanged
        ' --- TASK 17: Reset pager to Page 1 on filter change ---
        pager.FirstPage()
        LoadStudents()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' --- TASK 17: Reset pager to Page 1 on refresh click ---
        pager.FirstPage()
        LoadStudents()
    End Sub

    ' Click a student row -> remember the ID, close, and give it back to the caller
    Private Sub dgvStudents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudents.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If dgvStudents.Rows(e.RowIndex).IsNewRow Then Exit Sub

        SelectedStudentID = dgvStudents.Rows(e.RowIndex).Cells("StudentID").Value.ToString()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class