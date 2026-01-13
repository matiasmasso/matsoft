<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Csb
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
        Me.components = New System.ComponentModel.Container()
        Me.TextBoxCsa = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxMandato = New System.Windows.Forms.TextBox()
        Me.TextBoxResultCca = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEur = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxVto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.ContextMenuStripCsa = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStripClient = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_MailingLogs1 = New Winforms.Xl_MailingLogs()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_MailingLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxCsa
        '
        Me.TextBoxCsa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCsa.Location = New System.Drawing.Point(19, 48)
        Me.TextBoxCsa.Name = "TextBoxCsa"
        Me.TextBoxCsa.ReadOnly = True
        Me.TextBoxCsa.Size = New System.Drawing.Size(358, 20)
        Me.TextBoxCsa.TabIndex = 12
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.TextBoxMandato)
        Me.GroupBox2.Controls.Add(Me.TextBoxResultCca)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Xl_Iban1)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBoxResult)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TextBoxTxt)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.TextBoxEur)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TextBoxVto)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TextBoxCliNom)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 93)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(358, 293)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "efecte"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 187)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "mandato:"
        '
        'TextBoxMandato
        '
        Me.TextBoxMandato.Location = New System.Drawing.Point(98, 184)
        Me.TextBoxMandato.Name = "TextBoxMandato"
        Me.TextBoxMandato.ReadOnly = True
        Me.TextBoxMandato.Size = New System.Drawing.Size(248, 20)
        Me.TextBoxMandato.TabIndex = 20
        '
        'TextBoxResultCca
        '
        Me.TextBoxResultCca.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResultCca.Location = New System.Drawing.Point(97, 261)
        Me.TextBoxResultCca.Name = "TextBoxResultCca"
        Me.TextBoxResultCca.ReadOnly = True
        Me.TextBoxResultCca.Size = New System.Drawing.Size(248, 20)
        Me.TextBoxResultCca.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "resultat:"
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Location = New System.Drawing.Point(97, 128)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 235)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "status:"
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Location = New System.Drawing.Point(98, 232)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.ReadOnly = True
        Me.TextBoxResult.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxResult.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "domicliliació:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "concepte:"
        '
        'TextBoxTxt
        '
        Me.TextBoxTxt.Location = New System.Drawing.Point(97, 101)
        Me.TextBoxTxt.Name = "TextBoxTxt"
        Me.TextBoxTxt.ReadOnly = True
        Me.TextBoxTxt.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxTxt.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "import:"
        '
        'TextBoxEur
        '
        Me.TextBoxEur.Location = New System.Drawing.Point(97, 74)
        Me.TextBoxEur.Name = "TextBoxEur"
        Me.TextBoxEur.ReadOnly = True
        Me.TextBoxEur.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxEur.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "venciment:"
        '
        'TextBoxVto
        '
        Me.TextBoxVto.Location = New System.Drawing.Point(96, 47)
        Me.TextBoxVto.Name = "TextBoxVto"
        Me.TextBoxVto.ReadOnly = True
        Me.TextBoxVto.Size = New System.Drawing.Size(81, 20)
        Me.TextBoxVto.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "lliurat:"
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Location = New System.Drawing.Point(96, 20)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxCliNom.TabIndex = 3
        '
        'ContextMenuStripCsa
        '
        Me.ContextMenuStripCsa.AllowDrop = True
        Me.ContextMenuStripCsa.Name = "ContextMenuStripCsa"
        Me.ContextMenuStripCsa.Size = New System.Drawing.Size(61, 4)
        '
        'ContextMenuStripClient
        '
        Me.ContextMenuStripClient.AllowDrop = True
        Me.ContextMenuStripClient.Name = "ContextMenuStripCsa"
        Me.ContextMenuStripClient.Size = New System.Drawing.Size(61, 4)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(403, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(403, 418)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxCsa)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(395, 392)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_MailingLogs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(395, 392)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Avisos de venciment"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "remesa:"
        '
        'Xl_MailingLogs1
        '
        Me.Xl_MailingLogs1.AllowUserToAddRows = False
        Me.Xl_MailingLogs1.AllowUserToDeleteRows = False
        Me.Xl_MailingLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_MailingLogs1.DisplayObsolets = False
        Me.Xl_MailingLogs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_MailingLogs1.Filter = Nothing
        Me.Xl_MailingLogs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_MailingLogs1.MouseIsDown = False
        Me.Xl_MailingLogs1.Name = "Xl_MailingLogs1"
        Me.Xl_MailingLogs1.ReadOnly = True
        Me.Xl_MailingLogs1.Size = New System.Drawing.Size(389, 386)
        Me.Xl_MailingLogs1.TabIndex = 0
        '
        'Frm_Csb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 445)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Csb"
        Me.Text = "Efecte presentat al banc"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_MailingLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxCsa As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxResult As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxTxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxEur As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxVto As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxCliNom As TextBox
    Friend WithEvents ContextMenuStripCsa As ContextMenuStrip
    Friend WithEvents ContextMenuStripClient As ContextMenuStrip
    Friend WithEvents Xl_Iban1 As Xl_Iban
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBoxResultCca As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxMandato As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label9 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_MailingLogs1 As Xl_MailingLogs
End Class
