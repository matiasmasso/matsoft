<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ibans
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
        Me.Xl_Ibans1 = New Winforms.Xl_Ibans()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        CType(Me.Xl_Ibans1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Ibans1
        '
        Me.Xl_Ibans1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Ibans1.DisplayObsolets = False
        Me.Xl_Ibans1.Filter = Nothing
        Me.Xl_Ibans1.Location = New System.Drawing.Point(0, 39)
        Me.Xl_Ibans1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Ibans1.Name = "Xl_Ibans1"
        Me.Xl_Ibans1.Size = New System.Drawing.Size(704, 263)
        Me.Xl_Ibans1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCount})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 304)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(704, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelCount
        '
        Me.ToolStripStatusLabelCount.Name = "ToolStripStatusLabelCount"
        Me.ToolStripStatusLabelCount.Size = New System.Drawing.Size(147, 17)
        Me.ToolStripStatusLabelCount.Text = "ToolStripStatusLabelCount"
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(554, 9)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 9
        '
        'Frm_Ibans
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 326)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Xl_Ibans1)
        Me.Name = "Frm_Ibans"
        Me.Text = "Domiciliacions bancàries"
        CType(Me.Xl_Ibans1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Ibans1 As Winforms.Xl_Ibans
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabelCount As ToolStripStatusLabel
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
