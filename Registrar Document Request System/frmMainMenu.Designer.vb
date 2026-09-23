<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainMenu
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainMenu))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnUserManagement = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnReqList = New System.Windows.Forms.Button()
        Me.btnDocumentRequests = New System.Windows.Forms.Button()
        Me.btnDocumentManagement = New System.Windows.Forms.Button()
        Me.btnStudentManagement = New System.Windows.Forms.Button()
        Me.btnMainMenu = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbltotalstudents = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lbltotrequests = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lblpendingrequests = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.lblcompleted = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chtdocreqpermonth = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.pnlDocreqpermonth = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlreqstatus = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlmostreqdoc = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chtMostreqdoc = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.pnlrecentreqdoc = New System.Windows.Forms.Panel()
        Me.btnViewReq = New System.Windows.Forms.Button()
        Me.dgvrecentreqdoc = New System.Windows.Forms.DataGridView()
        Me.RequestNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Document = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateRequested = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbldatetime = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblposition = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tmrDateTime = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chtdocreqpermonth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocreqpermonth.SuspendLayout()
        Me.pnlreqstatus.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlmostreqdoc.SuspendLayout()
        CType(Me.chtMostreqdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlrecentreqdoc.SuspendLayout()
        CType(Me.dgvrecentreqdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(278, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 32)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Main Menu"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnUserManagement)
        Me.Panel1.Controls.Add(Me.btnReport)
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.btnReqList)
        Me.Panel1.Controls.Add(Me.btnDocumentRequests)
        Me.Panel1.Controls.Add(Me.btnDocumentManagement)
        Me.Panel1.Controls.Add(Me.btnStudentManagement)
        Me.Panel1.Controls.Add(Me.btnMainMenu)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 836)
        Me.Panel1.TabIndex = 27
        '
        'btnUserManagement
        '
        Me.btnUserManagement.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnUserManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnUserManagement.FlatAppearance.BorderSize = 0
        Me.btnUserManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUserManagement.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUserManagement.ForeColor = System.Drawing.Color.White
        Me.btnUserManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUserManagement.Location = New System.Drawing.Point(15, 394)
        Me.btnUserManagement.Name = "btnUserManagement"
        Me.btnUserManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnUserManagement.TabIndex = 12
        Me.btnUserManagement.Text = " User Management"
        Me.btnUserManagement.UseVisualStyleBackColor = False
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnReport.FlatAppearance.BorderSize = 0
        Me.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.ForeColor = System.Drawing.Color.White
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(15, 345)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(220, 49)
        Me.btnReport.TabIndex = 11
        Me.btnReport.Text = "Reports"
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.Location = New System.Drawing.Point(15, 787)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(220, 49)
        Me.btnLogout.TabIndex = 10
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnReqList
        '
        Me.btnReqList.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnReqList.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnReqList.FlatAppearance.BorderSize = 0
        Me.btnReqList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReqList.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReqList.ForeColor = System.Drawing.Color.White
        Me.btnReqList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReqList.Location = New System.Drawing.Point(15, 296)
        Me.btnReqList.Name = "btnReqList"
        Me.btnReqList.Size = New System.Drawing.Size(220, 49)
        Me.btnReqList.TabIndex = 8
        Me.btnReqList.Text = "Request List"
        Me.btnReqList.UseVisualStyleBackColor = False
        '
        'btnDocumentRequests
        '
        Me.btnDocumentRequests.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnDocumentRequests.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDocumentRequests.FlatAppearance.BorderSize = 0
        Me.btnDocumentRequests.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDocumentRequests.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDocumentRequests.ForeColor = System.Drawing.Color.White
        Me.btnDocumentRequests.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentRequests.Location = New System.Drawing.Point(15, 247)
        Me.btnDocumentRequests.Name = "btnDocumentRequests"
        Me.btnDocumentRequests.Size = New System.Drawing.Size(220, 49)
        Me.btnDocumentRequests.TabIndex = 7
        Me.btnDocumentRequests.Text = "Document Requests"
        Me.btnDocumentRequests.UseVisualStyleBackColor = False
        '
        'btnDocumentManagement
        '
        Me.btnDocumentManagement.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnDocumentManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDocumentManagement.FlatAppearance.BorderSize = 0
        Me.btnDocumentManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDocumentManagement.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDocumentManagement.ForeColor = System.Drawing.Color.White
        Me.btnDocumentManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentManagement.Location = New System.Drawing.Point(15, 198)
        Me.btnDocumentManagement.Name = "btnDocumentManagement"
        Me.btnDocumentManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnDocumentManagement.TabIndex = 6
        Me.btnDocumentManagement.Text = "Document Management"
        Me.btnDocumentManagement.UseVisualStyleBackColor = False
        '
        'btnStudentManagement
        '
        Me.btnStudentManagement.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnStudentManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnStudentManagement.FlatAppearance.BorderSize = 0
        Me.btnStudentManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStudentManagement.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStudentManagement.ForeColor = System.Drawing.Color.White
        Me.btnStudentManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStudentManagement.Location = New System.Drawing.Point(15, 149)
        Me.btnStudentManagement.Name = "btnStudentManagement"
        Me.btnStudentManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnStudentManagement.TabIndex = 5
        Me.btnStudentManagement.Text = "Student Management"
        Me.btnStudentManagement.UseVisualStyleBackColor = False
        '
        'btnMainMenu
        '
        Me.btnMainMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnMainMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMainMenu.FlatAppearance.BorderSize = 0
        Me.btnMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMainMenu.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMainMenu.ForeColor = System.Drawing.Color.White
        Me.btnMainMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMainMenu.Location = New System.Drawing.Point(15, 100)
        Me.btnMainMenu.Name = "btnMainMenu"
        Me.btnMainMenu.Size = New System.Drawing.Size(220, 49)
        Me.btnMainMenu.TabIndex = 4
        Me.btnMainMenu.Text = "Main Menu"
        Me.btnMainMenu.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(235, 100)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(15, 736)
        Me.Panel4.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 100)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 736)
        Me.Panel3.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Logo)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(250, 100)
        Me.Panel2.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(64, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 32)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "REGISTRAR"
        '
        'Logo
        '
        Me.Logo.BackgroundImage = CType(resources.GetObject("Logo.BackgroundImage"), System.Drawing.Image)
        Me.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Logo.Location = New System.Drawing.Point(21, 27)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(40, 40)
        Me.Logo.TabIndex = 22
        Me.Logo.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(67, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(159, 15)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Document Request System"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.PictureBox1)
        Me.Panel5.Controls.Add(Me.lbltotalstudents)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Location = New System.Drawing.Point(284, 73)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(246, 111)
        Me.Panel5.TabIndex = 28
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
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.PictureBox2)
        Me.Panel6.Controls.Add(Me.lbltotrequests)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Location = New System.Drawing.Point(574, 73)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(246, 111)
        Me.Panel6.TabIndex = 32
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
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.PictureBox3)
        Me.Panel7.Controls.Add(Me.lblpendingrequests)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Location = New System.Drawing.Point(860, 73)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(246, 111)
        Me.Panel7.TabIndex = 33
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
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.White
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.PictureBox4)
        Me.Panel8.Controls.Add(Me.lblcompleted)
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Location = New System.Drawing.Point(1145, 73)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(246, 111)
        Me.Panel8.TabIndex = 34
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(27, 28)
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
        Me.lblcompleted.Location = New System.Drawing.Point(98, 50)
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
        Me.Label9.Location = New System.Drawing.Point(94, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 21)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Completed"
        '
        'chtdocreqpermonth
        '
        ChartArea1.Name = "ChartArea1"
        Me.chtdocreqpermonth.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chtdocreqpermonth.Legends.Add(Legend1)
        Me.chtdocreqpermonth.Location = New System.Drawing.Point(-15, 62)
        Me.chtdocreqpermonth.Name = "chtdocreqpermonth"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.chtdocreqpermonth.Series.Add(Series1)
        Me.chtdocreqpermonth.Size = New System.Drawing.Size(656, 198)
        Me.chtdocreqpermonth.TabIndex = 35
        Me.chtdocreqpermonth.Text = "Chart1"
        '
        'pnlDocreqpermonth
        '
        Me.pnlDocreqpermonth.BackColor = System.Drawing.Color.White
        Me.pnlDocreqpermonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDocreqpermonth.Controls.Add(Me.Label3)
        Me.pnlDocreqpermonth.Controls.Add(Me.chtdocreqpermonth)
        Me.pnlDocreqpermonth.Location = New System.Drawing.Point(860, 508)
        Me.pnlDocreqpermonth.Name = "pnlDocreqpermonth"
        Me.pnlDocreqpermonth.Size = New System.Drawing.Size(534, 282)
        Me.pnlDocreqpermonth.TabIndex = 37
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
        'pnlreqstatus
        '
        Me.pnlreqstatus.BackColor = System.Drawing.Color.White
        Me.pnlreqstatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlreqstatus.Controls.Add(Me.DataGridView1)
        Me.pnlreqstatus.Controls.Add(Me.Label8)
        Me.pnlreqstatus.Location = New System.Drawing.Point(860, 204)
        Me.pnlreqstatus.Name = "pnlreqstatus"
        Me.pnlreqstatus.Size = New System.Drawing.Size(533, 282)
        Me.pnlreqstatus.TabIndex = 38
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        Me.DataGridView1.Location = New System.Drawing.Point(0, 52)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(536, 229)
        Me.DataGridView1.TabIndex = 38
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Request #"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Student Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Document"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Date Requested"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
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
        'pnlmostreqdoc
        '
        Me.pnlmostreqdoc.BackColor = System.Drawing.Color.White
        Me.pnlmostreqdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlmostreqdoc.Controls.Add(Me.Label10)
        Me.pnlmostreqdoc.Controls.Add(Me.chtMostreqdoc)
        Me.pnlmostreqdoc.Location = New System.Drawing.Point(284, 508)
        Me.pnlmostreqdoc.Name = "pnlmostreqdoc"
        Me.pnlmostreqdoc.Size = New System.Drawing.Size(536, 282)
        Me.pnlmostreqdoc.TabIndex = 38
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
        ChartArea2.Name = "ChartArea1"
        Me.chtMostreqdoc.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.chtMostreqdoc.Legends.Add(Legend2)
        Me.chtMostreqdoc.Location = New System.Drawing.Point(-10, 62)
        Me.chtMostreqdoc.Name = "chtMostreqdoc"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.chtMostreqdoc.Series.Add(Series2)
        Me.chtMostreqdoc.Size = New System.Drawing.Size(664, 198)
        Me.chtMostreqdoc.TabIndex = 35
        Me.chtMostreqdoc.Text = "Chart3"
        '
        'pnlrecentreqdoc
        '
        Me.pnlrecentreqdoc.BackColor = System.Drawing.Color.White
        Me.pnlrecentreqdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlrecentreqdoc.Controls.Add(Me.btnViewReq)
        Me.pnlrecentreqdoc.Controls.Add(Me.dgvrecentreqdoc)
        Me.pnlrecentreqdoc.Controls.Add(Me.Label11)
        Me.pnlrecentreqdoc.Location = New System.Drawing.Point(284, 205)
        Me.pnlrecentreqdoc.Name = "pnlrecentreqdoc"
        Me.pnlrecentreqdoc.Size = New System.Drawing.Size(536, 282)
        Me.pnlrecentreqdoc.TabIndex = 39
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
        Me.dgvrecentreqdoc.Size = New System.Drawing.Size(536, 229)
        Me.dgvrecentreqdoc.TabIndex = 37
        '
        'RequestNo
        '
        Me.RequestNo.HeaderText = "Request #"
        Me.RequestNo.Name = "RequestNo"
        '
        'StudentName
        '
        Me.StudentName.HeaderText = "Student Name"
        Me.StudentName.Name = "StudentName"
        Me.StudentName.Width = 150
        '
        'Document
        '
        Me.Document.HeaderText = "Document"
        Me.Document.Name = "Document"
        Me.Document.Width = 150
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        '
        'DateRequested
        '
        Me.DateRequested.HeaderText = "Date Requested"
        Me.DateRequested.Name = "DateRequested"
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
        'lbldatetime
        '
        Me.lbldatetime.AutoSize = True
        Me.lbldatetime.BackColor = System.Drawing.Color.Transparent
        Me.lbldatetime.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatetime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lbldatetime.Location = New System.Drawing.Point(852, 804)
        Me.lbldatetime.Name = "lbldatetime"
        Me.lbldatetime.Size = New System.Drawing.Size(16, 21)
        Me.lbldatetime.TabIndex = 62
        Me.lbldatetime.Text = "-"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(780, 804)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 21)
        Me.Label19.TabIndex = 61
        Me.Label19.Text = "Today is"
        '
        'lblposition
        '
        Me.lblposition.AutoSize = True
        Me.lblposition.BackColor = System.Drawing.Color.Transparent
        Me.lblposition.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblposition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblposition.Location = New System.Drawing.Point(591, 804)
        Me.lblposition.Name = "lblposition"
        Me.lblposition.Size = New System.Drawing.Size(82, 21)
        Me.lblposition.TabIndex = 60
        Me.lblposition.Text = "NPosition"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(525, 804)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 21)
        Me.Label17.TabIndex = 59
        Me.Label17.Text = "Position:"
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.BackColor = System.Drawing.Color.Transparent
        Me.lblname.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblname.Location = New System.Drawing.Point(330, 804)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(53, 21)
        Me.lblname.TabIndex = 58
        Me.lblname.Text = "Name"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(280, 804)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 21)
        Me.Label15.TabIndex = 57
        Me.Label15.Text = "Name:"
        '
        'Timer1
        '
        '
        'frmMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1424, 836)
        Me.Controls.Add(Me.lbldatetime)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lblposition)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lblname)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.pnlrecentreqdoc)
        Me.Controls.Add(Me.pnlmostreqdoc)
        Me.Controls.Add(Me.pnlreqstatus)
        Me.Controls.Add(Me.pnlDocreqpermonth)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMainMenu"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chtdocreqpermonth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocreqpermonth.ResumeLayout(False)
        Me.pnlDocreqpermonth.PerformLayout()
        Me.pnlreqstatus.ResumeLayout(False)
        Me.pnlreqstatus.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlmostreqdoc.ResumeLayout(False)
        Me.pnlmostreqdoc.PerformLayout()
        CType(Me.chtMostreqdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlrecentreqdoc.ResumeLayout(False)
        Me.pnlrecentreqdoc.PerformLayout()
        CType(Me.dgvrecentreqdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnReport As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnReqList As Button
    Friend WithEvents btnDocumentRequests As Button
    Friend WithEvents btnDocumentManagement As Button
    Friend WithEvents btnStudentManagement As Button
    Friend WithEvents btnMainMenu As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Logo As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lbltotalstudents As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lbltotrequests As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents lblpendingrequests As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents lblcompleted As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents chtdocreqpermonth As DataVisualization.Charting.Chart
    Friend WithEvents pnlDocreqpermonth As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents pnlreqstatus As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents pnlmostreqdoc As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents chtMostreqdoc As DataVisualization.Charting.Chart
    Friend WithEvents pnlrecentreqdoc As Panel
    Friend WithEvents btnViewReq As Button
    Friend WithEvents dgvrecentreqdoc As DataGridView
    Friend WithEvents Label11 As Label
    Friend WithEvents RequestNo As DataGridViewTextBoxColumn
    Friend WithEvents StudentName As DataGridViewTextBoxColumn
    Friend WithEvents Document As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents DateRequested As DataGridViewTextBoxColumn
    Friend WithEvents btnUserManagement As Button
    Friend WithEvents lbldatetime As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblposition As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblname As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents tmrDateTime As Timer
    Friend WithEvents Timer1 As Timer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
End Class
