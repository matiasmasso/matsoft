<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrderItem
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
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.RadioButtonSuccess = New System.Windows.Forms.RadioButton()
        Me.RadioButtonErr = New System.Windows.Forms.RadioButton()
        Me.ComboBoxErr = New System.Windows.Forms.ComboBox()
        Me.TextBoxErrDsc = New System.Windows.Forms.TextBox()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 258)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(472, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(253, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(364, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'RadioButtonSuccess
        '
        Me.RadioButtonSuccess.AutoSize = True
        Me.RadioButtonSuccess.Location = New System.Drawing.Point(57, 44)
        Me.RadioButtonSuccess.Name = "RadioButtonSuccess"
        Me.RadioButtonSuccess.Size = New System.Drawing.Size(74, 17)
        Me.RadioButtonSuccess.TabIndex = 43
        Me.RadioButtonSuccess.TabStop = True
        Me.RadioButtonSuccess.Text = "Disponible"
        Me.RadioButtonSuccess.UseVisualStyleBackColor = True
        '
        'RadioButtonErr
        '
        Me.RadioButtonErr.AutoSize = True
        Me.RadioButtonErr.Location = New System.Drawing.Point(57, 86)
        Me.RadioButtonErr.Name = "RadioButtonErr"
        Me.RadioButtonErr.Size = New System.Drawing.Size(69, 17)
        Me.RadioButtonErr.TabIndex = 44
        Me.RadioButtonErr.TabStop = True
        Me.RadioButtonErr.Text = "Bloquejat"
        Me.RadioButtonErr.UseVisualStyleBackColor = True
        '
        'ComboBoxErr
        '
        Me.ComboBoxErr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxErr.FormattingEnabled = True
        Me.ComboBoxErr.Location = New System.Drawing.Point(99, 116)
        Me.ComboBoxErr.Name = "ComboBoxErr"
        Me.ComboBoxErr.Size = New System.Drawing.Size(361, 21)
        Me.ComboBoxErr.TabIndex = 45
        '
        'TextBoxErrDsc
        '
        Me.TextBoxErrDsc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxErrDsc.Location = New System.Drawing.Point(99, 143)
        Me.TextBoxErrDsc.Multiline = True
        Me.TextBoxErrDsc.Name = "TextBoxErrDsc"
        Me.TextBoxErrDsc.Size = New System.Drawing.Size(361, 99)
        Me.TextBoxErrDsc.TabIndex = 46
        '
        'Frm_PurchaseOrderItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 289)
        Me.Controls.Add(Me.TextBoxErrDsc)
        Me.Controls.Add(Me.ComboBoxErr)
        Me.Controls.Add(Me.RadioButtonErr)
        Me.Controls.Add(Me.RadioButtonSuccess)
        Me.Controls.Add(Me.PanelButtons)
        Me.Name = "Frm_PurchaseOrderItem"
        Me.Text = "Frm_PurchaseOrderItem"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents RadioButtonSuccess As RadioButton
    Friend WithEvents RadioButtonErr As RadioButton
    Friend WithEvents ComboBoxErr As ComboBox
    Friend WithEvents TextBoxErrDsc As TextBox
End Class
