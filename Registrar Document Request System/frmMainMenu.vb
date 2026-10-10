Imports MySql.Data.MySqlClient
Public Class frmMainMenu

    Private _currentPage As Form = Nothing
    Private _loggingOut As Boolean = False
    Private _navButtons As Button()

    Private ReadOnly NavNormalColor As Color = Color.FromArgb(1, 21, 78)
    Private ReadOnly NavActiveColor As Color = Color.FromArgb(38, 70, 160)


    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _navButtons = New Button() {btnMainMenu, btnStudentManagement, btnDocumentManagement,
                                    btnDocumentRequests, btnReqList, btnReport, btnUserManagement,
                                    btnsettings, btnactLogs}
        RefreshUserSession()
        ApplyAutoRules()
        If CurrentUser.IsAdmin Then
            OpenDashboard()
        Else
            ShowPage(New frmNewRequest(), btnDocumentRequests)   ' staff never see the dashboard
        End If
    End Sub

    Private Sub RefreshUserSession()
        Dim isAdmin As Boolean = CurrentUser.IsAdmin
        btnMainMenu.Visible = isAdmin               ' Dashboard: admin only
        btnactLogs.Visible = isAdmin                ' Activity Logs: admin only
        btnUserManagement.Visible = isAdmin
        btnDocumentManagement.Visible = isAdmin
        btnStudentManagement.Visible = isAdmin
        btnsettings.Visible = isAdmin
        btnchangepassword.Visible = Not isAdmin     ' Change Password: staff only
        ArrangeNavButtons()
    End Sub

    Private Sub ArrangeNavButtons()
        Dim ordered As Button() = {btnMainMenu, btnStudentManagement, btnDocumentManagement,
                                   btnDocumentRequests, btnReqList, btnReport, btnUserManagement,
                                   btnsettings, btnchangepassword, btnactLogs}
        Dim y As Integer = 100
        For Each b As Button In ordered
            If b.Visible Then
                b.Top = y
                y += 49
            End If
        Next
    End Sub

    Private Sub frmMainMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _loggingOut Then Exit Sub
        If e.CloseReason = CloseReason.UserClosing Then
            If MsgBox("Are you sure you want to exit system?", vbQuestion + vbYesNo, "Registrar Document Request System") <> vbYes Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMainMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not _loggingOut Then System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub ShowPage(page As Form, activeButton As Button)
        If page Is Nothing Then Exit Sub

        Dim oldPage As Form = _currentPage

        pnlmain.SuspendLayout()

        page.TopLevel = False                         ' MUST be False to live inside a panel
        page.FormBorderStyle = FormBorderStyle.None
        page.Dock = DockStyle.Fill
        page.AutoScroll = True
        pnlmain.Controls.Add(page)
        _currentPage = page
        page.Show()                                   ' fires the page's Load event
        page.BringToFront()

        pnlmain.ResumeLayout(True)

        SetActiveButton(activeButton)

        If oldPage IsNot Nothing Then
            pnlmain.Controls.Remove(oldPage)
            Me.BeginInvoke(New MethodInvoker(Sub() oldPage.Dispose()))
        End If
    End Sub

    Private Sub SetActiveButton(activeButton As Button)
        If _navButtons Is Nothing Then Exit Sub
        For Each b As Button In _navButtons
            b.BackColor = If(b Is activeButton, NavActiveColor, NavNormalColor)
        Next
    End Sub

    Public Sub OpenDashboard()
        If Not CurrentUser.IsAdmin Then
            OpenRequestList()
            Exit Sub
        End If
        ApplyAutoRules()
        ShowPage(New frmDashboard(), btnMainMenu)
    End Sub

    Public Sub OpenRequestList()
        ShowPage(New frmRequestList(), btnReqList)
    End Sub

    Public Sub OpenRequestDetails(requestNo As String)
        Dim page As New frmRequestDetails()
        page.SelectedRequestNo = requestNo            ' the page loads it in its own Load event
        ShowPage(page, btnReqList)
    End Sub

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        OpenDashboard()
    End Sub
    Private Sub btnactLogs_Click(sender As Object, e As EventArgs) Handles btnactLogs.Click
        If Not CurrentUser.IsAdmin Then
            MsgBox("Only the Administrator can view Activity Logs.", vbExclamation, "Access Denied")
            Exit Sub
        End If
        ShowPage(New frmActivityLogs(), btnactLogs)
    End Sub

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
        If Not CurrentUser.IsAdmin Then
            MsgBox("Only the Administrator can access Student Management.", vbExclamation, "Access Denied")
            Exit Sub
        End If
        ShowPage(New frmStudentManagement(), btnStudentManagement)
    End Sub

    Private Sub btnDocumentManagement_Click(sender As Object, e As EventArgs) Handles btnDocumentManagement.Click
        ShowPage(New frmDocumentManagement(), btnDocumentManagement)
    End Sub

    Private Sub btnDocumentRequests_Click(sender As Object, e As EventArgs) Handles btnDocumentRequests.Click
        ShowPage(New frmNewRequest(), btnDocumentRequests)
    End Sub

    Private Sub btnReqList_Click(sender As Object, e As EventArgs) Handles btnReqList.Click
        OpenRequestList()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        ShowPage(New frmReports(), btnReport)
    End Sub

    Private Sub btnUserManagement_Click(sender As Object, e As EventArgs) Handles btnUserManagement.Click
        ShowPage(New frmUserManagement(), btnUserManagement)
    End Sub

    Private Sub btnchangepassword_Click(sender As Object, e As EventArgs) Handles btnchangepassword.Click
        If CurrentUser.IsAdmin Then
            MsgBox("Administrators cannot change their password here.", vbExclamation, "Access Denied")
            Exit Sub
        End If
        Using frm As New frmChangePass()
            frm.ShowDialog(Me)
        End Using
    End Sub
    Private Sub btnsettings_Click(sender As Object, e As EventArgs) Handles btnsettings.Click
        If Not CurrentUser.IsAdmin Then
            MsgBox("Only the Administrator can access Settings.", vbExclamation, "Access Denied")
            Exit Sub
        End If
        ShowPage(New frmSettings(), btnsettings)
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to logout?", vbYesNo + vbQuestion, "Confirm Logout") = MsgBoxResult.Yes Then
            _loggingOut = True
            LogActivity("Logout", "Logged out of the system")
            CurrentUser.UserID = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            frmLogin.Show()
            Me.Close()
        End If
    End Sub

End Class