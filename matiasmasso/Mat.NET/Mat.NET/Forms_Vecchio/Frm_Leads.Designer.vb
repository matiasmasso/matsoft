<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Leads
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
        Me.Xl_Leads1 = New Mat.NET.Xl_Leads()
        Me.SuspendLayout()
        '
        'Xl_Leads1
        '
        Me.Xl_Leads1.Location = New System.Drawing.Point(1, 12)
        Me.Xl_Leads1.Name = "Xl_Leads1"
        Me.Xl_Leads1.SelectionMode = BLL.Defaults.SelectionModes.Browse
        Me.Xl_Leads1.Size = New System.Drawing.Size(494, 386)
        Me.Xl_Leads1.TabIndex = 0
        '
        'Frm_Leads
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 398)
        Me.Controls.Add(Me.Xl_Leads1)
        Me.Name = "Frm_Leads"
        Me.Text = "Leads"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Leads1 As Mat.NET.Xl_Leads
End Class
