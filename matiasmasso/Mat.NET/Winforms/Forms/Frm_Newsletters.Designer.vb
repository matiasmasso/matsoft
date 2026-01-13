<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Newsletters
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
        Me.Xl_Newsletters1 = New Winforms.Xl_Newsletters()
        CType(Me.Xl_Newsletters1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Newsletters1
        '
        Me.Xl_Newsletters1.AllowUserToAddRows = False
        Me.Xl_Newsletters1.AllowUserToDeleteRows = False
        Me.Xl_Newsletters1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Newsletters1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Newsletters1.Filter = Nothing
        Me.Xl_Newsletters1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Newsletters1.Name = "Xl_Newsletters1"
        Me.Xl_Newsletters1.ReadOnly = True
        Me.Xl_Newsletters1.RowTemplate.Height = 40
        Me.Xl_Newsletters1.Size = New System.Drawing.Size(1136, 573)
        Me.Xl_Newsletters1.TabIndex = 0
        '
        'Frm_Newsletters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1136, 573)
        Me.Controls.Add(Me.Xl_Newsletters1)
        Me.Name = "Frm_Newsletters"
        Me.Text = "Newsletters"
        CType(Me.Xl_Newsletters1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Newsletters1 As Xl_Newsletters
End Class
