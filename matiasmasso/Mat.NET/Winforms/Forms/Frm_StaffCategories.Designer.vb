<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StaffCategories
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
        Me.Xl_StaffCategories1 = New Winforms.Xl_StaffCategories()
        CType(Me.Xl_StaffCategories1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_StaffCategories1
        '
        Me.Xl_StaffCategories1.AllowUserToAddRows = False
        Me.Xl_StaffCategories1.AllowUserToDeleteRows = False
        Me.Xl_StaffCategories1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StaffCategories1.DisplayObsolets = False
        Me.Xl_StaffCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StaffCategories1.Filter = Nothing
        Me.Xl_StaffCategories1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StaffCategories1.MouseIsDown = False
        Me.Xl_StaffCategories1.Name = "Xl_StaffCategories1"
        Me.Xl_StaffCategories1.ReadOnly = True
        Me.Xl_StaffCategories1.Size = New System.Drawing.Size(301, 261)
        Me.Xl_StaffCategories1.TabIndex = 0
        '
        'Frm_StaffCategories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 261)
        Me.Controls.Add(Me.Xl_StaffCategories1)
        Me.Name = "Frm_StaffCategories"
        Me.Text = "Categories laborals"
        CType(Me.Xl_StaffCategories1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_StaffCategories1 As Xl_StaffCategories
End Class
