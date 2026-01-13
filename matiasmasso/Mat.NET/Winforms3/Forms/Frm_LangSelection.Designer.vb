<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LangSelection
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.RadioButtonEsp = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCat = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEng = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPor = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 168)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(226, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(7, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(118, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'RadioButtonEsp
        '
        Me.RadioButtonEsp.AutoSize = True
        Me.RadioButtonEsp.Location = New System.Drawing.Point(81, 43)
        Me.RadioButtonEsp.Name = "RadioButtonEsp"
        Me.RadioButtonEsp.Size = New System.Drawing.Size(63, 17)
        Me.RadioButtonEsp.TabIndex = 0
        Me.RadioButtonEsp.TabStop = True
        Me.RadioButtonEsp.Text = "Español"
        Me.RadioButtonEsp.UseVisualStyleBackColor = True
        '
        'RadioButtonCat
        '
        Me.RadioButtonCat.AutoSize = True
        Me.RadioButtonCat.Location = New System.Drawing.Point(81, 66)
        Me.RadioButtonCat.Name = "RadioButtonCat"
        Me.RadioButtonCat.Size = New System.Drawing.Size(55, 17)
        Me.RadioButtonCat.TabIndex = 1
        Me.RadioButtonCat.TabStop = True
        Me.RadioButtonCat.Text = "Català"
        Me.RadioButtonCat.UseVisualStyleBackColor = True
        '
        'RadioButtonEng
        '
        Me.RadioButtonEng.AutoSize = True
        Me.RadioButtonEng.Location = New System.Drawing.Point(81, 89)
        Me.RadioButtonEng.Name = "RadioButtonEng"
        Me.RadioButtonEng.Size = New System.Drawing.Size(57, 17)
        Me.RadioButtonEng.TabIndex = 2
        Me.RadioButtonEng.TabStop = True
        Me.RadioButtonEng.Text = "Anglès"
        Me.RadioButtonEng.UseVisualStyleBackColor = True
        '
        'RadioButtonPor
        '
        Me.RadioButtonPor.AutoSize = True
        Me.RadioButtonPor.Location = New System.Drawing.Point(81, 112)
        Me.RadioButtonPor.Name = "RadioButtonPor"
        Me.RadioButtonPor.Size = New System.Drawing.Size(73, 17)
        Me.RadioButtonPor.TabIndex = 3
        Me.RadioButtonPor.TabStop = True
        Me.RadioButtonPor.Text = "Portuguès"
        Me.RadioButtonPor.UseVisualStyleBackColor = True
        '
        'Frm_LangSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(226, 199)
        Me.Controls.Add(Me.RadioButtonPor)
        Me.Controls.Add(Me.RadioButtonEng)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadioButtonCat)
        Me.Controls.Add(Me.RadioButtonEsp)
        Me.Name = "Frm_LangSelection"
        Me.Text = "Idioma"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents RadioButtonPor As RadioButton
    Friend WithEvents RadioButtonEng As RadioButton
    Friend WithEvents RadioButtonCat As RadioButton
    Friend WithEvents RadioButtonEsp As RadioButton
End Class
