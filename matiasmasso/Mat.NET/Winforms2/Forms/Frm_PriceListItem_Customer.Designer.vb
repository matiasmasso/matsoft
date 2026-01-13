<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PricelistItemCustomer
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxPriceList = New System.Windows.Forms.TextBox()
        Me.LabelPriceList = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Xl_LookupProduct()
        Me.Xl_PercentMarginRetail = New Xl_Percent()
        Me.Xl_AmtCostBrutEur = New Xl_Amount()
        Me.Xl_AmtCostNet = New Xl_Amount()
        Me.Xl_PercentDtoOffInvoice = New Xl_Percent()
        Me.Xl_AmtCurCostTarifa = New Xl_AmountCur()
        Me.Xl_PercentDtoOnInvoice = New Xl_Percent()
        Me.Xl_AmtRetail = New Xl_Amount()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(369, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(480, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 247)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(588, 31)
        Me.Panel1.TabIndex = 57
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxPriceList
        '
        Me.TextBoxPriceList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPriceList.Location = New System.Drawing.Point(104, 9)
        Me.TextBoxPriceList.Name = "TextBoxPriceList"
        Me.TextBoxPriceList.ReadOnly = True
        Me.TextBoxPriceList.Size = New System.Drawing.Size(369, 20)
        Me.TextBoxPriceList.TabIndex = 56
        '
        'LabelPriceList
        '
        Me.LabelPriceList.AutoSize = True
        Me.LabelPriceList.Location = New System.Drawing.Point(13, 12)
        Me.LabelPriceList.Name = "LabelPriceList"
        Me.LabelPriceList.Size = New System.Drawing.Size(37, 13)
        Me.LabelPriceList.TabIndex = 55
        Me.LabelPriceList.Text = "Tarifa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Producte:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(480, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(104, 112)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 60
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 212)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "PVP:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "Tarifa Cost:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "Descompte en factura:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 160)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 13)
        Me.Label7.TabIndex = 74
        Me.Label7.Text = "Descompte fora factura:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 187)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "Cost net:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 13)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Contravalor Eur:"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(104, 35)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(369, 20)
        Me.Xl_LookupProduct1.TabIndex = 82
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_PercentMarginRetail
        '
        Me.Xl_PercentMarginRetail.Enabled = False
        Me.Xl_PercentMarginRetail.Location = New System.Drawing.Point(229, 209)
        Me.Xl_PercentMarginRetail.Name = "Xl_PercentMarginRetail"
        Me.Xl_PercentMarginRetail.Size = New System.Drawing.Size(57, 20)
        Me.Xl_PercentMarginRetail.TabIndex = 81
        Me.Xl_PercentMarginRetail.Text = "0,00 %"
        Me.Xl_PercentMarginRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentMarginRetail.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtCostBrutEur
        '
        Me.Xl_AmtCostBrutEur.Amt = Nothing
        Me.Xl_AmtCostBrutEur.Enabled = False
        Me.Xl_AmtCostBrutEur.Location = New System.Drawing.Point(104, 106)
        Me.Xl_AmtCostBrutEur.Name = "Xl_AmtCostBrutEur"
        Me.Xl_AmtCostBrutEur.Size = New System.Drawing.Size(109, 20)
        Me.Xl_AmtCostBrutEur.TabIndex = 78
        '
        'Xl_AmtCostNet
        '
        Me.Xl_AmtCostNet.Amt = Nothing
        Me.Xl_AmtCostNet.Enabled = False
        Me.Xl_AmtCostNet.Location = New System.Drawing.Point(104, 184)
        Me.Xl_AmtCostNet.Name = "Xl_AmtCostNet"
        Me.Xl_AmtCostNet.Size = New System.Drawing.Size(109, 20)
        Me.Xl_AmtCostNet.TabIndex = 76
        '
        'Xl_PercentDtoOffInvoice
        '
        Me.Xl_PercentDtoOffInvoice.Enabled = False
        Me.Xl_PercentDtoOffInvoice.Location = New System.Drawing.Point(156, 158)
        Me.Xl_PercentDtoOffInvoice.Name = "Xl_PercentDtoOffInvoice"
        Me.Xl_PercentDtoOffInvoice.Size = New System.Drawing.Size(57, 20)
        Me.Xl_PercentDtoOffInvoice.TabIndex = 73
        Me.Xl_PercentDtoOffInvoice.Text = "0,00 %"
        Me.Xl_PercentDtoOffInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoOffInvoice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtCurCostTarifa
        '
        Me.Xl_AmtCurCostTarifa.Amt = Nothing
        Me.Xl_AmtCurCostTarifa.Enabled = False
        Me.Xl_AmtCurCostTarifa.Location = New System.Drawing.Point(104, 79)
        Me.Xl_AmtCurCostTarifa.Name = "Xl_AmtCurCostTarifa"
        Me.Xl_AmtCurCostTarifa.Size = New System.Drawing.Size(140, 20)
        Me.Xl_AmtCurCostTarifa.TabIndex = 71
        '
        'Xl_PercentDtoOnInvoice
        '
        Me.Xl_PercentDtoOnInvoice.Enabled = False
        Me.Xl_PercentDtoOnInvoice.Location = New System.Drawing.Point(156, 132)
        Me.Xl_PercentDtoOnInvoice.Name = "Xl_PercentDtoOnInvoice"
        Me.Xl_PercentDtoOnInvoice.Size = New System.Drawing.Size(57, 20)
        Me.Xl_PercentDtoOnInvoice.TabIndex = 70
        Me.Xl_PercentDtoOnInvoice.Text = "0,00 %"
        Me.Xl_PercentDtoOnInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoOnInvoice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtRetail
        '
        Me.Xl_AmtRetail.Amt = Nothing
        Me.Xl_AmtRetail.Location = New System.Drawing.Point(104, 210)
        Me.Xl_AmtRetail.Name = "Xl_AmtRetail"
        Me.Xl_AmtRetail.Size = New System.Drawing.Size(109, 20)
        Me.Xl_AmtRetail.TabIndex = 66
        '
        'Frm_PricelistItemCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 278)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_PercentMarginRetail)
        Me.Controls.Add(Me.Xl_AmtCostBrutEur)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Xl_AmtCostNet)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_PercentDtoOffInvoice)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_AmtCurCostTarifa)
        Me.Controls.Add(Me.Xl_PercentDtoOnInvoice)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtRetail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxPriceList)
        Me.Controls.Add(Me.LabelPriceList)
        Me.Name = "Frm_PricelistItemCustomer"
        Me.Text = "Preus de venda"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxPriceList As System.Windows.Forms.TextBox
    Friend WithEvents LabelPriceList As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtRetail As Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentDtoOnInvoice As Xl_Percent
    Friend WithEvents Xl_AmtCurCostTarifa As Xl_AmountCur
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentDtoOffInvoice As Xl_Percent
    Friend WithEvents Xl_AmtCostNet As Xl_Amount
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCostBrutEur As Xl_Amount
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_PercentMarginRetail As Xl_Percent
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
End Class
