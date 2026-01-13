<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LiniaTelefon
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
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxAlias = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Contact1 = New Xl_Contact2()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Xl_ProductDownloads()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxIcc = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxImei = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxPuk = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Num:"
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(74, 37)
        Me.TextBoxNum.MaxLength = 9
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxNum.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 391)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(437, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(218, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(329, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
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
        Me.ButtonDel.TabIndex = 12
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(74, 63)
        Me.TextBoxAlias.MaxLength = 9
        Me.TextBoxAlias.Name = "TextBoxAlias"
        Me.TextBoxAlias.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxAlias.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Alias:"
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Emp = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(74, 90)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(339, 20)
        Me.Xl_Contact1.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Usuari:"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(74, 228)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(121, 20)
        Me.DateTimePickerAlta.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 232)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Alta:"
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(74, 254)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(121, 20)
        Me.DateTimePickerBaixa.TabIndex = 8
        Me.DateTimePickerBaixa.Visible = False
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(143, 291)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxPrivat.TabIndex = 9
        Me.CheckBoxPrivat.Text = "privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(22, 256)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxBaixa.TabIndex = 7
        Me.CheckBoxBaixa.Text = "baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(427, 358)
        Me.TabControl1.TabIndex = 46
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxPuk)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxImei)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxIcc)
        Me.TabPage1.Controls.Add(Me.Xl_Contact1)
        Me.TabPage1.Controls.Add(Me.CheckBoxBaixa)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.CheckBoxPrivat)
        Me.TabPage1.Controls.Add(Me.TextBoxNum)
        Me.TabPage1.Controls.Add(Me.DateTimePickerBaixa)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxAlias)
        Me.TabPage1.Controls.Add(Me.DateTimePickerAlta)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(419, 332)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ProductDownloads1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(419, 292)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Documents"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.AllowUserToAddRows = False
        Me.Xl_ProductDownloads1.AllowUserToDeleteRows = False
        Me.Xl_ProductDownloads1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Filter = Nothing
        Me.Xl_ProductDownloads1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductDownloads1.Name = "Xl_ProductDownloads1"
        Me.Xl_ProductDownloads1.ReadOnly = True
        Me.Xl_ProductDownloads1.Size = New System.Drawing.Size(413, 286)
        Me.Xl_ProductDownloads1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "ICC:"
        '
        'TextBoxIcc
        '
        Me.TextBoxIcc.Location = New System.Drawing.Point(74, 116)
        Me.TextBoxIcc.MaxLength = 9
        Me.TextBoxIcc.Name = "TextBoxIcc"
        Me.TextBoxIcc.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxIcc.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Imei:"
        '
        'TextBoxImei
        '
        Me.TextBoxImei.Location = New System.Drawing.Point(74, 142)
        Me.TextBoxImei.MaxLength = 9
        Me.TextBoxImei.Name = "TextBoxImei"
        Me.TextBoxImei.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxImei.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 171)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Puk:"
        '
        'TextBoxPuk
        '
        Me.TextBoxPuk.Location = New System.Drawing.Point(74, 168)
        Me.TextBoxPuk.MaxLength = 9
        Me.TextBoxPuk.Name = "TextBoxPuk"
        Me.TextBoxPuk.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxPuk.TabIndex = 5
        '
        'Frm_LiniaTelefon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 422)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_LiniaTelefon"
        Me.Text = "Linia de telefon"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact2
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_ProductDownloads1 As Xl_ProductDownloads
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxPuk As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxImei As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxIcc As TextBox
End Class
