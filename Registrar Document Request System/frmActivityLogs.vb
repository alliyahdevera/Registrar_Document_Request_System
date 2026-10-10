Imports MySql.Data.MySqlClient

Public Class frmActivityLogs

    Private WithEvents tmrClock As New System.Windows.Forms.Timer() With {.Interval = 1000}

    Private Sub frmActivityLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not CurrentUser.IsAdmin Then
            MsgBox("Only the Administrator can view Activity Logs.", vbExclamation, "Access Denied")
            Exit Sub
        End If

        lblname.Text = CurrentUser.FullName
        lblposition.Text = CurrentUser.Role
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
        tmrClock.Start()
        AddHandler Me.Disposed, Sub() tmrClock.Stop()

        With dgvActivityHistory
            If Not .Columns.Contains("LogDate") Then
                .Columns.Insert(0, New DataGridViewTextBoxColumn() With {.Name = "LogDate", .HeaderText = "Date / Time", .Width = 150})
            End If
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns("Details").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        dtpfrom.Value = Date.Today.AddDays(-7)
        dtpto.Value = Date.Today
        LoadLogs()
    End Sub

    Private Sub tmrClock_Tick(sender As Object, e As EventArgs) Handles tmrClock.Tick
        lbldatetime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt")
    End Sub

    Private Sub btngenerate_Click(sender As Object, e As EventArgs) Handles btngenerate.Click
        LoadLogs()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadLogs()
    End Sub

    Private Sub LoadLogs()
        If dtpto.Value.Date < dtpfrom.Value.Date Then
            MsgBox("'To' date cannot be earlier than 'From' date.", vbExclamation, "Activity Logs")
            Exit Sub
        End If

        Try
            Call connection()
            sql = "SELECT LogDate, Username, FullName, Role, ActionType, Details FROM tblactivitylogs " &
                  "WHERE LogDate >= @f AND LogDate < @t " &
                  "AND (Username LIKE @s OR ActionType LIKE @s OR FullName LIKE @s) " &
                  "ORDER BY LogDate DESC, LogID DESC"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@f", dtpfrom.Value.Date)
            cmd.Parameters.AddWithValue("@t", dtpto.Value.Date.AddDays(1))
            cmd.Parameters.AddWithValue("@s", "%" & txtSearch.Text.Trim() & "%")
            dr = cmd.ExecuteReader()

            dgvActivityHistory.Rows.Clear()
            While dr.Read()
                dgvActivityHistory.Rows.Add(Convert.ToDateTime(dr("LogDate")).ToString("yyyy-MM-dd hh:mm tt"),
                                            dr("Username").ToString(), dr("FullName").ToString(),
                                            dr("Role").ToString(), dr("ActionType").ToString(),
                                            dr("Details").ToString())
            End While
            dr.Close()
            cn.Close()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then cn.Close()
            MsgBox("Error loading activity logs: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub
End Class