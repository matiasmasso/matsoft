<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Promos
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
        Me.Xl_Promos1 = New Mat.NET.Xl_Promos()
        Me.SuspendLayout()
        '
        'Xl_Promos1
        '
        Me.Xl_Promos1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Promos1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Promos1.Name = "Xl_Promos1"
        Me.Xl_Promos1.Size = New System.Drawing.Size(570, 261)
        Me.Xl_Promos1.TabIndex = 0
        '
        'Frm_Promos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 261)
        Me.Controls.Add(Me.Xl_Promos1)
        Me.Name = "Frm_Promos"
        Me.Text = "Promos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Promos1 As Mat.NET.Xl_Promos
End Class
