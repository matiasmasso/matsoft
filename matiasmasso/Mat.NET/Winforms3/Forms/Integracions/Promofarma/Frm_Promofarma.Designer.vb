<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Promofarma
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
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_PromofarmaOrders1 = New Mat.Net.Xl_PromofarmaOrders()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBoxCsv = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowDisabled = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_MarketplaceSkus1 = New Mat.Net.Xl_MarketplaceSkus()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_PromofarmaOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_MarketplaceSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(661, 250)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_PromofarmaOrders1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(653, 224)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Comandes"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_PromofarmaOrders1
        '
        Me.Xl_PromofarmaOrders1.AllowUserToAddRows = False
        Me.Xl_PromofarmaOrders1.AllowUserToDeleteRows = False
        Me.Xl_PromofarmaOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PromofarmaOrders1.DisplayObsolets = False
        Me.Xl_PromofarmaOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PromofarmaOrders1.Filter = Nothing
        Me.Xl_PromofarmaOrders1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PromofarmaOrders1.MouseIsDown = False
        Me.Xl_PromofarmaOrders1.Name = "Xl_PromofarmaOrders1"
        Me.Xl_PromofarmaOrders1.ReadOnly = True
        Me.Xl_PromofarmaOrders1.Size = New System.Drawing.Size(647, 218)
        Me.Xl_PromofarmaOrders1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_MarketplaceSkus1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Size = New System.Drawing.Size(653, 224)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Feed"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TextBoxCsv)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Size = New System.Drawing.Size(653, 224)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Csv"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBoxCsv
        '
        Me.TextBoxCsv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCsv.Location = New System.Drawing.Point(2, 2)
        Me.TextBoxCsv.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxCsv.Multiline = True
        Me.TextBoxCsv.Name = "TextBoxCsv"
        Me.TextBoxCsv.Size = New System.Drawing.Size(649, 220)
        Me.TextBoxCsv.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(-1, 27)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(661, 265)
        Me.Panel1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 250)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(2)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(661, 15)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(505, 9)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(661, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDisabled})
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
        Me.ShowDisabled.Visible = False
        '
        'Xl_MarketplaceSkus1
        '
        Me.Xl_MarketplaceSkus1.AllowUserToAddRows = False
        Me.Xl_MarketplaceSkus1.AllowUserToDeleteRows = False
        Me.Xl_MarketplaceSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_MarketplaceSkus1.DisplayObsolets = False
        Me.Xl_MarketplaceSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_MarketplaceSkus1.Filter = Nothing
        Me.Xl_MarketplaceSkus1.Location = New System.Drawing.Point(2, 2)
        Me.Xl_MarketplaceSkus1.MouseIsDown = False
        Me.Xl_MarketplaceSkus1.Name = "Xl_MarketplaceSkus1"
        Me.Xl_MarketplaceSkus1.ReadOnly = True
        Me.Xl_MarketplaceSkus1.Size = New System.Drawing.Size(649, 220)
        Me.Xl_MarketplaceSkus1.TabIndex = 0
        '
        'Frm_Promofarma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 292)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Frm_Promofarma"
        Me.Text = "Promofarma"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_PromofarmaOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_MarketplaceSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxCsv As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowDisabled As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_PromofarmaOrders1 As Xl_PromofarmaOrders
    Friend WithEvents Xl_MarketplaceSkus1 As Xl_MarketplaceSkus
End Class
