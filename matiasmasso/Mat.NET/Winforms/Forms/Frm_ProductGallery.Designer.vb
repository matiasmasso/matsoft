<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductGallery
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
        Me.Xl_ProductHighResImages1 = New Winforms.Xl_ProductHighResImages()
        CType(Me.Xl_ProductHighResImages1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_ProductHighResImages1
        '
        Me.Xl_ProductHighResImages1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductHighResImages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductHighResImages1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductHighResImages1.Name = "Xl_ProductHighResImages1"
        Me.Xl_ProductHighResImages1.Size = New System.Drawing.Size(456, 261)
        Me.Xl_ProductHighResImages1.TabIndex = 0
        '
        'Frm_ProductGallery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 261)
        Me.Controls.Add(Me.Xl_ProductHighResImages1)
        Me.Name = "Frm_ProductGallery"
        Me.Text = "Galeria"
        CType(Me.Xl_ProductHighResImages1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_ProductHighResImages1 As Winforms.Xl_ProductHighResImages
End Class
