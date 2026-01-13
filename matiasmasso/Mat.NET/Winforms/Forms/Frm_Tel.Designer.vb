<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Tel
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
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.LabelNum = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.LabelCod = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNum.Location = New System.Drawing.Point(107, 63)
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxNum.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 154)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(117, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 6
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(228, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 5
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'LabelNum
        '
        Me.LabelNum.AutoSize = True
        Me.LabelNum.Location = New System.Drawing.Point(11, 66)
        Me.LabelNum.Name = "LabelNum"
        Me.LabelNum.Size = New System.Drawing.Size(44, 13)
        Me.LabelNum.TabIndex = 56
        Me.LabelNum.Text = "Numero"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(107, 89)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(225, 20)
        Me.TextBoxObs.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Observacions"
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(107, 115)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxPrivat.TabIndex = 4
        Me.CheckBoxPrivat.Text = "Privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(107, 37)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(225, 20)
        Me.Xl_LookupArea1.TabIndex = 1
        Me.Xl_LookupArea1.TabStop = False
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Pais"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"(tria un dispositiu)", "Telèfon", "Fax", "Mobil"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(107, 10)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxCod.TabIndex = 0
        Me.ComboBoxCod.TabStop = False
        Me.ComboBoxCod.Text = "(tria'n un)"
        Me.ComboBoxCod.Visible = False
        '
        'LabelCod
        '
        Me.LabelCod.AutoSize = True
        Me.LabelCod.Location = New System.Drawing.Point(12, 13)
        Me.LabelCod.Name = "LabelCod"
        Me.LabelCod.Size = New System.Drawing.Size(52, 13)
        Me.LabelCod.TabIndex = 64
        Me.LabelCod.Text = "Dispositiu"
        Me.LabelCod.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(228, 65)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 65
        Me.PictureBox1.TabStop = False
        '
        'Frm_Tel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 185)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LabelCod)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.CheckBoxPrivat)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelNum)
        Me.Name = "Frm_Tel"
        Me.Text = "Telèfon"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxNum As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents LabelNum As Label
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBoxPrivat As CheckBox
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxCod As ComboBox
    Friend WithEvents LabelCod As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
