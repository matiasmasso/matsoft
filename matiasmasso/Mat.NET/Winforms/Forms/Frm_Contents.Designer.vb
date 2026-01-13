<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contents
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
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_Noticias1 = New Winforms.Xl_Contents()
        CType(Me.Xl_Noticias1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Notícies", "Eventos", "Sabías Que...", "Promos", "TablonDeAnuncios", "Blog", "Continguts"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(12, 12)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(153, 21)
        Me.ComboBoxCod.TabIndex = 1
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(595, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(190, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'Xl_Noticias1
        '
        Me.Xl_Noticias1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Noticias1.DisplayObsolets = False
        Me.Xl_Noticias1.Filter = Nothing
        Me.Xl_Noticias1.Location = New System.Drawing.Point(0, 39)
        Me.Xl_Noticias1.MouseIsDown = False
        Me.Xl_Noticias1.Name = "Xl_Noticias1"
        Me.Xl_Noticias1.Size = New System.Drawing.Size(797, 350)
        Me.Xl_Noticias1.TabIndex = 0
        '
        'Frm_Noticias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 389)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.Xl_Noticias1)
        Me.Name = "Frm_Noticias"
        Me.Text = "NOTICIES / NOTES DE PREMSA"
        CType(Me.Xl_Noticias1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Noticias1 As Winforms.Xl_Contents
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
