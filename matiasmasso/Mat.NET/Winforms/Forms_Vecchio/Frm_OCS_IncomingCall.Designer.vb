<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OCS_IncomingCall
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
        Me.Xl_Contact_Logo1 = New Xl_Contact_Logo()
        Me.PictureBoxCall = New System.Windows.Forms.PictureBox()
        Me.LabelNom = New System.Windows.Forms.Label()
        CType(Me.PictureBoxCall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(3, 2)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 0
        '
        'PictureBoxCall
        '
        Me.PictureBoxCall.Image = My.Resources.Resources.CallOpen
        Me.PictureBoxCall.Location = New System.Drawing.Point(500, 2)
        Me.PictureBoxCall.Name = "PictureBoxCall"
        Me.PictureBoxCall.Size = New System.Drawing.Size(48, 48)
        Me.PictureBoxCall.TabIndex = 1
        Me.PictureBoxCall.TabStop = False
        '
        'LabelNom
        '
        Me.LabelNom.AutoSize = True
        Me.LabelNom.Location = New System.Drawing.Point(160, 2)
        Me.LabelNom.Name = "LabelNom"
        Me.LabelNom.Size = New System.Drawing.Size(72, 13)
        Me.LabelNom.TabIndex = 2
        Me.LabelNom.Text = "(contact nom)"
        '
        'Frm_OCS_IncomingCall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 53)
        Me.Controls.Add(Me.LabelNom)
        Me.Controls.Add(Me.PictureBoxCall)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Name = "Frm_OCS_IncomingCall"
        Me.Text = "TRUCADA ENTRANT"
        CType(Me.PictureBoxCall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents PictureBoxCall As System.Windows.Forms.PictureBox
    Friend WithEvents LabelNom As System.Windows.Forms.Label
End Class
