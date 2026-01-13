<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PgcClass
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.NumericUpDownOrd = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDownLevel = New System.Windows.Forms.NumericUpDown()
        Me.LabelPlan = New System.Windows.Forms.Label()
        Me.LabelParent = New System.Windows.Forms.Label()
        Me.CheckBoxHideFigures = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_LookupPgcClassParent = New Winforms.Xl_LookupPgcClass()
        Me.Xl_LookupPgcPlan1 = New Winforms.Xl_LookupPgcPlan()
        Me.Xl_PgcClassesSumandos = New Winforms.Xl_PgcClasses()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_PgcClassesSumandos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEng.Location = New System.Drawing.Point(105, 124)
        Me.TextBoxEng.MaxLength = 60
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxEng.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 127)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Anglés"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCat.Location = New System.Drawing.Point(105, 104)
        Me.TextBoxCat.MaxLength = 60
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxCat.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Catalá"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(105, 83)
        Me.TextBoxEsp.MaxLength = 60
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxEsp.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Espanyol"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 322)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(478, 31)
        Me.Panel1.TabIndex = 67
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(259, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(370, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 10
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.TabIndex = 12
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'NumericUpDownOrd
        '
        Me.NumericUpDownOrd.Location = New System.Drawing.Point(105, 148)
        Me.NumericUpDownOrd.Name = "NumericUpDownOrd"
        Me.NumericUpDownOrd.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownOrd.TabIndex = 6
        Me.NumericUpDownOrd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 150)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Ordre"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Nivell"
        '
        'NumericUpDownLevel
        '
        Me.NumericUpDownLevel.Location = New System.Drawing.Point(105, 170)
        Me.NumericUpDownLevel.Name = "NumericUpDownLevel"
        Me.NumericUpDownLevel.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownLevel.TabIndex = 7
        Me.NumericUpDownLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelPlan
        '
        Me.LabelPlan.AutoSize = True
        Me.LabelPlan.Location = New System.Drawing.Point(14, 17)
        Me.LabelPlan.Name = "LabelPlan"
        Me.LabelPlan.Size = New System.Drawing.Size(22, 13)
        Me.LabelPlan.TabIndex = 73
        Me.LabelPlan.Text = "Pla"
        Me.LabelPlan.Visible = False
        '
        'LabelParent
        '
        Me.LabelParent.AutoSize = True
        Me.LabelParent.Location = New System.Drawing.Point(14, 40)
        Me.LabelParent.Name = "LabelParent"
        Me.LabelParent.Size = New System.Drawing.Size(78, 13)
        Me.LabelParent.TabIndex = 75
        Me.LabelParent.Text = "Descendeix de"
        Me.LabelParent.Visible = False
        '
        'CheckBoxHideFigures
        '
        Me.CheckBoxHideFigures.AutoSize = True
        Me.CheckBoxHideFigures.Location = New System.Drawing.Point(105, 194)
        Me.CheckBoxHideFigures.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.CheckBoxHideFigures.Name = "CheckBoxHideFigures"
        Me.CheckBoxHideFigures.Size = New System.Drawing.Size(100, 17)
        Me.CheckBoxHideFigures.TabIndex = 8
        Me.CheckBoxHideFigures.Text = "Oculta les xifres"
        Me.CheckBoxHideFigures.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 215)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 78
        Me.Label8.Text = "Suma de classes"
        '
        'Xl_LookupPgcClassParent
        '
        Me.Xl_LookupPgcClassParent.IsDirty = False
        Me.Xl_LookupPgcClassParent.Location = New System.Drawing.Point(105, 37)
        Me.Xl_LookupPgcClassParent.Name = "Xl_LookupPgcClassParent"
        Me.Xl_LookupPgcClassParent.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcClassParent.PgcClass = Nothing
        Me.Xl_LookupPgcClassParent.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupPgcClassParent.TabIndex = 1
        Me.Xl_LookupPgcClassParent.Value = Nothing
        Me.Xl_LookupPgcClassParent.Visible = False
        '
        'Xl_LookupPgcPlan1
        '
        Me.Xl_LookupPgcPlan1.IsDirty = False
        Me.Xl_LookupPgcPlan1.Location = New System.Drawing.Point(105, 12)
        Me.Xl_LookupPgcPlan1.Name = "Xl_LookupPgcPlan1"
        Me.Xl_LookupPgcPlan1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPgcPlan1.PgcPlan = Nothing
        Me.Xl_LookupPgcPlan1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupPgcPlan1.TabIndex = 0
        Me.Xl_LookupPgcPlan1.Value = Nothing
        Me.Xl_LookupPgcPlan1.Visible = False
        '
        'Xl_PgcClassesSumandos
        '
        Me.Xl_PgcClassesSumandos.AllowUserToAddRows = False
        Me.Xl_PgcClassesSumandos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PgcClassesSumandos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcClassesSumandos.Location = New System.Drawing.Point(105, 215)
        Me.Xl_PgcClassesSumandos.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_PgcClassesSumandos.Name = "Xl_PgcClassesSumandos"
        Me.Xl_PgcClassesSumandos.ReadOnly = True
        Me.Xl_PgcClassesSumandos.RowTemplate.Height = 40
        Me.Xl_PgcClassesSumandos.Size = New System.Drawing.Size(373, 105)
        Me.Xl_PgcClassesSumandos.TabIndex = 9
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Location = New System.Drawing.Point(105, 60)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(200, 21)
        Me.ComboBoxCod.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "Codi"
        Me.Label4.Visible = False
        '
        'Frm_PgcClass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 353)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.Xl_PgcClassesSumandos)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CheckBoxHideFigures)
        Me.Controls.Add(Me.LabelParent)
        Me.Controls.Add(Me.Xl_LookupPgcClassParent)
        Me.Controls.Add(Me.LabelPlan)
        Me.Controls.Add(Me.Xl_LookupPgcPlan1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDownLevel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownOrd)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxEng)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCat)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxEsp)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Frm_PgcClass"
        Me.Text = "Classe Comptes"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_PgcClassesSumandos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents NumericUpDownOrd As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDownLevel As NumericUpDown
    Friend WithEvents Xl_LookupPgcPlan1 As Xl_LookupPgcPlan
    Friend WithEvents LabelPlan As Label
    Friend WithEvents Xl_LookupPgcClassParent As Xl_LookupPgcClass
    Friend WithEvents LabelParent As Label
    Friend WithEvents CheckBoxHideFigures As CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Xl_PgcClassesSumandos As Xl_PgcClasses
    Friend WithEvents ComboBoxCod As ComboBox
    Friend WithEvents Label4 As Label
End Class
