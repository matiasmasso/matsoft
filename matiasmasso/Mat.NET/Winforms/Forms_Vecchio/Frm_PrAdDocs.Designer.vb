<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrAdDocs
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
        Me.Xl_PrAds1 = New Winforms.Xl_PrAds()
        Me.Xl_PrAdDocs1 = New Winforms.Xl_PrAdDocs()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_PrAds1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PrAdDocs1)
        Me.SplitContainer1.Size = New System.Drawing.Size(567, 264)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_PrAds1
        '
        Me.Xl_PrAds1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrAds1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PrAds1.Name = "Xl_PrAds1"
        Me.Xl_PrAds1.Size = New System.Drawing.Size(453, 264)
        Me.Xl_PrAds1.TabIndex = 0
        '
        'Xl_PrAdDocs1
        '
        Me.Xl_PrAdDocs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrAdDocs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PrAdDocs1.Name = "Xl_PrAdDocs1"
        Me.Xl_PrAdDocs1.Size = New System.Drawing.Size(110, 264)
        Me.Xl_PrAdDocs1.TabIndex = 0
        '
        'Frm_PrAdDocs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 264)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_PrAdDocs"
        Me.Text = "ANUNCIS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_PrAds1 As Winforms.Xl_PrAds
    Friend WithEvents Xl_PrAdDocs1 As Winforms.Xl_PrAdDocs
End Class
