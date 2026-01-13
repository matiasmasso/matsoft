<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TarifaDto
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
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.CheckBoxContact = New System.Windows.Forms.CheckBox()
        Me.CheckBoxProduct = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProductBrand1 = New Winforms.Xl_LookupProductBrand()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_PercentDto = New Winforms.Xl_Percent()
        Me.TextBoxMasterDto = New System.Windows.Forms.TextBox()
        Me.TextBoxResultDto = New System.Windows.Forms.TextBox()
        Me.TextBoxUsr = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(114, 92)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 59
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(0, 176)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(339, 147)
        Me.TextBoxObs.TabIndex = 58
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 343)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(339, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(120, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(231, 4)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Observacions"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(114, 50)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(221, 20)
        Me.Xl_Contact21.TabIndex = 60
        Me.Xl_Contact21.Visible = False
        '
        'CheckBoxContact
        '
        Me.CheckBoxContact.AutoSize = True
        Me.CheckBoxContact.Location = New System.Drawing.Point(6, 52)
        Me.CheckBoxContact.Name = "CheckBoxContact"
        Me.CheckBoxContact.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxContact.TabIndex = 61
        Me.CheckBoxContact.Text = "Client"
        Me.CheckBoxContact.UseVisualStyleBackColor = True
        '
        'CheckBoxProduct
        '
        Me.CheckBoxProduct.AutoSize = True
        Me.CheckBoxProduct.Location = New System.Drawing.Point(6, 30)
        Me.CheckBoxProduct.Name = "CheckBoxProduct"
        Me.CheckBoxProduct.Size = New System.Drawing.Size(104, 17)
        Me.CheckBoxProduct.TabIndex = 62
        Me.CheckBoxProduct.Text = "Marca comercial"
        Me.CheckBoxProduct.UseVisualStyleBackColor = True
        '
        'Xl_LookupProductBrand1
        '
        Me.Xl_LookupProductBrand1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProductBrand1.IsDirty = False
        Me.Xl_LookupProductBrand1.Location = New System.Drawing.Point(114, 24)
        Me.Xl_LookupProductBrand1.Name = "Xl_LookupProductBrand1"
        Me.Xl_LookupProductBrand1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProductBrand1.Product = Nothing
        Me.Xl_LookupProductBrand1.Size = New System.Drawing.Size(221, 20)
        Me.Xl_LookupProductBrand1.TabIndex = 63
        Me.Xl_LookupProductBrand1.Value = Nothing
        Me.Xl_LookupProductBrand1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "a partir de"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "Descompte"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(166, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 68
        Me.Label4.Text = "+"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(234, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "="
        '
        'Xl_PercentDto
        '
        Me.Xl_PercentDto.Location = New System.Drawing.Point(181, 118)
        Me.Xl_PercentDto.Name = "Xl_PercentDto"
        Me.Xl_PercentDto.Size = New System.Drawing.Size(51, 20)
        Me.Xl_PercentDto.TabIndex = 72
        Me.Xl_PercentDto.Text = "0,00 %"
        Me.Xl_PercentDto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDto.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'TextBoxMasterDto
        '
        Me.TextBoxMasterDto.Location = New System.Drawing.Point(114, 118)
        Me.TextBoxMasterDto.Name = "TextBoxMasterDto"
        Me.TextBoxMasterDto.ReadOnly = True
        Me.TextBoxMasterDto.Size = New System.Drawing.Size(51, 20)
        Me.TextBoxMasterDto.TabIndex = 73
        Me.TextBoxMasterDto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxResultDto
        '
        Me.TextBoxResultDto.Location = New System.Drawing.Point(248, 118)
        Me.TextBoxResultDto.Name = "TextBoxResultDto"
        Me.TextBoxResultDto.ReadOnly = True
        Me.TextBoxResultDto.Size = New System.Drawing.Size(51, 20)
        Me.TextBoxResultDto.TabIndex = 74
        Me.TextBoxResultDto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxUsr
        '
        Me.TextBoxUsr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsr.Location = New System.Drawing.Point(0, 322)
        Me.TextBoxUsr.Name = "TextBoxUsr"
        Me.TextBoxUsr.ReadOnly = True
        Me.TextBoxUsr.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxUsr.TabIndex = 75
        '
        'Frm_TarifaDto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 374)
        Me.Controls.Add(Me.TextBoxUsr)
        Me.Controls.Add(Me.TextBoxResultDto)
        Me.Controls.Add(Me.TextBoxMasterDto)
        Me.Controls.Add(Me.Xl_PercentDto)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_LookupProductBrand1)
        Me.Controls.Add(Me.CheckBoxProduct)
        Me.Controls.Add(Me.CheckBoxContact)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_TarifaDto"
        Me.Text = "Descompte per càlcul tarifa"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents CheckBoxContact As CheckBox
    Friend WithEvents CheckBoxProduct As CheckBox
    Friend WithEvents Xl_LookupProductBrand1 As Xl_LookupProductBrand
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_PercentDto As Xl_Percent
    Friend WithEvents TextBoxMasterDto As TextBox
    Friend WithEvents TextBoxResultDto As TextBox
    Friend WithEvents TextBoxUsr As TextBox
End Class
