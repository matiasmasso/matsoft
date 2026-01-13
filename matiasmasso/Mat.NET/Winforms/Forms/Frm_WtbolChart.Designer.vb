<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WtbolChart
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
        Me.Xl_Chart1 = New Winforms.Xl_Chart()
        Me.SuspendLayout()
        '
        'Xl_Chart1
        '
        Me.Xl_Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Chart1.Location = New System.Drawing.Point(9, 53)
        Me.Xl_Chart1.Name = "Xl_Chart1"
        Me.Xl_Chart1.Size = New System.Drawing.Size(786, 398)
        Me.Xl_Chart1.TabIndex = 0
        '
        'Frm_WtbolChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Xl_Chart1)
        Me.Name = "Frm_WtbolChart"
        Me.Text = "Frm_WtbolChart"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Chart1 As Xl_Chart
End Class
