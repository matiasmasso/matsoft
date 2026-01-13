<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SepaMandato
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
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.LabelRef = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile_Old()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(80, 190)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFrom.TabIndex = 3
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(12, 141)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxRef.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 434)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(624, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(405, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(516, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 6
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
        'LabelRef
        '
        Me.LabelRef.AutoSize = True
        Me.LabelRef.Location = New System.Drawing.Point(12, 125)
        Me.LabelRef.Name = "LabelRef"
        Me.LabelRef.Size = New System.Drawing.Size(24, 13)
        Me.LabelRef.TabIndex = 57
        Me.LabelRef.Text = "Ref"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 193)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Data inici"
        '
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerTo.Location = New System.Drawing.Point(80, 216)
        Me.DateTimePickerTo.Name = "DateTimePickerTo"
        Me.DateTimePickerTo.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerTo.TabIndex = 5
        Me.DateTimePickerTo.Visible = False
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(14, 218)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxFchTo.TabIndex = 4
        Me.CheckBoxFchTo.Text = "Caduca"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(12, 96)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(250, 20)
        Me.Xl_Contact21.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Lliurador"
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Location = New System.Drawing.Point(12, 13)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 65
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(268, 12)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 66
        '
        'Frm_SepaMandato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 465)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Xl_Iban1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelRef)
        Me.Name = "Frm_SepaMandato"
        Me.Text = "Mandato Sepa"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePickerFrom As DateTimePicker
    Friend WithEvents TextBoxRef As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents LabelRef As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePickerTo As DateTimePicker
    Friend WithEvents CheckBoxFchTo As CheckBox
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Iban1 As Xl_Iban
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
End Class
