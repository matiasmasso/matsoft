<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_ProgressBar
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.PictureBoxIcon = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonCancel.Location = New System.Drawing.Point(139, 0)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 30)
        Me.ButtonCancel.TabIndex = 0
        Me.ButtonCancel.Text = "Cancel·lar"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 20)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(139, 10)
        Me.ProgressBar1.TabIndex = 1
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelStatus.Location = New System.Drawing.Point(16, 0)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(48, 13)
        Me.LabelStatus.TabIndex = 2
        Me.LabelStatus.Text = "Progress"
        '
        'PictureBoxIcon
        '
        Me.PictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBoxIcon.Image = Global.Mat.Net.My.Resources.Resources.vb
        Me.PictureBoxIcon.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxIcon.Name = "PictureBoxIcon"
        Me.PictureBoxIcon.Size = New System.Drawing.Size(16, 20)
        Me.PictureBoxIcon.TabIndex = 3
        Me.PictureBoxIcon.TabStop = False
        Me.PictureBoxIcon.Visible = False
        '
        'Xl_ProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.PictureBoxIcon)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Name = "Xl_ProgressBar"
        Me.Size = New System.Drawing.Size(214, 30)
        CType(Me.PictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelStatus As Label
    Friend WithEvents PictureBoxIcon As PictureBox
End Class
