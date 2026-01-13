<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CustomerDto
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.RadioButtonAllBrands = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBrand = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Xl_PercentDtoBrut = New Winforms.Xl_Percent()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Xl_PercentDtoNet = New Winforms.Xl_Percent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 309)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(362, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(143, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(254, 4)
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
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.CustomFormat = "dd/MM/yy HH:mm:ss"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(218, 6)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(139, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'RadioButtonAllBrands
        '
        Me.RadioButtonAllBrands.AutoSize = True
        Me.RadioButtonAllBrands.Checked = True
        Me.RadioButtonAllBrands.Location = New System.Drawing.Point(29, 36)
        Me.RadioButtonAllBrands.Name = "RadioButtonAllBrands"
        Me.RadioButtonAllBrands.Size = New System.Drawing.Size(111, 17)
        Me.RadioButtonAllBrands.TabIndex = 54
        Me.RadioButtonAllBrands.TabStop = True
        Me.RadioButtonAllBrands.Text = "Totes les marques"
        Me.RadioButtonAllBrands.UseVisualStyleBackColor = True
        '
        'RadioButtonBrand
        '
        Me.RadioButtonBrand.AutoSize = True
        Me.RadioButtonBrand.Location = New System.Drawing.Point(29, 61)
        Me.RadioButtonBrand.Name = "RadioButtonBrand"
        Me.RadioButtonBrand.Size = New System.Drawing.Size(102, 17)
        Me.RadioButtonBrand.TabIndex = 55
        Me.RadioButtonBrand.Text = "marca comercial"
        Me.RadioButtonBrand.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Descompte"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 179)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Observacions"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(35, 195)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(315, 108)
        Me.TextBoxObs.TabIndex = 59
        '
        'Xl_PercentDtoBrut
        '
        Me.Xl_PercentDtoBrut.Location = New System.Drawing.Point(143, 105)
        Me.Xl_PercentDtoBrut.Name = "Xl_PercentDtoBrut"
        Me.Xl_PercentDtoBrut.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Xl_PercentDtoBrut.Size = New System.Drawing.Size(58, 20)
        Me.Xl_PercentDtoBrut.TabIndex = 57
        Me.Xl_PercentDtoBrut.Text = "0,00 %"
        Me.Xl_PercentDtoBrut.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.Enabled = False
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(143, 58)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(207, 20)
        Me.Xl_LookupProduct1.TabIndex = 53
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_PercentDtoNet
        '
        Me.Xl_PercentDtoNet.Location = New System.Drawing.Point(143, 131)
        Me.Xl_PercentDtoNet.Name = "Xl_PercentDtoNet"
        Me.Xl_PercentDtoNet.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Xl_PercentDtoNet.Size = New System.Drawing.Size(58, 20)
        Me.Xl_PercentDtoNet.TabIndex = 61
        Me.Xl_PercentDtoNet.Text = "0,00 %"
        Me.Xl_PercentDtoNet.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Descompte"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(215, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "(brut de IVA)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(215, 134)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "(net de IVA)"
        '
        'Frm_CustomerDto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 340)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_PercentDtoNet)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_PercentDtoBrut)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButtonBrand)
        Me.Controls.Add(Me.RadioButtonAllBrands)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Frm_CustomerDto"
        Me.Text = "Frm_CustomerDto"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents RadioButtonAllBrands As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBrand As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentDtoBrut As Winforms.Xl_Percent
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Xl_PercentDtoNet As Xl_Percent
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
