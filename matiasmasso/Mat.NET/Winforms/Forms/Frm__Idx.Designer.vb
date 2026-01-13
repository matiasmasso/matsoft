<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm__Idx
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm__Idx))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmpresaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseLocalApiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerfilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CredencialsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesconectarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancellarDemoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncidenciesInformatiquesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssistenciaRemotaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PanelSplashScreen = New System.Windows.Forms.Panel()
        Me.LabelLaunchStatus = New System.Windows.Forms.Label()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.Xl_WinMenuTree1 = New Winforms.Xl_WinMenuTree()
        Me.Xl_Contact1 = New Winforms.Xl_Contact2()
        Me.Xl_DropFile1 = New Winforms.Xl_DropFile()
        Me.LangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSplashScreen.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_WinMenuTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Size = New System.Drawing.Size(584, 321)
        Me.SplitContainer1.SplitterDistance = 134
        Me.SplitContainer1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(160, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FakeControl"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AllowDrop = True
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.PerfilToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(584, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmpresaToolStripMenuItem, Me.ImportarToolStripMenuItem, Me.RefrescaMenuToolStripMenuItem, Me.UseLocalApiToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.MenuToolStripMenuItem.Text = "menú"
        '
        'EmpresaToolStripMenuItem
        '
        Me.EmpresaToolStripMenuItem.Name = "EmpresaToolStripMenuItem"
        Me.EmpresaToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.EmpresaToolStripMenuItem.Text = "empresa"
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.ImportarToolStripMenuItem.Text = "importar"
        '
        'RefrescaMenuToolStripMenuItem
        '
        Me.RefrescaMenuToolStripMenuItem.Name = "RefrescaMenuToolStripMenuItem"
        Me.RefrescaMenuToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.RefrescaMenuToolStripMenuItem.Text = "refresca menu"
        '
        'UseLocalApiToolStripMenuItem
        '
        Me.UseLocalApiToolStripMenuItem.CheckOnClick = True
        Me.UseLocalApiToolStripMenuItem.Name = "UseLocalApiToolStripMenuItem"
        Me.UseLocalApiToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.UseLocalApiToolStripMenuItem.Text = "use Local Api"
        '
        'PerfilToolStripMenuItem
        '
        Me.PerfilToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LangToolStripMenuItem, Me.CredencialsToolStripMenuItem, Me.DesconectarToolStripMenuItem, Me.CancellarDemoToolStripMenuItem})
        Me.PerfilToolStripMenuItem.Name = "PerfilToolStripMenuItem"
        Me.PerfilToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.PerfilToolStripMenuItem.Text = "perfil"
        '
        'CredencialsToolStripMenuItem
        '
        Me.CredencialsToolStripMenuItem.Name = "CredencialsToolStripMenuItem"
        Me.CredencialsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CredencialsToolStripMenuItem.Text = "credencials"
        '
        'DesconectarToolStripMenuItem
        '
        Me.DesconectarToolStripMenuItem.Name = "DesconectarToolStripMenuItem"
        Me.DesconectarToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DesconectarToolStripMenuItem.Text = "desconectar"
        '
        'CancellarDemoToolStripMenuItem
        '
        Me.CancellarDemoToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.CancellarDemoToolStripMenuItem.ForeColor = System.Drawing.Color.Red
        Me.CancellarDemoToolStripMenuItem.Name = "CancellarDemoToolStripMenuItem"
        Me.CancellarDemoToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CancellarDemoToolStripMenuItem.Text = "cancel·lar demo"
        Me.CancellarDemoToolStripMenuItem.Visible = False
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.IncidenciesInformatiquesToolStripMenuItem, Me.AssistenciaRemotaToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.HelpToolStripMenuItem.Text = "ajuda"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.AboutToolStripMenuItem.Text = "about"
        '
        'IncidenciesInformatiquesToolStripMenuItem
        '
        Me.IncidenciesInformatiquesToolStripMenuItem.Name = "IncidenciesInformatiquesToolStripMenuItem"
        Me.IncidenciesInformatiquesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.IncidenciesInformatiquesToolStripMenuItem.Text = "incidencies informatiques"
        '
        'AssistenciaRemotaToolStripMenuItem
        '
        Me.AssistenciaRemotaToolStripMenuItem.Name = "AssistenciaRemotaToolStripMenuItem"
        Me.AssistenciaRemotaToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.AssistenciaRemotaToolStripMenuItem.Text = "assistencia remota"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 26)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(17, 18)
        Me.PictureBox1.TabIndex = 46
        Me.PictureBox1.TabStop = False
        '
        'PanelSplashScreen
        '
        Me.PanelSplashScreen.BackColor = System.Drawing.SystemColors.Control
        Me.PanelSplashScreen.Controls.Add(Me.LabelLaunchStatus)
        Me.PanelSplashScreen.Controls.Add(Me.LabelUser)
        Me.PanelSplashScreen.Controls.Add(Me.Label4)
        Me.PanelSplashScreen.Controls.Add(Me.Label3)
        Me.PanelSplashScreen.Controls.Add(Me.Label2)
        Me.PanelSplashScreen.Controls.Add(Me.PictureBox2)
        Me.PanelSplashScreen.Controls.Add(Me.LabelVersion)
        Me.PanelSplashScreen.Controls.Add(Me.ProgressBar1)
        Me.PanelSplashScreen.Location = New System.Drawing.Point(246, 12)
        Me.PanelSplashScreen.Name = "PanelSplashScreen"
        Me.PanelSplashScreen.Size = New System.Drawing.Size(400, 171)
        Me.PanelSplashScreen.TabIndex = 1
        '
        'LabelLaunchStatus
        '
        Me.LabelLaunchStatus.AutoSize = True
        Me.LabelLaunchStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelLaunchStatus.Location = New System.Drawing.Point(90, 129)
        Me.LabelLaunchStatus.Name = "LabelLaunchStatus"
        Me.LabelLaunchStatus.Size = New System.Drawing.Size(20, 17)
        Me.LabelLaunchStatus.TabIndex = 7
        Me.LabelLaunchStatus.Text = "..."
        '
        'LabelUser
        '
        Me.LabelUser.AutoSize = True
        Me.LabelUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUser.Location = New System.Drawing.Point(90, 110)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(57, 17)
        Me.LabelUser.TabIndex = 6
        Me.LabelUser.Text = "Usuari: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label4.Location = New System.Drawing.Point(90, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 17)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "www.matiasmasso.es"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(90, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(217, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Diagonal 403 - 08008 Barcelona "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(89, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(222, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "MATIAS MASSO, S.A. "
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(17, 13)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(61, 63)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVersion.Location = New System.Drawing.Point(90, 90)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(107, 17)
        Me.LabelVersion.TabIndex = 1
        Me.LabelVersion.Text = "Mat.Net versió: "
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 157)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(400, 14)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.ProgressBar2)
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(584, 344)
        Me.Panel1.TabIndex = 47
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar2.Location = New System.Drawing.Point(0, 321)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(584, 23)
        Me.ProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar2.TabIndex = 2
        Me.ProgressBar2.Visible = False
        '
        'Xl_WinMenuTree1
        '
        Me.Xl_WinMenuTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WinMenuTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WinMenuTree1.Name = "Xl_WinMenuTree1"
        Me.Xl_WinMenuTree1.Size = New System.Drawing.Size(134, 321)
        Me.Xl_WinMenuTree1.TabIndex = 0
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Emp = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(24, 25)
        Me.Xl_Contact1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(498, 20)
        Me.Xl_Contact1.TabIndex = 0
        '
        'Xl_DropFile1
        '
        Me.Xl_DropFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DropFile1.BackColor = System.Drawing.SystemColors.Control
        Me.Xl_DropFile1.Location = New System.Drawing.Point(533, 0)
        Me.Xl_DropFile1.Name = "Xl_DropFile1"
        Me.Xl_DropFile1.Size = New System.Drawing.Size(48, 48)
        Me.Xl_DropFile1.TabIndex = 45
        '
        'LangToolStripMenuItem
        '
        Me.LangToolStripMenuItem.Name = "LangToolStripMenuItem"
        Me.LangToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LangToolStripMenuItem.Text = "idioma"
        '
        'Frm__Idx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 397)
        Me.Controls.Add(Me.PanelSplashScreen)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.Xl_DropFile1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MinimumSize = New System.Drawing.Size(347, 221)
        Me.Name = "Frm__Idx"
        Me.Text = "Mat.Net"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSplashScreen.ResumeLayout(False)
        Me.PanelSplashScreen.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_WinMenuTree1 As Winforms.Xl_WinMenuTree
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmpresaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PerfilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesconectarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_DropFile1 As Winforms.Xl_DropFile
    Friend WithEvents Xl_Contact1 As Winforms.Xl_Contact2
    Friend WithEvents CredencialsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CancellarDemoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents IncidenciesInformatiquesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AssistenciaRemotaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaMenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UseLocalApiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PanelSplashScreen As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents LabelVersion As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelUser As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents LabelLaunchStatus As Label
    Friend WithEvents LangToolStripMenuItem As ToolStripMenuItem
End Class
