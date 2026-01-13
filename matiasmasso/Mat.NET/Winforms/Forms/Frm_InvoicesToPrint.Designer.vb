<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InvoicesToPrint
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
        Me.Xl_ProgressBar1 = New Winforms.Xl_ProgressBar()
        Me.Xl_InvoicesToPrint1 = New Winforms.Xl_InvoicesToPrint()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSelectNone = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSelectRest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemRecuperar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemPaper = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemEmail = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemEdi = New System.Windows.Forms.ToolStripMenuItem()
        Me.ButtonOk = New System.Windows.Forms.Button()
        CType(Me.Xl_InvoicesToPrint1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 458)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(550, 30)
        Me.Xl_ProgressBar1.TabIndex = 0
        Me.Xl_ProgressBar1.Visible = False
        '
        'Xl_InvoicesToPrint1
        '
        Me.Xl_InvoicesToPrint1.AllowUserToAddRows = False
        Me.Xl_InvoicesToPrint1.AllowUserToDeleteRows = False
        Me.Xl_InvoicesToPrint1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_InvoicesToPrint1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoicesToPrint1.DisplayObsolets = False
        Me.Xl_InvoicesToPrint1.Filter = Nothing
        Me.Xl_InvoicesToPrint1.Location = New System.Drawing.Point(0, 30)
        Me.Xl_InvoicesToPrint1.Name = "Xl_InvoicesToPrint1"
        Me.Xl_InvoicesToPrint1.ReadOnly = True
        Me.Xl_InvoicesToPrint1.Size = New System.Drawing.Size(550, 428)
        Me.Xl_InvoicesToPrint1.TabIndex = 1
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(400, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 10
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(550, 24)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemSelectAll, Me.ToolStripMenuItemSelectNone, Me.ToolStripMenuItemSelectRest, Me.ToolStripMenuItemRecuperar, Me.ToolStripMenuItem1, Me.ToolStripMenuItemPaper, Me.ToolStripMenuItemEmail, Me.ToolStripMenuItemEdi})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ToolStripMenuItemSelectAll
        '
        Me.ToolStripMenuItemSelectAll.Name = "ToolStripMenuItemSelectAll"
        Me.ToolStripMenuItemSelectAll.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemSelectAll.Text = "seleccionar tot"
        '
        'ToolStripMenuItemSelectNone
        '
        Me.ToolStripMenuItemSelectNone.Name = "ToolStripMenuItemSelectNone"
        Me.ToolStripMenuItemSelectNone.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemSelectNone.Text = "deseleccionar tot"
        '
        'ToolStripMenuItemSelectRest
        '
        Me.ToolStripMenuItemSelectRest.Name = "ToolStripMenuItemSelectRest"
        Me.ToolStripMenuItemSelectRest.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemSelectRest.Text = "deseleccionar la resta"
        '
        'ToolStripMenuItemRecuperar
        '
        Me.ToolStripMenuItemRecuperar.Name = "ToolStripMenuItemRecuperar"
        Me.ToolStripMenuItemRecuperar.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemRecuperar.Text = "recuperar de una data"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(186, 6)
        '
        'ToolStripMenuItemPaper
        '
        Me.ToolStripMenuItemPaper.Checked = True
        Me.ToolStripMenuItemPaper.CheckOnClick = True
        Me.ToolStripMenuItemPaper.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItemPaper.Name = "ToolStripMenuItemPaper"
        Me.ToolStripMenuItemPaper.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemPaper.Text = "paper"
        '
        'ToolStripMenuItemEmail
        '
        Me.ToolStripMenuItemEmail.Checked = True
        Me.ToolStripMenuItemEmail.CheckOnClick = True
        Me.ToolStripMenuItemEmail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItemEmail.Name = "ToolStripMenuItemEmail"
        Me.ToolStripMenuItemEmail.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemEmail.Text = "email"
        '
        'ToolStripMenuItemEdi
        '
        Me.ToolStripMenuItemEdi.Checked = True
        Me.ToolStripMenuItemEdi.CheckOnClick = True
        Me.ToolStripMenuItemEdi.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItemEdi.Name = "ToolStripMenuItemEdi"
        Me.ToolStripMenuItemEdi.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemEdi.Text = "edi"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOk.Location = New System.Drawing.Point(474, 458)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(76, 30)
        Me.ButtonOk.TabIndex = 12
        Me.ButtonOk.Text = "enviar"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'Frm_InvoicesToPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 488)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_InvoicesToPrint1)
        Me.Controls.Add(Me.Xl_ProgressBar1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_InvoicesToPrint"
        Me.Text = "Enviament de factures"
        CType(Me.Xl_InvoicesToPrint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents Xl_InvoicesToPrint1 As Xl_InvoicesToPrint
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemSelectAll As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemSelectNone As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemSelectRest As ToolStripMenuItem
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ToolStripMenuItemRecuperar As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItemPaper As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemEmail As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemEdi As ToolStripMenuItem
End Class
