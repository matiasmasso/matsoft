<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BankBranch
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
        Me.TabControlBank = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Xl_LookupLocation1 = New Winforms.Xl_LookupLocation()
        Me.TextBoxBank = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PictureBoxBankLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxCod = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPageBank = New System.Windows.Forms.TabPage()
        Me.CheckBoxSEPAB2B = New System.Windows.Forms.CheckBox()
        Me.PictureBoxBrowse = New System.Windows.Forms.PictureBox()
        Me.TextBoxBankWeb = New System.Windows.Forms.TextBox()
        Me.LabelWeb = New System.Windows.Forms.Label()
        Me.TextBoxBankCod = New System.Windows.Forms.TextBox()
        Me.TextBoxBankSwift = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxBankTel = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxBankAlias = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxBankNom = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxPais = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_ImageLogo = New Winforms.Xl_Image()
        Me.TabPageIbans = New System.Windows.Forms.TabPage()
        Me.Xl_Ibans1 = New Winforms.Xl_Ibans_Old()
        Me.PictureBoxId = New System.Windows.Forms.PictureBox()
        Me.TabControlBank.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxBankLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageBank.SuspendLayout()
        CType(Me.PictureBoxBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageIbans.SuspendLayout()
        CType(Me.PictureBoxId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControlBank
        '
        Me.TabControlBank.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlBank.Controls.Add(Me.TabPageGral)
        Me.TabControlBank.Controls.Add(Me.TabPageBank)
        Me.TabControlBank.Controls.Add(Me.TabPageIbans)
        Me.TabControlBank.Location = New System.Drawing.Point(1, 27)
        Me.TabControlBank.Name = "TabControlBank"
        Me.TabControlBank.SelectedIndex = 0
        Me.TabControlBank.Size = New System.Drawing.Size(389, 343)
        Me.TabControlBank.TabIndex = 1
        Me.TabControlBank.TabStop = False
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.PictureBoxId)
        Me.TabPageGral.Controls.Add(Me.Xl_LookupLocation1)
        Me.TabPageGral.Controls.Add(Me.TextBoxBank)
        Me.TabPageGral.Controls.Add(Me.Panel1)
        Me.TabPageGral.Controls.Add(Me.PictureBoxBankLogo)
        Me.TabPageGral.Controls.Add(Me.TextBoxCod)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.TextBoxTel)
        Me.TabPageGral.Controls.Add(Me.Label11)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.TextBoxAdr)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(381, 317)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "Oficina"
        '
        'Xl_LookupLocation1
        '
        Me.Xl_LookupLocation1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupLocation1.IsDirty = False
        Me.Xl_LookupLocation1.Location = New System.Drawing.Point(80, 157)
        Me.Xl_LookupLocation1.LocationValue = Nothing
        Me.Xl_LookupLocation1.Name = "Xl_LookupLocation1"
        Me.Xl_LookupLocation1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupLocation1.Size = New System.Drawing.Size(292, 20)
        Me.Xl_LookupLocation1.TabIndex = 54
        Me.Xl_LookupLocation1.Value = Nothing
        '
        'TextBoxBank
        '
        Me.TextBoxBank.Location = New System.Drawing.Point(80, 26)
        Me.TextBoxBank.Multiline = True
        Me.TextBoxBank.Name = "TextBoxBank"
        Me.TextBoxBank.ReadOnly = True
        Me.TextBoxBank.Size = New System.Drawing.Size(292, 48)
        Me.TextBoxBank.TabIndex = 53
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 283)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(375, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(156, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(267, 4)
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
        'PictureBoxBankLogo
        '
        Me.PictureBoxBankLogo.Location = New System.Drawing.Point(29, 26)
        Me.PictureBoxBankLogo.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.PictureBoxBankLogo.Name = "PictureBoxBankLogo"
        Me.PictureBoxBankLogo.Size = New System.Drawing.Size(48, 48)
        Me.PictureBoxBankLogo.TabIndex = 51
        Me.PictureBoxBankLogo.TabStop = False
        '
        'TextBoxCod
        '
        Me.TextBoxCod.Location = New System.Drawing.Point(80, 102)
        Me.TextBoxCod.Name = "TextBoxCod"
        Me.TextBoxCod.Size = New System.Drawing.Size(73, 20)
        Me.TextBoxCod.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "codi:"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Location = New System.Drawing.Point(80, 183)
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(155, 20)
        Me.TextBoxTel.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(29, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "tel.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 159)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "població:"
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAdr.Location = New System.Drawing.Point(80, 129)
        Me.TextBoxAdr.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(292, 20)
        Me.TextBoxAdr.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 132)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "adreça:"
        '
        'TabPageBank
        '
        Me.TabPageBank.Controls.Add(Me.CheckBoxSEPAB2B)
        Me.TabPageBank.Controls.Add(Me.PictureBoxBrowse)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankWeb)
        Me.TabPageBank.Controls.Add(Me.LabelWeb)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankCod)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankSwift)
        Me.TabPageBank.Controls.Add(Me.Label2)
        Me.TabPageBank.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPageBank.Controls.Add(Me.Label5)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankTel)
        Me.TabPageBank.Controls.Add(Me.Label6)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankAlias)
        Me.TabPageBank.Controls.Add(Me.Label7)
        Me.TabPageBank.Controls.Add(Me.TextBoxBankNom)
        Me.TabPageBank.Controls.Add(Me.Label8)
        Me.TabPageBank.Controls.Add(Me.TextBoxPais)
        Me.TabPageBank.Controls.Add(Me.Label9)
        Me.TabPageBank.Controls.Add(Me.Xl_ImageLogo)
        Me.TabPageBank.Location = New System.Drawing.Point(4, 22)
        Me.TabPageBank.Name = "TabPageBank"
        Me.TabPageBank.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageBank.Size = New System.Drawing.Size(381, 317)
        Me.TabPageBank.TabIndex = 2
        Me.TabPageBank.Text = "Entitat"
        Me.TabPageBank.UseVisualStyleBackColor = True
        '
        'CheckBoxSEPAB2B
        '
        Me.CheckBoxSEPAB2B.AutoSize = True
        Me.CheckBoxSEPAB2B.Location = New System.Drawing.Point(89, 269)
        Me.CheckBoxSEPAB2B.Name = "CheckBoxSEPAB2B"
        Me.CheckBoxSEPAB2B.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxSEPAB2B.TabIndex = 65
        Me.CheckBoxSEPAB2B.Text = "SEPA B2B"
        Me.CheckBoxSEPAB2B.UseVisualStyleBackColor = True
        '
        'PictureBoxBrowse
        '
        Me.PictureBoxBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxBrowse.Image = Global.Winforms.My.Resources.Resources.iExplorer
        Me.PictureBoxBrowse.Location = New System.Drawing.Point(359, 244)
        Me.PictureBoxBrowse.Name = "PictureBoxBrowse"
        Me.PictureBoxBrowse.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxBrowse.TabIndex = 64
        Me.PictureBoxBrowse.TabStop = False
        '
        'TextBoxBankWeb
        '
        Me.TextBoxBankWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankWeb.Location = New System.Drawing.Point(89, 242)
        Me.TextBoxBankWeb.MaxLength = 60
        Me.TextBoxBankWeb.Name = "TextBoxBankWeb"
        Me.TextBoxBankWeb.Size = New System.Drawing.Size(264, 20)
        Me.TextBoxBankWeb.TabIndex = 54
        '
        'LabelWeb
        '
        Me.LabelWeb.AutoSize = True
        Me.LabelWeb.Location = New System.Drawing.Point(15, 248)
        Me.LabelWeb.Name = "LabelWeb"
        Me.LabelWeb.Size = New System.Drawing.Size(30, 13)
        Me.LabelWeb.TabIndex = 63
        Me.LabelWeb.Text = "web:"
        '
        'TextBoxBankCod
        '
        Me.TextBoxBankCod.Location = New System.Drawing.Point(89, 136)
        Me.TextBoxBankCod.MaxLength = 10
        Me.TextBoxBankCod.Name = "TextBoxBankCod"
        Me.TextBoxBankCod.Size = New System.Drawing.Size(73, 20)
        Me.TextBoxBankCod.TabIndex = 50
        '
        'TextBoxBankSwift
        '
        Me.TextBoxBankSwift.Location = New System.Drawing.Point(89, 28)
        Me.TextBoxBankSwift.MaxLength = 11
        Me.TextBoxBankSwift.Name = "TextBoxBankSwift"
        Me.TextBoxBankSwift.Size = New System.Drawing.Size(110, 20)
        Me.TextBoxBankSwift.TabIndex = 48
        Me.TextBoxBankSwift.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Swift.:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(307, 294)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsoleto.TabIndex = 61
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "codi:"
        '
        'TextBoxBankTel
        '
        Me.TextBoxBankTel.Location = New System.Drawing.Point(89, 216)
        Me.TextBoxBankTel.MaxLength = 20
        Me.TextBoxBankTel.Name = "TextBoxBankTel"
        Me.TextBoxBankTel.Size = New System.Drawing.Size(105, 20)
        Me.TextBoxBankTel.TabIndex = 53
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 222)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "tel.:"
        '
        'TextBoxBankAlias
        '
        Me.TextBoxBankAlias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankAlias.Location = New System.Drawing.Point(89, 189)
        Me.TextBoxBankAlias.MaxLength = 20
        Me.TextBoxBankAlias.Name = "TextBoxBankAlias"
        Me.TextBoxBankAlias.Size = New System.Drawing.Size(105, 20)
        Me.TextBoxBankAlias.TabIndex = 52
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 195)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "alias:"
        '
        'TextBoxBankNom
        '
        Me.TextBoxBankNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankNom.Location = New System.Drawing.Point(89, 162)
        Me.TextBoxBankNom.MaxLength = 60
        Me.TextBoxBankNom.Name = "TextBoxBankNom"
        Me.TextBoxBankNom.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxBankNom.TabIndex = 51
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "nom:"
        '
        'TextBoxPais
        '
        Me.TextBoxPais.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPais.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxPais.Location = New System.Drawing.Point(89, 94)
        Me.TextBoxPais.Name = "TextBoxPais"
        Me.TextBoxPais.ReadOnly = True
        Me.TextBoxPais.Size = New System.Drawing.Size(286, 20)
        Me.TextBoxPais.TabIndex = 49
        Me.TextBoxPais.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "pais:"
        '
        'Xl_ImageLogo
        '
        Me.Xl_ImageLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageLogo.Bitmap = Nothing
        Me.Xl_ImageLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageLogo.EmptyImageLabelText = ""
        Me.Xl_ImageLogo.IsDirty = False
        Me.Xl_ImageLogo.Location = New System.Drawing.Point(327, 28)
        Me.Xl_ImageLogo.Name = "Xl_ImageLogo"
        Me.Xl_ImageLogo.Size = New System.Drawing.Size(48, 48)
        Me.Xl_ImageLogo.TabIndex = 55
        Me.Xl_ImageLogo.ZipStream = Nothing
        '
        'TabPageIbans
        '
        Me.TabPageIbans.Controls.Add(Me.Xl_Ibans1)
        Me.TabPageIbans.Location = New System.Drawing.Point(4, 22)
        Me.TabPageIbans.Name = "TabPageIbans"
        Me.TabPageIbans.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageIbans.Size = New System.Drawing.Size(381, 317)
        Me.TabPageIbans.TabIndex = 1
        Me.TabPageIbans.Text = "Comptes registrades"
        '
        'Xl_Ibans1
        '
        Me.Xl_Ibans1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Ibans1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Ibans1.Name = "Xl_Ibans1"
        Me.Xl_Ibans1.Size = New System.Drawing.Size(375, 311)
        Me.Xl_Ibans1.TabIndex = 0
        '
        'PictureBoxId
        '
        Me.PictureBoxId.Location = New System.Drawing.Point(160, 102)
        Me.PictureBoxId.Name = "PictureBoxId"
        Me.PictureBoxId.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxId.TabIndex = 55
        Me.PictureBoxId.TabStop = False
        '
        'Frm_BankBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 373)
        Me.Controls.Add(Me.TabControlBank)
        Me.Name = "Frm_BankBranch"
        Me.Text = "Oficina Bancària"
        Me.TabControlBank.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxBankLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageBank.ResumeLayout(False)
        Me.TabPageBank.PerformLayout()
        CType(Me.PictureBoxBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageIbans.ResumeLayout(False)
        CType(Me.PictureBoxId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControlBank As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxBank As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents PictureBoxBankLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxCod As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTel As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAdr As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabPageIbans As System.Windows.Forms.TabPage
    Friend WithEvents Xl_LookupLocation1 As Winforms.Xl_LookupLocation
    Friend WithEvents Xl_Ibans1 As Winforms.Xl_Ibans_Old
    Friend WithEvents TabPageBank As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxSEPAB2B As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBoxBrowse As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxBankWeb As System.Windows.Forms.TextBox
    Friend WithEvents LabelWeb As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankCod As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxBankSwift As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankTel As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankNom As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPais As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageLogo As Winforms.Xl_Image
    Friend WithEvents PictureBoxId As PictureBox
End Class
