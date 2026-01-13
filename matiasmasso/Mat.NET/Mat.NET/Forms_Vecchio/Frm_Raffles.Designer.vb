<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Raffles
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
        Me.Xl_Raffles1 = New Mat.NET.Xl_Raffles()
        Me.SuspendLayout()
        '
        'Xl_Raffles1
        '
        Me.Xl_Raffles1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Raffles1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Raffles1.Name = "Xl_Raffles1"
        Me.Xl_Raffles1.Size = New System.Drawing.Size(849, 261)
        Me.Xl_Raffles1.TabIndex = 0
        '
        'Frm_Raffles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 261)
        Me.Controls.Add(Me.Xl_Raffles1)
        Me.Name = "Frm_Raffles"
        Me.Text = "Sortejos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Raffles1 As Mat.NET.Xl_Raffles
End Class
