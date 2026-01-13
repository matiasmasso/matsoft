<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BookFrasCompact
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
        Me.Xl_BookfrasCompact1 = New Xl_BookfrasCompact()
        CType(Me.Xl_BookfrasCompact1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_BookfrasCompact1
        '
        Me.Xl_BookfrasCompact1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BookfrasCompact1.DisplayObsolets = False
        Me.Xl_BookfrasCompact1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_BookfrasCompact1.Filter = Nothing
        Me.Xl_BookfrasCompact1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_BookfrasCompact1.MouseIsDown = False
        Me.Xl_BookfrasCompact1.Name = "Xl_BookfrasCompact1"
        Me.Xl_BookfrasCompact1.Size = New System.Drawing.Size(703, 261)
        Me.Xl_BookfrasCompact1.TabIndex = 0
        '
        'Frm_BookFrasCompact
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 261)
        Me.Controls.Add(Me.Xl_BookfrasCompact1)
        Me.Name = "Frm_BookFrasCompact"
        Me.Text = "Sel.leccionar factura"
        CType(Me.Xl_BookfrasCompact1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_BookfrasCompact1 As Xl_BookfrasCompact
End Class
