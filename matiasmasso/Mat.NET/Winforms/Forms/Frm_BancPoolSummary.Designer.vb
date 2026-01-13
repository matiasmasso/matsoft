<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancPoolSummary
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
        Me.Xl_BancPoolSummary1 = New Winforms.Xl_BancPoolSummary()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        CType(Me.Xl_BancPoolSummary1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_BancPoolSummary1
        '
        Me.Xl_BancPoolSummary1.AllowUserToAddRows = False
        Me.Xl_BancPoolSummary1.AllowUserToDeleteRows = False
        Me.Xl_BancPoolSummary1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BancPoolSummary1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BancPoolSummary1.DisplayObsolets = False
        Me.Xl_BancPoolSummary1.Location = New System.Drawing.Point(0, 46)
        Me.Xl_BancPoolSummary1.Name = "Xl_BancPoolSummary1"
        Me.Xl_BancPoolSummary1.ReadOnly = True
        Me.Xl_BancPoolSummary1.Size = New System.Drawing.Size(659, 215)
        Me.Xl_BancPoolSummary1.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(548, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(99, 20)
        Me.DateTimePicker1.TabIndex = 1
        '
        'Frm_BancPoolSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 261)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Xl_BancPoolSummary1)
        Me.Name = "Frm_BancPoolSummary"
        Me.Text = "Pool Bancari"
        CType(Me.Xl_BancPoolSummary1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_BancPoolSummary1 As Xl_BancPoolSummary
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
