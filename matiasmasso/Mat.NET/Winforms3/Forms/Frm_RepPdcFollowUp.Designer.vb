<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepPdcFollowUp
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
        Me.Xl_RepPdcFollowUp1 = New Xl_RepPdcFollowUp()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.Xl_RepPdcFollowUp1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_RepPdcFollowUp1
        '
        Me.Xl_RepPdcFollowUp1.AllowUserToAddRows = False
        Me.Xl_RepPdcFollowUp1.AllowUserToDeleteRows = False
        Me.Xl_RepPdcFollowUp1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RepPdcFollowUp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RepPdcFollowUp1.DisplayObsolets = False
        Me.Xl_RepPdcFollowUp1.Filter = Nothing
        Me.Xl_RepPdcFollowUp1.Location = New System.Drawing.Point(0, 38)
        Me.Xl_RepPdcFollowUp1.MouseIsDown = False
        Me.Xl_RepPdcFollowUp1.Name = "Xl_RepPdcFollowUp1"
        Me.Xl_RepPdcFollowUp1.ReadOnly = True
        Me.Xl_RepPdcFollowUp1.Size = New System.Drawing.Size(799, 412)
        Me.Xl_RepPdcFollowUp1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(649, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(0, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(643, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Frm_RepPdcFollowUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_RepPdcFollowUp1)
        Me.Name = "Frm_RepPdcFollowUp"
        Me.Text = "Seguiment comisions de comanda "
        CType(Me.Xl_RepPdcFollowUp1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_RepPdcFollowUp1 As Xl_RepPdcFollowUp
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents TextBox1 As TextBox
End Class
