Public Partial Class Frm_Etqs
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
        Me.TextBoxAdr1 = New System.Windows.Forms.TextBox
        Me.TextBoxAdr2 = New System.Windows.Forms.TextBox
        Me.TextBoxAdr3 = New System.Windows.Forms.TextBox
        Me.TextBoxAdr4 = New System.Windows.Forms.TextBox
        Me.ButtonAdd = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ExportarToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ImportarToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.ButtonClear = New System.Windows.Forms.Button
        Me.ButtonUpdate = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxAdr1
        '
        Me.TextBoxAdr1.Location = New System.Drawing.Point(25, 76)
        Me.TextBoxAdr1.Name = "TextBoxAdr1"
        Me.TextBoxAdr1.Size = New System.Drawing.Size(230, 20)
        Me.TextBoxAdr1.TabIndex = 0
        '
        'TextBoxAdr2
        '
        Me.TextBoxAdr2.Location = New System.Drawing.Point(25, 95)
        Me.TextBoxAdr2.Name = "TextBoxAdr2"
        Me.TextBoxAdr2.Size = New System.Drawing.Size(230, 20)
        Me.TextBoxAdr2.TabIndex = 1
        '
        'TextBoxAdr3
        '
        Me.TextBoxAdr3.Location = New System.Drawing.Point(25, 114)
        Me.TextBoxAdr3.Name = "TextBoxAdr3"
        Me.TextBoxAdr3.Size = New System.Drawing.Size(230, 20)
        Me.TextBoxAdr3.TabIndex = 2
        '
        'TextBoxAdr4
        '
        Me.TextBoxAdr4.Location = New System.Drawing.Point(25, 133)
        Me.TextBoxAdr4.Name = "TextBoxAdr4"
        Me.TextBoxAdr4.Size = New System.Drawing.Size(230, 20)
        Me.TextBoxAdr4.TabIndex = 3
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Enabled = False
        Me.ButtonAdd.Image = My.Resources.Resources.clip
        Me.ButtonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAdd.Location = New System.Drawing.Point(165, 160)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(90, 24)
        Me.ButtonAdd.TabIndex = 4
        Me.ButtonAdd.Text = "AFEGIR"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.ExportarToolStripButton, Me.ImportarToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(578, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.Image = My.Resources.Resources.pdf
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(73, 22)
        Me.PrintToolStripButton.Text = "Imprimir"
        '
        'ExportarToolStripButton
        '
        Me.ExportarToolStripButton.Name = "ExportarToolStripButton"
        Me.ExportarToolStripButton.Size = New System.Drawing.Size(54, 22)
        Me.ExportarToolStripButton.Text = "exportar"
        '
        'ImportarToolStripButton
        '
        Me.ImportarToolStripButton.Image = My.Resources.Resources.Excel
        Me.ImportarToolStripButton.Name = "ImportarToolStripButton"
        Me.ImportarToolStripButton.Size = New System.Drawing.Size(73, 22)
        Me.ImportarToolStripButton.Text = "importar"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(127, 234)
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(37, 20)
        Me.NumericUpDown1.TabIndex = 12
        Me.NumericUpDown1.TabStop = False
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "primera etiqueta:"
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Image = My.Resources.Resources.del
        Me.ButtonDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonDel.Location = New System.Drawing.Point(25, 160)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(90, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "(F1 busca)"
        '
        'ButtonClear
        '
        Me.ButtonClear.Enabled = False
        Me.ButtonClear.Location = New System.Drawing.Point(165, 45)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(90, 24)
        Me.ButtonClear.TabIndex = 20
        Me.ButtonClear.Text = "NOU"
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Enabled = False
        Me.ButtonUpdate.Location = New System.Drawing.Point(165, 182)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(90, 24)
        Me.ButtonUpdate.TabIndex = 25
        Me.ButtonUpdate.Text = "MODIFICAR"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(276, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(302, 261)
        Me.DataGridView1.TabIndex = 26
        '
        'Frm_Etqs
        '
        Me.ClientSize = New System.Drawing.Size(578, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.TextBoxAdr4)
        Me.Controls.Add(Me.TextBoxAdr3)
        Me.Controls.Add(Me.TextBoxAdr2)
        Me.Controls.Add(Me.TextBoxAdr1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_Etqs"
        Me.Text = "ETIQUETES"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxAdr1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAdr2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAdr3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAdr4 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExportarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImportarToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
