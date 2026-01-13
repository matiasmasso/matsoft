<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MarketPlace
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Percent1 = New Mat.Net.Xl_Percent()
        Me.Xl_Contact21 = New Mat.Net.Xl_Contact2()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PanelCatalog = New System.Windows.Forms.Panel()
        Me.Xl_MarketplaceSkus1 = New Mat.Net.Xl_MarketplaceSkus()
        Me.ProgressBarCatalog = New System.Windows.Forms.ProgressBar()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.PanelTickets = New System.Windows.Forms.Panel()
        Me.Xl_ConsumerTickets1 = New Mat.Net.Xl_ConsumerTickets()
        Me.ProgressBarTickets = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowDisabled = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.PanelCatalog.SuspendLayout()
        CType(Me.Xl_MarketplaceSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.PanelTickets.SuspendLayout()
        CType(Me.Xl_ConsumerTickets1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Entitat"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 34)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(470, 284)
        Me.TabControl1.TabIndex = 60
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.PanelButtons)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_Percent1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Xl_Contact21)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(462, 258)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(3, 224)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(456, 31)
        Me.PanelButtons.TabIndex = 62
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(237, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(348, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Nom"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(72, 47)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(384, 20)
        Me.TextBoxNom.TabIndex = 60
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Comisió:"
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Location = New System.Drawing.Point(72, 99)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(76, 20)
        Me.Xl_Percent1.TabIndex = 58
        Me.Xl_Percent1.Text = "0 %"
        Me.Xl_Percent1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Enabled = False
        Me.Xl_Contact21.Location = New System.Drawing.Point(72, 20)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(384, 20)
        Me.Xl_Contact21.TabIndex = 57
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PanelCatalog)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(462, 258)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Cataleg"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PanelCatalog
        '
        Me.PanelCatalog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelCatalog.Controls.Add(Me.Xl_MarketplaceSkus1)
        Me.PanelCatalog.Controls.Add(Me.ProgressBarCatalog)
        Me.PanelCatalog.Location = New System.Drawing.Point(0, 1)
        Me.PanelCatalog.Name = "PanelCatalog"
        Me.PanelCatalog.Size = New System.Drawing.Size(461, 260)
        Me.PanelCatalog.TabIndex = 1
        '
        'Xl_MarketplaceSkus1
        '
        Me.Xl_MarketplaceSkus1.AllowUserToAddRows = False
        Me.Xl_MarketplaceSkus1.AllowUserToDeleteRows = False
        Me.Xl_MarketplaceSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_MarketplaceSkus1.DisplayObsolets = False
        Me.Xl_MarketplaceSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_MarketplaceSkus1.Filter = Nothing
        Me.Xl_MarketplaceSkus1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_MarketplaceSkus1.MouseIsDown = False
        Me.Xl_MarketplaceSkus1.Name = "Xl_MarketplaceSkus1"
        Me.Xl_MarketplaceSkus1.ReadOnly = True
        Me.Xl_MarketplaceSkus1.Size = New System.Drawing.Size(461, 237)
        Me.Xl_MarketplaceSkus1.TabIndex = 0
        '
        'ProgressBarCatalog
        '
        Me.ProgressBarCatalog.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarCatalog.Location = New System.Drawing.Point(0, 237)
        Me.ProgressBarCatalog.Name = "ProgressBarCatalog"
        Me.ProgressBarCatalog.Size = New System.Drawing.Size(461, 23)
        Me.ProgressBarCatalog.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarCatalog.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PanelTickets)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(462, 258)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tickets"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PanelTickets
        '
        Me.PanelTickets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelTickets.Controls.Add(Me.Xl_ConsumerTickets1)
        Me.PanelTickets.Controls.Add(Me.ProgressBarTickets)
        Me.PanelTickets.Location = New System.Drawing.Point(3, 3)
        Me.PanelTickets.Name = "PanelTickets"
        Me.PanelTickets.Size = New System.Drawing.Size(459, 252)
        Me.PanelTickets.TabIndex = 2
        '
        'Xl_ConsumerTickets1
        '
        Me.Xl_ConsumerTickets1.AllowUserToAddRows = False
        Me.Xl_ConsumerTickets1.AllowUserToDeleteRows = False
        Me.Xl_ConsumerTickets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ConsumerTickets1.DisplayObsolets = False
        Me.Xl_ConsumerTickets1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ConsumerTickets1.Filter = Nothing
        Me.Xl_ConsumerTickets1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ConsumerTickets1.MouseIsDown = False
        Me.Xl_ConsumerTickets1.Name = "Xl_ConsumerTickets1"
        Me.Xl_ConsumerTickets1.ReadOnly = True
        Me.Xl_ConsumerTickets1.Size = New System.Drawing.Size(459, 229)
        Me.Xl_ConsumerTickets1.TabIndex = 0
        '
        'ProgressBarTickets
        '
        Me.ProgressBarTickets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarTickets.Location = New System.Drawing.Point(0, 229)
        Me.ProgressBarTickets.Name = "ProgressBarTickets"
        Me.ProgressBarTickets.Size = New System.Drawing.Size(459, 23)
        Me.ProgressBarTickets.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarTickets.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(470, 24)
        Me.MenuStrip1.TabIndex = 61
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDisabled, Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ShowDisabled
        '
        Me.ShowDisabled.CheckOnClick = True
        Me.ShowDisabled.Name = "ShowDisabled"
        Me.ShowDisabled.Size = New System.Drawing.Size(189, 22)
        Me.ShowDisabled.Text = "Mostra els desactivats"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        Me.ExcelToolStripMenuItem.Visible = False
        '
        'Frm_MarketPlace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 317)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_MarketPlace"
        Me.Text = "Market Place"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.PanelButtons.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.PanelCatalog.ResumeLayout(False)
        CType(Me.Xl_MarketplaceSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.PanelTickets.ResumeLayout(False)
        CType(Me.Xl_ConsumerTickets1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents Xl_ConsumerTickets1 As Xl_ConsumerTickets
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents PanelCatalog As Panel
    Friend WithEvents Xl_MarketplaceSkus1 As Xl_MarketplaceSkus
    Friend WithEvents ProgressBarCatalog As ProgressBar
    Friend WithEvents PanelTickets As Panel
    Friend WithEvents ProgressBarTickets As ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowDisabled As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
End Class
