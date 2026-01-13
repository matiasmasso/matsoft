<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceListsxSku
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
        Me.Xl_PriceListsSuplierxSku1 = New Winforms.Xl_PriceListsSuplierxSku()
        Me.LabelSku = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_PricelistItems_CustomerXSku1 = New Winforms.Xl_PricelistItems_CustomerXSku()
        CType(Me.Xl_PriceListsSuplierxSku1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_PricelistItems_CustomerXSku1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PriceListsSuplierxSku1
        '
        Me.Xl_PriceListsSuplierxSku1.AllowUserToAddRows = False
        Me.Xl_PriceListsSuplierxSku1.AllowUserToDeleteRows = False
        Me.Xl_PriceListsSuplierxSku1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PriceListsSuplierxSku1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PriceListsSuplierxSku1.Filter = Nothing
        Me.Xl_PriceListsSuplierxSku1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PriceListsSuplierxSku1.Name = "Xl_PriceListsSuplierxSku1"
        Me.Xl_PriceListsSuplierxSku1.ReadOnly = True
        Me.Xl_PriceListsSuplierxSku1.Size = New System.Drawing.Size(372, 180)
        Me.Xl_PriceListsSuplierxSku1.TabIndex = 0
        '
        'LabelSku
        '
        Me.LabelSku.AutoSize = True
        Me.LabelSku.Location = New System.Drawing.Point(13, 13)
        Me.LabelSku.Name = "LabelSku"
        Me.LabelSku.Size = New System.Drawing.Size(26, 13)
        Me.LabelSku.TabIndex = 1
        Me.LabelSku.Text = "Sku"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(580, 218)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_PricelistItems_CustomerXSku1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(572, 192)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Venda"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_PriceListsSuplierxSku1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(378, 186)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Cost"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_PricelistItems_CustomerXSku1
        '
        Me.Xl_PricelistItems_CustomerXSku1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PricelistItems_CustomerXSku1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PricelistItems_CustomerXSku1.Filter = Nothing
        Me.Xl_PricelistItems_CustomerXSku1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PricelistItems_CustomerXSku1.Name = "Xl_PricelistItems_CustomerXSku1"
        Me.Xl_PricelistItems_CustomerXSku1.Size = New System.Drawing.Size(566, 186)
        Me.Xl_PricelistItems_CustomerXSku1.TabIndex = 0
        '
        'Frm_PriceListsSuplierxSku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 261)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.LabelSku)
        Me.Name = "Frm_PriceListsSuplierxSku"
        Me.Text = "Historial de Tarifes per Producte"
        CType(Me.Xl_PriceListsSuplierxSku1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_PricelistItems_CustomerXSku1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_PriceListsSuplierxSku1 As Xl_PriceListsSuplierxSku
    Friend WithEvents LabelSku As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_PricelistItems_CustomerXSku1 As Xl_PricelistItems_CustomerXSku
End Class
