Public Class Xl_Art_Edit
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PictureBoxThumbnail As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomLlarg As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoWeb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoStk As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_EAN1 As Xl_EANOld
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomPrv As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxDscInheritFromParent As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxLastProduction As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_DropdownList_Ivas1 As Xl_DropdownList_Ivas
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoTarifa As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCodiProveedor As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AmtCurRetail As Xl_AmountCur
    Friend WithEvents Xl_AmtCurCost As Xl_AmountCur
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents LabelCost As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoPro As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonTarifaCost As System.Windows.Forms.Button
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Xl_LookupCountryMadeIn As Xl_LookupCountry
    Friend WithEvents Label15 As Label
    Friend WithEvents Xl_LookupCnap1 As Xl_LookupCnap
    Friend WithEvents Label8 As Label
    Friend WithEvents DateTimePickerHideUntil As DateTimePicker
    Friend WithEvents CheckBoxHideUntil As CheckBox
    Friend WithEvents DateTimePickerFchObsolet As DateTimePicker
    Friend WithEvents LabelObsoletFrom As Label
    Friend WithEvents CheckBoxSubstituted As CheckBox
    Friend WithEvents Xl_LookupProductSubstitute As Xl_LookupProduct
    Friend WithEvents Label10 As Label
    Friend WithEvents NumericUpDownSecurityStock As NumericUpDown
    Friend WithEvents ButtonNomLlarg As Button
    Friend WithEvents ButtonNomCurt As Button
    Friend WithEvents TextBoxNomCurt As TextBox
    Friend WithEvents CheckBoxAvailability As CheckBox
    Friend WithEvents DateTimePickerAvailability As DateTimePicker
    Friend WithEvents DateTimePickerObsoletoConfirmed As DateTimePicker
    Friend WithEvents CheckBoxObsoletoConfirmed As CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DtoEan1 As DTO.DTOEan = New DTO.DTOEan()
        Me.PictureBoxThumbnail = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNomLlarg = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoStk = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoWeb = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxNomPrv = New System.Windows.Forms.TextBox()
        Me.CheckBoxDscInheritFromParent = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLastProduction = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxNoTarifa = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCodiProveedor = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabelCost = New System.Windows.Forms.Label()
        Me.CheckBoxNoPro = New System.Windows.Forms.CheckBox()
        Me.ButtonTarifaCost = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DateTimePickerHideUntil = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxHideUntil = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFchObsolet = New System.Windows.Forms.DateTimePicker()
        Me.LabelObsoletFrom = New System.Windows.Forms.Label()
        Me.CheckBoxSubstituted = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDownSecurityStock = New System.Windows.Forms.NumericUpDown()
        Me.ButtonNomLlarg = New System.Windows.Forms.Button()
        Me.ButtonNomCurt = New System.Windows.Forms.Button()
        Me.TextBoxNomCurt = New System.Windows.Forms.TextBox()
        Me.CheckBoxAvailability = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerAvailability = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerObsoletoConfirmed = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxObsoletoConfirmed = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupProductSubstitute = New Mat.Net.Xl_LookupProduct()
        Me.Xl_LookupCnap1 = New Mat.Net.Xl_LookupCnap()
        Me.Xl_LookupCountryMadeIn = New Mat.Net.Xl_LookupCountry()
        Me.Xl_LookupProduct1 = New Mat.Net.Xl_LookupProduct()
        Me.Xl_AmtCurRetail = New Mat.Net.Xl_AmountCur()
        Me.Xl_AmtCurCost = New Mat.Net.Xl_AmountCur()
        Me.Xl_DropdownList_Ivas1 = New Mat.Net.Xl_DropdownList_Ivas()
        Me.Xl_EAN1 = New Mat.Net.Xl_EANOld()
        Me.Xl_Image1 = New Mat.Net.Xl_Image()
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownSecurityStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxThumbnail
        '
        Me.PictureBoxThumbnail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxThumbnail.Location = New System.Drawing.Point(730, 8)
        Me.PictureBoxThumbnail.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.PictureBoxThumbnail.Name = "PictureBoxThumbnail"
        Me.PictureBoxThumbnail.Size = New System.Drawing.Size(48, 48)
        Me.PictureBoxThumbnail.TabIndex = 29
        Me.PictureBoxThumbnail.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Classif.:"
        '
        'TextBoxNomLlarg
        '
        Me.TextBoxNomLlarg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomLlarg.Location = New System.Drawing.Point(64, 51)
        Me.TextBoxNomLlarg.MaxLength = 60
        Me.TextBoxNomLlarg.Name = "TextBoxNomLlarg"
        Me.TextBoxNomLlarg.ReadOnly = True
        Me.TextBoxNomLlarg.Size = New System.Drawing.Size(266, 20)
        Me.TextBoxNomLlarg.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Nom curt:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 16)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Nom:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 240)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "C.Barres:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(3, 359)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(62, 16)
        Me.CheckBoxObsoleto.TabIndex = 50
        Me.CheckBoxObsoleto.Text = "Obsolet"
        '
        'CheckBoxNoStk
        '
        Me.CheckBoxNoStk.Location = New System.Drawing.Point(270, 185)
        Me.CheckBoxNoStk.Name = "CheckBoxNoStk"
        Me.CheckBoxNoStk.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoStk.TabIndex = 51
        Me.CheckBoxNoStk.Text = "No Picking"
        '
        'CheckBoxNoWeb
        '
        Me.CheckBoxNoWeb.Location = New System.Drawing.Point(270, 202)
        Me.CheckBoxNoWeb.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoWeb.Name = "CheckBoxNoWeb"
        Me.CheckBoxNoWeb.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoWeb.TabIndex = 53
        Me.CheckBoxNoWeb.Text = "No web"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Nom prov:"
        '
        'TextBoxNomPrv
        '
        Me.TextBoxNomPrv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomPrv.Location = New System.Drawing.Point(64, 96)
        Me.TextBoxNomPrv.MaxLength = 60
        Me.TextBoxNomPrv.Name = "TextBoxNomPrv"
        Me.TextBoxNomPrv.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomPrv.TabIndex = 59
        '
        'CheckBoxDscInheritFromParent
        '
        Me.CheckBoxDscInheritFromParent.AutoSize = True
        Me.CheckBoxDscInheritFromParent.Location = New System.Drawing.Point(5, 145)
        Me.CheckBoxDscInheritFromParent.Name = "CheckBoxDscInheritFromParent"
        Me.CheckBoxDscInheritFromParent.Size = New System.Drawing.Size(224, 17)
        Me.CheckBoxDscInheritFromParent.TabIndex = 71
        Me.CheckBoxDscInheritFromParent.Text = "Hereda descripcions de la seva categoría"
        Me.CheckBoxDscInheritFromParent.UseVisualStyleBackColor = True
        '
        'CheckBoxLastProduction
        '
        Me.CheckBoxLastProduction.Location = New System.Drawing.Point(270, 168)
        Me.CheckBoxLastProduction.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxLastProduction.Name = "CheckBoxLastProduction"
        Me.CheckBoxLastProduction.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxLastProduction.TabIndex = 75
        Me.CheckBoxLastProduction.Text = "Ultimes unitats"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(0, 215)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "IVA:"
        '
        'CheckBoxNoTarifa
        '
        Me.CheckBoxNoTarifa.Location = New System.Drawing.Point(270, 233)
        Me.CheckBoxNoTarifa.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoTarifa.Name = "CheckBoxNoTarifa"
        Me.CheckBoxNoTarifa.Size = New System.Drawing.Size(91, 16)
        Me.CheckBoxNoTarifa.TabIndex = 106
        Me.CheckBoxNoTarifa.Text = "no tarifa"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(0, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 16)
        Me.Label7.TabIndex = 113
        Me.Label7.Text = "Codi prov.:"
        '
        'TextBoxCodiProveedor
        '
        Me.TextBoxCodiProveedor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCodiProveedor.Location = New System.Drawing.Point(64, 74)
        Me.TextBoxCodiProveedor.MaxLength = 20
        Me.TextBoxCodiProveedor.Name = "TextBoxCodiProveedor"
        Me.TextBoxCodiProveedor.Size = New System.Drawing.Size(154, 20)
        Me.TextBoxCodiProveedor.TabIndex = 112
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 192)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 16)
        Me.Label9.TabIndex = 121
        Me.Label9.Text = "Public:"
        '
        'LabelCost
        '
        Me.LabelCost.Location = New System.Drawing.Point(0, 169)
        Me.LabelCost.Name = "LabelCost"
        Me.LabelCost.Size = New System.Drawing.Size(64, 16)
        Me.LabelCost.TabIndex = 118
        Me.LabelCost.Text = "Cost:"
        '
        'CheckBoxNoPro
        '
        Me.CheckBoxNoPro.Location = New System.Drawing.Point(270, 218)
        Me.CheckBoxNoPro.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoPro.Name = "CheckBoxNoPro"
        Me.CheckBoxNoPro.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoPro.TabIndex = 126
        Me.CheckBoxNoPro.Text = "No Pro"
        '
        'ButtonTarifaCost
        '
        Me.ButtonTarifaCost.Location = New System.Drawing.Point(219, 168)
        Me.ButtonTarifaCost.Name = "ButtonTarifaCost"
        Me.ButtonTarifaCost.Size = New System.Drawing.Size(28, 20)
        Me.ButtonTarifaCost.TabIndex = 128
        Me.ButtonTarifaCost.Text = "..."
        Me.ButtonTarifaCost.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(0, 263)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 16)
        Me.Label15.TabIndex = 130
        Me.Label15.Text = "Made In:"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 16)
        Me.Label8.TabIndex = 133
        Me.Label8.Text = "Cnap:"
        '
        'DateTimePickerHideUntil
        '
        Me.DateTimePickerHideUntil.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerHideUntil.Location = New System.Drawing.Point(97, 336)
        Me.DateTimePickerHideUntil.Name = "DateTimePickerHideUntil"
        Me.DateTimePickerHideUntil.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerHideUntil.TabIndex = 135
        Me.DateTimePickerHideUntil.Visible = False
        '
        'CheckBoxHideUntil
        '
        Me.CheckBoxHideUntil.AutoSize = True
        Me.CheckBoxHideUntil.Location = New System.Drawing.Point(3, 338)
        Me.CheckBoxHideUntil.Name = "CheckBoxHideUntil"
        Me.CheckBoxHideUntil.Size = New System.Drawing.Size(79, 17)
        Me.CheckBoxHideUntil.TabIndex = 136
        Me.CheckBoxHideUntil.Text = "Ocultar fins"
        Me.CheckBoxHideUntil.UseVisualStyleBackColor = True
        '
        'DateTimePickerFchObsolet
        '
        Me.DateTimePickerFchObsolet.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchObsolet.Location = New System.Drawing.Point(97, 358)
        Me.DateTimePickerFchObsolet.Name = "DateTimePickerFchObsolet"
        Me.DateTimePickerFchObsolet.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFchObsolet.TabIndex = 138
        Me.DateTimePickerFchObsolet.Visible = False
        '
        'LabelObsoletFrom
        '
        Me.LabelObsoletFrom.Location = New System.Drawing.Point(57, 360)
        Me.LabelObsoletFrom.Name = "LabelObsoletFrom"
        Me.LabelObsoletFrom.Size = New System.Drawing.Size(44, 16)
        Me.LabelObsoletFrom.TabIndex = 139
        Me.LabelObsoletFrom.Text = "des de"
        Me.LabelObsoletFrom.Visible = False
        '
        'CheckBoxSubstituted
        '
        Me.CheckBoxSubstituted.Location = New System.Drawing.Point(3, 382)
        Me.CheckBoxSubstituted.Name = "CheckBoxSubstituted"
        Me.CheckBoxSubstituted.Size = New System.Drawing.Size(82, 16)
        Me.CheckBoxSubstituted.TabIndex = 140
        Me.CheckBoxSubstituted.Text = "Sustituit per"
        Me.CheckBoxSubstituted.Visible = False
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(0, 286)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 16)
        Me.Label10.TabIndex = 142
        Me.Label10.Text = "Stk.Seg.:"
        '
        'NumericUpDownSecurityStock
        '
        Me.NumericUpDownSecurityStock.Location = New System.Drawing.Point(64, 284)
        Me.NumericUpDownSecurityStock.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.NumericUpDownSecurityStock.Name = "NumericUpDownSecurityStock"
        Me.NumericUpDownSecurityStock.Size = New System.Drawing.Size(97, 20)
        Me.NumericUpDownSecurityStock.TabIndex = 132
        Me.NumericUpDownSecurityStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonNomLlarg
        '
        Me.ButtonNomLlarg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNomLlarg.Location = New System.Drawing.Point(336, 52)
        Me.ButtonNomLlarg.Name = "ButtonNomLlarg"
        Me.ButtonNomLlarg.Size = New System.Drawing.Size(30, 20)
        Me.ButtonNomLlarg.TabIndex = 143
        Me.ButtonNomLlarg.Text = "..."
        Me.ButtonNomLlarg.UseVisualStyleBackColor = True
        '
        'ButtonNomCurt
        '
        Me.ButtonNomCurt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNomCurt.Location = New System.Drawing.Point(336, 29)
        Me.ButtonNomCurt.Name = "ButtonNomCurt"
        Me.ButtonNomCurt.Size = New System.Drawing.Size(30, 20)
        Me.ButtonNomCurt.TabIndex = 145
        Me.ButtonNomCurt.Text = "..."
        Me.ButtonNomCurt.UseVisualStyleBackColor = True
        '
        'TextBoxNomCurt
        '
        Me.TextBoxNomCurt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomCurt.Location = New System.Drawing.Point(64, 28)
        Me.TextBoxNomCurt.MaxLength = 60
        Me.TextBoxNomCurt.Name = "TextBoxNomCurt"
        Me.TextBoxNomCurt.ReadOnly = True
        Me.TextBoxNomCurt.Size = New System.Drawing.Size(266, 20)
        Me.TextBoxNomCurt.TabIndex = 144
        '
        'CheckBoxAvailability
        '
        Me.CheckBoxAvailability.AutoSize = True
        Me.CheckBoxAvailability.Location = New System.Drawing.Point(3, 316)
        Me.CheckBoxAvailability.Name = "CheckBoxAvailability"
        Me.CheckBoxAvailability.Size = New System.Drawing.Size(91, 17)
        Me.CheckBoxAvailability.TabIndex = 147
        Me.CheckBoxAvailability.Text = "Disponibilidad"
        Me.CheckBoxAvailability.UseVisualStyleBackColor = True
        '
        'DateTimePickerAvailability
        '
        Me.DateTimePickerAvailability.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAvailability.Location = New System.Drawing.Point(97, 313)
        Me.DateTimePickerAvailability.Name = "DateTimePickerAvailability"
        Me.DateTimePickerAvailability.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerAvailability.TabIndex = 146
        Me.DateTimePickerAvailability.Visible = False
        '
        'DateTimePickerObsoletoConfirmed
        '
        Me.DateTimePickerObsoletoConfirmed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerObsoletoConfirmed.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerObsoletoConfirmed.Location = New System.Drawing.Point(269, 358)
        Me.DateTimePickerObsoletoConfirmed.Name = "DateTimePickerObsoletoConfirmed"
        Me.DateTimePickerObsoletoConfirmed.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerObsoletoConfirmed.TabIndex = 149
        Me.DateTimePickerObsoletoConfirmed.Visible = False
        '
        'CheckBoxObsoletoConfirmed
        '
        Me.CheckBoxObsoletoConfirmed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoletoConfirmed.Location = New System.Drawing.Point(199, 360)
        Me.CheckBoxObsoletoConfirmed.Name = "CheckBoxObsoletoConfirmed"
        Me.CheckBoxObsoletoConfirmed.Size = New System.Drawing.Size(79, 16)
        Me.CheckBoxObsoletoConfirmed.TabIndex = 148
        Me.CheckBoxObsoletoConfirmed.Text = "Confirmat"
        Me.CheckBoxObsoletoConfirmed.Visible = False
        '
        'Xl_LookupProductSubstitute
        '
        Me.Xl_LookupProductSubstitute.IsDirty = False
        Me.Xl_LookupProductSubstitute.Location = New System.Drawing.Point(97, 380)
        Me.Xl_LookupProductSubstitute.Name = "Xl_LookupProductSubstitute"
        Me.Xl_LookupProductSubstitute.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProductSubstitute.ReadOnlyLookup = False
        Me.Xl_LookupProductSubstitute.Size = New System.Drawing.Size(268, 20)
        Me.Xl_LookupProductSubstitute.TabIndex = 141
        Me.Xl_LookupProductSubstitute.Value = Nothing
        Me.Xl_LookupProductSubstitute.Visible = False
        '
        'Xl_LookupCnap1
        '
        Me.Xl_LookupCnap1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupCnap1.IsDirty = False
        Me.Xl_LookupCnap1.Location = New System.Drawing.Point(65, 118)
        Me.Xl_LookupCnap1.Name = "Xl_LookupCnap1"
        Me.Xl_LookupCnap1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCnap1.ReadOnlyLookup = False
        Me.Xl_LookupCnap1.Size = New System.Drawing.Size(300, 20)
        Me.Xl_LookupCnap1.TabIndex = 132
        Me.Xl_LookupCnap1.Value = Nothing
        '
        'Xl_LookupCountryMadeIn
        '
        Me.Xl_LookupCountryMadeIn.Country = Nothing
        Me.Xl_LookupCountryMadeIn.IsDirty = False
        Me.Xl_LookupCountryMadeIn.Location = New System.Drawing.Point(64, 261)
        Me.Xl_LookupCountryMadeIn.Name = "Xl_LookupCountryMadeIn"
        Me.Xl_LookupCountryMadeIn.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCountryMadeIn.ReadOnlyLookup = False
        Me.Xl_LookupCountryMadeIn.Size = New System.Drawing.Size(183, 20)
        Me.Xl_LookupCountryMadeIn.TabIndex = 131
        Me.Xl_LookupCountryMadeIn.Value = Nothing
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(64, 5)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(302, 20)
        Me.Xl_LookupProduct1.TabIndex = 129
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_AmtCurRetail
        '
        Me.Xl_AmtCurRetail.Amt = Nothing
        Me.Xl_AmtCurRetail.Enabled = False
        Me.Xl_AmtCurRetail.Location = New System.Drawing.Point(64, 191)
        Me.Xl_AmtCurRetail.Margin = New System.Windows.Forms.Padding(1, 2, 3, 3)
        Me.Xl_AmtCurRetail.Name = "Xl_AmtCurRetail"
        Me.Xl_AmtCurRetail.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurRetail.TabIndex = 117
        '
        'Xl_AmtCurCost
        '
        Me.Xl_AmtCurCost.Amt = Nothing
        Me.Xl_AmtCurCost.Enabled = False
        Me.Xl_AmtCurCost.Location = New System.Drawing.Point(64, 168)
        Me.Xl_AmtCurCost.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_AmtCurCost.Name = "Xl_AmtCurCost"
        Me.Xl_AmtCurCost.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurCost.TabIndex = 114
        '
        'Xl_DropdownList_Ivas1
        '
        Me.Xl_DropdownList_Ivas1.IvaCod = DTO.DTOTax.Codis.iva_Standard
        Me.Xl_DropdownList_Ivas1.Location = New System.Drawing.Point(64, 214)
        Me.Xl_DropdownList_Ivas1.Name = "Xl_DropdownList_Ivas1"
        Me.Xl_DropdownList_Ivas1.Size = New System.Drawing.Size(97, 21)
        Me.Xl_DropdownList_Ivas1.TabIndex = 102
        '
        'Xl_EAN1
        '
        DtoEan1.Value = ""
        Me.Xl_EAN1.Ean13 = DtoEan1
        Me.Xl_EAN1.Location = New System.Drawing.Point(64, 238)
        Me.Xl_EAN1.Name = "Xl_EAN1"
        Me.Xl_EAN1.Size = New System.Drawing.Size(117, 20)
        Me.Xl_EAN1.TabIndex = 58
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(379, 0)
        Me.Xl_Image1.Margin = New System.Windows.Forms.Padding(2, 3, 0, 3)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(350, 400)
        Me.Xl_Image1.TabIndex = 49
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_Art_Edit
        '
        Me.Controls.Add(Me.DateTimePickerObsoletoConfirmed)
        Me.Controls.Add(Me.DateTimePickerFchObsolet)
        Me.Controls.Add(Me.CheckBoxAvailability)
        Me.Controls.Add(Me.DateTimePickerAvailability)
        Me.Controls.Add(Me.ButtonNomCurt)
        Me.Controls.Add(Me.TextBoxNomCurt)
        Me.Controls.Add(Me.ButtonNomLlarg)
        Me.Controls.Add(Me.NumericUpDownSecurityStock)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_LookupProductSubstitute)
        Me.Controls.Add(Me.CheckBoxSubstituted)
        Me.Controls.Add(Me.LabelObsoletFrom)
        Me.Controls.Add(Me.CheckBoxHideUntil)
        Me.Controls.Add(Me.DateTimePickerHideUntil)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Xl_LookupCnap1)
        Me.Controls.Add(Me.Xl_LookupCountryMadeIn)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.CheckBoxDscInheritFromParent)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.ButtonTarifaCost)
        Me.Controls.Add(Me.CheckBoxNoPro)
        Me.Controls.Add(Me.Xl_AmtCurRetail)
        Me.Controls.Add(Me.Xl_AmtCurCost)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LabelCost)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCodiProveedor)
        Me.Controls.Add(Me.CheckBoxNoTarifa)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_DropdownList_Ivas1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxNomPrv)
        Me.Controls.Add(Me.Xl_EAN1)
        Me.Controls.Add(Me.CheckBoxNoWeb)
        Me.Controls.Add(Me.CheckBoxNoStk)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNomLlarg)
        Me.Controls.Add(Me.PictureBoxThumbnail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxLastProduction)
        Me.Controls.Add(Me.CheckBoxObsoletoConfirmed)
        Me.Name = "Xl_Art_Edit"
        Me.Size = New System.Drawing.Size(730, 403)
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownSecurityStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mAllowEvents As Boolean
    Private _Sku As DTOProductSku
    Property Dirty As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToRefreshLangTexts(sender As Object, e As MatEventArgs)
    Public Event AfterImageUpdate(sender As Object, e As MatEventArgs)
    Public Event AfterImageDelete()
    Public Event ForzarInnerPackChanged(ByVal BlForzar As Boolean, ByVal iInnerPack As Integer)
    Public Event DscInheritanceChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AvailabilityChanged(sender As Object, e As MatEventArgs)

    Public Shadows Async Function Load(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of Boolean)
        If oSku IsNot Nothing Then
            _Sku = oSku
            With oSku
                Dim oProducts As New List(Of DTOProduct)
                If .Category IsNot Nothing Then oProducts.Add(.Category)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectCategory, CustomLookup:=True)
                'CheckBoxVirtual.Checked = .Virtual

                Xl_Image1.Load(.Image, 700, 800, DTOProductSku.FullNom(oSku))
                RefrescaLangTexts()
                TextBoxNomPrv.Text = .NomProveidor
                TextBoxCodiProveedor.Text = .RefProveidor

                Xl_LookupCnap1.Load(.Cnap, .Category.Cnap)

                CheckBoxDscInheritFromParent.Checked = .Hereda

                If .Ean13 IsNot Nothing Then
                    Xl_EAN1.Text = .Ean13.Value
                End If

                Dim oCostItem = Await FEB.PriceListItemSupplier.FromSku(exs, oSku)
                If oCostItem IsNot Nothing Then
                    Xl_AmtCurCost.Amt = DTOPriceListItem_Supplier.CostNet(oCostItem)
                End If

                Xl_AmtCurRetail.Amt = .Rrpp

                Select Case Current.Session.User.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin
                    Case Else
                        LabelCost.Visible = False
                        Xl_AmtCurCost.Visible = False
                End Select

                SetHereda()
                CheckBoxLastProduction.Checked = .LastProduction
                CheckBoxNoStk.Checked = .NoStk
                CheckBoxNoWeb.Checked = .NoWeb
                CheckBoxNoPro.Checked = .NoPro
                CheckBoxNoTarifa.Checked = .NoTarifa
                Xl_DropdownList_Ivas1.IvaCod = .IvaCod
                Xl_LookupCountryMadeIn.Country = .MadeIn
                NumericUpDownSecurityStock.Value = .SecurityStock

                If .HideUntil <> Nothing Then
                    CheckBoxHideUntil.Checked = True
                    DateTimePickerHideUntil.Visible = True
                    DateTimePickerHideUntil.Value = .HideUntil
                End If

                If .Availability <> Nothing Then
                    CheckBoxAvailability.Checked = True
                    DateTimePickerAvailability.Visible = True
                    DateTimePickerAvailability.Value = .Availability
                End If

                CheckBoxObsoleto.Checked = .obsoleto
                If .FchObsoleto > DateTimePickerFchObsolet.MinDate Then
                    DateTimePickerFchObsolet.Value = .FchObsoleto
                    If .ObsoletoConfirmed > DateTimePickerFchObsolet.MinDate Then
                        CheckBoxObsoletoConfirmed.Checked = True
                        DateTimePickerObsoletoConfirmed.Value = .ObsoletoConfirmed
                    End If
                End If
                CheckBoxSubstituted.Checked = (.Substitute IsNot Nothing)
                Dim oProducts2 As New List(Of DTOProduct)
                If .Substitute IsNot Nothing Then oProducts2.Add(.Substitute)
                Xl_LookupProductSubstitute.Load(oProducts, DTOProduct.SelectionModes.SelectSku)
            End With

            mAllowEvents = True
        End If
        Return exs.Count = 0
    End Function

    Private Sub RefrescaLangTexts()
        TextBoxNomCurt.Text = _Sku.Nom.Tradueix(Current.Session.Lang)
        TextBoxNomLlarg.Text = _Sku.NomLlarg.Tradueix(Current.Session.Lang)
        RaiseEvent RequestToRefreshLangTexts(Me, New MatEventArgs(_Sku))
    End Sub

    Public Shadows Sub Update(ByRef oSku As DTOProductSku)
        With oSku
            .Category = Xl_LookupProduct1.Product
            .NomProveidor = TextBoxNomPrv.Text
            .RefProveidor = TextBoxCodiProveedor.Text
            '.Virtual = CheckBoxVirtual.Checked
            .Cnap = Xl_LookupCnap1.Cnap
            .Hereda = CheckBoxDscInheritFromParent.Checked
            .Ean13 = DTOEan.Factory(Xl_EAN1.Text)
            .LastProduction = CheckBoxLastProduction.Checked
            .NoStk = CheckBoxNoStk.Checked
            .NoWeb = CheckBoxNoWeb.Checked
            .NoPro = CheckBoxNoPro.Checked
            .NoTarifa = CheckBoxNoTarifa.Checked
            .MadeIn = Xl_LookupCountryMadeIn.Country
            .SecurityStock = NumericUpDownSecurityStock.Value
            .IvaCod = Xl_DropdownList_Ivas1.IvaCod ' IIf(CheckBoxIVAred.Checked, Art.IVAcods.reduit, Art.IVAcods.standard)
            .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .HideUntil = If(CheckBoxHideUntil.Checked, DateTimePickerHideUntil.Value, Nothing)
            .Availability = If(CheckBoxAvailability.Checked, DateTimePickerAvailability.Value, Nothing)

            .obsoleto = CheckBoxObsoleto.Checked
            If CheckBoxObsoleto.Checked Then
                .FchObsoleto = DateTimePickerFchObsolet.Value
                If CheckBoxSubstituted.Checked Then
                    .Substitute = Xl_LookupProductSubstitute.Product
                Else
                    .Substitute = Nothing
                End If
                If CheckBoxObsoletoConfirmed.Checked Then
                    .ObsoletoConfirmed = DateTimePickerObsoletoConfirmed.Value
                Else
                    .ObsoletoConfirmed = Nothing
                End If
            Else
                .FchObsoleto = Nothing
                .Substitute = Nothing
                .ObsoletoConfirmed = Nothing
            End If

        End With
    End Sub

    Public Sub SetHereda()
    End Sub


    Private Sub TextBoxNomCurt_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        Select Case e.KeyChar
            Case "&", ":", "/", "?", "'", "."
                e.KeyChar = "-"
        End Select
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNomPrv.TextChanged,
        TextBoxCodiProveedor.TextChanged,
        CheckBoxLastProduction.CheckedChanged,
        CheckBoxNoStk.CheckedChanged,
        CheckBoxNoWeb.CheckedChanged,
        CheckBoxNoPro.CheckedChanged,
        CheckBoxNoTarifa.CheckedChanged,
        Xl_LookupCnap1.AfterUpdate,
        Xl_LookupCountryMadeIn.AfterUpdate,
        NumericUpDownSecurityStock.ValueChanged,
        DateTimePickerHideUntil.ValueChanged,
        DateTimePickerFchObsolet.ValueChanged,
          DateTimePickerObsoletoConfirmed.ValueChanged,
        Xl_LookupProductSubstitute.AfterUpdate

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxObsoleto_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxObsoleto.CheckedChanged
        LabelObsoletFrom.Visible = CheckBoxObsoleto.Checked
        If CheckBoxObsoleto.Checked Then DateTimePickerFchObsolet.Value = DTO.GlobalVariables.Now()
        DateTimePickerFchObsolet.Visible = CheckBoxObsoleto.Checked
        CheckBoxObsoletoConfirmed.Visible = CheckBoxObsoleto.Checked
        DateTimePickerObsoletoConfirmed.Visible = CheckBoxObsoletoConfirmed.Checked
        CheckBoxSubstituted.Visible = CheckBoxObsoleto.Checked
        Xl_LookupProductSubstitute.Visible = CheckBoxSubstituted.Checked
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxObsoletoConfirmed_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxObsoletoConfirmed.CheckedChanged
        DateTimePickerObsoletoConfirmed.Visible = CheckBoxObsoletoConfirmed.Checked
        If CheckBoxObsoletoConfirmed.Checked Then DateTimePickerObsoletoConfirmed.Value = DTO.GlobalVariables.Now()
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxSubstituted_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSubstituted.CheckedChanged
        Xl_LookupProductSubstitute.Visible = CheckBoxSubstituted.Checked
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub


    Private Sub Xl_Image1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        RaiseEvent AfterImageUpdate(Me, e)
        SetDirty()
    End Sub

    Private Sub CheckBoxHeredaDsc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDscInheritFromParent.CheckedChanged
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub ControlAfterUpdateValue(ByVal oAmt As DTOAmt)
        SetDirty()
    End Sub

    Private Sub ControlAfterUpdateCur(ByVal oCur As DTOCur)
        SetDirty()
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            _Dirty = True
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_EAN1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_EAN1.Changed
        SetDirty()
    End Sub


    Private Sub Xl_DropdownList_Ivas1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_DropdownList_Ivas1.AfterUpdate
        SetDirty()
    End Sub



    Private Sub RefreshTarifa()

    End Sub

    Private Sub Xl_AmtCurRetail_MouseDown(sender As Object, e As MouseEventArgs) Handles Xl_AmtCurRetail.MouseDown
        MsgBox("Xl_AmtCurRetail_MouseDown event")
    End Sub

    Private Sub CheckBoxHideUntil_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHideUntil.CheckedChanged
        If mAllowEvents Then
            DateTimePickerHideUntil.Visible = CheckBoxHideUntil.Checked
            SetDirty()
        End If
    End Sub

    Private Async Sub Xl_LookupProduct1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.RequestToLookup
        Dim exs As New List(Of Exception)
        Dim oCatalog = Await FEB.ProductCategories.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang)
        Dim oBrand = oCatalog.FirstOrDefault(Function(x) x.Equals(_Sku.Category.Brand))
        Dim oSingleBrandCatalog = {oBrand}.ToList

        'Dim oFrm As New Frm_ProductCategories(DTOProduct.SelectionModes.SelectCategory, _Sku.Category, oCatalog:=oSingleBrandCatalog)
        Dim oFrm As New Frm_ProductCategories(DTOProduct.SelectionModes.SelectCategory, _Sku.Category, oCatalog:=oCatalog)
        AddHandler oFrm.OnItemSelected, AddressOf onCategorySelected
        oFrm.Show()
    End Sub

    Private Sub onCategorySelected(sender As Object, e As MatEventArgs)
        Dim oProducts As New List(Of DTOProduct)
        If e.Argument IsNot Nothing Then oProducts.Add(e.Argument)
        Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectCategory, True)
        DirectCast(sender, Form).Close()
        SetDirty()
    End Sub

    Private Sub ButtonShowLangTexts_Click(sender As Object, e As EventArgs) Handles ButtonNomCurt.Click, ButtonNomLlarg.Click
        Dim oFrm As New Frm_ProductDescription(_Sku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaLangTexts
        oFrm.Show()
    End Sub

    Private Sub CheckBoxAvailability_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAvailability.CheckedChanged
        If mAllowEvents Then
            DateTimePickerAvailability.Visible = CheckBoxAvailability.Checked
            Dim availability = If(CheckBoxAvailability.Checked, DateTimePickerAvailability.Value, Nothing)
            RaiseEvent AvailabilityChanged(Me, New MatEventArgs(availability))
            SetDirty()
        End If

    End Sub

    Private Sub DateTimePickerAvailability_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerAvailability.ValueChanged
        If mAllowEvents Then
            Dim availability = If(CheckBoxAvailability.Checked, DateTimePickerAvailability.Value, Nothing)
            RaiseEvent AvailabilityChanged(Me, New MatEventArgs(availability))
            SetDirty()
        End If
    End Sub

End Class
