<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConsumerTicketPorts
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
        Me.Xl_AmountCurBase = New Mat.Net.Xl_AmountCur()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_AmountCurTot = New Mat.Net.Xl_AmountCur()
        Me.Xl_AmountCurVat = New Mat.Net.Xl_AmountCur()
        Me.CheckBoxVat = New System.Windows.Forms.CheckBox()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 164)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(318, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(99, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(210, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_AmountCurBase
        '
        Me.Xl_AmountCurBase.Amt = Nothing
        Me.Xl_AmountCurBase.Enabled = False
        Me.Xl_AmountCurBase.Location = New System.Drawing.Point(142, 109)
        Me.Xl_AmountCurBase.Name = "Xl_AmountCurBase"
        Me.Xl_AmountCurBase.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCurBase.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Ports (base imponible)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Ports (Iva inclos)"
        '
        'Xl_AmountCurTot
        '
        Me.Xl_AmountCurTot.Amt = Nothing
        Me.Xl_AmountCurTot.Location = New System.Drawing.Point(142, 57)
        Me.Xl_AmountCurTot.Name = "Xl_AmountCurTot"
        Me.Xl_AmountCurTot.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCurTot.TabIndex = 45
        '
        'Xl_AmountCurVat
        '
        Me.Xl_AmountCurVat.Amt = Nothing
        Me.Xl_AmountCurVat.Enabled = False
        Me.Xl_AmountCurVat.Location = New System.Drawing.Point(142, 83)
        Me.Xl_AmountCurVat.Name = "Xl_AmountCurVat"
        Me.Xl_AmountCurVat.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmountCurVat.TabIndex = 47
        '
        'CheckBoxVat
        '
        Me.CheckBoxVat.AutoSize = True
        Me.CheckBoxVat.Enabled = False
        Me.CheckBoxVat.Location = New System.Drawing.Point(27, 85)
        Me.CheckBoxVat.Name = "CheckBoxVat"
        Me.CheckBoxVat.Size = New System.Drawing.Size(41, 17)
        Me.CheckBoxVat.TabIndex = 51
        Me.CheckBoxVat.Text = "Iva"
        Me.CheckBoxVat.UseVisualStyleBackColor = True
        '
        'Frm_ConsumerTicketPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(318, 195)
        Me.Controls.Add(Me.CheckBoxVat)
        Me.Controls.Add(Me.Xl_AmountCurVat)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_AmountCurTot)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_AmountCurBase)
        Me.Controls.Add(Me.PanelButtons)
        Me.Name = "Frm_ConsumerTicketPorts"
        Me.Text = "Afegir ports a ticket consumidor"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_AmountCurBase As Xl_AmountCur
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_AmountCurTot As Xl_AmountCur
    Friend WithEvents Xl_AmountCurVat As Xl_AmountCur
    Friend WithEvents CheckBoxVat As CheckBox
End Class
