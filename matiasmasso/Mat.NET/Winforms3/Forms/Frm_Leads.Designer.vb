<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Leads
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
        Me.Xl_Leads1 = New Xl_Leads()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        CType(Me.Xl_Leads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Leads1
        '
        Me.Xl_Leads1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Leads1.DisplayObsolets = False
        Me.Xl_Leads1.Filter = Nothing
        Me.Xl_Leads1.Location = New System.Drawing.Point(1, 29)
        Me.Xl_Leads1.MouseIsDown = False
        Me.Xl_Leads1.Name = "Xl_Leads1"
        Me.Xl_Leads1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Xl_Leads1.Size = New System.Drawing.Size(494, 369)
        Me.Xl_Leads1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(345, 3)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Frm_Leads
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 398)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_Leads1)
        Me.Name = "Frm_Leads"
        Me.Text = "Leads"
        CType(Me.Xl_Leads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Leads1 As Xl_Leads
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
