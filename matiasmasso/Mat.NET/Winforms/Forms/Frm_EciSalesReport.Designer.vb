<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EciSalesReport
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
        Me.Xl_StatMonths1 = New Winforms.Xl_StatMonths()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.CheckBoxProductFilter = New System.Windows.Forms.CheckBox()
        Me.ComboBoxUnits = New System.Windows.Forms.ComboBox()
        Me.ComboBoxConceptType = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_StatMonths1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_StatMonths1
        '
        Me.Xl_StatMonths1.AllowUserToAddRows = False
        Me.Xl_StatMonths1.AllowUserToDeleteRows = False
        Me.Xl_StatMonths1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StatMonths1.DisplayObsolets = False
        Me.Xl_StatMonths1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StatMonths1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StatMonths1.MouseIsDown = False
        Me.Xl_StatMonths1.Name = "Xl_StatMonths1"
        Me.Xl_StatMonths1.ReadOnly = True
        Me.Xl_StatMonths1.Size = New System.Drawing.Size(1227, 357)
        Me.Xl_StatMonths1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(204, 4)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(1026, 6)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupProduct1.TabIndex = 2
        Me.Xl_LookupProduct1.Value = Nothing
        Me.Xl_LookupProduct1.Visible = False
        '
        'CheckBoxProductFilter
        '
        Me.CheckBoxProductFilter.AutoSize = True
        Me.CheckBoxProductFilter.Location = New System.Drawing.Point(908, 8)
        Me.CheckBoxProductFilter.Name = "CheckBoxProductFilter"
        Me.CheckBoxProductFilter.Size = New System.Drawing.Size(114, 17)
        Me.CheckBoxProductFilter.TabIndex = 3
        Me.CheckBoxProductFilter.Text = "Filtrar per producte"
        Me.CheckBoxProductFilter.UseVisualStyleBackColor = True
        '
        'ComboBoxUnits
        '
        Me.ComboBoxUnits.FormattingEnabled = True
        Me.ComboBoxUnits.Items.AddRange(New Object() {"Imports", "Unitats"})
        Me.ComboBoxUnits.Location = New System.Drawing.Point(545, 4)
        Me.ComboBoxUnits.Name = "ComboBoxUnits"
        Me.ComboBoxUnits.Size = New System.Drawing.Size(145, 21)
        Me.ComboBoxUnits.TabIndex = 4
        '
        'ComboBoxConceptType
        '
        Me.ComboBoxConceptType.FormattingEnabled = True
        Me.ComboBoxConceptType.Items.AddRange(New Object() {"Productes", "Centres"})
        Me.ComboBoxConceptType.Location = New System.Drawing.Point(394, 4)
        Me.ComboBoxConceptType.Name = "ComboBoxConceptType"
        Me.ComboBoxConceptType.Size = New System.Drawing.Size(145, 21)
        Me.ComboBoxConceptType.TabIndex = 5
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1227, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = Global.Winforms.My.Resources.Resources.Excel
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_StatMonths1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1227, 380)
        Me.Panel1.TabIndex = 8
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 357)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1227, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_EciSalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1227, 415)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBoxConceptType)
        Me.Controls.Add(Me.ComboBoxUnits)
        Me.Controls.Add(Me.CheckBoxProductFilter)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_EciSalesReport"
        Me.Text = "ECI Sellout"
        CType(Me.Xl_StatMonths1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_StatMonths1 As Xl_StatMonths
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents CheckBoxProductFilter As CheckBox
    Friend WithEvents ComboBoxUnits As ComboBox
    Friend WithEvents ComboBoxConceptType As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
