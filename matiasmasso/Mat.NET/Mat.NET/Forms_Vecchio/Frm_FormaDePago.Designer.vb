<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FormaDePago
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelNBanc = New System.Windows.Forms.Label()
        Me.ComboBoxNBanc = New System.Windows.Forms.ComboBox()
        Me.LabelText = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDownDias = New System.Windows.Forms.NumericUpDown()
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox()
        Me.TabPageDias = New System.Windows.Forms.TabPage()
        Me.DataGridViewDies = New System.Windows.Forms.DataGridView()
        Me.RadioButtonWeekDays = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDiasDelMes = New System.Windows.Forms.RadioButton()
        Me.TabPageVacances = New System.Windows.Forms.TabPage()
        Me.DataGridViewVacances = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_Contact_Ibans1 = New Mat.NET.Xl_Contact_Ibans()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownDias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDias.SuspendLayout()
        CType(Me.DataGridViewDies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageVacances.SuspendLayout()
        CType(Me.DataGridViewVacances, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageDias)
        Me.TabControl1.Controls.Add(Me.TabPageVacances)
        Me.TabControl1.Location = New System.Drawing.Point(0, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(380, 419)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Contact_Ibans1)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.LabelNBanc)
        Me.TabPage1.Controls.Add(Me.ComboBoxNBanc)
        Me.TabPage1.Controls.Add(Me.LabelText)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.NumericUpDownDias)
        Me.TabPage1.Controls.Add(Me.ComboBoxCfp)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(372, 393)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "GENERAL"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "compte corrent:"
        '
        'LabelNBanc
        '
        Me.LabelNBanc.AutoSize = True
        Me.LabelNBanc.Location = New System.Drawing.Point(43, 135)
        Me.LabelNBanc.Name = "LabelNBanc"
        Me.LabelNBanc.Size = New System.Drawing.Size(66, 13)
        Me.LabelNBanc.TabIndex = 9
        Me.LabelNBanc.Text = "nostre banc:"
        '
        'ComboBoxNBanc
        '
        Me.ComboBoxNBanc.FormattingEnabled = True
        Me.ComboBoxNBanc.Location = New System.Drawing.Point(116, 135)
        Me.ComboBoxNBanc.Name = "ComboBoxNBanc"
        Me.ComboBoxNBanc.Size = New System.Drawing.Size(177, 21)
        Me.ComboBoxNBanc.TabIndex = 8
        '
        'LabelText
        '
        Me.LabelText.AutoSize = True
        Me.LabelText.Location = New System.Drawing.Point(47, 96)
        Me.LabelText.Name = "LabelText"
        Me.LabelText.Size = New System.Drawing.Size(28, 13)
        Me.LabelText.TabIndex = 5
        Me.LabelText.Text = "Text"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(102, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "dies"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-69, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "dies"
        '
        'NumericUpDownDias
        '
        Me.NumericUpDownDias.Increment = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NumericUpDownDias.Location = New System.Drawing.Point(43, 54)
        Me.NumericUpDownDias.Maximum = New Decimal(New Integer() {9000, 0, 0, 0})
        Me.NumericUpDownDias.Name = "NumericUpDownDias"
        Me.NumericUpDownDias.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDownDias.TabIndex = 2
        Me.NumericUpDownDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(43, 26)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(250, 21)
        Me.ComboBoxCfp.TabIndex = 0
        '
        'TabPageDias
        '
        Me.TabPageDias.Controls.Add(Me.DataGridViewDies)
        Me.TabPageDias.Controls.Add(Me.RadioButtonWeekDays)
        Me.TabPageDias.Controls.Add(Me.RadioButtonDiasDelMes)
        Me.TabPageDias.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDias.Name = "TabPageDias"
        Me.TabPageDias.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDias.Size = New System.Drawing.Size(372, 393)
        Me.TabPageDias.TabIndex = 2
        Me.TabPageDias.Text = "DIES DE PAGO"
        '
        'DataGridViewDies
        '
        Me.DataGridViewDies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDies.Location = New System.Drawing.Point(13, 53)
        Me.DataGridViewDies.Name = "DataGridViewDies"
        Me.DataGridViewDies.Size = New System.Drawing.Size(252, 234)
        Me.DataGridViewDies.TabIndex = 2
        '
        'RadioButtonWeekDays
        '
        Me.RadioButtonWeekDays.AutoSize = True
        Me.RadioButtonWeekDays.Location = New System.Drawing.Point(13, 30)
        Me.RadioButtonWeekDays.Name = "RadioButtonWeekDays"
        Me.RadioButtonWeekDays.Size = New System.Drawing.Size(112, 17)
        Me.RadioButtonWeekDays.TabIndex = 1
        Me.RadioButtonWeekDays.Text = "Dies de la semana"
        '
        'RadioButtonDiasDelMes
        '
        Me.RadioButtonDiasDelMes.AutoSize = True
        Me.RadioButtonDiasDelMes.Checked = True
        Me.RadioButtonDiasDelMes.Location = New System.Drawing.Point(13, 14)
        Me.RadioButtonDiasDelMes.Name = "RadioButtonDiasDelMes"
        Me.RadioButtonDiasDelMes.Size = New System.Drawing.Size(85, 17)
        Me.RadioButtonDiasDelMes.TabIndex = 0
        Me.RadioButtonDiasDelMes.TabStop = True
        Me.RadioButtonDiasDelMes.Text = "Dies del mes"
        '
        'TabPageVacances
        '
        Me.TabPageVacances.Controls.Add(Me.DataGridViewVacances)
        Me.TabPageVacances.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVacances.Name = "TabPageVacances"
        Me.TabPageVacances.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVacances.Size = New System.Drawing.Size(372, 393)
        Me.TabPageVacances.TabIndex = 3
        Me.TabPageVacances.Text = "VACANCES"
        '
        'DataGridViewVacances
        '
        Me.DataGridViewVacances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewVacances.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewVacances.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewVacances.Name = "DataGridViewVacances"
        Me.DataGridViewVacances.Size = New System.Drawing.Size(366, 387)
        Me.DataGridViewVacances.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 437)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(165, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(276, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_Contact_Ibans1
        '
        Me.Xl_Contact_Ibans1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact_Ibans1.Location = New System.Drawing.Point(3, 197)
        Me.Xl_Contact_Ibans1.Name = "Xl_Contact_Ibans1"
        Me.Xl_Contact_Ibans1.Size = New System.Drawing.Size(366, 193)
        Me.Xl_Contact_Ibans1.TabIndex = 13
        '
        'Frm_FormaDePago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 468)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_FormaDePago"
        Me.Text = "FORMA DE PAGO"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownDias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDias.ResumeLayout(False)
        Me.TabPageDias.PerformLayout()
        CType(Me.DataGridViewDies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageVacances.ResumeLayout(False)
        CType(Me.DataGridViewVacances, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents LabelNBanc As System.Windows.Forms.Label
    Friend WithEvents ComboBoxNBanc As System.Windows.Forms.ComboBox
    Friend WithEvents LabelText As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownDias As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents TabPageDias As System.Windows.Forms.TabPage
    Friend WithEvents RadioButtonWeekDays As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDiasDelMes As System.Windows.Forms.RadioButton
    Friend WithEvents TabPageVacances As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents DataGridViewDies As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewVacances As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact_Ibans1 As Mat.NET.Xl_Contact_Ibans
End Class
