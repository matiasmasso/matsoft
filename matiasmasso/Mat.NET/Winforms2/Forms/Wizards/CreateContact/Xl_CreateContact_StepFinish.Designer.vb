<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepFinish
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TextBoxResum = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBoxResum
        '
        Me.TextBoxResum.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxResum.Location = New System.Drawing.Point(29, 29)
        Me.TextBoxResum.Multiline = True
        Me.TextBoxResum.Name = "TextBoxResum"
        Me.TextBoxResum.ReadOnly = True
        Me.TextBoxResum.Size = New System.Drawing.Size(315, 290)
        Me.TextBoxResum.TabIndex = 0
        '
        'Xl_CreateContact_StepFinish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBoxResum)
        Me.Name = "Xl_CreateContact_StepFinish"
        Me.Size = New System.Drawing.Size(373, 359)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxResum As TextBox
End Class
