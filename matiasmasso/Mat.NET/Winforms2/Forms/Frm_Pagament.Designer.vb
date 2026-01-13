<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Pagament
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
        Dim DtoAmt5 As DTOAmt = New DTOAmt()
        Dim DtoAmt6 As DTOAmt = New DTOAmt()
        Dim DtoAmt7 As DTOAmt = New DTOAmt()
        Dim DtoAmt8 As DTOAmt = New DTOAmt()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Pnds_Select1 = New Xl_Pnds_Select()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Xl_BancsComboBox1 = New Xl_BancsComboBox()
        Me.LabelTitularVisa = New System.Windows.Forms.Label()
        Me.Xl_LookupVisaCard1 = New Xl_LookupVisaCard()
        Me.RadioButtonVisa = New System.Windows.Forms.RadioButton()
        Me.LabelBancEmissor = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxCcaConcept = New System.Windows.Forms.TextBox()
        Me.GroupBoxCreditTransfer = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxTransferConcept = New System.Windows.Forms.TextBox()
        Me.Xl_IbanDigits1 = New Xl_IbanDigits()
        Me.RadioButtonCreditTransfer = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDirectDebit = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_AmountCur1 = New Xl_AmountCur()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelDivisa = New System.Windows.Forms.Label()
        Me.Xl_EurLiquid = New Xl_Eur()
        Me.Xl_EurContravalor = New Xl_Eur()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabelExchange = New System.Windows.Forms.Label()
        Me.Xl_EurDespeses = New Xl_Eur()
        Me.Xl_EurDiferenciesDeCanvi = New Xl_Eur()
        Me.LabelDiferenciesDeCanvi = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PictureBoxResult = New System.Windows.Forms.PictureBox()
        Me.GroupBoxTransferResult = New System.Windows.Forms.GroupBox()
        Me.ButtonSaveTransferRef = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxBancTransferPoolRef = New System.Windows.Forms.TextBox()
        Me.ButtonSepaCreditTransfer = New System.Windows.Forms.Button()
        Me.ButtonEmail = New System.Windows.Forms.Button()
        Me.TextBoxSepaCreditTransfer = New System.Windows.Forms.TextBox()
        Me.LabelSaveFile = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_DocFile1 = New Xl_DocFile_Old()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBoxCreditTransfer.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBoxResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxTransferResult.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonPrevious)
        Me.Panel1.Controls.Add(Me.ButtonNext)
        Me.Panel1.Controls.Add(Me.ButtonEnd)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 461)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1029, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(700, 4)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(104, 24)
        Me.ButtonPrevious.TabIndex = 15
        Me.ButtonPrevious.Text = "< Enrera"
        Me.ButtonPrevious.UseVisualStyleBackColor = False
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNext.Location = New System.Drawing.Point(810, 4)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(104, 24)
        Me.ButtonNext.TabIndex = 12
        Me.ButtonNext.Text = "Següent >"
        Me.ButtonNext.UseVisualStyleBackColor = False
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(921, 4)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(104, 24)
        Me.ButtonEnd.TabIndex = 11
        Me.ButtonEnd.Text = "Fi >>"
        Me.ButtonEnd.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(672, 421)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Pnds_Select1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(664, 395)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Selecció de factures a pagar"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Pnds_Select1
        '
        Me.Xl_Pnds_Select1.Codi = DTOPnd.Codis.NotSet
        Me.Xl_Pnds_Select1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Pnds_Select1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Pnds_Select1.Name = "Xl_Pnds_Select1"
        Me.Xl_Pnds_Select1.Size = New System.Drawing.Size(658, 389)
        Me.Xl_Pnds_Select1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(664, 395)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Forma de pagament"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Xl_BancsComboBox1)
        Me.GroupBox2.Controls.Add(Me.LabelTitularVisa)
        Me.GroupBox2.Controls.Add(Me.Xl_LookupVisaCard1)
        Me.GroupBox2.Controls.Add(Me.RadioButtonVisa)
        Me.GroupBox2.Controls.Add(Me.LabelBancEmissor)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBoxCcaConcept)
        Me.GroupBox2.Controls.Add(Me.GroupBoxCreditTransfer)
        Me.GroupBox2.Controls.Add(Me.RadioButtonCreditTransfer)
        Me.GroupBox2.Controls.Add(Me.RadioButtonDirectDebit)
        Me.GroupBox2.Location = New System.Drawing.Point(213, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(443, 328)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Forma de pago"
        '
        'Xl_BancsComboBox1
        '
        Me.Xl_BancsComboBox1.FormattingEnabled = True
        Me.Xl_BancsComboBox1.Location = New System.Drawing.Point(164, 89)
        Me.Xl_BancsComboBox1.Name = "Xl_BancsComboBox1"
        Me.Xl_BancsComboBox1.Size = New System.Drawing.Size(251, 21)
        Me.Xl_BancsComboBox1.TabIndex = 10
        '
        'LabelTitularVisa
        '
        Me.LabelTitularVisa.AutoSize = True
        Me.LabelTitularVisa.Location = New System.Drawing.Point(161, 26)
        Me.LabelTitularVisa.Name = "LabelTitularVisa"
        Me.LabelTitularVisa.Size = New System.Drawing.Size(36, 13)
        Me.LabelTitularVisa.TabIndex = 9
        Me.LabelTitularVisa.Text = "Titular"
        Me.LabelTitularVisa.Visible = False
        '
        'Xl_LookupVisaCard1
        '
        Me.Xl_LookupVisaCard1.IsDirty = False
        Me.Xl_LookupVisaCard1.Location = New System.Drawing.Point(164, 42)
        Me.Xl_LookupVisaCard1.Name = "Xl_LookupVisaCard1"
        Me.Xl_LookupVisaCard1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVisaCard1.Size = New System.Drawing.Size(251, 20)
        Me.Xl_LookupVisaCard1.TabIndex = 8
        Me.Xl_LookupVisaCard1.Value = Nothing
        Me.Xl_LookupVisaCard1.VisaCard = Nothing
        '
        'RadioButtonVisa
        '
        Me.RadioButtonVisa.AutoSize = True
        Me.RadioButtonVisa.Checked = True
        Me.RadioButtonVisa.Location = New System.Drawing.Point(26, 42)
        Me.RadioButtonVisa.Name = "RadioButtonVisa"
        Me.RadioButtonVisa.Size = New System.Drawing.Size(93, 17)
        Me.RadioButtonVisa.TabIndex = 7
        Me.RadioButtonVisa.TabStop = True
        Me.RadioButtonVisa.Text = "Tarja de crèdit"
        Me.RadioButtonVisa.UseVisualStyleBackColor = True
        '
        'LabelBancEmissor
        '
        Me.LabelBancEmissor.AutoSize = True
        Me.LabelBancEmissor.Location = New System.Drawing.Point(161, 72)
        Me.LabelBancEmissor.Name = "LabelBancEmissor"
        Me.LabelBancEmissor.Size = New System.Drawing.Size(81, 13)
        Me.LabelBancEmissor.TabIndex = 6
        Me.LabelBancEmissor.Text = "Entitat emissora"
        Me.LabelBancEmissor.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(161, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(145, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Concepte per el assentament"
        '
        'TextBoxCcaConcept
        '
        Me.TextBoxCcaConcept.Location = New System.Drawing.Point(164, 138)
        Me.TextBoxCcaConcept.MaxLength = 60
        Me.TextBoxCcaConcept.Name = "TextBoxCcaConcept"
        Me.TextBoxCcaConcept.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxCcaConcept.TabIndex = 4
        '
        'GroupBoxCreditTransfer
        '
        Me.GroupBoxCreditTransfer.Controls.Add(Me.Label5)
        Me.GroupBoxCreditTransfer.Controls.Add(Me.TextBoxTransferConcept)
        Me.GroupBoxCreditTransfer.Controls.Add(Me.Xl_IbanDigits1)
        Me.GroupBoxCreditTransfer.Location = New System.Drawing.Point(158, 165)
        Me.GroupBoxCreditTransfer.Name = "GroupBoxCreditTransfer"
        Me.GroupBoxCreditTransfer.Size = New System.Drawing.Size(266, 144)
        Me.GroupBoxCreditTransfer.TabIndex = 3
        Me.GroupBoxCreditTransfer.TabStop = False
        Me.GroupBoxCreditTransfer.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Concepte per el beneficiari"
        '
        'TextBoxTransferConcept
        '
        Me.TextBoxTransferConcept.Location = New System.Drawing.Point(6, 36)
        Me.TextBoxTransferConcept.Name = "TextBoxTransferConcept"
        Me.TextBoxTransferConcept.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxTransferConcept.TabIndex = 1
        '
        'Xl_IbanDigits1
        '
        Me.Xl_IbanDigits1.BankBranch = Nothing
        Me.Xl_IbanDigits1.Location = New System.Drawing.Point(6, 67)
        Me.Xl_IbanDigits1.Name = "Xl_IbanDigits1"
        Me.Xl_IbanDigits1.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigits1.TabIndex = 0
        '
        'RadioButtonCreditTransfer
        '
        Me.RadioButtonCreditTransfer.AutoSize = True
        Me.RadioButtonCreditTransfer.Location = New System.Drawing.Point(26, 96)
        Me.RadioButtonCreditTransfer.Name = "RadioButtonCreditTransfer"
        Me.RadioButtonCreditTransfer.Size = New System.Drawing.Size(90, 17)
        Me.RadioButtonCreditTransfer.TabIndex = 1
        Me.RadioButtonCreditTransfer.TabStop = True
        Me.RadioButtonCreditTransfer.Text = "Transferència"
        Me.RadioButtonCreditTransfer.UseVisualStyleBackColor = True
        '
        'RadioButtonDirectDebit
        '
        Me.RadioButtonDirectDebit.AutoSize = True
        Me.RadioButtonDirectDebit.Location = New System.Drawing.Point(26, 69)
        Me.RadioButtonDirectDebit.Name = "RadioButtonDirectDebit"
        Me.RadioButtonDirectDebit.Size = New System.Drawing.Size(112, 17)
        Me.RadioButtonDirectDebit.TabIndex = 0
        Me.RadioButtonDirectDebit.TabStop = True
        Me.RadioButtonDirectDebit.Text = "Domiciliat en banc"
        Me.RadioButtonDirectDebit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Xl_AmountCur1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.LabelDivisa)
        Me.GroupBox1.Controls.Add(Me.Xl_EurLiquid)
        Me.GroupBox1.Controls.Add(Me.Xl_EurContravalor)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.LabelExchange)
        Me.GroupBox1.Controls.Add(Me.Xl_EurDespeses)
        Me.GroupBox1.Controls.Add(Me.Xl_EurDiferenciesDeCanvi)
        Me.GroupBox1.Controls.Add(Me.LabelDiferenciesDeCanvi)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 328)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Imports"
        '
        'Xl_AmountCur1
        '
        Me.Xl_AmountCur1.Amt = Nothing
        Me.Xl_AmountCur1.Location = New System.Drawing.Point(31, 69)
        Me.Xl_AmountCur1.Name = "Xl_AmountCur1"
        Me.Xl_AmountCur1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCur1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 247)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Total a pagar:"
        '
        'LabelDivisa
        '
        Me.LabelDivisa.AutoSize = True
        Me.LabelDivisa.Location = New System.Drawing.Point(7, 49)
        Me.LabelDivisa.Name = "LabelDivisa"
        Me.LabelDivisa.Size = New System.Drawing.Size(96, 13)
        Me.LabelDivisa.TabIndex = 1
        Me.LabelDivisa.Text = "Total sel.leccionat:"
        '
        'Xl_EurLiquid
        '
        DtoAmt5.Cur = Nothing
        DtoAmt5.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt5.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurLiquid.Amt = DtoAmt5
        Me.Xl_EurLiquid.Location = New System.Drawing.Point(31, 263)
        Me.Xl_EurLiquid.Name = "Xl_EurLiquid"
        Me.Xl_EurLiquid.ReadOnly = True
        Me.Xl_EurLiquid.Size = New System.Drawing.Size(120, 20)
        Me.Xl_EurLiquid.TabIndex = 8
        Me.Xl_EurLiquid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_EurContravalor
        '
        DtoAmt6.Cur = Nothing
        DtoAmt6.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt6.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurContravalor.Amt = DtoAmt6
        Me.Xl_EurContravalor.Location = New System.Drawing.Point(31, 117)
        Me.Xl_EurContravalor.Name = "Xl_EurContravalor"
        Me.Xl_EurContravalor.Size = New System.Drawing.Size(120, 20)
        Me.Xl_EurContravalor.TabIndex = 2
        Me.Xl_EurContravalor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Despeses:"
        '
        'LabelExchange
        '
        Me.LabelExchange.AutoSize = True
        Me.LabelExchange.Location = New System.Drawing.Point(7, 101)
        Me.LabelExchange.Name = "LabelExchange"
        Me.LabelExchange.Size = New System.Drawing.Size(98, 13)
        Me.LabelExchange.TabIndex = 3
        Me.LabelExchange.Text = "Contravalor en Eur:"
        '
        'Xl_EurDespeses
        '
        DtoAmt7.Cur = Nothing
        DtoAmt7.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt7.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurDespeses.Amt = DtoAmt7
        Me.Xl_EurDespeses.Location = New System.Drawing.Point(31, 214)
        Me.Xl_EurDespeses.Name = "Xl_EurDespeses"
        Me.Xl_EurDespeses.Size = New System.Drawing.Size(120, 20)
        Me.Xl_EurDespeses.TabIndex = 6
        Me.Xl_EurDespeses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_EurDiferenciesDeCanvi
        '
        DtoAmt8.Cur = Nothing
        DtoAmt8.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt8.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurDiferenciesDeCanvi.Amt = DtoAmt8
        Me.Xl_EurDiferenciesDeCanvi.Location = New System.Drawing.Point(31, 165)
        Me.Xl_EurDiferenciesDeCanvi.Name = "Xl_EurDiferenciesDeCanvi"
        Me.Xl_EurDiferenciesDeCanvi.ReadOnly = True
        Me.Xl_EurDiferenciesDeCanvi.Size = New System.Drawing.Size(120, 20)
        Me.Xl_EurDiferenciesDeCanvi.TabIndex = 4
        Me.Xl_EurDiferenciesDeCanvi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelDiferenciesDeCanvi
        '
        Me.LabelDiferenciesDeCanvi.AutoSize = True
        Me.LabelDiferenciesDeCanvi.Location = New System.Drawing.Point(7, 149)
        Me.LabelDiferenciesDeCanvi.Name = "LabelDiferenciesDeCanvi"
        Me.LabelDiferenciesDeCanvi.Size = New System.Drawing.Size(107, 13)
        Me.LabelDiferenciesDeCanvi.TabIndex = 5
        Me.LabelDiferenciesDeCanvi.Text = "Diferencies de canvi:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PictureBoxResult)
        Me.TabPage3.Controls.Add(Me.GroupBoxTransferResult)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.TextBoxResult)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(664, 395)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Resum"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PictureBoxResult
        '
        Me.PictureBoxResult.Location = New System.Drawing.Point(504, 107)
        Me.PictureBoxResult.Name = "PictureBoxResult"
        Me.PictureBoxResult.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxResult.TabIndex = 9
        Me.PictureBoxResult.TabStop = False
        '
        'GroupBoxTransferResult
        '
        Me.GroupBoxTransferResult.Controls.Add(Me.ButtonSaveTransferRef)
        Me.GroupBoxTransferResult.Controls.Add(Me.Label1)
        Me.GroupBoxTransferResult.Controls.Add(Me.TextBoxBancTransferPoolRef)
        Me.GroupBoxTransferResult.Controls.Add(Me.ButtonSepaCreditTransfer)
        Me.GroupBoxTransferResult.Controls.Add(Me.ButtonEmail)
        Me.GroupBoxTransferResult.Controls.Add(Me.TextBoxSepaCreditTransfer)
        Me.GroupBoxTransferResult.Controls.Add(Me.LabelSaveFile)
        Me.GroupBoxTransferResult.Location = New System.Drawing.Point(54, 154)
        Me.GroupBoxTransferResult.Name = "GroupBoxTransferResult"
        Me.GroupBoxTransferResult.Size = New System.Drawing.Size(468, 134)
        Me.GroupBoxTransferResult.TabIndex = 8
        Me.GroupBoxTransferResult.TabStop = False
        Me.GroupBoxTransferResult.Text = "Transferencia"
        '
        'ButtonSaveTransferRef
        '
        Me.ButtonSaveTransferRef.Enabled = False
        Me.ButtonSaveTransferRef.Location = New System.Drawing.Point(398, 73)
        Me.ButtonSaveTransferRef.Name = "ButtonSaveTransferRef"
        Me.ButtonSaveTransferRef.Size = New System.Drawing.Size(45, 21)
        Me.ButtonSaveTransferRef.TabIndex = 10
        Me.ButtonSaveTransferRef.Text = "desar"
        Me.ButtonSaveTransferRef.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Referencia del banc justificant de la transferència"
        '
        'TextBoxBancTransferPoolRef
        '
        Me.TextBoxBancTransferPoolRef.Location = New System.Drawing.Point(269, 74)
        Me.TextBoxBancTransferPoolRef.Name = "TextBoxBancTransferPoolRef"
        Me.TextBoxBancTransferPoolRef.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxBancTransferPoolRef.TabIndex = 8
        '
        'ButtonSepaCreditTransfer
        '
        Me.ButtonSepaCreditTransfer.Location = New System.Drawing.Point(398, 48)
        Me.ButtonSepaCreditTransfer.Name = "ButtonSepaCreditTransfer"
        Me.ButtonSepaCreditTransfer.Size = New System.Drawing.Size(45, 20)
        Me.ButtonSepaCreditTransfer.TabIndex = 7
        Me.ButtonSepaCreditTransfer.Text = "..."
        Me.ButtonSepaCreditTransfer.UseVisualStyleBackColor = True
        '
        'ButtonEmail
        '
        Me.ButtonEmail.Location = New System.Drawing.Point(269, 100)
        Me.ButtonEmail.Name = "ButtonEmail"
        Me.ButtonEmail.Size = New System.Drawing.Size(131, 23)
        Me.ButtonEmail.TabIndex = 1
        Me.ButtonEmail.Text = "notificació per email"
        Me.ButtonEmail.UseVisualStyleBackColor = True
        '
        'TextBoxSepaCreditTransfer
        '
        Me.TextBoxSepaCreditTransfer.Location = New System.Drawing.Point(20, 48)
        Me.TextBoxSepaCreditTransfer.Name = "TextBoxSepaCreditTransfer"
        Me.TextBoxSepaCreditTransfer.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxSepaCreditTransfer.TabIndex = 6
        '
        'LabelSaveFile
        '
        Me.LabelSaveFile.AutoSize = True
        Me.LabelSaveFile.Location = New System.Drawing.Point(23, 32)
        Me.LabelSaveFile.Name = "LabelSaveFile"
        Me.LabelSaveFile.Size = New System.Drawing.Size(250, 13)
        Me.LabelSaveFile.TabIndex = 5
        Me.LabelSaveFile.Text = "Desar fitxer Sepa Credit Transfer (Quadern 34 XML)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(74, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "resultat"
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Location = New System.Drawing.Point(74, 107)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.Size = New System.Drawing.Size(423, 20)
        Me.TextBoxResult.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(923, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker1.TabIndex = 44
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 38)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_DocFile1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1026, 421)
        Me.SplitContainer1.SplitterDistance = 350
        Me.SplitContainer1.TabIndex = 45
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1029, 24)
        Me.MenuStrip1.TabIndex = 46
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Frm_Pagament
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 492)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Pagament"
        Me.Text = "Pagament a Proveidor"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBoxCreditTransfer.ResumeLayout(False)
        Me.GroupBoxCreditTransfer.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PictureBoxResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxTransferResult.ResumeLayout(False)
        Me.GroupBoxTransferResult.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonNext As Button
    Friend WithEvents ButtonEnd As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents ButtonPrevious As Button
    Friend WithEvents Xl_Pnds_Select1 As Xl_Pnds_Select
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Xl_AmountCur1 As Xl_AmountCur
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelDivisa As Label
    Friend WithEvents Xl_EurLiquid As Xl_Eur
    Friend WithEvents Xl_EurContravalor As Xl_Eur
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelExchange As Label
    Friend WithEvents Xl_EurDespeses As Xl_Eur
    Friend WithEvents Xl_EurDiferenciesDeCanvi As Xl_Eur
    Friend WithEvents LabelDiferenciesDeCanvi As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents LabelBancEmissor As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxCcaConcept As TextBox
    Friend WithEvents GroupBoxCreditTransfer As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxTransferConcept As TextBox
    Friend WithEvents Xl_IbanDigits1 As Xl_IbanDigits
    Friend WithEvents RadioButtonCreditTransfer As RadioButton
    Friend WithEvents RadioButtonDirectDebit As RadioButton
    Friend WithEvents LabelTitularVisa As Label
    Friend WithEvents Xl_LookupVisaCard1 As Xl_LookupVisaCard
    Friend WithEvents RadioButtonVisa As RadioButton
    Friend WithEvents GroupBoxTransferResult As GroupBox
    Friend WithEvents ButtonSepaCreditTransfer As Button
    Friend WithEvents ButtonEmail As Button
    Friend WithEvents TextBoxSepaCreditTransfer As TextBox
    Friend WithEvents LabelSaveFile As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxResult As TextBox
    Friend WithEvents PictureBoxResult As PictureBox
    Friend WithEvents Xl_BancsComboBox1 As Xl_BancsComboBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ButtonSaveTransferRef As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxBancTransferPoolRef As TextBox
End Class
