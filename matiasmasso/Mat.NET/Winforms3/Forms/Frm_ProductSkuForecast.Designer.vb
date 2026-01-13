<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductSkuForecast
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
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelComandaPerCategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstimationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComandaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Forecasts1 = New Xl_Forecasts()
        Me.Xl_Years1 = New Xl_Years()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumericUpDownM3 = New System.Windows.Forms.NumericUpDown()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ProductCategoriesCheckedList1 = New Xl_ProductCategoriesCheckedList()
        Me.Xl_ProductSkuForecasts1 = New Xl_ProductSkuForecasts()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Xl_Forecasts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.NumericUpDownM3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_ProductCategoriesCheckedList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_ProductSkuForecasts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(2, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(999, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.ExcelComandaPerCategoriesToolStripMenuItem, Me.EstimationToolStripMenuItem, Me.ComandaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel Forecast"
        '
        'ExcelComandaPerCategoriesToolStripMenuItem
        '
        Me.ExcelComandaPerCategoriesToolStripMenuItem.Name = "ExcelComandaPerCategoriesToolStripMenuItem"
        Me.ExcelComandaPerCategoriesToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.ExcelComandaPerCategoriesToolStripMenuItem.Text = "Excel comanda per categories"
        '
        'EstimationToolStripMenuItem
        '
        Me.EstimationToolStripMenuItem.Name = "EstimationToolStripMenuItem"
        Me.EstimationToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.EstimationToolStripMenuItem.Text = "Estimation"
        '
        'ComandaToolStripMenuItem
        '
        Me.ComandaToolStripMenuItem.Name = "ComandaToolStripMenuItem"
        Me.ComandaToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.ComandaToolStripMenuItem.Text = "Comanda"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(999, 379)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Forecasts1)
        Me.TabPage1.Controls.Add(Me.Xl_Years1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Size = New System.Drawing.Size(991, 373)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Forecast"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Forecasts1
        '
        Me.Xl_Forecasts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Forecasts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Forecasts1.DisplayObsolets = False
        Me.Xl_Forecasts1.Location = New System.Drawing.Point(4, 30)
        Me.Xl_Forecasts1.MouseIsDown = False
        Me.Xl_Forecasts1.Name = "Xl_Forecasts1"
        Me.Xl_Forecasts1.Size = New System.Drawing.Size(983, 340)
        Me.Xl_Forecasts1.TabIndex = 2
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(2, 3)
        Me.Xl_Years1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.NumericUpDownM3)
        Me.TabPage2.Controls.Add(Me.DateTimePicker1)
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Size = New System.Drawing.Size(991, 353)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Comanda"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(804, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Data sortida"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(399, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Volum m3"
        '
        'NumericUpDownM3
        '
        Me.NumericUpDownM3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownM3.Location = New System.Drawing.Point(458, 5)
        Me.NumericUpDownM3.Name = "NumericUpDownM3"
        Me.NumericUpDownM3.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownM3.TabIndex = 5
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(874, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(90, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 30)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ProductCategoriesCheckedList1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ProductSkuForecasts1)
        Me.SplitContainer1.Size = New System.Drawing.Size(985, 319)
        Me.SplitContainer1.SplitterDistance = 235
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_ProductCategoriesCheckedList1
        '
        Me.Xl_ProductCategoriesCheckedList1.AllowUserToAddRows = False
        Me.Xl_ProductCategoriesCheckedList1.AllowUserToDeleteRows = False
        Me.Xl_ProductCategoriesCheckedList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductCategoriesCheckedList1.DisplayObsolets = False
        Me.Xl_ProductCategoriesCheckedList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategoriesCheckedList1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductCategoriesCheckedList1.MouseIsDown = False
        Me.Xl_ProductCategoriesCheckedList1.Name = "Xl_ProductCategoriesCheckedList1"
        Me.Xl_ProductCategoriesCheckedList1.ReadOnly = True
        Me.Xl_ProductCategoriesCheckedList1.Size = New System.Drawing.Size(235, 319)
        Me.Xl_ProductCategoriesCheckedList1.TabIndex = 0
        '
        'Xl_ProductSkuForecasts1
        '
        Me.Xl_ProductSkuForecasts1.AllowUserToAddRows = False
        Me.Xl_ProductSkuForecasts1.AllowUserToDeleteRows = False
        Me.Xl_ProductSkuForecasts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkuForecasts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkuForecasts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductSkuForecasts1.Name = "Xl_ProductSkuForecasts1"
        Me.Xl_ProductSkuForecasts1.ReadOnly = True
        Me.Xl_ProductSkuForecasts1.Size = New System.Drawing.Size(746, 319)
        Me.Xl_ProductSkuForecasts1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(999, 402)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 379)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(999, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_ProductSkuForecast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(999, 430)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "Frm_ProductSkuForecast"
        Me.Text = "Forecast"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Xl_Forecasts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.NumericUpDownM3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_ProductCategoriesCheckedList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_ProductSkuForecasts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EstimationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ProductCategoriesCheckedList1 As Xl_ProductCategoriesCheckedList
    Friend WithEvents Xl_ProductSkuForecasts1 As Xl_ProductSkuForecasts
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents NumericUpDownM3 As NumericUpDown
    Friend WithEvents Xl_Forecasts1 As Xl_Forecasts
    Friend WithEvents ComandaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ExcelComandaPerCategoriesToolStripMenuItem As ToolStripMenuItem
End Class
