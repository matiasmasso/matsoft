<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SkuPreuJustification
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
        Me.TextBoxSku = New System.Windows.Forms.TextBox()
        Me.TextBoxTarifa = New System.Windows.Forms.TextBox()
        Me.TextBoxRetail = New System.Windows.Forms.TextBox()
        Me.TextBoxDtoText = New System.Windows.Forms.TextBox()
        Me.TextBoxCostDistribuidor = New System.Windows.Forms.TextBox()
        Me.TextBoxDtoPct = New System.Windows.Forms.TextBox()
        Me.TextBoxPdc = New System.Windows.Forms.TextBox()
        Me.TextBoxCustomer = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBoxSku
        '
        Me.TextBoxSku.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSku.Location = New System.Drawing.Point(13, 60)
        Me.TextBoxSku.Name = "TextBoxSku"
        Me.TextBoxSku.ReadOnly = True
        Me.TextBoxSku.Size = New System.Drawing.Size(393, 20)
        Me.TextBoxSku.TabIndex = 0
        '
        'TextBoxTarifa
        '
        Me.TextBoxTarifa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTarifa.Location = New System.Drawing.Point(13, 86)
        Me.TextBoxTarifa.Name = "TextBoxTarifa"
        Me.TextBoxTarifa.ReadOnly = True
        Me.TextBoxTarifa.Size = New System.Drawing.Size(393, 20)
        Me.TextBoxTarifa.TabIndex = 1
        '
        'TextBoxRetail
        '
        Me.TextBoxRetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRetail.Location = New System.Drawing.Point(441, 86)
        Me.TextBoxRetail.Name = "TextBoxRetail"
        Me.TextBoxRetail.ReadOnly = True
        Me.TextBoxRetail.Size = New System.Drawing.Size(81, 20)
        Me.TextBoxRetail.TabIndex = 2
        '
        'TextBoxDtoText
        '
        Me.TextBoxDtoText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDtoText.Location = New System.Drawing.Point(13, 112)
        Me.TextBoxDtoText.Name = "TextBoxDtoText"
        Me.TextBoxDtoText.ReadOnly = True
        Me.TextBoxDtoText.Size = New System.Drawing.Size(333, 20)
        Me.TextBoxDtoText.TabIndex = 3
        Me.TextBoxDtoText.Text = "descompte distribuidor"
        Me.TextBoxDtoText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxCostDistribuidor
        '
        Me.TextBoxCostDistribuidor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCostDistribuidor.Location = New System.Drawing.Point(441, 112)
        Me.TextBoxCostDistribuidor.Name = "TextBoxCostDistribuidor"
        Me.TextBoxCostDistribuidor.ReadOnly = True
        Me.TextBoxCostDistribuidor.Size = New System.Drawing.Size(81, 20)
        Me.TextBoxCostDistribuidor.TabIndex = 4
        '
        'TextBoxDtoPct
        '
        Me.TextBoxDtoPct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDtoPct.Location = New System.Drawing.Point(361, 112)
        Me.TextBoxDtoPct.Name = "TextBoxDtoPct"
        Me.TextBoxDtoPct.ReadOnly = True
        Me.TextBoxDtoPct.Size = New System.Drawing.Size(45, 20)
        Me.TextBoxDtoPct.TabIndex = 5
        '
        'TextBoxPdc
        '
        Me.TextBoxPdc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPdc.Location = New System.Drawing.Point(13, 35)
        Me.TextBoxPdc.Name = "TextBoxPdc"
        Me.TextBoxPdc.ReadOnly = True
        Me.TextBoxPdc.Size = New System.Drawing.Size(509, 20)
        Me.TextBoxPdc.TabIndex = 6
        '
        'TextBoxCustomer
        '
        Me.TextBoxCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCustomer.Location = New System.Drawing.Point(13, 10)
        Me.TextBoxCustomer.Name = "TextBoxCustomer"
        Me.TextBoxCustomer.ReadOnly = True
        Me.TextBoxCustomer.Size = New System.Drawing.Size(509, 20)
        Me.TextBoxCustomer.TabIndex = 7
        '
        'Frm_SkuPreuJustification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 143)
        Me.Controls.Add(Me.TextBoxCustomer)
        Me.Controls.Add(Me.TextBoxPdc)
        Me.Controls.Add(Me.TextBoxDtoPct)
        Me.Controls.Add(Me.TextBoxCostDistribuidor)
        Me.Controls.Add(Me.TextBoxDtoText)
        Me.Controls.Add(Me.TextBoxRetail)
        Me.Controls.Add(Me.TextBoxTarifa)
        Me.Controls.Add(Me.TextBoxSku)
        Me.Name = "Frm_SkuPreuJustification"
        Me.Text = "Per qué te aquest preu?"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxSku As TextBox
    Friend WithEvents TextBoxTarifa As TextBox
    Friend WithEvents TextBoxRetail As TextBox
    Friend WithEvents TextBoxDtoText As TextBox
    Friend WithEvents TextBoxCostDistribuidor As TextBox
    Friend WithEvents TextBoxDtoPct As TextBox
    Friend WithEvents TextBoxPdc As TextBox
    Friend WithEvents TextBoxCustomer As TextBox
End Class
