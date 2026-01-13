<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact2
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBoxWrongMail = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxWrongMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox1.Location = New System.Drawing.Point(50, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(180, 20)
        Me.TextBox1.TabIndex = 3
        '
        'PictureBoxWrongMail
        '
        Me.PictureBoxWrongMail.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBoxWrongMail.Image = Global.Winforms.My.Resources.Resources.WrongMail
        Me.PictureBoxWrongMail.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxWrongMail.Name = "PictureBoxWrongMail"
        Me.PictureBoxWrongMail.Size = New System.Drawing.Size(50, 20)
        Me.PictureBoxWrongMail.TabIndex = 2
        Me.PictureBoxWrongMail.TabStop = False
        Me.PictureBoxWrongMail.Visible = False
        '
        'Xl_Contact2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PictureBoxWrongMail)
        Me.Name = "Xl_Contact2"
        Me.Size = New System.Drawing.Size(230, 20)
        CType(Me.PictureBoxWrongMail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWrongMail As System.Windows.Forms.PictureBox

End Class
