<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AuditSocks
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
        Me.ImportarDeVivaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AsignarUltimCostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_AuditStocks1 = New Mat.Net.Xl_AuditStocks()
        Me.Xl_Yea1 = New Mat.Net.Xl_Yea()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_AuditStocks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(493, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarDeVivaceToolStripMenuItem, Me.AsignarUltimCostToolStripMenuItem, Me.ExportarToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ImportarDeVivaceToolStripMenuItem
        '
        Me.ImportarDeVivaceToolStripMenuItem.Name = "ImportarDeVivaceToolStripMenuItem"
        Me.ImportarDeVivaceToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ImportarDeVivaceToolStripMenuItem.Text = "Importar de Vivace"
        '
        'AsignarUltimCostToolStripMenuItem
        '
        Me.AsignarUltimCostToolStripMenuItem.Name = "AsignarUltimCostToolStripMenuItem"
        Me.AsignarUltimCostToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.AsignarUltimCostToolStripMenuItem.Text = "Asignar ultim cost"
        '
        'ExportarToolStripMenuItem
        '
        Me.ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem"
        Me.ExportarToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ExportarToolStripMenuItem.Text = "Exportar"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(392, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "exercici:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 339)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(493, 11)
        Me.ProgressBar1.TabIndex = 4
        Me.ProgressBar1.Visible = False
        '
        'Xl_AuditStocks1
        '
        Me.Xl_AuditStocks1.AllowUserToAddRows = False
        Me.Xl_AuditStocks1.AllowUserToDeleteRows = False
        Me.Xl_AuditStocks1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AuditStocks1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AuditStocks1.DisplayObsolets = False
        Me.Xl_AuditStocks1.Filter = Nothing
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_AuditStocks1, "Frm_AuditSocks.htm#Xl_AuditStocks1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_AuditStocks1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_AuditStocks1.Location = New System.Drawing.Point(0, 30)
        Me.Xl_AuditStocks1.MouseIsDown = False
        Me.Xl_AuditStocks1.Name = "Xl_AuditStocks1"
        Me.Xl_AuditStocks1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.Xl_AuditStocks1, True)
        Me.Xl_AuditStocks1.Size = New System.Drawing.Size(493, 277)
        Me.Xl_AuditStocks1.TabIndex = 3
        '
        'Xl_Yea1
        '
        Me.Xl_Yea1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpProviderHG.SetHelpKeyword(Me.Xl_Yea1, "Frm_AuditSocks.htm#Xl_Yea")
        Me.HelpProviderHG.SetHelpNavigator(Me.Xl_Yea1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Xl_Yea1.Location = New System.Drawing.Point(444, 4)
        Me.Xl_Yea1.Name = "Xl_Yea1"
        Me.HelpProviderHG.SetShowHelp(Me.Xl_Yea1, True)
        Me.Xl_Yea1.Size = New System.Drawing.Size(49, 20)
        Me.Xl_Yea1.TabIndex = 1
        Me.Xl_Yea1.Yea = 0
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_AuditSocks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(493, 349)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Xl_AuditStocks1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Yea1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_AuditSocks.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_AuditSocks"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Stocks per auditoria"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_AuditStocks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportarDeVivaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_Yea1 As Xl_Yea
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_AuditStocks1 As Xl_AuditStocks
    Friend WithEvents AsignarUltimCostToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ExportarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpProviderHG As HelpProvider
End Class
