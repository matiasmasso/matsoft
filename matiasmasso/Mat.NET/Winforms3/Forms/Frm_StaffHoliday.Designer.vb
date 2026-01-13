<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StaffHoliday
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
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelFchTo = New System.Windows.Forms.Label()
        Me.TextBoxTitularNom = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxHourFrom = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerHourFrom = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerHourTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxHourTo = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMoreDays = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(90, 172)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxObs.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 210)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(359, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(140, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(251, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 4
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
        Me.ButtonDel.TabIndex = 6
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Observacions"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Location = New System.Drawing.Point(90, 66)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(123, 21)
        Me.ComboBoxCod.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Codi"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchFrom.CustomFormat = "dd/MM/yy   HH:mm"
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(90, 93)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePickerFchFrom.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Des de"
        '
        'LabelFchTo
        '
        Me.LabelFchTo.AutoSize = True
        Me.LabelFchTo.Location = New System.Drawing.Point(12, 152)
        Me.LabelFchTo.Name = "LabelFchTo"
        Me.LabelFchTo.Size = New System.Drawing.Size(26, 13)
        Me.LabelFchTo.TabIndex = 64
        Me.LabelFchTo.Text = "Fins"
        Me.LabelFchTo.Visible = False
        '
        'TextBoxTitularNom
        '
        Me.TextBoxTitularNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitularNom.Location = New System.Drawing.Point(90, 40)
        Me.TextBoxTitularNom.Name = "TextBoxTitularNom"
        Me.TextBoxTitularNom.ReadOnly = True
        Me.TextBoxTitularNom.Size = New System.Drawing.Size(257, 20)
        Me.TextBoxTitularNom.TabIndex = 66
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Titular"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchTo.CustomFormat = "dd/MM/yy   HH:mm"
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(90, 146)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePickerFchTo.TabIndex = 2
        Me.DateTimePickerFchTo.Visible = False
        '
        'CheckBoxHourFrom
        '
        Me.CheckBoxHourFrom.AutoSize = True
        Me.CheckBoxHourFrom.Location = New System.Drawing.Point(207, 96)
        Me.CheckBoxHourFrom.Name = "CheckBoxHourFrom"
        Me.CheckBoxHourFrom.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxHourFrom.TabIndex = 67
        Me.CheckBoxHourFrom.Text = "Hora:"
        Me.CheckBoxHourFrom.UseVisualStyleBackColor = True
        '
        'DateTimePickerHourFrom
        '
        Me.DateTimePickerHourFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerHourFrom.CustomFormat = "HH:mm"
        Me.DateTimePickerHourFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerHourFrom.Location = New System.Drawing.Point(265, 93)
        Me.DateTimePickerHourFrom.Name = "DateTimePickerHourFrom"
        Me.DateTimePickerHourFrom.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePickerHourFrom.TabIndex = 68
        Me.DateTimePickerHourFrom.Value = New Date(2019, 5, 14, 0, 0, 0, 0)
        Me.DateTimePickerHourFrom.Visible = False
        '
        'DateTimePickerHourTo
        '
        Me.DateTimePickerHourTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerHourTo.CustomFormat = "HH:mm"
        Me.DateTimePickerHourTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerHourTo.Location = New System.Drawing.Point(265, 146)
        Me.DateTimePickerHourTo.Name = "DateTimePickerHourTo"
        Me.DateTimePickerHourTo.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePickerHourTo.TabIndex = 70
        Me.DateTimePickerHourTo.Value = New Date(2019, 5, 14, 0, 0, 0, 0)
        Me.DateTimePickerHourTo.Visible = False
        '
        'CheckBoxHourTo
        '
        Me.CheckBoxHourTo.AutoSize = True
        Me.CheckBoxHourTo.Location = New System.Drawing.Point(207, 149)
        Me.CheckBoxHourTo.Name = "CheckBoxHourTo"
        Me.CheckBoxHourTo.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxHourTo.TabIndex = 69
        Me.CheckBoxHourTo.Text = "Hora:"
        Me.CheckBoxHourTo.UseVisualStyleBackColor = True
        Me.CheckBoxHourTo.Visible = False
        '
        'CheckBoxMoreDays
        '
        Me.CheckBoxMoreDays.AutoSize = True
        Me.CheckBoxMoreDays.Location = New System.Drawing.Point(90, 119)
        Me.CheckBoxMoreDays.Name = "CheckBoxMoreDays"
        Me.CheckBoxMoreDays.Size = New System.Drawing.Size(95, 17)
        Me.CheckBoxMoreDays.TabIndex = 71
        Me.CheckBoxMoreDays.Text = "Mes de un día"
        Me.CheckBoxMoreDays.UseVisualStyleBackColor = True
        '
        'Frm_StaffHoliday
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 241)
        Me.Controls.Add(Me.CheckBoxMoreDays)
        Me.Controls.Add(Me.DateTimePickerHourTo)
        Me.Controls.Add(Me.CheckBoxHourTo)
        Me.Controls.Add(Me.DateTimePickerHourFrom)
        Me.Controls.Add(Me.CheckBoxHourFrom)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.TextBoxTitularNom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LabelFchTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_StaffHoliday"
        Me.Text = "Festiu"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxCod As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerFchFrom As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents LabelFchTo As Label
    Friend WithEvents TextBoxTitularNom As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePickerFchTo As DateTimePicker
    Friend WithEvents CheckBoxHourFrom As CheckBox
    Friend WithEvents DateTimePickerHourFrom As DateTimePicker
    Friend WithEvents DateTimePickerHourTo As DateTimePicker
    Friend WithEvents CheckBoxHourTo As CheckBox
    Friend WithEvents CheckBoxMoreDays As CheckBox
End Class
