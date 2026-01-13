<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Last_Memos
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.AnyanteriorToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.AnysegüentToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripComboBoxYea = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnyanteriorToolStripButton, Me.AnysegüentToolStripButton, Me.ToolStripComboBoxYea, Me.ToolStripButtonRefresca})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(555, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(555, 239)
        Me.DataGridView1.TabIndex = 1
        '
        'AnyanteriorToolStripButton
        '
        Me.AnyanteriorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnyanteriorToolStripButton.Image = My.Resources.Resources.SquareArrowBackOrange
        Me.AnyanteriorToolStripButton.Name = "AnyanteriorToolStripButton"
        Me.AnyanteriorToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.AnyanteriorToolStripButton.Text = "any anterior"
        '
        'AnysegüentToolStripButton
        '
        Me.AnysegüentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnysegüentToolStripButton.Image = My.Resources.Resources.SquareArrowTurquesa
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
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'Frm_Last_Memos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 264)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Last_Memos"
        Me.Text = "ULTIMS MEMOS REGISTRATS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents AnyanteriorToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AnysegüentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripComboBoxYea As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
End Class
