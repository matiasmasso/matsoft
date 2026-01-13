<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Tpa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ButtonShowLangTexts = New System.Windows.Forms.Button()
        Me.LabelWebAtlasRafflesDeadline2 = New System.Windows.Forms.Label()
        Me.NumericUpDownWebatlasRafflesDeadline = New System.Windows.Forms.NumericUpDown()
        Me.LabelWebAtlasRafflesDeadline1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.LabelWebatlasDeadline2 = New System.Windows.Forms.Label()
        Me.NumericUpDownWebatlasDeadline = New System.Windows.Forms.NumericUpDown()
        Me.LabelWebAtlasDeadline1 = New System.Windows.Forms.Label()
        Me.Xl_LookupCountryMadeIn = New Mat.Net.Xl_LookupCountry()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Xl_ProductChannels1 = New Mat.Net.Xl_ProductChannels()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBoxDistribuidorsOficials = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowAtlas = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_ContactProveidor = New Mat.Net.Xl_Contact2()
        Me.Xl_Image_Logo = New Mat.Net.Xl_Image()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.LabelNum = New System.Windows.Forms.Label()
        Me.TabPageStps = New System.Windows.Forms.TabPage()
        Me.Xl_ProductCategories1 = New Mat.Net.Xl_ProductCategories()
        Me.TabPageZonas = New System.Windows.Forms.TabPage()
        Me.Xl_BrandAreas1 = New Mat.Net.Xl_BrandAreas()
        Me.TabPageReps = New System.Windows.Forms.TabPage()
        Me.Xl_RepProductsxRep1 = New Mat.Net.Xl_RepProducts()
        Me.ButtonRepsExcel = New System.Windows.Forms.Button()
        Me.ButtonMailTpaReps = New System.Windows.Forms.Button()
        Me.CheckBoxOnlyEmpty = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBoxWebEnabledPro = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWebEnabledConsumer = New System.Windows.Forms.CheckBox()
        Me.Xl_ImageLogoDistribuidorOficial = New Mat.Net.Xl_Image()
        Me.TabPagePlugins = New System.Windows.Forms.TabPage()
        Me.Xl_ProductPlugins1 = New Mat.Net.Xl_ProductPlugins()
        Me.TabPageDownloads = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Mat.Net.Xl_ProductDownloads()
        Me.TabPageRecursos = New System.Windows.Forms.TabPage()
        Me.ProgressBarMediaResources = New System.Windows.Forms.ProgressBar()
        Me.Xl_MediaResources1 = New Mat.Net.Xl_MediaResources()
        Me.TabPageLogistica = New System.Windows.Forms.TabPage()
        Me.Xl_LookupCodiMercancia1 = New Mat.Net.Xl_LookupCodiMercancia()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuStripDownload = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AfegirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonActiva = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEnLiquidacio = New System.Windows.Forms.RadioButton()
        Me.RadioButtonObsoleto = New System.Windows.Forms.RadioButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownWebatlasRafflesDeadline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownWebatlasDeadline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStps.SuspendLayout()
        Me.TabPageZonas.SuspendLayout()
        Me.TabPageReps.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPagePlugins.SuspendLayout()
        CType(Me.Xl_ProductPlugins1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDownloads.SuspendLayout()
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageRecursos.SuspendLayout()
        Me.TabPageLogistica.SuspendLayout()
        Me.ContextMenuStripDownload.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageStps)
        Me.TabControl1.Controls.Add(Me.TabPageZonas)
        Me.TabControl1.Controls.Add(Me.TabPageReps)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPagePlugins)
        Me.TabControl1.Controls.Add(Me.TabPageDownloads)
        Me.TabControl1.Controls.Add(Me.TabPageRecursos)
        Me.TabControl1.Controls.Add(Me.TabPageLogistica)
        Me.TabControl1.Location = New System.Drawing.Point(4, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(607, 409)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.ButtonShowLangTexts)
        Me.TabPage1.Controls.Add(Me.LabelWebAtlasRafflesDeadline2)
        Me.TabPage1.Controls.Add(Me.NumericUpDownWebatlasRafflesDeadline)
        Me.TabPage1.Controls.Add(Me.LabelWebAtlasRafflesDeadline1)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.LabelWebatlasDeadline2)
        Me.TabPage1.Controls.Add(Me.NumericUpDownWebatlasDeadline)
        Me.TabPage1.Controls.Add(Me.LabelWebAtlasDeadline1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupCountryMadeIn)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Xl_ProductChannels1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.CheckBoxDistribuidorsOficials)
        Me.TabPage1.Controls.Add(Me.CheckBoxShowAtlas)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Xl_ContactProveidor)
        Me.TabPage1.Controls.Add(Me.Xl_Image_Logo)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.LabelNum)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(599, 383)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ButtonShowLangTexts
        '
        Me.ButtonShowLangTexts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonShowLangTexts.Location = New System.Drawing.Point(559, 8)
        Me.ButtonShowLangTexts.Name = "ButtonShowLangTexts"
        Me.ButtonShowLangTexts.Size = New System.Drawing.Size(29, 20)
        Me.ButtonShowLangTexts.TabIndex = 38
        Me.ButtonShowLangTexts.Text = "..."
        Me.ButtonShowLangTexts.UseVisualStyleBackColor = True
        '
        'LabelWebAtlasRafflesDeadline2
        '
        Me.LabelWebAtlasRafflesDeadline2.AutoSize = True
        Me.LabelWebAtlasRafflesDeadline2.Location = New System.Drawing.Point(360, 120)
        Me.LabelWebAtlasRafflesDeadline2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelWebAtlasRafflesDeadline2.Name = "LabelWebAtlasRafflesDeadline2"
        Me.LabelWebAtlasRafflesDeadline2.Size = New System.Drawing.Size(26, 13)
        Me.LabelWebAtlasRafflesDeadline2.TabIndex = 37
        Me.LabelWebAtlasRafflesDeadline2.Text = "dies"
        '
        'NumericUpDownWebatlasRafflesDeadline
        '
        Me.NumericUpDownWebatlasRafflesDeadline.Location = New System.Drawing.Point(313, 118)
        Me.NumericUpDownWebatlasRafflesDeadline.Margin = New System.Windows.Forms.Padding(1)
        Me.NumericUpDownWebatlasRafflesDeadline.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDownWebatlasRafflesDeadline.Name = "NumericUpDownWebatlasRafflesDeadline"
        Me.NumericUpDownWebatlasRafflesDeadline.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownWebatlasRafflesDeadline.TabIndex = 36
        '
        'LabelWebAtlasRafflesDeadline1
        '
        Me.LabelWebAtlasRafflesDeadline1.AutoSize = True
        Me.LabelWebAtlasRafflesDeadline1.Location = New System.Drawing.Point(25, 120)
        Me.LabelWebAtlasRafflesDeadline1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelWebAtlasRafflesDeadline1.Name = "LabelWebAtlasRafflesDeadline1"
        Me.LabelWebAtlasRafflesDeadline1.Size = New System.Drawing.Size(181, 13)
        Me.LabelWebAtlasRafflesDeadline1.TabIndex = 35
        Me.LabelWebAtlasRafflesDeadline1.Text = "Limita-ho als distribuidors als sortejos "
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 351)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(599, 32)
        Me.Panel1.TabIndex = 34
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(3, 5)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonDel.TabIndex = 15
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(515, 5)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(80, 24)
        Me.ButtonOk.TabIndex = 13
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(425, 5)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'LabelWebatlasDeadline2
        '
        Me.LabelWebatlasDeadline2.AutoSize = True
        Me.LabelWebatlasDeadline2.Location = New System.Drawing.Point(360, 100)
        Me.LabelWebatlasDeadline2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelWebatlasDeadline2.Name = "LabelWebatlasDeadline2"
        Me.LabelWebatlasDeadline2.Size = New System.Drawing.Size(26, 13)
        Me.LabelWebatlasDeadline2.TabIndex = 33
        Me.LabelWebatlasDeadline2.Text = "dies"
        '
        'NumericUpDownWebatlasDeadline
        '
        Me.NumericUpDownWebatlasDeadline.Location = New System.Drawing.Point(313, 98)
        Me.NumericUpDownWebatlasDeadline.Margin = New System.Windows.Forms.Padding(1)
        Me.NumericUpDownWebatlasDeadline.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDownWebatlasDeadline.Name = "NumericUpDownWebatlasDeadline"
        Me.NumericUpDownWebatlasDeadline.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownWebatlasDeadline.TabIndex = 32
        '
        'LabelWebAtlasDeadline1
        '
        Me.LabelWebAtlasDeadline1.AutoSize = True
        Me.LabelWebAtlasDeadline1.Location = New System.Drawing.Point(25, 100)
        Me.LabelWebAtlasDeadline1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelWebAtlasDeadline1.Name = "LabelWebAtlasDeadline1"
        Me.LabelWebAtlasDeadline1.Size = New System.Drawing.Size(280, 13)
        Me.LabelWebAtlasDeadline1.TabIndex = 31
        Me.LabelWebAtlasDeadline1.Text = "Limita-ho als distribuidors amb comanda durant els darrers "
        '
        'Xl_LookupCountryMadeIn
        '
        Me.Xl_LookupCountryMadeIn.Country = Nothing
        Me.Xl_LookupCountryMadeIn.IsDirty = False
        Me.Xl_LookupCountryMadeIn.Location = New System.Drawing.Point(64, 237)
        Me.Xl_LookupCountryMadeIn.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_LookupCountryMadeIn.Name = "Xl_LookupCountryMadeIn"
        Me.Xl_LookupCountryMadeIn.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCountryMadeIn.ReadOnlyLookup = False
        Me.Xl_LookupCountryMadeIn.Size = New System.Drawing.Size(263, 20)
        Me.Xl_LookupCountryMadeIn.TabIndex = 30
        Me.Xl_LookupCountryMadeIn.Value = Nothing
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(4, 239)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 16)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Made In:"
        '
        'Xl_ProductChannels1
        '
        Me.Xl_ProductChannels1.AllowUserToAddRows = False
        Me.Xl_ProductChannels1.AllowUserToDeleteRows = False
        Me.Xl_ProductChannels1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ProductChannels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductChannels1.DisplayObsolets = False
        Me.Xl_ProductChannels1.Location = New System.Drawing.Point(439, 189)
        Me.Xl_ProductChannels1.MouseIsDown = False
        Me.Xl_ProductChannels1.Name = "Xl_ProductChannels1"
        Me.Xl_ProductChannels1.ReadOnly = True
        Me.Xl_ProductChannels1.Size = New System.Drawing.Size(149, 142)
        Me.Xl_ProductChannels1.TabIndex = 26
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(28, 181)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(336, 34)
        Me.TextBox1.TabIndex = 15
        Me.TextBox1.Text = "bloqueja les comandes dels clients que no estan registrats preeviament com a dist" &
    "ribuidors oficials de la marca a clients->exclusives"
        '
        'CheckBoxDistribuidorsOficials
        '
        Me.CheckBoxDistribuidorsOficials.Location = New System.Drawing.Point(7, 159)
        Me.CheckBoxDistribuidorsOficials.Name = "CheckBoxDistribuidorsOficials"
        Me.CheckBoxDistribuidorsOficials.Size = New System.Drawing.Size(195, 16)
        Me.CheckBoxDistribuidorsOficials.TabIndex = 13
        Me.CheckBoxDistribuidorsOficials.Text = "Xarxa de distribuidors oficials"
        '
        'CheckBoxShowAtlas
        '
        Me.CheckBoxShowAtlas.Location = New System.Drawing.Point(7, 73)
        Me.CheckBoxShowAtlas.Name = "CheckBoxShowAtlas"
        Me.CheckBoxShowAtlas.Size = New System.Drawing.Size(195, 16)
        Me.CheckBoxShowAtlas.TabIndex = 11
        Me.CheckBoxShowAtlas.Text = "Publicar llista distribuidors a la web"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Proveidor:"
        '
        'Xl_ContactProveidor
        '
        Me.Xl_ContactProveidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactProveidor.Contact = Nothing
        Me.Xl_ContactProveidor.Emp = Nothing
        Me.Xl_ContactProveidor.Location = New System.Drawing.Point(64, 35)
        Me.Xl_ContactProveidor.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactProveidor.Name = "Xl_ContactProveidor"
        Me.Xl_ContactProveidor.ReadOnly = False
        Me.Xl_ContactProveidor.Size = New System.Drawing.Size(525, 20)
        Me.Xl_ContactProveidor.TabIndex = 4
        '
        'Xl_Image_Logo
        '
        Me.Xl_Image_Logo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image_Logo.Bitmap = Nothing
        Me.Xl_Image_Logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image_Logo.EmptyImageLabelText = ""
        Me.Xl_Image_Logo.IsDirty = False
        Me.Xl_Image_Logo.Location = New System.Drawing.Point(439, 73)
        Me.Xl_Image_Logo.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Image_Logo.Name = "Xl_Image_Logo"
        Me.Xl_Image_Logo.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Image_Logo.TabIndex = 9
        Me.Xl_Image_Logo.TabStop = False
        Me.Xl_Image_Logo.ZipStream = Nothing
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(64, 8)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.ReadOnly = True
        Me.TextBoxNom.Size = New System.Drawing.Size(489, 20)
        Me.TextBoxNom.TabIndex = 2
        '
        'LabelNum
        '
        Me.LabelNum.Location = New System.Drawing.Point(4, 8)
        Me.LabelNum.Name = "LabelNum"
        Me.LabelNum.Size = New System.Drawing.Size(40, 16)
        Me.LabelNum.TabIndex = 1
        Me.LabelNum.Text = "Nom:"
        '
        'TabPageStps
        '
        Me.TabPageStps.Controls.Add(Me.Xl_ProductCategories1)
        Me.TabPageStps.Location = New System.Drawing.Point(4, 22)
        Me.TabPageStps.Name = "TabPageStps"
        Me.TabPageStps.Size = New System.Drawing.Size(599, 392)
        Me.TabPageStps.TabIndex = 1
        Me.TabPageStps.Text = "Categories"
        Me.TabPageStps.UseVisualStyleBackColor = True
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.AllowRemoveOnContextMenu = False
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductCategories1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.ShowObsolets = False
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(599, 392)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'TabPageZonas
        '
        Me.TabPageZonas.Controls.Add(Me.Xl_BrandAreas1)
        Me.TabPageZonas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageZonas.Name = "TabPageZonas"
        Me.TabPageZonas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageZonas.Size = New System.Drawing.Size(599, 392)
        Me.TabPageZonas.TabIndex = 7
        Me.TabPageZonas.Text = "Zones"
        Me.TabPageZonas.UseVisualStyleBackColor = True
        '
        'Xl_BrandAreas1
        '
        Me.Xl_BrandAreas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BrandAreas1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_BrandAreas1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_BrandAreas1.Name = "Xl_BrandAreas1"
        Me.Xl_BrandAreas1.Size = New System.Drawing.Size(593, 386)
        Me.Xl_BrandAreas1.TabIndex = 0
        '
        'TabPageReps
        '
        Me.TabPageReps.Controls.Add(Me.Xl_RepProductsxRep1)
        Me.TabPageReps.Controls.Add(Me.ButtonRepsExcel)
        Me.TabPageReps.Controls.Add(Me.ButtonMailTpaReps)
        Me.TabPageReps.Controls.Add(Me.CheckBoxOnlyEmpty)
        Me.TabPageReps.Location = New System.Drawing.Point(4, 22)
        Me.TabPageReps.Name = "TabPageReps"
        Me.TabPageReps.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReps.Size = New System.Drawing.Size(599, 392)
        Me.TabPageReps.TabIndex = 3
        Me.TabPageReps.Text = "Reps"
        Me.TabPageReps.UseVisualStyleBackColor = True
        '
        'Xl_RepProductsxRep1
        '
        Me.Xl_RepProductsxRep1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RepProductsxRep1.Location = New System.Drawing.Point(5, 58)
        Me.Xl_RepProductsxRep1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_RepProductsxRep1.Name = "Xl_RepProductsxRep1"
        Me.Xl_RepProductsxRep1.Size = New System.Drawing.Size(594, 334)
        Me.Xl_RepProductsxRep1.TabIndex = 5
        '
        'ButtonRepsExcel
        '
        Me.ButtonRepsExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRepsExcel.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ButtonRepsExcel.Location = New System.Drawing.Point(641, 11)
        Me.ButtonRepsExcel.Name = "ButtonRepsExcel"
        Me.ButtonRepsExcel.Size = New System.Drawing.Size(20, 20)
        Me.ButtonRepsExcel.TabIndex = 4
        Me.ButtonRepsExcel.UseVisualStyleBackColor = True
        '
        'ButtonMailTpaReps
        '
        Me.ButtonMailTpaReps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMailTpaReps.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMailTpaReps.Location = New System.Drawing.Point(232, 6)
        Me.ButtonMailTpaReps.Name = "ButtonMailTpaReps"
        Me.ButtonMailTpaReps.Size = New System.Drawing.Size(202, 26)
        Me.ButtonMailTpaReps.TabIndex = 2
        Me.ButtonMailTpaReps.Text = "missatge a tots els representants"
        '
        'CheckBoxOnlyEmpty
        '
        Me.CheckBoxOnlyEmpty.AutoSize = True
        Me.CheckBoxOnlyEmpty.Location = New System.Drawing.Point(4, 14)
        Me.CheckBoxOnlyEmpty.Name = "CheckBoxOnlyEmpty"
        Me.CheckBoxOnlyEmpty.Size = New System.Drawing.Size(159, 17)
        Me.CheckBoxOnlyEmpty.TabIndex = 1
        Me.CheckBoxOnlyEmpty.Text = "mostrar només zones buidas"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.CheckBoxWebEnabledPro)
        Me.TabPage2.Controls.Add(Me.CheckBoxWebEnabledConsumer)
        Me.TabPage2.Controls.Add(Me.Xl_ImageLogoDistribuidorOficial)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(599, 392)
        Me.TabPage2.TabIndex = 5
        Me.TabPage2.Text = "Web"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "logo de distribuidor oficial:"
        '
        'CheckBoxWebEnabledPro
        '
        Me.CheckBoxWebEnabledPro.Location = New System.Drawing.Point(17, 20)
        Me.CheckBoxWebEnabledPro.Name = "CheckBoxWebEnabledPro"
        Me.CheckBoxWebEnabledPro.Size = New System.Drawing.Size(169, 16)
        Me.CheckBoxWebEnabledPro.TabIndex = 8
        Me.CheckBoxWebEnabledPro.Text = "Publicar en web al profesional"
        '
        'CheckBoxWebEnabledConsumer
        '
        Me.CheckBoxWebEnabledConsumer.Location = New System.Drawing.Point(238, 20)
        Me.CheckBoxWebEnabledConsumer.Name = "CheckBoxWebEnabledConsumer"
        Me.CheckBoxWebEnabledConsumer.Size = New System.Drawing.Size(179, 16)
        Me.CheckBoxWebEnabledConsumer.TabIndex = 10
        Me.CheckBoxWebEnabledConsumer.Text = "Publicar en web al consumidor"
        '
        'Xl_ImageLogoDistribuidorOficial
        '
        Me.Xl_ImageLogoDistribuidorOficial.Bitmap = Nothing
        Me.Xl_ImageLogoDistribuidorOficial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageLogoDistribuidorOficial.EmptyImageLabelText = ""
        Me.Xl_ImageLogoDistribuidorOficial.IsDirty = False
        Me.Xl_ImageLogoDistribuidorOficial.Location = New System.Drawing.Point(17, 66)
        Me.Xl_ImageLogoDistribuidorOficial.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ImageLogoDistribuidorOficial.Name = "Xl_ImageLogoDistribuidorOficial"
        Me.Xl_ImageLogoDistribuidorOficial.Size = New System.Drawing.Size(200, 200)
        Me.Xl_ImageLogoDistribuidorOficial.TabIndex = 16
        Me.Xl_ImageLogoDistribuidorOficial.ZipStream = Nothing
        '
        'TabPagePlugins
        '
        Me.TabPagePlugins.Controls.Add(Me.Xl_ProductPlugins1)
        Me.TabPagePlugins.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePlugins.Name = "TabPagePlugins"
        Me.TabPagePlugins.Size = New System.Drawing.Size(599, 392)
        Me.TabPagePlugins.TabIndex = 12
        Me.TabPagePlugins.Text = "Plugins"
        Me.TabPagePlugins.UseVisualStyleBackColor = True
        '
        'Xl_ProductPlugins1
        '
        Me.Xl_ProductPlugins1.AllowUserToAddRows = False
        Me.Xl_ProductPlugins1.AllowUserToDeleteRows = False
        Me.Xl_ProductPlugins1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductPlugins1.DisplayObsolets = False
        Me.Xl_ProductPlugins1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductPlugins1.Filter = Nothing
        Me.Xl_ProductPlugins1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductPlugins1.MouseIsDown = False
        Me.Xl_ProductPlugins1.Name = "Xl_ProductPlugins1"
        Me.Xl_ProductPlugins1.ReadOnly = True
        Me.Xl_ProductPlugins1.Size = New System.Drawing.Size(599, 392)
        Me.Xl_ProductPlugins1.TabIndex = 1
        '
        'TabPageDownloads
        '
        Me.TabPageDownloads.Controls.Add(Me.Xl_ProductDownloads1)
        Me.TabPageDownloads.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDownloads.Name = "TabPageDownloads"
        Me.TabPageDownloads.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDownloads.Size = New System.Drawing.Size(599, 392)
        Me.TabPageDownloads.TabIndex = 6
        Me.TabPageDownloads.Text = "Descàrregues"
        Me.TabPageDownloads.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Filter = Nothing
        Me.Xl_ProductDownloads1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductDownloads1.Name = "Xl_ProductDownloads1"
        Me.Xl_ProductDownloads1.Size = New System.Drawing.Size(593, 386)
        Me.Xl_ProductDownloads1.TabIndex = 0
        '
        'TabPageRecursos
        '
        Me.TabPageRecursos.Controls.Add(Me.ProgressBarMediaResources)
        Me.TabPageRecursos.Controls.Add(Me.Xl_MediaResources1)
        Me.TabPageRecursos.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRecursos.Name = "TabPageRecursos"
        Me.TabPageRecursos.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRecursos.Size = New System.Drawing.Size(599, 392)
        Me.TabPageRecursos.TabIndex = 11
        Me.TabPageRecursos.Text = "Recursos"
        Me.TabPageRecursos.UseVisualStyleBackColor = True
        '
        'ProgressBarMediaResources
        '
        Me.ProgressBarMediaResources.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarMediaResources.Location = New System.Drawing.Point(3, 366)
        Me.ProgressBarMediaResources.Name = "ProgressBarMediaResources"
        Me.ProgressBarMediaResources.Size = New System.Drawing.Size(593, 23)
        Me.ProgressBarMediaResources.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarMediaResources.TabIndex = 1
        '
        'Xl_MediaResources1
        '
        Me.Xl_MediaResources1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_MediaResources1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_MediaResources1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_MediaResources1.Name = "Xl_MediaResources1"
        Me.Xl_MediaResources1.Size = New System.Drawing.Size(593, 386)
        Me.Xl_MediaResources1.TabIndex = 0
        '
        'TabPageLogistica
        '
        Me.TabPageLogistica.Controls.Add(Me.Xl_LookupCodiMercancia1)
        Me.TabPageLogistica.Controls.Add(Me.Label3)
        Me.TabPageLogistica.Location = New System.Drawing.Point(4, 22)
        Me.TabPageLogistica.Name = "TabPageLogistica"
        Me.TabPageLogistica.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLogistica.Size = New System.Drawing.Size(599, 392)
        Me.TabPageLogistica.TabIndex = 8
        Me.TabPageLogistica.Text = "Logística"
        Me.TabPageLogistica.UseVisualStyleBackColor = True
        '
        'Xl_LookupCodiMercancia1
        '
        Me.Xl_LookupCodiMercancia1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupCodiMercancia1.CodiMercancia = Nothing
        Me.Xl_LookupCodiMercancia1.IsDirty = False
        Me.Xl_LookupCodiMercancia1.Location = New System.Drawing.Point(93, 44)
        Me.Xl_LookupCodiMercancia1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_LookupCodiMercancia1.Name = "Xl_LookupCodiMercancia1"
        Me.Xl_LookupCodiMercancia1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCodiMercancia1.ReadOnlyLookup = False
        Me.Xl_LookupCodiMercancia1.Size = New System.Drawing.Size(404, 20)
        Me.Xl_LookupCodiMercancia1.TabIndex = 7
        Me.Xl_LookupCodiMercancia1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Codi arancelari"
        '
        'ContextMenuStripDownload
        '
        Me.ContextMenuStripDownload.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.ContextMenuStripDownload.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfegirToolStripMenuItem, Me.ZoomToolStripMenuItem})
        Me.ContextMenuStripDownload.Name = "ContextMenuStripDownload"
        Me.ContextMenuStripDownload.Size = New System.Drawing.Size(105, 48)
        '
        'AfegirToolStripMenuItem
        '
        Me.AfegirToolStripMenuItem.Name = "AfegirToolStripMenuItem"
        Me.AfegirToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.AfegirToolStripMenuItem.Text = "afegir"
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.ZoomToolStripMenuItem.Text = "zoom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(368, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Tarifa PVP"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(170, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Tarifa"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(50, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Cataleg vigent"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonObsoleto)
        Me.GroupBox1.Controls.Add(Me.RadioButtonEnLiquidacio)
        Me.GroupBox1.Controls.Add(Me.RadioButtonActiva)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 286)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(351, 45)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status"
        '
        'RadioButtonActiva
        '
        Me.RadioButtonActiva.AutoSize = True
        Me.RadioButtonActiva.Checked = True
        Me.RadioButtonActiva.Location = New System.Drawing.Point(57, 19)
        Me.RadioButtonActiva.Name = "RadioButtonActiva"
        Me.RadioButtonActiva.Size = New System.Drawing.Size(55, 17)
        Me.RadioButtonActiva.TabIndex = 0
        Me.RadioButtonActiva.TabStop = True
        Me.RadioButtonActiva.Text = "Activa"
        Me.RadioButtonActiva.UseVisualStyleBackColor = True
        '
        'RadioButtonEnLiquidacio
        '
        Me.RadioButtonEnLiquidacio.AutoSize = True
        Me.RadioButtonEnLiquidacio.Location = New System.Drawing.Point(129, 19)
        Me.RadioButtonEnLiquidacio.Name = "RadioButtonEnLiquidacio"
        Me.RadioButtonEnLiquidacio.Size = New System.Drawing.Size(85, 17)
        Me.RadioButtonEnLiquidacio.TabIndex = 1
        Me.RadioButtonEnLiquidacio.Text = "En liquidació"
        Me.RadioButtonEnLiquidacio.UseVisualStyleBackColor = True
        '
        'RadioButtonObsoleto
        '
        Me.RadioButtonObsoleto.AutoSize = True
        Me.RadioButtonObsoleto.Location = New System.Drawing.Point(231, 19)
        Me.RadioButtonObsoleto.Name = "RadioButtonObsoleto"
        Me.RadioButtonObsoleto.Size = New System.Drawing.Size(67, 17)
        Me.RadioButtonObsoleto.TabIndex = 2
        Me.RadioButtonObsoleto.Text = "Obsoleta"
        Me.RadioButtonObsoleto.UseVisualStyleBackColor = True
        '
        'Frm_Tpa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(614, 423)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Tpa"
        Me.Text = "MARCA COMERCIAL"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownWebatlasRafflesDeadline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownWebatlasDeadline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStps.ResumeLayout(False)
        Me.TabPageZonas.ResumeLayout(False)
        Me.TabPageReps.ResumeLayout(False)
        Me.TabPageReps.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPagePlugins.ResumeLayout(False)
        CType(Me.Xl_ProductPlugins1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDownloads.ResumeLayout(False)
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageRecursos.ResumeLayout(False)
        Me.TabPageLogistica.ResumeLayout(False)
        Me.TabPageLogistica.PerformLayout()
        Me.ContextMenuStripDownload.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactProveidor As Xl_Contact2
    Friend WithEvents Xl_Image_Logo As Xl_Image
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents LabelNum As System.Windows.Forms.Label
    Friend WithEvents TabPageStps As System.Windows.Forms.TabPage
    Friend WithEvents TabPageReps As System.Windows.Forms.TabPage
    Friend WithEvents ButtonMailTpaReps As System.Windows.Forms.Button
    Friend WithEvents CheckBoxOnlyEmpty As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxWebEnabledPro As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageDownloads As System.Windows.Forms.TabPage
    Friend WithEvents ContextMenuStripDownload As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AfegirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TabPageZonas As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxShowAtlas As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageLogistica As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonRepsExcel As System.Windows.Forms.Button
    Friend WithEvents Xl_ProductDownloads1 As Xl_ProductDownloads
    Friend WithEvents CheckBoxWebEnabledConsumer As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxDistribuidorsOficials As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageLogoDistribuidorOficial As Xl_Image
    Friend WithEvents Xl_BrandAreas1 As Xl_BrandAreas
    Friend WithEvents Xl_RepProductsxRep1 As Xl_RepProducts
    Friend WithEvents TabPageRecursos As TabPage
    Friend WithEvents Xl_MediaResources1 As Xl_MediaResources
    Friend WithEvents Xl_ProductChannels1 As Xl_ProductChannels
    Friend WithEvents Xl_ProductCategories1 As Xl_ProductCategories
    Friend WithEvents Xl_LookupCountryMadeIn As Xl_LookupCountry
    Friend WithEvents Label15 As Label
    Friend WithEvents Xl_LookupCodiMercancia1 As Xl_LookupCodiMercancia
    Friend WithEvents LabelWebatlasDeadline2 As Label
    Friend WithEvents NumericUpDownWebatlasDeadline As NumericUpDown
    Friend WithEvents LabelWebAtlasDeadline1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonDel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents LabelWebAtlasRafflesDeadline2 As Label
    Friend WithEvents NumericUpDownWebatlasRafflesDeadline As NumericUpDown
    Friend WithEvents LabelWebAtlasRafflesDeadline1 As Label
    Friend WithEvents ProgressBarMediaResources As ProgressBar
    Friend WithEvents ButtonShowLangTexts As Button
    Friend WithEvents TabPagePlugins As TabPage
    Friend WithEvents Xl_ProductPlugins1 As Xl_ProductPlugins
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonObsoleto As RadioButton
    Friend WithEvents RadioButtonEnLiquidacio As RadioButton
    Friend WithEvents RadioButtonActiva As RadioButton
End Class
