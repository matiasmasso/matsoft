<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cnap
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
        Me.TextBoxParent = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCod = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNomShort_ESP = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTags = New System.Windows.Forms.TextBox()
        Me.TextBoxNomShort_CAT = New System.Windows.Forms.TextBox()
        Me.TextBoxNomShort_ENG = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxNomLong_ESP = New System.Windows.Forms.TextBox()
        Me.TextBoxNomLong_ENG = New System.Windows.Forms.TextBox()
        Me.TextBoxNomLong_CAT = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxParent
        '
        Me.TextBoxParent.Location = New System.Drawing.Point(104, 26)
        Me.TextBoxParent.Name = "TextBoxParent"
        Me.TextBoxParent.ReadOnly = True
        Me.TextBoxParent.Size = New System.Drawing.Size(345, 20)
        Me.TextBoxParent.TabIndex = 0
        Me.TextBoxParent.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "descendeix de:"
        '
        'TextBoxCod
        '
        Me.TextBoxCod.Location = New System.Drawing.Point(104, 52)
        Me.TextBoxCod.MaxLength = 8
        Me.TextBoxCod.Name = "TextBoxCod"
        Me.TextBoxCod.Size = New System.Drawing.Size(78, 20)
        Me.TextBoxCod.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "codi:"
        '
        'TextBoxNomShort_ESP
        '
        Me.TextBoxNomShort_ESP.Location = New System.Drawing.Point(82, 19)
        Me.TextBoxNomShort_ESP.MaxLength = 60
        Me.TextBoxNomShort_ESP.Name = "TextBoxNomShort_ESP"
        Me.TextBoxNomShort_ESP.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomShort_ESP.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 424)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(-305, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 29
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonCancel.Location = New System.Drawing.Point(267, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 26
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(378, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 27
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 328)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "tags: (1 per linia)"
        '
        'TextBoxTags
        '
        Me.TextBoxTags.Location = New System.Drawing.Point(22, 344)
        Me.TextBoxTags.Multiline = True
        Me.TextBoxTags.Name = "TextBoxTags"
        Me.TextBoxTags.Size = New System.Drawing.Size(427, 74)
        Me.TextBoxTags.TabIndex = 44
        '
        'TextBoxNomShort_CAT
        '
        Me.TextBoxNomShort_CAT.Location = New System.Drawing.Point(82, 45)
        Me.TextBoxNomShort_CAT.MaxLength = 60
        Me.TextBoxNomShort_CAT.Name = "TextBoxNomShort_CAT"
        Me.TextBoxNomShort_CAT.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomShort_CAT.TabIndex = 45
        '
        'TextBoxNomShort_ENG
        '
        Me.TextBoxNomShort_ENG.Location = New System.Drawing.Point(82, 71)
        Me.TextBoxNomShort_ENG.MaxLength = 60
        Me.TextBoxNomShort_ENG.Name = "TextBoxNomShort_ENG"
        Me.TextBoxNomShort_ENG.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomShort_ENG.TabIndex = 46
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxNomShort_ESP)
        Me.GroupBox1.Controls.Add(Me.TextBoxNomShort_ENG)
        Me.GroupBox1.Controls.Add(Me.TextBoxNomShort_CAT)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 98)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(427, 100)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Nom (dins el seu ascendent)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Espanyol:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Català"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Anglès:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.TextBoxNomLong_ESP)
        Me.GroupBox2.Controls.Add(Me.TextBoxNomLong_ENG)
        Me.GroupBox2.Controls.Add(Me.TextBoxNomLong_CAT)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 214)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(427, 100)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Nom complert"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Anglès:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(27, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "Català"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(27, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Espanyol:"
        '
        'TextBoxNomLong_ESP
        '
        Me.TextBoxNomLong_ESP.Location = New System.Drawing.Point(82, 19)
        Me.TextBoxNomLong_ESP.MaxLength = 60
        Me.TextBoxNomLong_ESP.Name = "TextBoxNomLong_ESP"
        Me.TextBoxNomLong_ESP.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomLong_ESP.TabIndex = 4
        '
        'TextBoxNomLong_ENG
        '
        Me.TextBoxNomLong_ENG.Location = New System.Drawing.Point(82, 71)
        Me.TextBoxNomLong_ENG.MaxLength = 60
        Me.TextBoxNomLong_ENG.Name = "TextBoxNomLong_ENG"
        Me.TextBoxNomLong_ENG.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomLong_ENG.TabIndex = 46
        '
        'TextBoxNomLong_CAT
        '
        Me.TextBoxNomLong_CAT.Location = New System.Drawing.Point(82, 45)
        Me.TextBoxNomLong_CAT.MaxLength = 60
        Me.TextBoxNomLong_CAT.Name = "TextBoxNomLong_CAT"
        Me.TextBoxNomLong_CAT.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxNomLong_CAT.TabIndex = 45
        '
        'Frm_Cnap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 455)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBoxTags)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxCod)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxParent)
        Me.Name = "Frm_Cnap"
        Me.Text = "CNAP"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxParent As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCod As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomShort_ESP As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTags As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNomShort_CAT As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNomShort_ENG As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomLong_ESP As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNomLong_ENG As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNomLong_CAT As System.Windows.Forms.TextBox
End Class
