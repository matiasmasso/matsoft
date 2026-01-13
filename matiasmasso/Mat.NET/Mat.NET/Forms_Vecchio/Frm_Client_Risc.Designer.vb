Public Partial Class Frm_Client_Risc
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.ButtonPnds = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelIndexImpagats = New System.Windows.Forms.Label()
        Me.TextBoxIndexImpagats = New System.Windows.Forms.TextBox()
        Me.PictureBoxWarnIndexImpagats = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxSdoImpagats = New System.Windows.Forms.TextBox()
        Me.PictureBoxWarnImpagats = New System.Windows.Forms.PictureBox()
        Me.ButtonImpagats = New System.Windows.Forms.Button()
        Me.TextBoxSdoCta = New System.Windows.Forms.TextBox()
        Me.TextBoxDisposat = New System.Windows.Forms.TextBox()
        Me.TextBoxClassificacio = New System.Windows.Forms.TextBox()
        Me.TextBoxDisponible = New System.Windows.Forms.TextBox()
        Me.TextBoxSdoAlbsCredit = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ButtonAlbsCredit = New System.Windows.Forms.Button()
        Me.ButtonSdoDue = New System.Windows.Forms.Button()
        Me.PictureBoxDue = New System.Windows.Forms.PictureBox()
        Me.TextBoxSdoDue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxDueDias = New System.Windows.Forms.TextBox()
        Me.ButtonLimit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxEntregatACompte = New System.Windows.Forms.TextBox()
        Me.ButtonAlbsNoCredit = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxSdoAlbsNoCredit = New System.Windows.Forms.TextBox()
        Me.ButtonDiasImpagat = New System.Windows.Forms.Button()
        Me.LabelDiesImpagat = New System.Windows.Forms.Label()
        Me.TextBoxDiasImpagat = New System.Windows.Forms.TextBox()
        Me.PictureBoxWarnDiasImpagat = New System.Windows.Forms.PictureBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Client_CreditLimit1 = New Mat.NET.Xl_Client_CreditLimit()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Asnef_logs1 = New Mat.NET.Xl_Asnef_CliLogs()
        Me.Xl_Contact_Logo1 = New Mat.NET.Xl_Contact_Logo()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxDiposit = New System.Windows.Forms.TextBox()
        CType(Me.PictureBoxWarnIndexImpagats, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxWarnImpagats, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxDue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBoxWarnDiasImpagat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(128, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "risc disposat:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(129, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "classificació:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(129, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "disponible:"
        '
        'ButtonExit
        '
        Me.ButtonExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExit.Location = New System.Drawing.Point(382, 544)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(92, 27)
        Me.ButtonExit.TabIndex = 18
        Me.ButtonExit.Text = "SORTIDA"
        '
        'ButtonPnds
        '
        Me.ButtonPnds.Location = New System.Drawing.Point(391, 14)
        Me.ButtonPnds.Name = "ButtonPnds"
        Me.ButtonPnds.Size = New System.Drawing.Size(34, 21)
        Me.ButtonPnds.TabIndex = 7
        Me.ButtonPnds.Text = "..."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "factures pendents de pagament"
        '
        'LabelIndexImpagats
        '
        Me.LabelIndexImpagats.AutoSize = True
        Me.LabelIndexImpagats.Location = New System.Drawing.Point(53, 371)
        Me.LabelIndexImpagats.Name = "LabelIndexImpagats"
        Me.LabelIndexImpagats.Size = New System.Drawing.Size(155, 13)
        Me.LabelIndexImpagats.TabIndex = 19
        Me.LabelIndexImpagats.Text = "Index impagats darrers 6 mesos"
        '
        'TextBoxIndexImpagats
        '
        Me.TextBoxIndexImpagats.Location = New System.Drawing.Point(356, 368)
        Me.TextBoxIndexImpagats.Name = "TextBoxIndexImpagats"
        Me.TextBoxIndexImpagats.ReadOnly = True
        Me.TextBoxIndexImpagats.Size = New System.Drawing.Size(28, 20)
        Me.TextBoxIndexImpagats.TabIndex = 20
        '
        'PictureBoxWarnIndexImpagats
        '
        Me.PictureBoxWarnIndexImpagats.Image = Global.Mat.NET.My.Resources.Resources.warn
        Me.PictureBoxWarnIndexImpagats.Location = New System.Drawing.Point(31, 371)
        Me.PictureBoxWarnIndexImpagats.Name = "PictureBoxWarnIndexImpagats"
        Me.PictureBoxWarnIndexImpagats.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarnIndexImpagats.TabIndex = 21
        Me.PictureBoxWarnIndexImpagats.TabStop = False
        Me.PictureBoxWarnIndexImpagats.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(150, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "impagats pendents de liquidar:"
        '
        'TextBoxSdoImpagats
        '
        Me.TextBoxSdoImpagats.Location = New System.Drawing.Point(261, 342)
        Me.TextBoxSdoImpagats.Name = "TextBoxSdoImpagats"
        Me.TextBoxSdoImpagats.ReadOnly = True
        Me.TextBoxSdoImpagats.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxSdoImpagats.TabIndex = 24
        Me.TextBoxSdoImpagats.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PictureBoxWarnImpagats
        '
        Me.PictureBoxWarnImpagats.Image = Global.Mat.NET.My.Resources.Resources.warn
        Me.PictureBoxWarnImpagats.Location = New System.Drawing.Point(31, 342)
        Me.PictureBoxWarnImpagats.Name = "PictureBoxWarnImpagats"
        Me.PictureBoxWarnImpagats.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarnImpagats.TabIndex = 25
        Me.PictureBoxWarnImpagats.TabStop = False
        Me.PictureBoxWarnImpagats.Visible = False
        '
        'ButtonImpagats
        '
        Me.ButtonImpagats.Location = New System.Drawing.Point(391, 341)
        Me.ButtonImpagats.Name = "ButtonImpagats"
        Me.ButtonImpagats.Size = New System.Drawing.Size(34, 21)
        Me.ButtonImpagats.TabIndex = 26
        Me.ButtonImpagats.Text = "..."
        '
        'TextBoxSdoCta
        '
        Me.TextBoxSdoCta.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxSdoCta.Location = New System.Drawing.Point(261, 15)
        Me.TextBoxSdoCta.Name = "TextBoxSdoCta"
        Me.TextBoxSdoCta.ReadOnly = True
        Me.TextBoxSdoCta.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxSdoCta.TabIndex = 30
        Me.TextBoxSdoCta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxDisposat
        '
        Me.TextBoxDisposat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TextBoxDisposat.Location = New System.Drawing.Point(261, 93)
        Me.TextBoxDisposat.Name = "TextBoxDisposat"
        Me.TextBoxDisposat.ReadOnly = True
        Me.TextBoxDisposat.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxDisposat.TabIndex = 32
        Me.TextBoxDisposat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxClassificacio
        '
        Me.TextBoxClassificacio.BackColor = System.Drawing.Color.Azure
        Me.TextBoxClassificacio.Location = New System.Drawing.Point(262, 138)
        Me.TextBoxClassificacio.Name = "TextBoxClassificacio"
        Me.TextBoxClassificacio.ReadOnly = True
        Me.TextBoxClassificacio.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxClassificacio.TabIndex = 33
        Me.TextBoxClassificacio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxDisponible
        '
        Me.TextBoxDisponible.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBoxDisponible.Location = New System.Drawing.Point(262, 217)
        Me.TextBoxDisponible.Name = "TextBoxDisponible"
        Me.TextBoxDisponible.ReadOnly = True
        Me.TextBoxDisponible.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxDisponible.TabIndex = 34
        Me.TextBoxDisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxSdoAlbsCredit
        '
        Me.TextBoxSdoAlbsCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxSdoAlbsCredit.Location = New System.Drawing.Point(262, 41)
        Me.TextBoxSdoAlbsCredit.Name = "TextBoxSdoAlbsCredit"
        Me.TextBoxSdoAlbsCredit.ReadOnly = True
        Me.TextBoxSdoAlbsCredit.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxSdoAlbsCredit.TabIndex = 35
        Me.TextBoxSdoAlbsCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(54, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(186, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "albarans a crèdit pendents de facturar"
        '
        'ButtonAlbsCredit
        '
        Me.ButtonAlbsCredit.Location = New System.Drawing.Point(391, 41)
        Me.ButtonAlbsCredit.Name = "ButtonAlbsCredit"
        Me.ButtonAlbsCredit.Size = New System.Drawing.Size(34, 21)
        Me.ButtonAlbsCredit.TabIndex = 39
        Me.ButtonAlbsCredit.Text = "..."
        '
        'ButtonSdoDue
        '
        Me.ButtonSdoDue.Location = New System.Drawing.Point(391, 269)
        Me.ButtonSdoDue.Name = "ButtonSdoDue"
        Me.ButtonSdoDue.Size = New System.Drawing.Size(34, 21)
        Me.ButtonSdoDue.TabIndex = 43
        Me.ButtonSdoDue.Text = "..."
        '
        'PictureBoxDue
        '
        Me.PictureBoxDue.Image = Global.Mat.NET.My.Resources.Resources.warn
        Me.PictureBoxDue.Location = New System.Drawing.Point(31, 270)
        Me.PictureBoxDue.Name = "PictureBoxDue"
        Me.PictureBoxDue.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxDue.TabIndex = 42
        Me.PictureBoxDue.TabStop = False
        Me.PictureBoxDue.Visible = False
        '
        'TextBoxSdoDue
        '
        Me.TextBoxSdoDue.Location = New System.Drawing.Point(261, 270)
        Me.TextBoxSdoDue.Name = "TextBoxSdoDue"
        Me.TextBoxSdoDue.ReadOnly = True
        Me.TextBoxSdoDue.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxSdoDue.TabIndex = 41
        Me.TextBoxSdoDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 273)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(143, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "factures vençudes per pagar"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(97, 300)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(174, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "dies promig de retras saldo pendent"
        '
        'TextBoxDueDias
        '
        Me.TextBoxDueDias.Location = New System.Drawing.Point(330, 296)
        Me.TextBoxDueDias.Name = "TextBoxDueDias"
        Me.TextBoxDueDias.ReadOnly = True
        Me.TextBoxDueDias.Size = New System.Drawing.Size(54, 20)
        Me.TextBoxDueDias.TabIndex = 45
        Me.TextBoxDueDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonLimit
        '
        Me.ButtonLimit.Location = New System.Drawing.Point(391, 137)
        Me.ButtonLimit.Name = "ButtonLimit"
        Me.ButtonLimit.Size = New System.Drawing.Size(34, 21)
        Me.ButtonLimit.TabIndex = 46
        Me.ButtonLimit.Text = "..."
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 63)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(471, 475)
        Me.TabControl1.TabIndex = 47
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.TextBoxDiposit)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.TextBoxEntregatACompte)
        Me.TabPage1.Controls.Add(Me.ButtonAlbsNoCredit)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxSdoAlbsNoCredit)
        Me.TabPage1.Controls.Add(Me.ButtonDiasImpagat)
        Me.TabPage1.Controls.Add(Me.LabelDiesImpagat)
        Me.TabPage1.Controls.Add(Me.TextBoxDiasImpagat)
        Me.TabPage1.Controls.Add(Me.PictureBoxWarnDiasImpagat)
        Me.TabPage1.Controls.Add(Me.TextBoxSdoCta)
        Me.TabPage1.Controls.Add(Me.ButtonLimit)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxDueDias)
        Me.TabPage1.Controls.Add(Me.ButtonPnds)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.ButtonSdoDue)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.PictureBoxDue)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxSdoDue)
        Me.TabPage1.Controls.Add(Me.LabelIndexImpagats)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxIndexImpagats)
        Me.TabPage1.Controls.Add(Me.ButtonAlbsCredit)
        Me.TabPage1.Controls.Add(Me.PictureBoxWarnIndexImpagats)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxSdoAlbsCredit)
        Me.TabPage1.Controls.Add(Me.TextBoxSdoImpagats)
        Me.TabPage1.Controls.Add(Me.TextBoxDisponible)
        Me.TabPage1.Controls.Add(Me.PictureBoxWarnImpagats)
        Me.TabPage1.Controls.Add(Me.TextBoxClassificacio)
        Me.TabPage1.Controls.Add(Me.ButtonImpagats)
        Me.TabPage1.Controls.Add(Me.TextBoxDisposat)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(463, 449)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "GENERAL"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(128, 167)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 13)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "entregat a compte:"
        '
        'TextBoxEntregatACompte
        '
        Me.TextBoxEntregatACompte.BackColor = System.Drawing.Color.Azure
        Me.TextBoxEntregatACompte.Location = New System.Drawing.Point(261, 164)
        Me.TextBoxEntregatACompte.Name = "TextBoxEntregatACompte"
        Me.TextBoxEntregatACompte.ReadOnly = True
        Me.TextBoxEntregatACompte.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxEntregatACompte.TabIndex = 55
        Me.TextBoxEntregatACompte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonAlbsNoCredit
        '
        Me.ButtonAlbsNoCredit.Location = New System.Drawing.Point(391, 67)
        Me.ButtonAlbsNoCredit.Name = "ButtonAlbsNoCredit"
        Me.ButtonAlbsNoCredit.Size = New System.Drawing.Size(34, 21)
        Me.ButtonAlbsNoCredit.TabIndex = 53
        Me.ButtonAlbsNoCredit.Text = "..."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(54, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(186, 13)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "albarans cobrats pendents de facturar"
        '
        'TextBoxSdoAlbsNoCredit
        '
        Me.TextBoxSdoAlbsNoCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBoxSdoAlbsNoCredit.Location = New System.Drawing.Point(262, 67)
        Me.TextBoxSdoAlbsNoCredit.Name = "TextBoxSdoAlbsNoCredit"
        Me.TextBoxSdoAlbsNoCredit.ReadOnly = True
        Me.TextBoxSdoAlbsNoCredit.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxSdoAlbsNoCredit.TabIndex = 51
        Me.TextBoxSdoAlbsNoCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonDiasImpagat
        '
        Me.ButtonDiasImpagat.Location = New System.Drawing.Point(391, 389)
        Me.ButtonDiasImpagat.Name = "ButtonDiasImpagat"
        Me.ButtonDiasImpagat.Size = New System.Drawing.Size(34, 21)
        Me.ButtonDiasImpagat.TabIndex = 50
        Me.ButtonDiasImpagat.Text = "..."
        '
        'LabelDiesImpagat
        '
        Me.LabelDiesImpagat.AutoSize = True
        Me.LabelDiesImpagat.Location = New System.Drawing.Point(53, 394)
        Me.LabelDiesImpagat.Name = "LabelDiesImpagat"
        Me.LabelDiesImpagat.Size = New System.Drawing.Size(146, 13)
        Me.LabelDiesImpagat.TabIndex = 47
        Me.LabelDiesImpagat.Text = "mitjana dies recobro impagats"
        '
        'TextBoxDiasImpagat
        '
        Me.TextBoxDiasImpagat.Location = New System.Drawing.Point(356, 391)
        Me.TextBoxDiasImpagat.Name = "TextBoxDiasImpagat"
        Me.TextBoxDiasImpagat.ReadOnly = True
        Me.TextBoxDiasImpagat.Size = New System.Drawing.Size(28, 20)
        Me.TextBoxDiasImpagat.TabIndex = 48
        '
        'PictureBoxWarnDiasImpagat
        '
        Me.PictureBoxWarnDiasImpagat.Image = Global.Mat.NET.My.Resources.Resources.warn
        Me.PictureBoxWarnDiasImpagat.Location = New System.Drawing.Point(31, 394)
        Me.PictureBoxWarnDiasImpagat.Name = "PictureBoxWarnDiasImpagat"
        Me.PictureBoxWarnDiasImpagat.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarnDiasImpagat.TabIndex = 49
        Me.PictureBoxWarnDiasImpagat.TabStop = False
        Me.PictureBoxWarnDiasImpagat.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Client_CreditLimit1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(463, 415)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "CLASSIFICACIONS"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Client_CreditLimit1
        '
        Me.Xl_Client_CreditLimit1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Client_CreditLimit1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Client_CreditLimit1.Name = "Xl_Client_CreditLimit1"
        Me.Xl_Client_CreditLimit1.Size = New System.Drawing.Size(457, 409)
        Me.Xl_Client_CreditLimit1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Asnef_logs1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(463, 415)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "ASNEF"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Asnef_logs1
        '
        Me.Xl_Asnef_logs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Asnef_logs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Asnef_logs1.Name = "Xl_Asnef_logs1"
        Me.Xl_Asnef_logs1.Size = New System.Drawing.Size(457, 409)
        Me.Xl_Asnef_logs1.TabIndex = 0
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(318, 9)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 48
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(128, 193)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "diposit irrevocable:"
        '
        'TextBoxDiposit
        '
        Me.TextBoxDiposit.BackColor = System.Drawing.Color.Azure
        Me.TextBoxDiposit.Location = New System.Drawing.Point(261, 190)
        Me.TextBoxDiposit.Name = "TextBoxDiposit"
        Me.TextBoxDiposit.ReadOnly = True
        Me.TextBoxDiposit.Size = New System.Drawing.Size(123, 20)
        Me.TextBoxDiposit.TabIndex = 57
        Me.TextBoxDiposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_Client_Risc
        '
        Me.ClientSize = New System.Drawing.Size(477, 573)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonExit)
        Me.Name = "Frm_Client_Risc"
        Me.Text = "RISC"
        CType(Me.PictureBoxWarnIndexImpagats, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxWarnImpagats, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxDue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBoxWarnDiasImpagat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents ButtonPnds As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelIndexImpagats As System.Windows.Forms.Label
    Friend WithEvents TextBoxIndexImpagats As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWarnIndexImpagats As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSdoImpagats As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWarnImpagats As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonImpagats As System.Windows.Forms.Button
    Friend WithEvents TextBoxSdoCta As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDisposat As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxClassificacio As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDisponible As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSdoAlbsCredit As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ButtonAlbsCredit As System.Windows.Forms.Button
    Friend WithEvents ButtonSdoDue As System.Windows.Forms.Button
    Friend WithEvents PictureBoxDue As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxSdoDue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDueDias As System.Windows.Forms.TextBox
    Friend WithEvents ButtonLimit As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Client_CreditLimit1 As Xl_Client_CreditLimit
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Asnef_logs1 As Xl_Asnef_CliLogs
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents LabelDiesImpagat As System.Windows.Forms.Label
    Friend WithEvents TextBoxDiasImpagat As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWarnDiasImpagat As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonDiasImpagat As System.Windows.Forms.Button
    Friend WithEvents ButtonAlbsNoCredit As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSdoAlbsNoCredit As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEntregatACompte As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDiposit As System.Windows.Forms.TextBox
End Class
