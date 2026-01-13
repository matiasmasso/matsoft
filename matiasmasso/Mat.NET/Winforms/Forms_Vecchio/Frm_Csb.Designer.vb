Public Partial Class Frm_Csb
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Csb))
        Me.TextBoxCsa = New System.Windows.Forms.TextBox()
        Me.ContextMenuStripCsa = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBoxVtoCcaBrowse = New System.Windows.Forms.PictureBox()
        Me.TextBoxCcaVto = New System.Windows.Forms.TextBox()
        Me.ButtonCcaVto = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEur = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxVto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.ContextMenuStripClient = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PictureBoxIBAN = New System.Windows.Forms.PictureBox()
        Me.PictureBoxBancLogo = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ReclamarToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.RebutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonImpagat = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBoxVtoCcaBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxIBAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxCsa
        '
        Me.TextBoxCsa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCsa.ContextMenuStrip = Me.ContextMenuStripCsa
        Me.TextBoxCsa.Location = New System.Drawing.Point(33, 104)
        Me.TextBoxCsa.Name = "TextBoxCsa"
        Me.TextBoxCsa.ReadOnly = True
        Me.TextBoxCsa.Size = New System.Drawing.Size(346, 20)
        Me.TextBoxCsa.TabIndex = 4
        '
        'ContextMenuStripCsa
        '
        Me.ContextMenuStripCsa.AllowDrop = True
        Me.ContextMenuStripCsa.Name = "ContextMenuStripCsa"
        Me.ContextMenuStripCsa.Size = New System.Drawing.Size(61, 4)
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.PictureBoxVtoCcaBrowse)
        Me.GroupBox2.Controls.Add(Me.TextBoxCcaVto)
        Me.GroupBox2.Controls.Add(Me.ButtonCcaVto)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TextBoxTxt)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.TextBoxEur)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TextBoxVto)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TextBoxCliNom)
        Me.GroupBox2.Controls.Add(Me.PictureBoxIBAN)
        Me.GroupBox2.Location = New System.Drawing.Point(33, 131)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(361, 250)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "efecte"
        '
        'PictureBoxVtoCcaBrowse
        '
        Me.PictureBoxVtoCcaBrowse.Image = CType(resources.GetObject("PictureBoxVtoCcaBrowse.Image"), System.Drawing.Image)
        Me.PictureBoxVtoCcaBrowse.Location = New System.Drawing.Point(299, 47)
        Me.PictureBoxVtoCcaBrowse.Name = "PictureBoxVtoCcaBrowse"
        Me.PictureBoxVtoCcaBrowse.Size = New System.Drawing.Size(20, 20)
        Me.PictureBoxVtoCcaBrowse.TabIndex = 16
        Me.PictureBoxVtoCcaBrowse.TabStop = False
        '
        'TextBoxCcaVto
        '
        Me.TextBoxCcaVto.Location = New System.Drawing.Point(183, 47)
        Me.TextBoxCcaVto.Name = "TextBoxCcaVto"
        Me.TextBoxCcaVto.ReadOnly = True
        Me.TextBoxCcaVto.Size = New System.Drawing.Size(113, 20)
        Me.TextBoxCcaVto.TabIndex = 15
        '
        'ButtonCcaVto
        '
        Me.ButtonCcaVto.Enabled = False
        Me.ButtonCcaVto.Location = New System.Drawing.Point(323, 47)
        Me.ButtonCcaVto.Name = "ButtonCcaVto"
        Me.ButtonCcaVto.Size = New System.Drawing.Size(24, 20)
        Me.ButtonCcaVto.TabIndex = 14
        Me.ButtonCcaVto.Text = "..."
        Me.ButtonCcaVto.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 188)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "status:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(97, 185)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(156, 20)
        Me.TextBox1.TabIndex = 12
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
        Me.TextBoxCliNom.ContextMenuStrip = Me.ContextMenuStripClient
        Me.TextBoxCliNom.Location = New System.Drawing.Point(96, 20)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxCliNom.TabIndex = 3
        '
        'ContextMenuStripClient
        '
        Me.ContextMenuStripClient.AllowDrop = True
        Me.ContextMenuStripClient.Name = "ContextMenuStripCsa"
        Me.ContextMenuStripClient.Size = New System.Drawing.Size(61, 4)
        '
        'PictureBoxIBAN
        '
        Me.PictureBoxIBAN.Location = New System.Drawing.Point(96, 128)
        Me.PictureBoxIBAN.Name = "PictureBoxIBAN"
        Me.PictureBoxIBAN.Size = New System.Drawing.Size(250, 50)
        Me.PictureBoxIBAN.TabIndex = 0
        Me.PictureBoxIBAN.TabStop = False
        '
        'PictureBoxBancLogo
        '
        Me.PictureBoxBancLogo.Location = New System.Drawing.Point(230, 38)
        Me.PictureBoxBancLogo.Name = "PictureBoxBancLogo"
        Me.PictureBoxBancLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxBancLogo.TabIndex = 10
        Me.PictureBoxBancLogo.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReclamarToolStripButton, Me.RebutToolStripButton, Me.ToolStripButtonImpagat})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(406, 25)
        Me.ToolStrip1.TabIndex = 11
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ReclamarToolStripButton
        '
        Me.ReclamarToolStripButton.Image = Global.Winforms.My.Resources.Resources.REDO
        Me.ReclamarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ReclamarToolStripButton.Name = "ReclamarToolStripButton"
        Me.ReclamarToolStripButton.Size = New System.Drawing.Size(76, 22)
        Me.ReclamarToolStripButton.Text = "Reclamar"
        '
        'RebutToolStripButton
        '
        Me.RebutToolStripButton.Image = Global.Winforms.My.Resources.Resources.pdf
        Me.RebutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RebutToolStripButton.Name = "RebutToolStripButton"
        Me.RebutToolStripButton.Size = New System.Drawing.Size(58, 22)
        Me.RebutToolStripButton.Text = "Rebut"
        '
        'ToolStripButtonImpagat
        '
        Me.ToolStripButtonImpagat.Image = Global.Winforms.My.Resources.Resources.pirata_rojo
        Me.ToolStripButtonImpagat.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonImpagat.Name = "ToolStripButtonImpagat"
        Me.ToolStripButtonImpagat.Size = New System.Drawing.Size(71, 22)
        Me.ToolStripButtonImpagat.Text = "Impagat"
        '
        'Frm_Csb
        '
        Me.ClientSize = New System.Drawing.Size(406, 393)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.PictureBoxBancLogo)
        Me.Controls.Add(Me.TextBoxCsa)
        Me.Controls.Add(Me.GroupBox2)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "Frm_Csb"
        Me.Text = "EFECTE"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBoxVtoCcaBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxIBAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxCsa As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEur As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxVto As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxIBAN As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStripCsa As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuStripClient As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PictureBoxBancLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ReclamarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RebutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonImpagat As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBoxCcaVto As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCcaVto As System.Windows.Forms.Button
    Friend WithEvents PictureBoxVtoCcaBrowse As System.Windows.Forms.PictureBox
End Class
