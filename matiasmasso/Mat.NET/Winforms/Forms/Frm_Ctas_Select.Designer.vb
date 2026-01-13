<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ctas_Select
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
        Me.Xl_PgcCtas1 = New Winforms.Xl_PgcCtas()
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PgcCtas1
        '
        Me.Xl_PgcCtas1.AllowUserToAddRows = False
        Me.Xl_PgcCtas1.AllowUserToDeleteRows = False
        Me.Xl_PgcCtas1.BackgroundColor = System.Drawing.SystemColors.Info
        Me.Xl_PgcCtas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcCtas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PgcCtas1.Filter = Nothing
        Me.Xl_PgcCtas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PgcCtas1.Name = "Xl_PgcCtas1"
        Me.Xl_PgcCtas1.ReadOnly = True
        Me.Xl_PgcCtas1.RowTemplate.Height = 40
        Me.Xl_PgcCtas1.Size = New System.Drawing.Size(640, 827)
        Me.Xl_PgcCtas1.TabIndex = 0
        '
        'Frm_Ctas_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Beige
        Me.ClientSize = New System.Drawing.Size(640, 827)
        Me.Controls.Add(Me.Xl_PgcCtas1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_Ctas_Select"
        Me.Opacity = 0.9R
        Me.Text = "Seleccionar compte"
        CType(Me.Xl_PgcCtas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PgcCtas1 As Xl_PgcCtas
End Class
