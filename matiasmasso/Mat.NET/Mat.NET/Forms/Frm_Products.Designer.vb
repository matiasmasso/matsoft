<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Products
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_ProductBrands1 = New Mat.NET.Xl_ProductBrands()
        Me.Xl_ProductCategories1 = New Mat.NET.Xl_ProductCategories()
        Me.Xl_ProductSkus1 = New Mat.NET.Xl_ProductSkus()
        Me.Xl_ProductStocks1 = New Mat.NET.Xl_ProductStocks()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_ProductBrands1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(729, 261)
        Me.SplitContainer1.SplitterDistance = 167
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_ProductCategories1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_ProductStocks1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_ProductSkus1)
        Me.SplitContainer2.Size = New System.Drawing.Size(558, 261)
        Me.SplitContainer2.SplitterDistance = 186
        Me.SplitContainer2.TabIndex = 0
        '
        'Xl_ProductBrands1
        '
        Me.Xl_ProductBrands1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductBrands1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductBrands1.Name = "Xl_ProductBrands1"
        Me.Xl_ProductBrands1.Size = New System.Drawing.Size(167, 261)
        Me.Xl_ProductBrands1.TabIndex = 0
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(186, 261)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'Xl_ProductSkus1
        '
        Me.Xl_ProductSkus1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkus1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductSkus1.Name = "Xl_ProductSkus1"
        Me.Xl_ProductSkus1.Size = New System.Drawing.Size(368, 261)
        Me.Xl_ProductSkus1.TabIndex = 0
        '
        'Xl_ProductStocks1
        '
        Me.Xl_ProductStocks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductStocks1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductStocks1.Name = "Xl_ProductStocks1"
        Me.Xl_ProductStocks1.Size = New System.Drawing.Size(368, 261)
        Me.Xl_ProductStocks1.TabIndex = 1
        '
        'Frm_Products
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Products"
        Me.Text = "Cataleg de Productes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_ProductBrands1 As Mat.NET.Xl_ProductBrands
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_ProductCategories1 As Mat.NET.Xl_ProductCategories
    Friend WithEvents Xl_ProductSkus1 As Mat.NET.Xl_ProductSkus
    Friend WithEvents Xl_ProductStocks1 As Mat.NET.Xl_ProductStocks
End Class
