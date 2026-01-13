<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_VisaCard
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.TextBoxDigits = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCaduca = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCCID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxBaja = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerBaja = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_ContactTitular = New Winforms.Xl_Contact2()
        Me.Xl_LookupVisaOrg1 = New Winforms.Xl_LookupVisaOrg()
        Me.Xl_LookupBanc1 = New Winforms.Xl_LookupBanc()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 236)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(473, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(254, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(365, 4)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Titular"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Ref.:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(78, 13)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(391, 20)
        Me.TextBoxNom.TabIndex = 46
        '
        'TextBoxDigits
        '
        Me.TextBoxDigits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDigits.Location = New System.Drawing.Point(78, 39)
        Me.TextBoxDigits.Name = "TextBoxDigits"
        Me.TextBoxDigits.Size = New System.Drawing.Size(238, 20)
        Me.TextBoxDigits.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Digits.:"
        '
        'TextBoxCaduca
        '
        Me.TextBoxCaduca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaduca.Location = New System.Drawing.Point(78, 65)
        Me.TextBoxCaduca.MaxLength = 4
        Me.TextBoxCaduca.Name = "TextBoxCaduca"
        Me.TextBoxCaduca.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCaduca.TabIndex = 50
        Me.TextBoxCaduca.Text = "0000"
        Me.TextBoxCaduca.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Caduca:"
        '
        'TextBoxCCID
        '
        Me.TextBoxCCID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCCID.Location = New System.Drawing.Point(78, 91)
        Me.TextBoxCCID.MaxLength = 3
        Me.TextBoxCCID.Name = "TextBoxCCID"
        Me.TextBoxCCID.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCCID.TabIndex = 52
        Me.TextBoxCCID.Text = "000"
        Me.TextBoxCCID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "CCID:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "Cta.Carrec:"
        '
        'CheckBoxBaja
        '
        Me.CheckBoxBaja.AutoSize = True
        Me.CheckBoxBaja.Location = New System.Drawing.Point(13, 196)
        Me.CheckBoxBaja.Name = "CheckBoxBaja"
        Me.CheckBoxBaja.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxBaja.TabIndex = 55
        Me.CheckBoxBaja.Text = "Baixa"
        Me.CheckBoxBaja.UseVisualStyleBackColor = True
        '
        'DateTimePickerBaja
        '
        Me.DateTimePickerBaja.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaja.Location = New System.Drawing.Point(78, 196)
        Me.DateTimePickerBaja.Name = "DateTimePickerBaja"
        Me.DateTimePickerBaja.Size = New System.Drawing.Size(102, 20)
        Me.DateTimePickerBaja.TabIndex = 56
        Me.DateTimePickerBaja.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "Emissor:"
        '
        'Xl_ContactTitular
        '
        Me.Xl_ContactTitular.Contact = Nothing
        Me.Xl_ContactTitular.Emp = Nothing
        Me.Xl_ContactTitular.Location = New System.Drawing.Point(77, 170)
        Me.Xl_ContactTitular.Name = "Xl_ContactTitular"
        Me.Xl_ContactTitular.ReadOnly = False
        Me.Xl_ContactTitular.Size = New System.Drawing.Size(391, 20)
        Me.Xl_ContactTitular.TabIndex = 59
        '
        'Xl_LookupVisaOrg1
        '
        Me.Xl_LookupVisaOrg1.IsDirty = False
        Me.Xl_LookupVisaOrg1.Location = New System.Drawing.Point(78, 118)
        Me.Xl_LookupVisaOrg1.Name = "Xl_LookupVisaOrg1"
        Me.Xl_LookupVisaOrg1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVisaOrg1.ReadOnlyLookup = False
        Me.Xl_LookupVisaOrg1.Size = New System.Drawing.Size(391, 20)
        Me.Xl_LookupVisaOrg1.TabIndex = 57
        Me.Xl_LookupVisaOrg1.Value = Nothing
        Me.Xl_LookupVisaOrg1.VisaOrg = Nothing
        '
        'Xl_LookupBanc1
        '
        Me.Xl_LookupBanc1.Banc = Nothing
        Me.Xl_LookupBanc1.IsDirty = False
        Me.Xl_LookupBanc1.Location = New System.Drawing.Point(78, 144)
        Me.Xl_LookupBanc1.Name = "Xl_LookupBanc1"
        Me.Xl_LookupBanc1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupBanc1.ReadOnlyLookup = False
        Me.Xl_LookupBanc1.Size = New System.Drawing.Size(390, 20)
        Me.Xl_LookupBanc1.TabIndex = 60
        Me.Xl_LookupBanc1.Value = Nothing
        '
        'Frm_VisaCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 267)
        Me.Controls.Add(Me.Xl_LookupBanc1)
        Me.Controls.Add(Me.Xl_ContactTitular)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_LookupVisaOrg1)
        Me.Controls.Add(Me.DateTimePickerBaja)
        Me.Controls.Add(Me.CheckBoxBaja)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxCCID)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxCaduca)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxDigits)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_VisaCard"
        Me.Text = "Tarja de Crèdit"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDigits As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCaduca As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCCID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxBaja As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerBaja As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_LookupVisaOrg1 As Winforms.Xl_LookupVisaOrg
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_ContactTitular As Winforms.Xl_Contact2
    Friend WithEvents Xl_LookupBanc1 As Winforms.Xl_LookupBanc
End Class
