<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Grup_Stat
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ComboBoxYea = New System.Windows.Forms.ComboBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageClis = New System.Windows.Forms.TabPage
        Me.DataGridViewClis = New System.Windows.Forms.DataGridView
        Me.TabPageArts = New System.Windows.Forms.TabPage
        Me.DataGridViewArts = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButtonPdc = New System.Windows.Forms.RadioButton
        Me.RadioButtonArc = New System.Windows.Forms.RadioButton
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageClis.SuspendLayout()
        CType(Me.DataGridViewClis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageArts.SuspendLayout()
        CType(Me.DataGridViewArts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1026, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = My.Resources.Resources.Excel
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "Excel"
        '
        'ComboBoxYea
        '
        Me.ComboBoxYea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYea.FormattingEnabled = True
        Me.ComboBoxYea.Location = New System.Drawing.Point(926, 4)
        Me.ComboBoxYea.Name = "ComboBoxYea"
        Me.ComboBoxYea.Size = New System.Drawing.Size(88, 21)
        Me.ComboBoxYea.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageClis)
        Me.TabControl1.Controls.Add(Me.TabPageArts)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1026, 316)
        Me.TabControl1.TabIndex = 2
        '
        'TabPageClis
        '
        Me.TabPageClis.Controls.Add(Me.DataGridViewClis)
        Me.TabPageClis.Location = New System.Drawing.Point(4, 22)
        Me.TabPageClis.Name = "TabPageClis"
        Me.TabPageClis.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageClis.Size = New System.Drawing.Size(1018, 290)
        Me.TabPageClis.TabIndex = 0
        Me.TabPageClis.Text = "PER PUNT DE VENDA"
        Me.TabPageClis.UseVisualStyleBackColor = True
        '
        'DataGridViewClis
        '
        Me.DataGridViewClis.AllowUserToAddRows = False
        Me.DataGridViewClis.AllowUserToDeleteRows = False
        Me.DataGridViewClis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewClis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewClis.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewClis.Name = "DataGridViewClis"
        Me.DataGridViewClis.ReadOnly = True
        Me.DataGridViewClis.Size = New System.Drawing.Size(1012, 284)
        Me.DataGridViewClis.TabIndex = 3
        '
        'TabPageArts
        '
        Me.TabPageArts.Controls.Add(Me.DataGridViewArts)
        Me.TabPageArts.Location = New System.Drawing.Point(4, 22)
        Me.TabPageArts.Name = "TabPageArts"
        Me.TabPageArts.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageArts.Size = New System.Drawing.Size(1018, 290)
        Me.TabPageArts.TabIndex = 1
        Me.TabPageArts.Text = "PER PRODUCTE"
        Me.TabPageArts.UseVisualStyleBackColor = True
        '
        'DataGridViewArts
        '
        Me.DataGridViewArts.AllowUserToAddRows = False
        Me.DataGridViewArts.AllowUserToDeleteRows = False
        Me.DataGridViewArts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewArts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewArts.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewArts.Name = "DataGridViewArts"
        Me.DataGridViewArts.ReadOnly = True
        Me.DataGridViewArts.Size = New System.Drawing.Size(1012, 284)
        Me.DataGridViewArts.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButtonArc)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPdc)
        Me.GroupBox1.Location = New System.Drawing.Point(752, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(142, 40)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonPdc
        '
        Me.RadioButtonPdc.AutoSize = True
        Me.RadioButtonPdc.Checked = True
        Me.RadioButtonPdc.Location = New System.Drawing.Point(6, 4)
        Me.RadioButtonPdc.Name = "RadioButtonPdc"
        Me.RadioButtonPdc.Size = New System.Drawing.Size(92, 17)
        Me.RadioButtonPdc.TabIndex = 0
        Me.RadioButtonPdc.TabStop = True
        Me.RadioButtonPdc.Text = "per comandes"
        Me.RadioButtonPdc.UseVisualStyleBackColor = True
        '
        'RadioButtonArc
        '
        Me.RadioButtonArc.AutoSize = True
        Me.RadioButtonArc.Location = New System.Drawing.Point(6, 20)
        Me.RadioButtonArc.Name = "RadioButtonArc"
        Me.RadioButtonArc.Size = New System.Drawing.Size(90, 17)
        Me.RadioButtonArc.TabIndex = 1
        Me.RadioButtonArc.Text = "per entregues"
        Me.RadioButtonArc.UseVisualStyleBackColor = True
        '
        'Frm_Grup_Stat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 341)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ComboBoxYea)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Grup_Stat"
        Me.Text = "ESTADISTICA DE VENDES"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageClis.ResumeLayout(False)
        CType(Me.DataGridViewClis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageArts.ResumeLayout(False)
        CType(Me.DataGridViewArts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ComboBoxYea As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageClis As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewClis As System.Windows.Forms.DataGridView
    Friend WithEvents TabPageArts As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewArts As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonArc As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPdc As System.Windows.Forms.RadioButton
End Class
