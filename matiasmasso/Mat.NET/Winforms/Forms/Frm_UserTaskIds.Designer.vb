<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_UserTaskIds
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
        Me.Xl_UserTaskIds1 = New Winforms.Xl_UserTaskIds()
        CType(Me.Xl_UserTaskIds1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_UserTaskIds1
        '
        Me.Xl_UserTaskIds1.AllowUserToAddRows = False
        Me.Xl_UserTaskIds1.AllowUserToDeleteRows = False
        Me.Xl_UserTaskIds1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_UserTaskIds1.DisplayObsolets = False
        Me.Xl_UserTaskIds1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_UserTaskIds1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_UserTaskIds1.MouseIsDown = False
        Me.Xl_UserTaskIds1.Name = "Xl_UserTaskIds1"
        Me.Xl_UserTaskIds1.ReadOnly = True
        Me.Xl_UserTaskIds1.RowTemplate.Height = 40
        Me.Xl_UserTaskIds1.Size = New System.Drawing.Size(1236, 543)
        Me.Xl_UserTaskIds1.TabIndex = 0
        '
        'Frm_UserTaskIds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1236, 543)
        Me.Controls.Add(Me.Xl_UserTaskIds1)
        Me.Name = "Frm_UserTaskIds"
        Me.Text = "Tasques de usuari"
        CType(Me.Xl_UserTaskIds1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_UserTaskIds1 As Xl_UserTaskIds
End Class
