<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_SiiLog
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
        Me.components = New System.ComponentModel.Container()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextBoxCsv = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxFch
        '
        Me.TextBoxFch.BackColor = System.Drawing.Color.White
        Me.TextBoxFch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxFch.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxFch.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(192, 31)
        Me.TextBoxFch.TabIndex = 0
        Me.TextBoxFch.TabStop = False
        Me.TextBoxFch.Text = "00/00/00 00:00"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(194, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 38)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'TextBoxCsv
        '
        Me.TextBoxCsv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCsv.Location = New System.Drawing.Point(249, -1)
        Me.TextBoxCsv.Name = "TextBoxCsv"
        Me.TextBoxCsv.ReadOnly = True
        Me.TextBoxCsv.Size = New System.Drawing.Size(572, 38)
        Me.TextBoxCsv.TabIndex = 2
        '
        'Xl_SiiLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.TextBoxCsv)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Xl_SiiLog"
        Me.Size = New System.Drawing.Size(824, 45)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxFch As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TextBoxCsv As TextBox
End Class
