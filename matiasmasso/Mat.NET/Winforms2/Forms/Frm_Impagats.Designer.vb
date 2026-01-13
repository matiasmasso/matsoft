<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Impagats
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ImpagatsSummary1 = New Xl_ImpagatsSummary()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ImpagatsDetail1 = New Xl_ImpagatsDetail()
        Me.Xl_Mems1 = New Xl_Mems()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelTotals = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_ImpagatsSummary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Xl_ImpagatsDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Mems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ImpagatsSummary1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(972, 611)
        Me.SplitContainer1.SplitterDistance = 625
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_ImpagatsSummary1
        '
        Me.Xl_ImpagatsSummary1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImpagatsSummary1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ImpagatsSummary1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ImpagatsSummary1.Name = "Xl_ImpagatsSummary1"
        Me.Xl_ImpagatsSummary1.Size = New System.Drawing.Size(625, 586)
        Me.Xl_ImpagatsSummary1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_ImpagatsDetail1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Mems1)
        Me.SplitContainer2.Size = New System.Drawing.Size(343, 611)
        Me.SplitContainer2.SplitterDistance = 305
        Me.SplitContainer2.TabIndex = 2
        '
        'Xl_ImpagatsDetail1
        '
        Me.Xl_ImpagatsDetail1.AllowUserToAddRows = False
        Me.Xl_ImpagatsDetail1.AllowUserToDeleteRows = False
        Me.Xl_ImpagatsDetail1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ImpagatsDetail1.DisplayObsolets = False
        Me.Xl_ImpagatsDetail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ImpagatsDetail1.Filter = Nothing
        Me.Xl_ImpagatsDetail1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ImpagatsDetail1.Name = "Xl_ImpagatsDetail1"
        Me.Xl_ImpagatsDetail1.ReadOnly = True
        Me.Xl_ImpagatsDetail1.Size = New System.Drawing.Size(343, 305)
        Me.Xl_ImpagatsDetail1.TabIndex = 0
        '
        'Xl_Mems1
        '
        Me.Xl_Mems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Mems1.Filter = Nothing
        Me.Xl_Mems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Mems1.Name = "Xl_Mems1"
        Me.Xl_Mems1.Size = New System.Drawing.Size(343, 302)
        Me.Xl_Mems1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelTotals})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 589)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(625, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelTotals
        '
        Me.ToolStripStatusLabelTotals.Name = "ToolStripStatusLabelTotals"
        Me.ToolStripStatusLabelTotals.Size = New System.Drawing.Size(33, 17)
        Me.ToolStripStatusLabelTotals.Text = "Total"
        '
        'Frm_Impagats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 611)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Impagats"
        Me.Text = "IMPAGATS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_ImpagatsSummary1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Xl_ImpagatsDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Mems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Mems1 As Xl_Mems
    Friend WithEvents Xl_ImpagatsSummary1 As Xl_ImpagatsSummary
    Friend WithEvents Xl_ImpagatsDetail1 As Xl_ImpagatsDetail
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabelTotals As ToolStripStatusLabel
End Class
