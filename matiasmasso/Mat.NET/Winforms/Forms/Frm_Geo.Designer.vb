<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Geo
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
        Me.SplitContainerCountries = New System.Windows.Forms.SplitContainer()
        Me.Xl_Countries1 = New Winforms.Xl_Countries()
        Me.SplitContainerZonas = New System.Windows.Forms.SplitContainer()
        Me.Xl_Zonas1 = New Winforms.Xl_Zonas()
        Me.SplitContainerLocations = New System.Windows.Forms.SplitContainer()
        Me.Xl_Locations1 = New Winforms.Xl_Locations()
        Me.Xl_Zips1 = New Winforms.Xl_Zips()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GeonamesExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerCountries.Panel1.SuspendLayout()
        Me.SplitContainerCountries.Panel2.SuspendLayout()
        Me.SplitContainerCountries.SuspendLayout()
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerZonas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerZonas.Panel1.SuspendLayout()
        Me.SplitContainerZonas.Panel2.SuspendLayout()
        Me.SplitContainerZonas.SuspendLayout()
        CType(Me.Xl_Zonas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerLocations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerLocations.Panel1.SuspendLayout()
        Me.SplitContainerLocations.Panel2.SuspendLayout()
        Me.SplitContainerLocations.SuspendLayout()
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Zips1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerCountries
        '
        Me.SplitContainerCountries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerCountries.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerCountries.Name = "SplitContainerCountries"
        '
        'SplitContainerCountries.Panel1
        '
        Me.SplitContainerCountries.Panel1.Controls.Add(Me.Xl_Countries1)
        '
        'SplitContainerCountries.Panel2
        '
        Me.SplitContainerCountries.Panel2.Controls.Add(Me.SplitContainerZonas)
        Me.SplitContainerCountries.Size = New System.Drawing.Size(859, 225)
        Me.SplitContainerCountries.SplitterDistance = 199
        Me.SplitContainerCountries.TabIndex = 0
        '
        'Xl_Countries1
        '
        Me.Xl_Countries1.DisplayObsolets = False
        Me.Xl_Countries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Countries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries1.MouseIsDown = False
        Me.Xl_Countries1.Name = "Xl_Countries1"
        Me.Xl_Countries1.Size = New System.Drawing.Size(199, 225)
        Me.Xl_Countries1.TabIndex = 0
        '
        'SplitContainerZonas
        '
        Me.SplitContainerZonas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerZonas.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerZonas.Name = "SplitContainerZonas"
        '
        'SplitContainerZonas.Panel1
        '
        Me.SplitContainerZonas.Panel1.Controls.Add(Me.Xl_Zonas1)
        '
        'SplitContainerZonas.Panel2
        '
        Me.SplitContainerZonas.Panel2.Controls.Add(Me.SplitContainerLocations)
        Me.SplitContainerZonas.Size = New System.Drawing.Size(656, 225)
        Me.SplitContainerZonas.SplitterDistance = 259
        Me.SplitContainerZonas.TabIndex = 0
        '
        'Xl_Zonas1
        '
        Me.Xl_Zonas1.DisplayObsolets = False
        Me.Xl_Zonas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zonas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Zonas1.MouseIsDown = False
        Me.Xl_Zonas1.Name = "Xl_Zonas1"
        Me.Xl_Zonas1.Size = New System.Drawing.Size(259, 225)
        Me.Xl_Zonas1.TabIndex = 0
        '
        'SplitContainerLocations
        '
        Me.SplitContainerLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerLocations.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerLocations.IsSplitterFixed = True
        Me.SplitContainerLocations.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerLocations.Name = "SplitContainerLocations"
        '
        'SplitContainerLocations.Panel1
        '
        Me.SplitContainerLocations.Panel1.Controls.Add(Me.Xl_Locations1)
        '
        'SplitContainerLocations.Panel2
        '
        Me.SplitContainerLocations.Panel2.Controls.Add(Me.Xl_Zips1)
        Me.SplitContainerLocations.Size = New System.Drawing.Size(393, 225)
        Me.SplitContainerLocations.SplitterDistance = 320
        Me.SplitContainerLocations.TabIndex = 1
        '
        'Xl_Locations1
        '
        Me.Xl_Locations1.AllowUserToResizeColumns = False
        Me.Xl_Locations1.DisplayObsolets = False
        Me.Xl_Locations1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Locations1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Locations1.MouseIsDown = False
        Me.Xl_Locations1.Name = "Xl_Locations1"
        Me.Xl_Locations1.Size = New System.Drawing.Size(320, 225)
        Me.Xl_Locations1.TabIndex = 0
        '
        'Xl_Zips1
        '
        Me.Xl_Zips1.DisplayObsolets = False
        Me.Xl_Zips1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Zips1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Zips1.MouseIsDown = False
        Me.Xl_Zips1.Name = "Xl_Zips1"
        Me.Xl_Zips1.Size = New System.Drawing.Size(69, 225)
        Me.Xl_Zips1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainerCountries)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(2, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(859, 248)
        Me.Panel1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 225)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(859, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        Me.ProgressBar1.Visible = False
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(712, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(862, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GeonamesExcelToolStripMenuItem})
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ImportarToolStripMenuItem.Text = "Importar"
        '
        'GeonamesExcelToolStripMenuItem
        '
        Me.GeonamesExcelToolStripMenuItem.Name = "GeonamesExcelToolStripMenuItem"
        Me.GeonamesExcelToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.GeonamesExcelToolStripMenuItem.Text = "Excel de geonames.org"
        '
        'Frm_Geo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 279)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Geo"
        Me.Text = "Poblacions"
        Me.SplitContainerCountries.Panel1.ResumeLayout(False)
        Me.SplitContainerCountries.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerCountries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerCountries.ResumeLayout(False)
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerZonas.Panel1.ResumeLayout(False)
        Me.SplitContainerZonas.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerZonas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerZonas.ResumeLayout(False)
        CType(Me.Xl_Zonas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLocations.Panel1.ResumeLayout(False)
        Me.SplitContainerLocations.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerLocations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLocations.ResumeLayout(False)
        CType(Me.Xl_Locations1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Zips1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainerCountries As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Countries1 As Winforms.Xl_Countries
    Friend WithEvents SplitContainerZonas As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Zonas1 As Winforms.Xl_Zonas
    Friend WithEvents Xl_Locations1 As Winforms.Xl_Locations
    Friend WithEvents SplitContainerLocations As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Zips1 As Winforms.Xl_Zips
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GeonamesExcelToolStripMenuItem As ToolStripMenuItem
End Class
