Partial Public Class Xl_Cobrament
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonPagare = New System.Windows.Forms.RadioButton()
        Me.RadioButtonXec = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTransfer = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCash = New System.Windows.Forms.RadioButton()
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox()
        Me.LabelConcepte = New System.Windows.Forms.Label()
        Me.GroupBoxXec = New System.Windows.Forms.GroupBox()
        Me.Xl_IbanDigits1 = New Winforms.Xl_IbanDigits()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelVto = New System.Windows.Forms.Label()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxXecNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxBancs = New System.Windows.Forms.ComboBox()
        Me.GroupBoxTransfer = New System.Windows.Forms.GroupBox()
        Me.Xl_IbanDigitsTransfer = New Winforms.Xl_IbanDigits()
        Me.TextBoxConcepteBeneficiari = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxXec.SuspendLayout()
        Me.GroupBoxTransfer.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonPagare)
        Me.GroupBox1.Controls.Add(Me.RadioButtonXec)
        Me.GroupBox1.Controls.Add(Me.RadioButtonTransfer)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCash)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(103, 160)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "forma de pago:"
        '
        'RadioButtonPagare
        '
        Me.RadioButtonPagare.AutoSize = True
        Me.RadioButtonPagare.Location = New System.Drawing.Point(7, 80)
        Me.RadioButtonPagare.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.RadioButtonPagare.Name = "RadioButtonPagare"
        Me.RadioButtonPagare.Size = New System.Drawing.Size(58, 17)
        Me.RadioButtonPagare.TabIndex = 24
        Me.RadioButtonPagare.Text = "pagaré"
        '
        'RadioButtonXec
        '
        Me.RadioButtonXec.AutoSize = True
        Me.RadioButtonXec.Location = New System.Drawing.Point(7, 60)
        Me.RadioButtonXec.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.RadioButtonXec.Name = "RadioButtonXec"
        Me.RadioButtonXec.Size = New System.Drawing.Size(42, 17)
        Me.RadioButtonXec.TabIndex = 23
        Me.RadioButtonXec.Text = "xec"
        '
        'RadioButtonTransfer
        '
        Me.RadioButtonTransfer.AutoSize = True
        Me.RadioButtonTransfer.Location = New System.Drawing.Point(7, 40)
        Me.RadioButtonTransfer.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.RadioButtonTransfer.Name = "RadioButtonTransfer"
        Me.RadioButtonTransfer.Size = New System.Drawing.Size(86, 17)
        Me.RadioButtonTransfer.TabIndex = 22
        Me.RadioButtonTransfer.Text = "transferencia"
        '
        'RadioButtonCash
        '
        Me.RadioButtonCash.AutoSize = True
        Me.RadioButtonCash.Location = New System.Drawing.Point(7, 20)
        Me.RadioButtonCash.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.RadioButtonCash.Name = "RadioButtonCash"
        Me.RadioButtonCash.Size = New System.Drawing.Size(57, 17)
        Me.RadioButtonCash.TabIndex = 21
        Me.RadioButtonCash.Text = "efectiu"
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(74, 180)
        Me.TextBoxConcepte.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(317, 20)
        Me.TextBoxConcepte.TabIndex = 9
        Me.TextBoxConcepte.Visible = False
        '
        'LabelConcepte
        '
        Me.LabelConcepte.AutoSize = True
        Me.LabelConcepte.Location = New System.Drawing.Point(11, 183)
        Me.LabelConcepte.Name = "LabelConcepte"
        Me.LabelConcepte.Size = New System.Drawing.Size(56, 13)
        Me.LabelConcepte.TabIndex = 10
        Me.LabelConcepte.Text = "Concepte:"
        Me.LabelConcepte.Visible = False
        '
        'GroupBoxXec
        '
        Me.GroupBoxXec.Controls.Add(Me.Xl_IbanDigits1)
        Me.GroupBoxXec.Controls.Add(Me.Label2)
        Me.GroupBoxXec.Controls.Add(Me.LabelVto)
        Me.GroupBoxXec.Controls.Add(Me.DateTimePickerVto)
        Me.GroupBoxXec.Controls.Add(Me.TextBoxXecNum)
        Me.GroupBoxXec.Controls.Add(Me.Label1)
        Me.GroupBoxXec.Location = New System.Drawing.Point(134, 4)
        Me.GroupBoxXec.Name = "GroupBoxXec"
        Me.GroupBoxXec.Size = New System.Drawing.Size(266, 160)
        Me.GroupBoxXec.TabIndex = 0
        Me.GroupBoxXec.TabStop = False
        Me.GroupBoxXec.Text = "xec"
        Me.GroupBoxXec.Visible = False
        '
        'Xl_IbanDigits1
        '
        Me.Xl_IbanDigits1.Location = New System.Drawing.Point(7, 80)
        Me.Xl_IbanDigits1.Name = "Xl_IbanDigits1"
        Me.Xl_IbanDigits1.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigits1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Compte:"
        '
        'LabelVto
        '
        Me.LabelVto.AutoSize = True
        Me.LabelVto.Enabled = False
        Me.LabelVto.Location = New System.Drawing.Point(107, 43)
        Me.LabelVto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.LabelVto.Name = "LabelVto"
        Me.LabelVto.Size = New System.Drawing.Size(56, 13)
        Me.LabelVto.TabIndex = 2
        Me.LabelVto.Text = "venciment"
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Enabled = False
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(171, 41)
        Me.DateTimePickerVto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(86, 20)
        Me.DateTimePickerVto.TabIndex = 3
        '
        'TextBoxXecNum
        '
        Me.TextBoxXecNum.Location = New System.Drawing.Point(171, 17)
        Me.TextBoxXecNum.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.TextBoxXecNum.Name = "TextBoxXecNum"
        Me.TextBoxXecNum.Size = New System.Drawing.Size(86, 20)
        Me.TextBoxXecNum.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(107, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "xec num:"
        '
        'ComboBoxBancs
        '
        Me.ComboBoxBancs.FormattingEnabled = True
        Me.ComboBoxBancs.Location = New System.Drawing.Point(81, 17)
        Me.ComboBoxBancs.Name = "ComboBoxBancs"
        Me.ComboBoxBancs.Size = New System.Drawing.Size(176, 21)
        Me.ComboBoxBancs.TabIndex = 7
        '
        'GroupBoxTransfer
        '
        Me.GroupBoxTransfer.Controls.Add(Me.Label3)
        Me.GroupBoxTransfer.Controls.Add(Me.Xl_IbanDigitsTransfer)
        Me.GroupBoxTransfer.Controls.Add(Me.ComboBoxBancs)
        Me.GroupBoxTransfer.Controls.Add(Me.TextBoxConcepteBeneficiari)
        Me.GroupBoxTransfer.Controls.Add(Me.Label5)
        Me.GroupBoxTransfer.Location = New System.Drawing.Point(134, 215)
        Me.GroupBoxTransfer.Name = "GroupBoxTransfer"
        Me.GroupBoxTransfer.Size = New System.Drawing.Size(266, 160)
        Me.GroupBoxTransfer.TabIndex = 6
        Me.GroupBoxTransfer.TabStop = False
        Me.GroupBoxTransfer.Text = "transferencia"
        Me.GroupBoxTransfer.Visible = False
        '
        'Xl_IbanDigitsTransfer
        '
        Me.Xl_IbanDigitsTransfer.Location = New System.Drawing.Point(7, 80)
        Me.Xl_IbanDigitsTransfer.Name = "Xl_IbanDigitsTransfer"
        Me.Xl_IbanDigitsTransfer.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigitsTransfer.TabIndex = 5
        '
        'TextBoxConcepteBeneficiari
        '
        Me.TextBoxConcepteBeneficiari.Location = New System.Drawing.Point(6, 55)
        Me.TextBoxConcepteBeneficiari.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.TextBoxConcepteBeneficiari.Name = "TextBoxConcepteBeneficiari"
        Me.TextBoxConcepteBeneficiari.Size = New System.Drawing.Size(251, 20)
        Me.TextBoxConcepteBeneficiari.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Concepte per el beneficiari"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Banc emisor:"
        '
        'Xl_Cobrament
        '
        Me.Controls.Add(Me.GroupBoxTransfer)
        Me.Controls.Add(Me.GroupBoxXec)
        Me.Controls.Add(Me.LabelConcepte)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Xl_Cobrament"
        Me.Size = New System.Drawing.Size(434, 417)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxXec.ResumeLayout(False)
        Me.GroupBoxXec.PerformLayout()
        Me.GroupBoxTransfer.ResumeLayout(False)
        Me.GroupBoxTransfer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonXec As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCash As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPagare As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents LabelConcepte As System.Windows.Forms.Label
    Friend WithEvents GroupBoxXec As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBoxBancs As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelVto As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxXecNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_IbanDigits1 As Winforms.Xl_IbanDigits
    Friend WithEvents GroupBoxTransfer As GroupBox
    Friend WithEvents Xl_IbanDigitsTransfer As Xl_IbanDigits
    Friend WithEvents TextBoxConcepteBeneficiari As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
End Class
