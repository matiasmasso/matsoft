<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Adr3
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
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.PictureBoxFlag = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxFlag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxAdr.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxAdr.Multiline = True
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.ReadOnly = True
        Me.TextBoxAdr.Size = New System.Drawing.Size(431, 41)
        Me.TextBoxAdr.TabIndex = 1
        '
        'PictureBoxFlag
        '
        Me.PictureBoxFlag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxFlag.Location = New System.Drawing.Point(401, 26)
        Me.PictureBoxFlag.Name = "PictureBoxFlag"
        Me.PictureBoxFlag.Size = New System.Drawing.Size(30, 15)
        Me.PictureBoxFlag.TabIndex = 2
        Me.PictureBoxFlag.TabStop = False
        '
        'Xl_Adr3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBoxFlag)
        Me.Controls.Add(Me.TextBoxAdr)
        Me.Name = "Xl_Adr3"
        Me.Size = New System.Drawing.Size(431, 41)
        CType(Me.PictureBoxFlag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxAdr As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxFlag As System.Windows.Forms.PictureBox

End Class
