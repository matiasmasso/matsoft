

Public Class Xl_Contact_Cli
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents CheckBoxFacturarA As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactCcx As Xl_Contact
    Friend WithEvents TextBoxRef As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckboxOrdersToCentral As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDownCopiasFra As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxReq As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxValorarAlbarans As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxAdr As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Adr1 As Xl_Adr
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDpp As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxTarifa As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxProvNum As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxWarnAlbs As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxWarnAlbs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObsComercials As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxNoSsc As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoRep As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxPortsCondicions As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPorts As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Xl_FormaDePago1 As Xl_FormaDePago
    Friend WithEvents GroupBoxCYC As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxLimit As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxAlbsXFra As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ButtonCYC As System.Windows.Forms.Button
    Friend WithEvents ButtonPortsCondicions As System.Windows.Forms.Button
    Friend WithEvents CheckBoxDtoFixe As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonDtoFixe As System.Windows.Forms.Button
    Friend WithEvents CheckBoxGrandesCuentas As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBoxTarifasExcelLinks As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxTarifasExcel As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxNoWeb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFrasIndepents As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFpgIndepent As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoIncentius As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDisponible As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDisposat As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoASNEF As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxEShopOnly As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxQuotaOnline As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactPlatform As Mat.NET.Xl_Contact
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCash As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CheckBoxFacturarA = New System.Windows.Forms.CheckBox()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckboxOrdersToCentral = New System.Windows.Forms.CheckBox()
        Me.NumericUpDownCopiasFra = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.CheckBoxReq = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxValorarAlbarans = New System.Windows.Forms.CheckBox()
        Me.CheckBoxAdr = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxDpp = New System.Windows.Forms.TextBox()
        Me.ComboBoxTarifa = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxProvNum = New System.Windows.Forms.TextBox()
        Me.CheckBoxWarnAlbs = New System.Windows.Forms.CheckBox()
        Me.TextBoxWarnAlbs = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxObsComercials = New System.Windows.Forms.TextBox()
        Me.CheckBoxNoSsc = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoRep = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBoxPortsCondicions = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPorts = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxCash = New System.Windows.Forms.ComboBox()
        Me.GroupBoxCYC = New System.Windows.Forms.GroupBox()
        Me.CheckBoxNoASNEF = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxDisponible = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxDisposat = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonCYC = New System.Windows.Forms.Button()
        Me.TextBoxLimit = New System.Windows.Forms.TextBox()
        Me.ComboBoxAlbsXFra = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ButtonPortsCondicions = New System.Windows.Forms.Button()
        Me.CheckBoxDtoFixe = New System.Windows.Forms.CheckBox()
        Me.ButtonDtoFixe = New System.Windows.Forms.Button()
        Me.CheckBoxGrandesCuentas = New System.Windows.Forms.CheckBox()
        Me.PictureBoxTarifasExcelLinks = New System.Windows.Forms.PictureBox()
        Me.PictureBoxTarifasExcel = New System.Windows.Forms.PictureBox()
        Me.CheckBoxNoWeb = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFrasIndepents = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFpgIndepent = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoIncentius = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEShopOnly = New System.Windows.Forms.CheckBox()
        Me.Xl_FormaDePago1 = New Mat.NET.Xl_FormaDePago()
        Me.Xl_Adr1 = New Mat.NET.Xl_Adr()
        Me.Xl_ContactCcx = New Mat.NET.Xl_Contact()
        Me.ComboBoxQuotaOnline = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_ContactPlatform = New Mat.NET.Xl_Contact()
        Me.Label15 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDownCopiasFra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxCYC.SuspendLayout()
        CType(Me.PictureBoxTarifasExcelLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxTarifasExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckBoxFacturarA
        '
        Me.CheckBoxFacturarA.Location = New System.Drawing.Point(8, 8)
        Me.CheckBoxFacturarA.Name = "CheckBoxFacturarA"
        Me.CheckBoxFacturarA.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxFacturarA.TabIndex = 0
        Me.CheckBoxFacturarA.Text = "Facturar a..."
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRef.Location = New System.Drawing.Point(97, 29)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxRef.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(28, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "referencia:"
        '
        'CheckboxOrdersToCentral
        '
        Me.CheckboxOrdersToCentral.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckboxOrdersToCentral.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckboxOrdersToCentral.Location = New System.Drawing.Point(352, 8)
        Me.CheckboxOrdersToCentral.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.CheckboxOrdersToCentral.Name = "CheckboxOrdersToCentral"
        Me.CheckboxOrdersToCentral.Size = New System.Drawing.Size(128, 16)
        Me.CheckboxOrdersToCentral.TabIndex = 4
        Me.CheckboxOrdersToCentral.TabStop = False
        Me.CheckboxOrdersToCentral.Text = "Comandes a central"
        '
        'NumericUpDownCopiasFra
        '
        Me.NumericUpDownCopiasFra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownCopiasFra.Location = New System.Drawing.Point(248, 272)
        Me.NumericUpDownCopiasFra.Name = "NumericUpDownCopiasFra"
        Me.NumericUpDownCopiasFra.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDownCopiasFra.TabIndex = 18
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.Location = New System.Drawing.Point(8, 56)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxIVA.TabIndex = 9
        Me.CheckBoxIVA.Text = "IVA"
        '
        'CheckBoxReq
        '
        Me.CheckBoxReq.Location = New System.Drawing.Point(8, 72)
        Me.CheckBoxReq.Name = "CheckBoxReq"
        Me.CheckBoxReq.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxReq.TabIndex = 11
        Me.CheckBoxReq.Text = "Recàrrec equivalencia"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(184, 275)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Copies fra.:"
        '
        'CheckBoxValorarAlbarans
        '
        Me.CheckBoxValorarAlbarans.Location = New System.Drawing.Point(239, 56)
        Me.CheckBoxValorarAlbarans.Name = "CheckBoxValorarAlbarans"
        Me.CheckBoxValorarAlbarans.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxValorarAlbarans.TabIndex = 10
        Me.CheckBoxValorarAlbarans.Text = "Albará valorat"
        '
        'CheckBoxAdr
        '
        Me.CheckBoxAdr.Location = New System.Drawing.Point(8, 133)
        Me.CheckBoxAdr.Name = "CheckBoxAdr"
        Me.CheckBoxAdr.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxAdr.TabIndex = 12
        Me.CheckBoxAdr.Text = "Adr.entregues:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 296)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 16)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "descompte pp.:"
        '
        'TextBoxDpp
        '
        Me.TextBoxDpp.Location = New System.Drawing.Point(96, 296)
        Me.TextBoxDpp.Name = "TextBoxDpp"
        Me.TextBoxDpp.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxDpp.TabIndex = 20
        '
        'ComboBoxTarifa
        '
        Me.ComboBoxTarifa.FormattingEnabled = True
        Me.ComboBoxTarifa.Items.AddRange(New Object() {"", "Standard", "Comerç electronic", "especial"})
        Me.ComboBoxTarifa.Location = New System.Drawing.Point(96, 235)
        Me.ComboBoxTarifa.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        Me.ComboBoxTarifa.Name = "ComboBoxTarifa"
        Me.ComboBoxTarifa.Size = New System.Drawing.Size(87, 21)
        Me.ComboBoxTarifa.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(3, 235)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 16)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "tarifa:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Location = New System.Drawing.Point(144, 296)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "prov.num:"
        '
        'TextBoxProvNum
        '
        Me.TextBoxProvNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxProvNum.Location = New System.Drawing.Point(197, 296)
        Me.TextBoxProvNum.Name = "TextBoxProvNum"
        Me.TextBoxProvNum.Size = New System.Drawing.Size(91, 20)
        Me.TextBoxProvNum.TabIndex = 22
        '
        'CheckBoxWarnAlbs
        '
        Me.CheckBoxWarnAlbs.Location = New System.Drawing.Point(8, 320)
        Me.CheckBoxWarnAlbs.Name = "CheckBoxWarnAlbs"
        Me.CheckBoxWarnAlbs.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxWarnAlbs.TabIndex = 23
        Me.CheckBoxWarnAlbs.Text = "Advert.albs:"
        '
        'TextBoxWarnAlbs
        '
        Me.TextBoxWarnAlbs.BackColor = System.Drawing.Color.LightSalmon
        Me.TextBoxWarnAlbs.Location = New System.Drawing.Point(96, 320)
        Me.TextBoxWarnAlbs.Name = "TextBoxWarnAlbs"
        Me.TextBoxWarnAlbs.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxWarnAlbs.TabIndex = 24
        Me.TextBoxWarnAlbs.Visible = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 344)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 16)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Observacions:"
        '
        'TextBoxObsComercials
        '
        Me.TextBoxObsComercials.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObsComercials.Location = New System.Drawing.Point(96, 336)
        Me.TextBoxObsComercials.Name = "TextBoxObsComercials"
        Me.TextBoxObsComercials.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxObsComercials.TabIndex = 26
        '
        'CheckBoxNoSsc
        '
        Me.CheckBoxNoSsc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoSsc.Location = New System.Drawing.Point(352, 25)
        Me.CheckBoxNoSsc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.CheckBoxNoSsc.Name = "CheckBoxNoSsc"
        Me.CheckBoxNoSsc.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoSsc.TabIndex = 4
        Me.CheckBoxNoSsc.TabStop = False
        Me.CheckBoxNoSsc.Text = "No Subscripcions"
        '
        'CheckBoxNoRep
        '
        Me.CheckBoxNoRep.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoRep.Location = New System.Drawing.Point(352, 41)
        Me.CheckBoxNoRep.Name = "CheckBoxNoRep"
        Me.CheckBoxNoRep.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoRep.TabIndex = 6
        Me.CheckBoxNoRep.TabStop = False
        Me.CheckBoxNoRep.Text = "No Rep"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(232, 211)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 16)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "condicions:"
        '
        'ComboBoxPortsCondicions
        '
        Me.ComboBoxPortsCondicions.FormattingEnabled = True
        Me.ComboBoxPortsCondicions.Location = New System.Drawing.Point(296, 211)
        Me.ComboBoxPortsCondicions.Margin = New System.Windows.Forms.Padding(3, 3, 1, 2)
        Me.ComboBoxPortsCondicions.Name = "ComboBoxPortsCondicions"
        Me.ComboBoxPortsCondicions.Size = New System.Drawing.Size(162, 21)
        Me.ComboBoxPortsCondicions.TabIndex = 12
        '
        'ComboBoxPorts
        '
        Me.ComboBoxPorts.FormattingEnabled = True
        Me.ComboBoxPorts.Items.AddRange(New Object() {"(modalitat de ports)", "PAGATS", "DEGUTS", "RECULLIRAN", "ALTRES"})
        Me.ComboBoxPorts.Location = New System.Drawing.Point(96, 211)
        Me.ComboBoxPorts.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.ComboBoxPorts.Name = "ComboBoxPorts"
        Me.ComboBoxPorts.Size = New System.Drawing.Size(128, 21)
        Me.ComboBoxPorts.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(4, 211)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 16)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "ports:"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(232, 235)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 16)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "pagament:"
        '
        'ComboBoxCash
        '
        Me.ComboBoxCash.FormattingEnabled = True
        Me.ComboBoxCash.Items.AddRange(New Object() {"(modalitat credit)", "CREDIT", "REEMBOLS", "TRANSF.PREVIA"})
        Me.ComboBoxCash.Location = New System.Drawing.Point(296, 235)
        Me.ComboBoxCash.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ComboBoxCash.Name = "ComboBoxCash"
        Me.ComboBoxCash.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxCash.TabIndex = 14
        '
        'GroupBoxCYC
        '
        Me.GroupBoxCYC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCYC.Controls.Add(Me.CheckBoxNoASNEF)
        Me.GroupBoxCYC.Controls.Add(Me.Label9)
        Me.GroupBoxCYC.Controls.Add(Me.TextBoxDisponible)
        Me.GroupBoxCYC.Controls.Add(Me.Label8)
        Me.GroupBoxCYC.Controls.Add(Me.TextBoxDisposat)
        Me.GroupBoxCYC.Controls.Add(Me.Label3)
        Me.GroupBoxCYC.Controls.Add(Me.ButtonCYC)
        Me.GroupBoxCYC.Controls.Add(Me.TextBoxLimit)
        Me.GroupBoxCYC.Location = New System.Drawing.Point(296, 397)
        Me.GroupBoxCYC.Name = "GroupBoxCYC"
        Me.GroupBoxCYC.Size = New System.Drawing.Size(180, 121)
        Me.GroupBoxCYC.TabIndex = 31
        Me.GroupBoxCYC.TabStop = False
        Me.GroupBoxCYC.Text = "gestió de risc"
        '
        'CheckBoxNoASNEF
        '
        Me.CheckBoxNoASNEF.AutoSize = True
        Me.CheckBoxNoASNEF.Location = New System.Drawing.Point(102, 98)
        Me.CheckBoxNoASNEF.Name = "CheckBoxNoASNEF"
        Me.CheckBoxNoASNEF.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxNoASNEF.TabIndex = 11
        Me.CheckBoxNoASNEF.Text = "No ASNEF"
        Me.CheckBoxNoASNEF.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "disponible"
        '
        'TextBoxDisponible
        '
        Me.TextBoxDisponible.Location = New System.Drawing.Point(65, 74)
        Me.TextBoxDisponible.Name = "TextBoxDisponible"
        Me.TextBoxDisponible.ReadOnly = True
        Me.TextBoxDisponible.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxDisponible.TabIndex = 9
        Me.TextBoxDisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "disposat"
        '
        'TextBoxDisposat
        '
        Me.TextBoxDisposat.Location = New System.Drawing.Point(65, 49)
        Me.TextBoxDisposat.Name = "TextBoxDisposat"
        Me.TextBoxDisposat.ReadOnly = True
        Me.TextBoxDisposat.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxDisposat.TabIndex = 7
        Me.TextBoxDisposat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "limit"
        '
        'ButtonCYC
        '
        Me.ButtonCYC.Location = New System.Drawing.Point(150, 25)
        Me.ButtonCYC.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.ButtonCYC.Name = "ButtonCYC"
        Me.ButtonCYC.Size = New System.Drawing.Size(24, 20)
        Me.ButtonCYC.TabIndex = 5
        Me.ButtonCYC.Text = "..."
        '
        'TextBoxLimit
        '
        Me.TextBoxLimit.Location = New System.Drawing.Point(65, 25)
        Me.TextBoxLimit.Name = "TextBoxLimit"
        Me.TextBoxLimit.ReadOnly = True
        Me.TextBoxLimit.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxLimit.TabIndex = 1
        Me.TextBoxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ComboBoxAlbsXFra
        '
        Me.ComboBoxAlbsXFra.FormattingEnabled = True
        Me.ComboBoxAlbsXFra.Items.AddRange(New Object() {"(seleccionar agrupació albs en fra)", "Una sola factura mensual", "Juntar albarans", "Juntar només albarans petits", "Factura per albará"})
        Me.ComboBoxAlbsXFra.Location = New System.Drawing.Point(97, 361)
        Me.ComboBoxAlbsXFra.Margin = New System.Windows.Forms.Padding(1, 0, 3, 3)
        Me.ComboBoxAlbsXFra.Name = "ComboBoxAlbsXFra"
        Me.ComboBoxAlbsXFra.Size = New System.Drawing.Size(191, 21)
        Me.ComboBoxAlbsXFra.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(8, 366)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 3, 0, 2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 16)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Facturació:"
        '
        'ButtonPortsCondicions
        '
        Me.ButtonPortsCondicions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPortsCondicions.Location = New System.Drawing.Point(460, 211)
        Me.ButtonPortsCondicions.Margin = New System.Windows.Forms.Padding(1, 3, 3, 1)
        Me.ButtonPortsCondicions.Name = "ButtonPortsCondicions"
        Me.ButtonPortsCondicions.Size = New System.Drawing.Size(21, 21)
        Me.ButtonPortsCondicions.TabIndex = 14
        Me.ButtonPortsCondicions.Text = "..."
        '
        'CheckBoxDtoFixe
        '
        Me.CheckBoxDtoFixe.AutoSize = True
        Me.CheckBoxDtoFixe.Location = New System.Drawing.Point(8, 274)
        Me.CheckBoxDtoFixe.Name = "CheckBoxDtoFixe"
        Me.CheckBoxDtoFixe.Size = New System.Drawing.Size(99, 17)
        Me.CheckBoxDtoFixe.TabIndex = 15
        Me.CheckBoxDtoFixe.Text = "Descompte fixe"
        '
        'ButtonDtoFixe
        '
        Me.ButtonDtoFixe.Location = New System.Drawing.Point(106, 271)
        Me.ButtonDtoFixe.Name = "ButtonDtoFixe"
        Me.ButtonDtoFixe.Size = New System.Drawing.Size(30, 18)
        Me.ButtonDtoFixe.TabIndex = 16
        Me.ButtonDtoFixe.Text = "..."
        Me.ButtonDtoFixe.Visible = False
        '
        'CheckBoxGrandesCuentas
        '
        Me.CheckBoxGrandesCuentas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxGrandesCuentas.Location = New System.Drawing.Point(352, 57)
        Me.CheckBoxGrandesCuentas.Name = "CheckBoxGrandesCuentas"
        Me.CheckBoxGrandesCuentas.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxGrandesCuentas.TabIndex = 7
        Me.CheckBoxGrandesCuentas.TabStop = False
        Me.CheckBoxGrandesCuentas.Text = "Grandes Cuentas"
        '
        'PictureBoxTarifasExcelLinks
        '
        Me.PictureBoxTarifasExcelLinks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxTarifasExcelLinks.Image = Global.Mat.NET.My.Resources.Resources.Hyperlink
        Me.PictureBoxTarifasExcelLinks.Location = New System.Drawing.Point(186, 236)
        Me.PictureBoxTarifasExcelLinks.Name = "PictureBoxTarifasExcelLinks"
        Me.PictureBoxTarifasExcelLinks.Size = New System.Drawing.Size(20, 17)
        Me.PictureBoxTarifasExcelLinks.TabIndex = 43
        Me.PictureBoxTarifasExcelLinks.TabStop = False
        Me.PictureBoxTarifasExcelLinks.Tag = "enllaços a les tarifes en PDF"
        '
        'PictureBoxTarifasExcel
        '
        Me.PictureBoxTarifasExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxTarifasExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.PictureBoxTarifasExcel.Location = New System.Drawing.Point(207, 236)
        Me.PictureBoxTarifasExcel.Name = "PictureBoxTarifasExcel"
        Me.PictureBoxTarifasExcel.Size = New System.Drawing.Size(20, 17)
        Me.PictureBoxTarifasExcel.TabIndex = 44
        Me.PictureBoxTarifasExcel.TabStop = False
        Me.PictureBoxTarifasExcel.Tag = "Tarifes en format Excel"
        '
        'CheckBoxNoWeb
        '
        Me.CheckBoxNoWeb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoWeb.Location = New System.Drawing.Point(352, 73)
        Me.CheckBoxNoWeb.Name = "CheckBoxNoWeb"
        Me.CheckBoxNoWeb.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoWeb.TabIndex = 8
        Me.CheckBoxNoWeb.TabStop = False
        Me.CheckBoxNoWeb.Text = "exclou de publicitat"
        '
        'CheckBoxFrasIndepents
        '
        Me.CheckBoxFrasIndepents.Location = New System.Drawing.Point(239, 34)
        Me.CheckBoxFrasIndepents.Name = "CheckBoxFrasIndepents"
        Me.CheckBoxFrasIndepents.Size = New System.Drawing.Size(114, 16)
        Me.CheckBoxFrasIndepents.TabIndex = 45
        Me.CheckBoxFrasIndepents.Text = "fras.independents"
        '
        'CheckBoxFpgIndepent
        '
        Me.CheckBoxFpgIndepent.Location = New System.Drawing.Point(19, 395)
        Me.CheckBoxFpgIndepent.Name = "CheckBoxFpgIndepent"
        Me.CheckBoxFpgIndepent.Size = New System.Drawing.Size(172, 16)
        Me.CheckBoxFpgIndepent.TabIndex = 46
        Me.CheckBoxFpgIndepent.Text = "forma de pago independient"
        '
        'CheckBoxNoIncentius
        '
        Me.CheckBoxNoIncentius.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoIncentius.Location = New System.Drawing.Point(352, 89)
        Me.CheckBoxNoIncentius.Name = "CheckBoxNoIncentius"
        Me.CheckBoxNoIncentius.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoIncentius.TabIndex = 47
        Me.CheckBoxNoIncentius.TabStop = False
        Me.CheckBoxNoIncentius.Text = "exclou de incentius"
        '
        'CheckBoxEShopOnly
        '
        Me.CheckBoxEShopOnly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxEShopOnly.Location = New System.Drawing.Point(352, 105)
        Me.CheckBoxEShopOnly.Name = "CheckBoxEShopOnly"
        Me.CheckBoxEShopOnly.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxEShopOnly.TabIndex = 48
        Me.CheckBoxEShopOnly.TabStop = False
        Me.CheckBoxEShopOnly.Text = "botiga nomes online"
        '
        'Xl_FormaDePago1
        '
        Me.Xl_FormaDePago1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FormaDePago1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_FormaDePago1.Location = New System.Drawing.Point(3, 392)
        Me.Xl_FormaDePago1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Xl_FormaDePago1.Name = "Xl_FormaDePago1"
        Me.Xl_FormaDePago1.Size = New System.Drawing.Size(293, 128)
        Me.Xl_FormaDePago1.TabIndex = 30
        '
        'Xl_Adr1
        '
        Me.Xl_Adr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Adr1.Location = New System.Drawing.Point(96, 133)
        Me.Xl_Adr1.Name = "Xl_Adr1"
        Me.Xl_Adr1.Size = New System.Drawing.Size(384, 39)
        Me.Xl_Adr1.TabIndex = 13
        Me.Xl_Adr1.Visible = False
        '
        'Xl_ContactCcx
        '
        Me.Xl_ContactCcx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactCcx.Contact = Nothing
        Me.Xl_ContactCcx.Location = New System.Drawing.Point(96, 8)
        Me.Xl_ContactCcx.Name = "Xl_ContactCcx"
        Me.Xl_ContactCcx.ReadOnly = False
        Me.Xl_ContactCcx.Size = New System.Drawing.Size(248, 20)
        Me.Xl_ContactCcx.TabIndex = 1
        Me.Xl_ContactCcx.Visible = False
        '
        'ComboBoxQuotaOnline
        '
        Me.ComboBoxQuotaOnline.FormattingEnabled = True
        Me.ComboBoxQuotaOnline.Items.AddRange(New Object() {"0", "10", "30", "50", "70", "90", "100"})
        Me.ComboBoxQuotaOnline.Location = New System.Drawing.Point(164, 102)
        Me.ComboBoxQuotaOnline.Name = "ComboBoxQuotaOnline"
        Me.ComboBoxQuotaOnline.Size = New System.Drawing.Size(60, 21)
        Me.ComboBoxQuotaOnline.TabIndex = 49
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(93, 105)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 13)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "quota online"
        '
        'Xl_ContactPlatform
        '
        Me.Xl_ContactPlatform.Contact = Nothing
        Me.Xl_ContactPlatform.Location = New System.Drawing.Point(96, 179)
        Me.Xl_ContactPlatform.Name = "Xl_ContactPlatform"
        Me.Xl_ContactPlatform.ReadOnly = False
        Me.Xl_ContactPlatform.Size = New System.Drawing.Size(384, 20)
        Me.Xl_ContactPlatform.TabIndex = 51
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "plataforma"
        '
        'Xl_Contact_Cli
        '
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Xl_ContactPlatform)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.ComboBoxQuotaOnline)
        Me.Controls.Add(Me.CheckBoxEShopOnly)
        Me.Controls.Add(Me.CheckBoxNoIncentius)
        Me.Controls.Add(Me.CheckBoxFpgIndepent)
        Me.Controls.Add(Me.CheckBoxFrasIndepents)
        Me.Controls.Add(Me.CheckBoxNoWeb)
        Me.Controls.Add(Me.PictureBoxTarifasExcel)
        Me.Controls.Add(Me.PictureBoxTarifasExcelLinks)
        Me.Controls.Add(Me.CheckBoxGrandesCuentas)
        Me.Controls.Add(Me.ButtonDtoFixe)
        Me.Controls.Add(Me.CheckBoxDtoFixe)
        Me.Controls.Add(Me.TextBoxProvNum)
        Me.Controls.Add(Me.ButtonPortsCondicions)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.ComboBoxAlbsXFra)
        Me.Controls.Add(Me.GroupBoxCYC)
        Me.Controls.Add(Me.Xl_FormaDePago1)
        Me.Controls.Add(Me.ComboBoxCash)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.ComboBoxPorts)
        Me.Controls.Add(Me.ComboBoxPortsCondicions)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.CheckBoxNoRep)
        Me.Controls.Add(Me.CheckBoxNoSsc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxObsComercials)
        Me.Controls.Add(Me.TextBoxWarnAlbs)
        Me.Controls.Add(Me.CheckBoxWarnAlbs)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxTarifa)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxDpp)
        Me.Controls.Add(Me.Xl_Adr1)
        Me.Controls.Add(Me.CheckBoxAdr)
        Me.Controls.Add(Me.CheckBoxValorarAlbarans)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxReq)
        Me.Controls.Add(Me.CheckBoxIVA)
        Me.Controls.Add(Me.NumericUpDownCopiasFra)
        Me.Controls.Add(Me.CheckboxOrdersToCentral)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Xl_ContactCcx)
        Me.Controls.Add(Me.CheckBoxFacturarA)
        Me.Name = "Xl_Contact_Cli"
        Me.Size = New System.Drawing.Size(488, 524)
        CType(Me.NumericUpDownCopiasFra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxCYC.ResumeLayout(False)
        Me.GroupBoxCYC.PerformLayout()
        CType(Me.PictureBoxTarifasExcelLinks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxTarifasExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean
    Private mAllowAdrEvents As Boolean
    Private mClient As Client
    Private mDsCliTpas As DataSet
    Private mDto As Decimal = 0

    Public Event ChangedClient()
    Public Event ChangedCll()
    Public Event ChangedClx()
    Public Event ChangedAdrEntregas()

    Private Enum ColCliTpas
        Cod
        Ico
        Tpa
        Dsc
    End Enum

    Public WriteOnly Property Client() As Client
        Set(ByVal value As Client)
            mClient = value
            With mClient
                mDto = .Dto
                CheckBoxFacturarA.Checked = (.Ccx IsNot Nothing)
                Xl_ContactCcx.Visible = CheckBoxFacturarA.Checked
                Xl_ContactCcx.Contact = .Ccx
                TextBoxRef.Text = .Referencia
                CheckBoxFrasIndepents.Checked = .FrasIndependents
                CheckboxOrdersToCentral.Checked = .OrdersToCentral
                CheckBoxNoSsc.Checked = .NoSsc
                CheckBoxNoRep.Checked = .NoRep
                CheckBoxNoWeb.Checked = .NoWeb
                CheckBoxEShopOnly.Checked = .EShopOnly
                CheckBoxNoIncentius.Checked = .NoIncentius
                'CheckBoxEfras.Checked = .EfrasEnabled
                CheckBoxGrandesCuentas.Checked = .GrandesCuentas
                CheckBoxIVA.Checked = .IVA
                CheckBoxReq.Checked = .REQ

                Dim iQuotaOnline As Integer = .QuotaOnline
                For Each oItem As String In ComboBoxQuotaOnline.Items
                    Dim iItem As Integer = oItem
                    If iItem >= iQuotaOnline Then
                        ComboBoxQuotaOnline.SelectedItem = oItem
                        Exit For
                    End If
                Next
                NumericUpDownCopiasFra.Value = .CopiasFra
                CheckBoxValorarAlbarans.Checked = .ValorarAlbarans
                CheckBoxDtoFixe.Checked = .DtoExist
                TextBoxDpp.Text = .Dpp
                ComboBoxTarifa.SelectedIndex = CInt(.Tarifa)
                SetPortsCondicions(.PortsCondicions)
                ComboBoxPorts.SelectedIndex = .PortsCod
                ComboBoxCash.SelectedIndex = .CashCod
                TextBoxProvNum.Text = .SuProveedorNum
                TextBoxWarnAlbs.Text = .WarnAlbs
                CheckBoxWarnAlbs.Checked = (.WarnAlbs > "")
                TextBoxWarnAlbs.Visible = CheckBoxWarnAlbs.Checked
                TextBoxObsComercials.Text = .ObsComercials
                Dim oAdr As Adr = .DeliveryAdr
                If Not oAdr.Equals(.Adr) Then
                    If oAdr.Text > "" Then
                        Xl_Adr1.Adr = oAdr
                        CheckBoxAdr.Checked = True
                        Xl_Adr1.Visible = True
                    End If
                End If

                Xl_ContactPlatform.Contact = .DeliveryPlatform

                ComboBoxAlbsXFra.SelectedIndex = CInt(.CodAlbsXFra)
                CheckBoxFpgIndepent.Checked = .FpgIndependent
                Xl_FormaDePago1.LoadFromContact(Contact.Tipus.Client, mClient, .FormaDePago)
                TextBoxLimit.Text = .CreditLimit.Formatted
                TextBoxDisposat.Text = .CreditDisposat.Formatted
                TextBoxDisponible.Text = .CreditDisponible.Formatted
                CheckBoxNoASNEF.Checked = .NoAsnef
                'RefrescaCliTpas(Nothing, New System.EventArgs)
                SetFacturarAVisible()
                mAllowEvents = True
            End With
        End Set
    End Property

    Public ReadOnly Property Ccx() As Client
        Get
            Dim retval As Client = Nothing
            If CheckBoxFacturarA.Checked Then
                If Xl_ContactCcx.Contact IsNot Nothing Then
                    retval = New Client(Xl_ContactCcx.Contact.Guid)
                End If
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property Referencia() As String
        Get
            Return TextBoxRef.Text
        End Get
    End Property

    Public ReadOnly Property FrasIndependents() As Boolean
        Get
            Return CheckBoxFrasIndepents.Checked
        End Get
    End Property

    Public ReadOnly Property OrdersToCentral() As Boolean
        Get
            Dim retVal As Boolean = False
            If CheckBoxFacturarA.Checked Then
                If CheckboxOrdersToCentral.Checked Then
                    retVal = True
                End If
            End If
            Return retVal
        End Get
    End Property

    Public ReadOnly Property NoSsc() As Boolean
        Get
            Return CheckBoxNoSsc.Checked
        End Get
    End Property

    Public ReadOnly Property NoRep() As Boolean
        Get
            Return CheckBoxNoRep.Checked
        End Get
    End Property

    Public ReadOnly Property NoIncentius() As Boolean
        Get
            Return CheckBoxNoIncentius.Checked
        End Get
    End Property

    Public ReadOnly Property NoWeb() As Boolean
        Get
            Return CheckBoxNoWeb.Checked
        End Get
    End Property

    Public ReadOnly Property EfrasEnabled() As Boolean
        Get
            'Return CheckBoxEfras.Checked
            Return False
        End Get
    End Property

    Public ReadOnly Property GrandesCuentas() As Boolean
        Get
            Return CheckBoxGrandesCuentas.Checked
        End Get
    End Property

    Public ReadOnly Property EShopOnly() As Boolean
        Get
            Return CheckBoxEShopOnly.Checked
        End Get
    End Property

    Public ReadOnly Property QuotaOnline() As Integer
        Get
            Return ComboBoxQuotaOnline.SelectedItem
        End Get
    End Property

    Public ReadOnly Property IVA() As Boolean
        Get
            Return CheckBoxIVA.Checked
        End Get
    End Property

    Public ReadOnly Property REQ() As Boolean
        Get
            Return CheckBoxReq.Checked
        End Get
    End Property

    Public ReadOnly Property CopiasFra() As Integer
        Get
            Return NumericUpDownCopiasFra.Value
        End Get
    End Property

    Public ReadOnly Property ValorarAlbarans() As Boolean
        Get
            Return CheckBoxValorarAlbarans.Checked
        End Get
    End Property


    Public ReadOnly Property Dto() As Decimal
        Get
            Return mDto
        End Get
    End Property

    Public ReadOnly Property Dpp() As Decimal
        Get
            Dim retval As Decimal
            If IsNumeric(TextBoxDpp.Text) Then
                retval = CDec(TextBoxDpp.Text)
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property Tarifa() As Client.Tarifas
        Get
            Return ComboBoxTarifa.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property PortsCondicions() As PortsCondicions
        Get
            Return New PortsCondicions(mEmp, ComboBoxPortsCondicions.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property PortsCod() As DTO.DTOCustomer.PortsCodes
        Get
            Return ComboBoxPorts.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property CashCod() As DTO.DTOCustomer.CashCodes
        Get
            Return ComboBoxCash.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property SuProveedorNum() As String
        Get
            Return TextBoxProvNum.Text
        End Get
    End Property

    Public ReadOnly Property WarnAlbs() As String
        Get
            Dim s As String = ""
            If CheckBoxWarnAlbs.Checked Then
                s = TextBoxWarnAlbs.Text
            End If
            Return s
        End Get
    End Property

    Public ReadOnly Property ObsComercials() As String
        Get
            Return TextBoxObsComercials.Text
        End Get
    End Property

    Public ReadOnly Property DeliveryAdr() As Adr
        Get
            If CheckBoxAdr.Checked Then
                Return Xl_Adr1.Adr
            Else
                Return New Adr
            End If
        End Get
    End Property

    Public ReadOnly Property CodAlbsXFra() As Client.CodsAlbsXFra
        Get
            Return ComboBoxAlbsXFra.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property FpgIndependent() As Boolean
        Get
            Return CheckBoxFpgIndepent.Checked
        End Get
    End Property

    Public ReadOnly Property FormaDePago() As FormaDePago
        Get
            Return Xl_FormaDePago1.FormaDePago
        End Get
    End Property

    Public ReadOnly Property NoAsnef() As Boolean
        Get
            Return CheckBoxNoASNEF.Checked
        End Get
    End Property

    Public ReadOnly Property DeliveryPlatform() As Contact
        Get
            Return Xl_ContactPlatform.Contact
        End Get
    End Property

    Private Sub CheckBoxFacturarA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFacturarA.CheckedChanged
        If mAllowEvents Then
            SetFacturarAVisible()
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub SetFacturarAVisible()
        Dim Delega As Boolean = CheckBoxFacturarA.Checked
        Xl_FormaDePago1.Visible = (Not Delega) Or CheckBoxFpgIndepent.Checked
        CheckBoxFpgIndepent.Visible = Delega
        Xl_ContactCcx.Visible = Delega
        CheckboxOrdersToCentral.Visible = Delega
        CheckBoxIVA.Visible = Not Delega
        CheckBoxReq.Visible = Not Delega
        'CheckBoxValorarAlbarans.Visible = Not Delega
        GroupBoxCYC.Visible = Not Delega
        ComboBoxTarifa.Visible = Not Delega
    End Sub

    Private Sub CheckBoxAdr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAdr.CheckedChanged
        If mAllowEvents Then
            Xl_Adr1.Visible = CheckBoxAdr.Checked
            RaiseEvent ChangedAdrEntregas()
        End If
    End Sub

    Private Sub Xl_Adr1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Adr1.Changed
        If mAllowEvents Then
            RaiseEvent ChangedAdrEntregas()
        End If
    End Sub

    Private Sub Xl_FormaDePago1_AfterUpdate() Handles Xl_FormaDePago1.AfterUpdate
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        CheckBoxFacturarA.CheckedChanged,
        CheckboxOrdersToCentral.CheckedChanged,
        CheckBoxFrasIndepents.CheckedChanged,
        CheckBoxIVA.CheckedChanged,
        CheckBoxReq.CheckedChanged,
        CheckBoxNoSsc.CheckedChanged,
        CheckBoxNoRep.CheckedChanged,
        CheckBoxNoWeb.CheckedChanged,
         CheckBoxEShopOnly.CheckedChanged,
        CheckBoxNoIncentius.CheckedChanged,
        CheckBoxValorarAlbarans.CheckedChanged,
         ComboBoxQuotaOnline.SelectedIndexChanged,
        ComboBoxPortsCondicions.SelectedIndexChanged,
        ComboBoxPorts.SelectedIndexChanged,
        ComboBoxTarifa.SelectedIndexChanged,
        ComboBoxCash.SelectedIndexChanged,
        ComboBoxAlbsXFra.SelectedIndexChanged,
        TextBoxLimit.TextChanged,
        TextBoxObsComercials.TextChanged,
        TextBoxWarnAlbs.TextChanged,
        Xl_ContactPlatform.AfterUpdate


        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub


    Private Sub NomChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxRef.TextChanged
        If mAllowEvents Then
            RaiseEvent ChangedClient()
            RaiseEvent ChangedCll()
            RaiseEvent ChangedClx()
        End If
    End Sub

    Private Sub Xl_ContactCcx_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactCcx.AfterUpdate
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub CheckBoxWarnAlbs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxWarnAlbs.CheckedChanged
        If mAllowEvents Then
            TextBoxWarnAlbs.Visible = CheckBoxWarnAlbs.Checked
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub Xl_AmtCurCYC_AfterUpdateCur(ByVal oCur As maxisrvr.Cur)
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub Xl_AmtCurCYC_AfterUpdateValue(ByVal oAmt As maxisrvr.Amt)
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Public Sub SetDirty()
        RaiseEvent ChangedClient()
    End Sub

    Private Sub SetPortsCondicions(ByVal oCondicions As PortsCondicions)
        If ComboBoxPortsCondicions.Items.Count = 0 Then
            Dim SQL As String = "SELECT ID,NOM FROM PORTSCONDICIONS ORDER BY ID"
            Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
            With ComboBoxPortsCondicions
                .DataSource = oDs.Tables(0)
                .DisplayMember = "NOM"
                .ValueMember = "ID"
            End With
        End If
        ComboBoxPortsCondicions.SelectedValue = oCondicions.Id
    End Sub


    Private Sub ButtonCYC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCYC.Click
        Dim oFrm As New Frm_Client_Risc(mClient)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshCredit
        oFrm.Show()
    End Sub

    Private Sub RefreshCredit(ByVal sender As Object, ByVal e As System.EventArgs)
        With mClient
            TextBoxLimit.Text = .CreditLimit.Formatted
            TextBoxDisposat.Text = .CreditDisposat.Formatted
            TextBoxDisponible.Text = .CreditDisponible.Formatted
        End With
    End Sub

    Private Sub ButtonPortsCondicions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPortsCondicions.Click
        Dim oConds As New PortsCondicions(mEmp, ComboBoxPortsCondicions.SelectedValue)
        root.ShowPortsCondicions(oConds)
    End Sub

    Private Sub ButtonDtoFixe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDtoFixe.Click
        Dim oFrm As New Frm_CliDtos(mClient)
        AddHandler oFrm.AfterUpdate, AddressOf AfterDtosUpdate
        oFrm.Show()
    End Sub

    Private Sub AfterDtosUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oClient As Client = sender
        If mDto <> oClient.Dto Then
            mDto = oClient.Dto
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub CheckBoxDtoFixe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDtoFixe.CheckedChanged
        ButtonDtoFixe.Visible = CheckBoxDtoFixe.Checked
    End Sub

    Private Sub CheckBoxGrandesCuentas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxGrandesCuentas.CheckedChanged
        If Xl_ContactCcx.Contact IsNot Nothing Then
            Dim SQL As String
            Dim oDrd As SqlClient.SqlDataReader
            'vigila que el principal sigu Gran Compte
            If Not Xl_ContactCcx.Contact.Client.GrandesCuentas Then
                InputBox("El compte de facturació no está assignat com 'Grans Comptes'. El posem?")
            End If
            'vigila que totes les sucursals estiguin correctes
            SQL = "SELECT COUNT(CLI) FROM CLICLIENT WHERE CCX=" & Xl_ContactCcx.Contact.Id
            oDrd = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
            oDrd.Close()
        End If
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub


    Private Sub PictureBoxTarifasExcelLinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxTarifasExcelLinks.Click
        Dim sToday As String = Format(Today, "yyyyMMdd")
        Dim SQL As String = "SELECT TARIFA.GUID, TARIFA.TPA, TARIFA.TARIFA, TPA.DSC FROM " _
        & "TARIFA INNER JOIN TPA ON TARIFA.EMP=TPA.EMP AND TARIFA.TPA=TPA.TPA " _
        & "WHERE TARIFA.EMP=" & mEmp.Id & " AND " _
        & "MEMBRETE=1 "

        If Not mClient.Tarifa = MaxiSrvr.Client.Tarifas.Virtual Then
            SQL = SQL & "AND (TARIFA.TARIFA=" & mClient.Tarifa & " " _
            & "OR TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Pvp & ") "
        Else
            SQL = SQL & "AND (TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Standard & " " _
            & "OR TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Pvp & ") "
        End If

        SQL = SQL & "ORDER BY TPA.ORD, TARIFA.TARIFA"



        Dim oArrayText As New ArrayList
        Dim oArrayGuid As New ArrayList
        Dim sText As String = ""
        Dim sGuid As String = ""

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            sGuid = oDrd("Guid").ToString
            oArrayGuid.Add(sGuid)
            Select Case CType(oDrd("TARIFA"), Client.Tarifas)
                Case MaxiSrvr.Client.Tarifas.Pvp
                    sText = oDrd("DSC") & ": " & mClient.Lang.Tradueix("TARIFA PVP", "TARIFA PVP", "RETAIL PRICE LIST")
                Case Else
                    sText = oDrd("DSC") & ": " & mClient.Lang.Tradueix("TARIFA COSTE", "TARIFA COST", "DISTRIBUTOR PRICE LIST")
            End Select
            oArrayText.Add(sText)
        Loop
        MatExcel.CopyLinksToClipboard(oArrayText, oArrayGuid).Visible = True


    End Sub

    Private Sub PictureBoxTarifasExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxTarifasExcel.Click
        UIHelper.ShowHtml(mClient.UrlPro & "/tarifas")
    End Sub

    Private Sub CheckBoxFpgIndepent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFpgIndepent.Click
        Dim Delega As Boolean = CheckBoxFacturarA.Checked
        Xl_FormaDePago1.Visible = (Not Delega) Or CheckBoxFpgIndepent.Checked
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub


    Private Sub CheckBoxNoASNEF_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNoASNEF.CheckedChanged
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub Xl_FormaDePago1_AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Xl_FormaDePago1.AfterUpdate

    End Sub
End Class
