<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcPlans
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
        Me.Xl_PgcPlans1 = New Winforms.Xl_PgcPlans()
        CType(Me.Xl_PgcPlans1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PgcPlans1
        '
        Me.Xl_PgcPlans1.AllowUserToAddRows = False
        Me.Xl_PgcPlans1.AllowUserToDeleteRows = False
        Me.Xl_PgcPlans1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcPlans1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PgcPlans1.Filter = Nothing
        Me.Xl_PgcPlans1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PgcPlans1.Name = "Xl_PgcPlans1"
        Me.Xl_PgcPlans1.ReadOnly = True
        Me.Xl_PgcPlans1.RowTemplate.Height = 40
        Me.Xl_PgcPlans1.Size = New System.Drawing.Size(932, 606)
        Me.Xl_PgcPlans1.TabIndex = 0
        '
        'Frm_PgcPlans
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 606)
        Me.Controls.Add(Me.Xl_PgcPlans1)
        Me.Name = "Frm_PgcPlans"
        Me.Text = "Plans comptables"
        CType(Me.Xl_PgcPlans1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PgcPlans1 As Xl_PgcPlans
End Class
