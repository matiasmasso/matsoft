<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_NUK
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_NUK))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.PanelHost = New System.Windows.Forms.Panel
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.StatusStripTop = New System.Windows.Forms.StatusStrip
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripStatusLabelTit = New System.Windows.Forms.ToolStripStatusLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.StatusStripTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStripTop)
        Me.SplitContainer1.Size = New System.Drawing.Size(855, 526)
        Me.SplitContainer1.SplitterDistance = 285
        Me.SplitContainer1.TabIndex = 0
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(285, 526)
        Me.TreeView1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Logo-NUK-16x16.gif")
        Me.ImageList1.Images.SetKeyName(1, "folder.gif")
        Me.ImageList1.Images.SetKeyName(2, "clock.gif")
        Me.ImageList1.Images.SetKeyName(3, "warn.gif")
        Me.ImageList1.Images.SetKeyName(4, "WarnEmpty.gif")
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 22)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.PanelHost)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.PropertyGrid1)
        Me.SplitContainer2.Size = New System.Drawing.Size(566, 504)
        Me.SplitContainer2.SplitterDistance = 298
        Me.SplitContainer2.TabIndex = 2
        '
        'PanelHost
        '
        Me.PanelHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelHost.Location = New System.Drawing.Point(0, 0)
        Me.PanelHost.Name = "PanelHost"
        Me.PanelHost.Size = New System.Drawing.Size(566, 298)
        Me.PanelHost.TabIndex = 0
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(566, 202)
        Me.PropertyGrid1.TabIndex = 0
        '
        'StatusStripTop
        '
        Me.StatusStripTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStripTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelTit, Me.ToolStripProgressBar1})
        Me.StatusStripTop.Location = New System.Drawing.Point(0, 0)
        Me.StatusStripTop.Name = "StatusStripTop"
        Me.StatusStripTop.Size = New System.Drawing.Size(566, 22)
        Me.StatusStripTop.TabIndex = 0
        Me.StatusStripTop.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(250, 16)
        '
        'ToolStripStatusLabelTit
        '
        Me.ToolStripStatusLabelTit.Name = "ToolStripStatusLabelTit"
        Me.ToolStripStatusLabelTit.Size = New System.Drawing.Size(36, 17)
        Me.ToolStripStatusLabelTit.Text = "(titol)"
        '
        'Frm_NUK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 526)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_NUK"
        Me.Text = "NUK"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.StatusStripTop.ResumeLayout(False)
        Me.StatusStripTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents StatusStripTop As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents PanelHost As System.Windows.Forms.Panel
    Friend WithEvents ToolStripStatusLabelTit As System.Windows.Forms.ToolStripStatusLabel
End Class
