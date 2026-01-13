<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Budgets
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_BudgetsTree1 = New Winforms.Xl_BudgetsTree()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_BudgetOrders1 = New Winforms.Xl_BudgetOrders()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_BudgetOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(482, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(482, 307)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_BudgetsTree1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage3.Size = New System.Drawing.Size(474, 281)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Pressupostos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_BudgetsTree1
        '
        Me.Xl_BudgetsTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BudgetsTree1.Location = New System.Drawing.Point(1, 1)
        Me.Xl_BudgetsTree1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_BudgetsTree1.Name = "Xl_BudgetsTree1"
        Me.Xl_BudgetsTree1.Size = New System.Drawing.Size(472, 279)
        Me.Xl_BudgetsTree1.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage2.Controls.Add(Me.Xl_BudgetOrders1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(474, 281)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Ordres de compra"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(267, 6)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(222, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Xl_BudgetOrders1
        '
        Me.Xl_BudgetOrders1.AllowUserToAddRows = False
        Me.Xl_BudgetOrders1.AllowUserToDeleteRows = False
        Me.Xl_BudgetOrders1.AllowUserToOrderColumns = True
        Me.Xl_BudgetOrders1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BudgetOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BudgetOrders1.DisplayObsolets = False
        Me.Xl_BudgetOrders1.Filter = Nothing
        Me.Xl_BudgetOrders1.Location = New System.Drawing.Point(3, 32)
        Me.Xl_BudgetOrders1.MouseIsDown = False
        Me.Xl_BudgetOrders1.Name = "Xl_BudgetOrders1"
        Me.Xl_BudgetOrders1.ReadOnly = True
        Me.Xl_BudgetOrders1.Size = New System.Drawing.Size(468, 249)
        Me.Xl_BudgetOrders1.TabIndex = 0
        '
        'Frm_Budgets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 334)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Budgets"
        Me.Text = "Pressupostos"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_BudgetOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_BudgetOrders1 As Xl_BudgetOrders
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_BudgetsTree1 As Xl_BudgetsTree
End Class
