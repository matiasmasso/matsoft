<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CatalogImport
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
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ButtonAddField = New System.Windows.Forms.Button()
        Me.ComboBoxSkuField = New System.Windows.Forms.ComboBox()
        Me.ComboBoxColHeader = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxHeadersFromRow = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_CatalegExcelMap1 = New Winforms.Xl_CatalegExcelMap()
        Me.Xl_ExcelValidationResults1 = New Winforms.Xl_ExcelValidationResults()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        CType(Me.Xl_CatalegExcelMap1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_ExcelValidationResults1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(467, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 35)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(467, 350)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ButtonAddField)
        Me.TabPage1.Controls.Add(Me.ComboBoxSkuField)
        Me.TabPage1.Controls.Add(Me.ComboBoxColHeader)
        Me.TabPage1.Controls.Add(Me.Xl_CatalegExcelMap1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.ComboBoxHeadersFromRow)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 324)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Mapeig de camps"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ButtonAddField
        '
        Me.ButtonAddField.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddField.Location = New System.Drawing.Point(370, 53)
        Me.ButtonAddField.Name = "ButtonAddField"
        Me.ButtonAddField.Size = New System.Drawing.Size(81, 23)
        Me.ButtonAddField.TabIndex = 5
        Me.ButtonAddField.Text = "afegir"
        Me.ButtonAddField.UseVisualStyleBackColor = True
        '
        'ComboBoxSkuField
        '
        Me.ComboBoxSkuField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSkuField.FormattingEnabled = True
        Me.ComboBoxSkuField.Location = New System.Drawing.Point(199, 53)
        Me.ComboBoxSkuField.Name = "ComboBoxSkuField"
        Me.ComboBoxSkuField.Size = New System.Drawing.Size(165, 21)
        Me.ComboBoxSkuField.TabIndex = 4
        '
        'ComboBoxColHeader
        '
        Me.ComboBoxColHeader.FormattingEnabled = True
        Me.ComboBoxColHeader.Location = New System.Drawing.Point(10, 53)
        Me.ComboBoxColHeader.Name = "ComboBoxColHeader"
        Me.ComboBoxColHeader.Size = New System.Drawing.Size(183, 21)
        Me.ComboBoxColHeader.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(227, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Llegeix les capçaleres de la fila"
        '
        'ComboBoxHeadersFromRow
        '
        Me.ComboBoxHeadersFromRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxHeadersFromRow.FormattingEnabled = True
        Me.ComboBoxHeadersFromRow.Location = New System.Drawing.Point(386, 6)
        Me.ComboBoxHeadersFromRow.Name = "ComboBoxHeadersFromRow"
        Me.ComboBoxHeadersFromRow.Size = New System.Drawing.Size(67, 21)
        Me.ComboBoxHeadersFromRow.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_ExcelValidationResults1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 324)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Resultats"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 387)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(467, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(248, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(359, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_CatalegExcelMap1
        '
        Me.Xl_CatalegExcelMap1.AllowUserToAddRows = False
        Me.Xl_CatalegExcelMap1.AllowUserToDeleteRows = False
        Me.Xl_CatalegExcelMap1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CatalegExcelMap1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CatalegExcelMap1.DisplayObsolets = False
        Me.Xl_CatalegExcelMap1.Location = New System.Drawing.Point(0, 83)
        Me.Xl_CatalegExcelMap1.MouseIsDown = False
        Me.Xl_CatalegExcelMap1.Name = "Xl_CatalegExcelMap1"
        Me.Xl_CatalegExcelMap1.ReadOnly = True
        Me.Xl_CatalegExcelMap1.Size = New System.Drawing.Size(453, 238)
        Me.Xl_CatalegExcelMap1.TabIndex = 2
        '
        'Xl_ExcelValidationResults1
        '
        Me.Xl_ExcelValidationResults1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ExcelValidationResults1.DisplayObsolets = False
        Me.Xl_ExcelValidationResults1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ExcelValidationResults1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ExcelValidationResults1.MouseIsDown = False
        Me.Xl_ExcelValidationResults1.Name = "Xl_ExcelValidationResults1"
        Me.Xl_ExcelValidationResults1.Size = New System.Drawing.Size(453, 318)
        Me.Xl_ExcelValidationResults1.TabIndex = 0
        '
        'Frm_CatalogImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 418)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_CatalogImport"
        Me.Text = "Importar Excel al catàleg"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.PanelButtons.ResumeLayout(False)
        CType(Me.Xl_CatalegExcelMap1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_ExcelValidationResults1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxHeadersFromRow As ComboBox
    Friend WithEvents Xl_CatalegExcelMap1 As Xl_CatalegExcelMap
    Friend WithEvents ButtonAddField As Button
    Friend WithEvents ComboBoxSkuField As ComboBox
    Friend WithEvents ComboBoxColHeader As ComboBox
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_ExcelValidationResults1 As Xl_ExcelValidationResults
End Class
