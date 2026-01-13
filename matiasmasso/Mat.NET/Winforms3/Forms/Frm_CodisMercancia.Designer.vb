<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CodisMercancia
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
        Me.Xl_CodisMercancia1 = New Xl_CodisMercancia()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NouCodiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.Xl_CodisMercancia1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_CodisMercancia1
        '
        Me.Xl_CodisMercancia1.AllowUserToAddRows = False
        Me.Xl_CodisMercancia1.AllowUserToDeleteRows = False
        Me.Xl_CodisMercancia1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CodisMercancia1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CodisMercancia1.DisplayObsolets = False
        Me.Xl_CodisMercancia1.Filter = Nothing
        Me.Xl_CodisMercancia1.Location = New System.Drawing.Point(0, 28)
        Me.Xl_CodisMercancia1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_CodisMercancia1.MouseIsDown = False
        Me.Xl_CodisMercancia1.Name = "Xl_CodisMercancia1"
        Me.Xl_CodisMercancia1.ReadOnly = True
        Me.Xl_CodisMercancia1.Size = New System.Drawing.Size(292, 236)
        Me.Xl_CodisMercancia1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(142, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(2, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(292, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NouCodiToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 22)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'NouCodiToolStripMenuItem
        '
        Me.NouCodiToolStripMenuItem.Name = "NouCodiToolStripMenuItem"
        Me.NouCodiToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.NouCodiToolStripMenuItem.Text = "Nou codi"
        '
        'Frm_CodisMercancia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 264)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_CodisMercancia1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_CodisMercancia"
        Me.Text = "Codis arancelaris"
        CType(Me.Xl_CodisMercancia1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_CodisMercancia1 As Xl_CodisMercancia
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NouCodiToolStripMenuItem As ToolStripMenuItem
End Class
