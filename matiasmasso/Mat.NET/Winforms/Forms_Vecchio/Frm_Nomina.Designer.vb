<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Nomina
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxStaffNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_AmtEmbargos = New Winforms.Xl_Amount()
        Me.Xl_AmtDeutes = New Winforms.Xl_Amount()
        Me.Xl_AmtSegSocial = New Winforms.Xl_Amount()
        Me.Xl_AmtDietes = New Winforms.Xl_Amount()
        Me.Xl_AmtDevengat = New Winforms.Xl_Amount()
        Me.Xl_AmtBaseIRPF = New Winforms.Xl_Amount()
        Me.Xl_AmtIRPF = New Winforms.Xl_Amount()
        Me.Xl_AmtLiquid = New Winforms.Xl_Amount()
        Me.Xl_NominaItems1 = New Winforms.Xl_NominaItems()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(623, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(95, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 474)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(726, 31)
        Me.Panel1.TabIndex = 34
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(506, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(617, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxStaffNom
        '
        Me.TextBoxStaffNom.Location = New System.Drawing.Point(73, 12)
        Me.TextBoxStaffNom.Name = "TextBoxStaffNom"
        Me.TextBoxStaffNom.ReadOnly = True
        Me.TextBoxStaffNom.Size = New System.Drawing.Size(511, 20)
        Me.TextBoxStaffNom.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "treballador"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(609, 266)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Liquid"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(351, 266)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "IRPF"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(264, 266)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "base IRPF"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 266)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Devengat"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(95, 266)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Dietes"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(179, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Seg.Social"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(437, 266)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Embargaments"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(524, 266)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Deutes"
        '
        'Xl_AmtEmbargos
        '
        Me.Xl_AmtEmbargos.Amt = Nothing
        Me.Xl_AmtEmbargos.Location = New System.Drawing.Point(440, 282)
        Me.Xl_AmtEmbargos.Name = "Xl_AmtEmbargos"
        Me.Xl_AmtEmbargos.ReadOnly = False
        Me.Xl_AmtEmbargos.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtEmbargos.TabIndex = 52
        '
        'Xl_AmtDeutes
        '
        Me.Xl_AmtDeutes.Amt = Nothing
        Me.Xl_AmtDeutes.Location = New System.Drawing.Point(527, 282)
        Me.Xl_AmtDeutes.Name = "Xl_AmtDeutes"
        Me.Xl_AmtDeutes.ReadOnly = False
        Me.Xl_AmtDeutes.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtDeutes.TabIndex = 50
        '
        'Xl_AmtSegSocial
        '
        Me.Xl_AmtSegSocial.Amt = Nothing
        Me.Xl_AmtSegSocial.Location = New System.Drawing.Point(182, 282)
        Me.Xl_AmtSegSocial.Name = "Xl_AmtSegSocial"
        Me.Xl_AmtSegSocial.ReadOnly = False
        Me.Xl_AmtSegSocial.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtSegSocial.TabIndex = 48
        '
        'Xl_AmtDietes
        '
        Me.Xl_AmtDietes.Amt = Nothing
        Me.Xl_AmtDietes.Location = New System.Drawing.Point(98, 282)
        Me.Xl_AmtDietes.Name = "Xl_AmtDietes"
        Me.Xl_AmtDietes.ReadOnly = False
        Me.Xl_AmtDietes.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtDietes.TabIndex = 46
        '
        'Xl_AmtDevengat
        '
        Me.Xl_AmtDevengat.Amt = Nothing
        Me.Xl_AmtDevengat.Location = New System.Drawing.Point(10, 282)
        Me.Xl_AmtDevengat.Name = "Xl_AmtDevengat"
        Me.Xl_AmtDevengat.ReadOnly = False
        Me.Xl_AmtDevengat.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtDevengat.TabIndex = 44
        '
        'Xl_AmtBaseIRPF
        '
        Me.Xl_AmtBaseIRPF.Amt = Nothing
        Me.Xl_AmtBaseIRPF.Location = New System.Drawing.Point(267, 282)
        Me.Xl_AmtBaseIRPF.Name = "Xl_AmtBaseIRPF"
        Me.Xl_AmtBaseIRPF.ReadOnly = False
        Me.Xl_AmtBaseIRPF.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtBaseIRPF.TabIndex = 42
        '
        'Xl_AmtIRPF
        '
        Me.Xl_AmtIRPF.Amt = Nothing
        Me.Xl_AmtIRPF.Location = New System.Drawing.Point(354, 282)
        Me.Xl_AmtIRPF.Name = "Xl_AmtIRPF"
        Me.Xl_AmtIRPF.ReadOnly = False
        Me.Xl_AmtIRPF.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtIRPF.TabIndex = 40
        '
        'Xl_AmtLiquid
        '
        Me.Xl_AmtLiquid.Amt = Nothing
        Me.Xl_AmtLiquid.Location = New System.Drawing.Point(612, 282)
        Me.Xl_AmtLiquid.Name = "Xl_AmtLiquid"
        Me.Xl_AmtLiquid.ReadOnly = False
        Me.Xl_AmtLiquid.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmtLiquid.TabIndex = 37
        '
        'Xl_NominaItems1
        '
        Me.Xl_NominaItems1.Location = New System.Drawing.Point(10, 17)
        Me.Xl_NominaItems1.Name = "Xl_NominaItems1"
        Me.Xl_NominaItems1.Size = New System.Drawing.Size(680, 234)
        Me.Xl_NominaItems1.TabIndex = 54
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(13, 52)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(709, 416)
        Me.TabControl1.TabIndex = 55
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Iban1)
        Me.TabPage1.Controls.Add(Me.Xl_NominaItems1)
        Me.TabPage1.Controls.Add(Me.Xl_AmtLiquid)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_AmtEmbargos)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Xl_AmtIRPF)
        Me.TabPage1.Controls.Add(Me.Xl_AmtDeutes)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_AmtBaseIRPF)
        Me.TabPage1.Controls.Add(Me.Xl_AmtSegSocial)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Xl_AmtDevengat)
        Me.TabPage1.Controls.Add(Me.Xl_AmtDietes)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(701, 390)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Nomina"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Location = New System.Drawing.Point(440, 317)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 55
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(701, 390)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Assentament"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Frm_Nomina
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 505)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxStaffNom)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Frm_Nomina"
        Me.Text = "Frm_Nomina"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxStaffNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtLiquid As Winforms.Xl_Amount
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtIRPF As Winforms.Xl_Amount
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtBaseIRPF As Winforms.Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDevengat As Winforms.Xl_Amount
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDietes As Winforms.Xl_Amount
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtSegSocial As Winforms.Xl_Amount
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtEmbargos As Winforms.Xl_Amount
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtDeutes As Winforms.Xl_Amount
    Friend WithEvents Xl_NominaItems1 As Winforms.Xl_NominaItems
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Iban1 As Xl_Iban
End Class
