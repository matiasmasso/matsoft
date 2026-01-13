Public Class Wz_Proveidor_NewFra
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonCash As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBanc As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCredit As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVISA As System.Windows.Forms.RadioButton
    Friend WithEvents LabelFpg As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCREDIT As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCFP As System.Windows.Forms.TabPage
    Friend WithEvents Xl_AmtTot As Xl_Amount
    Friend WithEvents Xl_AmtDevengat As Xl_Amount
    Friend WithEvents Xl_AmtCurBaseExento As Xl_AmountCur
    Friend WithEvents TextBoxConcept As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxPndObs As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents LabelParcial As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageVISA As System.Windows.Forms.TabPage
    Friend WithEvents TabPageBanc As System.Windows.Forms.TabPage
    Friend WithEvents TabPageEND As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelCca As System.Windows.Forms.Label
    Friend WithEvents ContextMenuPrv As System.Windows.Forms.ContextMenu
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_CtaBaseIva As Xl_Cta
    Friend WithEvents ComboBoxIRPFcta As System.Windows.Forms.ComboBox
    Friend WithEvents LabelExchange As System.Windows.Forms.Label
    Friend WithEvents TextBoxExchangeRate As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Bancs_Select2 As Xl_Bancs_Select
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_ContactPrv As Xl_Contact2
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
    Friend WithEvents Xl_LookupVisa1 As Xl_LookupVisaCard
    Friend WithEvents Xl_LookupProjecte1 As Xl_LookupProjecte
    Friend WithEvents Label8 As Label
    Friend WithEvents Xl_BaseQuotaIva As Xl_BaseQuota
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Xl_BaseQuotaIrpf As Xl_BaseQuota
    Friend WithEvents Xl_CtaExentoIva As Xl_Cta
    Friend WithEvents CheckBoxIrpf As CheckBox
    Friend WithEvents CheckBoxExento As CheckBox
    Friend WithEvents CheckBoxSujeto As CheckBox
    Friend WithEvents ComboBoxCausaExempcio As ComboBox
    Friend WithEvents ComboBoxTipoFra As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxDsc As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Xl_BaseQuotaIva2 As Xl_BaseQuota
    Friend WithEvents Xl_CtaBaseIva2 As Xl_Cta
    Friend WithEvents Xl_BaseQuotaIva1 As Xl_BaseQuota
    Friend WithEvents Xl_CtaBaseIva1 As Xl_Cta
    Friend WithEvents Label12 As Label
    Friend WithEvents ComboBoxRegEspOTrascs As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Xl_CtaCreditora As Xl_Cta
    Friend WithEvents PictureBoxVISA As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wz_Proveidor_NewFra))
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ComboBoxRegEspOTrascs = New System.Windows.Forms.ComboBox()
        Me.Xl_BaseQuotaIva1 = New Mat.Net.Xl_BaseQuota()
        Me.Xl_CtaBaseIva1 = New Mat.Net.Xl_Cta()
        Me.Xl_BaseQuotaIva2 = New Mat.Net.Xl_BaseQuota()
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_CtaBaseIva2 = New Mat.Net.Xl_Cta()
        Me.ComboBoxTipoFra = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxCausaExempcio = New System.Windows.Forms.ComboBox()
        Me.CheckBoxExento = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSujeto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIrpf = New System.Windows.Forms.CheckBox()
        Me.Xl_BaseQuotaIrpf = New Mat.Net.Xl_BaseQuota()
        Me.Xl_CtaExentoIva = New Mat.Net.Xl_Cta()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Xl_BaseQuotaIva = New Mat.Net.Xl_BaseQuota()
        Me.Xl_CtaBaseIva = New Mat.Net.Xl_Cta()
        Me.Xl_LookupProjecte1 = New Mat.Net.Xl_LookupProjecte()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_ContactPrv = New Mat.Net.Xl_Contact2()
        Me.TextBoxExchangeRate = New System.Windows.Forms.TextBox()
        Me.LabelExchange = New System.Windows.Forms.Label()
        Me.ComboBoxIRPFcta = New System.Windows.Forms.ComboBox()
        Me.Xl_AmtTot = New Mat.Net.Xl_Amount()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_AmtDevengat = New Mat.Net.Xl_Amount()
        Me.LabelParcial = New System.Windows.Forms.Label()
        Me.Xl_AmtCurBaseExento = New Mat.Net.Xl_AmountCur()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.TextBoxFra = New System.Windows.Forms.TextBox()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPageCFP = New System.Windows.Forms.TabPage()
        Me.LabelFpg = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonVISA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCredit = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBanc = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCash = New System.Windows.Forms.RadioButton()
        Me.TabPageCREDIT = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxPndObs = New System.Windows.Forms.TextBox()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabPageVISA = New System.Windows.Forms.TabPage()
        Me.Xl_LookupVisa1 = New Mat.Net.Xl_LookupVisaCard()
        Me.PictureBoxVISA = New System.Windows.Forms.PictureBox()
        Me.TabPageBanc = New System.Windows.Forms.TabPage()
        Me.Xl_Bancs_Select2 = New Mat.Net.Xl_Bancs_Select()
        Me.TabPageEND = New System.Windows.Forms.TabPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LabelCca = New System.Windows.Forms.Label()
        Me.ContextMenuPrv = New System.Windows.Forms.ContextMenu()
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_DocFile1 = New Mat.Net.Xl_DocFile()
        Me.Xl_CtaCreditora = New Mat.Net.Xl_Cta()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabPageCFP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageCREDIT.SuspendLayout()
        Me.TabPageVISA.SuspendLayout()
        CType(Me.PictureBoxVISA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageBanc.SuspendLayout()
        Me.TabPageEND.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(5, 550)
        Me.ButtonPrevious.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 90
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(727, 550)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 24
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(623, 550)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 23
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageCFP)
        Me.TabControl1.Controls.Add(Me.TabPageCREDIT)
        Me.TabControl1.Controls.Add(Me.TabPageVISA)
        Me.TabControl1.Controls.Add(Me.TabPageBanc)
        Me.TabControl1.Controls.Add(Me.TabPageEND)
        Me.TabControl1.Location = New System.Drawing.Point(372, 59)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(462, 486)
        Me.TabControl1.TabIndex = 4
        Me.TabControl1.TabStop = False
        '
        'TabPageGral
        '
        Me.TabPageGral.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageGral.Controls.Add(Me.Label12)
        Me.TabPageGral.Controls.Add(Me.ComboBoxRegEspOTrascs)
        Me.TabPageGral.Controls.Add(Me.Xl_BaseQuotaIva1)
        Me.TabPageGral.Controls.Add(Me.Xl_CtaBaseIva1)
        Me.TabPageGral.Controls.Add(Me.Xl_BaseQuotaIva2)
        Me.TabPageGral.Controls.Add(Me.TextBoxDsc)
        Me.TabPageGral.Controls.Add(Me.Label7)
        Me.TabPageGral.Controls.Add(Me.Xl_CtaBaseIva2)
        Me.TabPageGral.Controls.Add(Me.ComboBoxTipoFra)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.ComboBoxCausaExempcio)
        Me.TabPageGral.Controls.Add(Me.CheckBoxExento)
        Me.TabPageGral.Controls.Add(Me.CheckBoxSujeto)
        Me.TabPageGral.Controls.Add(Me.CheckBoxIrpf)
        Me.TabPageGral.Controls.Add(Me.Xl_BaseQuotaIrpf)
        Me.TabPageGral.Controls.Add(Me.Xl_CtaExentoIva)
        Me.TabPageGral.Controls.Add(Me.Label16)
        Me.TabPageGral.Controls.Add(Me.Label15)
        Me.TabPageGral.Controls.Add(Me.Label14)
        Me.TabPageGral.Controls.Add(Me.Label13)
        Me.TabPageGral.Controls.Add(Me.Xl_BaseQuotaIva)
        Me.TabPageGral.Controls.Add(Me.Xl_CtaBaseIva)
        Me.TabPageGral.Controls.Add(Me.Xl_LookupProjecte1)
        Me.TabPageGral.Controls.Add(Me.Label8)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactPrv)
        Me.TabPageGral.Controls.Add(Me.TextBoxExchangeRate)
        Me.TabPageGral.Controls.Add(Me.LabelExchange)
        Me.TabPageGral.Controls.Add(Me.ComboBoxIRPFcta)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtTot)
        Me.TabPageGral.Controls.Add(Me.Label9)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtDevengat)
        Me.TabPageGral.Controls.Add(Me.LabelParcial)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtCurBaseExento)
        Me.TabPageGral.Controls.Add(Me.TextBoxConcept)
        Me.TabPageGral.Controls.Add(Me.TextBoxFra)
        Me.TabPageGral.Controls.Add(Me.DateTimePickerFch)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 25)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Size = New System.Drawing.Size(454, 457)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 142)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 13)
        Me.Label12.TabIndex = 98
        Me.Label12.Text = "Reg.Esp.O Trasc"
        '
        'ComboBoxRegEspOTrascs
        '
        Me.ComboBoxRegEspOTrascs.FormattingEnabled = True
        Me.ComboBoxRegEspOTrascs.Location = New System.Drawing.Point(125, 139)
        Me.ComboBoxRegEspOTrascs.Name = "ComboBoxRegEspOTrascs"
        Me.ComboBoxRegEspOTrascs.Size = New System.Drawing.Size(321, 21)
        Me.ComboBoxRegEspOTrascs.TabIndex = 6
        '
        'Xl_BaseQuotaIva1
        '
        Me.Xl_BaseQuotaIva1.EditQuotaAllowed = False
        Me.Xl_BaseQuotaIva1.Location = New System.Drawing.Point(125, 237)
        Me.Xl_BaseQuotaIva1.Name = "Xl_BaseQuotaIva1"
        Me.Xl_BaseQuotaIva1.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva1.TabIndex = 11
        '
        'Xl_CtaBaseIva1
        '
        Me.Xl_CtaBaseIva1.Cta = Nothing
        Me.Xl_CtaBaseIva1.Location = New System.Drawing.Point(319, 237)
        Me.Xl_CtaBaseIva1.Name = "Xl_CtaBaseIva1"
        Me.Xl_CtaBaseIva1.Size = New System.Drawing.Size(128, 20)
        Me.Xl_CtaBaseIva1.TabIndex = 12
        '
        'Xl_BaseQuotaIva2
        '
        Me.Xl_BaseQuotaIva2.EditQuotaAllowed = False
        Me.Xl_BaseQuotaIva2.Location = New System.Drawing.Point(125, 260)
        Me.Xl_BaseQuotaIva2.Name = "Xl_BaseQuotaIva2"
        Me.Xl_BaseQuotaIva2.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva2.TabIndex = 13
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(125, 166)
        Me.TextBoxDsc.MaxLength = 15
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(321, 20)
        Me.TextBoxDsc.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 169)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Descripció:"
        '
        'Xl_CtaBaseIva2
        '
        Me.Xl_CtaBaseIva2.Cta = Nothing
        Me.Xl_CtaBaseIva2.Location = New System.Drawing.Point(319, 260)
        Me.Xl_CtaBaseIva2.Name = "Xl_CtaBaseIva2"
        Me.Xl_CtaBaseIva2.Size = New System.Drawing.Size(128, 20)
        Me.Xl_CtaBaseIva2.TabIndex = 14
        '
        'ComboBoxTipoFra
        '
        Me.ComboBoxTipoFra.FormattingEnabled = True
        Me.ComboBoxTipoFra.Location = New System.Drawing.Point(125, 112)
        Me.ComboBoxTipoFra.Name = "ComboBoxTipoFra"
        Me.ComboBoxTipoFra.Size = New System.Drawing.Size(321, 21)
        Me.ComboBoxTipoFra.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "Tipo Factura:"
        '
        'ComboBoxCausaExempcio
        '
        Me.ComboBoxCausaExempcio.FormattingEnabled = True
        Me.ComboBoxCausaExempcio.ItemHeight = 13
        Me.ComboBoxCausaExempcio.Location = New System.Drawing.Point(235, 285)
        Me.ComboBoxCausaExempcio.Name = "ComboBoxCausaExempcio"
        Me.ComboBoxCausaExempcio.Size = New System.Drawing.Size(78, 21)
        Me.ComboBoxCausaExempcio.TabIndex = 14
        '
        'CheckBoxExento
        '
        Me.CheckBoxExento.AutoSize = True
        Me.CheckBoxExento.Location = New System.Drawing.Point(17, 288)
        Me.CheckBoxExento.Name = "CheckBoxExento"
        Me.CheckBoxExento.Size = New System.Drawing.Size(96, 17)
        Me.CheckBoxExento.TabIndex = 15
        Me.CheckBoxExento.TabStop = False
        Me.CheckBoxExento.Text = "Exempt de IVA"
        Me.CheckBoxExento.UseVisualStyleBackColor = True
        '
        'CheckBoxSujeto
        '
        Me.CheckBoxSujeto.AutoSize = True
        Me.CheckBoxSujeto.Location = New System.Drawing.Point(17, 213)
        Me.CheckBoxSujeto.Name = "CheckBoxSujeto"
        Me.CheckBoxSujeto.Size = New System.Drawing.Size(97, 17)
        Me.CheckBoxSujeto.TabIndex = 8
        Me.CheckBoxSujeto.TabStop = False
        Me.CheckBoxSujeto.Text = "Subjecte a IVA"
        Me.CheckBoxSujeto.UseVisualStyleBackColor = True
        '
        'CheckBoxIrpf
        '
        Me.CheckBoxIrpf.AutoSize = True
        Me.CheckBoxIrpf.Location = New System.Drawing.Point(17, 336)
        Me.CheckBoxIrpf.Name = "CheckBoxIrpf"
        Me.CheckBoxIrpf.Size = New System.Drawing.Size(41, 17)
        Me.CheckBoxIrpf.TabIndex = 20
        Me.CheckBoxIrpf.TabStop = False
        Me.CheckBoxIrpf.Text = "Irpf"
        Me.CheckBoxIrpf.UseVisualStyleBackColor = True
        '
        'Xl_BaseQuotaIrpf
        '
        Me.Xl_BaseQuotaIrpf.EditQuotaAllowed = False
        Me.Xl_BaseQuotaIrpf.Location = New System.Drawing.Point(125, 335)
        Me.Xl_BaseQuotaIrpf.Name = "Xl_BaseQuotaIrpf"
        Me.Xl_BaseQuotaIrpf.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIrpf.TabIndex = 21
        Me.Xl_BaseQuotaIrpf.Visible = False
        '
        'Xl_CtaExentoIva
        '
        Me.Xl_CtaExentoIva.Cta = Nothing
        Me.Xl_CtaExentoIva.Location = New System.Drawing.Point(319, 286)
        Me.Xl_CtaExentoIva.Name = "Xl_CtaExentoIva"
        Me.Xl_CtaExentoIva.Size = New System.Drawing.Size(128, 20)
        Me.Xl_CtaExentoIva.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Location = New System.Drawing.Point(319, 194)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(54, 16)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "Compte:"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Location = New System.Drawing.Point(256, 194)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 16)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "Quota:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Location = New System.Drawing.Point(213, 194)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 16)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Tipus:"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Location = New System.Drawing.Point(123, 194)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 16)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Base:"
        '
        'Xl_BaseQuotaIva
        '
        Me.Xl_BaseQuotaIva.EditQuotaAllowed = False
        Me.Xl_BaseQuotaIva.Location = New System.Drawing.Point(125, 213)
        Me.Xl_BaseQuotaIva.Name = "Xl_BaseQuotaIva"
        Me.Xl_BaseQuotaIva.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva.TabIndex = 9
        '
        'Xl_CtaBaseIva
        '
        Me.Xl_CtaBaseIva.Cta = Nothing
        Me.Xl_CtaBaseIva.Location = New System.Drawing.Point(319, 213)
        Me.Xl_CtaBaseIva.Name = "Xl_CtaBaseIva"
        Me.Xl_CtaBaseIva.Size = New System.Drawing.Size(128, 20)
        Me.Xl_CtaBaseIva.TabIndex = 10
        '
        'Xl_LookupProjecte1
        '
        Me.Xl_LookupProjecte1.IsDirty = False
        Me.Xl_LookupProjecte1.Location = New System.Drawing.Point(270, 61)
        Me.Xl_LookupProjecte1.Name = "Xl_LookupProjecte1"
        Me.Xl_LookupProjecte1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProjecte1.Projecte = Nothing
        Me.Xl_LookupProjecte1.ReadOnlyLookup = False
        Me.Xl_LookupProjecte1.Size = New System.Drawing.Size(177, 20)
        Me.Xl_LookupProjecte1.TabIndex = 2
        Me.Xl_LookupProjecte1.TabStop = False
        Me.Xl_LookupProjecte1.Value = Nothing
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Location = New System.Drawing.Point(219, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 16)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Projecte:"
        '
        'Xl_ContactPrv
        '
        Me.Xl_ContactPrv.Contact = Nothing
        Me.Xl_ContactPrv.Emp = Nothing
        Me.Xl_ContactPrv.Location = New System.Drawing.Point(125, 8)
        Me.Xl_ContactPrv.Name = "Xl_ContactPrv"
        Me.Xl_ContactPrv.ReadOnly = False
        Me.Xl_ContactPrv.Size = New System.Drawing.Size(322, 20)
        Me.Xl_ContactPrv.TabIndex = 0
        Me.Xl_ContactPrv.TabStop = False
        '
        'TextBoxExchangeRate
        '
        Me.TextBoxExchangeRate.Location = New System.Drawing.Point(319, 310)
        Me.TextBoxExchangeRate.Name = "TextBoxExchangeRate"
        Me.TextBoxExchangeRate.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxExchangeRate.TabIndex = 19
        Me.TextBoxExchangeRate.TabStop = False
        Me.TextBoxExchangeRate.Visible = False
        '
        'LabelExchange
        '
        Me.LabelExchange.AutoSize = True
        Me.LabelExchange.Location = New System.Drawing.Point(276, 313)
        Me.LabelExchange.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelExchange.Name = "LabelExchange"
        Me.LabelExchange.Size = New System.Drawing.Size(37, 13)
        Me.LabelExchange.TabIndex = 24
        Me.LabelExchange.Text = "Canvi:"
        Me.LabelExchange.Visible = False
        '
        'ComboBoxIRPFcta
        '
        Me.ComboBoxIRPFcta.FormattingEnabled = True
        Me.ComboBoxIRPFcta.ItemHeight = 13
        Me.ComboBoxIRPFcta.Location = New System.Drawing.Point(319, 334)
        Me.ComboBoxIRPFcta.Name = "ComboBoxIRPFcta"
        Me.ComboBoxIRPFcta.Size = New System.Drawing.Size(128, 21)
        Me.ComboBoxIRPFcta.TabIndex = 22
        Me.ComboBoxIRPFcta.TabStop = False
        Me.ComboBoxIRPFcta.Visible = False
        '
        'Xl_AmtTot
        '
        Me.Xl_AmtTot.Amt = Nothing
        Me.Xl_AmtTot.Location = New System.Drawing.Point(125, 362)
        Me.Xl_AmtTot.Name = "Xl_AmtTot"
        Me.Xl_AmtTot.ReadOnly = True
        Me.Xl_AmtTot.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtTot.TabIndex = 19
        Me.Xl_AmtTot.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(14, 364)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Total:"
        '
        'Xl_AmtDevengat
        '
        Me.Xl_AmtDevengat.Amt = Nothing
        Me.Xl_AmtDevengat.Location = New System.Drawing.Point(125, 310)
        Me.Xl_AmtDevengat.Name = "Xl_AmtDevengat"
        Me.Xl_AmtDevengat.ReadOnly = True
        Me.Xl_AmtDevengat.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtDevengat.TabIndex = 19
        Me.Xl_AmtDevengat.TabStop = False
        '
        'LabelParcial
        '
        Me.LabelParcial.BackColor = System.Drawing.SystemColors.Control
        Me.LabelParcial.Location = New System.Drawing.Point(14, 310)
        Me.LabelParcial.Name = "LabelParcial"
        Me.LabelParcial.Size = New System.Drawing.Size(96, 16)
        Me.LabelParcial.TabIndex = 15
        Me.LabelParcial.Text = "Base devengada:"
        '
        'Xl_AmtCurBaseExento
        '
        Me.Xl_AmtCurBaseExento.Amt = Nothing
        Me.Xl_AmtCurBaseExento.Location = New System.Drawing.Point(125, 286)
        Me.Xl_AmtCurBaseExento.Name = "Xl_AmtCurBaseExento"
        Me.Xl_AmtCurBaseExento.Size = New System.Drawing.Size(104, 20)
        Me.Xl_AmtCurBaseExento.TabIndex = 16
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Location = New System.Drawing.Point(125, 87)
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.Size = New System.Drawing.Size(322, 20)
        Me.TextBoxConcept.TabIndex = 4
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Location = New System.Drawing.Point(125, 61)
        Me.TextBoxFra.MaxLength = 18
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.Size = New System.Drawing.Size(84, 20)
        Me.TextBoxFra.TabIndex = 3
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(125, 35)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(84, 20)
        Me.DateTimePickerFch.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(14, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Concepte:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(14, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Factura:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(14, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Data:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(14, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Proveidor:"
        '
        'TabPageCFP
        '
        Me.TabPageCFP.Controls.Add(Me.Label17)
        Me.TabPageCFP.Controls.Add(Me.Xl_CtaCreditora)
        Me.TabPageCFP.Controls.Add(Me.LabelFpg)
        Me.TabPageCFP.Controls.Add(Me.GroupBox1)
        Me.TabPageCFP.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCFP.Name = "TabPageCFP"
        Me.TabPageCFP.Size = New System.Drawing.Size(454, 457)
        Me.TabPageCFP.TabIndex = 1
        Me.TabPageCFP.Text = "FORMA DE PAGAMENT"
        '
        'LabelFpg
        '
        Me.LabelFpg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFpg.Location = New System.Drawing.Point(16, 8)
        Me.LabelFpg.Name = "LabelFpg"
        Me.LabelFpg.Size = New System.Drawing.Size(431, 104)
        Me.LabelFpg.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonVISA)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCredit)
        Me.GroupBox1.Controls.Add(Me.RadioButtonBanc)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCash)
        Me.GroupBox1.Location = New System.Drawing.Point(120, 127)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(223, 152)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Forma de pagament:"
        '
        'RadioButtonVISA
        '
        Me.RadioButtonVISA.Location = New System.Drawing.Point(24, 72)
        Me.RadioButtonVISA.Name = "RadioButtonVISA"
        Me.RadioButtonVISA.Size = New System.Drawing.Size(64, 16)
        Me.RadioButtonVISA.TabIndex = 3
        Me.RadioButtonVISA.Text = "VISA"
        '
        'RadioButtonCredit
        '
        Me.RadioButtonCredit.Location = New System.Drawing.Point(24, 96)
        Me.RadioButtonCredit.Name = "RadioButtonCredit"
        Me.RadioButtonCredit.Size = New System.Drawing.Size(64, 16)
        Me.RadioButtonCredit.TabIndex = 2
        Me.RadioButtonCredit.Text = "Crèdit"
        '
        'RadioButtonBanc
        '
        Me.RadioButtonBanc.Location = New System.Drawing.Point(24, 48)
        Me.RadioButtonBanc.Name = "RadioButtonBanc"
        Me.RadioButtonBanc.Size = New System.Drawing.Size(104, 16)
        Me.RadioButtonBanc.TabIndex = 1
        Me.RadioButtonBanc.Text = "Càrrec al banc"
        '
        'RadioButtonCash
        '
        Me.RadioButtonCash.Location = New System.Drawing.Point(24, 24)
        Me.RadioButtonCash.Name = "RadioButtonCash"
        Me.RadioButtonCash.Size = New System.Drawing.Size(64, 16)
        Me.RadioButtonCash.TabIndex = 0
        Me.RadioButtonCash.Text = "Cash"
        '
        'TabPageCREDIT
        '
        Me.TabPageCREDIT.Controls.Add(Me.Label1)
        Me.TabPageCREDIT.Controls.Add(Me.ComboBoxCfp)
        Me.TabPageCREDIT.Controls.Add(Me.Label11)
        Me.TabPageCREDIT.Controls.Add(Me.TextBoxPndObs)
        Me.TabPageCREDIT.Controls.Add(Me.DateTimePickerVto)
        Me.TabPageCREDIT.Controls.Add(Me.Label10)
        Me.TabPageCREDIT.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCREDIT.Name = "TabPageCREDIT"
        Me.TabPageCREDIT.Size = New System.Drawing.Size(454, 457)
        Me.TabPageCREDIT.TabIndex = 2
        Me.TabPageCREDIT.Text = "CREDIT"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(72, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 16)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "forma de pagament:"
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(176, 75)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(207, 21)
        Me.ComboBoxCfp.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(72, 101)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 16)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "observacions:"
        '
        'TextBoxPndObs
        '
        Me.TextBoxPndObs.Location = New System.Drawing.Point(176, 101)
        Me.TextBoxPndObs.Name = "TextBoxPndObs"
        Me.TextBoxPndObs.Size = New System.Drawing.Size(208, 20)
        Me.TextBoxPndObs.TabIndex = 3
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(176, 48)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerVto.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(72, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "venciment:"
        '
        'TabPageVISA
        '
        Me.TabPageVISA.Controls.Add(Me.Xl_LookupVisa1)
        Me.TabPageVISA.Controls.Add(Me.PictureBoxVISA)
        Me.TabPageVISA.Location = New System.Drawing.Point(4, 25)
        Me.TabPageVISA.Name = "TabPageVISA"
        Me.TabPageVISA.Size = New System.Drawing.Size(454, 457)
        Me.TabPageVISA.TabIndex = 4
        Me.TabPageVISA.Text = "VISA"
        '
        'Xl_LookupVisa1
        '
        Me.Xl_LookupVisa1.IsDirty = False
        Me.Xl_LookupVisa1.Location = New System.Drawing.Point(27, 93)
        Me.Xl_LookupVisa1.Name = "Xl_LookupVisa1"
        Me.Xl_LookupVisa1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVisa1.ReadOnlyLookup = False
        Me.Xl_LookupVisa1.Size = New System.Drawing.Size(376, 20)
        Me.Xl_LookupVisa1.TabIndex = 2
        Me.Xl_LookupVisa1.Value = Nothing
        Me.Xl_LookupVisa1.VisaCard = Nothing
        '
        'PictureBoxVISA
        '
        Me.PictureBoxVISA.Image = CType(resources.GetObject("PictureBoxVISA.Image"), System.Drawing.Image)
        Me.PictureBoxVISA.Location = New System.Drawing.Point(27, 119)
        Me.PictureBoxVISA.Name = "PictureBoxVISA"
        Me.PictureBoxVISA.Size = New System.Drawing.Size(376, 48)
        Me.PictureBoxVISA.TabIndex = 1
        Me.PictureBoxVISA.TabStop = False
        '
        'TabPageBanc
        '
        Me.TabPageBanc.Controls.Add(Me.Xl_Bancs_Select2)
        Me.TabPageBanc.Location = New System.Drawing.Point(4, 25)
        Me.TabPageBanc.Name = "TabPageBanc"
        Me.TabPageBanc.Size = New System.Drawing.Size(454, 457)
        Me.TabPageBanc.TabIndex = 12
        Me.TabPageBanc.Text = "BANC"
        '
        'Xl_Bancs_Select2
        '
        Me.Xl_Bancs_Select2.Location = New System.Drawing.Point(25, 14)
        Me.Xl_Bancs_Select2.Name = "Xl_Bancs_Select2"
        Me.Xl_Bancs_Select2.Size = New System.Drawing.Size(384, 272)
        Me.Xl_Bancs_Select2.TabIndex = 0
        '
        'TabPageEND
        '
        Me.TabPageEND.Controls.Add(Me.PictureBox1)
        Me.TabPageEND.Controls.Add(Me.LabelCca)
        Me.TabPageEND.Location = New System.Drawing.Point(4, 25)
        Me.TabPageEND.Name = "TabPageEND"
        Me.TabPageEND.Size = New System.Drawing.Size(454, 457)
        Me.TabPageEND.TabIndex = 12
        Me.TabPageEND.Text = "FI"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(192, 40)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 96)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'LabelCca
        '
        Me.LabelCca.BackColor = System.Drawing.Color.Wheat
        Me.LabelCca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelCca.Location = New System.Drawing.Point(160, 159)
        Me.LabelCca.Name = "LabelCca"
        Me.LabelCca.Size = New System.Drawing.Size(152, 32)
        Me.LabelCca.TabIndex = 32
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(675, 5)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 8
        Me.PictureBoxLogo.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 547)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(834, 31)
        Me.Panel1.TabIndex = 45
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1._DefaultMimeCod = MatHelperStd.MimeCods.Pdf
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.IsInedit = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(5, 58)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 46
        '
        'Xl_CtaCreditora
        '
        Me.Xl_CtaCreditora.Cta = Nothing
        Me.Xl_CtaCreditora.Location = New System.Drawing.Point(120, 308)
        Me.Xl_CtaCreditora.Name = "Xl_CtaCreditora"
        Me.Xl_CtaCreditora.Size = New System.Drawing.Size(223, 20)
        Me.Xl_CtaCreditora.TabIndex = 100
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(120, 286)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(84, 13)
        Me.Label17.TabIndex = 101
        Me.Label17.Text = "Compte creditor:"
        '
        'Wz_Proveidor_NewFra
        '
        Me.ClientSize = New System.Drawing.Size(834, 578)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Wz_Proveidor_NewFra"
        Me.Text = "NOVA FACTURA DE PROVEIDOR"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.TabPageCFP.ResumeLayout(False)
        Me.TabPageCFP.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPageCREDIT.ResumeLayout(False)
        Me.TabPageCREDIT.PerformLayout()
        Me.TabPageVISA.ResumeLayout(False)
        CType(Me.PictureBoxVISA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageBanc.ResumeLayout(False)
        Me.TabPageEND.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _Proveidor As DTOProveidor
    Private _Docfile As DTODocFile
    Private _Fch As Date
    Private _Ccd As DTOCca.CcdEnum = DTOCca.CcdEnum.FacturaProveidor
    Private _Ctas As List(Of DTOPgcCta)
    Private _Lang As DTOLang = Current.Session.Lang
    Private _Importacio As DTOImportacio
    Private mIRPFCtas As New List(Of DTOPgcCta)
    Private mShowDivisa As Boolean = False
    Private _CurExchangeRate As DTOCurExchangeRate
    Private _TaxIva As DTOTax
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        Gral
        Fpg
        Credit
        Visa
        Banc
        Fi
    End Enum


    Public Enum Cfps
        Not_Set
        Cash
        Banc
        Visa
        Credit
    End Enum

    Private Enum IRPFctas
        Treballadors
        Professionals
        Lloguers
    End Enum

    Public Sub New(oPrv As DTOProveidor, DtFch As Date, Optional oDocFile As DTODocFile = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oPrv
        _Fch = DtFch
        _Docfile = oDocFile
    End Sub

    Public Sub New(ByVal oImportacio As DTOImportacio, Optional oDocFile As DTODocFile = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Importacio = oImportacio
        _Proveidor = oImportacio.Proveidor
        _Docfile = oDocFile
    End Sub

    Private Async Sub Wz_Proveidor_NewFra_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Await LoadComboboxes()
        _Ctas = Await FEB.PgcCtas.All(exs)
        If exs.Count = 0 Then
            If setProveidor(exs) Then
                If _Importacio Is Nothing Then
                    DateTimePickerFch.Value = _Fch
                Else
                    With _Importacio
                        Me.Text = String.Format("Nova factura (remesa {0})", .Id)
                        DateTimePickerFch.Value = .FchETD
                    End With
                End If
                Await Xl_DocFile1.Load(_Docfile)
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function setProveidor(exs As List(Of Exception)) As Boolean
        Dim DtFch As Date = DateTimePickerFch.Value

        If _Proveidor Is Nothing Then
        Else
            If FEB.Contact.Load(_Proveidor, exs) Then
                If FEB.Proveidor.Load(_Proveidor, exs) Then
                    PictureBoxLogo.Image = LegacyHelper.ImageHelper.Converter(_Proveidor.Logo)
                    If _Proveidor.DefaultCtaCarrec IsNot Nothing Then
                        If FEB.PgcCta.Load(_Proveidor.DefaultCtaCarrec, exs) Then
                            If _Proveidor.DefaultCtaCarrec.Plan.Equals(DTOApp.Current.PgcPlan) Then
                                Xl_CtaBaseIva.Cta = _Proveidor.DefaultCtaCarrec
                                Xl_CtaExentoIva.Cta = _Proveidor.DefaultCtaCarrec
                                TextBoxDsc.Text = _Proveidor.DefaultCtaCarrec.Nom.Esp
                            Else
                                MsgBox("A la fitxa d'aquest proveidor hi surt el compte '" & _Proveidor.DefaultCtaCarrec.Id & "' que correspon a un pla comptable obsolet")
                            End If

                            Dim oCur As DTOCur = _Proveidor.Cur
                            Dim oAmt As DTOAmt = DTOAmt.Factory(oCur)
                            Xl_AmtCurBaseExento.Amt = oAmt
                            refrescaDivisa()
                            If DTOContact.isIVASujeto(_Proveidor) Then
                                CheckBoxSujeto.Checked = True
                                Dim DcTipusIva = DTOTax.Closest(DTOTax.Codis.Iva_Standard, DtFch).Tipus
                                Dim oBaseQuota As New DTOBaseQuota(DTOAmt.Empty, DcTipusIva)
                                Xl_BaseQuotaIva.Load(oBaseQuota)
                                Xl_BaseQuotaIva.Visible = True
                                Xl_CtaBaseIva.Visible = True

                                Dim oBaseQuota1 As New DTOBaseQuota(DTOAmt.Empty, 0)
                                Xl_BaseQuotaIva1.Load(oBaseQuota1)
                                Xl_BaseQuotaIva1.Visible = True
                                Xl_CtaBaseIva1.Visible = True

                                Dim oBaseQuota2 As New DTOBaseQuota(DTOAmt.Empty, 0)
                                Xl_BaseQuotaIva2.Load(oBaseQuota2)
                                Xl_BaseQuotaIva2.Visible = True
                                Xl_CtaBaseIva2.Visible = True

                                CheckBoxExento.Checked = False
                                Xl_AmtCurBaseExento.Visible = False
                                Xl_CtaExentoIva.Visible = False
                                ComboBoxCausaExempcio.Visible = False

                                ComboBoxRegEspOTrascs.SelectedValue = "01"
                            Else
                                CheckBoxSujeto.Checked = False
                                Xl_BaseQuotaIva.Visible = False
                                Xl_CtaBaseIva.Visible = False
                                Xl_BaseQuotaIva1.Visible = False
                                Xl_CtaBaseIva1.Visible = False
                                Xl_BaseQuotaIva2.Visible = False
                                Xl_CtaBaseIva2.Visible = False

                                CheckBoxExento.Checked = True
                                Xl_AmtCurBaseExento.Visible = True
                                Xl_CtaExentoIva.Visible = True

                                ComboBoxRegEspOTrascs.SelectedValue = DTOContact.ClaveRegimenEspecialOTrascendencia(_Proveidor)
                                ComboBoxCausaExempcio.SelectedValue = DTOContact.ClaveCausaExempcio(_Proveidor)
                            End If

                            Dim TipoIrpf = DTOProveidor.IRPF(_Proveidor, DtFch)
                            If TipoIrpf <> 0 Then
                                CheckBoxIrpf.Checked = True
                                Xl_BaseQuotaIrpf.Visible = True
                                ComboBoxIRPFcta.Visible = True
                                Dim oIrpf As New DTOBaseQuota(DTOAmt.Empty, TipoIrpf)
                                Xl_BaseQuotaIrpf.Load(oIrpf)
                            End If
                            LoadIRPFcuentas()
                            Calcula()
                            LabelFpg.Text = DTOPaymentTerms.CfpText(_Proveidor.PaymentTerms.Cod, _Lang)
                        End If
                    End If

                    SetCtaCreditora()

                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If


        End If

        Xl_ContactPrv.Contact = _Proveidor
        ComboBoxTipoFra.SelectedValue = "F1"
        _TaxIva = DTOTax.Closest(DTOTax.Codis.Iva_Standard, DateTimePickerFch.Value)


        root.TabControlHideTabLabels(TabControl1)

        Return exs.Count = 0
    End Function

    Private Sub SetCtaCreditora()
        Select Case CurrentCfp()
            Case Cfps.Cash
                Xl_CtaCreditora.Cta = _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Caixa)
            Case Cfps.Banc
                Xl_CtaCreditora.Cta = _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Bancs)
            Case Cfps.Credit
                If _Proveidor.defaultCtaCreditora IsNot Nothing Then
                    Xl_CtaCreditora.Cta = _Proveidor.defaultCtaCreditora
                Else
                    Xl_CtaCreditora.Cta = _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcCta.getCtaProveedors(Xl_AmtCurBaseExento.Amt.Cur))
                End If
            Case Cfps.Visa
                Xl_CtaCreditora.Cta = _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.VisasPagadas)
        End Select
    End Sub


    Private Function CurrentCfp() As Cfps
        If RadioButtonCash.Checked Then Return Cfps.Cash
        If RadioButtonBanc.Checked Then Return Cfps.Banc
        If RadioButtonVISA.Checked Then Return Cfps.Visa
        If RadioButtonCredit.Checked Then Return Cfps.Credit
        Return Cfps.Not_Set
    End Function

    Private Function ValidateForm(exs As List(Of Exception)) As Boolean
        If Not Xl_CtaBaseIva.Cta IsNot Nothing AndAlso Xl_CtaBaseIva.ValidateFch(DateTimePickerFch.Value) Then
            exs.Add(New Exception("el compte no correspon al pla comptable vigent a la data indicada"))
        End If
        If TextBoxDsc.Text = "" Then
            exs.Add(New Exception("cal posar una descripció"))
        End If
        If ComboBoxTipoFra.SelectedValue = "" Then
            exs.Add(New Exception("cal triar un tipus de factura"))
        End If
        If ComboBoxRegEspOTrascs.SelectedValue = "" Then
            exs.Add(New Exception("cal triar un tipus de Regim Especial"))
        End If
        If CheckBoxExento.Checked And ComboBoxCausaExempcio.SelectedValue = "" Then
            exs.Add(New Exception("cal triar una causa de exempció de Iva"))
        End If
        If (Xl_BaseQuotaIva.IsEmptyQuota And Not Xl_BaseQuotaIva.IsEmptyBase) Or (Xl_BaseQuotaIva1.IsEmptyQuota And Not Xl_BaseQuotaIva1.IsEmptyBase) Or (Xl_BaseQuotaIva2.IsEmptyQuota And Not Xl_BaseQuotaIva2.IsEmptyBase) Then
            exs.Add(New Exception("Les bases exemptes de Iva cal posar-les a la seva casella i indicar el motiu d'exempció"))
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Private Async Function Grabacion(exs As List(Of Exception)) As Task(Of DTOCca)
        Dim oCca = DTOCca.Factory(DateTimePickerFch.Value, Current.Session.User, _Ccd)
        With oCca
            .Concept = TextBoxConcept.Text
            .DocFile = Xl_DocFile1.Value
            .Projecte = Xl_LookupProjecte1.Projecte
            .BookFra = DTOBookFra.Factory(oCca, _Proveidor)
            With .BookFra
                .TipoFra = ComboBoxTipoFra.SelectedValue
                .ClaveRegimenEspecialOTrascendencia = ComboBoxRegEspOTrascs.SelectedValue
                .Cta = Xl_CtaBaseIva.Cta
                .Contact = _Proveidor
                .FraNum = TextBoxFra.Text
                .Dsc = TextBoxDsc.Text
                If Not Xl_BaseQuotaIva.IsEmptyQuota Then
                    Dim oSujeto As DTOBaseQuota = Xl_BaseQuotaIva.Value
                    oSujeto.Source = Xl_CtaBaseIva.Cta
                    .IvaBaseQuotas.Add(oSujeto)
                End If
                If Not Xl_BaseQuotaIva1.IsEmptyQuota Then
                    Dim oSujeto1 As DTOBaseQuota = Xl_BaseQuotaIva1.Value
                    oSujeto1.Source = Xl_CtaBaseIva1.Cta
                    .IvaBaseQuotas.Add(oSujeto1)
                End If
                If Not Xl_BaseQuotaIva2.IsEmptyQuota Then
                    Dim oSujeto2 As DTOBaseQuota = Xl_BaseQuotaIva2.Value
                    oSujeto2.Source = Xl_CtaBaseIva2.Cta
                    .IvaBaseQuotas.Add(oSujeto2)
                End If
                If Xl_AmtCurBaseExento.Amt.IsNotZero Then
                    Dim oExento As New DTOBaseQuota(Xl_AmtCurBaseExento.Amt)
                    oExento.Source = Xl_CtaExentoIva.Cta
                    .IvaBaseQuotas.Add(oExento)
                    .ClaveExenta = ComboBoxCausaExempcio.SelectedValue
                End If
                If Not Xl_BaseQuotaIrpf.IsEmptyQuota Then
                    .IrpfBaseQuota = Xl_BaseQuotaIrpf.Value
                End If
                AddIvas(oCca, .IvaBaseQuotas)
                AddIRPF(oCca, .IrpfBaseQuota)

                If .Import = DTOInvoice.ExportCods.Intracomunitari Then
                    Dim oBaseExempta As DTOBaseQuota = .IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus = 0)
                    If oBaseExempta IsNot Nothing Then
                        AddIvaIntracomunitari(oCca, oBaseExempta.baseImponible)
                    End If
                End If
            End With
        End With

        Dim oCreditor As DTOContact = Nothing
        Select Case CurrentCfp()
            Case Cfps.Credit
                oCreditor = _Proveidor
            Case Cfps.Visa
                Dim oTitular As DTOContact = Xl_LookupVisa1.VisaCard.Titular
                oCreditor = oTitular
        End Select

        AddTotal(oCca, Xl_CtaCreditora.Cta, oCreditor)



        'If _Importacio IsNot Nothing Then

        'Dim oItem As ImportacioItem
        'If Xl_Cta1.Cta.IsOrInheritsFrom(DTOPgcPlan.ctas.transport) Then
        'oItem = New ImportacioItem(ImportacioItem.SourceCodes.FraTrp, oCca.Guid)
        'Else
        'oItem = New ImportacioItem(ImportacioItem.SourceCodes.Fra, oCca.Guid)
        'End If

        '_Importacio.Items.Add(oItem)
        '_Importacio.Update()
        'End If

        Dim oPnds As List(Of DTOPnd) = Nothing
        Dim oPnd As DTOPnd = Nothing
        If CurrentCfp() = Cfps.Credit Then
            oPnd = DTOPnd.Factory(Current.Session.Emp)
            With oPnd
                .Contact = _Proveidor
                .Fch = DateTimePickerFch.Value
                .Cta = Xl_CtaCreditora.Cta
                .Cca = oCca
                .Vto = DateTimePickerVto.Value
                .Amt = Xl_AmtTot.Amt
                '.Cfp = ComboBoxCfp.SelectedValue
                .Cfp = DirectCast(ComboBoxCfp.SelectedItem, DTOValueNom).Value
                .Yef = oCca.Fch.Year
                .FraNum = TextBoxFra.Text
                .Fpg = TextBoxPndObs.Text
                .Cod = DTOPnd.Codis.Creditor
                .Status = DTOPnd.StatusCod.pendent
            End With
            oPnds = New List(Of DTOPnd)
            oPnds.Add(oPnd)
        End If

        If _Importacio IsNot Nothing Then
            If FEB.Importacio.Load(exs, _Importacio) Then
                Dim oItem As DTOImportacioItem
                Dim oCtaCarrec As DTOPgcCta = oCca.BookFra.Cta
                If FEB.PgcCta.Load(oCtaCarrec, exs) Then
                    Select Case oCtaCarrec.Codi
                        Case DTOPgcPlan.Ctas.transport_internacional_fletes, DTOPgcPlan.Ctas.transport_internacional_despeses, DTOPgcPlan.Ctas.transport_internacional_aranzels
                            oItem = DTOImportacioItem.Factory(_Importacio, DTOImportacioItem.SourceCodes.FraTrp, oCca.Guid)
                        Case Else
                            oItem = DTOImportacioItem.Factory(_Importacio, DTOImportacioItem.SourceCodes.Fra, oCca.Guid)
                    End Select
                    oItem.Amt = oCca.BookFra.BaseDevengada
                    _Importacio.Items.Add(oItem)
                Else
                    UIHelper.WarnError(exs)
                End If

            End If
        End If

        Dim retval As DTOCca = Await FEB.Proveidor.SaveFactura(exs, oCca, oPnds, _Importacio)
        Return retval
    End Function

    Private Sub AddIvaIntracomunitari(ByRef oCca As DTOCca, oBaseImponible As DTOAmt)
        Dim oTaxIva = DTOApp.Current.Taxes.FirstOrDefault(Function(x) x.Codi = DTOTax.Codis.Iva_Standard)
        If oTaxIva IsNot Nothing Then
            If oBaseImponible IsNot Nothing AndAlso oBaseImponible.IsNotZero Then
                Dim oQuota As DTOAmt = oBaseImponible.Percent(oTaxIva.Tipus)
                oCca.AddDebit(oQuota, _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatIntracomunitari))
                oCca.AddCredit(oQuota, _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari))
            End If
        End If
    End Sub


    Private Sub AddIvas(ByRef oCca As DTOCca, oBaseIvas As List(Of DTOBaseQuota))
        For Each item As DTOBaseQuota In oBaseIvas
            Dim oCta As DTOPgcCta = item.source
            oCca.addDebit(item.baseImponible, oCta, _Proveidor)
            If item.Quota IsNot Nothing Then
                If item.Quota.IsNotZero Then
                    oCca.AddDebit(item.Quota, _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatNacional))
                End If
            End If
        Next
    End Sub


    Private Sub AddIRPF(ByRef oCca As DTOCca, oBaseIrpf As DTOBaseQuota)
        If oBaseIrpf IsNot Nothing Then
            If oBaseIrpf.Quota IsNot Nothing Then
                If oBaseIrpf.Quota.IsNotZero Then
                    Dim oCta As DTOPgcCta = mIRPFCtas(ComboBoxIRPFcta.SelectedIndex)
                    oCca.AddCredit(oBaseIrpf.Quota, oCta, _Proveidor)
                End If
            End If
        End If
    End Sub

    Private Sub AddTotal(ByRef oCca As DTOCca, ByVal oCta As DTOPgcCta, ByVal oContact As DTOContact)
        Dim oAmt As DTOAmt = Xl_AmtTot.Amt
        If oAmt.IsNotZero Then
            oCca.AddSaldo(oCta, oContact)
        End If
    End Sub






    Private Sub Calcula()

        If Xl_AmtCurBaseExento.Amt IsNot Nothing Then
            Dim oCur As DTOCur = Xl_AmtCurBaseExento.Amt.Cur

            Dim CheckedIVA As Boolean = Not (Xl_BaseQuotaIva.IsEmptyQuota Or Xl_BaseQuotaIva1.IsEmptyQuota Or Xl_BaseQuotaIva2.IsEmptyQuota)
            Dim CheckedIRPF As Boolean = Not Xl_BaseQuotaIrpf.IsEmptyQuota
            Dim CheckedExento As Boolean = Xl_AmtCurBaseExento.Amt.IsNotZero

            Dim oSum = DTOAmt.Empty
            oSum.Add(Xl_BaseQuotaIva.Base)
            oSum.Add(Xl_BaseQuotaIva1.Base)
            oSum.Add(Xl_BaseQuotaIva2.Base)

            If CheckBoxIrpf.Checked Then
                Dim oBaseQuota As New DTOBaseQuota(oSum.Clone, Xl_BaseQuotaIrpf.Tipus)
                Xl_BaseQuotaIrpf.Load(oBaseQuota)
            End If

            oSum.Add(Xl_AmtCurBaseExento.Amt)
            Xl_AmtDevengat.Amt = oSum.Clone

            oSum.Add(Xl_BaseQuotaIva.Quota)
            oSum.Add(Xl_BaseQuotaIva1.Quota)
            oSum.Add(Xl_BaseQuotaIva2.Quota)
            oSum.Substract(Xl_BaseQuotaIrpf.Quota)
            Xl_AmtTot.Amt = oSum.Clone


            EnableButtons()
        End If
    End Sub

    Private Sub ContextMenuPrv_Popup(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As New MenuItem
        With ContextMenuPrv.MenuItems
            .Clear()
            'For Each oMenuItem In New MenuItems_Contact(_Proveidor)
            ' .Add(oMenuItem.CloneMenu)
            ' Next
        End With
    End Sub

    Private Async Sub TextBoxFra_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxFra.Validated
        Dim exs As New List(Of Exception)
        Dim oDuplicada As DTOCca = Await FEB.Proveidor.CheckFacturaAlreadyExists(exs, _Proveidor, DTOExercici.Current(Current.Session.Emp), TextBoxFra.Text)
        If exs.Count = 0 Then
            If oDuplicada IsNot Nothing Then
                Dim sWarn As String = "aquesta factura ja está entrada" & vbCrLf
                sWarn = sWarn & "per " & DTOUser.NicknameOrElse(oDuplicada.UsrLog.UsrCreated) & " el " & oDuplicada.UsrLog.FchCreated.ToShortDateString & " a las " & Format(oDuplicada.UsrLog.FchCreated, "HH:mm")
                sWarn = sWarn & vbCrLf & " (assentament " & oDuplicada.Id.ToString & ")"
                sWarn = sWarn & vbCrLf & oDuplicada.Concept
                TextBoxFra.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                MsgBox(sWarn, MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                Dim s As String = "fra." & TextBoxFra.Text
                If _Importacio IsNot Nothing Then
                    s = s & " (R." & _Importacio.Id & ")"
                End If
                s = s & " de " & _Proveidor.Nom
                TextBoxFra.BackColor = TextBox.DefaultBackColor
                TextBoxConcept.Text = s
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Xl_AmtCurBase_AfterUpdateValue(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurBaseExento.AfterUpdateValue
        If _AllowEvents Then
            Dim BlAllowEventsBackup As Boolean = _AllowEvents
            _AllowEvents = False
            RefrescaBase()
            Calcula()
            _AllowEvents = BlAllowEventsBackup
        End If
    End Sub


    Private Sub RadioButtonFpg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonCredit.CheckedChanged, RadioButtonBanc.CheckedChanged, RadioButtonCash.CheckedChanged, RadioButtonVISA.CheckedChanged
        If _AllowEvents Then
            SetCtaCreditora()
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_Bancs_Select2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Bancs_Select2.AfterUpdate
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_LookupVisa1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupVisa1.AfterUpdate
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub LoadIRPFcuentas()
        Static Loaded As Boolean
        Dim exs As New List(Of Exception)
        If Not Loaded Then
            Loaded = True
            mIRPFCtas.Add(FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.IrpfTreballadors, Current.Session.Emp, exs))
            mIRPFCtas.Add(FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.IrpfProfessionals, Current.Session.Emp, exs))
            mIRPFCtas.Add(FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.IrpfLloguers, Current.Session.Emp, exs))
            Dim i As Integer
            For i = 0 To mIRPFCtas.Count - 1
                ComboBoxIRPFcta.Items.Add(DTOPgcCta.FullNom(mIRPFCtas(i), _Lang))
            Next

            If Xl_CtaBaseIva.Cta IsNot Nothing Then
                Select Case Microsoft.VisualBasic.Left(Xl_CtaBaseIva.Cta.Id, 3)
                    Case "621"
                        ComboBoxIRPFcta.SelectedIndex = IRPFctas.Lloguers
                    Case "640"
                        ComboBoxIRPFcta.SelectedIndex = IRPFctas.Treballadors
                    Case Else
                        ComboBoxIRPFcta.SelectedIndex = IRPFctas.Professionals
                End Select
            End If
        End If

    End Sub

    Private Sub Xl_AmtCurBase_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurBaseExento.Changed
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Async Function Wizard_AfterTabSelect() As Task
        Dim exs As New List(Of Exception)
        If ValidateForm(exs) Then
            Dim oTabIdx As Tabs = TabControl1.SelectedIndex
            Select Case oTabIdx
                Case Tabs.Visa
                    EnableButtons()
                Case Tabs.Credit
                    Dim DtVto As Date = DTOPaymentTerms.Vto(_Proveidor.paymentTerms, DateTimePickerFch.Value)
                    DateTimePickerVto.Value = DtVto
                    SetCodi(_Proveidor.paymentTerms.Cod)
                    TextBoxPndObs.Text = FEB.PaymentTerms.Text(_Proveidor.paymentTerms, _Lang, DtVto)
                    EnableButtons()
                Case Tabs.Fi
                    Dim oCca As DTOCca = Nothing
                    If ValidateForm(exs) Then
                        oCca = Await Grabacion(exs)
                        If exs.Count = 0 Then
                            MsgBox("Factura registrada correctament" & vbCrLf & "assentament " & oCca.Id, MsgBoxStyle.Information)
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCca)) ' si ho fem torna a grabar via frm_Importacio
                            Me.Close()
                        Else
                            MsgBox("error al desar factura de proveidor:" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
                            TabControl1.SelectedTab = TabControl1.TabPages(Tabs.Fi)
                            Dim oTabGoto As Tabs
                            Select Case CurrentCfp()
                                Case Cfps.Cash
                                    oTabGoto = Tabs.Fpg
                                Case Cfps.Banc
                                    oTabGoto = Tabs.Banc
                                Case Cfps.Visa
                                    oTabGoto = Tabs.Visa
                                Case Cfps.Credit
                                    oTabGoto = Tabs.Credit
                            End Select
                            TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
                            Await Wizard_AfterTabSelect()
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub SetCodi(ByVal oCod As DTOPaymentTerms.CodsFormaDePago)
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oDictionary As List(Of DTOValueNom) = DTOPaymentTerms.Cods(oLang)
        With ComboBoxCfp
            .ValueMember = "Value"
            .DisplayMember = "Nom"
            .DataSource = oDictionary
            '.SelectedValue = oCod
            .SelectedItem = oDictionary.Find(Function(x) x.Value = oCod)
        End With
    End Sub

    Private Sub EnableButtons()
        Dim BlEnableNext As Boolean = True
        Dim BlEnablePrevious As Boolean = True
        Dim BlEnableEnd As Boolean = False
        Dim sCaptionEnd As String = "FI >>"

        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Select Case oTabIdx
            Case Tabs.Gral
                BlEnablePrevious = False
                If Xl_AmtTot.Amt.Val = 0 Then
                    Dim s As String = Xl_AmtCurBaseExento.Text
                    If IsNumeric(s) Then
                        If CSng(s) = 0 Then
                            BlEnableNext = False
                        End If
                    Else
                        BlEnableNext = False
                    End If
                End If
            Case Tabs.Fpg
                BlEnablePrevious = True
                Select Case CurrentCfp()
                    Case Cfps.Not_Set
                        BlEnableNext = False
                    Case Cfps.Cash
                        BlEnableNext = False
                        BlEnableEnd = True
                    Case Else
                        BlEnableNext = True
                End Select
                If CurrentCfp() = Cfps.Not_Set Then BlEnableNext = False
            Case Tabs.Banc
                BlEnablePrevious = True
                BlEnableNext = False
                If Xl_Bancs_Select2.Banc IsNot Nothing Then
                    BlEnableEnd = True
                End If
            Case Tabs.Visa
                BlEnablePrevious = True
                BlEnableNext = False
                If Xl_LookupVisa1.VisaCard IsNot Nothing Then
                    BlEnableEnd = True
                End If
            Case Tabs.Credit
                BlEnablePrevious = True
                BlEnableNext = False
                BlEnableEnd = True
            Case Tabs.Fi
                BlEnablePrevious = False
                BlEnableNext = False
                BlEnableEnd = True
                sCaptionEnd = "SORTIDA"
        End Select
        ButtonPrevious.Enabled = BlEnablePrevious
        ButtonNext.Enabled = BlEnableNext
        ButtonEnd.Enabled = BlEnableEnd
        ButtonEnd.Text = sCaptionEnd
    End Sub

    Private Async Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Dim oTabGoto As Tabs
        Select Case oTabIdx
            Case Tabs.Banc, Tabs.Credit, Tabs.Visa
                oTabGoto = Tabs.Fpg
            Case Else
                oTabGoto = oTabIdx - 1
        End Select
        TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
        Await Wizard_AfterTabSelect()
        ButtonNext.Enabled = True
    End Sub

    Private Async Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Dim oTabGoto As Tabs
        Select Case oTabIdx
            Case Tabs.Fpg
                Select Case CurrentCfp()
                    Case Cfps.Cash
                        oTabGoto = Tabs.Fi
                    Case Cfps.Banc
                        oTabGoto = Tabs.Banc
                    Case Cfps.Visa
                        oTabGoto = Tabs.Visa
                    Case Cfps.Credit
                        oTabGoto = Tabs.Credit
                End Select
            Case Tabs.Banc, Tabs.Visa, Tabs.Credit
                oTabGoto = Tabs.Fi
            Case Else
                oTabGoto = oTabIdx + 1
        End Select

        TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
        Await Wizard_AfterTabSelect()
    End Sub

    Private Async Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Dim oTabGoto As Tabs
        Select Case oTabIdx
            Case Tabs.Fi
                Me.Close()
            Case Else
                oTabGoto = Tabs.Fi
        End Select
        TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
        Await Wizard_AfterTabSelect()
    End Sub


    Private Sub Xl_AmtCurBase_AfterUpdateCur(ByVal xCur As DTOCur) Handles Xl_AmtCurBaseExento.AfterUpdateCur
        If _AllowEvents Then
            refrescaDivisa()
            Calcula()
        End If
    End Sub

    Private Sub refrescaDivisa()
        Dim BlAllowEventsBackup As Boolean = _AllowEvents
        _AllowEvents = False
        Dim oCur As DTOCur = Xl_AmtCurBaseExento.Amt.Cur
        If oCur Is Nothing Then oCur = Current.Session.Cur
        mShowDivisa = oCur.UnEquals(Current.Session.Cur)
        'Xl_AmtBasEur.Visible = mShowDivisa
        LabelExchange.Visible = mShowDivisa
        'LabelEur.Visible = mShowDivisa
        TextBoxExchangeRate.Visible = mShowDivisa

        'CurExchangeRate = Await FEB.CurExchangeRate.Closest(oCur, DateTimePickerFch.Value, exs)
        'If exs.Count = 0 Then
        RefrescaBase()
        _AllowEvents = BlAllowEventsBackup
        'End If
        'Return exs.Count = 0
    End Sub

    Private Sub RefrescaBase()
        Dim BlAllowEventsBackup As Boolean = _AllowEvents
        _AllowEvents = False
        If _CurExchangeRate IsNot Nothing Then
            Dim oCur As DTOCur = Xl_AmtCurBaseExento.Amt.Cur
            If oCur Is Nothing Then oCur = Current.Session.Cur
            TextBoxExchangeRate.Text = _CurExchangeRate.Rate
            LabelExchange.Text = oCur.Tag & "/€"

            Dim DblVal As Decimal = Xl_AmtCurBaseExento.Amt.Val
            Dim DblEur As Decimal = oCur.AmtFromDivisa(DblVal, _CurExchangeRate).Eur
            Dim oBase As DTOAmt = oCur.AmtFromDivisa(DblVal, _CurExchangeRate)
            Xl_AmtCurBaseExento.Amt = oBase
            'Xl_AmtBasEur.Amt = DTOAmt.Factory(DblEur)
        End If

        _AllowEvents = BlAllowEventsBackup
    End Sub


    Private Sub TextBoxExchangeRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxExchangeRate.TextChanged
        If _AllowEvents Then
            Dim BlAllowEventsBackup As Boolean = _AllowEvents
            _AllowEvents = False
            Dim oCur As DTOCur = Xl_AmtCurBaseExento.Amt.Cur
            Dim DblVal As Decimal = Xl_AmtCurBaseExento.Amt.Val
            Dim oBase = DTOAmt.Factory(oCur, DblVal)
            Xl_AmtCurBaseExento.Amt = oBase
            _AllowEvents = BlAllowEventsBackup
            Calcula()
        End If
    End Sub


    Private Sub Xl_AmtBasEur_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        If _AllowEvents Then
            Dim BlAllowEventsBackup As Boolean = _AllowEvents
            _AllowEvents = False
            Dim oCur As DTOCur = Xl_AmtCurBaseExento.Amt.Cur
            Dim DblVal As Decimal = Xl_AmtCurBaseExento.Amt.Val
            Dim DblEur As Decimal = Xl_AmtCurBaseExento.Amt.Eur
            Dim SngRate As Decimal = Math.Round(DblEur / DblVal, 2, MidpointRounding.AwayFromZero)
            TextBoxExchangeRate.Text = SngRate
            Dim oBase As DTOAmt = DTOAmt.Factory(DblEur, oCur.Tag, DblVal)
            Xl_AmtCurBaseExento.Amt = oBase
            _AllowEvents = BlAllowEventsBackup
            Calcula()
        End If
    End Sub

    Private Sub Xl_ContactPrv_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_ContactPrv.AfterUpdate
        If e.Argument Is Nothing Then
            _Proveidor = Nothing
        Else
            _Proveidor = DTOProveidor.FromContact(e.Argument)
        End If

        Dim exs As New List(Of Exception)
        If setProveidor(exs) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DateTimePickerFch_ValueChanged(sender As Object, e As System.EventArgs) Handles DateTimePickerFch.ValueChanged
        If _AllowEvents Then
            Dim DtFch As Date = DateTimePickerFch.Value
            'closest
            If DTOContact.isIVASujeto(_Proveidor) Then
                Dim oTaxIva As DTOTax = DTOTax.Closest(DTOTax.Codis.Iva_Standard, DateTimePickerFch.Value)
                Dim oBaseQuota As New DTOBaseQuota(DTOAmt.Empty, oTaxIva.Tipus)
                Xl_BaseQuotaIva.Load(oBaseQuota)
                Xl_BaseQuotaIva.Visible = True
                Xl_CtaBaseIva.Visible = True

                Dim oBaseQuota1 As New DTOBaseQuota(DTOAmt.Empty, 0)
                Xl_BaseQuotaIva1.Load(oBaseQuota1)
                Xl_BaseQuotaIva1.Visible = True
                Xl_CtaBaseIva1.Visible = True

                Dim oBaseQuota2 As New DTOBaseQuota(DTOAmt.Empty, 0)
                Xl_BaseQuotaIva2.Load(oBaseQuota2)
                Xl_BaseQuotaIva2.Visible = True
                Xl_CtaBaseIva2.Visible = True
            Else
                Xl_BaseQuotaIva.Visible = False
                Xl_BaseQuotaIva1.Visible = False
                Xl_BaseQuotaIva2.Visible = False
            End If

            Dim TipoIrpf = DTOProveidor.IRPF(_Proveidor, DtFch)
            If TipoIrpf <> Xl_BaseQuotaIrpf.Tipus Then
                Dim oIrpf As New DTOBaseQuota(Xl_BaseQuotaIrpf.Base, TipoIrpf)
                Xl_BaseQuotaIrpf.Load(oIrpf)
            End If

            refrescaDivisa()
            Calcula()
            If Xl_CtaBaseIva.Cta IsNot Nothing Then
                Xl_CtaBaseIva.ValidateFch(DtFch)
            End If
        End If
    End Sub

    Private Sub Xl_LookupProjecte1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProjecte1.RequestToLookup
        Dim oFrm As New Frm_Projectes(Nothing, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onProjectSelected
        oFrm.Show()
    End Sub

    Private Sub onProjectSelected(sender As Object, e As MatEventArgs)
        Xl_LookupProjecte1.Projecte = e.Argument
    End Sub



    Private Sub Xl_BaseQuota_AfterUpdate(sender As Object, e As MatEventArgs) Handles _
        Xl_BaseQuotaIva.AfterUpdate,
         Xl_BaseQuotaIva1.AfterUpdate,
          Xl_BaseQuotaIva2.AfterUpdate,
           Xl_BaseQuotaIrpf.AfterUpdate

        If _AllowEvents Then
            Calcula()
        End If
    End Sub

    Private Sub CheckBoxSujeto_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSujeto.CheckedChanged
        If _AllowEvents Then
            Xl_BaseQuotaIva.Visible = CheckBoxSujeto.Checked
            Xl_CtaBaseIva.Visible = CheckBoxSujeto.Checked
            Xl_BaseQuotaIva1.Visible = CheckBoxSujeto.Checked
            Xl_BaseQuotaIva2.Visible = CheckBoxSujeto.Checked
            Xl_CtaBaseIva1.Visible = CheckBoxSujeto.Checked
            Xl_CtaBaseIva2.Visible = CheckBoxSujeto.Checked
        End If
    End Sub

    Private Sub CheckBoxExento_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExento.CheckedChanged
        If _AllowEvents Then
            Xl_AmtCurBaseExento.Visible = CheckBoxExento.Checked
            Xl_CtaExentoIva.Visible = CheckBoxExento.Checked
            ComboBoxCausaExempcio.Visible = CheckBoxExento.Checked
        End If
    End Sub

    Private Sub CheckBoxIrpf_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIrpf.CheckedChanged
        If _AllowEvents Then
            Xl_BaseQuotaIrpf.Visible = CheckBoxIrpf.Checked
            ComboBoxIRPFcta.Visible = CheckBoxIrpf.Checked
        End If
    End Sub

    Private Async Function LoadComboboxes() As Task
        Dim exs As New List(Of Exception)
        Dim CausasExencion As List(Of KeyValuePair(Of String, String)) = DTOBookFra.CausasExencion
        With ComboBoxCausaExempcio
            .DataSource = CausasExencion
            .DisplayMember = "value"
            .ValueMember = "key"
        End With
        Dim tiposFra As List(Of KeyValuePair(Of String, String)) = DTOBookFra.TiposFra
        With ComboBoxTipoFra
            .DataSource = tiposFra
            .DisplayMember = "value"
            .ValueMember = "key"
        End With
        Dim regEspOTrascs As List(Of KeyValuePair(Of String, String)) = DTOBookFra.regEspOTrascs
        With ComboBoxRegEspOTrascs
            .DataSource = regEspOTrascs
            .DisplayMember = "value"
            .ValueMember = "key"
        End With

        Dim oDefaultBanc As DTOBanc = Nothing
        Dim oGuid = Await FEB.Default.EmpGuid(Current.Session.Emp, DTODefault.Codis.BancNominaTransfers, exs)
        If exs.Count = 0 Then
            oDefaultBanc = New DTOBanc(oGuid)
            Await Xl_Bancs_Select2.Load(oDefaultBanc)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function


End Class
