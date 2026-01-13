<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BloggerPost
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_Blogger1 = New Winforms.Xl_Lookup_Blogger()
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.Xl_Products1 = New Winforms.Xl_Products()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.GroupBoxHighlight = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePickerHighlightTo = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateTimePickerHighlightFrom = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxHighlight = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_Langs1 = New Winforms.Xl_Langs()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxHighlight.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(77, 97)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrl.Location = New System.Drawing.Point(77, 71)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(484, 20)
        Me.TextBoxUrl.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Url"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 384)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(573, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(354, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(465, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Data"
        '
        'TextBoxTitle
        '
        Me.TextBoxTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitle.Location = New System.Drawing.Point(77, 45)
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.TextBoxTitle.Size = New System.Drawing.Size(484, 20)
        Me.TextBoxTitle.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Titol"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Blogger"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 189)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Producte"
        '
        'Xl_Lookup_Blogger1
        '
        Me.Xl_Lookup_Blogger1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Lookup_Blogger1.Blogger = Nothing
        Me.Xl_Lookup_Blogger1.IsDirty = False
        Me.Xl_Lookup_Blogger1.Location = New System.Drawing.Point(77, 19)
        Me.Xl_Lookup_Blogger1.Name = "Xl_Lookup_Blogger1"
        Me.Xl_Lookup_Blogger1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_Blogger1.Size = New System.Drawing.Size(484, 20)
        Me.Xl_Lookup_Blogger1.TabIndex = 0
        Me.Xl_Lookup_Blogger1.Value = Nothing
        '
        'ButtonAddProduct
        '
        Me.ButtonAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddProduct.Enabled = False
        Me.ButtonAddProduct.Location = New System.Drawing.Point(486, 185)
        Me.ButtonAddProduct.Name = "ButtonAddProduct"
        Me.ButtonAddProduct.Size = New System.Drawing.Size(75, 21)
        Me.ButtonAddProduct.TabIndex = 9
        Me.ButtonAddProduct.Text = "afegir"
        Me.ButtonAddProduct.UseVisualStyleBackColor = True
        '
        'Xl_Products1
        '
        Me.Xl_Products1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Products1.Location = New System.Drawing.Point(77, 212)
        Me.Xl_Products1.Name = "Xl_Products1"
        Me.Xl_Products1.Size = New System.Drawing.Size(484, 166)
        Me.Xl_Products1.TabIndex = 62
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(77, 186)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(403, 20)
        Me.Xl_LookupProduct1.TabIndex = 8
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'GroupBoxHighlight
        '
        Me.GroupBoxHighlight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxHighlight.Controls.Add(Me.Label7)
        Me.GroupBoxHighlight.Controls.Add(Me.DateTimePickerHighlightTo)
        Me.GroupBoxHighlight.Controls.Add(Me.Label6)
        Me.GroupBoxHighlight.Controls.Add(Me.DateTimePickerHighlightFrom)
        Me.GroupBoxHighlight.Location = New System.Drawing.Point(306, 100)
        Me.GroupBoxHighlight.Name = "GroupBoxHighlight"
        Me.GroupBoxHighlight.Size = New System.Drawing.Size(255, 80)
        Me.GroupBoxHighlight.TabIndex = 5
        Me.GroupBoxHighlight.TabStop = False
        Me.GroupBoxHighlight.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(89, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(26, 13)
        Me.Label7.TabIndex = 57
        Me.Label7.Text = "Fins"
        '
        'DateTimePickerHighlightTo
        '
        Me.DateTimePickerHighlightTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerHighlightTo.Location = New System.Drawing.Point(153, 45)
        Me.DateTimePickerHighlightTo.Name = "DateTimePickerHighlightTo"
        Me.DateTimePickerHighlightTo.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerHighlightTo.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(89, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Des de"
        '
        'DateTimePickerHighlightFrom
        '
        Me.DateTimePickerHighlightFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerHighlightFrom.Location = New System.Drawing.Point(153, 19)
        Me.DateTimePickerHighlightFrom.Name = "DateTimePickerHighlightFrom"
        Me.DateTimePickerHighlightFrom.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerHighlightFrom.TabIndex = 6
        '
        'CheckBoxHighlight
        '
        Me.CheckBoxHighlight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxHighlight.AutoSize = True
        Me.CheckBoxHighlight.Location = New System.Drawing.Point(297, 99)
        Me.CheckBoxHighlight.Name = "CheckBoxHighlight"
        Me.CheckBoxHighlight.Size = New System.Drawing.Size(121, 17)
        Me.CheckBoxHighlight.TabIndex = 5
        Me.CheckBoxHighlight.Text = "destacar en portada"
        Me.CheckBoxHighlight.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "Idioma"
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(77, 122)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(80, 21)
        Me.Xl_Langs1.TabIndex = 4
        Me.Xl_Langs1.Value = Nothing
        '
        'Frm_BloggerPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 415)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CheckBoxHighlight)
        Me.Controls.Add(Me.GroupBoxHighlight)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_Products1)
        Me.Controls.Add(Me.ButtonAddProduct)
        Me.Controls.Add(Me.Xl_Lookup_Blogger1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxTitle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxUrl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BloggerPost"
        Me.Text = "Post de producte"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxHighlight.ResumeLayout(False)
        Me.GroupBoxHighlight.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_Blogger1 As Winforms.Xl_Lookup_Blogger
    Friend WithEvents ButtonAddProduct As System.Windows.Forms.Button
    Friend WithEvents Xl_Products1 As Winforms.Xl_Products
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents GroupBoxHighlight As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerHighlightTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerHighlightFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxHighlight As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Xl_Langs1 As Xl_Langs
End Class
