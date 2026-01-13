<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cataleg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Cataleg))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ProductBrands1 = New Mat.Net.Xl_ProductBrands()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ProductCategories1 = New Mat.Net.Xl_ProductCategories()
        Me.Xl_ProductSkusExtended1 = New Mat.Net.Xl_ProductSkusExtended()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.IncentiusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StocksRealsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaF5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_LookupMgz1 = New Mat.Net.Xl_LookupMgz()
        Me.Xl_ProductSku1 = New Mat.Net.Xl_ProductSku()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DescatalogatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_ProductSkusExtended1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ProductBrands1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(716, 217)
        Me.SplitContainer1.SplitterDistance = 120
        Me.SplitContainer1.TabIndex = 2
        '
        'Xl_ProductBrands1
        '
        Me.Xl_ProductBrands1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductBrands1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductBrands1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductBrands1.Name = "Xl_ProductBrands1"
        Me.Xl_ProductBrands1.ShowObsolets = False
        Me.Xl_ProductBrands1.Size = New System.Drawing.Size(120, 217)
        Me.Xl_ProductBrands1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_ProductCategories1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_ProductSkusExtended1)
        Me.SplitContainer2.Size = New System.Drawing.Size(592, 217)
        Me.SplitContainer2.SplitterDistance = 140
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.AllowRemoveOnContextMenu = False
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductCategories1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.ShowObsolets = False
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(140, 217)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'Xl_ProductSkusExtended1
        '
        Me.Xl_ProductSkusExtended1.AllowUserToAddRows = False
        Me.Xl_ProductSkusExtended1.AllowUserToDeleteRows = False
        Me.Xl_ProductSkusExtended1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkusExtended1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkusExtended1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductSkusExtended1.Name = "Xl_ProductSkusExtended1"
        Me.Xl_ProductSkusExtended1.ReadOnly = True
        Me.Xl_ProductSkusExtended1.ShowObsolets = False
        Me.Xl_ProductSkusExtended1.Size = New System.Drawing.Size(448, 217)
        Me.Xl_ProductSkusExtended1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(716, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DescatalogatsToolStripMenuItem, Me.IncentiusToolStripMenuItem, Me.StocksRealsToolStripMenuItem, Me.RefrescaF5ToolStripMenuItem, Me.ImportarExcelToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(48, 22)
        Me.ToolStripDropDownButton1.Text = "Arxiu"
        '
        'IncentiusToolStripMenuItem
        '
        Me.IncentiusToolStripMenuItem.Name = "IncentiusToolStripMenuItem"
        Me.IncentiusToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.IncentiusToolStripMenuItem.Text = "Incentius"
        '
        'StocksRealsToolStripMenuItem
        '
        Me.StocksRealsToolStripMenuItem.CheckOnClick = True
        Me.StocksRealsToolStripMenuItem.Name = "StocksRealsToolStripMenuItem"
        Me.StocksRealsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.StocksRealsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.StocksRealsToolStripMenuItem.Text = "Stocks reals"
        '
        'RefrescaF5ToolStripMenuItem
        '
        Me.RefrescaF5ToolStripMenuItem.Name = "RefrescaF5ToolStripMenuItem"
        Me.RefrescaF5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefrescaF5ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RefrescaF5ToolStripMenuItem.Text = "Refresca"
        '
        'ImportarExcelToolStripMenuItem
        '
        Me.ImportarExcelToolStripMenuItem.Name = "ImportarExcelToolStripMenuItem"
        Me.ImportarExcelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ImportarExcelToolStripMenuItem.Text = "Importar Excel"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 242)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(716, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(716, 217)
        Me.Panel1.TabIndex = 7
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 194)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(716, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_LookupMgz1
        '
        Me.Xl_LookupMgz1.IsDirty = False
        Me.Xl_LookupMgz1.Location = New System.Drawing.Point(72, 1)
        Me.Xl_LookupMgz1.Mgz = Nothing
        Me.Xl_LookupMgz1.Name = "Xl_LookupMgz1"
        Me.Xl_LookupMgz1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupMgz1.ReadOnlyLookup = False
        Me.Xl_LookupMgz1.Size = New System.Drawing.Size(268, 20)
        Me.Xl_LookupMgz1.TabIndex = 6
        Me.Xl_LookupMgz1.Value = Nothing
        '
        'Xl_ProductSku1
        '
        Me.Xl_ProductSku1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ProductSku1.Location = New System.Drawing.Point(415, 1)
        Me.Xl_ProductSku1.Name = "Xl_ProductSku1"
        Me.Xl_ProductSku1.Size = New System.Drawing.Size(275, 20)
        Me.Xl_ProductSku1.TabIndex = 8
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(692, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'DescatalogatsToolStripMenuItem
        '
        Me.DescatalogatsToolStripMenuItem.Name = "DescatalogatsToolStripMenuItem"
        Me.DescatalogatsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DescatalogatsToolStripMenuItem.Text = "Descatalogats"
        '
        'Frm_Cataleg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 264)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Xl_ProductSku1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_LookupMgz1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Frm_Cataleg"
        Me.Text = "Catàleg"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_ProductSkusExtended1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_ProductBrands1 As Xl_ProductBrands
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_ProductCategories1 As Xl_ProductCategories
    Friend WithEvents Xl_ProductSkusExtended1 As Xl_ProductSkusExtended
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents IncentiusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaF5ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StocksRealsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_LookupMgz1 As Xl_LookupMgz
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_ProductSku1 As Xl_ProductSku
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ImportarExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DescatalogatsToolStripMenuItem As ToolStripMenuItem
End Class
