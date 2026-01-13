<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EDiversaExceptions
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
        Me.Xl_EdiversaExceptions1 = New Xl_EdiversaExceptions()
        Me.LabelSrc = New System.Windows.Forms.Label()
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_EdiversaExceptions1
        '
        Me.Xl_EdiversaExceptions1.AllowUserToAddRows = False
        Me.Xl_EdiversaExceptions1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaExceptions1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_EdiversaExceptions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaExceptions1.Location = New System.Drawing.Point(0, 99)
        Me.Xl_EdiversaExceptions1.Name = "Xl_EdiversaExceptions1"
        Me.Xl_EdiversaExceptions1.ReadOnly = True
        Me.Xl_EdiversaExceptions1.Size = New System.Drawing.Size(572, 218)
        Me.Xl_EdiversaExceptions1.TabIndex = 0
        '
        'LabelSrc
        '
        Me.LabelSrc.AutoSize = True
        Me.LabelSrc.Location = New System.Drawing.Point(13, 13)
        Me.LabelSrc.Name = "LabelSrc"
        Me.LabelSrc.Size = New System.Drawing.Size(28, 13)
        Me.LabelSrc.TabIndex = 1
        Me.LabelSrc.Text = "font:"
        '
        'Frm_EDiversaExceptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 317)
        Me.Controls.Add(Me.LabelSrc)
        Me.Controls.Add(Me.Xl_EdiversaExceptions1)
        Me.Name = "Frm_EDiversaExceptions"
        Me.Text = "Validació incidencies EDI"
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_EdiversaExceptions1 As Xl_EdiversaExceptions
    Friend WithEvents LabelSrc As Label
End Class
