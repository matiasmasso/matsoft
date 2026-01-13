<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Vehicle
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_LookupVehicleMarcayModel1 = New Xl_LookupVehicleMarcayModel()
        Me.Xl_Image1 = New Xl_Image()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Xl_Lookup_ContracteInsurance = New Xl_Lookup_Contracte()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_Contracte1 = New Xl_Lookup_Contracte()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_ContactConductor = New Xl_Contact2()
        Me.TextBoxMatricula = New System.Windows.Forms.TextBox()
        Me.Xl_ContactVenedor = New Xl_Contact2()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Xl_ProductDownloads()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Xl_Multas1 = New Xl_Multas()
        Me.TextBoxBastidor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_Multas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "marca i model"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "matricula"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "conductor"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 167)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "venedor"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 311)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "alta"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(90, 311)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerAlta.TabIndex = 10
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(15, 338)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxBaixa.TabIndex = 11
        Me.CheckBoxBaixa.Text = "baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(90, 338)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerBaixa.TabIndex = 12
        Me.DateTimePickerBaixa.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 483)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(610, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(721, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 13
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
        Me.ButtonDel.TabIndex = 15
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(6, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(820, 465)
        Me.TabControl1.TabIndex = 45
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxBastidor)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Xl_LookupVehicleMarcayModel1)
        Me.TabPage1.Controls.Add(Me.Xl_Image1)
        Me.TabPage1.Controls.Add(Me.CheckBoxPrivat)
        Me.TabPage1.Controls.Add(Me.PictureBox1)
        Me.TabPage1.Controls.Add(Me.Xl_Lookup_ContracteInsurance)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_Lookup_Contracte1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Xl_ContactConductor)
        Me.TabPage1.Controls.Add(Me.DateTimePickerBaixa)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.CheckBoxBaixa)
        Me.TabPage1.Controls.Add(Me.DateTimePickerAlta)
        Me.TabPage1.Controls.Add(Me.TextBoxMatricula)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Xl_ContactVenedor)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(812, 439)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_LookupVehicleMarcayModel1
        '
        Me.Xl_LookupVehicleMarcayModel1.IsDirty = False
        Me.Xl_LookupVehicleMarcayModel1.Location = New System.Drawing.Point(91, 27)
        Me.Xl_LookupVehicleMarcayModel1.Name = "Xl_LookupVehicleMarcayModel1"
        Me.Xl_LookupVehicleMarcayModel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVehicleMarcayModel1.Size = New System.Drawing.Size(348, 20)
        Me.Xl_LookupVehicleMarcayModel1.TabIndex = 1
        Me.Xl_LookupVehicleMarcayModel1.Value = Nothing
        Me.Xl_LookupVehicleMarcayModel1.VehicleModelValue = Nothing
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(449, 26)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(350, 400)
        Me.Xl_Image1.TabIndex = 23
        Me.Xl_Image1.ZipStream = Nothing
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(114, 253)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxPrivat.TabIndex = 22
        Me.CheckBoxPrivat.Text = "privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Mat.Net.My.Resources.Resources.candau
        Me.PictureBox1.Location = New System.Drawing.Point(91, 253)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 21
        Me.PictureBox1.TabStop = False
        '
        'Xl_Lookup_ContracteInsurance
        '
        Me.Xl_Lookup_ContracteInsurance.Contract = Nothing
        Me.Xl_Lookup_ContracteInsurance.IsDirty = False
        Me.Xl_Lookup_ContracteInsurance.Location = New System.Drawing.Point(91, 215)
        Me.Xl_Lookup_ContracteInsurance.Name = "Xl_Lookup_ContracteInsurance"
        Me.Xl_Lookup_ContracteInsurance.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_ContracteInsurance.Size = New System.Drawing.Size(349, 20)
        Me.Xl_Lookup_ContracteInsurance.TabIndex = 9
        Me.Xl_Lookup_ContracteInsurance.Value = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 217)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "assegurança"
        '
        'Xl_Lookup_Contracte1
        '
        Me.Xl_Lookup_Contracte1.Contract = Nothing
        Me.Xl_Lookup_Contracte1.IsDirty = False
        Me.Xl_Lookup_Contracte1.Location = New System.Drawing.Point(91, 189)
        Me.Xl_Lookup_Contracte1.Name = "Xl_Lookup_Contracte1"
        Me.Xl_Lookup_Contracte1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Contracte1.Size = New System.Drawing.Size(349, 20)
        Me.Xl_Lookup_Contracte1.TabIndex = 8
        Me.Xl_Lookup_Contracte1.Value = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 191)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "contracte"
        '
        'Xl_ContactConductor
        '
        Me.Xl_ContactConductor.Contact = Nothing
        Me.Xl_ContactConductor.Location = New System.Drawing.Point(91, 136)
        Me.Xl_ContactConductor.Name = "Xl_ContactConductor"
        Me.Xl_ContactConductor.ReadOnly = False
        Me.Xl_ContactConductor.Size = New System.Drawing.Size(314, 20)
        Me.Xl_ContactConductor.TabIndex = 6
        '
        'TextBoxMatricula
        '
        Me.TextBoxMatricula.Location = New System.Drawing.Point(90, 53)
        Me.TextBoxMatricula.Name = "TextBoxMatricula"
        Me.TextBoxMatricula.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxMatricula.TabIndex = 2
        '
        'Xl_ContactVenedor
        '
        Me.Xl_ContactVenedor.Contact = Nothing
        Me.Xl_ContactVenedor.Location = New System.Drawing.Point(91, 162)
        Me.Xl_ContactVenedor.Name = "Xl_ContactVenedor"
        Me.Xl_ContactVenedor.ReadOnly = False
        Me.Xl_ContactVenedor.Size = New System.Drawing.Size(314, 20)
        Me.Xl_ContactVenedor.TabIndex = 7
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ProductDownloads1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(812, 439)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Documentació"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Filter = Nothing
        Me.Xl_ProductDownloads1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductDownloads1.Name = "Xl_ProductDownloads1"
        Me.Xl_ProductDownloads1.Size = New System.Drawing.Size(806, 433)
        Me.Xl_ProductDownloads1.TabIndex = 2
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage3.Controls.Add(Me.Xl_Multas1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(812, 439)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Multes"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(579, 13)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(227, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Xl_Multas1
        '
        Me.Xl_Multas1.AllowUserToAddRows = False
        Me.Xl_Multas1.AllowUserToDeleteRows = False
        Me.Xl_Multas1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Multas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Multas1.DisplayObsolets = False
        Me.Xl_Multas1.Filter = Nothing
        Me.Xl_Multas1.Location = New System.Drawing.Point(3, 39)
        Me.Xl_Multas1.MouseIsDown = False
        Me.Xl_Multas1.Name = "Xl_Multas1"
        Me.Xl_Multas1.ReadOnly = True
        Me.Xl_Multas1.Size = New System.Drawing.Size(806, 400)
        Me.Xl_Multas1.TabIndex = 0
        '
        'TextBoxBastidor
        '
        Me.TextBoxBastidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBastidor.Location = New System.Drawing.Point(90, 79)
        Me.TextBoxBastidor.Name = "TextBoxBastidor"
        Me.TextBoxBastidor.Size = New System.Drawing.Size(314, 20)
        Me.TextBoxBastidor.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "bastidor"
        '
        'Frm_Vehicle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 514)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Vehicle"
        Me.Text = "VEHICLE"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_Multas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_ContactConductor As Xl_Contact2
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactVenedor As Xl_Contact2
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxMatricula As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_Contracte1 As Xl_Lookup_Contracte
    Friend WithEvents Xl_Lookup_ContracteInsurance As Xl_Lookup_Contracte
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_ProductDownloads1 As Xl_ProductDownloads
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Xl_LookupVehicleMarcayModel1 As Xl_LookupVehicleMarcayModel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Multas1 As Xl_Multas
    Friend WithEvents TextBoxBastidor As TextBox
    Friend WithEvents Label8 As Label
End Class
