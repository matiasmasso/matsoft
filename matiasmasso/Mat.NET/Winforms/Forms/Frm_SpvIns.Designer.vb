<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SpvIns
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
        Me.Xl_SpvIns1 = New Winforms.Xl_SpvIns()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_SpvIns1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_SpvIns1
        '
        Me.Xl_SpvIns1.AllowUserToAddRows = False
        Me.Xl_SpvIns1.AllowUserToDeleteRows = False
        Me.Xl_SpvIns1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SpvIns1.DisplayObsolets = False
        Me.Xl_SpvIns1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SpvIns1.Filter = Nothing
        Me.Xl_SpvIns1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SpvIns1.MouseIsDown = False
        Me.Xl_SpvIns1.Name = "Xl_SpvIns1"
        Me.Xl_SpvIns1.ReadOnly = True
        Me.Xl_SpvIns1.Size = New System.Drawing.Size(534, 238)
        Me.Xl_SpvIns1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 238)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(534, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Frm_SpvIns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 261)
        Me.Controls.Add(Me.Xl_SpvIns1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "Frm_SpvIns"
        Me.Text = "Entrades de mercancía per reparar"
        CType(Me.Xl_SpvIns1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_SpvIns1 As Xl_SpvIns
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
