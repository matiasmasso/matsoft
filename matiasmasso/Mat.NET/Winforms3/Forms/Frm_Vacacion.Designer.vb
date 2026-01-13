<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Vacacion
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxFromDay = New System.Windows.Forms.ComboBox()
        Me.ComboBoxToDay = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFromMonth = New System.Windows.Forms.ComboBox()
        Me.ComboBoxToMonth = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadioButton30Days = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSpecificDay = New System.Windows.Forms.RadioButton()
        Me.GroupBoxResult = New System.Windows.Forms.GroupBox()
        Me.ComboBoxResultMonth = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxResultDay = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 212)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(337, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(118, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(229, 4)
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
        Me.Label1.Location = New System.Drawing.Point(13, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Des del dia"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Fins el dia"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(155, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "del mes"
        '
        'ComboBoxFromDay
        '
        Me.ComboBoxFromDay.FormattingEnabled = True
        Me.ComboBoxFromDay.Location = New System.Drawing.Point(83, 21)
        Me.ComboBoxFromDay.Name = "ComboBoxFromDay"
        Me.ComboBoxFromDay.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxFromDay.TabIndex = 60
        '
        'ComboBoxToDay
        '
        Me.ComboBoxToDay.FormattingEnabled = True
        Me.ComboBoxToDay.Location = New System.Drawing.Point(83, 48)
        Me.ComboBoxToDay.Name = "ComboBoxToDay"
        Me.ComboBoxToDay.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxToDay.TabIndex = 61
        '
        'ComboBoxFromMonth
        '
        Me.ComboBoxFromMonth.FormattingEnabled = True
        Me.ComboBoxFromMonth.Location = New System.Drawing.Point(204, 20)
        Me.ComboBoxFromMonth.Name = "ComboBoxFromMonth"
        Me.ComboBoxFromMonth.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxFromMonth.TabIndex = 62
        '
        'ComboBoxToMonth
        '
        Me.ComboBoxToMonth.FormattingEnabled = True
        Me.ComboBoxToMonth.Location = New System.Drawing.Point(204, 48)
        Me.ComboBoxToMonth.Name = "ComboBoxToMonth"
        Me.ComboBoxToMonth.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxToMonth.TabIndex = 64
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(155, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "del mes"
        '
        'RadioButton30Days
        '
        Me.RadioButton30Days.AutoSize = True
        Me.RadioButton30Days.Checked = True
        Me.RadioButton30Days.Location = New System.Drawing.Point(16, 92)
        Me.RadioButton30Days.Name = "RadioButton30Days"
        Me.RadioButton30Days.Size = New System.Drawing.Size(97, 17)
        Me.RadioButton30Days.TabIndex = 65
        Me.RadioButton30Days.TabStop = True
        Me.RadioButton30Days.Text = "aplaçar 30 dies"
        Me.RadioButton30Days.UseVisualStyleBackColor = True
        '
        'RadioButtonSpecificDay
        '
        Me.RadioButtonSpecificDay.AutoSize = True
        Me.RadioButtonSpecificDay.Location = New System.Drawing.Point(16, 115)
        Me.RadioButtonSpecificDay.Name = "RadioButtonSpecificDay"
        Me.RadioButtonSpecificDay.Size = New System.Drawing.Size(142, 17)
        Me.RadioButtonSpecificDay.TabIndex = 66
        Me.RadioButtonSpecificDay.Text = "aplaçar a un día concret"
        Me.RadioButtonSpecificDay.UseVisualStyleBackColor = True
        '
        'GroupBoxResult
        '
        Me.GroupBoxResult.Controls.Add(Me.ComboBoxResultMonth)
        Me.GroupBoxResult.Controls.Add(Me.Label5)
        Me.GroupBoxResult.Controls.Add(Me.ComboBoxResultDay)
        Me.GroupBoxResult.Controls.Add(Me.Label6)
        Me.GroupBoxResult.Location = New System.Drawing.Point(28, 115)
        Me.GroupBoxResult.Name = "GroupBoxResult"
        Me.GroupBoxResult.Size = New System.Drawing.Size(306, 73)
        Me.GroupBoxResult.TabIndex = 67
        Me.GroupBoxResult.TabStop = False
        Me.GroupBoxResult.Visible = False
        '
        'ComboBoxResultMonth
        '
        Me.ComboBoxResultMonth.FormattingEnabled = True
        Me.ComboBoxResultMonth.Location = New System.Drawing.Point(228, 40)
        Me.ComboBoxResultMonth.Name = "ComboBoxResultMonth"
        Me.ComboBoxResultMonth.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxResultMonth.TabIndex = 68
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(179, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "del mes"
        '
        'ComboBoxResultDay
        '
        Me.ComboBoxResultDay.FormattingEnabled = True
        Me.ComboBoxResultDay.Location = New System.Drawing.Point(107, 40)
        Me.ComboBoxResultDay.Name = "ComboBoxResultDay"
        Me.ComboBoxResultDay.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxResultDay.TabIndex = 66
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "aplaçar al dia"
        '
        'Frm_Vacacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 243)
        Me.Controls.Add(Me.RadioButtonSpecificDay)
        Me.Controls.Add(Me.RadioButton30Days)
        Me.Controls.Add(Me.ComboBoxToMonth)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxFromMonth)
        Me.Controls.Add(Me.ComboBoxToDay)
        Me.Controls.Add(Me.ComboBoxFromDay)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBoxResult)
        Me.Name = "Frm_Vacacion"
        Me.Text = "Aplaçament per vacances"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxResult.ResumeLayout(False)
        Me.GroupBoxResult.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxFromDay As ComboBox
    Friend WithEvents ComboBoxToDay As ComboBox
    Friend WithEvents ComboBoxFromMonth As ComboBox
    Friend WithEvents ComboBoxToMonth As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents RadioButton30Days As RadioButton
    Friend WithEvents RadioButtonSpecificDay As RadioButton
    Friend WithEvents GroupBoxResult As GroupBox
    Friend WithEvents ComboBoxResultMonth As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBoxResultDay As ComboBox
    Friend WithEvents Label6 As Label
End Class
