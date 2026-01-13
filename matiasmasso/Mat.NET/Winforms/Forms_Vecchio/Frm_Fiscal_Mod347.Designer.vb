<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fiscal_Mod347
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripComboBoxYea = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripComboBoxOp = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripButtonFile = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonNewYear = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripButtonMail = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBoxYea, Me.ToolStripComboBoxOp, Me.ToolStripButtonFile, Me.ToolStripButtonNewYear, Me.ToolStripSeparator1, Me.ToolStripProgressBar1, Me.ToolStripButtonMail, Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(632, 25)
        Me.ToolStrip1.TabIndex = 0
        '
        'ToolStripComboBoxYea
        '
        Me.ToolStripComboBoxYea.Name = "ToolStripComboBoxYea"
        Me.ToolStripComboBoxYea.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripComboBoxOp
        '
        Me.ToolStripComboBoxOp.Name = "ToolStripComboBoxOp"
        Me.ToolStripComboBoxOp.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripButtonFile
        '
        Me.ToolStripButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFile.Image = My.Resources.Resources.save_16
        Me.ToolStripButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFile.Name = "ToolStripButtonFile"
        Me.ToolStripButtonFile.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFile.Text = "exportar fitxer"
        '
        'ToolStripButtonNewYear
        '
        Me.ToolStripButtonNewYear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonNewYear.Image = My.Resources.Resources.NewDoc
        Me.ToolStripButtonNewYear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonNewYear.Name = "ToolStripButtonNewYear"
        Me.ToolStripButtonNewYear.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonNewYear.Text = "nou any"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripProgressBar1.Visible = False
        '
        'ToolStripButtonMail
        '
        Me.ToolStripButtonMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonMail.Image = My.Resources.Resources.MailSobreGroc
        Me.ToolStripButtonMail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonMail.Name = "ToolStripButtonMail"
        Me.ToolStripButtonMail.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonMail.Text = "mailing"
        Me.ToolStripButtonMail.ToolTipText = "llistat de adreçes de correu per mailing"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Ecel"
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
        Me.DataGridView1.Size = New System.Drawing.Size(632, 348)
        Me.DataGridView1.TabIndex = 1
        '
        'Frm_Fiscal_Mod347
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 373)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Fiscal_Mod347"
        Me.Text = "MODEL 347"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripComboBoxYea As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripComboBoxOp As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButtonFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonNewYear As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButtonMail As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
End Class
