<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TaskSchedule
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
        Me.TextBoxTask = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.CheckBoxMon = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTue = New System.Windows.Forms.CheckBox()
        Me.CheckBoxWed = New System.Windows.Forms.CheckBox()
        Me.CheckBoxThu = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFri = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSat = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSun = New System.Windows.Forms.CheckBox()
        Me.RadioButtonInterval = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDaily = New System.Windows.Forms.RadioButton()
        Me.NumericUpDownInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxNextRunFch = New System.Windows.Forms.TextBox()
        Me.GroupBoxWeekdays = New System.Windows.Forms.GroupBox()
        Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox()
        Me.NumericUpDownHours = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownMinutes = New System.Windows.Forms.NumericUpDown()
        Me.LabelTimeSpan = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxWeekdays.SuspendLayout()
        CType(Me.NumericUpDownHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownMinutes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxTask
        '
        Me.TextBoxTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTask.Location = New System.Drawing.Point(77, 29)
        Me.TextBoxTask.Name = "TextBoxTask"
        Me.TextBoxTask.ReadOnly = True
        Me.TextBoxTask.Size = New System.Drawing.Size(249, 20)
        Me.TextBoxTask.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Tasca"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 424)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(338, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(119, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(230, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'CheckBoxMon
        '
        Me.CheckBoxMon.AutoSize = True
        Me.CheckBoxMon.Location = New System.Drawing.Point(33, 21)
        Me.CheckBoxMon.Name = "CheckBoxMon"
        Me.CheckBoxMon.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxMon.TabIndex = 47
        Me.CheckBoxMon.Text = "dilluns"
        Me.CheckBoxMon.UseVisualStyleBackColor = True
        '
        'CheckBoxTue
        '
        Me.CheckBoxTue.AutoSize = True
        Me.CheckBoxTue.Location = New System.Drawing.Point(33, 44)
        Me.CheckBoxTue.Name = "CheckBoxTue"
        Me.CheckBoxTue.Size = New System.Drawing.Size(59, 17)
        Me.CheckBoxTue.TabIndex = 48
        Me.CheckBoxTue.Text = "dimarts"
        Me.CheckBoxTue.UseVisualStyleBackColor = True
        '
        'CheckBoxWed
        '
        Me.CheckBoxWed.AutoSize = True
        Me.CheckBoxWed.Location = New System.Drawing.Point(33, 67)
        Me.CheckBoxWed.Name = "CheckBoxWed"
        Me.CheckBoxWed.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxWed.TabIndex = 49
        Me.CheckBoxWed.Text = "Dimecres"
        Me.CheckBoxWed.UseVisualStyleBackColor = True
        '
        'CheckBoxThu
        '
        Me.CheckBoxThu.AutoSize = True
        Me.CheckBoxThu.Location = New System.Drawing.Point(33, 90)
        Me.CheckBoxThu.Name = "CheckBoxThu"
        Me.CheckBoxThu.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxThu.TabIndex = 50
        Me.CheckBoxThu.Text = "Dijous"
        Me.CheckBoxThu.UseVisualStyleBackColor = True
        '
        'CheckBoxFri
        '
        Me.CheckBoxFri.AutoSize = True
        Me.CheckBoxFri.Location = New System.Drawing.Point(33, 113)
        Me.CheckBoxFri.Name = "CheckBoxFri"
        Me.CheckBoxFri.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxFri.TabIndex = 51
        Me.CheckBoxFri.Text = "Divendres"
        Me.CheckBoxFri.UseVisualStyleBackColor = True
        '
        'CheckBoxSat
        '
        Me.CheckBoxSat.AutoSize = True
        Me.CheckBoxSat.Location = New System.Drawing.Point(33, 136)
        Me.CheckBoxSat.Name = "CheckBoxSat"
        Me.CheckBoxSat.Size = New System.Drawing.Size(67, 17)
        Me.CheckBoxSat.TabIndex = 52
        Me.CheckBoxSat.Text = "Dissabte"
        Me.CheckBoxSat.UseVisualStyleBackColor = True
        '
        'CheckBoxSun
        '
        Me.CheckBoxSun.AutoSize = True
        Me.CheckBoxSun.Location = New System.Drawing.Point(33, 159)
        Me.CheckBoxSun.Name = "CheckBoxSun"
        Me.CheckBoxSun.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxSun.TabIndex = 53
        Me.CheckBoxSun.Text = "Diumenge"
        Me.CheckBoxSun.UseVisualStyleBackColor = True
        '
        'RadioButtonInterval
        '
        Me.RadioButtonInterval.AutoSize = True
        Me.RadioButtonInterval.Location = New System.Drawing.Point(26, 113)
        Me.RadioButtonInterval.Name = "RadioButtonInterval"
        Me.RadioButtonInterval.Size = New System.Drawing.Size(135, 17)
        Me.RadioButtonInterval.TabIndex = 54
        Me.RadioButtonInterval.TabStop = True
        Me.RadioButtonInterval.Text = "Cada interval de minuts"
        Me.RadioButtonInterval.UseVisualStyleBackColor = True
        '
        'RadioButtonDaily
        '
        Me.RadioButtonDaily.AutoSize = True
        Me.RadioButtonDaily.Location = New System.Drawing.Point(26, 136)
        Me.RadioButtonDaily.Name = "RadioButtonDaily"
        Me.RadioButtonDaily.Size = New System.Drawing.Size(163, 17)
        Me.RadioButtonDaily.TabIndex = 55
        Me.RadioButtonDaily.TabStop = True
        Me.RadioButtonDaily.Text = "Cada dia a l'hora programada"
        Me.RadioButtonDaily.UseVisualStyleBackColor = True
        '
        'NumericUpDownInterval
        '
        Me.NumericUpDownInterval.Location = New System.Drawing.Point(215, 113)
        Me.NumericUpDownInterval.Name = "NumericUpDownInterval"
        Me.NumericUpDownInterval.Size = New System.Drawing.Size(80, 20)
        Me.NumericUpDownInterval.TabIndex = 57
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 371)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Propera execució"
        '
        'TextBoxNextRunFch
        '
        Me.TextBoxNextRunFch.Location = New System.Drawing.Point(95, 368)
        Me.TextBoxNextRunFch.Name = "TextBoxNextRunFch"
        Me.TextBoxNextRunFch.ReadOnly = True
        Me.TextBoxNextRunFch.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxNextRunFch.TabIndex = 66
        '
        'GroupBoxWeekdays
        '
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxTue)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxWed)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxMon)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxSun)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxThu)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxFri)
        Me.GroupBoxWeekdays.Controls.Add(Me.CheckBoxSat)
        Me.GroupBoxWeekdays.Location = New System.Drawing.Point(95, 172)
        Me.GroupBoxWeekdays.Name = "GroupBoxWeekdays"
        Me.GroupBoxWeekdays.Size = New System.Drawing.Size(132, 179)
        Me.GroupBoxWeekdays.TabIndex = 59
        Me.GroupBoxWeekdays.TabStop = False
        Me.GroupBoxWeekdays.Text = "Dies de la setmana"
        '
        'CheckBoxEnabled
        '
        Me.CheckBoxEnabled.AutoSize = True
        Me.CheckBoxEnabled.Location = New System.Drawing.Point(26, 78)
        Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
        Me.CheckBoxEnabled.Size = New System.Drawing.Size(64, 17)
        Me.CheckBoxEnabled.TabIndex = 58
        Me.CheckBoxEnabled.Text = "Habilitat"
        Me.CheckBoxEnabled.UseVisualStyleBackColor = True
        '
        'NumericUpDownHours
        '
        Me.NumericUpDownHours.Location = New System.Drawing.Point(215, 136)
        Me.NumericUpDownHours.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumericUpDownHours.Name = "NumericUpDownHours"
        Me.NumericUpDownHours.Size = New System.Drawing.Size(37, 20)
        Me.NumericUpDownHours.TabIndex = 69
        '
        'NumericUpDownMinutes
        '
        Me.NumericUpDownMinutes.Location = New System.Drawing.Point(258, 136)
        Me.NumericUpDownMinutes.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumericUpDownMinutes.Name = "NumericUpDownMinutes"
        Me.NumericUpDownMinutes.Size = New System.Drawing.Size(37, 20)
        Me.NumericUpDownMinutes.TabIndex = 70
        '
        'LabelTimeSpan
        '
        Me.LabelTimeSpan.AutoSize = True
        Me.LabelTimeSpan.Location = New System.Drawing.Point(198, 371)
        Me.LabelTimeSpan.Name = "LabelTimeSpan"
        Me.LabelTimeSpan.Size = New System.Drawing.Size(44, 13)
        Me.LabelTimeSpan.TabIndex = 71
        Me.LabelTimeSpan.Text = "d'aqui a"
        '
        'Frm_TaskSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 455)
        Me.Controls.Add(Me.LabelTimeSpan)
        Me.Controls.Add(Me.NumericUpDownMinutes)
        Me.Controls.Add(Me.NumericUpDownHours)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckBoxEnabled)
        Me.Controls.Add(Me.TextBoxNextRunFch)
        Me.Controls.Add(Me.TextBoxTask)
        Me.Controls.Add(Me.GroupBoxWeekdays)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownInterval)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadioButtonInterval)
        Me.Controls.Add(Me.RadioButtonDaily)
        Me.Name = "Frm_TaskSchedule"
        Me.Text = "Programació Tasca"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxWeekdays.ResumeLayout(False)
        Me.GroupBoxWeekdays.PerformLayout()
        CType(Me.NumericUpDownHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownMinutes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxTask As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents CheckBoxMon As CheckBox
    Friend WithEvents CheckBoxTue As CheckBox
    Friend WithEvents CheckBoxWed As CheckBox
    Friend WithEvents CheckBoxThu As CheckBox
    Friend WithEvents CheckBoxFri As CheckBox
    Friend WithEvents CheckBoxSat As CheckBox
    Friend WithEvents CheckBoxSun As CheckBox
    Friend WithEvents RadioButtonInterval As RadioButton
    Friend WithEvents RadioButtonDaily As RadioButton
    Friend WithEvents NumericUpDownInterval As NumericUpDown
    Friend WithEvents CheckBoxEnabled As CheckBox
    Friend WithEvents GroupBoxWeekdays As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxNextRunFch As TextBox
    Friend WithEvents NumericUpDownHours As NumericUpDown
    Friend WithEvents NumericUpDownMinutes As NumericUpDown
    Friend WithEvents LabelTimeSpan As Label
End Class
