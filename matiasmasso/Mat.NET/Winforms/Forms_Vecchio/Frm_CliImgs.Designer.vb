<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliImgs
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
        Me.Xl_CliImgs1 = New Xl_CliImgs
        Me.SuspendLayout()
        '
        'Xl_CliImgs1
        '
        Me.Xl_CliImgs1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Xl_CliImgs1.Location = New System.Drawing.Point(82, 1)
        Me.Xl_CliImgs1.Name = "Xl_CliImgs1"
        Me.Xl_CliImgs1.Size = New System.Drawing.Size(200, 261)
        Me.Xl_CliImgs1.TabIndex = 0
        '
        'Frm_CliImgs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.Controls.Add(Me.Xl_CliImgs1)
        Me.Name = "Frm_CliImgs"
        Me.Text = "IMATGES DE CONTACTE"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_CliImgs1 As Xl_CliImgs
End Class
