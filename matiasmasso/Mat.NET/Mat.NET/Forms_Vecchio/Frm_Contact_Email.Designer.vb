<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Frm_Contact_Email
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
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxPwd = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxContactNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBoxBadMail = New System.Windows.Forms.CheckBox()
        Me.ComboBoxBadMail = New System.Windows.Forms.ComboBox()
        Me.PictureBoxBadMail = New System.Windows.Forms.PictureBox()
        Me.PictureBoxPrivat = New System.Windows.Forms.PictureBox()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.LabelGuid = New System.Windows.Forms.Label()
        Me.TextBoxGuid = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxLang = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckedListBoxSsc = New System.Windows.Forms.CheckedListBox()
        Me.CheckBoxProductRange = New System.Windows.Forms.CheckBox()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.PictureBoxGravatar = New System.Windows.Forms.PictureBox()
        Me.TabPageSocialNetworks = New System.Windows.Forms.TabPage()
        Me.Xl_Contacts1 = New Mat.NET.Xl_Contacts_Old()
        Me.Xl_ProductRange = New Mat.NET.Xl_Product()
        Me.Xl_Rol1 = New Mat.NET.Xl_Rol()
        Me.Xl_SocialNetworkUsers1 = New Mat.NET.Xl_SocialNetworkUsers()
        CType(Me.PictureBoxBadMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPrivat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBoxGravatar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageSocialNetworks.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(547, 450)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(122, 30)
        Me.ButtonOk.TabIndex = 1
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(419, 450)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(122, 30)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(251, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "password"
        '
        'TextBoxPwd
        '
        Me.TextBoxPwd.Location = New System.Drawing.Point(303, 57)
        Me.TextBoxPwd.MaxLength = 24
        Me.TextBoxPwd.Name = "TextBoxPwd"
        Me.TextBoxPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPwd.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxPwd.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "contacte"
        '
        'TextBoxContactNom
        '
        Me.TextBoxContactNom.Location = New System.Drawing.Point(72, 29)
        Me.TextBoxContactNom.Name = "TextBoxContactNom"
        Me.TextBoxContactNom.Size = New System.Drawing.Size(319, 20)
        Me.TextBoxContactNom.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "adreça"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(72, 3)
        Me.TextBoxEmail.MaxLength = 100
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(319, 20)
        Me.TextBoxEmail.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 447)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(672, 36)
        Me.Panel1.TabIndex = 18
        '
        'CheckBoxBadMail
        '
        Me.CheckBoxBadMail.AutoSize = True
        Me.CheckBoxBadMail.Location = New System.Drawing.Point(72, 108)
        Me.CheckBoxBadMail.Name = "CheckBoxBadMail"
        Me.CheckBoxBadMail.Size = New System.Drawing.Size(117, 17)
        Me.CheckBoxBadMail.TabIndex = 19
        Me.CheckBoxBadMail.Text = "tornen els missatjes"
        Me.CheckBoxBadMail.UseVisualStyleBackColor = True
        '
        'ComboBoxBadMail
        '
        Me.ComboBoxBadMail.FormattingEnabled = True
        Me.ComboBoxBadMail.Location = New System.Drawing.Point(198, 107)
        Me.ComboBoxBadMail.Name = "ComboBoxBadMail"
        Me.ComboBoxBadMail.Size = New System.Drawing.Size(193, 21)
        Me.ComboBoxBadMail.TabIndex = 20
        Me.ComboBoxBadMail.Visible = False
        '
        'PictureBoxBadMail
        '
        Me.PictureBoxBadMail.Location = New System.Drawing.Point(50, 108)
        Me.PictureBoxBadMail.Name = "PictureBoxBadMail"
        Me.PictureBoxBadMail.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxBadMail.TabIndex = 21
        Me.PictureBoxBadMail.TabStop = False
        '
        'PictureBoxPrivat
        '
        Me.PictureBoxPrivat.Location = New System.Drawing.Point(398, 7)
        Me.PictureBoxPrivat.Name = "PictureBoxPrivat"
        Me.PictureBoxPrivat.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxPrivat.TabIndex = 35
        Me.PictureBoxPrivat.TabStop = False
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(420, 6)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxPrivat.TabIndex = 34
        Me.CheckBoxPrivat.Text = "privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        '
        'LabelGuid
        '
        Me.LabelGuid.AutoSize = True
        Me.LabelGuid.Location = New System.Drawing.Point(42, 85)
        Me.LabelGuid.Name = "LabelGuid"
        Me.LabelGuid.Size = New System.Drawing.Size(27, 13)
        Me.LabelGuid.TabIndex = 16
        Me.LabelGuid.Text = "clau"
        Me.LabelGuid.Visible = False
        '
        'TextBoxGuid
        '
        Me.TextBoxGuid.Location = New System.Drawing.Point(72, 82)
        Me.TextBoxGuid.MaxLength = 24
        Me.TextBoxGuid.Name = "TextBoxGuid"
        Me.TextBoxGuid.Size = New System.Drawing.Size(319, 20)
        Me.TextBoxGuid.TabIndex = 15
        Me.TextBoxGuid.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(48, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(18, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "rol"
        '
        'ComboBoxLang
        '
        Me.ComboBoxLang.FormattingEnabled = True
        Me.ComboBoxLang.Items.AddRange(New Object() {"ESP", "CAT", "ENG", "POR"})
        Me.ComboBoxLang.Location = New System.Drawing.Point(599, 6)
        Me.ComboBoxLang.Name = "ComboBoxLang"
        Me.ComboBoxLang.Size = New System.Drawing.Size(55, 21)
        Me.ComboBoxLang.TabIndex = 34
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(542, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "idioma:"
        '
        'CheckedListBoxSsc
        '
        Me.CheckedListBoxSsc.FormattingEnabled = True
        Me.CheckedListBoxSsc.Location = New System.Drawing.Point(478, 186)
        Me.CheckedListBoxSsc.Name = "CheckedListBoxSsc"
        Me.CheckedListBoxSsc.Size = New System.Drawing.Size(176, 199)
        Me.CheckedListBoxSsc.TabIndex = 37
        '
        'CheckBoxProductRange
        '
        Me.CheckBoxProductRange.AutoSize = True
        Me.CheckBoxProductRange.Location = New System.Drawing.Point(72, 131)
        Me.CheckBoxProductRange.Name = "CheckBoxProductRange"
        Me.CheckBoxProductRange.Size = New System.Drawing.Size(114, 17)
        Me.CheckBoxProductRange.TabIndex = 38
        Me.CheckBoxProductRange.Text = "restringir el cataleg"
        Me.CheckBoxProductRange.UseVisualStyleBackColor = True
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(586, 131)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsoleto.TabIndex = 40
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        Me.CheckBoxObsoleto.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageSocialNetworks)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabControl1.Location = New System.Drawing.Point(0, 32)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(672, 415)
        Me.TabControl1.TabIndex = 41
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.PictureBoxPrivat)
        Me.TabPage1.Controls.Add(Me.Xl_Contacts1)
        Me.TabPage1.Controls.Add(Me.CheckBoxPrivat)
        Me.TabPage1.Controls.Add(Me.PictureBoxGravatar)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPage1.Controls.Add(Me.ComboBoxLang)
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.Xl_ProductRange)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.CheckBoxProductRange)
        Me.TabPage1.Controls.Add(Me.TextBoxContactNom)
        Me.TabPage1.Controls.Add(Me.CheckedListBoxSsc)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxPwd)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxGuid)
        Me.TabPage1.Controls.Add(Me.LabelGuid)
        Me.TabPage1.Controls.Add(Me.Xl_Rol1)
        Me.TabPage1.Controls.Add(Me.CheckBoxBadMail)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.ComboBoxBadMail)
        Me.TabPage1.Controls.Add(Me.PictureBoxBadMail)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(664, 389)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'PictureBoxGravatar
        '
        Me.PictureBoxGravatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxGravatar.Location = New System.Drawing.Point(574, 38)
        Me.PictureBoxGravatar.Name = "PictureBoxGravatar"
        Me.PictureBoxGravatar.Size = New System.Drawing.Size(80, 80)
        Me.PictureBoxGravatar.TabIndex = 41
        Me.PictureBoxGravatar.TabStop = False
        '
        'TabPageSocialNetworks
        '
        Me.TabPageSocialNetworks.Controls.Add(Me.Xl_SocialNetworkUsers1)
        Me.TabPageSocialNetworks.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSocialNetworks.Name = "TabPageSocialNetworks"
        Me.TabPageSocialNetworks.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSocialNetworks.Size = New System.Drawing.Size(664, 389)
        Me.TabPageSocialNetworks.TabIndex = 1
        Me.TabPageSocialNetworks.Text = "Xarxes socials"
        Me.TabPageSocialNetworks.UseVisualStyleBackColor = True
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.Location = New System.Drawing.Point(4, 186)
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.Size = New System.Drawing.Size(468, 200)
        Me.Xl_Contacts1.TabIndex = 42
        '
        'Xl_ProductRange
        '
        Me.Xl_ProductRange.IsDirty = False
        Me.Xl_ProductRange.Location = New System.Drawing.Point(198, 128)
        Me.Xl_ProductRange.Name = "Xl_ProductRange"
        Me.Xl_ProductRange.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_ProductRange.Product = Nothing
        Me.Xl_ProductRange.Size = New System.Drawing.Size(229, 20)
        Me.Xl_ProductRange.TabIndex = 39
        Me.Xl_ProductRange.Value = Nothing
        Me.Xl_ProductRange.Visible = False
        '
        'Xl_Rol1
        '
        Me.Xl_Rol1.Location = New System.Drawing.Point(72, 56)
        Me.Xl_Rol1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_Rol1.Name = "Xl_Rol1"
        Me.Xl_Rol1.Rol = Nothing
        Me.Xl_Rol1.Size = New System.Drawing.Size(173, 20)
        Me.Xl_Rol1.TabIndex = 31
        '
        'Xl_SocialNetworkUsers1
        '
        Me.Xl_SocialNetworkUsers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SocialNetworkUsers1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_SocialNetworkUsers1.Name = "Xl_SocialNetworkUsers1"
        Me.Xl_SocialNetworkUsers1.Size = New System.Drawing.Size(658, 383)
        Me.Xl_SocialNetworkUsers1.TabIndex = 0
        '
        'Frm_Contact_Email
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 483)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Contact_Email"
        Me.Text = "E-MAIL"
        CType(Me.PictureBoxBadMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPrivat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBoxGravatar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageSocialNetworks.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxContactNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CheckBoxBadMail As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxBadMail As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBoxBadMail As System.Windows.Forms.PictureBox
    Friend WithEvents LabelGuid As System.Windows.Forms.Label
    Friend WithEvents TextBoxGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_Rol1 As Xl_Rol
    Friend WithEvents PictureBoxPrivat As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxLang As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBoxSsc As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxProductRange As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ProductRange As Xl_Product
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPageSocialNetworks As System.Windows.Forms.TabPage
    Friend WithEvents Xl_SocialNetworkUsers1 As Xl_SocialNetworkUsers
    Friend WithEvents PictureBoxGravatar As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_Contacts1 As Mat.Net.Xl_Contacts_Old
End Class
