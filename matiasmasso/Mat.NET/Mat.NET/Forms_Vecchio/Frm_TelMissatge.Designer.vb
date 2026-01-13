<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TelMissatge
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonBrowseEsp = New System.Windows.Forms.Button()
        Me.TextBoxSearchEsp = New System.Windows.Forms.TextBox()
        Me.ButtonPlayEsp = New System.Windows.Forms.Button()
        Me.ButtonUploadEsp = New System.Windows.Forms.Button()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonBrowseCat = New System.Windows.Forms.Button()
        Me.TextBoxSearchCat = New System.Windows.Forms.TextBox()
        Me.ButtonPlayCat = New System.Windows.Forms.Button()
        Me.ButtonUploadCat = New System.Windows.Forms.Button()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ButtonBrowseEng = New System.Windows.Forms.Button()
        Me.TextBoxSearchEng = New System.Windows.Forms.TextBox()
        Me.ButtonPlayEng = New System.Windows.Forms.Button()
        Me.ButtonUploadEng = New System.Windows.Forms.Button()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nom:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(68, 34)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(393, 20)
        Me.TextBoxNom.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 634)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(474, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(255, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(366, 4)
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
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonBrowseEsp)
        Me.GroupBox1.Controls.Add(Me.TextBoxSearchEsp)
        Me.GroupBox1.Controls.Add(Me.ButtonPlayEsp)
        Me.GroupBox1.Controls.Add(Me.ButtonUploadEsp)
        Me.GroupBox1.Controls.Add(Me.TextBoxEsp)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(451, 159)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Espanyol"
        '
        'ButtonBrowseEsp
        '
        Me.ButtonBrowseEsp.Location = New System.Drawing.Point(282, 121)
        Me.ButtonBrowseEsp.Name = "ButtonBrowseEsp"
        Me.ButtonBrowseEsp.Size = New System.Drawing.Size(31, 24)
        Me.ButtonBrowseEsp.TabIndex = 7
        Me.ButtonBrowseEsp.Text = "..."
        Me.ButtonBrowseEsp.UseVisualStyleBackColor = True
        '
        'TextBoxSearchEsp
        '
        Me.TextBoxSearchEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearchEsp.Enabled = False
        Me.TextBoxSearchEsp.Location = New System.Drawing.Point(6, 124)
        Me.TextBoxSearchEsp.Name = "TextBoxSearchEsp"
        Me.TextBoxSearchEsp.Size = New System.Drawing.Size(270, 20)
        Me.TextBoxSearchEsp.TabIndex = 6
        '
        'ButtonPlayEsp
        '
        Me.ButtonPlayEsp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonPlayEsp.Enabled = False
        Me.ButtonPlayEsp.FlatAppearance.BorderSize = 0
        Me.ButtonPlayEsp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPlayEsp.Image = My.Resources.Resources.wav32
        Me.ButtonPlayEsp.Location = New System.Drawing.Point(400, 121)
        Me.ButtonPlayEsp.Name = "ButtonPlayEsp"
        Me.ButtonPlayEsp.Size = New System.Drawing.Size(45, 38)
        Me.ButtonPlayEsp.TabIndex = 5
        Me.ButtonPlayEsp.UseVisualStyleBackColor = True
        '
        'ButtonUploadEsp
        '
        Me.ButtonUploadEsp.Enabled = False
        Me.ButtonUploadEsp.Location = New System.Drawing.Point(319, 121)
        Me.ButtonUploadEsp.Name = "ButtonUploadEsp"
        Me.ButtonUploadEsp.Size = New System.Drawing.Size(75, 24)
        Me.ButtonUploadEsp.TabIndex = 4
        Me.ButtonUploadEsp.Text = "pujar fitxer"
        Me.ButtonUploadEsp.UseVisualStyleBackColor = True
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(6, 19)
        Me.TextBoxEsp.Multiline = True
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(439, 95)
        Me.TextBoxEsp.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ButtonBrowseCat)
        Me.GroupBox2.Controls.Add(Me.TextBoxSearchCat)
        Me.GroupBox2.Controls.Add(Me.ButtonPlayCat)
        Me.GroupBox2.Controls.Add(Me.ButtonUploadCat)
        Me.GroupBox2.Controls.Add(Me.TextBoxCat)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 247)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(451, 159)
        Me.GroupBox2.TabIndex = 47
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Catalá"
        '
        'ButtonBrowseCat
        '
        Me.ButtonBrowseCat.Location = New System.Drawing.Point(282, 121)
        Me.ButtonBrowseCat.Name = "ButtonBrowseCat"
        Me.ButtonBrowseCat.Size = New System.Drawing.Size(31, 24)
        Me.ButtonBrowseCat.TabIndex = 9
        Me.ButtonBrowseCat.Text = "..."
        Me.ButtonBrowseCat.UseVisualStyleBackColor = True
        '
        'TextBoxSearchCat
        '
        Me.TextBoxSearchCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearchCat.Enabled = False
        Me.TextBoxSearchCat.Location = New System.Drawing.Point(6, 124)
        Me.TextBoxSearchCat.Name = "TextBoxSearchCat"
        Me.TextBoxSearchCat.Size = New System.Drawing.Size(270, 20)
        Me.TextBoxSearchCat.TabIndex = 8
        '
        'ButtonPlayCat
        '
        Me.ButtonPlayCat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonPlayCat.Enabled = False
        Me.ButtonPlayCat.FlatAppearance.BorderSize = 0
        Me.ButtonPlayCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPlayCat.Image = My.Resources.Resources.wav32
        Me.ButtonPlayCat.Location = New System.Drawing.Point(400, 121)
        Me.ButtonPlayCat.Name = "ButtonPlayCat"
        Me.ButtonPlayCat.Size = New System.Drawing.Size(45, 38)
        Me.ButtonPlayCat.TabIndex = 5
        Me.ButtonPlayCat.UseVisualStyleBackColor = True
        '
        'ButtonUploadCat
        '
        Me.ButtonUploadCat.Enabled = False
        Me.ButtonUploadCat.Location = New System.Drawing.Point(319, 121)
        Me.ButtonUploadCat.Name = "ButtonUploadCat"
        Me.ButtonUploadCat.Size = New System.Drawing.Size(75, 24)
        Me.ButtonUploadCat.TabIndex = 4
        Me.ButtonUploadCat.Text = "pujar fitxer"
        Me.ButtonUploadCat.UseVisualStyleBackColor = True
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCat.Location = New System.Drawing.Point(6, 19)
        Me.TextBoxCat.Multiline = True
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(439, 95)
        Me.TextBoxCat.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ButtonBrowseEng)
        Me.GroupBox3.Controls.Add(Me.TextBoxSearchEng)
        Me.GroupBox3.Controls.Add(Me.ButtonPlayEng)
        Me.GroupBox3.Controls.Add(Me.ButtonUploadEng)
        Me.GroupBox3.Controls.Add(Me.TextBoxEng)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 412)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(451, 159)
        Me.GroupBox3.TabIndex = 48
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Anglès"
        '
        'ButtonBrowseEng
        '
        Me.ButtonBrowseEng.Location = New System.Drawing.Point(282, 121)
        Me.ButtonBrowseEng.Name = "ButtonBrowseEng"
        Me.ButtonBrowseEng.Size = New System.Drawing.Size(31, 24)
        Me.ButtonBrowseEng.TabIndex = 9
        Me.ButtonBrowseEng.Text = "..."
        Me.ButtonBrowseEng.UseVisualStyleBackColor = True
        '
        'TextBoxSearchEng
        '
        Me.TextBoxSearchEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearchEng.Enabled = False
        Me.TextBoxSearchEng.Location = New System.Drawing.Point(6, 124)
        Me.TextBoxSearchEng.Name = "TextBoxSearchEng"
        Me.TextBoxSearchEng.Size = New System.Drawing.Size(270, 20)
        Me.TextBoxSearchEng.TabIndex = 8
        '
        'ButtonPlayEng
        '
        Me.ButtonPlayEng.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonPlayEng.Enabled = False
        Me.ButtonPlayEng.FlatAppearance.BorderSize = 0
        Me.ButtonPlayEng.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPlayEng.Image = My.Resources.Resources.wav32
        Me.ButtonPlayEng.Location = New System.Drawing.Point(400, 121)
        Me.ButtonPlayEng.Name = "ButtonPlayEng"
        Me.ButtonPlayEng.Size = New System.Drawing.Size(45, 38)
        Me.ButtonPlayEng.TabIndex = 5
        Me.ButtonPlayEng.UseVisualStyleBackColor = True
        '
        'ButtonUploadEng
        '
        Me.ButtonUploadEng.Enabled = False
        Me.ButtonUploadEng.Location = New System.Drawing.Point(319, 121)
        Me.ButtonUploadEng.Name = "ButtonUploadEng"
        Me.ButtonUploadEng.Size = New System.Drawing.Size(75, 24)
        Me.ButtonUploadEng.TabIndex = 4
        Me.ButtonUploadEng.Text = "pujar fitxer"
        Me.ButtonUploadEng.UseVisualStyleBackColor = True
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEng.Location = New System.Drawing.Point(6, 19)
        Me.TextBoxEng.Multiline = True
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(439, 95)
        Me.TextBoxEng.TabIndex = 2
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.AutoSize = True
        Me.CheckBoxObsoleto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(393, 598)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsoleto.TabIndex = 49
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        Me.CheckBoxObsoleto.UseVisualStyleBackColor = True
        '
        'Frm_TelMissatge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 665)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_TelMissatge"
        Me.Text = "MISSATGE DE CONTESTADOR"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonUploadEsp As System.Windows.Forms.Button
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents ButtonPlayEsp As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPlayCat As System.Windows.Forms.Button
    Friend WithEvents ButtonUploadCat As System.Windows.Forms.Button
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPlayEng As System.Windows.Forms.Button
    Friend WithEvents ButtonUploadEng As System.Windows.Forms.Button
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonBrowseEsp As System.Windows.Forms.Button
    Friend WithEvents TextBoxSearchEsp As System.Windows.Forms.TextBox
    Friend WithEvents ButtonBrowseCat As System.Windows.Forms.Button
    Friend WithEvents TextBoxSearchCat As System.Windows.Forms.TextBox
    Friend WithEvents ButtonBrowseEng As System.Windows.Forms.Button
    Friend WithEvents TextBoxSearchEng As System.Windows.Forms.TextBox
End Class
