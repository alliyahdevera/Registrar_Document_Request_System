<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequestList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRequestList))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnViewDetails = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgvReqDoc = New System.Windows.Forms.DataGridView()
        Me.RequestNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Documents = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateReq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProcessedReq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReleasedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblrq = New System.Windows.Forms.Label()
        Me.lbldatetime = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblposition = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel7.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.dgvReqDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnViewDetails
        '
        Me.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDetails.ForeColor = System.Drawing.Color.White
        Me.btnViewDetails.Image = CType(resources.GetObject("btnViewDetails.Image"), System.Drawing.Image)
        Me.btnViewDetails.Location = New System.Drawing.Point(988, 84)
        Me.btnViewDetails.Name = "btnViewDetails"
        Me.btnViewDetails.Size = New System.Drawing.Size(155, 35)
        Me.btnViewDetails.TabIndex = 75
        Me.btnViewDetails.Text = "     View Details"
        Me.btnViewDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnViewDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnViewDetails.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Panel16)
        Me.Panel7.Controls.Add(Me.dgvReqDoc)
        Me.Panel7.Location = New System.Drawing.Point(21, 131)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1122, 645)
        Me.Panel7.TabIndex = 73
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel16.Controls.Add(Me.Label11)
        Me.Panel16.Location = New System.Drawing.Point(-3, -1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(1125, 35)
        Me.Panel16.TabIndex = 68
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(7, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(203, 25)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Requested Document"
        '
        'dgvReqDoc
        '
        Me.dgvReqDoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvReqDoc.BackgroundColor = System.Drawing.Color.White
        Me.dgvReqDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReqDoc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RequestNo, Me._date, Me.StudentID, Me.FirstName, Me.LastName, Me.Documents, Me.TotalAmount, Me.PaymentStatus, Me.AmountPaid, Me.ORNo, Me.ORDate, Me.Status, Me.CreateReq, Me.ProcessedReq, Me.ReleasedBy})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReqDoc.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvReqDoc.Location = New System.Drawing.Point(-1, 34)
        Me.dgvReqDoc.Name = "dgvReqDoc"
        Me.dgvReqDoc.ReadOnly = True
        Me.dgvReqDoc.RowHeadersVisible = False
        Me.dgvReqDoc.Size = New System.Drawing.Size(1123, 609)
        Me.dgvReqDoc.TabIndex = 59
        '
        'RequestNo
        '
        Me.RequestNo.HeaderText = "Request Number"
        Me.RequestNo.Name = "RequestNo"
        Me.RequestNo.ReadOnly = True
        '
        '_date
        '
        Me._date.HeaderText = "Date"
        Me._date.Name = "_date"
        Me._date.ReadOnly = True
        Me._date.Width = 130
        '
        'StudentID
        '
        Me.StudentID.HeaderText = "Student ID"
        Me.StudentID.Name = "StudentID"
        Me.StudentID.ReadOnly = True
        '
        'FirstName
        '
        Me.FirstName.HeaderText = "First Name"
        Me.FirstName.Name = "FirstName"
        Me.FirstName.ReadOnly = True
        Me.FirstName.Width = 130
        '
        'LastName
        '
        Me.LastName.HeaderText = "Last Name"
        Me.LastName.Name = "LastName"
        Me.LastName.ReadOnly = True
        Me.LastName.Width = 130
        '
        'Documents
        '
        Me.Documents.HeaderText = "Documents"
        Me.Documents.Name = "Documents"
        Me.Documents.ReadOnly = True
        Me.Documents.Width = 200
        '
        'TotalAmount
        '
        Me.TotalAmount.HeaderText = "Total Amount"
        Me.TotalAmount.Name = "TotalAmount"
        Me.TotalAmount.ReadOnly = True
        '
        'PaymentStatus
        '
        Me.PaymentStatus.HeaderText = "Payment Status"
        Me.PaymentStatus.Name = "PaymentStatus"
        Me.PaymentStatus.ReadOnly = True
        Me.PaymentStatus.Width = 150
        '
        'AmountPaid
        '
        Me.AmountPaid.HeaderText = "Amount Paid"
        Me.AmountPaid.Name = "AmountPaid"
        Me.AmountPaid.ReadOnly = True
        '
        'ORNo
        '
        Me.ORNo.HeaderText = "OR No."
        Me.ORNo.Name = "ORNo"
        Me.ORNo.ReadOnly = True
        '
        'ORDate
        '
        Me.ORDate.HeaderText = "OR Date"
        Me.ORDate.Name = "ORDate"
        Me.ORDate.ReadOnly = True
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 110
        '
        'CreateReq
        '
        Me.CreateReq.HeaderText = "Create Request"
        Me.CreateReq.Name = "CreateReq"
        Me.CreateReq.ReadOnly = True
        Me.CreateReq.Width = 120
        '
        'ProcessedReq
        '
        Me.ProcessedReq.HeaderText = "Processed Request"
        Me.ProcessedReq.Name = "ProcessedReq"
        Me.ProcessedReq.ReadOnly = True
        Me.ProcessedReq.Width = 120
        '
        'ReleasedBy
        '
        Me.ReleasedBy.HeaderText = "Released By"
        Me.ReleasedBy.Name = "ReleasedBy"
        Me.ReleasedBy.ReadOnly = True
        Me.ReleasedBy.Width = 120
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label15.Location = New System.Drawing.Point(25, 52)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(216, 15)
        Me.Label15.TabIndex = 70
        Me.Label15.Text = "View and Manage all document request"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.White
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.txtSearch)
        Me.Panel10.Controls.Add(Me.PictureBox1)
        Me.Panel10.Location = New System.Drawing.Point(274, 87)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(238, 28)
        Me.Panel10.TabIndex = 61
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(3, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(210, 18)
        Me.txtSearch.TabIndex = 32
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(216, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(19, 19)
        Me.PictureBox1.TabIndex = 60
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(246, 20)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Search by Student ID or Last Name"
        '
        'lblrq
        '
        Me.lblrq.AutoSize = True
        Me.lblrq.BackColor = System.Drawing.Color.Transparent
        Me.lblrq.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblrq.Location = New System.Drawing.Point(22, 20)
        Me.lblrq.Name = "lblrq"
        Me.lblrq.Size = New System.Drawing.Size(151, 32)
        Me.lblrq.TabIndex = 68
        Me.lblrq.Text = "Request List"
        '
        'lbldatetime
        '
        Me.lbldatetime.AutoSize = True
        Me.lbldatetime.BackColor = System.Drawing.Color.Transparent
        Me.lbldatetime.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatetime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lbldatetime.Location = New System.Drawing.Point(578, 6)
        Me.lbldatetime.Name = "lbldatetime"
        Me.lbldatetime.Size = New System.Drawing.Size(16, 21)
        Me.lbldatetime.TabIndex = 81
        Me.lbldatetime.Text = "-"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(506, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 21)
        Me.Label19.TabIndex = 80
        Me.Label19.Text = "Today is"
        '
        'lblposition
        '
        Me.lblposition.AutoSize = True
        Me.lblposition.BackColor = System.Drawing.Color.Transparent
        Me.lblposition.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblposition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblposition.Location = New System.Drawing.Point(317, 6)
        Me.lblposition.Name = "lblposition"
        Me.lblposition.Size = New System.Drawing.Size(82, 21)
        Me.lblposition.TabIndex = 79
        Me.lblposition.Text = "NPosition"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(251, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 21)
        Me.Label17.TabIndex = 78
        Me.Label17.Text = "Position:"
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.BackColor = System.Drawing.Color.Transparent
        Me.lblname.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblname.Location = New System.Drawing.Point(56, 6)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(53, 21)
        Me.lblname.TabIndex = 77
        Me.lblname.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 21)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Name:"
        '
        'Timer1
        '
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbldatetime)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.lblposition)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.lblname)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 801)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1174, 35)
        Me.Panel1.TabIndex = 82
        '
        'frmRequestList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1174, 836)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnViewDetails)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.lblrq)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmRequestList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRequestList"
        Me.Panel7.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        CType(Me.dgvReqDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnViewDetails As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvReqDoc As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblrq As Label
    Friend WithEvents lbldatetime As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblposition As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblname As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents RequestNo As DataGridViewTextBoxColumn
    Friend WithEvents _date As DataGridViewTextBoxColumn
    Friend WithEvents StudentID As DataGridViewTextBoxColumn
    Friend WithEvents FirstName As DataGridViewTextBoxColumn
    Friend WithEvents LastName As DataGridViewTextBoxColumn
    Friend WithEvents Documents As DataGridViewTextBoxColumn
    Friend WithEvents TotalAmount As DataGridViewTextBoxColumn
    Friend WithEvents PaymentStatus As DataGridViewTextBoxColumn
    Friend WithEvents AmountPaid As DataGridViewTextBoxColumn
    Friend WithEvents ORNo As DataGridViewTextBoxColumn
    Friend WithEvents ORDate As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents CreateReq As DataGridViewTextBoxColumn
    Friend WithEvents ProcessedReq As DataGridViewTextBoxColumn
    Friend WithEvents ReleasedBy As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
End Class
