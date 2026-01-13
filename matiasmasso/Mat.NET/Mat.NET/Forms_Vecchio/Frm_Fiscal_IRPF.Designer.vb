<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fiscal_IRPF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Fiscal_IRPF))
        Me.ComboBoxBanc = New System.Windows.Forms.ComboBox
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonXls = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonFitxer = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewCtas = New System.Windows.Forms.DataGridView
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewSctas = New System.Windows.Forms.DataGridView
        Me.DataGridViewCcas = New System.Windows.Forms.DataGridView
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewSctas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewCcas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxBanc
        '
        Me.ComboBoxBanc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxBanc.FormattingEnabled = True
        Me.ComboBoxBanc.Location = New System.Drawing.Point(1, 364)
        Me.ComboBoxBanc.Name = "ComboBoxBanc"
        Me.ComboBoxBanc.Size = New System.Drawing.Size(190, 21)
        Me.ComboBoxBanc.TabIndex = 7
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Location = New System.Drawing.Point(395, 361)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(87, 24)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "OK"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonXls, Me.ToolStripButtonFitxer})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(486, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonXls
        '
        Me.ToolStripButtonXls.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonXls.Image = CType(resources.GetObject("ToolStripButtonXls.Image"), System.Drawing.Image)
        Me.ToolStripButtonXls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonXls.Name = "ToolStripButtonXls"
        Me.ToolStripButtonXls.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonXls.Text = "Excel"
        '
        'ToolStripButtonFitxer
        '
        Me.ToolStripButtonFitxer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFitxer.Image = My.Resources.Resources.save_16
        Me.ToolStripButtonFitxer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFitxer.Name = "ToolStripButtonFitxer"
        Me.ToolStripButtonFitxer.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFitxer.Text = "Fitxer"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 28)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewCtas)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(485, 330)
        Me.SplitContainer1.SplitterDistance = 94
        Me.SplitContainer1.TabIndex = 8
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
        Me.DataGridViewCtas.Size = New System.Drawing.Size(485, 94)
        Me.DataGridViewCtas.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewSctas)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridViewCcas)
        Me.SplitContainer2.Size = New System.Drawing.Size(485, 232)
        Me.SplitContainer2.SplitterDistance = 115
        Me.SplitContainer2.TabIndex = 9
        '
        'DataGridViewSctas
        '
        Me.DataGridViewSctas.AllowUserToAddRows = False
        Me.DataGridViewSctas.AllowUserToDeleteRows = False
        Me.DataGridViewSctas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSctas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewSctas.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewSctas.Name = "DataGridViewSctas"
        Me.DataGridViewSctas.ReadOnly = True
        Me.DataGridViewSctas.Size = New System.Drawing.Size(485, 115)
        Me.DataGridViewSctas.TabIndex = 1
        '
        'DataGridViewCcas
        '
        Me.DataGridViewCcas.AllowUserToAddRows = False
        Me.DataGridViewCcas.AllowUserToDeleteRows = False
        Me.DataGridViewCcas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCcas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCcas.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewCcas.Name = "DataGridViewCcas"
        Me.DataGridViewCcas.ReadOnly = True
        Me.DataGridViewCcas.Size = New System.Drawing.Size(485, 113)
        Me.DataGridViewCcas.TabIndex = 1
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(394, 2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 9
        '
        'Frm_Fiscal_IRPF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 387)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ComboBoxBanc)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Fiscal_IRPF"
        Me.Text = "IRPF"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewCtas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewSctas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewCcas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxBanc As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonXls As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewCtas As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewSctas As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCcas As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButtonFitxer As System.Windows.Forms.ToolStripButton
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
End Class
