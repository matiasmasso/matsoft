<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Invoice
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxFra = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.LabelBaseImponible = New System.Windows.Forms.Label()
        Me.LabelIva = New System.Windows.Forms.Label()
        Me.LabelReq = New System.Windows.Forms.Label()
        Me.LabelLiquid = New System.Windows.Forms.Label()
        Me.TextBoxLiquid = New System.Windows.Forms.TextBox()
        Me.TextBoxReqAmt = New System.Windows.Forms.TextBox()
        Me.TextBoxReqPct = New System.Windows.Forms.TextBox()
        Me.TextBoxIvaPct = New System.Windows.Forms.TextBox()
        Me.TextBoxIvaAmt = New System.Windows.Forms.TextBox()
        Me.TextBoxBaseImponible = New System.Windows.Forms.TextBox()
        Me.Xl_InvoiceDeliveryItems1 = New Winforms.Xl_InvoiceDeliveryItems()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_LookupZip1 = New Winforms.Xl_LookupZip()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.Xl_LookupNif1 = New Winforms.Xl_LookupNif()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Xl_SiiLog1 = New Winforms.Xl_SiiLog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxConcepte = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCausaExempcio = New System.Windows.Forms.ComboBox()
        Me.ComboBoxTipoFra = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Langs1 = New Winforms.Xl_Langs()
        Me.CheckBoxCEE = New System.Windows.Forms.CheckBox()
        Me.CheckBoxExport = New System.Windows.Forms.CheckBox()
        Me.ComboBoxFraNum = New System.Windows.Forms.ComboBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LabelNif = New System.Windows.Forms.Label()
        Me.TextBoxNif = New System.Windows.Forms.TextBox()
        Me.RadioButtonExempta = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSubjecta = New System.Windows.Forms.RadioButton()
        Me.GroupBoxExempta = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxBaseExempta = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBoxSubjecta = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxIvaQuota = New System.Windows.Forms.TextBox()
        Me.TextBoxIvaTipus = New System.Windows.Forms.TextBox()
        Me.TextBoxBaseSujeta = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBoxRegEspOTrascs = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InversionSujetoPasivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Xl_LookupIncoterm1 = New Winforms.Xl_LookupIncoterm()
        Me.PanelButtons.SuspendLayout()
        CType(Me.Xl_InvoiceDeliveryItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBoxExempta.SuspendLayout()
        Me.GroupBoxSubjecta.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(467, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFra.Enabled = False
        Me.TextBoxFra.Location = New System.Drawing.Point(368, 5)
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.ReadOnly = True
        Me.TextBoxFra.Size = New System.Drawing.Size(65, 20)
        Me.TextBoxFra.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(319, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Factura"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(437, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Data"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(461, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(350, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 463)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(569, 31)
        Me.PanelButtons.TabIndex = 49
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(275, 80)
        Me.TextBoxObs.TabIndex = 59
        '
        'LabelBaseImponible
        '
        Me.LabelBaseImponible.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelBaseImponible.AutoSize = True
        Me.LabelBaseImponible.Location = New System.Drawing.Point(23, 330)
        Me.LabelBaseImponible.Name = "LabelBaseImponible"
        Me.LabelBaseImponible.Size = New System.Drawing.Size(78, 13)
        Me.LabelBaseImponible.TabIndex = 63
        Me.LabelBaseImponible.Text = "Base imponible"
        '
        'LabelIva
        '
        Me.LabelIva.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelIva.AutoSize = True
        Me.LabelIva.Location = New System.Drawing.Point(242, 330)
        Me.LabelIva.Name = "LabelIva"
        Me.LabelIva.Size = New System.Drawing.Size(24, 13)
        Me.LabelIva.TabIndex = 65
        Me.LabelIva.Text = "IVA"
        '
        'LabelReq
        '
        Me.LabelReq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelReq.AutoSize = True
        Me.LabelReq.Location = New System.Drawing.Point(301, 330)
        Me.LabelReq.Name = "LabelReq"
        Me.LabelReq.Size = New System.Drawing.Size(123, 13)
        Me.LabelReq.TabIndex = 66
        Me.LabelReq.Text = "Recàrrec d'Equivalència"
        '
        'LabelLiquid
        '
        Me.LabelLiquid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelLiquid.AutoSize = True
        Me.LabelLiquid.Location = New System.Drawing.Point(510, 330)
        Me.LabelLiquid.Name = "LabelLiquid"
        Me.LabelLiquid.Size = New System.Drawing.Size(35, 13)
        Me.LabelLiquid.TabIndex = 67
        Me.LabelLiquid.Text = "Liquid"
        '
        'TextBoxLiquid
        '
        Me.TextBoxLiquid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLiquid.Location = New System.Drawing.Point(448, 345)
        Me.TextBoxLiquid.Name = "TextBoxLiquid"
        Me.TextBoxLiquid.ReadOnly = True
        Me.TextBoxLiquid.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxLiquid.TabIndex = 68
        Me.TextBoxLiquid.TabStop = False
        Me.TextBoxLiquid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxReqAmt
        '
        Me.TextBoxReqAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxReqAmt.Location = New System.Drawing.Point(327, 345)
        Me.TextBoxReqAmt.Name = "TextBoxReqAmt"
        Me.TextBoxReqAmt.ReadOnly = True
        Me.TextBoxReqAmt.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxReqAmt.TabIndex = 69
        Me.TextBoxReqAmt.TabStop = False
        Me.TextBoxReqAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxReqPct
        '
        Me.TextBoxReqPct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxReqPct.Location = New System.Drawing.Point(291, 345)
        Me.TextBoxReqPct.Name = "TextBoxReqPct"
        Me.TextBoxReqPct.ReadOnly = True
        Me.TextBoxReqPct.Size = New System.Drawing.Size(37, 20)
        Me.TextBoxReqPct.TabIndex = 70
        Me.TextBoxReqPct.TabStop = False
        Me.TextBoxReqPct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxIvaPct
        '
        Me.TextBoxIvaPct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxIvaPct.Location = New System.Drawing.Point(133, 345)
        Me.TextBoxIvaPct.Name = "TextBoxIvaPct"
        Me.TextBoxIvaPct.ReadOnly = True
        Me.TextBoxIvaPct.Size = New System.Drawing.Size(37, 20)
        Me.TextBoxIvaPct.TabIndex = 72
        Me.TextBoxIvaPct.TabStop = False
        Me.TextBoxIvaPct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxIvaAmt
        '
        Me.TextBoxIvaAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxIvaAmt.Location = New System.Drawing.Point(169, 345)
        Me.TextBoxIvaAmt.Name = "TextBoxIvaAmt"
        Me.TextBoxIvaAmt.ReadOnly = True
        Me.TextBoxIvaAmt.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxIvaAmt.TabIndex = 71
        Me.TextBoxIvaAmt.TabStop = False
        Me.TextBoxIvaAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxBaseImponible
        '
        Me.TextBoxBaseImponible.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBaseImponible.Location = New System.Drawing.Point(4, 345)
        Me.TextBoxBaseImponible.Name = "TextBoxBaseImponible"
        Me.TextBoxBaseImponible.ReadOnly = True
        Me.TextBoxBaseImponible.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxBaseImponible.TabIndex = 73
        Me.TextBoxBaseImponible.TabStop = False
        Me.TextBoxBaseImponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_InvoiceDeliveryItems1
        '
        Me.Xl_InvoiceDeliveryItems1.AllowUserToAddRows = False
        Me.Xl_InvoiceDeliveryItems1.AllowUserToDeleteRows = False
        Me.Xl_InvoiceDeliveryItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_InvoiceDeliveryItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoiceDeliveryItems1.Location = New System.Drawing.Point(0, 117)
        Me.Xl_InvoiceDeliveryItems1.Name = "Xl_InvoiceDeliveryItems1"
        Me.Xl_InvoiceDeliveryItems1.ReadOnly = True
        Me.Xl_InvoiceDeliveryItems1.Size = New System.Drawing.Size(548, 210)
        Me.Xl_InvoiceDeliveryItems1.TabIndex = 56
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 31)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_LookupZip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxAdr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_LookupNif1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxNom)
        Me.SplitContainer1.Panel1MinSize = 20
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxObs)
        Me.SplitContainer1.Size = New System.Drawing.Size(546, 80)
        Me.SplitContainer1.SplitterDistance = 267
        Me.SplitContainer1.TabIndex = 75
        '
        'Xl_LookupZip1
        '
        Me.Xl_LookupZip1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_LookupZip1.IsDirty = False
        Me.Xl_LookupZip1.Location = New System.Drawing.Point(0, 60)
        Me.Xl_LookupZip1.Name = "Xl_LookupZip1"
        Me.Xl_LookupZip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupZip1.ReadOnlyLookup = False
        Me.Xl_LookupZip1.Size = New System.Drawing.Size(267, 20)
        Me.Xl_LookupZip1.TabIndex = 3
        Me.Xl_LookupZip1.Value = Nothing
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxAdr.Location = New System.Drawing.Point(0, 40)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxAdr.TabIndex = 1
        '
        'Xl_LookupNif1
        '
        Me.Xl_LookupNif1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_LookupNif1.IsDirty = False
        Me.Xl_LookupNif1.Location = New System.Drawing.Point(0, 20)
        Me.Xl_LookupNif1.Name = "Xl_LookupNif1"
        Me.Xl_LookupNif1.Nif = Nothing
        Me.Xl_LookupNif1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif1.ReadOnlyLookup = False
        Me.Xl_LookupNif1.Size = New System.Drawing.Size(267, 20)
        Me.Xl_LookupNif1.TabIndex = 2
        Me.Xl_LookupNif1.Value = Nothing
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBoxNom.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Xl_SiiLog1
        '
        Me.Xl_SiiLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SiiLog1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_SiiLog1.Location = New System.Drawing.Point(105, 313)
        Me.Xl_SiiLog1.Name = "Xl_SiiLog1"
        Me.Xl_SiiLog1.Size = New System.Drawing.Size(446, 20)
        Me.Xl_SiiLog1.TabIndex = 76
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Concepte"
        '
        'ComboBoxConcepte
        '
        Me.ComboBoxConcepte.FormattingEnabled = True
        Me.ComboBoxConcepte.Location = New System.Drawing.Point(105, 99)
        Me.ComboBoxConcepte.Name = "ComboBoxConcepte"
        Me.ComboBoxConcepte.Size = New System.Drawing.Size(108, 21)
        Me.ComboBoxConcepte.TabIndex = 2
        '
        'ComboBoxCausaExempcio
        '
        Me.ComboBoxCausaExempcio.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCausaExempcio.FormattingEnabled = True
        Me.ComboBoxCausaExempcio.Location = New System.Drawing.Point(88, 45)
        Me.ComboBoxCausaExempcio.Name = "ComboBoxCausaExempcio"
        Me.ComboBoxCausaExempcio.Size = New System.Drawing.Size(164, 21)
        Me.ComboBoxCausaExempcio.TabIndex = 3
        '
        'ComboBoxTipoFra
        '
        Me.ComboBoxTipoFra.FormattingEnabled = True
        Me.ComboBoxTipoFra.Location = New System.Drawing.Point(105, 45)
        Me.ComboBoxTipoFra.Name = "ComboBoxTipoFra"
        Me.ComboBoxTipoFra.Size = New System.Drawing.Size(262, 21)
        Me.ComboBoxTipoFra.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 89
        Me.Label5.Text = "Tipus Factura:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(563, 414)
        Me.TabControl1.TabIndex = 90
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LookupIncoterm1)
        Me.TabPage1.Controls.Add(Me.Xl_Langs1)
        Me.TabPage1.Controls.Add(Me.CheckBoxCEE)
        Me.TabPage1.Controls.Add(Me.CheckBoxExport)
        Me.TabPage1.Controls.Add(Me.ComboBoxFraNum)
        Me.TabPage1.Controls.Add(Me.LabelStatus)
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxFra)
        Me.TabPage1.Controls.Add(Me.DateTimePicker1)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Xl_InvoiceDeliveryItems1)
        Me.TabPage1.Controls.Add(Me.TextBoxIvaAmt)
        Me.TabPage1.Controls.Add(Me.LabelBaseImponible)
        Me.TabPage1.Controls.Add(Me.TextBoxBaseImponible)
        Me.TabPage1.Controls.Add(Me.LabelIva)
        Me.TabPage1.Controls.Add(Me.TextBoxIvaPct)
        Me.TabPage1.Controls.Add(Me.LabelReq)
        Me.TabPage1.Controls.Add(Me.LabelLiquid)
        Me.TabPage1.Controls.Add(Me.TextBoxReqPct)
        Me.TabPage1.Controls.Add(Me.TextBoxLiquid)
        Me.TabPage1.Controls.Add(Me.TextBoxReqAmt)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(555, 388)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(184, 5)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 81
        Me.Xl_Langs1.Value = Nothing
        '
        'CheckBoxCEE
        '
        Me.CheckBoxCEE.AutoSize = True
        Me.CheckBoxCEE.Location = New System.Drawing.Point(62, 7)
        Me.CheckBoxCEE.Name = "CheckBoxCEE"
        Me.CheckBoxCEE.Size = New System.Drawing.Size(47, 17)
        Me.CheckBoxCEE.TabIndex = 79
        Me.CheckBoxCEE.Text = "CEE"
        Me.CheckBoxCEE.UseVisualStyleBackColor = True
        Me.CheckBoxCEE.Visible = False
        '
        'CheckBoxExport
        '
        Me.CheckBoxExport.AutoSize = True
        Me.CheckBoxExport.Location = New System.Drawing.Point(7, 7)
        Me.CheckBoxExport.Name = "CheckBoxExport"
        Me.CheckBoxExport.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxExport.TabIndex = 78
        Me.CheckBoxExport.Text = "Export"
        Me.CheckBoxExport.UseVisualStyleBackColor = True
        '
        'ComboBoxFraNum
        '
        Me.ComboBoxFraNum.FormattingEnabled = True
        Me.ComboBoxFraNum.Location = New System.Drawing.Point(248, 5)
        Me.ComboBoxFraNum.Name = "ComboBoxFraNum"
        Me.ComboBoxFraNum.Size = New System.Drawing.Size(65, 21)
        Me.ComboBoxFraNum.TabIndex = 77
        Me.ComboBoxFraNum.Visible = False
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LabelStatus.Location = New System.Drawing.Point(3, 372)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(63, 13)
        Me.LabelStatus.TabIndex = 76
        Me.LabelStatus.Text = "LabelStatus"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.LabelNif)
        Me.TabPage2.Controls.Add(Me.TextBoxNif)
        Me.TabPage2.Controls.Add(Me.RadioButtonExempta)
        Me.TabPage2.Controls.Add(Me.RadioButtonSubjecta)
        Me.TabPage2.Controls.Add(Me.GroupBoxExempta)
        Me.TabPage2.Controls.Add(Me.GroupBoxSubjecta)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.ComboBoxRegEspOTrascs)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.ComboBoxTipoFra)
        Me.TabPage2.Controls.Add(Me.Xl_SiiLog1)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.ComboBoxConcepte)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(555, 388)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Llibre factures (Sii)"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'LabelNif
        '
        Me.LabelNif.AutoSize = True
        Me.LabelNif.Location = New System.Drawing.Point(14, 22)
        Me.LabelNif.Name = "LabelNif"
        Me.LabelNif.Size = New System.Drawing.Size(20, 13)
        Me.LabelNif.TabIndex = 100
        Me.LabelNif.Text = "Nif"
        '
        'TextBoxNif
        '
        Me.TextBoxNif.Location = New System.Drawing.Point(105, 17)
        Me.TextBoxNif.Name = "TextBoxNif"
        Me.TextBoxNif.ReadOnly = True
        Me.TextBoxNif.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxNif.TabIndex = 99
        Me.TextBoxNif.TabStop = False
        '
        'RadioButtonExempta
        '
        Me.RadioButtonExempta.AutoSize = True
        Me.RadioButtonExempta.Location = New System.Drawing.Point(291, 144)
        Me.RadioButtonExempta.Name = "RadioButtonExempta"
        Me.RadioButtonExempta.Size = New System.Drawing.Size(66, 17)
        Me.RadioButtonExempta.TabIndex = 98
        Me.RadioButtonExempta.TabStop = True
        Me.RadioButtonExempta.Text = "Exempta"
        Me.RadioButtonExempta.UseVisualStyleBackColor = True
        '
        'RadioButtonSubjecta
        '
        Me.RadioButtonSubjecta.AutoSize = True
        Me.RadioButtonSubjecta.Location = New System.Drawing.Point(16, 144)
        Me.RadioButtonSubjecta.Name = "RadioButtonSubjecta"
        Me.RadioButtonSubjecta.Size = New System.Drawing.Size(96, 17)
        Me.RadioButtonSubjecta.TabIndex = 97
        Me.RadioButtonSubjecta.TabStop = True
        Me.RadioButtonSubjecta.Text = "Subjecta a IVA"
        Me.RadioButtonSubjecta.UseVisualStyleBackColor = True
        '
        'GroupBoxExempta
        '
        Me.GroupBoxExempta.Controls.Add(Me.Label11)
        Me.GroupBoxExempta.Controls.Add(Me.TextBoxBaseExempta)
        Me.GroupBoxExempta.Controls.Add(Me.ComboBoxCausaExempcio)
        Me.GroupBoxExempta.Controls.Add(Me.Label4)
        Me.GroupBoxExempta.Location = New System.Drawing.Point(291, 154)
        Me.GroupBoxExempta.Name = "GroupBoxExempta"
        Me.GroupBoxExempta.Size = New System.Drawing.Size(258, 100)
        Me.GroupBoxExempta.TabIndex = 96
        Me.GroupBoxExempta.TabStop = False
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 93
        Me.Label11.Text = "Base exempta"
        '
        'TextBoxBaseExempta
        '
        Me.TextBoxBaseExempta.Location = New System.Drawing.Point(88, 20)
        Me.TextBoxBaseExempta.Name = "TextBoxBaseExempta"
        Me.TextBoxBaseExempta.ReadOnly = True
        Me.TextBoxBaseExempta.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxBaseExempta.TabIndex = 92
        Me.TextBoxBaseExempta.TabStop = False
        Me.TextBoxBaseExempta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Causa"
        '
        'GroupBoxSubjecta
        '
        Me.GroupBoxSubjecta.Controls.Add(Me.Label10)
        Me.GroupBoxSubjecta.Controls.Add(Me.Label9)
        Me.GroupBoxSubjecta.Controls.Add(Me.Label8)
        Me.GroupBoxSubjecta.Controls.Add(Me.TextBoxIvaQuota)
        Me.GroupBoxSubjecta.Controls.Add(Me.TextBoxIvaTipus)
        Me.GroupBoxSubjecta.Controls.Add(Me.TextBoxBaseSujeta)
        Me.GroupBoxSubjecta.Location = New System.Drawing.Point(17, 154)
        Me.GroupBoxSubjecta.Name = "GroupBoxSubjecta"
        Me.GroupBoxSubjecta.Size = New System.Drawing.Size(258, 100)
        Me.GroupBoxSubjecta.TabIndex = 95
        Me.GroupBoxSubjecta.TabStop = False
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 76)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 93
        Me.Label10.Text = "Quota"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 13)
        Me.Label9.TabIndex = 92
        Me.Label9.Text = "Tipus"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 91
        Me.Label8.Text = "Base imponible"
        '
        'TextBoxIvaQuota
        '
        Me.TextBoxIvaQuota.Location = New System.Drawing.Point(124, 73)
        Me.TextBoxIvaQuota.Name = "TextBoxIvaQuota"
        Me.TextBoxIvaQuota.ReadOnly = True
        Me.TextBoxIvaQuota.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxIvaQuota.TabIndex = 2
        Me.TextBoxIvaQuota.TabStop = False
        Me.TextBoxIvaQuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxIvaTipus
        '
        Me.TextBoxIvaTipus.Location = New System.Drawing.Point(124, 46)
        Me.TextBoxIvaTipus.Name = "TextBoxIvaTipus"
        Me.TextBoxIvaTipus.ReadOnly = True
        Me.TextBoxIvaTipus.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxIvaTipus.TabIndex = 1
        Me.TextBoxIvaTipus.TabStop = False
        Me.TextBoxIvaTipus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxBaseSujeta
        '
        Me.TextBoxBaseSujeta.Location = New System.Drawing.Point(124, 20)
        Me.TextBoxBaseSujeta.Name = "TextBoxBaseSujeta"
        Me.TextBoxBaseSujeta.ReadOnly = True
        Me.TextBoxBaseSujeta.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxBaseSujeta.TabIndex = 0
        Me.TextBoxBaseSujeta.TabStop = False
        Me.TextBoxBaseSujeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 94
        Me.Label7.Text = "Reg.Esp.O Trasc"
        '
        'ComboBoxRegEspOTrascs
        '
        Me.ComboBoxRegEspOTrascs.FormattingEnabled = True
        Me.ComboBoxRegEspOTrascs.Location = New System.Drawing.Point(105, 72)
        Me.ComboBoxRegEspOTrascs.Name = "ComboBoxRegEspOTrascs"
        Me.ComboBoxRegEspOTrascs.Size = New System.Drawing.Size(262, 21)
        Me.ComboBoxRegEspOTrascs.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 316)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 13)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Log"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(569, 24)
        Me.MenuStrip1.TabIndex = 91
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InversionSujetoPasivoToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'InversionSujetoPasivoToolStripMenuItem
        '
        Me.InversionSujetoPasivoToolStripMenuItem.CheckOnClick = True
        Me.InversionSujetoPasivoToolStripMenuItem.Name = "InversionSujetoPasivoToolStripMenuItem"
        Me.InversionSujetoPasivoToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.InversionSujetoPasivoToolStripMenuItem.Text = "Inversión de sujeto pasivo"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(569, 439)
        Me.Panel2.TabIndex = 92
        '
        'Xl_LookupIncoterm1
        '
        Me.Xl_LookupIncoterm1.FormattingEnabled = True
        Me.Xl_LookupIncoterm1.Location = New System.Drawing.Point(115, 5)
        Me.Xl_LookupIncoterm1.Name = "Xl_LookupIncoterm1"
        Me.Xl_LookupIncoterm1.Size = New System.Drawing.Size(55, 21)
        Me.Xl_LookupIncoterm1.TabIndex = 82
        Me.Xl_LookupIncoterm1.Value = Nothing
        Me.Xl_LookupIncoterm1.Visible = False
        '
        'Frm_Invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 494)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.PanelButtons)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Invoice"
        Me.Text = "Factura a client"
        Me.PanelButtons.ResumeLayout(False)
        CType(Me.Xl_InvoiceDeliveryItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBoxExempta.ResumeLayout(False)
        Me.GroupBoxExempta.PerformLayout()
        Me.GroupBoxSubjecta.ResumeLayout(False)
        Me.GroupBoxSubjecta.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents PanelButtons As System.Windows.Forms.Panel
    Friend WithEvents Xl_InvoiceDeliveryItems1 As Winforms.Xl_InvoiceDeliveryItems
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents LabelBaseImponible As Label
    Friend WithEvents LabelIva As Label
    Friend WithEvents LabelReq As Label
    Friend WithEvents LabelLiquid As Label
    Friend WithEvents TextBoxLiquid As TextBox
    Friend WithEvents TextBoxReqAmt As TextBox
    Friend WithEvents TextBoxReqPct As TextBox
    Friend WithEvents TextBoxIvaPct As TextBox
    Friend WithEvents TextBoxIvaAmt As TextBox
    Friend WithEvents TextBoxBaseImponible As TextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_SiiLog1 As Xl_SiiLog
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBoxConcepte As ComboBox
    Friend WithEvents ComboBoxCausaExempcio As ComboBox
    Friend WithEvents ComboBoxTipoFra As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelStatus As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBoxRegEspOTrascs As ComboBox
    Friend WithEvents LabelNif As Label
    Friend WithEvents TextBoxNif As TextBox
    Friend WithEvents RadioButtonExempta As RadioButton
    Friend WithEvents RadioButtonSubjecta As RadioButton
    Friend WithEvents GroupBoxExempta As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBoxBaseExempta As TextBox
    Friend WithEvents GroupBoxSubjecta As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxIvaQuota As TextBox
    Friend WithEvents TextBoxIvaTipus As TextBox
    Friend WithEvents TextBoxBaseSujeta As TextBox
    Friend WithEvents ComboBoxFraNum As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CheckBoxCEE As CheckBox
    Friend WithEvents CheckBoxExport As CheckBox
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents Xl_LookupNif1 As Xl_LookupNif
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents Xl_LookupZip1 As Xl_LookupZip
    Friend WithEvents InversionSujetoPasivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_LookupIncoterm1 As Xl_LookupIncoterm
End Class
