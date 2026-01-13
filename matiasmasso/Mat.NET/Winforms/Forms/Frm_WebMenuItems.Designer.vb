<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WebMenuItems
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
        Me.Xl_WebMenuItems1 = New Winforms.Xl_WebMenuItems()
        Me.SuspendLayout()
        '
        'Xl_WebMenuItems1
        '
        Me.Xl_WebMenuItems1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_WebMenuItems1.Location = New System.Drawing.Point(0, 28)
        Me.Xl_WebMenuItems1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_WebMenuItems1.Name = "Xl_WebMenuItems1"
        Me.Xl_WebMenuItems1.Size = New System.Drawing.Size(280, 256)
        Me.Xl_WebMenuItems1.TabIndex = 0
        '
        'Frm_WebMenuItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 286)
        Me.Controls.Add(Me.Xl_WebMenuItems1)
        Me.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Name = "Frm_WebMenuItems"
        Me.Text = "Menus web"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_WebMenuItems1 As Xl_WebMenuItems
End Class
