<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EdiversaFile
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxDocNum = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxAmt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxIOCod = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxFchCreated = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxFilename = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxReceiverNom = New System.Windows.Forms.TextBox()
        Me.TextBoxReceiverEan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxSenderNom = New System.Windows.Forms.TextBox()
        Me.TextBoxSenderEan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTag = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TabPageExceptions = New System.Windows.Forms.TabPage()
        Me.Xl_EdiversaExceptions1 = New Winforms.Xl_EdiversaExceptions()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageExceptions.SuspendLayout()
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 455)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(435, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(216, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(327, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPageExceptions)
        Me.TabControl1.Location = New System.Drawing.Point(2, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(431, 414)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxFch)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.TextBoxDocNum)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxAmt)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.TextBoxResult)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxIOCod)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxFchCreated)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxFilename)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxReceiverNom)
        Me.TabPage1.Controls.Add(Me.TextBoxReceiverEan)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxSenderNom)
        Me.TabPage1.Controls.Add(Me.TextBoxSenderEan)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxTag)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(423, 388)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(94, 149)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxFch.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Data:"
        '
        'TextBoxDocNum
        '
        Me.TextBoxDocNum.Location = New System.Drawing.Point(94, 123)
        Me.TextBoxDocNum.Name = "TextBoxDocNum"
        Me.TextBoxDocNum.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxDocNum.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 126)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Document:"
        '
        'TextBoxAmt
        '
        Me.TextBoxAmt.Location = New System.Drawing.Point(94, 97)
        Me.TextBoxAmt.Name = "TextBoxAmt"
        Me.TextBoxAmt.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxAmt.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Import:"
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResult.Location = New System.Drawing.Point(94, 267)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxResult.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 270)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Status:"
        '
        'TextBoxIOCod
        '
        Me.TextBoxIOCod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxIOCod.Location = New System.Drawing.Point(94, 241)
        Me.TextBoxIOCod.Name = "TextBoxIOCod"
        Me.TextBoxIOCod.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxIOCod.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Safata:"
        '
        'TextBoxFchCreated
        '
        Me.TextBoxFchCreated.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFchCreated.Location = New System.Drawing.Point(94, 215)
        Me.TextBoxFchCreated.Name = "TextBoxFchCreated"
        Me.TextBoxFchCreated.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxFchCreated.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Hora recepció:"
        '
        'TextBoxFilename
        '
        Me.TextBoxFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFilename.Location = New System.Drawing.Point(94, 189)
        Me.TextBoxFilename.Name = "TextBoxFilename"
        Me.TextBoxFilename.Size = New System.Drawing.Size(313, 20)
        Me.TextBoxFilename.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Fitxer:"
        '
        'TextBoxReceiverNom
        '
        Me.TextBoxReceiverNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxReceiverNom.Location = New System.Drawing.Point(193, 70)
        Me.TextBoxReceiverNom.Name = "TextBoxReceiverNom"
        Me.TextBoxReceiverNom.Size = New System.Drawing.Size(214, 20)
        Me.TextBoxReceiverNom.TabIndex = 7
        '
        'TextBoxReceiverEan
        '
        Me.TextBoxReceiverEan.Location = New System.Drawing.Point(94, 70)
        Me.TextBoxReceiverEan.Name = "TextBoxReceiverEan"
        Me.TextBoxReceiverEan.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxReceiverEan.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Destinatari:"
        '
        'TextBoxSenderNom
        '
        Me.TextBoxSenderNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSenderNom.Location = New System.Drawing.Point(193, 44)
        Me.TextBoxSenderNom.Name = "TextBoxSenderNom"
        Me.TextBoxSenderNom.Size = New System.Drawing.Size(214, 20)
        Me.TextBoxSenderNom.TabIndex = 4
        '
        'TextBoxSenderEan
        '
        Me.TextBoxSenderEan.Location = New System.Drawing.Point(94, 44)
        Me.TextBoxSenderEan.Name = "TextBoxSenderEan"
        Me.TextBoxSenderEan.Size = New System.Drawing.Size(93, 20)
        Me.TextBoxSenderEan.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Remitent:"
        '
        'TextBoxTag
        '
        Me.TextBoxTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTag.Location = New System.Drawing.Point(94, 18)
        Me.TextBoxTag.Name = "TextBoxTag"
        Me.TextBoxTag.Size = New System.Drawing.Size(313, 20)
        Me.TextBoxTag.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Missatge:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(423, 388)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Codi"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(417, 382)
        Me.DataGridView1.TabIndex = 43
        '
        'TabPageExceptions
        '
        Me.TabPageExceptions.Controls.Add(Me.Xl_EdiversaExceptions1)
        Me.TabPageExceptions.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExceptions.Name = "TabPageExceptions"
        Me.TabPageExceptions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExceptions.Size = New System.Drawing.Size(423, 388)
        Me.TabPageExceptions.TabIndex = 2
        Me.TabPageExceptions.Text = "Errors"
        Me.TabPageExceptions.UseVisualStyleBackColor = True
        '
        'Xl_EdiversaExceptions1
        '
        Me.Xl_EdiversaExceptions1.AllowUserToAddRows = False
        Me.Xl_EdiversaExceptions1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaExceptions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaExceptions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaExceptions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_EdiversaExceptions1.Name = "Xl_EdiversaExceptions1"
        Me.Xl_EdiversaExceptions1.ReadOnly = True
        Me.Xl_EdiversaExceptions1.Size = New System.Drawing.Size(417, 382)
        Me.Xl_EdiversaExceptions1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(435, 24)
        Me.MenuStrip1.TabIndex = 44
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestoreToolStripMenuItem, Me.ImportaToolStripMenuItem, Me.ExportaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RestoreToolStripMenuItem
        '
        Me.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem"
        Me.RestoreToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestoreToolStripMenuItem.Text = "Restaura"
        '
        'ExportaToolStripMenuItem
        '
        Me.ExportaToolStripMenuItem.Name = "ExportaToolStripMenuItem"
        Me.ExportaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExportaToolStripMenuItem.Text = "Exporta"
        '
        'ImportaToolStripMenuItem
        '
        Me.ImportaToolStripMenuItem.Name = "ImportaToolStripMenuItem"
        Me.ImportaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ImportaToolStripMenuItem.Text = "Importa"
        '
        'Frm_EdiversaFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 486)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_EdiversaFile"
        Me.Text = "Fitxer Edi"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageExceptions.ResumeLayout(False)
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxResult As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxIOCod As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxFchCreated As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxFilename As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxReceiverNom As TextBox
    Friend WithEvents TextBoxReceiverEan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxSenderNom As TextBox
    Friend WithEvents TextBoxSenderEan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxTag As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBoxAmt As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBoxDocNum As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TabPageExceptions As TabPage
    Friend WithEvents Xl_EdiversaExceptions1 As Xl_EdiversaExceptions
    Friend WithEvents ExportaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportaToolStripMenuItem As ToolStripMenuItem
End Class
