<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DistribuidorsOficialsRankingLastPdc
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
        Me.Xl_DistribuidorsOficialsRankingLastPdc1 = New Winforms.Xl_DistribuidorsOficialsRankingLastPdc()
        Me.ComboBoxBrands = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.Xl_DistribuidorsOficialsRankingLastPdc1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_DistribuidorsOficialsRankingLastPdc1
        '
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.AllowUserToAddRows = False
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.AllowUserToDeleteRows = False
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.DisplayObsolets = False
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.Filter = Nothing
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.Location = New System.Drawing.Point(1, 39)
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.Name = "Xl_DistribuidorsOficialsRankingLastPdc1"
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.ReadOnly = True
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.Size = New System.Drawing.Size(700, 222)
        Me.Xl_DistribuidorsOficialsRankingLastPdc1.TabIndex = 0
        '
        'ComboBoxBrands
        '
        Me.ComboBoxBrands.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxBrands.FormattingEnabled = True
        Me.ComboBoxBrands.Location = New System.Drawing.Point(485, 12)
        Me.ComboBoxBrands.Name = "ComboBoxBrands"
        Me.ComboBoxBrands.Size = New System.Drawing.Size(216, 21)
        Me.ComboBoxBrands.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(395, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "marca comercial"
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(50, 13)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(300, 20)
        Me.Xl_LookupArea1.TabIndex = 3
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Zona"
        '
        'Frm_DistribuidorsOficialsRankingLastPdc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 261)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_LookupArea1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxBrands)
        Me.Controls.Add(Me.Xl_DistribuidorsOficialsRankingLastPdc1)
        Me.Name = "Frm_DistribuidorsOficialsRankingLastPdc"
        Me.Text = "Ranking Rezagados"
        CType(Me.Xl_DistribuidorsOficialsRankingLastPdc1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_DistribuidorsOficialsRankingLastPdc1 As Xl_DistribuidorsOficialsRankingLastPdc
    Friend WithEvents ComboBoxBrands As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents Label2 As Label
End Class
