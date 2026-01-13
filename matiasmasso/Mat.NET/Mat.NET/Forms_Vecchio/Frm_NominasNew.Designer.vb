<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_NominasNew
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxBanc = New System.Windows.Forms.ComboBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelUserStatus = New System.Windows.Forms.Label()
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox()
        Me.Xl_NominasNew1 = New Mat.NET.Xl_NominasNew()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(7, 17)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(519, 10)
        Me.ProgressBar1.TabIndex = 3
        Me.ProgressBar1.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(659, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePicker1.TabIndex = 41
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Banc emisor:"
        '
        'ComboBoxBanc
        '
        Me.ComboBoxBanc.FormattingEnabled = True
        Me.ComboBoxBanc.Location = New System.Drawing.Point(81, 11)
        Me.ComboBoxBanc.Name = "ComboBoxBanc"
        Me.ComboBoxBanc.Size = New System.Drawing.Size(155, 21)
        Me.ComboBoxBanc.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(532, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(643, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.LabelUserStatus)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 390)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(751, 31)
        Me.Panel1.TabIndex = 44
        '
        'LabelUserStatus
        '
        Me.LabelUserStatus.AutoSize = True
        Me.LabelUserStatus.Location = New System.Drawing.Point(4, 4)
        Me.LabelUserStatus.Name = "LabelUserStatus"
        Me.LabelUserStatus.Size = New System.Drawing.Size(85, 13)
        Me.LabelUserStatus.TabIndex = 13
        Me.LabelUserStatus.Text = "LabelUserStatus"
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(242, 12)
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(411, 20)
        Me.TextBoxConcepte.TabIndex = 46
        '
        'Xl_NominasNew1
        '
        Me.Xl_NominasNew1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_NominasNew1.BackColor = System.Drawing.Color.Transparent
        Me.Xl_NominasNew1.Location = New System.Drawing.Point(7, 38)
        Me.Xl_NominasNew1.Name = "Xl_NominasNew1"
        Me.Xl_NominasNew1.Size = New System.Drawing.Size(739, 351)
        Me.Xl_NominasNew1.TabIndex = 47
        '
        'Frm_NominasNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 421)
        Me.Controls.Add(Me.Xl_NominasNew1)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxBanc)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_NominasNew"
        Me.Text = "NOMINAS"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxBanc As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents Xl_NominasNew1 As Mat.Net.Xl_NominasNew
    Friend WithEvents LabelUserStatus As System.Windows.Forms.Label
End Class
