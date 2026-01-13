<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Alb_lineItems
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
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.LabelTotals = New System.Windows.Forms.Label()
        Me.MatDataGridView1 = New Mat.NET.MatDataGridView()
        CType(Me.MatDataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelUser
        '
        Me.LabelUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelUser.AutoSize = True
        Me.LabelUser.Location = New System.Drawing.Point(3, 134)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(43, 13)
        Me.LabelUser.TabIndex = 6
        Me.LabelUser.Text = "(Usuari)"
        '
        'LabelTotals
        '
        Me.LabelTotals.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTotals.AutoSize = True
        Me.LabelTotals.Location = New System.Drawing.Point(3, 119)
        Me.LabelTotals.Name = "LabelTotals"
        Me.LabelTotals.Size = New System.Drawing.Size(38, 13)
        Me.LabelTotals.TabIndex = 7
        Me.LabelTotals.Text = "(totals)"
        '
        'MatDataGridView1
        '
        Me.MatDataGridView1.AllowUserToAddRows = False
        Me.MatDataGridView1.AllowUserToDeleteRows = False
        Me.MatDataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MatDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MatDataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.MatDataGridView1.Name = "MatDataGridView1"
        Me.MatDataGridView1.ReadOnly = True
        Me.MatDataGridView1.Size = New System.Drawing.Size(322, 116)
        Me.MatDataGridView1.TabIndex = 5
        '
        'Xl_Alb_lineItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelTotals)
        Me.Controls.Add(Me.LabelUser)
        Me.Controls.Add(Me.MatDataGridView1)
        Me.Name = "Xl_Alb_lineItems"
        Me.Size = New System.Drawing.Size(322, 150)
        CType(Me.MatDataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MatDataGridView1 As Mat.NET.MatDataGridView
    Friend WithEvents LabelUser As System.Windows.Forms.Label
    Friend WithEvents LabelTotals As System.Windows.Forms.Label

End Class
