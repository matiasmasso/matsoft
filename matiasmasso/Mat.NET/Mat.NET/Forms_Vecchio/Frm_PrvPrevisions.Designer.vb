<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrvPrevisions
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDownWeek = New System.Windows.Forms.NumericUpDown
        Me.TextBoxPrv = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.LabelWeekTxt = New System.Windows.Forms.Label
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownWeek, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(2, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Semana:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(2, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Proveidor:"
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(106, 33)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {2050, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {1993, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(56, 20)
        Me.NumericUpDownYea.TabIndex = 14
        Me.NumericUpDownYea.TabStop = False
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {1993, 0, 0, 0})
        Me.NumericUpDownYea.Visible = False
        '
        'NumericUpDownWeek
        '
        Me.NumericUpDownWeek.Location = New System.Drawing.Point(66, 33)
        Me.NumericUpDownWeek.Maximum = New Decimal(New Integer() {53, 0, 0, 0})
        Me.NumericUpDownWeek.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownWeek.Name = "NumericUpDownWeek"
        Me.NumericUpDownWeek.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDownWeek.TabIndex = 13
        Me.NumericUpDownWeek.TabStop = False
        Me.NumericUpDownWeek.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownWeek.Visible = False
        '
        'TextBoxPrv
        '
        Me.TextBoxPrv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPrv.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBoxPrv.Location = New System.Drawing.Point(66, 9)
        Me.TextBoxPrv.Name = "TextBoxPrv"
        Me.TextBoxPrv.ReadOnly = True
        Me.TextBoxPrv.Size = New System.Drawing.Size(334, 20)
        Me.TextBoxPrv.TabIndex = 16
        Me.TextBoxPrv.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 452)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(416, 31)
        Me.Panel1.TabIndex = 34
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(197, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(308, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(5, 59)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(407, 391)
        Me.DataGridView1.TabIndex = 35
        '
        'LabelWeekTxt
        '
        Me.LabelWeekTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelWeekTxt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelWeekTxt.Location = New System.Drawing.Point(164, 32)
        Me.LabelWeekTxt.Name = "LabelWeekTxt"
        Me.LabelWeekTxt.Size = New System.Drawing.Size(240, 20)
        Me.LabelWeekTxt.TabIndex = 36
        '
        'Frm_PrvPrevisions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 483)
        Me.Controls.Add(Me.LabelWeekTxt)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxPrv)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.NumericUpDownWeek)
        Me.Name = "Frm_PrvPrevisions"
        Me.Text = "PREVISIONS"
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownWeek, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownWeek As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBoxPrv As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelWeekTxt As System.Windows.Forms.Label
End Class
