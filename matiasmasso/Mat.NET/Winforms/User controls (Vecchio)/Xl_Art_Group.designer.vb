<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Art_Group
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
        Me.Xl_ArtWiths1 = New Winforms.Xl_ArtWiths()
        Me.CheckBoxGroupParentOnly = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Xl_ArtWiths1
        '
        Me.Xl_ArtWiths1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ArtWiths1.Location = New System.Drawing.Point(3, 27)
        Me.Xl_ArtWiths1.Name = "Xl_ArtWiths1"
        Me.Xl_ArtWiths1.Size = New System.Drawing.Size(315, 346)
        Me.Xl_ArtWiths1.TabIndex = 5
        '
        'CheckBoxGroupParentOnly
        '
        Me.CheckBoxGroupParentOnly.AutoSize = True
        Me.CheckBoxGroupParentOnly.Location = New System.Drawing.Point(4, 4)
        Me.CheckBoxGroupParentOnly.Name = "CheckBoxGroupParentOnly"
        Me.CheckBoxGroupParentOnly.Size = New System.Drawing.Size(187, 17)
        Me.CheckBoxGroupParentOnly.TabIndex = 6
        Me.CheckBoxGroupParentOnly.Text = "Agrupació comercial de productes"
        Me.CheckBoxGroupParentOnly.UseVisualStyleBackColor = True
        '
        'Xl_Art_Group
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxGroupParentOnly)
        Me.Controls.Add(Me.Xl_ArtWiths1)
        Me.Name = "Xl_Art_Group"
        Me.Size = New System.Drawing.Size(321, 373)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_ArtWiths1 As Winforms.Xl_ArtWiths
    Friend WithEvents CheckBoxGroupParentOnly As System.Windows.Forms.CheckBox

End Class
