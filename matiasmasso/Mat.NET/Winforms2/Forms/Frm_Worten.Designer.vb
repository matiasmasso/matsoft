<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Worten
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage0 = New System.Windows.Forms.TabPage()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_shop_name = New System.Windows.Forms.TextBox()
        Me.TextBox_shop_id = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_WortenOffers1 = New Mat.Net.Xl_WortenOffers()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_WortenProductCategories1 = New Mat.Net.Xl_WortenProductCategories()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActualitzarStocksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.Xl_WortenOrders1 = New Mat.Net.Xl_WortenOrders()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage0.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_WortenOffers1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_WortenOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(2, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(603, 303)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage0)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(603, 280)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage0
        '
        Me.TabPage0.Controls.Add(Me.PanelButtons)
        Me.TabPage0.Controls.Add(Me.TextBox1)
        Me.TabPage0.Controls.Add(Me.Label3)
        Me.TabPage0.Controls.Add(Me.TextBox_shop_name)
        Me.TabPage0.Controls.Add(Me.TextBox_shop_id)
        Me.TabPage0.Controls.Add(Me.Label2)
        Me.TabPage0.Controls.Add(Me.Label1)
        Me.TabPage0.Location = New System.Drawing.Point(4, 22)
        Me.TabPage0.Name = "TabPage0"
        Me.TabPage0.Size = New System.Drawing.Size(595, 254)
        Me.TabPage0.TabIndex = 1
        Me.TabPage0.Text = "General"
        Me.TabPage0.UseVisualStyleBackColor = True
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 223)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(595, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(376, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(487, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(103, 80)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(325, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Email"
        '
        'TextBox_shop_name
        '
        Me.TextBox_shop_name.Enabled = False
        Me.TextBox_shop_name.Location = New System.Drawing.Point(103, 54)
        Me.TextBox_shop_name.Name = "TextBox_shop_name"
        Me.TextBox_shop_name.Size = New System.Drawing.Size(325, 20)
        Me.TextBox_shop_name.TabIndex = 3
        '
        'TextBox_shop_id
        '
        Me.TextBox_shop_id.Enabled = False
        Me.TextBox_shop_id.Location = New System.Drawing.Point(103, 28)
        Me.TextBox_shop_id.Name = "TextBox_shop_id"
        Me.TextBox_shop_id.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_shop_id.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nom"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Id botiga"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_WortenOffers1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(595, 254)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Oferta"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_WortenOffers1
        '
        Me.Xl_WortenOffers1.AllowUserToAddRows = False
        Me.Xl_WortenOffers1.AllowUserToDeleteRows = False
        Me.Xl_WortenOffers1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WortenOffers1.DisplayObsolets = False
        Me.Xl_WortenOffers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WortenOffers1.Filter = Nothing
        Me.Xl_WortenOffers1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WortenOffers1.MouseIsDown = False
        Me.Xl_WortenOffers1.Name = "Xl_WortenOffers1"
        Me.Xl_WortenOffers1.ReadOnly = True
        Me.Xl_WortenOffers1.Size = New System.Drawing.Size(595, 254)
        Me.Xl_WortenOffers1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_WortenOrders1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(595, 254)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Comandes"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_WortenProductCategories1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(595, 254)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Categories"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_WortenProductCategories1
        '
        Me.Xl_WortenProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WortenProductCategories1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WortenProductCategories1.Name = "Xl_WortenProductCategories1"
        Me.Xl_WortenProductCategories1.Size = New System.Drawing.Size(589, 248)
        Me.Xl_WortenProductCategories1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 280)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(603, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(606, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActualitzarStocksToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ActualitzarStocksToolStripMenuItem
        '
        Me.ActualitzarStocksToolStripMenuItem.Name = "ActualitzarStocksToolStripMenuItem"
        Me.ActualitzarStocksToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ActualitzarStocksToolStripMenuItem.Text = "Actualitzar stocks"
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(455, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        Me.Xl_TextboxSearch1.Visible = False
        '
        'Xl_WortenOrders1
        '
        Me.Xl_WortenOrders1.AllowUserToAddRows = False
        Me.Xl_WortenOrders1.AllowUserToDeleteRows = False
        Me.Xl_WortenOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WortenOrders1.DisplayObsolets = False
        Me.Xl_WortenOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WortenOrders1.Filter = Nothing
        Me.Xl_WortenOrders1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WortenOrders1.MouseIsDown = False
        Me.Xl_WortenOrders1.Name = "Xl_WortenOrders1"
        Me.Xl_WortenOrders1.ReadOnly = True
        Me.Xl_WortenOrders1.Size = New System.Drawing.Size(589, 248)
        Me.Xl_WortenOrders1.TabIndex = 0
        '
        'Frm_Worten
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 354)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Worten"
        Me.Text = "Worten"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage0.ResumeLayout(False)
        Me.TabPage0.PerformLayout()
        Me.PanelButtons.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_WortenOffers1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_WortenOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_WortenProductCategories1 As Xl_WortenProductCategories
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage0 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_WortenOffers1 As Xl_WortenOffers
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents ActualitzarStocksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox_shop_name As TextBox
    Friend WithEvents TextBox_shop_id As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_WortenOrders1 As Xl_WortenOrders
End Class
