<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EDiversaOrders
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
        Me.CheckBoxOnlyOpenOrders = New System.Windows.Forms.CheckBox()
        Me.LabelTotals = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_EdiversaOrders1 = New Mat.Net.Xl_EdiversaOrders()
        Me.Xl_Brands_CheckList1 = New Mat.Net.Xl_Brands_CheckList()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_EdiversaOrdersSkus1 = New Mat.Net.Xl_EdiversaOrdersSkus()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_EdiversaOrdersConfirmationPending = New Mat.Net.Xl_EdiversaOrders()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_EdiversaOrdrSps1 = New Mat.Net.Xl_EdiversaOrdrSps()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportEdiversaFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_EdiversaOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Brands_CheckList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_EdiversaOrdersSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_EdiversaOrdersConfirmationPending, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_EdiversaOrdrSps1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxOnlyOpenOrders
        '
        Me.CheckBoxOnlyOpenOrders.AutoSize = True
        Me.CheckBoxOnlyOpenOrders.Checked = True
        Me.CheckBoxOnlyOpenOrders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxOnlyOpenOrders.Location = New System.Drawing.Point(169, 12)
        Me.CheckBoxOnlyOpenOrders.Name = "CheckBoxOnlyOpenOrders"
        Me.CheckBoxOnlyOpenOrders.Size = New System.Drawing.Size(167, 17)
        Me.CheckBoxOnlyOpenOrders.TabIndex = 2
        Me.CheckBoxOnlyOpenOrders.Text = "Nomes els pendents de entrar"
        Me.CheckBoxOnlyOpenOrders.UseVisualStyleBackColor = True
        '
        'LabelTotals
        '
        Me.LabelTotals.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTotals.AutoSize = True
        Me.LabelTotals.Location = New System.Drawing.Point(4, 430)
        Me.LabelTotals.Name = "LabelTotals"
        Me.LabelTotals.Size = New System.Drawing.Size(31, 13)
        Me.LabelTotals.TabIndex = 3
        Me.LabelTotals.Text = "Total"
        Me.LabelTotals.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(708, 375)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(700, 349)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pendents de entrar"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_EdiversaOrders1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Brands_CheckList1)
        Me.SplitContainer1.Size = New System.Drawing.Size(694, 343)
        Me.SplitContainer1.SplitterDistance = 487
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_EdiversaOrders1
        '
        Me.Xl_EdiversaOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaOrders1.DisplayObsolets = False
        Me.Xl_EdiversaOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaOrders1.Filter = Nothing
        Me.Xl_EdiversaOrders1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaOrders1.MouseIsDown = False
        Me.Xl_EdiversaOrders1.Name = "Xl_EdiversaOrders1"
        Me.Xl_EdiversaOrders1.Size = New System.Drawing.Size(487, 343)
        Me.Xl_EdiversaOrders1.TabIndex = 0
        '
        'Xl_Brands_CheckList1
        '
        Me.Xl_Brands_CheckList1.AllowUserToAddRows = False
        Me.Xl_Brands_CheckList1.AllowUserToDeleteRows = False
        Me.Xl_Brands_CheckList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Brands_CheckList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Brands_CheckList1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Brands_CheckList1.Name = "Xl_Brands_CheckList1"
        Me.Xl_Brands_CheckList1.ReadOnly = True
        Me.Xl_Brands_CheckList1.Size = New System.Drawing.Size(203, 343)
        Me.Xl_Brands_CheckList1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_EdiversaOrdersSkus1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(700, 349)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Productes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_EdiversaOrdersSkus1
        '
        Me.Xl_EdiversaOrdersSkus1.AllowUserToAddRows = False
        Me.Xl_EdiversaOrdersSkus1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaOrdersSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaOrdersSkus1.DisplayObsolets = False
        Me.Xl_EdiversaOrdersSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaOrdersSkus1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_EdiversaOrdersSkus1.MouseIsDown = False
        Me.Xl_EdiversaOrdersSkus1.Name = "Xl_EdiversaOrdersSkus1"
        Me.Xl_EdiversaOrdersSkus1.ReadOnly = True
        Me.Xl_EdiversaOrdersSkus1.Size = New System.Drawing.Size(694, 343)
        Me.Xl_EdiversaOrdersSkus1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_EdiversaOrdersConfirmationPending)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(700, 349)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Pendents de Confirmar"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_EdiversaOrdersConfirmationPending
        '
        Me.Xl_EdiversaOrdersConfirmationPending.AllowUserToAddRows = False
        Me.Xl_EdiversaOrdersConfirmationPending.AllowUserToDeleteRows = False
        Me.Xl_EdiversaOrdersConfirmationPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaOrdersConfirmationPending.DisplayObsolets = False
        Me.Xl_EdiversaOrdersConfirmationPending.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaOrdersConfirmationPending.Filter = Nothing
        Me.Xl_EdiversaOrdersConfirmationPending.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaOrdersConfirmationPending.MouseIsDown = False
        Me.Xl_EdiversaOrdersConfirmationPending.Name = "Xl_EdiversaOrdersConfirmationPending"
        Me.Xl_EdiversaOrdersConfirmationPending.ReadOnly = True
        Me.Xl_EdiversaOrdersConfirmationPending.Size = New System.Drawing.Size(700, 349)
        Me.Xl_EdiversaOrdersConfirmationPending.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_EdiversaOrdrSps1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(700, 349)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Confirmacions"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_EdiversaOrdrSps1
        '
        Me.Xl_EdiversaOrdrSps1.AllowUserToAddRows = False
        Me.Xl_EdiversaOrdrSps1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaOrdrSps1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaOrdrSps1.DisplayObsolets = False
        Me.Xl_EdiversaOrdrSps1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaOrdrSps1.Filter = Nothing
        Me.Xl_EdiversaOrdrSps1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaOrdrSps1.MouseIsDown = False
        Me.Xl_EdiversaOrdrSps1.Name = "Xl_EdiversaOrdrSps1"
        Me.Xl_EdiversaOrdrSps1.ReadOnly = True
        Me.Xl_EdiversaOrdrSps1.Size = New System.Drawing.Size(700, 349)
        Me.Xl_EdiversaOrdrSps1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(708, 385)
        Me.Panel1.TabIndex = 7
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 375)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(708, 10)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(540, 11)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 4
        Me.Xl_Years1.Value = 0
        Me.Xl_Years1.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(708, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportEdiversaFileToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ImportEdiversaFileToolStripMenuItem
        '
        Me.ImportEdiversaFileToolStripMenuItem.Name = "ImportEdiversaFileToolStripMenuItem"
        Me.ImportEdiversaFileToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
        Me.ImportEdiversaFileToolStripMenuItem.Text = "Importar fitxer pla Ediversa"
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(342, 11)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(182, 20)
        Me.Xl_TextboxSearch1.TabIndex = 9
        '
        'Frm_EDiversaOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 449)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelTotals)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.CheckBoxOnlyOpenOrders)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_EDiversaOrders"
        Me.Text = "Comandes EDI pendents de entrar"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_EdiversaOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Brands_CheckList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_EdiversaOrdersSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_EdiversaOrdersConfirmationPending, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_EdiversaOrdrSps1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_EdiversaOrders1 As Xl_EdiversaOrders
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_Brands_CheckList1 As Xl_Brands_CheckList
    Friend WithEvents CheckBoxOnlyOpenOrders As CheckBox
    Friend WithEvents LabelTotals As Label
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_EdiversaOrdersSkus1 As Xl_EdiversaOrdersSkus
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_EdiversaOrdersConfirmationPending As Xl_EdiversaOrders
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_EdiversaOrdrSps1 As Xl_EdiversaOrdrSps
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportEdiversaFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
