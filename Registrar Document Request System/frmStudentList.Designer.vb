<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStudentList
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStudentList))
        Me.dgvStudents = New System.Windows.Forms.DataGridView()
        Me.StudentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LRN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MiddleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Course = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.YearLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Section = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboGradeLevel = New System.Windows.Forms.ComboBox()
        CType(Me.dgvStudents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvStudents
        '
        Me.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStudents.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.StudentID, Me.LRN, Me.LastName, Me.FirstName, Me.MiddleName, Me.Course, Me.YearLevel, Me.Section, Me.ContactNumber})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStudents.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStudents.Location = New System.Drawing.Point(19, 122)
        Me.dgvStudents.Name = "dgvStudents"
        Me.dgvStudents.ReadOnly = True
        Me.dgvStudents.RowHeadersWidth = 51
        Me.dgvStudents.Size = New System.Drawing.Size(1056, 586)
        Me.dgvStudents.TabIndex = 30
        '
        'StudentID
        '
        Me.StudentID.HeaderText = "StudentID"
        Me.StudentID.MinimumWidth = 6
        Me.StudentID.Name = "StudentID"
        Me.StudentID.ReadOnly = True
        Me.StudentID.Width = 125
        '
        'LRN
        '
        Me.LRN.HeaderText = "LRN"
        Me.LRN.MinimumWidth = 6
        Me.LRN.Name = "LRN"
        Me.LRN.ReadOnly = True
        Me.LRN.Width = 125
        '
        'LastName
        '
        Me.LastName.HeaderText = "Last Name"
        Me.LastName.MinimumWidth = 6
        Me.LastName.Name = "LastName"
        Me.LastName.ReadOnly = True
        Me.LastName.Width = 140
        '
        'FirstName
        '
        Me.FirstName.HeaderText = "First Name"
        Me.FirstName.MinimumWidth = 6
        Me.FirstName.Name = "FirstName"
        Me.FirstName.ReadOnly = True
        Me.FirstName.Width = 140
        '
        'MiddleName
        '
        Me.MiddleName.HeaderText = "Middle Name"
        Me.MiddleName.MinimumWidth = 6
        Me.MiddleName.Name = "MiddleName"
        Me.MiddleName.ReadOnly = True
        Me.MiddleName.Width = 140
        '
        'Course
        '
        Me.Course.HeaderText = "Course"
        Me.Course.MinimumWidth = 6
        Me.Course.Name = "Course"
        Me.Course.ReadOnly = True
        Me.Course.Width = 140
        '
        'YearLevel
        '
        Me.YearLevel.HeaderText = "Year Level"
        Me.YearLevel.MinimumWidth = 6
        Me.YearLevel.Name = "YearLevel"
        Me.YearLevel.ReadOnly = True
        Me.YearLevel.Width = 125
        '
        'Section
        '
        Me.Section.HeaderText = "Section"
        Me.Section.MinimumWidth = 6
        Me.Section.Name = "Section"
        Me.Section.ReadOnly = True
        Me.Section.Width = 120
        '
        'ContactNumber
        '
        Me.ContactNumber.HeaderText = "Contact Number"
        Me.ContactNumber.MinimumWidth = 6
        Me.ContactNumber.Name = "ContactNumber"
        Me.ContactNumber.ReadOnly = True
        Me.ContactNumber.Width = 125
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(15, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(257, 15)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = "View list of all students and filter by Grade Level"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(12, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(148, 32)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "Student List"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Location = New System.Drawing.Point(243, 81)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(263, 26)
        Me.Panel1.TabIndex = 123
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(239, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(19, 19)
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(230, 20)
        Me.txtSearch.TabIndex = 28
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(16, 85)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(221, 17)
        Me.Label14.TabIndex = 122
        Me.Label14.Text = "Search by Student ID or Last Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(847, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 17)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "Grade Level"
        '
        'cboGradeLevel
        '
        Me.cboGradeLevel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGradeLevel.FormattingEnabled = True
        Me.cboGradeLevel.Items.AddRange(New Object() {"Grade 1", "Grade 2", "Grade 3", "Grade 4", "Grade 5", "Grade 6", "Grade 7", "Grade 8", "Grade 9", "Grade 10", "Grade 11", "Grade 12", "1st Year College", "2nd Year College", "3rd Year College", "4th Year College"})
        Me.cboGradeLevel.Location = New System.Drawing.Point(931, 81)
        Me.cboGradeLevel.Name = "cboGradeLevel"
        Me.cboGradeLevel.Size = New System.Drawing.Size(144, 25)
        Me.cboGradeLevel.TabIndex = 127
        '
        'frmStudentList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1097, 727)
        Me.Controls.Add(Me.cboGradeLevel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dgvStudents)
        Me.Name = "frmStudentList"
        Me.Text = "frmStudentList"
        CType(Me.dgvStudents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvStudents As DataGridView
    Friend WithEvents StudentID As DataGridViewTextBoxColumn
    Friend WithEvents LRN As DataGridViewTextBoxColumn
    Friend WithEvents LastName As DataGridViewTextBoxColumn
    Friend WithEvents FirstName As DataGridViewTextBoxColumn
    Friend WithEvents MiddleName As DataGridViewTextBoxColumn
    Friend WithEvents Course As DataGridViewTextBoxColumn
    Friend WithEvents YearLevel As DataGridViewTextBoxColumn
    Friend WithEvents Section As DataGridViewTextBoxColumn
    Friend WithEvents ContactNumber As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboGradeLevel As ComboBox
End Class
