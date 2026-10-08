Imports System.Drawing
Imports System.Windows.Forms

' Reusable pager for any DataGridView that is filled with Rows.Add(...).
' Usage:  pager = New GridPager(dgvSomething)
Public Class GridPager

    Private ReadOnly _grid As DataGridView
    Private ReadOnly _panel As New Panel()
    Private ReadOnly _lblInfo As New Label()
    Private ReadOnly _flow As New FlowLayoutPanel()
    Private ReadOnly _lblSize As New Label()
    Private ReadOnly _cboSize As New ComboBox()
    Private ReadOnly _btnFirst As New Button()
    Private ReadOnly _btnPrev As New Button()
    Private ReadOnly _lblPage As New Label()
    Private ReadOnly _btnNext As New Button()
    Private ReadOnly _btnLast As New Button()
    Private WithEvents _debounce As New System.Windows.Forms.Timer With {.Interval = 20}

    Private ReadOnly NavyColor As Color = Color.FromArgb(1, 21, 78)

    Private _page As Integer = 1
    Private _pageSize As Integer = 20
    Private _pages As Integer = 1
    Private _busy As Boolean = False
    Private _initializing As Boolean = True

#Region "Setup"

    Public Sub New(grid As DataGridView, Optional pageSize As Integer = 20)
        _grid = grid
        _pageSize = Math.Max(1, pageSize)
        BuildUi()

        AddHandler _grid.RowsAdded, AddressOf GridChanged
        AddHandler _grid.RowsRemoved, AddressOf GridChanged
        AddHandler _grid.Sorted, AddressOf GridChanged
        AddHandler _grid.Disposed, AddressOf GridDisposed

        _initializing = False
        _debounce.Start()
    End Sub

    Private Sub BuildUi()
        Dim parent As Control = _grid.Parent
        Dim h As Integer = 38

        _panel.Height = h
        _panel.BackColor = parent.BackColor

        ' left side: "Showing 1-20 of 39"
        _lblInfo.Dock = DockStyle.Left
        _lblInfo.AutoSize = False
        _lblInfo.Width = 260
        _lblInfo.TextAlign = ContentAlignment.MiddleLeft
        _lblInfo.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        _lblInfo.ForeColor = NavyColor

        ' right side: controls
        _flow.Dock = DockStyle.Right
        _flow.AutoSize = True
        _flow.AutoSizeMode = AutoSizeMode.GrowAndShrink
        _flow.FlowDirection = FlowDirection.LeftToRight
        _flow.WrapContents = False
        _flow.BackColor = Color.Transparent

        _lblSize.Text = "Rows per page:"
        _lblSize.AutoSize = True
        _lblSize.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        _lblSize.ForeColor = NavyColor
        _lblSize.Margin = New Padding(6, 10, 3, 0)

        _cboSize.DropDownStyle = ComboBoxStyle.DropDownList
        _cboSize.Width = 60
        _cboSize.Margin = New Padding(3, 6, 14, 0)
        For Each n As Integer In New Integer() {10, 20, 50, 100}
            _cboSize.Items.Add(n)
        Next
        If Not _cboSize.Items.Contains(_pageSize) Then _cboSize.Items.Add(_pageSize)
        _cboSize.SelectedItem = _pageSize
        AddHandler _cboSize.SelectedIndexChanged, AddressOf PageSizeChanged

        StyleButton(_btnFirst, "<<")
        StyleButton(_btnPrev, "<")
        StyleButton(_btnNext, ">")
        StyleButton(_btnLast, ">>")
        AddHandler _btnFirst.Click, AddressOf GoFirst
        AddHandler _btnPrev.Click, AddressOf GoPrev
        AddHandler _btnNext.Click, AddressOf GoNext
        AddHandler _btnLast.Click, AddressOf GoLast

        _lblPage.AutoSize = True
        _lblPage.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        _lblPage.ForeColor = NavyColor
        _lblPage.Margin = New Padding(8, 10, 8, 0)
        _lblPage.Text = "Page 1 of 1"

        _flow.Controls.Add(_lblSize)
        _flow.Controls.Add(_cboSize)
        _flow.Controls.Add(_btnFirst)
        _flow.Controls.Add(_btnPrev)
        _flow.Controls.Add(_lblPage)
        _flow.Controls.Add(_btnNext)
        _flow.Controls.Add(_btnLast)

        _panel.Controls.Add(_flow)
        _panel.Controls.Add(_lblInfo)

        ' ---- make room under the grid and place the pager there ----
        If _grid.Dock <> DockStyle.Fill Then
            _grid.Height = Math.Max(60, _grid.Height - h)
        End If

        If _grid.Dock <> DockStyle.None Then
            _panel.Dock = DockStyle.Bottom
            parent.Controls.Add(_panel)
            _panel.SendToBack()                       ' docks first = very bottom
        Else
            _panel.Location = New Point(_grid.Left, _grid.Bottom)
            _panel.Width = _grid.Width
            Dim vert As AnchorStyles =
                If((_grid.Anchor And AnchorStyles.Bottom) = AnchorStyles.Bottom, AnchorStyles.Bottom, AnchorStyles.Top)
            _panel.Anchor = vert Or (_grid.Anchor And (AnchorStyles.Left Or AnchorStyles.Right))
            parent.Controls.Add(_panel)
            _panel.BringToFront()
        End If
    End Sub

    Private Sub StyleButton(b As Button, text As String)
        b.Text = text
        b.Size = New Size(38, 28)
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.BackColor = NavyColor
        b.ForeColor = Color.White
        b.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        b.Margin = New Padding(3, 5, 3, 0)
        b.Cursor = Cursors.Hand
    End Sub

