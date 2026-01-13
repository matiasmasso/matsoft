<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepProduct
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonExcluded = New System.Windows.Forms.RadioButton()
        Me.RadioButtonIncluded = New System.Windows.Forms.RadioButton()
        Me.Xl_PercentComRed = New Winforms.Xl_Percent()
        Me.Xl_PercentComStd = New Winforms.Xl_Percent()
        Me.GroupBoxComisions = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_LookupRep1 = New Winforms.Xl_LookupRep()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Xl_LookupDistributionChannel1 = New Winforms.Xl_LookupDistributionChannel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxComisions.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonExcluded)
        Me.GroupBox1.Controls.Add(Me.RadioButtonIncluded)
        Me.GroupBox1.Location = New System.Drawing.Point(94, 120)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(187, 38)
        Me.GroupBox1.TabIndex = 76
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonExcluded
        '
        Me.RadioButtonExcluded.AutoSize = True
        Me.RadioButtonExcluded.Location = New System.Drawing.Point(97, 10)
        Me.RadioButtonExcluded.Name = "RadioButtonExcluded"
        Me.RadioButtonExcluded.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonExcluded.TabIndex = 1
        Me.RadioButtonExcluded.TabStop = True
        Me.RadioButtonExcluded.Text = "Exclos"
        Me.RadioButtonExcluded.UseVisualStyleBackColor = True
        '
        'RadioButtonIncluded
        '
        Me.RadioButtonIncluded.AutoSize = True
        Me.RadioButtonIncluded.Location = New System.Drawing.Point(6, 10)
        Me.RadioButtonIncluded.Name = "RadioButtonIncluded"
        Me.RadioButtonIncluded.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonIncluded.TabIndex = 0
        Me.RadioButtonIncluded.TabStop = True
        Me.RadioButtonIncluded.Text = "Inclos"
        Me.RadioButtonIncluded.UseVisualStyleBackColor = True
        '
        'Xl_PercentComRed
        '
        Me.Xl_PercentComRed.Location = New System.Drawing.Point(74, 47)
        Me.Xl_PercentComRed.Name = "Xl_PercentComRed"
        Me.Xl_PercentComRed.Size = New System.Drawing.Size(55, 20)
        Me.Xl_PercentComRed.TabIndex = 57
        Me.Xl_PercentComRed.Text = "0,00 %"
        Me.Xl_PercentComRed.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_PercentComStd
        '
        Me.Xl_PercentComStd.Location = New System.Drawing.Point(74, 25)
        Me.Xl_PercentComStd.Name = "Xl_PercentComStd"
        Me.Xl_PercentComStd.Size = New System.Drawing.Size(55, 20)
        Me.Xl_PercentComStd.TabIndex = 56
        Me.Xl_PercentComStd.Text = "0,00 %"
        Me.Xl_PercentComStd.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'GroupBoxComisions
        '
        Me.GroupBoxComisions.Controls.Add(Me.Xl_PercentComRed)
        Me.GroupBoxComisions.Controls.Add(Me.Xl_PercentComStd)
        Me.GroupBoxComisions.Controls.Add(Me.Label2)
        Me.GroupBoxComisions.Controls.Add(Me.Label3)
        Me.GroupBoxComisions.Location = New System.Drawing.Point(100, 259)
        Me.GroupBoxComisions.Name = "GroupBoxComisions"
        Me.GroupBoxComisions.Size = New System.Drawing.Size(154, 81)
        Me.GroupBoxComisions.TabIndex = 80
        Me.GroupBoxComisions.TabStop = False
        Me.GroupBoxComisions.Text = "Comisions"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "estandar:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "reduida:"
        '
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerTo.Location = New System.Drawing.Point(153, 212)
        Me.DateTimePickerTo.Name = "DateTimePickerTo"
        Me.DateTimePickerTo.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePickerTo.TabIndex = 79
        Me.DateTimePickerTo.Visible = False
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(153, 182)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(101, 20)
        Me.DateTimePickerFrom.TabIndex = 78
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(100, 213)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(42, 17)
        Me.CheckBoxFchTo.TabIndex = 77
        Me.CheckBoxFchTo.Text = "fins"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 75
        Me.Label6.Text = "Zona:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Producte:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "Representant:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(97, 185)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Des de:"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(229, 4)
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
        Me.ButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOk.Location = New System.Drawing.Point(340, 4)
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
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 362)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(448, 31)
        Me.Panel1.TabIndex = 69
        '
        'Xl_LookupRep1
        '
        Me.Xl_LookupRep1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupRep1.IsDirty = False
        Me.Xl_LookupRep1.Location = New System.Drawing.Point(94, 14)
        Me.Xl_LookupRep1.Name = "Xl_LookupRep1"
        Me.Xl_LookupRep1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRep1.Rep = Nothing
        Me.Xl_LookupRep1.Size = New System.Drawing.Size(343, 20)
        Me.Xl_LookupRep1.TabIndex = 81
        Me.Xl_LookupRep1.Value = Nothing
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(94, 67)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(343, 20)
        Me.Xl_LookupArea1.TabIndex = 82
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(94, 41)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(343, 20)
        Me.Xl_LookupProduct1.TabIndex = 83
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_LookupDistributionChannel1
        '
        Me.Xl_LookupDistributionChannel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupDistributionChannel1.DistributionChannel = Nothing
        Me.Xl_LookupDistributionChannel1.IsDirty = False
        Me.Xl_LookupDistributionChannel1.Location = New System.Drawing.Point(94, 94)
        Me.Xl_LookupDistributionChannel1.Name = "Xl_LookupDistributionChannel1"
        Me.Xl_LookupDistributionChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDistributionChannel1.Size = New System.Drawing.Size(343, 20)
        Me.Xl_LookupDistributionChannel1.TabIndex = 84
        Me.Xl_LookupDistributionChannel1.Value = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 97)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "Canal:"
        '
        'Frm_RepProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 393)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_LookupDistributionChannel1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.Xl_LookupRep1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBoxComisions)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_RepProduct"
        Me.Text = "Zona i producte de representant"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxComisions.ResumeLayout(False)
        Me.GroupBoxComisions.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonExcluded As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonIncluded As System.Windows.Forms.RadioButton
    Friend WithEvents Xl_PercentComRed As Winforms.Xl_Percent
    Friend WithEvents Xl_PercentComStd As Winforms.Xl_Percent
    Friend WithEvents GroupBoxComisions As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxFchTo As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_LookupRep1 As Winforms.Xl_LookupRep
    Friend WithEvents Xl_LookupArea1 As Winforms.Xl_LookupArea
    Friend WithEvents Xl_LookupProduct1 As Winforms.Xl_LookupProduct
    Friend WithEvents Xl_LookupDistributionChannel1 As Xl_LookupDistributionChannel
    Friend WithEvents Label7 As Label
End Class
