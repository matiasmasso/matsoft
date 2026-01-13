<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_BaseQuota
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
        Me.Xl_Percent1 = New Winforms.Xl_Percent()
        Me.Xl_AmountQuota = New Winforms.Xl_Amount()
        Me.Xl_AmountBase = New Winforms.Xl_Amount()
        Me.SuspendLayout()
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Percent1.Location = New System.Drawing.Point(101, 0)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(50, 20)
        Me.Xl_Percent1.TabIndex = 1
        Me.Xl_Percent1.Text = "0,00 %"
        Me.Xl_Percent1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_Percent1.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmountQuota
        '
        Me.Xl_AmountQuota.Amt = Nothing
        Me.Xl_AmountQuota.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmountQuota.Location = New System.Drawing.Point(154, 0)
        Me.Xl_AmountQuota.Margin = New System.Windows.Forms.Padding(0)
        Me.Xl_AmountQuota.Name = "Xl_AmountQuota"
        Me.Xl_AmountQuota.ReadOnly = True
        Me.Xl_AmountQuota.Size = New System.Drawing.Size(56, 20)
        Me.Xl_AmountQuota.TabIndex = 2
        Me.Xl_AmountQuota.TabStop = False
        '
        'Xl_AmountBase
        '
        Me.Xl_AmountBase.Amt = Nothing
        Me.Xl_AmountBase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmountBase.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AmountBase.Margin = New System.Windows.Forms.Padding(0)
        Me.Xl_AmountBase.Name = "Xl_AmountBase"
        Me.Xl_AmountBase.ReadOnly = False
        Me.Xl_AmountBase.Size = New System.Drawing.Size(98, 20)
        Me.Xl_AmountBase.TabIndex = 0
        '
        'Xl_BaseQuota
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_AmountBase)
        Me.Controls.Add(Me.Xl_AmountQuota)
        Me.Controls.Add(Me.Xl_Percent1)
        Me.Name = "Xl_BaseQuota"
        Me.Size = New System.Drawing.Size(213, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents Xl_AmountQuota As Xl_Amount
    Friend WithEvents Xl_AmountBase As Xl_Amount
End Class
