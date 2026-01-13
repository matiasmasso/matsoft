<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Rol_Select
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
        Me.Xl_Rols1 = New Xl_Rols()
        Me.SuspendLayout()
        '
        'Xl_Rols1
        '
        Me.Xl_Rols1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Rols1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Rols1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Rols1.Name = "Xl_Rols1"
        Me.Xl_Rols1.Size = New System.Drawing.Size(779, 634)
        Me.Xl_Rols1.TabIndex = 0
        '
        'Frm_Rol_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 634)
        Me.Controls.Add(Me.Xl_Rols1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_Rol_Select"
        Me.Text = "SELECCIONA ROL..."
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Rols1 As Xl_Rols
End Class
