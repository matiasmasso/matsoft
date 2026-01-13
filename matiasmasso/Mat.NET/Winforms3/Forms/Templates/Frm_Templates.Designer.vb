<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Templates
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Xl_Templates1 = New Mat.Net.Xl_Templates()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_Templates1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Templates1
        '
        Me.Xl_Templates1.AllowUserToAddRows = False
        Me.Xl_Templates1.AllowUserToDeleteRows = False
        Me.Xl_Templates1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Templates1.DisplayObsolets = False
        Me.Xl_Templates1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Templates1.Filter = Nothing
        Me.Xl_Templates1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Templates1.MouseIsDown = False
        Me.Xl_Templates1.Name = "Xl_Templates1"
        Me.Xl_Templates1.ReadOnly = True
        Me.Xl_Templates1.Size = New System.Drawing.Size(490, 238)
        Me.Xl_Templates1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 238)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(490, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Frm_Templates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 261)
        Me.Controls.Add(Me.Xl_Templates1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "Frm_Templates"
        Me.Text = "Templates"
        CType(Me.Xl_Templates1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Templates1 As Xl_Templates
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
