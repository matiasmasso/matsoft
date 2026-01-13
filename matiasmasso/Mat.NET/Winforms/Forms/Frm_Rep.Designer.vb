<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Rep
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPageRepComFollowUp = New System.Windows.Forms.TabPage()
        Me.Xl_RepComsFollowUp1 = New Winforms.Xl_RepComsFollowUp()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_RepRepLiqs1 = New Winforms.Xl_RepRepLiqs()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_RepCertRetencions1 = New Winforms.Xl_RepCertRetencions()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPageRepComFollowUp.SuspendLayout()
        CType(Me.Xl_RepComsFollowUp1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_RepCertRetencions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageRepComFollowUp)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 26)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(566, 235)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(558, 209)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPageRepComFollowUp
        '
        Me.TabPageRepComFollowUp.Controls.Add(Me.Xl_RepComsFollowUp1)
        Me.TabPageRepComFollowUp.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRepComFollowUp.Name = "TabPageRepComFollowUp"
        Me.TabPageRepComFollowUp.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRepComFollowUp.Size = New System.Drawing.Size(558, 209)
        Me.TabPageRepComFollowUp.TabIndex = 1
        Me.TabPageRepComFollowUp.Text = "Seguiment comisions"
        Me.TabPageRepComFollowUp.UseVisualStyleBackColor = True
        '
        'Xl_RepComsFollowUp1
        '
        Me.Xl_RepComsFollowUp1.AllowUserToAddRows = False
        Me.Xl_RepComsFollowUp1.AllowUserToDeleteRows = False
        Me.Xl_RepComsFollowUp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RepComsFollowUp1.DisplayObsolets = False
        Me.Xl_RepComsFollowUp1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepComsFollowUp1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepComsFollowUp1.MouseIsDown = False
        Me.Xl_RepComsFollowUp1.Name = "Xl_RepComsFollowUp1"
        Me.Xl_RepComsFollowUp1.ReadOnly = True
        Me.Xl_RepComsFollowUp1.Size = New System.Drawing.Size(552, 203)
        Me.Xl_RepComsFollowUp1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_RepRepLiqs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(558, 209)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Liquidacions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_RepRepLiqs1
        '
        Me.Xl_RepRepLiqs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepRepLiqs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepRepLiqs1.Name = "Xl_RepRepLiqs1"
        Me.Xl_RepRepLiqs1.Size = New System.Drawing.Size(552, 203)
        Me.Xl_RepRepLiqs1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_RepCertRetencions1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(558, 209)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Retencions"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_RepCertRetencions1
        '
        Me.Xl_RepCertRetencions1.AllowUserToAddRows = False
        Me.Xl_RepCertRetencions1.AllowUserToDeleteRows = False
        Me.Xl_RepCertRetencions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RepCertRetencions1.DisplayObsolets = False
        Me.Xl_RepCertRetencions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepCertRetencions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_RepCertRetencions1.MouseIsDown = False
        Me.Xl_RepCertRetencions1.Name = "Xl_RepCertRetencions1"
        Me.Xl_RepCertRetencions1.ReadOnly = True
        Me.Xl_RepCertRetencions1.Size = New System.Drawing.Size(552, 203)
        Me.Xl_RepCertRetencions1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(568, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'Frm_Rep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 261)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Rep"
        Me.Text = "Frm_Rep"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageRepComFollowUp.ResumeLayout(False)
        CType(Me.Xl_RepComsFollowUp1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_RepCertRetencions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPageRepComFollowUp As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RepComsFollowUp1 As Xl_RepComsFollowUp
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_RepRepLiqs1 As Xl_RepRepLiqs
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_RepCertRetencions1 As Xl_RepCertRetencions
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
End Class
