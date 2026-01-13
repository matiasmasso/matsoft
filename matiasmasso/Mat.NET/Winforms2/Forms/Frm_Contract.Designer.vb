<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Contract
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxCodis = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxIndefinit = New System.Windows.Forms.CheckBox()
        Me.Xl_Contact1 = New Xl_Contact2()
        Me.TextBoxNum = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Logo1 = New Xl_Contact_Logo()
        Me.Xl_DocFile1 = New Xl_DocFile_Old()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 494)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(781, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(562, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 14
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(673, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 15
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
        Me.ButtonDel.TabIndex = 13
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Concepte"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(89, 127)
        Me.TextBoxNom.MaxLength = 50
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(315, 20)
        Me.TextBoxNom.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Contacte"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Codi"
        '
        'ComboBoxCodis
        '
        Me.ComboBoxCodis.FormattingEnabled = True
        Me.ComboBoxCodis.Location = New System.Drawing.Point(89, 74)
        Me.ComboBoxCodis.Name = "ComboBoxCodis"
        Me.ComboBoxCodis.Size = New System.Drawing.Size(315, 21)
        Me.ComboBoxCodis.TabIndex = 1
        Me.ComboBoxCodis.TabStop = False
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(209, 210)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchFrom.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "data de inici"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(95, 239)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "data de finalització"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(209, 236)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerFchTo.TabIndex = 12
        '
        'CheckBoxIndefinit
        '
        Me.CheckBoxIndefinit.AutoSize = True
        Me.CheckBoxIndefinit.Location = New System.Drawing.Point(315, 212)
        Me.CheckBoxIndefinit.Name = "CheckBoxIndefinit"
        Me.CheckBoxIndefinit.Size = New System.Drawing.Size(63, 17)
        Me.CheckBoxIndefinit.TabIndex = 10
        Me.CheckBoxIndefinit.Text = "Indefinit"
        Me.CheckBoxIndefinit.UseVisualStyleBackColor = True
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(89, 101)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(315, 20)
        Me.Xl_Contact1.TabIndex = 3
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(89, 153)
        Me.TextBoxNum.MaxLength = 30
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(220, 20)
        Me.TextBoxNum.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Num.contracte"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(765, 451)
        Me.TabControl1.TabIndex = 55
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_DocFile1)
        Me.TabPage1.Controls.Add(Me.CheckBoxPrivat)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxNum)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.CheckBoxIndefinit)
        Me.TabPage1.Controls.Add(Me.Xl_Contact1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.ComboBoxCodis)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchTo)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchFrom)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(757, 425)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "GENERAL"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(352, 155)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(52, 17)
        Me.CheckBoxPrivat.TabIndex = 13
        Me.CheckBoxPrivat.Text = "privat"
        Me.CheckBoxPrivat.UseVisualStyleBackColor = True
        Me.CheckBoxPrivat.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(757, 425)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "DETALL"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(627, 3)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 56
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(407, 3)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 14
        '
        'Frm_Contract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 525)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Contract"
        Me.Text = "CONTRACTE"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact2
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCodis As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxIndefinit As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
End Class
