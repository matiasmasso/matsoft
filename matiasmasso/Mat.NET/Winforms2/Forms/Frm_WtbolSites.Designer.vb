<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WtbolSites
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
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarEmailsTécnicsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_WtbolSites1 = New Mat.Net.Xl_WtbolSites()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_WtbolBaskets1 = New Mat.Net.Xl_WtbolBaskets()
        Me.Xl_WtbolSiteClicks1 = New Mat.Net.Xl_WtbolSiteClicks()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_WtbolSites1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_WtbolBaskets1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_WtbolSiteClicks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(408, 4)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(558, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.CopiarEmailsTécnicsToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'CopiarEmailsTécnicsToolStripMenuItem
        '
        Me.CopiarEmailsTécnicsToolStripMenuItem.Name = "CopiarEmailsTécnicsToolStripMenuItem"
        Me.CopiarEmailsTécnicsToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.CopiarEmailsTécnicsToolStripMenuItem.Text = "Copiar emails técnics"
        '
        'Xl_WtbolSites1
        '
        Me.Xl_WtbolSites1.AllowUserToAddRows = False
        Me.Xl_WtbolSites1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSites1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSites1.DisplayObsolets = False
        Me.Xl_WtbolSites1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolSites1.Filter = Nothing
        Me.Xl_WtbolSites1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolSites1.MouseIsDown = False
        Me.Xl_WtbolSites1.Name = "Xl_WtbolSites1"
        Me.Xl_WtbolSites1.ReadOnly = True
        Me.Xl_WtbolSites1.Size = New System.Drawing.Size(544, 284)
        Me.Xl_WtbolSites1.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(558, 316)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_WtbolSites1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(550, 290)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_WtbolSiteClicks1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(550, 290)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Clics"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_WtbolBaskets1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(550, 290)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Baskets"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Location = New System.Drawing.Point(0, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(558, 316)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 293)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(558, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'Xl_WtbolBaskets1
        '
        Me.Xl_WtbolBaskets1.AllowUserToAddRows = False
        Me.Xl_WtbolBaskets1.AllowUserToDeleteRows = False
        Me.Xl_WtbolBaskets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolBaskets1.DisplayObsolets = False
        Me.Xl_WtbolBaskets1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolBaskets1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolBaskets1.MouseIsDown = False
        Me.Xl_WtbolBaskets1.Name = "Xl_WtbolBaskets1"
        Me.Xl_WtbolBaskets1.ReadOnly = True
        Me.Xl_WtbolBaskets1.Size = New System.Drawing.Size(544, 284)
        Me.Xl_WtbolBaskets1.TabIndex = 1
        '
        'Xl_WtbolSiteClicks1
        '
        Me.Xl_WtbolSiteClicks1.AllowUserToAddRows = False
        Me.Xl_WtbolSiteClicks1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSiteClicks1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSiteClicks1.DisplayObsolets = False
        Me.Xl_WtbolSiteClicks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolSiteClicks1.Filter = Nothing
        Me.Xl_WtbolSiteClicks1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolSiteClicks1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_WtbolSiteClicks1.MouseIsDown = False
        Me.Xl_WtbolSiteClicks1.Name = "Xl_WtbolSiteClicks1"
        Me.Xl_WtbolSiteClicks1.ReadOnly = True
        Me.Xl_WtbolSiteClicks1.RowTemplate.Height = 40
        Me.Xl_WtbolSiteClicks1.Size = New System.Drawing.Size(544, 284)
        Me.Xl_WtbolSiteClicks1.TabIndex = 1
        '
        'Frm_WtbolSites
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 349)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_WtbolSites"
        Me.Text = "WhereToBuyOnline Sites"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_WtbolSites1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_WtbolBaskets1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_WtbolSiteClicks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_WtbolSites1 As Xl_WtbolSites
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents CopiarEmailsTécnicsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_WtbolBaskets1 As Xl_WtbolBaskets
    Friend WithEvents Xl_WtbolSiteClicks1 As Xl_WtbolSiteClicks
End Class
