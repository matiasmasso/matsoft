<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Youtube
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxYoutubeId = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_LangSet1 = New Mat.Net.Xl_LangSet()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateTimePickerDuration = New System.Windows.Forms.DateTimePicker()
        Me.Xl_LangTextShortExcerpt = New Mat.Net.Xl_LangTextShort()
        Me.Xl_LangTextShortNom = New Mat.Net.Xl_LangTextShort()
        Me.Xl_Langs1 = New Mat.Net.Xl_Langs()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProduct1 = New Mat.Net.Xl_LookupProduct()
        Me.Xl_Products1 = New Mat.Net.Xl_Products()
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ImageMime1 = New Mat.Net.Xl_ImageMime()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ImageMime1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nom:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 481)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(571, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(352, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(463, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 8
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
        'TextBoxYoutubeId
        '
        Me.TextBoxYoutubeId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxYoutubeId.Location = New System.Drawing.Point(443, 8)
        Me.TextBoxYoutubeId.Name = "TextBoxYoutubeId"
        Me.TextBoxYoutubeId.Size = New System.Drawing.Size(111, 20)
        Me.TextBoxYoutubeId.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(375, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Youtube Id:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Descripció:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1, 239)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "Productes:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(571, 432)
        Me.TabControl1.TabIndex = 72
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LangSet1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.DateTimePickerDuration)
        Me.TabPage1.Controls.Add(Me.Xl_LangTextShortExcerpt)
        Me.TabPage1.Controls.Add(Me.Xl_LangTextShortNom)
        Me.TabPage1.Controls.Add(Me.Xl_Langs1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPage1.Controls.Add(Me.Xl_LookupProduct1)
        Me.TabPage1.Controls.Add(Me.Xl_Products1)
        Me.TabPage1.Controls.Add(Me.ButtonAddProduct)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxYoutubeId)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(563, 406)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_LangSet1
        '
        Me.Xl_LangSet1.Location = New System.Drawing.Point(148, 48)
        Me.Xl_LangSet1.Name = "Xl_LangSet1"
        Me.Xl_LangSet1.Size = New System.Drawing.Size(176, 24)
        Me.Xl_LangSet1.TabIndex = 80
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 79
        Me.Label6.Text = "Durada:"
        '
        'DateTimePickerDuration
        '
        Me.DateTimePickerDuration.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePickerDuration.Location = New System.Drawing.Point(71, 181)
        Me.DateTimePickerDuration.Name = "DateTimePickerDuration"
        Me.DateTimePickerDuration.Size = New System.Drawing.Size(83, 20)
        Me.DateTimePickerDuration.TabIndex = 74
        Me.DateTimePickerDuration.Value = New Date(2023, 4, 5, 0, 0, 0, 0)
        '
        'Xl_LangTextShortExcerpt
        '
        Me.Xl_LangTextShortExcerpt.IsDirty = False
        Me.Xl_LangTextShortExcerpt.Location = New System.Drawing.Point(68, 145)
        Me.Xl_LangTextShortExcerpt.Name = "Xl_LangTextShortExcerpt"
        Me.Xl_LangTextShortExcerpt.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LangTextShortExcerpt.ReadOnlyLookup = False
        Me.Xl_LangTextShortExcerpt.Size = New System.Drawing.Size(484, 20)
        Me.Xl_LangTextShortExcerpt.TabIndex = 78
        Me.Xl_LangTextShortExcerpt.Value = Nothing
        '
        'Xl_LangTextShortNom
        '
        Me.Xl_LangTextShortNom.IsDirty = False
        Me.Xl_LangTextShortNom.Location = New System.Drawing.Point(68, 119)
        Me.Xl_LangTextShortNom.Name = "Xl_LangTextShortNom"
        Me.Xl_LangTextShortNom.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LangTextShortNom.ReadOnlyLookup = False
        Me.Xl_LangTextShortNom.Size = New System.Drawing.Size(484, 20)
        Me.Xl_LangTextShortNom.TabIndex = 77
        Me.Xl_LangTextShortNom.Value = Nothing
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(68, 51)
        Me.Xl_Langs1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 1
        Me.Xl_Langs1.Value = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(0, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Idioma:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(492, 34)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxObsoleto.TabIndex = 7
        Me.CheckBoxObsoleto.Text = "Obsolet"
        Me.CheckBoxObsoleto.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(70, 235)
        Me.Xl_LookupProduct1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.SelectionMode = DTO.DTOProduct.SelectionModes.SelectMany
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(432, 20)
        Me.Xl_LookupProduct1.TabIndex = 4
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_Products1
        '
        Me.Xl_Products1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Products1.Location = New System.Drawing.Point(71, 262)
        Me.Xl_Products1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Xl_Products1.Name = "Xl_Products1"
        Me.Xl_Products1.Size = New System.Drawing.Size(483, 139)
        Me.Xl_Products1.TabIndex = 6
        '
        'ButtonAddProduct
        '
        Me.ButtonAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddProduct.Enabled = False
        Me.ButtonAddProduct.Location = New System.Drawing.Point(507, 234)
        Me.ButtonAddProduct.Name = "ButtonAddProduct"
        Me.ButtonAddProduct.Size = New System.Drawing.Size(45, 22)
        Me.ButtonAddProduct.TabIndex = 5
        Me.ButtonAddProduct.Text = "afegir"
        Me.ButtonAddProduct.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ImageMime1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(563, 406)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Portada"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ImageMime1
        '
        Me.Xl_ImageMime1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageMime1.Location = New System.Drawing.Point(2, 0)
        Me.Xl_ImageMime1.Name = "Xl_ImageMime1"
        Me.Xl_ImageMime1.Size = New System.Drawing.Size(558, 248)
        Me.Xl_ImageMime1.TabIndex = 0
        Me.Xl_ImageMime1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(571, 24)
        Me.MenuStrip1.TabIndex = 73
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlayToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'PlayToolStripMenuItem
        '
        Me.PlayToolStripMenuItem.Name = "PlayToolStripMenuItem"
        Me.PlayToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.PlayToolStripMenuItem.Text = "Play"
        '
        'Frm_Youtube
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 512)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Youtube"
        Me.Text = "VIDEO YOUTUBE"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ImageMime1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxYoutubeId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Xl_Products1 As Xl_Products
    Friend WithEvents ButtonAddProduct As Button
    Friend WithEvents CheckBoxObsoleto As CheckBox
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents Label5 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_LangTextShortNom As Xl_LangTextShort
    Friend WithEvents Xl_LangTextShortExcerpt As Xl_LangTextShort
    Friend WithEvents Xl_ImageMime1 As Xl_ImageMime
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlayToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label6 As Label
    Friend WithEvents DateTimePickerDuration As DateTimePicker
    Friend WithEvents Xl_LangSet1 As Xl_LangSet
End Class
