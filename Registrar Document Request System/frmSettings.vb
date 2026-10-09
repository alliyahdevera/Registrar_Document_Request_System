Public Class frmSettings

    Private WithEvents tmrClock As New System.Windows.Forms.Timer With {.Interval = 1000}

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
        UpdateClock()
        tmrClock.Start()
        BuildUI()
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
        Me.Controls.Add(heading)

        Me.Controls.Add(btnmanagesy)
        btnmanagesy.Location = New Point(30, 80)
        btnmanagesy.Size = New Size(200, 36)
        btnmanagesy.Visible = CurrentUser.IsAdmin       ' admin only
        btnmanagesy.BringToFront()
    End Sub

    ' Redirects to frmManageSY
    Private Sub btnmanagesy_Click(sender As Object, e As EventArgs) Handles btnmanagesy.Click
        Using frm As New frmManageSY()
            frm.ShowDialog(frmMainMenu)
        End Using
    End Sub

End Class