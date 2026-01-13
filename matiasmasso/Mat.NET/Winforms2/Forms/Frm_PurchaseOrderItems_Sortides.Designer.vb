<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrderItems_Sortides
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
        Me.Xl_PurchaseOrderItem_Sortides1 = New Mat.Net.Xl_PurchaseOrderItem_Sortides()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.Xl_PurchaseOrderItem_Sortides1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PurchaseOrderItem_Sortides1
        '
        Me.Xl_PurchaseOrderItem_Sortides1.AllowUserToAddRows = False
        Me.Xl_PurchaseOrderItem_Sortides1.AllowUserToDeleteRows = False
        Me.Xl_PurchaseOrderItem_Sortides1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PurchaseOrderItem_Sortides1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PurchaseOrderItem_Sortides1.DisplayObsolets = False
        Me.Xl_PurchaseOrderItem_Sortides1.Location = New System.Drawing.Point(-1, 92)
        Me.Xl_PurchaseOrderItem_Sortides1.MouseIsDown = False
        Me.Xl_PurchaseOrderItem_Sortides1.Name = "Xl_PurchaseOrderItem_Sortides1"
        Me.Xl_PurchaseOrderItem_Sortides1.ReadOnly = True
        Me.Xl_PurchaseOrderItem_Sortides1.Size = New System.Drawing.Size(361, 241)
        Me.Xl_PurchaseOrderItem_Sortides1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(-1, 13)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(501, 73)
        Me.TextBox1.TabIndex = 1
        '
        'Frm_PurchaseOrderItems_Sortides
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 334)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Xl_PurchaseOrderItem_Sortides1)
        Me.Name = "Frm_PurchaseOrderItems_Sortides"
        Me.Text = "Sortides de línia de comanda"
        CType(Me.Xl_PurchaseOrderItem_Sortides1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_PurchaseOrderItem_Sortides1 As Xl_PurchaseOrderItem_Sortides
    Friend WithEvents TextBox1 As TextBox
End Class
