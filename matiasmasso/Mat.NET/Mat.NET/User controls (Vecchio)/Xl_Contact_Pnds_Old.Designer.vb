<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact_Pnds_Old
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.LabelCreditor = New System.Windows.Forms.Label
        Me.LabelDeutor = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 34)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(441, 93)
        Me.DataGridView1.TabIndex = 0
        '
        'LabelCreditor
        '
        Me.LabelCreditor.AutoSize = True
        Me.LabelCreditor.Location = New System.Drawing.Point(4, 4)
        Me.LabelCreditor.Name = "LabelCreditor"
        Me.LabelCreditor.Size = New System.Drawing.Size(73, 13)
        Me.LabelCreditor.TabIndex = 1
        Me.LabelCreditor.Text = "Total Creditor:"
        '
        'LabelDeutor
        '
        Me.LabelDeutor.AutoSize = True
        Me.LabelDeutor.Location = New System.Drawing.Point(292, 4)
        Me.LabelDeutor.Name = "LabelDeutor"
        Me.LabelDeutor.Size = New System.Drawing.Size(69, 13)
        Me.LabelDeutor.TabIndex = 2
        Me.LabelDeutor.Text = "Total Deutor:"
        '
        'Xl_ContactPnds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelDeutor)
        Me.Controls.Add(Me.LabelCreditor)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Xl_ContactPnds"
        Me.Size = New System.Drawing.Size(441, 127)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelCreditor As System.Windows.Forms.Label
    Friend WithEvents LabelDeutor As System.Windows.Forms.Label

End Class
