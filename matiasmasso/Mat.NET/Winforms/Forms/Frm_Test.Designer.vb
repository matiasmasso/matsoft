<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Test
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
        Me.ButtonUpload = New System.Windows.Forms.Button()
        Me.LabelFilename = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ButtonUpload
        '
        Me.ButtonUpload.Location = New System.Drawing.Point(52, 50)
        Me.ButtonUpload.Name = "ButtonUpload"
        Me.ButtonUpload.Size = New System.Drawing.Size(46, 23)
        Me.ButtonUpload.TabIndex = 1
        Me.ButtonUpload.Text = "..."
        Me.ButtonUpload.UseVisualStyleBackColor = True
        '
        'LabelFilename
        '
        Me.LabelFilename.AutoSize = True
        Me.LabelFilename.Location = New System.Drawing.Point(104, 55)
        Me.LabelFilename.Name = "LabelFilename"
        Me.LabelFilename.Size = New System.Drawing.Size(194, 13)
        Me.LabelFilename.TabIndex = 2
        Me.LabelFilename.Text = "(no hi ha cap video seleccionat encara)"
        '
        'Frm_Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 193)
        Me.Controls.Add(Me.LabelFilename)
        Me.Controls.Add(Me.ButtonUpload)
        Me.Name = "Frm_Test"
        Me.Text = "Frm_Test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonUpload As Button
    Friend WithEvents LabelFilename As Label
End Class
