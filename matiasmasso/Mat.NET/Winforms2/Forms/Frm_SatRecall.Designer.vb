<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SatRecall
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxIncidencia = New System.Windows.Forms.TextBox()
        Me.ComboBoxPickupFrom = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBoxFchCustomer = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerCustomer = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerManufacturer = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchManufacturer = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPickupRef = New System.Windows.Forms.TextBox()
        Me.TextBoxCreditNum = New System.Windows.Forms.TextBox()
        Me.LabelCreditFch = New System.Windows.Forms.Label()
        Me.DateTimePickerCredit = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxCredit = New System.Windows.Forms.CheckBox()
        Me.LabelCreditNum = New System.Windows.Forms.Label()
        Me.TextBoxDefect = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormulariFabricantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmailAlClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmailAlFabricantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_LookupZip1 = New Mat.Net.Xl_LookupZip()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.TextBoxContactPerson = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxReturnRef = New System.Windows.Forms.TextBox()
        Me.DateTimePickerReturn = New System.Windows.Forms.DateTimePicker()
        Me.LabelReturnFch = New System.Windows.Forms.Label()
        Me.CheckBoxCarrec = New System.Windows.Forms.CheckBox()
        Me.RadioButtonRepara = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAbona = New System.Windows.Forms.RadioButton()
        Me.CheckBoxReturned = New System.Windows.Forms.CheckBox()
        Me.LabelReturnRef = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 609)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(529, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(310, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 17
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(421, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 16
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
        Me.ButtonDel.TabIndex = 18
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBoxIncidencia)
        Me.GroupBox1.Location = New System.Drawing.Point(36, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(473, 104)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Incidencia"
        '
        'TextBoxIncidencia
        '
        Me.TextBoxIncidencia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxIncidencia.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxIncidencia.Location = New System.Drawing.Point(7, 20)
        Me.TextBoxIncidencia.Multiline = True
        Me.TextBoxIncidencia.Name = "TextBoxIncidencia"
        Me.TextBoxIncidencia.ReadOnly = True
        Me.TextBoxIncidencia.Size = New System.Drawing.Size(460, 78)
        Me.TextBoxIncidencia.TabIndex = 0
        '
        'ComboBoxPickupFrom
        '
        Me.ComboBoxPickupFrom.FormattingEnabled = True
        Me.ComboBoxPickupFrom.Items.AddRange(New Object() {"(sel·leccionar)", "botiga", "taller", "magatzem"})
        Me.ComboBoxPickupFrom.Location = New System.Drawing.Point(153, 184)
        Me.ComboBoxPickupFrom.Name = "ComboBoxPickupFrom"
        Me.ComboBoxPickupFrom.Size = New System.Drawing.Size(108, 21)
        Me.ComboBoxPickupFrom.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 187)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "Punt de recollida"
        '
        'CheckBoxFchCustomer
        '
        Me.CheckBoxFchCustomer.AutoSize = True
        Me.CheckBoxFchCustomer.Location = New System.Drawing.Point(36, 265)
        Me.CheckBoxFchCustomer.Name = "CheckBoxFchCustomer"
        Me.CheckBoxFchCustomer.Size = New System.Drawing.Size(94, 17)
        Me.CheckBoxFchCustomer.TabIndex = 5
        Me.CheckBoxFchCustomer.Text = "Avisat al client"
        Me.CheckBoxFchCustomer.UseVisualStyleBackColor = True
        '
        'DateTimePickerCustomer
        '
        Me.DateTimePickerCustomer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerCustomer.Location = New System.Drawing.Point(153, 263)
        Me.DateTimePickerCustomer.Name = "DateTimePickerCustomer"
        Me.DateTimePickerCustomer.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerCustomer.TabIndex = 6
        Me.DateTimePickerCustomer.Visible = False
        '
        'DateTimePickerManufacturer
        '
        Me.DateTimePickerManufacturer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerManufacturer.Location = New System.Drawing.Point(153, 289)
        Me.DateTimePickerManufacturer.Name = "DateTimePickerManufacturer"
        Me.DateTimePickerManufacturer.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerManufacturer.TabIndex = 8
        Me.DateTimePickerManufacturer.Visible = False
        '
        'CheckBoxFchManufacturer
        '
        Me.CheckBoxFchManufacturer.AutoSize = True
        Me.CheckBoxFchManufacturer.Location = New System.Drawing.Point(36, 291)
        Me.CheckBoxFchManufacturer.Name = "CheckBoxFchManufacturer"
        Me.CheckBoxFchManufacturer.Size = New System.Drawing.Size(110, 17)
        Me.CheckBoxFchManufacturer.TabIndex = 7
        Me.CheckBoxFchManufacturer.Text = "Avisat al fabricant"
        Me.CheckBoxFchManufacturer.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 328)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "Referència recollida"
        '
        'TextBoxPickupRef
        '
        Me.TextBoxPickupRef.Location = New System.Drawing.Point(153, 325)
        Me.TextBoxPickupRef.MaxLength = 50
        Me.TextBoxPickupRef.Name = "TextBoxPickupRef"
        Me.TextBoxPickupRef.Size = New System.Drawing.Size(108, 20)
        Me.TextBoxPickupRef.TabIndex = 9
        '
        'TextBoxCreditNum
        '
        Me.TextBoxCreditNum.Location = New System.Drawing.Point(153, 378)
        Me.TextBoxCreditNum.MaxLength = 50
        Me.TextBoxCreditNum.Name = "TextBoxCreditNum"
        Me.TextBoxCreditNum.Size = New System.Drawing.Size(108, 20)
        Me.TextBoxCreditNum.TabIndex = 11
        Me.TextBoxCreditNum.Visible = False
        '
        'LabelCreditFch
        '
        Me.LabelCreditFch.AutoSize = True
        Me.LabelCreditFch.Location = New System.Drawing.Point(267, 381)
        Me.LabelCreditFch.Name = "LabelCreditFch"
        Me.LabelCreditFch.Size = New System.Drawing.Size(30, 13)
        Me.LabelCreditFch.TabIndex = 71
        Me.LabelCreditFch.Text = "Data"
        Me.LabelCreditFch.Visible = False
        '
        'DateTimePickerCredit
        '
        Me.DateTimePickerCredit.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerCredit.Location = New System.Drawing.Point(303, 378)
        Me.DateTimePickerCredit.Name = "DateTimePickerCredit"
        Me.DateTimePickerCredit.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerCredit.TabIndex = 12
        Me.DateTimePickerCredit.Visible = False
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(38, 522)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(466, 81)
        Me.TextBoxObs.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(35, 506)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 74
        Me.Label5.Text = "Observacions"
        '
        'CheckBoxCredit
        '
        Me.CheckBoxCredit.AutoSize = True
        Me.CheckBoxCredit.Location = New System.Drawing.Point(36, 380)
        Me.CheckBoxCredit.Name = "CheckBoxCredit"
        Me.CheckBoxCredit.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxCredit.TabIndex = 10
        Me.CheckBoxCredit.Text = "Credit"
        Me.CheckBoxCredit.UseVisualStyleBackColor = True
        '
        'LabelCreditNum
        '
        Me.LabelCreditNum.AutoSize = True
        Me.LabelCreditNum.Location = New System.Drawing.Point(117, 381)
        Me.LabelCreditNum.Name = "LabelCreditNum"
        Me.LabelCreditNum.Size = New System.Drawing.Size(29, 13)
        Me.LabelCreditNum.TabIndex = 76
        Me.LabelCreditNum.Text = "Num"
        Me.LabelCreditNum.Visible = False
        '
        'TextBoxDefect
        '
        Me.TextBoxDefect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDefect.Location = New System.Drawing.Point(153, 158)
        Me.TextBoxDefect.MaxLength = 50
        Me.TextBoxDefect.Name = "TextBoxDefect"
        Me.TextBoxDefect.Size = New System.Drawing.Size(349, 20)
        Me.TextBoxDefect.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "Defecte observat"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(529, 24)
        Me.MenuStrip1.TabIndex = 78
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FormulariFabricantToolStripMenuItem, Me.EmailAlClientToolStripMenuItem, Me.EmailAlFabricantToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'FormulariFabricantToolStripMenuItem
        '
        Me.FormulariFabricantToolStripMenuItem.Name = "FormulariFabricantToolStripMenuItem"
        Me.FormulariFabricantToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.FormulariFabricantToolStripMenuItem.Text = "Formulari fabricant"
        '
        'EmailAlClientToolStripMenuItem
        '
        Me.EmailAlClientToolStripMenuItem.Name = "EmailAlClientToolStripMenuItem"
        Me.EmailAlClientToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.EmailAlClientToolStripMenuItem.Text = "Email al client"
        '
        'EmailAlFabricantToolStripMenuItem
        '
        Me.EmailAlFabricantToolStripMenuItem.Name = "EmailAlFabricantToolStripMenuItem"
        Me.EmailAlFabricantToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.EmailAlFabricantToolStripMenuItem.Text = "Email al fabricant"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Xl_LookupZip1)
        Me.GroupBox2.Controls.Add(Me.TextBoxAdr)
        Me.GroupBox2.Location = New System.Drawing.Point(36, 417)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(466, 77)
        Me.GroupBox2.TabIndex = 79
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Adreça de recollida"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Carrer i nº"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Població"
        '
        'Xl_LookupZip1
        '
        Me.Xl_LookupZip1.IsDirty = False
        Me.Xl_LookupZip1.Location = New System.Drawing.Point(117, 46)
        Me.Xl_LookupZip1.Name = "Xl_LookupZip1"
        Me.Xl_LookupZip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupZip1.ReadOnlyLookup = False
        Me.Xl_LookupZip1.Size = New System.Drawing.Size(343, 20)
        Me.Xl_LookupZip1.TabIndex = 14
        Me.Xl_LookupZip1.Value = Nothing
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Location = New System.Drawing.Point(117, 20)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(343, 20)
        Me.TextBoxAdr.TabIndex = 13
        '
        'TextBoxContactPerson
        '
        Me.TextBoxContactPerson.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactPerson.Location = New System.Drawing.Point(152, 211)
        Me.TextBoxContactPerson.MaxLength = 50
        Me.TextBoxContactPerson.Name = "TextBoxContactPerson"
        Me.TextBoxContactPerson.Size = New System.Drawing.Size(349, 20)
        Me.TextBoxContactPerson.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(32, 214)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 81
        Me.Label7.Text = "Persona de contacte"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTel.Location = New System.Drawing.Point(152, 237)
        Me.TextBoxTel.MaxLength = 50
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(349, 20)
        Me.TextBoxTel.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(32, 240)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 83
        Me.Label8.Text = "Telèfon"
        '
        'TextBoxReturnRef
        '
        Me.TextBoxReturnRef.Location = New System.Drawing.Point(153, 352)
        Me.TextBoxReturnRef.MaxLength = 50
        Me.TextBoxReturnRef.Name = "TextBoxReturnRef"
        Me.TextBoxReturnRef.Size = New System.Drawing.Size(108, 20)
        Me.TextBoxReturnRef.TabIndex = 85
        Me.TextBoxReturnRef.Visible = False
        '
        'DateTimePickerReturn
        '
        Me.DateTimePickerReturn.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerReturn.Location = New System.Drawing.Point(303, 352)
        Me.DateTimePickerReturn.Name = "DateTimePickerReturn"
        Me.DateTimePickerReturn.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerReturn.TabIndex = 87
        Me.DateTimePickerReturn.Visible = False
        '
        'LabelReturnFch
        '
        Me.LabelReturnFch.AutoSize = True
        Me.LabelReturnFch.Location = New System.Drawing.Point(267, 355)
        Me.LabelReturnFch.Name = "LabelReturnFch"
        Me.LabelReturnFch.Size = New System.Drawing.Size(30, 13)
        Me.LabelReturnFch.TabIndex = 88
        Me.LabelReturnFch.Text = "Data"
        Me.LabelReturnFch.Visible = False
        '
        'CheckBoxCarrec
        '
        Me.CheckBoxCarrec.AutoSize = True
        Me.CheckBoxCarrec.Location = New System.Drawing.Point(428, 354)
        Me.CheckBoxCarrec.Name = "CheckBoxCarrec"
        Me.CheckBoxCarrec.Size = New System.Drawing.Size(57, 17)
        Me.CheckBoxCarrec.TabIndex = 89
        Me.CheckBoxCarrec.Text = "Càrrec"
        Me.CheckBoxCarrec.UseVisualStyleBackColor = True
        Me.CheckBoxCarrec.Visible = False
        '
        'RadioButtonRepara
        '
        Me.RadioButtonRepara.AutoSize = True
        Me.RadioButtonRepara.Location = New System.Drawing.Point(270, 134)
        Me.RadioButtonRepara.Name = "RadioButtonRepara"
        Me.RadioButtonRepara.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonRepara.TabIndex = 93
        Me.RadioButtonRepara.TabStop = True
        Me.RadioButtonRepara.Text = "per reparar"
        Me.RadioButtonRepara.UseVisualStyleBackColor = True
        '
        'RadioButtonAbona
        '
        Me.RadioButtonAbona.AutoSize = True
        Me.RadioButtonAbona.Location = New System.Drawing.Point(153, 134)
        Me.RadioButtonAbona.Name = "RadioButtonAbona"
        Me.RadioButtonAbona.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonAbona.TabIndex = 92
        Me.RadioButtonAbona.TabStop = True
        Me.RadioButtonAbona.Text = "per abonar"
        Me.RadioButtonAbona.UseVisualStyleBackColor = True
        '
        'CheckBoxReturned
        '
        Me.CheckBoxReturned.AutoSize = True
        Me.CheckBoxReturned.Location = New System.Drawing.Point(36, 354)
        Me.CheckBoxReturned.Name = "CheckBoxReturned"
        Me.CheckBoxReturned.Size = New System.Drawing.Size(66, 17)
        Me.CheckBoxReturned.TabIndex = 94
        Me.CheckBoxReturned.Text = "Tornada"
        Me.CheckBoxReturned.UseVisualStyleBackColor = True
        '
        'LabelReturnRef
        '
        Me.LabelReturnRef.AutoSize = True
        Me.LabelReturnRef.Location = New System.Drawing.Point(117, 355)
        Me.LabelReturnRef.Name = "LabelReturnRef"
        Me.LabelReturnRef.Size = New System.Drawing.Size(24, 13)
        Me.LabelReturnRef.TabIndex = 95
        Me.LabelReturnRef.Text = "Ref"
        Me.LabelReturnRef.Visible = False
        '
        'Frm_SatRecall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 640)
        Me.Controls.Add(Me.LabelReturnRef)
        Me.Controls.Add(Me.CheckBoxReturned)
        Me.Controls.Add(Me.RadioButtonRepara)
        Me.Controls.Add(Me.RadioButtonAbona)
        Me.Controls.Add(Me.CheckBoxCarrec)
        Me.Controls.Add(Me.DateTimePickerReturn)
        Me.Controls.Add(Me.LabelReturnFch)
        Me.Controls.Add(Me.TextBoxReturnRef)
        Me.Controls.Add(Me.TextBoxTel)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxContactPerson)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TextBoxDefect)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabelCreditNum)
        Me.Controls.Add(Me.CheckBoxCredit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.DateTimePickerCredit)
        Me.Controls.Add(Me.LabelCreditFch)
        Me.Controls.Add(Me.TextBoxCreditNum)
        Me.Controls.Add(Me.TextBoxPickupRef)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerManufacturer)
        Me.Controls.Add(Me.CheckBoxFchManufacturer)
        Me.Controls.Add(Me.DateTimePickerCustomer)
        Me.Controls.Add(Me.CheckBoxFchCustomer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxPickupFrom)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_SatRecall"
        Me.Text = "Recollides fabricant"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBoxIncidencia As TextBox
    Friend WithEvents ComboBoxPickupFrom As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBoxFchCustomer As CheckBox
    Friend WithEvents DateTimePickerCustomer As DateTimePicker
    Friend WithEvents DateTimePickerManufacturer As DateTimePicker
    Friend WithEvents CheckBoxFchManufacturer As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxPickupRef As TextBox
    Friend WithEvents TextBoxCreditNum As TextBox
    Friend WithEvents LabelCreditFch As Label
    Friend WithEvents DateTimePickerCredit As DateTimePicker
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBoxCredit As CheckBox
    Friend WithEvents LabelCreditNum As Label
    Friend WithEvents TextBoxDefect As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EmailAlClientToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EmailAlFabricantToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FormulariFabricantToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_LookupZip1 As Xl_LookupZip
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents TextBoxContactPerson As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxTel As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxReturnRef As TextBox
    Friend WithEvents DateTimePickerReturn As DateTimePicker
    Friend WithEvents LabelReturnFch As Label
    Friend WithEvents CheckBoxCarrec As CheckBox
    Friend WithEvents RadioButtonRepara As RadioButton
    Friend WithEvents RadioButtonAbona As RadioButton
    Friend WithEvents CheckBoxReturned As CheckBox
    Friend WithEvents LabelReturnRef As Label
End Class
