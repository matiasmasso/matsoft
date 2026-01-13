<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Inem_CompresProductRanking
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
        Me.Xl_YearMonth1 = New Winforms.Xl_YearMonth()
        Me.Xl_Inem_CompresProductRanking1 = New Winforms.Xl_Inem_CompresProductRanking()
        CType(Me.Xl_Inem_CompresProductRanking1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_YearMonth1
        '
        Me.Xl_YearMonth1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_YearMonth1.Location = New System.Drawing.Point(714, 12)
        Me.Xl_YearMonth1.Name = "Xl_YearMonth1"
        Me.Xl_YearMonth1.Size = New System.Drawing.Size(100, 19)
        Me.Xl_YearMonth1.TabIndex = 1
        '
        'Xl_Inem_CompresProductRanking1
        '
        Me.Xl_Inem_CompresProductRanking1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Inem_CompresProductRanking1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Inem_CompresProductRanking1.DisplayObsolets = False
        Me.Xl_Inem_CompresProductRanking1.Location = New System.Drawing.Point(1, 48)
        Me.Xl_Inem_CompresProductRanking1.MouseIsDown = False
        Me.Xl_Inem_CompresProductRanking1.Name = "Xl_Inem_CompresProductRanking1"
        Me.Xl_Inem_CompresProductRanking1.Size = New System.Drawing.Size(813, 214)
        Me.Xl_Inem_CompresProductRanking1.TabIndex = 0
        '
        'Frm_Inem_CompresProductRanking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 261)
        Me.Controls.Add(Me.Xl_YearMonth1)
        Me.Controls.Add(Me.Xl_Inem_CompresProductRanking1)
        Me.Name = "Frm_Inem_CompresProductRanking"
        Me.Text = "Ine: ranking mensual de productes comprats"
        CType(Me.Xl_Inem_CompresProductRanking1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Inem_CompresProductRanking1 As Xl_Inem_CompresProductRanking
    Friend WithEvents Xl_YearMonth1 As Xl_YearMonth
End Class
