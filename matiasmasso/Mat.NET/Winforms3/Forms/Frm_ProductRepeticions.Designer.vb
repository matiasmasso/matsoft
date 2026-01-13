<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductRepeticions
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ProductRepeticionsParent1 = New Xl_ProductRepeticionsParent()
        Me.Xl_ProductRepeticionsChildren1 = New Xl_ProductRepeticionsChildren()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_ProductRepeticionsParent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_ProductRepeticionsChildren1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(873, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ProductRepeticionsParent1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ProductRepeticionsChildren1)
        Me.SplitContainer1.Size = New System.Drawing.Size(873, 237)
        Me.SplitContainer1.SplitterDistance = 356
        Me.SplitContainer1.TabIndex = 2
        '
        'Xl_ProductRepeticionsParent1
        '
        Me.Xl_ProductRepeticionsParent1.AllowUserToAddRows = False
        Me.Xl_ProductRepeticionsParent1.AllowUserToDeleteRows = False
        Me.Xl_ProductRepeticionsParent1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductRepeticionsParent1.DisplayObsolets = False
        Me.Xl_ProductRepeticionsParent1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductRepeticionsParent1.Filter = Nothing
        Me.Xl_ProductRepeticionsParent1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductRepeticionsParent1.Name = "Xl_ProductRepeticionsParent1"
        Me.Xl_ProductRepeticionsParent1.ReadOnly = True
        Me.Xl_ProductRepeticionsParent1.Size = New System.Drawing.Size(356, 237)
        Me.Xl_ProductRepeticionsParent1.TabIndex = 0
        '
        'Xl_ProductRepeticionsChildren1
        '
        Me.Xl_ProductRepeticionsChildren1.AllowUserToAddRows = False
        Me.Xl_ProductRepeticionsChildren1.AllowUserToDeleteRows = False
        Me.Xl_ProductRepeticionsChildren1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductRepeticionsChildren1.DisplayObsolets = False
        Me.Xl_ProductRepeticionsChildren1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductRepeticionsChildren1.Filter = Nothing
        Me.Xl_ProductRepeticionsChildren1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductRepeticionsChildren1.Name = "Xl_ProductRepeticionsChildren1"
        Me.Xl_ProductRepeticionsChildren1.ReadOnly = True
        Me.Xl_ProductRepeticionsChildren1.Size = New System.Drawing.Size(513, 237)
        Me.Xl_ProductRepeticionsChildren1.TabIndex = 0
        '
        'Frm_ProductRepeticions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_ProductRepeticions"
        Me.Text = "Repeticions de producte"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_ProductRepeticionsParent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_ProductRepeticionsChildren1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_ProductRepeticionsParent1 As Xl_ProductRepeticionsParent
    Friend WithEvents Xl_ProductRepeticionsChildren1 As Xl_ProductRepeticionsChildren
End Class
