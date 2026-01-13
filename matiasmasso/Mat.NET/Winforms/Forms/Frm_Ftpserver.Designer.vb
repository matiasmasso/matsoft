<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ftpserver
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
        Me.TextBoxServerName = New System.Windows.Forms.TextBox()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxUsername = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxPort = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxSsl = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPassiveMode = New System.Windows.Forms.CheckBox()
        Me.Xl_FtpserverPaths1 = New Winforms.Xl_FtpserverPaths()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PanelButtons.SuspendLayout()
        CType(Me.Xl_FtpserverPaths1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxServerName
        '
        Me.TextBoxServerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxServerName.Location = New System.Drawing.Point(74, 28)
        Me.TextBoxServerName.Name = "TextBoxServerName"
        Me.TextBoxServerName.Size = New System.Drawing.Size(205, 20)
        Me.TextBoxServerName.TabIndex = 58
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 374)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(459, 31)
        Me.PanelButtons.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(351, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Servidor"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(74, 54)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(380, 20)
        Me.Xl_Contact21.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Propietari"
        '
        'TextBoxUsername
        '
        Me.TextBoxUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsername.Location = New System.Drawing.Point(74, 80)
        Me.TextBoxUsername.Name = "TextBoxUsername"
        Me.TextBoxUsername.Size = New System.Drawing.Size(205, 20)
        Me.TextBoxUsername.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Usuari"
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPassword.Location = New System.Drawing.Point(74, 106)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(205, 20)
        Me.TextBoxPassword.TabIndex = 64
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Password"
        '
        'TextBoxPort
        '
        Me.TextBoxPort.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPort.Location = New System.Drawing.Point(74, 132)
        Me.TextBoxPort.Name = "TextBoxPort"
        Me.TextBoxPort.Size = New System.Drawing.Size(62, 20)
        Me.TextBoxPort.TabIndex = 66
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Port"
        '
        'CheckBoxSsl
        '
        Me.CheckBoxSsl.AutoSize = True
        Me.CheckBoxSsl.Location = New System.Drawing.Point(74, 159)
        Me.CheckBoxSsl.Name = "CheckBoxSsl"
        Me.CheckBoxSsl.Size = New System.Drawing.Size(46, 17)
        Me.CheckBoxSsl.TabIndex = 67
        Me.CheckBoxSsl.Text = "SSL"
        Me.CheckBoxSsl.UseVisualStyleBackColor = True
        '
        'CheckBoxPassiveMode
        '
        Me.CheckBoxPassiveMode.AutoSize = True
        Me.CheckBoxPassiveMode.Location = New System.Drawing.Point(74, 182)
        Me.CheckBoxPassiveMode.Name = "CheckBoxPassiveMode"
        Me.CheckBoxPassiveMode.Size = New System.Drawing.Size(86, 17)
        Me.CheckBoxPassiveMode.TabIndex = 68
        Me.CheckBoxPassiveMode.Text = "Modo passiu"
        Me.CheckBoxPassiveMode.UseVisualStyleBackColor = True
        '
        'Xl_FtpserverPaths1
        '
        Me.Xl_FtpserverPaths1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FtpserverPaths1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_FtpserverPaths1.DisplayObsolets = False
        Me.Xl_FtpserverPaths1.Filter = Nothing
        Me.Xl_FtpserverPaths1.Location = New System.Drawing.Point(13, 231)
        Me.Xl_FtpserverPaths1.MouseIsDown = False
        Me.Xl_FtpserverPaths1.Name = "Xl_FtpserverPaths1"
        Me.Xl_FtpserverPaths1.Size = New System.Drawing.Size(441, 137)
        Me.Xl_FtpserverPaths1.TabIndex = 69
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 215)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Rutes"
        '
        'Frm_Ftpserver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 405)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_FtpserverPaths1)
        Me.Controls.Add(Me.CheckBoxPassiveMode)
        Me.Controls.Add(Me.CheckBoxSsl)
        Me.Controls.Add(Me.TextBoxPort)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxPassword)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxUsername)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.TextBoxServerName)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Ftpserver"
        Me.Text = "Servidor Ftp"
        Me.PanelButtons.ResumeLayout(False)
        CType(Me.Xl_FtpserverPaths1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxServerName As TextBox
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxUsername As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxPort As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBoxSsl As CheckBox
    Friend WithEvents CheckBoxPassiveMode As CheckBox
    Friend WithEvents Xl_FtpserverPaths1 As Xl_FtpserverPaths
    Friend WithEvents Label6 As Label
End Class
