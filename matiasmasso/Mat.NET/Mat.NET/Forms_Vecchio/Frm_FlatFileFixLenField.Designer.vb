<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FlatFileFixLenField
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxOpcional = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxDefaultValue = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumPosTo = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumPosFrom = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumLen = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumLin = New Xl_TextBoxNum()
        Me.TextBoxRegex = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(107, 37)
        Me.TextBoxNom.MaxLength = 50
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(444, 20)
        Me.TextBoxNom.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Nom:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 415)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(572, 31)
        Me.Panel1.TabIndex = 57
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(353, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(464, 4)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Camp nº:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "longitud del camp:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 165)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "opcional:"
        '
        'ComboBoxOpcional
        '
        Me.ComboBoxOpcional.FormattingEnabled = True
        Me.ComboBoxOpcional.Location = New System.Drawing.Point(107, 160)
        Me.ComboBoxOpcional.Name = "ComboBoxOpcional"
        Me.ComboBoxOpcional.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxOpcional.TabIndex = 5
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Location = New System.Drawing.Point(107, 187)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxFormat.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 191)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "format:"
        '
        'TextBoxDefaultValue
        '
        Me.TextBoxDefaultValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDefaultValue.Location = New System.Drawing.Point(107, 214)
        Me.TextBoxDefaultValue.MaxLength = 50
        Me.TextBoxDefaultValue.Name = "TextBoxDefaultValue"
        Me.TextBoxDefaultValue.Size = New System.Drawing.Size(444, 20)
        Me.TextBoxDefaultValue.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 217)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 13)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "Valor per defecte:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(107, 266)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(444, 143)
        Me.TextBoxObs.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "Observacions:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "posició inicial:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "posició final:"
        '
        'Xl_TextBoxNumPosTo
        '
        Me.Xl_TextBoxNumPosTo.Location = New System.Drawing.Point(107, 96)
        Me.Xl_TextBoxNumPosTo.Mat_FormatString = ""
        Me.Xl_TextBoxNumPosTo.Name = "Xl_TextBoxNumPosTo"
        Me.Xl_TextBoxNumPosTo.ReadOnly = False
        Me.Xl_TextBoxNumPosTo.Size = New System.Drawing.Size(56, 20)
        Me.Xl_TextBoxNumPosTo.TabIndex = 3
        Me.Xl_TextBoxNumPosTo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumPosFrom
        '
        Me.Xl_TextBoxNumPosFrom.Location = New System.Drawing.Point(107, 71)
        Me.Xl_TextBoxNumPosFrom.Mat_FormatString = ""
        Me.Xl_TextBoxNumPosFrom.Name = "Xl_TextBoxNumPosFrom"
        Me.Xl_TextBoxNumPosFrom.ReadOnly = True
        Me.Xl_TextBoxNumPosFrom.Size = New System.Drawing.Size(56, 20)
        Me.Xl_TextBoxNumPosFrom.TabIndex = 2
        Me.Xl_TextBoxNumPosFrom.TabStop = False
        Me.Xl_TextBoxNumPosFrom.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumLen
        '
        Me.Xl_TextBoxNumLen.Location = New System.Drawing.Point(107, 122)
        Me.Xl_TextBoxNumLen.Mat_FormatString = ""
        Me.Xl_TextBoxNumLen.Name = "Xl_TextBoxNumLen"
        Me.Xl_TextBoxNumLen.ReadOnly = False
        Me.Xl_TextBoxNumLen.Size = New System.Drawing.Size(56, 20)
        Me.Xl_TextBoxNumLen.TabIndex = 4
        Me.Xl_TextBoxNumLen.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumLin
        '
        Me.Xl_TextBoxNumLin.Location = New System.Drawing.Point(107, 13)
        Me.Xl_TextBoxNumLin.Mat_FormatString = ""
        Me.Xl_TextBoxNumLin.Name = "Xl_TextBoxNumLin"
        Me.Xl_TextBoxNumLin.ReadOnly = True
        Me.Xl_TextBoxNumLin.Size = New System.Drawing.Size(56, 20)
        Me.Xl_TextBoxNumLin.TabIndex = 0
        Me.Xl_TextBoxNumLin.TabStop = False
        Me.Xl_TextBoxNumLin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'TextBoxRegex
        '
        Me.TextBoxRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRegex.Location = New System.Drawing.Point(107, 240)
        Me.TextBoxRegex.MaxLength = 50
        Me.TextBoxRegex.Name = "TextBoxRegex"
        Me.TextBoxRegex.Size = New System.Drawing.Size(444, 20)
        Me.TextBoxRegex.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 243)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 75
        Me.Label10.Text = "Regex"
        '
        'Frm_FlatFileFixLenField
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 446)
        Me.Controls.Add(Me.TextBoxRegex)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_TextBoxNumPosTo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Xl_TextBoxNumPosFrom)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxDefaultValue)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxOpcional)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_TextBoxNumLen)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_TextBoxNumLin)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_FlatFileFixLenField"
        Me.Text = "Frm_FlatFileFixLenField"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumLin As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumLen As Xl_TextBoxNum
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxOpcional As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDefaultValue As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumPosFrom As Xl_TextBoxNum
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumPosTo As Xl_TextBoxNum
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRegex As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
