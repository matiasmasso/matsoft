<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_SkuPmcs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Xl_SkuPmcs1 = New Winforms.Xl_SkuPmcs()
        CType(Me.Xl_SkuPmcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_SkuPmcs1
        '
        Me.Xl_SkuPmcs1.AllowUserToAddRows = False
        Me.Xl_SkuPmcs1.AllowUserToDeleteRows = False
        Me.Xl_SkuPmcs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SkuPmcs1.DisplayObsolets = False
        Me.Xl_SkuPmcs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_SkuPmcs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_SkuPmcs1.Name = "Xl_SkuPmcs1"
        Me.Xl_SkuPmcs1.ReadOnly = True
        Me.Xl_SkuPmcs1.Size = New System.Drawing.Size(943, 261)
        Me.Xl_SkuPmcs1.TabIndex = 0
        '
        'Frm_SkuPmcs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 261)
        Me.Controls.Add(Me.Xl_SkuPmcs1)
        Me.Name = "Frm_SkuPmcs"
        Me.Text = "Marges comercials per producte"
        CType(Me.Xl_SkuPmcs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_SkuPmcs1 As Xl_SkuPmcs
End Class
