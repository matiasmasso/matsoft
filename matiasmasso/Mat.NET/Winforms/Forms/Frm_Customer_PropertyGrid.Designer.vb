<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Customer_PropertyGrid
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
        Me.Xl_PropertyGrid_Customer1 = New Winforms.Xl_PropertyGrid_Customer()
        Me.ButtonOk = New System.Windows.Forms.Button()
        CType(Me.Xl_PropertyGrid_Customer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PropertyGrid_Customer1
        '
        Me.Xl_PropertyGrid_Customer1.AllowUserToAddRows = False
        Me.Xl_PropertyGrid_Customer1.AllowUserToDeleteRows = False
        Me.Xl_PropertyGrid_Customer1.AllowUserToResizeRows = False
        Me.Xl_PropertyGrid_Customer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PropertyGrid_Customer1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PropertyGrid_Customer1.ColumnHeadersVisible = False
        Me.Xl_PropertyGrid_Customer1.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Xl_PropertyGrid_Customer1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PropertyGrid_Customer1.MultiSelect = False
        Me.Xl_PropertyGrid_Customer1.Name = "Xl_PropertyGrid_Customer1"
        Me.Xl_PropertyGrid_Customer1.RowHeadersVisible = False
        Me.Xl_PropertyGrid_Customer1.RowTemplate.Height = 17
        Me.Xl_PropertyGrid_Customer1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Xl_PropertyGrid_Customer1.Size = New System.Drawing.Size(284, 229)
        Me.Xl_PropertyGrid_Customer1.TabIndex = 0
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(206, 235)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOk.TabIndex = 1
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'Frm_Customer_PropertyGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.Xl_PropertyGrid_Customer1)
        Me.Name = "Frm_Customer_PropertyGrid"
        Me.Text = "Client"
        CType(Me.Xl_PropertyGrid_Customer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_PropertyGrid_Customer1 As Winforms.Xl_PropertyGrid_Customer
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
End Class
