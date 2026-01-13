<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Login))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.LinkLabelRemindPassword = New System.Windows.Forms.LinkLabel()
        Me.TextBoxPwd = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LabelWarnUser = New System.Windows.Forms.Label()
        Me.PictureBoxWarn = New System.Windows.Forms.PictureBox()
        Me.CheckBoxPersist = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.LabelVersion)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 156)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(460, 31)
        Me.Panel1.TabIndex = 44
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(4, 14)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(41, 13)
        Me.LabelVersion.TabIndex = 5
        Me.LabelVersion.Text = "version"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(351, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'LinkLabelRemindPassword
        '
        Me.LinkLabelRemindPassword.AutoSize = True
        Me.LinkLabelRemindPassword.Location = New System.Drawing.Point(207, 61)
        Me.LinkLabelRemindPassword.Name = "LinkLabelRemindPassword"
        Me.LinkLabelRemindPassword.Size = New System.Drawing.Size(117, 13)
        Me.LinkLabelRemindPassword.TabIndex = 43
        Me.LinkLabelRemindPassword.TabStop = True
        Me.LinkLabelRemindPassword.Text = "no recordo el password"
        '
        'TextBoxPwd
        '
        Me.TextBoxPwd.Enabled = False
        Me.TextBoxPwd.Location = New System.Drawing.Point(84, 58)
        Me.TextBoxPwd.Name = "TextBoxPwd"
        Me.TextBoxPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPwd.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxPwd.TabIndex = 42
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "password"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(84, 32)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(280, 20)
        Me.TextBoxEmail.TabIndex = 40
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "email"
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(397, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(58, 70)
        Me.PictureBox1.TabIndex = 48
        Me.PictureBox1.TabStop = False
        '
        'LabelWarnUser
        '
        Me.LabelWarnUser.AutoSize = True
        Me.LabelWarnUser.ForeColor = System.Drawing.Color.Red
        Me.LabelWarnUser.Location = New System.Drawing.Point(106, 111)
        Me.LabelWarnUser.Name = "LabelWarnUser"
        Me.LabelWarnUser.Size = New System.Drawing.Size(112, 13)
        Me.LabelWarnUser.TabIndex = 47
        Me.LabelWarnUser.Text = "credencials no valides"
        Me.LabelWarnUser.Visible = False
        '
        'PictureBoxWarn
        '
        Me.PictureBoxWarn.Image = Global.Mat.NET.My.Resources.Resources.warn
        Me.PictureBoxWarn.Location = New System.Drawing.Point(84, 108)
        Me.PictureBoxWarn.Name = "PictureBoxWarn"
        Me.PictureBoxWarn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarn.TabIndex = 46
        Me.PictureBoxWarn.TabStop = False
        Me.PictureBoxWarn.Visible = False
        '
        'CheckBoxPersist
        '
        Me.CheckBoxPersist.AutoSize = True
        Me.CheckBoxPersist.Enabled = False
        Me.CheckBoxPersist.Location = New System.Drawing.Point(84, 85)
        Me.CheckBoxPersist.Name = "CheckBoxPersist"
        Me.CheckBoxPersist.Size = New System.Drawing.Size(222, 17)
        Me.CheckBoxPersist.TabIndex = 45
        Me.CheckBoxPersist.Text = "Recordar-me en aquesta máquina i usuari"
        Me.CheckBoxPersist.UseVisualStyleBackColor = True
        '
        'Frm_Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 187)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LinkLabelRemindPassword)
        Me.Controls.Add(Me.TextBoxPwd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LabelWarnUser)
        Me.Controls.Add(Me.PictureBoxWarn)
        Me.Controls.Add(Me.CheckBoxPersist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_Login"
        Me.Text = "Frm_Login"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents LinkLabelRemindPassword As System.Windows.Forms.LinkLabel
    Friend WithEvents TextBoxPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelWarnUser As System.Windows.Forms.Label
    Friend WithEvents PictureBoxWarn As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxPersist As System.Windows.Forms.CheckBox
End Class
