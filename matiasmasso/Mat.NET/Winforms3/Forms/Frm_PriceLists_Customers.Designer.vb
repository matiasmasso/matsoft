<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceLists_Customers
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
        Me.Xl_PriceList_Customers_Vigent1 = New Xl_PriceList_Customers_Vigent()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_PriceLists_Customers1 = New Xl_PriceLists_Customers()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgressBarVigent = New System.Windows.Forms.ProgressBar()
        Me.ProgressBarHistoric = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_PriceList_Customers_Vigent1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(806, 490)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_PriceList_Customers_Vigent1)
        Me.TabPage1.Controls.Add(Me.ProgressBarVigent)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(798, 464)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Vigent"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_PriceList_Customers_Vigent1
        '
        Me.Xl_PriceList_Customers_Vigent1.DisplayObsolets = False
        Me.Xl_PriceList_Customers_Vigent1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PriceList_Customers_Vigent1.Filter = Nothing
        Me.Xl_PriceList_Customers_Vigent1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PriceList_Customers_Vigent1.MouseIsDown = False
        Me.Xl_PriceList_Customers_Vigent1.Name = "Xl_PriceList_Customers_Vigent1"
        Me.Xl_PriceList_Customers_Vigent1.Size = New System.Drawing.Size(792, 435)
        Me.Xl_PriceList_Customers_Vigent1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_PriceLists_Customers1)
        Me.TabPage2.Controls.Add(Me.ProgressBarHistoric)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(798, 464)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Historic"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_PriceLists_Customers1
        '
        Me.Xl_PriceLists_Customers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PriceLists_Customers1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PriceLists_Customers1.Name = "Xl_PriceLists_Customers1"
        Me.Xl_PriceLists_Customers1.Size = New System.Drawing.Size(792, 435)
        Me.Xl_PriceLists_Customers1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(805, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ProgressBarVigent
        '
        Me.ProgressBarVigent.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarVigent.Location = New System.Drawing.Point(3, 438)
        Me.ProgressBarVigent.Name = "ProgressBarVigent"
        Me.ProgressBarVigent.Size = New System.Drawing.Size(792, 23)
        Me.ProgressBarVigent.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarVigent.TabIndex = 1
        '
        'ProgressBarHistoric
        '
        Me.ProgressBarHistoric.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarHistoric.Location = New System.Drawing.Point(3, 438)
        Me.ProgressBarHistoric.Name = "ProgressBarHistoric"
        Me.ProgressBarHistoric.Size = New System.Drawing.Size(792, 23)
        Me.ProgressBarHistoric.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarHistoric.TabIndex = 2
        '
        'Frm_PriceLists_Customers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 521)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_PriceLists_Customers"
        Me.Text = "Tarifes de Preus de Venda"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_PriceList_Customers_Vigent1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_PriceList_Customers_Vigent1 As Xl_PriceList_Customers_Vigent
    Friend WithEvents Xl_PriceLists_Customers1 As Xl_PriceLists_Customers
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBarVigent As ProgressBar
    Friend WithEvents ProgressBarHistoric As ProgressBar
End Class
