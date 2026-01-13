<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Noticia
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxTitol = New System.Windows.Forms.TextBox()
        Me.TextBoxText = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxUrlRoot = New System.Windows.Forms.TextBox()
        Me.CheckBoxVisible = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPriority = New System.Windows.Forms.CheckBox()
        Me.Xl_Image265x150 = New Mat.NET.Xl_Image()
        Me.TextBoxExcerpt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxProfessional = New System.Windows.Forms.CheckBox()
        Me.Xl_CategoriasDeNoticia1 = New Mat.NET.Xl_CategoriasDeNoticia()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        Me.Xl_Keywords1 = New Mat.NET.Xl_Keywords()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBoxEvento = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_Location1 = New Mat.NET.Xl_Lookup_Location()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxEvento.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "producte"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "titol"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 446)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "contingut:"
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(629, 12)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFch.TabIndex = 5
        '
        'TextBoxTitol
        '
        Me.TextBoxTitol.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitol.Location = New System.Drawing.Point(139, 65)
        Me.TextBoxTitol.MaxLength = 150
        Me.TextBoxTitol.Name = "TextBoxTitol"
        Me.TextBoxTitol.Size = New System.Drawing.Size(586, 20)
        Me.TextBoxTitol.TabIndex = 9
        '
        'TextBoxText
        '
        Me.TextBoxText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxText.Location = New System.Drawing.Point(32, 462)
        Me.TextBoxText.Multiline = True
        Me.TextBoxText.Name = "TextBoxText"
        Me.TextBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxText.Size = New System.Drawing.Size(694, 225)
        Me.TextBoxText.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 693)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(737, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(518, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(629, 4)
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
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(339, 39)
        Me.TextBoxUrl.MaxLength = 150
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(386, 20)
        Me.TextBoxUrl.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Url"
        '
        'TextBoxUrlRoot
        '
        Me.TextBoxUrlRoot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrlRoot.Enabled = False
        Me.TextBoxUrlRoot.Location = New System.Drawing.Point(139, 39)
        Me.TextBoxUrlRoot.Name = "TextBoxUrlRoot"
        Me.TextBoxUrlRoot.ReadOnly = True
        Me.TextBoxUrlRoot.Size = New System.Drawing.Size(202, 20)
        Me.TextBoxUrlRoot.TabIndex = 48
        Me.TextBoxUrlRoot.Text = "http://www.matiasmasso.es/news/"
        Me.TextBoxUrlRoot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxVisible
        '
        Me.CheckBoxVisible.AutoSize = True
        Me.CheckBoxVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxVisible.Checked = True
        Me.CheckBoxVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxVisible.Location = New System.Drawing.Point(670, 214)
        Me.CheckBoxVisible.Name = "CheckBoxVisible"
        Me.CheckBoxVisible.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxVisible.TabIndex = 49
        Me.CheckBoxVisible.Text = "Visible"
        Me.CheckBoxVisible.UseVisualStyleBackColor = True
        '
        'CheckBoxPriority
        '
        Me.CheckBoxPriority.AutoSize = True
        Me.CheckBoxPriority.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxPriority.Location = New System.Drawing.Point(663, 168)
        Me.CheckBoxPriority.Name = "CheckBoxPriority"
        Me.CheckBoxPriority.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxPriority.TabIndex = 50
        Me.CheckBoxPriority.Text = "Prioritari"
        Me.CheckBoxPriority.UseVisualStyleBackColor = True
        '
        'Xl_Image265x150
        '
        Me.Xl_Image265x150.Bitmap = Nothing
        Me.Xl_Image265x150.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image265x150.EmptyImageLabelText = ""
        Me.Xl_Image265x150.IsDirty = False
        Me.Xl_Image265x150.Location = New System.Drawing.Point(33, 166)
        Me.Xl_Image265x150.MaxHeight = 0
        Me.Xl_Image265x150.MaxWidth = 0
        Me.Xl_Image265x150.Name = "Xl_Image265x150"
        Me.Xl_Image265x150.Size = New System.Drawing.Size(265, 150)
        Me.Xl_Image265x150.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_Image265x150.TabIndex = 51
        Me.Xl_Image265x150.ZipStream = Nothing
        '
        'TextBoxExcerpt
        '
        Me.TextBoxExcerpt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExcerpt.Location = New System.Drawing.Point(31, 338)
        Me.TextBoxExcerpt.Multiline = True
        Me.TextBoxExcerpt.Name = "TextBoxExcerpt"
        Me.TextBoxExcerpt.Size = New System.Drawing.Size(694, 105)
        Me.TextBoxExcerpt.TabIndex = 55
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 322)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "extracte:"
        '
        'CheckBoxProfessional
        '
        Me.CheckBoxProfessional.AutoSize = True
        Me.CheckBoxProfessional.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxProfessional.Location = New System.Drawing.Point(643, 191)
        Me.CheckBoxProfessional.Name = "CheckBoxProfessional"
        Me.CheckBoxProfessional.Size = New System.Drawing.Size(83, 17)
        Me.CheckBoxProfessional.TabIndex = 56
        Me.CheckBoxProfessional.Text = "Professional"
        Me.CheckBoxProfessional.UseVisualStyleBackColor = True
        '
        'Xl_CategoriasDeNoticia1
        '
        Me.Xl_CategoriasDeNoticia1.Location = New System.Drawing.Point(304, 166)
        Me.Xl_CategoriasDeNoticia1.Name = "Xl_CategoriasDeNoticia1"
        Me.Xl_CategoriasDeNoticia1.Size = New System.Drawing.Size(156, 162)
        Me.Xl_CategoriasDeNoticia1.TabIndex = 57
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(139, 11)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(483, 20)
        Me.Xl_LookupProduct1.TabIndex = 58
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_Keywords1
        '
        Me.Xl_Keywords1.Location = New System.Drawing.Point(467, 166)
        Me.Xl_Keywords1.Name = "Xl_Keywords1"
        Me.Xl_Keywords1.Size = New System.Drawing.Size(171, 150)
        Me.Xl_Keywords1.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "imatge (265x150)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(301, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "categoría:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(464, 152)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "keywords:"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Noticia", "Evento", "Sabías Que..."})
        Me.ComboBoxCod.Location = New System.Drawing.Point(139, 92)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(157, 21)
        Me.ComboBoxCod.TabIndex = 63
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(163, 11)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFchFrom.TabIndex = 64
        '
        'GroupBoxEvento
        '
        Me.GroupBoxEvento.Controls.Add(Me.Xl_Lookup_Location1)
        Me.GroupBoxEvento.Controls.Add(Me.Label11)
        Me.GroupBoxEvento.Controls.Add(Me.Label10)
        Me.GroupBoxEvento.Controls.Add(Me.DateTimePickerFchTo)
        Me.GroupBoxEvento.Controls.Add(Me.Label9)
        Me.GroupBoxEvento.Controls.Add(Me.DateTimePickerFchFrom)
        Me.GroupBoxEvento.Location = New System.Drawing.Point(304, 92)
        Me.GroupBoxEvento.Name = "GroupBoxEvento"
        Me.GroupBoxEvento.Size = New System.Drawing.Size(422, 57)
        Me.GroupBoxEvento.TabIndex = 65
        Me.GroupBoxEvento.TabStop = False
        Me.GroupBoxEvento.Text = "evento"
        Me.GroupBoxEvento.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 38)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 13)
        Me.Label11.TabIndex = 69
        Me.Label11.Text = "localitat"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(291, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 13)
        Me.Label10.TabIndex = 67
        Me.Label10.Text = "fins"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(320, 11)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFchTo.TabIndex = 66
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(118, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 65
        Me.Label9.Text = "des de"
        '
        'Xl_Lookup_Location1
        '
        Me.Xl_Lookup_Location1.IsDirty = False
        Me.Xl_Lookup_Location1.Location = New System.Drawing.Point(56, 35)
        Me.Xl_Lookup_Location1.LocationValue = Nothing
        Me.Xl_Lookup_Location1.Name = "Xl_Lookup_Location1"
        Me.Xl_Lookup_Location1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Location1.Size = New System.Drawing.Size(360, 20)
        Me.Xl_Lookup_Location1.TabIndex = 70
        Me.Xl_Lookup_Location1.Value = Nothing
        '
        'Frm_Noticia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 724)
        Me.Controls.Add(Me.GroupBoxEvento)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Keywords1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_CategoriasDeNoticia1)
        Me.Controls.Add(Me.CheckBoxProfessional)
        Me.Controls.Add(Me.TextBoxExcerpt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_Image265x150)
        Me.Controls.Add(Me.CheckBoxPriority)
        Me.Controls.Add(Me.CheckBoxVisible)
        Me.Controls.Add(Me.TextBoxUrl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxText)
        Me.Controls.Add(Me.TextBoxTitol)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxUrlRoot)
        Me.Name = "Frm_Noticia"
        Me.Text = "NOTICIA / NOTA DE PREMSA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxEvento.ResumeLayout(False)
        Me.GroupBoxEvento.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxTitol As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxText As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxUrlRoot As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxVisible As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPriority As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Image265x150 As Mat.NET.Xl_Image
    Friend WithEvents TextBoxExcerpt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxProfessional As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_CategoriasDeNoticia1 As Mat.NET.Xl_CategoriasDeNoticia
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
    Friend WithEvents Xl_Keywords1 As Mat.NET.Xl_Keywords
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBoxEvento As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_Location1 As Mat.NET.Xl_Lookup_Location
End Class
