<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OutVivaceExpedicions
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
        Me.Xl_OutVivaceExpedicions1 = New Winforms.Xl_OutVivaceExpedicions()
        CType(Me.Xl_OutVivaceExpedicions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_OutVivaceExpedicions1
        '
        Me.Xl_OutVivaceExpedicions1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_OutVivaceExpedicions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_OutVivaceExpedicions1.DisplayObsolets = False
        Me.Xl_OutVivaceExpedicions1.Filter = Nothing
        Me.Xl_OutVivaceExpedicions1.Location = New System.Drawing.Point(0, 62)
        Me.Xl_OutVivaceExpedicions1.MouseIsDown = False
        Me.Xl_OutVivaceExpedicions1.Name = "Xl_OutVivaceExpedicions1"
        Me.Xl_OutVivaceExpedicions1.Size = New System.Drawing.Size(953, 235)
        Me.Xl_OutVivaceExpedicions1.TabIndex = 0
        '
        'Frm_OutVivaceExpedicions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 298)
        Me.Controls.Add(Me.Xl_OutVivaceExpedicions1)
        Me.Name = "Frm_OutVivaceExpedicions"
        Me.Text = "Expedicions Vivace"
        CType(Me.Xl_OutVivaceExpedicions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_OutVivaceExpedicions1 As Xl_OutVivaceExpedicions
End Class
