<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Art_Promo
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
        Dim BigFileNew2 As maxisrvr.BigFileNew = New maxisrvr.BigFileNew()
        Me.CheckBoxFchFrom = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.CheckBoxToEndStk = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelTot = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Xl_BigFile1 = New Xl_BigFile()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.LabelNom = New System.Windows.Forms.Label()
        Me.CheckBoxPortsPagats = New System.Windows.Forms.CheckBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxFchFrom
        '
        Me.CheckBoxFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFchFrom.AutoSize = True
        Me.CheckBoxFchFrom.Location = New System.Drawing.Point(340, 15)
        Me.CheckBoxFchFrom.Name = "CheckBoxFchFrom"
        Me.CheckBoxFchFrom.Size = New System.Drawing.Size(86, 17)
        Me.CheckBoxFchFrom.TabIndex = 0
        Me.CheckBoxFchFrom.Text = "valida desde"
        Me.CheckBoxFchFrom.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(445, 12)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerFchFrom.TabIndex = 1
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(445, 32)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerFchTo.TabIndex = 3
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(340, 32)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxFchTo.TabIndex = 2
        Me.CheckBoxFchTo.Text = "caducitat"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'CheckBoxToEndStk
        '
        Me.CheckBoxToEndStk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxToEndStk.AutoSize = True
        Me.CheckBoxToEndStk.Location = New System.Drawing.Point(340, 64)
        Me.CheckBoxToEndStk.Name = "CheckBoxToEndStk"
        Me.CheckBoxToEndStk.Size = New System.Drawing.Size(144, 17)
        Me.CheckBoxToEndStk.TabIndex = 4
        Me.CheckBoxToEndStk.Text = "fins a esgotar existencies"
        Me.CheckBoxToEndStk.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(1, 99)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(541, 314)
        Me.DataGridView1.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 456)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(906, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(687, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(798, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelTot})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 434)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(906, 22)
        Me.StatusStrip1.TabIndex = 42
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelTot
        '
        Me.ToolStripStatusLabelTot.Name = "ToolStripStatusLabelTot"
        Me.ToolStripStatusLabelTot.Size = New System.Drawing.Size(73, 17)
        Me.ToolStripStatusLabelTot.Text = "(sense valor)"
        '
        'Xl_BigFile1
        '
        Me.Xl_BigFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        BigFileNew2.Height = 0
        BigFileNew2.Hres = 0
        BigFileNew2.Img = Nothing
        BigFileNew2.MimeCod = DTOEnums.MimeCods.NotSet
        BigFileNew2.Pags = 0
        BigFileNew2.Size = 0
        BigFileNew2.Stream = Nothing
        BigFileNew2.Vres = 0
        BigFileNew2.Width = 0
        Me.Xl_BigFile1.BigFile = BigFileNew2
        Me.Xl_BigFile1.Location = New System.Drawing.Point(548, 12)
        Me.Xl_BigFile1.Name = "Xl_BigFile1"
        Me.Xl_BigFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_BigFile1.TabIndex = 43
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(6, 58)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(328, 20)
        Me.TextBoxNom.TabIndex = 44
        '
        'LabelNom
        '
        Me.LabelNom.AutoSize = True
        Me.LabelNom.Location = New System.Drawing.Point(6, 41)
        Me.LabelNom.Name = "LabelNom"
        Me.LabelNom.Size = New System.Drawing.Size(32, 13)
        Me.LabelNom.TabIndex = 45
        Me.LabelNom.Text = "Nom:"
        '
        'CheckBoxPortsPagats
        '
        Me.CheckBoxPortsPagats.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxPortsPagats.AutoSize = True
        Me.CheckBoxPortsPagats.Location = New System.Drawing.Point(340, 48)
        Me.CheckBoxPortsPagats.Name = "CheckBoxPortsPagats"
        Me.CheckBoxPortsPagats.Size = New System.Drawing.Size(84, 17)
        Me.CheckBoxPortsPagats.TabIndex = 3
        Me.CheckBoxPortsPagats.Text = "ports pagats"
        Me.CheckBoxPortsPagats.UseVisualStyleBackColor = True
        '
        'Frm_Art_Promo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 487)
        Me.Controls.Add(Me.CheckBoxPortsPagats)
        Me.Controls.Add(Me.LabelNom)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Xl_BigFile1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CheckBoxToEndStk)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.CheckBoxFchFrom)
        Me.Name = "Frm_Art_Promo"
        Me.Text = "PROMOCIO"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxFchFrom As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxFchTo As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxToEndStk As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelTot As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Xl_BigFile1 As Xl_Bigfile
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents LabelNom As System.Windows.Forms.Label
    Friend WithEvents CheckBoxPortsPagats As System.Windows.Forms.CheckBox
End Class
