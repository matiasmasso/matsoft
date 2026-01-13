<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Leads2
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarCsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CheckedGuidNomsCountries = New Winforms.Xl_CheckedGuidNoms()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CheckedGuidNomsZonas = New Winforms.Xl_CheckedGuidNoms()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CheckedGuidNomsLocations = New Winforms.Xl_CheckedGuidNoms()
        Me.Xl_CheckedGuidNomsLeads = New Winforms.Xl_CheckedGuidNoms()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_CheckedGuidNomsCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.Xl_CheckedGuidNomsZonas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.Xl_CheckedGuidNomsLocations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_CheckedGuidNomsLeads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 427)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(998, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer2)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 427)
        Me.Panel1.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(998, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarCsvToolStripMenuItem, Me.ExportarExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExportarCsvToolStripMenuItem
        '
        Me.ExportarCsvToolStripMenuItem.Name = "ExportarCsvToolStripMenuItem"
        Me.ExportarCsvToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ExportarCsvToolStripMenuItem.Text = "Exportar Csv"
        '
        'ExportarExcelToolStripMenuItem
        '
        Me.ExportarExcelToolStripMenuItem.Name = "ExportarExcelToolStripMenuItem"
        Me.ExportarExcelToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ExportarExcelToolStripMenuItem.Text = "Exportar Excel"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_CheckedGuidNomsCountries)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(998, 403)
        Me.SplitContainer2.SplitterDistance = 196
        Me.SplitContainer2.TabIndex = 3
        '
        'Xl_CheckedGuidNomsCountries
        '
        Me.Xl_CheckedGuidNomsCountries.AllowUserToAddRows = False
        Me.Xl_CheckedGuidNomsCountries.AllowUserToDeleteRows = False
        Me.Xl_CheckedGuidNomsCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsCountries.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsCountries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsCountries.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsCountries.MouseIsDown = False
        Me.Xl_CheckedGuidNomsCountries.Name = "Xl_CheckedGuidNomsCountries"
        Me.Xl_CheckedGuidNomsCountries.ReadOnly = True
        Me.Xl_CheckedGuidNomsCountries.Size = New System.Drawing.Size(196, 403)
        Me.Xl_CheckedGuidNomsCountries.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Xl_CheckedGuidNomsZonas)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(798, 403)
        Me.SplitContainer3.SplitterDistance = 265
        Me.SplitContainer3.TabIndex = 0
        '
        'Xl_CheckedGuidNomsZonas
        '
        Me.Xl_CheckedGuidNomsZonas.AllowUserToAddRows = False
        Me.Xl_CheckedGuidNomsZonas.AllowUserToDeleteRows = False
        Me.Xl_CheckedGuidNomsZonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsZonas.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsZonas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsZonas.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsZonas.MouseIsDown = False
        Me.Xl_CheckedGuidNomsZonas.Name = "Xl_CheckedGuidNomsZonas"
        Me.Xl_CheckedGuidNomsZonas.ReadOnly = True
        Me.Xl_CheckedGuidNomsZonas.Size = New System.Drawing.Size(265, 403)
        Me.Xl_CheckedGuidNomsZonas.TabIndex = 1
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.Xl_CheckedGuidNomsLocations)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.Xl_CheckedGuidNomsLeads)
        Me.SplitContainer4.Size = New System.Drawing.Size(529, 403)
        Me.SplitContainer4.SplitterDistance = 176
        Me.SplitContainer4.TabIndex = 0
        '
        'Xl_CheckedGuidNomsLocations
        '
        Me.Xl_CheckedGuidNomsLocations.AllowUserToAddRows = False
        Me.Xl_CheckedGuidNomsLocations.AllowUserToDeleteRows = False
        Me.Xl_CheckedGuidNomsLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsLocations.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsLocations.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsLocations.MouseIsDown = False
        Me.Xl_CheckedGuidNomsLocations.Name = "Xl_CheckedGuidNomsLocations"
        Me.Xl_CheckedGuidNomsLocations.ReadOnly = True
        Me.Xl_CheckedGuidNomsLocations.Size = New System.Drawing.Size(176, 403)
        Me.Xl_CheckedGuidNomsLocations.TabIndex = 2
        '
        'Xl_CheckedGuidNomsLeads
        '
        Me.Xl_CheckedGuidNomsLeads.AllowUserToAddRows = False
        Me.Xl_CheckedGuidNomsLeads.AllowUserToDeleteRows = False
        Me.Xl_CheckedGuidNomsLeads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CheckedGuidNomsLeads.DisplayObsolets = False
        Me.Xl_CheckedGuidNomsLeads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedGuidNomsLeads.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedGuidNomsLeads.MouseIsDown = False
        Me.Xl_CheckedGuidNomsLeads.Name = "Xl_CheckedGuidNomsLeads"
        Me.Xl_CheckedGuidNomsLeads.ReadOnly = True
        Me.Xl_CheckedGuidNomsLeads.Size = New System.Drawing.Size(349, 403)
        Me.Xl_CheckedGuidNomsLeads.TabIndex = 3
        '
        'Frm_Leads2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Leads2"
        Me.Text = "Frm_Leads2"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_CheckedGuidNomsCountries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.Xl_CheckedGuidNomsZonas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.Xl_CheckedGuidNomsLocations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_CheckedGuidNomsLeads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarCsvToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_CheckedGuidNomsCountries As Xl_CheckedGuidNoms
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents Xl_CheckedGuidNomsZonas As Xl_CheckedGuidNoms
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents Xl_CheckedGuidNomsLocations As Xl_CheckedGuidNoms
    Friend WithEvents Xl_CheckedGuidNomsLeads As Xl_CheckedGuidNoms
End Class
