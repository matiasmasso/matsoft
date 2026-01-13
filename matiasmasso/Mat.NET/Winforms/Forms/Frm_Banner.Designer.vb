<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Banner
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
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Image1 = New Winforms.Xl_Image()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.TextBoxNavigateTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_Langs1 = New Winforms.Xl_Langs()
        Me.CheckBoxLang = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(77, 85)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchFrom.TabIndex = 52
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(76, 6)
        Me.TextBoxNom.MaxLength = 50
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(576, 20)
        Me.TextBoxNom.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Nom"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 421)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(659, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(440, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(551, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_Image1
        '
        Me.Xl_Image1.AllowDrop = True
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(12, 118)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(640, 292)
        Me.Xl_Image1.TabIndex = 53
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Des de"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(256, 85)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchTo.TabIndex = 55
        Me.DateTimePickerFchTo.Visible = False
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(192, 88)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(45, 17)
        Me.CheckBoxFchTo.TabIndex = 57
        Me.CheckBoxFchTo.Text = "Fins"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'TextBoxNavigateTo
        '
        Me.TextBoxNavigateTo.Location = New System.Drawing.Point(76, 30)
        Me.TextBoxNavigateTo.MaxLength = 100
        Me.TextBoxNavigateTo.Name = "TextBoxNavigateTo"
        Me.TextBoxNavigateTo.Size = New System.Drawing.Size(576, 20)
        Me.TextBoxNavigateTo.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Url"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(76, 54)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(576, 20)
        Me.Xl_LookupProduct1.TabIndex = 60
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "Producte"
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(484, 85)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 62
        Me.Xl_Langs1.Value = Nothing
        Me.Xl_Langs1.Visible = False
        '
        'CheckBoxLang
        '
        Me.CheckBoxLang.AutoSize = True
        Me.CheckBoxLang.Location = New System.Drawing.Point(376, 88)
        Me.CheckBoxLang.Name = "CheckBoxLang"
        Me.CheckBoxLang.Size = New System.Drawing.Size(102, 17)
        Me.CheckBoxLang.TabIndex = 63
        Me.CheckBoxLang.Text = "Idioma especific"
        Me.CheckBoxLang.UseVisualStyleBackColor = True
        '
        'Frm_Banner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 452)
        Me.Controls.Add(Me.CheckBoxLang)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.TextBoxNavigateTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Banner"
        Me.Text = "Banner"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_Image1 As Winforms.Xl_Image
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxFchTo As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNavigateTo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents CheckBoxLang As CheckBox
End Class
