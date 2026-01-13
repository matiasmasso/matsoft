<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BancTerm
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
        Dim DtoAmt1 As DTOAmt = New DTOAmt()
        Dim DtoCur1 = DTOCur.Eur
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.ComboBoxTarget = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxEuribor = New System.Windows.Forms.CheckBox()
        Me.Xl_PercentDiferencial = New Winforms.Xl_Percent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_AmtMinim = New Winforms.Xl_Amt()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 194)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(354, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(135, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(246, 4)
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
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(262, 6)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Entitat"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(70, 32)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(272, 20)
        Me.Xl_Contact21.TabIndex = 53
        '
        'ComboBoxTarget
        '
        Me.ComboBoxTarget.FormattingEnabled = True
        Me.ComboBoxTarget.Items.AddRange(New Object() {"(seleccionar codi)", "anticips de crèdit"})
        Me.ComboBoxTarget.Location = New System.Drawing.Point(70, 59)
        Me.ComboBoxTarget.Name = "ComboBoxTarget"
        Me.ComboBoxTarget.Size = New System.Drawing.Size(272, 21)
        Me.ComboBoxTarget.TabIndex = 54
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Codi"
        '
        'CheckBoxEuribor
        '
        Me.CheckBoxEuribor.AutoSize = True
        Me.CheckBoxEuribor.Location = New System.Drawing.Point(70, 88)
        Me.CheckBoxEuribor.Name = "CheckBoxEuribor"
        Me.CheckBoxEuribor.Size = New System.Drawing.Size(108, 17)
        Me.CheckBoxEuribor.TabIndex = 56
        Me.CheckBoxEuribor.Text = "Indexat al Euribor"
        Me.CheckBoxEuribor.UseVisualStyleBackColor = True
        '
        'Xl_PercentDiferencial
        '
        Me.Xl_PercentDiferencial.Location = New System.Drawing.Point(70, 111)
        Me.Xl_PercentDiferencial.Name = "Xl_PercentDiferencial"
        Me.Xl_PercentDiferencial.Size = New System.Drawing.Size(59, 20)
        Me.Xl_PercentDiferencial.TabIndex = 57
        Me.Xl_PercentDiferencial.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Diferencial"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Minim"
        '
        'Xl_AmtMinim
        '
        Me.Xl_AmtMinim.Location = New System.Drawing.Point(70, 139)
        Me.Xl_AmtMinim.Name = "Xl_AmtMinim"
        Me.Xl_AmtMinim.Size = New System.Drawing.Size(59, 20)
        Me.Xl_AmtMinim.TabIndex = 60
        DtoCur1.tag = DTOCur.Ids.EUR.ToString
        DtoAmt1.Cur = DtoCur1
        DtoAmt1.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt1.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_AmtMinim.Value = DtoAmt1
        '
        'Frm_BancTerm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 225)
        Me.Controls.Add(Me.Xl_AmtMinim)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_PercentDiferencial)
        Me.Controls.Add(Me.CheckBoxEuribor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxTarget)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_BancTerm"
        Me.Text = "Condicions bancaries"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact21 As Winforms.Xl_Contact2
    Friend WithEvents ComboBoxTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxEuribor As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_PercentDiferencial As Winforms.Xl_Percent
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtMinim As Winforms.Xl_Amt
End Class
