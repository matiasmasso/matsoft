<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IncentiuOld
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxTo = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelQty = New System.Windows.Forms.Label()
        Me.LabelDto = New System.Windows.Forms.Label()
        Me.ButtonAddQtyDto = New System.Windows.Forms.Button()
        Me.GroupBoxQtyDtos = New System.Windows.Forms.GroupBox()
        Me.DataGridViewDtos = New System.Windows.Forms.DataGridView()
        Me.Xl_TextBoxNumQty = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumDto = New Xl_TextBoxNum()
        Me.GroupBoxProducts = New System.Windows.Forms.GroupBox()
        Me.DataGridViewProducts = New System.Windows.Forms.DataGridView()
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.Xl_LookupProduct1 = New Xl_Lookup_Product_Old()
        Me.CheckBoxOnlyInStock = New System.Windows.Forms.CheckBox()
        Me.ComboBoxEventos = New System.Windows.Forms.ComboBox()
        Me.RadioButtonDto = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFreeUnits = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxQtyDtos.SuspendLayout()
        CType(Me.DataGridViewDtos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxProducts.SuspendLayout()
        CType(Me.DataGridViewProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 447)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(726, 31)
        Me.Panel1.TabIndex = 52
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
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(271, 36)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFchTo.TabIndex = 47
        Me.DateTimePickerFchTo.Visible = False
        '
        'CheckBoxTo
        '
        Me.CheckBoxTo.Location = New System.Drawing.Point(207, 38)
        Me.CheckBoxTo.Name = "CheckBoxTo"
        Me.CheckBoxTo.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxTo.TabIndex = 46
        Me.CheckBoxTo.Text = "Caduca"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(88, 36)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFchFrom.TabIndex = 45
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(88, 12)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(630, 20)
        Me.TextBoxNom.TabIndex = 44
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Desde:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Nom:"
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
        'GroupBoxQtyDtos
        '
        Me.GroupBoxQtyDtos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxQtyDtos.Controls.Add(Me.DataGridViewDtos)
        Me.GroupBoxQtyDtos.Controls.Add(Me.Xl_TextBoxNumQty)
        Me.GroupBoxQtyDtos.Controls.Add(Me.ButtonAddQtyDto)
        Me.GroupBoxQtyDtos.Controls.Add(Me.LabelQty)
        Me.GroupBoxQtyDtos.Controls.Add(Me.LabelDto)
        Me.GroupBoxQtyDtos.Controls.Add(Me.Xl_TextBoxNumDto)
        Me.GroupBoxQtyDtos.Enabled = False
        Me.GroupBoxQtyDtos.Location = New System.Drawing.Point(520, 167)
        Me.GroupBoxQtyDtos.Name = "GroupBoxQtyDtos"
        Me.GroupBoxQtyDtos.Size = New System.Drawing.Size(198, 264)
        Me.GroupBoxQtyDtos.TabIndex = 61
        Me.GroupBoxQtyDtos.TabStop = False
        Me.GroupBoxQtyDtos.Text = "descomptes"
        '
        'DataGridViewDtos
        '
        Me.DataGridViewDtos.AllowUserToAddRows = False
        Me.DataGridViewDtos.AllowUserToDeleteRows = False
        Me.DataGridViewDtos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewDtos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDtos.Location = New System.Drawing.Point(34, 76)
        Me.DataGridViewDtos.Name = "DataGridViewDtos"
        Me.DataGridViewDtos.ReadOnly = True
        Me.DataGridViewDtos.Size = New System.Drawing.Size(151, 172)
        Me.DataGridViewDtos.TabIndex = 66
        '
        'Xl_TextBoxNumQty
        '
        Me.Xl_TextBoxNumQty.Location = New System.Drawing.Point(34, 44)
        Me.Xl_TextBoxNumQty.Mat_CustomFormat = Xl_TextBoxNum.Formats.Numeric
        Me.Xl_TextBoxNumQty.Mat_FormatString = ""
        Me.Xl_TextBoxNumQty.Name = "Xl_TextBoxNumQty"
        Me.Xl_TextBoxNumQty.Size = New System.Drawing.Size(47, 20)
        Me.Xl_TextBoxNumQty.TabIndex = 55
        Me.Xl_TextBoxNumQty.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumDto
        '
        Me.Xl_TextBoxNumDto.Location = New System.Drawing.Point(87, 44)
        Me.Xl_TextBoxNumDto.Mat_CustomFormat = Xl_TextBoxNum.Formats.percent
        Me.Xl_TextBoxNumDto.Mat_FormatString = ""
        Me.Xl_TextBoxNumDto.Name = "Xl_TextBoxNumDto"
        Me.Xl_TextBoxNumDto.Size = New System.Drawing.Size(47, 20)
        Me.Xl_TextBoxNumDto.TabIndex = 57
        Me.Xl_TextBoxNumDto.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'GroupBoxProducts
        '
        Me.GroupBoxProducts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxProducts.Controls.Add(Me.DataGridViewProducts)
        Me.GroupBoxProducts.Controls.Add(Me.ButtonAddProduct)
        Me.GroupBoxProducts.Controls.Add(Me.Xl_LookupProduct1)
        Me.GroupBoxProducts.Enabled = False
        Me.GroupBoxProducts.Location = New System.Drawing.Point(88, 167)
        Me.GroupBoxProducts.Name = "GroupBoxProducts"
        Me.GroupBoxProducts.Size = New System.Drawing.Size(415, 264)
        Me.GroupBoxProducts.TabIndex = 63
        Me.GroupBoxProducts.TabStop = False
        Me.GroupBoxProducts.Text = "Productes"
        '
        'DataGridViewProducts
        '
        Me.DataGridViewProducts.AllowUserToAddRows = False
        Me.DataGridViewProducts.AllowUserToDeleteRows = False
        Me.DataGridViewProducts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewProducts.Location = New System.Drawing.Point(34, 76)
        Me.DataGridViewProducts.Name = "DataGridViewProducts"
        Me.DataGridViewProducts.ReadOnly = True
        Me.DataGridViewProducts.Size = New System.Drawing.Size(365, 172)
        Me.DataGridViewProducts.TabIndex = 65
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
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(34, 44)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(314, 20)
        Me.Xl_LookupProduct1.TabIndex = 63
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'CheckBoxOnlyInStock
        '
        Me.CheckBoxOnlyInStock.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxOnlyInStock.Location = New System.Drawing.Point(507, 35)
        Me.CheckBoxOnlyInStock.Name = "CheckBoxOnlyInStock"
        Me.CheckBoxOnlyInStock.Size = New System.Drawing.Size(211, 16)
        Me.CheckBoxOnlyInStock.TabIndex = 64
        Me.CheckBoxOnlyInStock.Text = "Només per producte en stock"
        Me.CheckBoxOnlyInStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboBoxEventos
        '
        Me.ComboBoxEventos.FormattingEnabled = True
        Me.ComboBoxEventos.Location = New System.Drawing.Point(365, 35)
        Me.ComboBoxEventos.Name = "ComboBoxEventos"
        Me.ComboBoxEventos.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxEventos.TabIndex = 65
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButtonDto)
        Me.GroupBox3.Controls.Add(Me.RadioButtonFreeUnits)
        Me.GroupBox3.Location = New System.Drawing.Point(88, 78)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(458, 58)
        Me.GroupBox3.TabIndex = 68
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "promoció:"
        '
        'Frm_Incentiu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 478)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ComboBoxEventos)
        Me.Controls.Add(Me.CheckBoxOnlyInStock)
        Me.Controls.Add(Me.GroupBoxProducts)
        Me.Controls.Add(Me.GroupBoxQtyDtos)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxTo)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Incentiu"
        Me.Text = "TABLA DE INCENTIU"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxQtyDtos.ResumeLayout(False)
        CType(Me.DataGridViewDtos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxProducts.ResumeLayout(False)
        CType(Me.DataGridViewProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxTo As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumQty As Xl_TextBoxNum
    Friend WithEvents LabelQty As System.Windows.Forms.Label
    Friend WithEvents LabelDto As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumDto As Xl_TextBoxNum
    Friend WithEvents ButtonAddQtyDto As System.Windows.Forms.Button
    Friend WithEvents GroupBoxQtyDtos As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxProducts As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_LookupProduct1 As Xl_Lookup_Product_Old
    Friend WithEvents ButtonAddProduct As System.Windows.Forms.Button
    Friend WithEvents DataGridViewDtos As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewProducts As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBoxOnlyInStock As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxEventos As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonDto As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFreeUnits As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
