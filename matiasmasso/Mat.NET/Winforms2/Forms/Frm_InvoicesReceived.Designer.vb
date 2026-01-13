<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InvoicesReceived
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Xl_InvoicesReceived1 = New Mat.Net.Xl_InvoicesReceived()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ComboBoxProveidors = New System.Windows.Forms.ComboBox()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NovaFacturaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.Xl_InvoicesReceived1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_InvoicesReceived1
        '
        Me.Xl_InvoicesReceived1.AllowUserToAddRows = False
        Me.Xl_InvoicesReceived1.AllowUserToDeleteRows = False
        Me.Xl_InvoicesReceived1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoicesReceived1.DisplayObsolets = False
        Me.Xl_InvoicesReceived1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InvoicesReceived1.Filter = Nothing
        Me.Xl_InvoicesReceived1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InvoicesReceived1.MouseIsDown = False
        Me.Xl_InvoicesReceived1.Name = "Xl_InvoicesReceived1"
        Me.Xl_InvoicesReceived1.ReadOnly = True
        Me.Xl_InvoicesReceived1.Size = New System.Drawing.Size(452, 243)
        Me.Xl_InvoicesReceived1.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_InvoicesReceived1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 266)
        Me.Panel1.TabIndex = 4
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 243)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(452, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 4
        '
        'ComboBoxProveidors
        '
        Me.ComboBoxProveidors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxProveidors.FormattingEnabled = True
        Me.ComboBoxProveidors.Location = New System.Drawing.Point(63, 1)
        Me.ComboBoxProveidors.Name = "ComboBoxProveidors"
        Me.ComboBoxProveidors.Size = New System.Drawing.Size(222, 21)
        Me.ComboBoxProveidors.TabIndex = 5
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(289, 1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 6
        Me.Xl_Years1.Value = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(452, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem, Me.NovaFacturaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'NovaFacturaToolStripMenuItem
        '
        Me.NovaFacturaToolStripMenuItem.Name = "NovaFacturaToolStripMenuItem"
        Me.NovaFacturaToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.NovaFacturaToolStripMenuItem.Text = "Nova factura"
        '
        'Frm_InvoicesReceived
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 299)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.ComboBoxProveidors)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_InvoicesReceived"
        Me.Text = "Factures de compres"
        CType(Me.Xl_InvoicesReceived1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_InvoicesReceived1 As Xl_InvoicesReceived
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ComboBoxProveidors As ComboBox
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NovaFacturaToolStripMenuItem As ToolStripMenuItem
End Class
