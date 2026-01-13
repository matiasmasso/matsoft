<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_RRHH
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
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarNominesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarCertificatsIrpfToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ComboBoxYears = New System.Windows.Forms.ComboBox()
        Me.CheckBoxYear = New System.Windows.Forms.CheckBox()
        Me.Xl_Staffs1 = New Winforms.Xl_Staffs()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Nominas1 = New Winforms.Xl_Nominas()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxYearNominas = New System.Windows.Forms.ComboBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_CertificatsIrpf1 = New Winforms.Xl_CertificatsIrpf()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_Staffs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_Nominas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_CertificatsIrpf1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(498, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ImportarNominesToolStripMenuItem, Me.ImportarCertificatsIrpfToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ImportarNominesToolStripMenuItem
        '
        Me.ImportarNominesToolStripMenuItem.Name = "ImportarNominesToolStripMenuItem"
        Me.ImportarNominesToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ImportarNominesToolStripMenuItem.Text = "Importar Nomines"
        '
        'ImportarCertificatsIrpfToolStripMenuItem
        '
        Me.ImportarCertificatsIrpfToolStripMenuItem.Name = "ImportarCertificatsIrpfToolStripMenuItem"
        Me.ImportarCertificatsIrpfToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ImportarCertificatsIrpfToolStripMenuItem.Text = "Importar Certificats Irpf"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(498, 204)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ComboBoxYears)
        Me.TabPage1.Controls.Add(Me.CheckBoxYear)
        Me.TabPage1.Controls.Add(Me.Xl_Staffs1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(490, 178)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Personal"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ComboBoxYears
        '
        Me.ComboBoxYears.FormattingEnabled = True
        Me.ComboBoxYears.Location = New System.Drawing.Point(100, 8)
        Me.ComboBoxYears.Name = "ComboBoxYears"
        Me.ComboBoxYears.Size = New System.Drawing.Size(87, 21)
        Me.ComboBoxYears.TabIndex = 2
        '
        'CheckBoxYear
        '
        Me.CheckBoxYear.AutoSize = True
        Me.CheckBoxYear.Checked = True
        Me.CheckBoxYear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxYear.Location = New System.Drawing.Point(6, 10)
        Me.CheckBoxYear.Name = "CheckBoxYear"
        Me.CheckBoxYear.Size = New System.Drawing.Size(88, 17)
        Me.CheckBoxYear.TabIndex = 1
        Me.CheckBoxYear.Text = "en actiu l'any"
        Me.CheckBoxYear.UseVisualStyleBackColor = True
        '
        'Xl_Staffs1
        '
        Me.Xl_Staffs1.AllowUserToAddRows = False
        Me.Xl_Staffs1.AllowUserToDeleteRows = False
        Me.Xl_Staffs1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Staffs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Staffs1.DisplayObsolets = False
        Me.Xl_Staffs1.Filter = Nothing
        Me.Xl_Staffs1.Location = New System.Drawing.Point(0, 35)
        Me.Xl_Staffs1.MouseIsDown = False
        Me.Xl_Staffs1.Name = "Xl_Staffs1"
        Me.Xl_Staffs1.ReadOnly = True
        Me.Xl_Staffs1.Size = New System.Drawing.Size(490, 143)
        Me.Xl_Staffs1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Nominas1)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.ComboBoxYearNominas)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(490, 178)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Nomines"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Nominas1
        '
        Me.Xl_Nominas1.AllowUserToAddRows = False
        Me.Xl_Nominas1.AllowUserToDeleteRows = False
        Me.Xl_Nominas1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Nominas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Nominas1.DisplayObsolets = False
        Me.Xl_Nominas1.Filter = Nothing
        Me.Xl_Nominas1.Location = New System.Drawing.Point(0, 36)
        Me.Xl_Nominas1.MouseIsDown = False
        Me.Xl_Nominas1.Name = "Xl_Nominas1"
        Me.Xl_Nominas1.ReadOnly = True
        Me.Xl_Nominas1.Size = New System.Drawing.Size(490, 142)
        Me.Xl_Nominas1.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(346, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Exercici:"
        '
        'ComboBoxYearNominas
        '
        Me.ComboBoxYearNominas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYearNominas.FormattingEnabled = True
        Me.ComboBoxYearNominas.Location = New System.Drawing.Point(395, 9)
        Me.ComboBoxYearNominas.Name = "ComboBoxYearNominas"
        Me.ComboBoxYearNominas.Size = New System.Drawing.Size(87, 21)
        Me.ComboBoxYearNominas.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_CertificatsIrpf1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(490, 178)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Certificats Irpf"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_CertificatsIrpf1
        '
        Me.Xl_CertificatsIrpf1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CertificatsIrpf1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CertificatsIrpf1.DisplayObsolets = False
        Me.Xl_CertificatsIrpf1.Filter = Nothing
        Me.Xl_CertificatsIrpf1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CertificatsIrpf1.MouseIsDown = False
        Me.Xl_CertificatsIrpf1.Name = "Xl_CertificatsIrpf1"
        Me.Xl_CertificatsIrpf1.Size = New System.Drawing.Size(490, 178)
        Me.Xl_CertificatsIrpf1.TabIndex = 2
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(344, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 3
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
        Me.Panel1.Size = New System.Drawing.Size(498, 227)
        Me.Panel1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 204)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(498, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'Frm_RRHH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 265)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_RRHH"
        Me.Text = "Recursos Humans"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.Xl_Staffs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.Xl_Nominas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_CertificatsIrpf1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ComboBoxYears As ComboBox
    Friend WithEvents CheckBoxYear As CheckBox
    Friend WithEvents Xl_Staffs1 As Xl_Staffs
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_CertificatsIrpf1 As Xl_CertificatsIrpf
    Friend WithEvents ImportarNominesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportarCertificatsIrpfToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxYearNominas As ComboBox
    Friend WithEvents Xl_Nominas1 As Xl_Nominas
End Class
