<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Catalog
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItemHome = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideObsoleteSkus = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideObsoleteBrands = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBoxFchLastUpdate = New System.Windows.Forms.ToolStripTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_Catalog1 = New Mat.Net.Xl_Catalog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Catalog1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemHome, Me.ArxiuToolStripMenuItem, Me.ToolStripTextBoxFchLastUpdate})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(575, 27)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItemHome
        '
        Me.ToolStripMenuItemHome.Image = Global.Mat.Net.My.Resources.Resources.home_24
        Me.ToolStripMenuItemHome.Name = "ToolStripMenuItemHome"
        Me.ToolStripMenuItemHome.Size = New System.Drawing.Size(28, 23)
        Me.ToolStripMenuItemHome.ToolTipText = "Colapsa totes les marques obertes"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.CheckOnClick = True
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem, Me.HideObsoleteSkus, Me.HideObsoleteBrands})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 23)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.RefrescaToolStripMenuItem.Text = "refresca"
        '
        'HideObsoleteSkus
        '
        Me.HideObsoleteSkus.Checked = True
        Me.HideObsoleteSkus.CheckOnClick = True
        Me.HideObsoleteSkus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HideObsoleteSkus.Name = "HideObsoleteSkus"
        Me.HideObsoleteSkus.Size = New System.Drawing.Size(257, 22)
        Me.HideObsoleteSkus.Text = "oculta categories i articles obsolets"
        '
        'HideObsoleteBrands
        '
        Me.HideObsoleteBrands.Checked = True
        Me.HideObsoleteBrands.CheckOnClick = True
        Me.HideObsoleteBrands.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HideObsoleteBrands.Name = "HideObsoleteBrands"
        Me.HideObsoleteBrands.Size = New System.Drawing.Size(257, 22)
        Me.HideObsoleteBrands.Text = "oculta les marques obsoletes"
        '
        'ToolStripTextBoxFchLastUpdate
        '
        Me.ToolStripTextBoxFchLastUpdate.Name = "ToolStripTextBoxFchLastUpdate"
        Me.ToolStripTextBoxFchLastUpdate.ReadOnly = True
        Me.ToolStripTextBoxFchLastUpdate.Size = New System.Drawing.Size(100, 23)
        Me.ToolStripTextBoxFchLastUpdate.ToolTipText = "Data de la última actualització"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Catalog1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(573, 305)
        Me.Panel1.TabIndex = 1
        '
        'Xl_Catalog1
        '
        Me.Xl_Catalog1.AllowUserToAddRows = False
        Me.Xl_Catalog1.AllowUserToDeleteRows = False
        Me.Xl_Catalog1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Catalog1.DisplayObsolets = False
        Me.Xl_Catalog1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Catalog1.Filter = Nothing
        Me.Xl_Catalog1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Catalog1.MouseIsDown = False
        Me.Xl_Catalog1.Name = "Xl_Catalog1"
        Me.Xl_Catalog1.ReadOnly = True
        Me.Xl_Catalog1.Size = New System.Drawing.Size(573, 282)
        Me.Xl_Catalog1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 282)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(573, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(369, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(204, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'Frm_Catalog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 343)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Catalog"
        Me.Text = "Cataleg"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Catalog1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItemHome As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HideObsoleteSkus As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_Catalog1 As Xl_Catalog
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HideObsoleteBrands As ToolStripMenuItem
    Friend WithEvents ToolStripTextBoxFchLastUpdate As ToolStripTextBox
End Class
