<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PortsCondicions
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_PortsCondicions1 = New Mat.Net.Xl_PortsCondicions()
        CType(Me.Xl_PortsCondicions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 193)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(537, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Xl_PortsCondicions1
        '
        Me.Xl_PortsCondicions1.AllowUserToAddRows = False
        Me.Xl_PortsCondicions1.AllowUserToDeleteRows = False
        Me.Xl_PortsCondicions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PortsCondicions1.DisplayObsolets = False
        Me.Xl_PortsCondicions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PortsCondicions1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PortsCondicions1.MouseIsDown = False
        Me.Xl_PortsCondicions1.Name = "Xl_PortsCondicions1"
        Me.Xl_PortsCondicions1.ReadOnly = True
        Me.Xl_PortsCondicions1.Size = New System.Drawing.Size(537, 193)
        Me.Xl_PortsCondicions1.TabIndex = 1
        '
        'Frm_PortsCondicions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 216)
        Me.Controls.Add(Me.Xl_PortsCondicions1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "Frm_PortsCondicions"
        Me.Text = "Condicions de transport"
        CType(Me.Xl_PortsCondicions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_PortsCondicions1 As Xl_PortsCondicions
End Class
