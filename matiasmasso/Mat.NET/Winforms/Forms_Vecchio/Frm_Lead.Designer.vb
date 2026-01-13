<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Lead
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
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCognoms = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBoxSource = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchCreated = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBoxActivated = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDeleted = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchActivated = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFchDeleted = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxTelefon = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_Langs1 = New Xl_Langs_Old()
        Me.Xl_Yea1 = New Xl_Yea()
        Me.Xl_Pais1 = New Xl_Pais()
        Me.Xl_Sex1 = New Xl_Sex()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPassword.Location = New System.Drawing.Point(108, 153)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(205, 20)
        Me.TextBoxPassword.TabIndex = 56
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 156)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Password:"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(108, 49)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(439, 20)
        Me.TextBoxEmail.TabIndex = 54
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Email:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 474)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(572, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(353, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(464, 4)
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
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(108, 75)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(439, 20)
        Me.TextBoxNom.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom:"
        '
        'TextBoxCognoms
        '
        Me.TextBoxCognoms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCognoms.Location = New System.Drawing.Point(108, 101)
        Me.TextBoxCognoms.Name = "TextBoxCognoms"
        Me.TextBoxCognoms.Size = New System.Drawing.Size(439, 20)
        Me.TextBoxCognoms.TabIndex = 58
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Cognoms"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 182)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "neixament:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "pais:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "idioma:"
        '
        'ComboBoxSource
        '
        Me.ComboBoxSource.FormattingEnabled = True
        Me.ComboBoxSource.Location = New System.Drawing.Point(108, 252)
        Me.ComboBoxSource.Name = "ComboBoxSource"
        Me.ComboBoxSource.Size = New System.Drawing.Size(205, 21)
        Me.ComboBoxSource.TabIndex = 66
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(27, 256)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "font:"
        '
        'DateTimePickerFchCreated
        '
        Me.DateTimePickerFchCreated.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchCreated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchCreated.Location = New System.Drawing.Point(108, 280)
        Me.DateTimePickerFchCreated.Name = "DateTimePickerFchCreated"
        Me.DateTimePickerFchCreated.Size = New System.Drawing.Size(113, 20)
        Me.DateTimePickerFchCreated.TabIndex = 68
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(26, 283)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 69
        Me.Label9.Text = "registre:"
        '
        'CheckBoxActivated
        '
        Me.CheckBoxActivated.AutoSize = True
        Me.CheckBoxActivated.Location = New System.Drawing.Point(108, 307)
        Me.CheckBoxActivated.Name = "CheckBoxActivated"
        Me.CheckBoxActivated.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxActivated.TabIndex = 70
        Me.CheckBoxActivated.Text = "activat"
        Me.CheckBoxActivated.UseVisualStyleBackColor = True
        '
        'CheckBoxDeleted
        '
        Me.CheckBoxDeleted.AutoSize = True
        Me.CheckBoxDeleted.Location = New System.Drawing.Point(108, 330)
        Me.CheckBoxDeleted.Name = "CheckBoxDeleted"
        Me.CheckBoxDeleted.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxDeleted.TabIndex = 71
        Me.CheckBoxDeleted.Text = "baixa"
        Me.CheckBoxDeleted.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchActivated
        '
        Me.DateTimePickerFchActivated.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchActivated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchActivated.Location = New System.Drawing.Point(197, 307)
        Me.DateTimePickerFchActivated.Name = "DateTimePickerFchActivated"
        Me.DateTimePickerFchActivated.Size = New System.Drawing.Size(116, 20)
        Me.DateTimePickerFchActivated.TabIndex = 72
        Me.DateTimePickerFchActivated.Visible = False
        '
        'DateTimePickerFchDeleted
        '
        Me.DateTimePickerFchDeleted.CustomFormat = "dd/MM/yy HH:mm"
        Me.DateTimePickerFchDeleted.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerFchDeleted.Location = New System.Drawing.Point(197, 330)
        Me.DateTimePickerFchDeleted.Name = "DateTimePickerFchDeleted"
        Me.DateTimePickerFchDeleted.Size = New System.Drawing.Size(116, 20)
        Me.DateTimePickerFchDeleted.TabIndex = 73
        Me.DateTimePickerFchDeleted.Visible = False
        '
        'TextBoxTelefon
        '
        Me.TextBoxTelefon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTelefon.Location = New System.Drawing.Point(108, 127)
        Me.TextBoxTelefon.Name = "TextBoxTelefon"
        Me.TextBoxTelefon.Size = New System.Drawing.Size(205, 20)
        Me.TextBoxTelefon.TabIndex = 75
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(27, 130)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 74
        Me.Label10.Text = "Telefon:"
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(108, 205)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(48, 21)
        Me.Xl_Langs1.TabIndex = 64
        Me.Xl_Langs1.Tag = "Idioma"
        '
        'Xl_Yea1
        '
        Me.Xl_Yea1.Location = New System.Drawing.Point(108, 179)
        Me.Xl_Yea1.Name = "Xl_Yea1"
        Me.Xl_Yea1.Size = New System.Drawing.Size(49, 20)
        Me.Xl_Yea1.TabIndex = 61
        Me.Xl_Yea1.Yea = 0
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.FlagVisible = True
        Me.Xl_Pais1.Location = New System.Drawing.Point(108, 230)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 62
        '
        'Xl_Sex1
        '
        Me.Xl_Sex1.Location = New System.Drawing.Point(268, 182)
        Me.Xl_Sex1.Name = "Xl_Sex1"
        Me.Xl_Sex1.Sex = MaxiSrvr.Contact.Sexs.NotSet
        Me.Xl_Sex1.Size = New System.Drawing.Size(45, 45)
        Me.Xl_Sex1.TabIndex = 59
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(561, 460)
        Me.TabControl1.TabIndex = 76
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.TextBoxTelefon)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchDeleted)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchActivated)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.CheckBoxDeleted)
        Me.TabPage1.Controls.Add(Me.TextBoxPassword)
        Me.TabPage1.Controls.Add(Me.CheckBoxActivated)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxCognoms)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchCreated)
        Me.TabPage1.Controls.Add(Me.Xl_Sex1)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.ComboBoxSource)
        Me.TabPage1.Controls.Add(Me.Xl_Yea1)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_Pais1)
        Me.TabPage1.Controls.Add(Me.Xl_Langs1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(553, 434)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(553, 434)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sortejos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(547, 428)
        Me.DataGridView1.TabIndex = 1
        '
        'Frm_Lead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 505)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Lead"
        Me.Text = "Lead"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCognoms As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_Sex1 As Xl_Sex
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_Yea1 As Xl_Yea
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_Langs1 As Xl_Langs_Old
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSource As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxActivated As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxDeleted As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerFchActivated As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerFchDeleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxTelefon As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
