<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Aeat_Mods
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
        Me.components = New System.ComponentModel.Container
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.AnyanteriorToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.AnysegüentToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripComboBoxYea = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.ContextMenuStripDetail = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddNewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EmailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WebToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PdfToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnyanteriorToolStripButton, Me.AnysegüentToolStripButton, Me.ToolStripComboBoxYea, Me.ToolStripButtonRefresca})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(556, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'AnyanteriorToolStripButton
        '
        Me.AnyanteriorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnyanteriorToolStripButton.Image = My.Resources.Resources.SquareArrowBackOrange
        Me.AnyanteriorToolStripButton.Name = "AnyanteriorToolStripButton"
        Me.AnyanteriorToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.AnyanteriorToolStripButton.Text = "any anterior"
        '
        'AnysegüentToolStripButton
        '
        Me.AnysegüentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AnysegüentToolStripButton.Image = My.Resources.Resources.SquareArrowTurquesa
        Me.AnysegüentToolStripButton.Name = "AnysegüentToolStripButton"
        Me.AnysegüentToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.AnysegüentToolStripButton.Text = "any següent"
        '
        'ToolStripComboBoxYea
        '
        Me.ToolStripComboBoxYea.AutoSize = False
        Me.ToolStripComboBoxYea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBoxYea.DropDownWidth = 80
        Me.ToolStripComboBoxYea.MaxLength = 4
        Me.ToolStripComboBoxYea.Name = "ToolStripComboBoxYea"
        Me.ToolStripComboBoxYea.Size = New System.Drawing.Size(80, 23)
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(556, 294)
        Me.SplitContainer1.SplitterDistance = 268
        Me.SplitContainer1.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(268, 294)
        Me.DataGridView1.TabIndex = 2
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.ContextMenuStrip = Me.ContextMenuStripDetail
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(284, 294)
        Me.DataGridView2.TabIndex = 0
        '
        'ContextMenuStripDetail
        '
        Me.ContextMenuStripDetail.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomToolStripMenuItem, Me.AddNewToolStripMenuItem, Me.ExportarToolStripMenuItem, Me.EmailToolStripMenuItem, Me.WebToolStripMenuItem, Me.CopyLinkToolStripMenuItem, Me.PdfToolStripMenuItem})
        Me.ContextMenuStripDetail.Name = "ContextMenuStripDetail"
        Me.ContextMenuStripDetail.Size = New System.Drawing.Size(153, 180)
        '
        'ZoomToolStripMenuItem
        '
        Me.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem"
        Me.ZoomToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ZoomToolStripMenuItem.Text = "zoom"
        '
        'AddNewToolStripMenuItem
        '
        Me.AddNewToolStripMenuItem.Name = "AddNewToolStripMenuItem"
        Me.AddNewToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AddNewToolStripMenuItem.Text = "Afegir"
        '
        'ExportarToolStripMenuItem
        '
        Me.ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem"
        Me.ExportarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExportarToolStripMenuItem.Text = "exportar"
        '
        'EmailToolStripMenuItem
        '
        Me.EmailToolStripMenuItem.Name = "EmailToolStripMenuItem"
        Me.EmailToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EmailToolStripMenuItem.Text = "e-mail"
        '
        'WebToolStripMenuItem
        '
        Me.WebToolStripMenuItem.Name = "WebToolStripMenuItem"
        Me.WebToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.WebToolStripMenuItem.Text = "web"
        '
        'CopyLinkToolStripMenuItem
        '
        Me.CopyLinkToolStripMenuItem.Image = My.Resources.Resources.Copy
        Me.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem"
        Me.CopyLinkToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CopyLinkToolStripMenuItem.Text = "copiar enllaç"
        '
        'PdfToolStripMenuItem
        '
        Me.PdfToolStripMenuItem.Image = My.Resources.Resources.pdf
        Me.PdfToolStripMenuItem.Name = "PdfToolStripMenuItem"
        Me.PdfToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PdfToolStripMenuItem.Text = "pdf"
        '
        'Frm_Aeat_Mods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 319)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Aeat_Mods"
        Me.Text = "MODELS DE DECLARACIONS A HISENDA"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripDetail.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents AnyanteriorToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AnysegüentToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripComboBoxYea As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuStripDetail As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PdfToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
