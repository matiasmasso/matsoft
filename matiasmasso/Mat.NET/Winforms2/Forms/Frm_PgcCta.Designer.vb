<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PgcCta
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_LookupPgcClass1 = New Xl_LookupPgcClass()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxDh = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxId = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxCodi = New System.Windows.Forms.ComboBox()
        Me.CheckBoxIsBaseImponibleIva = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIsQuotaIva = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIsQuotaIrpf = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Location = New System.Drawing.Point(89, 149)
        Me.TextBoxEng.MaxLength = 20
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(392, 20)
        Me.TextBoxEng.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 153)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Anglés"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Location = New System.Drawing.Point(89, 123)
        Me.TextBoxCat.MaxLength = 20
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(392, 20)
        Me.TextBoxCat.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 126)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Catalá"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Location = New System.Drawing.Point(89, 97)
        Me.TextBoxEsp.MaxLength = 20
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(392, 20)
        Me.TextBoxEsp.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Espanyol"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 361)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(493, 31)
        Me.Panel1.TabIndex = 67
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(274, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(385, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
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
        Me.ButtonDel.TabIndex = 12
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_LookupPgcClass1
        '
        Me.Xl_LookupPgcClass1.IsDirty = False
        Me.Xl_LookupPgcClass1.Location = New System.Drawing.Point(89, 27)
        Me.Xl_LookupPgcClass1.Name = "Xl_LookupPgcClass1"
        Me.Xl_LookupPgcClass1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcClass1.PgcClass = Nothing
        Me.Xl_LookupPgcClass1.ReadOnlyLookup = False
        Me.Xl_LookupPgcClass1.Size = New System.Drawing.Size(392, 20)
        Me.Xl_LookupPgcClass1.TabIndex = 0
        Me.Xl_LookupPgcClass1.TabStop = False
        Me.Xl_LookupPgcClass1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Classificació"
        '
        'ComboBoxDh
        '
        Me.ComboBoxDh.FormattingEnabled = True
        Me.ComboBoxDh.Items.AddRange(New Object() {"(signe del saldo)", "Saldo deutor", "Saldo creditor"})
        Me.ComboBoxDh.Location = New System.Drawing.Point(89, 194)
        Me.ComboBoxDh.Name = "ComboBoxDh"
        Me.ComboBoxDh.Size = New System.Drawing.Size(192, 21)
        Me.ComboBoxDh.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Saldo"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(89, 53)
        Me.TextBoxId.MaxLength = 50
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxId.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Compte"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 13)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Codi"
        '
        'ComboBoxCodi
        '
        Me.ComboBoxCodi.Enabled = False
        Me.ComboBoxCodi.FormattingEnabled = True
        Me.ComboBoxCodi.Items.AddRange(New Object() {"(signe del saldo)", "Saldo deutor", "Saldo creditor"})
        Me.ComboBoxCodi.Location = New System.Drawing.Point(89, 221)
        Me.ComboBoxCodi.Name = "ComboBoxCodi"
        Me.ComboBoxCodi.Size = New System.Drawing.Size(192, 21)
        Me.ComboBoxCodi.TabIndex = 6
        '
        'CheckBoxIsBaseImponibleIva
        '
        Me.CheckBoxIsBaseImponibleIva.AutoSize = True
        Me.CheckBoxIsBaseImponibleIva.Location = New System.Drawing.Point(89, 272)
        Me.CheckBoxIsBaseImponibleIva.Name = "CheckBoxIsBaseImponibleIva"
        Me.CheckBoxIsBaseImponibleIva.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxIsBaseImponibleIva.TabIndex = 7
        Me.CheckBoxIsBaseImponibleIva.Text = "Es base imponible de IVA"
        Me.CheckBoxIsBaseImponibleIva.UseVisualStyleBackColor = True
        '
        'CheckBoxIsQuotaIva
        '
        Me.CheckBoxIsQuotaIva.AutoSize = True
        Me.CheckBoxIsQuotaIva.Location = New System.Drawing.Point(89, 295)
        Me.CheckBoxIsQuotaIva.Name = "CheckBoxIsQuotaIva"
        Me.CheckBoxIsQuotaIva.Size = New System.Drawing.Size(103, 17)
        Me.CheckBoxIsQuotaIva.TabIndex = 8
        Me.CheckBoxIsQuotaIva.Text = "Es quota de IVA"
        Me.CheckBoxIsQuotaIva.UseVisualStyleBackColor = True
        '
        'CheckBoxIsQuotaIrpf
        '
        Me.CheckBoxIsQuotaIrpf.AutoSize = True
        Me.CheckBoxIsQuotaIrpf.Location = New System.Drawing.Point(89, 318)
        Me.CheckBoxIsQuotaIrpf.Name = "CheckBoxIsQuotaIrpf"
        Me.CheckBoxIsQuotaIrpf.Size = New System.Drawing.Size(110, 17)
        Me.CheckBoxIsQuotaIrpf.TabIndex = 9
        Me.CheckBoxIsQuotaIrpf.Text = "Es quota de IRPF"
        Me.CheckBoxIsQuotaIrpf.UseVisualStyleBackColor = True
        '
        'Frm_PgcCta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(493, 392)
        Me.Controls.Add(Me.CheckBoxIsQuotaIrpf)
        Me.Controls.Add(Me.CheckBoxIsQuotaIva)
        Me.Controls.Add(Me.CheckBoxIsBaseImponibleIva)
        Me.Controls.Add(Me.ComboBoxCodi)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxDh)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_LookupPgcClass1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxEng)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCat)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxEsp)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Frm_PgcCta"
        Me.Text = "Compte"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_LookupPgcClass1 As Xl_LookupPgcClass
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxDh As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxId As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBoxCodi As ComboBox
    Friend WithEvents CheckBoxIsBaseImponibleIva As CheckBox
    Friend WithEvents CheckBoxIsQuotaIva As CheckBox
    Friend WithEvents CheckBoxIsQuotaIrpf As CheckBox
End Class
