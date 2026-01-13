<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepContractes
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
        Me.Xl_Contracts1 = New Xl_Contracts()
        CType(Me.Xl_Contracts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Contracts1
        '
        Me.Xl_Contracts1.AllowUserToAddRows = False
        Me.Xl_Contracts1.AllowUserToDeleteRows = False
        Me.Xl_Contracts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contracts1.DisplayObsolets = False
        Me.Xl_Contracts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contracts1.Filter = Nothing
        Me.Xl_Contracts1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contracts1.MouseIsDown = False
        Me.Xl_Contracts1.Name = "Xl_Contracts1"
        Me.Xl_Contracts1.ReadOnly = True
        Me.Xl_Contracts1.Size = New System.Drawing.Size(528, 261)
        Me.Xl_Contracts1.TabIndex = 0
        '
        'Frm_RepContractes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 261)
        Me.Controls.Add(Me.Xl_Contracts1)
        Me.Name = "Frm_RepContractes"
        Me.Text = "Contractes de representant"
        CType(Me.Xl_Contracts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Contracts1 As Xl_Contracts
End Class
