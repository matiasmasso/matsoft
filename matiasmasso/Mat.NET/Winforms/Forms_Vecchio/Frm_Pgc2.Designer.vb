<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Pgc2
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
        Me.Xl_PgcClassTree1 = New Winforms.Xl_PgcClassTree()
        Me.SuspendLayout()
        '
        'Xl_PgcClassTree1
        '
        Me.Xl_PgcClassTree1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PgcClassTree1.Location = New System.Drawing.Point(0, 51)
        Me.Xl_PgcClassTree1.Name = "Xl_PgcClassTree1"
        Me.Xl_PgcClassTree1.Size = New System.Drawing.Size(397, 475)
        Me.Xl_PgcClassTree1.TabIndex = 0
        '
        'Frm_Pgc2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 527)
        Me.Controls.Add(Me.Xl_PgcClassTree1)
        Me.Name = "Frm_Pgc2"
        Me.Text = "Frm_Pgc2"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_PgcClassTree1 As Winforms.Xl_PgcClassTree
End Class
