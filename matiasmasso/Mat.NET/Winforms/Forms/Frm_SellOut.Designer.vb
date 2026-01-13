<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SellOut
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        Me.ComboBoxConceptType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.Xl_SelloutFilters1 = New Winforms.Xl_SelloutFilters()
        Me.Xl_SellOut1 = New Winforms.Xl_SellOut()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_SelloutFilters1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_SellOut1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1412, 24)
        Me.MenuStrip1.TabIndex = 0
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
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 43)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_SelloutFilters1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_SellOut1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1412, 218)
        Me.SplitContainer1.SplitterDistance = 179
        Me.SplitContainer1.TabIndex = 1
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Items.AddRange(New Object() {"Imports", "Unitats"})
        Me.ComboBoxFormat.Location = New System.Drawing.Point(728, 3)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxFormat.TabIndex = 11
        '
        'ComboBoxConceptType
        '
        Me.ComboBoxConceptType.FormattingEnabled = True
        Me.ComboBoxConceptType.Location = New System.Drawing.Point(495, 3)
        Me.ComboBoxConceptType.Name = "ComboBoxConceptType"
        Me.ComboBoxConceptType.Size = New System.Drawing.Size(168, 21)
        Me.ComboBoxConceptType.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(436, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Concepte"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(683, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Format"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(187, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Exercici"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(246, 1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 9
        Me.Xl_Years1.Value = 0
        '
        'Xl_SelloutFilters1
        '
        Me.Xl_SelloutFilters1.AllowUserToAddRows = False
        Me.Xl_SelloutFilters1.AllowUserToDeleteRows = False
        Me.Xl_SelloutFilters1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SelloutFilters1.DisplayObsolets = False
        Me.Xl_SelloutFilters1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SelloutFilters1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SelloutFilters1.MouseIsDown = False
        Me.Xl_SelloutFilters1.Name = "Xl_SelloutFilters1"
        Me.Xl_SelloutFilters1.ReadOnly = True
        Me.Xl_SelloutFilters1.Size = New System.Drawing.Size(179, 218)
        Me.Xl_SelloutFilters1.TabIndex = 0
        '
        'Xl_SellOut1
        '
        Me.Xl_SellOut1.AllowUserToAddRows = False
        Me.Xl_SellOut1.AllowUserToDeleteRows = False
        Me.Xl_SellOut1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SellOut1.DisplayObsolets = False
        Me.Xl_SellOut1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SellOut1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SellOut1.MouseIsDown = False
        Me.Xl_SellOut1.Name = "Xl_SellOut1"
        Me.Xl_SellOut1.ReadOnly = True
        Me.Xl_SellOut1.Size = New System.Drawing.Size(1229, 218)
        Me.Xl_SellOut1.TabIndex = 0
        '
        'Frm_SellOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1412, 261)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.ComboBoxConceptType)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_SellOut"
        Me.Text = "Sell-Out"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_SelloutFilters1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_SellOut1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_SelloutFilters1 As Xl_SelloutFilters
    Friend WithEvents Xl_SellOut1 As Xl_SellOut
    Friend WithEvents ComboBoxFormat As ComboBox
    Friend WithEvents ComboBoxConceptType As ComboBox
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
