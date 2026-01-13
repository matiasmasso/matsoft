<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConsumerTickets
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_ConsumerTickets1 = New Winforms.Xl_ConsumerTickets()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_Years1 = New Winforms.Xl_Years()
        Me.RefrescaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_ConsumerTickets1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 427)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(800, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'Xl_ConsumerTickets1
        '
        Me.Xl_ConsumerTickets1.AllowUserToAddRows = False
        Me.Xl_ConsumerTickets1.AllowUserToDeleteRows = False
        Me.Xl_ConsumerTickets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ConsumerTickets1.DisplayObsolets = False
        Me.Xl_ConsumerTickets1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ConsumerTickets1.Filter = Nothing
        Me.Xl_ConsumerTickets1.Location = New System.Drawing.Point(0, 24)
        Me.Xl_ConsumerTickets1.MouseIsDown = False
        Me.Xl_ConsumerTickets1.Name = "Xl_ConsumerTickets1"
        Me.Xl_ConsumerTickets1.ReadOnly = True
        Me.Xl_ConsumerTickets1.Size = New System.Drawing.Size(800, 403)
        Me.Xl_ConsumerTickets1.TabIndex = 2
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(559, 1)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(238, 22)
        Me.Xl_TextboxSearch1.TabIndex = 3
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(395, 1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 4
        Me.Xl_Years1.Value = 0
        '
        'RefrescaToolStripMenuItem
        '
        Me.RefrescaToolStripMenuItem.Name = "RefrescaToolStripMenuItem"
        Me.RefrescaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RefrescaToolStripMenuItem.Text = "Refresca"
        '
        'Frm_ConsumerTickets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_ConsumerTickets1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_ConsumerTickets"
        Me.Text = "Tickets de consumidor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_ConsumerTickets1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Xl_ConsumerTickets1 As Xl_ConsumerTickets
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents RefrescaToolStripMenuItem As ToolStripMenuItem
End Class
