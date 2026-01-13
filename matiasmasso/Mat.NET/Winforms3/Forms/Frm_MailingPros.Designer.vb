<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MailingPros
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
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarCsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CheckBoxBrands = New System.Windows.Forms.CheckBox()
        Me.Xl_Brands_CheckList1 = New Xl_Brands_CheckList()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LabelClientsCount = New System.Windows.Forms.Label()
        Me.Xl_TextboxSearchClients = New Xl_TextboxSearch()
        Me.Xl_leadsCheckedClients = New Xl_leadsChecked()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_leadsCheckedReps = New Xl_leadsChecked()
        Me.LabelGlobalCount = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_DistributionChannels_Checklist1 = New Xl_DistributionChannels_Checklist()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_Brands_CheckList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_leadsCheckedClients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_leadsCheckedReps, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_DistributionChannels_Checklist1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(499, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarCsvToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExportarCsvToolStripMenuItem
        '
        Me.ExportarCsvToolStripMenuItem.Name = "ExportarCsvToolStripMenuItem"
        Me.ExportarCsvToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ExportarCsvToolStripMenuItem.Text = "Exportar Csv"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 42)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(487, 541)
        Me.SplitContainer1.SplitterDistance = 162
        Me.SplitContainer1.TabIndex = 3
        '
        'CheckBoxBrands
        '
        Me.CheckBoxBrands.AutoSize = True
        Me.CheckBoxBrands.Location = New System.Drawing.Point(3, 3)
        Me.CheckBoxBrands.Name = "CheckBoxBrands"
        Me.CheckBoxBrands.Size = New System.Drawing.Size(98, 17)
        Me.CheckBoxBrands.TabIndex = 3
        Me.CheckBoxBrands.Text = "filtrar per marca"
        Me.CheckBoxBrands.UseVisualStyleBackColor = True
        '
        'Xl_Brands_CheckList1
        '
        Me.Xl_Brands_CheckList1.AllowUserToAddRows = False
        Me.Xl_Brands_CheckList1.AllowUserToDeleteRows = False
        Me.Xl_Brands_CheckList1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Brands_CheckList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Brands_CheckList1.Location = New System.Drawing.Point(0, 22)
        Me.Xl_Brands_CheckList1.Name = "Xl_Brands_CheckList1"
        Me.Xl_Brands_CheckList1.ReadOnly = True
        Me.Xl_Brands_CheckList1.Size = New System.Drawing.Size(162, 242)
        Me.Xl_Brands_CheckList1.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(321, 541)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.LabelClientsCount)
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearchClients)
        Me.TabPage2.Controls.Add(Me.Xl_leadsCheckedClients)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(313, 515)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Clients"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'LabelClientsCount
        '
        Me.LabelClientsCount.AutoSize = True
        Me.LabelClientsCount.Location = New System.Drawing.Point(7, 9)
        Me.LabelClientsCount.Name = "LabelClientsCount"
        Me.LabelClientsCount.Size = New System.Drawing.Size(60, 13)
        Me.LabelClientsCount.TabIndex = 3
        Me.LabelClientsCount.Text = "destinataris"
        '
        'Xl_TextboxSearchClients
        '
        Me.Xl_TextboxSearchClients.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchClients.Location = New System.Drawing.Point(124, 6)
        Me.Xl_TextboxSearchClients.Name = "Xl_TextboxSearchClients"
        Me.Xl_TextboxSearchClients.Size = New System.Drawing.Size(186, 20)
        Me.Xl_TextboxSearchClients.TabIndex = 2
        '
        'Xl_leadsCheckedClients
        '
        Me.Xl_leadsCheckedClients.AllowUserToAddRows = False
        Me.Xl_leadsCheckedClients.AllowUserToDeleteRows = False
        Me.Xl_leadsCheckedClients.AllValues = Nothing
        Me.Xl_leadsCheckedClients.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_leadsCheckedClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_leadsCheckedClients.Filter = Nothing
        Me.Xl_leadsCheckedClients.Location = New System.Drawing.Point(3, 32)
        Me.Xl_leadsCheckedClients.Name = "Xl_leadsCheckedClients"
        Me.Xl_leadsCheckedClients.ReadOnly = True
        Me.Xl_leadsCheckedClients.Size = New System.Drawing.Size(307, 479)
        Me.Xl_leadsCheckedClients.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_leadsCheckedReps)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(313, 515)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "Representants"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_leadsCheckedReps
        '
        Me.Xl_leadsCheckedReps.AllowUserToAddRows = False
        Me.Xl_leadsCheckedReps.AllowUserToDeleteRows = False
        Me.Xl_leadsCheckedReps.AllValues = Nothing
        Me.Xl_leadsCheckedReps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_leadsCheckedReps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_leadsCheckedReps.Filter = Nothing
        Me.Xl_leadsCheckedReps.Location = New System.Drawing.Point(3, 3)
        Me.Xl_leadsCheckedReps.Name = "Xl_leadsCheckedReps"
        Me.Xl_leadsCheckedReps.ReadOnly = True
        Me.Xl_leadsCheckedReps.Size = New System.Drawing.Size(307, 509)
        Me.Xl_leadsCheckedReps.TabIndex = 2
        '
        'LabelGlobalCount
        '
        Me.LabelGlobalCount.AutoSize = True
        Me.LabelGlobalCount.Location = New System.Drawing.Point(179, 5)
        Me.LabelGlobalCount.Name = "LabelGlobalCount"
        Me.LabelGlobalCount.Size = New System.Drawing.Size(87, 13)
        Me.LabelGlobalCount.TabIndex = 4
        Me.LabelGlobalCount.Text = "Total destinataris"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_DistributionChannels_Checklist1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Brands_CheckList1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.CheckBoxBrands)
        Me.SplitContainer2.Size = New System.Drawing.Size(162, 541)
        Me.SplitContainer2.SplitterDistance = 270
        Me.SplitContainer2.TabIndex = 4
        '
        'Xl_DistributionChannels_Checklist1
        '
        Me.Xl_DistributionChannels_Checklist1.AllowUserToAddRows = False
        Me.Xl_DistributionChannels_Checklist1.AllowUserToDeleteRows = False
        Me.Xl_DistributionChannels_Checklist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DistributionChannels_Checklist1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_DistributionChannels_Checklist1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DistributionChannels_Checklist1.Name = "Xl_DistributionChannels_Checklist1"
        Me.Xl_DistributionChannels_Checklist1.ReadOnly = True
        Me.Xl_DistributionChannels_Checklist1.Size = New System.Drawing.Size(162, 270)
        Me.Xl_DistributionChannels_Checklist1.TabIndex = 1
        '
        'Frm_MailingPros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 587)
        Me.Controls.Add(Me.LabelGlobalCount)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_MailingPros"
        Me.Text = "Mailing a professionals"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_Brands_CheckList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.Xl_leadsCheckedClients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_leadsCheckedReps, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_DistributionChannels_Checklist1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarCsvToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_Brands_CheckList1 As Xl_Brands_CheckList
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents CheckBoxBrands As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_leadsCheckedClients As Xl_leadsChecked
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents LabelGlobalCount As Label
    Friend WithEvents Xl_leadsCheckedReps As Xl_leadsChecked
    Friend WithEvents Xl_TextboxSearchClients As Xl_TextboxSearch
    Friend WithEvents LabelClientsCount As Label
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_DistributionChannels_Checklist1 As Xl_DistributionChannels_Checklist
End Class
