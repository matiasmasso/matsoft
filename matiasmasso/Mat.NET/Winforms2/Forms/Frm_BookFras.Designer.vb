<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BookFras
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
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.Xl_BookFras1 = New Mat.Net.Xl_BookFras()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZipAmbTotesLesFacturesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_ProgressBar1 = New Mat.Net.Xl_ProgressBar()
        CType(Me.Xl_BookFras1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(690, 5)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_BookFras1
        '
        Me.Xl_BookFras1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BookFras1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BookFras1.Filter = Nothing
        Me.Xl_BookFras1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BookFras1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_BookFras1.Name = "Xl_BookFras1"
        Me.Xl_BookFras1.RowTemplate.Height = 40
        Me.Xl_BookFras1.Size = New System.Drawing.Size(839, 162)
        Me.Xl_BookFras1.TabIndex = 1
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(339, 2)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 2
        Me.Xl_Years1.Value = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(839, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ZipAmbTotesLesFacturesToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ZipAmbTotesLesFacturesToolStripMenuItem
        '
        Me.ZipAmbTotesLesFacturesToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.pdf
        Me.ZipAmbTotesLesFacturesToolStripMenuItem.Name = "ZipAmbTotesLesFacturesToolStripMenuItem"
        Me.ZipAmbTotesLesFacturesToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.ZipAmbTotesLesFacturesToolStripMenuItem.Text = "Zip amb totes les factures"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_BookFras1)
        Me.Panel1.Controls.Add(Me.Xl_ProgressBar1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(839, 219)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 192)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(839, 27)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 162)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(839, 30)
        Me.Xl_ProgressBar1.TabIndex = 14
        Me.Xl_ProgressBar1.Visible = False
        '
        'Frm_BookFras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 247)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_BookFras"
        Me.Text = "Llibre de Factures rebudes"
        CType(Me.Xl_BookFras1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_BookFras1 As Xl_BookFras
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ZipAmbTotesLesFacturesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
End Class
