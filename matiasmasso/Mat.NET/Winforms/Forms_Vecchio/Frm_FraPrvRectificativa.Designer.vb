<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FraPrvRectificativa
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxFraOriginal = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_AmtTot = New Winforms.Xl_Amount()
        Me.Xl_AmtIva = New Winforms.Xl_Amount()
        Me.Xl_AmtBas = New Winforms.Xl_Amount()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact_Logo1 = New Winforms.Xl_Contact_Logo()
        Me.Xl_AmtPendent = New Winforms.Xl_Amount()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 430)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(655, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(436, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(547, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxFraOriginal
        '
        Me.TextBoxFraOriginal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxFraOriginal.Location = New System.Drawing.Point(3, 16)
        Me.TextBoxFraOriginal.Multiline = True
        Me.TextBoxFraOriginal.Name = "TextBoxFraOriginal"
        Me.TextBoxFraOriginal.ReadOnly = True
        Me.TextBoxFraOriginal.Size = New System.Drawing.Size(259, 81)
        Me.TextBoxFraOriginal.TabIndex = 42
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxFraOriginal)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 100)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "factura original"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DateTimePickerVto)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Xl_AmtTot)
        Me.GroupBox2.Controls.Add(Me.Xl_AmtIva)
        Me.GroupBox2.Controls.Add(Me.Xl_AmtBas)
        Me.GroupBox2.Controls.Add(Me.DateTimePickerFch)
        Me.GroupBox2.Controls.Add(Me.TextBoxNum)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(262, 224)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "factura rectificativa"
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(109, 165)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerVto.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 170)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "venciment"
        '
        'Xl_AmtTot
        '
        Me.Xl_AmtTot.Amt = Nothing
        Me.Xl_AmtTot.Location = New System.Drawing.Point(109, 139)
        Me.Xl_AmtTot.Name = "Xl_AmtTot"
        Me.Xl_AmtTot.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtTot.TabIndex = 9
        '
        'Xl_AmtIva
        '
        Me.Xl_AmtIva.Amt = Nothing
        Me.Xl_AmtIva.Location = New System.Drawing.Point(109, 113)
        Me.Xl_AmtIva.Name = "Xl_AmtIva"
        Me.Xl_AmtIva.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtIva.TabIndex = 8
        '
        'Xl_AmtBas
        '
        Me.Xl_AmtBas.Amt = Nothing
        Me.Xl_AmtBas.Location = New System.Drawing.Point(109, 87)
        Me.Xl_AmtBas.Name = "Xl_AmtBas"
        Me.Xl_AmtBas.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtBas.TabIndex = 7
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(109, 60)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFch.TabIndex = 6
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(109, 33)
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxNum.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "total"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Iva"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "base"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "data"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "numero"
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(12, 13)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 45
        '
        'Xl_AmtPendent
        '
        Me.Xl_AmtPendent.Amt = Nothing
        Me.Xl_AmtPendent.Location = New System.Drawing.Point(124, 391)
        Me.Xl_AmtPendent.Name = "Xl_AmtPendent"
        Me.Xl_AmtPendent.Size = New System.Drawing.Size(100, 20)
        Me.Xl_AmtPendent.TabIndex = 47
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(41, 398)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "nou pendent"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(301, 4)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 48
        '
        'Frm_FraPrvRectificativa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 461)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Xl_AmtPendent)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_FraPrvRectificativa"
        Me.Text = "FACTURA RECTIFICATIVA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxFraOriginal As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_AmtTot As Xl_Amount
    Friend WithEvents Xl_AmtIva As Xl_Amount
    Friend WithEvents Xl_AmtBas As Xl_Amount
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents Xl_AmtPendent As Xl_Amount
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile
End Class
