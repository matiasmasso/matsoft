<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FraProveidor
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
        Me.Xl_Contact_Logo1 = New Mat.NET.Xl_Contact_Logo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_AmtBas = New Mat.Net.Xl_Amount()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_AmtIva = New Mat.Net.Xl_Amount()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_AmtLiq = New Mat.Net.Xl_Amount()
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_CtaCarrec = New Mat.NET.Xl_Cta()
        Me.Xl_CtaAbonament = New Mat.NET.Xl_Cta()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_AmtPendentDeAbonar = New Mat.Net.Xl_Amount()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(538, 3)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "numero"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "data"
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(93, 13)
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxNum.TabIndex = 3
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(93, 40)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFch.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 259)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "albarans:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 518)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(689, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(470, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(581, 4)
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
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_AmtBas
        '
        Me.Xl_AmtBas.Amt = Nothing
        Me.Xl_AmtBas.Location = New System.Drawing.Point(93, 72)
        Me.Xl_AmtBas.Name = "Xl_AmtBas"
        Me.Xl_AmtBas.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtBas.TabIndex = 42
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "base imponible"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Iva"
        '
        'Xl_AmtIva
        '
        Me.Xl_AmtIva.Amt = Nothing
        Me.Xl_AmtIva.Location = New System.Drawing.Point(93, 98)
        Me.Xl_AmtIva.Name = "Xl_AmtIva"
        Me.Xl_AmtIva.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtIva.TabIndex = 44
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Total"
        '
        'Xl_AmtLiq
        '
        Me.Xl_AmtLiq.Amt = Nothing
        Me.Xl_AmtLiq.Location = New System.Drawing.Point(93, 124)
        Me.Xl_AmtLiq.Name = "Xl_AmtLiq"
        Me.Xl_AmtLiq.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtLiq.TabIndex = 46
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(93, 151)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(241, 21)
        Me.ComboBoxCfp.TabIndex = 48
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "forma de pago"
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(93, 178)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerVto.TabIndex = 51
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 182)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "venciment"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 210)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "compte carrec"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 236)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "compte abon."
        '
        'Xl_CtaCarrec
        '
        Me.Xl_CtaCarrec.Cta = Nothing
        Me.Xl_CtaCarrec.Location = New System.Drawing.Point(93, 204)
        Me.Xl_CtaCarrec.Name = "Xl_CtaCarrec"
        Me.Xl_CtaCarrec.Size = New System.Drawing.Size(240, 20)
        Me.Xl_CtaCarrec.TabIndex = 55
        '
        'Xl_CtaAbonament
        '
        Me.Xl_CtaAbonament.Cta = Nothing
        Me.Xl_CtaAbonament.Location = New System.Drawing.Point(94, 230)
        Me.Xl_CtaAbonament.Name = "Xl_CtaAbonament"
        Me.Xl_CtaAbonament.Size = New System.Drawing.Size(240, 20)
        Me.Xl_CtaAbonament.TabIndex = 56
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(131, 499)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "pendent de abonar"
        '
        'Xl_AmtPendentDeAbonar
        '
        Me.Xl_AmtPendentDeAbonar.Amt = Nothing
        Me.Xl_AmtPendentDeAbonar.Location = New System.Drawing.Point(234, 492)
        Me.Xl_AmtPendentDeAbonar.Name = "Xl_AmtPendentDeAbonar"
        Me.Xl_AmtPendentDeAbonar.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtPendentDeAbonar.TabIndex = 58
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(18, 431)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(317, 55)
        Me.TextBoxObs.TabIndex = 61
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 415)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(154, 13)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "observacions validació factura:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(15, 276)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(317, 150)
        Me.DataGridView1.TabIndex = 62
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(339, 66)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 63
        '
        'Frm_FraProveidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 549)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Xl_AmtPendentDeAbonar)
        Me.Controls.Add(Me.Xl_CtaAbonament)
        Me.Controls.Add(Me.Xl_CtaCarrec)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.DateTimePickerVto)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ComboBoxCfp)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_AmtLiq)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtIva)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_AmtBas)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Name = "Frm_FraProveidor"
        Me.Text = "FACTURA DE PROVEIDOR"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_AmtBas As Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtIva As Xl_Amount
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtLiq As Xl_Amount
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Xl_CtaCarrec As Xl_Cta
    Friend WithEvents Xl_CtaAbonament As Xl_Cta
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtPendentDeAbonar As Xl_Amount
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
End Class
