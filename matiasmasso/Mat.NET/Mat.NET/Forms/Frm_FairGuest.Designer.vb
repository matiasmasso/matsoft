<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FairGuest
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
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxFirstName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxLastName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPosition = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNIF = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxRaoSocial = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxActivityCode = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxAddress = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxLocation = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_Country1 = New Mat.NET.Xl_Country()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxPhone = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxCellPhone = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxFax = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBoxFchCreated = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.RadioButtonShortDistance = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLongDistance = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxWeb = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 553)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(461, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(242, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(353, 4)
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
        'TextBoxFirstName
        '
        Me.TextBoxFirstName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFirstName.Location = New System.Drawing.Point(77, 29)
        Me.TextBoxFirstName.MaxLength = 50
        Me.TextBoxFirstName.Name = "TextBoxFirstName"
        Me.TextBoxFirstName.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxFirstName.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom"
        '
        'TextBoxLastName
        '
        Me.TextBoxLastName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLastName.Location = New System.Drawing.Point(77, 55)
        Me.TextBoxLastName.MaxLength = 60
        Me.TextBoxLastName.Name = "TextBoxLastName"
        Me.TextBoxLastName.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxLastName.TabIndex = 53
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Cognoms"
        '
        'TextBoxPosition
        '
        Me.TextBoxPosition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPosition.Location = New System.Drawing.Point(77, 81)
        Me.TextBoxPosition.MaxLength = 50
        Me.TextBoxPosition.Name = "TextBoxPosition"
        Me.TextBoxPosition.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxPosition.TabIndex = 55
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Carrec"
        '
        'TextBoxNIF
        '
        Me.TextBoxNIF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNIF.Location = New System.Drawing.Point(77, 107)
        Me.TextBoxNIF.MaxLength = 12
        Me.TextBoxNIF.Name = "TextBoxNIF"
        Me.TextBoxNIF.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxNIF.TabIndex = 57
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "NIF"
        '
        'TextBoxRaoSocial
        '
        Me.TextBoxRaoSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRaoSocial.Location = New System.Drawing.Point(77, 133)
        Me.TextBoxRaoSocial.MaxLength = 60
        Me.TextBoxRaoSocial.Name = "TextBoxRaoSocial"
        Me.TextBoxRaoSocial.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxRaoSocial.TabIndex = 59
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "RaoSocial"
        '
        'ComboBoxActivityCode
        '
        Me.ComboBoxActivityCode.FormattingEnabled = True
        Me.ComboBoxActivityCode.Items.AddRange(New Object() {"(seleccionar activitat)", "Botiga física", "Botiga online", "Cap de compres de franquicia o grans magatzems", "Altres"})
        Me.ComboBoxActivityCode.Location = New System.Drawing.Point(77, 160)
        Me.ComboBoxActivityCode.Name = "ComboBoxActivityCode"
        Me.ComboBoxActivityCode.Size = New System.Drawing.Size(372, 21)
        Me.ComboBoxActivityCode.TabIndex = 60
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Activitat"
        '
        'TextBoxAddress
        '
        Me.TextBoxAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAddress.Location = New System.Drawing.Point(77, 187)
        Me.TextBoxAddress.MaxLength = 60
        Me.TextBoxAddress.Name = "TextBoxAddress"
        Me.TextBoxAddress.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxAddress.TabIndex = 63
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 189)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 62
        Me.Label7.Text = "Adreça"
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxZip.Location = New System.Drawing.Point(77, 213)
        Me.TextBoxZip.MaxLength = 5
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxZip.TabIndex = 65
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 215)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Codi Postal"
        '
        'TextBoxLocation
        '
        Me.TextBoxLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLocation.Location = New System.Drawing.Point(77, 239)
        Me.TextBoxLocation.MaxLength = 60
        Me.TextBoxLocation.Name = "TextBoxLocation"
        Me.TextBoxLocation.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxLocation.TabIndex = 67
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 241)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Població"
        '
        'Xl_Country1
        '
        Me.Xl_Country1.Country = Nothing
        Me.Xl_Country1.IsDirty = False
        Me.Xl_Country1.Location = New System.Drawing.Point(77, 266)
        Me.Xl_Country1.Name = "Xl_Country1"
        Me.Xl_Country1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Country1.Size = New System.Drawing.Size(372, 20)
        Me.Xl_Country1.TabIndex = 68
        Me.Xl_Country1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(27, 13)
        Me.Label10.TabIndex = 69
        Me.Label10.Text = "Pais"
        '
        'TextBoxPhone
        '
        Me.TextBoxPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPhone.Location = New System.Drawing.Point(77, 364)
        Me.TextBoxPhone.MaxLength = 60
        Me.TextBoxPhone.Name = "TextBoxPhone"
        Me.TextBoxPhone.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxPhone.TabIndex = 71
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 366)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 13)
        Me.Label11.TabIndex = 70
        Me.Label11.Text = "Telefon"
        '
        'TextBoxCellPhone
        '
        Me.TextBoxCellPhone.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCellPhone.Location = New System.Drawing.Point(77, 390)
        Me.TextBoxCellPhone.MaxLength = 50
        Me.TextBoxCellPhone.Name = "TextBoxCellPhone"
        Me.TextBoxCellPhone.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxCellPhone.TabIndex = 73
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 392)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 72
        Me.Label12.Text = "Movil"
        '
        'TextBoxFax
        '
        Me.TextBoxFax.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFax.Location = New System.Drawing.Point(77, 416)
        Me.TextBoxFax.MaxLength = 50
        Me.TextBoxFax.Name = "TextBoxFax"
        Me.TextBoxFax.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxFax.TabIndex = 75
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 418)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 13)
        Me.Label13.TabIndex = 74
        Me.Label13.Text = "Fax"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(77, 442)
        Me.TextBoxEmail.MaxLength = 100
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxEmail.TabIndex = 77
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 444)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 13)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "email"
        '
        'TextBoxFchCreated
        '
        Me.TextBoxFchCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFchCreated.Enabled = False
        Me.TextBoxFchCreated.Location = New System.Drawing.Point(77, 504)
        Me.TextBoxFchCreated.Name = "TextBoxFchCreated"
        Me.TextBoxFchCreated.ReadOnly = True
        Me.TextBoxFchCreated.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxFchCreated.TabIndex = 79
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 506)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 13)
        Me.Label15.TabIndex = 78
        Me.Label15.Text = "data"
        '
        'RadioButtonShortDistance
        '
        Me.RadioButtonShortDistance.AutoSize = True
        Me.RadioButtonShortDistance.Location = New System.Drawing.Point(70, 19)
        Me.RadioButtonShortDistance.Name = "RadioButtonShortDistance"
        Me.RadioButtonShortDistance.Size = New System.Drawing.Size(155, 17)
        Me.RadioButtonShortDistance.TabIndex = 80
        Me.RadioButtonShortDistance.TabStop = True
        Me.RadioButtonShortDistance.Text = "Resido a menos de 200Km "
        Me.RadioButtonShortDistance.UseVisualStyleBackColor = True
        '
        'RadioButtonLongDistance
        '
        Me.RadioButtonLongDistance.AutoSize = True
        Me.RadioButtonLongDistance.Location = New System.Drawing.Point(70, 42)
        Me.RadioButtonLongDistance.Name = "RadioButtonLongDistance"
        Me.RadioButtonLongDistance.Size = New System.Drawing.Size(143, 17)
        Me.RadioButtonLongDistance.TabIndex = 81
        Me.RadioButtonLongDistance.TabStop = True
        Me.RadioButtonLongDistance.Text = "Resido a más de 200Km "
        Me.RadioButtonLongDistance.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonShortDistance)
        Me.GroupBox1.Controls.Add(Me.RadioButtonLongDistance)
        Me.GroupBox1.Location = New System.Drawing.Point(77, 292)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(372, 65)
        Me.GroupBox1.TabIndex = 82
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Distancia de residencia de Madrid"
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWeb.Location = New System.Drawing.Point(77, 468)
        Me.TextBoxWeb.MaxLength = 100
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxWeb.TabIndex = 84
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 470)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(27, 13)
        Me.Label16.TabIndex = 83
        Me.Label16.Text = "web"
        '
        'Frm_FairGuest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(461, 584)
        Me.Controls.Add(Me.TextBoxWeb)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBoxFchCreated)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBoxFax)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TextBoxCellPhone)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextBoxPhone)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_Country1)
        Me.Controls.Add(Me.TextBoxLocation)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxZip)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxAddress)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxActivityCode)
        Me.Controls.Add(Me.TextBoxRaoSocial)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxNIF)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxPosition)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxLastName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxFirstName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_FairGuest"
        Me.Text = "Convidat Fira"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPosition As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNIF As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRaoSocial As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxActivityCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxZip As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_Country1 As Mat.NET.Xl_Country
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCellPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFax As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFchCreated As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonShortDistance As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonLongDistance As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxWeb As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
