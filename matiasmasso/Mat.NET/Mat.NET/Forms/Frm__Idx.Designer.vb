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
        Me.Xl_WinMenuTree1 = New Mat.NET.Xl_WinMenuTree()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_WinMenuListView1 = New Mat.NET.Xl_WinMenuListView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenúToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServidorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmpresaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeveloperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerfilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesconectarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Xl_DropFile1 = New Mat.NET.Xl_DropFile()
        Me.Xl_Contact1 = New Mat.NET.Xl_Contact()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_WinMenuTree1
        '
        Me.Xl_WinMenuTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WinMenuTree1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WinMenuTree1.Name = "Xl_WinMenuTree1"
        Me.Xl_WinMenuTree1.Size = New System.Drawing.Size(168, 413)
        Me.Xl_WinMenuTree1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 46)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_WinMenuTree1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_WinMenuListView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(610, 413)
        Me.SplitContainer1.SplitterDistance = 168
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_WinMenuListView1
        '
        Me.Xl_WinMenuListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WinMenuListView1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WinMenuListView1.Name = "Xl_WinMenuListView1"
        Me.Xl_WinMenuListView1.Size = New System.Drawing.Size(438, 413)
        Me.Xl_WinMenuListView1.TabIndex = 0
        Me.Xl_WinMenuListView1.UseCompatibleStateImageBehavior = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AllowDrop = True
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenúToolStripMenuItem, Me.PerfilToolStripMenuItem, Me.AjudaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(611, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenúToolStripMenuItem
        '
        Me.MenúToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServidorToolStripMenuItem, Me.EmpresaToolStripMenuItem, Me.ImportarToolStripMenuItem, Me.DeveloperToolStripMenuItem})
        Me.MenúToolStripMenuItem.Name = "MenúToolStripMenuItem"
        Me.MenúToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.MenúToolStripMenuItem.Text = "menú"
        '
        'ServidorToolStripMenuItem
        '
        Me.ServidorToolStripMenuItem.Name = "ServidorToolStripMenuItem"
        Me.ServidorToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ServidorToolStripMenuItem.Text = "servidor"
        '
        'EmpresaToolStripMenuItem
        '
        Me.EmpresaToolStripMenuItem.Name = "EmpresaToolStripMenuItem"
        Me.EmpresaToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.EmpresaToolStripMenuItem.Text = "empresa"
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ImportarToolStripMenuItem.Text = "importar"
        '
        'DeveloperToolStripMenuItem
        '
        Me.DeveloperToolStripMenuItem.Name = "DeveloperToolStripMenuItem"
        Me.DeveloperToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.DeveloperToolStripMenuItem.Text = "developer"
        '
        'PerfilToolStripMenuItem
        '
        Me.PerfilToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DesconectarToolStripMenuItem})
        Me.PerfilToolStripMenuItem.Name = "PerfilToolStripMenuItem"
        Me.PerfilToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.PerfilToolStripMenuItem.Text = "perfil"
        '
        'DesconectarToolStripMenuItem
        '
        Me.DesconectarToolStripMenuItem.Name = "DesconectarToolStripMenuItem"
        Me.DesconectarToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.DesconectarToolStripMenuItem.Text = "desconectar"
        '
        'AjudaToolStripMenuItem
        '
        Me.AjudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.AjudaToolStripMenuItem.Name = "AjudaToolStripMenuItem"
        Me.AjudaToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.AjudaToolStripMenuItem.Text = "ajuda"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.AboutToolStripMenuItem.Text = "about"
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
        'Xl_DropFile1
        '
        Me.Xl_DropFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DropFile1.BackColor = System.Drawing.SystemColors.Control
        Me.Xl_DropFile1.Location = New System.Drawing.Point(561, 2)
        Me.Xl_DropFile1.Name = "Xl_DropFile1"
        Me.Xl_DropFile1.Size = New System.Drawing.Size(48, 48)
        Me.Xl_DropFile1.TabIndex = 45
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(24, 25)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(533, 20)
        Me.Xl_Contact1.TabIndex = 44
        '
        'Frm__Idx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 461)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Xl_DropFile1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MinimumSize = New System.Drawing.Size(500, 500)
        Me.Name = "Frm__Idx"
        Me.Text = "Frm_Test"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_WinMenuTree1 As Mat.NET.Xl_WinMenuTree
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_WinMenuListView1 As Mat.NET.Xl_WinMenuListView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenúToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServidorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmpresaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeveloperToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PerfilToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesconectarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_DropFile1 As Mat.NET.Xl_DropFile
    Friend WithEvents Xl_Contact1 As Mat.NET.Xl_Contact
End Class
