<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Impagat
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxRefBanc = New System.Windows.Forms.TextBox()
        Me.DateTimePickerAFP = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerSaldo = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxAFP = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSaldat = New System.Windows.Forms.CheckBox()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageImpago = New System.Windows.Forms.TabPage()
        Me.Xl_LookupCcaIncobrable = New Winforms.Xl_LookupCca()
        Me.CheckBoxIncobrable = New System.Windows.Forms.CheckBox()
        Me.GroupBoxAsnef = New System.Windows.Forms.GroupBox()
        Me.CheckBoxAsnefBaixa = New System.Windows.Forms.CheckBox()
        Me.CheckBoxAsnefAlta = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerAsnefBaixa = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerAsnefAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_AmtPendent = New Winforms.Xl_Amount()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_AmtDeute = New Winforms.Xl_Amount()
        Me.Xl_AmtDespeses = New Winforms.Xl_Amount()
        Me.Xl_AmtPagatACompte = New Winforms.Xl_Amount()
        Me.Xl_AmtNominal = New Winforms.Xl_Amount()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.PictureBoxBancLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxCsa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEur = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxVto = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageImpago.SuspendLayout()
        Me.GroupBoxAsnef.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 444)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(440, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(221, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 22
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(332, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 21
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.TabIndex = 20
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ref.bancaria"
        '
        'TextBoxRefBanc
        '
        Me.TextBoxRefBanc.Location = New System.Drawing.Point(104, 16)
        Me.TextBoxRefBanc.Name = "TextBoxRefBanc"
        Me.TextBoxRefBanc.Size = New System.Drawing.Size(180, 20)
        Me.TextBoxRefBanc.TabIndex = 1
        '
        'DateTimePickerAFP
        '
        Me.DateTimePickerAFP.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAFP.Location = New System.Drawing.Point(145, 187)
        Me.DateTimePickerAFP.Name = "DateTimePickerAFP"
        Me.DateTimePickerAFP.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePickerAFP.TabIndex = 13
        '
        'DateTimePickerSaldo
        '
        Me.DateTimePickerSaldo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerSaldo.Location = New System.Drawing.Point(145, 210)
        Me.DateTimePickerSaldo.Name = "DateTimePickerSaldo"
        Me.DateTimePickerSaldo.Size = New System.Drawing.Size(103, 20)
        Me.DateTimePickerSaldo.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Despeses"
        '
        'CheckBoxAFP
        '
        Me.CheckBoxAFP.AutoSize = True
        Me.CheckBoxAFP.Location = New System.Drawing.Point(10, 190)
        Me.CheckBoxAFP.Name = "CheckBoxAFP"
        Me.CheckBoxAFP.Size = New System.Drawing.Size(130, 17)
        Me.CheckBoxAFP.TabIndex = 12
        Me.CheckBoxAFP.Text = "Avis de Falta de Pago"
        Me.CheckBoxAFP.UseVisualStyleBackColor = True
        '
        'CheckBoxSaldat
        '
        Me.CheckBoxSaldat.AutoSize = True
        Me.CheckBoxSaldat.Location = New System.Drawing.Point(10, 210)
        Me.CheckBoxSaldat.Name = "CheckBoxSaldat"
        Me.CheckBoxSaldat.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxSaldat.TabIndex = 14
        Me.CheckBoxSaldat.Text = "Saldat"
        Me.CheckBoxSaldat.UseVisualStyleBackColor = True
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCliNom.Location = New System.Drawing.Point(12, 12)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(420, 20)
        Me.TextBoxCliNom.TabIndex = 58
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageImpago)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 38)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(424, 401)
        Me.TabControl1.TabIndex = 59
        '
        'TabPageImpago
        '
        Me.TabPageImpago.Controls.Add(Me.Xl_LookupCcaIncobrable)
        Me.TabPageImpago.Controls.Add(Me.CheckBoxIncobrable)
        Me.TabPageImpago.Controls.Add(Me.GroupBoxAsnef)
        Me.TabPageImpago.Controls.Add(Me.Label11)
        Me.TabPageImpago.Controls.Add(Me.TextBoxObs)
        Me.TabPageImpago.Controls.Add(Me.Label10)
        Me.TabPageImpago.Controls.Add(Me.Xl_AmtPendent)
        Me.TabPageImpago.Controls.Add(Me.Label9)
        Me.TabPageImpago.Controls.Add(Me.Label8)
        Me.TabPageImpago.Controls.Add(Me.Xl_AmtDeute)
        Me.TabPageImpago.Controls.Add(Me.Xl_AmtDespeses)
        Me.TabPageImpago.Controls.Add(Me.Xl_AmtPagatACompte)
        Me.TabPageImpago.Controls.Add(Me.Xl_AmtNominal)
        Me.TabPageImpago.Controls.Add(Me.Label7)
        Me.TabPageImpago.Controls.Add(Me.TextBoxRefBanc)
        Me.TabPageImpago.Controls.Add(Me.Label1)
        Me.TabPageImpago.Controls.Add(Me.DateTimePickerAFP)
        Me.TabPageImpago.Controls.Add(Me.DateTimePickerSaldo)
        Me.TabPageImpago.Controls.Add(Me.Label5)
        Me.TabPageImpago.Controls.Add(Me.CheckBoxSaldat)
        Me.TabPageImpago.Controls.Add(Me.CheckBoxAFP)
        Me.TabPageImpago.Location = New System.Drawing.Point(4, 22)
        Me.TabPageImpago.Name = "TabPageImpago"
        Me.TabPageImpago.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageImpago.Size = New System.Drawing.Size(416, 375)
        Me.TabPageImpago.TabIndex = 0
        Me.TabPageImpago.Text = "IMPAGO"
        Me.TabPageImpago.UseVisualStyleBackColor = True
        '
        'Xl_LookupCcaIncobrable
        '
        Me.Xl_LookupCcaIncobrable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupCcaIncobrable.Cca = Nothing
        Me.Xl_LookupCcaIncobrable.IsDirty = False
        Me.Xl_LookupCcaIncobrable.Location = New System.Drawing.Point(145, 236)
        Me.Xl_LookupCcaIncobrable.Name = "Xl_LookupCcaIncobrable"
        Me.Xl_LookupCcaIncobrable.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCcaIncobrable.Size = New System.Drawing.Size(250, 20)
        Me.Xl_LookupCcaIncobrable.TabIndex = 24
        Me.Xl_LookupCcaIncobrable.Value = Nothing
        Me.Xl_LookupCcaIncobrable.Visible = False
        '
        'CheckBoxIncobrable
        '
        Me.CheckBoxIncobrable.AutoSize = True
        Me.CheckBoxIncobrable.Location = New System.Drawing.Point(9, 236)
        Me.CheckBoxIncobrable.Name = "CheckBoxIncobrable"
        Me.CheckBoxIncobrable.Size = New System.Drawing.Size(130, 17)
        Me.CheckBoxIncobrable.TabIndex = 23
        Me.CheckBoxIncobrable.Text = "Abonat per incobrable"
        Me.CheckBoxIncobrable.UseVisualStyleBackColor = True
        '
        'GroupBoxAsnef
        '
        Me.GroupBoxAsnef.Controls.Add(Me.CheckBoxAsnefBaixa)
        Me.GroupBoxAsnef.Controls.Add(Me.CheckBoxAsnefAlta)
        Me.GroupBoxAsnef.Controls.Add(Me.DateTimePickerAsnefBaixa)
        Me.GroupBoxAsnef.Controls.Add(Me.DateTimePickerAsnefAlta)
        Me.GroupBoxAsnef.Location = New System.Drawing.Point(236, 50)
        Me.GroupBoxAsnef.Name = "GroupBoxAsnef"
        Me.GroupBoxAsnef.Size = New System.Drawing.Size(160, 110)
        Me.GroupBoxAsnef.TabIndex = 22
        Me.GroupBoxAsnef.TabStop = False
        Me.GroupBoxAsnef.Text = "ASNEF"
        '
        'CheckBoxAsnefBaixa
        '
        Me.CheckBoxAsnefBaixa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAsnefBaixa.AutoSize = True
        Me.CheckBoxAsnefBaixa.Enabled = False
        Me.CheckBoxAsnefBaixa.Location = New System.Drawing.Point(6, 66)
        Me.CheckBoxAsnefBaixa.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxAsnefBaixa.Name = "CheckBoxAsnefBaixa"
        Me.CheckBoxAsnefBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxAsnefBaixa.TabIndex = 31
        Me.CheckBoxAsnefBaixa.TabStop = False
        Me.CheckBoxAsnefBaixa.Text = "baixa"
        '
        'CheckBoxAsnefAlta
        '
        Me.CheckBoxAsnefAlta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAsnefAlta.AutoSize = True
        Me.CheckBoxAsnefAlta.Location = New System.Drawing.Point(6, 41)
        Me.CheckBoxAsnefAlta.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxAsnefAlta.Name = "CheckBoxAsnefAlta"
        Me.CheckBoxAsnefAlta.Size = New System.Drawing.Size(43, 17)
        Me.CheckBoxAsnefAlta.TabIndex = 30
        Me.CheckBoxAsnefAlta.TabStop = False
        Me.CheckBoxAsnefAlta.Text = "alta"
        '
        'DateTimePickerAsnefBaixa
        '
        Me.DateTimePickerAsnefBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAsnefBaixa.Location = New System.Drawing.Point(60, 65)
        Me.DateTimePickerAsnefBaixa.Name = "DateTimePickerAsnefBaixa"
        Me.DateTimePickerAsnefBaixa.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePickerAsnefBaixa.TabIndex = 29
        Me.DateTimePickerAsnefBaixa.Visible = False
        '
        'DateTimePickerAsnefAlta
        '
        Me.DateTimePickerAsnefAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAsnefAlta.Location = New System.Drawing.Point(60, 39)
        Me.DateTimePickerAsnefAlta.Name = "DateTimePickerAsnefAlta"
        Me.DateTimePickerAsnefAlta.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePickerAsnefAlta.TabIndex = 28
        Me.DateTimePickerAsnefAlta.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 267)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "observacions:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(3, 283)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(397, 89)
        Me.TextBoxObs.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 130)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "pendent"
        '
        'Xl_AmtPendent
        '
        Me.Xl_AmtPendent.Amt = Nothing
        Me.Xl_AmtPendent.Location = New System.Drawing.Point(104, 130)
        Me.Xl_AmtPendent.Name = "Xl_AmtPendent"
        Me.Xl_AmtPendent.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmtPendent.TabIndex = 11
        Me.Xl_AmtPendent.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(23, 110)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "a compte"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Deute"
        '
        'Xl_AmtDeute
        '
        Me.Xl_AmtDeute.Amt = Nothing
        Me.Xl_AmtDeute.Location = New System.Drawing.Point(104, 90)
        Me.Xl_AmtDeute.Name = "Xl_AmtDeute"
        Me.Xl_AmtDeute.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmtDeute.TabIndex = 7
        Me.Xl_AmtDeute.TabStop = False
        '
        'Xl_AmtDespeses
        '
        Me.Xl_AmtDespeses.Amt = Nothing
        Me.Xl_AmtDespeses.Location = New System.Drawing.Point(104, 70)
        Me.Xl_AmtDespeses.Name = "Xl_AmtDespeses"
        Me.Xl_AmtDespeses.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmtDespeses.TabIndex = 5
        '
        'Xl_AmtPagatACompte
        '
        Me.Xl_AmtPagatACompte.Amt = Nothing
        Me.Xl_AmtPagatACompte.Location = New System.Drawing.Point(104, 110)
        Me.Xl_AmtPagatACompte.Name = "Xl_AmtPagatACompte"
        Me.Xl_AmtPagatACompte.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmtPagatACompte.TabIndex = 9
        '
        'Xl_AmtNominal
        '
        Me.Xl_AmtNominal.Amt = Nothing
        Me.Xl_AmtNominal.Location = New System.Drawing.Point(104, 50)
        Me.Xl_AmtNominal.Name = "Xl_AmtNominal"
        Me.Xl_AmtNominal.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmtNominal.TabIndex = 3
        Me.Xl_AmtNominal.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Nominal"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Iban1)
        Me.TabPage2.Controls.Add(Me.PictureBoxBancLogo)
        Me.TabPage2.Controls.Add(Me.TextBoxCsa)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.TextBoxTxt)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.TextBoxEur)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.TextBoxVto)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(416, 375)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "EFECTE"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Location = New System.Drawing.Point(140, 225)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 22
        '
        'PictureBoxBancLogo
        '
        Me.PictureBoxBancLogo.Location = New System.Drawing.Point(239, 24)
        Me.PictureBoxBancLogo.Name = "PictureBoxBancLogo"
        Me.PictureBoxBancLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxBancLogo.TabIndex = 21
        Me.PictureBoxBancLogo.TabStop = False
        '
        'TextBoxCsa
        '
        Me.TextBoxCsa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCsa.Location = New System.Drawing.Point(42, 90)
        Me.TextBoxCsa.Name = "TextBoxCsa"
        Me.TextBoxCsa.ReadOnly = True
        Me.TextBoxCsa.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxCsa.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "domicliliació:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(51, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "concepte:"
        '
        'TextBoxTxt
        '
        Me.TextBoxTxt.Location = New System.Drawing.Point(140, 198)
        Me.TextBoxTxt.Name = "TextBoxTxt"
        Me.TextBoxTxt.ReadOnly = True
        Me.TextBoxTxt.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxTxt.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(51, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "import:"
        '
        'TextBoxEur
        '
        Me.TextBoxEur.Location = New System.Drawing.Point(140, 171)
        Me.TextBoxEur.Name = "TextBoxEur"
        Me.TextBoxEur.ReadOnly = True
        Me.TextBoxEur.Size = New System.Drawing.Size(90, 20)
        Me.TextBoxEur.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(50, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "venciment:"
        '
        'TextBoxVto
        '
        Me.TextBoxVto.Location = New System.Drawing.Point(139, 144)
        Me.TextBoxVto.Name = "TextBoxVto"
        Me.TextBoxVto.ReadOnly = True
        Me.TextBoxVto.Size = New System.Drawing.Size(91, 20)
        Me.TextBoxVto.TabIndex = 13
        '
        'Frm_Impagat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 475)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TextBoxCliNom)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Impagat"
        Me.Text = "IMPAGAT"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageImpago.ResumeLayout(False)
        Me.TabPageImpago.PerformLayout()
        Me.GroupBoxAsnef.ResumeLayout(False)
        Me.GroupBoxAsnef.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRefBanc As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerAFP As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerSaldo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxAFP As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSaldat As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageImpago As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEur As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxVto As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxBancLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxCsa As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AmtPagatACompte As Xl_Amount
    Friend WithEvents Xl_AmtNominal As Xl_Amount
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDeute As Xl_Amount
    Friend WithEvents Xl_AmtDespeses As Xl_Amount
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtPendent As Xl_Amount
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxAsnef As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxAsnefBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxAsnefAlta As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerAsnefBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerAsnefAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_Iban1 As Winforms.Xl_Iban
    Friend WithEvents Xl_LookupCcaIncobrable As Xl_LookupCca
    Friend WithEvents CheckBoxIncobrable As CheckBox
End Class
