<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDashboard))
        Me.lbldatetime = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblposition = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnlrecentreqdoc = New System.Windows.Forms.Panel()
        Me.btnViewReq = New System.Windows.Forms.Button()
        Me.dgvrecentreqdoc = New System.Windows.Forms.DataGridView()
        Me.RequestNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Document = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateRequested = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlmostreqdoc = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chtMostreqdoc = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.pnlreqstatus = New System.Windows.Forms.Panel()
        Me.dgvOverdueReq = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlDocreqpermonth = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chtdocreqpermonth = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.lblcompleted = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lblpendingrequests = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lbltotrequests = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbltotalstudents = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbomonth = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlrecentreqdoc.SuspendLayout()
        CType(Me.dgvrecentreqdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlmostreqdoc.SuspendLayout()
        CType(Me.chtMostreqdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlreqstatus.SuspendLayout()
        CType(Me.dgvOverdueReq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocreqpermonth.SuspendLayout()
        CType(Me.chtdocreqpermonth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbldatetime
        '
        Me.lbldatetime.AutoSize = True
        Me.lbldatetime.BackColor = System.Drawing.Color.Transparent
        Me.lbldatetime.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatetime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lbldatetime.Location = New System.Drawing.Point(579, 6)
        Me.lbldatetime.Name = "lbldatetime"
        Me.lbldatetime.Size = New System.Drawing.Size(16, 21)
        Me.lbldatetime.TabIndex = 77
        Me.lbldatetime.Text = "-"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(507, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 21)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "Today is"
        '
        'lblposition
        '
        Me.lblposition.AutoSize = True
        Me.lblposition.BackColor = System.Drawing.Color.Transparent
        Me.lblposition.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblposition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblposition.Location = New System.Drawing.Point(318, 6)
        Me.lblposition.Name = "lblposition"
        Me.lblposition.Size = New System.Drawing.Size(82, 21)
        Me.lblposition.TabIndex = 75
        Me.lblposition.Text = "NPosition"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(252, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 21)
        Me.Label17.TabIndex = 74
        Me.Label17.Text = "Position:"
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.BackColor = System.Drawing.Color.Transparent
        Me.lblname.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblname.Location = New System.Drawing.Point(57, 6)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(53, 21)
        Me.lblname.TabIndex = 73
        Me.lblname.Text = "Name"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(7, 6)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 21)
        Me.Label15.TabIndex = 72
        Me.Label15.Text = "Name:"
        '
        'pnlrecentreqdoc
        '
        Me.pnlrecentreqdoc.BackColor = System.Drawing.Color.White
        Me.pnlrecentreqdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlrecentreqdoc.Controls.Add(Me.btnViewReq)
        Me.pnlrecentreqdoc.Controls.Add(Me.dgvrecentreqdoc)
        Me.pnlrecentreqdoc.Controls.Add(Me.Label11)
        Me.pnlrecentreqdoc.Location = New System.Drawing.Point(40, 197)
        Me.pnlrecentreqdoc.Name = "pnlrecentreqdoc"
        Me.pnlrecentreqdoc.Size = New System.Drawing.Size(536, 282)
        Me.pnlrecentreqdoc.TabIndex = 71
        '
        'btnViewReq
        '
        Me.btnViewReq.BackColor = System.Drawing.Color.Navy
        Me.btnViewReq.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewReq.ForeColor = System.Drawing.Color.White
        Me.btnViewReq.Location = New System.Drawing.Point(412, 14)
        Me.btnViewReq.Name = "btnViewReq"
        Me.btnViewReq.Size = New System.Drawing.Size(107, 23)
        Me.btnViewReq.TabIndex = 38
        Me.btnViewReq.Text = "View All Requests"
        Me.btnViewReq.UseVisualStyleBackColor = False
        '
        'dgvrecentreqdoc
        '
        Me.dgvrecentreqdoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvrecentreqdoc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RequestNo, Me.StudentName, Me.Document, Me.Status, Me.DateRequested})
        Me.dgvrecentreqdoc.Location = New System.Drawing.Point(0, 52)
        Me.dgvrecentreqdoc.Name = "dgvrecentreqdoc"
        Me.dgvrecentreqdoc.ReadOnly = True
        Me.dgvrecentreqdoc.Size = New System.Drawing.Size(536, 229)
        Me.dgvrecentreqdoc.TabIndex = 37
        '
        'RequestNo
        '
        Me.RequestNo.HeaderText = "Request #"
        Me.RequestNo.Name = "RequestNo"
        Me.RequestNo.ReadOnly = True
        '
        'StudentName
        '
        Me.StudentName.HeaderText = "Student Name"
        Me.StudentName.Name = "StudentName"
        Me.StudentName.ReadOnly = True
        Me.StudentName.Width = 150
        '
        'Document
        '
        Me.Document.HeaderText = "Document"
        Me.Document.Name = "Document"
        Me.Document.ReadOnly = True
        Me.Document.Width = 150
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'DateRequested
        '
        Me.DateRequested.HeaderText = "Date Requested"
        Me.DateRequested.Name = "DateRequested"
        Me.DateRequested.ReadOnly = True
        Me.DateRequested.Width = 105
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(230, 21)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "RECENT REQUEST DOCUMENT"
        '
        'pnlmostreqdoc
        '
        Me.pnlmostreqdoc.BackColor = System.Drawing.Color.White
        Me.pnlmostreqdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlmostreqdoc.Controls.Add(Me.Label10)
        Me.pnlmostreqdoc.Controls.Add(Me.chtMostreqdoc)
        Me.pnlmostreqdoc.Location = New System.Drawing.Point(40, 500)
        Me.pnlmostreqdoc.Name = "pnlmostreqdoc"
        Me.pnlmostreqdoc.Size = New System.Drawing.Size(536, 282)
        Me.pnlmostreqdoc.TabIndex = 69
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(23, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(246, 21)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "MOST REQUESTED DOCUMENTS"
        '
        'chtMostreqdoc
        '
        ChartArea3.Name = "ChartArea1"
        Me.chtMostreqdoc.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.chtMostreqdoc.Legends.Add(Legend3)
        Me.chtMostreqdoc.Location = New System.Drawing.Point(20, 62)
        Me.chtMostreqdoc.Name = "chtMostreqdoc"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.chtMostreqdoc.Series.Add(Series3)
        Me.chtMostreqdoc.Size = New System.Drawing.Size(499, 198)
        Me.chtMostreqdoc.TabIndex = 35
        Me.chtMostreqdoc.Text = "Chart3"
        '
        'pnlreqstatus
        '
        Me.pnlreqstatus.BackColor = System.Drawing.Color.White
        Me.pnlreqstatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlreqstatus.Controls.Add(Me.dgvOverdueReq)
        Me.pnlreqstatus.Controls.Add(Me.Label8)
        Me.pnlreqstatus.Location = New System.Drawing.Point(606, 196)
        Me.pnlreqstatus.Name = "pnlreqstatus"
        Me.pnlreqstatus.Size = New System.Drawing.Size(533, 282)
        Me.pnlreqstatus.TabIndex = 70
        '
        'dgvOverdueReq
        '
        Me.dgvOverdueReq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverdueReq.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        Me.dgvOverdueReq.Location = New System.Drawing.Point(0, 52)
        Me.dgvOverdueReq.Name = "dgvOverdueReq"
        Me.dgvOverdueReq.ReadOnly = True
        Me.dgvOverdueReq.Size = New System.Drawing.Size(536, 229)
        Me.dgvOverdueReq.TabIndex = 38
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Request #"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Student Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Document"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Date Requested"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 105
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(151, 21)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "OVERDUE REQUEST"
        '
        'pnlDocreqpermonth
        '
        Me.pnlDocreqpermonth.BackColor = System.Drawing.Color.White
        Me.pnlDocreqpermonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDocreqpermonth.Controls.Add(Me.cbomonth)
        Me.pnlDocreqpermonth.Controls.Add(Me.Label3)
        Me.pnlDocreqpermonth.Controls.Add(Me.chtdocreqpermonth)
        Me.pnlDocreqpermonth.Location = New System.Drawing.Point(606, 500)
        Me.pnlDocreqpermonth.Name = "pnlDocreqpermonth"
        Me.pnlDocreqpermonth.Size = New System.Drawing.Size(534, 282)
        Me.pnlDocreqpermonth.TabIndex = 68
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(264, 21)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "DOCUMENT REQUEST PER MONTH"
        '
        'chtdocreqpermonth
        '
        ChartArea4.Name = "ChartArea1"
        Me.chtdocreqpermonth.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.chtdocreqpermonth.Legends.Add(Legend4)
        Me.chtdocreqpermonth.Location = New System.Drawing.Point(21, 62)
        Me.chtdocreqpermonth.Name = "chtdocreqpermonth"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Me.chtdocreqpermonth.Series.Add(Series4)
        Me.chtdocreqpermonth.Size = New System.Drawing.Size(450, 198)
        Me.chtdocreqpermonth.TabIndex = 35
        Me.chtdocreqpermonth.Text = "Chart1"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.White
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.PictureBox4)
        Me.Panel8.Controls.Add(Me.lblcompleted)
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Location = New System.Drawing.Point(887, 65)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(250, 111)
        Me.Panel8.TabIndex = 67
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(22, 28)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(55, 55)
        Me.PictureBox4.TabIndex = 31
        Me.PictureBox4.TabStop = False
        '
        'lblcompleted
        '
        Me.lblcompleted.AutoSize = True
        Me.lblcompleted.BackColor = System.Drawing.Color.Transparent
        Me.lblcompleted.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcompleted.ForeColor = System.Drawing.Color.Navy
        Me.lblcompleted.Location = New System.Drawing.Point(93, 50)
        Me.lblcompleted.Name = "lblcompleted"
        Me.lblcompleted.Size = New System.Drawing.Size(24, 32)
        Me.lblcompleted.TabIndex = 30
        Me.lblcompleted.Text = "-"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(85, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 21)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Released Request"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.PictureBox3)
        Me.Panel7.Controls.Add(Me.lblpendingrequests)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Location = New System.Drawing.Point(606, 65)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(246, 111)
        Me.Panel7.TabIndex = 66
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(27, 28)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(55, 55)
        Me.PictureBox3.TabIndex = 31
        Me.PictureBox3.TabStop = False
        '
        'lblpendingrequests
        '
        Me.lblpendingrequests.AutoSize = True
        Me.lblpendingrequests.BackColor = System.Drawing.Color.Transparent
        Me.lblpendingrequests.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpendingrequests.ForeColor = System.Drawing.Color.Navy
        Me.lblpendingrequests.Location = New System.Drawing.Point(98, 50)
        Me.lblpendingrequests.Name = "lblpendingrequests"
        Me.lblpendingrequests.Size = New System.Drawing.Size(24, 32)
        Me.lblpendingrequests.TabIndex = 30
        Me.lblpendingrequests.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(94, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 21)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Pending Request"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.PictureBox2)
        Me.Panel6.Controls.Add(Me.lbltotrequests)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Location = New System.Drawing.Point(327, 65)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(249, 111)
        Me.Panel6.TabIndex = 65
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(27, 28)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(55, 55)
        Me.PictureBox2.TabIndex = 31
        Me.PictureBox2.TabStop = False
        '
        'lbltotrequests
        '
        Me.lbltotrequests.AutoSize = True
        Me.lbltotrequests.BackColor = System.Drawing.Color.Transparent
        Me.lbltotrequests.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotrequests.ForeColor = System.Drawing.Color.Navy
        Me.lbltotrequests.Location = New System.Drawing.Point(98, 50)
        Me.lbltotrequests.Name = "lbltotrequests"
        Me.lbltotrequests.Size = New System.Drawing.Size(24, 32)
        Me.lbltotrequests.TabIndex = 30
        Me.lbltotrequests.Text = "-"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(94, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 21)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Total Request"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.PictureBox1)
        Me.Panel5.Controls.Add(Me.lbltotalstudents)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Location = New System.Drawing.Point(40, 65)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(246, 111)
        Me.Panel5.TabIndex = 64
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(27, 28)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(55, 55)
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'lbltotalstudents
        '
        Me.lbltotalstudents.AutoSize = True
        Me.lbltotalstudents.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalstudents.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalstudents.ForeColor = System.Drawing.Color.Navy
        Me.lbltotalstudents.Location = New System.Drawing.Point(98, 50)
        Me.lbltotalstudents.Name = "lbltotalstudents"
        Me.lbltotalstudents.Size = New System.Drawing.Size(24, 32)
        Me.lbltotalstudents.TabIndex = 30
        Me.lbltotalstudents.Text = "-"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(94, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 21)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Total Students"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(34, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 32)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "Dashboard"
        '
        'cbomonth
        '
        Me.cbomonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbomonth.DropDownWidth = 300
        Me.cbomonth.FormattingEnabled = True
        Me.cbomonth.Location = New System.Drawing.Point(408, 17)
        Me.cbomonth.Name = "cbomonth"
        Me.cbomonth.Size = New System.Drawing.Size(109, 21)
        Me.cbomonth.TabIndex = 37
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.lbldatetime)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.lblname)
        Me.Panel1.Controls.Add(Me.lblposition)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 805)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1174, 31)
        Me.Panel1.TabIndex = 78
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1174, 836)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlrecentreqdoc)
        Me.Controls.Add(Me.pnlmostreqdoc)
        Me.Controls.Add(Me.pnlreqstatus)
        Me.Controls.Add(Me.pnlDocreqpermonth)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDashboard"
        Me.Text = "frmDashboard"
        Me.pnlrecentreqdoc.ResumeLayout(False)
        Me.pnlrecentreqdoc.PerformLayout()
        CType(Me.dgvrecentreqdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlmostreqdoc.ResumeLayout(False)
        Me.pnlmostreqdoc.PerformLayout()
        CType(Me.chtMostreqdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlreqstatus.ResumeLayout(False)
        Me.pnlreqstatus.PerformLayout()
        CType(Me.dgvOverdueReq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocreqpermonth.ResumeLayout(False)
        Me.pnlDocreqpermonth.PerformLayout()
        CType(Me.chtdocreqpermonth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbldatetime As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblposition As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblname As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents pnlrecentreqdoc As Panel
    Friend WithEvents btnViewReq As Button
    Friend WithEvents dgvrecentreqdoc As DataGridView
    Friend WithEvents RequestNo As DataGridViewTextBoxColumn
    Friend WithEvents StudentName As DataGridViewTextBoxColumn
    Friend WithEvents Document As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents DateRequested As DataGridViewTextBoxColumn
    Friend WithEvents Label11 As Label
    Friend WithEvents pnlmostreqdoc As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents chtMostreqdoc As DataVisualization.Charting.Chart
    Friend WithEvents pnlreqstatus As Panel
    Friend WithEvents dgvOverdueReq As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents Label8 As Label
    Friend WithEvents pnlDocreqpermonth As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents chtdocreqpermonth As DataVisualization.Charting.Chart
    Friend WithEvents Panel8 As Panel
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents lblcompleted As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents lblpendingrequests As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lbltotrequests As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbltotalstudents As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cbomonth As ComboBox
    Friend WithEvents Panel1 As Panel
End Class
