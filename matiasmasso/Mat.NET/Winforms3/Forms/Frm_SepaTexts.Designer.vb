<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SepaTexts
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
        Me.Xl_SepaTexts1 = New Xl_SepaTexts()
        Me.Xl_Langs1 = New Xl_Langs()
        CType(Me.Xl_SepaTexts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(611, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(226, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_SepaTexts1
        '
        Me.Xl_SepaTexts1.AllowUserToAddRows = False
        Me.Xl_SepaTexts1.AllowUserToDeleteRows = False
        Me.Xl_SepaTexts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SepaTexts1.DisplayObsolets = False
        Me.Xl_SepaTexts1.Filter = Nothing
        Me.Xl_SepaTexts1.Location = New System.Drawing.Point(1, 39)
        Me.Xl_SepaTexts1.MouseIsDown = False
        Me.Xl_SepaTexts1.Name = "Xl_SepaTexts1"
        Me.Xl_SepaTexts1.ReadOnly = True
        Me.Xl_SepaTexts1.Size = New System.Drawing.Size(847, 536)
        Me.Xl_SepaTexts1.TabIndex = 1
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(12, 11)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 2
        '
        'Frm_SepaTexts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 576)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.Xl_SepaTexts1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Name = "Frm_SepaTexts"
        Me.Text = "Texts mandat SEPA Core"
        CType(Me.Xl_SepaTexts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_SepaTexts1 As Xl_SepaTexts
    Friend WithEvents Xl_Langs1 As Xl_Langs
End Class
