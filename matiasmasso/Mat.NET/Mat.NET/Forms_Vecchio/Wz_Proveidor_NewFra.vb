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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIRPF As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_AmtIVA As Xl_Amount
    Friend WithEvents Xl_AmtIRPF As Xl_Amount
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
    Friend WithEvents Xl_AmtParcial As Xl_Amount
    Friend WithEvents Xl_PercentIRPF As Xl_Percent
    Friend WithEvents Xl_PercentIVA As Xl_Percent
    Friend WithEvents Xl_AmtCurBase As Xl_AmountCur
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
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents ComboBoxIRPFcta As System.Windows.Forms.ComboBox
    Friend WithEvents LabelExchange As System.Windows.Forms.Label
    Friend WithEvents TextBoxExchangeRate As System.Windows.Forms.TextBox
    Friend WithEvents LabelEur As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtBasEur As Xl_Amount
    Friend WithEvents Xl_Bancs_Select2 As Xl_Bancs_Select
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_AmtSuplidos As Xl_Amount
    Friend WithEvents CheckBoxSuplidos As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactPrv As Xl_Contact
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
    Friend WithEvents Xl_LookupVisa1 As Mat.NET.Xl_LookupVisaCard
    Friend WithEvents PictureBoxVISA As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wz_Proveidor_NewFra))
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Xl_ContactPrv = New Mat.NET.Xl_Contact()
        Me.CheckBoxSuplidos = New System.Windows.Forms.CheckBox()
        Me.Xl_AmtSuplidos = New Mat.NET.Xl_Amount()
        Me.LabelEur = New System.Windows.Forms.Label()
        Me.Xl_AmtBasEur = New Mat.NET.Xl_Amount()
        Me.TextBoxExchangeRate = New System.Windows.Forms.TextBox()
        Me.LabelExchange = New System.Windows.Forms.Label()
        Me.ComboBoxIRPFcta = New System.Windows.Forms.ComboBox()
        Me.Xl_AmtTot = New Mat.NET.Xl_Amount()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_AmtParcial = New Mat.NET.Xl_Amount()
        Me.LabelParcial = New System.Windows.Forms.Label()
        Me.Xl_AmtIRPF = New Mat.NET.Xl_Amount()
        Me.Xl_AmtIVA = New Mat.NET.Xl_Amount()
        Me.Xl_PercentIRPF = New Mat.NET.Xl_Percent()
        Me.Xl_PercentIVA = New Mat.NET.Xl_Percent()
        Me.CheckBoxIRPF = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.Xl_AmtCurBase = New Mat.NET.Xl_AmountCur()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_Cta1 = New Mat.NET.Xl_Cta()
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
        Me.PictureBoxVISA = New System.Windows.Forms.PictureBox()
        Me.TabPageBanc = New System.Windows.Forms.TabPage()
        Me.Xl_Bancs_Select2 = New Mat.NET.Xl_Bancs_Select()
        Me.TabPageEND = New System.Windows.Forms.TabPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LabelCca = New System.Windows.Forms.Label()
        Me.ContextMenuPrv = New System.Windows.Forms.ContextMenu()
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Xl_LookupVisa1 = New Mat.NET.Xl_LookupVisaCard()
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
        Me.ButtonPrevious.Location = New System.Drawing.Point(5, 429)
        Me.ButtonPrevious.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 7
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(710, 429)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 23
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(606, 429)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 24
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
        Me.TabControl1.Location = New System.Drawing.Point(372, 76)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(440, 324)
        Me.TabControl1.TabIndex = 4
        Me.TabControl1.TabStop = False
        '
        'TabPageGral
        '
        Me.TabPageGral.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageGral.Controls.Add(Me.Xl_ContactPrv)
        Me.TabPageGral.Controls.Add(Me.CheckBoxSuplidos)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtSuplidos)
        Me.TabPageGral.Controls.Add(Me.LabelEur)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtBasEur)
        Me.TabPageGral.Controls.Add(Me.TextBoxExchangeRate)
        Me.TabPageGral.Controls.Add(Me.LabelExchange)
        Me.TabPageGral.Controls.Add(Me.ComboBoxIRPFcta)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtTot)
        Me.TabPageGral.Controls.Add(Me.Label9)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtParcial)
        Me.TabPageGral.Controls.Add(Me.LabelParcial)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtIRPF)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtIVA)
        Me.TabPageGral.Controls.Add(Me.Xl_PercentIRPF)
        Me.TabPageGral.Controls.Add(Me.Xl_PercentIVA)
        Me.TabPageGral.Controls.Add(Me.CheckBoxIRPF)
        Me.TabPageGral.Controls.Add(Me.CheckBoxIVA)
        Me.TabPageGral.Controls.Add(Me.Xl_AmtCurBase)
        Me.TabPageGral.Controls.Add(Me.Label7)
        Me.TabPageGral.Controls.Add(Me.TextBoxConcept)
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.Xl_Cta1)
        Me.TabPageGral.Controls.Add(Me.TextBoxFra)
        Me.TabPageGral.Controls.Add(Me.DateTimePickerFch)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 25)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Size = New System.Drawing.Size(432, 295)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        '
        'Xl_ContactPrv
        '
        Me.Xl_ContactPrv.Contact = Nothing
        Me.Xl_ContactPrv.Location = New System.Drawing.Point(110, 8)
        Me.Xl_ContactPrv.Name = "Xl_ContactPrv"
        Me.Xl_ContactPrv.ReadOnly = False
        Me.Xl_ContactPrv.Size = New System.Drawing.Size(311, 20)
        Me.Xl_ContactPrv.TabIndex = 1
        '
        'CheckBoxSuplidos
        '
        Me.CheckBoxSuplidos.BackColor = System.Drawing.SystemColors.Control
        Me.CheckBoxSuplidos.Location = New System.Drawing.Point(14, 233)
        Me.CheckBoxSuplidos.Name = "CheckBoxSuplidos"
        Me.CheckBoxSuplidos.Size = New System.Drawing.Size(96, 16)
        Me.CheckBoxSuplidos.TabIndex = 31
        Me.CheckBoxSuplidos.TabStop = False
        Me.CheckBoxSuplidos.Text = "Suplidos"
        Me.CheckBoxSuplidos.UseVisualStyleBackColor = False
        '
        'Xl_AmtSuplidos
        '
        Me.Xl_AmtSuplidos.Amt = Nothing
        Me.Xl_AmtSuplidos.Location = New System.Drawing.Point(110, 233)
        Me.Xl_AmtSuplidos.Name = "Xl_AmtSuplidos"
        Me.Xl_AmtSuplidos.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtSuplidos.TabIndex = 30
        Me.Xl_AmtSuplidos.TabStop = False
        Me.Xl_AmtSuplidos.Visible = False
        '
        'LabelEur
        '
        Me.LabelEur.AutoSize = True
        Me.LabelEur.Location = New System.Drawing.Point(282, 169)
        Me.LabelEur.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelEur.Name = "LabelEur"
        Me.LabelEur.Size = New System.Drawing.Size(30, 13)
        Me.LabelEur.TabIndex = 27
        Me.LabelEur.Text = "EUR"
        Me.LabelEur.Visible = False
        '
        'Xl_AmtBasEur
        '
        Me.Xl_AmtBasEur.Amt = Nothing
        Me.Xl_AmtBasEur.Location = New System.Drawing.Point(331, 165)
        Me.Xl_AmtBasEur.Name = "Xl_AmtBasEur"
        Me.Xl_AmtBasEur.Size = New System.Drawing.Size(90, 20)
        Me.Xl_AmtBasEur.TabIndex = 26
        Me.Xl_AmtBasEur.Visible = False
        '
        'TextBoxExchangeRate
        '
        Me.TextBoxExchangeRate.Location = New System.Drawing.Point(331, 139)
        Me.TextBoxExchangeRate.Name = "TextBoxExchangeRate"
        Me.TextBoxExchangeRate.Size = New System.Drawing.Size(90, 20)
        Me.TextBoxExchangeRate.TabIndex = 25
        Me.TextBoxExchangeRate.Visible = False
        '
        'LabelExchange
        '
        Me.LabelExchange.AutoSize = True
        Me.LabelExchange.Location = New System.Drawing.Point(278, 142)
        Me.LabelExchange.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelExchange.Name = "LabelExchange"
        Me.LabelExchange.Size = New System.Drawing.Size(34, 13)
        Me.LabelExchange.TabIndex = 24
        Me.LabelExchange.Text = "Canvi"
        Me.LabelExchange.Visible = False
        '
        'ComboBoxIRPFcta
        '
        Me.ComboBoxIRPFcta.FormattingEnabled = True
        Me.ComboBoxIRPFcta.Location = New System.Drawing.Point(244, 207)
        Me.ComboBoxIRPFcta.Name = "ComboBoxIRPFcta"
        Me.ComboBoxIRPFcta.Size = New System.Drawing.Size(177, 21)
        Me.ComboBoxIRPFcta.TabIndex = 20
        Me.ComboBoxIRPFcta.TabStop = False
        Me.ComboBoxIRPFcta.Visible = False
        '
        'Xl_AmtTot
        '
        Me.Xl_AmtTot.Amt = Nothing
        Me.Xl_AmtTot.Location = New System.Drawing.Point(110, 259)
        Me.Xl_AmtTot.Name = "Xl_AmtTot"
        Me.Xl_AmtTot.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtTot.TabIndex = 22
        Me.Xl_AmtTot.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(14, 259)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Total:"
        '
        'Xl_AmtParcial
        '
        Me.Xl_AmtParcial.Amt = Nothing
        Me.Xl_AmtParcial.Location = New System.Drawing.Point(110, 183)
        Me.Xl_AmtParcial.Name = "Xl_AmtParcial"
        Me.Xl_AmtParcial.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtParcial.TabIndex = 16
        Me.Xl_AmtParcial.TabStop = False
        '
        'LabelParcial
        '
        Me.LabelParcial.BackColor = System.Drawing.SystemColors.Control
        Me.LabelParcial.Location = New System.Drawing.Point(14, 183)
        Me.LabelParcial.Name = "LabelParcial"
        Me.LabelParcial.Size = New System.Drawing.Size(88, 16)
        Me.LabelParcial.TabIndex = 15
        Me.LabelParcial.Text = "Suma parcial:"
        '
        'Xl_AmtIRPF
        '
        Me.Xl_AmtIRPF.Amt = Nothing
        Me.Xl_AmtIRPF.Location = New System.Drawing.Point(110, 207)
        Me.Xl_AmtIRPF.Name = "Xl_AmtIRPF"
        Me.Xl_AmtIRPF.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtIRPF.TabIndex = 19
        Me.Xl_AmtIRPF.TabStop = False
        '
        'Xl_AmtIVA
        '
        Me.Xl_AmtIVA.Amt = Nothing
        Me.Xl_AmtIVA.Location = New System.Drawing.Point(110, 159)
        Me.Xl_AmtIVA.Name = "Xl_AmtIVA"
        Me.Xl_AmtIVA.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtIVA.TabIndex = 14
        Me.Xl_AmtIVA.TabStop = False
        '
        'Xl_PercentIRPF
        '
        Me.Xl_PercentIRPF.Location = New System.Drawing.Point(62, 207)
        Me.Xl_PercentIRPF.Name = "Xl_PercentIRPF"
        Me.Xl_PercentIRPF.Size = New System.Drawing.Size(48, 20)
        Me.Xl_PercentIRPF.TabIndex = 18
        Me.Xl_PercentIRPF.TabStop = False
        Me.Xl_PercentIRPF.Text = "0,00 %"
        Me.Xl_PercentIRPF.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_PercentIVA
        '
        Me.Xl_PercentIVA.Location = New System.Drawing.Point(62, 159)
        Me.Xl_PercentIVA.Name = "Xl_PercentIVA"
        Me.Xl_PercentIVA.Size = New System.Drawing.Size(48, 20)
        Me.Xl_PercentIVA.TabIndex = 13
        Me.Xl_PercentIVA.TabStop = False
        Me.Xl_PercentIVA.Text = "0,00 %"
        Me.Xl_PercentIVA.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'CheckBoxIRPF
        '
        Me.CheckBoxIRPF.BackColor = System.Drawing.SystemColors.Control
        Me.CheckBoxIRPF.Location = New System.Drawing.Point(14, 207)
        Me.CheckBoxIRPF.Name = "CheckBoxIRPF"
        Me.CheckBoxIRPF.Size = New System.Drawing.Size(56, 16)
        Me.CheckBoxIRPF.TabIndex = 17
        Me.CheckBoxIRPF.TabStop = False
        Me.CheckBoxIRPF.Text = "IRPF"
        Me.CheckBoxIRPF.UseVisualStyleBackColor = False
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.BackColor = System.Drawing.SystemColors.Control
        Me.CheckBoxIVA.Location = New System.Drawing.Point(14, 159)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(48, 16)
        Me.CheckBoxIVA.TabIndex = 12
        Me.CheckBoxIVA.TabStop = False
        Me.CheckBoxIVA.Text = "IVA"
        Me.CheckBoxIVA.UseVisualStyleBackColor = False
        '
        'Xl_AmtCurBase
        '
        Me.Xl_AmtCurBase.Amt = Nothing
        Me.Xl_AmtCurBase.Location = New System.Drawing.Point(110, 135)
        Me.Xl_AmtCurBase.Name = "Xl_AmtCurBase"
        Me.Xl_AmtCurBase.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurBase.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Location = New System.Drawing.Point(14, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 16)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Compte:"
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Location = New System.Drawing.Point(110, 109)
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.Size = New System.Drawing.Size(311, 20)
        Me.TextBoxConcept.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(14, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 16)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Base imponible:"
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(110, 87)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(128, 20)
        Me.Xl_Cta1.TabIndex = 28
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Location = New System.Drawing.Point(110, 61)
        Me.TextBoxFra.MaxLength = 15
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.Size = New System.Drawing.Size(84, 20)
        Me.TextBoxFra.TabIndex = 5
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(110, 37)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(84, 20)
        Me.DateTimePickerFch.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(14, 109)
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
        Me.TabPageCFP.Controls.Add(Me.LabelFpg)
        Me.TabPageCFP.Controls.Add(Me.GroupBox1)
        Me.TabPageCFP.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCFP.Name = "TabPageCFP"
        Me.TabPageCFP.Size = New System.Drawing.Size(432, 295)
        Me.TabPageCFP.TabIndex = 1
        Me.TabPageCFP.Text = "FORMA DE PAGAMENT"
        '
        'LabelFpg
        '
        Me.LabelFpg.Location = New System.Drawing.Point(16, 8)
        Me.LabelFpg.Name = "LabelFpg"
        Me.LabelFpg.Size = New System.Drawing.Size(496, 104)
        Me.LabelFpg.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonVISA)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCredit)
        Me.GroupBox1.Controls.Add(Me.RadioButtonBanc)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCash)
        Me.GroupBox1.Location = New System.Drawing.Point(176, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 152)
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
        Me.TabPageCREDIT.Size = New System.Drawing.Size(432, 295)
        Me.TabPageCREDIT.TabIndex = 2
        Me.TabPageCREDIT.Text = "CREDIT"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(72, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 16)
        Me.Label1.TabIndex = 5
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
        Me.TabPageVISA.Size = New System.Drawing.Size(432, 295)
        Me.TabPageVISA.TabIndex = 4
        Me.TabPageVISA.Text = "VISA"
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
        Me.TabPageBanc.Size = New System.Drawing.Size(432, 295)
        Me.TabPageBanc.TabIndex = 5
        Me.TabPageBanc.Text = "BANC"
        '
        'Xl_Bancs_Select2
        '
        Me.Xl_Bancs_Select2.Banc = Nothing
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
        Me.TabPageEND.Size = New System.Drawing.Size(432, 295)
        Me.TabPageEND.TabIndex = 6
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
        Me.PictureBoxLogo.Location = New System.Drawing.Point(658, 5)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 8
        Me.PictureBoxLogo.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 426)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(817, 31)
        Me.Panel1.TabIndex = 45
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(5, 5)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 46
        '
        'Xl_LookupVisa1
        '
        Me.Xl_LookupVisa1.IsDirty = False
        Me.Xl_LookupVisa1.Location = New System.Drawing.Point(27, 93)
        Me.Xl_LookupVisa1.Name = "Xl_LookupVisa1"
        Me.Xl_LookupVisa1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVisa1.Size = New System.Drawing.Size(376, 20)
        Me.Xl_LookupVisa1.TabIndex = 2
        Me.Xl_LookupVisa1.Value = Nothing
        Me.Xl_LookupVisa1.VisaCard = Nothing
        '
        'Wz_Proveidor_NewFra
        '
        Me.ClientSize = New System.Drawing.Size(817, 457)
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

    Private mProveidor As Proveidor
    Private mEmp As DTOEmp
    Private mCcd As DTOCca.CcdEnum = DTOCca.CcdEnum.FacturaProveidor
    Private mPlan As PgcPlan = PgcPlan.FromToday
    Private mImportacio As Importacio
    Private mInsercions As PrInsercions
    Private mIRPFCtas As New PgcCtas
    Private mShowDivisa As Boolean = False
    Private mAllowEvents As Boolean

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

    Public Sub New(oPrv As Proveidor, DtFch As Date, Optional oDocFile As DTODocFile = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        Me.Proveidor = oPrv
        DateTimePickerFch.Value = DtFch
        Xl_DocFile1.Load(oDocFile)
    End Sub

    Public Sub New(ByVal oImportacio As Importacio, Optional oDocFile As DTODocFile = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mImportacio = oImportacio
        With mImportacio
            Me.Text = "NOVA FACTURA (REMESA " & .Id & ")"
            Me.Proveidor = .Proveidor
            DateTimePickerFch.Value = .Fch
            If oDocFile IsNot Nothing Then
                Xl_DocFile1.Load(oDocFile)
            End If
        End With
    End Sub

    Public Sub New(ByVal oInsercions As PrInsercions)
        MyBase.New()
        Me.InitializeComponent()
        mInsercions = oInsercions
        If mInsercions IsNot Nothing Then
            If mInsercions.Count > 0 Then
                Me.Proveidor = New Proveidor(mInsercions(0).Numero.Revista.Editorial.Guid)
                mCcd = DTOCca.CcdEnum.FacturaInsercionsPublicitaries
                Xl_AmtCurBase.Amt = mInsercions.Cost
                Calcula()
            End If
        End If
    End Sub

    Public WriteOnly Property Proveidor() As Proveidor
        Set(ByVal Value As Proveidor)
            Dim DtFch As Date = DateTimePickerFch.Value

            mProveidor = Value
            mEmp = mProveidor.Emp
            PictureBoxLogo.Image = mProveidor.Img48
            Xl_ContactPrv.Contact = mProveidor

            Dim oCta As PgcCta = mProveidor.DefaultCtaCarrec
            If oCta IsNot Nothing Then
                PgcCtaLoader.Load(oCta)
                Xl_Cta1.Cta = oCta
            End If

            mShowDivisa = (mProveidor.DefaultCur.Id <> "EUR")
            Dim oCurExchangeRate As DTOCurExchangeRate = BLL.BLLCurExchangeRate.Closest(mProveidor.DefaultCur.Id, DateTimePickerFch.Value)
            Xl_AmtBasEur.Visible = mShowDivisa
            LabelExchange.Visible = mShowDivisa
            LabelEur.Visible = mShowDivisa
            LabelExchange.Text = "EUR/" & oCurExchangeRate.ISO
            TextBoxExchangeRate.Visible = mShowDivisa
            TextBoxExchangeRate.Text = oCurExchangeRate.Rate

            Xl_AmtCurBase.Amt = New MaxiSrvr.Amt(0, mProveidor.DefaultCur, 0)
            Xl_PercentIRPF.Value = mProveidor.IRPF(DtFch)
            Xl_AmtIRPF.Amt = New MaxiSrvr.Amt(0, mProveidor.DefaultCur, 0)
            Xl_PercentIVA.Value = MaxiSrvr.Iva.Standard(DtFch).Tipus
            CheckBoxIVA.Checked = (mProveidor.IVA > 0)
            If (mProveidor.IRPF_Cod > DTOProveidor.IRPFCods.exento) Then
                CheckBoxIRPF.Checked = True
                LoadIRPFcuentas()
            End If
            Calcula()
            LabelFpg.Text = mProveidor.FormaDePago.Text(BLL.BLLApp.Lang)
            root.TabControlHideTabLabels(TabControl1)
            mAllowEvents = True
        End Set
    End Property



    Private Function CurrentCfp() As Cfps
        If RadioButtonCash.Checked Then Return Cfps.Cash
        If RadioButtonBanc.Checked Then Return Cfps.Banc
        If RadioButtonVISA.Checked Then Return Cfps.Visa
        If RadioButtonCredit.Checked Then Return Cfps.Credit
        Return Cfps.Not_Set
    End Function

    Private Function Grabacion(ByRef oCca As Cca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If Xl_Cta1.ValidateFch(DateTimePickerFch.Value) Then

            Dim oContact As Contact
            Dim oCtaHabIdCod As DTOPgcPlan.Ctas

            oCca = New Cca(BLL.BLLApp.Emp)
            With oCca
                .Ccd = mCcd
                .fch = DateTimePickerFch.Value
                .Txt = TextBoxConcept.Text

                Dim oBaseImponible As Ccb = BaseImponible(oCca)
                If oBaseImponible.Amt.IsNotZero Then
                    .ccbs.Add(BaseImponible(oCca))
                End If

                If CheckBoxIVA.Checked Then .ccbs.Add(IVA(oCca))
                If CheckBoxIRPF.Checked Then .ccbs.Add(IRPF(oCca))

                oContact = Nothing
                Select Case CurrentCfp()
                    Case Cfps.Cash
                        oCtaHabIdCod = DTOPgcPlan.Ctas.caixa
                    Case Cfps.Banc
                        oCtaHabIdCod = DTOPgcPlan.Ctas.bancs
                        oContact = New Contact(Xl_Bancs_Select2.Banc.Guid)
                    Case Cfps.Credit
                        oContact = mProveidor
                        oCtaHabIdCod = PgcPlan.GetCtaProveedors(Xl_AmtCurBase.Amt.Cur)
                    Case Cfps.Visa
                        Dim oTitular As DTOContact = Xl_LookupVisa1.VisaCard.Titular
                        oContact = New Contact(oTitular.Guid)
                        oCtaHabIdCod = DTOPgcPlan.Ctas.VisasPagadas
                End Select

                .ccbs.Add(Total(oCca, oCtaHabIdCod, oContact))

                'If Xl_MediaObject1.IsDirty Then (sempre es Dirty, pero no ho reflecteix quan ja ve el mediaObject de Frm_Import
                .DocFile = Xl_DocFile1.Value
                'End If

                'If Xl_BigFile1.BigFile IsNot Nothing Then
                '.BigFile = New BigFileSrc(DTODocFile.Cods.Assentament, .Guid, Xl_BigFile1.BigFile)
                'End If

            End With

            Dim oBookFra As New BookFra(oCca)
            With oBookFra
                .Cta = Xl_Cta1.Cta
                .Contact = mProveidor
                .FraNum = TextBoxFra.Text
                .Base = Xl_AmtBasEur.Amt
                .Iva = Xl_AmtIVA.Amt
                .Irpf = Xl_AmtIRPF.Amt
            End With


            'If mImportacio IsNot Nothing Then

            'Dim oItem As ImportacioItem
            'If Xl_Cta1.Cta.IsOrInheritsFrom(DTOPgcPlan.ctas.transport) Then
            'oItem = New ImportacioItem(ImportacioItem.SourceCodes.FraTrp, oCca.Guid)
            'Else
            'oItem = New ImportacioItem(ImportacioItem.SourceCodes.Fra, oCca.Guid)
            'End If

            'mImportacio.Items.Add(oItem)
            'mImportacio.Update()
            'End If

            Dim oPnd As Pnd = Nothing
            If CurrentCfp() = Cfps.Credit Then
                oPnd = New Pnd
                With oPnd
                    .Contact = mProveidor
                    .Fch = DateTimePickerFch.Value
                    .Cta = mPlan.Cta(oCtaHabIdCod)
                    .Cca = oCca
                    .Vto = DateTimePickerVto.Value
                    .Amt = Xl_AmtTot.Amt
                    .Cfp = ComboBoxCfp.SelectedValue
                    .Yef = oCca.yea
                    .FraNum = TextBoxFra.Text
                    .Fpg = TextBoxPndObs.Text
                    .Cod = Pnd.Codis.Creditor
                    .Status = Pnd.StatusCod.pendent
                End With
            End If

            retval = mProveidor.SaveFactura(oCca, oBookFra, mImportacio, mInsercions, oPnd, TextBoxFra.Text, exs)
        Else
            MsgBox("el compte no correspon al pla comptable vigent a la data indicada", MsgBoxStyle.Exclamation)
        End If
        Return retval

    End Function

    Private Function BaseImponible(ByVal oCca As Cca) As Ccb
        Dim oCta As PgcCta = Xl_Cta1.Cta
        Dim oContact As Contact = mProveidor
        Dim oAmt As MaxiSrvr.Amt = Nothing

        If Xl_AmtCurBase.Amt.Cur.Equals(MaxiSrvr.Cur.Eur) Then
            oAmt = Xl_AmtCurBase.Amt
        Else
            oAmt = New MaxiSrvr.Amt(Xl_AmtBasEur.Amt.Eur, Xl_AmtCurBase.Amt.Cur, Xl_AmtCurBase.Amt.Val)
        End If

        If CheckBoxSuplidos.Checked Then
            Dim oSuplidos As MaxiSrvr.Amt = Xl_AmtSuplidos.Amt
            oAmt.Add(oSuplidos)
        End If
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.Debe
        Dim oCcb As New Ccb(oCca, oCta, oContact, oAmt, oDh)
        Return oCcb
    End Function

    Private Function IVA(ByVal oCca As Cca) As Ccb
        Dim oCta As PgcCta = mPlan.Cta(DTOPgcPlan.Ctas.IvaSoportat)
        Dim oContact As Contact = Nothing
        Dim oAmt As MaxiSrvr.Amt = Xl_AmtIVA.Amt
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.Debe
        Dim oCcb As New Ccb(oCca, oCta, oContact, oAmt, oDh)
        Return oCcb
    End Function

    Private Function IRPF(ByVal oCca As Cca) As Ccb
        Dim oCta As PgcCta = mIRPFCtas(ComboBoxIRPFcta.SelectedIndex)
        Dim oContact As Contact = mProveidor
        Dim oAmt As MaxiSrvr.Amt = Xl_AmtIRPF.Amt
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.Haber
        Dim oCcb As New Ccb(oCca, oCta, oContact, oAmt, oDh)
        Return oCcb
    End Function

    Private Function Total(ByVal oCca As Cca, ByVal oCtaCod As DTOPgcPlan.Ctas, ByVal oContact As Contact) As Ccb
        Dim oCta As PgcCta = mPlan.Cta(oCtaCod)
        Dim oAmt As MaxiSrvr.Amt = Xl_AmtTot.Amt
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.Haber
        Dim oCcb As New Ccb(oCca, oCta, oContact, oAmt, oDh)
        Return oCcb
    End Function

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIVA.CheckedChanged, CheckBoxSuplidos.CheckedChanged
        Calcula()
    End Sub

    Private Sub CheckBoxIRPF_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIRPF.CheckedChanged
        If mAllowEvents Then
            Dim DtFch As Date = DateTimePickerFch.Value
            If CheckBoxIRPF.Checked Then
                Xl_PercentIRPF.Value = mProveidor.IRPF(DateTimePickerFch.Value)
            End If
            LoadIRPFcuentas()
            Calcula()
        End If
    End Sub


    Private Sub Calcula()

        If Xl_AmtCurBase.Amt IsNot Nothing Then
            Dim oCur As MaxiSrvr.Cur = Xl_AmtCurBase.Amt.Cur

            Dim CheckedIVA As Boolean = CheckBoxIVA.Checked
            Dim CheckedIRPF As Boolean = CheckBoxIRPF.Checked
            Dim CheckedSuplidos As Boolean = CheckBoxSuplidos.Checked

            Dim oSum As MaxiSrvr.Amt = Xl_AmtCurBase.Amt.Clone

            Xl_PercentIVA.Visible = CheckedIVA
            Xl_AmtIVA.Visible = CheckedIVA
            Xl_AmtSuplidos.Visible = CheckedSuplidos

            If CheckedIVA Then
                If Xl_PercentIVA.Value > 0 Then Xl_AmtIVA.Amt = oSum.Percent(Xl_PercentIVA.Value)
                oSum.Add(Xl_AmtIVA.Amt)
            End If

            Xl_AmtParcial.Amt = oSum.Clone
            If CheckedIVA And CheckedIRPF Then
                LabelParcial.Visible = True
                Xl_AmtParcial.Visible = True
            Else
                LabelParcial.Visible = False
                Xl_AmtParcial.Visible = False
            End If

            Xl_PercentIRPF.Visible = CheckedIRPF
            Xl_AmtIRPF.Visible = CheckedIRPF
            If CheckedIRPF Then
                If Xl_PercentIRPF.Value > 0 Then Xl_AmtIRPF.Amt = Xl_AmtCurBase.Amt.Percent(Xl_PercentIRPF.Value)
                oSum.Substract(Xl_AmtIRPF.Amt)
            End If

            If CheckedSuplidos Then
                oSum.Add(Xl_AmtSuplidos.Amt)
            End If
            Xl_AmtTot.Amt = oSum.Clone
            EnableButtons()
        End If
    End Sub

    Private Sub ContextMenuPrv_Popup(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As New MenuItem
        With ContextMenuPrv.MenuItems
            .Clear()
            'For Each oMenuItem In New MenuItems_Contact(mProveidor)
            ' .Add(oMenuItem.CloneMenu)
            ' Next
        End With
    End Sub

    Private Sub TextBoxFra_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxFra.Validated
        Dim oDuplicada As Cca = mProveidor.CheckFacturaAlreadyExists(DateTimePickerFch.Value.Year, TextBoxFra.Text)
        If oDuplicada IsNot Nothing Then
            Dim sWarn As String = "aquesta factura ja está entrada" & vbCrLf
            sWarn = sWarn & "per " & MaxiSrvr.Usr.FromContact(oDuplicada.UsrLog.UsrCreated).login & " el " & oDuplicada.UsrLog.FchCreated.ToShortDateString
            sWarn = sWarn & vbCrLf & " (assentament " & oDuplicada.Id.ToString & ")"
            sWarn = sWarn & vbCrLf & oDuplicada.Txt
            TextBoxFra.BackColor = MaxiSrvr.COLOR_NOTOK
            MsgBox(sWarn, MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            Dim s As String = "fra." & TextBoxFra.Text
            If mImportacio IsNot Nothing Then
                s = s & " (R." & mImportacio.Id & ")"
            End If
            s = s & " de " & mProveidor.Clx
            TextBoxFra.BackColor = TextBox.DefaultBackColor
            TextBoxConcept.Text = s
        End If
    End Sub

    Private Sub Xl_AmtCurBase_AfterUpdateValue(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurBase.AfterUpdateValue
        If mAllowEvents Then
            mAllowEvents = False
            Dim DcVal As Decimal = Xl_AmtCurBase.Amt.Val
            Dim SngRate As Decimal = TextBoxExchangeRate.Text
            Dim DcEur As Decimal = Math.Round(DcVal * SngRate, 2)
            Dim oCur As MaxiSrvr.Cur = Xl_AmtCurBase.Amt.Cur
            Xl_AmtCurBase.Amt = New MaxiSrvr.Amt(DcEur, oCur, DcVal)
            Xl_AmtBasEur.Amt = New MaxiSrvr.Amt(DcEur)
            mAllowEvents = True
            Calcula()
        End If
    End Sub

    Private Sub Xl_AmtIVA_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtIVA.AfterUpdate
        Calcula()
    End Sub

    Private Sub Xl_AmtIRPF_AfterUpdate(ByVal SngPercent As Object, ByVal e As System.EventArgs) Handles Xl_AmtIRPF.AfterUpdate
        Calcula()
    End Sub

    Private Sub Xl_PercentIVA_AfterUpdate(ByVal SngPercent As Object, ByVal e As System.EventArgs) Handles Xl_PercentIVA.AfterUpdate
        Calcula()
    End Sub

    Private Sub Xl_PercentIRPF_AfterUpdate(ByVal SngPercent As Object, ByVal e As System.EventArgs) Handles Xl_PercentIRPF.AfterUpdate
        Calcula()
    End Sub

    Private Sub Xl_AmtSuplidos_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtSuplidos.AfterUpdate
        Calcula()
    End Sub

    Private Sub RadioButtonFpg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonCredit.CheckedChanged, RadioButtonBanc.CheckedChanged, RadioButtonCash.CheckedChanged, RadioButtonVISA.CheckedChanged
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_Bancs_Select2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Bancs_Select2.AfterUpdate
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Xl_LookupVisa1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupVisa1.AfterUpdate
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub LoadIRPFcuentas()
        Static Loaded As Boolean
        If Not Loaded Then
            Loaded = True
            mIRPFCtas.Add(mPlan.Cta(DTOPgcPlan.ctas.IrpfTreballadors))
            mIRPFCtas.Add(mPlan.Cta(DTOPgcPlan.ctas.IrpfProfessionals))
            mIRPFCtas.Add(mPlan.Cta(DTOPgcPlan.ctas.IrpfLloguers))
            Dim i As Integer
            For i = 0 To mIRPFCtas.Count - 1
                ComboBoxIRPFcta.Items.Add(mIRPFCtas(i).FullNom)
            Next
            Select Case Microsoft.VisualBasic.Left(Xl_Cta1.Cta.Id, 3)
                Case "621"
                    ComboBoxIRPFcta.SelectedIndex = IRPFctas.Lloguers
                Case "640"
                    ComboBoxIRPFcta.SelectedIndex = IRPFctas.Treballadors
                Case Else
                    ComboBoxIRPFcta.SelectedIndex = IRPFctas.Professionals
            End Select
        End If
        ComboBoxIRPFcta.Visible = CheckBoxIRPF.Checked

    End Sub

    Private Sub Xl_AmtCurBase_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurBase.Changed
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub Wizard_AfterTabSelect()
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Select Case oTabIdx
            Case Tabs.Visa
                EnableButtons()
            Case Tabs.Credit
                Dim DtVto As Date = mProveidor.FormaDePago.Vto(DateTimePickerFch.Value)
                DateTimePickerVto.Value = DtVto
                SetCodi(mProveidor.FormaDePago.Cod)
                TextBoxPndObs.Text = mProveidor.FormaDePago.Text(BLL.BLLApp.Lang, DtVto.ToShortDateString)
                EnableButtons()
            Case Tabs.Fi
                Dim oCca As Cca = Nothing
                Dim exs as New List(Of exception)
                If Grabacion(oCca, exs) Then
                    MsgBox("Factura registrada correctament" & vbCrLf & "assentament " & oCca.Id, MsgBoxStyle.Information)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oCca)) ' si ho fem torna a grabar via frm_Importacio
                    Me.Close()
                Else
                    MsgBox("error al desar factura de proveidor:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
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
                    Wizard_AfterTabSelect()

                End If
        End Select
    End Sub

    Private Sub SetCodi(ByVal oCod As DTOCustomer.FormasDePagament)
        If ComboBoxCfp.Items.Count = 0 Then
            Dim SQL As String = "SELECT COD,DSC FROM COD WHERE FNM=10 ORDER BY COD"
            Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow = oTb.Rows(0)
            oRow("DSC") = "(seleccionar forma de pago)"
            With ComboBoxCfp
                .DataSource = mProveidor.FormaDePago.DsCods(BLL.BLLApp.Lang).Tables(0)
                .ValueMember = "ID"
                .DisplayMember = "NOM"
            End With
        End If
        ComboBoxCfp.SelectedValue = oCod
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
                    Dim s As String = Xl_AmtCurBase.Text
                    If IsNumeric(s) Then
                        If CSng(s) = 0 Then
                            BlEnableNext = False
                        End If
                    Else
                        BlEnableNext = False
                    End If
                End If
            Case Tabs.Fpg
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
                BlEnableNext = False
                If Xl_Bancs_Select2.Banc IsNot Nothing Then
                    BlEnableEnd = True
                End If
            Case Tabs.Visa
                BlEnableNext = False
                If Xl_LookupVisa1.VisaCard IsNot Nothing Then
                    BlEnableEnd = True
                End If
            Case Tabs.Credit
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

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Dim oTabGoto As Tabs
        Select Case oTabIdx
            Case Tabs.Banc, Tabs.Credit, Tabs.Visa
                oTabGoto = Tabs.Fpg
            Case Else
                oTabGoto = oTabIdx - 1
        End Select
        TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
        Wizard_AfterTabSelect()
        ButtonNext.Enabled = True
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
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
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Dim oTabIdx As Tabs = TabControl1.SelectedIndex
        Dim oTabGoto As Tabs
        Select Case oTabIdx
            Case Tabs.Fi
                Me.Close()
            Case Else
                oTabGoto = Tabs.Fi
        End Select
        TabControl1.SelectedTab = TabControl1.TabPages(oTabGoto)
        Wizard_AfterTabSelect()
    End Sub


    Private Sub Xl_AmtCurBase_AfterUpdateCur(ByVal xCur As MaxiSrvr.Cur) Handles Xl_AmtCurBase.AfterUpdateCur
        If mAllowEvents Then
            mAllowEvents = False
            Dim oCur As MaxiSrvr.Cur = Xl_AmtCurBase.Amt.Cur
            mShowDivisa = (oCur.Id <> "EUR")
            Xl_AmtBasEur.Visible = mShowDivisa
            LabelExchange.Visible = mShowDivisa
            LabelEur.Visible = mShowDivisa
            TextBoxExchangeRate.Visible = mShowDivisa

            TextBoxExchangeRate.Text = oCur.Euros
            LabelExchange.Text = "EUR/" & mProveidor.DefaultCur.Id
            Dim DblVal As Decimal = Xl_AmtCurBase.Amt.Val
            Dim DblEur As Decimal = Math.Round(DblVal * oCur.Euros, 2)
            Dim oBase As New MaxiSrvr.Amt(DblEur, oCur, DblVal)
            Xl_AmtBasEur.Amt = New MaxiSrvr.Amt(DblEur, MaxiSrvr.Cur.Eur, DblEur)
            Xl_AmtCurBase.Amt = oBase
            mAllowEvents = True
            Calcula()
        End If
    End Sub


    Private Sub TextBoxExchangeRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxExchangeRate.TextChanged
        If mAllowEvents Then
            mAllowEvents = False
            Dim oCur As MaxiSrvr.Cur = Xl_AmtCurBase.Amt.Cur
            Dim SngRate As Decimal = CSng(TextBoxExchangeRate.Text)
            oCur.Euros = SngRate
            Dim DblVal As Decimal = Xl_AmtCurBase.Amt.Val
            Dim DblEur As Decimal = Math.Round(DblVal * SngRate, 2)
            Dim oBase As New MaxiSrvr.Amt(DblEur, oCur, DblVal)
            Xl_AmtBasEur.Amt = New MaxiSrvr.Amt(DblEur, MaxiSrvr.Cur.Eur, DblEur)
            Xl_AmtCurBase.Amt = oBase
            mAllowEvents = True
            Calcula()
        End If
    End Sub


    Private Sub Xl_AmtBasEur_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtBasEur.AfterUpdate
        If mAllowEvents Then
            mAllowEvents = False
            Dim oCur As MaxiSrvr.Cur = Xl_AmtCurBase.Amt.Cur
            Dim DblVal As Decimal = Xl_AmtCurBase.Amt.Val
            Dim DblEur As Decimal = Xl_AmtBasEur.Amt.Val
            Dim SngRate As Decimal = Math.Round(DblEur / DblVal, 2)
            TextBoxExchangeRate.Text = SngRate
            Dim oBase As New MaxiSrvr.Amt(DblEur, oCur, DblVal)
            Xl_AmtCurBase.Amt = oBase
            mAllowEvents = True
            Calcula()
        End If
    End Sub

    Private Sub Xl_ContactPrv_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactPrv.AfterUpdate
        Me.Proveidor = New Proveidor(Xl_ContactPrv.Contact.Guid)
    End Sub

    Private Sub DateTimePickerFch_ValueChanged(sender As Object, e As System.EventArgs) Handles DateTimePickerFch.ValueChanged
        If mAllowEvents Then
            Dim DtFch As Date = DateTimePickerFch.Value
            Xl_PercentIVA.Value = MaxiSrvr.Iva.Standard(DtFch).Tipus
            If CheckBoxIRPF.Checked Then
                Xl_PercentIRPF.Value = mProveidor.IRPF(DtFch)
            End If
            Calcula()
            If Xl_Cta1.Cta IsNot Nothing Then
                Xl_Cta1.ValidateFch(DtFch)
            End If
        End If
    End Sub


End Class
