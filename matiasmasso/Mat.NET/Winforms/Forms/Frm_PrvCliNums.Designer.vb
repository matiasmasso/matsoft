<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PrvCliNums
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
        Me.Xl_PrvCliNums1 = New Winforms.Xl_PrvCliNums()
        CType(Me.Xl_PrvCliNums1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PrvCliNums1
        '
        Me.Xl_PrvCliNums1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PrvCliNums1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrvCliNums1.Filter = Nothing
        Me.Xl_PrvCliNums1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PrvCliNums1.Name = "Xl_PrvCliNums1"
        Me.Xl_PrvCliNums1.Size = New System.Drawing.Size(800, 450)
        Me.Xl_PrvCliNums1.TabIndex = 0
        '
        'Frm_PrvCliNums
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Xl_PrvCliNums1)
        Me.Name = "Frm_PrvCliNums"
        Me.Text = "Numeros de client de "
        CType(Me.Xl_PrvCliNums1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PrvCliNums1 As Xl_PrvCliNums
End Class
