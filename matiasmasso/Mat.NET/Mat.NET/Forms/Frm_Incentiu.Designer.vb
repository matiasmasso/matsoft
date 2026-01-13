<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Incentiu
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonDto = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFreeUnits = New System.Windows.Forms.RadioButton()
        Me.ComboBoxEventos = New System.Windows.Forms.ComboBox()
        Me.CheckBoxOnlyInStock = New System.Windows.Forms.CheckBox()
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.GroupBoxProducts = New System.Windows.Forms.GroupBox()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        Me.Xl_Products1 = New Mat.NET.Xl_Products()
        Me.Xl_TextBoxNumQty = New Mat.NET.Xl_TextBoxNum()
        Me.GroupBoxQtyDtos = New System.Windows.Forms.GroupBox()
        Me.ButtonAddQtyDto = New System.Windows.Forms.Button()
        Me.LabelQty = New System.Windows.Forms.Label()
        Me.LabelDto = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumDto = New Mat.NET.Xl_TextBoxNum()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxTo = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_QtyDtos1 = New Mat.NET.Xl_QtyDtos()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBoxProducts.SuspendLayout()
        Me.GroupBoxQtyDtos.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButtonDto)
        Me.GroupBox3.Controls.Add(Me.RadioButtonFreeUnits)
        Me.GroupBox3.Location = New System.Drawing.Point(88, 72)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(458, 58)
        Me.GroupBox3.TabIndex = 80
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "promoció:"
        '
        'RadioButtonDto
        '
        Me.RadioButtonDto.AutoSize = True
        Me.RadioButtonDto.Location = New System.Drawing.Point(121, 12)
        Me.RadioButtonDto.Name = "RadioButtonDto"
        Me.RadioButtonDto.Size = New System.Drawing.Size(210, 17)
        Me.RadioButtonDto.TabIndex = 66
        Me.RadioButtonDto.TabStop = True
        Me.RadioButtonDto.Text = "escalat de descompte segons quantitat"
        Me.RadioButtonDto.UseVisualStyleBackColor = True
        '
        'RadioButtonFreeUnits
        '
        Me.RadioButtonFreeUnits.AutoSize = True
        Me.RadioButtonFreeUnits.Location = New System.Drawing.Point(121, 30)
        Me.RadioButtonFreeUnits.Name = "RadioButtonFreeUnits"
        Me.RadioButtonFreeUnits.Size = New System.Drawing.Size(266, 17)
        Me.RadioButtonFreeUnits.TabIndex = 67
        Me.RadioButtonFreeUnits.TabStop = True
        Me.RadioButtonFreeUnits.Text = "unitats sense carrec cada tantes unitats de compra"
        Me.RadioButtonFreeUnits.UseVisualStyleBackColor = True
        '
        'ComboBoxEventos
        '
        Me.ComboBoxEventos.FormattingEnabled = True
        Me.ComboBoxEventos.Location = New System.Drawing.Point(365, 29)
        Me.ComboBoxEventos.Name = "ComboBoxEventos"
        Me.ComboBoxEventos.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxEventos.TabIndex = 79
        '
        'CheckBoxOnlyInStock
        '
        Me.CheckBoxOnlyInStock.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxOnlyInStock.Location = New System.Drawing.Point(507, 29)
        Me.CheckBoxOnlyInStock.Name = "CheckBoxOnlyInStock"
        Me.CheckBoxOnlyInStock.Size = New System.Drawing.Size(211, 16)
        Me.CheckBoxOnlyInStock.TabIndex = 78
        Me.CheckBoxOnlyInStock.Text = "Només per producte en stock"
        Me.CheckBoxOnlyInStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ButtonAddProduct
        '
        Me.ButtonAddProduct.Enabled = False
        Me.ButtonAddProduct.Location = New System.Drawing.Point(354, 43)
        Me.ButtonAddProduct.Name = "ButtonAddProduct"
        Me.ButtonAddProduct.Size = New System.Drawing.Size(45, 22)
        Me.ButtonAddProduct.TabIndex = 64
        Me.ButtonAddProduct.Text = "afegir"
        Me.ButtonAddProduct.UseVisualStyleBackColor = True
        '
        'GroupBoxProducts
        '
        Me.GroupBoxProducts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxProducts.Controls.Add(Me.Xl_LookupProduct1)
        Me.GroupBoxProducts.Controls.Add(Me.Xl_Products1)
        Me.GroupBoxProducts.Controls.Add(Me.ButtonAddProduct)
        Me.GroupBoxProducts.Enabled = False
        Me.GroupBoxProducts.Location = New System.Drawing.Point(88, 161)
        Me.GroupBoxProducts.Name = "GroupBoxProducts"
        Me.GroupBoxProducts.Size = New System.Drawing.Size(415, 264)
        Me.GroupBoxProducts.TabIndex = 77
        Me.GroupBoxProducts.TabStop = False
        Me.GroupBoxProducts.Text = "Productes"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(34, 44)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(314, 20)
        Me.Xl_LookupProduct1.TabIndex = 66
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_Products1
        '
        Me.Xl_Products1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Products1.Location = New System.Drawing.Point(34, 71)
        Me.Xl_Products1.Name = "Xl_Products1"
        Me.Xl_Products1.Size = New System.Drawing.Size(365, 177)
        Me.Xl_Products1.TabIndex = 65
        '
        'Xl_TextBoxNumQty
        '
        Me.Xl_TextBoxNumQty.Location = New System.Drawing.Point(34, 44)
        Me.Xl_TextBoxNumQty.Mat_CustomFormat = Mat.NET.Xl_TextBoxNum.Formats.Numeric
        Me.Xl_TextBoxNumQty.Mat_FormatString = ""
        Me.Xl_TextBoxNumQty.Name = "Xl_TextBoxNumQty"
        Me.Xl_TextBoxNumQty.ReadOnly = False
        Me.Xl_TextBoxNumQty.Size = New System.Drawing.Size(47, 20)
        Me.Xl_TextBoxNumQty.TabIndex = 55
        Me.Xl_TextBoxNumQty.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'GroupBoxQtyDtos
        '
        Me.GroupBoxQtyDtos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxQtyDtos.Controls.Add(Me.Xl_QtyDtos1)
        Me.GroupBoxQtyDtos.Controls.Add(Me.Xl_TextBoxNumQty)
        Me.GroupBoxQtyDtos.Controls.Add(Me.ButtonAddQtyDto)
        Me.GroupBoxQtyDtos.Controls.Add(Me.LabelQty)
        Me.GroupBoxQtyDtos.Controls.Add(Me.LabelDto)
        Me.GroupBoxQtyDtos.Controls.Add(Me.Xl_TextBoxNumDto)
        Me.GroupBoxQtyDtos.Enabled = False
        Me.GroupBoxQtyDtos.Location = New System.Drawing.Point(520, 161)
        Me.GroupBoxQtyDtos.Name = "GroupBoxQtyDtos"
        Me.GroupBoxQtyDtos.Size = New System.Drawing.Size(198, 264)
        Me.GroupBoxQtyDtos.TabIndex = 76
        Me.GroupBoxQtyDtos.TabStop = False
        Me.GroupBoxQtyDtos.Text = "descomptes"
        '
        'ButtonAddQtyDto
        '
        Me.ButtonAddQtyDto.Enabled = False
        Me.ButtonAddQtyDto.Location = New System.Drawing.Point(140, 43)
        Me.ButtonAddQtyDto.Name = "ButtonAddQtyDto"
        Me.ButtonAddQtyDto.Size = New System.Drawing.Size(45, 22)
        Me.ButtonAddQtyDto.TabIndex = 59
        Me.ButtonAddQtyDto.Text = "afegir"
        Me.ButtonAddQtyDto.UseVisualStyleBackColor = True
        '
        'LabelQty
        '
        Me.LabelQty.Location = New System.Drawing.Point(9, 24)
        Me.LabelQty.Name = "LabelQty"
        Me.LabelQty.Size = New System.Drawing.Size(72, 16)
        Me.LabelQty.TabIndex = 56
        Me.LabelQty.Text = "quantitat:"
        Me.LabelQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelDto
        '
        Me.LabelDto.Location = New System.Drawing.Point(87, 24)
        Me.LabelDto.Name = "LabelDto"
        Me.LabelDto.Size = New System.Drawing.Size(47, 16)
        Me.LabelDto.TabIndex = 58
        Me.LabelDto.Text = "dte:"
        Me.LabelDto.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_TextBoxNumDto
        '
        Me.Xl_TextBoxNumDto.Location = New System.Drawing.Point(87, 44)
        Me.Xl_TextBoxNumDto.Mat_CustomFormat = Mat.NET.Xl_TextBoxNum.Formats.percent
        Me.Xl_TextBoxNumDto.Mat_FormatString = ""
        Me.Xl_TextBoxNumDto.Name = "Xl_TextBoxNumDto"
        Me.Xl_TextBoxNumDto.ReadOnly = False
        Me.Xl_TextBoxNumDto.Size = New System.Drawing.Size(47, 20)
        Me.Xl_TextBoxNumDto.TabIndex = 57
        Me.Xl_TextBoxNumDto.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Desde:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Nom:"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(271, 30)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFchTo.TabIndex = 74
        Me.DateTimePickerFchTo.Visible = False
        '
        'CheckBoxTo
        '
        Me.CheckBoxTo.Location = New System.Drawing.Point(207, 32)
        Me.CheckBoxTo.Name = "CheckBoxTo"
        Me.CheckBoxTo.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxTo.TabIndex = 73
        Me.CheckBoxTo.Text = "Caduca"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(88, 30)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFchFrom.TabIndex = 72
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(88, 6)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(630, 20)
        Me.TextBoxNom.TabIndex = 71
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(507, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(618, 4)
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 446)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(726, 31)
        Me.Panel1.TabIndex = 75
        '
        'Xl_QtyDtos1
        '
        Me.Xl_QtyDtos1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_QtyDtos1.Location = New System.Drawing.Point(34, 71)
        Me.Xl_QtyDtos1.Name = "Xl_QtyDtos1"
        Me.Xl_QtyDtos1.Size = New System.Drawing.Size(150, 177)
        Me.Xl_QtyDtos1.TabIndex = 60
        '
        'Frm_Incentiu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 477)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ComboBoxEventos)
        Me.Controls.Add(Me.CheckBoxOnlyInStock)
        Me.Controls.Add(Me.GroupBoxProducts)
        Me.Controls.Add(Me.GroupBoxQtyDtos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxTo)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Incentiu"
        Me.Text = "Incentiu"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBoxProducts.ResumeLayout(False)
        Me.GroupBoxQtyDtos.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonDto As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFreeUnits As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxEventos As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxOnlyInStock As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonAddProduct As System.Windows.Forms.Button
    Friend WithEvents GroupBoxProducts As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_TextBoxNumQty As Mat.NET.Xl_TextBoxNum
    Friend WithEvents GroupBoxQtyDtos As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonAddQtyDto As System.Windows.Forms.Button
    Friend WithEvents LabelQty As System.Windows.Forms.Label
    Friend WithEvents LabelDto As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumDto As Mat.NET.Xl_TextBoxNum
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxTo As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_Products1 As Mat.NET.Xl_Products
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
    Friend WithEvents Xl_QtyDtos1 As Mat.NET.Xl_QtyDtos
End Class
