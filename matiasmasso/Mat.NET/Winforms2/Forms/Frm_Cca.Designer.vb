<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cca
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
        Me.LabelUsr = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonCur = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxDeb = New System.Windows.Forms.TextBox()
        Me.TextBoxHab = New System.Windows.Forms.TextBox()
        Me.TextBoxDif = New System.Windows.Forms.TextBox()
        Me.ComboBoxCcd = New System.Windows.Forms.ComboBox()
        Me.Xl_DocFile1 = New Xl_DocFile()
        Me.Xl_LookupProjecte1 = New Xl_LookupProjecte()
        Me.CheckBoxProjecte = New System.Windows.Forms.CheckBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelUsr
        '
        Me.LabelUsr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelUsr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelUsr.Location = New System.Drawing.Point(6, 422)
        Me.LabelUsr.Name = "LabelUsr"
        Me.LabelUsr.Size = New System.Drawing.Size(1001, 16)
        Me.LabelUsr.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Concepte:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(564, 2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(65, 28)
        Me.TextBoxConcepte.MaxLength = 60
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(437, 20)
        Me.TextBoxConcepte.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 51)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(646, 348)
        Me.DataGridView1.TabIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExport, Me.ToolStripButtonCur})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1011, 25)
        Me.ToolStrip1.TabIndex = 31
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExport
        '
        Me.ToolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExport.Image = Global.Mat.Net.My.Resources.Resources.Excel
        Me.ToolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExport.Name = "ToolStripButtonExport"
        Me.ToolStripButtonExport.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExport.Text = "ToolStripButton1"
        Me.ToolStripButtonExport.ToolTipText = "exportar partides a format Excel"
        '
        'ToolStripButtonCur
        '
        Me.ToolStripButtonCur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonCur.Image = Global.Mat.Net.My.Resources.Resources.DollarBlue
        Me.ToolStripButtonCur.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCur.Name = "ToolStripButtonCur"
        Me.ToolStripButtonCur.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonCur.Text = "moneda estrangera"
        Me.ToolStripButtonCur.ToolTipText = "moneda estrangera"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 437)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1011, 31)
        Me.Panel1.TabIndex = 33
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(791, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(902, 4)
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
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(12, 404)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "total debe:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(185, 404)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "total haber:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.Location = New System.Drawing.Point(483, 404)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 16)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "diferencia:"
        '
        'TextBoxDeb
        '
        Me.TextBoxDeb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDeb.Location = New System.Drawing.Point(6, 401)
        Me.TextBoxDeb.Name = "TextBoxDeb"
        Me.TextBoxDeb.ReadOnly = True
        Me.TextBoxDeb.Size = New System.Drawing.Size(171, 20)
        Me.TextBoxDeb.TabIndex = 40
        Me.TextBoxDeb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxHab
        '
        Me.TextBoxHab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxHab.Location = New System.Drawing.Point(183, 401)
        Me.TextBoxHab.Name = "TextBoxHab"
        Me.TextBoxHab.ReadOnly = True
        Me.TextBoxHab.Size = New System.Drawing.Size(171, 20)
        Me.TextBoxHab.TabIndex = 41
        Me.TextBoxHab.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxDif
        '
        Me.TextBoxDif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDif.Location = New System.Drawing.Point(479, 401)
        Me.TextBoxDif.Name = "TextBoxDif"
        Me.TextBoxDif.ReadOnly = True
        Me.TextBoxDif.Size = New System.Drawing.Size(171, 20)
        Me.TextBoxDif.TabIndex = 42
        Me.TextBoxDif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ComboBoxCcd
        '
        Me.ComboBoxCcd.FormattingEnabled = True
        Me.ComboBoxCcd.Location = New System.Drawing.Point(510, 28)
        Me.ComboBoxCcd.Name = "ComboBoxCcd"
        Me.ComboBoxCcd.Size = New System.Drawing.Size(142, 21)
        Me.ComboBoxCcd.TabIndex = 44
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(657, 2)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 45
        '
        'Xl_LookupProjecte1
        '
        Me.Xl_LookupProjecte1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProjecte1.Projecte = Nothing
        Me.Xl_LookupProjecte1.IsDirty = False
        Me.Xl_LookupProjecte1.Location = New System.Drawing.Point(302, 2)
        Me.Xl_LookupProjecte1.Name = "Xl_LookupProjecte1"
        Me.Xl_LookupProjecte1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProjecte1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupProjecte1.TabIndex = 46
        Me.Xl_LookupProjecte1.Value = Nothing
        Me.Xl_LookupProjecte1.Visible = False
        '
        'CheckBoxProjecte
        '
        Me.CheckBoxProjecte.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxProjecte.AutoSize = True
        Me.CheckBoxProjecte.Location = New System.Drawing.Point(232, 5)
        Me.CheckBoxProjecte.Name = "CheckBoxProjecte"
        Me.CheckBoxProjecte.Size = New System.Drawing.Size(64, 17)
        Me.CheckBoxProjecte.TabIndex = 47
        Me.CheckBoxProjecte.Text = "projecte"
        Me.CheckBoxProjecte.UseVisualStyleBackColor = True
        '
        'Frm_Cca
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 468)
        Me.Controls.Add(Me.CheckBoxProjecte)
        Me.Controls.Add(Me.Xl_LookupProjecte1)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.ComboBoxCcd)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxDeb)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelUsr)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TextBoxHab)
        Me.Controls.Add(Me.TextBoxDif)
        Me.Name = "Frm_Cca"
        Me.Text = "ASSENTAMENT"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelUsr As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDeb As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxHab As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDif As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripButtonCur As System.Windows.Forms.ToolStripButton
    Friend WithEvents ComboBoxCcd As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
    Friend WithEvents Xl_LookupProjecte1 As Xl_LookupProjecte
    Friend WithEvents CheckBoxProjecte As CheckBox
End Class
