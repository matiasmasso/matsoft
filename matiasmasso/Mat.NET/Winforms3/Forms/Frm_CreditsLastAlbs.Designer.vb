<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CreditsLastAlbs
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
        Me.Xl_CreditsLastAlbs1 = New Xl_CreditsLastAlbs()
        CType(Me.Xl_CreditsLastAlbs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(13, 13)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(400, 48)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_CreditsLastAlbs1
        '
        Me.Xl_CreditsLastAlbs1.AllowUserToAddRows = False
        Me.Xl_CreditsLastAlbs1.AllowUserToDeleteRows = False
        Me.Xl_CreditsLastAlbs1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CreditsLastAlbs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CreditsLastAlbs1.DisplayObsolets = False
        Me.Xl_CreditsLastAlbs1.Filter = Nothing
        Me.Xl_CreditsLastAlbs1.Location = New System.Drawing.Point(13, 71)
        Me.Xl_CreditsLastAlbs1.MouseIsDown = False
        Me.Xl_CreditsLastAlbs1.Name = "Xl_CreditsLastAlbs1"
        Me.Xl_CreditsLastAlbs1.ReadOnly = True
        Me.Xl_CreditsLastAlbs1.RowTemplate.Height = 40
        Me.Xl_CreditsLastAlbs1.Size = New System.Drawing.Size(1467, 698)
        Me.Xl_CreditsLastAlbs1.TabIndex = 1
        '
        'Frm_CreditsLastAlbs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1492, 781)
        Me.Controls.Add(Me.Xl_CreditsLastAlbs1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Name = "Frm_CreditsLastAlbs"
        Me.Text = "Credits de clients"
        CType(Me.Xl_CreditsLastAlbs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_CreditsLastAlbs1 As Xl_CreditsLastAlbs
End Class
