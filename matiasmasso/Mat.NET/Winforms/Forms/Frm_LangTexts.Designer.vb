<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LangTexts
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
        Me.Xl_LangTexts1 = New Winforms.Xl_LangTexts()
        CType(Me.Xl_LangTexts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_LangTexts1
        '
        Me.Xl_LangTexts1.AllowUserToAddRows = False
        Me.Xl_LangTexts1.AllowUserToDeleteRows = False
        Me.Xl_LangTexts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LangTexts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LangTexts1.DisplayObsolets = False
        Me.Xl_LangTexts1.Location = New System.Drawing.Point(1, 52)
        Me.Xl_LangTexts1.MouseIsDown = False
        Me.Xl_LangTexts1.Name = "Xl_LangTexts1"
        Me.Xl_LangTexts1.ReadOnly = True
        Me.Xl_LangTexts1.Size = New System.Drawing.Size(611, 274)
        Me.Xl_LangTexts1.TabIndex = 0
        '
        'Frm_LangTexts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(614, 327)
        Me.Controls.Add(Me.Xl_LangTexts1)
        Me.Name = "Frm_LangTexts"
        Me.Text = "Frm_LangTexts"
        CType(Me.Xl_LangTexts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_LangTexts1 As Xl_LangTexts
End Class
