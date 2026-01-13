<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Transmisio_New
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Transmisio_New))
        Me.LabelTot = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.CheckBoxMailTo = New System.Windows.Forms.CheckBox
        Me.TextBoxMailTo = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButtonPdfAlbs = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonCheckNone = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButtonCheckAll = New System.Windows.Forms.ToolStripButton
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelTot
        '
        Me.LabelTot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTot.Location = New System.Drawing.Point(0, 226)
        Me.LabelTot.Name = "LabelTot"
        Me.LabelTot.Size = New System.Drawing.Size(167, 16)
        Me.LabelTot.TabIndex = 6
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(2, 28)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(609, 195)
        Me.DataGridView1.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 245)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(611, 31)
        Me.Panel1.TabIndex = 46
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(392, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(503, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'CheckBoxMailTo
        '
        Me.CheckBoxMailTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxMailTo.AutoSize = True
        Me.CheckBoxMailTo.Checked = True
        Me.CheckBoxMailTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxMailTo.Location = New System.Drawing.Point(180, 227)
        Me.CheckBoxMailTo.Name = "CheckBoxMailTo"
        Me.CheckBoxMailTo.Size = New System.Drawing.Size(73, 17)
        Me.CheckBoxMailTo.TabIndex = 47
        Me.CheckBoxMailTo.Text = "enviar a..."
        Me.CheckBoxMailTo.UseVisualStyleBackColor = True
        '
        'TextBoxMailTo
        '
        Me.TextBoxMailTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMailTo.Location = New System.Drawing.Point(254, 224)
        Me.TextBoxMailTo.Name = "TextBoxMailTo"
        Me.TextBoxMailTo.Size = New System.Drawing.Size(357, 20)
        Me.TextBoxMailTo.TabIndex = 48
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonPdfAlbs, Me.ToolStripButtonCheckNone, Me.ToolStripButtonCheckAll})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(611, 25)
        Me.ToolStrip1.TabIndex = 49
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonPdfAlbs
        '
        Me.ToolStripButtonPdfAlbs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonPdfAlbs.Image = My.Resources.Resources.pdf
        Me.ToolStripButtonPdfAlbs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPdfAlbs.Name = "ToolStripButtonPdfAlbs"
        Me.ToolStripButtonPdfAlbs.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonPdfAlbs.Text = "albarans"
        '
        'ToolStripButtonCheckNone
        '
        Me.ToolStripButtonCheckNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonCheckNone.Image = CType(resources.GetObject("ToolStripButtonCheckNone.Image"), System.Drawing.Image)
        Me.ToolStripButtonCheckNone.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCheckNone.Name = "ToolStripButtonCheckNone"
        Me.ToolStripButtonCheckNone.Size = New System.Drawing.Size(85, 22)
        Me.ToolStripButtonCheckNone.Text = "Selecciona res"
        '
        'ToolStripButtonCheckAll
        '
        Me.ToolStripButtonCheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonCheckAll.Image = CType(resources.GetObject("ToolStripButtonCheckAll.Image"), System.Drawing.Image)
        Me.ToolStripButtonCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCheckAll.Name = "ToolStripButtonCheckAll"
        Me.ToolStripButtonCheckAll.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripButtonCheckAll.Text = "Seleciona tot"
        '
        'Frm_Transmisio_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 276)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TextBoxMailTo)
        Me.Controls.Add(Me.CheckBoxMailTo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelTot)
        Me.Name = "Frm_Transmisio_New"
        Me.Text = "NOVA TRANSMISIO"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelTot As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents CheckBoxMailTo As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxMailTo As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonPdfAlbs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCheckNone As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCheckAll As System.Windows.Forms.ToolStripButton
End Class
