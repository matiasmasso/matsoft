<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AeatMod347
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
        Me.Xl_AeatMod347_items1 = New Mat.NET.Xl_AeatMod347_items()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesarFitxerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.ExportarACsvToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_AeatMod347_items1
        '
        Me.Xl_AeatMod347_items1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AeatMod347_items1.Filter = Nothing
        Me.Xl_AeatMod347_items1.Location = New System.Drawing.Point(1, 28)
        Me.Xl_AeatMod347_items1.Name = "Xl_AeatMod347_items1"
        Me.Xl_AeatMod347_items1.Size = New System.Drawing.Size(800, 360)
        Me.Xl_AeatMod347_items1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(802, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DesarFitxerToolStripMenuItem, Me.ExportarACsvToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(46, 20)
        Me.ToolStripMenuItem1.Text = "Arxiu"
        '
        'DesarFitxerToolStripMenuItem
        '
        Me.DesarFitxerToolStripMenuItem.Name = "DesarFitxerToolStripMenuItem"
        Me.DesarFitxerToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DesarFitxerToolStripMenuItem.Text = "Desar fitxer Hisenda"
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.Location = New System.Drawing.Point(573, 2)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(228, 20)
        Me.TextBoxSearch.TabIndex = 2
        '
        'ExportarACsvToolStripMenuItem
        '
        Me.ExportarACsvToolStripMenuItem.Name = "ExportarACsvToolStripMenuItem"
        Me.ExportarACsvToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.ExportarACsvToolStripMenuItem.Text = "Exportar a Csv"
        '
        'Frm_AeatMod347
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 389)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.Xl_AeatMod347_items1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_AeatMod347"
        Me.Text = "Hisenda - Model 347"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_AeatMod347_items1 As Mat.NET.Xl_AeatMod347_items
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesarFitxerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
    Friend WithEvents ExportarACsvToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
