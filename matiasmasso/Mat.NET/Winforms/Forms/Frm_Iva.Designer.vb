<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Iva
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
        Me.ComboBoxMonth = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VeureAssentamentLiquidacióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_BaseQuotas1 = New Winforms.Xl_BaseQuotas()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.GeneraAssentamentLiquidacióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_BaseQuotas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxMonth
        '
        Me.ComboBoxMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxMonth.FormattingEnabled = True
        Me.ComboBoxMonth.Location = New System.Drawing.Point(405, 12)
        Me.ComboBoxMonth.Name = "ComboBoxMonth"
        Me.ComboBoxMonth.Size = New System.Drawing.Size(73, 21)
        Me.ComboBoxMonth.TabIndex = 2
        Me.ComboBoxMonth.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(478, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.VeureAssentamentLiquidacióToolStripMenuItem, Me.GeneraAssentamentLiquidacióToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'VeureAssentamentLiquidacióToolStripMenuItem
        '
        Me.VeureAssentamentLiquidacióToolStripMenuItem.Name = "VeureAssentamentLiquidacióToolStripMenuItem"
        Me.VeureAssentamentLiquidacióToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.VeureAssentamentLiquidacióToolStripMenuItem.Text = "veure assentament Liquidació"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_BaseQuotas1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 47)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(478, 245)
        Me.Panel1.TabIndex = 47
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 222)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(478, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Xl_BaseQuotas1
        '
        Me.Xl_BaseQuotas1.AllowUserToAddRows = False
        Me.Xl_BaseQuotas1.AllowUserToDeleteRows = False
        Me.Xl_BaseQuotas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BaseQuotas1.DisplayObsolets = False
        Me.Xl_BaseQuotas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BaseQuotas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BaseQuotas1.MouseIsDown = False
        Me.Xl_BaseQuotas1.Name = "Xl_BaseQuotas1"
        Me.Xl_BaseQuotas1.ReadOnly = True
        Me.Xl_BaseQuotas1.Size = New System.Drawing.Size(478, 222)
        Me.Xl_BaseQuotas1.TabIndex = 0
        Me.Xl_BaseQuotas1.Visible = False
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(231, 12)
        Me.Xl_Years1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 21)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        Me.Xl_Years1.Visible = False
        '
        'GeneraAssentamentLiquidacióToolStripMenuItem
        '
        Me.GeneraAssentamentLiquidacióToolStripMenuItem.Name = "GeneraAssentamentLiquidacióToolStripMenuItem"
        Me.GeneraAssentamentLiquidacióToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.GeneraAssentamentLiquidacióToolStripMenuItem.Text = "genera assentament liquidació"
        '
        'Frm_Iva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 291)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBoxMonth)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Iva"
        Me.Text = "IVA Mod.303"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_BaseQuotas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_BaseQuotas1 As Xl_BaseQuotas
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents ComboBoxMonth As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents VeureAssentamentLiquidacióToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents GeneraAssentamentLiquidacióToolStripMenuItem As ToolStripMenuItem
End Class
