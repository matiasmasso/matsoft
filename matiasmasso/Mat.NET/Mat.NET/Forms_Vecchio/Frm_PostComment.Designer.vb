<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PostComment
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
        Me.ButtonOkAndAnswer = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_PostCommentParent1 = New Mat.NET.Xl_PostCommentParent()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxComment = New System.Windows.Forms.TextBox()
        Me.CheckBoxAproved = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerAproved = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerDeleted = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxDeleted = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxAnswer = New System.Windows.Forms.TextBox()
        Me.ButtonAnswer = New System.Windows.Forms.Button()
        Me.Xl_Usuari1 = New Mat.NET.Xl_Usuari()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonOkAndAnswer)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 486)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(733, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonOkAndAnswer
        '
        Me.ButtonOkAndAnswer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOkAndAnswer.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOkAndAnswer.Location = New System.Drawing.Point(575, 3)
        Me.ButtonOkAndAnswer.Name = "ButtonOkAndAnswer"
        Me.ButtonOkAndAnswer.Size = New System.Drawing.Size(154, 24)
        Me.ButtonOkAndAnswer.TabIndex = 15
        Me.ButtonOkAndAnswer.Text = "Aprovar i Contestar"
        Me.ButtonOkAndAnswer.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(354, 3)
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
        Me.ButtonOk.Location = New System.Drawing.Point(465, 3)
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
        'Xl_PostCommentParent1
        '
        Me.Xl_PostCommentParent1.Location = New System.Drawing.Point(90, 76)
        Me.Xl_PostCommentParent1.Name = "Xl_PostCommentParent1"
        Me.Xl_PostCommentParent1.Size = New System.Drawing.Size(639, 20)
        Me.Xl_PostCommentParent1.TabIndex = 43
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(625, 12)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePickerFch.TabIndex = 44
        '
        'TextBoxComment
        '
        Me.TextBoxComment.Location = New System.Drawing.Point(21, 102)
        Me.TextBoxComment.Multiline = True
        Me.TextBoxComment.Name = "TextBoxComment"
        Me.TextBoxComment.Size = New System.Drawing.Size(708, 238)
        Me.TextBoxComment.TabIndex = 46
        '
        'CheckBoxAproved
        '
        Me.CheckBoxAproved.AutoSize = True
        Me.CheckBoxAproved.Location = New System.Drawing.Point(21, 414)
        Me.CheckBoxAproved.Name = "CheckBoxAproved"
        Me.CheckBoxAproved.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxAproved.TabIndex = 47
        Me.CheckBoxAproved.Text = "Aprovat"
        Me.CheckBoxAproved.UseVisualStyleBackColor = True
        '
        'DateTimePickerAproved
        '
        Me.DateTimePickerAproved.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAproved.Location = New System.Drawing.Point(90, 413)
        Me.DateTimePickerAproved.Name = "DateTimePickerAproved"
        Me.DateTimePickerAproved.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePickerAproved.TabIndex = 48
        Me.DateTimePickerAproved.Visible = False
        '
        'DateTimePickerDeleted
        '
        Me.DateTimePickerDeleted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDeleted.Location = New System.Drawing.Point(90, 439)
        Me.DateTimePickerDeleted.Name = "DateTimePickerDeleted"
        Me.DateTimePickerDeleted.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePickerDeleted.TabIndex = 50
        Me.DateTimePickerDeleted.Visible = False
        '
        'CheckBoxDeleted
        '
        Me.CheckBoxDeleted.AutoSize = True
        Me.CheckBoxDeleted.Location = New System.Drawing.Point(21, 440)
        Me.CheckBoxDeleted.Name = "CheckBoxDeleted"
        Me.CheckBoxDeleted.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxDeleted.TabIndex = 49
        Me.CheckBoxDeleted.Text = "Eliminat"
        Me.CheckBoxDeleted.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "post:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "usuari:"
        '
        'TextBoxAnswer
        '
        Me.TextBoxAnswer.Location = New System.Drawing.Point(21, 205)
        Me.TextBoxAnswer.Multiline = True
        Me.TextBoxAnswer.Name = "TextBoxAnswer"
        Me.TextBoxAnswer.Size = New System.Drawing.Size(708, 192)
        Me.TextBoxAnswer.TabIndex = 53
        '
        'ButtonAnswer
        '
        Me.ButtonAnswer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAnswer.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonAnswer.Location = New System.Drawing.Point(625, 373)
        Me.ButtonAnswer.Name = "ButtonAnswer"
        Me.ButtonAnswer.Size = New System.Drawing.Size(104, 24)
        Me.ButtonAnswer.TabIndex = 54
        Me.ButtonAnswer.Text = "Contestar"
        Me.ButtonAnswer.UseVisualStyleBackColor = False
        '
        'Xl_Usuari1
        '
        Me.Xl_Usuari1.Location = New System.Drawing.Point(90, 57)
        Me.Xl_Usuari1.Name = "Xl_Usuari1"
        Me.Xl_Usuari1.Size = New System.Drawing.Size(639, 20)
        Me.Xl_Usuari1.TabIndex = 55
        '
        'Frm_PostComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 517)
        Me.Controls.Add(Me.Xl_Usuari1)
        Me.Controls.Add(Me.ButtonAnswer)
        Me.Controls.Add(Me.TextBoxAnswer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerDeleted)
        Me.Controls.Add(Me.CheckBoxDeleted)
        Me.Controls.Add(Me.DateTimePickerAproved)
        Me.Controls.Add(Me.CheckBoxAproved)
        Me.Controls.Add(Me.TextBoxComment)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.Xl_PostCommentParent1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PostComment"
        Me.Text = "Comentari d'usuari"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_PostCommentParent1 As Mat.Net.Xl_PostCommentParent
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxComment As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxAproved As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerAproved As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerDeleted As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxDeleted As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonOkAndAnswer As System.Windows.Forms.Button
    Friend WithEvents TextBoxAnswer As System.Windows.Forms.TextBox
    Friend WithEvents ButtonAnswer As System.Windows.Forms.Button
    Friend WithEvents Xl_Usuari1 As Mat.NET.Xl_Usuari
End Class
