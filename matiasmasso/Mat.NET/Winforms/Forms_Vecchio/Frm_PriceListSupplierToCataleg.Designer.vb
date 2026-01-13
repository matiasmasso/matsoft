<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceListSupplierToCataleg
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxParent = New System.Windows.Forms.TextBox()
        Me.TextBoxNomProveidor = New System.Windows.Forms.TextBox()
        Me.TextBoxRefProveidor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_PercentCostDto = New Winforms.Xl_Percent()
        Me.Xl_PercentRetail = New Winforms.Xl_Percent()
        Me.Xl_AmtCurRetail = New Winforms.Xl_AmountCur()
        Me.Xl_AmtCurCost = New Winforms.Xl_AmountCur()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabelCost = New System.Windows.Forms.Label()
        Me.TextBoxNomCurt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_PriceList_Customer1 = New Winforms.Xl_PriceList_Customer()
        Me.TextBoxSearchKey = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_AmtCostBrutEur = New Winforms.Xl_Amount()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_PercentDtoOffInvoice = New Winforms.Xl_Percent()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Xl_AmtCurCostTarifa = New Winforms.Xl_AmountCur()
        Me.Xl_PercentDtoOnInvoice = New Winforms.Xl_Percent()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBoxExchange = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDownInnerPack = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxHideUntil = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownInnerPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "categoría:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "tarifa"
        '
        'TextBoxParent
        '
        Me.TextBoxParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxParent.Enabled = False
        Me.TextBoxParent.Location = New System.Drawing.Point(94, 23)
        Me.TextBoxParent.Name = "TextBoxParent"
        Me.TextBoxParent.ReadOnly = True
        Me.TextBoxParent.Size = New System.Drawing.Size(368, 20)
        Me.TextBoxParent.TabIndex = 9
        '
        'TextBoxNomProveidor
        '
        Me.TextBoxNomProveidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomProveidor.Location = New System.Drawing.Point(209, 49)
        Me.TextBoxNomProveidor.Name = "TextBoxNomProveidor"
        Me.TextBoxNomProveidor.ReadOnly = True
        Me.TextBoxNomProveidor.Size = New System.Drawing.Size(253, 20)
        Me.TextBoxNomProveidor.TabIndex = 15
        '
        'TextBoxRefProveidor
        '
        Me.TextBoxRefProveidor.Location = New System.Drawing.Point(94, 49)
        Me.TextBoxRefProveidor.MaxLength = 20
        Me.TextBoxRefProveidor.Name = "TextBoxRefProveidor"
        Me.TextBoxRefProveidor.ReadOnly = True
        Me.TextBoxRefProveidor.Size = New System.Drawing.Size(109, 20)
        Me.TextBoxRefProveidor.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "producte"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 498)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(483, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(264, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(375, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_PercentCostDto
        '
        Me.Xl_PercentCostDto.BackColor = System.Drawing.Color.White
        Me.Xl_PercentCostDto.Enabled = False
        Me.Xl_PercentCostDto.Location = New System.Drawing.Point(198, 404)
        Me.Xl_PercentCostDto.Name = "Xl_PercentCostDto"
        Me.Xl_PercentCostDto.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentCostDto.TabIndex = 137
        Me.Xl_PercentCostDto.Text = "0,00 %"
        Me.Xl_PercentCostDto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentCostDto.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_PercentRetail
        '
        Me.Xl_PercentRetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PercentRetail.Enabled = False
        Me.Xl_PercentRetail.Location = New System.Drawing.Point(198, 429)
        Me.Xl_PercentRetail.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Xl_PercentRetail.Name = "Xl_PercentRetail"
        Me.Xl_PercentRetail.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentRetail.TabIndex = 136
        Me.Xl_PercentRetail.TabStop = False
        Me.Xl_PercentRetail.Text = "0,00 %"
        Me.Xl_PercentRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentRetail.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtCurRetail
        '
        Me.Xl_AmtCurRetail.Amt = Nothing
        Me.Xl_AmtCurRetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmtCurRetail.Location = New System.Drawing.Point(94, 428)
        Me.Xl_AmtCurRetail.Margin = New System.Windows.Forms.Padding(1, 2, 3, 3)
        Me.Xl_AmtCurRetail.Name = "Xl_AmtCurRetail"
        Me.Xl_AmtCurRetail.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurRetail.TabIndex = 129
        '
        'Xl_AmtCurCost
        '
        Me.Xl_AmtCurCost.Amt = Nothing
        Me.Xl_AmtCurCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmtCurCost.Enabled = False
        Me.Xl_AmtCurCost.Location = New System.Drawing.Point(94, 404)
        Me.Xl_AmtCurCost.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_AmtCurCost.Name = "Xl_AmtCurCost"
        Me.Xl_AmtCurCost.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurCost.TabIndex = 126
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 429)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 16)
        Me.Label9.TabIndex = 133
        Me.Label9.Text = "Public:"
        '
        'LabelCost
        '
        Me.LabelCost.Location = New System.Drawing.Point(12, 405)
        Me.LabelCost.Name = "LabelCost"
        Me.LabelCost.Size = New System.Drawing.Size(64, 16)
        Me.LabelCost.TabIndex = 130
        Me.LabelCost.Text = "Cost net"
        '
        'TextBoxNomCurt
        '
        Me.TextBoxNomCurt.Location = New System.Drawing.Point(94, 142)
        Me.TextBoxNomCurt.MaxLength = 20
        Me.TextBoxNomCurt.Name = "TextBoxNomCurt"
        Me.TextBoxNomCurt.Size = New System.Drawing.Size(168, 20)
        Me.TextBoxNomCurt.TabIndex = 139
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "nom curt"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(94, 168)
        Me.TextBoxNom.MaxLength = 50
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(368, 20)
        Me.TextBoxNom.TabIndex = 141
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 140
        Me.Label3.Text = "nom complert"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(278, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(141, 13)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "(l'identifica dins la categoría)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "tarifa"
        '
        'Xl_PriceList_Customer1
        '
        Me.Xl_PriceList_Customer1.IsDirty = False
        Me.Xl_PriceList_Customer1.Location = New System.Drawing.Point(94, 90)
        Me.Xl_PriceList_Customer1.Name = "Xl_PriceList_Customer1"
        Me.Xl_PriceList_Customer1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_PriceList_Customer1.PriceList_Customer = Nothing
        Me.Xl_PriceList_Customer1.ReadOnlyLookup = False
        Me.Xl_PriceList_Customer1.Size = New System.Drawing.Size(368, 20)
        Me.Xl_PriceList_Customer1.TabIndex = 145
        Me.Xl_PriceList_Customer1.Value = Nothing
        '
        'TextBoxSearchKey
        '
        Me.TextBoxSearchKey.Location = New System.Drawing.Point(94, 194)
        Me.TextBoxSearchKey.MaxLength = 200
        Me.TextBoxSearchKey.Name = "TextBoxSearchKey"
        Me.TextBoxSearchKey.Size = New System.Drawing.Size(368, 20)
        Me.TextBoxSearchKey.TabIndex = 147
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 197)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 146
        Me.Label8.Text = "claus"
        '
        'Xl_AmtCostBrutEur
        '
        Me.Xl_AmtCostBrutEur.Amt = Nothing
        Me.Xl_AmtCostBrutEur.Location = New System.Drawing.Point(94, 300)
        Me.Xl_AmtCostBrutEur.Name = "Xl_AmtCostBrutEur"
        Me.Xl_AmtCostBrutEur.ReadOnly = False
        Me.Xl_AmtCostBrutEur.Size = New System.Drawing.Size(68, 20)
        Me.Xl_AmtCostBrutEur.TabIndex = 157
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 303)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 156
        Me.Label11.Text = "Contravalor Eur:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 354)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(121, 13)
        Me.Label14.TabIndex = 153
        Me.Label14.Text = "Descompte fora factura:"
        '
        'Xl_PercentDtoOffInvoice
        '
        Me.Xl_PercentDtoOffInvoice.Enabled = False
        Me.Xl_PercentDtoOffInvoice.Location = New System.Drawing.Point(198, 351)
        Me.Xl_PercentDtoOffInvoice.Name = "Xl_PercentDtoOffInvoice"
        Me.Xl_PercentDtoOffInvoice.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentDtoOffInvoice.TabIndex = 152
        Me.Xl_PercentDtoOffInvoice.Text = "0,00 %"
        Me.Xl_PercentDtoOffInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoOffInvoice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 328)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 13)
        Me.Label15.TabIndex = 151
        Me.Label15.Text = "Descompte en factura:"
        '
        'Xl_AmtCurCostTarifa
        '
        Me.Xl_AmtCurCostTarifa.Amt = Nothing
        Me.Xl_AmtCurCostTarifa.Enabled = False
        Me.Xl_AmtCurCostTarifa.Location = New System.Drawing.Point(94, 241)
        Me.Xl_AmtCurCostTarifa.Name = "Xl_AmtCurCostTarifa"
        Me.Xl_AmtCurCostTarifa.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurCostTarifa.TabIndex = 150
        '
        'Xl_PercentDtoOnInvoice
        '
        Me.Xl_PercentDtoOnInvoice.Enabled = False
        Me.Xl_PercentDtoOnInvoice.Location = New System.Drawing.Point(198, 326)
        Me.Xl_PercentDtoOnInvoice.Name = "Xl_PercentDtoOnInvoice"
        Me.Xl_PercentDtoOnInvoice.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentDtoOnInvoice.TabIndex = 149
        Me.Xl_PercentDtoOnInvoice.Text = "0,00 %"
        Me.Xl_PercentDtoOnInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoOnInvoice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 241)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 13)
        Me.Label16.TabIndex = 148
        Me.Label16.Text = "Tarifa Cost:"
        '
        'TextBoxExchange
        '
        Me.TextBoxExchange.Location = New System.Drawing.Point(94, 268)
        Me.TextBoxExchange.Name = "TextBoxExchange"
        Me.TextBoxExchange.Size = New System.Drawing.Size(68, 20)
        Me.TextBoxExchange.TabIndex = 158
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 271)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(37, 13)
        Me.Label13.TabIndex = 159
        Me.Label13.Text = "Canvi:"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(94, 117)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(368, 20)
        Me.Xl_LookupProduct1.TabIndex = 160
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 219)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 161
        Me.Label10.Text = "Uds/caixa"
        '
        'NumericUpDownInnerPack
        '
        Me.NumericUpDownInnerPack.Location = New System.Drawing.Point(94, 215)
        Me.NumericUpDownInnerPack.Name = "NumericUpDownInnerPack"
        Me.NumericUpDownInnerPack.Size = New System.Drawing.Size(68, 20)
        Me.NumericUpDownInnerPack.TabIndex = 162
        '
        'CheckBoxHideUntil
        '
        Me.CheckBoxHideUntil.AutoSize = True
        Me.CheckBoxHideUntil.Checked = True
        Me.CheckBoxHideUntil.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHideUntil.Location = New System.Drawing.Point(13, 462)
        Me.CheckBoxHideUntil.Name = "CheckBoxHideUntil"
        Me.CheckBoxHideUntil.Size = New System.Drawing.Size(217, 17)
        Me.CheckBoxHideUntil.TabIndex = 163
        Me.CheckBoxHideUntil.Text = "Ocultar a consumidors i profesionals fins "
        Me.CheckBoxHideUntil.UseVisualStyleBackColor = True
        '
        'Frm_PriceListSupplierToCataleg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 529)
        Me.Controls.Add(Me.CheckBoxHideUntil)
        Me.Controls.Add(Me.NumericUpDownInnerPack)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TextBoxExchange)
        Me.Controls.Add(Me.Xl_AmtCostBrutEur)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Xl_PercentDtoOffInvoice)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Xl_AmtCurCostTarifa)
        Me.Controls.Add(Me.Xl_PercentDtoOnInvoice)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TextBoxSearchKey)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Xl_PriceList_Customer1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxNomCurt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_PercentCostDto)
        Me.Controls.Add(Me.Xl_PercentRetail)
        Me.Controls.Add(Me.Xl_AmtCurRetail)
        Me.Controls.Add(Me.Xl_AmtCurCost)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LabelCost)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxParent)
        Me.Controls.Add(Me.TextBoxNomProveidor)
        Me.Controls.Add(Me.TextBoxRefProveidor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_PriceListSupplierToCataleg"
        Me.Text = "Entrada de producte"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownInnerPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxParent As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNomProveidor As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRefProveidor As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Xl_PercentCostDto As Winforms.Xl_Percent
    Friend WithEvents Xl_PercentRetail As Winforms.Xl_Percent
    Friend WithEvents Xl_AmtCurRetail As Winforms.Xl_AmountCur
    Friend WithEvents Xl_AmtCurCost As Winforms.Xl_AmountCur
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents LabelCost As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomCurt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_PriceList_Customer1 As Winforms.Xl_PriceList_Customer
    Friend WithEvents TextBoxSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCostBrutEur As Winforms.Xl_Amount
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentDtoOffInvoice As Winforms.Xl_Percent
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurCostTarifa As Winforms.Xl_AmountCur
    Friend WithEvents Xl_PercentDtoOnInvoice As Winforms.Xl_Percent
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExchange As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownInnerPack As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBoxHideUntil As CheckBox
End Class
