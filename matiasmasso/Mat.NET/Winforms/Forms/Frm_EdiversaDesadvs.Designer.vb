<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EdiversaDesadvs
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
        Me.Xl_EdiversaDesadvs1 = New Winforms.Xl_EdiversaDesadvs()
        CType(Me.Xl_EdiversaDesadvs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_EdiversaDesadvs1
        '
        Me.Xl_EdiversaDesadvs1.AllowUserToAddRows = False
        Me.Xl_EdiversaDesadvs1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaDesadvs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaDesadvs1.DisplayObsolets = False
        Me.Xl_EdiversaDesadvs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaDesadvs1.Filter = Nothing
        Me.Xl_EdiversaDesadvs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaDesadvs1.MouseIsDown = False
        Me.Xl_EdiversaDesadvs1.Name = "Xl_EdiversaDesadvs1"
        Me.Xl_EdiversaDesadvs1.ReadOnly = True
        Me.Xl_EdiversaDesadvs1.RowTemplate.Height = 40
        Me.Xl_EdiversaDesadvs1.Size = New System.Drawing.Size(1445, 593)
        Me.Xl_EdiversaDesadvs1.TabIndex = 0
        '
        'Frm_EdiversaDesadvs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1445, 593)
        Me.Controls.Add(Me.Xl_EdiversaDesadvs1)
        Me.Name = "Frm_EdiversaDesadvs"
        Me.Text = "Despatch Advices (DESADVs)"
        CType(Me.Xl_EdiversaDesadvs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_EdiversaDesadvs1 As Xl_EdiversaDesadvs
End Class
