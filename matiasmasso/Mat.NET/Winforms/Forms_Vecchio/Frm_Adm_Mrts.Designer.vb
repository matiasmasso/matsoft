<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Adm_Mrts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Adm_Mrts))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonAmortitzar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonRetroceder = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonAddNew = New System.Windows.Forms.ToolStripButton
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox
        Me.DataGridViewItms = New System.Windows.Forms.DataGridView
        Me.DataGridViewCtas = New System.Windows.Forms.DataGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridViewItms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonAmortitzar, Me.ToolStripButtonRetroceder, Me.ToolStripButtonRefresca, Me.ToolStripButtonExcel, Me.ToolStripButtonAddNew})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(631, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonAmortitzar
        '
        Me.ToolStripButtonAmortitzar.Image = CType(resources.GetObject("ToolStripButtonAmortitzar.Image"), System.Drawing.Image)
        Me.ToolStripButtonAmortitzar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonAmortitzar.Name = "ToolStripButtonAmortitzar"
        Me.ToolStripButtonAmortitzar.Size = New System.Drawing.Size(81, 22)
        Me.ToolStripButtonAmortitzar.Text = "amortitzar"
        '
        'ToolStripButtonRetroceder
        '
        Me.ToolStripButtonRetroceder.Image = My.Resources.Resources.UNDO
        Me.ToolStripButtonRetroceder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRetroceder.Name = "ToolStripButtonRetroceder"
        Me.ToolStripButtonRetroceder.Size = New System.Drawing.Size(78, 22)
        Me.ToolStripButtonRetroceder.Text = "retrocedir"
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.Image = My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(68, 22)
        Me.ToolStripButtonRefresca.Text = "refresca"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(53, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        '
        'ToolStripButtonAddNew
        '
        Me.ToolStripButtonAddNew.Image = My.Resources.Resources.NewDoc
        Me.ToolStripButtonAddNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonAddNew.Name = "ToolStripButtonAddNew"
        Me.ToolStripButtonAddNew.Size = New System.Drawing.Size(48, 22)
        Me.ToolStripButtonAddNew.Text = "Alta"
        '
        'ComboBoxYea
        '
        Me.ComboBoxYea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYea.Location = New System.Drawing.Point(573, 2)
        Me.ComboBoxYea.Name = "ComboBoxYea"
        Me.ComboBoxYea.Size = New System.Drawing.Size(56, 21)
        Me.ComboBoxYea.TabIndex = 1
        '
        'DataGridViewItms
        '
        Me.DataGridViewItms.AllowUserToAddRows = False
        Me.DataGridViewItms.AllowUserToDeleteRows = False
        Me.DataGridViewItms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewItms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewItms.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewItms.Name = "DataGridViewItms"
        Me.DataGridViewItms.ReadOnly = True
        Me.DataGridViewItms.Size = New System.Drawing.Size(631, 306)
        Me.DataGridViewItms.TabIndex = 1
        '
        'DataGridViewCtas
        '
        Me.DataGridViewCtas.AllowUserToAddRows = False
        Me.DataGridViewCtas.AllowUserToDeleteRows = False
        Me.DataGridViewCtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCtas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCtas.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewCtas.Name = "DataGridViewCtas"
        Me.DataGridViewCtas.ReadOnly = True
        Me.DataGridViewCtas.Size = New System.Drawing.Size(631, 160)
        Me.DataGridViewCtas.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 29)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewCtas)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewItms)
        Me.SplitContainer1.Size = New System.Drawing.Size(631, 470)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 502)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(631, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(16, 17)
        Me.ToolStripStatusLabel1.Text = "..."
        '
        'Frm_Adm_Mrts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 524)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ComboBoxYea)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Adm_Mrts"
        Me.Text = "AMORTITZACIONS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridViewItms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ComboBoxYea As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripButtonAmortitzar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRetroceder As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonAddNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridViewItms As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCtas As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
End Class
