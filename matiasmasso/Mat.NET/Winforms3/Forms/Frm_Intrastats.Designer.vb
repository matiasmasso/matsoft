<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Intrastats
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
        Me.Xl_Intrastats1 = New Xl_Intrastats()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarNouIntrastatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeIntroduccióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeExpedicióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_Intrastats1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Intrastats1
        '
        Me.Xl_Intrastats1.AllowUserToAddRows = False
        Me.Xl_Intrastats1.AllowUserToDeleteRows = False
        Me.Xl_Intrastats1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Intrastats1.DisplayObsolets = False
        Me.Xl_Intrastats1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Intrastats1.Filter = Nothing
        Me.Xl_Intrastats1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Intrastats1.MouseIsDown = False
        Me.Xl_Intrastats1.Name = "Xl_Intrastats1"
        Me.Xl_Intrastats1.Size = New System.Drawing.Size(602, 214)
        Me.Xl_Intrastats1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(602, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerarNouIntrastatToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'GenerarNouIntrastatToolStripMenuItem
        '
        Me.GenerarNouIntrastatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeIntroduccióToolStripMenuItem, Me.DeExpedicióToolStripMenuItem})
        Me.GenerarNouIntrastatToolStripMenuItem.Name = "GenerarNouIntrastatToolStripMenuItem"
        Me.GenerarNouIntrastatToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.GenerarNouIntrastatToolStripMenuItem.Text = "Generar nou Intrastat"
        '
        'DeIntroduccióToolStripMenuItem
        '
        Me.DeIntroduccióToolStripMenuItem.Name = "DeIntroduccióToolStripMenuItem"
        Me.DeIntroduccióToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.DeIntroduccióToolStripMenuItem.Text = "de Introducció"
        '
        'DeExpedicióToolStripMenuItem
        '
        Me.DeExpedicióToolStripMenuItem.Name = "DeExpedicióToolStripMenuItem"
        Me.DeExpedicióToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.DeExpedicióToolStripMenuItem.Text = "de expedició"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Xl_Intrastats1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(602, 237)
        Me.Panel1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 214)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(602, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_Intrastats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 261)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_Intrastats"
        Me.Text = "Intrastats"
        CType(Me.Xl_Intrastats1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Intrastats1 As Xl_Intrastats
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GenerarNouIntrastatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeIntroduccióToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeExpedicióToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
