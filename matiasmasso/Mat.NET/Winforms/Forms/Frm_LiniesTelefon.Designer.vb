<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LiniesTelefon
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
        Me.Xl_LiniesTelefon1 = New Winforms.Xl_LiniesTelefon()
        CType(Me.Xl_LiniesTelefon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_LiniesTelefon1
        '
        Me.Xl_LiniesTelefon1.AllowUserToAddRows = False
        Me.Xl_LiniesTelefon1.AllowUserToDeleteRows = False
        Me.Xl_LiniesTelefon1.DisplayObsolets = False
        Me.Xl_LiniesTelefon1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LiniesTelefon1.Filter = Nothing
        Me.Xl_LiniesTelefon1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LiniesTelefon1.MouseIsDown = False
        Me.Xl_LiniesTelefon1.Name = "Xl_LiniesTelefon1"
        Me.Xl_LiniesTelefon1.ReadOnly = True
        Me.Xl_LiniesTelefon1.Size = New System.Drawing.Size(339, 351)
        Me.Xl_LiniesTelefon1.TabIndex = 0
        '
        'Frm_LiniesTelefon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 351)
        Me.Controls.Add(Me.Xl_LiniesTelefon1)
        Me.Name = "Frm_LiniesTelefon"
        Me.Text = "Linies de Telefon"
        CType(Me.Xl_LiniesTelefon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_LiniesTelefon1 As Xl_LiniesTelefon
End Class
