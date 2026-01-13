<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Skus_Select
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
        Me.Xl_Skus_Selection1 = New Mat.NET.Xl_Skus_Selection()
        Me.SuspendLayout()
        '
        'Xl_Skus_Selection1
        '
        Me.Xl_Skus_Selection1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Skus_Selection1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Skus_Selection1.Name = "Xl_Skus_Selection1"
        Me.Xl_Skus_Selection1.Size = New System.Drawing.Size(284, 261)
        Me.Xl_Skus_Selection1.TabIndex = 0
        '
        'Frm_Skus_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.Xl_Skus_Selection1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_Skus_Select"
        Me.Text = "Frm_Skus_Select"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Skus_Selection1 As Mat.NET.Xl_Skus_Selection
End Class
