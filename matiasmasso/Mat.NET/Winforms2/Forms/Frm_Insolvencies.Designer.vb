<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Insolvencies
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
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Xl_Insolvencies1 = New Xl_Insolvencies()
        CType(Me.Xl_Insolvencies1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(421, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_Insolvencies1
        '
        Me.Xl_Insolvencies1.AllowUserToAddRows = False
        Me.Xl_Insolvencies1.AllowUserToDeleteRows = False
        Me.Xl_Insolvencies1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Insolvencies1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Insolvencies1.DisplayObsolets = False
        Me.Xl_Insolvencies1.Filter = Nothing
        Me.Xl_Insolvencies1.Location = New System.Drawing.Point(1, 47)
        Me.Xl_Insolvencies1.MouseIsDown = False
        Me.Xl_Insolvencies1.Name = "Xl_Insolvencies1"
        Me.Xl_Insolvencies1.ReadOnly = True
        Me.Xl_Insolvencies1.Size = New System.Drawing.Size(570, 231)
        Me.Xl_Insolvencies1.TabIndex = 1
        '
        'Frm_Insolvencies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 278)
        Me.Controls.Add(Me.Xl_Insolvencies1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Name = "Frm_Insolvencies"
        Me.Text = "Insolvencies"
        CType(Me.Xl_Insolvencies1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Insolvencies1 As Xl_Insolvencies
End Class
