<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_PurchaseOrderItemsForPlatforms
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
        Me.MatDataGridView1 = New MatDataGridView()
        CType(Me.MatDataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MatDataGridView1
        '
        Me.MatDataGridView1.AllowUserToAddRows = False
        Me.MatDataGridView1.AllowUserToDeleteRows = False
        Me.MatDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MatDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MatDataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.MatDataGridView1.Name = "MatDataGridView1"
        Me.MatDataGridView1.Size = New System.Drawing.Size(391, 604)
        Me.MatDataGridView1.TabIndex = 0
        '
        '
        'Xl_PurchaseOrderItemsForPlatforms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MatDataGridView1)
        Me.Name = "Xl_PurchaseOrderItemsForPlatforms"
        Me.Size = New System.Drawing.Size(391, 604)
        CType(Me.MatDataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MatDataGridView1 As MatDataGridView

End Class
