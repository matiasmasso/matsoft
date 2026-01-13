<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Art_Arc
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
        Me.PictureBoxArt = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripComboBoxMgz = New System.Windows.Forms.ToolStripComboBox()
        Me.Xl_Contact1 = New Xl_Contact()
        Me.CheckBoxClient = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxEntrades = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSortides = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBoxArt
        '
        Me.PictureBoxArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxArt.BackColor = System.Drawing.Color.White
        Me.PictureBoxArt.Location = New System.Drawing.Point(589, 0)
        Me.PictureBoxArt.Name = "PictureBoxArt"
        Me.PictureBoxArt.Size = New System.Drawing.Size(66, 76)
        Me.PictureBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxArt.TabIndex = 6
        Me.PictureBoxArt.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonExcel, Me.ToolStripComboBoxMgz})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(437, 25)
        Me.ToolStrip1.TabIndex = 13
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "exportar"
        '
        'ToolStripComboBoxMgz
        '
        Me.ToolStripComboBoxMgz.Name = "ToolStripComboBoxMgz"
        Me.ToolStripComboBoxMgz.Size = New System.Drawing.Size(400, 25)
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Location = New System.Drawing.Point(141, 49)
        Me.Xl_Contact1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(443, 20)
        Me.Xl_Contact1.TabIndex = 12
        Me.Xl_Contact1.Visible = False
        '
        'CheckBoxClient
        '
        Me.CheckBoxClient.Location = New System.Drawing.Point(141, 28)
        Me.CheckBoxClient.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.CheckBoxClient.Name = "CheckBoxClient"
        Me.CheckBoxClient.Size = New System.Drawing.Size(96, 23)
        Me.CheckBoxClient.TabIndex = 11
        Me.CheckBoxClient.Text = "filtre per client"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 76)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(654, 294)
        Me.DataGridView1.TabIndex = 18
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxEntrades)
        Me.GroupBox1.Controls.Add(Me.CheckBoxSortides)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(99, 50)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        '
        'CheckBoxEntrades
        '
        Me.CheckBoxEntrades.Checked = True
        Me.CheckBoxEntrades.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxEntrades.Location = New System.Drawing.Point(6, 7)
        Me.CheckBoxEntrades.Name = "CheckBoxEntrades"
        Me.CheckBoxEntrades.Size = New System.Drawing.Size(67, 19)
        Me.CheckBoxEntrades.TabIndex = 19
        Me.CheckBoxEntrades.Text = "entrades"
        '
        'CheckBoxSortides
        '
        Me.CheckBoxSortides.Checked = True
        Me.CheckBoxSortides.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxSortides.Location = New System.Drawing.Point(6, 26)
        Me.CheckBoxSortides.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxSortides.Name = "CheckBoxSortides"
        Me.CheckBoxSortides.Size = New System.Drawing.Size(67, 19)
        Me.CheckBoxSortides.TabIndex = 18
        Me.CheckBoxSortides.Text = "sortides"
        '
        'Frm_Art_Arc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 370)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBoxArt)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.CheckBoxClient)
        Me.Name = "Frm_Art_Arc"
        Me.Text = "HISTORIAL"
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxArt As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents CheckBoxClient As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxEntrades As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSortides As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripComboBoxMgz As System.Windows.Forms.ToolStripComboBox
End Class
