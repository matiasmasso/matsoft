<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Importacio
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.TabPageDocs = New System.Windows.Forms.TabPage()
        Me.Xl_ImportacioDocs1 = New Mat.NET.Xl_ImportacioDocs()
        Me.TabPageItems = New System.Windows.Forms.TabPage()
        Me.Xl_ImportacioArts1 = New Mat.NET.Xl_ImportacioArts()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_Ship1 = New Mat.NET.Xl_Lookup_Ship()
        Me.Xl_PaisOrigen = New Mat.NET.Xl_Pais()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxIncoterms = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ButtonSearchCodiMercancia = New System.Windows.Forms.Button()
        Me.TextBoxDscMercancia = New System.Windows.Forms.TextBox()
        Me.TextBoxCodiMercancia = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxIntrastat = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxAmt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBoxLogoTrp = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_ContactTrp = New Mat.NET.Xl_Contact()
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.TextBoxKg = New System.Windows.Forms.TextBox()
        Me.TextBoxBultos = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_ContactPrv = New Mat.NET.Xl_Contact()
        Me.TextBoxYea = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBoxLogoPrv = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonMailMgz = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabPageDocs.SuspendLayout()
        Me.TabPageItems.SuspendLayout()
        CType(Me.PictureBoxLogoTrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogoPrv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 576)
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
        Me.ButtonDel.TabIndex = 10
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
        Me.ButtonCancel.TabIndex = 9
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
        Me.ButtonOk.TabIndex = 8
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageDocs)
        Me.TabControl1.Controls.Add(Me.TabPageItems)
        Me.TabControl1.Location = New System.Drawing.Point(0, 46)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(433, 528)
        Me.TabControl1.TabIndex = 43
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Label14)
        Me.TabPageGral.Controls.Add(Me.Xl_Lookup_Ship1)
        Me.TabPageGral.Controls.Add(Me.Xl_PaisOrigen)
        Me.TabPageGral.Controls.Add(Me.Label13)
        Me.TabPageGral.Controls.Add(Me.ComboBoxIncoterms)
        Me.TabPageGral.Controls.Add(Me.Label12)
        Me.TabPageGral.Controls.Add(Me.ButtonSearchCodiMercancia)
        Me.TabPageGral.Controls.Add(Me.TextBoxDscMercancia)
        Me.TabPageGral.Controls.Add(Me.TextBoxCodiMercancia)
        Me.TabPageGral.Controls.Add(Me.Label11)
        Me.TabPageGral.Controls.Add(Me.TextBoxIntrastat)
        Me.TabPageGral.Controls.Add(Me.Label10)
        Me.TabPageGral.Controls.Add(Me.TextBoxAmt)
        Me.TabPageGral.Controls.Add(Me.Label9)
        Me.TabPageGral.Controls.Add(Me.PictureBoxLogoTrp)
        Me.TabPageGral.Controls.Add(Me.Label8)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactTrp)
        Me.TabPageGral.Controls.Add(Me.TextBoxM3)
        Me.TabPageGral.Controls.Add(Me.TextBoxKg)
        Me.TabPageGral.Controls.Add(Me.TextBoxBultos)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.TextBoxObs)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.DateTimePicker1)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.Xl_ContactPrv)
        Me.TabPageGral.Controls.Add(Me.TextBoxYea)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(425, 502)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "General"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'TabPageDocs
        '
        Me.TabPageDocs.Controls.Add(Me.Xl_ImportacioDocs1)
        Me.TabPageDocs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDocs.Name = "TabPageDocs"
        Me.TabPageDocs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDocs.Size = New System.Drawing.Size(425, 482)
        Me.TabPageDocs.TabIndex = 1
        Me.TabPageDocs.Text = "Documentació"
        Me.TabPageDocs.UseVisualStyleBackColor = True
        '
        'Xl_ImportacioDocs1
        '
        Me.Xl_ImportacioDocs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportacioDocs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ImportacioDocs1.Name = "Xl_ImportacioDocs1"
        Me.Xl_ImportacioDocs1.Size = New System.Drawing.Size(419, 476)
        Me.Xl_ImportacioDocs1.TabIndex = 3
        '
        'TabPageItems
        '
        Me.TabPageItems.Controls.Add(Me.Xl_ImportacioArts1)
        Me.TabPageItems.Location = New System.Drawing.Point(4, 22)
        Me.TabPageItems.Name = "TabPageItems"
        Me.TabPageItems.Size = New System.Drawing.Size(425, 482)
        Me.TabPageItems.TabIndex = 2
        Me.TabPageItems.Text = "Productes"
        Me.TabPageItems.UseVisualStyleBackColor = True
        '
        'Xl_ImportacioArts1
        '
        Me.Xl_ImportacioArts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImportacioArts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ImportacioArts1.Name = "Xl_ImportacioArts1"
        Me.Xl_ImportacioArts1.Size = New System.Drawing.Size(425, 482)
        Me.Xl_ImportacioArts1.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 175)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(36, 13)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "vaixell"
        '
        'Xl_Lookup_Ship1
        '
        Me.Xl_Lookup_Ship1.IsDirty = False
        Me.Xl_Lookup_Ship1.Location = New System.Drawing.Point(80, 171)
        Me.Xl_Lookup_Ship1.Name = "Xl_Lookup_Ship1"
        Me.Xl_Lookup_Ship1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Ship1.Ship = Nothing
        Me.Xl_Lookup_Ship1.Size = New System.Drawing.Size(311, 20)
        Me.Xl_Lookup_Ship1.TabIndex = 91
        Me.Xl_Lookup_Ship1.Value = Nothing
        '
        'Xl_PaisOrigen
        '
        Me.Xl_PaisOrigen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_PaisOrigen.Country = Nothing
        Me.Xl_PaisOrigen.FlagVisible = False
        Me.Xl_PaisOrigen.Location = New System.Drawing.Point(330, 203)
        Me.Xl_PaisOrigen.Name = "Xl_PaisOrigen"
        Me.Xl_PaisOrigen.Size = New System.Drawing.Size(60, 15)
        Me.Xl_PaisOrigen.TabIndex = 90
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(238, 203)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 89
        Me.Label13.Text = "pais de origen"
        '
        'ComboBoxIncoterms
        '
        Me.ComboBoxIncoterms.FormattingEnabled = True
        Me.ComboBoxIncoterms.Location = New System.Drawing.Point(80, 200)
        Me.ComboBoxIncoterms.Name = "ComboBoxIncoterms"
        Me.ComboBoxIncoterms.Size = New System.Drawing.Size(60, 21)
        Me.ComboBoxIncoterms.TabIndex = 88
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 203)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 87
        Me.Label12.Text = "incoterms"
        '
        'ButtonSearchCodiMercancia
        '
        Me.ButtonSearchCodiMercancia.Location = New System.Drawing.Point(140, 227)
        Me.ButtonSearchCodiMercancia.Name = "ButtonSearchCodiMercancia"
        Me.ButtonSearchCodiMercancia.Size = New System.Drawing.Size(31, 20)
        Me.ButtonSearchCodiMercancia.TabIndex = 86
        Me.ButtonSearchCodiMercancia.Text = "..."
        Me.ButtonSearchCodiMercancia.UseVisualStyleBackColor = True
        '
        'TextBoxDscMercancia
        '
        Me.TextBoxDscMercancia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscMercancia.Enabled = False
        Me.TextBoxDscMercancia.Location = New System.Drawing.Point(177, 227)
        Me.TextBoxDscMercancia.Name = "TextBoxDscMercancia"
        Me.TextBoxDscMercancia.ReadOnly = True
        Me.TextBoxDscMercancia.Size = New System.Drawing.Size(213, 20)
        Me.TextBoxDscMercancia.TabIndex = 85
        Me.TextBoxDscMercancia.TabStop = False
        Me.TextBoxDscMercancia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCodiMercancia
        '
        Me.TextBoxCodiMercancia.Location = New System.Drawing.Point(80, 227)
        Me.TextBoxCodiMercancia.Name = "TextBoxCodiMercancia"
        Me.TextBoxCodiMercancia.Size = New System.Drawing.Size(60, 20)
        Me.TextBoxCodiMercancia.TabIndex = 83
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 230)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 84
        Me.Label11.Text = "mercancia"
        '
        'TextBoxIntrastat
        '
        Me.TextBoxIntrastat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxIntrastat.Enabled = False
        Me.TextBoxIntrastat.Location = New System.Drawing.Point(80, 281)
        Me.TextBoxIntrastat.Name = "TextBoxIntrastat"
        Me.TextBoxIntrastat.ReadOnly = True
        Me.TextBoxIntrastat.Size = New System.Drawing.Size(104, 20)
        Me.TextBoxIntrastat.TabIndex = 82
        Me.TextBoxIntrastat.TabStop = False
        Me.TextBoxIntrastat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 284)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 81
        Me.Label10.Text = "Intrastat"
        '
        'TextBoxAmt
        '
        Me.TextBoxAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAmt.Enabled = False
        Me.TextBoxAmt.Location = New System.Drawing.Point(80, 255)
        Me.TextBoxAmt.Name = "TextBoxAmt"
        Me.TextBoxAmt.ReadOnly = True
        Me.TextBoxAmt.Size = New System.Drawing.Size(104, 20)
        Me.TextBoxAmt.TabIndex = 79
        Me.TextBoxAmt.TabStop = False
        Me.TextBoxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 258)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 80
        Me.Label9.Text = "import"
        '
        'PictureBoxLogoTrp
        '
        Me.PictureBoxLogoTrp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogoTrp.Location = New System.Drawing.Point(241, 95)
        Me.PictureBoxLogoTrp.Name = "PictureBoxLogoTrp"
        Me.PictureBoxLogoTrp.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogoTrp.TabIndex = 78
        Me.PictureBoxLogoTrp.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 77
        Me.Label8.Text = "transportista"
        '
        'Xl_ContactTrp
        '
        Me.Xl_ContactTrp.Contact = Nothing
        Me.Xl_ContactTrp.Location = New System.Drawing.Point(80, 64)
        Me.Xl_ContactTrp.Name = "Xl_ContactTrp"
        Me.Xl_ContactTrp.ReadOnly = False
        Me.Xl_ContactTrp.Size = New System.Drawing.Size(310, 20)
        Me.Xl_ContactTrp.TabIndex = 69
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(80, 144)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxM3.TabIndex = 74
        Me.TextBoxM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxKg
        '
        Me.TextBoxKg.Location = New System.Drawing.Point(80, 118)
        Me.TextBoxKg.Name = "TextBoxKg"
        Me.TextBoxKg.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxKg.TabIndex = 72
        Me.TextBoxKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxBultos
        '
        Me.TextBoxBultos.Location = New System.Drawing.Point(80, 92)
        Me.TextBoxBultos.Name = "TextBoxBultos"
        Me.TextBoxBultos.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxBultos.TabIndex = 71
        Me.TextBoxBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 95)
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
        Me.TextBoxObs.Location = New System.Drawing.Point(12, 326)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(409, 173)
        Me.TextBoxObs.TabIndex = 75
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 310)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 13)
        Me.Label3.TabIndex = 73
        Me.Label3.Text = "Observacions (transportista, etc):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 13)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Data prevista de arribada:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(135, 6)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePicker1.TabIndex = 65
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "proveidor"
        '
        'Xl_ContactPrv
        '
        Me.Xl_ContactPrv.Contact = Nothing
        Me.Xl_ContactPrv.Location = New System.Drawing.Point(80, 38)
        Me.Xl_ContactPrv.Name = "Xl_ContactPrv"
        Me.Xl_ContactPrv.ReadOnly = False
        Me.Xl_ContactPrv.Size = New System.Drawing.Size(310, 20)
        Me.Xl_ContactPrv.TabIndex = 67
        '
        'TextBoxYea
        '
        Me.TextBoxYea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxYea.Location = New System.Drawing.Point(350, 6)
        Me.TextBoxYea.Name = "TextBoxYea"
        Me.TextBoxYea.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxYea.TabIndex = 66
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(319, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "any:"
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
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonMailMgz, Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(433, 25)
        Me.ToolStrip1.TabIndex = 46
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonMailMgz
        '
        Me.ToolStripButtonMailMgz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonMailMgz.Image = Global.Mat.NET.My.Resources.Resources.MailSobreGroc
        Me.ToolStripButtonMailMgz.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonMailMgz.Name = "ToolStripButtonMailMgz"
        Me.ToolStripButtonMailMgz.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonMailMgz.Text = "ToolStripButton1"
        Me.ToolStripButtonMailMgz.ToolTipText = "enviar avis d'arribada a magatzem"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        '
        'Frm_Importacio2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 607)
        Me.Controls.Add(Me.PictureBoxLogoPrv)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Importacio"
        Me.Text = "Importacio"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.TabPageDocs.ResumeLayout(False)
        Me.TabPageItems.ResumeLayout(False)
        CType(Me.PictureBoxLogoTrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogoPrv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TabPageDocs As System.Windows.Forms.TabPage
    Friend WithEvents TabPageItems As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ImportacioArts1 As Mat.NET.Xl_ImportacioArts
    Friend WithEvents Xl_ImportacioDocs1 As Mat.NET.Xl_ImportacioDocs
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_Ship1 As Mat.NET.Xl_Lookup_Ship
    Friend WithEvents Xl_PaisOrigen As Mat.NET.Xl_Pais
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxIncoterms As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ButtonSearchCodiMercancia As System.Windows.Forms.Button
    Friend WithEvents TextBoxDscMercancia As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCodiMercancia As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxIntrastat As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxLogoTrp As System.Windows.Forms.PictureBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactTrp As Mat.NET.Xl_Contact
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxKg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxBultos As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactPrv As Mat.NET.Xl_Contact
    Friend WithEvents TextBoxYea As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxLogoPrv As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonMailMgz As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
End Class
