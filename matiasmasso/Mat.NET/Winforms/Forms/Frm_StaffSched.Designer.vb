<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StaffSched
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
        Me.Xl_StaffSched1 = New Winforms.Xl_StaffSchedItems()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_StaffSched2 = New Winforms.Xl_StaffSchedItems()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBoxObsolet = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_StaffSched1
        '
        Me.Xl_StaffSched1.Location = New System.Drawing.Point(6, 18)
        Me.Xl_StaffSched1.Name = "Xl_StaffSched1"
        Me.Xl_StaffSched1.Size = New System.Drawing.Size(437, 272)
        Me.Xl_StaffSched1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 367)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(462, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(243, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(354, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 76)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(456, 290)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_StaffSched1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(448, 264)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Ordinari"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_StaffSched2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(448, 280)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Intensiu"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_StaffSched2
        '
        Me.Xl_StaffSched2.Location = New System.Drawing.Point(6, 19)
        Me.Xl_StaffSched2.Name = "Xl_StaffSched2"
        Me.Xl_StaffSched2.Size = New System.Drawing.Size(437, 272)
        Me.Xl_StaffSched2.TabIndex = 1
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(354, 8)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchFrom.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(273, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Vigent des de:"
        '
        'CheckBoxObsolet
        '
        Me.CheckBoxObsolet.AutoSize = True
        Me.CheckBoxObsolet.Location = New System.Drawing.Point(276, 37)
        Me.CheckBoxObsolet.Name = "CheckBoxObsolet"
        Me.CheckBoxObsolet.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxObsolet.TabIndex = 46
        Me.CheckBoxObsolet.Text = "Obsolet"
        Me.CheckBoxObsolet.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(354, 34)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchTo.TabIndex = 47
        Me.DateTimePickerFchTo.Visible = False
        '
        'Frm_StaffSched
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 398)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxObsolet)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_StaffSched"
        Me.Text = "Horaris jornada"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_StaffSched1 As Xl_StaffSchedItems
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_StaffSched2 As Xl_StaffSchedItems
    Friend WithEvents DateTimePickerFchFrom As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBoxObsolet As CheckBox
    Friend WithEvents DateTimePickerFchTo As DateTimePicker
End Class
