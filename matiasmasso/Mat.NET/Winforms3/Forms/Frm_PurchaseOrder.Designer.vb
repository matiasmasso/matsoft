<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrder
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
        Me.ButtonAlb = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonObs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFpg = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFchMin = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFchMax = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonServirTotJunt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPot = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonBlockStock = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPromos = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonCustomDoc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonView = New System.Windows.Forms.ToolStripButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_ContactPlatform = New Mat.Net.Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTotal = New System.Windows.Forms.TextBox()
        Me.PanelItems = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_PurchaseOrderItems1 = New Mat.Net.Xl_PurchaseOrderItems()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_DocFileDoc = New Mat.Net.Xl_DocFile()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_DocFileEtiquetesTransport = New Mat.Net.Xl_DocFile()
        Me.ImageListTabHeaders = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStripObs = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelObs = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelCustDoc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.Xl_PdcSrc1 = New Mat.Net.Xl_PdcSrc()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxPromo = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupPromo1 = New Mat.Net.Xl_LookupIncentiu()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.PanelItems.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.StatusStripObs.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonAlb)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 377)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1071, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonAlb
        '
        Me.ButtonAlb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAlb.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonAlb.Enabled = False
        Me.ButtonAlb.Location = New System.Drawing.Point(964, 3)
        Me.ButtonAlb.Name = "ButtonAlb"
        Me.ButtonAlb.Size = New System.Drawing.Size(104, 24)
        Me.ButtonAlb.TabIndex = 3
        Me.ButtonAlb.Text = "Albarà"
        Me.ButtonAlb.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(744, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(854, 3)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(3, 3)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "Eliminar"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ContactPlatform)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxTotal)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelItems)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxStatus)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PdcSrc1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxPromo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_LookupPromo1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxConcept)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1071, 377)
        Me.SplitContainer1.SplitterDistance = 155
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonObs, Me.ToolStripButtonFpg, Me.ToolStripButtonFchMin, Me.ToolStripButtonFchMax, Me.ToolStripButtonServirTotJunt, Me.ToolStripButtonPot, Me.ToolStripButtonBlockStock, Me.ToolStripButtonPromos, Me.ToolStripButtonCustomDoc, Me.ToolStripButtonView})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 136)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(155, 241)
        Me.ToolStrip1.TabIndex = 108
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonObs
        '
        Me.ToolStripButtonObs.Image = Global.Mat.Net.My.Resources.Resources.info
        Me.ToolStripButtonObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonObs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonObs.Name = "ToolStripButtonObs"
        Me.ToolStripButtonObs.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonObs.Text = "observacions"
        Me.ToolStripButtonObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonFpg
        '
        Me.ToolStripButtonFpg.Image = Global.Mat.Net.My.Resources.Resources.info
        Me.ToolStripButtonFpg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFpg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFpg.Name = "ToolStripButtonFpg"
        Me.ToolStripButtonFpg.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFpg.Text = "pagament especial"
        Me.ToolStripButtonFpg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonFchMin
        '
        Me.ToolStripButtonFchMin.Image = Global.Mat.Net.My.Resources.Resources.Outlook_16
        Me.ToolStripButtonFchMin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFchMin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFchMin.Name = "ToolStripButtonFchMin"
        Me.ToolStripButtonFchMin.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFchMin.Text = "servei inmediat"
        '
        'ToolStripButtonFchMax
        '
        Me.ToolStripButtonFchMax.Image = Global.Mat.Net.My.Resources.Resources.Outlook_16
        Me.ToolStripButtonFchMax.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFchMax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFchMax.Name = "ToolStripButtonFchMax"
        Me.ToolStripButtonFchMax.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFchMax.Text = "data tope"
        '
        'ToolStripButtonServirTotJunt
        '
        Me.ToolStripButtonServirTotJunt.CheckOnClick = True
        Me.ToolStripButtonServirTotJunt.DoubleClickEnabled = True
        Me.ToolStripButtonServirTotJunt.Image = Global.Mat.Net.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonServirTotJunt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonServirTotJunt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonServirTotJunt.Name = "ToolStripButtonServirTotJunt"
        Me.ToolStripButtonServirTotJunt.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonServirTotJunt.Text = "servir tot junt"
        '
        'ToolStripButtonPot
        '
        Me.ToolStripButtonPot.CheckOnClick = True
        Me.ToolStripButtonPot.DoubleClickEnabled = True
        Me.ToolStripButtonPot.Image = Global.Mat.Net.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonPot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonPot.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPot.Name = "ToolStripButtonPot"
        Me.ToolStripButtonPot.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonPot.Text = "pot"
        '
        'ToolStripButtonBlockStock
        '
        Me.ToolStripButtonBlockStock.CheckOnClick = True
        Me.ToolStripButtonBlockStock.Image = Global.Mat.Net.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonBlockStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonBlockStock.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonBlockStock.Name = "ToolStripButtonBlockStock"
        Me.ToolStripButtonBlockStock.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonBlockStock.Text = "bloqueja l'stock"
        '
        'ToolStripButtonPromos
        '
        Me.ToolStripButtonPromos.Image = Global.Mat.Net.My.Resources.Resources.star
        Me.ToolStripButtonPromos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonPromos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPromos.Name = "ToolStripButtonPromos"
        Me.ToolStripButtonPromos.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonPromos.Text = "promocions"
        '
        'ToolStripButtonCustomDoc
        '
        Me.ToolStripButtonCustomDoc.Image = Global.Mat.Net.My.Resources.Resources.iExplorer
        Me.ToolStripButtonCustomDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonCustomDoc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCustomDoc.Name = "ToolStripButtonCustomDoc"
        Me.ToolStripButtonCustomDoc.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonCustomDoc.Text = "doc.consumidor"
        '
        'ToolStripButtonView
        '
        Me.ToolStripButtonView.CheckOnClick = True
        Me.ToolStripButtonView.Image = Global.Mat.Net.My.Resources.Resources.prismatics
        Me.ToolStripButtonView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonView.Name = "ToolStripButtonView"
        Me.ToolStripButtonView.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonView.Text = "vista sortides"
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
        'Xl_ContactPlatform
        '
        Me.Xl_ContactPlatform.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactPlatform.Contact = Nothing
        Me.Xl_ContactPlatform.Emp = Nothing
        Me.Xl_ContactPlatform.Location = New System.Drawing.Point(566, 30)
        Me.Xl_ContactPlatform.Name = "Xl_ContactPlatform"
        Me.Xl_ContactPlatform.ReadOnly = False
        Me.Xl_ContactPlatform.Size = New System.Drawing.Size(166, 20)
        Me.Xl_ContactPlatform.TabIndex = 129
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(511, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 128
        Me.Label2.Text = "plataforma:"
        '
        'TextBoxTotal
        '
        Me.TextBoxTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTotal.Location = New System.Drawing.Point(804, 357)
        Me.TextBoxTotal.Name = "TextBoxTotal"
        Me.TextBoxTotal.ReadOnly = True
        Me.TextBoxTotal.Size = New System.Drawing.Size(101, 20)
        Me.TextBoxTotal.TabIndex = 127
        Me.TextBoxTotal.TabStop = False
        Me.TextBoxTotal.Text = "Total:"
        '
        'PanelItems
        '
        Me.PanelItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelItems.Controls.Add(Me.TabControl1)
        Me.PanelItems.Controls.Add(Me.StatusStripObs)
        Me.PanelItems.Location = New System.Drawing.Point(3, 57)
        Me.PanelItems.Name = "PanelItems"
        Me.PanelItems.Size = New System.Drawing.Size(903, 301)
        Me.PanelItems.TabIndex = 126
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageListTabHeaders
        Me.TabControl1.Location = New System.Drawing.Point(0, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(903, 279)
        Me.TabControl1.TabIndex = 112
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_PurchaseOrderItems1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(895, 252)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Linies"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_PurchaseOrderItems1
        '
        Me.Xl_PurchaseOrderItems1.CliProductDtos = Nothing
        Me.Xl_PurchaseOrderItems1.CustomCosts = Nothing
        Me.Xl_PurchaseOrderItems1.CustomerTarifaDtos = Nothing
        Me.Xl_PurchaseOrderItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrderItems1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PurchaseOrderItems1.MultiTruck = False
        Me.Xl_PurchaseOrderItems1.Name = "Xl_PurchaseOrderItems1"
        Me.Xl_PurchaseOrderItems1.Size = New System.Drawing.Size(889, 246)
        Me.Xl_PurchaseOrderItems1.TabIndex = 2
        Me.Xl_PurchaseOrderItems1.Trucks = Nothing
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_DocFileDoc)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(895, 252)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Document"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_DocFileDoc
        '
        Me.Xl_DocFileDoc.IsDirty = False
        Me.Xl_DocFileDoc.IsInedit = False
        Me.Xl_DocFileDoc.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DocFileDoc.Name = "Xl_DocFileDoc"
        Me.Xl_DocFileDoc.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFileDoc.TabIndex = 116
        Me.Xl_DocFileDoc.TabStop = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_DocFileEtiquetesTransport)
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(895, 252)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Etiquetes"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_DocFileEtiquetesTransport
        '
        Me.Xl_DocFileEtiquetesTransport.IsDirty = False
        Me.Xl_DocFileEtiquetesTransport.IsInedit = False
        Me.Xl_DocFileEtiquetesTransport.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DocFileEtiquetesTransport.Name = "Xl_DocFileEtiquetesTransport"
        Me.Xl_DocFileEtiquetesTransport.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFileEtiquetesTransport.TabIndex = 0
        '
        'ImageListTabHeaders
        '
        Me.ImageListTabHeaders.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageListTabHeaders.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageListTabHeaders.TransparentColor = System.Drawing.Color.Transparent
        '
        'StatusStripObs
        '
        Me.StatusStripObs.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusStripObs.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStripObs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelObs, Me.ToolStripStatusLabelCustDoc})
        Me.StatusStripObs.Location = New System.Drawing.Point(0, 0)
        Me.StatusStripObs.Name = "StatusStripObs"
        Me.StatusStripObs.Size = New System.Drawing.Size(903, 22)
        Me.StatusStripObs.TabIndex = 111
        Me.StatusStripObs.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelObs
        '
        Me.ToolStripStatusLabelObs.Image = Global.Mat.Net.My.Resources.Resources.info
        Me.ToolStripStatusLabelObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatusLabelObs.Name = "ToolStripStatusLabelObs"
        Me.ToolStripStatusLabelObs.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.ToolStripStatusLabelObs.Size = New System.Drawing.Size(872, 17)
        Me.ToolStripStatusLabelObs.Spring = True
        Me.ToolStripStatusLabelObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabelCustDoc
        '
        Me.ToolStripStatusLabelCustDoc.Image = Global.Mat.Net.My.Resources.Resources.iExplorer
        Me.ToolStripStatusLabelCustDoc.Name = "ToolStripStatusLabelCustDoc"
        Me.ToolStripStatusLabelCustDoc.Size = New System.Drawing.Size(16, 17)
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.Location = New System.Drawing.Point(-1, 357)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.ReadOnly = True
        Me.TextBoxStatus.Size = New System.Drawing.Size(799, 20)
        Me.TextBoxStatus.TabIndex = 125
        Me.TextBoxStatus.TabStop = False
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExcel.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(738, 10)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(18, 18)
        Me.ButtonExcel.TabIndex = 124
        Me.ButtonExcel.TabStop = False
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'Xl_PdcSrc1
        '
        Me.Xl_PdcSrc1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PdcSrc1.Location = New System.Drawing.Point(762, 10)
        Me.Xl_PdcSrc1.Name = "Xl_PdcSrc1"
        Me.Xl_PdcSrc1.Size = New System.Drawing.Size(18, 16)
        Me.Xl_PdcSrc1.TabIndex = 123
        Me.Xl_PdcSrc1.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(786, 9)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 122
        Me.DateTimePicker1.TabStop = False
        '
        'CheckBoxPromo
        '
        Me.CheckBoxPromo.AutoSize = True
        Me.CheckBoxPromo.Location = New System.Drawing.Point(67, 33)
        Me.CheckBoxPromo.Name = "CheckBoxPromo"
        Me.CheckBoxPromo.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxPromo.TabIndex = 121
        Me.CheckBoxPromo.TabStop = False
        Me.CheckBoxPromo.Text = "promo"
        Me.CheckBoxPromo.UseVisualStyleBackColor = True
        '
        'Xl_LookupPromo1
        '
        Me.Xl_LookupPromo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupPromo1.Incentiu = Nothing
        Me.Xl_LookupPromo1.IsDirty = False
        Me.Xl_LookupPromo1.Location = New System.Drawing.Point(129, 31)
        Me.Xl_LookupPromo1.Name = "Xl_LookupPromo1"
        Me.Xl_LookupPromo1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPromo1.ReadOnlyLookup = False
        Me.Xl_LookupPromo1.Size = New System.Drawing.Size(373, 20)
        Me.Xl_LookupPromo1.TabIndex = 120
        Me.Xl_LookupPromo1.TabStop = False
        Me.Xl_LookupPromo1.Value = Nothing
        Me.Xl_LookupPromo1.Visible = False
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConcept.Location = New System.Drawing.Point(67, 9)
        Me.TextBoxConcept.MaxLength = 60
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.Size = New System.Drawing.Size(665, 20)
        Me.TextBoxConcept.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "&concepte:"
        '
        'Frm_PurchaseOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 408)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_PurchaseOrder"
        Me.Text = "Comanda"
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
        Me.PanelItems.ResumeLayout(False)
        Me.PanelItems.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CheckBoxPromo As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_LookupPromo1 As Xl_LookupIncentiu
    Friend WithEvents TextBoxConcept As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
    Friend WithEvents Xl_PdcSrc1 As Xl_PdcSrc
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonObs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFpg As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFchMin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonServirTotJunt As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPot As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPromos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCustomDoc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonView As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_DocFileDoc As Xl_DocFile
    Friend WithEvents ButtonAlb As System.Windows.Forms.Button
    Friend WithEvents TextBoxStatus As System.Windows.Forms.TextBox
    Friend WithEvents PanelItems As System.Windows.Forms.Panel
    Friend WithEvents Xl_PurchaseOrderItems1 As Xl_PurchaseOrderItems
    Friend WithEvents StatusStripObs As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelObs As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelCustDoc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextBoxTotal As System.Windows.Forms.TextBox
    Friend WithEvents Xl_ContactPlatform As Xl_Contact2
    Friend WithEvents Label2 As Label
    Friend WithEvents ToolStripButtonBlockStock As ToolStripButton
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_DocFileEtiquetesTransport As Xl_DocFile
    Friend WithEvents ImageListTabHeaders As ImageList
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClientToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButtonFchMax As ToolStripButton
End Class
