<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ContactAdvancedSearch
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
        Me.RadioButtonCcc = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSubContact = New System.Windows.Forms.RadioButton()
        Me.RadioButtonNif = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAdr = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTel = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEmail = New System.Windows.Forms.RadioButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.Xl_Contacts1 = New Mat.Net.Xl_Contacts()
        Me.RadioButtonGln = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButtonCcc
        '
        Me.RadioButtonCcc.AutoSize = True
        Me.RadioButtonCcc.Location = New System.Drawing.Point(12, 165)
        Me.RadioButtonCcc.Name = "RadioButtonCcc"
        Me.RadioButtonCcc.Size = New System.Drawing.Size(97, 17)
        Me.RadioButtonCcc.TabIndex = 5
        Me.RadioButtonCcc.TabStop = True
        Me.RadioButtonCcc.Tag = "6"
        Me.RadioButtonCcc.Text = "Compte corrent"
        Me.RadioButtonCcc.UseVisualStyleBackColor = True
        '
        'RadioButtonSubContact
        '
        Me.RadioButtonSubContact.AutoSize = True
        Me.RadioButtonSubContact.Location = New System.Drawing.Point(12, 142)
        Me.RadioButtonSubContact.Name = "RadioButtonSubContact"
        Me.RadioButtonSubContact.Size = New System.Drawing.Size(68, 17)
        Me.RadioButtonSubContact.TabIndex = 4
        Me.RadioButtonSubContact.TabStop = True
        Me.RadioButtonSubContact.Tag = "5"
        Me.RadioButtonSubContact.Text = "Contacte"
        Me.RadioButtonSubContact.UseVisualStyleBackColor = True
        '
        'RadioButtonNif
        '
        Me.RadioButtonNif.AutoSize = True
        Me.RadioButtonNif.Location = New System.Drawing.Point(12, 119)
        Me.RadioButtonNif.Name = "RadioButtonNif"
        Me.RadioButtonNif.Size = New System.Drawing.Size(38, 17)
        Me.RadioButtonNif.TabIndex = 3
        Me.RadioButtonNif.TabStop = True
        Me.RadioButtonNif.Tag = "4"
        Me.RadioButtonNif.Text = "Nif"
        Me.RadioButtonNif.UseVisualStyleBackColor = True
        '
        'RadioButtonAdr
        '
        Me.RadioButtonAdr.AutoSize = True
        Me.RadioButtonAdr.Location = New System.Drawing.Point(12, 96)
        Me.RadioButtonAdr.Name = "RadioButtonAdr"
        Me.RadioButtonAdr.Size = New System.Drawing.Size(59, 17)
        Me.RadioButtonAdr.TabIndex = 2
        Me.RadioButtonAdr.TabStop = True
        Me.RadioButtonAdr.Tag = "3"
        Me.RadioButtonAdr.Text = "Adreça"
        Me.RadioButtonAdr.UseVisualStyleBackColor = True
        '
        'RadioButtonTel
        '
        Me.RadioButtonTel.AutoSize = True
        Me.RadioButtonTel.Location = New System.Drawing.Point(12, 73)
        Me.RadioButtonTel.Name = "RadioButtonTel"
        Me.RadioButtonTel.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonTel.TabIndex = 1
        Me.RadioButtonTel.TabStop = True
        Me.RadioButtonTel.Tag = "2"
        Me.RadioButtonTel.Text = "Telefon"
        Me.RadioButtonTel.UseVisualStyleBackColor = True
        '
        'RadioButtonEmail
        '
        Me.RadioButtonEmail.AutoSize = True
        Me.RadioButtonEmail.Location = New System.Drawing.Point(12, 50)
        Me.RadioButtonEmail.Name = "RadioButtonEmail"
        Me.RadioButtonEmail.Size = New System.Drawing.Size(50, 17)
        Me.RadioButtonEmail.TabIndex = 0
        Me.RadioButtonEmail.TabStop = True
        Me.RadioButtonEmail.Tag = "1"
        Me.RadioButtonEmail.Text = "Email"
        Me.RadioButtonEmail.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(592, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 234)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(509, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.AllowUserToAddRows = False
        Me.Xl_Contacts1.AllowUserToDeleteRows = False
        Me.Xl_Contacts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Location = New System.Drawing.Point(156, 39)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.ReadOnly = True
        Me.Xl_Contacts1.Size = New System.Drawing.Size(448, 189)
        Me.Xl_Contacts1.TabIndex = 43
        '
        'RadioButtonGln
        '
        Me.RadioButtonGln.AutoSize = True
        Me.RadioButtonGln.Location = New System.Drawing.Point(12, 188)
        Me.RadioButtonGln.Name = "RadioButtonGln"
        Me.RadioButtonGln.Size = New System.Drawing.Size(41, 17)
        Me.RadioButtonGln.TabIndex = 44
        Me.RadioButtonGln.TabStop = True
        Me.RadioButtonGln.Tag = "6"
        Me.RadioButtonGln.Text = "Gln"
        Me.RadioButtonGln.UseVisualStyleBackColor = True
        '
        'Frm_ContactAdvancedSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 265)
        Me.Controls.Add(Me.RadioButtonGln)
        Me.Controls.Add(Me.Xl_Contacts1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadioButtonCcc)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.RadioButtonSubContact)
        Me.Controls.Add(Me.RadioButtonNif)
        Me.Controls.Add(Me.RadioButtonAdr)
        Me.Controls.Add(Me.RadioButtonEmail)
        Me.Controls.Add(Me.RadioButtonTel)
        Me.Name = "Frm_ContactAdvancedSearch"
        Me.Text = "Cerca avançada de contacte"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButtonEmail As RadioButton
    Friend WithEvents RadioButtonNif As RadioButton
    Friend WithEvents RadioButtonAdr As RadioButton
    Friend WithEvents RadioButtonTel As RadioButton
    Friend WithEvents RadioButtonCcc As RadioButton
    Friend WithEvents RadioButtonSubContact As RadioButton
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents Xl_Contacts1 As Xl_Contacts
    Friend WithEvents RadioButtonGln As RadioButton
End Class
