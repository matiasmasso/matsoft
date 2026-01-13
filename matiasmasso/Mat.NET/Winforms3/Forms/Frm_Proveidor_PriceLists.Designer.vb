<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Proveidor_PriceLists
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
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelTarifaVigentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_PriceLists_Suppliers1 = New Xl_PriceLists_Suppliers()
        Me.Xl_TextboxSearchHistorial = New Xl_TextboxSearch()
        Me.Xl_TarifaProveidor1 = New Xl_TarifaProveidor()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_PriceLists_Suppliers1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_TarifaProveidor1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(480, 215)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_PriceLists_Suppliers1)
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearchHistorial)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(472, 189)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Historial"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_TarifaProveidor1)
        Me.TabPage1.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(472, 189)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Vigent"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(480, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelTarifaVigentToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelTarifaVigentToolStripMenuItem
        '
        Me.ExcelTarifaVigentToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.Excel_16
        Me.ExcelTarifaVigentToolStripMenuItem.Name = "ExcelTarifaVigentToolStripMenuItem"
        Me.ExcelTarifaVigentToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExcelTarifaVigentToolStripMenuItem.Text = "Excel tarifa vigent"
        '
        'Xl_PriceLists_Suppliers1
        '
        Me.Xl_PriceLists_Suppliers1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PriceLists_Suppliers1.DisplayObsolets = False
        Me.Xl_PriceLists_Suppliers1.Filter = Nothing
        Me.Xl_PriceLists_Suppliers1.Location = New System.Drawing.Point(0, 32)
        Me.Xl_PriceLists_Suppliers1.MouseIsDown = False
        Me.Xl_PriceLists_Suppliers1.Name = "Xl_PriceLists_Suppliers1"
        Me.Xl_PriceLists_Suppliers1.Size = New System.Drawing.Size(472, 154)
        Me.Xl_PriceLists_Suppliers1.TabIndex = 2
        '
        'Xl_TextboxSearchHistorial
        '
        Me.Xl_TextboxSearchHistorial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchHistorial.Location = New System.Drawing.Point(319, 6)
        Me.Xl_TextboxSearchHistorial.Name = "Xl_TextboxSearchHistorial"
        Me.Xl_TextboxSearchHistorial.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearchHistorial.TabIndex = 3
        '
        'Xl_TarifaProveidor1
        '
        Me.Xl_TarifaProveidor1.AllowUserToAddRows = False
        Me.Xl_TarifaProveidor1.AllowUserToDeleteRows = False
        Me.Xl_TarifaProveidor1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TarifaProveidor1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_TarifaProveidor1.DisplayObsolets = False
        Me.Xl_TarifaProveidor1.Filter = Nothing
        Me.Xl_TarifaProveidor1.Location = New System.Drawing.Point(0, 32)
        Me.Xl_TarifaProveidor1.MouseIsDown = False
        Me.Xl_TarifaProveidor1.Name = "Xl_TarifaProveidor1"
        Me.Xl_TarifaProveidor1.ReadOnly = True
        Me.Xl_TarifaProveidor1.Size = New System.Drawing.Size(469, 155)
        Me.Xl_TarifaProveidor1.TabIndex = 1
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(319, 6)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.TabControl1)
        Me.Panel3.Controls.Add(Me.ProgressBar1)
        Me.Panel3.Location = New System.Drawing.Point(0, 37)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(480, 238)
        Me.Panel3.TabIndex = 5
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 215)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(480, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        Me.ProgressBar1.Visible = False
        '
        'Frm_Proveidor_PriceLists
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 275)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Proveidor_PriceLists"
        Me.Text = "Tarifes de preus"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_PriceLists_Suppliers1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_TarifaProveidor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_PriceLists_Suppliers1 As Xl_PriceLists_Suppliers
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_TarifaProveidor1 As Xl_TarifaProveidor
    Friend WithEvents Xl_TextboxSearchHistorial As Xl_TextboxSearch
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelTarifaVigentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
