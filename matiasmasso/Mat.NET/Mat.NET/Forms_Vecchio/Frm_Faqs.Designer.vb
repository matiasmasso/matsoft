<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Faqs
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
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AfegirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PujaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BaixaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.AllowDrop = True
        Me.TreeView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(398, 404)
        Me.TreeView1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfegirToolStripMenuItem, Me.EliminarToolStripMenuItem, Me.ZoomToolStripMenuItem, Me.PujaToolStripMenuItem, Me.BaixaToolStripMenuItem, Me.CopyLinkToolStripMenuItem, Me.WebToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 180)
        '
        'AfegirToolStripMenuItem
        '
        Me.AfegirToolStripMenuItem.Name = "AfegirToolStripMenuItem"
        Me.AfegirToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AfegirToolStripMenuItem.Text = "afegir"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EliminarToolStripMenuItem.Text = "eliminar"
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ZoomToolStripMenuItem.Text = "zoom"
        '
        'PujaToolStripMenuItem
        '
        Me.PujaToolStripMenuItem.Name = "PujaToolStripMenuItem"
        Me.PujaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PujaToolStripMenuItem.Text = "puja"
        '
        'BaixaToolStripMenuItem
        '
        Me.BaixaToolStripMenuItem.Name = "BaixaToolStripMenuItem"
        Me.BaixaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.BaixaToolStripMenuItem.Text = "baixa"
        '
        'CopyLinkToolStripMenuItem
        '
        Me.CopyLinkToolStripMenuItem.Image = My.Resources.Resources.Copy
        Me.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem"
        Me.CopyLinkToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CopyLinkToolStripMenuItem.Text = "copiar enllaç"
        '
        'WebToolStripMenuItem
        '
        Me.WebToolStripMenuItem.Image = My.Resources.Resources.iExplorer
        Me.WebToolStripMenuItem.Name = "WebToolStripMenuItem"
        Me.WebToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.WebToolStripMenuItem.Text = "web"
        '
        'Frm_Faqs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 404)
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "Frm_Faqs"
        Me.Text = "FAQS"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AfegirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PujaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BaixaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
