Partial Public Class Xl_Iban
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
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarTxtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarImgToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelNoDigits = New System.Windows.Forms.Label()
        Me.SelleccionarForaDeLaUEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(250, 50)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.AllowDrop = True
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomToolStripMenuItem, Me.CopiarTxtToolStripMenuItem, Me.CopiarImgToolStripMenuItem, Me.SelleccionarForaDeLaUEToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(209, 114)
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.ZoomToolStripMenuItem.Text = "zoom"
        '
        'CopiarTxtToolStripMenuItem
        '
        Me.CopiarTxtToolStripMenuItem.Name = "CopiarTxtToolStripMenuItem"
        Me.CopiarTxtToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CopiarTxtToolStripMenuItem.Text = "copiar texte"
        '
        'CopiarImgToolStripMenuItem
        '
        Me.CopiarImgToolStripMenuItem.Name = "CopiarImgToolStripMenuItem"
        Me.CopiarImgToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CopiarImgToolStripMenuItem.Text = "copiar image"
        '
        'LabelNoDigits
        '
        Me.LabelNoDigits.AutoSize = True
        Me.LabelNoDigits.Location = New System.Drawing.Point(52, 19)
        Me.LabelNoDigits.Name = "LabelNoDigits"
        Me.LabelNoDigits.Size = New System.Drawing.Size(149, 13)
        Me.LabelNoDigits.TabIndex = 2
        Me.LabelNoDigits.Text = "(doble clic per entrar els digits)"
        Me.LabelNoDigits.Visible = False
        '
        'SelleccionarForaDeLaUEToolStripMenuItem
        '
        Me.SelleccionarForaDeLaUEToolStripMenuItem.Name = "SelleccionarForaDeLaUEToolStripMenuItem"
        Me.SelleccionarForaDeLaUEToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.SelleccionarForaDeLaUEToolStripMenuItem.Text = "sel.leccionar fora de la UE"
        '
        'Xl_Iban
        '
        Me.Controls.Add(Me.LabelNoDigits)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Xl_Iban"
        Me.Size = New System.Drawing.Size(250, 50)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopiarTxtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopiarImgToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelNoDigits As Label
    Friend WithEvents SelleccionarForaDeLaUEToolStripMenuItem As ToolStripMenuItem
End Class
