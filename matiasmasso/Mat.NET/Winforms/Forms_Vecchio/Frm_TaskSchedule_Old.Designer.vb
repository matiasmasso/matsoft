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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.LabelNom = New System.Windows.Forms.Label()
        Me.NumericUpDownHour = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDownMinutes = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox()
        Me.CheckedListBoxWeekDays = New System.Windows.Forms.CheckedListBox()
        Me.CheckBoxAllWeekDays = New System.Windows.Forms.CheckBox()
        Me.GroupBoxGivenTime = New System.Windows.Forms.GroupBox()
        Me.GroupBoxInterval = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDownInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButtonIntervals = New System.Windows.Forms.RadioButton()
        Me.RadioButtonGivenTime = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownHour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownMinutes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxGivenTime.SuspendLayout()
        Me.GroupBoxInterval.SuspendLayout()
        CType(Me.NumericUpDownInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 370)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(390, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(171, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(282, 4)
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
        'LabelNom
        '
        Me.LabelNom.AutoSize = True
        Me.LabelNom.Location = New System.Drawing.Point(13, 18)
        Me.LabelNom.Name = "LabelNom"
        Me.LabelNom.Size = New System.Drawing.Size(33, 13)
        Me.LabelNom.TabIndex = 45
        Me.LabelNom.Text = "(nom)"
        '
        'NumericUpDownHour
        '
        Me.NumericUpDownHour.Location = New System.Drawing.Point(60, 16)
        Me.NumericUpDownHour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumericUpDownHour.Name = "NumericUpDownHour"
        Me.NumericUpDownHour.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDownHour.TabIndex = 47
        Me.NumericUpDownHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "hora:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "minuts:"
        '
        'NumericUpDownMinutes
        '
        Me.NumericUpDownMinutes.Location = New System.Drawing.Point(60, 42)
        Me.NumericUpDownMinutes.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumericUpDownMinutes.Name = "NumericUpDownMinutes"
        Me.NumericUpDownMinutes.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDownMinutes.TabIndex = 49
        Me.NumericUpDownMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CheckBoxEnabled
        '
        Me.CheckBoxEnabled.AutoSize = True
        Me.CheckBoxEnabled.Location = New System.Drawing.Point(16, 54)
        Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
        Me.CheckBoxEnabled.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxEnabled.TabIndex = 51
        Me.CheckBoxEnabled.Text = "activat"
        Me.CheckBoxEnabled.UseVisualStyleBackColor = True
        '
        'CheckedListBoxWeekDays
        '
        Me.CheckedListBoxWeekDays.FormattingEnabled = True
        Me.CheckedListBoxWeekDays.Items.AddRange(New Object() {"diumenge", "dilluns", "dimarts", "dimecres", "dijous", "divendres", "dissabte"})
        Me.CheckedListBoxWeekDays.Location = New System.Drawing.Point(208, 39)
        Me.CheckedListBoxWeekDays.Name = "CheckedListBoxWeekDays"
        Me.CheckedListBoxWeekDays.Size = New System.Drawing.Size(86, 109)
        Me.CheckedListBoxWeekDays.TabIndex = 52
        Me.CheckedListBoxWeekDays.Visible = False
        '
        'CheckBoxAllWeekDays
        '
        Me.CheckBoxAllWeekDays.AutoSize = True
        Me.CheckBoxAllWeekDays.Checked = True
        Me.CheckBoxAllWeekDays.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAllWeekDays.Location = New System.Drawing.Point(144, 16)
        Me.CheckBoxAllWeekDays.Name = "CheckBoxAllWeekDays"
        Me.CheckBoxAllWeekDays.Size = New System.Drawing.Size(150, 17)
        Me.CheckBoxAllWeekDays.TabIndex = 53
        Me.CheckBoxAllWeekDays.Text = "tots els dies de la setmana"
        Me.CheckBoxAllWeekDays.UseVisualStyleBackColor = True
        '
        'GroupBoxGivenTime
        '
        Me.GroupBoxGivenTime.Controls.Add(Me.Label2)
        Me.GroupBoxGivenTime.Controls.Add(Me.CheckBoxAllWeekDays)
        Me.GroupBoxGivenTime.Controls.Add(Me.NumericUpDownHour)
        Me.GroupBoxGivenTime.Controls.Add(Me.CheckedListBoxWeekDays)
        Me.GroupBoxGivenTime.Controls.Add(Me.NumericUpDownMinutes)
        Me.GroupBoxGivenTime.Controls.Add(Me.Label3)
        Me.GroupBoxGivenTime.Location = New System.Drawing.Point(51, 115)
        Me.GroupBoxGivenTime.Name = "GroupBoxGivenTime"
        Me.GroupBoxGivenTime.Size = New System.Drawing.Size(306, 160)
        Me.GroupBoxGivenTime.TabIndex = 54
        Me.GroupBoxGivenTime.TabStop = False
        '
        'GroupBoxInterval
        '
        Me.GroupBoxInterval.Controls.Add(Me.Label4)
        Me.GroupBoxInterval.Controls.Add(Me.NumericUpDownInterval)
        Me.GroupBoxInterval.Controls.Add(Me.Label1)
        Me.GroupBoxInterval.Enabled = False
        Me.GroupBoxInterval.Location = New System.Drawing.Point(51, 38)
        Me.GroupBoxInterval.Name = "GroupBoxInterval"
        Me.GroupBoxInterval.Size = New System.Drawing.Size(306, 50)
        Me.GroupBoxInterval.TabIndex = 55
        Me.GroupBoxInterval.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(116, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "minuts"
        '
        'NumericUpDownInterval
        '
        Me.NumericUpDownInterval.Location = New System.Drawing.Point(60, 19)
        Me.NumericUpDownInterval.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumericUpDownInterval.Name = "NumericUpDownInterval"
        Me.NumericUpDownInterval.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDownInterval.TabIndex = 51
        Me.NumericUpDownInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "cada"
        '
        'RadioButtonIntervals
        '
        Me.RadioButtonIntervals.AutoSize = True
        Me.RadioButtonIntervals.Location = New System.Drawing.Point(23, 24)
        Me.RadioButtonIntervals.Name = "RadioButtonIntervals"
        Me.RadioButtonIntervals.Size = New System.Drawing.Size(82, 17)
        Me.RadioButtonIntervals.TabIndex = 56
        Me.RadioButtonIntervals.Text = "per intervals"
        Me.RadioButtonIntervals.UseVisualStyleBackColor = True
        '
        'RadioButtonGivenTime
        '
        Me.RadioButtonGivenTime.AutoSize = True
        Me.RadioButtonGivenTime.Checked = True
        Me.RadioButtonGivenTime.Location = New System.Drawing.Point(23, 102)
        Me.RadioButtonGivenTime.Name = "RadioButtonGivenTime"
        Me.RadioButtonGivenTime.Size = New System.Drawing.Size(116, 17)
        Me.RadioButtonGivenTime.TabIndex = 58
        Me.RadioButtonGivenTime.TabStop = True
        Me.RadioButtonGivenTime.Text = "a determinada hora"
        Me.RadioButtonGivenTime.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBoxInterval)
        Me.GroupBox1.Controls.Add(Me.RadioButtonGivenTime)
        Me.GroupBox1.Controls.Add(Me.GroupBoxGivenTime)
        Me.GroupBox1.Controls.Add(Me.RadioButtonIntervals)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(367, 288)
        Me.GroupBox1.TabIndex = 59
        Me.GroupBox1.TabStop = False
        '
        'Frm_TaskSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 401)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBoxEnabled)
        Me.Controls.Add(Me.LabelNom)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_TaskSchedule"
        Me.Text = "Calendari de activació"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownHour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownMinutes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxGivenTime.ResumeLayout(False)
        Me.GroupBoxGivenTime.PerformLayout()
        Me.GroupBoxInterval.ResumeLayout(False)
        Me.GroupBoxInterval.PerformLayout()
        CType(Me.NumericUpDownInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents LabelNom As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownHour As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownMinutes As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBoxEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBoxWeekDays As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxAllWeekDays As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxGivenTime As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxInterval As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonIntervals As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonGivenTime As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
