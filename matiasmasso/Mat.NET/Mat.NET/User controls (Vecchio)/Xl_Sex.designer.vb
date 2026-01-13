<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Sex
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
        Me.components = New System.ComponentModel.Container
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemMale = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemFemale = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemNotSet = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = My.Resources.Resources.SexPending
        Me.PictureBox1.InitialImage = My.Resources.Resources.SexPending
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(45, 45)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemMale, Me.ToolStripMenuItemFemale, Me.ToolStripMenuItemNotSet})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(143, 70)
        '
        'ToolStripMenuItemMale
        '
        Me.ToolStripMenuItemMale.CheckOnClick = True
        Me.ToolStripMenuItemMale.Name = "ToolStripMenuItemMale"
        Me.ToolStripMenuItemMale.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemMale.Text = "home"
        '
        'ToolStripMenuItemFemale
        '
        Me.ToolStripMenuItemFemale.CheckOnClick = True
        Me.ToolStripMenuItemFemale.Name = "ToolStripMenuItemFemale"
        Me.ToolStripMenuItemFemale.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemFemale.Text = "dona"
        '
        'ToolStripMenuItemNotSet
        '
        Me.ToolStripMenuItemNotSet.Checked = True
        Me.ToolStripMenuItemNotSet.CheckOnClick = True
        Me.ToolStripMenuItemNotSet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItemNotSet.Name = "ToolStripMenuItemNotSet"
        Me.ToolStripMenuItemNotSet.Size = New System.Drawing.Size(142, 22)
        Me.ToolStripMenuItemNotSet.Text = "indeterminat"
        '
        'Xl_Sex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Xl_Sex"
        Me.Size = New System.Drawing.Size(45, 45)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemMale As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemFemale As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemNotSet As System.Windows.Forms.ToolStripMenuItem

End Class
