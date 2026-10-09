Imports MySql.Data.MySqlClient

Public Class frmSettings

    Private WithEvents tmrClock As New System.Windows.Forms.Timer With {.Interval = 1000}
    Private WithEvents lstYears As New ListBox()
    Private lblCurrent As New Label()
    Private _loading As Boolean = False

    Private Class YearItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property StartD As Date
        Public Property EndD As Date

        Public Overrides Function ToString() As String
            If ID = 0 Then Return Name
            Return Name & "     (" & StartD.ToString("MMM d, yyyy") & "  -  " & EndD.ToString("MMM d, yyyy") & ")"
        End Function
    End Class

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
        UpdateClock()
        tmrClock.Start()

        BuildUI()
        LoadYears()
    End Sub

    Private Sub frmSettings_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        tmrClock.Stop()
        tmrClock.Dispose()
    End Sub

    Private Sub tmrClock_Tick(sender As Object, e As EventArgs) Handles tmrClock.Tick
        UpdateClock()
    End Sub

    Private Sub UpdateClock()
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    Private Sub BuildUI()
        Dim heading As New Label With {
            .Text = "Settings",
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .ForeColor = Color.FromArgb(0, 0, 102),
            .AutoSize = True,
            .Location = New Point(30, 20)}

        Dim card As New Panel With {
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .Location = New Point(30, 80),
            .Size = New Size(620, 470)}

        Dim lblTitle As New Label With {
            .Text = "SELECT SCHOOL YEAR",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Location = New Point(20, 15),
            .AutoSize = True}

        Dim lblHint As New Label With {
            .Text = "Click a school year to show only its data in the Dashboard, Request List and Reports. " &
                    "Choose All Time to see every year combined.",
            .ForeColor = Color.DimGray,
            .Location = New Point(20, 45),
            .Size = New Size(575, 40)}

        lstYears.Location = New Point(20, 95)
        lstYears.Size = New Size(575, 280)
        lstYears.Font = New Font("Segoe UI", 12)
        lstYears.IntegralHeight = False
        lstYears.BorderStyle = BorderStyle.FixedSingle

        lblCurrent.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblCurrent.ForeColor = Color.FromArgb(0, 0, 102)
        lblCurrent.AutoSize = True
        lblCurrent.Location = New Point(20, 385)

        ' move the existing designer button into the card
        card.Controls.Add(btnmanagesy)
        btnmanagesy.Location = New Point(20, 420)
        btnmanagesy.Size = New Size(200, 36)
        btnmanagesy.Visible = CurrentUser.IsAdmin       ' admin only

        card.Controls.AddRange(New Control() {lblTitle, lblHint, lstYears, lblCurrent})
        Me.Controls.Add(heading)
        Me.Controls.Add(card)
    End Sub

    Private Sub LoadYears()
        _loading = True
        lstYears.Items.Clear()
        lstYears.Items.Add(New YearItem With {.ID = 0, .Name = "All Time"})

        Try
            Call connection()
            sql = "SELECT * FROM tblschoolyear ORDER BY StartDate DESC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                lstYears.Items.Add(New YearItem With {
                    .ID = Convert.ToInt32(dr("SchoolYearID")),
                    .Name = dr("SchoolYearName").ToString(),
                    .StartD = Convert.ToDateTime(dr("StartDate")),
                    .EndD = Convert.ToDateTime(dr("EndDate"))})
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading school years: " & ex.Message, vbCritical, "Error")
        End Try

        ' re-select whatever is currently active
        Dim idx As Integer = 0
        If Not SchoolYear.IsAllTime Then
            Dim found As Boolean = False
            For i As Integer = 1 To lstYears.Items.Count - 1
                Dim item As YearItem = CType(lstYears.Items(i), YearItem)
                If item.ID = SchoolYear.SchoolYearID Then
                    SchoolYear.SelectYear(item.ID, item.Name, item.StartD, item.EndD)   ' pick up edited dates
                    idx = i
                    found = True
                    Exit For
                End If
            Next
            If Not found Then SchoolYear.SelectAllTime()      ' the selected year was deleted
        End If

        lstYears.SelectedIndex = idx
        UpdateCurrentLabel()
        _loading = False
    End Sub

    Private Sub lstYears_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstYears.SelectedIndexChanged
        If _loading Then Exit Sub
        Dim item As YearItem = TryCast(lstYears.SelectedItem, YearItem)
        If item Is Nothing Then Exit Sub

        If item.ID = 0 Then
            SchoolYear.SelectAllTime()
        Else
            SchoolYear.SelectYear(item.ID, item.Name, item.StartD, item.EndD)
        End If
        UpdateCurrentLabel()
    End Sub

    Private Sub UpdateCurrentLabel()
        lblCurrent.Text = "Currently viewing:  " & SchoolYear.DisplayName
    End Sub


End Class