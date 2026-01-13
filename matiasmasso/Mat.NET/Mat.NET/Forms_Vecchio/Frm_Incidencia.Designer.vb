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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonNewPdc = New System.Windows.Forms.Button()
        Me.ButtonSpv = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Xl_Contact21 = New Mat.NET.Xl_Contact2()
        Me.RadioButtonConsumidor = New System.Windows.Forms.RadioButton()
        Me.RadioButtonProfesional = New System.Windows.Forms.RadioButton()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCod = New System.Windows.Forms.TextBox()
        Me.ButtonCodLookUp = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.CheckBoxClosed = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.ContextMenuStripDocs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ButtonTancamentLookUp = New System.Windows.Forms.Button()
        Me.TextBoxTancament = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabelDropHere = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxSerialNumber = New System.Windows.Forms.TextBox()
        Me.TextBoxPerson = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_IncidenciaDocFilesImgs = New Mat.NET.Xl_IncidenciaDocFiles()
        Me.Xl_IncidenciaDocFilesTicket = New Mat.NET.Xl_IncidenciaDocFiles()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBoxProcedencia = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 521)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(701, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonNewPdc
        '
        Me.ButtonNewPdc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNewPdc.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNewPdc.Image = Global.Mat.NET.My.Resources.Resources.NewDoc
        Me.ButtonNewPdc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonNewPdc.Location = New System.Drawing.Point(306, 4)
        Me.ButtonNewPdc.Name = "ButtonNewPdc"
        Me.ButtonNewPdc.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNewPdc.TabIndex = 19
        Me.ButtonNewPdc.TabStop = False
        Me.ButtonNewPdc.Text = "COMANDA"
        Me.ButtonNewPdc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonNewPdc.UseVisualStyleBackColor = False
        '
        'ButtonSpv
        '
        Me.ButtonSpv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSpv.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSpv.Image = Global.Mat.NET.My.Resources.Resources.Spv
        Me.ButtonSpv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSpv.Location = New System.Drawing.Point(204, 4)
        Me.ButtonSpv.Name = "ButtonSpv"
        Me.ButtonSpv.Size = New System.Drawing.Size(96, 24)
        Me.ButtonSpv.TabIndex = 18
        Me.ButtonSpv.TabStop = False
        Me.ButtonSpv.Text = "REPARACIO"
        Me.ButtonSpv.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSpv.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(478, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 21
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(593, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 20
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
        Me.ButtonDel.TabIndex = 17
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Xl_Contact21)
        Me.GroupBox1.Controls.Add(Me.RadioButtonConsumidor)
        Me.GroupBox1.Controls.Add(Me.RadioButtonProfesional)
        Me.GroupBox1.Location = New System.Drawing.Point(192, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(502, 75)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(127, 20)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(369, 20)
        Me.Xl_Contact21.TabIndex = 5
        '
        'RadioButtonConsumidor
        '
        Me.RadioButtonConsumidor.AutoSize = True
        Me.RadioButtonConsumidor.Enabled = False
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
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(593, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(207, 314)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "codi incidencia"
        '
        'TextBoxCod
        '
        Me.TextBoxCod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCod.Location = New System.Drawing.Point(319, 311)
        Me.TextBoxCod.Name = "TextBoxCod"
        Me.TextBoxCod.ReadOnly = True
        Me.TextBoxCod.Size = New System.Drawing.Size(333, 20)
        Me.TextBoxCod.TabIndex = 11
        '
        'ButtonCodLookUp
        '
        Me.ButtonCodLookUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCodLookUp.Location = New System.Drawing.Point(658, 311)
        Me.ButtonCodLookUp.Name = "ButtonCodLookUp"
        Me.ButtonCodLookUp.Size = New System.Drawing.Size(30, 20)
        Me.ButtonCodLookUp.TabIndex = 11
        Me.ButtonCodLookUp.Text = "..."
        Me.ButtonCodLookUp.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(207, 337)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "historial:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(319, 337)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(370, 127)
        Me.TextBoxObs.TabIndex = 12
        '
        'CheckBoxClosed
        '
        Me.CheckBoxClosed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxClosed.AutoSize = True
        Me.CheckBoxClosed.Location = New System.Drawing.Point(208, 483)
        Me.CheckBoxClosed.Name = "CheckBoxClosed"
        Me.CheckBoxClosed.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxClosed.TabIndex = 13
        Me.CheckBoxClosed.Text = "tancada"
        Me.CheckBoxClosed.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(588, 478)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePicker2.TabIndex = 16
        Me.DateTimePicker2.Visible = False
        '
        'ContextMenuStripDocs
        '
        Me.ContextMenuStripDocs.Name = "ContextMenuStripDocs"
        Me.ContextMenuStripDocs.Size = New System.Drawing.Size(61, 4)
        '
        'ButtonTancamentLookUp
        '
        Me.ButtonTancamentLookUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonTancamentLookUp.Location = New System.Drawing.Point(544, 475)
        Me.ButtonTancamentLookUp.Name = "ButtonTancamentLookUp"
        Me.ButtonTancamentLookUp.Size = New System.Drawing.Size(38, 23)
        Me.ButtonTancamentLookUp.TabIndex = 15
        Me.ButtonTancamentLookUp.Text = "..."
        Me.ButtonTancamentLookUp.UseVisualStyleBackColor = True
        Me.ButtonTancamentLookUp.Visible = False
        '
        'TextBoxTancament
        '
        Me.TextBoxTancament.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTancament.Location = New System.Drawing.Point(319, 477)
        Me.TextBoxTancament.Name = "TextBoxTancament"
        Me.TextBoxTancament.ReadOnly = True
        Me.TextBoxTancament.Size = New System.Drawing.Size(218, 20)
        Me.TextBoxTancament.TabIndex = 14
        Me.TextBoxTancament.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(207, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "producte"
        '
        'LabelDropHere
        '
        Me.LabelDropHere.AutoSize = True
        Me.LabelDropHere.BackColor = System.Drawing.SystemColors.Control
        Me.LabelDropHere.Location = New System.Drawing.Point(12, 27)
        Me.LabelDropHere.Name = "LabelDropHere"
        Me.LabelDropHere.Size = New System.Drawing.Size(33, 13)
        Me.LabelDropHere.TabIndex = 64
        Me.LabelDropHere.Text = "fotos:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(207, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "nº de serie:"
        '
        'TextBoxSerialNumber
        '
        Me.TextBoxSerialNumber.Location = New System.Drawing.Point(319, 150)
        Me.TextBoxSerialNumber.MaxLength = 50
        Me.TextBoxSerialNumber.Name = "TextBoxSerialNumber"
        Me.TextBoxSerialNumber.Size = New System.Drawing.Size(196, 20)
        Me.TextBoxSerialNumber.TabIndex = 6
        '
        'TextBoxPerson
        '
        Me.TextBoxPerson.Location = New System.Drawing.Point(319, 176)
        Me.TextBoxPerson.MaxLength = 50
        Me.TextBoxPerson.Name = "TextBoxPerson"
        Me.TextBoxPerson.Size = New System.Drawing.Size(332, 20)
        Me.TextBoxPerson.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(207, 180)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "persona de contacte:"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Location = New System.Drawing.Point(319, 202)
        Me.TextBoxTel.MaxLength = 50
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(332, 20)
        Me.TextBoxTel.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(207, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "telefon de contacte:"
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(319, 255)
        Me.TextBoxRef.MaxLength = 50
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(332, 20)
        Me.TextBoxRef.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(207, 259)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 71
        Me.Label7.Text = "referencia client:"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(319, 228)
        Me.TextBoxEmail.MaxLength = 100
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(332, 20)
        Me.TextBoxEmail.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(207, 232)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "email"
        '
        'Xl_IncidenciaDocFilesImgs
        '
        Me.Xl_IncidenciaDocFilesImgs.Location = New System.Drawing.Point(13, 43)
        Me.Xl_IncidenciaDocFilesImgs.Name = "Xl_IncidenciaDocFilesImgs"
        Me.Xl_IncidenciaDocFilesImgs.Size = New System.Drawing.Size(159, 279)
        Me.Xl_IncidenciaDocFilesImgs.TabIndex = 74
        '
        'Xl_IncidenciaDocFilesTicket
        '
        Me.Xl_IncidenciaDocFilesTicket.Location = New System.Drawing.Point(13, 363)
        Me.Xl_IncidenciaDocFilesTicket.Name = "Xl_IncidenciaDocFilesTicket"
        Me.Xl_IncidenciaDocFilesTicket.Size = New System.Drawing.Size(159, 137)
        Me.Xl_IncidenciaDocFilesTicket.TabIndex = 75
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(10, 347)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 76
        Me.Label9.Text = "ticket de compra:"
        '
        'ComboBoxProcedencia
        '
        Me.ComboBoxProcedencia.FormattingEnabled = True
        Me.ComboBoxProcedencia.Items.AddRange(New Object() {"(sense especificar)", "adquirit al propi establiment", "adquirit a altres establiments", "producte d'exposició"})
        Me.ComboBoxProcedencia.Location = New System.Drawing.Point(319, 282)
        Me.ComboBoxProcedencia.Name = "ComboBoxProcedencia"
        Me.ComboBoxProcedencia.Size = New System.Drawing.Size(332, 21)
        Me.ComboBoxProcedencia.TabIndex = 78
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(207, 285)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "procedencia:"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(319, 128)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(369, 20)
        Me.Xl_LookupProduct1.TabIndex = 80
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Frm_Incidencia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 552)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.ComboBoxProcedencia)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Xl_IncidenciaDocFilesTicket)
        Me.Controls.Add(Me.LabelDropHere)
        Me.Controls.Add(Me.Xl_IncidenciaDocFilesImgs)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxTel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxPerson)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxSerialNumber)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ButtonTancamentLookUp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxTancament)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.CheckBoxClosed)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.TextBoxCod)
        Me.Controls.Add(Me.ButtonCodLookUp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Incidencia"
        Me.Text = "INCIDENCIA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonConsumidor As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonProfesional As System.Windows.Forms.RadioButton
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCod As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCodLookUp As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxClosed As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContextMenuStripDocs As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ButtonTancamentLookUp As System.Windows.Forms.Button
    Friend WithEvents TextBoxTancament As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSpv As System.Windows.Forms.Button
    Friend WithEvents ButtonNewPdc As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LabelDropHere As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPerson As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTel As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRef As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_IncidenciaDocFilesImgs As Mat.NET.Xl_IncidenciaDocFiles
    Friend WithEvents Xl_Contact21 As Mat.NET.Xl_Contact2
    Friend WithEvents Xl_IncidenciaDocFilesTicket As Mat.NET.Xl_IncidenciaDocFiles
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxProcedencia As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
End Class
