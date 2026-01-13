<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ECI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.Xl_Contact1 = New Xl_Contact
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBoxAgrupar = New System.Windows.Forms.GroupBox
        Me.RadioButtonWeek5 = New System.Windows.Forms.RadioButton
        Me.RadioButtonWeek4 = New System.Windows.Forms.RadioButton
        Me.RadioButtonWeek3 = New System.Windows.Forms.RadioButton
        Me.RadioButtonWeek2 = New System.Windows.Forms.RadioButton
        Me.RadioButtonWeek1 = New System.Windows.Forms.RadioButton
        Me.CheckBoxAgrupar = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxDepto = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.GroupBoxAgrupar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 282)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(374, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(155, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(266, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Location = New System.Drawing.Point(82, 28)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(288, 20)
        Me.Xl_Contact1.TabIndex = 42
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "rao social:"
        '
        'GroupBoxAgrupar
        '
        Me.GroupBoxAgrupar.Controls.Add(Me.RadioButtonWeek5)
        Me.GroupBoxAgrupar.Controls.Add(Me.RadioButtonWeek4)
        Me.GroupBoxAgrupar.Controls.Add(Me.RadioButtonWeek3)
        Me.GroupBoxAgrupar.Controls.Add(Me.RadioButtonWeek2)
        Me.GroupBoxAgrupar.Controls.Add(Me.RadioButtonWeek1)
        Me.GroupBoxAgrupar.Location = New System.Drawing.Point(77, 107)
        Me.GroupBoxAgrupar.Name = "GroupBoxAgrupar"
        Me.GroupBoxAgrupar.Size = New System.Drawing.Size(156, 155)
        Me.GroupBoxAgrupar.TabIndex = 44
        Me.GroupBoxAgrupar.TabStop = False
        '
        'RadioButtonWeek5
        '
        Me.RadioButtonWeek5.AutoSize = True
        Me.RadioButtonWeek5.Location = New System.Drawing.Point(48, 125)
        Me.RadioButtonWeek5.Name = "RadioButtonWeek5"
        Me.RadioButtonWeek5.Size = New System.Drawing.Size(71, 17)
        Me.RadioButtonWeek5.TabIndex = 50
        Me.RadioButtonWeek5.TabStop = True
        Me.RadioButtonWeek5.Text = "divendres"
        Me.RadioButtonWeek5.UseVisualStyleBackColor = True
        '
        'RadioButtonWeek4
        '
        Me.RadioButtonWeek4.AutoSize = True
        Me.RadioButtonWeek4.Location = New System.Drawing.Point(48, 102)
        Me.RadioButtonWeek4.Name = "RadioButtonWeek4"
        Me.RadioButtonWeek4.Size = New System.Drawing.Size(52, 17)
        Me.RadioButtonWeek4.TabIndex = 49
        Me.RadioButtonWeek4.TabStop = True
        Me.RadioButtonWeek4.Text = "dijous"
        Me.RadioButtonWeek4.UseVisualStyleBackColor = True
        '
        'RadioButtonWeek3
        '
        Me.RadioButtonWeek3.AutoSize = True
        Me.RadioButtonWeek3.Location = New System.Drawing.Point(48, 79)
        Me.RadioButtonWeek3.Name = "RadioButtonWeek3"
        Me.RadioButtonWeek3.Size = New System.Drawing.Size(67, 17)
        Me.RadioButtonWeek3.TabIndex = 48
        Me.RadioButtonWeek3.TabStop = True
        Me.RadioButtonWeek3.Text = "dimecres"
        Me.RadioButtonWeek3.UseVisualStyleBackColor = True
        '
        'RadioButtonWeek2
        '
        Me.RadioButtonWeek2.AutoSize = True
        Me.RadioButtonWeek2.Location = New System.Drawing.Point(48, 56)
        Me.RadioButtonWeek2.Name = "RadioButtonWeek2"
        Me.RadioButtonWeek2.Size = New System.Drawing.Size(58, 17)
        Me.RadioButtonWeek2.TabIndex = 47
        Me.RadioButtonWeek2.TabStop = True
        Me.RadioButtonWeek2.Text = "dimarts"
        Me.RadioButtonWeek2.UseVisualStyleBackColor = True
        '
        'RadioButtonWeek1
        '
        Me.RadioButtonWeek1.AutoSize = True
        Me.RadioButtonWeek1.Location = New System.Drawing.Point(48, 33)
        Me.RadioButtonWeek1.Name = "RadioButtonWeek1"
        Me.RadioButtonWeek1.Size = New System.Drawing.Size(54, 17)
        Me.RadioButtonWeek1.TabIndex = 46
        Me.RadioButtonWeek1.TabStop = True
        Me.RadioButtonWeek1.Text = "dilluns"
        Me.RadioButtonWeek1.UseVisualStyleBackColor = True
        '
        'CheckBoxAgrupar
        '
        Me.CheckBoxAgrupar.AutoSize = True
        Me.CheckBoxAgrupar.Location = New System.Drawing.Point(82, 101)
        Me.CheckBoxAgrupar.Name = "CheckBoxAgrupar"
        Me.CheckBoxAgrupar.Size = New System.Drawing.Size(122, 17)
        Me.CheckBoxAgrupar.TabIndex = 45
        Me.CheckBoxAgrupar.Text = "Agrupar expedicions"
        Me.CheckBoxAgrupar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "departament:"
        '
        'TextBoxDepto
        '
        Me.TextBoxDepto.Location = New System.Drawing.Point(82, 59)
        Me.TextBoxDepto.Name = "TextBoxDepto"
        Me.TextBoxDepto.Size = New System.Drawing.Size(68, 20)
        Me.TextBoxDepto.TabIndex = 46
        '
        'Frm_ECI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 313)
        Me.Controls.Add(Me.CheckBoxAgrupar)
        Me.Controls.Add(Me.TextBoxDepto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBoxAgrupar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_ECI"
        Me.Text = "EL CORTE INGLES"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxAgrupar.ResumeLayout(False)
        Me.GroupBoxAgrupar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxAgrupar As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxAgrupar As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDepto As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonWeek5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonWeek4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonWeek3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonWeek2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonWeek1 As System.Windows.Forms.RadioButton
End Class
