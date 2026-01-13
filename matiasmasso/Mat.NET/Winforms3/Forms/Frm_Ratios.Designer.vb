<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ratios
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_Ratios1 = New Xl_Ratios()
        CType(Me.Xl_Ratios1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(13, 13)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 38)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Xl_Ratios1
        '
        Me.Xl_Ratios1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Ratios1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Ratios1.DisplayObsolets = False
        Me.Xl_Ratios1.Filter = Nothing
        Me.Xl_Ratios1.Location = New System.Drawing.Point(1, 77)
        Me.Xl_Ratios1.MouseIsDown = False
        Me.Xl_Ratios1.Name = "Xl_Ratios1"
        Me.Xl_Ratios1.RowTemplate.Height = 40
        Me.Xl_Ratios1.Size = New System.Drawing.Size(1175, 681)
        Me.Xl_Ratios1.TabIndex = 1
        '
        'Frm_Ratios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1393, 846)
        Me.Controls.Add(Me.Xl_Ratios1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "Frm_Ratios"
        Me.Text = "Ratios"
        CType(Me.Xl_Ratios1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Xl_Ratios1 As Xl_Ratios
End Class
