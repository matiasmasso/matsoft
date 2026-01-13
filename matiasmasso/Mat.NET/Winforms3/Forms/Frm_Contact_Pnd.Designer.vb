Partial Public Class Frm_Contact_Pnd
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPagePnd = New System.Windows.Forms.TabPage()
        Me.GroupBoxVto = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Xl_LookupCca2 = New Xl_LookupCca()
        Me.Xl_AmtEur = New Xl_Amt()
        Me.Xl_AmtCurDivisa = New Xl_AmountCur()
        Me.Xl_Cta1 = New Xl_Cta()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBoxStatus = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CheckBoxDivisa = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox()
        Me.ComboBoxAD = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBoxCca = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxFraNum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_LookupCca1 = New Xl_LookupCca()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxYea = New System.Windows.Forms.TextBox()
        Me.LabelEUR = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxContactNom = New System.Windows.Forms.TextBox()
        Me.TabPageCsb = New System.Windows.Forms.TabPage()
        Me.GroupBoxCsa = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBoxTxt = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBoxVto = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox()
        Me.PictureBoxCsbIban = New System.Windows.Forms.PictureBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxCsbDoc = New System.Windows.Forms.TextBox()
        Me.LabelCsa = New System.Windows.Forms.Label()
        Me.PictureBoxBancLogo = New System.Windows.Forms.PictureBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPagePnd.SuspendLayout()
        Me.GroupBoxVto.SuspendLayout()
        Me.GroupBoxCca.SuspendLayout()
        Me.TabPageCsb.SuspendLayout()
        Me.GroupBoxCsa.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxCsbIban, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPagePnd)
        Me.TabControl1.Controls.Add(Me.TabPageCsb)
        Me.TabControl1.Location = New System.Drawing.Point(3, 13)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(409, 453)
        Me.TabControl1.TabIndex = 0
        '
        'TabPagePnd
        '
        Me.TabPagePnd.Controls.Add(Me.GroupBoxVto)
        Me.TabPagePnd.Controls.Add(Me.Xl_AmtEur)
        Me.TabPagePnd.Controls.Add(Me.Xl_AmtCurDivisa)
        Me.TabPagePnd.Controls.Add(Me.Xl_Cta1)
        Me.TabPagePnd.Controls.Add(Me.Label11)
        Me.TabPagePnd.Controls.Add(Me.ComboBoxStatus)
        Me.TabPagePnd.Controls.Add(Me.Label10)
        Me.TabPagePnd.Controls.Add(Me.CheckBoxDivisa)
        Me.TabPagePnd.Controls.Add(Me.Label8)
        Me.TabPagePnd.Controls.Add(Me.Label7)
        Me.TabPagePnd.Controls.Add(Me.TextBoxObs)
        Me.TabPagePnd.Controls.Add(Me.ComboBoxCfp)
        Me.TabPagePnd.Controls.Add(Me.ComboBoxAD)
        Me.TabPagePnd.Controls.Add(Me.Label6)
        Me.TabPagePnd.Controls.Add(Me.GroupBoxCca)
        Me.TabPagePnd.Controls.Add(Me.LabelEUR)
        Me.TabPagePnd.Controls.Add(Me.Label1)
        Me.TabPagePnd.Controls.Add(Me.TextBoxContactNom)
        Me.TabPagePnd.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePnd.Name = "TabPagePnd"
        Me.TabPagePnd.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePnd.Size = New System.Drawing.Size(401, 427)
        Me.TabPagePnd.TabIndex = 0
        Me.TabPagePnd.Text = "GENERAL"
        '
        'GroupBoxVto
        '
        Me.GroupBoxVto.Controls.Add(Me.Label18)
        Me.GroupBoxVto.Controls.Add(Me.Label2)
        Me.GroupBoxVto.Controls.Add(Me.DateTimePickerVto)
        Me.GroupBoxVto.Controls.Add(Me.Xl_LookupCca2)
        Me.GroupBoxVto.Location = New System.Drawing.Point(8, 338)
        Me.GroupBoxVto.Name = "GroupBoxVto"
        Me.GroupBoxVto.Size = New System.Drawing.Size(385, 78)
        Me.GroupBoxVto.TabIndex = 27
        Me.GroupBoxVto.TabStop = False
        Me.GroupBoxVto.Text = "Venciment"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(24, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(31, 13)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "data:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "assentament:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(102, 19)
        Me.DateTimePickerVto.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(84, 20)
        Me.DateTimePickerVto.TabIndex = 12
        '
        'Xl_LookupCca2
        '
        Me.Xl_LookupCca2.Cca = Nothing
        Me.Xl_LookupCca2.IsDirty = False
        Me.Xl_LookupCca2.Location = New System.Drawing.Point(102, 45)
        Me.Xl_LookupCca2.Name = "Xl_LookupCca2"
        Me.Xl_LookupCca2.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCca2.ReadOnlyLookup = False
        Me.Xl_LookupCca2.Size = New System.Drawing.Size(270, 21)
        Me.Xl_LookupCca2.TabIndex = 13
        Me.Xl_LookupCca2.Value = Nothing
        '
        'Xl_AmtEur
        '
        Me.Xl_AmtEur.Location = New System.Drawing.Point(88, 42)
        Me.Xl_AmtEur.Name = "Xl_AmtEur"
        Me.Xl_AmtEur.Size = New System.Drawing.Size(104, 20)
        Me.Xl_AmtEur.TabIndex = 22
        Me.Xl_AmtEur.Value = Nothing
        '
        'Xl_AmtCurDivisa
        '
        Me.Xl_AmtCurDivisa.Amt = Nothing
        Me.Xl_AmtCurDivisa.Location = New System.Drawing.Point(88, 63)
        Me.Xl_AmtCurDivisa.Name = "Xl_AmtCurDivisa"
        Me.Xl_AmtCurDivisa.Size = New System.Drawing.Size(135, 20)
        Me.Xl_AmtCurDivisa.TabIndex = 21
        Me.Xl_AmtCurDivisa.Visible = False
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(88, 92)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(133, 20)
        Me.Xl_Cta1.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(43, 134)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "status:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ComboBoxStatus
        '
        Me.ComboBoxStatus.FormattingEnabled = True
        Me.ComboBoxStatus.Location = New System.Drawing.Point(88, 131)
        Me.ComboBoxStatus.Name = "ComboBoxStatus"
        Me.ComboBoxStatus.Size = New System.Drawing.Size(133, 21)
        Me.ComboBoxStatus.TabIndex = 14
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(61, 115)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(21, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "rol:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CheckBoxDivisa
        '
        Me.CheckBoxDivisa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDivisa.AutoSize = True
        Me.CheckBoxDivisa.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDivisa.Location = New System.Drawing.Point(23, 64)
        Me.CheckBoxDivisa.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxDivisa.Name = "CheckBoxDivisa"
        Me.CheckBoxDivisa.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxDivisa.TabIndex = 4
        Me.CheckBoxDivisa.TabStop = False
        Me.CheckBoxDivisa.Text = "Divisa"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 192)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "observacions:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 168)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "forma de pago:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(88, 189)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(305, 20)
        Me.TextBoxObs.TabIndex = 18
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(88, 164)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(305, 21)
        Me.ComboBoxCfp.TabIndex = 16
        '
        'ComboBoxAD
        '
        Me.ComboBoxAD.FormattingEnabled = True
        Me.ComboBoxAD.Items.AddRange(New Object() {"deutor", "creditor"})
        Me.ComboBoxAD.Location = New System.Drawing.Point(88, 112)
        Me.ComboBoxAD.Name = "ComboBoxAD"
        Me.ComboBoxAD.Size = New System.Drawing.Size(133, 21)
        Me.ComboBoxAD.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(40, 96)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "compte"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBoxCca
        '
        Me.GroupBoxCca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCca.Controls.Add(Me.DateTimePicker2)
        Me.GroupBoxCca.Controls.Add(Me.Label9)
        Me.GroupBoxCca.Controls.Add(Me.Label5)
        Me.GroupBoxCca.Controls.Add(Me.TextBoxFraNum)
        Me.GroupBoxCca.Controls.Add(Me.Label4)
        Me.GroupBoxCca.Controls.Add(Me.Xl_LookupCca1)
        Me.GroupBoxCca.Controls.Add(Me.Label3)
        Me.GroupBoxCca.Controls.Add(Me.TextBoxYea)
        Me.GroupBoxCca.Location = New System.Drawing.Point(8, 215)
        Me.GroupBoxCca.Name = "GroupBoxCca"
        Me.GroupBoxCca.Size = New System.Drawing.Size(385, 116)
        Me.GroupBoxCca.TabIndex = 19
        Me.GroupBoxCca.TabStop = False
        Me.GroupBoxCca.Text = "Assentament"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(102, 73)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(84, 20)
        Me.DateTimePicker2.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(62, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "data:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "factura num.:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxFraNum
        '
        Me.TextBoxFraNum.Location = New System.Drawing.Point(102, 20)
        Me.TextBoxFraNum.Name = "TextBoxFraNum"
        Me.TextBoxFraNum.Size = New System.Drawing.Size(59, 20)
        Me.TextBoxFraNum.TabIndex = 21
        Me.TextBoxFraNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "assentament:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_LookupCca1
        '
        Me.Xl_LookupCca1.Cca = Nothing
        Me.Xl_LookupCca1.IsDirty = False
        Me.Xl_LookupCca1.Location = New System.Drawing.Point(102, 46)
        Me.Xl_LookupCca1.Name = "Xl_LookupCca1"
        Me.Xl_LookupCca1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCca1.ReadOnlyLookup = False
        Me.Xl_LookupCca1.Size = New System.Drawing.Size(270, 21)
        Me.Xl_LookupCca1.TabIndex = 10
        Me.Xl_LookupCca1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(169, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "any:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxYea
        '
        Me.TextBoxYea.Enabled = False
        Me.TextBoxYea.Location = New System.Drawing.Point(202, 20)
        Me.TextBoxYea.Name = "TextBoxYea"
        Me.TextBoxYea.Size = New System.Drawing.Size(59, 20)
        Me.TextBoxYea.TabIndex = 23
        Me.TextBoxYea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelEUR
        '
        Me.LabelEUR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelEUR.AutoSize = True
        Me.LabelEUR.Location = New System.Drawing.Point(194, 44)
        Me.LabelEUR.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelEUR.Name = "LabelEUR"
        Me.LabelEUR.Size = New System.Drawing.Size(30, 13)
        Me.LabelEUR.TabIndex = 3
        Me.LabelEUR.Text = "EUR"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 45)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Import:"
        '
        'TextBoxContactNom
        '
        Me.TextBoxContactNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactNom.Location = New System.Drawing.Point(8, 13)
        Me.TextBoxContactNom.Name = "TextBoxContactNom"
        Me.TextBoxContactNom.ReadOnly = True
        Me.TextBoxContactNom.Size = New System.Drawing.Size(385, 20)
        Me.TextBoxContactNom.TabIndex = 0
        Me.TextBoxContactNom.TabStop = False
        '
        'TabPageCsb
        '
        Me.TabPageCsb.Controls.Add(Me.GroupBoxCsa)
        Me.TabPageCsb.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCsb.Name = "TabPageCsb"
        Me.TabPageCsb.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCsb.Size = New System.Drawing.Size(401, 427)
        Me.TabPageCsb.TabIndex = 1
        Me.TabPageCsb.Text = "EFECTE"
        '
        'GroupBoxCsa
        '
        Me.GroupBoxCsa.Controls.Add(Me.GroupBox1)
        Me.GroupBoxCsa.Controls.Add(Me.LabelCsa)
        Me.GroupBoxCsa.Controls.Add(Me.PictureBoxBancLogo)
        Me.GroupBoxCsa.Location = New System.Drawing.Point(10, 13)
        Me.GroupBoxCsa.Name = "GroupBoxCsa"
        Me.GroupBoxCsa.Size = New System.Drawing.Size(368, 378)
        Me.GroupBoxCsa.TabIndex = 3
        Me.GroupBoxCsa.TabStop = False
        Me.GroupBoxCsa.Text = "remesa"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.TextBoxTxt)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.TextBoxVto)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.TextBoxCliNom)
        Me.GroupBox1.Controls.Add(Me.PictureBoxCsbIban)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.TextBoxCsbDoc)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 130)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 242)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "document"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "domicliliació:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "concepte:"
        '
        'TextBoxTxt
        '
        Me.TextBoxTxt.Location = New System.Drawing.Point(95, 161)
        Me.TextBoxTxt.Name = "TextBoxTxt"
        Me.TextBoxTxt.ReadOnly = True
        Me.TextBoxTxt.Size = New System.Drawing.Size(243, 20)
        Me.TextBoxTxt.TabIndex = 19
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 137)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 13)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "import:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(95, 134)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(90, 20)
        Me.TextBox1.TabIndex = 17
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(5, 110)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 13)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "venciment:"
        '
        'TextBoxVto
        '
        Me.TextBoxVto.Location = New System.Drawing.Point(94, 107)
        Me.TextBoxVto.Name = "TextBoxVto"
        Me.TextBoxVto.ReadOnly = True
        Me.TextBoxVto.Size = New System.Drawing.Size(91, 20)
        Me.TextBoxVto.TabIndex = 15
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(5, 83)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(34, 13)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "lliurat:"
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Location = New System.Drawing.Point(94, 80)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(244, 20)
        Me.TextBoxCliNom.TabIndex = 13
        '
        'PictureBoxCsbIban
        '
        Me.PictureBoxCsbIban.Location = New System.Drawing.Point(94, 188)
        Me.PictureBoxCsbIban.Name = "PictureBoxCsbIban"
        Me.PictureBoxCsbIban.Size = New System.Drawing.Size(244, 50)
        Me.PictureBoxCsbIban.TabIndex = 12
        Me.PictureBoxCsbIban.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(5, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "efecte num:"
        '
        'TextBoxCsbDoc
        '
        Me.TextBoxCsbDoc.Location = New System.Drawing.Point(95, 26)
        Me.TextBoxCsbDoc.Name = "TextBoxCsbDoc"
        Me.TextBoxCsbDoc.ReadOnly = True
        Me.TextBoxCsbDoc.Size = New System.Drawing.Size(57, 20)
        Me.TextBoxCsbDoc.TabIndex = 33
        '
        'LabelCsa
        '
        Me.LabelCsa.AutoSize = True
        Me.LabelCsa.Location = New System.Drawing.Point(8, 37)
        Me.LabelCsa.Name = "LabelCsa"
        Me.LabelCsa.Size = New System.Drawing.Size(50, 13)
        Me.LabelCsa.TabIndex = 5
        Me.LabelCsa.Text = "remesa..."
        '
        'PictureBoxBancLogo
        '
        Me.PictureBoxBancLogo.Location = New System.Drawing.Point(11, 53)
        Me.PictureBoxBancLogo.Name = "PictureBoxBancLogo"
        Me.PictureBoxBancLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxBancLogo.TabIndex = 1
        Me.PictureBoxBancLogo.TabStop = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(184, 473)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(113, 25)
        Me.ButtonCancel.TabIndex = 15
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(299, 473)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(113, 25)
        Me.ButtonOk.TabIndex = 14
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(3, 473)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(113, 25)
        Me.ButtonDel.TabIndex = 16
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'Frm_Contact_Pnd
        '
        Me.ClientSize = New System.Drawing.Size(413, 502)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Contact_Pnd"
        Me.Text = "PARTIDA PENDENT DE LIQUIDAR"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPagePnd.ResumeLayout(False)
        Me.TabPagePnd.PerformLayout()
        Me.GroupBoxVto.ResumeLayout(False)
        Me.GroupBoxVto.PerformLayout()
        Me.GroupBoxCca.ResumeLayout(False)
        Me.GroupBoxCca.PerformLayout()
        Me.TabPageCsb.ResumeLayout(False)
        Me.GroupBoxCsa.ResumeLayout(False)
        Me.GroupBoxCsa.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBoxCsbIban, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBancLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPagePnd As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCsb As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxContactNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelEUR As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBoxCca As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFraNum As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupCca1 As Xl_LookupCca
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYea As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAD As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxDivisa As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxCsa As System.Windows.Forms.GroupBox
    Friend WithEvents LabelCsa As System.Windows.Forms.Label
    Friend WithEvents PictureBoxBancLogo As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCsbDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBoxVto As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxCsbIban As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_Cta1 As Xl_Cta
    Friend WithEvents Xl_AmtEur As Xl_Amt
    Friend WithEvents Xl_AmtCurDivisa As Xl_AmountCur
    Friend WithEvents GroupBoxVto As GroupBox
    Friend WithEvents Xl_LookupCca2 As Xl_LookupCca
    Friend WithEvents Label18 As Label
    Friend WithEvents Label2 As Label
End Class
