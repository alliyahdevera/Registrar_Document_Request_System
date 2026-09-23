<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequestDetails
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
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.btnBacktoRequestList = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtYearLevel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCourse = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStudentName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStudentID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRequestDate = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRequestNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgvReqDoc = New System.Windows.Forms.DataGridView()
        Me.DocumentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGenerateReport = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.dtpORDate = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtORNo = New System.Windows.Forms.TextBox()
        Me.cboPaymentStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtRequestID = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.dgvReqDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Panel11.Controls.Add(Me.btnBacktoRequestList)
        Me.Panel11.Controls.Add(Me.Label14)
        Me.Panel11.Location = New System.Drawing.Point(0, 1)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1121, 53)
        Me.Panel11.TabIndex = 63
        '
        'btnBacktoRequestList
        '
        Me.btnBacktoRequestList.BackColor = System.Drawing.Color.White
        Me.btnBacktoRequestList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBacktoRequestList.Location = New System.Drawing.Point(979, 12)
        Me.btnBacktoRequestList.Name = "btnBacktoRequestList"
        Me.btnBacktoRequestList.Size = New System.Drawing.Size(130, 27)
        Me.btnBacktoRequestList.TabIndex = 52
        Me.btnBacktoRequestList.Text = "Back to Request List"
        Me.btnBacktoRequestList.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(15, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(147, 25)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "Request Details"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.White
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.txtTotalAmount)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.txtYearLevel)
        Me.Panel8.Controls.Add(Me.Label2)
        Me.Panel8.Controls.Add(Me.txtCourse)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.txtStudentName)
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Controls.Add(Me.txtStudentID)
        Me.Panel8.Controls.Add(Me.Label1)
        Me.Panel8.Controls.Add(Me.Panel15)
        Me.Panel8.Controls.Add(Me.txtRequestDate)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.txtRequestNo)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Location = New System.Drawing.Point(23, 77)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1076, 201)
        Me.Panel8.TabIndex = 64
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmount.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.Location = New System.Drawing.Point(821, 93)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.ReadOnly = True
        Me.txtTotalAmount.Size = New System.Drawing.Size(226, 43)
        Me.txtTotalAmount.TabIndex = 77
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(816, 58)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(133, 25)
        Me.Label24.TabIndex = 76
        Me.Label24.Text = "Total Amount"
        '
        'txtYearLevel
        '
        Me.txtYearLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYearLevel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYearLevel.Location = New System.Drawing.Point(544, 149)
        Me.txtYearLevel.Name = "txtYearLevel"
        Me.txtYearLevel.ReadOnly = True
        Me.txtYearLevel.Size = New System.Drawing.Size(229, 25)
        Me.txtYearLevel.TabIndex = 75
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(422, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "Year Level"
        '
        'txtCourse
        '
        Me.txtCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCourse.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCourse.Location = New System.Drawing.Point(544, 104)
        Me.txtCourse.Name = "txtCourse"
        Me.txtCourse.ReadOnly = True
        Me.txtCourse.Size = New System.Drawing.Size(229, 25)
        Me.txtCourse.TabIndex = 73
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(422, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "Course"
        '
        'txtStudentName
        '
        Me.txtStudentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStudentName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudentName.Location = New System.Drawing.Point(544, 60)
        Me.txtStudentName.Name = "txtStudentName"
        Me.txtStudentName.ReadOnly = True
        Me.txtStudentName.Size = New System.Drawing.Size(229, 25)
        Me.txtStudentName.TabIndex = 71
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(422, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 20)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Student Name"
        '
        'txtStudentID
        '
        Me.txtStudentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStudentID.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudentID.Location = New System.Drawing.Point(154, 149)
        Me.txtStudentID.Name = "txtStudentID"
        Me.txtStudentID.ReadOnly = True
        Me.txtStudentID.Size = New System.Drawing.Size(229, 25)
        Me.txtStudentID.TabIndex = 69
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 20)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Student ID"
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel15.Controls.Add(Me.Label19)
        Me.Panel15.Location = New System.Drawing.Point(-1, -1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(1076, 37)
        Me.Panel15.TabIndex = 67
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(4, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(196, 25)
        Me.Label19.TabIndex = 51
        Me.Label19.Text = "Request Information"
        '
        'txtRequestDate
        '
        Me.txtRequestDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestDate.Location = New System.Drawing.Point(154, 104)
        Me.txtRequestDate.Name = "txtRequestDate"
        Me.txtRequestDate.ReadOnly = True
        Me.txtRequestDate.Size = New System.Drawing.Size(229, 25)
        Me.txtRequestDate.TabIndex = 54
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(32, 107)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(99, 20)
        Me.Label20.TabIndex = 45
        Me.Label20.Text = "Request Date"
        '
        'txtRequestNo
        '
        Me.txtRequestNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestNo.Location = New System.Drawing.Point(154, 60)
        Me.txtRequestNo.Name = "txtRequestNo"
        Me.txtRequestNo.ReadOnly = True
        Me.txtRequestNo.Size = New System.Drawing.Size(229, 25)
        Me.txtRequestNo.TabIndex = 38
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(32, 63)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(92, 20)
        Me.Label22.TabIndex = 33
        Me.Label22.Text = "Request No."
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Panel16)
        Me.Panel7.Controls.Add(Me.dgvReqDoc)
        Me.Panel7.Location = New System.Drawing.Point(23, 298)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1069, 265)
        Me.Panel7.TabIndex = 65
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel16.Controls.Add(Me.Label11)
        Me.Panel16.Location = New System.Drawing.Point(-3, -1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(1135, 35)
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
        Me.dgvReqDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReqDoc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DocumentName, Me.Quantity, Me.Amount, Me.Subtotal})
        Me.dgvReqDoc.Location = New System.Drawing.Point(-1, 32)
        Me.dgvReqDoc.Name = "dgvReqDoc"
        Me.dgvReqDoc.ReadOnly = True
        Me.dgvReqDoc.Size = New System.Drawing.Size(1069, 232)
        Me.dgvReqDoc.TabIndex = 59
        '
        'DocumentName
        '
        Me.DocumentName.HeaderText = "Document Name"
        Me.DocumentName.Name = "DocumentName"
        Me.DocumentName.ReadOnly = True
        Me.DocumentName.Width = 330
        '
        'Quantity
        '
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        Me.Quantity.Width = 220
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        Me.Amount.Width = 235
        '
        'Subtotal
        '
        Me.Subtotal.HeaderText = "Subtotal"
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.ReadOnly = True
        Me.Subtotal.Width = 240
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnGenerateReport)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.txtAmountPaid)
        Me.Panel1.Controls.Add(Me.dtpORDate)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtORNo)
        Me.Panel1.Controls.Add(Me.cboPaymentStatus)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Location = New System.Drawing.Point(23, 590)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(641, 183)
        Me.Panel1.TabIndex = 66
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnGenerateReport.FlatAppearance.BorderSize = 0
        Me.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateReport.ForeColor = System.Drawing.Color.White
        Me.btnGenerateReport.Location = New System.Drawing.Point(449, 137)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(163, 27)
        Me.btnGenerateReport.TabIndex = 80
        Me.btnGenerateReport.Text = "Save Payment"
        Me.btnGenerateReport.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(320, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 20)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "Amount Paid"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Location = New System.Drawing.Point(-1, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(642, 37)
        Me.Panel2.TabIndex = 67
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(4, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(203, 25)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Payment Information"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.Location = New System.Drawing.Point(449, 52)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(163, 25)
        Me.txtAmountPaid.TabIndex = 68
        '
        'dtpORDate
        '
        Me.dtpORDate.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpORDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpORDate.Location = New System.Drawing.Point(94, 95)
        Me.dtpORDate.Name = "dtpORDate"
        Me.dtpORDate.Size = New System.Drawing.Size(209, 25)
        Me.dtpORDate.TabIndex = 58
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(18, 95)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 20)
        Me.Label16.TabIndex = 55
        Me.Label16.Text = "OR Date"
        '
        'txtORNo
        '
        Me.txtORNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtORNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtORNo.Location = New System.Drawing.Point(94, 55)
        Me.txtORNo.Name = "txtORNo"
        Me.txtORNo.Size = New System.Drawing.Size(209, 25)
        Me.txtORNo.TabIndex = 54
        '
        'cboPaymentStatus
        '
        Me.cboPaymentStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentStatus.FormattingEnabled = True
        Me.cboPaymentStatus.Items.AddRange(New Object() {"Unpaid", "Paid"})
        Me.cboPaymentStatus.Location = New System.Drawing.Point(449, 94)
        Me.cboPaymentStatus.Name = "cboPaymentStatus"
        Me.cboPaymentStatus.Size = New System.Drawing.Size(163, 25)
        Me.cboPaymentStatus.TabIndex = 53
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 20)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "OR No."
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(320, 95)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(114, 20)
        Me.Label21.TabIndex = 39
        Me.Label21.Text = "Payment Status"
        '
        'cboStatus
        '
        Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Pending", "Processing", "Ready for Release", "Released", "Cancelled"})
        Me.cboStatus.Location = New System.Drawing.Point(136, 95)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(229, 25)
        Me.cboStatus.TabIndex = 57
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(37, 96)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(50, 20)
        Me.Label23.TabIndex = 56
        Me.Label23.Text = "Status"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtRequestID)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.cboStatus)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Location = New System.Drawing.Point(690, 590)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(402, 183)
        Me.Panel3.TabIndex = 81
        '
        'txtRequestID
        '
        Me.txtRequestID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestID.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestID.Location = New System.Drawing.Point(136, 55)
        Me.txtRequestID.Name = "txtRequestID"
        Me.txtRequestID.ReadOnly = True
        Me.txtRequestID.Size = New System.Drawing.Size(229, 25)
        Me.txtRequestID.TabIndex = 79
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(136, 137)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(229, 27)
        Me.Button1.TabIndex = 80
        Me.Button1.Text = "Update Status"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(37, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 20)
        Me.Label8.TabIndex = 78
        Me.Label8.Text = "Request ID"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Location = New System.Drawing.Point(-1, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(642, 37)
        Me.Panel4.TabIndex = 67
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(4, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(143, 25)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Request Status"
        '
        'frmRequestDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1121, 799)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel11)
        Me.Name = "frmRequestDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRequestDetails"
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        CType(Me.dgvReqDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Label19 As Label
    Friend WithEvents txtRequestDate As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtRequestNo As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtYearLevel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtCourse As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtStudentName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtStudentID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTotalAmount As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvReqDoc As DataGridView
    Friend WithEvents DocumentName As DataGridViewTextBoxColumn
    Friend WithEvents Quantity As DataGridViewTextBoxColumn
    Friend WithEvents Amount As DataGridViewTextBoxColumn
    Friend WithEvents Subtotal As DataGridViewTextBoxColumn
    Friend WithEvents btnBacktoRequestList As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpORDate As DateTimePicker
    Friend WithEvents Label16 As Label
    Friend WithEvents txtORNo As TextBox
    Friend WithEvents cboPaymentStatus As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtAmountPaid As TextBox
    Friend WithEvents btnGenerateReport As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents txtRequestID As TextBox
    Friend WithEvents Label8 As Label
End Class
