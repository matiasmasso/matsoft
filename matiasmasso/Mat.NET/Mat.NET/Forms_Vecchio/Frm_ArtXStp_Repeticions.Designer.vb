<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ArtXStp_Repeticions
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewHdr = New System.Windows.Forms.DataGridView
        Me.DataGridViewDtl = New System.Windows.Forms.DataGridView
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewHdr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewDtl, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewHdr)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewDtl)
        Me.SplitContainer1.Size = New System.Drawing.Size(859, 264)
        Me.SplitContainer1.SplitterDistance = 351
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridViewHdr
        '
        Me.DataGridViewHdr.AllowUserToAddRows = False
        Me.DataGridViewHdr.AllowUserToDeleteRows = False
        Me.DataGridViewHdr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewHdr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewHdr.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewHdr.Name = "DataGridViewHdr"
        Me.DataGridViewHdr.ReadOnly = True
        Me.DataGridViewHdr.Size = New System.Drawing.Size(351, 264)
        Me.DataGridViewHdr.TabIndex = 0
        '
        'DataGridViewDtl
        '
        Me.DataGridViewDtl.AllowUserToAddRows = False
        Me.DataGridViewDtl.AllowUserToDeleteRows = False
        Me.DataGridViewDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDtl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewDtl.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewDtl.Name = "DataGridViewDtl"
        Me.DataGridViewDtl.ReadOnly = True
        Me.DataGridViewDtl.Size = New System.Drawing.Size(504, 264)
        Me.DataGridViewDtl.TabIndex = 1
        '
        'Frm_ArtXStp_Repeticions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 264)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_ArtXStp_Repeticions"
        Me.Text = "REPETICIONS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewHdr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewDtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewHdr As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewDtl As System.Windows.Forms.DataGridView
End Class
