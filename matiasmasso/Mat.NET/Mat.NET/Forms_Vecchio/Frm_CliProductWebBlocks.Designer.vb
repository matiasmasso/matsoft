<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliProductWebBlocks
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
        Me.Xl_CliProducts1 = New Mat.NET.Xl_CliProducts()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Xl_CliProducts1
        '
        Me.Xl_CliProducts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CliProducts1.Location = New System.Drawing.Point(0, 64)
        Me.Xl_CliProducts1.Name = "Xl_CliProducts1"
        Me.Xl_CliProducts1.Size = New System.Drawing.Size(722, 277)
        Me.Xl_CliProducts1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-3, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(375, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Els següents clients no seran publicitats a la web per els respectius productes."
        '
        'Frm_CliProductWebBlocks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 343)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_CliProducts1)
        Me.Name = "Frm_CliProductWebBlocks"
        Me.Text = "Frm_CliProductWebBlocks"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_CliProducts1 As Mat.NET.Xl_CliProducts
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
