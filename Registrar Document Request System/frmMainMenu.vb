Imports MySql.Data.MySqlClient

' ============================================================================
'  frmMainMenu  =  the application SHELL
'  - Left sidebar (Panel1) with the navigation buttons
'  - pnlmain (Dock = Fill) hosts ONE page (child form) at a time
'  Every other form (frmDashboard, frmStudentManagement, ...) is loaded INTO
'  pnlmain through ShowPage(). No page is ever shown as a separate window.
' ============================================================================
Public Class frmMainMenu

    Private _currentPage As Form = Nothing
    Private _loggingOut As Boolean = False
    Private _navButtons As Button()

    Private ReadOnly NavNormalColor As Color = Color.FromArgb(1, 21, 78)
    Private ReadOnly NavActiveColor As Color = Color.FromArgb(38, 70, 160)

#Region "Form lifecycle"

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _navButtons = New Button() {btnMainMenu, btnStudentManagement, btnDocumentManagement,
                                    btnDocumentRequests, btnReqList, btnReport, btnUserManagement}
        RefreshUserSession()
        OpenDashboard()
    End Sub

    ' Admin-only buttons are hidden for Registrar Staff.
    Private Sub RefreshUserSession()
        btnUserManagement.Visible = CurrentUser.IsAdmin
        btnDocumentManagement.Visible = CurrentUser.IsAdmin
    End Sub

    ' Clicking the window "X" = leave the system (frmLogin is only hidden,
    ' so without this the program would keep running invisibly).
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

#End Region

#Region "Page hosting (the heart of the one-panel design)"

    ' Loads any form into pnlmain, replacing the page that is currently shown.
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

        ' Dispose the old page AFTER the current event handler finishes
        ' (a page may be asking us to replace it from inside its own button click).
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

    ' ---- Public entry points that the pages call --------------------------
    Public Sub OpenDashboard()
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

#End Region

#Region "Sidebar buttons"

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        OpenDashboard()
    End Sub

    Private Sub btnStudentManagement_Click(sender As Object, e As EventArgs) Handles btnStudentManagement.Click
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

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to logout?", vbYesNo + vbQuestion, "Confirm Logout") = MsgBoxResult.Yes Then
            _loggingOut = True
            CurrentUser.UserID = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            frmLogin.Show()
            Me.Close()
        End If
    End Sub

#End Region

End Class