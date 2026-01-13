<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LeadAreas
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_LeadAreasCountry = New Winforms.Xl_LeadAreas()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_LeadAreasZona = New Winforms.Xl_LeadAreas()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Xl_LeadAreasLocation = New Winforms.Xl_LeadAreas()
        Me.Xl_leadsChecked1 = New Winforms.Xl_leadsChecked()
        Me.PictureBoxSaveFile = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesarCsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.LabelLeadsCount = New System.Windows.Forms.Label()
        Me.CheckBoxHideShops = New System.Windows.Forms.CheckBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_LeadAreasCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_LeadAreasZona, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.Xl_LeadAreasLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_leadsChecked1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxSaveFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 56)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_LeadAreasCountry)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(891, 374)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.Visible = False
        '
        'Xl_LeadAreasCountry
        '
        Me.Xl_LeadAreasCountry.AllowUserToAddRows = False
        Me.Xl_LeadAreasCountry.AllowUserToDeleteRows = False
        Me.Xl_LeadAreasCountry.AllValues = Nothing
        Me.Xl_LeadAreasCountry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LeadAreasCountry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LeadAreasCountry.FilteredValues = Nothing
        Me.Xl_LeadAreasCountry.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LeadAreasCountry.Name = "Xl_LeadAreasCountry"
        Me.Xl_LeadAreasCountry.ParentLeadArea = Nothing
        Me.Xl_LeadAreasCountry.ReadOnly = True
        Me.Xl_LeadAreasCountry.Size = New System.Drawing.Size(200, 374)
        Me.Xl_LeadAreasCountry.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_LeadAreasZona)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(687, 374)
        Me.SplitContainer2.SplitterDistance = 200
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_LeadAreasZona
        '
        Me.Xl_LeadAreasZona.AllowUserToAddRows = False
        Me.Xl_LeadAreasZona.AllowUserToDeleteRows = False
        Me.Xl_LeadAreasZona.AllValues = Nothing
        Me.Xl_LeadAreasZona.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LeadAreasZona.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LeadAreasZona.FilteredValues = Nothing
        Me.Xl_LeadAreasZona.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LeadAreasZona.Name = "Xl_LeadAreasZona"
        Me.Xl_LeadAreasZona.ParentLeadArea = Nothing
        Me.Xl_LeadAreasZona.ReadOnly = True
        Me.Xl_LeadAreasZona.Size = New System.Drawing.Size(200, 374)
        Me.Xl_LeadAreasZona.TabIndex = 1
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Xl_LeadAreasLocation)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Xl_leadsChecked1)
        Me.SplitContainer3.Size = New System.Drawing.Size(483, 374)
        Me.SplitContainer3.SplitterDistance = 200
        Me.SplitContainer3.TabIndex = 1
        '
        'Xl_LeadAreasLocation
        '
        Me.Xl_LeadAreasLocation.AllowUserToAddRows = False
        Me.Xl_LeadAreasLocation.AllowUserToDeleteRows = False
        Me.Xl_LeadAreasLocation.AllValues = Nothing
        Me.Xl_LeadAreasLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LeadAreasLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LeadAreasLocation.FilteredValues = Nothing
        Me.Xl_LeadAreasLocation.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LeadAreasLocation.Name = "Xl_LeadAreasLocation"
        Me.Xl_LeadAreasLocation.ParentLeadArea = Nothing
        Me.Xl_LeadAreasLocation.ReadOnly = True
        Me.Xl_LeadAreasLocation.Size = New System.Drawing.Size(200, 374)
        Me.Xl_LeadAreasLocation.TabIndex = 0
        '
        'Xl_leadsChecked1
        '
        Me.Xl_leadsChecked1.AllowUserToAddRows = False
        Me.Xl_leadsChecked1.AllowUserToDeleteRows = False
        Me.Xl_leadsChecked1.AllValues = Nothing
        Me.Xl_leadsChecked1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_leadsChecked1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_leadsChecked1.Filter = Nothing
        Me.Xl_leadsChecked1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_leadsChecked1.Name = "Xl_leadsChecked1"
        Me.Xl_leadsChecked1.ReadOnly = True
        Me.Xl_leadsChecked1.Size = New System.Drawing.Size(279, 374)
        Me.Xl_leadsChecked1.TabIndex = 0
        '
        'PictureBoxSaveFile
        '
        Me.PictureBoxSaveFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxSaveFile.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxSaveFile.Image = Global.Winforms.My.Resources.Resources.SaveFilex48disabled
        Me.PictureBoxSaveFile.Location = New System.Drawing.Point(845, 2)
        Me.PictureBoxSaveFile.Name = "PictureBoxSaveFile"
        Me.PictureBoxSaveFile.Size = New System.Drawing.Size(48, 48)
        Me.PictureBoxSaveFile.TabIndex = 2
        Me.PictureBoxSaveFile.TabStop = False
        Me.PictureBoxSaveFile.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(895, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DesarCsvToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        Me.ArxiuToolStripMenuItem.Visible = False
        '
        'DesarCsvToolStripMenuItem
        '
        Me.DesarCsvToolStripMenuItem.Name = "DesarCsvToolStripMenuItem"
        Me.DesarCsvToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.DesarCsvToolStripMenuItem.Text = "Desar Csv"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(2, 29)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(1)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(891, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 4
        '
        'LabelLeadsCount
        '
        Me.LabelLeadsCount.AutoSize = True
        Me.LabelLeadsCount.Location = New System.Drawing.Point(322, 8)
        Me.LabelLeadsCount.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelLeadsCount.Name = "LabelLeadsCount"
        Me.LabelLeadsCount.Size = New System.Drawing.Size(59, 13)
        Me.LabelLeadsCount.TabIndex = 5
        Me.LabelLeadsCount.Text = "Total leads"
        Me.LabelLeadsCount.Visible = False
        '
        'CheckBoxHideShops
        '
        Me.CheckBoxHideShops.AutoSize = True
        Me.CheckBoxHideShops.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHideShops.Checked = True
        Me.CheckBoxHideShops.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideShops.Location = New System.Drawing.Point(172, 7)
        Me.CheckBoxHideShops.Name = "CheckBoxHideShops"
        Me.CheckBoxHideShops.Size = New System.Drawing.Size(131, 17)
        Me.CheckBoxHideShops.TabIndex = 6
        Me.CheckBoxHideShops.Text = "Descarta els botiguers"
        Me.CheckBoxHideShops.UseVisualStyleBackColor = True
        Me.CheckBoxHideShops.Visible = False
        '
        'Frm_LeadAreas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 434)
        Me.Controls.Add(Me.CheckBoxHideShops)
        Me.Controls.Add(Me.LabelLeadsCount)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.PictureBoxSaveFile)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_LeadAreas"
        Me.Text = "Sel.lecció de leads per circular a consumidors"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_LeadAreasCountry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_LeadAreasZona, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.Xl_LeadAreasLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_leadsChecked1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxSaveFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_LeadAreasLocation As Xl_LeadAreas
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_LeadAreasCountry As Xl_LeadAreas
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_LeadAreasZona As Xl_LeadAreas
    Friend WithEvents PictureBoxSaveFile As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DesarCsvToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelLeadsCount As Label
    Friend WithEvents CheckBoxHideShops As CheckBox
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents Xl_leadsChecked1 As Xl_leadsChecked
End Class
