<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fiscal_IVA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Fiscal_IVA))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonXls = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonFitxer = New System.Windows.Forms.ToolStripButton
        Me.LabelPaid = New System.Windows.Forms.Label
        Me.ButtonPayNow = New System.Windows.Forms.Button
        Me.DateTimePickerPay = New System.Windows.Forms.DateTimePicker
        Me.ComboBoxBancs = New System.Windows.Forms.ComboBox
        Me.LabelFchs = New System.Windows.Forms.Label
        Me.ComboBoxYeas = New System.Windows.Forms.ComboBox
        Me.ComboBoxPeriods = New System.Windows.Forms.ComboBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonXls, Me.ToolStripButtonFitxer})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(686, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonXls
        '
        Me.ToolStripButtonXls.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonXls.Image = CType(resources.GetObject("ToolStripButtonXls.Image"), System.Drawing.Image)
        Me.ToolStripButtonXls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonXls.Name = "ToolStripButtonXls"
        Me.ToolStripButtonXls.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonXls.Text = "ToolStripButton1"
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
        'LabelPaid
        '
        Me.LabelPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelPaid.AutoSize = True
        Me.LabelPaid.Location = New System.Drawing.Point(299, 399)
        Me.LabelPaid.Name = "LabelPaid"
        Me.LabelPaid.Size = New System.Drawing.Size(121, 13)
        Me.LabelPaid.TabIndex = 31
        Me.LabelPaid.Text = "registrat al assentament "
        '
        'ButtonPayNow
        '
        Me.ButtonPayNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPayNow.Location = New System.Drawing.Point(571, 390)
        Me.ButtonPayNow.Name = "ButtonPayNow"
        Me.ButtonPayNow.Size = New System.Drawing.Size(115, 31)
        Me.ButtonPayNow.TabIndex = 30
        Me.ButtonPayNow.Text = "PAGAR"
        '
        'DateTimePickerPay
        '
        Me.DateTimePickerPay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerPay.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerPay.Location = New System.Drawing.Point(201, 398)
        Me.DateTimePickerPay.Name = "DateTimePickerPay"
        Me.DateTimePickerPay.Size = New System.Drawing.Size(86, 20)
        Me.DateTimePickerPay.TabIndex = 29
        '
        'ComboBoxBancs
        '
        Me.ComboBoxBancs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxBancs.FormattingEnabled = True
        Me.ComboBoxBancs.Location = New System.Drawing.Point(1, 397)
        Me.ComboBoxBancs.Name = "ComboBoxBancs"
        Me.ComboBoxBancs.Size = New System.Drawing.Size(194, 21)
        Me.ComboBoxBancs.TabIndex = 28
        '
        'LabelFchs
        '
        Me.LabelFchs.AutoSize = True
        Me.LabelFchs.Location = New System.Drawing.Point(122, 38)
        Me.LabelFchs.Name = "LabelFchs"
        Me.LabelFchs.Size = New System.Drawing.Size(130, 13)
        Me.LabelFchs.TabIndex = 27
        Me.LabelFchs.Text = "del 00/00/00 al 00/00/00"
        '
        'ComboBoxYeas
        '
        Me.ComboBoxYeas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYeas.FormattingEnabled = True
        Me.ComboBoxYeas.Location = New System.Drawing.Point(10, 35)
        Me.ComboBoxYeas.Name = "ComboBoxYeas"
        Me.ComboBoxYeas.Size = New System.Drawing.Size(52, 21)
        Me.ComboBoxYeas.TabIndex = 26
        '
        'ComboBoxPeriods
        '
        Me.ComboBoxPeriods.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPeriods.FormattingEnabled = True
        Me.ComboBoxPeriods.Items.AddRange(New Object() {"GEN", "FEB", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DES"})
        Me.ComboBoxPeriods.Location = New System.Drawing.Point(66, 35)
        Me.ComboBoxPeriods.Name = "ComboBoxPeriods"
        Me.ComboBoxPeriods.Size = New System.Drawing.Size(50, 21)
        Me.ComboBoxPeriods.TabIndex = 25
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 62)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(685, 322)
        Me.SplitContainer1.SplitterDistance = 154
        Me.SplitContainer1.TabIndex = 32
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(685, 154)
        Me.DataGridView1.TabIndex = 0
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(685, 164)
        Me.DataGridView2.TabIndex = 1
        '
        'Frm_Fiscal_IVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 422)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.LabelPaid)
        Me.Controls.Add(Me.ButtonPayNow)
        Me.Controls.Add(Me.DateTimePickerPay)
        Me.Controls.Add(Me.ComboBoxBancs)
        Me.Controls.Add(Me.LabelFchs)
        Me.Controls.Add(Me.ComboBoxYeas)
        Me.Controls.Add(Me.ComboBoxPeriods)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Fiscal_IVA"
        Me.Text = "IVA"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonXls As System.Windows.Forms.ToolStripButton
    Friend WithEvents LabelPaid As System.Windows.Forms.Label
    Friend WithEvents ButtonPayNow As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerPay As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxBancs As System.Windows.Forms.ComboBox
    Friend WithEvents LabelFchs As System.Windows.Forms.Label
    Friend WithEvents ComboBoxYeas As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPeriods As System.Windows.Forms.ComboBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButtonFitxer As System.Windows.Forms.ToolStripButton
End Class
