<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_JsonSchemas
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
        Me.Xl_JsonSchemas1 = New Xl_JsonSchemas()
        CType(Me.Xl_JsonSchemas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_JsonSchemas1
        '
        Me.Xl_JsonSchemas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_JsonSchemas1.DisplayObsolets = False
        Me.Xl_JsonSchemas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_JsonSchemas1.Filter = Nothing
        Me.Xl_JsonSchemas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_JsonSchemas1.MouseIsDown = False
        Me.Xl_JsonSchemas1.Name = "Xl_JsonSchemas1"
        Me.Xl_JsonSchemas1.Size = New System.Drawing.Size(487, 244)
        Me.Xl_JsonSchemas1.TabIndex = 0
        '
        'Frm_JsonSchemas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 244)
        Me.Controls.Add(Me.Xl_JsonSchemas1)
        Me.Name = "Frm_JsonSchemas"
        Me.Text = "Json Schemas"
        CType(Me.Xl_JsonSchemas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_JsonSchemas1 As Xl_JsonSchemas
End Class
