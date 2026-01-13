<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mrt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label5 = New System.Windows.Forms.Label
        Me.Xl_AmtCur1 = New Xl_AmountCur
        Me.TextBoxTipus = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_Cta1 = New Xl_Cta
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxAltaYea = New System.Windows.Forms.TextBox
        Me.TextBoxAltaCca = New System.Windows.Forms.TextBox
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox
        Me.GroupBoxBaixa = New System.Windows.Forms.GroupBox
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker
        Me.TextBoxBaixa = New System.Windows.Forms.TextBox
        Me.ButtonCcaBaixaShow = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxBaixa.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(7, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Valor:"
        '
        'Xl_AmtCur1
        '
        Me.Xl_AmtCur1.Amt = Nothing
        Me.Xl_AmtCur1.Location = New System.Drawing.Point(79, 60)
        Me.Xl_AmtCur1.Name = "Xl_AmtCur1"
        Me.Xl_AmtCur1.Size = New System.Drawing.Size(120, 20)
        Me.Xl_AmtCur1.TabIndex = 4
        '
        'TextBoxTipus
        '
        Me.TextBoxTipus.Location = New System.Drawing.Point(79, 108)
        Me.TextBoxTipus.Name = "TextBoxTipus"
        Me.TextBoxTipus.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxTipus.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(7, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Tipus:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(79, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Adquisició:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(7, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Compte:"
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(79, 36)
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(384, 20)
        Me.TextBoxDsc.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Concepte:"
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(79, 84)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(176, 20)
        Me.Xl_Cta1.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(-1, 216)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(495, 280)
        Me.DataGridView1.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 498)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(496, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(277, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(388, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 8
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(183, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 16)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Assentament:"
        '
        'TextBoxAltaYea
        '
        Me.TextBoxAltaYea.Location = New System.Drawing.Point(261, 12)
        Me.TextBoxAltaYea.Name = "TextBoxAltaYea"
        Me.TextBoxAltaYea.Size = New System.Drawing.Size(57, 20)
        Me.TextBoxAltaYea.TabIndex = 1
        '
        'TextBoxAltaCca
        '
        Me.TextBoxAltaCca.Location = New System.Drawing.Point(324, 12)
        Me.TextBoxAltaCca.Name = "TextBoxAltaCca"
        Me.TextBoxAltaCca.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxAltaCca.TabIndex = 2
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(13, 139)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxBaixa.TabIndex = 43
        Me.CheckBoxBaixa.Text = "Baixa"
        Me.CheckBoxBaixa.UseVisualStyleBackColor = True
        '
        'GroupBoxBaixa
        '
        Me.GroupBoxBaixa.Controls.Add(Me.ButtonCcaBaixaShow)
        Me.GroupBoxBaixa.Controls.Add(Me.TextBoxBaixa)
        Me.GroupBoxBaixa.Controls.Add(Me.DateTimePickerBaixa)
        Me.GroupBoxBaixa.Location = New System.Drawing.Point(13, 144)
        Me.GroupBoxBaixa.Name = "GroupBoxBaixa"
        Me.GroupBoxBaixa.Size = New System.Drawing.Size(458, 66)
        Me.GroupBoxBaixa.TabIndex = 44
        Me.GroupBoxBaixa.TabStop = False
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Enabled = False
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(311, 9)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePickerBaixa.TabIndex = 0
        '
        'TextBoxBaixa
        '
        Me.TextBoxBaixa.Enabled = False
        Me.TextBoxBaixa.Location = New System.Drawing.Point(66, 35)
        Me.TextBoxBaixa.Name = "TextBoxBaixa"
        Me.TextBoxBaixa.Size = New System.Drawing.Size(384, 20)
        Me.TextBoxBaixa.TabIndex = 1
        '
        'ButtonCcaBaixaShow
        '
        Me.ButtonCcaBaixaShow.Enabled = False
        Me.ButtonCcaBaixaShow.Image = My.Resources.Resources.prismatics
        Me.ButtonCcaBaixaShow.Location = New System.Drawing.Point(412, 9)
        Me.ButtonCcaBaixaShow.Name = "ButtonCcaBaixaShow"
        Me.ButtonCcaBaixaShow.Size = New System.Drawing.Size(35, 21)
        Me.ButtonCcaBaixaShow.TabIndex = 2
        Me.ButtonCcaBaixaShow.Text = "..."
        Me.ButtonCcaBaixaShow.UseVisualStyleBackColor = True
        '
        'Frm_Mrt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 529)
        Me.Controls.Add(Me.CheckBoxBaixa)
        Me.Controls.Add(Me.TextBoxAltaCca)
        Me.Controls.Add(Me.TextBoxAltaYea)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_AmtCur1)
        Me.Controls.Add(Me.TextBoxTipus)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Cta1)
        Me.Controls.Add(Me.GroupBoxBaixa)
        Me.Name = "Frm_Mrt"
        Me.Text = "TABLA D'AMORTITZACIÓ"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxBaixa.ResumeLayout(False)
        Me.GroupBoxBaixa.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCur1 As Xl_AmountCur
    Friend WithEvents TextBoxTipus As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAltaYea As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAltaCca As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxBaixa As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonCcaBaixaShow As System.Windows.Forms.Button
    Friend WithEvents TextBoxBaixa As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
End Class
