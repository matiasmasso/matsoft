<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliDtos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox
        Me.TextBoxDtoGlobal = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.RadioButtonDtoGlobal = New System.Windows.Forms.RadioButton
        Me.RadioButtonDtoTpa = New System.Windows.Forms.RadioButton
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 346)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(299, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(3, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 13
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(191, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCliNom.Location = New System.Drawing.Point(0, 1)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxCliNom.TabIndex = 44
        '
        'TextBoxDtoGlobal
        '
        Me.TextBoxDtoGlobal.Location = New System.Drawing.Point(241, 27)
        Me.TextBoxDtoGlobal.Name = "TextBoxDtoGlobal"
        Me.TextBoxDtoGlobal.Size = New System.Drawing.Size(49, 20)
        Me.TextBoxDtoGlobal.TabIndex = 46
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 83)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(277, 257)
        Me.DataGridView1.TabIndex = 49
        '
        'RadioButtonDtoGlobal
        '
        Me.RadioButtonDtoGlobal.AutoSize = True
        Me.RadioButtonDtoGlobal.Location = New System.Drawing.Point(13, 27)
        Me.RadioButtonDtoGlobal.Name = "RadioButtonDtoGlobal"
        Me.RadioButtonDtoGlobal.Size = New System.Drawing.Size(211, 17)
        Me.RadioButtonDtoGlobal.TabIndex = 50
        Me.RadioButtonDtoGlobal.TabStop = True
        Me.RadioButtonDtoGlobal.Text = "descompte global, compri el que compri"
        Me.RadioButtonDtoGlobal.UseVisualStyleBackColor = True
        '
        'RadioButtonDtoTpa
        '
        Me.RadioButtonDtoTpa.AutoSize = True
        Me.RadioButtonDtoTpa.Location = New System.Drawing.Point(13, 50)
        Me.RadioButtonDtoTpa.Name = "RadioButtonDtoTpa"
        Me.RadioButtonDtoTpa.Size = New System.Drawing.Size(194, 17)
        Me.RadioButtonDtoTpa.TabIndex = 51
        Me.RadioButtonDtoTpa.TabStop = True
        Me.RadioButtonDtoTpa.Text = "descompte segons marca comercial"
        Me.RadioButtonDtoTpa.UseVisualStyleBackColor = True
        '
        'Frm_CliDtos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(299, 377)
        Me.Controls.Add(Me.RadioButtonDtoTpa)
        Me.Controls.Add(Me.RadioButtonDtoGlobal)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxDtoGlobal)
        Me.Controls.Add(Me.TextBoxCliNom)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_CliDtos"
        Me.Text = "DESCOMPTES FIXES"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxDtoGlobal As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButtonDtoGlobal As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDtoTpa As System.Windows.Forms.RadioButton
End Class
