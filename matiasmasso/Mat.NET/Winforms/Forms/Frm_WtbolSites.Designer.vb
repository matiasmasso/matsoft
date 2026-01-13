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
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarEmailsTécnicsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_WtbolSites1 = New Winforms.Xl_WtbolSites()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolSerps1 = New Winforms.Xl_WtbolSerps()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_WtBolCtrsSites = New Winforms.Xl_WtBolCtrs()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_WtBolCtrsProducts = New Winforms.Xl_WtBolCtrs()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolSitesBaskets1 = New Winforms.Xl_WtbolSitesBaskets()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_WtbolSites1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_WtbolSerps1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_WtBolCtrsSites, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_WtBolCtrsProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.Xl_WtbolSitesBaskets1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
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
        Me.Xl_WtbolSites1.Size = New System.Drawing.Size(544, 274)
        Me.Xl_WtbolSites1.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(0, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(558, 306)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_WtbolSites1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(550, 280)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_WtbolSerps1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(550, 280)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Pàgines vistes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_WtbolSerps1
        '
        Me.Xl_WtbolSerps1.AllowUserToAddRows = False
        Me.Xl_WtbolSerps1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSerps1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSerps1.DisplayObsolets = False
        Me.Xl_WtbolSerps1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolSerps1.Filter = Nothing
        Me.Xl_WtbolSerps1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolSerps1.MouseIsDown = False
        Me.Xl_WtbolSerps1.Name = "Xl_WtbolSerps1"
        Me.Xl_WtbolSerps1.ReadOnly = True
        Me.Xl_WtbolSerps1.Size = New System.Drawing.Size(544, 274)
        Me.Xl_WtbolSerps1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_WtBolCtrsSites)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Size = New System.Drawing.Size(550, 280)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Ctr Sites"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_WtBolCtrsSites
        '
        Me.Xl_WtBolCtrsSites.AllowUserToAddRows = False
        Me.Xl_WtBolCtrsSites.AllowUserToDeleteRows = False
        Me.Xl_WtBolCtrsSites.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_WtBolCtrsSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtBolCtrsSites.DisplayObsolets = False
        Me.Xl_WtBolCtrsSites.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtBolCtrsSites.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_WtBolCtrsSites.MouseIsDown = False
        Me.Xl_WtBolCtrsSites.Name = "Xl_WtBolCtrsSites"
        Me.Xl_WtBolCtrsSites.ReadOnly = True
        Me.Xl_WtBolCtrsSites.RowTemplate.Height = 40
        Me.Xl_WtBolCtrsSites.Size = New System.Drawing.Size(547, 276)
        Me.Xl_WtBolCtrsSites.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_WtBolCtrsProducts)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(550, 280)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Ctr Products"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_WtBolCtrsProducts
        '
        Me.Xl_WtBolCtrsProducts.AllowUserToAddRows = False
        Me.Xl_WtBolCtrsProducts.AllowUserToDeleteRows = False
        Me.Xl_WtBolCtrsProducts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_WtBolCtrsProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtBolCtrsProducts.DisplayObsolets = False
        Me.Xl_WtBolCtrsProducts.Location = New System.Drawing.Point(2, 2)
        Me.Xl_WtBolCtrsProducts.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_WtBolCtrsProducts.MouseIsDown = False
        Me.Xl_WtBolCtrsProducts.Name = "Xl_WtBolCtrsProducts"
        Me.Xl_WtBolCtrsProducts.ReadOnly = True
        Me.Xl_WtBolCtrsProducts.RowTemplate.Height = 40
        Me.Xl_WtBolCtrsProducts.Size = New System.Drawing.Size(547, 276)
        Me.Xl_WtBolCtrsProducts.TabIndex = 1
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_WtbolSitesBaskets1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(550, 280)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Baskets"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_WtbolSitesBaskets1
        '
        Me.Xl_WtbolSitesBaskets1.AllowUserToAddRows = False
        Me.Xl_WtbolSitesBaskets1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSitesBaskets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSitesBaskets1.DisplayObsolets = False
        Me.Xl_WtbolSitesBaskets1.Filter = Nothing
        Me.Xl_WtbolSitesBaskets1.Location = New System.Drawing.Point(3, 37)
        Me.Xl_WtbolSitesBaskets1.MouseIsDown = False
        Me.Xl_WtbolSitesBaskets1.Name = "Xl_WtbolSitesBaskets1"
        Me.Xl_WtbolSitesBaskets1.ReadOnly = True
        Me.Xl_WtbolSitesBaskets1.Size = New System.Drawing.Size(544, 138)
        Me.Xl_WtbolSitesBaskets1.TabIndex = 0
        '
        'Frm_WtbolSites
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 349)
        Me.Controls.Add(Me.TabControl1)
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
        CType(Me.Xl_WtbolSerps1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_WtBolCtrsSites, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_WtBolCtrsProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.Xl_WtbolSitesBaskets1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_WtBolCtrsSites As Xl_WtBolCtrs
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_WtBolCtrsProducts As Xl_WtBolCtrs
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_WtbolSerps1 As Xl_WtbolSerps
    Friend WithEvents CopiarEmailsTécnicsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Xl_WtbolSitesBaskets1 As Xl_WtbolSitesBaskets
End Class
