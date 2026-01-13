<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BookFra
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
        Me.TextBoxFranum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxExchangeRate = New System.Windows.Forms.TextBox()
        Me.LabelExchange = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabelParcial = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxCausaExempcio = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxTipoFra = New System.Windows.Forms.ComboBox()
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabelSiiError = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelSiiLastEdited = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.LabelSiiCuadre = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_SiiLog1 = New Winforms.Xl_SiiLog()
        Me.Xl_BaseQuotaIva2 = New Winforms.Xl_BaseQuota()
        Me.Xl_BaseQuotaIrpf = New Winforms.Xl_BaseQuota()
        Me.Xl_BaseQuotaIva = New Winforms.Xl_BaseQuota()
        Me.Xl_AmtTot = New Winforms.Xl_Amount()
        Me.Xl_AmtDevengat = New Winforms.Xl_Amount()
        Me.Xl_AmtCurBaseExento = New Winforms.Xl_AmountCur()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile_Old()
        Me.Xl_Cta1 = New Winforms.Xl_Cta()
        Me.Xl_Contact1 = New Winforms.Xl_Contact2()
        Me.Xl_BaseQuotaIva1 = New Winforms.Xl_BaseQuota()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBoxRegEspOTrascs = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 475)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(610, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 15
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(721, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 14
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
        Me.ButtonDel.TabIndex = 16
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxFranum
        '
        Me.TextBoxFranum.Location = New System.Drawing.Point(128, 63)
        Me.TextBoxFranum.MaxLength = 18
        Me.TextBoxFranum.Name = "TextBoxFranum"
        Me.TextBoxFranum.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxFranum.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Factura numero:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Proveidor:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Compte:"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Location = New System.Drawing.Point(18, 324)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 16)
        Me.Label17.TabIndex = 83
        Me.Label17.Text = "Irpf:"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Location = New System.Drawing.Point(258, 181)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 16)
        Me.Label15.TabIndex = 81
        Me.Label15.Text = "Quota:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Location = New System.Drawing.Point(215, 181)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 16)
        Me.Label14.TabIndex = 80
        Me.Label14.Text = "Tipus:"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Location = New System.Drawing.Point(125, 181)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 16)
        Me.Label13.TabIndex = 79
        Me.Label13.Text = "Base:"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Location = New System.Drawing.Point(18, 273)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 16)
        Me.Label12.TabIndex = 78
        Me.Label12.Text = "Exempt de Iva"
        '
        'TextBoxExchangeRate
        '
        Me.TextBoxExchangeRate.Location = New System.Drawing.Point(256, 299)
        Me.TextBoxExchangeRate.Name = "TextBoxExchangeRate"
        Me.TextBoxExchangeRate.Size = New System.Drawing.Size(74, 20)
        Me.TextBoxExchangeRate.TabIndex = 11
        Me.TextBoxExchangeRate.TabStop = False
        Me.TextBoxExchangeRate.Visible = False
        '
        'LabelExchange
        '
        Me.LabelExchange.AutoSize = True
        Me.LabelExchange.Location = New System.Drawing.Point(212, 303)
        Me.LabelExchange.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelExchange.Name = "LabelExchange"
        Me.LabelExchange.Size = New System.Drawing.Size(37, 13)
        Me.LabelExchange.TabIndex = 76
        Me.LabelExchange.Text = "Canvi:"
        Me.LabelExchange.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(18, 349)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "Total:"
        '
        'LabelParcial
        '
        Me.LabelParcial.BackColor = System.Drawing.SystemColors.Control
        Me.LabelParcial.Location = New System.Drawing.Point(18, 301)
        Me.LabelParcial.Name = "LabelParcial"
        Me.LabelParcial.Size = New System.Drawing.Size(96, 16)
        Me.LabelParcial.TabIndex = 73
        Me.LabelParcial.Text = "Base devengada:"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Location = New System.Drawing.Point(18, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 16)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "Subjecte a IVA"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "Resultat:"
        '
        'ComboBoxCausaExempcio
        '
        Me.ComboBoxCausaExempcio.FormattingEnabled = True
        Me.ComboBoxCausaExempcio.Location = New System.Drawing.Point(256, 272)
        Me.ComboBoxCausaExempcio.Name = "ComboBoxCausaExempcio"
        Me.ComboBoxCausaExempcio.Size = New System.Drawing.Size(202, 21)
        Me.ComboBoxCausaExempcio.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Tipo Factura:"
        '
        'ComboBoxTipoFra
        '
        Me.ComboBoxTipoFra.FormattingEnabled = True
        Me.ComboBoxTipoFra.Location = New System.Drawing.Point(128, 90)
        Me.ComboBoxTipoFra.Name = "ComboBoxTipoFra"
        Me.ComboBoxTipoFra.Size = New System.Drawing.Size(330, 21)
        Me.ComboBoxTipoFra.TabIndex = 4
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(128, 146)
        Me.TextBoxDsc.MaxLength = 60
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(330, 20)
        Me.TextBoxDsc.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 149)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 89
        Me.Label7.Text = "Descripció:"
        '
        'LabelSiiError
        '
        Me.LabelSiiError.AutoSize = True
        Me.LabelSiiError.Location = New System.Drawing.Point(224, 23)
        Me.LabelSiiError.Name = "LabelSiiError"
        Me.LabelSiiError.Size = New System.Drawing.Size(0, 13)
        Me.LabelSiiError.TabIndex = 91
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelSiiLastEdited)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.LabelSiiCuadre)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Xl_SiiLog1)
        Me.GroupBox1.Controls.Add(Me.LabelSiiError)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 375)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(446, 94)
        Me.GroupBox1.TabIndex = 92
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sii"
        '
        'LabelSiiLastEdited
        '
        Me.LabelSiiLastEdited.AutoSize = True
        Me.LabelSiiLastEdited.Location = New System.Drawing.Point(112, 67)
        Me.LabelSiiLastEdited.Name = "LabelSiiLastEdited"
        Me.LabelSiiLastEdited.Size = New System.Drawing.Size(95, 13)
        Me.LabelSiiLastEdited.TabIndex = 95
        Me.LabelSiiLastEdited.Text = "dd/MM/yy HH:mm"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 67)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 13)
        Me.Label11.TabIndex = 94
        Me.Label11.Text = "Ultima modificació:"
        '
        'LabelSiiCuadre
        '
        Me.LabelSiiCuadre.AutoSize = True
        Me.LabelSiiCuadre.Location = New System.Drawing.Point(112, 45)
        Me.LabelSiiCuadre.Name = "LabelSiiCuadre"
        Me.LabelSiiCuadre.Size = New System.Drawing.Size(67, 13)
        Me.LabelSiiCuadre.TabIndex = 93
        Me.LabelSiiCuadre.Text = "Estat cuadre"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 92
        Me.Label8.Text = "Contraste:"
        '
        'Xl_SiiLog1
        '
        Me.Xl_SiiLog1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_SiiLog1.Location = New System.Drawing.Point(115, 19)
        Me.Xl_SiiLog1.Name = "Xl_SiiLog1"
        Me.Xl_SiiLog1.Size = New System.Drawing.Size(330, 20)
        Me.Xl_SiiLog1.TabIndex = 90
        Me.Xl_SiiLog1.TabStop = False
        '
        'Xl_BaseQuotaIva2
        '
        Me.Xl_BaseQuotaIva2.EditQuotaAllowed = True
        Me.Xl_BaseQuotaIva2.Location = New System.Drawing.Point(127, 244)
        Me.Xl_BaseQuotaIva2.Name = "Xl_BaseQuotaIva2"
        Me.Xl_BaseQuotaIva2.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva2.TabIndex = 8
        '
        'Xl_BaseQuotaIrpf
        '
        Me.Xl_BaseQuotaIrpf.EditQuotaAllowed = True
        Me.Xl_BaseQuotaIrpf.Location = New System.Drawing.Point(127, 324)
        Me.Xl_BaseQuotaIrpf.Name = "Xl_BaseQuotaIrpf"
        Me.Xl_BaseQuotaIrpf.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIrpf.TabIndex = 12
        '
        'Xl_BaseQuotaIva
        '
        Me.Xl_BaseQuotaIva.EditQuotaAllowed = True
        Me.Xl_BaseQuotaIva.Location = New System.Drawing.Point(127, 200)
        Me.Xl_BaseQuotaIva.Name = "Xl_BaseQuotaIva"
        Me.Xl_BaseQuotaIva.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva.TabIndex = 6
        '
        'Xl_AmtTot
        '
        Me.Xl_AmtTot.Amt = Nothing
        Me.Xl_AmtTot.Location = New System.Drawing.Point(127, 349)
        Me.Xl_AmtTot.Name = "Xl_AmtTot"
        Me.Xl_AmtTot.ReadOnly = True
        Me.Xl_AmtTot.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtTot.TabIndex = 13
        Me.Xl_AmtTot.TabStop = False
        '
        'Xl_AmtDevengat
        '
        Me.Xl_AmtDevengat.Amt = Nothing
        Me.Xl_AmtDevengat.Location = New System.Drawing.Point(127, 299)
        Me.Xl_AmtDevengat.Name = "Xl_AmtDevengat"
        Me.Xl_AmtDevengat.ReadOnly = True
        Me.Xl_AmtDevengat.Size = New System.Drawing.Size(74, 20)
        Me.Xl_AmtDevengat.TabIndex = 10
        Me.Xl_AmtDevengat.TabStop = False
        '
        'Xl_AmtCurBaseExento
        '
        Me.Xl_AmtCurBaseExento.Amt = Nothing
        Me.Xl_AmtCurBaseExento.Location = New System.Drawing.Point(127, 273)
        Me.Xl_AmtCurBaseExento.Name = "Xl_AmtCurBaseExento"
        Me.Xl_AmtCurBaseExento.Size = New System.Drawing.Size(103, 20)
        Me.Xl_AmtCurBaseExento.TabIndex = 9
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(475, 12)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 13
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(128, 37)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Cta1.TabIndex = 1
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(128, 12)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(330, 20)
        Me.Xl_Contact1.TabIndex = 0
        '
        'Xl_BaseQuotaIva1
        '
        Me.Xl_BaseQuotaIva1.EditQuotaAllowed = True
        Me.Xl_BaseQuotaIva1.Location = New System.Drawing.Point(127, 222)
        Me.Xl_BaseQuotaIva1.Name = "Xl_BaseQuotaIva1"
        Me.Xl_BaseQuotaIva1.Size = New System.Drawing.Size(188, 20)
        Me.Xl_BaseQuotaIva1.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 96
        Me.Label10.Text = "Reg.Esp.O Trasc"
        '
        'ComboBoxRegEspOTrascs
        '
        Me.ComboBoxRegEspOTrascs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxRegEspOTrascs.FormattingEnabled = True
        Me.ComboBoxRegEspOTrascs.Location = New System.Drawing.Point(128, 117)
        Me.ComboBoxRegEspOTrascs.Name = "ComboBoxRegEspOTrascs"
        Me.ComboBoxRegEspOTrascs.Size = New System.Drawing.Size(330, 21)
        Me.ComboBoxRegEspOTrascs.TabIndex = 95
        '
        'Frm_BookFra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 506)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.ComboBoxRegEspOTrascs)
        Me.Controls.Add(Me.Xl_BaseQuotaIva1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_BaseQuotaIva2)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ComboBoxTipoFra)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxCausaExempcio)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Xl_BaseQuotaIrpf)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Xl_BaseQuotaIva)
        Me.Controls.Add(Me.TextBoxExchangeRate)
        Me.Controls.Add(Me.LabelExchange)
        Me.Controls.Add(Me.Xl_AmtTot)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Xl_AmtDevengat)
        Me.Controls.Add(Me.LabelParcial)
        Me.Controls.Add(Me.Xl_AmtCurBaseExento)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_Cta1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxFranum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_BookFra"
        Me.Text = "Registre de llibre de factures rebudes"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxFranum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact2
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile_Old
    Friend WithEvents Label17 As Label
    Friend WithEvents Xl_BaseQuotaIrpf As Xl_BaseQuota
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Xl_BaseQuotaIva As Xl_BaseQuota
    Friend WithEvents TextBoxExchangeRate As TextBox
    Friend WithEvents LabelExchange As Label
    Friend WithEvents Xl_AmtTot As Xl_Amount
    Friend WithEvents Label9 As Label
    Friend WithEvents Xl_AmtDevengat As Xl_Amount
    Friend WithEvents LabelParcial As Label
    Friend WithEvents Xl_AmtCurBaseExento As Xl_AmountCur
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBoxCausaExempcio As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBoxTipoFra As ComboBox
    Friend WithEvents TextBoxDsc As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Xl_SiiLog1 As Xl_SiiLog
    Friend WithEvents LabelSiiError As Label
    Friend WithEvents Xl_BaseQuotaIva2 As Xl_BaseQuota
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents LabelSiiCuadre As Label
    Friend WithEvents LabelSiiLastEdited As Label
    Friend WithEvents Xl_BaseQuotaIva1 As Xl_BaseQuota
    Friend WithEvents Label10 As Label
    Friend WithEvents ComboBoxRegEspOTrascs As ComboBox
End Class
