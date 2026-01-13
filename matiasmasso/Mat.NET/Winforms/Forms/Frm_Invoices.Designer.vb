<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Invoices
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_InvoicesSummaryYears1 = New Winforms.Xl_InvoicesSummaryYears()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_InvoicesSummaryMonths1 = New Winforms.Xl_InvoicesSummaryMonths()
        Me.Xl_Invoices1 = New Winforms.Xl_Invoices()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.PanelWrap = New System.Windows.Forms.Panel()
        Me.Xl_ProgressBar1 = New Winforms.Xl_ProgressBar()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBoxSeries = New System.Windows.Forms.ComboBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_InvoicesSummaryYears1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_InvoicesSummaryMonths1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Invoices1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelWrap.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_InvoicesSummaryYears1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1023, 448)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 6
        '
        'Xl_InvoicesSummaryYears1
        '
        Me.Xl_InvoicesSummaryYears1.AllowUserToAddRows = False
        Me.Xl_InvoicesSummaryYears1.AllowUserToDeleteRows = False
        Me.Xl_InvoicesSummaryYears1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoicesSummaryYears1.DisplayObsolets = False
        Me.Xl_InvoicesSummaryYears1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InvoicesSummaryYears1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InvoicesSummaryYears1.MouseIsDown = False
        Me.Xl_InvoicesSummaryYears1.Name = "Xl_InvoicesSummaryYears1"
        Me.Xl_InvoicesSummaryYears1.ReadOnly = True
        Me.Xl_InvoicesSummaryYears1.Size = New System.Drawing.Size(150, 448)
        Me.Xl_InvoicesSummaryYears1.TabIndex = 4
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_InvoicesSummaryMonths1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Invoices1)
        Me.SplitContainer2.Size = New System.Drawing.Size(869, 448)
        Me.SplitContainer2.SplitterDistance = 150
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_InvoicesSummaryMonths1
        '
        Me.Xl_InvoicesSummaryMonths1.AllowUserToAddRows = False
        Me.Xl_InvoicesSummaryMonths1.AllowUserToDeleteRows = False
        Me.Xl_InvoicesSummaryMonths1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoicesSummaryMonths1.DisplayObsolets = False
        Me.Xl_InvoicesSummaryMonths1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InvoicesSummaryMonths1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InvoicesSummaryMonths1.MouseIsDown = False
        Me.Xl_InvoicesSummaryMonths1.Name = "Xl_InvoicesSummaryMonths1"
        Me.Xl_InvoicesSummaryMonths1.ReadOnly = True
        Me.Xl_InvoicesSummaryMonths1.Size = New System.Drawing.Size(150, 448)
        Me.Xl_InvoicesSummaryMonths1.TabIndex = 0
        '
        'Xl_Invoices1
        '
        Me.Xl_Invoices1.AllowUserToAddRows = False
        Me.Xl_Invoices1.AllowUserToDeleteRows = False
        Me.Xl_Invoices1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Invoices1.DisplayObsolets = False
        Me.Xl_Invoices1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Invoices1.Filter = Nothing
        Me.Xl_Invoices1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Invoices1.MouseIsDown = False
        Me.Xl_Invoices1.Name = "Xl_Invoices1"
        Me.Xl_Invoices1.ReadOnly = True
        Me.Xl_Invoices1.Size = New System.Drawing.Size(715, 448)
        Me.Xl_Invoices1.TabIndex = 2
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(581, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(443, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'PanelWrap
        '
        Me.PanelWrap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelWrap.Controls.Add(Me.SplitContainer1)
        Me.PanelWrap.Controls.Add(Me.Xl_ProgressBar1)
        Me.PanelWrap.Location = New System.Drawing.Point(1, 38)
        Me.PanelWrap.Name = "PanelWrap"
        Me.PanelWrap.Size = New System.Drawing.Size(1023, 478)
        Me.PanelWrap.TabIndex = 7
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 448)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(1023, 30)
        Me.Xl_ProgressBar1.TabIndex = 13
        Me.Xl_ProgressBar1.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1024, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(101, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ComboBoxSeries
        '
        Me.ComboBoxSeries.FormattingEnabled = True
        Me.ComboBoxSeries.Items.AddRange(New Object() {"standard", "rectificativa", "simplificada", "(totes les series)"})
        Me.ComboBoxSeries.Location = New System.Drawing.Point(309, 10)
        Me.ComboBoxSeries.Name = "ComboBoxSeries"
        Me.ComboBoxSeries.Size = New System.Drawing.Size(266, 21)
        Me.ComboBoxSeries.TabIndex = 9
        '
        'Frm_Invoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 517)
        Me.Controls.Add(Me.ComboBoxSeries)
        Me.Controls.Add(Me.PanelWrap)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Invoices"
        Me.Text = "Llibre de Factures emeses"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_InvoicesSummaryYears1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_InvoicesSummaryMonths1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Invoices1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelWrap.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Invoices1 As Xl_Invoices
    Friend WithEvents Xl_InvoicesSummaryYears1 As Xl_InvoicesSummaryYears
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Xl_InvoicesSummaryMonths1 As Xl_InvoicesSummaryMonths
    Friend WithEvents PanelWrap As Panel
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComboBoxSeries As ComboBox
End Class