#End Region

#Region "Public"

    ' Call BEFORE reloading the grid after a new search / filter, so it starts on page 1.
    Public Sub FirstPage()
        _page = 1
    End Sub

    ' Optional: apply the paging right now instead of waiting a few milliseconds.
    Public Sub RefreshPage()
        ApplyPage()
    End Sub

#End Region

#Region "Paging engine"

    Private Sub GridChanged(sender As Object, e As EventArgs)
        If _busy OrElse _initializing Then Exit Sub
        _debounce.Stop()
        _debounce.Start()          ' many rows added in a loop = ONE refresh at the end
    End Sub

    Private Sub _debounce_Tick(sender As Object, e As EventArgs) Handles _debounce.Tick
        _debounce.Stop()
        ApplyPage()
    End Sub

    Private Sub ApplyPage()
        If _busy OrElse _grid.IsDisposed Then Exit Sub
        _busy = True
        Try
            Dim total As Integer = 0
            For Each r As DataGridViewRow In _grid.Rows
                If Not r.IsNewRow Then total += 1
            Next

            _pages = Math.Max(1, CInt(Math.Ceiling(total / CDbl(_pageSize))))
            If _page > _pages Then _page = _pages
            If _page < 1 Then _page = 1

            Dim firstIdx As Integer = (_page - 1) * _pageSize
            Dim lastIdx As Integer = firstIdx + _pageSize - 1

            _grid.CurrentCell = Nothing          ' a selected row cannot be hidden

            Dim idx As Integer = 0
            For Each r As DataGridViewRow In _grid.Rows
                If r.IsNewRow Then Continue For
                Dim show As Boolean = (idx >= firstIdx AndAlso idx <= lastIdx)
                If r.Visible <> show Then r.Visible = show
                idx += 1
            Next

            If total > 0 Then
                Try
                    _grid.FirstDisplayedScrollingRowIndex = firstIdx
                Catch
                End Try
            End If

            UpdateUi(total, firstIdx)
        Finally
            _busy = False
        End Try
    End Sub

    Private Sub UpdateUi(total As Integer, firstIdx As Integer)
        Dim fromNo As Integer = If(total = 0, 0, firstIdx + 1)
        Dim toNo As Integer = Math.Min(total, firstIdx + _pageSize)

        _lblInfo.Text = String.Format("Showing {0}-{1} of {2}", fromNo, toNo, total)
        _lblPage.Text = String.Format("Page {0} of {1}", _page, _pages)

        SetEnabled(_btnFirst, _page > 1)
        SetEnabled(_btnPrev, _page > 1)
        SetEnabled(_btnNext, _page < _pages)
        SetEnabled(_btnLast, _page < _pages)
    End Sub

    Private Sub SetEnabled(b As Button, enabled As Boolean)
        b.Enabled = enabled
        b.BackColor = If(enabled, NavyColor, Color.Silver)
    End Sub

    Private Sub GoFirst(sender As Object, e As EventArgs)
        _page = 1
        ApplyPage()
    End Sub

    Private Sub GoPrev(sender As Object, e As EventArgs)
        _page -= 1
        ApplyPage()
    End Sub

    Private Sub GoNext(sender As Object, e As EventArgs)
        _page += 1
        ApplyPage()
    End Sub

    Private Sub GoLast(sender As Object, e As EventArgs)
        _page = _pages
        ApplyPage()
    End Sub

    Private Sub PageSizeChanged(sender As Object, e As EventArgs)
        If _initializing OrElse _cboSize.SelectedItem Is Nothing Then Exit Sub
        _pageSize = CInt(_cboSize.SelectedItem)
        _page = 1
        ApplyPage()
    End Sub

    Private Sub GridDisposed(sender As Object, e As EventArgs)
        _debounce.Stop()
        _debounce.Dispose()
    End Sub

#End Region

End Class