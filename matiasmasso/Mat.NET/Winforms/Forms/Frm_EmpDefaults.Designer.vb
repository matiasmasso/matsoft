<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EmpDefaults
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
        Dim DtoAmt2 As DTOAmt = New DTOAmt()
        Me.Xl_LookupSermepaConfigProduccio = New Winforms.Xl_LookupSermepaConfig()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_LookupSermepaConfigProves = New Winforms.Xl_LookupSermepaConfig()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_PercentImpago = New Winforms.Xl_Percent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_EurMinimImpago = New Winforms.Xl_Eur()
        Me.CheckBoxMatSchedLogLevelIntensive = New System.Windows.Forms.CheckBox()
        Me.TextBoxEdiversaPath = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_BancsComboBoxXNomines = New Winforms.Xl_BancsComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_BancsComboBoxTpv = New Winforms.Xl_BancsComboBox()
        Me.ComboBoxMatschedLogLevel = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Xl_ContactMgz = New Winforms.Xl_Contact2()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Xl_ContactTaller = New Winforms.Xl_Contact2()
        Me.NumericUpDownMatschedInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_PercentDtoTarifa = New Winforms.Xl_Percent()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_ContactSpvTrp = New Winforms.Xl_Contact2()
        Me.Label14 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDownMatschedInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_LookupSermepaConfigProduccio
        '
        Me.Xl_LookupSermepaConfigProduccio.IsDirty = False
        Me.Xl_LookupSermepaConfigProduccio.Location = New System.Drawing.Point(157, 68)
        Me.Xl_LookupSermepaConfigProduccio.Name = "Xl_LookupSermepaConfigProduccio"
        Me.Xl_LookupSermepaConfigProduccio.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupSermepaConfigProduccio.SermepaConfig = Nothing
        Me.Xl_LookupSermepaConfigProduccio.Size = New System.Drawing.Size(248, 20)
        Me.Xl_LookupSermepaConfigProduccio.TabIndex = 0
        Me.Xl_LookupSermepaConfigProduccio.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tpv Sermepa (producció)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tpv Sermepa (proves)"
        '
        'Xl_LookupSermepaConfigProves
        '
        Me.Xl_LookupSermepaConfigProves.IsDirty = False
        Me.Xl_LookupSermepaConfigProves.Location = New System.Drawing.Point(157, 94)
        Me.Xl_LookupSermepaConfigProves.Name = "Xl_LookupSermepaConfigProves"
        Me.Xl_LookupSermepaConfigProves.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupSermepaConfigProves.SermepaConfig = Nothing
        Me.Xl_LookupSermepaConfigProves.Size = New System.Drawing.Size(248, 20)
        Me.Xl_LookupSermepaConfigProves.TabIndex = 2
        Me.Xl_LookupSermepaConfigProves.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Despeses impago"
        '
        'Xl_PercentImpago
        '
        Me.Xl_PercentImpago.Location = New System.Drawing.Point(157, 123)
        Me.Xl_PercentImpago.Name = "Xl_PercentImpago"
        Me.Xl_PercentImpago.Size = New System.Drawing.Size(62, 20)
        Me.Xl_PercentImpago.TabIndex = 5
        Me.Xl_PercentImpago.Text = "0,00 %"
        Me.Xl_PercentImpago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentImpago.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(225, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "minim:"
        '
        'Xl_EurMinimImpago
        '
        DtoAmt2.Cur = Nothing
        DtoAmt2.Eur = New Decimal(New Integer() {0, 0, 0, 0})
        DtoAmt2.Val = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Xl_EurMinimImpago.Amt = DtoAmt2
        Me.Xl_EurMinimImpago.Location = New System.Drawing.Point(267, 123)
        Me.Xl_EurMinimImpago.Name = "Xl_EurMinimImpago"
        Me.Xl_EurMinimImpago.Size = New System.Drawing.Size(62, 20)
        Me.Xl_EurMinimImpago.TabIndex = 7
        Me.Xl_EurMinimImpago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxMatSchedLogLevelIntensive
        '
        Me.CheckBoxMatSchedLogLevelIntensive.AutoSize = True
        Me.CheckBoxMatSchedLogLevelIntensive.Location = New System.Drawing.Point(157, 150)
        Me.CheckBoxMatSchedLogLevelIntensive.Name = "CheckBoxMatSchedLogLevelIntensive"
        Me.CheckBoxMatSchedLogLevelIntensive.Size = New System.Drawing.Size(166, 17)
        Me.CheckBoxMatSchedLogLevelIntensive.TabIndex = 8
        Me.CheckBoxMatSchedLogLevelIntensive.Text = "MatSched Log level intensive"
        Me.CheckBoxMatSchedLogLevelIntensive.UseVisualStyleBackColor = True
        '
        'TextBoxEdiversaPath
        '
        Me.TextBoxEdiversaPath.Location = New System.Drawing.Point(157, 174)
        Me.TextBoxEdiversaPath.Name = "TextBoxEdiversaPath"
        Me.TextBoxEdiversaPath.Size = New System.Drawing.Size(255, 20)
        Me.TextBoxEdiversaPath.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 177)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "eDiversa root Path"
        '
        'Xl_BancsComboBoxXNomines
        '
        Me.Xl_BancsComboBoxXNomines.FormattingEnabled = True
        Me.Xl_BancsComboBoxXNomines.Location = New System.Drawing.Point(157, 201)
        Me.Xl_BancsComboBoxXNomines.Name = "Xl_BancsComboBoxXNomines"
        Me.Xl_BancsComboBoxXNomines.Size = New System.Drawing.Size(121, 21)
        Me.Xl_BancsComboBoxXNomines.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 204)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Banc x nomines"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Banc x Tpv"
        '
        'Xl_BancsComboBoxTpv
        '
        Me.Xl_BancsComboBoxTpv.FormattingEnabled = True
        Me.Xl_BancsComboBoxTpv.Location = New System.Drawing.Point(157, 41)
        Me.Xl_BancsComboBoxTpv.Name = "Xl_BancsComboBoxTpv"
        Me.Xl_BancsComboBoxTpv.Size = New System.Drawing.Size(121, 21)
        Me.Xl_BancsComboBoxTpv.TabIndex = 13
        '
        'ComboBoxMatschedLogLevel
        '
        Me.ComboBoxMatschedLogLevel.FormattingEnabled = True
        Me.ComboBoxMatschedLogLevel.Items.AddRange(New Object() {"Standard", "Verbose"})
        Me.ComboBoxMatschedLogLevel.Location = New System.Drawing.Point(157, 229)
        Me.ComboBoxMatschedLogLevel.Name = "ComboBoxMatschedLogLevel"
        Me.ComboBoxMatschedLogLevel.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxMatschedLogLevel.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 232)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "MatSched log level"
        '
        'Xl_ContactMgz
        '
        Me.Xl_ContactMgz.Contact = Nothing
        Me.Xl_ContactMgz.Location = New System.Drawing.Point(157, 298)
        Me.Xl_ContactMgz.Name = "Xl_ContactMgz"
        Me.Xl_ContactMgz.ReadOnly = False
        Me.Xl_ContactMgz.Size = New System.Drawing.Size(255, 20)
        Me.Xl_ContactMgz.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 302)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "magatzem"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 328)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "taller"
        '
        'Xl_ContactTaller
        '
        Me.Xl_ContactTaller.Contact = Nothing
        Me.Xl_ContactTaller.Location = New System.Drawing.Point(157, 324)
        Me.Xl_ContactTaller.Name = "Xl_ContactTaller"
        Me.Xl_ContactTaller.ReadOnly = False
        Me.Xl_ContactTaller.Size = New System.Drawing.Size(255, 20)
        Me.Xl_ContactTaller.TabIndex = 19
        '
        'NumericUpDownMatschedInterval
        '
        Me.NumericUpDownMatschedInterval.Location = New System.Drawing.Point(157, 257)
        Me.NumericUpDownMatschedInterval.Name = "NumericUpDownMatschedInterval"
        Me.NumericUpDownMatschedInterval.Size = New System.Drawing.Size(62, 20)
        Me.NumericUpDownMatschedInterval.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(25, 259)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(132, 13)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "MatSched interval minutes"
        '
        'Xl_PercentDtoTarifa
        '
        Me.Xl_PercentDtoTarifa.Location = New System.Drawing.Point(157, 348)
        Me.Xl_PercentDtoTarifa.Name = "Xl_PercentDtoTarifa"
        Me.Xl_PercentDtoTarifa.Size = New System.Drawing.Size(62, 20)
        Me.Xl_PercentDtoTarifa.TabIndex = 23
        Me.Xl_PercentDtoTarifa.Text = "0,00 %"
        Me.Xl_PercentDtoTarifa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentDtoTarifa.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(25, 351)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(114, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Descompte sobre PVP"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(225, 351)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(164, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "per tarifa client, totes les marques"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(531, 492)
        Me.TabControl1.TabIndex = 26
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LookupSermepaConfigProduccio)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Xl_LookupSermepaConfigProves)
        Me.TabPage1.Controls.Add(Me.Xl_PercentDtoTarifa)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.NumericUpDownMatschedInterval)
        Me.TabPage1.Controls.Add(Me.Xl_PercentImpago)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Xl_ContactTaller)
        Me.TabPage1.Controls.Add(Me.Xl_EurMinimImpago)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.CheckBoxMatSchedLogLevelIntensive)
        Me.TabPage1.Controls.Add(Me.Xl_ContactMgz)
        Me.TabPage1.Controls.Add(Me.TextBoxEdiversaPath)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.ComboBoxMatschedLogLevel)
        Me.TabPage1.Controls.Add(Me.Xl_BancsComboBoxXNomines)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Xl_BancsComboBoxTpv)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(523, 466)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.Xl_ContactSpvTrp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(523, 466)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Taller"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_ContactSpvTrp
        '
        Me.Xl_ContactSpvTrp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactSpvTrp.Contact = Nothing
        Me.Xl_ContactSpvTrp.Location = New System.Drawing.Point(97, 44)
        Me.Xl_ContactSpvTrp.Name = "Xl_ContactSpvTrp"
        Me.Xl_ContactSpvTrp.ReadOnly = False
        Me.Xl_ContactSpvTrp.Size = New System.Drawing.Size(419, 20)
        Me.Xl_ContactSpvTrp.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 47)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Transportista"
        '
        'Frm_EmpDefaults
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 528)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_EmpDefaults"
        Me.Text = "Configuració Empresa"
        CType(Me.NumericUpDownMatschedInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_LookupSermepaConfigProduccio As Xl_LookupSermepaConfig
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_LookupSermepaConfigProves As Xl_LookupSermepaConfig
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_PercentImpago As Xl_Percent
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_EurMinimImpago As Xl_Eur
    Friend WithEvents CheckBoxMatSchedLogLevelIntensive As CheckBox
    Friend WithEvents TextBoxEdiversaPath As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_BancsComboBoxXNomines As Xl_BancsComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Xl_BancsComboBoxTpv As Xl_BancsComboBox
    Friend WithEvents ComboBoxMatschedLogLevel As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Xl_ContactMgz As Xl_Contact2
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Xl_ContactTaller As Xl_Contact2
    Friend WithEvents NumericUpDownMatschedInterval As NumericUpDown
    Friend WithEvents Label11 As Label
    Friend WithEvents Xl_PercentDtoTarifa As Xl_Percent
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label14 As Label
    Friend WithEvents Xl_ContactSpvTrp As Xl_Contact2
End Class
