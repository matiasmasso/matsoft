<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_NIF
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.PictureBoxWarn = New System.Windows.Forms.PictureBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxWarn
        '
        Me.PictureBoxWarn.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBoxWarn.Image = My.Resources.Resources.warn
        Me.PictureBoxWarn.Location = New System.Drawing.Point(177, 0)
        Me.PictureBoxWarn.Name = "PictureBoxWarn"
        Me.PictureBoxWarn.Size = New System.Drawing.Size(18, 20)
        Me.PictureBoxWarn.TabIndex = 6
        Me.PictureBoxWarn.TabStop = False
        Me.PictureBoxWarn.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(174, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Xl_NIF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBoxWarn)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Xl_NIF"
        Me.Size = New System.Drawing.Size(195, 20)
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxWarn As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
