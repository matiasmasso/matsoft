<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_BudgetItemTicket
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Xl_LookupBookFra1 = New Winforms.Xl_LookupBookFra()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Amount1 = New Winforms.Xl_Amount()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.RadioButtonFra = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTicket = New System.Windows.Forms.RadioButton()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile_Old()
        Me.Xl_LookupBudgetItem1 = New Winforms.Xl_LookupBudgetItem()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_LookupBookFra1
        '
        Me.Xl_LookupBookFra1.BookFra = Nothing
        Me.Xl_LookupBookFra1.IsDirty = False
        Me.Xl_LookupBookFra1.Location = New System.Drawing.Point(129, 54)
        Me.Xl_LookupBookFra1.Name = "Xl_LookupBookFra1"
        Me.Xl_LookupBookFra1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupBookFra1.Size = New System.Drawing.Size(355, 20)
        Me.Xl_LookupBookFra1.TabIndex = 65
        Me.Xl_LookupBookFra1.Value = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Partida pressupostaria"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 198)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "Import"
        '
        'Xl_Amount1
        '
        Me.Xl_Amount1.Amt = Nothing
        Me.Xl_Amount1.Location = New System.Drawing.Point(129, 194)
        Me.Xl_Amount1.Name = "Xl_Amount1"
        Me.Xl_Amount1.ReadOnly = False
        Me.Xl_Amount1.Size = New System.Drawing.Size(78, 20)
        Me.Xl_Amount1.TabIndex = 60
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 230)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(496, 31)
        Me.Panel1.TabIndex = 59
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(277, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(388, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'RadioButtonFra
        '
        Me.RadioButtonFra.AutoSize = True
        Me.RadioButtonFra.Location = New System.Drawing.Point(20, 57)
        Me.RadioButtonFra.Name = "RadioButtonFra"
        Me.RadioButtonFra.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonFra.TabIndex = 66
        Me.RadioButtonFra.TabStop = True
        Me.RadioButtonFra.Text = "Factura"
        Me.RadioButtonFra.UseVisualStyleBackColor = True
        '
        'RadioButtonTicket
        '
        Me.RadioButtonTicket.AutoSize = True
        Me.RadioButtonTicket.Location = New System.Drawing.Point(20, 80)
        Me.RadioButtonTicket.Name = "RadioButtonTicket"
        Me.RadioButtonTicket.Size = New System.Drawing.Size(93, 17)
        Me.RadioButtonTicket.TabIndex = 67
        Me.RadioButtonTicket.TabStop = True
        Me.RadioButtonTicket.Text = "Altre justificant"
        Me.RadioButtonTicket.UseVisualStyleBackColor = True
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(277, 54)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(171, 160)
        Me.Xl_DocFile1.TabIndex = 68
        Me.Xl_DocFile1.Visible = False
        '
        'Xl_LookupBudgetItem1
        '
        Me.Xl_LookupBudgetItem1.BudgetItem = Nothing
        Me.Xl_LookupBudgetItem1.IsDirty = False
        Me.Xl_LookupBudgetItem1.Location = New System.Drawing.Point(129, 28)
        Me.Xl_LookupBudgetItem1.Name = "Xl_LookupBudgetItem1"
        Me.Xl_LookupBudgetItem1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupBudgetItem1.Size = New System.Drawing.Size(355, 20)
        Me.Xl_LookupBudgetItem1.TabIndex = 69
        Me.Xl_LookupBudgetItem1.Value = Nothing
        '
        'Frm_BudgetItemFra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 261)
        Me.Controls.Add(Me.Xl_LookupBudgetItem1)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.RadioButtonTicket)
        Me.Controls.Add(Me.RadioButtonFra)
        Me.Controls.Add(Me.Xl_LookupBookFra1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Amount1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BudgetItemFra"
        Me.Text = "Justificant"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_LookupBookFra1 As Xl_LookupBookFra
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_Amount1 As Xl_Amount
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents RadioButtonFra As RadioButton
    Friend WithEvents RadioButtonTicket As RadioButton
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
    Friend WithEvents Xl_LookupBudgetItem1 As Xl_LookupBudgetItem
End Class
