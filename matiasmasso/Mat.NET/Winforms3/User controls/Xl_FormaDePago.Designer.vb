Partial Public Class Xl_FormaDePago
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MandatoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BankToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BranchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_Iban1 = New Mat.Net.Xl_Iban()
        Me.LabelMissingMandato = New System.Windows.Forms.Label()
        Me.PictureBoxMissingMandato = New System.Windows.Forms.PictureBox()
        Me.ButtonEdit = New System.Windows.Forms.Button()
        Me.LabelText = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.PictureBoxMissingMandato, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GroupBox1.Controls.Add(Me.Xl_Iban1)
        Me.GroupBox1.Controls.Add(Me.LabelMissingMandato)
        Me.GroupBox1.Controls.Add(Me.PictureBoxMissingMandato)
        Me.GroupBox1.Controls.Add(Me.ButtonEdit)
        Me.GroupBox1.Controls.Add(Me.LabelText)
        Me.GroupBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GroupBox1.Location = New System.Drawing.Point(7, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(279, 122)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Forma de pago"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MandatoToolStripMenuItem, Me.BankToolStripMenuItem, Me.BranchToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(215, 70)
        '
        'MandatoToolStripMenuItem
        '
        Me.MandatoToolStripMenuItem.Name = "MandatoToolStripMenuItem"
        Me.MandatoToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.MandatoToolStripMenuItem.Text = "Mandato"
        '
        'BankToolStripMenuItem
        '
        Me.BankToolStripMenuItem.Name = "BankToolStripMenuItem"
        Me.BankToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.BankToolStripMenuItem.Text = "Fitxa del banc"
        '
        'BranchToolStripMenuItem
        '
        Me.BranchToolStripMenuItem.Name = "BranchToolStripMenuItem"
        Me.BranchToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.BranchToolStripMenuItem.Text = "Fitxa de la oficina bancària"
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Iban1.Location = New System.Drawing.Point(20, 70)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(245, 50)
        Me.Xl_Iban1.TabIndex = 7
        '
        'LabelMissingMandato
        '
        Me.LabelMissingMandato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMissingMandato.AutoSize = True
        Me.LabelMissingMandato.ContextMenuStrip = Me.ContextMenuStrip1
        Me.LabelMissingMandato.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMissingMandato.ForeColor = System.Drawing.Color.Red
        Me.LabelMissingMandato.Location = New System.Drawing.Point(135, 51)
        Me.LabelMissingMandato.Name = "LabelMissingMandato"
        Me.LabelMissingMandato.Size = New System.Drawing.Size(130, 13)
        Me.LabelMissingMandato.TabIndex = 6
        Me.LabelMissingMandato.Text = "falta mandato bancari"
        Me.LabelMissingMandato.Visible = False
        '
        'PictureBoxMissingMandato
        '
        Me.PictureBoxMissingMandato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxMissingMandato.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBoxMissingMandato.Image = Global.Mat.Net.My.Resources.Resources.warn
        Me.PictureBoxMissingMandato.Location = New System.Drawing.Point(122, 48)
        Me.PictureBoxMissingMandato.Name = "PictureBoxMissingMandato"
        Me.PictureBoxMissingMandato.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxMissingMandato.TabIndex = 5
        Me.PictureBoxMissingMandato.TabStop = False
        Me.PictureBoxMissingMandato.Visible = False
        '
        'ButtonEdit
        '
        Me.ButtonEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEdit.Location = New System.Drawing.Point(239, 11)
        Me.ButtonEdit.Name = "ButtonEdit"
        Me.ButtonEdit.Size = New System.Drawing.Size(26, 23)
        Me.ButtonEdit.TabIndex = 4
        Me.ButtonEdit.Text = "..."
        '
        'LabelText
        '
        Me.LabelText.AutoSize = True
        Me.LabelText.ContextMenuStrip = Me.ContextMenuStrip1
        Me.LabelText.Location = New System.Drawing.Point(7, 20)
        Me.LabelText.Name = "LabelText"
        Me.LabelText.Size = New System.Drawing.Size(54, 13)
        Me.LabelText.TabIndex = 1
        Me.LabelText.Text = "LabelText"
        '
        'Xl_FormaDePago
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "Xl_FormaDePago"
        Me.Size = New System.Drawing.Size(287, 129)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.PictureBoxMissingMandato, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LabelText As System.Windows.Forms.Label
    Friend WithEvents ButtonEdit As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MandatoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BankToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelMissingMandato As System.Windows.Forms.Label
    Friend WithEvents PictureBoxMissingMandato As System.Windows.Forms.PictureBox
    Friend WithEvents BranchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Xl_Iban1 As Xl_Iban

End Class
