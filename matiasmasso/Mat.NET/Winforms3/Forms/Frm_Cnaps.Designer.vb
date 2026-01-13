<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cnaps
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
        Me.Xl_CnapTree1 = New Xl_CnapTree()
        Me.SuspendLayout
        '
        'Xl_CnapTree1
        '
        Me.Xl_CnapTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CnapTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CnapTree1.Name = "Xl_CnapTree1"
        Me.Xl_CnapTree1.Size = New System.Drawing.Size(466, 471)
        Me.Xl_CnapTree1.TabIndex = 0
        '
        'Frm_Cnaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 471)
        Me.Controls.Add(Me.Xl_CnapTree1)
        Me.Name = "Frm_Cnaps"
        Me.Text = "CLASIFICACION NORMALIZADA DE ARTICULOS DE PUERICULTURA (CNAP)"
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Xl_CnapTree1 As Xl_CnapTree
End Class
