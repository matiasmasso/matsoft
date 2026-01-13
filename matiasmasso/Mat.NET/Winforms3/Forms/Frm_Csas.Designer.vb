<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Csas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NovaRemesaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SepaCoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemesaDeExportacioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Csas1 = New Xl_Csas()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_CsbMailingLogs1 = New Xl_CsbMailingLogs()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_Years1 = New Xl_Years()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_Csas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_CsbMailingLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(782, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NovaRemesaToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.RefrescaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'NovaRemesaToolStripMenuItem
        '
        Me.NovaRemesaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SepaCoreToolStripMenuItem, Me.RemesaDeExportacioToolStripMenuItem})
        Me.NovaRemesaToolStripMenuItem.Name = "NovaRemesaToolStripMenuItem"
        Me.NovaRemesaToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.NovaRemesaToolStripMenuItem.Text = "Nova remesa"
        '
        'SepaCoreToolStripMenuItem
        '
        Me.SepaCoreToolStripMenuItem.Name = "SepaCoreToolStripMenuItem"
        Me.SepaCoreToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.SepaCoreToolStripMenuItem.Text = "Sepa Core"
        '
        'RemesaDeExportacioToolStripMenuItem
        '
        Me.RemesaDeExportacioToolStripMenuItem.Name = "RemesaDeExportacioToolStripMenuItem"
        Me.RemesaDeExportacioToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.RemesaDeExportacioToolStripMenuItem.Text = "Remesa de Exportació"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(782, 228)
        Me.Panel1.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(782, 205)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Csas1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(774, 179)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Csas1
        '
        Me.Xl_Csas1.AllowUserToAddRows = False
        Me.Xl_Csas1.AllowUserToDeleteRows = False
        Me.Xl_Csas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Csas1.DisplayObsolets = False
        Me.Xl_Csas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Csas1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Csas1.MouseIsDown = False
        Me.Xl_Csas1.Name = "Xl_Csas1"
        Me.Xl_Csas1.ReadOnly = True
        Me.Xl_Csas1.Size = New System.Drawing.Size(768, 173)
        Me.Xl_Csas1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_CsbMailingLogs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(774, 179)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Notificacions de venciment"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_CsbMailingLogs1
        '
        Me.Xl_CsbMailingLogs1.AllowUserToAddRows = False
        Me.Xl_CsbMailingLogs1.AllowUserToDeleteRows = False
        Me.Xl_CsbMailingLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CsbMailingLogs1.DisplayObsolets = False
        Me.Xl_CsbMailingLogs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CsbMailingLogs1.Filter = Nothing
        Me.Xl_CsbMailingLogs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_CsbMailingLogs1.MouseIsDown = False
        Me.Xl_CsbMailingLogs1.Name = "Xl_CsbMailingLogs1"
        Me.Xl_CsbMailingLogs1.ReadOnly = True
        Me.Xl_CsbMailingLogs1.Size = New System.Drawing.Size(768, 173)
        Me.Xl_CsbMailingLogs1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 205)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(782, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(619, 4)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 2
        Me.Xl_Years1.Value = 0
        '
        'Frm_Csas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 256)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Csas"
        Me.Text = "Remeses de efectes"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_Csas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_CsbMailingLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Csas1 As Xl_Csas
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NovaRemesaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SepaCoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RemesaDeExportacioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_CsbMailingLogs1 As Xl_CsbMailingLogs
End Class
