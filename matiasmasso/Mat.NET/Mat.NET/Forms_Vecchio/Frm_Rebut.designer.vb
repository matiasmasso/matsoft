Public Partial Class Frm_Rebut
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBoxIBAN = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxCit = New System.Windows.Forms.TextBox
        Me.TextBoxAdr = New System.Windows.Forms.TextBox
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.NumBox1 = New NumBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "rebut num.:"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(82, 135)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(85, 20)
        Me.TextBoxId.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 14)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "import:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 190)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "data:"
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(82, 189)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(85, 20)
        Me.DateTimePickerFch.TabIndex = 10
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(82, 216)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(85, 20)
        Me.DateTimePickerVto.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "venciment:"
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(82, 243)
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(255, 20)
        Me.TextBoxConcepte.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 244)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Concepte:"
        '
        'TextBoxIBAN
        '
        Me.TextBoxIBAN.Location = New System.Drawing.Point(82, 270)
        Me.TextBoxIBAN.Name = "TextBoxIBAN"
        Me.TextBoxIBAN.Size = New System.Drawing.Size(255, 20)
        Me.TextBoxIBAN.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 271)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "IBAN:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TextBoxCit)
        Me.GroupBox1.Controls.Add(Me.TextBoxAdr)
        Me.GroupBox1.Controls.Add(Me.TextBoxNom)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(343, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Lliurat:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(270, 23)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "(F1 busca)"
        '
        'TextBoxCit
        '
        Me.TextBoxCit.Location = New System.Drawing.Point(68, 56)
        Me.TextBoxCit.Name = "TextBoxCit"
        Me.TextBoxCit.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxCit.TabIndex = 4
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Location = New System.Drawing.Point(68, 38)
        Me.TextBoxAdr.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxAdr.TabIndex = 3
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(68, 20)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(195, 20)
        Me.TextBoxNom.TabIndex = 1
        '
        'ButtonOk
        '
        Me.ButtonOk.Image = My.Resources.Resources.pdf
        Me.ButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOk.Location = New System.Drawing.Point(241, 297)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(96, 24)
        Me.ButtonOk.TabIndex = 17
        Me.ButtonOk.Text = "IMPRIMIR"
        Me.ButtonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NumBox1
        '
        Me.NumBox1.Location = New System.Drawing.Point(82, 163)
        Me.NumBox1.Name = "NumBox1"
        Me.NumBox1.Size = New System.Drawing.Size(85, 20)
        Me.NumBox1.TabIndex = 8
        Me.NumBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_Rebut
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(369, 335)
        Me.Controls.Add(Me.NumBox1)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBoxIBAN)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePickerVto)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxId)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Rebut"
        Me.Text = "REBUT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxIBAN As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxCit As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAdr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents NumBox1 As NumBox
End Class
