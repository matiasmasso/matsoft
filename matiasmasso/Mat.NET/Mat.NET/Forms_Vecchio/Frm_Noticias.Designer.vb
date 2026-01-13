<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Noticias
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
        Me.Xl_Noticias1 = New Mat.NET.Xl_Noticias()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Xl_Noticias1
        '
        Me.Xl_Noticias1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Noticias1.Location = New System.Drawing.Point(0, 52)
        Me.Xl_Noticias1.Name = "Xl_Noticias1"
        Me.Xl_Noticias1.Size = New System.Drawing.Size(797, 337)
        Me.Xl_Noticias1.TabIndex = 0
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Noticias", "Eventos", "Sabías Que..."})
        Me.ComboBoxCod.Location = New System.Drawing.Point(644, 13)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(153, 21)
        Me.ComboBoxCod.TabIndex = 1
        '
        'Frm_Noticias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 389)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.Xl_Noticias1)
        Me.Name = "Frm_Noticias"
        Me.Text = "NOTICIES / NOTES DE PREMSA"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Noticias1 As Mat.NET.Xl_Noticias
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
End Class
