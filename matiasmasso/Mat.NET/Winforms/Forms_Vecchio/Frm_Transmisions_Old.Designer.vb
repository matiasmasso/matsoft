<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Transmisions_Old
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.AnyanteriorToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.AnysegüentToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton
        Me.Xl_Yea1 = New Xl_Yea
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStripButtonAddNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnyanteriorToolStripButton, Me.AnysegüentToolStripButton, Me.ToolStripButtonRefresca, Me.ToolStripButtonAddNew})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(436, 25)
        Me.ToolStrip1.TabIndex = 10
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'AnyanteriorToolStripButton
        '
        Me.AnyanteriorToolStripButton.Image = My.Resources.Resources.SquareArrowBackOrange
        Me.AnyanteriorToolStripButton.Name = "AnyanteriorToolStripButton"
        Me.AnyanteriorToolStripButton.Size = New System.Drawing.Size(86, 22)
        Me.AnyanteriorToolStripButton.Text = "any anterior"
        '
        'AnysegüentToolStripButton
        '
        Me.AnysegüentToolStripButton.Image = My.Resources.Resources.SquareArrowTurquesa
        Me.AnysegüentToolStripButton.Name = "AnysegüentToolStripButton"
        Me.AnysegüentToolStripButton.Size = New System.Drawing.Size(87, 22)
        Me.AnysegüentToolStripButton.Text = "any següent"
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(67, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'Xl_Yea1
        '
        Me.Xl_Yea1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Yea1.Location = New System.Drawing.Point(385, 2)
        Me.Xl_Yea1.Name = "Xl_Yea1"
        Me.Xl_Yea1.Size = New System.Drawing.Size(48, 20)
        Me.Xl_Yea1.TabIndex = 11
        Me.Xl_Yea1.Yea = 0
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
        Me.DataGridView1.Size = New System.Drawing.Size(436, 248)
        Me.DataGridView1.TabIndex = 12
        '
        'ToolStripButtonAddNew
        '
        Me.ToolStripButtonAddNew.Image = My.Resources.Resources.NewDoc
        Me.ToolStripButtonAddNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonAddNew.Name = "ToolStripButtonAddNew"
        Me.ToolStripButtonAddNew.Size = New System.Drawing.Size(103, 22)
        Me.ToolStripButtonAddNew.Text = "Nova transmisió"
        '
        'Frm_Transmisions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 273)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Xl_Yea1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Transmisions"
        Me.Text = "TRANSMISIONS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents AnyanteriorToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AnysegüentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_Yea1 As Xl_Yea
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButtonAddNew As System.Windows.Forms.ToolStripButton
End Class
