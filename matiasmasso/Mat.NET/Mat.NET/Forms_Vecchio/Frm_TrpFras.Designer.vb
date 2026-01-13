<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_TrpFras
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_TrpFras))
        Me.PictureBoxTrpLogo = New System.Windows.Forms.PictureBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonZoom = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonDel = New System.Windows.Forms.ToolStripButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        CType(Me.PictureBoxTrpLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxTrpLogo
        '
        Me.PictureBoxTrpLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxTrpLogo.Location = New System.Drawing.Point(405, 0)
        Me.PictureBoxTrpLogo.Name = "PictureBoxTrpLogo"
        Me.PictureBoxTrpLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxTrpLogo.TabIndex = 8
        Me.PictureBoxTrpLogo.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonZoom, Me.ToolStripButtonNew, Me.ToolStripButtonDel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(555, 25)
        Me.ToolStrip1.TabIndex = 9
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonZoom
        '
        Me.ToolStripButtonZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonZoom.Enabled = False
        Me.ToolStripButtonZoom.Image = CType(resources.GetObject("ToolStripButtonZoom.Image"), System.Drawing.Image)
        Me.ToolStripButtonZoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonZoom.Name = "ToolStripButtonZoom"
        Me.ToolStripButtonZoom.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonZoom.Text = "Zoom"
        Me.ToolStripButtonZoom.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripButtonNew
        '
        Me.ToolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonNew.Image = CType(resources.GetObject("ToolStripButtonNew.Image"), System.Drawing.Image)
        Me.ToolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonNew.Name = "ToolStripButtonNew"
        Me.ToolStripButtonNew.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonNew.Text = "afegir"
        '
        'ToolStripButtonDel
        '
        Me.ToolStripButtonDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonDel.Image = CType(resources.GetObject("ToolStripButtonDel.Image"), System.Drawing.Image)
        Me.ToolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDel.Name = "ToolStripButtonDel"
        Me.ToolStripButtonDel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonDel.Text = "eliminar"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 60)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(555, 206)
        Me.DataGridView1.TabIndex = 10
        '
        'Frm_TrpFras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBoxTrpLogo)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_TrpFras"
        Me.Text = "FACTURES TRANSPORT"
        CType(Me.PictureBoxTrpLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxTrpLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonZoom As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonDel As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
