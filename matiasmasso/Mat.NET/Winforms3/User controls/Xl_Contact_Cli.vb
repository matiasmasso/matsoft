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
    Friend WithEvents Xl_ContactCcx As Xl_Contact2
    Friend WithEvents TextBoxRef As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckboxOrdersToCentral As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxReq As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxValorarAlbarans As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxAdr As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Adr1 As Xl_Address
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxProvNum As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxWarnAlbs As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxWarnAlbs As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxNoRep As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Xl_FormaDePago1 As Xl_FormaDePago
    Friend WithEvents TextBoxLimit As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCYC As System.Windows.Forms.Button
    Friend WithEvents CheckBoxNoWeb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFrasIndepents As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFpgIndepent As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoIncentius As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDisponible As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDisposat As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactPlatform As Xl_Contact2
    Friend WithEvents Label1Incoterms As Label
    Friend WithEvents CheckBoxNoRaffles As CheckBox
    Friend WithEvents ComboBoxFraPrintMode As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBoxwebatlaspriority As CheckBox
    Friend WithEvents CheckBoxPlatform As CheckBox
    Friend WithEvents CheckBoxHorarioEntregas As CheckBox
    Friend WithEvents TextBoxHorarioEntregas As TextBox
    Friend WithEvents Xl_LookupHolding1 As Xl_LookupHolding
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_LookupCustomerCluster1 As Xl_LookupCustomerCluster
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBoxExport As CheckBox
    Friend WithEvents CheckBoxCEE As CheckBox
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
    Friend WithEvents Xl_LookupPortsCondicions1 As Xl_LookupPortsCondicions
    Friend WithEvents ComboBoxCash As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CheckBoxFacturarA = New System.Windows.Forms.CheckBox()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckboxOrdersToCentral = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.CheckBoxReq = New System.Windows.Forms.CheckBox()
        Me.CheckBoxValorarAlbarans = New System.Windows.Forms.CheckBox()
        Me.CheckBoxAdr = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxProvNum = New System.Windows.Forms.TextBox()
        Me.CheckBoxWarnAlbs = New System.Windows.Forms.CheckBox()
        Me.TextBoxWarnAlbs = New System.Windows.Forms.TextBox()
        Me.CheckBoxNoRep = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxCash = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxDisponible = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxDisposat = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonCYC = New System.Windows.Forms.Button()
        Me.TextBoxLimit = New System.Windows.Forms.TextBox()
        Me.CheckBoxNoWeb = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFrasIndepents = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFpgIndepent = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoIncentius = New System.Windows.Forms.CheckBox()
        Me.Label1Incoterms = New System.Windows.Forms.Label()
        Me.CheckBoxNoRaffles = New System.Windows.Forms.CheckBox()
        Me.ComboBoxFraPrintMode = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxwebatlaspriority = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPlatform = New System.Windows.Forms.CheckBox()
        Me.CheckBoxHorarioEntregas = New System.Windows.Forms.CheckBox()
        Me.TextBoxHorarioEntregas = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_LookupHolding1 = New Mat.Net.Xl_LookupHolding()
        Me.Xl_ContactPlatform = New Mat.Net.Xl_Contact2()
        Me.Xl_FormaDePago1 = New Mat.Net.Xl_FormaDePago()
        Me.Xl_Adr1 = New Mat.Net.Xl_Address()
        Me.Xl_ContactCcx = New Mat.Net.Xl_Contact2()
        Me.Xl_LookupCustomerCluster1 = New Mat.Net.Xl_LookupCustomerCluster()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxExport = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCEE = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupIncoterm1 = New Mat.Net.Xl_LookupIncoterm()
        Me.Xl_LookupPortsCondicions1 = New Mat.Net.Xl_LookupPortsCondicions()
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
        Me.TextBoxRef.Location = New System.Drawing.Point(96, 29)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(146, 20)
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
        Me.CheckboxOrdersToCentral.Location = New System.Drawing.Point(367, 8)
        Me.CheckboxOrdersToCentral.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.CheckboxOrdersToCentral.Name = "CheckboxOrdersToCentral"
        Me.CheckboxOrdersToCentral.Size = New System.Drawing.Size(128, 16)
        Me.CheckboxOrdersToCentral.TabIndex = 4
        Me.CheckboxOrdersToCentral.TabStop = False
        Me.CheckboxOrdersToCentral.Text = "Comandes a central"
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.Location = New System.Drawing.Point(8, 78)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxIVA.TabIndex = 9
        Me.CheckBoxIVA.Text = "IVA"
        '
        'CheckBoxReq
        '
        Me.CheckBoxReq.Location = New System.Drawing.Point(8, 93)
        Me.CheckBoxReq.Name = "CheckBoxReq"
        Me.CheckBoxReq.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxReq.TabIndex = 11
        Me.CheckBoxReq.Text = "Recàrrec equivalencia"
        '
        'CheckBoxValorarAlbarans
        '
        Me.CheckBoxValorarAlbarans.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxValorarAlbarans.Location = New System.Drawing.Point(254, 77)
        Me.CheckBoxValorarAlbarans.Name = "CheckBoxValorarAlbarans"
        Me.CheckBoxValorarAlbarans.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxValorarAlbarans.TabIndex = 10
        Me.CheckBoxValorarAlbarans.Text = "Albará valorat"
        '
        'CheckBoxAdr
        '
        Me.CheckBoxAdr.Location = New System.Drawing.Point(8, 137)
        Me.CheckBoxAdr.Name = "CheckBoxAdr"
        Me.CheckBoxAdr.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxAdr.TabIndex = 12
        Me.CheckBoxAdr.Text = "Adr.entregues:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Location = New System.Drawing.Point(4, 281)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 16)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "prov.num:"
        '
        'TextBoxProvNum
        '
        Me.TextBoxProvNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxProvNum.Location = New System.Drawing.Point(96, 278)
        Me.TextBoxProvNum.Name = "TextBoxProvNum"
        Me.TextBoxProvNum.Size = New System.Drawing.Size(147, 20)
        Me.TextBoxProvNum.TabIndex = 22
        '
        'CheckBoxWarnAlbs
        '
        Me.CheckBoxWarnAlbs.Location = New System.Drawing.Point(8, 316)
        Me.CheckBoxWarnAlbs.Name = "CheckBoxWarnAlbs"
        Me.CheckBoxWarnAlbs.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxWarnAlbs.TabIndex = 23
        Me.CheckBoxWarnAlbs.Text = "Advert.albs:"
        '
        'TextBoxWarnAlbs
        '
        Me.TextBoxWarnAlbs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWarnAlbs.BackColor = System.Drawing.Color.LightSalmon
        Me.TextBoxWarnAlbs.Location = New System.Drawing.Point(96, 316)
        Me.TextBoxWarnAlbs.Name = "TextBoxWarnAlbs"
        Me.TextBoxWarnAlbs.Size = New System.Drawing.Size(207, 20)
        Me.TextBoxWarnAlbs.TabIndex = 24
        Me.TextBoxWarnAlbs.Visible = False
        '
        'CheckBoxNoRep
        '
        Me.CheckBoxNoRep.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNoRep.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoRep.Location = New System.Drawing.Point(367, 62)
        Me.CheckBoxNoRep.Name = "CheckBoxNoRep"
        Me.CheckBoxNoRep.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoRep.TabIndex = 6
        Me.CheckBoxNoRep.TabStop = False
        Me.CheckBoxNoRep.Text = "No Rep"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(4, 254)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 16)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "ports:"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.Location = New System.Drawing.Point(247, 278)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 16)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "pagament:"
        '
        'ComboBoxCash
        '
        Me.ComboBoxCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCash.FormattingEnabled = True
        Me.ComboBoxCash.Items.AddRange(New Object() {"(modalitat credit)", "CREDIT", "REEMBOLS", "TRANSF.PREVIA"})
        Me.ComboBoxCash.Location = New System.Drawing.Point(311, 278)
        Me.ComboBoxCash.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ComboBoxCash.Name = "ComboBoxCash"
        Me.ComboBoxCash.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxCash.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 23)
        Me.Label9.TabIndex = 0
        '
        'TextBoxDisponible
        '
        Me.TextBoxDisponible.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxDisponible.Name = "TextBoxDisponible"
        Me.TextBoxDisponible.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDisponible.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 23)
        Me.Label8.TabIndex = 0
        '
        'TextBoxDisposat
        '
        Me.TextBoxDisposat.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxDisposat.Name = "TextBoxDisposat"
        Me.TextBoxDisposat.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDisposat.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 23)
        Me.Label3.TabIndex = 0
        '
        'ButtonCYC
        '
        Me.ButtonCYC.Location = New System.Drawing.Point(0, 0)
        Me.ButtonCYC.Name = "ButtonCYC"
        Me.ButtonCYC.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCYC.TabIndex = 0
        '
        'TextBoxLimit
        '
        Me.TextBoxLimit.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxLimit.Name = "TextBoxLimit"
        Me.TextBoxLimit.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxLimit.TabIndex = 0
        '
        'CheckBoxNoWeb
        '
        Me.CheckBoxNoWeb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNoWeb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoWeb.Location = New System.Drawing.Point(367, 77)
        Me.CheckBoxNoWeb.Name = "CheckBoxNoWeb"
        Me.CheckBoxNoWeb.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoWeb.TabIndex = 8
        Me.CheckBoxNoWeb.TabStop = False
        Me.CheckBoxNoWeb.Text = "exclou de publicitat"
        '
        'CheckBoxFrasIndepents
        '
        Me.CheckBoxFrasIndepents.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFrasIndepents.Location = New System.Drawing.Point(254, 33)
        Me.CheckBoxFrasIndepents.Name = "CheckBoxFrasIndepents"
        Me.CheckBoxFrasIndepents.Size = New System.Drawing.Size(114, 16)
        Me.CheckBoxFrasIndepents.TabIndex = 45
        Me.CheckBoxFrasIndepents.Text = "fras.independents"
        '
        'CheckBoxFpgIndepent
        '
        Me.CheckBoxFpgIndepent.Location = New System.Drawing.Point(8, 347)
        Me.CheckBoxFpgIndepent.Name = "CheckBoxFpgIndepent"
        Me.CheckBoxFpgIndepent.Size = New System.Drawing.Size(191, 16)
        Me.CheckBoxFpgIndepent.TabIndex = 46
        Me.CheckBoxFpgIndepent.Text = "forma de pago independient"
        '
        'CheckBoxNoIncentius
        '
        Me.CheckBoxNoIncentius.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNoIncentius.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoIncentius.Location = New System.Drawing.Point(367, 93)
        Me.CheckBoxNoIncentius.Name = "CheckBoxNoIncentius"
        Me.CheckBoxNoIncentius.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoIncentius.TabIndex = 47
        Me.CheckBoxNoIncentius.TabStop = False
        Me.CheckBoxNoIncentius.Text = "exclou de incentius"
        '
        'Label1Incoterms
        '
        Me.Label1Incoterms.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1Incoterms.AutoSize = True
        Me.Label1Incoterms.Location = New System.Drawing.Point(319, 346)
        Me.Label1Incoterms.Name = "Label1Incoterms"
        Me.Label1Incoterms.Size = New System.Drawing.Size(47, 13)
        Me.Label1Incoterms.TabIndex = 65
        Me.Label1Incoterms.Text = "incoterm"
        '
        'CheckBoxNoRaffles
        '
        Me.CheckBoxNoRaffles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNoRaffles.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNoRaffles.Location = New System.Drawing.Point(367, 109)
        Me.CheckBoxNoRaffles.Name = "CheckBoxNoRaffles"
        Me.CheckBoxNoRaffles.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxNoRaffles.TabIndex = 67
        Me.CheckBoxNoRaffles.TabStop = False
        Me.CheckBoxNoRaffles.Text = "exclou de sortejos"
        '
        'ComboBoxFraPrintMode
        '
        Me.ComboBoxFraPrintMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFraPrintMode.FormattingEnabled = True
        Me.ComboBoxFraPrintMode.Items.AddRange(New Object() {"(sel·leccionar)", "No enviar", "Paper", "Email", "Edi"})
        Me.ComboBoxFraPrintMode.Location = New System.Drawing.Point(404, 370)
        Me.ComboBoxFraPrintMode.Name = "ComboBoxFraPrintMode"
        Me.ComboBoxFraPrintMode.Size = New System.Drawing.Size(92, 21)
        Me.ComboBoxFraPrintMode.TabIndex = 69
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(319, 373)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "enviament fres."
        '
        'CheckBoxwebatlaspriority
        '
        Me.CheckBoxwebatlaspriority.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxwebatlaspriority.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxwebatlaspriority.Location = New System.Drawing.Point(367, 24)
        Me.CheckBoxwebatlaspriority.Name = "CheckBoxwebatlaspriority"
        Me.CheckBoxwebatlaspriority.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxwebatlaspriority.TabIndex = 70
        Me.CheckBoxwebatlaspriority.Text = "Prioritat web"
        '
        'CheckBoxPlatform
        '
        Me.CheckBoxPlatform.Location = New System.Drawing.Point(8, 185)
        Me.CheckBoxPlatform.Name = "CheckBoxPlatform"
        Me.CheckBoxPlatform.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxPlatform.TabIndex = 71
        Me.CheckBoxPlatform.Text = "Plataforma:"
        '
        'CheckBoxHorarioEntregas
        '
        Me.CheckBoxHorarioEntregas.Location = New System.Drawing.Point(8, 209)
        Me.CheckBoxHorarioEntregas.Name = "CheckBoxHorarioEntregas"
        Me.CheckBoxHorarioEntregas.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxHorarioEntregas.TabIndex = 73
        Me.CheckBoxHorarioEntregas.Text = "Obs.Transp."
        '
        'TextBoxHorarioEntregas
        '
        Me.TextBoxHorarioEntregas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxHorarioEntregas.Location = New System.Drawing.Point(97, 207)
        Me.TextBoxHorarioEntregas.Name = "TextBoxHorarioEntregas"
        Me.TextBoxHorarioEntregas.Size = New System.Drawing.Size(399, 20)
        Me.TextBoxHorarioEntregas.TabIndex = 74
        Me.TextBoxHorarioEntregas.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "holding:"
        '
        'Xl_LookupHolding1
        '
        Me.Xl_LookupHolding1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupHolding1.Holding = Nothing
        Me.Xl_LookupHolding1.IsDirty = False
        Me.Xl_LookupHolding1.Location = New System.Drawing.Point(97, 230)
        Me.Xl_LookupHolding1.Name = "Xl_LookupHolding1"
        Me.Xl_LookupHolding1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupHolding1.ReadOnlyLookup = False
        Me.Xl_LookupHolding1.Size = New System.Drawing.Size(243, 20)
        Me.Xl_LookupHolding1.TabIndex = 75
        Me.Xl_LookupHolding1.Value = Nothing
        '
        'Xl_ContactPlatform
        '
        Me.Xl_ContactPlatform.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactPlatform.Contact = Nothing
        Me.Xl_ContactPlatform.Emp = Nothing
        Me.Xl_ContactPlatform.Location = New System.Drawing.Point(96, 183)
        Me.Xl_ContactPlatform.Name = "Xl_ContactPlatform"
        Me.Xl_ContactPlatform.ReadOnly = False
        Me.Xl_ContactPlatform.Size = New System.Drawing.Size(399, 20)
        Me.Xl_ContactPlatform.TabIndex = 51
        '
        'Xl_FormaDePago1
        '
        Me.Xl_FormaDePago1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FormaDePago1.BackColor = System.Drawing.SystemColors.Control
        Me.Xl_FormaDePago1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_FormaDePago1.Location = New System.Drawing.Point(6, 345)
        Me.Xl_FormaDePago1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Xl_FormaDePago1.Name = "Xl_FormaDePago1"
        Me.Xl_FormaDePago1.Size = New System.Drawing.Size(309, 128)
        Me.Xl_FormaDePago1.TabIndex = 30
        '
        'Xl_Adr1
        '
        Me.Xl_Adr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Adr1.Location = New System.Drawing.Point(96, 137)
        Me.Xl_Adr1.Name = "Xl_Adr1"
        Me.Xl_Adr1.ReadOnly = True
        Me.Xl_Adr1.Size = New System.Drawing.Size(403, 39)
        Me.Xl_Adr1.TabIndex = 13
        Me.Xl_Adr1.Text = ""
        Me.Xl_Adr1.Visible = False
        '
        'Xl_ContactCcx
        '
        Me.Xl_ContactCcx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactCcx.Contact = Nothing
        Me.Xl_ContactCcx.Emp = Nothing
        Me.Xl_ContactCcx.Location = New System.Drawing.Point(96, 8)
        Me.Xl_ContactCcx.Name = "Xl_ContactCcx"
        Me.Xl_ContactCcx.ReadOnly = False
        Me.Xl_ContactCcx.Size = New System.Drawing.Size(267, 20)
        Me.Xl_ContactCcx.TabIndex = 1
        Me.Xl_ContactCcx.Visible = False
        '
        'Xl_LookupCustomerCluster1
        '
        Me.Xl_LookupCustomerCluster1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupCustomerCluster1.CustomerCluster = Nothing
        Me.Xl_LookupCustomerCluster1.IsDirty = False
        Me.Xl_LookupCustomerCluster1.Location = New System.Drawing.Point(403, 316)
        Me.Xl_LookupCustomerCluster1.Name = "Xl_LookupCustomerCluster1"
        Me.Xl_LookupCustomerCluster1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCustomerCluster1.ReadOnlyLookup = False
        Me.Xl_LookupCustomerCluster1.Size = New System.Drawing.Size(92, 20)
        Me.Xl_LookupCustomerCluster1.TabIndex = 76
        Me.Xl_LookupCustomerCluster1.Value = Nothing
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(319, 319)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 16)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "Cluster:"
        '
        'CheckBoxExport
        '
        Me.CheckBoxExport.Location = New System.Drawing.Point(8, 62)
        Me.CheckBoxExport.Name = "CheckBoxExport"
        Me.CheckBoxExport.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxExport.TabIndex = 78
        Me.CheckBoxExport.Text = "Exportació"
        '
        'CheckBoxCEE
        '
        Me.CheckBoxCEE.Location = New System.Drawing.Point(97, 62)
        Me.CheckBoxCEE.Name = "CheckBoxCEE"
        Me.CheckBoxCEE.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxCEE.TabIndex = 79
        Me.CheckBoxCEE.Text = "CEE"
        Me.CheckBoxCEE.Visible = False
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(404, 343)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(91, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 80
        Me.Xl_LookupIncoterm1.Value = Nothing
        '
        'Xl_LookupPortsCondicions1
        '
        Me.Xl_LookupPortsCondicions1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupPortsCondicions1.IsDirty = False
        Me.Xl_LookupPortsCondicions1.Location = New System.Drawing.Point(97, 254)
        Me.Xl_LookupPortsCondicions1.Name = "Xl_LookupPortsCondicions1"
        Me.Xl_LookupPortsCondicions1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPortsCondicions1.ReadOnlyLookup = False
        Me.Xl_LookupPortsCondicions1.Size = New System.Drawing.Size(243, 20)
        Me.Xl_LookupPortsCondicions1.TabIndex = 81
        Me.Xl_LookupPortsCondicions1.Value = Nothing
        '
        'Xl_Contact_Cli
        '
        Me.Controls.Add(Me.Xl_LookupPortsCondicions1)
        Me.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.Controls.Add(Me.CheckBoxCEE)
        Me.Controls.Add(Me.CheckBoxExport)
        Me.Controls.Add(Me.Xl_LookupCustomerCluster1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_LookupHolding1)
        Me.Controls.Add(Me.TextBoxHorarioEntregas)
        Me.Controls.Add(Me.CheckBoxHorarioEntregas)
        Me.Controls.Add(Me.Xl_ContactPlatform)
        Me.Controls.Add(Me.CheckBoxPlatform)
        Me.Controls.Add(Me.CheckBoxwebatlaspriority)
        Me.Controls.Add(Me.ComboBoxFraPrintMode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckBoxNoRaffles)
        Me.Controls.Add(Me.Label1Incoterms)
        Me.Controls.Add(Me.CheckBoxNoIncentius)
        Me.Controls.Add(Me.CheckBoxFpgIndepent)
        Me.Controls.Add(Me.CheckBoxFrasIndepents)
        Me.Controls.Add(Me.CheckBoxNoWeb)
        Me.Controls.Add(Me.TextBoxProvNum)
        Me.Controls.Add(Me.Xl_FormaDePago1)
        Me.Controls.Add(Me.ComboBoxCash)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.CheckBoxNoRep)
        Me.Controls.Add(Me.TextBoxWarnAlbs)
        Me.Controls.Add(Me.CheckBoxWarnAlbs)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_Adr1)
        Me.Controls.Add(Me.CheckBoxAdr)
        Me.Controls.Add(Me.CheckBoxValorarAlbarans)
        Me.Controls.Add(Me.CheckBoxReq)
        Me.Controls.Add(Me.CheckBoxIVA)
        Me.Controls.Add(Me.CheckboxOrdersToCentral)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Xl_ContactCcx)
        Me.Controls.Add(Me.CheckBoxFacturarA)
        Me.Name = "Xl_Contact_Cli"
        Me.Size = New System.Drawing.Size(507, 497)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private _Customer As DTOCustomer

    Private mEmp As DTOEmp = Current.Session.Emp
    Private mAllowEvents As Boolean
    Private mAllowAdrEvents As Boolean
    Private mDsCliTpas As DataSet

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

    Public Shadows Async Sub load(oCustomer As DTOCustomer)
        Dim exs As New List(Of Exception)
        _Customer = oCustomer
        With _Customer
            If .Ccx IsNot Nothing Then
                CheckBoxFacturarA.Checked = True
                Xl_ContactCcx.Visible = CheckBoxFacturarA.Checked
                Xl_ContactCcx.Contact = .Ccx
            End If
            TextBoxRef.Text = .Ref
            CheckboxOrdersToCentral.Checked = .OrdersToCentral
            CheckBoxNoRep.Checked = .NoRep
            CheckBoxNoWeb.Checked = .NoWeb
            CheckBoxNoRaffles.Checked = .NoRaffles
            CheckBoxwebatlaspriority.Checked = .WebAtlasPriority
            CheckBoxNoIncentius.Checked = .NoIncentius
            CheckBoxExport.Checked = (.ExportCod = DTOInvoice.ExportCods.intracomunitari Or .ExportCod = DTOInvoice.ExportCods.extracomunitari)
            CheckBoxCEE.Checked = .ExportCod = DTOInvoice.ExportCods.intracomunitari
            CheckBoxIVA.Checked = .Iva
            CheckBoxReq.Checked = .Req
            CheckBoxCEE.Visible = CheckBoxExport.Checked
            CheckBoxIVA.Visible = Not CheckBoxExport.Checked
            CheckBoxReq.Visible = CheckBoxIVA.Checked And Not CheckBoxExport.Checked
            CheckBoxValorarAlbarans.Checked = .AlbValorat
            Xl_LookupPortsCondicions1.Load(.PortsCondicions)
            ComboBoxCash.SelectedIndex = .CashCod
            TextBoxProvNum.Text = .SuProveedorNum
            TextBoxWarnAlbs.Text = .WarnAlbs
            CheckBoxWarnAlbs.Checked = (.WarnAlbs.isNotEmpty())
            TextBoxWarnAlbs.Visible = CheckBoxWarnAlbs.Checked
            Dim oAddress As DTOAddress = Await FEB.Address.Find(oCustomer, DTOAddress.Codis.Entregas, exs)
            If exs.Count = 0 Then
                If oAddress IsNot Nothing Then
                    If Not oAddress.Equals(.Address) Then
                        If oAddress.Text > "" Then
                            CheckBoxAdr.Checked = True
                            Xl_Adr1.Load(oAddress)
                            Xl_Adr1.Visible = True
                        End If
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If

            If .DeliveryPlatform Is Nothing Then
                CheckBoxPlatform.Checked = False
                Xl_ContactPlatform.Visible = False
            Else
                CheckBoxPlatform.Checked = True
                Xl_ContactPlatform.Visible = True
                Xl_ContactPlatform.Contact = .DeliveryPlatform
            End If

            If .HorarioEntregas > "" Then
                CheckBoxHorarioEntregas.Checked = True
                TextBoxHorarioEntregas.Visible = True
                TextBoxHorarioEntregas.Text = .HorarioEntregas
            End If

            ComboBoxFraPrintMode.SelectedIndex = .FraPrintMode

            CheckBoxFpgIndepent.Checked = .FpgIndependent
            Await Xl_FormaDePago1.Load(DTOIban.Cods.client, _Customer, .PaymentTerms)
            SetFacturarAVisible()
            Dim oIncoterms = Await FEB.Incoterms.All(exs)
            If exs.Count = 0 Then
                Xl_LookupIncoterm1.Load(oIncoterms, .Incoterm)
            Else
                UIHelper.WarnError(exs)
            End If
            Xl_LookupHolding1.Holding = .Holding
            Xl_LookupCustomerCluster1.CustomerCluster = .CustomerCluster
            mAllowEvents = True
        End With
    End Sub

    Public ReadOnly Property Customer As DTOCustomer
        Get
            With _Customer
                If CheckBoxFacturarA.Checked Then
                    .Ccx = DTOCustomer.FromContact(Xl_ContactCcx.Contact)
                Else
                    .Ccx = Nothing
                End If
                .Ref = TextBoxRef.Text
                .OrdersToCentral = CheckboxOrdersToCentral.Checked
                .FraPrintMode = ComboBoxFraPrintMode.SelectedIndex
                .NoRep = CheckBoxNoRep.Checked
                .NoWeb = CheckBoxNoWeb.Checked
                .WebAtlasPriority = CheckBoxwebatlaspriority.Checked
                .NoIncentius = CheckBoxNoIncentius.Checked
                .NoRaffles = CheckBoxNoRaffles.Checked
                .ExportCod = IIf(CheckBoxExport.Checked, IIf(CheckBoxCEE.Checked, DTOInvoice.ExportCods.intracomunitari, DTOInvoice.ExportCods.extracomunitari), DTOInvoice.ExportCods.nacional)
                .Iva = CheckBoxIVA.Checked
                .Req = CheckBoxReq.Checked
                .AlbValorat = CheckBoxValorarAlbarans.Checked
                .PortsCondicions = Xl_LookupPortsCondicions1.PortsCondicio
                .CashCod = ComboBoxCash.SelectedIndex
                .SuProveedorNum = TextBoxProvNum.Text
                .WarnAlbs = IIf(CheckBoxWarnAlbs.Checked, TextBoxWarnAlbs.Text, "")
                .FpgIndependent = CheckBoxFpgIndepent.Checked
                .PaymentTerms = Xl_FormaDePago1.PaymentTerms
                If CheckBoxPlatform.Checked Then
                    .DeliveryPlatform = DTOCustomerPlatform.FromContact(Xl_ContactPlatform.Contact)
                Else
                    .DeliveryPlatform = Nothing
                End If

                If CheckBoxHorarioEntregas.Checked Then
                    .HorarioEntregas = TextBoxHorarioEntregas.Text
                Else
                    .HorarioEntregas = ""
                End If
                .Holding = Xl_LookupHolding1.Holding
                .CustomerCluster = Xl_LookupCustomerCluster1.CustomerCluster
                .Incoterm = Xl_LookupIncoterm1.Value
                'Xl_Contact_Cli1.DeliveryAdr.Update(mContact, Adr.Codis.Fiscal)

            End With
            Return _Customer
        End Get
    End Property

    Public ReadOnly Property DeliveryAddress As DTOAddress
        Get
            Dim retval As DTOAddress = Nothing
            If CheckBoxAdr.Checked Then
                retval = Xl_Adr1.Address
            End If
            Return retval
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
        CheckBoxwebatlaspriority.Visible = Not Delega
        'CheckBoxValorarAlbarans.Visible = Not Delega
    End Sub

    Private Sub CheckBoxAdr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAdr.CheckedChanged
        If mAllowEvents Then
            Xl_Adr1.Visible = CheckBoxAdr.Checked
            If CheckBoxAdr.Checked Then
                If Xl_Adr1.Address Is Nothing Then
                    Dim oAddress As New DTOAddress()
                    With oAddress
                        .Src = _Customer
                        .Codi = DTOAddress.Codis.Entregas
                    End With
                    Xl_Adr1.Load(oAddress)
                End If
            End If
            RaiseEvent ChangedAdrEntregas()
        End If
    End Sub

    Private Sub Xl_Adr1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Adr1.AfterUpdate
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
        CheckBoxReq.CheckedChanged,
        CheckBoxNoRep.CheckedChanged,
        CheckBoxwebatlaspriority.CheckedChanged,
        CheckBoxNoWeb.CheckedChanged,
        CheckBoxNoIncentius.CheckedChanged,
        CheckBoxValorarAlbarans.CheckedChanged,
        Xl_LookupPortsCondicions1.AfterUpdate,
        ComboBoxCash.SelectedIndexChanged,
        TextBoxLimit.TextChanged,
        TextBoxWarnAlbs.TextChanged,
        CheckBoxPlatform.CheckedChanged,
        Xl_ContactPlatform.AfterUpdate,
          ComboBoxFraPrintMode.SelectedIndexChanged,
           Xl_LookupHolding1.AfterUpdate,
            Xl_FormaDePago1.AfterUpdate,
             Xl_Adr1.AfterUpdate,
             Xl_ContactCcx.AfterUpdate,
              Xl_LookupCustomerCluster1.AfterUpdate,
               Xl_LookupIncoterm1.AfterUpdate


        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub CheckBoxExport_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExport.CheckedChanged
        CheckBoxCEE.Visible = CheckBoxExport.Checked
        CheckBoxIVA.Visible = Not CheckBoxExport.Checked
        CheckBoxReq.Visible = CheckBoxIVA.Checked And Not CheckBoxExport.Checked
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub CheckBoxIVA_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIVA.CheckedChanged
        CheckBoxReq.Visible = CheckBoxIVA.Checked
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

    Private Sub Xl_AmtCurCYC_AfterUpdateCur(ByVal oCur As DTOCur)
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Private Sub Xl_AmtCurCYC_AfterUpdateValue(ByVal oAmt As DTOAmt)
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub

    Public Sub SetDirty()
        RaiseEvent ChangedClient()
    End Sub

    Private Sub ButtonCYC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCYC.Click
        Dim oCustomer As DTOCustomer = _Customer
        Dim oFrm As New Frm_Client_Risc(oCustomer)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshCredit
        oFrm.Show()
    End Sub

    Private Async Sub RefreshCredit(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        Dim oCreditLimit = Await FEB.Risc.CreditLimit(_Customer, exs)
        If exs.Count = 0 And oCreditLimit IsNot Nothing Then
            TextBoxLimit.Text = oCreditLimit.Formatted
        Else
            UIHelper.WarnError(exs)
        End If

        Dim oCreditDisposat = Await FEB.Risc.CreditDisposat(_Customer, exs)
        If exs.Count = 0 Then
            TextBoxDisposat.Text = oCreditDisposat.Formatted
        Else
            UIHelper.WarnError(exs)
        End If

        Dim oCreditDisponible = Await FEB.Risc.CreditDisponible(_Customer, exs)
        If exs.Count = 0 Then
            TextBoxDisponible.Text = oCreditDisponible.Formatted
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub CheckBoxFpgIndepent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFpgIndepent.Click
        Dim Delega As Boolean = CheckBoxFacturarA.Checked
        Xl_FormaDePago1.Visible = (Not Delega) Or CheckBoxFpgIndepent.Checked
        If mAllowEvents Then
            RaiseEvent ChangedClient()
        End If
    End Sub


    Private Sub Xl_FormaDePago1_AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Xl_FormaDePago1.AfterUpdate
        RaiseEvent ChangedClient()
    End Sub

    Private Sub CheckBoxPlatform_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPlatform.CheckedChanged
        If mAllowEvents Then
            Xl_ContactPlatform.Visible = CheckBoxPlatform.Checked
        End If
    End Sub


    Private Sub CheckBoxHorarioEntregas_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHorarioEntregas.CheckedChanged
        If mAllowEvents Then
            TextBoxHorarioEntregas.Visible = CheckBoxHorarioEntregas.Checked
            RaiseEvent ChangedClient()
        End If
    End Sub






    Private Sub Xl_LookupCustomerCluster1_onLookUpRequest(sender As Object, e As MatEventArgs) Handles Xl_LookupCustomerCluster1.onLookUpRequest
        Dim oFrm As New Frm_CustomerClusters(_Customer)
        AddHandler oFrm.itemSelected, AddressOf onClusterSelected
        oFrm.Show()
    End Sub

    Private Sub onClusterSelected(sender As Object, e As MatEventArgs)
        Xl_LookupCustomerCluster1.CustomerCluster = e.Argument
        RaiseEvent ChangedClient()
    End Sub


End Class
