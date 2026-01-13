<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bal
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Bal))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonPgc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPdf = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageActiu = New System.Windows.Forms.TabPage()
        Me.DataGridViewActiu = New System.Windows.Forms.DataGridView()
        Me.TabPagePassiu = New System.Windows.Forms.TabPage()
        Me.DataGridViewPassiu = New System.Windows.Forms.DataGridView()
        Me.TabPageExplotacio = New System.Windows.Forms.TabPage()
        Me.DataGridViewExplotacio = New System.Windows.Forms.DataGridView()
        Me.TabPageCashFlow = New System.Windows.Forms.TabPage()
        Me.DataGridViewCashFlow = New System.Windows.Forms.DataGridView()
        Me.TabPageErr = New System.Windows.Forms.TabPage()
        Me.Xl_ErrorGrid1 = New Xl_ErrorGrid()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ComboBoxLang = New System.Windows.Forms.ComboBox()
        Me.CheckBoxIncludeCtas = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonRefresca = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageActiu.SuspendLayout()
        CType(Me.DataGridViewActiu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPagePassiu.SuspendLayout()
        CType(Me.DataGridViewPassiu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageExplotacio.SuspendLayout()
        CType(Me.DataGridViewExplotacio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageCashFlow.SuspendLayout()
        CType(Me.DataGridViewCashFlow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageErr.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonPgc, Me.ToolStripButtonRefresca, Me.ToolStripButtonExcel, Me.ToolStripButtonPdf})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(584, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonPgc
        '
        Me.ToolStripButtonPgc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonPgc.Image = My.Resources.Resources.BookClose
        Me.ToolStripButtonPgc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPgc.Name = "ToolStripButtonPgc"
        Me.ToolStripButtonPgc.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonPgc.Text = "Pla General de Comptabilitat"
        Me.ToolStripButtonPgc.ToolTipText = "Pla General de Comptabilitat"
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        Me.ToolStripButtonExcel.ToolTipText = "Exportar a Excel"
        '
        'ToolStripButtonPdf
        '
        Me.ToolStripButtonPdf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonPdf.Image = My.Resources.Resources.pdf
        Me.ToolStripButtonPdf.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPdf.Name = "ToolStripButtonPdf"
        Me.ToolStripButtonPdf.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonPdf.Text = "Pdf"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageActiu)
        Me.TabControl1.Controls.Add(Me.TabPagePassiu)
        Me.TabControl1.Controls.Add(Me.TabPageExplotacio)
        Me.TabControl1.Controls.Add(Me.TabPageCashFlow)
        Me.TabControl1.Controls.Add(Me.TabPageErr)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(584, 601)
        Me.TabControl1.TabIndex = 1
        '
        'TabPageActiu
        '
        Me.TabPageActiu.Controls.Add(Me.DataGridViewActiu)
        Me.TabPageActiu.Location = New System.Drawing.Point(4, 23)
        Me.TabPageActiu.Name = "TabPageActiu"
        Me.TabPageActiu.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageActiu.Size = New System.Drawing.Size(576, 574)
        Me.TabPageActiu.TabIndex = 0
        Me.TabPageActiu.Text = "ACTIU"
        Me.TabPageActiu.UseVisualStyleBackColor = True
        '
        'DataGridViewActiu
        '
        Me.DataGridViewActiu.AllowUserToAddRows = False
        Me.DataGridViewActiu.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewActiu.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewActiu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewActiu.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewActiu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewActiu.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewActiu.Name = "DataGridViewActiu"
        Me.DataGridViewActiu.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewActiu.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewActiu.Size = New System.Drawing.Size(570, 568)
        Me.DataGridViewActiu.TabIndex = 0
        '
        'TabPagePassiu
        '
        Me.TabPagePassiu.Controls.Add(Me.DataGridViewPassiu)
        Me.TabPagePassiu.Location = New System.Drawing.Point(4, 23)
        Me.TabPagePassiu.Name = "TabPagePassiu"
        Me.TabPagePassiu.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePassiu.Size = New System.Drawing.Size(576, 574)
        Me.TabPagePassiu.TabIndex = 1
        Me.TabPagePassiu.Text = "PASSIU"
        Me.TabPagePassiu.UseVisualStyleBackColor = True
        '
        'DataGridViewPassiu
        '
        Me.DataGridViewPassiu.AllowUserToAddRows = False
        Me.DataGridViewPassiu.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewPassiu.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewPassiu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewPassiu.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewPassiu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewPassiu.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewPassiu.Name = "DataGridViewPassiu"
        Me.DataGridViewPassiu.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewPassiu.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewPassiu.Size = New System.Drawing.Size(570, 568)
        Me.DataGridViewPassiu.TabIndex = 1
        '
        'TabPageExplotacio
        '
        Me.TabPageExplotacio.Controls.Add(Me.DataGridViewExplotacio)
        Me.TabPageExplotacio.Location = New System.Drawing.Point(4, 23)
        Me.TabPageExplotacio.Name = "TabPageExplotacio"
        Me.TabPageExplotacio.Size = New System.Drawing.Size(576, 574)
        Me.TabPageExplotacio.TabIndex = 2
        Me.TabPageExplotacio.Text = "EXPLOTACIO"
        Me.TabPageExplotacio.UseVisualStyleBackColor = True
        '
        'DataGridViewExplotacio
        '
        Me.DataGridViewExplotacio.AllowUserToAddRows = False
        Me.DataGridViewExplotacio.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewExplotacio.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewExplotacio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewExplotacio.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewExplotacio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewExplotacio.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewExplotacio.Name = "DataGridViewExplotacio"
        Me.DataGridViewExplotacio.ReadOnly = True
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewExplotacio.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewExplotacio.Size = New System.Drawing.Size(576, 574)
        Me.DataGridViewExplotacio.TabIndex = 1
        '
        'TabPageCashFlow
        '
        Me.TabPageCashFlow.Controls.Add(Me.DataGridViewCashFlow)
        Me.TabPageCashFlow.Location = New System.Drawing.Point(4, 23)
        Me.TabPageCashFlow.Name = "TabPageCashFlow"
        Me.TabPageCashFlow.Size = New System.Drawing.Size(576, 574)
        Me.TabPageCashFlow.TabIndex = 3
        Me.TabPageCashFlow.Text = "CASH FLOW"
        Me.TabPageCashFlow.UseVisualStyleBackColor = True
        '
        'DataGridViewCashFlow
        '
        Me.DataGridViewCashFlow.AllowUserToAddRows = False
        Me.DataGridViewCashFlow.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCashFlow.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewCashFlow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewCashFlow.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewCashFlow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCashFlow.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewCashFlow.Name = "DataGridViewCashFlow"
        Me.DataGridViewCashFlow.ReadOnly = True
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCashFlow.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewCashFlow.Size = New System.Drawing.Size(576, 574)
        Me.DataGridViewCashFlow.TabIndex = 1
        '
        'TabPageErr
        '
        Me.TabPageErr.Controls.Add(Me.Xl_ErrorGrid1)
        Me.TabPageErr.ImageKey = "(none)"
        Me.TabPageErr.Location = New System.Drawing.Point(4, 23)
        Me.TabPageErr.Name = "TabPageErr"
        Me.TabPageErr.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageErr.Size = New System.Drawing.Size(576, 574)
        Me.TabPageErr.TabIndex = 4
        Me.TabPageErr.Text = "ERRORS"
        Me.TabPageErr.UseVisualStyleBackColor = True
        '
        'Xl_ErrorGrid1
        '
        Me.Xl_ErrorGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ErrorGrid1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ErrorGrid1.Name = "Xl_ErrorGrid1"
        Me.Xl_ErrorGrid1.Size = New System.Drawing.Size(570, 568)
        Me.Xl_ErrorGrid1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "WARN")
        '
        'ComboBoxLang
        '
        Me.ComboBoxLang.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxLang.FormattingEnabled = True
        Me.ComboBoxLang.Items.AddRange(New Object() {"ESP", "CAT", "ENG"})
        Me.ComboBoxLang.Location = New System.Drawing.Point(396, 4)
        Me.ComboBoxLang.Name = "ComboBoxLang"
        Me.ComboBoxLang.Size = New System.Drawing.Size(46, 21)
        Me.ComboBoxLang.TabIndex = 3
        Me.ComboBoxLang.Text = "CAT"
        '
        'CheckBoxIncludeCtas
        '
        Me.CheckBoxIncludeCtas.AutoSize = True
        Me.CheckBoxIncludeCtas.Location = New System.Drawing.Point(256, 4)
        Me.CheckBoxIncludeCtas.Name = "CheckBoxIncludeCtas"
        Me.CheckBoxIncludeCtas.Size = New System.Drawing.Size(98, 17)
        Me.CheckBoxIncludeCtas.TabIndex = 1
        Me.CheckBoxIncludeCtas.Text = "Inclou comptes"
        Me.CheckBoxIncludeCtas.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(448, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'ButtonRefresca
        '
        Me.ButtonRefresca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresca.Enabled = False
        Me.ButtonRefresca.Location = New System.Drawing.Point(550, 4)
        Me.ButtonRefresca.Name = "ButtonRefresca"
        Me.ButtonRefresca.Size = New System.Drawing.Size(27, 20)
        Me.ButtonRefresca.TabIndex = 5
        Me.ButtonRefresca.Text = "..."
        Me.ButtonRefresca.UseVisualStyleBackColor = True
        '
        'Frm_Bal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 626)
        Me.Controls.Add(Me.ButtonRefresca)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.CheckBoxIncludeCtas)
        Me.Controls.Add(Me.ComboBoxLang)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Bal"
        Me.Text = "COMPTES ANUALS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageActiu.ResumeLayout(False)
        CType(Me.DataGridViewActiu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPagePassiu.ResumeLayout(False)
        CType(Me.DataGridViewPassiu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageExplotacio.ResumeLayout(False)
        CType(Me.DataGridViewExplotacio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageCashFlow.ResumeLayout(False)
        CType(Me.DataGridViewCashFlow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageErr.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageActiu As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePassiu As System.Windows.Forms.TabPage
    Friend WithEvents TabPageExplotacio As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCashFlow As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewActiu As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewPassiu As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewExplotacio As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCashFlow As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBoxLang As System.Windows.Forms.ComboBox
    Friend WithEvents TabPageErr As System.Windows.Forms.TabPage
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Xl_ErrorGrid1 As Xl_ErrorGrid
    Friend WithEvents ToolStripButtonPgc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPdf As System.Windows.Forms.ToolStripButton
    Friend WithEvents CheckBoxIncludeCtas As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonRefresca As System.Windows.Forms.Button
End Class
