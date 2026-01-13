<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcGrup
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
        Me.TextBoxEsp = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.TextBoxCat = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxEng = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBoxBalCod = New System.Windows.Forms.GroupBox
        Me.RadioButtonCod1 = New System.Windows.Forms.RadioButton
        Me.RadioButtonCod0 = New System.Windows.Forms.RadioButton
        Me.TextBoxPgcPlan = New System.Windows.Forms.TextBox
        Me.TextBoxEpg = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.GroupBoxBalCod.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Location = New System.Drawing.Point(109, 106)
        Me.TextBoxEsp.MaxLength = 50
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxEsp.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Espanyol"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(109, 12)
        Me.TextBoxId.MaxLength = 4
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(50, 20)
        Me.TextBoxId.TabIndex = 2
        Me.TextBoxId.TabStop = False
        Me.TextBoxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Id"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 351)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(533, 31)
        Me.Panel1.TabIndex = 54
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(314, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 15
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(425, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 14
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
        Me.ButtonDel.TabIndex = 16
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Location = New System.Drawing.Point(109, 132)
        Me.TextBoxCat.MaxLength = 50
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxCat.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(38, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Catalá"
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Location = New System.Drawing.Point(109, 158)
        Me.TextBoxEng.MaxLength = 50
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxEng.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(38, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Anglés"
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(109, 199)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(412, 105)
        Me.TextBoxDsc.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 199)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "descripció"
        '
        'GroupBoxBalCod
        '
        Me.GroupBoxBalCod.Controls.Add(Me.RadioButtonCod1)
        Me.GroupBoxBalCod.Controls.Add(Me.RadioButtonCod0)
        Me.GroupBoxBalCod.Location = New System.Drawing.Point(182, 28)
        Me.GroupBoxBalCod.Name = "GroupBoxBalCod"
        Me.GroupBoxBalCod.Size = New System.Drawing.Size(250, 69)
        Me.GroupBoxBalCod.TabIndex = 3
        Me.GroupBoxBalCod.TabStop = False
        Me.GroupBoxBalCod.Text = "Classificació"
        '
        'RadioButtonCod1
        '
        Me.RadioButtonCod1.AutoSize = True
        Me.RadioButtonCod1.Location = New System.Drawing.Point(158, 30)
        Me.RadioButtonCod1.Name = "RadioButtonCod1"
        Me.RadioButtonCod1.Size = New System.Drawing.Size(55, 17)
        Me.RadioButtonCod1.TabIndex = 5
        Me.RadioButtonCod1.TabStop = True
        Me.RadioButtonCod1.Text = "passiu"
        Me.RadioButtonCod1.UseVisualStyleBackColor = True
        '
        'RadioButtonCod0
        '
        Me.RadioButtonCod0.AutoSize = True
        Me.RadioButtonCod0.Location = New System.Drawing.Point(45, 30)
        Me.RadioButtonCod0.Name = "RadioButtonCod0"
        Me.RadioButtonCod0.Size = New System.Drawing.Size(48, 17)
        Me.RadioButtonCod0.TabIndex = 4
        Me.RadioButtonCod0.TabStop = True
        Me.RadioButtonCod0.Text = "actiu"
        Me.RadioButtonCod0.UseVisualStyleBackColor = True
        '
        'TextBoxPgcPlan
        '
        Me.TextBoxPgcPlan.Location = New System.Drawing.Point(457, 15)
        Me.TextBoxPgcPlan.Name = "TextBoxPgcPlan"
        Me.TextBoxPgcPlan.ReadOnly = True
        Me.TextBoxPgcPlan.Size = New System.Drawing.Size(64, 20)
        Me.TextBoxPgcPlan.TabIndex = 0
        Me.TextBoxPgcPlan.TabStop = False
        Me.TextBoxPgcPlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxEpg
        '
        Me.TextBoxEpg.Location = New System.Drawing.Point(109, 314)
        Me.TextBoxEpg.MaxLength = 50
        Me.TextBoxEpg.Name = "TextBoxEpg"
        Me.TextBoxEpg.ReadOnly = True
        Me.TextBoxEpg.Size = New System.Drawing.Size(412, 20)
        Me.TextBoxEpg.TabIndex = 56
        Me.TextBoxEpg.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 317)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Epígraf"
        '
        'Frm_PgcGrup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 382)
        Me.Controls.Add(Me.TextBoxEpg)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxPgcPlan)
        Me.Controls.Add(Me.GroupBoxBalCod)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.TextBoxEng)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCat)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxEsp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PgcGrup"
        Me.Text = "GRUP DE COMPTES"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBoxBalCod.ResumeLayout(False)
        Me.GroupBoxBalCod.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxEsp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxCat As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEng As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxBalCod As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonCod1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCod0 As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxPgcPlan As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxEpg As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
