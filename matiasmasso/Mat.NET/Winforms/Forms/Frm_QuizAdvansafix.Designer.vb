<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_QuizAdvansafix
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDownNoSICTPurchased = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownQtyNoSICT = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDownQtySICT = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDownSICTPurchased = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxClicked = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxConfirmed = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxLastUser = New System.Windows.Forms.TextBox()
        Me.CheckBoxSplioOpen = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownNoSICTPurchased, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownQtyNoSICT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDownQtySICT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownSICTPurchased, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 407)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(410, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(191, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(302, 4)
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
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(97, 34)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = True
        Me.Xl_Contact21.Size = New System.Drawing.Size(301, 20)
        Me.Xl_Contact21.TabIndex = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Client"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(70, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "comprades"
        '
        'NumericUpDownNoSICTPurchased
        '
        Me.NumericUpDownNoSICTPurchased.Enabled = False
        Me.NumericUpDownNoSICTPurchased.Location = New System.Drawing.Point(139, 26)
        Me.NumericUpDownNoSICTPurchased.Name = "NumericUpDownNoSICTPurchased"
        Me.NumericUpDownNoSICTPurchased.ReadOnly = True
        Me.NumericUpDownNoSICTPurchased.Size = New System.Drawing.Size(55, 20)
        Me.NumericUpDownNoSICTPurchased.TabIndex = 53
        Me.NumericUpDownNoSICTPurchased.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownQtyNoSICT
        '
        Me.NumericUpDownQtyNoSICT.Location = New System.Drawing.Point(139, 50)
        Me.NumericUpDownQtyNoSICT.Name = "NumericUpDownQtyNoSICT"
        Me.NumericUpDownQtyNoSICT.Size = New System.Drawing.Size(55, 20)
        Me.NumericUpDownQtyNoSICT.TabIndex = 55
        Me.NumericUpDownQtyNoSICT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(70, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "en stock"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NumericUpDownQtyNoSICT)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownNoSICTPurchased)
        Me.GroupBox1.Location = New System.Drawing.Point(95, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 85)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Advansafix II"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.NumericUpDownQtySICT)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.NumericUpDownSICTPurchased)
        Me.GroupBox2.Location = New System.Drawing.Point(97, 170)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 85)
        Me.GroupBox2.TabIndex = 57
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Advansafix II SICT"
        '
        'NumericUpDownQtySICT
        '
        Me.NumericUpDownQtySICT.Location = New System.Drawing.Point(139, 50)
        Me.NumericUpDownQtySICT.Name = "NumericUpDownQtySICT"
        Me.NumericUpDownQtySICT.Size = New System.Drawing.Size(55, 20)
        Me.NumericUpDownQtySICT.TabIndex = 55
        Me.NumericUpDownQtySICT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(70, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "comprades"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(70, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "en stock"
        '
        'NumericUpDownSICTPurchased
        '
        Me.NumericUpDownSICTPurchased.Enabled = False
        Me.NumericUpDownSICTPurchased.Location = New System.Drawing.Point(139, 26)
        Me.NumericUpDownSICTPurchased.Name = "NumericUpDownSICTPurchased"
        Me.NumericUpDownSICTPurchased.ReadOnly = True
        Me.NumericUpDownSICTPurchased.Size = New System.Drawing.Size(55, 20)
        Me.NumericUpDownSICTPurchased.TabIndex = 53
        Me.NumericUpDownSICTPurchased.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 310)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "Email obert"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 336)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "Enllaç clicat"
        '
        'TextBoxClicked
        '
        Me.TextBoxClicked.Location = New System.Drawing.Point(97, 333)
        Me.TextBoxClicked.Name = "TextBoxClicked"
        Me.TextBoxClicked.ReadOnly = True
        Me.TextBoxClicked.Size = New System.Drawing.Size(200, 20)
        Me.TextBoxClicked.TabIndex = 61
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 362)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "Resposta"
        '
        'TextBoxConfirmed
        '
        Me.TextBoxConfirmed.Location = New System.Drawing.Point(97, 359)
        Me.TextBoxConfirmed.Name = "TextBoxConfirmed"
        Me.TextBoxConfirmed.ReadOnly = True
        Me.TextBoxConfirmed.Size = New System.Drawing.Size(200, 20)
        Me.TextBoxConfirmed.TabIndex = 63
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 284)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "Ultim usuari"
        '
        'TextBoxLastUser
        '
        Me.TextBoxLastUser.Location = New System.Drawing.Point(97, 281)
        Me.TextBoxLastUser.Name = "TextBoxLastUser"
        Me.TextBoxLastUser.ReadOnly = True
        Me.TextBoxLastUser.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxLastUser.TabIndex = 66
        '
        'CheckBoxSplioOpen
        '
        Me.CheckBoxSplioOpen.AutoSize = True
        Me.CheckBoxSplioOpen.Enabled = False
        Me.CheckBoxSplioOpen.Location = New System.Drawing.Point(97, 310)
        Me.CheckBoxSplioOpen.Name = "CheckBoxSplioOpen"
        Me.CheckBoxSplioOpen.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxSplioOpen.TabIndex = 67
        Me.CheckBoxSplioOpen.UseVisualStyleBackColor = True
        '
        'Frm_QuizAdvansafix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 438)
        Me.Controls.Add(Me.CheckBoxSplioOpen)
        Me.Controls.Add(Me.TextBoxLastUser)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxConfirmed)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxClicked)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_QuizAdvansafix"
        Me.Text = "Enquesta Advansafix"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownNoSICTPurchased, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownQtyNoSICT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDownQtySICT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownSICTPurchased, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDownNoSICTPurchased As NumericUpDown
    Friend WithEvents NumericUpDownQtyNoSICT As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents NumericUpDownQtySICT As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDownSICTPurchased As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxClicked As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxConfirmed As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxLastUser As TextBox
    Friend WithEvents CheckBoxSplioOpen As CheckBox
End Class
