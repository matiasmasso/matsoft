<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Aeat_Mod349
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonFitxer = New System.Windows.Forms.ToolStripButton
        Me.ToolStripComboBoxYea = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripComboBoxQ = New System.Windows.Forms.ToolStripComboBox
        Me.TextBoxResum = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(219, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(284, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Declaració Recapitulativa de Operacions Intracomunitaries"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel, Me.ToolStripButtonFitxer, Me.ToolStripComboBoxYea, Me.ToolStripComboBoxQ})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(870, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "ToolStripButton1"
        '
        'ToolStripButtonFitxer
        '
        Me.ToolStripButtonFitxer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFitxer.Image = My.Resources.Resources.save_16
        Me.ToolStripButtonFitxer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFitxer.Name = "ToolStripButtonFitxer"
        Me.ToolStripButtonFitxer.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFitxer.Text = "ToolStripButton1"
        '
        'ToolStripComboBoxYea
        '
        Me.ToolStripComboBoxYea.Name = "ToolStripComboBoxYea"
        Me.ToolStripComboBoxYea.Size = New System.Drawing.Size(75, 25)
        '
        'ToolStripComboBoxQ
        '
        Me.ToolStripComboBoxQ.Items.AddRange(New Object() {"1T", "2T", "3T", "4T"})
        Me.ToolStripComboBoxQ.Name = "ToolStripComboBoxQ"
        Me.ToolStripComboBoxQ.Size = New System.Drawing.Size(75, 25)
        '
        'TextBoxResum
        '
        Me.TextBoxResum.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBoxResum.Location = New System.Drawing.Point(0, 260)
        Me.TextBoxResum.Name = "TextBoxResum"
        Me.TextBoxResum.Size = New System.Drawing.Size(870, 20)
        Me.TextBoxResum.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 24)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(870, 236)
        Me.DataGridView1.TabIndex = 3
        '
        'frm_Aeat_Mod349
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 280)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxResum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frm_Aeat_Mod349"
        Me.Text = "MODEL 349"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFitxer As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBoxResum As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripComboBoxYea As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripComboBoxQ As System.Windows.Forms.ToolStripComboBox
End Class
