Public Partial Class Frm_Spv
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Spv))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Xl_Product1 = New Mat.Net.Xl_Product()
        Me.LabelEmailTo = New System.Windows.Forms.Label()
        Me.ComboBoxEmailLabelTo = New System.Windows.Forms.ComboBox()
        Me.TextBoxFchRead = New System.Windows.Forms.TextBox()
        Me.CheckBoxRead = New System.Windows.Forms.CheckBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Xl_Adr1 = New Mat.Net.Xl_Adr()
        Me.CheckBoxAdr = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBoxPressupost = New System.Windows.Forms.GroupBox()
        Me.Xl_AmtSpvEmbalatje = New Mat.Net.Xl_Amount()
        Me.Xl_AmtSpvTransport = New Mat.Net.Xl_Amount()
        Me.Xl_AmtSpvMaterial = New Mat.Net.Xl_Amount()
        Me.Xl_AmtSpvJob = New Mat.Net.Xl_Amount()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBoxCliObs = New System.Windows.Forms.TextBox()
        Me.TextBoxObsOutOfSpvOut = New System.Windows.Forms.TextBox()
        Me.TextBoxObsOutOfSpvIn = New System.Windows.Forms.TextBox()
        Me.CheckBoxOutSpvOut = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOutSpvIn = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxSolicitaGarantia = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxsRef = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxContacto = New System.Windows.Forms.TextBox()
        Me.TextBoxReg = New System.Windows.Forms.TextBox()
        Me.Xl_Contact1 = New Mat.Net.Xl_Contact()
        Me.TabPageSpvIn = New System.Windows.Forms.TabPage()
        Me.ButtonNoSpvIn = New System.Windows.Forms.Button()
        Me.TextBoxOutSpvIn = New System.Windows.Forms.TextBox()
        Me.PanelSpvIn = New System.Windows.Forms.Panel()
        Me.DataGridViewSpvInSpvs = New System.Windows.Forms.DataGridView()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxExp = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxKg = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxBultos = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerSpvIn = New System.Windows.Forms.DateTimePicker()
        Me.TabPageSpvOut = New System.Windows.Forms.TabPage()
        Me.PanelSpvOut = New System.Windows.Forms.Panel()
        Me.CheckBoxFacturable = New System.Windows.Forms.CheckBox()
        Me.Xl_AmtTransport = New Mat.Net.Xl_Amount()
        Me.Xl_AmtEmbalatje = New Mat.Net.Xl_Amount()
        Me.Xl_AmtMaterial = New Mat.Net.Xl_Amount()
        Me.Xl_AmtJob = New Mat.Net.Xl_Amount()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBoxGarantiaConfirmada = New System.Windows.Forms.CheckBox()
        Me.TextBoxObsTecnic = New System.Windows.Forms.TextBox()
        Me.ButtonShowAlb = New System.Windows.Forms.Button()
        Me.TextBoxAlbNum = New System.Windows.Forms.TextBox()
        Me.TextBoxOutSpvOut = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxPressupost.SuspendLayout()
        Me.TabPageSpvIn.SuspendLayout()
        Me.PanelSpvIn.SuspendLayout()
        CType(Me.DataGridViewSpvInSpvs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageSpvOut.SuspendLayout()
        Me.PanelSpvOut.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageSpvIn)
        Me.TabControl1.Controls.Add(Me.TabPageSpvOut)
        Me.TabControl1.Location = New System.Drawing.Point(15, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(417, 508)
        Me.TabControl1.TabIndex = 4
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Xl_Product1)
        Me.TabPageGral.Controls.Add(Me.LabelEmailTo)
        Me.TabPageGral.Controls.Add(Me.ComboBoxEmailLabelTo)
        Me.TabPageGral.Controls.Add(Me.TextBoxFchRead)
        Me.TabPageGral.Controls.Add(Me.CheckBoxRead)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom)
        Me.TabPageGral.Controls.Add(Me.PictureBox1)
        Me.TabPageGral.Controls.Add(Me.Xl_Adr1)
        Me.TabPageGral.Controls.Add(Me.CheckBoxAdr)
        Me.TabPageGral.Controls.Add(Me.GroupBox1)
        Me.TabPageGral.Controls.Add(Me.GroupBoxPressupost)
        Me.TabPageGral.Controls.Add(Me.TextBoxCliObs)
        Me.TabPageGral.Controls.Add(Me.TextBoxObsOutOfSpvOut)
        Me.TabPageGral.Controls.Add(Me.TextBoxObsOutOfSpvIn)
        Me.TabPageGral.Controls.Add(Me.CheckBoxOutSpvOut)
        Me.TabPageGral.Controls.Add(Me.CheckBoxOutSpvIn)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.CheckBoxSolicitaGarantia)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.TextBoxsRef)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.TextBoxContacto)
        Me.TabPageGral.Controls.Add(Me.TextBoxReg)
        Me.TabPageGral.Controls.Add(Me.Xl_Contact1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(409, 482)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "REGISTRE"
        '
        'Xl_Product1
        '
        Me.Xl_Product1.IsDirty = False
        Me.Xl_Product1.Location = New System.Drawing.Point(94, 173)
        Me.Xl_Product1.Name = "Xl_Product1"
        Me.Xl_Product1.Product = Nothing
        Me.Xl_Product1.Size = New System.Drawing.Size(300, 20)
        Me.Xl_Product1.TabIndex = 52
        Me.Xl_Product1.Value = Nothing
        '
        'LabelEmailTo
        '
        Me.LabelEmailTo.AutoSize = True
        Me.LabelEmailTo.Location = New System.Drawing.Point(6, 427)
        Me.LabelEmailTo.Name = "LabelEmailTo"
        Me.LabelEmailTo.Size = New System.Drawing.Size(89, 13)
        Me.LabelEmailTo.TabIndex = 51
        Me.LabelEmailTo.Text = "enviar etiqueta a:"
        '
        'ComboBoxEmailLabelTo
        '
        Me.ComboBoxEmailLabelTo.FormattingEnabled = True
        Me.ComboBoxEmailLabelTo.Location = New System.Drawing.Point(94, 424)
        Me.ComboBoxEmailLabelTo.Name = "ComboBoxEmailLabelTo"
        Me.ComboBoxEmailLabelTo.Size = New System.Drawing.Size(300, 21)
        Me.ComboBoxEmailLabelTo.TabIndex = 50
        '
        'TextBoxFchRead
        '
        Me.TextBoxFchRead.Location = New System.Drawing.Point(175, 341)
        Me.TextBoxFchRead.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.TextBoxFchRead.Name = "TextBoxFchRead"
        Me.TextBoxFchRead.ReadOnly = True
        Me.TextBoxFchRead.Size = New System.Drawing.Size(219, 20)
        Me.TextBoxFchRead.TabIndex = 48
        '
        'CheckBoxRead
        '
        Me.CheckBoxRead.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxRead.AutoSize = True
        Me.CheckBoxRead.Location = New System.Drawing.Point(9, 343)
        Me.CheckBoxRead.Name = "CheckBoxRead"
        Me.CheckBoxRead.Size = New System.Drawing.Size(47, 17)
        Me.CheckBoxRead.TabIndex = 47
        Me.CheckBoxRead.Text = "llegit"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Enabled = False
        Me.TextBoxNom.Location = New System.Drawing.Point(14, 54)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(280, 20)
        Me.TextBoxNom.TabIndex = 45
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(309, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(94, 120)
        Me.PictureBox1.TabIndex = 42
        Me.PictureBox1.TabStop = False
        '
        'Xl_Adr1
        '
        Me.Xl_Adr1.Enabled = False
        Me.Xl_Adr1.Location = New System.Drawing.Point(14, 74)
        Me.Xl_Adr1.Name = "Xl_Adr1"
        Me.Xl_Adr1.Size = New System.Drawing.Size(280, 40)
        Me.Xl_Adr1.TabIndex = 44
        '
        'CheckBoxAdr
        '
        Me.CheckBoxAdr.AutoSize = True
        Me.CheckBoxAdr.Location = New System.Drawing.Point(15, 33)
        Me.CheckBoxAdr.Name = "CheckBoxAdr"
        Me.CheckBoxAdr.Size = New System.Drawing.Size(161, 17)
        Me.CheckBoxAdr.TabIndex = 43
        Me.CheckBoxAdr.Text = "enviar al següent destinatari:"
        Me.CheckBoxAdr.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(7, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(296, 95)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        '
        'GroupBoxPressupost
        '
        Me.GroupBoxPressupost.Controls.Add(Me.Xl_AmtSpvEmbalatje)
        Me.GroupBoxPressupost.Controls.Add(Me.Xl_AmtSpvTransport)
        Me.GroupBoxPressupost.Controls.Add(Me.Xl_AmtSpvMaterial)
        Me.GroupBoxPressupost.Controls.Add(Me.Xl_AmtSpvJob)
        Me.GroupBoxPressupost.Controls.Add(Me.Label13)
        Me.GroupBoxPressupost.Controls.Add(Me.Label14)
        Me.GroupBoxPressupost.Controls.Add(Me.Label15)
        Me.GroupBoxPressupost.Controls.Add(Me.Label16)
        Me.GroupBoxPressupost.Location = New System.Drawing.Point(15, 273)
        Me.GroupBoxPressupost.Name = "GroupBoxPressupost"
        Me.GroupBoxPressupost.Size = New System.Drawing.Size(380, 52)
        Me.GroupBoxPressupost.TabIndex = 23
        Me.GroupBoxPressupost.TabStop = False
        Me.GroupBoxPressupost.Text = "Pressupost"
        '
        'Xl_AmtSpvEmbalatje
        '
        Me.Xl_AmtSpvEmbalatje.Amt = Nothing
        Me.Xl_AmtSpvEmbalatje.Location = New System.Drawing.Point(269, 30)
        Me.Xl_AmtSpvEmbalatje.Margin = New System.Windows.Forms.Padding(2, 0, 3, 3)
        Me.Xl_AmtSpvEmbalatje.Name = "Xl_AmtSpvEmbalatje"
        Me.Xl_AmtSpvEmbalatje.Size = New System.Drawing.Size(50, 20)
        Me.Xl_AmtSpvEmbalatje.TabIndex = 29
        '
        'Xl_AmtSpvTransport
        '
        Me.Xl_AmtSpvTransport.Amt = Nothing
        Me.Xl_AmtSpvTransport.Location = New System.Drawing.Point(324, 30)
        Me.Xl_AmtSpvTransport.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_AmtSpvTransport.Name = "Xl_AmtSpvTransport"
        Me.Xl_AmtSpvTransport.Size = New System.Drawing.Size(50, 20)
        Me.Xl_AmtSpvTransport.TabIndex = 30
        '
        'Xl_AmtSpvMaterial
        '
        Me.Xl_AmtSpvMaterial.Amt = Nothing
        Me.Xl_AmtSpvMaterial.Location = New System.Drawing.Point(214, 30)
        Me.Xl_AmtSpvMaterial.Margin = New System.Windows.Forms.Padding(1, 0, 3, 1)
        Me.Xl_AmtSpvMaterial.Name = "Xl_AmtSpvMaterial"
        Me.Xl_AmtSpvMaterial.Size = New System.Drawing.Size(50, 20)
        Me.Xl_AmtSpvMaterial.TabIndex = 28
        '
        'Xl_AmtSpvJob
        '
        Me.Xl_AmtSpvJob.Amt = Nothing
        Me.Xl_AmtSpvJob.Location = New System.Drawing.Point(159, 30)
        Me.Xl_AmtSpvJob.Margin = New System.Windows.Forms.Padding(3, 3, 1, 1)
        Me.Xl_AmtSpvJob.Name = "Xl_AmtSpvJob"
        Me.Xl_AmtSpvJob.Size = New System.Drawing.Size(50, 20)
        Me.Xl_AmtSpvJob.TabIndex = 27
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(322, 12)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "transport:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(262, 12)
        Me.Label14.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "embalatje:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(216, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 13)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "material:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(152, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "ma d'obra:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxCliObs
        '
        Me.TextBoxCliObs.Location = New System.Drawing.Point(13, 218)
        Me.TextBoxCliObs.Multiline = True
        Me.TextBoxCliObs.Name = "TextBoxCliObs"
        Me.TextBoxCliObs.Size = New System.Drawing.Size(382, 49)
        Me.TextBoxCliObs.TabIndex = 14
        '
        'TextBoxObsOutOfSpvOut
        '
        Me.TextBoxObsOutOfSpvOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObsOutOfSpvOut.Location = New System.Drawing.Point(175, 390)
        Me.TextBoxObsOutOfSpvOut.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.TextBoxObsOutOfSpvOut.MaxLength = 30
        Me.TextBoxObsOutOfSpvOut.Name = "TextBoxObsOutOfSpvOut"
        Me.TextBoxObsOutOfSpvOut.Size = New System.Drawing.Size(219, 20)
        Me.TextBoxObsOutOfSpvOut.TabIndex = 13
        Me.TextBoxObsOutOfSpvOut.Visible = False
        '
        'TextBoxObsOutOfSpvIn
        '
        Me.TextBoxObsOutOfSpvIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObsOutOfSpvIn.Location = New System.Drawing.Point(175, 366)
        Me.TextBoxObsOutOfSpvIn.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.TextBoxObsOutOfSpvIn.MaxLength = 30
        Me.TextBoxObsOutOfSpvIn.Name = "TextBoxObsOutOfSpvIn"
        Me.TextBoxObsOutOfSpvIn.Size = New System.Drawing.Size(219, 20)
        Me.TextBoxObsOutOfSpvIn.TabIndex = 12
        Me.TextBoxObsOutOfSpvIn.Visible = False
        '
        'CheckBoxOutSpvOut
        '
        Me.CheckBoxOutSpvOut.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxOutSpvOut.AutoSize = True
        Me.CheckBoxOutSpvOut.Location = New System.Drawing.Point(9, 389)
        Me.CheckBoxOutSpvOut.Name = "CheckBoxOutSpvOut"
        Me.CheckBoxOutSpvOut.Size = New System.Drawing.Size(159, 17)
        Me.CheckBoxOutSpvOut.TabIndex = 11
        Me.CheckBoxOutSpvOut.Text = "Retirar de pendents de sortir"
        '
        'CheckBoxOutSpvIn
        '
        Me.CheckBoxOutSpvIn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxOutSpvIn.AutoSize = True
        Me.CheckBoxOutSpvIn.Location = New System.Drawing.Point(9, 366)
        Me.CheckBoxOutSpvIn.Name = "CheckBoxOutSpvIn"
        Me.CheckBoxOutSpvIn.Size = New System.Drawing.Size(166, 17)
        Me.CheckBoxOutSpvIn.TabIndex = 10
        Me.CheckBoxOutSpvIn.Text = "Retirar de pendents d'entrada"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 175)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "producte:"
        '
        'CheckBoxSolicitaGarantia
        '
        Me.CheckBoxSolicitaGarantia.AutoSize = True
        Me.CheckBoxSolicitaGarantia.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxSolicitaGarantia.Location = New System.Drawing.Point(293, 198)
        Me.CheckBoxSolicitaGarantia.Name = "CheckBoxSolicitaGarantia"
        Me.CheckBoxSolicitaGarantia.Size = New System.Drawing.Size(101, 17)
        Me.CheckBoxSolicitaGarantia.TabIndex = 7
        Me.CheckBoxSolicitaGarantia.Text = "Solicita garantia"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 155)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "su ref.:"
        '
        'TextBoxsRef
        '
        Me.TextBoxsRef.Location = New System.Drawing.Point(94, 152)
        Me.TextBoxsRef.Name = "TextBoxsRef"
        Me.TextBoxsRef.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxsRef.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Contacte:"
        '
        'TextBoxContacto
        '
        Me.TextBoxContacto.Location = New System.Drawing.Point(94, 132)
        Me.TextBoxContacto.Name = "TextBoxContacto"
        Me.TextBoxContacto.Size = New System.Drawing.Size(300, 20)
        Me.TextBoxContacto.TabIndex = 3
        '
        'TextBoxReg
        '
        Me.TextBoxReg.Location = New System.Drawing.Point(13, 450)
        Me.TextBoxReg.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.TextBoxReg.Name = "TextBoxReg"
        Me.TextBoxReg.ReadOnly = True
        Me.TextBoxReg.Size = New System.Drawing.Size(381, 20)
        Me.TextBoxReg.TabIndex = 2
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(4, 7)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(290, 20)
        Me.Xl_Contact1.TabIndex = 1
        '
        'TabPageSpvIn
        '
        Me.TabPageSpvIn.Controls.Add(Me.ButtonNoSpvIn)
        Me.TabPageSpvIn.Controls.Add(Me.TextBoxOutSpvIn)
        Me.TabPageSpvIn.Controls.Add(Me.PanelSpvIn)
        Me.TabPageSpvIn.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSpvIn.Name = "TabPageSpvIn"
        Me.TabPageSpvIn.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSpvIn.Size = New System.Drawing.Size(409, 482)
        Me.TabPageSpvIn.TabIndex = 1
        Me.TabPageSpvIn.Text = "ENTRADA"
        '
        'ButtonNoSpvIn
        '
        Me.ButtonNoSpvIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNoSpvIn.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNoSpvIn.Location = New System.Drawing.Point(182, 442)
        Me.ButtonNoSpvIn.Name = "ButtonNoSpvIn"
        Me.ButtonNoSpvIn.Size = New System.Drawing.Size(209, 24)
        Me.ButtonNoSpvIn.TabIndex = 13
        Me.ButtonNoSpvIn.Text = "retirar la reparació d'aquesta entrada"
        Me.ButtonNoSpvIn.UseVisualStyleBackColor = False
        '
        'TextBoxOutSpvIn
        '
        Me.TextBoxOutSpvIn.Location = New System.Drawing.Point(7, 381)
        Me.TextBoxOutSpvIn.Name = "TextBoxOutSpvIn"
        Me.TextBoxOutSpvIn.ReadOnly = True
        Me.TextBoxOutSpvIn.Size = New System.Drawing.Size(400, 20)
        Me.TextBoxOutSpvIn.TabIndex = 1
        '
        'PanelSpvIn
        '
        Me.PanelSpvIn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelSpvIn.Controls.Add(Me.DataGridViewSpvInSpvs)
        Me.PanelSpvIn.Controls.Add(Me.TextBoxObs)
        Me.PanelSpvIn.Controls.Add(Me.Label8)
        Me.PanelSpvIn.Controls.Add(Me.TextBoxExp)
        Me.PanelSpvIn.Controls.Add(Me.Label7)
        Me.PanelSpvIn.Controls.Add(Me.TextBoxKg)
        Me.PanelSpvIn.Controls.Add(Me.Label6)
        Me.PanelSpvIn.Controls.Add(Me.TextBoxBultos)
        Me.PanelSpvIn.Controls.Add(Me.Label5)
        Me.PanelSpvIn.Controls.Add(Me.Label4)
        Me.PanelSpvIn.Controls.Add(Me.DateTimePickerSpvIn)
        Me.PanelSpvIn.Location = New System.Drawing.Point(4, 6)
        Me.PanelSpvIn.Name = "PanelSpvIn"
        Me.PanelSpvIn.Size = New System.Drawing.Size(403, 442)
        Me.PanelSpvIn.TabIndex = 0
        '
        'DataGridViewSpvInSpvs
        '
        Me.DataGridViewSpvInSpvs.AllowUserToAddRows = False
        Me.DataGridViewSpvInSpvs.AllowUserToDeleteRows = False
        Me.DataGridViewSpvInSpvs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSpvInSpvs.Location = New System.Drawing.Point(15, 130)
        Me.DataGridViewSpvInSpvs.Name = "DataGridViewSpvInSpvs"
        Me.DataGridViewSpvInSpvs.ReadOnly = True
        Me.DataGridViewSpvInSpvs.Size = New System.Drawing.Size(372, 231)
        Me.DataGridViewSpvInSpvs.TabIndex = 21
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(15, 91)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxObs.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Observacions:"
        '
        'TextBoxExp
        '
        Me.TextBoxExp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExp.Location = New System.Drawing.Point(116, 36)
        Me.TextBoxExp.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxExp.Name = "TextBoxExp"
        Me.TextBoxExp.Size = New System.Drawing.Size(164, 20)
        Me.TextBoxExp.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(116, 19)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Expedició:"
        '
        'TextBoxKg
        '
        Me.TextBoxKg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxKg.Location = New System.Drawing.Point(342, 37)
        Me.TextBoxKg.Name = "TextBoxKg"
        Me.TextBoxKg.Size = New System.Drawing.Size(45, 20)
        Me.TextBoxKg.TabIndex = 16
        Me.TextBoxKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(366, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Kg:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxBultos
        '
        Me.TextBoxBultos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBultos.Location = New System.Drawing.Point(290, 37)
        Me.TextBoxBultos.Name = "TextBoxBultos"
        Me.TextBoxBultos.Size = New System.Drawing.Size(45, 20)
        Me.TextBoxBultos.TabIndex = 14
        Me.TextBoxBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(297, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "bultos:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 19)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "data:"
        '
        'DateTimePickerSpvIn
        '
        Me.DateTimePickerSpvIn.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerSpvIn.Location = New System.Drawing.Point(15, 36)
        Me.DateTimePickerSpvIn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.DateTimePickerSpvIn.Name = "DateTimePickerSpvIn"
        Me.DateTimePickerSpvIn.Size = New System.Drawing.Size(94, 20)
        Me.DateTimePickerSpvIn.TabIndex = 11
        '
        'TabPageSpvOut
        '
        Me.TabPageSpvOut.Controls.Add(Me.PanelSpvOut)
        Me.TabPageSpvOut.Controls.Add(Me.TextBoxOutSpvOut)
        Me.TabPageSpvOut.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSpvOut.Name = "TabPageSpvOut"
        Me.TabPageSpvOut.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSpvOut.Size = New System.Drawing.Size(409, 482)
        Me.TabPageSpvOut.TabIndex = 2
        Me.TabPageSpvOut.Text = "SORTIDA"
        '
        'PanelSpvOut
        '
        Me.PanelSpvOut.Controls.Add(Me.CheckBoxFacturable)
        Me.PanelSpvOut.Controls.Add(Me.Xl_AmtTransport)
        Me.PanelSpvOut.Controls.Add(Me.Xl_AmtEmbalatje)
        Me.PanelSpvOut.Controls.Add(Me.Xl_AmtMaterial)
        Me.PanelSpvOut.Controls.Add(Me.Xl_AmtJob)
        Me.PanelSpvOut.Controls.Add(Me.Label12)
        Me.PanelSpvOut.Controls.Add(Me.Label11)
        Me.PanelSpvOut.Controls.Add(Me.Label10)
        Me.PanelSpvOut.Controls.Add(Me.Label9)
        Me.PanelSpvOut.Controls.Add(Me.CheckBoxGarantiaConfirmada)
        Me.PanelSpvOut.Controls.Add(Me.TextBoxObsTecnic)
        Me.PanelSpvOut.Controls.Add(Me.ButtonShowAlb)
        Me.PanelSpvOut.Controls.Add(Me.TextBoxAlbNum)
        Me.PanelSpvOut.Location = New System.Drawing.Point(6, 6)
        Me.PanelSpvOut.Name = "PanelSpvOut"
        Me.PanelSpvOut.Size = New System.Drawing.Size(395, 360)
        Me.PanelSpvOut.TabIndex = 3
        '
        'CheckBoxFacturable
        '
        Me.CheckBoxFacturable.AutoSize = True
        Me.CheckBoxFacturable.Location = New System.Drawing.Point(9, 254)
        Me.CheckBoxFacturable.Name = "CheckBoxFacturable"
        Me.CheckBoxFacturable.Size = New System.Drawing.Size(76, 17)
        Me.CheckBoxFacturable.TabIndex = 14
        Me.CheckBoxFacturable.Text = "Facturable"
        '
        'Xl_AmtTransport
        '
        Me.Xl_AmtTransport.Amt = Nothing
        Me.Xl_AmtTransport.Location = New System.Drawing.Point(105, 208)
        Me.Xl_AmtTransport.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Xl_AmtTransport.Name = "Xl_AmtTransport"
        Me.Xl_AmtTransport.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtTransport.TabIndex = 13
        '
        'Xl_AmtEmbalatje
        '
        Me.Xl_AmtEmbalatje.Amt = Nothing
        Me.Xl_AmtEmbalatje.Location = New System.Drawing.Point(105, 187)
        Me.Xl_AmtEmbalatje.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_AmtEmbalatje.Name = "Xl_AmtEmbalatje"
        Me.Xl_AmtEmbalatje.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtEmbalatje.TabIndex = 12
        '
        'Xl_AmtMaterial
        '
        Me.Xl_AmtMaterial.Amt = Nothing
        Me.Xl_AmtMaterial.Location = New System.Drawing.Point(105, 166)
        Me.Xl_AmtMaterial.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_AmtMaterial.Name = "Xl_AmtMaterial"
        Me.Xl_AmtMaterial.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtMaterial.TabIndex = 11
        '
        'Xl_AmtJob
        '
        Me.Xl_AmtJob.Amt = Nothing
        Me.Xl_AmtJob.Location = New System.Drawing.Point(105, 145)
        Me.Xl_AmtJob.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Xl_AmtJob.Name = "Xl_AmtJob"
        Me.Xl_AmtJob.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtJob.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 214)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "transport:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 193)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "embalatje:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 172)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "material:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 151)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "ma d'obra:"
        '
        'CheckBoxGarantiaConfirmada
        '
        Me.CheckBoxGarantiaConfirmada.AutoSize = True
        Me.CheckBoxGarantiaConfirmada.Location = New System.Drawing.Point(6, 32)
        Me.CheckBoxGarantiaConfirmada.Name = "CheckBoxGarantiaConfirmada"
        Me.CheckBoxGarantiaConfirmada.Size = New System.Drawing.Size(177, 17)
        Me.CheckBoxGarantiaConfirmada.TabIndex = 5
        Me.CheckBoxGarantiaConfirmada.Text = "Garantía confirmada per el taller"
        '
        'TextBoxObsTecnic
        '
        Me.TextBoxObsTecnic.Location = New System.Drawing.Point(6, 56)
        Me.TextBoxObsTecnic.Multiline = True
        Me.TextBoxObsTecnic.Name = "TextBoxObsTecnic"
        Me.TextBoxObsTecnic.Size = New System.Drawing.Size(382, 75)
        Me.TextBoxObsTecnic.TabIndex = 4
        '
        'ButtonShowAlb
        '
        Me.ButtonShowAlb.Location = New System.Drawing.Point(350, 5)
        Me.ButtonShowAlb.Name = "ButtonShowAlb"
        Me.ButtonShowAlb.Size = New System.Drawing.Size(37, 19)
        Me.ButtonShowAlb.TabIndex = 3
        Me.ButtonShowAlb.Text = "..."
        '
        'TextBoxAlbNum
        '
        Me.TextBoxAlbNum.Location = New System.Drawing.Point(4, 4)
        Me.TextBoxAlbNum.Name = "TextBoxAlbNum"
        Me.TextBoxAlbNum.ReadOnly = True
        Me.TextBoxAlbNum.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxAlbNum.TabIndex = 2
        '
        'TextBoxOutSpvOut
        '
        Me.TextBoxOutSpvOut.Location = New System.Drawing.Point(3, 378)
        Me.TextBoxOutSpvOut.Name = "TextBoxOutSpvOut"
        Me.TextBoxOutSpvOut.ReadOnly = True
        Me.TextBoxOutSpvOut.Size = New System.Drawing.Size(400, 20)
        Me.TextBoxOutSpvOut.TabIndex = 2
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
        Me.Panel1.Size = New System.Drawing.Size(439, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(220, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(331, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(282, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(150, 48)
        Me.PictureBox2.TabIndex = 43
        Me.PictureBox2.TabStop = False
        '
        'Frm_Spv
        '
        Me.ClientSize = New System.Drawing.Size(439, 584)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Spv"
        Me.Text = "FULL DE REPARACIÓ"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxPressupost.ResumeLayout(False)
        Me.GroupBoxPressupost.PerformLayout()
        Me.TabPageSpvIn.ResumeLayout(False)
        Me.TabPageSpvIn.PerformLayout()
        Me.PanelSpvIn.ResumeLayout(False)
        Me.PanelSpvIn.PerformLayout()
        CType(Me.DataGridViewSpvInSpvs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageSpvOut.ResumeLayout(False)
        Me.TabPageSpvOut.PerformLayout()
        Me.PanelSpvOut.ResumeLayout(False)
        Me.PanelSpvOut.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents TabPageSpvIn As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxReg As System.Windows.Forms.TextBox
    Friend WithEvents TabPageSpvOut As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxContacto As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxsRef As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxSolicitaGarantia As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PanelSpvIn As System.Windows.Forms.Panel
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExp As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxKg As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBultos As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerSpvIn As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxOutSpvIn As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxOutSpvOut As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxOutSpvIn As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxOutSpvOut As System.Windows.Forms.TextBox
    Friend WithEvents PanelSpvOut As System.Windows.Forms.Panel
    Friend WithEvents ButtonShowAlb As System.Windows.Forms.Button
    Friend WithEvents TextBoxAlbNum As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxObsOutOfSpvIn As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxObsOutOfSpvOut As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxObsTecnic As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxGarantiaConfirmada As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtTransport As Xl_Amount
    Friend WithEvents Xl_AmtEmbalatje As Xl_Amount
    Friend WithEvents Xl_AmtMaterial As Xl_Amount
    Friend WithEvents Xl_AmtJob As Xl_Amount
    Friend WithEvents TextBoxCliObs As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxFacturable As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxPressupost As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_AmtSpvEmbalatje As Xl_Amount
    Friend WithEvents Xl_AmtSpvTransport As Xl_Amount
    Friend WithEvents Xl_AmtSpvMaterial As Xl_Amount
    Friend WithEvents Xl_AmtSpvJob As Xl_Amount
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewSpvInSpvs As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Adr1 As Xl_Adr
    Friend WithEvents CheckBoxAdr As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxFchRead As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxRead As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonNoSpvIn As System.Windows.Forms.Button
    Friend WithEvents LabelEmailTo As System.Windows.Forms.Label
    Friend WithEvents ComboBoxEmailLabelTo As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Product1 As Xl_Product
End Class
