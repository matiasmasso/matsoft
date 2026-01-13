<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepTraspas
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxOldRep = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_LookupRep1 = New Winforms.Xl_LookupRep()
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxComRed = New System.Windows.Forms.TextBox()
        Me.TextBoxComStd = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_RepProductsxRep1 = New Winforms.Xl_RepProducts()
        Me.GroupBoxTraspas = New System.Windows.Forms.GroupBox()
        Me.CheckBoxTraspas = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBoxTraspas.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Antic rep"
        '
        'TextBoxOldRep
        '
        Me.TextBoxOldRep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOldRep.Location = New System.Drawing.Point(86, 23)
        Me.TextBoxOldRep.Name = "TextBoxOldRep"
        Me.TextBoxOldRep.ReadOnly = True
        Me.TextBoxOldRep.Size = New System.Drawing.Size(559, 20)
        Me.TextBoxOldRep.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "nou rep"
        '
        'Xl_LookupRep1
        '
        Me.Xl_LookupRep1.IsDirty = False
        Me.Xl_LookupRep1.Location = New System.Drawing.Point(82, 14)
        Me.Xl_LookupRep1.Name = "Xl_LookupRep1"
        Me.Xl_LookupRep1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRep1.Rep = Nothing
        Me.Xl_LookupRep1.Size = New System.Drawing.Size(191, 24)
        Me.Xl_LookupRep1.TabIndex = 3
        Me.Xl_LookupRep1.Value = Nothing
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(69, 340)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(94, 20)
        Me.DateTimePickerBaixa.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 344)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "baixa"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "alta"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(82, 35)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(94, 20)
        Me.DateTimePickerAlta.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxComRed)
        Me.GroupBox1.Controls.Add(Me.TextBoxComStd)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(220, 73)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Comisió"
        '
        'TextBoxComRed
        '
        Me.TextBoxComRed.Location = New System.Drawing.Point(128, 40)
        Me.TextBoxComRed.Name = "TextBoxComRed"
        Me.TextBoxComRed.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxComRed.TabIndex = 3
        '
        'TextBoxComStd
        '
        Me.TextBoxComStd.Location = New System.Drawing.Point(128, 16)
        Me.TextBoxComStd.Name = "TextBoxComStd"
        Me.TextBoxComStd.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxComStd.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(76, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "reduida"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(76, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "standard"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 512)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(649, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(430, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(541, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_RepProductsxRep1
        '
        Me.Xl_RepProductsxRep1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RepProductsxRep1.Location = New System.Drawing.Point(12, 49)
        Me.Xl_RepProductsxRep1.Name = "Xl_RepProductsxRep1"
        Me.Xl_RepProductsxRep1.Size = New System.Drawing.Size(633, 273)
        Me.Xl_RepProductsxRep1.TabIndex = 42
        '
        'GroupBoxTraspas
        '
        Me.GroupBoxTraspas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxTraspas.Controls.Add(Me.Xl_LookupRep1)
        Me.GroupBoxTraspas.Controls.Add(Me.Label2)
        Me.GroupBoxTraspas.Controls.Add(Me.GroupBox1)
        Me.GroupBoxTraspas.Controls.Add(Me.Label4)
        Me.GroupBoxTraspas.Controls.Add(Me.DateTimePickerAlta)
        Me.GroupBoxTraspas.Enabled = False
        Me.GroupBoxTraspas.Location = New System.Drawing.Point(315, 346)
        Me.GroupBoxTraspas.Name = "GroupBoxTraspas"
        Me.GroupBoxTraspas.Size = New System.Drawing.Size(322, 147)
        Me.GroupBoxTraspas.TabIndex = 43
        Me.GroupBoxTraspas.TabStop = False
        '
        'CheckBoxTraspas
        '
        Me.CheckBoxTraspas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxTraspas.AutoSize = True
        Me.CheckBoxTraspas.Location = New System.Drawing.Point(315, 340)
        Me.CheckBoxTraspas.Name = "CheckBoxTraspas"
        Me.CheckBoxTraspas.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxTraspas.TabIndex = 44
        Me.CheckBoxTraspas.Text = "traspassar"
        Me.CheckBoxTraspas.UseVisualStyleBackColor = True
        '
        'Frm_RepTraspas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 543)
        Me.Controls.Add(Me.CheckBoxTraspas)
        Me.Controls.Add(Me.GroupBoxTraspas)
        Me.Controls.Add(Me.Xl_RepProductsxRep1)
        Me.Controls.Add(Me.DateTimePickerBaixa)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxOldRep)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_RepTraspas"
        Me.Text = "Traspas de Representant"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxTraspas.ResumeLayout(False)
        Me.GroupBoxTraspas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxOldRep As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupRep1 As Xl_LookupRep
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxComRed As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxComStd As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Xl_RepProductsxRep1 As Winforms.Xl_RepProducts
    Friend WithEvents GroupBoxTraspas As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxTraspas As System.Windows.Forms.CheckBox
End Class
