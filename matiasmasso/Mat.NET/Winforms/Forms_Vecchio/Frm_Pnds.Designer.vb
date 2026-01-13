Public Partial Class Frm_Pnds
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox
        Me.RadioButtonDeutors = New System.Windows.Forms.RadioButton
        Me.RadioButtonCreditors = New System.Windows.Forms.RadioButton
        Me.LabelSum = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(158, 42)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(264, 21)
        Me.ComboBoxCfp.TabIndex = 0
        '
        'RadioButtonDeutors
        '
        Me.RadioButtonDeutors.AutoSize = True
        Me.RadioButtonDeutors.Checked = True
        Me.RadioButtonDeutors.Location = New System.Drawing.Point(8, 30)
        Me.RadioButtonDeutors.Name = "RadioButtonDeutors"
        Me.RadioButtonDeutors.Size = New System.Drawing.Size(62, 17)
        Me.RadioButtonDeutors.TabIndex = 1
        Me.RadioButtonDeutors.TabStop = True
        Me.RadioButtonDeutors.Text = "Deutors"
        '
        'RadioButtonCreditors
        '
        Me.RadioButtonCreditors.AutoSize = True
        Me.RadioButtonCreditors.Location = New System.Drawing.Point(8, 46)
        Me.RadioButtonCreditors.Name = "RadioButtonCreditors"
        Me.RadioButtonCreditors.Size = New System.Drawing.Size(66, 17)
        Me.RadioButtonCreditors.TabIndex = 2
        Me.RadioButtonCreditors.Text = "Creditors"
        '
        'LabelSum
        '
        Me.LabelSum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSum.AutoSize = True
        Me.LabelSum.Location = New System.Drawing.Point(505, 42)
        Me.LabelSum.Name = "LabelSum"
        Me.LabelSum.Size = New System.Drawing.Size(34, 13)
        Me.LabelSum.TabIndex = 4
        Me.LabelSum.Text = "Total:"
        Me.LabelSum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 78)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(625, 374)
        Me.DataGridView1.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(632, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        '
        'Frm_Pnds
        '
        Me.ClientSize = New System.Drawing.Size(632, 457)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelSum)
        Me.Controls.Add(Me.RadioButtonCreditors)
        Me.Controls.Add(Me.RadioButtonDeutors)
        Me.Controls.Add(Me.ComboBoxCfp)
        Me.Name = "Frm_Pnds"
        Me.Text = "PENDENTS DE PAGO"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonDeutors As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCreditors As System.Windows.Forms.RadioButton
    Friend WithEvents LabelSum As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
End Class
