<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Frm_CcbBlock
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_CcbBlock))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TextBoxCta = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxContacte = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.GroupBoxBlockOptions = New System.Windows.Forms.GroupBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.RadioButtonFch = New System.Windows.Forms.RadioButton
        Me.RadioButtonFullYea = New System.Windows.Forms.RadioButton
        Me.CheckBoxBlock = New System.Windows.Forms.CheckBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxBlockOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(371, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(44, 24)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'TextBoxCta
        '
        Me.TextBoxCta.Location = New System.Drawing.Point(88, 45)
        Me.TextBoxCta.Name = "TextBoxCta"
        Me.TextBoxCta.ReadOnly = True
        Me.TextBoxCta.Size = New System.Drawing.Size(221, 20)
        Me.TextBoxCta.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Compte"
        '
        'TextBoxContacte
        '
        Me.TextBoxContacte.Location = New System.Drawing.Point(88, 71)
        Me.TextBoxContacte.Name = "TextBoxContacte"
        Me.TextBoxContacte.ReadOnly = True
        Me.TextBoxContacte.Size = New System.Drawing.Size(327, 20)
        Me.TextBoxContacte.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Contacte"
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(314, 287)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(101, 30)
        Me.ButtonOk.TabIndex = 5
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(19, 287)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(101, 30)
        Me.ButtonCancel.TabIndex = 0
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'GroupBoxBlockOptions
        '
        Me.GroupBoxBlockOptions.Controls.Add(Me.DateTimePicker1)
        Me.GroupBoxBlockOptions.Controls.Add(Me.RadioButtonFch)
        Me.GroupBoxBlockOptions.Controls.Add(Me.RadioButtonFullYea)
        Me.GroupBoxBlockOptions.Location = New System.Drawing.Point(19, 126)
        Me.GroupBoxBlockOptions.Name = "GroupBoxBlockOptions"
        Me.GroupBoxBlockOptions.Size = New System.Drawing.Size(396, 104)
        Me.GroupBoxBlockOptions.TabIndex = 13
        Me.GroupBoxBlockOptions.TabStop = False
        Me.GroupBoxBlockOptions.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(203, 56)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'RadioButtonFch
        '
        Me.RadioButtonFch.AutoSize = True
        Me.RadioButtonFch.Checked = True
        Me.RadioButtonFch.Location = New System.Drawing.Point(63, 58)
        Me.RadioButtonFch.Name = "RadioButtonFch"
        Me.RadioButtonFch.Size = New System.Drawing.Size(121, 17)
        Me.RadioButtonFch.TabIndex = 3
        Me.RadioButtonFch.Text = "Bloqueija fins la data:"
        '
        'RadioButtonFullYea
        '
        Me.RadioButtonFullYea.AutoSize = True
        Me.RadioButtonFullYea.Location = New System.Drawing.Point(63, 35)
        Me.RadioButtonFullYea.Name = "RadioButtonFullYea"
        Me.RadioButtonFullYea.Size = New System.Drawing.Size(103, 17)
        Me.RadioButtonFullYea.TabIndex = 2
        Me.RadioButtonFullYea.TabStop = False
        Me.RadioButtonFullYea.Text = "Bloqueija tot l'any"
        '
        'CheckBoxBlock
        '
        Me.CheckBoxBlock.AutoSize = True
        Me.CheckBoxBlock.Location = New System.Drawing.Point(19, 115)
        Me.CheckBoxBlock.Name = "CheckBoxBlock"
        Me.CheckBoxBlock.Size = New System.Drawing.Size(67, 17)
        Me.CheckBoxBlock.TabIndex = 14
        Me.CheckBoxBlock.Text = "bloqueijar"
        '
        'Frm_CcbBlock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 319)
        Me.Controls.Add(Me.CheckBoxBlock)
        Me.Controls.Add(Me.GroupBoxBlockOptions)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBoxContacte)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCta)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Frm_CcbBlock"
        Me.Text = "BLOQUEIG COMPTE"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxBlockOptions.ResumeLayout(False)
        Me.GroupBoxBlockOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxCta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxContacte As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBoxBlockOptions As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents RadioButtonFch As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFullYea As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxBlock As System.Windows.Forms.CheckBox
End Class
