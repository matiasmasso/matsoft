<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MadeIns
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
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Countries1 = New Mat.Net.Xl_Countries()
        Me.Xl_ProductSkus1 = New Mat.Net.Xl_ProductSkus()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_ProductSkus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
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
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(118, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Countries1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ProductSkus1)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 400)
        Me.SplitContainer1.SplitterDistance = 138
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_Countries1
        '
        Me.Xl_Countries1.AllowUserToAddRows = False
        Me.Xl_Countries1.AllowUserToDeleteRows = False
        Me.Xl_Countries1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Countries1.DisplayObsolets = False
        Me.Xl_Countries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Countries1.Filter = Nothing
        Me.Xl_Countries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Countries1.MouseIsDown = False
        Me.Xl_Countries1.Name = "Xl_Countries1"
        Me.Xl_Countries1.ReadOnly = True
        Me.Xl_Countries1.Size = New System.Drawing.Size(138, 400)
        Me.Xl_Countries1.TabIndex = 0
        '
        'Xl_ProductSkus1
        '
        Me.Xl_ProductSkus1.AllowUserToAddRows = False
        Me.Xl_ProductSkus1.AllowUserToDeleteRows = False
        Me.Xl_ProductSkus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkus1.DisplayObsolets = False
        Me.Xl_ProductSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkus1.Filter = Nothing
        Me.Xl_ProductSkus1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductSkus1.MouseIsDown = False
        Me.Xl_ProductSkus1.Name = "Xl_ProductSkus1"
        Me.Xl_ProductSkus1.ReadOnly = True
        Me.Xl_ProductSkus1.Size = New System.Drawing.Size(658, 400)
        Me.Xl_ProductSkus1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 423)
        Me.Panel1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 400)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(800, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_MadeIns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_MadeIns"
        Me.Text = "Origens de producte"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_Countries1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_ProductSkus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_Countries1 As Xl_Countries
    Friend WithEvents Xl_ProductSkus1 As Xl_ProductSkus
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
