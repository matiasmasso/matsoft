<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AlbNew2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_AlbNew2))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonObs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabelKg = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonTransmisio = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonCustomDoc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFra = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSplitButtonCredit = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripMenuItemCredit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemTransfer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemReembols = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButtonFpg = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonExemptIva = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonDiposit = New System.Windows.Forms.ToolStripButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxCEE = New System.Windows.Forms.CheckBox()
        Me.CheckBoxExport = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupZip1 = New Winforms.Xl_LookupZip()
        Me.Xl_Contact2Platform = New Winforms.Xl_Contact2()
        Me.CheckBoxPlatform = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_ContactMgz = New Winforms.Xl_Contact2()
        Me.LabelEmail = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LabelLastAlb = New System.Windows.Forms.Label()
        Me.ComboBoxNum = New System.Windows.Forms.ComboBox()
        Me.CheckBoxRecycle = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFacturable = New System.Windows.Forms.CheckBox()
        Me.CheckBoxValorat = New System.Windows.Forms.CheckBox()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.ComboBoxPorts = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.PanelHost = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_DeliveryItems1 = New Winforms.Xl_DeliveryItems()
        Me.Xl_UsrLog1 = New Winforms.Xl_UsrLog()
        Me.TextBoxTotals = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_DocFileEtiquetesTransport = New Winforms.Xl_DocFile_Old()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_Tree_DeliveryPackages1 = New Winforms.Xl_Tree_DeliveryPackages()
        Me.StatusStripObs = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelObs = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelCustDoc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Xl_LookupIncoterm1 = New Winforms.Xl_LookupIncoterm()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelHost.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_DeliveryItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.StatusStripObs.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 584)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(931, 31)
        Me.Panel1.TabIndex = 101
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(714, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 102
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOk.ImageList = Me.ImageList1
        Me.ButtonOk.Location = New System.Drawing.Point(824, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 101
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 103
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MenuStrip1)
        Me.SplitContainer1.Panel1MinSize = 155
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxCEE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_LookupZip1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Contact2Platform)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxPlatform)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ContactMgz)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelEmail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelLastAlb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBoxNum)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxRecycle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxFacturable)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxValorat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxTel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxAdr)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxNom)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBoxPorts)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePickerFch)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelHost)
        Me.SplitContainer1.Size = New System.Drawing.Size(931, 584)
        Me.SplitContainer1.SplitterDistance = 155
        Me.SplitContainer1.TabIndex = 100
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonObs, Me.ToolStripLabelKg, Me.ToolStripButtonTransmisio, Me.ToolStripButtonCustomDoc, Me.ToolStripButtonFra, Me.ToolStripSplitButtonCredit, Me.ToolStripButtonFpg, Me.ToolStripButtonExemptIva, Me.ToolStripButtonDiposit})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 370)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(155, 214)
        Me.ToolStrip1.TabIndex = 108
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonObs
        '
        Me.ToolStripButtonObs.Image = Global.Winforms.My.Resources.Resources.info
        Me.ToolStripButtonObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonObs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonObs.Name = "ToolStripButtonObs"
        Me.ToolStripButtonObs.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonObs.Text = "observacions"
        Me.ToolStripButtonObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripLabelKg
        '
        Me.ToolStripLabelKg.Image = Global.Winforms.My.Resources.Resources.package
        Me.ToolStripLabelKg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripLabelKg.Name = "ToolStripLabelKg"
        Me.ToolStripLabelKg.Size = New System.Drawing.Size(153, 16)
        Me.ToolStripLabelKg.Text = "bultos y Kg"
        '
        'ToolStripButtonTransmisio
        '
        Me.ToolStripButtonTransmisio.Image = Global.Winforms.My.Resources.Resources.pc
        Me.ToolStripButtonTransmisio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonTransmisio.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonTransmisio.Name = "ToolStripButtonTransmisio"
        Me.ToolStripButtonTransmisio.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonTransmisio.Text = "transmisió"
        Me.ToolStripButtonTransmisio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonCustomDoc
        '
        Me.ToolStripButtonCustomDoc.Image = Global.Winforms.My.Resources.Resources.iExplorer
        Me.ToolStripButtonCustomDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonCustomDoc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCustomDoc.Name = "ToolStripButtonCustomDoc"
        Me.ToolStripButtonCustomDoc.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonCustomDoc.Text = "doc.consumidor"
        '
        'ToolStripButtonFra
        '
        Me.ToolStripButtonFra.Image = Global.Winforms.My.Resources.Resources.NewDoc
        Me.ToolStripButtonFra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFra.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFra.Name = "ToolStripButtonFra"
        Me.ToolStripButtonFra.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFra.Text = "factura"
        Me.ToolStripButtonFra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripSplitButtonCredit
        '
        Me.ToolStripSplitButtonCredit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemCredit, Me.ToolStripMenuItemTransfer, Me.ToolStripMenuItemReembols})
        Me.ToolStripSplitButtonCredit.Image = CType(resources.GetObject("ToolStripSplitButtonCredit.Image"), System.Drawing.Image)
        Me.ToolStripSplitButtonCredit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripSplitButtonCredit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButtonCredit.Name = "ToolStripSplitButtonCredit"
        Me.ToolStripSplitButtonCredit.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripSplitButtonCredit.Text = "credit type"
        Me.ToolStripSplitButtonCredit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItemCredit
        '
        Me.ToolStripMenuItemCredit.Image = Global.Winforms.My.Resources.Resources.credit
        Me.ToolStripMenuItemCredit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripMenuItemCredit.Name = "ToolStripMenuItemCredit"
        Me.ToolStripMenuItemCredit.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemCredit.Text = "Credit"
        '
        'ToolStripMenuItemTransfer
        '
        Me.ToolStripMenuItemTransfer.Image = Global.Winforms.My.Resources.Resources.cash
        Me.ToolStripMenuItemTransfer.Name = "ToolStripMenuItemTransfer"
        Me.ToolStripMenuItemTransfer.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemTransfer.Text = "transferencia"
        '
        'ToolStripMenuItemReembols
        '
        Me.ToolStripMenuItemReembols.Image = Global.Winforms.My.Resources.Resources.reembols
        Me.ToolStripMenuItemReembols.Name = "ToolStripMenuItemReembols"
        Me.ToolStripMenuItemReembols.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemReembols.Text = "reembols"
        '
        'ToolStripButtonFpg
        '
        Me.ToolStripButtonFpg.Image = Global.Winforms.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonFpg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFpg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFpg.Name = "ToolStripButtonFpg"
        Me.ToolStripButtonFpg.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFpg.Text = "forma de pago"
        Me.ToolStripButtonFpg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonExemptIva
        '
        Me.ToolStripButtonExemptIva.Image = Global.Winforms.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonExemptIva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonExemptIva.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExemptIva.Name = "ToolStripButtonExemptIva"
        Me.ToolStripButtonExemptIva.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonExemptIva.Text = "exempt de Iva"
        '
        'ToolStripButtonDiposit
        '
        Me.ToolStripButtonDiposit.Image = Global.Winforms.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonDiposit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonDiposit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDiposit.Name = "ToolStripButtonDiposit"
        Me.ToolStripButtonDiposit.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonDiposit.Text = "diposit"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(155, 24)
        Me.MenuStrip1.TabIndex = 109
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClientToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ClientToolStripMenuItem
        '
        Me.ClientToolStripMenuItem.Name = "ClientToolStripMenuItem"
        Me.ClientToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.ClientToolStripMenuItem.Text = "Client"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(520, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 95
        Me.Label2.Text = "Incoterm"
        '
        'CheckBoxCEE
        '
        Me.CheckBoxCEE.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxCEE.Location = New System.Drawing.Point(710, 81)
        Me.CheckBoxCEE.Name = "CheckBoxCEE"
        Me.CheckBoxCEE.Size = New System.Drawing.Size(55, 16)
        Me.CheckBoxCEE.TabIndex = 93
        Me.CheckBoxCEE.TabStop = False
        Me.CheckBoxCEE.Text = "CEE"
        '
        'CheckBoxExport
        '
        Me.CheckBoxExport.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxExport.Location = New System.Drawing.Point(648, 81)
        Me.CheckBoxExport.Name = "CheckBoxExport"
        Me.CheckBoxExport.Size = New System.Drawing.Size(56, 16)
        Me.CheckBoxExport.TabIndex = 89
        Me.CheckBoxExport.TabStop = False
        Me.CheckBoxExport.Text = "export"
        '
        'Xl_LookupZip1
        '
        Me.Xl_LookupZip1.IsDirty = False
        Me.Xl_LookupZip1.Location = New System.Drawing.Point(3, 50)
        Me.Xl_LookupZip1.Name = "Xl_LookupZip1"
        Me.Xl_LookupZip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupZip1.ReadOnlyLookup = False
        Me.Xl_LookupZip1.Size = New System.Drawing.Size(320, 20)
        Me.Xl_LookupZip1.TabIndex = 88
        Me.Xl_LookupZip1.Value = Nothing
        '
        'Xl_Contact2Platform
        '
        Me.Xl_Contact2Platform.Contact = Nothing
        Me.Xl_Contact2Platform.Emp = Nothing
        Me.Xl_Contact2Platform.Location = New System.Drawing.Point(85, 71)
        Me.Xl_Contact2Platform.Name = "Xl_Contact2Platform"
        Me.Xl_Contact2Platform.ReadOnly = False
        Me.Xl_Contact2Platform.Size = New System.Drawing.Size(238, 20)
        Me.Xl_Contact2Platform.TabIndex = 87
        Me.Xl_Contact2Platform.Visible = False
        '
        'CheckBoxPlatform
        '
        Me.CheckBoxPlatform.AutoSize = True
        Me.CheckBoxPlatform.Location = New System.Drawing.Point(4, 73)
        Me.CheckBoxPlatform.Name = "CheckBoxPlatform"
        Me.CheckBoxPlatform.Size = New System.Drawing.Size(84, 17)
        Me.CheckBoxPlatform.TabIndex = 86
        Me.CheckBoxPlatform.Text = "Entregar en "
        Me.CheckBoxPlatform.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(520, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "ports"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(520, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "magatzem"
        '
        'Xl_ContactMgz
        '
        Me.Xl_ContactMgz.Contact = Nothing
        Me.Xl_ContactMgz.Emp = Nothing
        Me.Xl_ContactMgz.Location = New System.Drawing.Point(581, 57)
        Me.Xl_ContactMgz.Name = "Xl_ContactMgz"
        Me.Xl_ContactMgz.ReadOnly = False
        Me.Xl_ContactMgz.Size = New System.Drawing.Size(184, 20)
        Me.Xl_ContactMgz.TabIndex = 82
        '
        'LabelEmail
        '
        Me.LabelEmail.Location = New System.Drawing.Point(329, 76)
        Me.LabelEmail.Name = "LabelEmail"
        Me.LabelEmail.Size = New System.Drawing.Size(185, 16)
        Me.LabelEmail.TabIndex = 81
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Winforms.My.Resources.Resources.tel
        Me.PictureBox1.Location = New System.Drawing.Point(490, 57)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 79
        Me.PictureBox1.TabStop = False
        '
        'LabelLastAlb
        '
        Me.LabelLastAlb.Location = New System.Drawing.Point(520, 16)
        Me.LabelLastAlb.Name = "LabelLastAlb"
        Me.LabelLastAlb.Size = New System.Drawing.Size(151, 16)
        Me.LabelLastAlb.TabIndex = 69
        Me.LabelLastAlb.Text = "Ult.Albarà:"
        '
        'ComboBoxNum
        '
        Me.ComboBoxNum.FormattingEnabled = True
        Me.ComboBoxNum.Location = New System.Drawing.Point(408, 12)
        Me.ComboBoxNum.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.ComboBoxNum.Name = "ComboBoxNum"
        Me.ComboBoxNum.Size = New System.Drawing.Size(80, 21)
        Me.ComboBoxNum.TabIndex = 71
        Me.ComboBoxNum.TabStop = False
        Me.ComboBoxNum.Visible = False
        '
        'CheckBoxRecycle
        '
        Me.CheckBoxRecycle.Location = New System.Drawing.Point(331, 15)
        Me.CheckBoxRecycle.Name = "CheckBoxRecycle"
        Me.CheckBoxRecycle.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxRecycle.TabIndex = 70
        Me.CheckBoxRecycle.TabStop = False
        Me.CheckBoxRecycle.Text = "Reciclar nº"
        Me.CheckBoxRecycle.Visible = False
        '
        'CheckBoxFacturable
        '
        Me.CheckBoxFacturable.Location = New System.Drawing.Point(331, 55)
        Me.CheckBoxFacturable.Name = "CheckBoxFacturable"
        Me.CheckBoxFacturable.Size = New System.Drawing.Size(74, 16)
        Me.CheckBoxFacturable.TabIndex = 67
        Me.CheckBoxFacturable.TabStop = False
        Me.CheckBoxFacturable.Text = "facturable"
        '
        'CheckBoxValorat
        '
        Me.CheckBoxValorat.Location = New System.Drawing.Point(331, 35)
        Me.CheckBoxValorat.Name = "CheckBoxValorat"
        Me.CheckBoxValorat.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxValorat.TabIndex = 66
        Me.CheckBoxValorat.TabStop = False
        Me.CheckBoxValorat.Text = "valorat"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Location = New System.Drawing.Point(408, 55)
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxTel.TabIndex = 64
        Me.TextBoxTel.TabStop = False
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Location = New System.Drawing.Point(3, 33)
        Me.TextBoxAdr.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(320, 20)
        Me.TextBoxAdr.TabIndex = 63
        Me.TextBoxAdr.TabStop = False
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(3, 12)
        Me.TextBoxNom.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(320, 20)
        Me.TextBoxNom.TabIndex = 62
        Me.TextBoxNom.TabStop = False
        '
        'ComboBoxPorts
        '
        Me.ComboBoxPorts.FormattingEnabled = True
        Me.ComboBoxPorts.Items.AddRange(New Object() {"(seleccionar modalitat ports)", "PORTS PAGATS", "PORTS DEGUTS", "RECULLIRAN", "ALTRES", "ENTREGAT EN MA"})
        Me.ComboBoxPorts.Location = New System.Drawing.Point(581, 35)
        Me.ComboBoxPorts.Name = "ComboBoxPorts"
        Me.ComboBoxPorts.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxPorts.TabIndex = 65
        Me.ComboBoxPorts.TabStop = False
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(677, 12)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFch.TabIndex = 33
        Me.DateTimePickerFch.TabStop = False
        '
        'PanelHost
        '
        Me.PanelHost.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelHost.Controls.Add(Me.TabControl1)
        Me.PanelHost.Controls.Add(Me.StatusStripObs)
        Me.PanelHost.Location = New System.Drawing.Point(3, 102)
        Me.PanelHost.Name = "PanelHost"
        Me.PanelHost.Size = New System.Drawing.Size(766, 479)
        Me.PanelHost.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(766, 457)
        Me.TabControl1.TabIndex = 117
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_DeliveryItems1)
        Me.TabPage1.Controls.Add(Me.Xl_UsrLog1)
        Me.TabPage1.Controls.Add(Me.TextBoxTotals)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(758, 430)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Linies"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_DeliveryItems1
        '
        Me.Xl_DeliveryItems1.AllowUserToAddRows = False
        Me.Xl_DeliveryItems1.AllowUserToDeleteRows = False
        Me.Xl_DeliveryItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DeliveryItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_DeliveryItems1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_DeliveryItems1.Name = "Xl_DeliveryItems1"
        Me.Xl_DeliveryItems1.ReadOnly = True
        Me.Xl_DeliveryItems1.Size = New System.Drawing.Size(752, 384)
        Me.Xl_DeliveryItems1.TabIndex = 113
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(3, 387)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(752, 20)
        Me.Xl_UsrLog1.TabIndex = 116
        '
        'TextBoxTotals
        '
        Me.TextBoxTotals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBoxTotals.Location = New System.Drawing.Point(3, 407)
        Me.TextBoxTotals.Name = "TextBoxTotals"
        Me.TextBoxTotals.ReadOnly = True
        Me.TextBoxTotals.Size = New System.Drawing.Size(752, 20)
        Me.TextBoxTotals.TabIndex = 115
        Me.TextBoxTotals.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_DocFileEtiquetesTransport)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(758, 430)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Etiquetes transportista"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_DocFileEtiquetesTransport
        '
        Me.Xl_DocFileEtiquetesTransport.IsDirty = False
        Me.Xl_DocFileEtiquetesTransport.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DocFileEtiquetesTransport.Name = "Xl_DocFileEtiquetesTransport"
        Me.Xl_DocFileEtiquetesTransport.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFileEtiquetesTransport.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_Tree_DeliveryPackages1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(758, 430)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Bultos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_Tree_DeliveryPackages1
        '
        Me.Xl_Tree_DeliveryPackages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Tree_DeliveryPackages1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Tree_DeliveryPackages1.Name = "Xl_Tree_DeliveryPackages1"
        Me.Xl_Tree_DeliveryPackages1.Size = New System.Drawing.Size(752, 424)
        Me.Xl_Tree_DeliveryPackages1.TabIndex = 0
        '
        'StatusStripObs
        '
        Me.StatusStripObs.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusStripObs.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStripObs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelObs, Me.ToolStripStatusLabelCustDoc})
        Me.StatusStripObs.Location = New System.Drawing.Point(0, 0)
        Me.StatusStripObs.Name = "StatusStripObs"
        Me.StatusStripObs.Size = New System.Drawing.Size(766, 22)
        Me.StatusStripObs.TabIndex = 112
        Me.StatusStripObs.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelObs
        '
        Me.ToolStripStatusLabelObs.Image = Global.Winforms.My.Resources.Resources.info
        Me.ToolStripStatusLabelObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatusLabelObs.Name = "ToolStripStatusLabelObs"
        Me.ToolStripStatusLabelObs.Size = New System.Drawing.Size(735, 17)
        Me.ToolStripStatusLabelObs.Spring = True
        Me.ToolStripStatusLabelObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabelCustDoc
        '
        Me.ToolStripStatusLabelCustDoc.Image = Global.Winforms.My.Resources.Resources.iExplorer
        Me.ToolStripStatusLabelCustDoc.Name = "ToolStripStatusLabelCustDoc"
        Me.ToolStripStatusLabelCustDoc.Size = New System.Drawing.Size(16, 17)
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(581, 78)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(61, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 96
        Me.Xl_LookupIncoterm1.Value = Nothing
        '
        'Frm_AlbNew2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 615)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_AlbNew2"
        Me.Text = "ALBARA"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelHost.ResumeLayout(False)
        Me.PanelHost.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.Xl_DeliveryItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.StatusStripObs.ResumeLayout(False)
        Me.StatusStripObs.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelHost As System.Windows.Forms.Panel
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonObs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCustomDoc As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStripObs As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelObs As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelCustDoc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LabelLastAlb As System.Windows.Forms.Label
    Friend WithEvents ComboBoxNum As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxRecycle As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFacturable As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxValorat As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxTel As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAdr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxPorts As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelEmail As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripButtonTransmisio As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabelKg As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButtonFra As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButtonCredit As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ToolStripMenuItemCredit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemTransfer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemReembols As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButtonFpg As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactMgz As Xl_Contact2
    Friend WithEvents ToolStripButtonExemptIva As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonDiposit As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_Contact2Platform As Xl_Contact2
    Friend WithEvents CheckBoxPlatform As CheckBox
    Friend WithEvents Xl_LookupZip1 As Xl_LookupZip
    Friend WithEvents Xl_DeliveryItems1 As Xl_DeliveryItems
    Friend WithEvents TextBoxTotals As TextBox
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_DocFileEtiquetesTransport As Xl_DocFile_Old
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_Tree_DeliveryPackages1 As Xl_Tree_DeliveryPackages
    Friend WithEvents CheckBoxExport As CheckBox
    Friend WithEvents CheckBoxCEE As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClientToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
End Class
