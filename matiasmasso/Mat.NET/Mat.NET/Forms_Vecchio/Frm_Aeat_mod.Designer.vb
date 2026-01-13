<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Aeat_mod
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxMod = New System.Windows.Forms.TextBox()
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxTperiod = New System.Windows.Forms.ComboBox()
        Me.CheckBoxSoloInfo = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxGranEmpresa = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPymes = New System.Windows.Forms.CheckBox()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 263)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(428, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(209, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(320, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Model:"
        '
        'TextBoxMod
        '
        Me.TextBoxMod.Location = New System.Drawing.Point(83, 36)
        Me.TextBoxMod.Name = "TextBoxMod"
        Me.TextBoxMod.Size = New System.Drawing.Size(104, 20)
        Me.TextBoxMod.TabIndex = 43
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(83, 111)
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(333, 20)
        Me.TextBoxDsc.TabIndex = 45
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Descripció:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Periodicitat:"
        '
        'ComboBoxTperiod
        '
        Me.ComboBoxTperiod.FormattingEnabled = True
        Me.ComboBoxTperiod.Items.AddRange(New Object() {"(no especificat)", "Altres", "Mensual", "Trimestral", "Anual"})
        Me.ComboBoxTperiod.Location = New System.Drawing.Point(83, 84)
        Me.ComboBoxTperiod.Name = "ComboBoxTperiod"
        Me.ComboBoxTperiod.Size = New System.Drawing.Size(104, 21)
        Me.ComboBoxTperiod.TabIndex = 47
        '
        'CheckBoxSoloInfo
        '
        Me.CheckBoxSoloInfo.AutoSize = True
        Me.CheckBoxSoloInfo.Location = New System.Drawing.Point(83, 138)
        Me.CheckBoxSoloInfo.Name = "CheckBoxSoloInfo"
        Me.CheckBoxSoloInfo.Size = New System.Drawing.Size(239, 17)
        Me.CheckBoxSoloInfo.TabIndex = 48
        Me.CheckBoxSoloInfo.Text = "Declaració informativa sense valor ecomòmic"
        Me.CheckBoxSoloInfo.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxGranEmpresa)
        Me.GroupBox1.Controls.Add(Me.CheckBoxPymes)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 173)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 72)
        Me.GroupBox1.TabIndex = 51
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "aplicable a:"
        '
        'CheckBoxGranEmpresa
        '
        Me.CheckBoxGranEmpresa.AutoSize = True
        Me.CheckBoxGranEmpresa.Location = New System.Drawing.Point(70, 42)
        Me.CheckBoxGranEmpresa.Name = "CheckBoxGranEmpresa"
        Me.CheckBoxGranEmpresa.Size = New System.Drawing.Size(102, 17)
        Me.CheckBoxGranEmpresa.TabIndex = 52
        Me.CheckBoxGranEmpresa.Text = "Grans empreses"
        Me.CheckBoxGranEmpresa.UseVisualStyleBackColor = True
        '
        'CheckBoxPymes
        '
        Me.CheckBoxPymes.AutoSize = True
        Me.CheckBoxPymes.Location = New System.Drawing.Point(70, 19)
        Me.CheckBoxPymes.Name = "CheckBoxPymes"
        Me.CheckBoxPymes.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxPymes.TabIndex = 51
        Me.CheckBoxPymes.Text = "PYMES"
        Me.CheckBoxPymes.UseVisualStyleBackColor = True
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(3, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 13
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Frm_Aeat_mod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 294)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBoxSoloInfo)
        Me.Controls.Add(Me.ComboBoxTperiod)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxMod)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Aeat_mod"
        Me.Text = "MODEL DECLARACIO HISENDA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMod As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTperiod As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxSoloInfo As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxGranEmpresa As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPymes As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
End Class
