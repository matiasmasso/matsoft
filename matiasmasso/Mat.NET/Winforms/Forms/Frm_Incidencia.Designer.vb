<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Incidencia
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.RadioButtonConsumidor = New System.Windows.Forms.RadioButton()
        Me.RadioButtonProfesional = New System.Windows.Forms.RadioButton()
        Me.Xl_LookupIncidenciaCodApertura = New Winforms.Xl_LookupIncidenciaCod()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxPerson = New System.Windows.Forms.TextBox()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupIncidenciaCodTancament = New Winforms.Xl_LookupIncidenciaCod()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Xl_IncidenciaVideos1 = New Winforms.Xl_IncidenciaVideos()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_UsrLog1 = New Winforms.Xl_UsrLog()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxManufactureDate = New System.Windows.Forms.TextBox()
        Me.TextBoxSerialNumber = New System.Windows.Forms.TextBox()
        Me.Xl_IncidenciaDocFilesImgs = New Winforms.Xl_IncidenciaDocFiles()
        Me.ComboBoxProcedencia = New System.Windows.Forms.ComboBox()
        Me.CheckBoxClosed = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.LabelDropHere = New System.Windows.Forms.Label()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFchCompra = New Winforms.DateTimePickerNullable()
        Me.Xl_IncidenciaDocFilesTicket = New Winforms.Xl_IncidenciaDocFiles()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Trackings1 = New Winforms.Xl_Trackings()
        Me.ContextMenuStripDocs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonSpv = New System.Windows.Forms.Button()
        Me.ButtonNewPdc = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecollidaPerFabricantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ComboBoxTrackings = New System.Windows.Forms.ComboBox()
        Me.ButtonAddTracking = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Xl_IncidenciaVideos1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_Trackings1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(703, 537)
        Me.TabControl1.TabIndex = 89
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxObs)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupIncidenciaCodApertura)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.TextBoxPerson)
        Me.TabPage1.Controls.Add(Me.Xl_LookupProduct1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupIncidenciaCodTancament)
        Me.TabPage1.Controls.Add(Me.TextBoxTel)
        Me.TabPage1.Controls.Add(Me.Xl_IncidenciaVideos1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_UsrLog1)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxManufactureDate)
        Me.TabPage1.Controls.Add(Me.TextBoxSerialNumber)
        Me.TabPage1.Controls.Add(Me.Xl_IncidenciaDocFilesImgs)
        Me.TabPage1.Controls.Add(Me.ComboBoxProcedencia)
        Me.TabPage1.Controls.Add(Me.CheckBoxClosed)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.LabelDropHere)
        Me.TabPage1.Controls.Add(Me.TextBoxRef)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.DateTimePicker2)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchCompra)
        Me.TabPage1.Controls.Add(Me.Xl_IncidenciaDocFilesTicket)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(695, 511)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(322, 344)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(367, 80)
        Me.TextBoxObs.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Xl_Contact21)
        Me.GroupBox1.Controls.Add(Me.RadioButtonConsumidor)
        Me.GroupBox1.Controls.Add(Me.RadioButtonProfesional)
        Me.GroupBox1.Location = New System.Drawing.Point(195, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(494, 75)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(127, 20)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Contact21.TabIndex = 1
        '
        'RadioButtonConsumidor
        '
        Me.RadioButtonConsumidor.AutoSize = True
        Me.RadioButtonConsumidor.Location = New System.Drawing.Point(18, 43)
        Me.RadioButtonConsumidor.Name = "RadioButtonConsumidor"
        Me.RadioButtonConsumidor.Size = New System.Drawing.Size(79, 17)
        Me.RadioButtonConsumidor.TabIndex = 4
        Me.RadioButtonConsumidor.TabStop = True
        Me.RadioButtonConsumidor.Text = "consumidor"
        Me.RadioButtonConsumidor.UseVisualStyleBackColor = True
        '
        'RadioButtonProfesional
        '
        Me.RadioButtonProfesional.AutoSize = True
        Me.RadioButtonProfesional.Location = New System.Drawing.Point(18, 20)
        Me.RadioButtonProfesional.Name = "RadioButtonProfesional"
        Me.RadioButtonProfesional.Size = New System.Drawing.Size(81, 17)
        Me.RadioButtonProfesional.TabIndex = 2
        Me.RadioButtonProfesional.TabStop = True
        Me.RadioButtonProfesional.Text = "professional"
        Me.RadioButtonProfesional.UseVisualStyleBackColor = True
        '
        'Xl_LookupIncidenciaCodApertura
        '
        Me.Xl_LookupIncidenciaCodApertura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupIncidenciaCodApertura.IncidenciaCod = Nothing
        Me.Xl_LookupIncidenciaCodApertura.IsDirty = False
        Me.Xl_LookupIncidenciaCodApertura.Location = New System.Drawing.Point(322, 316)
        Me.Xl_LookupIncidenciaCodApertura.Name = "Xl_LookupIncidenciaCodApertura"
        Me.Xl_LookupIncidenciaCodApertura.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupIncidenciaCodApertura.ReadOnlyLookup = False
        Me.Xl_LookupIncidenciaCodApertura.Size = New System.Drawing.Size(367, 20)
        Me.Xl_LookupIncidenciaCodApertura.TabIndex = 11
        Me.Xl_LookupIncidenciaCodApertura.Value = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(210, 241)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "telefon de contacte:"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Location = New System.Drawing.Point(21, 383)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 13)
        Me.Label13.TabIndex = 88
        Me.Label13.Text = "videos:"
        '
        'TextBoxPerson
        '
        Me.TextBoxPerson.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPerson.Location = New System.Drawing.Point(322, 211)
        Me.TextBoxPerson.MaxLength = 50
        Me.TextBoxPerson.Name = "TextBoxPerson"
        Me.TextBoxPerson.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxPerson.TabIndex = 7
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(322, 160)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(367, 20)
        Me.Xl_LookupProduct1.TabIndex = 4
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(210, 321)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "codi incidencia"
        '
        'Xl_LookupIncidenciaCodTancament
        '
        Me.Xl_LookupIncidenciaCodTancament.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupIncidenciaCodTancament.IncidenciaCod = Nothing
        Me.Xl_LookupIncidenciaCodTancament.IsDirty = False
        Me.Xl_LookupIncidenciaCodTancament.Location = New System.Drawing.Point(322, 459)
        Me.Xl_LookupIncidenciaCodTancament.Name = "Xl_LookupIncidenciaCodTancament"
        Me.Xl_LookupIncidenciaCodTancament.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupIncidenciaCodTancament.ReadOnlyLookup = False
        Me.Xl_LookupIncidenciaCodTancament.Size = New System.Drawing.Size(223, 20)
        Me.Xl_LookupIncidenciaCodTancament.TabIndex = 14
        Me.Xl_LookupIncidenciaCodTancament.Value = Nothing
        Me.Xl_LookupIncidenciaCodTancament.Visible = False
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTel.Location = New System.Drawing.Point(322, 237)
        Me.TextBoxTel.MaxLength = 50
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxTel.TabIndex = 8
        '
        'Xl_IncidenciaVideos1
        '
        Me.Xl_IncidenciaVideos1.AllowUserToAddRows = False
        Me.Xl_IncidenciaVideos1.AllowUserToDeleteRows = False
        Me.Xl_IncidenciaVideos1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_IncidenciaVideos1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_IncidenciaVideos1.DisplayObsolets = False
        Me.Xl_IncidenciaVideos1.Filter = Nothing
        Me.Xl_IncidenciaVideos1.Location = New System.Drawing.Point(22, 400)
        Me.Xl_IncidenciaVideos1.MouseIsDown = False
        Me.Xl_IncidenciaVideos1.Name = "Xl_IncidenciaVideos1"
        Me.Xl_IncidenciaVideos1.ReadOnly = True
        Me.Xl_IncidenciaVideos1.Size = New System.Drawing.Size(159, 79)
        Me.Xl_IncidenciaVideos1.TabIndex = 87
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(210, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "persona de contacte:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(210, 112)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "procedencia:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(210, 344)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "historial:"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Enabled = False
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(24, 485)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.ReadOnly = True
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(665, 20)
        Me.Xl_UsrLog1.TabIndex = 82
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(210, 294)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 71
        Me.Label7.Text = "referencia client:"
        '
        'TextBoxManufactureDate
        '
        Me.TextBoxManufactureDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxManufactureDate.Location = New System.Drawing.Point(538, 185)
        Me.TextBoxManufactureDate.MaxLength = 20
        Me.TextBoxManufactureDate.Name = "TextBoxManufactureDate"
        Me.TextBoxManufactureDate.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxManufactureDate.TabIndex = 6
        '
        'TextBoxSerialNumber
        '
        Me.TextBoxSerialNumber.Location = New System.Drawing.Point(322, 185)
        Me.TextBoxSerialNumber.MaxLength = 50
        Me.TextBoxSerialNumber.Name = "TextBoxSerialNumber"
        Me.TextBoxSerialNumber.Size = New System.Drawing.Size(135, 20)
        Me.TextBoxSerialNumber.TabIndex = 5
        '
        'Xl_IncidenciaDocFilesImgs
        '
        Me.Xl_IncidenciaDocFilesImgs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_IncidenciaDocFilesImgs.Location = New System.Drawing.Point(22, 25)
        Me.Xl_IncidenciaDocFilesImgs.Name = "Xl_IncidenciaDocFilesImgs"
        Me.Xl_IncidenciaDocFilesImgs.Size = New System.Drawing.Size(159, 178)
        Me.Xl_IncidenciaDocFilesImgs.TabIndex = 74
        '
        'ComboBoxProcedencia
        '
        Me.ComboBoxProcedencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxProcedencia.FormattingEnabled = True
        Me.ComboBoxProcedencia.Items.AddRange(New Object() {"(sense especificar)", "adquirit al propi establiment", "adquirit a altres establiments", "producte d'exposició"})
        Me.ComboBoxProcedencia.Location = New System.Drawing.Point(322, 109)
        Me.ComboBoxProcedencia.Name = "ComboBoxProcedencia"
        Me.ComboBoxProcedencia.Size = New System.Drawing.Size(330, 21)
        Me.ComboBoxProcedencia.TabIndex = 2
        '
        'CheckBoxClosed
        '
        Me.CheckBoxClosed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxClosed.AutoSize = True
        Me.CheckBoxClosed.Location = New System.Drawing.Point(213, 459)
        Me.CheckBoxClosed.Name = "CheckBoxClosed"
        Me.CheckBoxClosed.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxClosed.TabIndex = 13
        Me.CheckBoxClosed.Text = "tancada"
        Me.CheckBoxClosed.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(210, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 13)
        Me.Label11.TabIndex = 83
        Me.Label11.Text = "data de compra:"
        '
        'LabelDropHere
        '
        Me.LabelDropHere.AutoSize = True
        Me.LabelDropHere.BackColor = System.Drawing.SystemColors.Control
        Me.LabelDropHere.Location = New System.Drawing.Point(21, 9)
        Me.LabelDropHere.Name = "LabelDropHere"
        Me.LabelDropHere.Size = New System.Drawing.Size(33, 13)
        Me.LabelDropHere.TabIndex = 64
        Me.LabelDropHere.Text = "fotos:"
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRef.Location = New System.Drawing.Point(322, 290)
        Me.TextBoxRef.MaxLength = 50
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxRef.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(463, 188)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(77, 13)
        Me.Label12.TabIndex = 86
        Me.Label12.Text = "data fabricaciò"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(210, 189)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "nº de serie:"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(322, 263)
        Me.TextBoxEmail.MaxLength = 100
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxEmail.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(19, 221)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 76
        Me.Label9.Text = "ticket de compra:"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(588, 459)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePicker2.TabIndex = 15
        Me.DateTimePicker2.Visible = False
        '
        'DateTimePickerFchCompra
        '
        Me.DateTimePickerFchCompra.CustomFormat = "''"
        Me.DateTimePickerFchCompra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchCompra.Location = New System.Drawing.Point(322, 134)
        Me.DateTimePickerFchCompra.Name = "DateTimePickerFchCompra"
        Me.DateTimePickerFchCompra.ShowCheckBox = True
        Me.DateTimePickerFchCompra.Size = New System.Drawing.Size(114, 20)
        Me.DateTimePickerFchCompra.TabIndex = 84
        Me.DateTimePickerFchCompra.Value = New Date(2018, 9, 18, 12, 49, 13, 372)
        '
        'Xl_IncidenciaDocFilesTicket
        '
        Me.Xl_IncidenciaDocFilesTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_IncidenciaDocFilesTicket.Location = New System.Drawing.Point(22, 236)
        Me.Xl_IncidenciaDocFilesTicket.Name = "Xl_IncidenciaDocFilesTicket"
        Me.Xl_IncidenciaDocFilesTicket.Size = New System.Drawing.Size(159, 137)
        Me.Xl_IncidenciaDocFilesTicket.TabIndex = 75
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(210, 267)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "email"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(210, 163)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "producte"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ButtonAddTracking)
        Me.TabPage2.Controls.Add(Me.ComboBoxTrackings)
        Me.TabPage2.Controls.Add(Me.Xl_Trackings1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(695, 511)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tracking"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Trackings1
        '
        Me.Xl_Trackings1.AllowUserToAddRows = False
        Me.Xl_Trackings1.AllowUserToDeleteRows = False
        Me.Xl_Trackings1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Trackings1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Trackings1.DisplayObsolets = False
        Me.Xl_Trackings1.Location = New System.Drawing.Point(6, 65)
        Me.Xl_Trackings1.MouseIsDown = False
        Me.Xl_Trackings1.Name = "Xl_Trackings1"
        Me.Xl_Trackings1.ReadOnly = True
        Me.Xl_Trackings1.Size = New System.Drawing.Size(686, 443)
        Me.Xl_Trackings1.TabIndex = 0
        '
        'ContextMenuStripDocs
        '
        Me.ContextMenuStripDocs.Name = "ContextMenuStripDocs"
        Me.ContextMenuStripDocs.Size = New System.Drawing.Size(61, 4)
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(604, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 20
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(601, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 16
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(486, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 17
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonSpv
        '
        Me.ButtonSpv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSpv.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSpv.Image = Global.Winforms.My.Resources.Resources.Spv
        Me.ButtonSpv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSpv.Location = New System.Drawing.Point(212, 4)
        Me.ButtonSpv.Name = "ButtonSpv"
        Me.ButtonSpv.Size = New System.Drawing.Size(96, 24)
        Me.ButtonSpv.TabIndex = 19
        Me.ButtonSpv.TabStop = False
        Me.ButtonSpv.Text = "REPARACIO"
        Me.ButtonSpv.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSpv.UseVisualStyleBackColor = False
        '
        'ButtonNewPdc
        '
        Me.ButtonNewPdc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNewPdc.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNewPdc.Image = Global.Winforms.My.Resources.Resources.NewDoc
        Me.ButtonNewPdc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonNewPdc.Location = New System.Drawing.Point(314, 4)
        Me.ButtonNewPdc.Name = "ButtonNewPdc"
        Me.ButtonNewPdc.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNewPdc.TabIndex = 18
        Me.ButtonNewPdc.TabStop = False
        Me.ButtonNewPdc.Text = "COMANDA"
        Me.ButtonNewPdc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonNewPdc.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonNewPdc)
        Me.Panel1.Controls.Add(Me.ButtonSpv)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 588)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(709, 31)
        Me.Panel1.TabIndex = 42
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecollidaPerFabricantToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RecollidaPerFabricantToolStripMenuItem
        '
        Me.RecollidaPerFabricantToolStripMenuItem.Name = "RecollidaPerFabricantToolStripMenuItem"
        Me.RecollidaPerFabricantToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.RecollidaPerFabricantToolStripMenuItem.Text = "Recollida per fabricant"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(709, 24)
        Me.MenuStrip1.TabIndex = 85
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ComboBoxTrackings
        '
        Me.ComboBoxTrackings.FormattingEnabled = True
        Me.ComboBoxTrackings.Location = New System.Drawing.Point(6, 27)
        Me.ComboBoxTrackings.Name = "ComboBoxTrackings"
        Me.ComboBoxTrackings.Size = New System.Drawing.Size(605, 21)
        Me.ComboBoxTrackings.TabIndex = 1
        '
        'ButtonAddTracking
        '
        Me.ButtonAddTracking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddTracking.Location = New System.Drawing.Point(617, 26)
        Me.ButtonAddTracking.Name = "ButtonAddTracking"
        Me.ButtonAddTracking.Size = New System.Drawing.Size(75, 21)
        Me.ButtonAddTracking.TabIndex = 2
        Me.ButtonAddTracking.Text = "afegir"
        Me.ButtonAddTracking.UseVisualStyleBackColor = True
        '
        'Frm_Incidencia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 619)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Incidencia"
        Me.Text = "INCIDENCIA"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Xl_IncidenciaVideos1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_Trackings1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents RadioButtonConsumidor As RadioButton
    Friend WithEvents RadioButtonProfesional As RadioButton
    Friend WithEvents Xl_LookupIncidenciaCodApertura As Xl_LookupIncidenciaCod
    Friend WithEvents Label6 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBoxPerson As TextBox
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupIncidenciaCodTancament As Xl_LookupIncidenciaCod
    Friend WithEvents TextBoxTel As TextBox
    Friend WithEvents Xl_IncidenciaVideos1 As Xl_IncidenciaVideos
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxManufactureDate As TextBox
    Friend WithEvents TextBoxSerialNumber As TextBox
    Friend WithEvents Xl_IncidenciaDocFilesImgs As Xl_IncidenciaDocFiles
    Friend WithEvents ComboBoxProcedencia As ComboBox
    Friend WithEvents CheckBoxClosed As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents LabelDropHere As Label
    Friend WithEvents TextBoxRef As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePickerFchCompra As DateTimePickerNullable
    Friend WithEvents Xl_IncidenciaDocFilesTicket As Xl_IncidenciaDocFiles
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ContextMenuStripDocs As ContextMenuStrip
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ButtonDel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonSpv As Button
    Friend WithEvents ButtonNewPdc As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecollidaPerFabricantToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents Xl_Trackings1 As Xl_Trackings
    Friend WithEvents ButtonAddTracking As Button
    Friend WithEvents ComboBoxTrackings As ComboBox
End Class
