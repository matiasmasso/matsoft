<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Task
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.LabelLastRun = New System.Windows.Forms.Label
        Me.CheckBoxNotBefore = New System.Windows.Forms.CheckBox
        Me.DateTimePickerNotBefore = New System.Windows.Forms.DateTimePicker
        Me.DateTimePickerNotAfter = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxNotAfter = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.LabelNextRun = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CheckBoxEnabled
        '
        Me.CheckBoxEnabled.AutoSize = True
        Me.CheckBoxEnabled.Location = New System.Drawing.Point(71, 149)
        Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
        Me.CheckBoxEnabled.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxEnabled.TabIndex = 55
        Me.CheckBoxEnabled.Text = "activat"
        Me.CheckBoxEnabled.UseVisualStyleBackColor = True
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(71, 12)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(488, 20)
        Me.TextBoxNom.TabIndex = 54
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "nom:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 396)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(563, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(344, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(455, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsc.Location = New System.Drawing.Point(71, 38)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(488, 102)
        Me.TextBoxDsc.TabIndex = 57
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "descripció:"
        '
        'LabelLastRun
        '
        Me.LabelLastRun.AutoSize = True
        Me.LabelLastRun.Location = New System.Drawing.Point(367, 150)
        Me.LabelLastRun.Name = "LabelLastRun"
        Me.LabelLastRun.Size = New System.Drawing.Size(81, 13)
        Me.LabelLastRun.TabIndex = 58
        Me.LabelLastRun.Text = "[Ultima vegada]"
        '
        'CheckBoxNotBefore
        '
        Me.CheckBoxNotBefore.AutoSize = True
        Me.CheckBoxNotBefore.Location = New System.Drawing.Point(71, 172)
        Me.CheckBoxNotBefore.Name = "CheckBoxNotBefore"
        Me.CheckBoxNotBefore.Size = New System.Drawing.Size(120, 17)
        Me.CheckBoxNotBefore.TabIndex = 60
        Me.CheckBoxNotBefore.Text = "no activar abans de"
        Me.CheckBoxNotBefore.UseVisualStyleBackColor = True
        '
        'DateTimePickerNotBefore
        '
        Me.DateTimePickerNotBefore.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNotBefore.Location = New System.Drawing.Point(207, 167)
        Me.DateTimePickerNotBefore.Name = "DateTimePickerNotBefore"
        Me.DateTimePickerNotBefore.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerNotBefore.TabIndex = 61
        Me.DateTimePickerNotBefore.Visible = False
        '
        'DateTimePickerNotAfter
        '
        Me.DateTimePickerNotAfter.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNotAfter.Location = New System.Drawing.Point(207, 190)
        Me.DateTimePickerNotAfter.Name = "DateTimePickerNotAfter"
        Me.DateTimePickerNotAfter.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePickerNotAfter.TabIndex = 63
        Me.DateTimePickerNotAfter.Visible = False
        '
        'CheckBoxNotAfter
        '
        Me.CheckBoxNotAfter.AutoSize = True
        Me.CheckBoxNotAfter.Location = New System.Drawing.Point(71, 195)
        Me.CheckBoxNotAfter.Name = "CheckBoxNotAfter"
        Me.CheckBoxNotAfter.Size = New System.Drawing.Size(130, 17)
        Me.CheckBoxNotAfter.TabIndex = 62
        Me.CheckBoxNotAfter.Text = "desactivar després de"
        Me.CheckBoxNotAfter.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "propera vegada:"
        '
        'LabelNextRun
        '
        Me.LabelNextRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelNextRun.AutoSize = True
        Me.LabelNextRun.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNextRun.Location = New System.Drawing.Point(441, 186)
        Me.LabelNextRun.Name = "LabelNextRun"
        Me.LabelNextRun.Size = New System.Drawing.Size(60, 24)
        Me.LabelNextRun.TabIndex = 65
        Me.LabelNextRun.Text = "00:00"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(71, 230)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.Size = New System.Drawing.Size(487, 160)
        Me.DataGridView1.TabIndex = 66
        '
        'Frm_Task
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 427)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelNextRun)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePickerNotAfter)
        Me.Controls.Add(Me.CheckBoxNotAfter)
        Me.Controls.Add(Me.DateTimePickerNotBefore)
        Me.Controls.Add(Me.CheckBoxNotBefore)
        Me.Controls.Add(Me.LabelLastRun)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxEnabled)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Task"
        Me.Text = "TASCA"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelLastRun As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNotBefore As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerNotBefore As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerNotAfter As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxNotAfter As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelNextRun As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
