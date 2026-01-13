<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Catalog
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Xl_ProductBrands1 = New Mat.NET.Xl_ProductBrands_Vecchio()
        Me.Xl_ProductCategories1 = New Mat.NET.Xl_ProductCategories_Vecchio()
        Me.Xl_ProductSkus1 = New Mat.NET.Xl_ProductSkus_Vecchio()
        Me.SuspendLayout()
        '
        'Xl_ProductBrands1
        '
        Me.Xl_ProductBrands1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Xl_ProductBrands1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductBrands1.Name = "Xl_ProductBrands1"
        Me.Xl_ProductBrands1.Size = New System.Drawing.Size(150, 150)
        Me.Xl_ProductBrands1.TabIndex = 0
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(150, 0)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(150, 150)
        Me.Xl_ProductCategories1.TabIndex = 1
        '
        'Xl_ProductSkus1
        '
        Me.Xl_ProductSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkus1.Location = New System.Drawing.Point(300, 0)
        Me.Xl_ProductSkus1.Name = "Xl_ProductSkus1"
        Me.Xl_ProductSkus1.Size = New System.Drawing.Size(316, 150)
        Me.Xl_ProductSkus1.TabIndex = 2
        '
        'Xl_Catalog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_ProductSkus1)
        Me.Controls.Add(Me.Xl_ProductCategories1)
        Me.Controls.Add(Me.Xl_ProductBrands1)
        Me.Name = "Xl_Catalog"
        Me.Size = New System.Drawing.Size(616, 150)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_ProductBrands1 As Mat.NET.Xl_ProductBrands_Vecchio
    Friend WithEvents Xl_ProductCategories1 As Mat.NET.Xl_ProductCategories_Vecchio
    Friend WithEvents Xl_ProductSkus1 As Mat.NET.Xl_ProductSkus_Vecchio

End Class
