<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Wz_Girs
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCsbs = New System.Windows.Forms.TabPage()
        Me.CheckBoxNoDomiciliats = New System.Windows.Forms.CheckBox()
        Me.Xl_Gir_SelEfts1 = New Mat.Net.Xl_Gir_SelEfts()
        Me.TabPageCsas = New System.Windows.Forms.TabPage()
        Me.Xl_Gir_SelBancs1 = New Mat.Net.Xl_Gir_SelBancs()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonSepaB2B = New System.Windows.Forms.RadioButton()
        Me.RadioButtonNorma58 = New System.Windows.Forms.RadioButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCsbs.SuspendLayout()
        Me.TabPageCsas.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageCsbs)
        Me.TabControl1.Controls.Add(Me.TabPageCsas)
        Me.TabControl1.Location = New System.Drawing.Point(0, 33)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(820, 337)
        Me.TabControl1.TabIndex = 1
        '
        'TabPageCsbs
        '
        Me.TabPageCsbs.Controls.Add(Me.CheckBoxNoDomiciliats)
        Me.TabPageCsbs.Controls.Add(Me.Xl_Gir_SelEfts1)
        Me.TabPageCsbs.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCsbs.Name = "TabPageCsbs"
        Me.TabPageCsbs.Size = New System.Drawing.Size(812, 311)
        Me.TabPageCsbs.TabIndex = 1
        Me.TabPageCsbs.Text = "EFECTES"
        '
        'CheckBoxNoDomiciliats
        '
        Me.CheckBoxNoDomiciliats.AutoSize = True
        Me.CheckBoxNoDomiciliats.Location = New System.Drawing.Point(447, 4)
        Me.CheckBoxNoDomiciliats.Name = "CheckBoxNoDomiciliats"
        Me.CheckBoxNoDomiciliats.Size = New System.Drawing.Size(167, 17)
        Me.CheckBoxNoDomiciliats.TabIndex = 2
        Me.CheckBoxNoDomiciliats.Text = "Només efectes No Domiciliats"
        Me.CheckBoxNoDomiciliats.UseVisualStyleBackColor = True
        '
        'Xl_Gir_SelEfts1
        '
        Me.Xl_Gir_SelEfts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelEfts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelEfts1.Name = "Xl_Gir_SelEfts1"
        Me.Xl_Gir_SelEfts1.Size = New System.Drawing.Size(812, 311)
        Me.Xl_Gir_SelEfts1.TabIndex = 0
        '
        'TabPageCsas
        '
        Me.TabPageCsas.Controls.Add(Me.Xl_Gir_SelBancs1)
        Me.TabPageCsas.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCsas.Name = "TabPageCsas"
        Me.TabPageCsas.Size = New System.Drawing.Size(812, 311)
        Me.TabPageCsas.TabIndex = 2
        Me.TabPageCsas.Text = "REMESES"
        '
        'Xl_Gir_SelBancs1
        '
        Me.Xl_Gir_SelBancs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Gir_SelBancs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Gir_SelBancs1.Name = "Xl_Gir_SelBancs1"
        Me.Xl_Gir_SelBancs1.Size = New System.Drawing.Size(812, 311)
        Me.Xl_Gir_SelBancs1.TabIndex = 0
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(724, 375)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 12
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Location = New System.Drawing.Point(620, 375)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 11
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(0, 375)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 10
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonSepaB2B)
        Me.GroupBox1.Controls.Add(Me.RadioButtonNorma58)
        Me.GroupBox1.Location = New System.Drawing.Point(620, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 30)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonSepaB2B
        '
        Me.RadioButtonSepaB2B.AutoSize = True
        Me.RadioButtonSepaB2B.Location = New System.Drawing.Point(112, 9)
        Me.RadioButtonSepaB2B.Name = "RadioButtonSepaB2B"
        Me.RadioButtonSepaB2B.Size = New System.Drawing.Size(76, 17)
        Me.RadioButtonSepaB2B.TabIndex = 1
        Me.RadioButtonSepaB2B.Text = "SEPA B2B"
        Me.RadioButtonSepaB2B.UseVisualStyleBackColor = True
        '
        'RadioButtonNorma58
        '
        Me.RadioButtonNorma58.AutoSize = True
        Me.RadioButtonNorma58.Checked = True
        Me.RadioButtonNorma58.Location = New System.Drawing.Point(6, 9)
        Me.RadioButtonNorma58.Name = "RadioButtonNorma58"
        Me.RadioButtonNorma58.Size = New System.Drawing.Size(71, 17)
        Me.RadioButtonNorma58.TabIndex = 0
        Me.RadioButtonNorma58.TabStop = True
        Me.RadioButtonNorma58.Text = "Norma 58"
        Me.RadioButtonNorma58.UseVisualStyleBackColor = True
        '
        'Wz_Girs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 401)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Wz_Girs"
        Me.Text = "DESCOMPTE DE EFECTES"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCsbs.ResumeLayout(False)
        Me.TabPageCsbs.PerformLayout()
        Me.TabPageCsas.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCsbs As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCsas As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Gir_SelEfts1 As Xl_Gir_SelEfts
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents Xl_Gir_SelBancs1 As Xl_Gir_SelBancs
    Friend WithEvents CheckBoxNoDomiciliats As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonSepaB2B As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNorma58 As System.Windows.Forms.RadioButton
End Class
