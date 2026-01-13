Public Partial Class Frm_Last_Spvs
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
        Me.CheckBoxNotRead = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRead = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotArrived = New System.Windows.Forms.CheckBox()
        Me.CheckBoxArrived = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotLeft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLeft = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOldClis = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownSpv = New System.Windows.Forms.NumericUpDown()
        Me.PictureBoxSearch = New System.Windows.Forms.PictureBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownSpv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckBoxNotRead
        '
        Me.CheckBoxNotRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotRead.AutoSize = True
        Me.CheckBoxNotRead.Checked = True
        Me.CheckBoxNotRead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotRead.Location = New System.Drawing.Point(657, 13)
        Me.CheckBoxNotRead.Name = "CheckBoxNotRead"
        Me.CheckBoxNotRead.Size = New System.Drawing.Size(69, 17)
        Me.CheckBoxNotRead.TabIndex = 2
        Me.CheckBoxNotRead.Text = "No llegits"
        '
        'CheckBoxRead
        '
        Me.CheckBoxRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxRead.AutoSize = True
        Me.CheckBoxRead.Checked = True
        Me.CheckBoxRead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRead.Location = New System.Drawing.Point(657, 28)
        Me.CheckBoxRead.Name = "CheckBoxRead"
        Me.CheckBoxRead.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxRead.TabIndex = 3
        Me.CheckBoxRead.Text = "llegits"
        '
        'CheckBoxNotArrived
        '
        Me.CheckBoxNotArrived.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotArrived.AutoSize = True
        Me.CheckBoxNotArrived.Checked = True
        Me.CheckBoxNotArrived.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotArrived.Location = New System.Drawing.Point(657, 52)
        Me.CheckBoxNotArrived.Name = "CheckBoxNotArrived"
        Me.CheckBoxNotArrived.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxNotArrived.TabIndex = 4
        Me.CheckBoxNotArrived.Text = "No arribats"
        '
        'CheckBoxArrived
        '
        Me.CheckBoxArrived.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxArrived.AutoSize = True
        Me.CheckBoxArrived.Checked = True
        Me.CheckBoxArrived.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxArrived.Location = New System.Drawing.Point(657, 66)
        Me.CheckBoxArrived.Name = "CheckBoxArrived"
        Me.CheckBoxArrived.Size = New System.Drawing.Size(60, 17)
        Me.CheckBoxArrived.TabIndex = 5
        Me.CheckBoxArrived.Text = "arribats"
        '
        'CheckBoxNotLeft
        '
        Me.CheckBoxNotLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotLeft.AutoSize = True
        Me.CheckBoxNotLeft.Checked = True
        Me.CheckBoxNotLeft.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotLeft.Location = New System.Drawing.Point(657, 90)
        Me.CheckBoxNotLeft.Name = "CheckBoxNotLeft"
        Me.CheckBoxNotLeft.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxNotLeft.TabIndex = 6
        Me.CheckBoxNotLeft.Text = "No sortits"
        '
        'CheckBoxLeft
        '
        Me.CheckBoxLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxLeft.AutoSize = True
        Me.CheckBoxLeft.Location = New System.Drawing.Point(657, 104)
        Me.CheckBoxLeft.Name = "CheckBoxLeft"
        Me.CheckBoxLeft.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxLeft.TabIndex = 7
        Me.CheckBoxLeft.Text = "sortits"
        '
        'CheckBoxOldClis
        '
        Me.CheckBoxOldClis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxOldClis.AutoSize = True
        Me.CheckBoxOldClis.Location = New System.Drawing.Point(657, 237)
        Me.CheckBoxOldClis.Name = "CheckBoxOldClis"
        Me.CheckBoxOldClis.Size = New System.Drawing.Size(93, 17)
        Me.CheckBoxOldClis.TabIndex = 8
        Me.CheckBoxOldClis.Text = "fitxes anteriors"
        Me.CheckBoxOldClis.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(651, 266)
        Me.DataGridView1.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBoxSearch)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownSpv)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownYea)
        Me.GroupBox1.Location = New System.Drawing.Point(659, 131)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(90, 76)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "buscar"
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(7, 20)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2050, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {1985, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownYea.TabIndex = 0
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {1985, 0, 0, 0})
        '
        'NumericUpDownSpv
        '
        Me.NumericUpDownSpv.Location = New System.Drawing.Point(7, 46)
        Me.NumericUpDownSpv.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumericUpDownSpv.Name = "NumericUpDownSpv"
        Me.NumericUpDownSpv.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownSpv.TabIndex = 1
        '
        'PictureBoxSearch
        '
        Me.PictureBoxSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxSearch.Image = My.Resources.Resources.Lupa
        Me.PictureBoxSearch.Location = New System.Drawing.Point(57, 45)
        Me.PictureBoxSearch.Name = "PictureBoxSearch"
        Me.PictureBoxSearch.Size = New System.Drawing.Size(32, 27)
        Me.PictureBoxSearch.TabIndex = 2
        Me.PictureBoxSearch.TabStop = False
        '
        'Frm_Last_Spvs
        '
        Me.ClientSize = New System.Drawing.Size(754, 266)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CheckBoxOldClis)
        Me.Controls.Add(Me.CheckBoxLeft)
        Me.Controls.Add(Me.CheckBoxNotLeft)
        Me.Controls.Add(Me.CheckBoxArrived)
        Me.Controls.Add(Me.CheckBoxNotArrived)
        Me.Controls.Add(Me.CheckBoxRead)
        Me.Controls.Add(Me.CheckBoxNotRead)
        Me.Name = "Frm_Last_Spvs"
        Me.Text = "ULTIMES REPARACIONS REGISTRADES"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownSpv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxNotRead As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxRead As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNotArrived As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxArrived As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNotLeft As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxLeft As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxOldClis As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBoxSearch As System.Windows.Forms.PictureBox
    Friend WithEvents NumericUpDownSpv As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
End Class
