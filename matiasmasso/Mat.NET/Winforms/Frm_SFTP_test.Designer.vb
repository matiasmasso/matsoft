<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SFTP_test
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
        Me.TextBoxHost = New System.Windows.Forms.TextBox()
        Me.TextBoxPort = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxUserUpload = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxUserDownload = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ButtonConnect = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxPKKFile = New System.Windows.Forms.TextBox()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Host"
        '
        'TextBoxHost
        '
        Me.TextBoxHost.Location = New System.Drawing.Point(137, 59)
        Me.TextBoxHost.Name = "TextBoxHost"
        Me.TextBoxHost.Size = New System.Drawing.Size(179, 20)
        Me.TextBoxHost.TabIndex = 1
        Me.TextBoxHost.Text = "sftp-eu.amazonsedi.com"
        '
        'TextBoxPort
        '
        Me.TextBoxPort.Location = New System.Drawing.Point(137, 85)
        Me.TextBoxPort.Name = "TextBoxPort"
        Me.TextBoxPort.Size = New System.Drawing.Size(55, 20)
        Me.TextBoxPort.TabIndex = 3
        Me.TextBoxPort.Text = "2222"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(41, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Port"
        '
        'TextBoxUserUpload
        '
        Me.TextBoxUserUpload.Location = New System.Drawing.Point(137, 111)
        Me.TextBoxUserUpload.Name = "TextBoxUserUpload"
        Me.TextBoxUserUpload.Size = New System.Drawing.Size(179, 20)
        Me.TextBoxUserUpload.TabIndex = 5
        Me.TextBoxUserUpload.Text = "IQG9F4L98DAT"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Upload User"
        '
        'TextBoxUserDownload
        '
        Me.TextBoxUserDownload.Location = New System.Drawing.Point(137, 137)
        Me.TextBoxUserDownload.Name = "TextBoxUserDownload"
        Me.TextBoxUserDownload.Size = New System.Drawing.Size(179, 20)
        Me.TextBoxUserDownload.TabIndex = 7
        Me.TextBoxUserDownload.Text = "3L6EU2US5D5ID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(41, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Download User"
        '
        'ButtonConnect
        '
        Me.ButtonConnect.Location = New System.Drawing.Point(241, 226)
        Me.ButtonConnect.Name = "ButtonConnect"
        Me.ButtonConnect.Size = New System.Drawing.Size(75, 23)
        Me.ButtonConnect.TabIndex = 8
        Me.ButtonConnect.Text = "Connecta"
        Me.ButtonConnect.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(41, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Private Key File"
        '
        'TextBoxPKKFile
        '
        Me.TextBoxPKKFile.Location = New System.Drawing.Point(137, 166)
        Me.TextBoxPKKFile.Name = "TextBoxPKKFile"
        Me.TextBoxPKKFile.Size = New System.Drawing.Size(142, 20)
        Me.TextBoxPKKFile.TabIndex = 10
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Location = New System.Drawing.Point(285, 166)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(31, 20)
        Me.ButtonBrowse.TabIndex = 11
        Me.ButtonBrowse.Text = "..."
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'Frm_SFTP_test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 261)
        Me.Controls.Add(Me.ButtonBrowse)
        Me.Controls.Add(Me.TextBoxPKKFile)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ButtonConnect)
        Me.Controls.Add(Me.TextBoxUserDownload)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxUserUpload)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxPort)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxHost)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_SFTP_test"
        Me.Text = "Frm_SFTP_test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxHost As TextBox
    Friend WithEvents TextBoxPort As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxUserUpload As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxUserDownload As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ButtonConnect As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxPKKFile As TextBox
    Friend WithEvents ButtonBrowse As Button
End Class
