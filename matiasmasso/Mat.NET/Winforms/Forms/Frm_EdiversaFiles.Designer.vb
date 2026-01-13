<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_EdiversaFiles
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_EdiversaFileTagsIn = New Winforms.Xl_EdiversaFileTags()
        Me.Xl_EdiversaFilesIn = New Winforms.Xl_EdiversaFiles()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_EdiversaFileTagsOut = New Winforms.Xl_EdiversaFileTags()
        Me.Xl_EdiversaFilesOut = New Winforms.Xl_EdiversaFiles()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncludeClosedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.NumericUpDownYear = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_EdiversaFileTagsIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_EdiversaFilesIn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_EdiversaFileTagsOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_EdiversaFilesOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(846, 340)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(838, 314)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entrades"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_EdiversaFileTagsIn)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_EdiversaFilesIn)
        Me.SplitContainer1.Size = New System.Drawing.Size(832, 308)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_EdiversaFileTagsIn
        '
        Me.Xl_EdiversaFileTagsIn.AllowUserToAddRows = False
        Me.Xl_EdiversaFileTagsIn.AllowUserToDeleteRows = False
        Me.Xl_EdiversaFileTagsIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaFileTagsIn.DisplayObsolets = False
        Me.Xl_EdiversaFileTagsIn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaFileTagsIn.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaFileTagsIn.MouseIsDown = False
        Me.Xl_EdiversaFileTagsIn.Name = "Xl_EdiversaFileTagsIn"
        Me.Xl_EdiversaFileTagsIn.ReadOnly = True
        Me.Xl_EdiversaFileTagsIn.Size = New System.Drawing.Size(150, 308)
        Me.Xl_EdiversaFileTagsIn.TabIndex = 0
        '
        'Xl_EdiversaFilesIn
        '
        Me.Xl_EdiversaFilesIn.AllowUserToAddRows = False
        Me.Xl_EdiversaFilesIn.AllowUserToDeleteRows = False
        Me.Xl_EdiversaFilesIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaFilesIn.DisplayObsolets = False
        Me.Xl_EdiversaFilesIn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaFilesIn.Filter = Nothing
        Me.Xl_EdiversaFilesIn.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaFilesIn.MouseIsDown = False
        Me.Xl_EdiversaFilesIn.Name = "Xl_EdiversaFilesIn"
        Me.Xl_EdiversaFilesIn.ReadOnly = True
        Me.Xl_EdiversaFilesIn.Size = New System.Drawing.Size(678, 308)
        Me.Xl_EdiversaFilesIn.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(838, 314)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sortides"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_EdiversaFileTagsOut)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_EdiversaFilesOut)
        Me.SplitContainer2.Size = New System.Drawing.Size(832, 308)
        Me.SplitContainer2.SplitterDistance = 150
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_EdiversaFileTagsOut
        '
        Me.Xl_EdiversaFileTagsOut.AllowUserToAddRows = False
        Me.Xl_EdiversaFileTagsOut.AllowUserToDeleteRows = False
        Me.Xl_EdiversaFileTagsOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaFileTagsOut.DisplayObsolets = False
        Me.Xl_EdiversaFileTagsOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaFileTagsOut.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaFileTagsOut.MouseIsDown = False
        Me.Xl_EdiversaFileTagsOut.Name = "Xl_EdiversaFileTagsOut"
        Me.Xl_EdiversaFileTagsOut.ReadOnly = True
        Me.Xl_EdiversaFileTagsOut.Size = New System.Drawing.Size(150, 308)
        Me.Xl_EdiversaFileTagsOut.TabIndex = 1
        '
        'Xl_EdiversaFilesOut
        '
        Me.Xl_EdiversaFilesOut.AllowUserToAddRows = False
        Me.Xl_EdiversaFilesOut.AllowUserToDeleteRows = False
        Me.Xl_EdiversaFilesOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaFilesOut.DisplayObsolets = False
        Me.Xl_EdiversaFilesOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaFilesOut.Filter = Nothing
        Me.Xl_EdiversaFilesOut.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaFilesOut.MouseIsDown = False
        Me.Xl_EdiversaFilesOut.Name = "Xl_EdiversaFilesOut"
        Me.Xl_EdiversaFilesOut.ReadOnly = True
        Me.Xl_EdiversaFilesOut.Size = New System.Drawing.Size(678, 308)
        Me.Xl_EdiversaFilesOut.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(846, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IncludeClosedToolStripMenuItem, Me.RefrescaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'IncludeClosedToolStripMenuItem
        '
        Me.IncludeClosedToolStripMenuItem.CheckOnClick = True
        Me.IncludeClosedToolStripMenuItem.Name = "IncludeClosedToolStripMenuItem"
        Me.IncludeClosedToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.IncludeClosedToolStripMenuItem.Text = "inclou tancats"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.RefrescaToolStripMenuItem.Text = "refresca"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(846, 363)
        Me.Panel1.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 340)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(846, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(632, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(210, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'NumericUpDownYear
        '
        Me.NumericUpDownYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownYear.Location = New System.Drawing.Point(559, 4)
        Me.NumericUpDownYear.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYear.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYear.Name = "NumericUpDownYear"
        Me.NumericUpDownYear.Size = New System.Drawing.Size(67, 20)
        Me.NumericUpDownYear.TabIndex = 4
        Me.NumericUpDownYear.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(528, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Any"
        '
        'Frm_EdiversaFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 391)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownYear)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_EdiversaFiles"
        Me.Text = "Fitxers Edi"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_EdiversaFileTagsIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_EdiversaFilesIn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_EdiversaFileTagsOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_EdiversaFilesOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_EdiversaFileTagsIn As Xl_EdiversaFileTags
    Friend WithEvents Xl_EdiversaFilesIn As Xl_EdiversaFiles
    Friend WithEvents Xl_EdiversaFileTagsOut As Xl_EdiversaFileTags
    Friend WithEvents Xl_EdiversaFilesOut As Xl_EdiversaFiles
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IncludeClosedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents NumericUpDownYear As NumericUpDown
    Friend WithEvents Label1 As Label
End Class
