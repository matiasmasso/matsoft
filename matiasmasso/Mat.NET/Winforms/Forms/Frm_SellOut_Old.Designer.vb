<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SellOut_Old
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
        Me.Xl_SellOut1 = New Winforms.Xl_SellOut()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.ComboBoxConceptType = New System.Windows.Forms.ComboBox()
        Me.ComboBoxProveidors = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupProduct1 = New Winforms.Xl_LookupProduct()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBoxChannels = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        CType(Me.Xl_SellOut1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_SellOut1
        '
        Me.Xl_SellOut1.AllowUserToAddRows = False
        Me.Xl_SellOut1.AllowUserToDeleteRows = False
        Me.Xl_SellOut1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SellOut1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SellOut1.DisplayObsolets = False
        Me.Xl_SellOut1.Location = New System.Drawing.Point(-1, 47)
        Me.Xl_SellOut1.MouseIsDown = False
        Me.Xl_SellOut1.Name = "Xl_SellOut1"
        Me.Xl_SellOut1.ReadOnly = True
        Me.Xl_SellOut1.Size = New System.Drawing.Size(1226, 214)
        Me.Xl_SellOut1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(59, 8)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'ComboBoxConceptType
        '
        Me.ComboBoxConceptType.FormattingEnabled = True
        Me.ComboBoxConceptType.Location = New System.Drawing.Point(228, 8)
        Me.ComboBoxConceptType.Name = "ComboBoxConceptType"
        Me.ComboBoxConceptType.Size = New System.Drawing.Size(168, 21)
        Me.ComboBoxConceptType.TabIndex = 2
        '
        'ComboBoxProveidors
        '
        Me.ComboBoxProveidors.FormattingEnabled = True
        Me.ComboBoxProveidors.Location = New System.Drawing.Point(402, 8)
        Me.ComboBoxProveidors.Name = "ComboBoxProveidors"
        Me.ComboBoxProveidors.Size = New System.Drawing.Size(168, 21)
        Me.ComboBoxProveidors.TabIndex = 3
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(819, 8)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupProduct1.TabIndex = 4
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.Area = Nothing
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(1025, 9)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.SelMode = Winforms.Frm_Areas.SelModes.SelectArea
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(200, 20)
        Me.Xl_LookupArea1.TabIndex = 5
        Me.Xl_LookupArea1.Value = Nothing
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1225, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.GroupToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = Global.Winforms.My.Resources.Resources.Excel
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'GroupToolStripMenuItem
        '
        Me.GroupToolStripMenuItem.CheckOnClick = True
        Me.GroupToolStripMenuItem.Name = "GroupToolStripMenuItem"
        Me.GroupToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.GroupToolStripMenuItem.Text = "Agrupar per compte client"
        '
        'ComboBoxChannels
        '
        Me.ComboBoxChannels.FormattingEnabled = True
        Me.ComboBoxChannels.Location = New System.Drawing.Point(576, 8)
        Me.ComboBoxChannels.Name = "ComboBoxChannels"
        Me.ComboBoxChannels.Size = New System.Drawing.Size(168, 21)
        Me.ComboBoxChannels.TabIndex = 7
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Items.AddRange(New Object() {"Imports", "Unitats"})
        Me.ComboBoxFormat.Location = New System.Drawing.Point(750, 8)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(62, 21)
        Me.ComboBoxFormat.TabIndex = 8
        '
        'Frm_SellOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1225, 261)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.ComboBoxChannels)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.ComboBoxProveidors)
        Me.Controls.Add(Me.ComboBoxConceptType)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_SellOut1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_SellOut"
        Me.Text = "Sell-out"
        CType(Me.Xl_SellOut1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_SellOut1 As Xl_SellOut
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents ComboBoxConceptType As ComboBox
    Friend WithEvents ComboBoxProveidors As ComboBox
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComboBoxChannels As ComboBox
    Friend WithEvents ComboBoxFormat As ComboBox
    Friend WithEvents GroupToolStripMenuItem As ToolStripMenuItem
End Class
