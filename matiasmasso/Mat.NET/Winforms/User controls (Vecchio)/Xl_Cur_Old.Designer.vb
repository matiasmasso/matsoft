Partial Public Class Xl_Cur_Old
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CanviarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox1.CausesValidation = False
        Me.TextBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(30, 20)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.TabStop = False
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.AllowDrop = True
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomToolStripMenuItem, Me.CanviarToolStripMenuItem})
        Me.ContextMenuStrip1.Location = New System.Drawing.Point(19, 31)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(96, 48)
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Image = My.Resources.prismatics_16
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        'Me.ZoomToolStripMenuItem.SettingsKey = "Xl_Cur.ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Text = "Zoom"
        '
        'CanviarToolStripMenuItem
        '
        Me.CanviarToolStripMenuItem.Image = My.Resources.refresca_16
        Me.CanviarToolStripMenuItem.Name = "CanviarToolStripMenuItem"
        'Me.CanviarToolStripMenuItem.SettingsKey = "Xl_Cur.CanviarToolStripMenuItem"
        Me.CanviarToolStripMenuItem.Text = "Canviar"
        '
        'Xl_Cur
        '
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Xl_Cur"
        Me.Size = New System.Drawing.Size(30, 20)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CanviarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
