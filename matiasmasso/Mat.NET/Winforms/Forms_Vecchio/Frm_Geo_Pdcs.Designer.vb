<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Geo_Pdcs
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.ComboBoxTpa = New System.Windows.Forms.ComboBox()
        Me.ComboBoxStp = New System.Windows.Forms.ComboBox()
        Me.ComboBoxArt = New System.Windows.Forms.ComboBox()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1083, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1083, 566)
        Me.SplitContainer1.SplitterDistance = 308
        Me.SplitContainer1.TabIndex = 1
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(308, 566)
        Me.TreeView1.TabIndex = 0
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
        Me.DataGridView1.Size = New System.Drawing.Size(771, 566)
        Me.DataGridView1.TabIndex = 0
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(12, 2)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(50, 20)
        Me.NumericUpDownYea.TabIndex = 2
        Me.NumericUpDownYea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {2011, 0, 0, 0})
        '
        'ComboBoxTpa
        '
        Me.ComboBoxTpa.FormattingEnabled = True
        Me.ComboBoxTpa.Location = New System.Drawing.Point(83, 0)
        Me.ComboBoxTpa.Name = "ComboBoxTpa"
        Me.ComboBoxTpa.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxTpa.TabIndex = 3
        '
        'ComboBoxStp
        '
        Me.ComboBoxStp.FormattingEnabled = True
        Me.ComboBoxStp.Location = New System.Drawing.Point(210, 0)
        Me.ComboBoxStp.Name = "ComboBoxStp"
        Me.ComboBoxStp.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxStp.TabIndex = 4
        '
        'ComboBoxArt
        '
        Me.ComboBoxArt.FormattingEnabled = True
        Me.ComboBoxArt.Location = New System.Drawing.Point(337, 1)
        Me.ComboBoxArt.Name = "ComboBoxArt"
        Me.ComboBoxArt.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxArt.TabIndex = 5
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExcel.Image = My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(1067, 3)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(16, 16)
        Me.ButtonExcel.TabIndex = 6
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'Frm_Geo_Pdcs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 591)
        Me.Controls.Add(Me.ButtonExcel)
        Me.Controls.Add(Me.ComboBoxArt)
        Me.Controls.Add(Me.ComboBoxStp)
        Me.Controls.Add(Me.ComboBoxTpa)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Geo_Pdcs"
        Me.Text = "VENDES PER ZONA"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBoxTpa As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxStp As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxArt As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
End Class
