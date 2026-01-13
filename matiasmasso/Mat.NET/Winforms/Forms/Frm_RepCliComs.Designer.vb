<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepCliComs
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
        Me.Xl_RepCliComs1 = New Winforms.Xl_RepCliComs()
        CType(Me.Xl_RepCliComs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_RepCliComs1
        '
        Me.Xl_RepCliComs1.AllowUserToAddRows = False
        Me.Xl_RepCliComs1.AllowUserToDeleteRows = False
        Me.Xl_RepCliComs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RepCliComs1.DisplayObsolets = False
        Me.Xl_RepCliComs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RepCliComs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RepCliComs1.Name = "Xl_RepCliComs1"
        Me.Xl_RepCliComs1.ReadOnly = True
        Me.Xl_RepCliComs1.Size = New System.Drawing.Size(695, 261)
        Me.Xl_RepCliComs1.TabIndex = 0
        '
        'Frm_RepCliComs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 261)
        Me.Controls.Add(Me.Xl_RepCliComs1)
        Me.Name = "Frm_RepCliComs"
        Me.Text = "Clients amb comisió reduïda o exclosa"
        CType(Me.Xl_RepCliComs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_RepCliComs1 As Xl_RepCliComs
End Class
