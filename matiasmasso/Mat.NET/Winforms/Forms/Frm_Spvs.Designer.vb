<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Spvs
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
        Me.CheckBoxRead = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotRead = New System.Windows.Forms.CheckBox()
        Me.CheckBoxArrived = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotArrived = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShipped = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNotShipped = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.LabelTot = New System.Windows.Forms.Label()
        Me.Xl_Spvs1 = New Winforms.Xl_Spvs()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.CheckBoxAllYears = New System.Windows.Forms.CheckBox()
        Me.NumericUpDownYears = New System.Windows.Forms.NumericUpDown()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Spvs1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownYears, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckBoxRead
        '
        Me.CheckBoxRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxRead.AutoSize = True
        Me.CheckBoxRead.Checked = True
        Me.CheckBoxRead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRead.Location = New System.Drawing.Point(730, 39)
        Me.CheckBoxRead.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxRead.Name = "CheckBoxRead"
        Me.CheckBoxRead.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxRead.TabIndex = 3
        Me.CheckBoxRead.Text = "Llegits"
        Me.CheckBoxRead.UseVisualStyleBackColor = True
        '
        'CheckBoxNotRead
        '
        Me.CheckBoxNotRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotRead.AutoSize = True
        Me.CheckBoxNotRead.Checked = True
        Me.CheckBoxNotRead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotRead.Location = New System.Drawing.Point(730, 57)
        Me.CheckBoxNotRead.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxNotRead.Name = "CheckBoxNotRead"
        Me.CheckBoxNotRead.Size = New System.Drawing.Size(69, 17)
        Me.CheckBoxNotRead.TabIndex = 4
        Me.CheckBoxNotRead.Text = "No llegits"
        Me.CheckBoxNotRead.UseVisualStyleBackColor = True
        '
        'CheckBoxArrived
        '
        Me.CheckBoxArrived.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxArrived.AutoSize = True
        Me.CheckBoxArrived.Checked = True
        Me.CheckBoxArrived.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxArrived.Location = New System.Drawing.Point(730, 91)
        Me.CheckBoxArrived.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxArrived.Name = "CheckBoxArrived"
        Me.CheckBoxArrived.Size = New System.Drawing.Size(61, 17)
        Me.CheckBoxArrived.TabIndex = 5
        Me.CheckBoxArrived.Text = "Arribats"
        Me.CheckBoxArrived.UseVisualStyleBackColor = True
        '
        'CheckBoxNotArrived
        '
        Me.CheckBoxNotArrived.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotArrived.AutoSize = True
        Me.CheckBoxNotArrived.Checked = True
        Me.CheckBoxNotArrived.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotArrived.Location = New System.Drawing.Point(730, 108)
        Me.CheckBoxNotArrived.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxNotArrived.Name = "CheckBoxNotArrived"
        Me.CheckBoxNotArrived.Size = New System.Drawing.Size(77, 17)
        Me.CheckBoxNotArrived.TabIndex = 6
        Me.CheckBoxNotArrived.Text = "No arribats"
        Me.CheckBoxNotArrived.UseVisualStyleBackColor = True
        '
        'CheckBoxShipped
        '
        Me.CheckBoxShipped.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxShipped.AutoSize = True
        Me.CheckBoxShipped.Location = New System.Drawing.Point(730, 142)
        Me.CheckBoxShipped.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxShipped.Name = "CheckBoxShipped"
        Me.CheckBoxShipped.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxShipped.TabIndex = 7
        Me.CheckBoxShipped.Text = "Sortits"
        Me.CheckBoxShipped.UseVisualStyleBackColor = True
        '
        'CheckBoxNotShipped
        '
        Me.CheckBoxNotShipped.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNotShipped.AutoSize = True
        Me.CheckBoxNotShipped.Checked = True
        Me.CheckBoxNotShipped.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxNotShipped.Location = New System.Drawing.Point(730, 159)
        Me.CheckBoxNotShipped.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxNotShipped.Name = "CheckBoxNotShipped"
        Me.CheckBoxNotShipped.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxNotShipped.TabIndex = 8
        Me.CheckBoxNotShipped.Text = "No sortits"
        Me.CheckBoxNotShipped.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Spvs1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(716, 350)
        Me.Panel1.TabIndex = 9
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 327)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(716, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'LabelTot
        '
        Me.LabelTot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTot.AutoSize = True
        Me.LabelTot.Location = New System.Drawing.Point(727, 334)
        Me.LabelTot.Name = "LabelTot"
        Me.LabelTot.Size = New System.Drawing.Size(30, 13)
        Me.LabelTot.TabIndex = 10
        Me.LabelTot.Text = "total:"
        '
        'Xl_Spvs1
        '
        Me.Xl_Spvs1.AllowUserToAddRows = False
        Me.Xl_Spvs1.AllowUserToDeleteRows = False
        Me.Xl_Spvs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Spvs1.DisplayObsolets = False
        Me.Xl_Spvs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Spvs1.Filter = Nothing
        Me.Xl_Spvs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Spvs1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_Spvs1.MouseIsDown = False
        Me.Xl_Spvs1.Name = "Xl_Spvs1"
        Me.Xl_Spvs1.ReadOnly = True
        Me.Xl_Spvs1.RowTemplate.Height = 40
        Me.Xl_Spvs1.Size = New System.Drawing.Size(716, 327)
        Me.Xl_Spvs1.TabIndex = 2
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(730, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(106, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'CheckBoxAllYears
        '
        Me.CheckBoxAllYears.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAllYears.AutoSize = True
        Me.CheckBoxAllYears.Location = New System.Drawing.Point(730, 218)
        Me.CheckBoxAllYears.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxAllYears.Name = "CheckBoxAllYears"
        Me.CheckBoxAllYears.Size = New System.Drawing.Size(88, 17)
        Me.CheckBoxAllYears.TabIndex = 11
        Me.CheckBoxAllYears.Text = "Tots els anys"
        Me.CheckBoxAllYears.UseVisualStyleBackColor = True
        Me.CheckBoxAllYears.Visible = False
        '
        'NumericUpDownYears
        '
        Me.NumericUpDownYears.Location = New System.Drawing.Point(730, 194)
        Me.NumericUpDownYears.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.NumericUpDownYears.Minimum = New Decimal(New Integer() {2001, 0, 0, 0})
        Me.NumericUpDownYears.Name = "NumericUpDownYears"
        Me.NumericUpDownYears.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownYears.TabIndex = 12
        Me.NumericUpDownYears.Value = New Decimal(New Integer() {2001, 0, 0, 0})
        Me.NumericUpDownYears.Visible = False
        '
        'Frm_Spvs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(837, 356)
        Me.Controls.Add(Me.NumericUpDownYears)
        Me.Controls.Add(Me.CheckBoxAllYears)
        Me.Controls.Add(Me.LabelTot)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CheckBoxNotShipped)
        Me.Controls.Add(Me.CheckBoxShipped)
        Me.Controls.Add(Me.CheckBoxNotArrived)
        Me.Controls.Add(Me.CheckBoxArrived)
        Me.Controls.Add(Me.CheckBoxNotRead)
        Me.Controls.Add(Me.CheckBoxRead)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_Spvs"
        Me.Text = "Reparacions Servei Tècnic"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Spvs1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownYears, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Spvs1 As Xl_Spvs
    Friend WithEvents CheckBoxRead As CheckBox
    Friend WithEvents CheckBoxNotRead As CheckBox
    Friend WithEvents CheckBoxArrived As CheckBox
    Friend WithEvents CheckBoxNotArrived As CheckBox
    Friend WithEvents CheckBoxShipped As CheckBox
    Friend WithEvents CheckBoxNotShipped As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents LabelTot As Label
    Friend WithEvents CheckBoxAllYears As CheckBox
    Friend WithEvents NumericUpDownYears As NumericUpDown
End Class
