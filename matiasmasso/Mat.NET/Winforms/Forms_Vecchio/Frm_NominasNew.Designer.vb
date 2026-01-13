<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_NominasNew
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_ProgressBar1 = New Winforms.Xl_ProgressBar()
        Me.LabelUserStatus = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(1413, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(1709, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.Xl_ProgressBar1)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 627)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1997, 74)
        Me.Panel1.TabIndex = 44
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 2)
        Me.Xl_ProgressBar1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(1997, 72)
        Me.Xl_ProgressBar1.TabIndex = 14
        Me.Xl_ProgressBar1.Visible = False
        '
        'LabelUserStatus
        '
        Me.LabelUserStatus.AutoSize = True
        Me.LabelUserStatus.Location = New System.Drawing.Point(24, 33)
        Me.LabelUserStatus.Name = "LabelUserStatus"
        Me.LabelUserStatus.Size = New System.Drawing.Size(226, 32)
        Me.LabelUserStatus.TabIndex = 48
        Me.LabelUserStatus.Text = "LabelUserStatus"
        '
        'Frm_NominasNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1997, 701)
        Me.Controls.Add(Me.LabelUserStatus)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_NominasNew"
        Me.Text = "Nominas"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents LabelUserStatus As Label
End Class
