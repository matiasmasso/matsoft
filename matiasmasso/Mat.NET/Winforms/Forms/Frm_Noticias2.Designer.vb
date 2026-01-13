<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Noticias2
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
        Me.Xl_Noticias21 = New Winforms.Xl_Noticias2()
        CType(Me.Xl_Noticias21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Noticias21
        '
        Me.Xl_Noticias21.AllowUserToAddRows = False
        Me.Xl_Noticias21.AllowUserToDeleteRows = False
        Me.Xl_Noticias21.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Noticias21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Noticias21.Filter = Nothing
        Me.Xl_Noticias21.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Noticias21.Name = "Xl_Noticias21"
        Me.Xl_Noticias21.ReadOnly = True
        Me.Xl_Noticias21.Size = New System.Drawing.Size(882, 588)
        Me.Xl_Noticias21.TabIndex = 0
        '
        'Frm_Noticias2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 588)
        Me.Controls.Add(Me.Xl_Noticias21)
        Me.Name = "Frm_Noticias2"
        Me.Text = "Frm_Noticias2"
        CType(Me.Xl_Noticias21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Noticias21 As Xl_Noticias2
End Class
