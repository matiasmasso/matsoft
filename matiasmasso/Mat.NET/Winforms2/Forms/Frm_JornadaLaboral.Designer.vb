<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_JornadaLaboral
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
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerHourFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerHourTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxSortida = New System.Windows.Forms.CheckBox()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(38, 25)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFch.TabIndex = 59
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 77)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(412, 31)
        Me.PanelButtons.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(193, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(304, 4)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Data"
        '
        'DateTimePickerHourFrom
        '
        Me.DateTimePickerHourFrom.CustomFormat = "HH:mm"
        Me.DateTimePickerHourFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerHourFrom.Location = New System.Drawing.Point(183, 25)
        Me.DateTimePickerHourFrom.Name = "DateTimePickerHourFrom"
        Me.DateTimePickerHourFrom.Size = New System.Drawing.Size(65, 20)
        Me.DateTimePickerHourFrom.TabIndex = 60
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(134, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Entrada"
        '
        'DateTimePickerHourTo
        '
        Me.DateTimePickerHourTo.CustomFormat = "HH:mm"
        Me.DateTimePickerHourTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerHourTo.Location = New System.Drawing.Point(331, 25)
        Me.DateTimePickerHourTo.Name = "DateTimePickerHourTo"
        Me.DateTimePickerHourTo.Size = New System.Drawing.Size(65, 20)
        Me.DateTimePickerHourTo.TabIndex = 62
        Me.DateTimePickerHourTo.Visible = False
        '
        'CheckBoxSortida
        '
        Me.CheckBoxSortida.AutoSize = True
        Me.CheckBoxSortida.Location = New System.Drawing.Point(271, 27)
        Me.CheckBoxSortida.Name = "CheckBoxSortida"
        Me.CheckBoxSortida.Size = New System.Drawing.Size(59, 17)
        Me.CheckBoxSortida.TabIndex = 63
        Me.CheckBoxSortida.Text = "Sortida"
        Me.CheckBoxSortida.UseVisualStyleBackColor = True
        '
        'Frm_JornadaLaboral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 108)
        Me.Controls.Add(Me.DateTimePickerHourTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerHourFrom)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckBoxSortida)
        Me.Name = "Frm_JornadaLaboral"
        Me.Text = "Jornada Laboral"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePickerFch As DateTimePicker
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePickerHourFrom As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerHourTo As DateTimePicker
    Friend WithEvents CheckBoxSortida As CheckBox
End Class
