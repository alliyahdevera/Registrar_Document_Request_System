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
        Me.tmrDateTime = New System.Windows.Forms.Timer(Me.components)
        Me.pnlmain = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.btnUserManagement.Image = CType(resources.GetObject("btnUserManagement.Image"), System.Drawing.Image)
        Me.btnUserManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUserManagement.Location = New System.Drawing.Point(15, 394)
        Me.btnUserManagement.Name = "btnUserManagement"
        Me.btnUserManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnUserManagement.TabIndex = 12
        Me.btnUserManagement.Text = "      User Management"
        Me.btnUserManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUserManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnReport.Image = CType(resources.GetObject("btnReport.Image"), System.Drawing.Image)
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(15, 345)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(220, 49)
        Me.btnReport.TabIndex = 11
        Me.btnReport.Text = "     Reports"
        Me.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnLogout.Image = CType(resources.GetObject("btnLogout.Image"), System.Drawing.Image)
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.Location = New System.Drawing.Point(15, 787)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(220, 49)
        Me.btnLogout.TabIndex = 10
        Me.btnLogout.Text = "     Logout"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnReqList.Image = CType(resources.GetObject("btnReqList.Image"), System.Drawing.Image)
        Me.btnReqList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReqList.Location = New System.Drawing.Point(15, 296)
        Me.btnReqList.Name = "btnReqList"
        Me.btnReqList.Size = New System.Drawing.Size(220, 49)
        Me.btnReqList.TabIndex = 8
        Me.btnReqList.Text = "     Request List"
        Me.btnReqList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReqList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnDocumentRequests.Image = CType(resources.GetObject("btnDocumentRequests.Image"), System.Drawing.Image)
        Me.btnDocumentRequests.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentRequests.Location = New System.Drawing.Point(15, 247)
        Me.btnDocumentRequests.Name = "btnDocumentRequests"
        Me.btnDocumentRequests.Size = New System.Drawing.Size(220, 49)
        Me.btnDocumentRequests.TabIndex = 7
        Me.btnDocumentRequests.Text = "     Document Requests"
        Me.btnDocumentRequests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentRequests.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnDocumentManagement.Image = CType(resources.GetObject("btnDocumentManagement.Image"), System.Drawing.Image)
        Me.btnDocumentManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentManagement.Location = New System.Drawing.Point(15, 198)
        Me.btnDocumentManagement.Name = "btnDocumentManagement"
        Me.btnDocumentManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnDocumentManagement.TabIndex = 6
        Me.btnDocumentManagement.Text = "     Document Management"
        Me.btnDocumentManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumentManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnStudentManagement.Image = CType(resources.GetObject("btnStudentManagement.Image"), System.Drawing.Image)
        Me.btnStudentManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStudentManagement.Location = New System.Drawing.Point(15, 149)
        Me.btnStudentManagement.Name = "btnStudentManagement"
        Me.btnStudentManagement.Size = New System.Drawing.Size(220, 49)
        Me.btnStudentManagement.TabIndex = 5
        Me.btnStudentManagement.Text = "     Student Management"
        Me.btnStudentManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStudentManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.btnMainMenu.Image = CType(resources.GetObject("btnMainMenu.Image"), System.Drawing.Image)
        Me.btnMainMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMainMenu.Location = New System.Drawing.Point(15, 100)
        Me.btnMainMenu.Name = "btnMainMenu"
        Me.btnMainMenu.Size = New System.Drawing.Size(220, 49)
        Me.btnMainMenu.TabIndex = 4
        Me.btnMainMenu.Text = "     Main Menu"
        Me.btnMainMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMainMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        'tmrDateTime
        '
        Me.tmrDateTime.Enabled = True
        Me.tmrDateTime.Interval = 1000
        '
        'pnlmain
        '
        Me.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmain.Location = New System.Drawing.Point(250, 0)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Size = New System.Drawing.Size(1174, 836)
        Me.pnlmain.TabIndex = 28
        '
        'frmMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1424, 836)
        Me.Controls.Add(Me.pnlmain)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMainMenu"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Friend WithEvents btnUserManagement As Button
    Friend WithEvents tmrDateTime As Timer
    Friend WithEvents pnlmain As Panel
End Class
