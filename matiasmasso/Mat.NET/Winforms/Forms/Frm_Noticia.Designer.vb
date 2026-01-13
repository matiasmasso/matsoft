<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Noticia
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxUrlRoot = New System.Windows.Forms.TextBox()
        Me.CheckBoxVisible = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPriority = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProfessional = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.GroupBoxEvento = New System.Windows.Forms.GroupBox()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DateTimePickerDestacarFrom = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxDestacar = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DateTimePickerDestacarTo = New System.Windows.Forms.DateTimePicker()
        Me.GroupBoxDestacar = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxVisitCount = New System.Windows.Forms.TextBox()
        Me.Xl_DistributionChannels_Checklist1 = New Winforms.Xl_DistributionChannels_Checklist()
        Me.Xl_Keywords1 = New Winforms.Xl_Keywords()
        Me.Xl_Image265x150 = New Winforms.Xl_Image()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Xl_CategoriasDeNoticia1 = New Winforms.Xl_CategoriasDeNoticia()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextTitol = New Winforms.Xl_LangsText()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextExcerpt = New Winforms.Xl_LangsText()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextContingut = New Winforms.Xl_LangsText()
        Me.Xl_UsrLog1 = New Winforms.Xl_UsrLog()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxEvento.SuspendLayout()
        Me.GroupBoxDestacar.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_DistributionChannels_Checklist1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "producte"
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(611, 28)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFch.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 550)
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
        Me.TextBoxUrl.Location = New System.Drawing.Point(321, 55)
        Me.TextBoxUrl.MaxLength = 150
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(386, 20)
        Me.TextBoxUrl.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 58)
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
        Me.TextBoxUrlRoot.Location = New System.Drawing.Point(121, 55)
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
        Me.CheckBoxVisible.Location = New System.Drawing.Point(648, 362)
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
        Me.CheckBoxPriority.Location = New System.Drawing.Point(641, 316)
        Me.CheckBoxPriority.Name = "CheckBoxPriority"
        Me.CheckBoxPriority.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxPriority.TabIndex = 50
        Me.CheckBoxPriority.Text = "Prioritari"
        Me.CheckBoxPriority.UseVisualStyleBackColor = True
        '
        'CheckBoxProfessional
        '
        Me.CheckBoxProfessional.AutoSize = True
        Me.CheckBoxProfessional.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxProfessional.Location = New System.Drawing.Point(335, 108)
        Me.CheckBoxProfessional.Name = "CheckBoxProfessional"
        Me.CheckBoxProfessional.Size = New System.Drawing.Size(83, 17)
        Me.CheckBoxProfessional.TabIndex = 56
        Me.CheckBoxProfessional.Text = "Professional"
        Me.CheckBoxProfessional.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 300)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "imatge (265x150)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(279, 300)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "categoría:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(442, 300)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "keywords:"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Noticia", "Evento", "Sabías Que...", "Promos", "TablonDeAnuncios", "Blog", "Contingut"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(121, 108)
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
        Me.GroupBoxEvento.Controls.Add(Me.Xl_LookupArea1)
        Me.GroupBoxEvento.Controls.Add(Me.Label11)
        Me.GroupBoxEvento.Controls.Add(Me.Label10)
        Me.GroupBoxEvento.Controls.Add(Me.DateTimePickerFchTo)
        Me.GroupBoxEvento.Controls.Add(Me.Label9)
        Me.GroupBoxEvento.Controls.Add(Me.DateTimePickerFchFrom)
        Me.GroupBoxEvento.Location = New System.Drawing.Point(121, 227)
        Me.GroupBoxEvento.Name = "GroupBoxEvento"
        Me.GroupBoxEvento.Size = New System.Drawing.Size(422, 57)
        Me.GroupBoxEvento.TabIndex = 65
        Me.GroupBoxEvento.TabStop = False
        Me.GroupBoxEvento.Text = "evento"
        Me.GroupBoxEvento.Visible = False
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(56, 35)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.ReadOnlyLookup = False
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(360, 20)
        Me.Xl_LookupArea1.TabIndex = 70
        Me.Xl_LookupArea1.Value = Nothing
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
        'DateTimePickerDestacarFrom
        '
        Me.DateTimePickerDestacarFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDestacarFrom.Enabled = False
        Me.DateTimePickerDestacarFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDestacarFrom.Location = New System.Drawing.Point(57, 23)
        Me.DateTimePickerDestacarFrom.Name = "DateTimePickerDestacarFrom"
        Me.DateTimePickerDestacarFrom.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerDestacarFrom.TabIndex = 66
        '
        'CheckBoxDestacar
        '
        Me.CheckBoxDestacar.AutoSize = True
        Me.CheckBoxDestacar.Location = New System.Drawing.Point(0, -1)
        Me.CheckBoxDestacar.Name = "CheckBoxDestacar"
        Me.CheckBoxDestacar.Size = New System.Drawing.Size(69, 17)
        Me.CheckBoxDestacar.TabIndex = 67
        Me.CheckBoxDestacar.Text = "Destacar"
        Me.CheckBoxDestacar.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 13)
        Me.Label12.TabIndex = 68
        Me.Label12.Text = "des de"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 13)
        Me.Label13.TabIndex = 70
        Me.Label13.Text = "fins"
        '
        'DateTimePickerDestacarTo
        '
        Me.DateTimePickerDestacarTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDestacarTo.Enabled = False
        Me.DateTimePickerDestacarTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDestacarTo.Location = New System.Drawing.Point(57, 47)
        Me.DateTimePickerDestacarTo.Name = "DateTimePickerDestacarTo"
        Me.DateTimePickerDestacarTo.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerDestacarTo.TabIndex = 69
        '
        'GroupBoxDestacar
        '
        Me.GroupBoxDestacar.Controls.Add(Me.DateTimePickerDestacarTo)
        Me.GroupBoxDestacar.Controls.Add(Me.CheckBoxDestacar)
        Me.GroupBoxDestacar.Controls.Add(Me.Label13)
        Me.GroupBoxDestacar.Controls.Add(Me.DateTimePickerDestacarFrom)
        Me.GroupBoxDestacar.Controls.Add(Me.Label12)
        Me.GroupBoxDestacar.Location = New System.Drawing.Point(121, 138)
        Me.GroupBoxDestacar.Name = "GroupBoxDestacar"
        Me.GroupBoxDestacar.Size = New System.Drawing.Size(159, 74)
        Me.GroupBoxDestacar.TabIndex = 71
        Me.GroupBoxDestacar.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Location = New System.Drawing.Point(6, 23)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(731, 526)
        Me.TabControl1.TabIndex = 72
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_UsrLog1)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxVisitCount)
        Me.TabPage1.Controls.Add(Me.Xl_DistributionChannels_Checklist1)
        Me.TabPage1.Controls.Add(Me.Xl_Keywords1)
        Me.TabPage1.Controls.Add(Me.GroupBoxDestacar)
        Me.TabPage1.Controls.Add(Me.CheckBoxVisible)
        Me.TabPage1.Controls.Add(Me.GroupBoxEvento)
        Me.TabPage1.Controls.Add(Me.CheckBoxPriority)
        Me.TabPage1.Controls.Add(Me.ComboBoxCod)
        Me.TabPage1.Controls.Add(Me.Xl_Image265x150)
        Me.TabPage1.Controls.Add(Me.Xl_LookupProduct1)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxUrl)
        Me.TabPage1.Controls.Add(Me.CheckBoxProfessional)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_CategoriasDeNoticia1)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFch)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxUrlRoot)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(723, 500)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Visites"
        '
        'TextBoxVisitCount
        '
        Me.TextBoxVisitCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVisitCount.Enabled = False
        Me.TextBoxVisitCount.Location = New System.Drawing.Point(121, 81)
        Me.TextBoxVisitCount.Name = "TextBoxVisitCount"
        Me.TextBoxVisitCount.ReadOnly = True
        Me.TextBoxVisitCount.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxVisitCount.TabIndex = 74
        Me.TextBoxVisitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Xl_DistributionChannels_Checklist1
        '
        Me.Xl_DistributionChannels_Checklist1.AllowUserToAddRows = False
        Me.Xl_DistributionChannels_Checklist1.AllowUserToDeleteRows = False
        Me.Xl_DistributionChannels_Checklist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DistributionChannels_Checklist1.Location = New System.Drawing.Point(431, 108)
        Me.Xl_DistributionChannels_Checklist1.Name = "Xl_DistributionChannels_Checklist1"
        Me.Xl_DistributionChannels_Checklist1.ReadOnly = True
        Me.Xl_DistributionChannels_Checklist1.Size = New System.Drawing.Size(276, 104)
        Me.Xl_DistributionChannels_Checklist1.TabIndex = 72
        '
        'Xl_Keywords1
        '
        Me.Xl_Keywords1.Location = New System.Drawing.Point(445, 314)
        Me.Xl_Keywords1.Name = "Xl_Keywords1"
        Me.Xl_Keywords1.Size = New System.Drawing.Size(171, 150)
        Me.Xl_Keywords1.TabIndex = 59
        '
        'Xl_Image265x150
        '
        Me.Xl_Image265x150.Bitmap = Nothing
        Me.Xl_Image265x150.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image265x150.EmptyImageLabelText = ""
        Me.Xl_Image265x150.IsDirty = False
        Me.Xl_Image265x150.Location = New System.Drawing.Point(11, 314)
        Me.Xl_Image265x150.Name = "Xl_Image265x150"
        Me.Xl_Image265x150.Size = New System.Drawing.Size(265, 150)
        Me.Xl_Image265x150.TabIndex = 51
        Me.Xl_Image265x150.ZipStream = Nothing
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(121, 28)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(483, 20)
        Me.Xl_LookupProduct1.TabIndex = 58
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_CategoriasDeNoticia1
        '
        Me.Xl_CategoriasDeNoticia1.Location = New System.Drawing.Point(282, 314)
        Me.Xl_CategoriasDeNoticia1.Name = "Xl_CategoriasDeNoticia1"
        Me.Xl_CategoriasDeNoticia1.Size = New System.Drawing.Size(156, 162)
        Me.Xl_CategoriasDeNoticia1.TabIndex = 57
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Xl_LangsTextTitol)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(723, 500)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Titol"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextTitol
        '
        Me.Xl_LangsTextTitol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextTitol.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextTitol.Name = "Xl_LangsTextTitol"
        Me.Xl_LangsTextTitol.Size = New System.Drawing.Size(723, 500)
        Me.Xl_LangsTextTitol.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.Xl_LangsTextExcerpt)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(723, 500)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Extracte"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextExcerpt
        '
        Me.Xl_LangsTextExcerpt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextExcerpt.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextExcerpt.Name = "Xl_LangsTextExcerpt"
        Me.Xl_LangsTextExcerpt.Size = New System.Drawing.Size(723, 500)
        Me.Xl_LangsTextExcerpt.TabIndex = 0
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.Xl_LangsTextContingut)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(723, 500)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "Contingut"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextContingut
        '
        Me.Xl_LangsTextContingut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextContingut.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextContingut.Name = "Xl_LangsTextContingut"
        Me.Xl_LangsTextContingut.Size = New System.Drawing.Size(723, 500)
        Me.Xl_LangsTextContingut.TabIndex = 1
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(11, 474)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(709, 20)
        Me.Xl_UsrLog1.TabIndex = 75
        '
        'Frm_Noticia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 581)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Noticia"
        Me.Text = "NOTICIA / NOTA DE PREMSA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxEvento.ResumeLayout(False)
        Me.GroupBoxEvento.PerformLayout()
        Me.GroupBoxDestacar.ResumeLayout(False)
        Me.GroupBoxDestacar.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.Xl_DistributionChannels_Checklist1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxUrlRoot As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxVisible As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPriority As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Image265x150 As Winforms.Xl_Image
    Friend WithEvents CheckBoxProfessional As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_CategoriasDeNoticia1 As Winforms.Xl_CategoriasDeNoticia
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents Xl_Keywords1 As Winforms.Xl_Keywords
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
    Friend WithEvents Xl_LookupArea1 As Winforms.Xl_LookupArea
    Friend WithEvents DateTimePickerDestacarFrom As DateTimePicker
    Friend WithEvents CheckBoxDestacar As CheckBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents DateTimePickerDestacarTo As DateTimePicker
    Friend WithEvents GroupBoxDestacar As GroupBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_DistributionChannels_Checklist1 As Xl_DistributionChannels_Checklist
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Xl_LangsTextTitol As Xl_LangsText
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents Xl_LangsTextExcerpt As Xl_LangsText
    Friend WithEvents Xl_LangsTextContingut As Xl_LangsText
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxVisitCount As TextBox
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
End Class
