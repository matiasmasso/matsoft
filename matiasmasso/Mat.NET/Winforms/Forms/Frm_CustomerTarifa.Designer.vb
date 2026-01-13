<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CustomerTarifa
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Tarifa1 = New Winforms.Xl_Tarifa()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Xl_CustomerDtos1 = New Winforms.Xl_CustomerTarifaDtos()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_CliProductDtos1 = New Winforms.Xl_CliProductDtos()
        Me.Xl_PercentDtoGlobal = New Winforms.Xl_Percent()
        Me.RadioButtonDtoTpa = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDtoGlobal = New System.Windows.Forms.RadioButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_CliProductsBlocked1 = New Winforms.Xl_CliProductsBlocked()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_PremiumLinesXCustomer1 = New Winforms.Xl_PremiumLinesXCustomer()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MargesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Xl_CliProductDtos1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_PremiumLinesXCustomer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.CustomFormat = "dd/MM/yy HH:mm:ss"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(231, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(137, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresh.Location = New System.Drawing.Point(367, 11)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(28, 22)
        Me.ButtonRefresh.TabIndex = 1
        Me.ButtonRefresh.Text = "..."
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(402, 524)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Tarifa1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(394, 498)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Preus"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Tarifa1
        '
        Me.Xl_Tarifa1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Tarifa1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Tarifa1.Name = "Xl_Tarifa1"
        Me.Xl_Tarifa1.Size = New System.Drawing.Size(388, 492)
        Me.Xl_Tarifa1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(394, 515)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Descomptes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Xl_CustomerDtos1)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(370, 214)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Descompte sobre el PVP per calcular el preu de tarifa"
        '
        'Xl_CustomerDtos1
        '
        Me.Xl_CustomerDtos1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CustomerDtos1.Location = New System.Drawing.Point(34, 19)
        Me.Xl_CustomerDtos1.Name = "Xl_CustomerDtos1"
        Me.Xl_CustomerDtos1.Size = New System.Drawing.Size(330, 182)
        Me.Xl_CustomerDtos1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Xl_CliProductDtos1)
        Me.GroupBox1.Controls.Add(Me.Xl_PercentDtoGlobal)
        Me.GroupBox1.Controls.Add(Me.RadioButtonDtoTpa)
        Me.GroupBox1.Controls.Add(Me.RadioButtonDtoGlobal)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 249)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(370, 253)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Descompte en comanda"
        '
        'Xl_CliProductDtos1
        '
        Me.Xl_CliProductDtos1.AllowUserToAddRows = False
        Me.Xl_CliProductDtos1.AllowUserToDeleteRows = False
        Me.Xl_CliProductDtos1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CliProductDtos1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CliProductDtos1.DisplayObsolets = False
        Me.Xl_CliProductDtos1.Location = New System.Drawing.Point(34, 73)
        Me.Xl_CliProductDtos1.MouseIsDown = False
        Me.Xl_CliProductDtos1.Name = "Xl_CliProductDtos1"
        Me.Xl_CliProductDtos1.ReadOnly = True
        Me.Xl_CliProductDtos1.Size = New System.Drawing.Size(330, 164)
        Me.Xl_CliProductDtos1.TabIndex = 58
        Me.Xl_CliProductDtos1.Visible = False
        '
        'Xl_PercentDtoGlobal
        '
        Me.Xl_PercentDtoGlobal.Location = New System.Drawing.Point(231, 27)
        Me.Xl_PercentDtoGlobal.Name = "Xl_PercentDtoGlobal"
        Me.Xl_PercentDtoGlobal.Size = New System.Drawing.Size(60, 20)
        Me.Xl_PercentDtoGlobal.TabIndex = 57
        Me.Xl_PercentDtoGlobal.Text = "0,00 %"
        Me.Xl_PercentDtoGlobal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoGlobal.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.Xl_PercentDtoGlobal.Visible = False
        '
        'RadioButtonDtoTpa
        '
        Me.RadioButtonDtoTpa.AutoSize = True
        Me.RadioButtonDtoTpa.Location = New System.Drawing.Point(18, 50)
        Me.RadioButtonDtoTpa.Name = "RadioButtonDtoTpa"
        Me.RadioButtonDtoTpa.Size = New System.Drawing.Size(194, 17)
        Me.RadioButtonDtoTpa.TabIndex = 55
        Me.RadioButtonDtoTpa.TabStop = True
        Me.RadioButtonDtoTpa.Text = "descompte segons marca comercial"
        Me.RadioButtonDtoTpa.UseVisualStyleBackColor = True
        '
        'RadioButtonDtoGlobal
        '
        Me.RadioButtonDtoGlobal.AutoSize = True
        Me.RadioButtonDtoGlobal.Location = New System.Drawing.Point(18, 27)
        Me.RadioButtonDtoGlobal.Name = "RadioButtonDtoGlobal"
        Me.RadioButtonDtoGlobal.Size = New System.Drawing.Size(211, 17)
        Me.RadioButtonDtoGlobal.TabIndex = 54
        Me.RadioButtonDtoGlobal.TabStop = True
        Me.RadioButtonDtoGlobal.Text = "descompte global, compri el que compri"
        Me.RadioButtonDtoGlobal.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_CliProductsBlocked1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(394, 515)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Exclusives"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_CliProductsBlocked1
        '
        Me.Xl_CliProductsBlocked1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CliProductsBlocked1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_CliProductsBlocked1.Name = "Xl_CliProductsBlocked1"
        Me.Xl_CliProductsBlocked1.Size = New System.Drawing.Size(388, 509)
        Me.Xl_CliProductsBlocked1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_PremiumLinesXCustomer1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(394, 515)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Premium Lines"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_PremiumLinesXCustomer1
        '
        Me.Xl_PremiumLinesXCustomer1.AllowUserToAddRows = False
        Me.Xl_PremiumLinesXCustomer1.AllowUserToDeleteRows = False
        Me.Xl_PremiumLinesXCustomer1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PremiumLinesXCustomer1.DisplayObsolets = False
        Me.Xl_PremiumLinesXCustomer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PremiumLinesXCustomer1.Filter = Nothing
        Me.Xl_PremiumLinesXCustomer1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PremiumLinesXCustomer1.MouseIsDown = False
        Me.Xl_PremiumLinesXCustomer1.Name = "Xl_PremiumLinesXCustomer1"
        Me.Xl_PremiumLinesXCustomer1.ReadOnly = True
        Me.Xl_PremiumLinesXCustomer1.Size = New System.Drawing.Size(388, 509)
        Me.Xl_PremiumLinesXCustomer1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(402, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MargesToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'MargesToolStripMenuItem
        '
        Me.MargesToolStripMenuItem.Name = "MargesToolStripMenuItem"
        Me.MargesToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.MargesToolStripMenuItem.Text = "Marges"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(402, 547)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 524)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(402, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 3
        '
        'Frm_CustomerTarifa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 586)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_CustomerTarifa"
        Me.Text = "Tarifas"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Xl_CliProductDtos1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_PremiumLinesXCustomer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Tarifa1 As Winforms.Xl_Tarifa
    Friend WithEvents Xl_CustomerDtos1 As Winforms.Xl_CustomerTarifaDtos
    Friend WithEvents Xl_CliProductsBlocked1 As Winforms.Xl_CliProductsBlocked
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MargesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Xl_PercentDtoGlobal As Xl_Percent
    Friend WithEvents RadioButtonDtoTpa As RadioButton
    Friend WithEvents RadioButtonDtoGlobal As RadioButton
    Friend WithEvents Xl_CliProductDtos1 As Xl_CliProductDtos
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_PremiumLinesXCustomer1 As Xl_PremiumLinesXCustomer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
