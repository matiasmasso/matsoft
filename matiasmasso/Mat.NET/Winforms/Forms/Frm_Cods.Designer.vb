<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cods
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
        Me.Xl_Cods1 = New Winforms.Xl_Cods()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_Cods1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Cods1
        '
        Me.Xl_Cods1.AllowUserToAddRows = False
        Me.Xl_Cods1.AllowUserToDeleteRows = False
        Me.Xl_Cods1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Cods1.DisplayObsolets = False
        Me.Xl_Cods1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Cods1.Filter = Nothing
        Me.Xl_Cods1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Cods1.MouseIsDown = False
        Me.Xl_Cods1.Name = "Xl_Cods1"
        Me.Xl_Cods1.ReadOnly = True
        Me.Xl_Cods1.Size = New System.Drawing.Size(440, 239)
        Me.Xl_Cods1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 239)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(440, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'Frm_Cods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 262)
        Me.Controls.Add(Me.Xl_Cods1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "Frm_Cods"
        Me.Text = "Codis"
        CType(Me.Xl_Cods1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Cods1 As Xl_Cods
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
