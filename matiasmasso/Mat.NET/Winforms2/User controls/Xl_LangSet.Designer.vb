<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_LangSet
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
        Me.CheckBoxEsp = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCat = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEng = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPor = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'CheckBoxEsp
        '
        Me.CheckBoxEsp.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxEsp.AutoSize = True
        Me.CheckBoxEsp.Location = New System.Drawing.Point(0, 0)
        Me.CheckBoxEsp.Name = "CheckBoxEsp"
        Me.CheckBoxEsp.Size = New System.Drawing.Size(38, 23)
        Me.CheckBoxEsp.TabIndex = 0
        Me.CheckBoxEsp.Text = "ESP"
        Me.CheckBoxEsp.UseVisualStyleBackColor = True
        '
        'CheckBoxCat
        '
        Me.CheckBoxCat.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxCat.AutoSize = True
        Me.CheckBoxCat.Location = New System.Drawing.Point(44, 0)
        Me.CheckBoxCat.Name = "CheckBoxCat"
        Me.CheckBoxCat.Size = New System.Drawing.Size(38, 23)
        Me.CheckBoxCat.TabIndex = 1
        Me.CheckBoxCat.Text = "CAT"
        Me.CheckBoxCat.UseVisualStyleBackColor = True
        '
        'CheckBoxEng
        '
        Me.CheckBoxEng.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxEng.AutoSize = True
        Me.CheckBoxEng.Location = New System.Drawing.Point(88, 0)
        Me.CheckBoxEng.Name = "CheckBoxEng"
        Me.CheckBoxEng.Size = New System.Drawing.Size(40, 23)
        Me.CheckBoxEng.TabIndex = 2
        Me.CheckBoxEng.Text = "ENG"
        Me.CheckBoxEng.UseVisualStyleBackColor = True
        '
        'CheckBoxPor
        '
        Me.CheckBoxPor.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxPor.AutoSize = True
        Me.CheckBoxPor.Location = New System.Drawing.Point(134, 0)
        Me.CheckBoxPor.Name = "CheckBoxPor"
        Me.CheckBoxPor.Size = New System.Drawing.Size(40, 23)
        Me.CheckBoxPor.TabIndex = 3
        Me.CheckBoxPor.Text = "POR"
        Me.CheckBoxPor.UseVisualStyleBackColor = True
        '
        'Xl_LangSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxPor)
        Me.Controls.Add(Me.CheckBoxEng)
        Me.Controls.Add(Me.CheckBoxCat)
        Me.Controls.Add(Me.CheckBoxEsp)
        Me.Name = "Xl_LangSet"
        Me.Size = New System.Drawing.Size(176, 24)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBoxEsp As CheckBox
    Friend WithEvents CheckBoxCat As CheckBox
    Friend WithEvents CheckBoxEng As CheckBox
    Friend WithEvents CheckBoxPor As CheckBox
End Class
