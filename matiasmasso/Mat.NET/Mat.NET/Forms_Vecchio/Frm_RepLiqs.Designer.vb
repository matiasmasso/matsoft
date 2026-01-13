<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepLiqs
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
        Me.Xl_RepRepLiqs1 = New Mat.Net.Xl_RepRepLiqs()
        Me.SuspendLayout()
        '
        'Xl_RepRepLiqs1
        '
        Me.Xl_RepRepLiqs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepRepLiqs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RepRepLiqs1.Name = "Xl_RepRepLiqs1"
        Me.Xl_RepRepLiqs1.Size = New System.Drawing.Size(372, 266)
        Me.Xl_RepRepLiqs1.TabIndex = 0
        '
        'Frm_RepLiqs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 266)
        Me.Controls.Add(Me.Xl_RepRepLiqs1)
        Me.Name = "Frm_RepLiqs"
        Me.Text = "LIQUIDACIONS A REPRESENTANT"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_RepRepLiqs1 As Mat.Net.Xl_RepRepLiqs
End Class
