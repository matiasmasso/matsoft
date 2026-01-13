<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LastCcas
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LlibreDiariToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LlibreMajorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_Ccas1 = New Winforms.Xl_Ccas()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_ProgressBar1 = New Winforms.Xl_ProgressBar()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Ccas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(714, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LlibreDiariToolStripMenuItem, Me.LlibreMajorToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'LlibreDiariToolStripMenuItem
        '
        Me.LlibreDiariToolStripMenuItem.Name = "LlibreDiariToolStripMenuItem"
        Me.LlibreDiariToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.LlibreDiariToolStripMenuItem.Text = "Llibre Diari"
        '
        'LlibreMajorToolStripMenuItem
        '
        Me.LlibreMajorToolStripMenuItem.Name = "LlibreMajorToolStripMenuItem"
        Me.LlibreMajorToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.LlibreMajorToolStripMenuItem.Text = "Llibre Major"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Ccas1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.Xl_ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(714, 398)
        Me.Panel1.TabIndex = 5
        '
        'Xl_Ccas1
        '
        Me.Xl_Ccas1.AllowUserToAddRows = False
        Me.Xl_Ccas1.AllowUserToDeleteRows = False
        Me.Xl_Ccas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Ccas1.DisplayObsolets = False
        Me.Xl_Ccas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Ccas1.Filter = Nothing
        Me.Xl_Ccas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Ccas1.MouseIsDown = False
        Me.Xl_Ccas1.Name = "Xl_Ccas1"
        Me.Xl_Ccas1.ReadOnly = True
        Me.Xl_Ccas1.Size = New System.Drawing.Size(714, 345)
        Me.Xl_Ccas1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 345)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(714, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 5
        Me.ProgressBar1.Visible = False
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 368)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(714, 30)
        Me.Xl_ProgressBar1.TabIndex = 4
        Me.Xl_ProgressBar1.Visible = False
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(392, 2)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(561, 2)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Frm_LastCcas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 428)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_LastCcas"
        Me.Text = "Ultims assentaments registrats"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Ccas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_Ccas1 As Xl_Ccas
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LlibreDiariToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LlibreMajorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
