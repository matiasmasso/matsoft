<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrders
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
        Me.Xl_PurchaseOrders1 = New Mat.NET.Xl_PurchaseOrders()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.LabelFiltre = New System.Windows.Forms.Label()
        Me.Xl_Years1 = New Mat.NET.Xl_Years()
        Me.SuspendLayout()
        '
        'Xl_PurchaseOrders1
        '
        Me.Xl_PurchaseOrders1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PurchaseOrders1.Location = New System.Drawing.Point(1, 29)
        Me.Xl_PurchaseOrders1.Name = "Xl_PurchaseOrders1"
        Me.Xl_PurchaseOrders1.Size = New System.Drawing.Size(925, 306)
        Me.Xl_PurchaseOrders1.TabIndex = 0
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.ForeColor = System.Drawing.Color.Gray
        Me.TextBoxSearch.Location = New System.Drawing.Point(635, 3)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(291, 20)
        Me.TextBoxSearch.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(927, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'LabelFiltre
        '
        Me.LabelFiltre.AutoSize = True
        Me.LabelFiltre.Location = New System.Drawing.Point(601, 6)
        Me.LabelFiltre.Name = "LabelFiltre"
        Me.LabelFiltre.Size = New System.Drawing.Size(29, 13)
        Me.LabelFiltre.TabIndex = 3
        Me.LabelFiltre.Text = "filtre:"
        Me.LabelFiltre.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(13, 1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 5
        Me.Xl_Years1.Value = 0
        '
        'Frm_PurchaseOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 336)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.LabelFiltre)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.Xl_PurchaseOrders1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_PurchaseOrders"
        Me.Text = "Comandes de clients"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_PurchaseOrders1 As Mat.NET.Xl_PurchaseOrders
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents LabelFiltre As System.Windows.Forms.Label
    Friend WithEvents Xl_Years1 As Mat.NET.Xl_Years
End Class
