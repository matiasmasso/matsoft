<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Stp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Stp))
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxCodi = New System.Windows.Forms.ComboBox()
        Me.CheckBoxLaunchment = New System.Windows.Forms.CheckBox()
        Me.Xl_YearMonthLaunchment = New Mat.NET.Xl_YearMonth()
        Me.CheckBoxBloqEShops = New System.Windows.Forms.CheckBox()
        Me.TextBoxClauPrefix = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_CnapLookup1 = New Mat.NET.Xl_CnapLookup()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Image_Thumbnail = New Mat.NET.Xl_Image()
        Me.CheckBoxWebEnabledConsumer = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDscPropagateToChildren = New System.Windows.Forms.CheckBox()
        Me.TextBoxDscENG = New System.Windows.Forms.TextBox()
        Me.TextBoxDscCAT = New System.Windows.Forms.TextBox()
        Me.TextBoxDscESP = New System.Windows.Forms.TextBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.CheckBoxHideStatistics = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPortsUnit = New System.Windows.Forms.CheckBox()
        Me.Xl_Color1 = New Mat.NET.Xl_Color()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxNoStk = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWebEnabledPro = New System.Windows.Forms.CheckBox()
        Me.LabelTpa = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPageLogistica = New System.Windows.Forms.TabPage()
        Me.Xl_ArtLogistics1 = New Mat.NET.Xl_Art_Logistics()
        Me.TabPageFeatures = New System.Windows.Forms.TabPage()
        Me.Xl_ProductFeaturedImages1 = New Mat.NET.Xl_ProductFeaturedImages()
        Me.TabPageAccessories = New System.Windows.Forms.TabPage()
        Me.Xl_ArtsAccessories = New Mat.NET.Xl_Arts()
        Me.TabPageSpares = New System.Windows.Forms.TabPage()
        Me.Xl_ArtsSpares = New Mat.NET.Xl_Arts()
        Me.TabPageDownloads = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Mat.NET.Xl_ProductDownloads()
        Me.TabPageGaleria = New System.Windows.Forms.TabPage()
        Me.Xl_HighResImages1 = New Mat.NET.Xl_HighResImages()
        Me.TabPageVideos = New System.Windows.Forms.TabPage()
        Me.Xl_ProductMovies1 = New Mat.NET.Xl_ProductMovies()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductBlogs1 = New Mat.NET.Xl_ProductBlogs()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageLogistica.SuspendLayout()
        Me.TabPageFeatures.SuspendLayout()
        Me.TabPageAccessories.SuspendLayout()
        Me.TabPageSpares.SuspendLayout()
        Me.TabPageDownloads.SuspendLayout()
        Me.TabPageGaleria.SuspendLayout()
        Me.TabPageVideos.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(2, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 29
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageLogistica)
        Me.TabControl1.Controls.Add(Me.TabPageFeatures)
        Me.TabControl1.Controls.Add(Me.TabPageAccessories)
        Me.TabControl1.Controls.Add(Me.TabPageSpares)
        Me.TabControl1.Controls.Add(Me.TabPageDownloads)
        Me.TabControl1.Controls.Add(Me.TabPageGaleria)
        Me.TabControl1.Controls.Add(Me.TabPageVideos)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(3, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(608, 441)
        Me.TabControl1.TabIndex = 28
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Label6)
        Me.TabPageGral.Controls.Add(Me.ComboBoxCodi)
        Me.TabPageGral.Controls.Add(Me.CheckBoxLaunchment)
        Me.TabPageGral.Controls.Add(Me.Xl_YearMonthLaunchment)
        Me.TabPageGral.Controls.Add(Me.CheckBoxBloqEShops)
        Me.TabPageGral.Controls.Add(Me.TextBoxClauPrefix)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.Label11)
        Me.TabPageGral.Controls.Add(Me.Xl_CnapLookup1)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.Xl_Image_Thumbnail)
        Me.TabPageGral.Controls.Add(Me.CheckBoxWebEnabledConsumer)
        Me.TabPageGral.Controls.Add(Me.CheckBoxDscPropagateToChildren)
        Me.TabPageGral.Controls.Add(Me.TextBoxDscENG)
        Me.TabPageGral.Controls.Add(Me.TextBoxDscCAT)
        Me.TabPageGral.Controls.Add(Me.TextBoxDscESP)
        Me.TabPageGral.Controls.Add(Me.PictureBox3)
        Me.TabPageGral.Controls.Add(Me.PictureBox2)
        Me.TabPageGral.Controls.Add(Me.PictureBox1)
        Me.TabPageGral.Controls.Add(Me.CheckBoxHideStatistics)
        Me.TabPageGral.Controls.Add(Me.CheckBoxPortsUnit)
        Me.TabPageGral.Controls.Add(Me.Xl_Color1)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.CheckBoxNoStk)
        Me.TabPageGral.Controls.Add(Me.CheckBoxWebEnabledPro)
        Me.TabPageGral.Controls.Add(Me.LabelTpa)
        Me.TabPageGral.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPageGral.Controls.Add(Me.TextBoxNom)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Size = New System.Drawing.Size(600, 415)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "General"
        Me.TabPageGral.ToolTipText = "manual de instruccions"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 120
        Me.Label6.Text = "Codi:"
        '
        'ComboBoxCodi
        '
        Me.ComboBoxCodi.FormattingEnabled = True
        Me.ComboBoxCodi.Items.AddRange(New Object() {"Standard", "Accessoris", "Recanvis", "POS", "Altres"})
        Me.ComboBoxCodi.Location = New System.Drawing.Point(72, 89)
        Me.ComboBoxCodi.Name = "ComboBoxCodi"
        Me.ComboBoxCodi.Size = New System.Drawing.Size(178, 21)
        Me.ComboBoxCodi.TabIndex = 119
        '
        'CheckBoxLaunchment
        '
        Me.CheckBoxLaunchment.AutoSize = True
        Me.CheckBoxLaunchment.Location = New System.Drawing.Point(405, 196)
        Me.CheckBoxLaunchment.Name = "CheckBoxLaunchment"
        Me.CheckBoxLaunchment.Size = New System.Drawing.Size(81, 17)
        Me.CheckBoxLaunchment.TabIndex = 118
        Me.CheckBoxLaunchment.Text = "Llençament"
        Me.CheckBoxLaunchment.UseVisualStyleBackColor = True
        '
        'Xl_YearMonthLaunchment
        '
        Me.Xl_YearMonthLaunchment.Location = New System.Drawing.Point(487, 196)
        Me.Xl_YearMonthLaunchment.Name = "Xl_YearMonthLaunchment"
        Me.Xl_YearMonthLaunchment.Size = New System.Drawing.Size(100, 19)
        Me.Xl_YearMonthLaunchment.TabIndex = 117
        '
        'CheckBoxBloqEShops
        '
        Me.CheckBoxBloqEShops.Location = New System.Drawing.Point(405, 221)
        Me.CheckBoxBloqEShops.Name = "CheckBoxBloqEShops"
        Me.CheckBoxBloqEShops.Size = New System.Drawing.Size(145, 16)
        Me.CheckBoxBloqEShops.TabIndex = 115
        Me.CheckBoxBloqEShops.Text = "bloquejar botigues online"
        '
        'TextBoxClauPrefix
        '
        Me.TextBoxClauPrefix.Location = New System.Drawing.Point(487, 170)
        Me.TextBoxClauPrefix.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.TextBoxClauPrefix.Name = "TextBoxClauPrefix"
        Me.TextBoxClauPrefix.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxClauPrefix.TabIndex = 114
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(420, 172)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 113
        Me.Label4.Text = "Prefixe:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 62)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 112
        Me.Label11.Text = "Cnap:"
        '
        'Xl_CnapLookup1
        '
        Me.Xl_CnapLookup1.Cnap = Nothing
        Me.Xl_CnapLookup1.IsDirty = False
        Me.Xl_CnapLookup1.Location = New System.Drawing.Point(72, 62)
        Me.Xl_CnapLookup1.Name = "Xl_CnapLookup1"
        Me.Xl_CnapLookup1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_CnapLookup1.Size = New System.Drawing.Size(213, 20)
        Me.Xl_CnapLookup1.TabIndex = 111
        Me.Xl_CnapLookup1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 240)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 16)
        Me.Label3.TabIndex = 85
        Me.Label3.Text = "departament:"
        '
        'Xl_Image_Thumbnail
        '
        Me.Xl_Image_Thumbnail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image_Thumbnail.Bitmap = Nothing
        Me.Xl_Image_Thumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image_Thumbnail.EmptyImageLabelText = ""
        Me.Xl_Image_Thumbnail.IsDirty = False
        Me.Xl_Image_Thumbnail.Location = New System.Drawing.Point(487, 12)
        Me.Xl_Image_Thumbnail.MaxHeight = 0
        Me.Xl_Image_Thumbnail.MaxWidth = 0
        Me.Xl_Image_Thumbnail.Name = "Xl_Image_Thumbnail"
        Me.Xl_Image_Thumbnail.Size = New System.Drawing.Size(100, 130)
        Me.Xl_Image_Thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_Image_Thumbnail.TabIndex = 82
        Me.Xl_Image_Thumbnail.ZipStream = Nothing
        '
        'CheckBoxWebEnabledConsumer
        '
        Me.CheckBoxWebEnabledConsumer.Location = New System.Drawing.Point(72, 163)
        Me.CheckBoxWebEnabledConsumer.Name = "CheckBoxWebEnabledConsumer"
        Me.CheckBoxWebEnabledConsumer.Size = New System.Drawing.Size(178, 16)
        Me.CheckBoxWebEnabledConsumer.TabIndex = 81
        Me.CheckBoxWebEnabledConsumer.Text = "Publicar en web al consumidor"
        '
        'CheckBoxDscPropagateToChildren
        '
        Me.CheckBoxDscPropagateToChildren.AutoSize = True
        Me.CheckBoxDscPropagateToChildren.Location = New System.Drawing.Point(72, 125)
        Me.CheckBoxDscPropagateToChildren.Name = "CheckBoxDscPropagateToChildren"
        Me.CheckBoxDscPropagateToChildren.Size = New System.Drawing.Size(292, 17)
        Me.CheckBoxDscPropagateToChildren.TabIndex = 77
        Me.CheckBoxDscPropagateToChildren.Text = "propagar aquestes descripcions a categories i productes"
        Me.CheckBoxDscPropagateToChildren.UseVisualStyleBackColor = True
        '
        'TextBoxDscENG
        '
        Me.TextBoxDscENG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscENG.Location = New System.Drawing.Point(73, 333)
        Me.TextBoxDscENG.Multiline = True
        Me.TextBoxDscENG.Name = "TextBoxDscENG"
        Me.TextBoxDscENG.Size = New System.Drawing.Size(514, 39)
        Me.TextBoxDscENG.TabIndex = 75
        '
        'TextBoxDscCAT
        '
        Me.TextBoxDscCAT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscCAT.Location = New System.Drawing.Point(73, 298)
        Me.TextBoxDscCAT.Multiline = True
        Me.TextBoxDscCAT.Name = "TextBoxDscCAT"
        Me.TextBoxDscCAT.Size = New System.Drawing.Size(514, 39)
        Me.TextBoxDscCAT.TabIndex = 73
        '
        'TextBoxDscESP
        '
        Me.TextBoxDscESP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscESP.Location = New System.Drawing.Point(73, 262)
        Me.TextBoxDscESP.Multiline = True
        Me.TextBoxDscESP.Name = "TextBoxDscESP"
        Me.TextBoxDscESP.Size = New System.Drawing.Size(514, 39)
        Me.TextBoxDscESP.TabIndex = 71
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(11, 333)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 76
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(11, 298)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 74
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(11, 262)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 72
        Me.PictureBox1.TabStop = False
        '
        'CheckBoxHideStatistics
        '
        Me.CheckBoxHideStatistics.Location = New System.Drawing.Point(72, 218)
        Me.CheckBoxHideStatistics.Name = "CheckBoxHideStatistics"
        Me.CheckBoxHideStatistics.Size = New System.Drawing.Size(152, 16)
        Me.CheckBoxHideStatistics.TabIndex = 28
        Me.CheckBoxHideStatistics.Text = "Ocultar en estadisticas"
        '
        'CheckBoxPortsUnit
        '
        Me.CheckBoxPortsUnit.Location = New System.Drawing.Point(72, 182)
        Me.CheckBoxPortsUnit.Name = "CheckBoxPortsUnit"
        Me.CheckBoxPortsUnit.Size = New System.Drawing.Size(152, 16)
        Me.CheckBoxPortsUnit.TabIndex = 27
        Me.CheckBoxPortsUnit.Text = "Conta per ports"
        '
        'Xl_Color1
        '
        Me.Xl_Color1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Color1.Color = System.Drawing.Color.LightCyan
        Me.Xl_Color1.Location = New System.Drawing.Point(487, 148)
        Me.Xl_Color1.Name = "Xl_Color1"
        Me.Xl_Color1.Size = New System.Drawing.Size(100, 16)
        Me.Xl_Color1.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(420, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Color:"
        '
        'CheckBoxNoStk
        '
        Me.CheckBoxNoStk.Location = New System.Drawing.Point(72, 200)
        Me.CheckBoxNoStk.Name = "CheckBoxNoStk"
        Me.CheckBoxNoStk.Size = New System.Drawing.Size(152, 16)
        Me.CheckBoxNoStk.TabIndex = 24
        Me.CheckBoxNoStk.Text = "No stockable"
        '
        'CheckBoxWebEnabledPro
        '
        Me.CheckBoxWebEnabledPro.Location = New System.Drawing.Point(72, 144)
        Me.CheckBoxWebEnabledPro.Name = "CheckBoxWebEnabledPro"
        Me.CheckBoxWebEnabledPro.Size = New System.Drawing.Size(188, 16)
        Me.CheckBoxWebEnabledPro.TabIndex = 23
        Me.CheckBoxWebEnabledPro.Text = "Publicar en web al profesional"
        '
        'LabelTpa
        '
        Me.LabelTpa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTpa.Location = New System.Drawing.Point(72, 16)
        Me.LabelTpa.Name = "LabelTpa"
        Me.LabelTpa.Size = New System.Drawing.Size(178, 20)
        Me.LabelTpa.TabIndex = 21
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(511, 378)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(76, 16)
        Me.CheckBoxObsoleto.TabIndex = 20
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(72, 36)
        Me.TextBoxNom.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(178, 20)
        Me.TextBoxNom.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 40)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Categoría:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 18)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 16)
        Me.Label5.TabIndex = 79
        Me.Label5.Text = "Marca:"
        '
        'TabPageLogistica
        '
        Me.TabPageLogistica.Controls.Add(Me.Xl_ArtLogistics1)
        Me.TabPageLogistica.Location = New System.Drawing.Point(4, 22)
        Me.TabPageLogistica.Name = "TabPageLogistica"
        Me.TabPageLogistica.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLogistica.Size = New System.Drawing.Size(600, 415)
        Me.TabPageLogistica.TabIndex = 6
        Me.TabPageLogistica.Text = "Logistica"
        Me.TabPageLogistica.UseVisualStyleBackColor = True
        '
        'Xl_ArtLogistics1
        '
        Me.Xl_ArtLogistics1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ArtLogistics1.Location = New System.Drawing.Point(6, 6)
        Me.Xl_ArtLogistics1.Name = "Xl_ArtLogistics1"
        Me.Xl_ArtLogistics1.Size = New System.Drawing.Size(591, 403)
        Me.Xl_ArtLogistics1.TabIndex = 2
        '
        'TabPageFeatures
        '
        Me.TabPageFeatures.Controls.Add(Me.Xl_ProductFeaturedImages1)
        Me.TabPageFeatures.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFeatures.Name = "TabPageFeatures"
        Me.TabPageFeatures.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageFeatures.Size = New System.Drawing.Size(600, 415)
        Me.TabPageFeatures.TabIndex = 2
        Me.TabPageFeatures.Text = "Caracteristiques"
        Me.TabPageFeatures.UseVisualStyleBackColor = True
        '
        'Xl_ProductFeaturedImages1
        '
        Me.Xl_ProductFeaturedImages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductFeaturedImages1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductFeaturedImages1.Name = "Xl_ProductFeaturedImages1"
        Me.Xl_ProductFeaturedImages1.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ProductFeaturedImages1.TabIndex = 0
        '
        'TabPageAccessories
        '
        Me.TabPageAccessories.Controls.Add(Me.Xl_ArtsAccessories)
        Me.TabPageAccessories.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAccessories.Name = "TabPageAccessories"
        Me.TabPageAccessories.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAccessories.Size = New System.Drawing.Size(600, 415)
        Me.TabPageAccessories.TabIndex = 3
        Me.TabPageAccessories.Text = "Accessoris"
        Me.TabPageAccessories.UseVisualStyleBackColor = True
        '
        'Xl_ArtsAccessories
        '
        Me.Xl_ArtsAccessories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ArtsAccessories.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ArtsAccessories.Name = "Xl_ArtsAccessories"
        Me.Xl_ArtsAccessories.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ArtsAccessories.TabIndex = 1
        '
        'TabPageSpares
        '
        Me.TabPageSpares.Controls.Add(Me.Xl_ArtsSpares)
        Me.TabPageSpares.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSpares.Name = "TabPageSpares"
        Me.TabPageSpares.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSpares.Size = New System.Drawing.Size(600, 415)
        Me.TabPageSpares.TabIndex = 4
        Me.TabPageSpares.Text = "Recanvis"
        Me.TabPageSpares.UseVisualStyleBackColor = True
        '
        'Xl_ArtsSpares
        '
        Me.Xl_ArtsSpares.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ArtsSpares.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ArtsSpares.Name = "Xl_ArtsSpares"
        Me.Xl_ArtsSpares.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ArtsSpares.TabIndex = 0
        '
        'TabPageDownloads
        '
        Me.TabPageDownloads.Controls.Add(Me.Xl_ProductDownloads1)
        Me.TabPageDownloads.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDownloads.Name = "TabPageDownloads"
        Me.TabPageDownloads.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDownloads.Size = New System.Drawing.Size(600, 415)
        Me.TabPageDownloads.TabIndex = 5
        Me.TabPageDownloads.Text = "Descarregues"
        Me.TabPageDownloads.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductDownloads1.Name = "Xl_ProductDownloads1"
        Me.Xl_ProductDownloads1.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ProductDownloads1.TabIndex = 1
        '
        'TabPageGaleria
        '
        Me.TabPageGaleria.Controls.Add(Me.Xl_HighResImages1)
        Me.TabPageGaleria.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGaleria.Name = "TabPageGaleria"
        Me.TabPageGaleria.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGaleria.Size = New System.Drawing.Size(600, 415)
        Me.TabPageGaleria.TabIndex = 9
        Me.TabPageGaleria.Text = "Galería"
        Me.TabPageGaleria.UseVisualStyleBackColor = True
        '
        'Xl_HighResImages1
        '
        Me.Xl_HighResImages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_HighResImages1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_HighResImages1.Name = "Xl_HighResImages1"
        Me.Xl_HighResImages1.Size = New System.Drawing.Size(594, 409)
        Me.Xl_HighResImages1.TabIndex = 0
        '
        'TabPageVideos
        '
        Me.TabPageVideos.Controls.Add(Me.Xl_ProductMovies1)
        Me.TabPageVideos.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVideos.Name = "TabPageVideos"
        Me.TabPageVideos.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVideos.Size = New System.Drawing.Size(600, 415)
        Me.TabPageVideos.TabIndex = 7
        Me.TabPageVideos.Text = "Videos"
        Me.TabPageVideos.UseVisualStyleBackColor = True
        '
        'Xl_ProductMovies1
        '
        Me.Xl_ProductMovies1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductMovies1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductMovies1.Name = "Xl_ProductMovies1"
        Me.Xl_ProductMovies1.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ProductMovies1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_ProductBlogs1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(600, 415)
        Me.TabPage1.TabIndex = 8
        Me.TabPage1.Text = "Blogs"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_ProductBlogs1
        '
        Me.Xl_ProductBlogs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductBlogs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductBlogs1.Name = "Xl_ProductBlogs1"
        Me.Xl_ProductBlogs1.Size = New System.Drawing.Size(594, 409)
        Me.Xl_ProductBlogs1.TabIndex = 0
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(507, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 27
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonCancel.Location = New System.Drawing.Point(396, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 26
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 459)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(618, 31)
        Me.Panel1.TabIndex = 41
        '
        'Frm_Stp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 490)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Stp"
        Me.Text = "Categoría d'articles"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageLogistica.ResumeLayout(False)
        Me.TabPageFeatures.ResumeLayout(False)
        Me.TabPageAccessories.ResumeLayout(False)
        Me.TabPageSpares.ResumeLayout(False)
        Me.TabPageDownloads.ResumeLayout(False)
        Me.TabPageGaleria.ResumeLayout(False)
        Me.TabPageVideos.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxDscPropagateToChildren As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxDscENG As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDscCAT As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDscESP As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxHideStatistics As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPortsUnit As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Color1 As Xl_Color
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoStk As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxWebEnabledPro As System.Windows.Forms.CheckBox
    Friend WithEvents LabelTpa As System.Windows.Forms.Label
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CheckBoxWebEnabledConsumer As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Image_Thumbnail As Xl_Image
    Friend WithEvents TabPageFeatures As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAccessories As System.Windows.Forms.TabPage
    Friend WithEvents TabPageSpares As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtsAccessories As Xl_Arts
    Friend WithEvents Xl_ArtsSpares As Xl_Arts
    Friend WithEvents TabPageDownloads As System.Windows.Forms.TabPage
    Friend WithEvents TabPageLogistica As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtLogistics1 As Xl_Art_Logistics
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_CnapLookup1 As Xl_CnapLookup
    Friend WithEvents Xl_ProductDownloads1 As Xl_ProductDownloads
    Friend WithEvents TabPageVideos As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ProductMovies1 As Xl_ProductMovies
    Friend WithEvents TextBoxClauPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxBloqEShops As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ProductBlogs1 As Xl_ProductBlogs
    Friend WithEvents CheckBoxLaunchment As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_YearMonthLaunchment As Xl_YearMonth
    Friend WithEvents TabPageGaleria As System.Windows.Forms.TabPage
    Friend WithEvents Xl_HighResImages1 As Mat.NET.Xl_HighResImages
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCodi As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_ProductFeaturedImages1 As Mat.NET.Xl_ProductFeaturedImages
End Class
