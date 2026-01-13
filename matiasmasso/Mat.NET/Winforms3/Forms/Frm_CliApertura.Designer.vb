<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliApertura
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxWeb = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxCit = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxNIF = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxNomComercial = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxRaoSocial = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchCreated = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ComboBoxCodExperiencia = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ComboBoxCodAntiguedad = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ComboBoxCodSalePoints = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxCodVolum = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxCodSuperficie = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupArea1 = New Xl_LookupArea()
        Me.Xl_ProductBrands1 = New Xl_ProductBrands()
        Me.Xl_LookupContactClass1 = New Xl_LookupContactClass()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 393)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(399, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(180, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(291, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 19)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(399, 369)
        Me.TabControl1.TabIndex = 74
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Xl_LookupContactClass1)
        Me.TabPage1.Controls.Add(Me.TextBoxWeb)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.TextBoxTel)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Xl_LookupArea1)
        Me.TabPage1.Controls.Add(Me.TextBoxCit)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxZip)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxAdr)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxNIF)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxNomComercial)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxRaoSocial)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchCreated)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage1.Size = New System.Drawing.Size(391, 343)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWeb.Location = New System.Drawing.Point(100, 260)
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxWeb.TabIndex = 98
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(20, 262)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(30, 13)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "Web"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(100, 239)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxEmail.TabIndex = 96
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 240)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 95
        Me.Label12.Text = "Email"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Location = New System.Drawing.Point(100, 217)
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxTel.TabIndex = 94
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(20, 218)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 13)
        Me.Label11.TabIndex = 93
        Me.Label11.Text = "Tel"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 200)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 92
        Me.Label10.Text = "Zona"
        '
        'TextBoxCit
        '
        Me.TextBoxCit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCit.Location = New System.Drawing.Point(100, 173)
        Me.TextBoxCit.Name = "TextBoxCit"
        Me.TextBoxCit.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxCit.TabIndex = 90
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 177)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 89
        Me.Label9.Text = "Població"
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Location = New System.Drawing.Point(100, 151)
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxZip.TabIndex = 88
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(20, 155)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 87
        Me.Label8.Text = "Codi Postal"
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAdr.Location = New System.Drawing.Point(100, 130)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxAdr.TabIndex = 86
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "Adreça"
        '
        'TextBoxNIF
        '
        Me.TextBoxNIF.Location = New System.Drawing.Point(100, 108)
        Me.TextBoxNIF.Name = "TextBoxNIF"
        Me.TextBoxNIF.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxNIF.TabIndex = 84
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 83
        Me.Label6.Text = "NIF"
        '
        'TextBoxNomComercial
        '
        Me.TextBoxNomComercial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomComercial.Location = New System.Drawing.Point(100, 86)
        Me.TextBoxNomComercial.Name = "TextBoxNomComercial"
        Me.TextBoxNomComercial.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxNomComercial.TabIndex = 82
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "Nom Comercial"
        '
        'TextBoxRaoSocial
        '
        Me.TextBoxRaoSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRaoSocial.Location = New System.Drawing.Point(100, 64)
        Me.TextBoxRaoSocial.Name = "TextBoxRaoSocial"
        Me.TextBoxRaoSocial.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxRaoSocial.TabIndex = 80
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 79
        Me.Label4.Text = "Rao social"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "data"
        '
        'DateTimePickerFchCreated
        '
        Me.DateTimePickerFchCreated.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchCreated.Location = New System.Drawing.Point(100, 22)
        Me.DateTimePickerFchCreated.Name = "DateTimePickerFchCreated"
        Me.DateTimePickerFchCreated.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchCreated.TabIndex = 76
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(100, 42)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxNom.TabIndex = 75
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Contacte"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ProductBrands1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage2.Size = New System.Drawing.Size(391, 343)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Marques"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label16)
        Me.TabPage3.Controls.Add(Me.ComboBoxCodExperiencia)
        Me.TabPage3.Controls.Add(Me.Label15)
        Me.TabPage3.Controls.Add(Me.ComboBoxCodAntiguedad)
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Controls.Add(Me.ComboBoxCodSalePoints)
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Controls.Add(Me.ComboBoxCodVolum)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.ComboBoxCodSuperficie)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TabPage3.Size = New System.Drawing.Size(391, 343)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Característiques"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 99)
        Me.Label16.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(62, 13)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "Experiencia"
        '
        'ComboBoxCodExperiencia
        '
        Me.ComboBoxCodExperiencia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCodExperiencia.FormattingEnabled = True
        Me.ComboBoxCodExperiencia.Location = New System.Drawing.Point(96, 97)
        Me.ComboBoxCodExperiencia.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxCodExperiencia.Name = "ComboBoxCodExperiencia"
        Me.ComboBoxCodExperiencia.Size = New System.Drawing.Size(286, 21)
        Me.ComboBoxCodExperiencia.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 80)
        Me.Label15.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(51, 13)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Antiguitat"
        '
        'ComboBoxCodAntiguedad
        '
        Me.ComboBoxCodAntiguedad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCodAntiguedad.FormattingEnabled = True
        Me.ComboBoxCodAntiguedad.Location = New System.Drawing.Point(96, 78)
        Me.ComboBoxCodAntiguedad.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxCodAntiguedad.Name = "ComboBoxCodAntiguedad"
        Me.ComboBoxCodAntiguedad.Size = New System.Drawing.Size(286, 21)
        Me.ComboBoxCodAntiguedad.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 61)
        Me.Label14.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Punts de venda"
        '
        'ComboBoxCodSalePoints
        '
        Me.ComboBoxCodSalePoints.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCodSalePoints.FormattingEnabled = True
        Me.ComboBoxCodSalePoints.Location = New System.Drawing.Point(96, 60)
        Me.ComboBoxCodSalePoints.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxCodSalePoints.Name = "ComboBoxCodSalePoints"
        Me.ComboBoxCodSalePoints.Size = New System.Drawing.Size(286, 21)
        Me.ComboBoxCodSalePoints.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 42)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(36, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Volum"
        '
        'ComboBoxCodVolum
        '
        Me.ComboBoxCodVolum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCodVolum.FormattingEnabled = True
        Me.ComboBoxCodVolum.Location = New System.Drawing.Point(96, 41)
        Me.ComboBoxCodVolum.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxCodVolum.Name = "ComboBoxCodVolum"
        Me.ComboBoxCodVolum.Size = New System.Drawing.Size(286, 21)
        Me.ComboBoxCodVolum.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 23)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Superficie"
        '
        'ComboBoxCodSuperficie
        '
        Me.ComboBoxCodSuperficie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCodSuperficie.FormattingEnabled = True
        Me.ComboBoxCodSuperficie.Location = New System.Drawing.Point(96, 22)
        Me.ComboBoxCodSuperficie.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.ComboBoxCodSuperficie.Name = "ComboBoxCodSuperficie"
        Me.ComboBoxCodSuperficie.Size = New System.Drawing.Size(286, 21)
        Me.ComboBoxCodSuperficie.TabIndex = 0
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(100, 194)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(284, 20)
        Me.Xl_LookupArea1.TabIndex = 91
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Xl_ProductBrands1
        '
        Me.Xl_ProductBrands1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductBrands1.Location = New System.Drawing.Point(1, 1)
        Me.Xl_ProductBrands1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductBrands1.Name = "Xl_ProductBrands1"
        Me.Xl_ProductBrands1.ShowObsolets = False
        Me.Xl_ProductBrands1.Size = New System.Drawing.Size(389, 341)
        Me.Xl_ProductBrands1.TabIndex = 54
        '
        'Xl_LookupContactClass1
        '
        Me.Xl_LookupContactClass1.ContactClass = Nothing
        Me.Xl_LookupContactClass1.IsDirty = False
        Me.Xl_LookupContactClass1.Location = New System.Drawing.Point(100, 282)
        Me.Xl_LookupContactClass1.Name = "Xl_LookupContactClass1"
        Me.Xl_LookupContactClass1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupContactClass1.Size = New System.Drawing.Size(286, 20)
        Me.Xl_LookupContactClass1.TabIndex = 99
        Me.Xl_LookupContactClass1.Value = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(20, 285)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 13)
        Me.Label18.TabIndex = 100
        Me.Label18.Text = "Activitat"
        '
        'Frm_CliApertura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 424)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_CliApertura"
        Me.Text = "Apertura Clients"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_ProductBrands1 As Xl_ProductBrands
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBoxTel As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents TextBoxCit As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxZip As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxNIF As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxNomComercial As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxRaoSocial As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerFchCreated As DateTimePicker
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label14 As Label
    Friend WithEvents ComboBoxCodSalePoints As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ComboBoxCodVolum As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxCodSuperficie As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents ComboBoxCodExperiencia As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents ComboBoxCodAntiguedad As ComboBox
    Friend WithEvents TextBoxWeb As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Xl_LookupContactClass1 As Xl_LookupContactClass
End Class
