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
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
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
        Me.Panel1.TabIndex = 54
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(4, 14)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(35, 13)
        Me.LabelVersion.TabIndex = 5
        Me.LabelVersion.Text = "versio"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonCancel, "Frm_Login.htm#ButtonCancel")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonCancel, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonCancel, True)
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonOk, "Frm_Login.htm#ButtonOk")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonOk, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonOk.Location = New System.Drawing.Point(351, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonOk, True)
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'LinkLabelRemindPassword
        '
        Me.LinkLabelRemindPassword.AutoSize = True
        Me.HelpProviderHG.SetHelpKeyword(Me.LinkLabelRemindPassword, "Frm_Login.htm#LinkLabelRemindPassword")
        Me.HelpProviderHG.SetHelpNavigator(Me.LinkLabelRemindPassword, System.Windows.Forms.HelpNavigator.Topic)
        Me.LinkLabelRemindPassword.Location = New System.Drawing.Point(207, 60)
        Me.LinkLabelRemindPassword.Name = "LinkLabelRemindPassword"
        Me.HelpProviderHG.SetShowHelp(Me.LinkLabelRemindPassword, True)
        Me.LinkLabelRemindPassword.Size = New System.Drawing.Size(117, 13)
        Me.LinkLabelRemindPassword.TabIndex = 53
        Me.LinkLabelRemindPassword.TabStop = True
        Me.LinkLabelRemindPassword.Text = "no recordo el password"
        '
        'TextBoxPwd
        '
        Me.TextBoxPwd.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxPwd, "Frm_Login.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxPwd, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxPwd.Location = New System.Drawing.Point(84, 57)
        Me.TextBoxPwd.Name = "TextBoxPwd"
        Me.TextBoxPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxPwd, True)
        Me.TextBoxPwd.Size = New System.Drawing.Size(117, 20)
        Me.TextBoxPwd.TabIndex = 52
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "password"
        '
        'TextBoxEmail
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxEmail, "Frm_Login.htm#Label1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxEmail, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxEmail.Location = New System.Drawing.Point(84, 31)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxEmail, True)
        Me.TextBoxEmail.Size = New System.Drawing.Size(280, 20)
        Me.TextBoxEmail.TabIndex = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "email"
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(397, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(58, 70)
        Me.PictureBox1.TabIndex = 58
        Me.PictureBox1.TabStop = False
        '
        'LabelWarnUser
        '
        Me.LabelWarnUser.AutoSize = True
        Me.LabelWarnUser.ForeColor = System.Drawing.Color.Red
        Me.LabelWarnUser.Location = New System.Drawing.Point(106, 110)
        Me.LabelWarnUser.Name = "LabelWarnUser"
        Me.LabelWarnUser.Size = New System.Drawing.Size(112, 13)
        Me.LabelWarnUser.TabIndex = 57
        Me.LabelWarnUser.Text = "credencials no válides"
        Me.LabelWarnUser.Visible = False
        '
        'PictureBoxWarn
        '
        Me.PictureBoxWarn.Location = New System.Drawing.Point(84, 107)
        Me.PictureBoxWarn.Name = "PictureBoxWarn"
        Me.PictureBoxWarn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarn.TabIndex = 56
        Me.PictureBoxWarn.TabStop = False
        Me.PictureBoxWarn.Visible = False
        '
        'CheckBoxPersist
        '
        Me.CheckBoxPersist.AutoSize = True
        Me.CheckBoxPersist.Enabled = False
        Me.HelpProviderHG.SetHelpKeyword(Me.CheckBoxPersist, "Frm_Login.htm#CheckBoxPersist")
        Me.HelpProviderHG.SetHelpNavigator(Me.CheckBoxPersist, System.Windows.Forms.HelpNavigator.Topic)
        Me.CheckBoxPersist.Location = New System.Drawing.Point(84, 84)
        Me.CheckBoxPersist.Name = "CheckBoxPersist"
        Me.HelpProviderHG.SetShowHelp(Me.CheckBoxPersist, True)
        Me.CheckBoxPersist.Size = New System.Drawing.Size(222, 17)
        Me.CheckBoxPersist.TabIndex = 55
        Me.CheckBoxPersist.Text = "Recordar-me en aquesta máquina i usuari"
        Me.CheckBoxPersist.UseVisualStyleBackColor = True
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
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
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_Login.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_Login"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Frm_Login"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents LabelVersion As Label
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents LinkLabelRemindPassword As LinkLabel
    Friend WithEvents TextBoxPwd As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LabelWarnUser As Label
    Friend WithEvents PictureBoxWarn As PictureBox
    Friend WithEvents CheckBoxPersist As CheckBox
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
