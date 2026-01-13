<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mail
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonReceived = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSent = New System.Windows.Forms.RadioButton()
        Me.TextBoxSubject = New System.Windows.Forms.TextBox()
        Me.TextBoxAtn = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_Contact21 = New Mat.NET.Xl_Contact2()
        Me.ButtonAddContact = New System.Windows.Forms.Button()
        Me.Xl_Contacts1 = New Mat.NET.Xl_Contacts()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(657, 2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 21
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButtonReceived)
        Me.GroupBox1.Controls.Add(Me.RadioButtonSent)
        Me.GroupBox1.Location = New System.Drawing.Point(449, 310)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 56)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonReceived
        '
        Me.RadioButtonReceived.Location = New System.Drawing.Point(8, 33)
        Me.RadioButtonReceived.Name = "RadioButtonReceived"
        Me.RadioButtonReceived.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonReceived.TabIndex = 10
        Me.RadioButtonReceived.Text = "&rebut"
        '
        'RadioButtonSent
        '
        Me.RadioButtonSent.Location = New System.Drawing.Point(8, 13)
        Me.RadioButtonSent.Name = "RadioButtonSent"
        Me.RadioButtonSent.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonSent.TabIndex = 9
        Me.RadioButtonSent.Text = "&enviat"
        '
        'TextBoxSubject
        '
        Me.TextBoxSubject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSubject.Location = New System.Drawing.Point(449, 286)
        Me.TextBoxSubject.Name = "TextBoxSubject"
        Me.TextBoxSubject.Size = New System.Drawing.Size(304, 20)
        Me.TextBoxSubject.TabIndex = 25
        '
        'TextBoxAtn
        '
        Me.TextBoxAtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAtn.Location = New System.Drawing.Point(449, 262)
        Me.TextBoxAtn.Name = "TextBoxAtn"
        Me.TextBoxAtn.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxAtn.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(369, 286)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "&concepte:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(369, 262)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "&a la atenció de:"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(538, 8)
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
        Me.ButtonOk.Location = New System.Drawing.Point(649, 8)
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
        Me.ButtonDel.Location = New System.Drawing.Point(12, 8)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(3, 2)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 31
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 425)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(757, 41)
        Me.Panel1.TabIndex = 29
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(372, 36)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(320, 20)
        Me.Xl_Contact21.TabIndex = 32
        '
        'ButtonAddContact
        '
        Me.ButtonAddContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddContact.Location = New System.Drawing.Point(698, 36)
        Me.ButtonAddContact.Name = "ButtonAddContact"
        Me.ButtonAddContact.Size = New System.Drawing.Size(55, 20)
        Me.ButtonAddContact.TabIndex = 33
        Me.ButtonAddContact.Text = "afegir"
        Me.ButtonAddContact.UseVisualStyleBackColor = True
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.Location = New System.Drawing.Point(372, 63)
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.Size = New System.Drawing.Size(381, 144)
        Me.Xl_Contacts1.TabIndex = 34
        '
        'Frm_Mail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 466)
        Me.Controls.Add(Me.Xl_Contacts1)
        Me.Controls.Add(Me.ButtonAddContact)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBoxSubject)
        Me.Controls.Add(Me.TextBoxAtn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Mail"
        Me.Text = "Correspondencia"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonReceived As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSent As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxSubject As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAtn As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Xl_Contact21 As Mat.NET.Xl_Contact2
    Friend WithEvents ButtonAddContact As System.Windows.Forms.Button
    Friend WithEvents Xl_Contacts1 As Mat.NET.Xl_Contacts
End Class
