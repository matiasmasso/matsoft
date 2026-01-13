<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ContactClass
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
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CheckBoxRaffles = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSalePoint = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupDistributionChannel1 = New Xl_LookupDistributionChannel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDownOrd = New System.Windows.Forms.NumericUpDown()
        Me.TextBoxPor = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Xl_Contacts1 = New Xl_Contacts()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(78, 94)
        Me.TextBoxEsp.MaxLength = 50
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxEsp.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 377)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(490, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(271, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 8
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(382, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 7
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
        Me.ButtonDel.TabIndex = 9
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Espanyol"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 33)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(490, 342)
        Me.TabControl1.TabIndex = 58
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.CheckBoxRaffles)
        Me.TabPage1.Controls.Add(Me.CheckBoxSalePoint)
        Me.TabPage1.Controls.Add(Me.Xl_LookupDistributionChannel1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.NumericUpDownOrd)
        Me.TabPage1.Controls.Add(Me.TextBoxPor)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxEng)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxCat)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxEsp)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(482, 316)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'CheckBoxRaffles
        '
        Me.CheckBoxRaffles.AutoSize = True
        Me.CheckBoxRaffles.Location = New System.Drawing.Point(78, 232)
        Me.CheckBoxRaffles.Name = "CheckBoxRaffles"
        Me.CheckBoxRaffles.Size = New System.Drawing.Size(121, 17)
        Me.CheckBoxRaffles.TabIndex = 68
        Me.CheckBoxRaffles.Text = "Participa en sortejos"
        Me.CheckBoxRaffles.UseVisualStyleBackColor = True
        '
        'CheckBoxSalePoint
        '
        Me.CheckBoxSalePoint.AutoSize = True
        Me.CheckBoxSalePoint.Location = New System.Drawing.Point(78, 209)
        Me.CheckBoxSalePoint.Name = "CheckBoxSalePoint"
        Me.CheckBoxSalePoint.Size = New System.Drawing.Size(168, 17)
        Me.CheckBoxSalePoint.TabIndex = 5
        Me.CheckBoxSalePoint.Text = "Publicar com a punt de venda"
        Me.CheckBoxSalePoint.UseVisualStyleBackColor = True
        '
        'Xl_LookupDistributionChannel1
        '
        Me.Xl_LookupDistributionChannel1.DistributionChannel = Nothing
        Me.Xl_LookupDistributionChannel1.IsDirty = False
        Me.Xl_LookupDistributionChannel1.Location = New System.Drawing.Point(78, 47)
        Me.Xl_LookupDistributionChannel1.Name = "Xl_LookupDistributionChannel1"
        Me.Xl_LookupDistributionChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDistributionChannel1.ReadOnlyLookup = False
        Me.Xl_LookupDistributionChannel1.Size = New System.Drawing.Size(305, 20)
        Me.Xl_LookupDistributionChannel1.TabIndex = 0
        Me.Xl_LookupDistributionChannel1.Value = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(22, 267)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "Ordre"
        '
        'NumericUpDownOrd
        '
        Me.NumericUpDownOrd.Location = New System.Drawing.Point(78, 265)
        Me.NumericUpDownOrd.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDownOrd.Name = "NumericUpDownOrd"
        Me.NumericUpDownOrd.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownOrd.TabIndex = 6
        '
        'TextBoxPor
        '
        Me.TextBoxPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPor.Location = New System.Drawing.Point(78, 173)
        Me.TextBoxPor.MaxLength = 50
        Me.TextBoxPor.Name = "TextBoxPor"
        Me.TextBoxPor.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxPor.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "Portuguès"
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEng.Location = New System.Drawing.Point(78, 146)
        Me.TextBoxEng.MaxLength = 50
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxEng.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Anglès"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCat.Location = New System.Drawing.Point(78, 120)
        Me.TextBoxCat.MaxLength = 50
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxCat.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Català"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Canal"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage2.Controls.Add(Me.Xl_Contacts1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(482, 316)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Contactes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(267, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(212, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Location = New System.Drawing.Point(0, 37)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.Size = New System.Drawing.Size(479, 279)
        Me.Xl_Contacts1.TabIndex = 0
        '
        'Frm_ContactClass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 408)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_ContactClass"
        Me.Text = "Classificació contactes"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownOrd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextBoxEsp As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Contacts1 As Xl_Contacts
    Friend WithEvents TextBoxPor As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxEng As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxCat As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents NumericUpDownOrd As NumericUpDown
    Friend WithEvents Xl_LookupDistributionChannel1 As Xl_LookupDistributionChannel
    Friend WithEvents CheckBoxSalePoint As CheckBox
    Friend WithEvents CheckBoxRaffles As CheckBox
End Class
