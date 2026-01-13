Partial Public Class Xl_Pagaments
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
        Me.RadioButtonVISA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEfecte = New System.Windows.Forms.RadioButton()
        Me.RadioButtonXec = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTransfer = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCash = New System.Windows.Forms.RadioButton()
        Me.LabelConcepte = New System.Windows.Forms.Label()
        Me.TextBoxConcepte = New System.Windows.Forms.TextBox()
        Me.GroupBoxBanc = New System.Windows.Forms.GroupBox()
        Me.TextBoxXecNum = New System.Windows.Forms.TextBox()
        Me.LabelXecNum = New System.Windows.Forms.Label()
        Me.ComboBoxBancs = New System.Windows.Forms.ComboBox()
        Me.PictureBoxBanc = New System.Windows.Forms.PictureBox()
        Me.GroupBoxVisas = New System.Windows.Forms.GroupBox()
        Me.GroupBoxTransfer = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Xl_LookupVisaCard1 = New Winforms.Xl_LookupVisaCard()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_IbanDigits1 = New Winforms.Xl_IbanDigits()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxBanc.SuspendLayout()
        CType(Me.PictureBoxBanc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxVisas.SuspendLayout()
        Me.GroupBoxTransfer.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonVISA)
        Me.GroupBox1.Controls.Add(Me.RadioButtonEfecte)
        Me.GroupBox1.Controls.Add(Me.RadioButtonXec)
        Me.GroupBox1.Controls.Add(Me.RadioButtonTransfer)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCash)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(103, 160)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "forma de pago:"
        '
        'RadioButtonVISA
        '
        Me.RadioButtonVISA.AutoSize = True
        Me.RadioButtonVISA.Location = New System.Drawing.Point(7, 88)
        Me.RadioButtonVISA.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.RadioButtonVISA.Name = "RadioButtonVISA"
        Me.RadioButtonVISA.Size = New System.Drawing.Size(49, 17)
        Me.RadioButtonVISA.TabIndex = 6
        Me.RadioButtonVISA.Text = "VISA"
        '
        'RadioButtonEfecte
        '
        Me.RadioButtonEfecte.AutoSize = True
        Me.RadioButtonEfecte.Location = New System.Drawing.Point(7, 108)
        Me.RadioButtonEfecte.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.RadioButtonEfecte.Name = "RadioButtonEfecte"
        Me.RadioButtonEfecte.Size = New System.Drawing.Size(55, 17)
        Me.RadioButtonEfecte.TabIndex = 5
        Me.RadioButtonEfecte.Text = "efecte"
        '
        'RadioButtonXec
        '
        Me.RadioButtonXec.AutoSize = True
        Me.RadioButtonXec.Location = New System.Drawing.Point(7, 68)
        Me.RadioButtonXec.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.RadioButtonXec.Name = "RadioButtonXec"
        Me.RadioButtonXec.Size = New System.Drawing.Size(42, 17)
        Me.RadioButtonXec.TabIndex = 4
        Me.RadioButtonXec.Text = "xec"
        '
        'RadioButtonTransfer
        '
        Me.RadioButtonTransfer.AutoSize = True
        Me.RadioButtonTransfer.Location = New System.Drawing.Point(7, 48)
        Me.RadioButtonTransfer.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.RadioButtonTransfer.Name = "RadioButtonTransfer"
        Me.RadioButtonTransfer.Size = New System.Drawing.Size(86, 17)
        Me.RadioButtonTransfer.TabIndex = 3
        Me.RadioButtonTransfer.Text = "transferencia"
        '
        'RadioButtonCash
        '
        Me.RadioButtonCash.AutoSize = True
        Me.RadioButtonCash.Location = New System.Drawing.Point(7, 28)
        Me.RadioButtonCash.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.RadioButtonCash.Name = "RadioButtonCash"
        Me.RadioButtonCash.Size = New System.Drawing.Size(57, 17)
        Me.RadioButtonCash.TabIndex = 2
        Me.RadioButtonCash.Text = "efectiu"
        '
        'LabelConcepte
        '
        Me.LabelConcepte.AutoSize = True
        Me.LabelConcepte.Location = New System.Drawing.Point(7, 173)
        Me.LabelConcepte.Name = "LabelConcepte"
        Me.LabelConcepte.Size = New System.Drawing.Size(56, 13)
        Me.LabelConcepte.TabIndex = 12
        Me.LabelConcepte.Text = "Concepte:"
        Me.LabelConcepte.Visible = False
        '
        'TextBoxConcepte
        '
        Me.TextBoxConcepte.Location = New System.Drawing.Point(7, 192)
        Me.TextBoxConcepte.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxConcepte.Name = "TextBoxConcepte"
        Me.TextBoxConcepte.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxConcepte.TabIndex = 11
        Me.TextBoxConcepte.Visible = False
        '
        'GroupBoxBanc
        '
        Me.GroupBoxBanc.Controls.Add(Me.TextBoxXecNum)
        Me.GroupBoxBanc.Controls.Add(Me.LabelXecNum)
        Me.GroupBoxBanc.Controls.Add(Me.ComboBoxBancs)
        Me.GroupBoxBanc.Controls.Add(Me.PictureBoxBanc)
        Me.GroupBoxBanc.Location = New System.Drawing.Point(110, 5)
        Me.GroupBoxBanc.Name = "GroupBoxBanc"
        Me.GroupBoxBanc.Size = New System.Drawing.Size(210, 159)
        Me.GroupBoxBanc.TabIndex = 14
        Me.GroupBoxBanc.TabStop = False
        Me.GroupBoxBanc.Text = "el nostre banc:"
        Me.GroupBoxBanc.Visible = False
        '
        'TextBoxXecNum
        '
        Me.TextBoxXecNum.Location = New System.Drawing.Point(90, 109)
        Me.TextBoxXecNum.Margin = New System.Windows.Forms.Padding(3, 1, 3, 0)
        Me.TextBoxXecNum.Name = "TextBoxXecNum"
        Me.TextBoxXecNum.Size = New System.Drawing.Size(97, 20)
        Me.TextBoxXecNum.TabIndex = 12
        Me.TextBoxXecNum.Visible = False
        '
        'LabelXecNum
        '
        Me.LabelXecNum.AutoSize = True
        Me.LabelXecNum.Location = New System.Drawing.Point(37, 112)
        Me.LabelXecNum.Name = "LabelXecNum"
        Me.LabelXecNum.Size = New System.Drawing.Size(53, 13)
        Me.LabelXecNum.TabIndex = 11
        Me.LabelXecNum.Text = "xec num.:"
        Me.LabelXecNum.Visible = False
        '
        'ComboBoxBancs
        '
        Me.ComboBoxBancs.FormattingEnabled = True
        Me.ComboBoxBancs.Location = New System.Drawing.Point(37, 19)
        Me.ComboBoxBancs.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.ComboBoxBancs.Name = "ComboBoxBancs"
        Me.ComboBoxBancs.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxBancs.TabIndex = 10
        '
        'PictureBoxBanc
        '
        Me.PictureBoxBanc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBanc.Location = New System.Drawing.Point(37, 38)
        Me.PictureBoxBanc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        Me.PictureBoxBanc.Name = "PictureBoxBanc"
        Me.PictureBoxBanc.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxBanc.TabIndex = 9
        Me.PictureBoxBanc.TabStop = False
        '
        'GroupBoxVisas
        '
        Me.GroupBoxVisas.Controls.Add(Me.Xl_LookupVisaCard1)
        Me.GroupBoxVisas.Location = New System.Drawing.Point(108, 228)
        Me.GroupBoxVisas.Name = "GroupBoxVisas"
        Me.GroupBoxVisas.Size = New System.Drawing.Size(210, 159)
        Me.GroupBoxVisas.TabIndex = 17
        Me.GroupBoxVisas.TabStop = False
        Me.GroupBoxVisas.Text = "tarja de crèdit:"
        Me.GroupBoxVisas.Visible = False
        '
        'GroupBoxTransfer
        '
        Me.GroupBoxTransfer.Controls.Add(Me.Xl_IbanDigits1)
        Me.GroupBoxTransfer.Controls.Add(Me.Label2)
        Me.GroupBoxTransfer.Controls.Add(Me.TextBox1)
        Me.GroupBoxTransfer.Controls.Add(Me.Label1)
        Me.GroupBoxTransfer.Controls.Add(Me.ComboBox1)
        Me.GroupBoxTransfer.Location = New System.Drawing.Point(35, 326)
        Me.GroupBoxTransfer.Name = "GroupBoxTransfer"
        Me.GroupBoxTransfer.Size = New System.Drawing.Size(210, 159)
        Me.GroupBoxTransfer.TabIndex = 18
        Me.GroupBoxTransfer.TabStop = False
        Me.GroupBoxTransfer.Text = "Transferencia:"
        Me.GroupBoxTransfer.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Concepte beneficiari:"
        Me.Label1.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(114, 24)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(89, 21)
        Me.ComboBox1.TabIndex = 10
        '
        'Xl_LookupVisaCard1
        '
        Me.Xl_LookupVisaCard1.IsDirty = False
        Me.Xl_LookupVisaCard1.Location = New System.Drawing.Point(6, 31)
        Me.Xl_LookupVisaCard1.Name = "Xl_LookupVisaCard1"
        Me.Xl_LookupVisaCard1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVisaCard1.Size = New System.Drawing.Size(198, 20)
        Me.Xl_LookupVisaCard1.TabIndex = 15
        Me.Xl_LookupVisaCard1.Value = Nothing
        Me.Xl_LookupVisaCard1.VisaCard = Nothing
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(114, 47)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(90, 20)
        Me.TextBox1.TabIndex = 12
        Me.TextBox1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Banc emisor:"
        Me.Label2.Visible = False
        '
        'Xl_IbanDigits1
        '
        Me.Xl_IbanDigits1.Location = New System.Drawing.Point(6, 82)
        Me.Xl_IbanDigits1.Name = "Xl_IbanDigits1"
        Me.Xl_IbanDigits1.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigits1.TabIndex = 14
        '
        'Xl_Pagaments
        '
        Me.Controls.Add(Me.GroupBoxTransfer)
        Me.Controls.Add(Me.GroupBoxVisas)
        Me.Controls.Add(Me.GroupBoxBanc)
        Me.Controls.Add(Me.LabelConcepte)
        Me.Controls.Add(Me.TextBoxConcepte)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Xl_Pagaments"
        Me.Size = New System.Drawing.Size(321, 488)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxBanc.ResumeLayout(False)
        Me.GroupBoxBanc.PerformLayout()
        CType(Me.PictureBoxBanc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxVisas.ResumeLayout(False)
        Me.GroupBoxTransfer.ResumeLayout(False)
        Me.GroupBoxTransfer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonEfecte As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonXec As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonTransfer As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCash As System.Windows.Forms.RadioButton
    Friend WithEvents LabelConcepte As System.Windows.Forms.Label
    Friend WithEvents TextBoxConcepte As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxBanc As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBoxBancs As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBoxBanc As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxXecNum As System.Windows.Forms.TextBox
    Friend WithEvents LabelXecNum As System.Windows.Forms.Label
    Friend WithEvents RadioButtonVISA As System.Windows.Forms.RadioButton
    Friend WithEvents Xl_LookupVisaCard1 As Winforms.Xl_LookupVisaCard
    Friend WithEvents GroupBoxVisas As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxTransfer As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Xl_IbanDigits1 As Xl_IbanDigits
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
End Class
