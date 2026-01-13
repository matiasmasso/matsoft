<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Iban
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
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxFchTo = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxGuid = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Mat.NET.Xl_Contact2()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Xl_Lookup_BankBranch1 = New Mat.NET.Xl_Lookup_BankBranch()
        Me.Xl_IbanTextbox1 = New Mat.NET.Xl_IbanTextbox()
        Me.LabelUploaded = New System.Windows.Forms.Label()
        Me.LabelApproved = New System.Windows.Forms.Label()
        Me.LabelDownloaded = New System.Windows.Forms.Label()
        Me.ButtonApprove = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(83, 167)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchFrom.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Iban"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 423)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(811, 31)
        Me.Panel1.TabIndex = 49
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(592, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(703, 4)
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Entitat"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "data"
        '
        'CheckBoxFchTo
        '
        Me.CheckBoxFchTo.AutoSize = True
        Me.CheckBoxFchTo.Location = New System.Drawing.Point(15, 197)
        Me.CheckBoxFchTo.Name = "CheckBoxFchTo"
        Me.CheckBoxFchTo.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxFchTo.TabIndex = 56
        Me.CheckBoxFchTo.Text = "caduca"
        Me.CheckBoxFchTo.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(83, 195)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchTo.TabIndex = 57
        Me.DateTimePickerFchTo.Visible = False
        '
        'TextBoxGuid
        '
        Me.TextBoxGuid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxGuid.Enabled = False
        Me.TextBoxGuid.Location = New System.Drawing.Point(83, 29)
        Me.TextBoxGuid.Name = "TextBoxGuid"
        Me.TextBoxGuid.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxGuid.TabIndex = 59
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Identificacio"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "Titular"
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Location = New System.Drawing.Point(83, 137)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxFormat.TabIndex = 63
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Format"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(83, 58)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(372, 20)
        Me.Xl_Contact21.TabIndex = 65
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(461, 1)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 62
        '
        'Xl_Lookup_BankBranch1
        '
        Me.Xl_Lookup_BankBranch1.Branch = Nothing
        Me.Xl_Lookup_BankBranch1.IsDirty = False
        Me.Xl_Lookup_BankBranch1.Location = New System.Drawing.Point(83, 110)
        Me.Xl_Lookup_BankBranch1.Name = "Xl_Lookup_BankBranch1"
        Me.Xl_Lookup_BankBranch1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_Lookup_BankBranch1.Size = New System.Drawing.Size(372, 20)
        Me.Xl_Lookup_BankBranch1.TabIndex = 53
        Me.Xl_Lookup_BankBranch1.Value = Nothing
        '
        'Xl_IbanTextbox1
        '
        Me.Xl_IbanTextbox1.Location = New System.Drawing.Point(83, 84)
        Me.Xl_IbanTextbox1.Name = "Xl_IbanTextbox1"
        Me.Xl_IbanTextbox1.Size = New System.Drawing.Size(372, 20)
        Me.Xl_IbanTextbox1.TabIndex = 66
        Me.Xl_IbanTextbox1.Value = ""
        '
        'LabelUploaded
        '
        Me.LabelUploaded.AutoSize = True
        Me.LabelUploaded.Location = New System.Drawing.Point(39, 48)
        Me.LabelUploaded.Name = "LabelUploaded"
        Me.LabelUploaded.Size = New System.Drawing.Size(79, 13)
        Me.LabelUploaded.TabIndex = 67
        Me.LabelUploaded.Text = "LabelUploaded"
        '
        'LabelApproved
        '
        Me.LabelApproved.AutoSize = True
        Me.LabelApproved.Location = New System.Drawing.Point(39, 69)
        Me.LabelApproved.Name = "LabelApproved"
        Me.LabelApproved.Size = New System.Drawing.Size(79, 13)
        Me.LabelApproved.TabIndex = 68
        Me.LabelApproved.Text = "LabelApproved"
        '
        'LabelDownloaded
        '
        Me.LabelDownloaded.AutoSize = True
        Me.LabelDownloaded.Location = New System.Drawing.Point(39, 27)
        Me.LabelDownloaded.Name = "LabelDownloaded"
        Me.LabelDownloaded.Size = New System.Drawing.Size(93, 13)
        Me.LabelDownloaded.TabIndex = 69
        Me.LabelDownloaded.Text = "LabelDownloaded"
        '
        'ButtonApprove
        '
        Me.ButtonApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonApprove.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonApprove.Location = New System.Drawing.Point(320, 70)
        Me.ButtonApprove.Name = "ButtonApprove"
        Me.ButtonApprove.Size = New System.Drawing.Size(104, 24)
        Me.ButtonApprove.TabIndex = 70
        Me.ButtonApprove.Text = "Validar"
        Me.ButtonApprove.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonApprove)
        Me.GroupBox1.Controls.Add(Me.LabelDownloaded)
        Me.GroupBox1.Controls.Add(Me.LabelApproved)
        Me.GroupBox1.Controls.Add(Me.LabelUploaded)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 243)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 100)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status"
        '
        'Frm_Iban
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 454)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_IbanTextbox1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxGuid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePickerFchTo)
        Me.Controls.Add(Me.CheckBoxFchTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Lookup_BankBranch1)
        Me.Controls.Add(Me.DateTimePickerFchFrom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Iban"
        Me.Text = "Domiciliació bancària"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_Lookup_BankBranch1 As Mat.NET.Xl_Lookup_BankBranch
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxFchTo As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
    Friend WithEvents ComboBoxFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact21 As Mat.NET.Xl_Contact2
    Friend WithEvents Xl_IbanTextbox1 As Mat.NET.Xl_IbanTextbox
    Friend WithEvents LabelUploaded As System.Windows.Forms.Label
    Friend WithEvents LabelApproved As System.Windows.Forms.Label
    Friend WithEvents LabelDownloaded As System.Windows.Forms.Label
    Friend WithEvents ButtonApprove As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
