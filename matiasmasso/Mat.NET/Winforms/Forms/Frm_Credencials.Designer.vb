<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Credencials
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
        Me.Xl_Credencials1 = New Winforms.Xl_Credencials()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        CType(Me.Xl_Credencials1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Credencials1
        '
        Me.Xl_Credencials1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Credencials1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Credencials1.DisplayObsolets = False
        Me.Xl_Credencials1.Filter = Nothing
        Me.Xl_Credencials1.Location = New System.Drawing.Point(0, 38)
        Me.Xl_Credencials1.MouseIsDown = False
        Me.Xl_Credencials1.Name = "Xl_Credencials1"
        Me.Xl_Credencials1.Size = New System.Drawing.Size(645, 315)
        Me.Xl_Credencials1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(430, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(215, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Frm_Credencials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 353)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_Credencials1)
        Me.Name = "Frm_Credencials"
        Me.Text = "Credencials"
        CType(Me.Xl_Credencials1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Credencials1 As Xl_Credencials
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
