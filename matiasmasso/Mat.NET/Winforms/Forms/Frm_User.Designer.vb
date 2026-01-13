<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_User
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCognoms = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxTelefon = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchDeleted = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFchActivated = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBoxDeleted = New System.Windows.Forms.CheckBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.CheckBoxActivated = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchCreated = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBoxSource = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxNickName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_LookupRol1 = New Winforms.Xl_LookupRol()
        Me.Xl_Langs1 = New Winforms.Xl_Langs()
        Me.Xl_Sex1 = New Winforms.Xl_Sex()
        Me.Xl_Country1 = New Winforms.Xl_Country()
        Me.Xl_ZipCod1 = New Winforms.Xl_ZipCod()
        Me.Xl_Yea1 = New Winforms.Xl_Yea()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Contacts1 = New Winforms.Xl_Contacts()
        Me.Xl_Contact_Add1 = New Winforms.Xl_Contact_Add()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_RaffleParticipants1 = New Winforms.Xl_RaffleParticipants2()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_RaffleParticipants1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(81, 68)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(507, 20)
        Me.TextBoxNom.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Nom"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 382)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(610, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(391, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(502, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(81, 41)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(507, 20)
        Me.TextBoxEmail.TabIndex = 48
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "email"
        '
        'TextBoxCognoms
        '
        Me.TextBoxCognoms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCognoms.Location = New System.Drawing.Point(81, 94)
        Me.TextBoxCognoms.Name = "TextBoxCognoms"
        Me.TextBoxCognoms.Size = New System.Drawing.Size(507, 20)
        Me.TextBoxCognoms.TabIndex = 50
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Cognoms"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Pais"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 178)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Codi postal"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 280)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Idioma"
        '
        'TextBoxTelefon
        '
        Me.TextBoxTelefon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTelefon.Location = New System.Drawing.Point(81, 198)
        Me.TextBoxTelefon.Name = "TextBoxTelefon"
        Me.TextBoxTelefon.Size = New System.Drawing.Size(149, 20)
        Me.TextBoxTelefon.TabIndex = 89
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 88
        Me.Label10.Text = "Telefon:"
        '
        'DateTimePickerFchDeleted
        '
        Me.DateTimePickerFchDeleted.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchDeleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchDeleted.Location = New System.Drawing.Point(161, 97)
        Me.DateTimePickerFchDeleted.Name = "DateTimePickerFchDeleted"
        Me.DateTimePickerFchDeleted.Size = New System.Drawing.Size(116, 20)
        Me.DateTimePickerFchDeleted.TabIndex = 87
        Me.DateTimePickerFchDeleted.Visible = False
        '
        'DateTimePickerFchActivated
        '
        Me.DateTimePickerFchActivated.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchActivated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchActivated.Location = New System.Drawing.Point(161, 74)
        Me.DateTimePickerFchActivated.Name = "DateTimePickerFchActivated"
        Me.DateTimePickerFchActivated.Size = New System.Drawing.Size(116, 20)
        Me.DateTimePickerFchActivated.TabIndex = 86
        Me.DateTimePickerFchActivated.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 227)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 76
        Me.Label7.Text = "Password:"
        '
        'CheckBoxDeleted
        '
        Me.CheckBoxDeleted.AutoSize = True
        Me.CheckBoxDeleted.Location = New System.Drawing.Point(72, 97)
        Me.CheckBoxDeleted.Name = "CheckBoxDeleted"
        Me.CheckBoxDeleted.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxDeleted.TabIndex = 85
        Me.CheckBoxDeleted.Text = "baixa"
        Me.CheckBoxDeleted.UseVisualStyleBackColor = True
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPassword.Location = New System.Drawing.Point(81, 224)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(149, 20)
        Me.TextBoxPassword.TabIndex = 77
        '
        'CheckBoxActivated
        '
        Me.CheckBoxActivated.AutoSize = True
        Me.CheckBoxActivated.Location = New System.Drawing.Point(72, 74)
        Me.CheckBoxActivated.Name = "CheckBoxActivated"
        Me.CheckBoxActivated.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxActivated.TabIndex = 84
        Me.CheckBoxActivated.Text = "activat"
        Me.CheckBoxActivated.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 83
        Me.Label9.Text = "registre:"
        '
        'DateTimePickerFchCreated
        '
        Me.DateTimePickerFchCreated.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchCreated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchCreated.Location = New System.Drawing.Point(72, 47)
        Me.DateTimePickerFchCreated.Name = "DateTimePickerFchCreated"
        Me.DateTimePickerFchCreated.Size = New System.Drawing.Size(113, 20)
        Me.DateTimePickerFchCreated.TabIndex = 82
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "font:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 253)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "neixament:"
        '
        'ComboBoxSource
        '
        Me.ComboBoxSource.FormattingEnabled = True
        Me.ComboBoxSource.Location = New System.Drawing.Point(72, 19)
        Me.ComboBoxSource.Name = "ComboBoxSource"
        Me.ComboBoxSource.Size = New System.Drawing.Size(205, 21)
        Me.ComboBoxSource.TabIndex = 80
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ComboBoxSource)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFchCreated)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFchDeleted)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFchActivated)
        Me.GroupBox1.Controls.Add(Me.CheckBoxActivated)
        Me.GroupBox1.Controls.Add(Me.CheckBoxDeleted)
        Me.GroupBox1.Location = New System.Drawing.Point(294, 198)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(296, 137)
        Me.GroupBox1.TabIndex = 90
        Me.GroupBox1.TabStop = False
        '
        'TextBoxNickName
        '
        Me.TextBoxNickName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNickName.Location = New System.Drawing.Point(81, 120)
        Me.TextBoxNickName.Name = "TextBoxNickName"
        Me.TextBoxNickName.Size = New System.Drawing.Size(507, 20)
        Me.TextBoxNickName.TabIndex = 92
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 120)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 91
        Me.Label12.Text = "NickName"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 304)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 13)
        Me.Label13.TabIndex = 94
        Me.Label13.Text = "Rol"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(604, 369)
        Me.TabControl1.TabIndex = 95
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LookupRol1)
        Me.TabPage1.Controls.Add(Me.Xl_Langs1)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.TextBoxNickName)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxTelefon)
        Me.TabPage1.Controls.Add(Me.TextBoxCognoms)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Xl_Sex1)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_Country1)
        Me.TabPage1.Controls.Add(Me.TextBoxPassword)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Xl_ZipCod1)
        Me.TabPage1.Controls.Add(Me.Xl_Yea1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(596, 343)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_LookupRol1
        '
        Me.Xl_LookupRol1.IsDirty = False
        Me.Xl_LookupRol1.Location = New System.Drawing.Point(81, 304)
        Me.Xl_LookupRol1.Name = "Xl_LookupRol1"
        Me.Xl_LookupRol1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRol1.ReadOnlyLookup = False
        Me.Xl_LookupRol1.Rol = Nothing
        Me.Xl_LookupRol1.Size = New System.Drawing.Size(149, 20)
        Me.Xl_LookupRol1.TabIndex = 96
        Me.Xl_LookupRol1.Value = Nothing
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(81, 276)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 95
        Me.Xl_Langs1.Value = Nothing
        '
        'Xl_Sex1
        '
        Me.Xl_Sex1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Sex1.Location = New System.Drawing.Point(969, 149)
        Me.Xl_Sex1.Name = "Xl_Sex1"
        Me.Xl_Sex1.Sex = DTO.DTOUser.Sexs.notSet
        Me.Xl_Sex1.Size = New System.Drawing.Size(45, 45)
        Me.Xl_Sex1.TabIndex = 51
        '
        'Xl_Country1
        '
        Me.Xl_Country1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Country1.Country = Nothing
        Me.Xl_Country1.IsDirty = False
        Me.Xl_Country1.Location = New System.Drawing.Point(81, 150)
        Me.Xl_Country1.Name = "Xl_Country1"
        Me.Xl_Country1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Country1.ReadOnlyLookup = False
        Me.Xl_Country1.Size = New System.Drawing.Size(508, 20)
        Me.Xl_Country1.TabIndex = 52
        Me.Xl_Country1.Value = Nothing
        '
        'Xl_ZipCod1
        '
        Me.Xl_ZipCod1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ZipCod1.Location = New System.Drawing.Point(81, 173)
        Me.Xl_ZipCod1.Name = "Xl_ZipCod1"
        Me.Xl_ZipCod1.Size = New System.Drawing.Size(508, 20)
        Me.Xl_ZipCod1.TabIndex = 54
        Me.Xl_ZipCod1.ZipCod = ""
        '
        'Xl_Yea1
        '
        Me.Xl_Yea1.Location = New System.Drawing.Point(81, 250)
        Me.Xl_Yea1.Name = "Xl_Yea1"
        Me.Xl_Yea1.Size = New System.Drawing.Size(49, 20)
        Me.Xl_Yea1.TabIndex = 79
        Me.Xl_Yea1.Yea = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Contacts1)
        Me.TabPage3.Controls.Add(Me.Xl_Contact_Add1)
        Me.TabPage3.Controls.Add(Me.TextBox1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(596, 343)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Drets"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Filter = Nothing
        Me.Xl_Contacts1.Location = New System.Drawing.Point(7, 72)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.Size = New System.Drawing.Size(581, 265)
        Me.Xl_Contacts1.TabIndex = 2
        '
        'Xl_Contact_Add1
        '
        Me.Xl_Contact_Add1.Location = New System.Drawing.Point(7, 45)
        Me.Xl_Contact_Add1.Name = "Xl_Contact_Add1"
        Me.Xl_Contact_Add1.Size = New System.Drawing.Size(581, 20)
        Me.Xl_Contact_Add1.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(4, 7)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(586, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Aquest usuari te drets d'accés a les dades dels següents contactes:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_RaffleParticipants1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(596, 343)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sortejos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_RaffleParticipants1
        '
        Me.Xl_RaffleParticipants1.DisplayObsolets = False
        Me.Xl_RaffleParticipants1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RaffleParticipants1.Filter = Nothing
        Me.Xl_RaffleParticipants1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RaffleParticipants1.MouseIsDown = False
        Me.Xl_RaffleParticipants1.Name = "Xl_RaffleParticipants1"
        Me.Xl_RaffleParticipants1.Size = New System.Drawing.Size(590, 337)
        Me.Xl_RaffleParticipants1.TabIndex = 0
        '
        'Frm_User
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 413)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_User"
        Me.Text = "Usuari"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_RaffleParticipants1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCognoms As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Sex1 As Winforms.Xl_Sex
    Friend WithEvents Xl_Country1 As Winforms.Xl_Country
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_ZipCod1 As Winforms.Xl_ZipCod
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTelefon As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchDeleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerFchActivated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxDeleted As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxActivated As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSource As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Yea1 As Winforms.Xl_Yea
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxNickName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RaffleParticipants1 As Winforms.Xl_RaffleParticipants2
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Contact_Add1 As Winforms.Xl_Contact_Add
    Friend WithEvents Xl_Contacts1 As Winforms.Xl_Contacts
    Friend WithEvents Xl_LookupRol1 As Winforms.Xl_LookupRol
    Friend WithEvents Xl_Langs1 As Winforms.Xl_Langs
End Class
