<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Rep
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPageRepComFollowUp = New System.Windows.Forms.TabPage()
        Me.Xl_RepComFollowUp1 = New Mat.Net.Xl_RepComFollowUp()
        Me.TabControl1.SuspendLayout()
        Me.TabPageRepComFollowUp.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageRepComFollowUp)
        Me.TabControl1.Location = New System.Drawing.Point(1, 26)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(566, 235)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(558, 209)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPageRepComFollowUp
        '
        Me.TabPageRepComFollowUp.Controls.Add(Me.Xl_RepComFollowUp1)
        Me.TabPageRepComFollowUp.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRepComFollowUp.Name = "TabPageRepComFollowUp"
        Me.TabPageRepComFollowUp.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRepComFollowUp.Size = New System.Drawing.Size(558, 209)
        Me.TabPageRepComFollowUp.TabIndex = 1
        Me.TabPageRepComFollowUp.Text = "Seguiment comisions"
        Me.TabPageRepComFollowUp.UseVisualStyleBackColor = True
        '
        'Xl_RepComFollowUp1
        '
        Me.Xl_RepComFollowUp1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepComFollowUp1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepComFollowUp1.Name = "Xl_RepComFollowUp1"
        Me.Xl_RepComFollowUp1.Size = New System.Drawing.Size(552, 203)
        Me.Xl_RepComFollowUp1.TabIndex = 0
        '
        'Frm_Rep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 261)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Rep"
        Me.Text = "Frm_Rep"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageRepComFollowUp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPageRepComFollowUp As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RepComFollowUp1 As Mat.Net.Xl_RepComFollowUp
End Class
