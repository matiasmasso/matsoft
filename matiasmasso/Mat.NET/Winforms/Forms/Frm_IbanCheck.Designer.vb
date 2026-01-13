<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IbanCheck
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
        Me.TextBoxCcc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_BankBranch1 = New Winforms.Xl_Lookup_BankBranch()
        Me.LabelBranch = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.PictureBoxWarn = New System.Windows.Forms.PictureBox()
        Me.ButtonCheck = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxCcc
        '
        Me.TextBoxCcc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCcc.Location = New System.Drawing.Point(102, 43)
        Me.TextBoxCcc.Name = "TextBoxCcc"
        Me.TextBoxCcc.Size = New System.Drawing.Size(217, 20)
        Me.TextBoxCcc.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Iban"
        '
        'Xl_Lookup_BankBranch1
        '
        Me.Xl_Lookup_BankBranch1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Lookup_BankBranch1.Branch = Nothing
        Me.Xl_Lookup_BankBranch1.IsDirty = False
        Me.Xl_Lookup_BankBranch1.Location = New System.Drawing.Point(102, 70)
        Me.Xl_Lookup_BankBranch1.Name = "Xl_Lookup_BankBranch1"
        Me.Xl_Lookup_BankBranch1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_BankBranch1.ReadOnlyLookup = False
        Me.Xl_Lookup_BankBranch1.Size = New System.Drawing.Size(317, 20)
        Me.Xl_Lookup_BankBranch1.TabIndex = 2
        Me.Xl_Lookup_BankBranch1.Value = Nothing
        Me.Xl_Lookup_BankBranch1.Visible = False
        '
        'LabelBranch
        '
        Me.LabelBranch.AutoSize = True
        Me.LabelBranch.Location = New System.Drawing.Point(12, 74)
        Me.LabelBranch.Name = "LabelBranch"
        Me.LabelBranch.Size = New System.Drawing.Size(84, 13)
        Me.LabelBranch.TabIndex = 3
        Me.LabelBranch.Text = "Oficina bancària"
        Me.LabelBranch.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 210)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(431, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(212, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(323, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxObs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxObs.Location = New System.Drawing.Point(102, 96)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(317, 108)
        Me.TextBoxObs.TabIndex = 43
        '
        'PictureBoxWarn
        '
        Me.PictureBoxWarn.Image = Global.Winforms.My.Resources.Resources.warn_16
        Me.PictureBoxWarn.Location = New System.Drawing.Point(80, 96)
        Me.PictureBoxWarn.Name = "PictureBoxWarn"
        Me.PictureBoxWarn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarn.TabIndex = 44
        Me.PictureBoxWarn.TabStop = False
        Me.PictureBoxWarn.Visible = False
        '
        'ButtonCheck
        '
        Me.ButtonCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCheck.Enabled = False
        Me.ButtonCheck.Location = New System.Drawing.Point(325, 44)
        Me.ButtonCheck.Name = "ButtonCheck"
        Me.ButtonCheck.Size = New System.Drawing.Size(57, 20)
        Me.ButtonCheck.TabIndex = 45
        Me.ButtonCheck.Text = "validar"
        Me.ButtonCheck.UseVisualStyleBackColor = True
        '
        'Frm_IbanCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 241)
        Me.Controls.Add(Me.ButtonCheck)
        Me.Controls.Add(Me.PictureBoxWarn)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelBranch)
        Me.Controls.Add(Me.Xl_Lookup_BankBranch1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxCcc)
        Me.Name = "Frm_IbanCheck"
        Me.Text = "Dades Iban"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxCcc As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Lookup_BankBranch1 As Xl_Lookup_BankBranch
    Friend WithEvents LabelBranch As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents PictureBoxWarn As PictureBox
    Friend WithEvents ButtonCheck As Button
End Class
