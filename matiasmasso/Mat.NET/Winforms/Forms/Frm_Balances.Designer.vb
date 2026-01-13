<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Balances
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
        Me.Xl_BalanceActiu = New Winforms.Xl_Balance()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_BalancePassiu = New Winforms.Xl_Balance()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_BalanceExplotacio = New Winforms.Xl_Balance()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TancarBalançToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltimBalançTancatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.PictureBoxOk = New System.Windows.Forms.PictureBox()
        Me.Xl_DateTimePicker1 = New Winforms.Xl_DateTimePicker()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_BalanceActiu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_BalancePassiu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_BalanceExplotacio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBoxOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(2, 35)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(474, 437)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_BalanceActiu)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Size = New System.Drawing.Size(466, 411)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Actiu"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_BalanceActiu
        '
        Me.Xl_BalanceActiu.AllowUserToAddRows = False
        Me.Xl_BalanceActiu.AllowUserToDeleteRows = False
        Me.Xl_BalanceActiu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BalanceActiu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BalanceActiu.Location = New System.Drawing.Point(1, 1)
        Me.Xl_BalanceActiu.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_BalanceActiu.Name = "Xl_BalanceActiu"
        Me.Xl_BalanceActiu.ReadOnly = True
        Me.Xl_BalanceActiu.Size = New System.Drawing.Size(464, 409)
        Me.Xl_BalanceActiu.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_BalancePassiu)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Size = New System.Drawing.Size(466, 411)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Passiu"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_BalancePassiu
        '
        Me.Xl_BalancePassiu.AllowUserToAddRows = False
        Me.Xl_BalancePassiu.AllowUserToDeleteRows = False
        Me.Xl_BalancePassiu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BalancePassiu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BalancePassiu.Location = New System.Drawing.Point(1, 1)
        Me.Xl_BalancePassiu.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_BalancePassiu.Name = "Xl_BalancePassiu"
        Me.Xl_BalancePassiu.ReadOnly = True
        Me.Xl_BalancePassiu.Size = New System.Drawing.Size(464, 409)
        Me.Xl_BalancePassiu.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_BalanceExplotacio)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(466, 411)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Explotació"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_BalanceExplotacio
        '
        Me.Xl_BalanceExplotacio.AllowUserToAddRows = False
        Me.Xl_BalanceExplotacio.AllowUserToDeleteRows = False
        Me.Xl_BalanceExplotacio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BalanceExplotacio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BalanceExplotacio.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BalanceExplotacio.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_BalanceExplotacio.Name = "Xl_BalanceExplotacio"
        Me.Xl_BalanceExplotacio.ReadOnly = True
        Me.Xl_BalanceExplotacio.Size = New System.Drawing.Size(466, 411)
        Me.Xl_BalanceExplotacio.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(2, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(475, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.TancarBalançToolStripMenuItem, Me.UltimBalançTancatToolStripMenuItem, Me.WebToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'TancarBalançToolStripMenuItem
        '
        Me.TancarBalançToolStripMenuItem.Name = "TancarBalançToolStripMenuItem"
        Me.TancarBalançToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.TancarBalançToolStripMenuItem.Text = "Tancar balanç"
        '
        'UltimBalançTancatToolStripMenuItem
        '
        Me.UltimBalançTancatToolStripMenuItem.Name = "UltimBalançTancatToolStripMenuItem"
        Me.UltimBalançTancatToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UltimBalançTancatToolStripMenuItem.Text = "Ultim balanç tancat"
        '
        'WebToolStripMenuItem
        '
        Me.WebToolStripMenuItem.Name = "WebToolStripMenuItem"
        Me.WebToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.WebToolStripMenuItem.Text = "Web"
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresh.Enabled = False
        Me.ButtonRefresh.Location = New System.Drawing.Point(442, 4)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(29, 20)
        Me.ButtonRefresh.TabIndex = 3
        Me.ButtonRefresh.Text = "..."
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'PictureBoxOk
        '
        Me.PictureBoxOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxOk.Image = Global.Winforms.My.Resources.Resources.vb
        Me.PictureBoxOk.Location = New System.Drawing.Point(426, 5)
        Me.PictureBoxOk.Name = "PictureBoxOk"
        Me.PictureBoxOk.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxOk.TabIndex = 4
        Me.PictureBoxOk.TabStop = False
        Me.PictureBoxOk.Visible = False
        '
        'Xl_DateTimePicker1
        '
        Me.Xl_DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Xl_DateTimePicker1.Location = New System.Drawing.Point(338, 4)
        Me.Xl_DateTimePicker1.Name = "Xl_DateTimePicker1"
        Me.Xl_DateTimePicker1.Size = New System.Drawing.Size(86, 20)
        Me.Xl_DateTimePicker1.TabIndex = 5
        '
        'Frm_Balances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 471)
        Me.Controls.Add(Me.Xl_DateTimePicker1)
        Me.Controls.Add(Me.PictureBoxOk)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_Balances"
        Me.Text = "Comptes anuals"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_BalanceActiu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_BalancePassiu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_BalanceExplotacio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBoxOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_BalanceActiu As Xl_Balance
    Friend WithEvents Xl_BalancePassiu As Xl_Balance
    Friend WithEvents Xl_BalanceExplotacio As Xl_Balance
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TancarBalançToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ButtonRefresh As Button
    Friend WithEvents UltimBalançTancatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WebToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBoxOk As PictureBox
    Friend WithEvents Xl_DateTimePicker1 As Xl_DateTimePicker
End Class
