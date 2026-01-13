<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Faq
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
        Me.components = New System.ComponentModel.Container()
        Me.TextBoxQuestion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxAnswer = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AfegirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ComboBoxAcces = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonRolsAllowed = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonExternalUrl = New System.Windows.Forms.Button()
        Me.TextBoxExternalUrl = New System.Windows.Forms.TextBox()
        Me.RadioButtonText = New System.Windows.Forms.RadioButton()
        Me.RadioButtonExternalUrl = New System.Windows.Forms.RadioButton()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxQuestion
        '
        Me.TextBoxQuestion.Location = New System.Drawing.Point(40, 62)
        Me.TextBoxQuestion.MaxLength = 100
        Me.TextBoxQuestion.Name = "TextBoxQuestion"
        Me.TextBoxQuestion.Size = New System.Drawing.Size(593, 20)
        Me.TextBoxQuestion.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "pregunta:"
        '
        'TextBoxAnswer
        '
        Me.TextBoxAnswer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAnswer.Location = New System.Drawing.Point(25, 91)
        Me.TextBoxAnswer.Multiline = True
        Me.TextBoxAnswer.Name = "TextBoxAnswer"
        Me.TextBoxAnswer.Size = New System.Drawing.Size(593, 264)
        Me.TextBoxAnswer.TabIndex = 3
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.AfegirToolStripMenuItem, Me.EliminarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 70)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
        '
        'AfegirToolStripMenuItem
        '
        Me.AfegirToolStripMenuItem.Name = "AfegirToolStripMenuItem"
        Me.AfegirToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.AfegirToolStripMenuItem.Text = "afegir"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EliminarToolStripMenuItem.Text = "eliminar"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 464)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(645, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(426, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(537, 4)
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
        'ComboBoxAcces
        '
        Me.ComboBoxAcces.FormattingEnabled = True
        Me.ComboBoxAcces.Items.AddRange(New Object() {"lliure", "hereda", "segons rol"})
        Me.ComboBoxAcces.Location = New System.Drawing.Point(392, 14)
        Me.ComboBoxAcces.Name = "ComboBoxAcces"
        Me.ComboBoxAcces.Size = New System.Drawing.Size(200, 21)
        Me.ComboBoxAcces.TabIndex = 46
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(347, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "acces:"
        '
        'ButtonRolsAllowed
        '
        Me.ButtonRolsAllowed.Location = New System.Drawing.Point(598, 12)
        Me.ButtonRolsAllowed.Name = "ButtonRolsAllowed"
        Me.ButtonRolsAllowed.Size = New System.Drawing.Size(35, 23)
        Me.ButtonRolsAllowed.TabIndex = 49
        Me.ButtonRolsAllowed.Text = "rols..."
        Me.ButtonRolsAllowed.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ButtonExternalUrl)
        Me.GroupBox1.Controls.Add(Me.TextBoxExternalUrl)
        Me.GroupBox1.Controls.Add(Me.RadioButtonText)
        Me.GroupBox1.Controls.Add(Me.RadioButtonExternalUrl)
        Me.GroupBox1.Controls.Add(Me.TextBoxAnswer)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(626, 366)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        '
        'ButtonExternalUrl
        '
        Me.ButtonExternalUrl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExternalUrl.Enabled = False
        Me.ButtonExternalUrl.Location = New System.Drawing.Point(583, 31)
        Me.ButtonExternalUrl.Name = "ButtonExternalUrl"
        Me.ButtonExternalUrl.Size = New System.Drawing.Size(35, 23)
        Me.ButtonExternalUrl.TabIndex = 50
        Me.ButtonExternalUrl.Text = "..."
        Me.ButtonExternalUrl.UseVisualStyleBackColor = True
        '
        'TextBoxExternalUrl
        '
        Me.TextBoxExternalUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxExternalUrl.Enabled = False
        Me.TextBoxExternalUrl.Location = New System.Drawing.Point(25, 33)
        Me.TextBoxExternalUrl.MaxLength = 100
        Me.TextBoxExternalUrl.Name = "TextBoxExternalUrl"
        Me.TextBoxExternalUrl.Size = New System.Drawing.Size(552, 20)
        Me.TextBoxExternalUrl.TabIndex = 7
        '
        'RadioButtonText
        '
        Me.RadioButtonText.AutoSize = True
        Me.RadioButtonText.Checked = True
        Me.RadioButtonText.Location = New System.Drawing.Point(13, 68)
        Me.RadioButtonText.Name = "RadioButtonText"
        Me.RadioButtonText.Size = New System.Drawing.Size(100, 17)
        Me.RadioButtonText.TabIndex = 6
        Me.RadioButtonText.TabStop = True
        Me.RadioButtonText.Text = "resposta de text"
        Me.RadioButtonText.UseVisualStyleBackColor = True
        '
        'RadioButtonExternalUrl
        '
        Me.RadioButtonExternalUrl.AutoSize = True
        Me.RadioButtonExternalUrl.Location = New System.Drawing.Point(13, 10)
        Me.RadioButtonExternalUrl.Name = "RadioButtonExternalUrl"
        Me.RadioButtonExternalUrl.Size = New System.Drawing.Size(156, 17)
        Me.RadioButtonExternalUrl.TabIndex = 5
        Me.RadioButtonExternalUrl.Text = "enllaç a una página externa"
        Me.RadioButtonExternalUrl.UseVisualStyleBackColor = True
        '
        'Frm_Faq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 495)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonRolsAllowed)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBoxAcces)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxQuestion)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Faq"
        Me.Text = "FAQ"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxQuestion As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAnswer As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AfegirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ComboBoxAcces As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonRolsAllowed As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonExternalUrl As System.Windows.Forms.Button
    Friend WithEvents TextBoxExternalUrl As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonText As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonExternalUrl As System.Windows.Forms.RadioButton
End Class
