<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Importacions
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_ProgressBar1 = New Mat.Net.Xl_ProgressBar()
        Me.Xl_Importacions1 = New Mat.Net.Xl_Importacions()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ImportHeaderxWeeks1 = New Mat.Net.Xl_ImportHeaderxWeeks()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_ImportTransits1 = New Mat.Net.Xl_ImportTransits()
        Me.ComboBoxProveidor = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.AfegirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_Importacions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ImportHeaderxWeeks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_ImportTransits1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(-2, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1074, 281)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_ProgressBar1)
        Me.TabPage1.Controls.Add(Me.Xl_Importacions1)
        Me.TabPage1.Controls.Add(Me.ProgressBar1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1066, 255)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(584, 191)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(214, 30)
        Me.Xl_ProgressBar1.TabIndex = 2
        Me.Xl_ProgressBar1.Visible = False
        '
        'Xl_Importacions1
        '
        Me.Xl_Importacions1.DisplayObsolets = False
        Me.Xl_Importacions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Importacions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Importacions1.MouseIsDown = False
        Me.Xl_Importacions1.Name = "Xl_Importacions1"
        Me.Xl_Importacions1.Size = New System.Drawing.Size(1060, 226)
        Me.Xl_Importacions1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(3, 229)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1060, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ImportHeaderxWeeks1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1066, 255)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Setmanes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ImportHeaderxWeeks1
        '
        Me.Xl_ImportHeaderxWeeks1.AllowUserToAddRows = False
        Me.Xl_ImportHeaderxWeeks1.AllowUserToDeleteRows = False
        Me.Xl_ImportHeaderxWeeks1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ImportHeaderxWeeks1.DisplayObsolets = False
        Me.Xl_ImportHeaderxWeeks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportHeaderxWeeks1.Filter = Nothing
        Me.Xl_ImportHeaderxWeeks1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ImportHeaderxWeeks1.MouseIsDown = False
        Me.Xl_ImportHeaderxWeeks1.Name = "Xl_ImportHeaderxWeeks1"
        Me.Xl_ImportHeaderxWeeks1.ReadOnly = True
        Me.Xl_ImportHeaderxWeeks1.Size = New System.Drawing.Size(1060, 249)
        Me.Xl_ImportHeaderxWeeks1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_ImportTransits1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1066, 255)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Transits"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_ImportTransits1
        '
        Me.Xl_ImportTransits1.AllowUserToAddRows = False
        Me.Xl_ImportTransits1.AllowUserToDeleteRows = False
        Me.Xl_ImportTransits1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ImportTransits1.DisplayObsolets = False
        Me.Xl_ImportTransits1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportTransits1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ImportTransits1.MouseIsDown = False
        Me.Xl_ImportTransits1.Name = "Xl_ImportTransits1"
        Me.Xl_ImportTransits1.ReadOnly = True
        Me.Xl_ImportTransits1.Size = New System.Drawing.Size(1060, 249)
        Me.Xl_ImportTransits1.TabIndex = 0
        '
        'ComboBoxProveidor
        '
        Me.ComboBoxProveidor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxProveidor.FormattingEnabled = True
        Me.ComboBoxProveidor.Location = New System.Drawing.Point(811, 12)
        Me.ComboBoxProveidor.Name = "ComboBoxProveidor"
        Me.ComboBoxProveidor.Size = New System.Drawing.Size(254, 21)
        Me.ComboBoxProveidor.TabIndex = 3
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1071, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.AfegirToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(642, 12)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 5
        Me.Xl_Years1.Value = 0
        '
        'AfegirToolStripMenuItem
        '
        Me.AfegirToolStripMenuItem.Name = "AfegirToolStripMenuItem"
        Me.AfegirToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AfegirToolStripMenuItem.Text = "Afegir"
        '
        'Frm_Importacions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 325)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.ComboBoxProveidor)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Importacions"
        Me.Text = "Remeses de importació"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_Importacions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ImportHeaderxWeeks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_ImportTransits1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Importacions1 As Xl_Importacions
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_ImportHeaderxWeeks1 As Xl_ImportHeaderxWeeks
    Friend WithEvents ComboBoxProveidor As ComboBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_ImportTransits1 As Xl_ImportTransits
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AfegirToolStripMenuItem As ToolStripMenuItem
End Class
