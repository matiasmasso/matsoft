<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Bank
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.PictureBoxSwiftValidation = New System.Windows.Forms.PictureBox()
        Me.CheckBoxSEPAB2B = New System.Windows.Forms.CheckBox()
        Me.PictureBoxBrowse = New System.Windows.Forms.PictureBox()
        Me.TextBoxBankWeb = New System.Windows.Forms.TextBox()
        Me.LabelWeb = New System.Windows.Forms.Label()
        Me.TextBoxBankCod = New System.Windows.Forms.TextBox()
        Me.TextBoxBankSwift = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxBankTel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxBankAlias = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxBankNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPais = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_ImageLogo = New Xl_Image()
        Me.TabPageAgcs = New System.Windows.Forms.TabPage()
        Me.Xl_BankBranches1 = New Xl_BankBranches()
        Me.TabPageGrup = New System.Windows.Forms.TabPage()
        Me.Xl_Banks1 = New Xl_Banks()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Ibans1 = New Xl_Ibans_Old()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        CType(Me.PictureBoxSwiftValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageAgcs.SuspendLayout()
        Me.TabPageGrup.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 378)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(446, 31)
        Me.Panel1.TabIndex = 43
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(227, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(338, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 7
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageAgcs)
        Me.TabControl1.Controls.Add(Me.TabPageGrup)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(0, 18)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(442, 358)
        Me.TabControl1.TabIndex = 42
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.PictureBoxSwiftValidation)
        Me.TabPageGral.Controls.Add(Me.CheckBoxSEPAB2B)
        Me.TabPageGral.Controls.Add(Me.PictureBoxBrowse)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankWeb)
        Me.TabPageGral.Controls.Add(Me.LabelWeb)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankCod)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankSwift)
        Me.TabPageGral.Controls.Add(Me.Label11)
        Me.TabPageGral.Controls.Add(Me.CheckBoxObsoleto)
        Me.TabPageGral.Controls.Add(Me.Label5)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankTel)
        Me.TabPageGral.Controls.Add(Me.Label4)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankAlias)
        Me.TabPageGral.Controls.Add(Me.Label3)
        Me.TabPageGral.Controls.Add(Me.TextBoxBankNom)
        Me.TabPageGral.Controls.Add(Me.Label2)
        Me.TabPageGral.Controls.Add(Me.TextBoxPais)
        Me.TabPageGral.Controls.Add(Me.Label1)
        Me.TabPageGral.Controls.Add(Me.Xl_ImageLogo)
        Me.TabPageGral.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGral.Size = New System.Drawing.Size(434, 332)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "ENTITAT"
        '
        'PictureBoxSwiftValidation
        '
        Me.PictureBoxSwiftValidation.Location = New System.Drawing.Point(214, 43)
        Me.PictureBoxSwiftValidation.Name = "PictureBoxSwiftValidation"
        Me.PictureBoxSwiftValidation.Size = New System.Drawing.Size(23, 19)
        Me.PictureBoxSwiftValidation.TabIndex = 48
        Me.PictureBoxSwiftValidation.TabStop = False
        '
        'CheckBoxSEPAB2B
        '
        Me.CheckBoxSEPAB2B.AutoSize = True
        Me.CheckBoxSEPAB2B.Location = New System.Drawing.Point(97, 284)
        Me.CheckBoxSEPAB2B.Name = "CheckBoxSEPAB2B"
        Me.CheckBoxSEPAB2B.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxSEPAB2B.TabIndex = 47
        Me.CheckBoxSEPAB2B.Text = "SEPA B2B"
        Me.CheckBoxSEPAB2B.UseVisualStyleBackColor = True
        '
        'PictureBoxBrowse
        '
        Me.PictureBoxBrowse.Image = Global.Mat.Net.My.Resources.Resources.iExplorer
        Me.PictureBoxBrowse.Location = New System.Drawing.Point(367, 260)
        Me.PictureBoxBrowse.Name = "PictureBoxBrowse"
        Me.PictureBoxBrowse.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxBrowse.TabIndex = 46
        Me.PictureBoxBrowse.TabStop = False
        '
        'TextBoxBankWeb
        '
        Me.TextBoxBankWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankWeb.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.TextBoxBankWeb.Location = New System.Drawing.Point(97, 257)
        Me.TextBoxBankWeb.MaxLength = 60
        Me.TextBoxBankWeb.Name = "TextBoxBankWeb"
        Me.TextBoxBankWeb.Size = New System.Drawing.Size(288, 20)
        Me.TextBoxBankWeb.TabIndex = 6
        '
        'LabelWeb
        '
        Me.LabelWeb.AutoSize = True
        Me.LabelWeb.Location = New System.Drawing.Point(23, 263)
        Me.LabelWeb.Name = "LabelWeb"
        Me.LabelWeb.Size = New System.Drawing.Size(30, 13)
        Me.LabelWeb.TabIndex = 45
        Me.LabelWeb.Text = "web:"
        '
        'TextBoxBankCod
        '
        Me.TextBoxBankCod.Location = New System.Drawing.Point(97, 151)
        Me.TextBoxBankCod.MaxLength = 10
        Me.TextBoxBankCod.Name = "TextBoxBankCod"
        Me.TextBoxBankCod.Size = New System.Drawing.Size(73, 20)
        Me.TextBoxBankCod.TabIndex = 2
        '
        'TextBoxBankSwift
        '
        Me.TextBoxBankSwift.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBoxBankSwift.Location = New System.Drawing.Point(97, 43)
        Me.TextBoxBankSwift.MaxLength = 11
        Me.TextBoxBankSwift.Name = "TextBoxBankSwift"
        Me.TextBoxBankSwift.Size = New System.Drawing.Size(110, 20)
        Me.TextBoxBankSwift.TabIndex = 0
        Me.TextBoxBankSwift.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(23, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Swift.:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(344, 299)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsoleto.TabIndex = 41
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "codi:"
        '
        'TextBoxBankTel
        '
        Me.TextBoxBankTel.Location = New System.Drawing.Point(97, 231)
        Me.TextBoxBankTel.MaxLength = 20
        Me.TextBoxBankTel.Name = "TextBoxBankTel"
        Me.TextBoxBankTel.Size = New System.Drawing.Size(155, 20)
        Me.TextBoxBankTel.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 237)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "tel.:"
        '
        'TextBoxBankAlias
        '
        Me.TextBoxBankAlias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankAlias.Location = New System.Drawing.Point(97, 204)
        Me.TextBoxBankAlias.MaxLength = 20
        Me.TextBoxBankAlias.Name = "TextBoxBankAlias"
        Me.TextBoxBankAlias.Size = New System.Drawing.Size(315, 20)
        Me.TextBoxBankAlias.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 210)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "alias:"
        '
        'TextBoxBankNom
        '
        Me.TextBoxBankNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBankNom.Location = New System.Drawing.Point(97, 177)
        Me.TextBoxBankNom.MaxLength = 60
        Me.TextBoxBankNom.Name = "TextBoxBankNom"
        Me.TextBoxBankNom.Size = New System.Drawing.Size(315, 20)
        Me.TextBoxBankNom.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 183)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "nom:"
        '
        'TextBoxPais
        '
        Me.TextBoxPais.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPais.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxPais.Location = New System.Drawing.Point(97, 109)
        Me.TextBoxPais.Name = "TextBoxPais"
        Me.TextBoxPais.ReadOnly = True
        Me.TextBoxPais.Size = New System.Drawing.Size(315, 20)
        Me.TextBoxPais.TabIndex = 1
        Me.TextBoxPais.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "pais:"
        '
        'Xl_ImageLogo
        '
        Me.Xl_ImageLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageLogo.Bitmap = Nothing
        Me.Xl_ImageLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageLogo.EmptyImageLabelText = ""
        Me.Xl_ImageLogo.IsDirty = False
        Me.Xl_ImageLogo.Location = New System.Drawing.Point(364, 43)
        Me.Xl_ImageLogo.Name = "Xl_ImageLogo"
        Me.Xl_ImageLogo.Size = New System.Drawing.Size(48, 48)
        Me.Xl_ImageLogo.TabIndex = 31
        Me.Xl_ImageLogo.ZipStream = Nothing
        '
        'TabPageAgcs
        '
        Me.TabPageAgcs.Controls.Add(Me.Xl_BankBranches1)
        Me.TabPageAgcs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAgcs.Name = "TabPageAgcs"
        Me.TabPageAgcs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAgcs.Size = New System.Drawing.Size(434, 332)
        Me.TabPageAgcs.TabIndex = 1
        Me.TabPageAgcs.Text = "AGENCIES"
        '
        'Xl_BankBranches1
        '
        Me.Xl_BankBranches1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BankBranches1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_BankBranches1.Name = "Xl_BankBranches1"
        Me.Xl_BankBranches1.Size = New System.Drawing.Size(428, 326)
        Me.Xl_BankBranches1.TabIndex = 0
        '
        'TabPageGrup
        '
        Me.TabPageGrup.Controls.Add(Me.Xl_Banks1)
        Me.TabPageGrup.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGrup.Name = "TabPageGrup"
        Me.TabPageGrup.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGrup.Size = New System.Drawing.Size(434, 332)
        Me.TabPageGrup.TabIndex = 2
        Me.TabPageGrup.Text = "GRUP"
        '
        'Xl_Banks1
        '
        Me.Xl_Banks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Banks1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Banks1.Name = "Xl_Banks1"
        Me.Xl_Banks1.Size = New System.Drawing.Size(428, 326)
        Me.Xl_Banks1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Ibans1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(434, 332)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "COMPTES"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Ibans1
        '
        Me.Xl_Ibans1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Ibans1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Ibans1.Name = "Xl_Ibans1"
        Me.Xl_Ibans1.Size = New System.Drawing.Size(428, 326)
        Me.Xl_Ibans1.TabIndex = 0
        '
        'Frm_Bank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 409)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Bank"
        Me.Text = "Entitat Bancària"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPageGral.PerformLayout()
        CType(Me.PictureBoxSwiftValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageAgcs.ResumeLayout(False)
        Me.TabPageGrup.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents PictureBoxBrowse As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxBankWeb As System.Windows.Forms.TextBox
    Friend WithEvents LabelWeb As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankCod As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxBankSwift As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankTel As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBankNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPais As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageLogo As Xl_Image
    Friend WithEvents TabPageAgcs As System.Windows.Forms.TabPage
    Friend WithEvents TabPageGrup As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxSEPAB2B As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_Banks1 As Xl_Banks
    Friend WithEvents Xl_BankBranches1 As Xl_BankBranches
    Friend WithEvents Xl_Ibans1 As Xl_Ibans_Old
    Friend WithEvents PictureBoxSwiftValidation As PictureBox
End Class
