<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_UserTaskId
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
        Me.TextBoxNomEsp = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxId = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.Xl_Users1 = New Winforms.Xl_Users()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNomCat = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxNomEng = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxNomPor = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RadioButtonEach = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Users1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNomEsp
        '
        Me.TextBoxNomEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomEsp.Location = New System.Drawing.Point(200, 114)
        Me.TextBoxNomEsp.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomEsp.Name = "TextBoxNomEsp"
        Me.TextBoxNomEsp.Size = New System.Drawing.Size(722, 38)
        Me.TextBoxNomEsp.TabIndex = 57
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 987)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(961, 74)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(377, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(673, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(16, 10)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 114)
        Me.Label1.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 32)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Espanyol"
        '
        'ComboBoxId
        '
        Me.ComboBoxId.FormattingEnabled = True
        Me.ComboBoxId.Location = New System.Drawing.Point(200, 65)
        Me.ComboBoxId.Name = "ComboBoxId"
        Me.ComboBoxId.Size = New System.Drawing.Size(454, 39)
        Me.ComboBoxId.TabIndex = 58
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 68)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 32)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Id"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(12, 501)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(779, 38)
        Me.TextBoxEmail.TabIndex = 60
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Enabled = False
        Me.ButtonAdd.Location = New System.Drawing.Point(797, 496)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(148, 47)
        Me.ButtonAdd.TabIndex = 61
        Me.ButtonAdd.Text = "afegir"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Xl_Users1
        '
        Me.Xl_Users1.AllowUserToAddRows = False
        Me.Xl_Users1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Users1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Users1.Filter = Nothing
        Me.Xl_Users1.Location = New System.Drawing.Point(16, 556)
        Me.Xl_Users1.Name = "Xl_Users1"
        Me.Xl_Users1.ReadOnly = True
        Me.Xl_Users1.RowTemplate.Height = 40
        Me.Xl_Users1.Size = New System.Drawing.Size(814, 270)
        Me.Xl_Users1.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 466)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(181, 32)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Subscriptors:"
        '
        'TextBoxNomCat
        '
        Me.TextBoxNomCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomCat.Location = New System.Drawing.Point(200, 166)
        Me.TextBoxNomCat.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomCat.Name = "TextBoxNomCat"
        Me.TextBoxNomCat.Size = New System.Drawing.Size(722, 38)
        Me.TextBoxNomCat.TabIndex = 65
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 166)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 32)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "Català"
        '
        'TextBoxNomEng
        '
        Me.TextBoxNomEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomEng.Location = New System.Drawing.Point(200, 219)
        Me.TextBoxNomEng.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomEng.Name = "TextBoxNomEng"
        Me.TextBoxNomEng.Size = New System.Drawing.Size(722, 38)
        Me.TextBoxNomEng.TabIndex = 67
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 219)
        Me.Label5.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 32)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "Anglès"
        '
        'TextBoxNomPor
        '
        Me.TextBoxNomPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomPor.Location = New System.Drawing.Point(200, 271)
        Me.TextBoxNomPor.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxNomPor.Name = "TextBoxNomPor"
        Me.TextBoxNomPor.Size = New System.Drawing.Size(722, 38)
        Me.TextBoxNomPor.TabIndex = 71
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 271)
        Me.Label7.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(145, 32)
        Me.Label7.TabIndex = 70
        Me.Label7.Text = "Portuguès"
        '
        'RadioButtonEach
        '
        Me.RadioButtonEach.AutoSize = True
        Me.RadioButtonEach.Checked = True
        Me.RadioButtonEach.Location = New System.Drawing.Point(200, 335)
        Me.RadioButtonEach.Name = "RadioButtonEach"
        Me.RadioButtonEach.Size = New System.Drawing.Size(500, 36)
        Me.RadioButtonEach.TabIndex = 72
        Me.RadioButtonEach.TabStop = True
        Me.RadioButtonEach.Text = "Cada usuari completa la seva tasca"
        Me.RadioButtonEach.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(200, 377)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(493, 36)
        Me.RadioButtonAll.TabIndex = 73
        Me.RadioButtonAll.Text = "Un usuari completa la tasca de tots"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'Frm_UserTaskId
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 1061)
        Me.Controls.Add(Me.RadioButtonAll)
        Me.Controls.Add(Me.RadioButtonEach)
        Me.Controls.Add(Me.TextBoxNomPor)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxNomEng)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxNomCat)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_Users1)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxId)
        Me.Controls.Add(Me.TextBoxNomEsp)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_UserTaskId"
        Me.Text = "Tasca d'usuari"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Users1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxNomEsp As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxId As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents Xl_Users1 As Xl_Users
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxNomCat As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxNomEng As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxNomPor As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents RadioButtonEach As RadioButton
    Friend WithEvents RadioButtonAll As RadioButton
End Class
