<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SegSocialGrups
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
        Me.Xl_SegSocialGrups1 = New Winforms.Xl_SegSocialGrups()
        CType(Me.Xl_SegSocialGrups1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_SegSocialGrups1
        '
        Me.Xl_SegSocialGrups1.AllowUserToAddRows = False
        Me.Xl_SegSocialGrups1.AllowUserToDeleteRows = False
        Me.Xl_SegSocialGrups1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SegSocialGrups1.DisplayObsolets = False
        Me.Xl_SegSocialGrups1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SegSocialGrups1.Filter = Nothing
        Me.Xl_SegSocialGrups1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SegSocialGrups1.MouseIsDown = False
        Me.Xl_SegSocialGrups1.Name = "Xl_SegSocialGrups1"
        Me.Xl_SegSocialGrups1.ReadOnly = True
        Me.Xl_SegSocialGrups1.Size = New System.Drawing.Size(428, 261)
        Me.Xl_SegSocialGrups1.TabIndex = 0
        '
        'Frm_SegSocialGrups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 261)
        Me.Controls.Add(Me.Xl_SegSocialGrups1)
        Me.Name = "Frm_SegSocialGrups"
        Me.Text = "Grups de Cotització a la Seguretat Social"
        CType(Me.Xl_SegSocialGrups1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_SegSocialGrups1 As Xl_SegSocialGrups
End Class
