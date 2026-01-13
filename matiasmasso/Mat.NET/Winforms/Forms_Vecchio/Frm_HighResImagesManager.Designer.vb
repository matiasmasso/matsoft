<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_HighResImagesManager
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
        Me.Xl_HighResImages1 = New Winforms.Xl_HighResImages()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_FtpExplorer1 = New Winforms.Xl_FtpExplorer()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_HighResImages1
        '
        Me.Xl_HighResImages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_HighResImages1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_HighResImages1.Name = "Xl_HighResImages1"
        Me.Xl_HighResImages1.Size = New System.Drawing.Size(430, 348)
        Me.Xl_HighResImages1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_FtpExplorer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_HighResImages1)
        Me.SplitContainer1.Size = New System.Drawing.Size(650, 348)
        Me.SplitContainer1.SplitterDistance = 216
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_FtpExplorer1
        '
        Me.Xl_FtpExplorer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_FtpExplorer1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_FtpExplorer1.Name = "Xl_FtpExplorer1"
        Me.Xl_FtpExplorer1.Size = New System.Drawing.Size(216, 348)
        Me.Xl_FtpExplorer1.TabIndex = 0
        '
        'Frm_HighResImagesManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 348)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_HighResImagesManager"
        Me.Text = "Frm_HighResImagesManager"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_HighResImages1 As Winforms.Xl_HighResImages
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_FtpExplorer1 As Winforms.Xl_FtpExplorer
End Class
