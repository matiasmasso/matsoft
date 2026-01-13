<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LeadsPro
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_LeadsPro))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarCsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TornaACarregarElsProfessionalsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TornaACarregarElsConsumidorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerPro = New System.Windows.Forms.SplitContainer()
        Me.LabelCountPro = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ProgressBarPro = New System.Windows.Forms.ProgressBar()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LabelCountConsumer = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.SplitContainerConsumer = New System.Windows.Forms.SplitContainer()
        Me.ProgressBarConsumer = New System.Windows.Forms.ProgressBar()
        Me.Xl_ContactClassesCheckedTreePro = New Mat.Net.Xl_ContactClassesCheckedTree()
        Me.Xl_CheckedLeadAreasPro = New Mat.Net.Xl_CheckedLeadAreas()
        Me.Xl_CheckedGuidNomsPro = New Mat.Net.Xl_CheckedGuidNoms()
        Me.Xl_CheckedLeadAreasConsumer = New Mat.Net.Xl_CheckedLeadAreas()
        Me.Xl_CheckedGuidNomsConsumer = New Mat.Net.Xl_CheckedGuidNoms()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainerPro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerPro.Panel1.SuspendLayout()
        Me.SplitContainerPro.Panel2.SuspendLayout()
        Me.SplitContainerPro.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.SplitContainerConsumer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerConsumer.Panel1.SuspendLayout()
        Me.SplitContainerConsumer.Panel2.SuspendLayout()
        Me.SplitContainerConsumer.SuspendLayout()
        CType(Me.Xl_CheckedGuidNomsPro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_CheckedGuidNomsConsumer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarCsvToolStripMenuItem, Me.ExportarExcelToolStripMenuItem, Me.TornaACarregarElsProfessionalsToolStripMenuItem, Me.TornaACarregarElsConsumidorsToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExportarCsvToolStripMenuItem
        '
        Me.ExportarCsvToolStripMenuItem.Name = "ExportarCsvToolStripMenuItem"
        Me.ExportarCsvToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ExportarCsvToolStripMenuItem.Text = "Exportar Csv"
        '
        'ExportarExcelToolStripMenuItem
        '
        Me.ExportarExcelToolStripMenuItem.Name = "ExportarExcelToolStripMenuItem"
        Me.ExportarExcelToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ExportarExcelToolStripMenuItem.Text = "Exportar Excel"
        '
        'TornaACarregarElsProfessionalsToolStripMenuItem
        '
        Me.TornaACarregarElsProfessionalsToolStripMenuItem.Name = "TornaACarregarElsProfessionalsToolStripMenuItem"
        Me.TornaACarregarElsProfessionalsToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.TornaACarregarElsProfessionalsToolStripMenuItem.Text = "Torna a carregar els professionals"
        '
        'TornaACarregarElsConsumidorsToolStripMenuItem
        '
        Me.TornaACarregarElsConsumidorsToolStripMenuItem.Name = "TornaACarregarElsConsumidorsToolStripMenuItem"
        Me.TornaACarregarElsConsumidorsToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.TornaACarregarElsConsumidorsToolStripMenuItem.Text = "Torna a carregar els consumidors"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ContactClassesCheckedTreePro)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainerPro)
        Me.SplitContainer1.Size = New System.Drawing.Size(783, 341)
        Me.SplitContainer1.SplitterDistance = 148
        Me.SplitContainer1.TabIndex = 4
        '
        'SplitContainerPro
        '
        Me.SplitContainerPro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerPro.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerPro.Name = "SplitContainerPro"
        '
        'SplitContainerPro.Panel1
        '
        Me.SplitContainerPro.Panel1.Controls.Add(Me.Xl_CheckedLeadAreasPro)
        '
        'SplitContainerPro.Panel2
        '
        Me.SplitContainerPro.Panel2.Controls.Add(Me.Xl_CheckedGuidNomsPro)
        Me.SplitContainerPro.Size = New System.Drawing.Size(631, 341)
        Me.SplitContainerPro.SplitterDistance = 266
        Me.SplitContainerPro.TabIndex = 3
        '
        'LabelCountPro
        '
        Me.LabelCountPro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCountPro.AutoSize = True
        Me.LabelCountPro.Location = New System.Drawing.Point(668, 7)
        Me.LabelCountPro.Name = "LabelCountPro"
        Me.LabelCountPro.Size = New System.Drawing.Size(62, 13)
        Me.LabelCountPro.TabIndex = 5
        Me.LabelCountPro.Text = "Total leads:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 450)
        Me.Panel1.TabIndex = 4
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(794, 420)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.LabelCountPro)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(786, 394)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Professionals"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.SplitContainer1)
        Me.Panel2.Controls.Add(Me.ProgressBarPro)
        Me.Panel2.Location = New System.Drawing.Point(0, 27)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(783, 364)
        Me.Panel2.TabIndex = 6
        '
        'ProgressBarPro
        '
        Me.ProgressBarPro.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarPro.Location = New System.Drawing.Point(0, 341)
        Me.ProgressBarPro.Name = "ProgressBarPro"
        Me.ProgressBarPro.Size = New System.Drawing.Size(783, 23)
        Me.ProgressBarPro.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarPro.TabIndex = 5
        Me.ProgressBarPro.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.LabelCountConsumer)
        Me.TabPage2.Controls.Add(Me.Panel3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(786, 394)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Consumidors"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'LabelCountConsumer
        '
        Me.LabelCountConsumer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCountConsumer.AutoSize = True
        Me.LabelCountConsumer.Location = New System.Drawing.Point(668, 7)
        Me.LabelCountConsumer.Name = "LabelCountConsumer"
        Me.LabelCountConsumer.Size = New System.Drawing.Size(62, 13)
        Me.LabelCountConsumer.TabIndex = 8
        Me.LabelCountConsumer.Text = "Total leads:"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.SplitContainerConsumer)
        Me.Panel3.Controls.Add(Me.ProgressBarConsumer)
        Me.Panel3.Location = New System.Drawing.Point(0, 27)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(783, 364)
        Me.Panel3.TabIndex = 7
        '
        'SplitContainerConsumer
        '
        Me.SplitContainerConsumer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerConsumer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerConsumer.Name = "SplitContainerConsumer"
        '
        'SplitContainerConsumer.Panel1
        '
        Me.SplitContainerConsumer.Panel1.Controls.Add(Me.Xl_CheckedLeadAreasConsumer)
        '
        'SplitContainerConsumer.Panel2
        '
        Me.SplitContainerConsumer.Panel2.Controls.Add(Me.Xl_CheckedGuidNomsConsumer)
        Me.SplitContainerConsumer.Size = New System.Drawing.Size(783, 341)
        Me.SplitContainerConsumer.SplitterDistance = 233
        Me.SplitContainerConsumer.TabIndex = 4
        '
        'ProgressBarConsumer
        '
        Me.ProgressBarConsumer.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarConsumer.Location = New System.Drawing.Point(0, 341)
        Me.ProgressBarConsumer.Name = "ProgressBarConsumer"
        Me.ProgressBarConsumer.Size = New System.Drawing.Size(783, 23)
        Me.ProgressBarConsumer.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarConsumer.TabIndex = 5
        Me.ProgressBarConsumer.Visible = False
        '
        'Xl_ContactClassesCheckedTreePro
        '
        Me.Xl_ContactClassesCheckedTreePro.CheckBoxes = True
        Me.Xl_ContactClassesCheckedTreePro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ContactClassesCheckedTreePro.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.Xl_ContactClassesCheckedTreePro.ExpandedTags = CType(resources.GetObject("Xl_ContactClassesCheckedTreePro.ExpandedTags"), System.Collections.Generic.List(Of Object))
        Me.Xl_ContactClassesCheckedTreePro.isDirty = False
        Me.Xl_ContactClassesCheckedTreePro.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ContactClassesCheckedTreePro.Name = "Xl_ContactClassesCheckedTreePro"
        Me.Xl_ContactClassesCheckedTreePro.Size = New System.Drawing.Size(148, 341)
        Me.Xl_ContactClassesCheckedTreePro.TabIndex = 0
        '
        'Xl_CheckedLeadAreasPro
        '
        Me.Xl_CheckedLeadAreasPro.CheckBoxes = True
        Me.Xl_CheckedLeadAreasPro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedLeadAreasPro.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.Xl_CheckedLeadAreasPro.ExpandedTags = CType(resources.GetObject("Xl_CheckedLeadAreasPro.ExpandedTags"), System.Collections.Generic.List(Of Object))
        Me.Xl_CheckedLeadAreasPro.isDirty = False
        Me.Xl_CheckedLeadAreasPro.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedLeadAreasPro.Name = "Xl_CheckedLeadAreasPro"
        Me.Xl_CheckedLeadAreasPro.Size = New System.Drawing.Size(266, 341)
        Me.Xl_CheckedLeadAreasPro.TabIndex = 0
        '
        'Xl_CheckedGuidNomsPro
        '
        Me.Xl_CheckedGuidNomsPro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsPro.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsPro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsPro.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsPro.MouseIsDown = False
        Me.Xl_CheckedGuidNomsPro.Name = "Xl_CheckedGuidNomsPro"
        Me.Xl_CheckedGuidNomsPro.Size = New System.Drawing.Size(361, 341)
        Me.Xl_CheckedGuidNomsPro.TabIndex = 0
        '
        'Xl_CheckedLeadAreasConsumer
        '
        Me.Xl_CheckedLeadAreasConsumer.CheckBoxes = True
        Me.Xl_CheckedLeadAreasConsumer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedLeadAreasConsumer.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.Xl_CheckedLeadAreasConsumer.ExpandedTags = CType(resources.GetObject("Xl_CheckedLeadAreasConsumer.ExpandedTags"), System.Collections.Generic.List(Of Object))
        Me.Xl_CheckedLeadAreasConsumer.isDirty = False
        Me.Xl_CheckedLeadAreasConsumer.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedLeadAreasConsumer.Name = "Xl_CheckedLeadAreasConsumer"
        Me.Xl_CheckedLeadAreasConsumer.Size = New System.Drawing.Size(233, 341)
        Me.Xl_CheckedLeadAreasConsumer.TabIndex = 1
        '
        'Xl_CheckedGuidNomsConsumer
        '
        Me.Xl_CheckedGuidNomsConsumer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsConsumer.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsConsumer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsConsumer.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsConsumer.MouseIsDown = False
        Me.Xl_CheckedGuidNomsConsumer.Name = "Xl_CheckedGuidNomsConsumer"
        Me.Xl_CheckedGuidNomsConsumer.Size = New System.Drawing.Size(546, 341)
        Me.Xl_CheckedGuidNomsConsumer.TabIndex = 1
        '
        'Frm_LeadsPro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_LeadsPro"
        Me.Text = "Leads"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainerPro.Panel1.ResumeLayout(False)
        Me.SplitContainerPro.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerPro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerPro.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.SplitContainerConsumer.Panel1.ResumeLayout(False)
        Me.SplitContainerConsumer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerConsumer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerConsumer.ResumeLayout(False)
        CType(Me.Xl_CheckedGuidNomsPro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_CheckedGuidNomsConsumer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarCsvToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ContactClassesCheckedTreePro As Xl_ContactClassesCheckedTree
    Friend WithEvents SplitContainerPro As SplitContainer
    Friend WithEvents Xl_CheckedLeadAreasPro As Xl_CheckedLeadAreas
    Friend WithEvents Xl_CheckedGuidNomsPro As Xl_CheckedGuidNoms
    Friend WithEvents LabelCountPro As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ProgressBarPro As ProgressBar
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents SplitContainerConsumer As SplitContainer
    Friend WithEvents Xl_CheckedLeadAreasConsumer As Xl_CheckedLeadAreas
    Friend WithEvents Xl_CheckedGuidNomsConsumer As Xl_CheckedGuidNoms
    Friend WithEvents ProgressBarConsumer As ProgressBar
    Friend WithEvents LabelCountConsumer As Label
    Friend WithEvents TornaACarregarElsProfessionalsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TornaACarregarElsConsumidorsToolStripMenuItem As ToolStripMenuItem
End Class
