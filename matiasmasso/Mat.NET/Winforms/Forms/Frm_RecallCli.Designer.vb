<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RecallCli
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComandaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxRecall = New System.Windows.Forms.TextBox()
        Me.TextBoxContactNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxContactEmail = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxContactTel = New System.Windows.Forms.TextBox()
        Me.TextBoxCustomer = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxAddress = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxZip = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxFchVivace = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxLocation = New System.Windows.Forms.TextBox()
        Me.TextBoxFchCreated = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.CheckBoxFchVivace = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxRegMuelle = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_LookupPurchaseOrder1 = New Winforms.Xl_LookupPurchaseOrder()
        Me.Xl_LookupDelivery1 = New Winforms.Xl_LookupDelivery()
        Me.Xl_LookupCountry1 = New Winforms.Xl_LookupCountry()
        Me.Xl_RecallProducts1 = New Winforms.Xl_RecallProducts()
        Me.CheckBoxPdc = New System.Windows.Forms.CheckBox()
        Me.CheckBoxAlb = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_RecallProducts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 561)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(541, 31)
        Me.Panel1.TabIndex = 62
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(322, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(433, 4)
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
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(541, 24)
        Me.MenuStrip1.TabIndex = 64
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem, Me.ComandaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'ComandaToolStripMenuItem
        '
        Me.ComandaToolStripMenuItem.Name = "ComandaToolStripMenuItem"
        Me.ComandaToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ComandaToolStripMenuItem.Text = "comanda"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Recall"
        '
        'TextBoxRecall
        '
        Me.TextBoxRecall.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRecall.Location = New System.Drawing.Point(89, 31)
        Me.TextBoxRecall.Name = "TextBoxRecall"
        Me.TextBoxRecall.ReadOnly = True
        Me.TextBoxRecall.Size = New System.Drawing.Size(448, 20)
        Me.TextBoxRecall.TabIndex = 66
        '
        'TextBoxContactNom
        '
        Me.TextBoxContactNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactNom.Location = New System.Drawing.Point(89, 85)
        Me.TextBoxContactNom.Name = "TextBoxContactNom"
        Me.TextBoxContactNom.Size = New System.Drawing.Size(448, 20)
        Me.TextBoxContactNom.TabIndex = 67
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "Contacte"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "Email"
        '
        'TextBoxContactEmail
        '
        Me.TextBoxContactEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactEmail.Location = New System.Drawing.Point(89, 111)
        Me.TextBoxContactEmail.Name = "TextBoxContactEmail"
        Me.TextBoxContactEmail.Size = New System.Drawing.Size(448, 20)
        Me.TextBoxContactEmail.TabIndex = 69
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 72
        Me.Label4.Text = "Telefon"
        '
        'TextBoxContactTel
        '
        Me.TextBoxContactTel.Location = New System.Drawing.Point(89, 137)
        Me.TextBoxContactTel.Name = "TextBoxContactTel"
        Me.TextBoxContactTel.Size = New System.Drawing.Size(120, 20)
        Me.TextBoxContactTel.TabIndex = 71
        '
        'TextBoxCustomer
        '
        Me.TextBoxCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCustomer.Location = New System.Drawing.Point(89, 163)
        Me.TextBoxCustomer.Name = "TextBoxCustomer"
        Me.TextBoxCustomer.ReadOnly = True
        Me.TextBoxCustomer.Size = New System.Drawing.Size(448, 20)
        Me.TextBoxCustomer.TabIndex = 74
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Client"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 192)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 76
        Me.Label6.Text = "Adreça"
        '
        'TextBoxAddress
        '
        Me.TextBoxAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAddress.Location = New System.Drawing.Point(89, 189)
        Me.TextBoxAddress.Name = "TextBoxAddress"
        Me.TextBoxAddress.Size = New System.Drawing.Size(448, 20)
        Me.TextBoxAddress.TabIndex = 75
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 218)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 78
        Me.Label7.Text = "Codi postal"
        '
        'TextBoxZip
        '
        Me.TextBoxZip.Location = New System.Drawing.Point(89, 215)
        Me.TextBoxZip.Name = "TextBoxZip"
        Me.TextBoxZip.Size = New System.Drawing.Size(120, 20)
        Me.TextBoxZip.TabIndex = 77
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 246)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "Pais"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 351)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 82
        Me.Label9.Text = "Albarà"
        '
        'TextBoxFchVivace
        '
        Me.TextBoxFchVivace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFchVivace.Location = New System.Drawing.Point(110, 270)
        Me.TextBoxFchVivace.Name = "TextBoxFchVivace"
        Me.TextBoxFchVivace.ReadOnly = True
        Me.TextBoxFchVivace.Size = New System.Drawing.Size(152, 20)
        Me.TextBoxFchVivace.TabIndex = 84
        Me.TextBoxFchVivace.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 273)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 83
        Me.Label10.Text = "Avis a Vivace"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(214, 218)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 86
        Me.Label11.Text = "Població"
        '
        'TextBoxLocation
        '
        Me.TextBoxLocation.Location = New System.Drawing.Point(268, 215)
        Me.TextBoxLocation.Name = "TextBoxLocation"
        Me.TextBoxLocation.Size = New System.Drawing.Size(269, 20)
        Me.TextBoxLocation.TabIndex = 85
        '
        'TextBoxFchCreated
        '
        Me.TextBoxFchCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFchCreated.Location = New System.Drawing.Point(89, 57)
        Me.TextBoxFchCreated.Name = "TextBoxFchCreated"
        Me.TextBoxFchCreated.ReadOnly = True
        Me.TextBoxFchCreated.Size = New System.Drawing.Size(120, 20)
        Me.TextBoxFchCreated.TabIndex = 88
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 60)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 87
        Me.Label12.Text = "Registre"
        '
        'CheckBoxFchVivace
        '
        Me.CheckBoxFchVivace.AutoSize = True
        Me.CheckBoxFchVivace.Location = New System.Drawing.Point(89, 273)
        Me.CheckBoxFchVivace.Name = "CheckBoxFchVivace"
        Me.CheckBoxFchVivace.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxFchVivace.TabIndex = 89
        Me.CheckBoxFchVivace.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 298)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "Reg.muelle"
        '
        'TextBoxRegMuelle
        '
        Me.TextBoxRegMuelle.Location = New System.Drawing.Point(110, 295)
        Me.TextBoxRegMuelle.MaxLength = 12
        Me.TextBoxRegMuelle.Name = "TextBoxRegMuelle"
        Me.TextBoxRegMuelle.Size = New System.Drawing.Size(152, 20)
        Me.TextBoxRegMuelle.TabIndex = 91
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 325)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 13)
        Me.Label14.TabIndex = 93
        Me.Label14.Text = "Comanda"
        '
        'Xl_LookupPurchaseOrder1
        '
        Me.Xl_LookupPurchaseOrder1.IsDirty = False
        Me.Xl_LookupPurchaseOrder1.Location = New System.Drawing.Point(110, 322)
        Me.Xl_LookupPurchaseOrder1.Name = "Xl_LookupPurchaseOrder1"
        Me.Xl_LookupPurchaseOrder1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPurchaseOrder1.PurchaseOrder = Nothing
        Me.Xl_LookupPurchaseOrder1.Size = New System.Drawing.Size(188, 20)
        Me.Xl_LookupPurchaseOrder1.TabIndex = 92
        Me.Xl_LookupPurchaseOrder1.Value = Nothing
        Me.Xl_LookupPurchaseOrder1.Visible = False
        '
        'Xl_LookupDelivery1
        '
        Me.Xl_LookupDelivery1.Delivery = Nothing
        Me.Xl_LookupDelivery1.IsDirty = False
        Me.Xl_LookupDelivery1.Location = New System.Drawing.Point(110, 348)
        Me.Xl_LookupDelivery1.Name = "Xl_LookupDelivery1"
        Me.Xl_LookupDelivery1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupDelivery1.Size = New System.Drawing.Size(188, 20)
        Me.Xl_LookupDelivery1.TabIndex = 81
        Me.Xl_LookupDelivery1.Value = Nothing
        Me.Xl_LookupDelivery1.Visible = False
        '
        'Xl_LookupCountry1
        '
        Me.Xl_LookupCountry1.Country = Nothing
        Me.Xl_LookupCountry1.IsDirty = False
        Me.Xl_LookupCountry1.Location = New System.Drawing.Point(89, 242)
        Me.Xl_LookupCountry1.Name = "Xl_LookupCountry1"
        Me.Xl_LookupCountry1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCountry1.Size = New System.Drawing.Size(209, 20)
        Me.Xl_LookupCountry1.TabIndex = 79
        Me.Xl_LookupCountry1.Value = Nothing
        '
        'Xl_RecallProducts1
        '
        Me.Xl_RecallProducts1.AllowUserToAddRows = False
        Me.Xl_RecallProducts1.AllowUserToDeleteRows = False
        Me.Xl_RecallProducts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RecallProducts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RecallProducts1.DisplayObsolets = False
        Me.Xl_RecallProducts1.Location = New System.Drawing.Point(0, 381)
        Me.Xl_RecallProducts1.MouseIsDown = False
        Me.Xl_RecallProducts1.Name = "Xl_RecallProducts1"
        Me.Xl_RecallProducts1.ReadOnly = True
        Me.Xl_RecallProducts1.Size = New System.Drawing.Size(537, 178)
        Me.Xl_RecallProducts1.TabIndex = 63
        '
        'CheckBoxPdc
        '
        Me.CheckBoxPdc.AutoSize = True
        Me.CheckBoxPdc.Location = New System.Drawing.Point(89, 325)
        Me.CheckBoxPdc.Name = "CheckBoxPdc"
        Me.CheckBoxPdc.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxPdc.TabIndex = 94
        Me.CheckBoxPdc.UseVisualStyleBackColor = True
        '
        'CheckBoxAlb
        '
        Me.CheckBoxAlb.AutoSize = True
        Me.CheckBoxAlb.Location = New System.Drawing.Point(89, 351)
        Me.CheckBoxAlb.Name = "CheckBoxAlb"
        Me.CheckBoxAlb.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxAlb.TabIndex = 95
        Me.CheckBoxAlb.UseVisualStyleBackColor = True
        '
        'Frm_RecallCli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 592)
        Me.Controls.Add(Me.CheckBoxAlb)
        Me.Controls.Add(Me.CheckBoxPdc)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Xl_LookupPurchaseOrder1)
        Me.Controls.Add(Me.TextBoxRegMuelle)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.CheckBoxFchVivace)
        Me.Controls.Add(Me.TextBoxFchCreated)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TextBoxLocation)
        Me.Controls.Add(Me.TextBoxFchVivace)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Xl_LookupDelivery1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Xl_LookupCountry1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxZip)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxAddress)
        Me.Controls.Add(Me.TextBoxCustomer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxContactTel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxContactEmail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxContactNom)
        Me.Controls.Add(Me.TextBoxRecall)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_RecallProducts1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_RecallCli"
        Me.Text = "Registre de recall"
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_RecallProducts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_RecallProducts1 As Xl_RecallProducts
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxRecall As TextBox
    Friend WithEvents TextBoxContactNom As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxContactEmail As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxContactTel As TextBox
    Friend WithEvents TextBoxCustomer As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxAddress As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxZip As TextBox
    Friend WithEvents Xl_LookupCountry1 As Xl_LookupCountry
    Friend WithEvents Label8 As Label
    Friend WithEvents Xl_LookupDelivery1 As Xl_LookupDelivery
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxFchVivace As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBoxLocation As TextBox
    Friend WithEvents TextBoxFchCreated As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents CheckBoxFchVivace As CheckBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBoxRegMuelle As TextBox
    Friend WithEvents Xl_LookupPurchaseOrder1 As Xl_LookupPurchaseOrder
    Friend WithEvents Label14 As Label
    Friend WithEvents ComandaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckBoxPdc As CheckBox
    Friend WithEvents CheckBoxAlb As CheckBox
End Class
