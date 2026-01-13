<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Last_Fras
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.AnyanteriorToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AnysegüentToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripComboBoxYea = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewMonths = New System.Windows.Forms.DataGridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.LabelProgressCaption = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnyanteriorToolStripButton, Me.AnysegüentToolStripButton, Me.ToolStripComboBoxYea, Me.ToolStripButtonRefresca})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(669, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'AnyanteriorToolStripButton
        '
        Me.AnyanteriorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnyanteriorToolStripButton.Image = Global.Mat.Net.My.Resources.Resources.SquareArrowBackOrange
        Me.AnyanteriorToolStripButton.Name = "AnyanteriorToolStripButton"
        Me.AnyanteriorToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.AnyanteriorToolStripButton.Text = "any anterior"
        '
        'AnysegüentToolStripButton
        '
        Me.AnysegüentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnysegüentToolStripButton.Image = Global.Mat.Net.My.Resources.Resources.SquareArrowTurquesa
        Me.AnysegüentToolStripButton.Name = "AnysegüentToolStripButton"
        Me.AnysegüentToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.AnysegüentToolStripButton.Text = "any següent"
        '
        'ToolStripComboBoxYea
        '
        Me.ToolStripComboBoxYea.AutoSize = False
        Me.ToolStripComboBoxYea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBoxYea.DropDownWidth = 80
        Me.ToolStripComboBoxYea.MaxLength = 4
        Me.ToolStripComboBoxYea.Name = "ToolStripComboBoxYea"
        Me.ToolStripComboBoxYea.Size = New System.Drawing.Size(80, 23)
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresca.Image = Global.Mat.Net.My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewMonths)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(669, 239)
        Me.SplitContainer1.SplitterDistance = 175
        Me.SplitContainer1.TabIndex = 1
        '
        'DataGridViewMonths
        '
        Me.DataGridViewMonths.AllowUserToAddRows = False
        Me.DataGridViewMonths.AllowUserToDeleteRows = False
        Me.DataGridViewMonths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMonths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewMonths.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewMonths.Name = "DataGridViewMonths"
        Me.DataGridViewMonths.ReadOnly = True
        Me.DataGridViewMonths.Size = New System.Drawing.Size(175, 239)
        Me.DataGridViewMonths.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(490, 239)
        Me.DataGridView1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(179, 9)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(357, 10)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
        '
        'LabelProgressCaption
        '
        Me.LabelProgressCaption.AutoSize = True
        Me.LabelProgressCaption.Location = New System.Drawing.Point(540, 6)
        Me.LabelProgressCaption.Name = "LabelProgressCaption"
        Me.LabelProgressCaption.Size = New System.Drawing.Size(86, 13)
        Me.LabelProgressCaption.TabIndex = 3
        Me.LabelProgressCaption.Text = "Progress caption"
        Me.LabelProgressCaption.Visible = False
        '
        'Frm_Last_Fras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 264)
        Me.Controls.Add(Me.LabelProgressCaption)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Last_Fras"
        Me.Text = "FACTURES"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents AnyanteriorToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AnysegüentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripComboBoxYea As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewMonths As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents LabelProgressCaption As System.Windows.Forms.Label
End Class
