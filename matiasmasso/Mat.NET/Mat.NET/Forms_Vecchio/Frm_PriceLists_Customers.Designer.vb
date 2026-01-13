<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceLists_Customers
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_PriceList_Customers_Vigent1 = New Mat.Net.Xl_PriceList_Customers_Vigent()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_PriceLists_Customers1 = New Mat.Net.Xl_PriceLists_Customers()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 21)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(806, 499)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_PriceList_Customers_Vigent1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(798, 473)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Vigent"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_PriceList_Customers_Vigent1
        '
        Me.Xl_PriceList_Customers_Vigent1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PriceList_Customers_Vigent1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PriceList_Customers_Vigent1.Name = "Xl_PriceList_Customers_Vigent1"
        Me.Xl_PriceList_Customers_Vigent1.Size = New System.Drawing.Size(792, 467)
        Me.Xl_PriceList_Customers_Vigent1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_PriceLists_Customers1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(798, 473)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Historic"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_PriceLists_Customers1
        '
        Me.Xl_PriceLists_Customers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PriceLists_Customers1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PriceLists_Customers1.Name = "Xl_PriceLists_Customers1"
        Me.Xl_PriceLists_Customers1.Size = New System.Drawing.Size(792, 467)
        Me.Xl_PriceLists_Customers1.TabIndex = 1
        '
        'Frm_PriceLists_Customers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 521)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_PriceLists_Customers"
        Me.Text = "Tarifes de Preus de Venda"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_PriceLists_Customers1 As Mat.Net.Xl_PriceLists_Customers
    Friend WithEvents Xl_PriceList_Customers_Vigent1 As Mat.Net.Xl_PriceList_Customers_Vigent
End Class
