<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MediaResource
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ComboBoxCods = New System.Windows.Forms.ComboBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStripPictureBox = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ImportarImatgeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarImatgeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarHashToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyGuidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_UsrLog1 = New Mat.Net.Xl_UsrLog()
        Me.Xl_Langs1 = New Mat.Net.Xl_Langs()
        Me.Xl_LookupProduct1 = New Mat.Net.Xl_LookupProduct()
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.Xl_Products1 = New Mat.Net.Xl_Products()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxFeatures = New System.Windows.Forms.TextBox()
        Me.LabelPictureboxInfo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxPor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Xl_LangSet1 = New Mat.Net.Xl_LangSet()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripPictureBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 497)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(457, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(238, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 8
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(349, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 7
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.TabIndex = 9
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ComboBoxCods
        '
        Me.ComboBoxCods.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCods.FormattingEnabled = True
        Me.ComboBoxCods.Location = New System.Drawing.Point(79, 78)
        Me.ComboBoxCods.Name = "ComboBoxCods"
        Me.ComboBoxCods.Size = New System.Drawing.Size(228, 21)
        Me.ComboBoxCods.TabIndex = 2
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(79, 52)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(228, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Nom"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Classificació"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.ContextMenuStrip = Me.ContextMenuStripPictureBox
        Me.PictureBox1.Location = New System.Drawing.Point(313, 52)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(140, 140)
        Me.PictureBox1.TabIndex = 52
        Me.PictureBox1.TabStop = False
        '
        'ContextMenuStripPictureBox
        '
        Me.ContextMenuStripPictureBox.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarImatgeToolStripMenuItem, Me.ExportarImatgeToolStripMenuItem, Me.CopiarLinkToolStripMenuItem, Me.CopiarHashToolStripMenuItem})
        Me.ContextMenuStripPictureBox.Name = "ContextMenuStripPictureBox"
        Me.ContextMenuStripPictureBox.Size = New System.Drawing.Size(180, 92)
        '
        'ImportarImatgeToolStripMenuItem
        '
        Me.ImportarImatgeToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.upload_16
        Me.ImportarImatgeToolStripMenuItem.Name = "ImportarImatgeToolStripMenuItem"
        Me.ImportarImatgeToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.ImportarImatgeToolStripMenuItem.Text = "Importar imatge"
        '
        'ExportarImatgeToolStripMenuItem
        '
        Me.ExportarImatgeToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.download_16
        Me.ExportarImatgeToolStripMenuItem.Name = "ExportarImatgeToolStripMenuItem"
        Me.ExportarImatgeToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.ExportarImatgeToolStripMenuItem.Text = "Exportar imatge"
        '
        'CopiarLinkToolStripMenuItem
        '
        Me.CopiarLinkToolStripMenuItem.Name = "CopiarLinkToolStripMenuItem"
        Me.CopiarLinkToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopiarLinkToolStripMenuItem.Text = "Copiar enllaç"
        '
        'CopiarHashToolStripMenuItem
        '
        Me.CopiarHashToolStripMenuItem.Name = "CopiarHashToolStripMenuItem"
        Me.CopiarHashToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopiarHashToolStripMenuItem.Text = "Copiar identificador"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(97, 135)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxObsoleto.TabIndex = 4
        Me.CheckBoxObsoleto.Text = "Obsolet"
        Me.CheckBoxObsoleto.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Idioma"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(457, 24)
        Me.MenuStrip1.TabIndex = 57
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarToolStripMenuItem, Me.DownloadToolStripMenuItem, Me.CopyLinkToolStripMenuItem, Me.CopyGuidToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.upload_16
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.ImportarToolStripMenuItem.Text = "Importar"
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.download_16
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.DownloadToolStripMenuItem.Text = "Exportar"
        '
        'CopyLinkToolStripMenuItem
        '
        Me.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem"
        Me.CopyLinkToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopyLinkToolStripMenuItem.Text = "Copiar enllaç"
        '
        'CopyGuidToolStripMenuItem
        '
        Me.CopyGuidToolStripMenuItem.Name = "CopyGuidToolStripMenuItem"
        Me.CopyGuidToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopyGuidToolStripMenuItem.Text = "Copiar identificador"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(0, 477)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.ReadOnly = True
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(457, 20)
        Me.Xl_UsrLog1.TabIndex = 56
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(79, 106)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(48, 21)
        Me.Xl_Langs1.TabIndex = 3
        Me.Xl_Langs1.Value = Nothing
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(17, 345)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.SelectionMode = DTO.DTOProduct.SelectionModes.SelectAny
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(355, 20)
        Me.Xl_LookupProduct1.TabIndex = 59
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'ButtonAddProduct
        '
        Me.ButtonAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddProduct.Location = New System.Drawing.Point(378, 345)
        Me.ButtonAddProduct.Name = "ButtonAddProduct"
        Me.ButtonAddProduct.Size = New System.Drawing.Size(75, 20)
        Me.ButtonAddProduct.TabIndex = 60
        Me.ButtonAddProduct.Text = "Afegir"
        Me.ButtonAddProduct.UseVisualStyleBackColor = True
        '
        'Xl_Products1
        '
        Me.Xl_Products1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Products1.Location = New System.Drawing.Point(17, 371)
        Me.Xl_Products1.Name = "Xl_Products1"
        Me.Xl_Products1.Size = New System.Drawing.Size(436, 100)
        Me.Xl_Products1.TabIndex = 61
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 329)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Productes:"
        '
        'TextBoxFeatures
        '
        Me.TextBoxFeatures.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFeatures.Location = New System.Drawing.Point(313, 193)
        Me.TextBoxFeatures.Name = "TextBoxFeatures"
        Me.TextBoxFeatures.ReadOnly = True
        Me.TextBoxFeatures.Size = New System.Drawing.Size(140, 20)
        Me.TextBoxFeatures.TabIndex = 63
        '
        'LabelPictureboxInfo
        '
        Me.LabelPictureboxInfo.Location = New System.Drawing.Point(316, 109)
        Me.LabelPictureboxInfo.Name = "LabelPictureboxInfo"
        Me.LabelPictureboxInfo.Size = New System.Drawing.Size(134, 43)
        Me.LabelPictureboxInfo.TabIndex = 64
        Me.LabelPictureboxInfo.Text = "Arrossega una imatge aquí o impórtala amb el menú"
        Me.LabelPictureboxInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 233)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 66
        Me.Label4.Text = "Esp"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(79, 230)
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(374, 20)
        Me.TextBoxEsp.TabIndex = 65
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 254)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "Cat"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCat.Location = New System.Drawing.Point(79, 251)
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(374, 20)
        Me.TextBoxCat.TabIndex = 67
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 295)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 13)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Por"
        '
        'TextBoxPor
        '
        Me.TextBoxPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPor.Location = New System.Drawing.Point(79, 292)
        Me.TextBoxPor.Name = "TextBoxPor"
        Me.TextBoxPor.Size = New System.Drawing.Size(374, 20)
        Me.TextBoxPor.TabIndex = 71
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 274)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 70
        Me.Label8.Text = "Eng"
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEng.Location = New System.Drawing.Point(79, 271)
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(374, 20)
        Me.TextBoxEng.TabIndex = 69
        '
        'Xl_LangSet1
        '
        Me.Xl_LangSet1.Location = New System.Drawing.Point(133, 105)
        Me.Xl_LangSet1.Name = "Xl_LangSet1"
        Me.Xl_LangSet1.Size = New System.Drawing.Size(176, 24)
        Me.Xl_LangSet1.TabIndex = 73
        '
        'Frm_MediaResource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 528)
        Me.Controls.Add(Me.Xl_LangSet1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxPor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxEng)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxCat)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxEsp)
        Me.Controls.Add(Me.LabelPictureboxInfo)
        Me.Controls.Add(Me.TextBoxFeatures)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Products1)
        Me.Controls.Add(Me.ButtonAddProduct)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_UsrLog1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.ComboBoxCods)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_MediaResource"
        Me.Text = "Recurs"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripPictureBox.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents ComboBoxCods As ComboBox
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents CheckBoxObsoleto As CheckBox
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents Label5 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyGuidToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents CopyLinkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DownloadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents ButtonAddProduct As Button
    Friend WithEvents Xl_Products1 As Xl_Products
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxFeatures As TextBox
    Friend WithEvents ImportarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStripPictureBox As ContextMenuStrip
    Friend WithEvents ImportarImatgeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarImatgeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopiarLinkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopiarHashToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LabelPictureboxInfo As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxEsp As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxCat As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxPor As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxEng As TextBox
    Friend WithEvents Xl_LangSet1 As Xl_LangSet
End Class
