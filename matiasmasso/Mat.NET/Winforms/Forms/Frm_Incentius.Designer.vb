<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Incentius
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
        Me.Xl_Incentius1 = New Winforms.Xl_Incentius()
        CType(Me.Xl_Incentius1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Incentius1
        '
        Me.Xl_Incentius1.AllowUserToAddRows = False
        Me.Xl_Incentius1.AllowUserToDeleteRows = False
        Me.Xl_Incentius1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Incentius1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Incentius1.Filter = Nothing
        Me.Xl_Incentius1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Incentius1.Name = "Xl_Incentius1"
        Me.Xl_Incentius1.ReadOnly = True
        Me.Xl_Incentius1.Size = New System.Drawing.Size(488, 261)
        Me.Xl_Incentius1.TabIndex = 0
        '
        'Frm_Incentius
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 261)
        Me.Controls.Add(Me.Xl_Incentius1)
        Me.Name = "Frm_Incentius"
        Me.Text = "Incentius"
        CType(Me.Xl_Incentius1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Incentius1 As Xl_Incentius
End Class
