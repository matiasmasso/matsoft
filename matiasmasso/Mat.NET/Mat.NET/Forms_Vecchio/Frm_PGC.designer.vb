<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PGC
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
        Me.PlanContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PlanToolStripMenuItemZoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlanToolStripMenuItemAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlanToolStripMenuItemDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewCta = New System.Windows.Forms.DataGridView()
        Me.CtasContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CtasToolStripMenuItemZoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.CtasToolStripMenuItemAddNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.CtasToolStripMenuItemAddGrup = New System.Windows.Forms.ToolStripMenuItem()
        Me.CtasToolStripMenuItemAddSubgrup = New System.Windows.Forms.ToolStripMenuItem()
        Me.CtasToolStripMenuItemAddCta = New System.Windows.Forms.ToolStripMenuItem()
        Me.CtasToolStripMenuItemDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCtas = New System.Windows.Forms.TabPage()
        Me.TabPageBal = New System.Windows.Forms.TabPage()
        Me.DataGridViewEpgBal = New System.Windows.Forms.DataGridView()
        Me.EpgContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EpgToolStripMenuItemZoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.EpgToolStripMenuItemAddNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.EpgToolStripMenuItemDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPageExplot = New System.Windows.Forms.TabPage()
        Me.DataGridViewExplot = New System.Windows.Forms.DataGridView()
        Me.TabPageCFlow = New System.Windows.Forms.TabPage()
        Me.DataGridViewCFlow = New System.Windows.Forms.DataGridView()
        Me.ComboBoxFiltre = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPlan = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonSortEpg = New System.Windows.Forms.ToolStripButton()
        Me.Xl_Langs1 = New Xl_Langs_Old()
        Me.PlanContextMenuStrip.SuspendLayout()
        CType(Me.DataGridViewCta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtasContextMenuStrip.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCtas.SuspendLayout()
        Me.TabPageBal.SuspendLayout()
        CType(Me.DataGridViewEpgBal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EpgContextMenuStrip.SuspendLayout()
        Me.TabPageExplot.SuspendLayout()
        CType(Me.DataGridViewExplot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageCFlow.SuspendLayout()
        CType(Me.DataGridViewCFlow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PlanContextMenuStrip
        '
        Me.PlanContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlanToolStripMenuItemZoom, Me.PlanToolStripMenuItemAdd, Me.PlanToolStripMenuItemDel})
        Me.PlanContextMenuStrip.Name = "PlanContextMenuStrip"
        Me.PlanContextMenuStrip.Size = New System.Drawing.Size(118, 70)
        '
        'PlanToolStripMenuItemZoom
        '
        Me.PlanToolStripMenuItemZoom.Enabled = False
        Me.PlanToolStripMenuItemZoom.Image = My.Resources.Resources.prismatics
        Me.PlanToolStripMenuItemZoom.Name = "PlanToolStripMenuItemZoom"
        Me.PlanToolStripMenuItemZoom.Size = New System.Drawing.Size(117, 22)
        Me.PlanToolStripMenuItemZoom.Text = "zoom"
        '
        'PlanToolStripMenuItemAdd
        '
        Me.PlanToolStripMenuItemAdd.Image = My.Resources.Resources.clip
        Me.PlanToolStripMenuItemAdd.Name = "PlanToolStripMenuItemAdd"
        Me.PlanToolStripMenuItemAdd.Size = New System.Drawing.Size(117, 22)
        Me.PlanToolStripMenuItemAdd.Text = "nou pla"
        '
        'PlanToolStripMenuItemDel
        '
        Me.PlanToolStripMenuItemDel.Enabled = False
        Me.PlanToolStripMenuItemDel.Image = My.Resources.Resources.del
        Me.PlanToolStripMenuItemDel.Name = "PlanToolStripMenuItemDel"
        Me.PlanToolStripMenuItemDel.Size = New System.Drawing.Size(117, 22)
        Me.PlanToolStripMenuItemDel.Text = "eliminar"
        '
        'DataGridViewCta
        '
        Me.DataGridViewCta.AllowUserToAddRows = False
        Me.DataGridViewCta.AllowUserToDeleteRows = False
        Me.DataGridViewCta.AllowUserToResizeColumns = False
        Me.DataGridViewCta.AllowUserToResizeRows = False
        Me.DataGridViewCta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCta.ContextMenuStrip = Me.CtasContextMenuStrip
        Me.DataGridViewCta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCta.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewCta.Name = "DataGridViewCta"
        Me.DataGridViewCta.ReadOnly = True
        Me.DataGridViewCta.Size = New System.Drawing.Size(569, 374)
        Me.DataGridViewCta.TabIndex = 1
        '
        'CtasContextMenuStrip
        '
        Me.CtasContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CtasToolStripMenuItemZoom, Me.CtasToolStripMenuItemAddNew, Me.CtasToolStripMenuItemDel})
        Me.CtasContextMenuStrip.Name = "PlanContextMenuStrip"
        Me.CtasContextMenuStrip.Size = New System.Drawing.Size(118, 70)
        '
        'CtasToolStripMenuItemZoom
        '
        Me.CtasToolStripMenuItemZoom.Enabled = False
        Me.CtasToolStripMenuItemZoom.Image = My.Resources.Resources.prismatics
        Me.CtasToolStripMenuItemZoom.Name = "CtasToolStripMenuItemZoom"
        Me.CtasToolStripMenuItemZoom.Size = New System.Drawing.Size(117, 22)
        Me.CtasToolStripMenuItemZoom.Text = "zoom"
        '
        'CtasToolStripMenuItemAddNew
        '
        Me.CtasToolStripMenuItemAddNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CtasToolStripMenuItemAddGrup, Me.CtasToolStripMenuItemAddSubgrup, Me.CtasToolStripMenuItemAddCta})
        Me.CtasToolStripMenuItemAddNew.Image = My.Resources.Resources.clip
        Me.CtasToolStripMenuItemAddNew.Name = "CtasToolStripMenuItemAddNew"
        Me.CtasToolStripMenuItemAddNew.Size = New System.Drawing.Size(117, 22)
        Me.CtasToolStripMenuItemAddNew.Text = "nou..."
        '
        'CtasToolStripMenuItemAddGrup
        '
        Me.CtasToolStripMenuItemAddGrup.Name = "CtasToolStripMenuItemAddGrup"
        Me.CtasToolStripMenuItemAddGrup.Size = New System.Drawing.Size(119, 22)
        Me.CtasToolStripMenuItemAddGrup.Text = "Grup"
        '
        'CtasToolStripMenuItemAddSubgrup
        '
        Me.CtasToolStripMenuItemAddSubgrup.Name = "CtasToolStripMenuItemAddSubgrup"
        Me.CtasToolStripMenuItemAddSubgrup.Size = New System.Drawing.Size(119, 22)
        Me.CtasToolStripMenuItemAddSubgrup.Text = "Subgrup"
        '
        'CtasToolStripMenuItemAddCta
        '
        Me.CtasToolStripMenuItemAddCta.Name = "CtasToolStripMenuItemAddCta"
        Me.CtasToolStripMenuItemAddCta.Size = New System.Drawing.Size(119, 22)
        Me.CtasToolStripMenuItemAddCta.Text = "Compte"
        '
        'CtasToolStripMenuItemDel
        '
        Me.CtasToolStripMenuItemDel.Enabled = False
        Me.CtasToolStripMenuItemDel.Image = My.Resources.Resources.del
        Me.CtasToolStripMenuItemDel.Name = "CtasToolStripMenuItemDel"
        Me.CtasToolStripMenuItemDel.Size = New System.Drawing.Size(117, 22)
        Me.CtasToolStripMenuItemDel.Text = "eliminar"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageCtas)
        Me.TabControl1.Controls.Add(Me.TabPageBal)
        Me.TabControl1.Controls.Add(Me.TabPageExplot)
        Me.TabControl1.Controls.Add(Me.TabPageCFlow)
        Me.TabControl1.Location = New System.Drawing.Point(0, 31)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(583, 406)
        Me.TabControl1.TabIndex = 2
        '
        'TabPageCtas
        '
        Me.TabPageCtas.Controls.Add(Me.DataGridViewCta)
        Me.TabPageCtas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCtas.Name = "TabPageCtas"
        Me.TabPageCtas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCtas.Size = New System.Drawing.Size(575, 380)
        Me.TabPageCtas.TabIndex = 0
        Me.TabPageCtas.Text = "QUADRE DE COMPTES"
        Me.TabPageCtas.UseVisualStyleBackColor = True
        '
        'TabPageBal
        '
        Me.TabPageBal.Controls.Add(Me.DataGridViewEpgBal)
        Me.TabPageBal.Location = New System.Drawing.Point(4, 22)
        Me.TabPageBal.Name = "TabPageBal"
        Me.TabPageBal.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageBal.Size = New System.Drawing.Size(575, 380)
        Me.TabPageBal.TabIndex = 1
        Me.TabPageBal.Text = "BALANÇ"
        Me.TabPageBal.UseVisualStyleBackColor = True
        '
        'DataGridViewEpgBal
        '
        Me.DataGridViewEpgBal.AllowUserToAddRows = False
        Me.DataGridViewEpgBal.AllowUserToDeleteRows = False
        Me.DataGridViewEpgBal.AllowUserToResizeColumns = False
        Me.DataGridViewEpgBal.AllowUserToResizeRows = False
        Me.DataGridViewEpgBal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEpgBal.ContextMenuStrip = Me.EpgContextMenuStrip
        Me.DataGridViewEpgBal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewEpgBal.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewEpgBal.Name = "DataGridViewEpgBal"
        Me.DataGridViewEpgBal.ReadOnly = True
        Me.DataGridViewEpgBal.Size = New System.Drawing.Size(569, 374)
        Me.DataGridViewEpgBal.TabIndex = 2
        '
        'EpgContextMenuStrip
        '
        Me.EpgContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EpgToolStripMenuItemZoom, Me.EpgToolStripMenuItemAddNew, Me.EpgToolStripMenuItemDel})
        Me.EpgContextMenuStrip.Name = "PlanContextMenuStrip"
        Me.EpgContextMenuStrip.Size = New System.Drawing.Size(118, 70)
        '
        'EpgToolStripMenuItemZoom
        '
        Me.EpgToolStripMenuItemZoom.Enabled = False
        Me.EpgToolStripMenuItemZoom.Image = My.Resources.Resources.prismatics
        Me.EpgToolStripMenuItemZoom.Name = "EpgToolStripMenuItemZoom"
        Me.EpgToolStripMenuItemZoom.Size = New System.Drawing.Size(117, 22)
        Me.EpgToolStripMenuItemZoom.Text = "zoom"
        '
        'EpgToolStripMenuItemAddNew
        '
        Me.EpgToolStripMenuItemAddNew.Image = My.Resources.Resources.clip
        Me.EpgToolStripMenuItemAddNew.Name = "EpgToolStripMenuItemAddNew"
        Me.EpgToolStripMenuItemAddNew.Size = New System.Drawing.Size(117, 22)
        Me.EpgToolStripMenuItemAddNew.Text = "nou..."
        '
        'EpgToolStripMenuItemDel
        '
        Me.EpgToolStripMenuItemDel.Enabled = False
        Me.EpgToolStripMenuItemDel.Image = My.Resources.Resources.del
        Me.EpgToolStripMenuItemDel.Name = "EpgToolStripMenuItemDel"
        Me.EpgToolStripMenuItemDel.Size = New System.Drawing.Size(117, 22)
        Me.EpgToolStripMenuItemDel.Text = "eliminar"
        '
        'TabPageExplot
        '
        Me.TabPageExplot.Controls.Add(Me.DataGridViewExplot)
        Me.TabPageExplot.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExplot.Name = "TabPageExplot"
        Me.TabPageExplot.Size = New System.Drawing.Size(575, 380)
        Me.TabPageExplot.TabIndex = 2
        Me.TabPageExplot.Text = "EXPLOTACIO"
        Me.TabPageExplot.UseVisualStyleBackColor = True
        '
        'DataGridViewExplot
        '
        Me.DataGridViewExplot.AllowUserToAddRows = False
        Me.DataGridViewExplot.AllowUserToDeleteRows = False
        Me.DataGridViewExplot.AllowUserToResizeColumns = False
        Me.DataGridViewExplot.AllowUserToResizeRows = False
        Me.DataGridViewExplot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewExplot.ContextMenuStrip = Me.EpgContextMenuStrip
        Me.DataGridViewExplot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewExplot.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewExplot.Name = "DataGridViewExplot"
        Me.DataGridViewExplot.ReadOnly = True
        Me.DataGridViewExplot.Size = New System.Drawing.Size(575, 380)
        Me.DataGridViewExplot.TabIndex = 3
        '
        'TabPageCFlow
        '
        Me.TabPageCFlow.Controls.Add(Me.DataGridViewCFlow)
        Me.TabPageCFlow.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCFlow.Name = "TabPageCFlow"
        Me.TabPageCFlow.Size = New System.Drawing.Size(575, 380)
        Me.TabPageCFlow.TabIndex = 3
        Me.TabPageCFlow.Text = "CASH FLOW"
        Me.TabPageCFlow.UseVisualStyleBackColor = True
        '
        'DataGridViewCFlow
        '
        Me.DataGridViewCFlow.AllowUserToAddRows = False
        Me.DataGridViewCFlow.AllowUserToDeleteRows = False
        Me.DataGridViewCFlow.AllowUserToResizeColumns = False
        Me.DataGridViewCFlow.AllowUserToResizeRows = False
        Me.DataGridViewCFlow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCFlow.ContextMenuStrip = Me.EpgContextMenuStrip
        Me.DataGridViewCFlow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCFlow.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewCFlow.Name = "DataGridViewCFlow"
        Me.DataGridViewCFlow.ReadOnly = True
        Me.DataGridViewCFlow.Size = New System.Drawing.Size(575, 380)
        Me.DataGridViewCFlow.TabIndex = 3
        '
        'ComboBoxFiltre
        '
        Me.ComboBoxFiltre.FormattingEnabled = True
        Me.ComboBoxFiltre.Items.AddRange(New Object() {"(sense filtrar)", "ocultar grups sense comptes", "ocultar grups amb comptes buides"})
        Me.ComboBoxFiltre.Location = New System.Drawing.Point(272, 4)
        Me.ComboBoxFiltre.Name = "ComboBoxFiltre"
        Me.ComboBoxFiltre.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxFiltre.TabIndex = 2
        '
        'ComboBoxPlan
        '
        Me.ComboBoxPlan.ContextMenuStrip = Me.PlanContextMenuStrip
        Me.ComboBoxPlan.FormattingEnabled = True
        Me.ComboBoxPlan.Location = New System.Drawing.Point(462, 4)
        Me.ComboBoxPlan.Name = "ComboBoxPlan"
        Me.ComboBoxPlan.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxPlan.TabIndex = 3
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonSortEpg})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(587, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonSortEpg
        '
        Me.ToolStripButtonSortEpg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonSortEpg.Image = My.Resources.Resources.rayo
        Me.ToolStripButtonSortEpg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSortEpg.Name = "ToolStripButtonSortEpg"
        Me.ToolStripButtonSortEpg.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonSortEpg.Text = "sort Epg"
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(218, 4)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(48, 21)
        Me.Xl_Langs1.TabIndex = 5
        Me.Xl_Langs1.Tag = "Idioma"
        '
        'Frm_PGC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 439)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.ComboBoxFiltre)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ComboBoxPlan)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_PGC"
        Me.Text = "PLA GENERAL DE COMPTABILITAT"
        Me.PlanContextMenuStrip.ResumeLayout(False)
        CType(Me.DataGridViewCta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtasContextMenuStrip.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCtas.ResumeLayout(False)
        Me.TabPageBal.ResumeLayout(False)
        CType(Me.DataGridViewEpgBal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EpgContextMenuStrip.ResumeLayout(False)
        Me.TabPageExplot.ResumeLayout(False)
        CType(Me.DataGridViewExplot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageCFlow.ResumeLayout(False)
        CType(Me.DataGridViewCFlow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PlanContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PlanToolStripMenuItemZoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlanToolStripMenuItemAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlanToolStripMenuItemDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewCta As System.Windows.Forms.DataGridView
    Friend WithEvents CtasContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CtasToolStripMenuItemZoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CtasToolStripMenuItemAddNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CtasToolStripMenuItemAddGrup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CtasToolStripMenuItemAddSubgrup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CtasToolStripMenuItemAddCta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CtasToolStripMenuItemDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCtas As System.Windows.Forms.TabPage
    Friend WithEvents TabPageBal As System.Windows.Forms.TabPage
    Friend WithEvents TabPageExplot As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCFlow As System.Windows.Forms.TabPage
    Friend WithEvents ComboBoxPlan As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents DataGridViewEpgBal As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewExplot As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCFlow As System.Windows.Forms.DataGridView
    Friend WithEvents EpgContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EpgToolStripMenuItemZoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EpgToolStripMenuItemAddNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EpgToolStripMenuItemDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComboBoxFiltre As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripButtonSortEpg As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_Langs1 As Xl_Langs_Old
End Class
