<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Importacio
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.CheckBoxDisabled = New System.Windows.Forms.CheckBox()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Xl_ContactPlataformaDeCarga = New Xl_Contact2()
        Me.CheckBoxOrdenDeCarga = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.DateTimePickerOrdenDeCarga = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePickerETD = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxGoods = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxArrived = New System.Windows.Forms.CheckBox()
        Me.TextBoxMatricula = New System.Windows.Forms.TextBox()
        Me.LabelUpDownWeek = New System.Windows.Forms.Label()
        Me.NumericUpDownWeek = New System.Windows.Forms.NumericUpDown()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_PaisOrigen = New Xl_Pais()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxAmt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_ContactTrp = New Xl_Contact2()
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.TextBoxKg = New System.Windows.Forms.TextBox()
        Me.TextBoxBultos = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerEta = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_ContactPrv = New Xl_Contact2()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPageDocs = New System.Windows.Forms.TabPage()
        Me.Xl_ImportacioDocs1 = New Xl_ImportacioDocs()
        Me.TabPagePrevisió = New System.Windows.Forms.TabPage()
        Me.Xl_ImportPrevisio1 = New Xl_ImportPrevisio()
        Me.PictureBoxLogoPrv = New System.Windows.Forms.PictureBox()
        Me.Xl_LookupIncoterm1 = New Xl_LookupIncoterm()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownWeek, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDocs.SuspendLayout()
        Me.TabPagePrevisió.SuspendLayout()
        CType(Me.Xl_ImportPrevisio1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogoPrv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 569)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Location = New System.Drawing.Point(4, 3)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 15
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(214, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(325, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 13
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageDocs)
        Me.TabControl1.Controls.Add(Me.TabPagePrevisió)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(433, 569)
        Me.TabControl1.TabIndex = 43
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.TabPageGral.Controls.Add(Me.CheckBoxDisabled)
        Me.TabPageGral.Controls.Add(Me.NumericUpDownYea)
        Me.TabPageGral.Controls.Add(Me.Label18)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactPlataformaDeCarga)
        Me.TabPageGral.Controls.Add(Me.CheckBoxOrdenDeCarga)
        Me.TabPageGral.Controls.Add(Me.Label17)
        Me.TabPageGral.Controls.Add(Me.DateTimePickerOrdenDeCarga)
        Me.TabPageGral.Controls.Add(Me.Label16)
        Me.TabPageGral.Controls.Add(Me.Label15)
        Me.TabPageGral.Controls.Add(Me.Label7)
        Me.TabPageGral.Controls.Add(Me.DateTimePickerETD)
        Me.TabPageGral.Controls.Add(Me.TextBoxGoods)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.CheckBoxArrived)
        Me.TabPageGral.Controls.Add(Me.TextBoxMatricula)
        Me.TabPageGral.Controls.Add(Me.LabelUpDownWeek)
        Me.TabPageGral.Controls.Add(Me.NumericUpDownWeek)
        Me.TabPageGral.Controls.Add(Me.Label14)
        Me.TabPageGral.Controls.Add(Me.Xl_PaisOrigen)
        Me.TabPageGral.Controls.Add(Me.Label13)
        Me.TabPageGral.Controls.Add(Me.Label12)
        Me.TabPageGral.Controls.Add(Me.TextBoxAmt)
        Me.TabPageGral.Controls.Add(Me.Label9)
        Me.TabPageGral.Controls.Add(Me.Label8)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactTrp)
        Me.TabPageGral.Controls.Add(Me.TextBoxM3)
        Me.TabPageGral.Controls.Add(Me.TextBoxKg)
        Me.TabPageGral.Controls.Add(Me.TextBoxBultos)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.TextBoxObs)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.DateTimePickerEta)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactPrv)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(425, 543)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "General"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'CheckBoxDisabled
        '
        Me.CheckBoxDisabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDisabled.AutoSize = True
        Me.CheckBoxDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDisabled.Location = New System.Drawing.Point(347, 422)
        Me.CheckBoxDisabled.Name = "CheckBoxDisabled"
        Me.CheckBoxDisabled.Size = New System.Drawing.Size(72, 17)
        Me.CheckBoxDisabled.TabIndex = 109
        Me.CheckBoxDisabled.Text = "Descartat"
        Me.CheckBoxDisabled.UseVisualStyleBackColor = True
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(369, 18)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDownYea.TabIndex = 108
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 124)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 13)
        Me.Label18.TabIndex = 107
        Me.Label18.Text = "Platf.de carga"
        '
        'Xl_ContactPlataformaDeCarga
        '
        Me.Xl_ContactPlataformaDeCarga.Contact = Nothing
        Me.Xl_ContactPlataformaDeCarga.Emp = Nothing
        Me.Xl_ContactPlataformaDeCarga.Location = New System.Drawing.Point(98, 124)
        Me.Xl_ContactPlataformaDeCarga.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactPlataformaDeCarga.Name = "Xl_ContactPlataformaDeCarga"
        Me.Xl_ContactPlataformaDeCarga.ReadOnly = False
        Me.Xl_ContactPlataformaDeCarga.Size = New System.Drawing.Size(300, 20)
        Me.Xl_ContactPlataformaDeCarga.TabIndex = 106
        '
        'CheckBoxOrdenDeCarga
        '
        Me.CheckBoxOrdenDeCarga.AutoSize = True
        Me.CheckBoxOrdenDeCarga.Location = New System.Drawing.Point(98, 188)
        Me.CheckBoxOrdenDeCarga.Name = "CheckBoxOrdenDeCarga"
        Me.CheckBoxOrdenDeCarga.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxOrdenDeCarga.TabIndex = 105
        Me.CheckBoxOrdenDeCarga.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 187)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 13)
        Me.Label17.TabIndex = 104
        Me.Label17.Text = "Ordre de Càrrega"
        '
        'DateTimePickerOrdenDeCarga
        '
        Me.DateTimePickerOrdenDeCarga.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.DateTimePickerOrdenDeCarga.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerOrdenDeCarga.Location = New System.Drawing.Point(119, 186)
        Me.DateTimePickerOrdenDeCarga.Name = "DateTimePickerOrdenDeCarga"
        Me.DateTimePickerOrdenDeCarga.Size = New System.Drawing.Size(128, 20)
        Me.DateTimePickerOrdenDeCarga.TabIndex = 103
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(9, 295)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 13)
        Me.Label16.TabIndex = 101
        Me.Label16.Text = "m3"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(9, 269)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(20, 13)
        Me.Label15.TabIndex = 100
        Me.Label15.Text = "Kg"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 99
        Me.Label7.Tag = "Estimated Time of Delivery"
        Me.Label7.Text = "ETD"
        '
        'DateTimePickerETD
        '
        Me.DateTimePickerETD.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.DateTimePickerETD.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerETD.Location = New System.Drawing.Point(98, 18)
        Me.DateTimePickerETD.Name = "DateTimePickerETD"
        Me.DateTimePickerETD.Size = New System.Drawing.Size(149, 20)
        Me.DateTimePickerETD.TabIndex = 98
        '
        'TextBoxGoods
        '
        Me.TextBoxGoods.Enabled = False
        Me.TextBoxGoods.Location = New System.Drawing.Point(98, 398)
        Me.TextBoxGoods.Name = "TextBoxGoods"
        Me.TextBoxGoods.ReadOnly = True
        Me.TextBoxGoods.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxGoods.TabIndex = 96
        Me.TextBoxGoods.TabStop = False
        Me.TextBoxGoods.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 401)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 97
        Me.Label6.Text = "mercancia:"
        '
        'CheckBoxArrived
        '
        Me.CheckBoxArrived.AutoSize = True
        Me.CheckBoxArrived.Location = New System.Drawing.Point(175, 73)
        Me.CheckBoxArrived.Name = "CheckBoxArrived"
        Me.CheckBoxArrived.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxArrived.TabIndex = 95
        Me.CheckBoxArrived.Text = "Arribat"
        Me.CheckBoxArrived.UseVisualStyleBackColor = True
        '
        'TextBoxMatricula
        '
        Me.TextBoxMatricula.Location = New System.Drawing.Point(98, 214)
        Me.TextBoxMatricula.MaxLength = 16
        Me.TextBoxMatricula.Name = "TextBoxMatricula"
        Me.TextBoxMatricula.Size = New System.Drawing.Size(149, 20)
        Me.TextBoxMatricula.TabIndex = 8
        '
        'LabelUpDownWeek
        '
        Me.LabelUpDownWeek.AutoSize = True
        Me.LabelUpDownWeek.Location = New System.Drawing.Point(8, 72)
        Me.LabelUpDownWeek.Name = "LabelUpDownWeek"
        Me.LabelUpDownWeek.Size = New System.Drawing.Size(90, 13)
        Me.LabelUpDownWeek.TabIndex = 94
        Me.LabelUpDownWeek.Text = "Previsió setmana:"
        '
        'NumericUpDownWeek
        '
        Me.NumericUpDownWeek.Location = New System.Drawing.Point(98, 70)
        Me.NumericUpDownWeek.Name = "NumericUpDownWeek"
        Me.NumericUpDownWeek.Size = New System.Drawing.Size(37, 20)
        Me.NumericUpDownWeek.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 217)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 13)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "Matrícula"
        '
        'Xl_PaisOrigen
        '
        Me.Xl_PaisOrigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_PaisOrigen.Country = Nothing
        Me.Xl_PaisOrigen.FlagVisible = False
        Me.Xl_PaisOrigen.Location = New System.Drawing.Point(98, 345)
        Me.Xl_PaisOrigen.Name = "Xl_PaisOrigen"
        Me.Xl_PaisOrigen.Size = New System.Drawing.Size(60, 21)
        Me.Xl_PaisOrigen.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 351)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 89
        Me.Label13.Text = "pais de origen"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 321)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 13)
        Me.Label12.TabIndex = 87
        Me.Label12.Text = "incoterm"
        '
        'TextBoxAmt
        '
        Me.TextBoxAmt.Enabled = False
        Me.TextBoxAmt.Location = New System.Drawing.Point(98, 372)
        Me.TextBoxAmt.Name = "TextBoxAmt"
        Me.TextBoxAmt.ReadOnly = True
        Me.TextBoxAmt.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxAmt.TabIndex = 79
        Me.TextBoxAmt.TabStop = False
        Me.TextBoxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 375)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 80
        Me.Label9.Text = "import"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 77
        Me.Label8.Text = "Transportista"
        '
        'Xl_ContactTrp
        '
        Me.Xl_ContactTrp.Contact = Nothing
        Me.Xl_ContactTrp.Emp = Nothing
        Me.Xl_ContactTrp.Location = New System.Drawing.Point(98, 150)
        Me.Xl_ContactTrp.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactTrp.Name = "Xl_ContactTrp"
        Me.Xl_ContactTrp.ReadOnly = False
        Me.Xl_ContactTrp.Size = New System.Drawing.Size(300, 20)
        Me.Xl_ContactTrp.TabIndex = 4
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(98, 292)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.Size = New System.Drawing.Size(60, 20)
        Me.TextBoxM3.TabIndex = 7
        Me.TextBoxM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxKg
        '
        Me.TextBoxKg.Location = New System.Drawing.Point(98, 266)
        Me.TextBoxKg.Name = "TextBoxKg"
        Me.TextBoxKg.Size = New System.Drawing.Size(60, 20)
        Me.TextBoxKg.TabIndex = 6
        Me.TextBoxKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxBultos
        '
        Me.TextBoxBultos.Location = New System.Drawing.Point(98, 240)
        Me.TextBoxBultos.Name = "TextBoxBultos"
        Me.TextBoxBultos.Size = New System.Drawing.Size(60, 20)
        Me.TextBoxBultos.TabIndex = 5
        Me.TextBoxBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 243)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "bultos"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(8, 445)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(411, 92)
        Me.TextBoxObs.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 429)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 13)
        Me.Label3.TabIndex = 73
        Me.Label3.Text = "Observacions (transportista, etc):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 70
        Me.Label4.Tag = "Estimated Time of Arrival"
        Me.Label4.Text = "ETA"
        '
        'DateTimePickerEta
        '
        Me.DateTimePickerEta.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.DateTimePickerEta.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEta.Location = New System.Drawing.Point(98, 44)
        Me.DateTimePickerEta.Name = "DateTimePickerEta"
        Me.DateTimePickerEta.Size = New System.Drawing.Size(149, 20)
        Me.DateTimePickerEta.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "Proveidor"
        '
        'Xl_ContactPrv
        '
        Me.Xl_ContactPrv.Contact = Nothing
        Me.Xl_ContactPrv.Emp = Nothing
        Me.Xl_ContactPrv.Location = New System.Drawing.Point(98, 99)
        Me.Xl_ContactPrv.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactPrv.Name = "Xl_ContactPrv"
        Me.Xl_ContactPrv.ReadOnly = False
        Me.Xl_ContactPrv.Size = New System.Drawing.Size(300, 20)
        Me.Xl_ContactPrv.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(335, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "any:"
        '
        'TabPageDocs
        '
        Me.TabPageDocs.Controls.Add(Me.Xl_ImportacioDocs1)
        Me.TabPageDocs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDocs.Name = "TabPageDocs"
        Me.TabPageDocs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDocs.Size = New System.Drawing.Size(425, 543)
        Me.TabPageDocs.TabIndex = 1
        Me.TabPageDocs.Text = "Documentació"
        Me.TabPageDocs.UseVisualStyleBackColor = True
        '
        'Xl_ImportacioDocs1
        '
        Me.Xl_ImportacioDocs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportacioDocs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ImportacioDocs1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ImportacioDocs1.Name = "Xl_ImportacioDocs1"
        Me.Xl_ImportacioDocs1.Size = New System.Drawing.Size(419, 537)
        Me.Xl_ImportacioDocs1.TabIndex = 3
        '
        'TabPagePrevisió
        '
        Me.TabPagePrevisió.Controls.Add(Me.Xl_ImportPrevisio1)
        Me.TabPagePrevisió.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePrevisió.Name = "TabPagePrevisió"
        Me.TabPagePrevisió.Size = New System.Drawing.Size(425, 543)
        Me.TabPagePrevisió.TabIndex = 3
        Me.TabPagePrevisió.Text = "Previsió"
        Me.TabPagePrevisió.UseVisualStyleBackColor = True
        '
        'Xl_ImportPrevisio1
        '
        Me.Xl_ImportPrevisio1.AllowUserToAddRows = False
        Me.Xl_ImportPrevisio1.AllowUserToDeleteRows = False
        Me.Xl_ImportPrevisio1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ImportPrevisio1.DisplayObsolets = False
        Me.Xl_ImportPrevisio1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportPrevisio1.Filter = Nothing
        Me.Xl_ImportPrevisio1.IsDirty = False
        Me.Xl_ImportPrevisio1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ImportPrevisio1.MouseIsDown = False
        Me.Xl_ImportPrevisio1.Name = "Xl_ImportPrevisio1"
        Me.Xl_ImportPrevisio1.ReadOnly = True
        Me.Xl_ImportPrevisio1.Size = New System.Drawing.Size(425, 543)
        Me.Xl_ImportPrevisio1.TabIndex = 0
        '
        'PictureBoxLogoPrv
        '
        Me.PictureBoxLogoPrv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogoPrv.Location = New System.Drawing.Point(275, 12)
        Me.PictureBoxLogoPrv.Name = "PictureBoxLogoPrv"
        Me.PictureBoxLogoPrv.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogoPrv.TabIndex = 45
        Me.PictureBoxLogoPrv.TabStop = False
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(98, 318)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(60, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 8
        Me.Xl_LookupIncoterm1.Value = Nothing
        '
        'Frm_Importacio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 600)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBoxLogoPrv)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Importacio"
        Me.Text = "Importacio"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownWeek, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDocs.ResumeLayout(False)
        Me.TabPagePrevisió.ResumeLayout(False)
        CType(Me.Xl_ImportPrevisio1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogoPrv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TabPageDocs As System.Windows.Forms.TabPage
    'Friend WithEvents TabPageValidació As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ImportacioDocs1 As Xl_ImportacioDocs
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Xl_PaisOrigen As Xl_Pais
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactTrp As Xl_Contact2
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxKg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxBultos As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerEta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactPrv As Xl_Contact2
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxLogoPrv As System.Windows.Forms.PictureBox
    Friend WithEvents TabPagePrevisió As TabPage
    Friend WithEvents Xl_ImportPrevisio1 As Xl_ImportPrevisio
    Friend WithEvents LabelUpDownWeek As Label
    Friend WithEvents NumericUpDownWeek As NumericUpDown
    ' Friend WithEvents Xl_ImportValidacions1 As Xl_ImportValidacions
    Friend WithEvents TextBoxMatricula As TextBox
    Friend WithEvents CheckBoxArrived As CheckBox
    Friend WithEvents TextBoxGoods As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePickerETD As DateTimePicker
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents CheckBoxOrdenDeCarga As CheckBox
    Friend WithEvents Label17 As Label
    Friend WithEvents DateTimePickerOrdenDeCarga As DateTimePicker
    Friend WithEvents Label18 As Label
    Friend WithEvents Xl_ContactPlataformaDeCarga As Xl_Contact2
    Friend WithEvents NumericUpDownYea As NumericUpDown
    Friend WithEvents CheckBoxDisabled As CheckBox
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
End Class
