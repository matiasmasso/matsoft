<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PaymentGateways
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
        Me.Xl_PaymentGateways1 = New Xl_PaymentGateways()
        CType(Me.Xl_PaymentGateways1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PaymentGateways1
        '
        Me.Xl_PaymentGateways1.AllowUserToAddRows = False
        Me.Xl_PaymentGateways1.AllowUserToDeleteRows = False
        Me.Xl_PaymentGateways1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PaymentGateways1.DisplayObsolets = False
        Me.Xl_PaymentGateways1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PaymentGateways1.Filter = Nothing
        Me.Xl_PaymentGateways1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PaymentGateways1.Name = "Xl_PaymentGateways1"
        Me.Xl_PaymentGateways1.ReadOnly = True
        Me.Xl_PaymentGateways1.Size = New System.Drawing.Size(398, 261)
        Me.Xl_PaymentGateways1.TabIndex = 0
        '
        'Frm_PaymentGateways
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 261)
        Me.Controls.Add(Me.Xl_PaymentGateways1)
        Me.Name = "Frm_PaymentGateways"
        Me.Text = "Configuracio Tpvs"
        CType(Me.Xl_PaymentGateways1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PaymentGateways1 As Xl_PaymentGateways
End Class
