<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ctas
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
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DateTimePicker1 = New Winforms.Xl_DateTimePicker()
        Me.Xl_SumasYSaldos1 = New Winforms.Xl_SumasYSaldos()
        Me.ButtonReload = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Excel3DigitsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3D = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4D = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5D = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_SumasYSaldos1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Location = New System.Drawing.Point(287, 5)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(288, 20)
        Me.TextBoxSearch.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Winforms.My.Resources.Resources.search_16
        Me.PictureBox1.Location = New System.Drawing.Point(576, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(22, 20)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(622, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 6
        '
        'Xl_SumasYSaldos1
        '
        Me.Xl_SumasYSaldos1.AllowUserToAddRows = False
        Me.Xl_SumasYSaldos1.AllowUserToDeleteRows = False
        Me.Xl_SumasYSaldos1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SumasYSaldos1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SumasYSaldos1.Filter = Nothing
        Me.Xl_SumasYSaldos1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SumasYSaldos1.Name = "Xl_SumasYSaldos1"
        Me.Xl_SumasYSaldos1.ReadOnly = True
        Me.Xl_SumasYSaldos1.Size = New System.Drawing.Size(773, 354)
        Me.Xl_SumasYSaldos1.TabIndex = 7
        '
        'ButtonReload
        '
        Me.ButtonReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReload.Enabled = False
        Me.ButtonReload.Location = New System.Drawing.Point(732, 5)
        Me.ButtonReload.Name = "ButtonReload"
        Me.ButtonReload.Size = New System.Drawing.Size(28, 20)
        Me.ButtonReload.TabIndex = 9
        Me.ButtonReload.Text = "..."
        Me.ButtonReload.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(773, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Excel3DigitsToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Excel3DigitsToolStripMenuItem
        '
        Me.Excel3DigitsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3D, Me.ToolStripMenuItem4D, Me.ToolStripMenuItem5D, Me.ToolStripMenuItemFull})
        Me.Excel3DigitsToolStripMenuItem.Image = Global.Winforms.My.Resources.Resources.Excel
        Me.Excel3DigitsToolStripMenuItem.Name = "Excel3DigitsToolStripMenuItem"
        Me.Excel3DigitsToolStripMenuItem.Size = New System.Drawing.Size(101, 22)
        Me.Excel3DigitsToolStripMenuItem.Text = "Excel"
        '
        'ToolStripMenuItem3D
        '
        Me.ToolStripMenuItem3D.Name = "ToolStripMenuItem3D"
        Me.ToolStripMenuItem3D.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripMenuItem3D.Text = "3 digits"
        '
        'ToolStripMenuItem4D
        '
        Me.ToolStripMenuItem4D.Name = "ToolStripMenuItem4D"
        Me.ToolStripMenuItem4D.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripMenuItem4D.Text = "4 digits"
        '
        'ToolStripMenuItem5D
        '
        Me.ToolStripMenuItem5D.Name = "ToolStripMenuItem5D"
        Me.ToolStripMenuItem5D.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripMenuItem5D.Text = "5 digits"
        '
        'ToolStripMenuItemFull
        '
        Me.ToolStripMenuItemFull.Name = "ToolStripMenuItemFull"
        Me.ToolStripMenuItemFull.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripMenuItemFull.Text = "complert"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_SumasYSaldos1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(773, 377)
        Me.Panel1.TabIndex = 11
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 354)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(773, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 8
        '
        'Frm_Ctas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 408)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonReload)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Ctas"
        Me.Text = "Sumes i Saldos"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_SumasYSaldos1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxSearch As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents DateTimePicker1 As Xl_DateTimePicker
    Friend WithEvents Xl_SumasYSaldos1 As Xl_SumasYSaldos
    Friend WithEvents ButtonReload As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Excel3DigitsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3D As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4D As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5D As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemFull As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
