<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cnap
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
        Me.TextBoxParent = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCod = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTags = New System.Windows.Forms.TextBox()
        Me.Xl_LangsTextShort = New Winforms.Xl_LangsText()
        Me.TabControlLong = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_LangsTextLong = New Winforms.Xl_LangsText()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_Products1 = New Winforms.Xl_Products()
        Me.Panel1.SuspendLayout()
        Me.TabControlLong.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxParent
        '
        Me.TextBoxParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxParent.Location = New System.Drawing.Point(90, 14)
        Me.TextBoxParent.Name = "TextBoxParent"
        Me.TextBoxParent.ReadOnly = True
        Me.TextBoxParent.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxParent.TabIndex = 0
        Me.TextBoxParent.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "descendeix de:"
        '
        'TextBoxCod
        '
        Me.TextBoxCod.Location = New System.Drawing.Point(90, 40)
        Me.TextBoxCod.MaxLength = 8
        Me.TextBoxCod.Name = "TextBoxCod"
        Me.TextBoxCod.Size = New System.Drawing.Size(78, 20)
        Me.TextBoxCod.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "codi:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 424)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(-305, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 29
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonCancel.Location = New System.Drawing.Point(267, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 26
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(378, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 27
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "tags: (1 per linia)"
        '
        'TextBoxTags
        '
        Me.TextBoxTags.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTags.Location = New System.Drawing.Point(6, 98)
        Me.TextBoxTags.Multiline = True
        Me.TextBoxTags.Name = "TextBoxTags"
        Me.TextBoxTags.Size = New System.Drawing.Size(467, 268)
        Me.TextBoxTags.TabIndex = 44
        '
        'Xl_LangsTextShort
        '
        Me.Xl_LangsTextShort.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LangsTextShort.Location = New System.Drawing.Point(0, 40)
        Me.Xl_LangsTextShort.Name = "Xl_LangsTextShort"
        Me.Xl_LangsTextShort.Size = New System.Drawing.Size(481, 332)
        Me.Xl_LangsTextShort.TabIndex = 45
        '
        'TabControlLong
        '
        Me.TabControlLong.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlLong.Controls.Add(Me.TabPage1)
        Me.TabControlLong.Controls.Add(Me.TabPage2)
        Me.TabControlLong.Controls.Add(Me.TabPage3)
        Me.TabControlLong.Controls.Add(Me.TabPage4)
        Me.TabControlLong.Location = New System.Drawing.Point(0, 24)
        Me.TabControlLong.Name = "TabControlLong"
        Me.TabControlLong.SelectedIndex = 0
        Me.TabControlLong.Size = New System.Drawing.Size(489, 398)
        Me.TabControlLong.TabIndex = 46
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxTags)
        Me.TabPage1.Controls.Add(Me.TextBoxParent)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxCod)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(481, 372)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Xl_LangsTextShort)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(481, 372)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Nom curt"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(231, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Nom per distingir-lo dins de la categoria superior"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.Xl_LangsTextLong)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(481, 372)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Nom llarg"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Nom absolut"
        '
        'Xl_LangsTextLong
        '
        Me.Xl_LangsTextLong.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LangsTextLong.Location = New System.Drawing.Point(3, 37)
        Me.Xl_LangsTextLong.Name = "Xl_LangsTextLong"
        Me.Xl_LangsTextLong.Size = New System.Drawing.Size(478, 332)
        Me.Xl_LangsTextLong.TabIndex = 47
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_Products1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(481, 372)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Productes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_Products1
        '
        Me.Xl_Products1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Products1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Products1.Name = "Xl_Products1"
        Me.Xl_Products1.Size = New System.Drawing.Size(475, 366)
        Me.Xl_Products1.TabIndex = 0
        '
        'Frm_Cnap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 455)
        Me.Controls.Add(Me.TabControlLong)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Cnap"
        Me.Text = "CNAP"
        Me.Panel1.ResumeLayout(False)
        Me.TabControlLong.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBoxParent As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCod As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTags As System.Windows.Forms.TextBox
    Friend WithEvents Xl_LangsTextShort As Xl_LangsText
    Friend WithEvents TabControlLong As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_LangsTextLong As Xl_LangsText
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_Products1 As Xl_Products
End Class
