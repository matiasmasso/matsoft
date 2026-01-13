<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TelLinia
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
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxCentraleta = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxGrupDeResposta = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numero:"
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNum.Location = New System.Drawing.Point(94, 34)
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(101, 20)
        Me.TextBoxNum.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 381)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(281, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(392, 4)
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
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(94, 60)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxObs.TabIndex = 47
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Observacions:"
        '
        'CheckBoxCentraleta
        '
        Me.CheckBoxCentraleta.AutoSize = True
        Me.CheckBoxCentraleta.Location = New System.Drawing.Point(6, 0)
        Me.CheckBoxCentraleta.Name = "CheckBoxCentraleta"
        Me.CheckBoxCentraleta.Size = New System.Drawing.Size(157, 17)
        Me.CheckBoxCentraleta.TabIndex = 48
        Me.CheckBoxCentraleta.Text = "gestionada per la centraleta"
        Me.CheckBoxCentraleta.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Grup de resposta:"
        '
        'ComboBoxGrupDeResposta
        '
        Me.ComboBoxGrupDeResposta.FormattingEnabled = True
        Me.ComboBoxGrupDeResposta.Location = New System.Drawing.Point(116, 60)
        Me.ComboBoxGrupDeResposta.Name = "ComboBoxGrupDeResposta"
        Me.ComboBoxGrupDeResposta.Size = New System.Drawing.Size(257, 21)
        Me.ComboBoxGrupDeResposta.TabIndex = 50
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(94, 87)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePickerAlta.TabIndex = 51
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "alta:"
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(16, 113)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxBaixa.TabIndex = 53
        Me.CheckBoxBaixa.Text = "baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(94, 113)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePickerBaixa.TabIndex = 54
        Me.DateTimePickerBaixa.Visible = False
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(16, 137)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxPrivat.TabIndex = 55
        Me.CheckBoxPrivat.Text = "Privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        Me.CheckBoxPrivat.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBoxGrupDeResposta)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CheckBoxCentraleta)
        Me.GroupBox1.Location = New System.Drawing.Point(94, 217)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(385, 100)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        '
        'Frm_TelLinia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(500, 412)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBoxPrivat)
        Me.Controls.Add(Me.DateTimePickerBaixa)
        Me.Controls.Add(Me.CheckBoxBaixa)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_TelLinia"
        Me.Text = "LINIA TELEFONICA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCentraleta As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxGrupDeResposta As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
