<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Vivace_DESADV
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
        Me.Xl_Filename1 = New Winforms.Xl_Filename()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Importar = New System.Windows.Forms.ToolStripMenuItem()
        Me.Exportar = New System.Windows.Forms.ToolStripMenuItem()
        Me.FtpToTradeInn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Filename1
        '
        Me.Xl_Filename1.Filename = Nothing
        Me.Xl_Filename1.IsDirty = False
        Me.Xl_Filename1.Location = New System.Drawing.Point(12, 74)
        Me.Xl_Filename1.Name = "Xl_Filename1"
        Me.Xl_Filename1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Filename1.ReadOnlyLookup = False
        Me.Xl_Filename1.Size = New System.Drawing.Size(414, 20)
        Me.Xl_Filename1.TabIndex = 0
        Me.Xl_Filename1.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Importar fitxer de Vivace"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(438, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Importar, Me.Exportar, Me.FtpToTradeInn})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'Importar
        '
        Me.Importar.Name = "Importar"
        Me.Importar.Size = New System.Drawing.Size(249, 22)
        Me.Importar.Text = "Importar fitxer de Vivace"
        '
        'Exportar
        '
        Me.Exportar.Name = "Exportar"
        Me.Exportar.Size = New System.Drawing.Size(249, 22)
        Me.Exportar.Text = "Exportar DESADV"
        '
        'FtpToTradeInn
        '
        Me.FtpToTradeInn.Name = "FtpToTradeInn"
        Me.FtpToTradeInn.Size = New System.Drawing.Size(249, 22)
        Me.FtpToTradeInn.Text = "Enviar DESADV a TradeInn per Ftp"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 126)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(438, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 4
        Me.ProgressBar1.Visible = False
        '
        'Frm_Vivace_DESADV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 149)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Filename1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "Frm_Vivace_DESADV"
        Me.Text = "DESADV Vivace a TradeInn"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Filename1 As Xl_Filename
    Friend WithEvents Label1 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Importar As ToolStripMenuItem
    Friend WithEvents Exportar As ToolStripMenuItem
    Friend WithEvents FtpToTradeInn As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
