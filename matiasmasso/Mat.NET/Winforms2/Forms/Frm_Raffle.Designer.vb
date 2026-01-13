<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Raffle
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_LookupCountry1 = New Mat.Net.Xl_LookupCountry()
        Me.Xl_Langs1 = New Mat.Net.Xl_Langs()
        Me.CheckBoxVisible = New System.Windows.Forms.CheckBox()
        Me.NumericUpDownSuplents = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DateTimePickerHHmmTo = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_AmtCostPubli = New Mat.Net.Xl_Amt()
        Me.Xl_AmtCostPrize = New Mat.Net.Xl_Amt()
        Me.DateTimePickerDelivery = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxDelivery = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerPicture = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxPicture = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerDistributorReaction = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxDistributorReaction = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerWinnerReaction = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxWinnerReaction = New System.Windows.Forms.CheckBox()
        Me.TextBoxUrlExterna = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_LookupProduct1 = New Mat.Net.Xl_LookupProduct()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_ImageCallToAction = New Mat.Net.Xl_Image()
        Me.Xl_ImageBanner = New Mat.Net.Xl_Image()
        Me.Xl_ImageFeatured = New Mat.Net.Xl_Image()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ComboBoxRightAnswer = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxAnswers = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxQuestion = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TextBoxBases = New System.Windows.Forms.TextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ButtonWinner = New System.Windows.Forms.Button()
        Me.TextBoxWinner = New System.Windows.Forms.TextBox()
        Me.Xl_ImageWinner = New Mat.Net.Xl_Image()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearchParticipants = New Mat.Net.Xl_TextboxSearch()
        Me.Xl_RaffleParticipants1 = New Mat.Net.Xl_RaffleParticipants2()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearchStoreLocator = New Mat.Net.Xl_TextboxSearch()
        Me.Xl_RaffleStoreLocator1 = New Mat.Net.Xl_RaffleStoreLocator()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.NumericUpDownSuplents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.Xl_RaffleParticipants1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.Xl_RaffleStoreLocator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 550)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(435, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(216, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(327, 4)
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(435, 527)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.Xl_LookupCountry1)
        Me.TabPage1.Controls.Add(Me.Xl_Langs1)
        Me.TabPage1.Controls.Add(Me.CheckBoxVisible)
        Me.TabPage1.Controls.Add(Me.NumericUpDownSuplents)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.DateTimePickerHHmmTo)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Xl_AmtCostPubli)
        Me.TabPage1.Controls.Add(Me.Xl_AmtCostPrize)
        Me.TabPage1.Controls.Add(Me.DateTimePickerDelivery)
        Me.TabPage1.Controls.Add(Me.CheckBoxDelivery)
        Me.TabPage1.Controls.Add(Me.DateTimePickerPicture)
        Me.TabPage1.Controls.Add(Me.CheckBoxPicture)
        Me.TabPage1.Controls.Add(Me.DateTimePickerDistributorReaction)
        Me.TabPage1.Controls.Add(Me.CheckBoxDistributorReaction)
        Me.TabPage1.Controls.Add(Me.DateTimePickerWinnerReaction)
        Me.TabPage1.Controls.Add(Me.CheckBoxWinnerReaction)
        Me.TabPage1.Controls.Add(Me.TextBoxUrlExterna)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_LookupProduct1)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Xl_ImageCallToAction)
        Me.TabPage1.Controls.Add(Me.Xl_ImageBanner)
        Me.TabPage1.Controls.Add(Me.Xl_ImageFeatured)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchTo)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxTitle)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchFrom)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(427, 501)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 55)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(27, 13)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "Pais"
        '
        'Xl_LookupCountry1
        '
        Me.Xl_LookupCountry1.Country = Nothing
        Me.Xl_LookupCountry1.IsDirty = False
        Me.Xl_LookupCountry1.Location = New System.Drawing.Point(67, 51)
        Me.Xl_LookupCountry1.Name = "Xl_LookupCountry1"
        Me.Xl_LookupCountry1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCountry1.ReadOnlyLookup = False
        Me.Xl_LookupCountry1.Size = New System.Drawing.Size(301, 20)
        Me.Xl_LookupCountry1.TabIndex = 35
        Me.Xl_LookupCountry1.Value = Nothing
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Langs1.Location = New System.Drawing.Point(369, 24)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 34
        Me.Xl_Langs1.Value = Nothing
        '
        'CheckBoxVisible
        '
        Me.CheckBoxVisible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxVisible.AutoSize = True
        Me.CheckBoxVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxVisible.Location = New System.Drawing.Point(311, 102)
        Me.CheckBoxVisible.Name = "CheckBoxVisible"
        Me.CheckBoxVisible.Size = New System.Drawing.Size(110, 17)
        Me.CheckBoxVisible.TabIndex = 33
        Me.CheckBoxVisible.Text = "Visible en portada"
        Me.CheckBoxVisible.UseVisualStyleBackColor = True
        '
        'NumericUpDownSuplents
        '
        Me.NumericUpDownSuplents.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownSuplents.Location = New System.Drawing.Point(378, 215)
        Me.NumericUpDownSuplents.Name = "NumericUpDownSuplents"
        Me.NumericUpDownSuplents.Size = New System.Drawing.Size(43, 20)
        Me.NumericUpDownSuplents.TabIndex = 32
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(320, 217)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Suplents"
        '
        'DateTimePickerHHmmTo
        '
        Me.DateTimePickerHHmmTo.CustomFormat = "HH:mm"
        Me.DateTimePickerHHmmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerHHmmTo.Location = New System.Drawing.Point(179, 125)
        Me.DateTimePickerHHmmTo.Name = "DateTimePickerHHmmTo"
        Me.DateTimePickerHHmmTo.Size = New System.Drawing.Size(65, 20)
        Me.DateTimePickerHHmmTo.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 238)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 13)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "Inversió publicitat"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 219)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Valor del premi"
        '
        'Xl_AmtCostPubli
        '
        Me.Xl_AmtCostPubli.Location = New System.Drawing.Point(156, 235)
        Me.Xl_AmtCostPubli.Name = "Xl_AmtCostPubli"
        Me.Xl_AmtCostPubli.Size = New System.Drawing.Size(106, 20)
        Me.Xl_AmtCostPubli.TabIndex = 27
        Me.Xl_AmtCostPubli.Value = Nothing
        '
        'Xl_AmtCostPrize
        '
        Me.Xl_AmtCostPrize.Location = New System.Drawing.Point(156, 214)
        Me.Xl_AmtCostPrize.Name = "Xl_AmtCostPrize"
        Me.Xl_AmtCostPrize.Size = New System.Drawing.Size(106, 20)
        Me.Xl_AmtCostPrize.TabIndex = 26
        Me.Xl_AmtCostPrize.Value = Nothing
        '
        'DateTimePickerDelivery
        '
        Me.DateTimePickerDelivery.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDelivery.Location = New System.Drawing.Point(156, 319)
        Me.DateTimePickerDelivery.Name = "DateTimePickerDelivery"
        Me.DateTimePickerDelivery.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerDelivery.TabIndex = 25
        Me.DateTimePickerDelivery.Visible = False
        '
        'CheckBoxDelivery
        '
        Me.CheckBoxDelivery.AutoSize = True
        Me.CheckBoxDelivery.Location = New System.Drawing.Point(11, 322)
        Me.CheckBoxDelivery.Name = "CheckBoxDelivery"
        Me.CheckBoxDelivery.Size = New System.Drawing.Size(81, 17)
        Me.CheckBoxDelivery.TabIndex = 24
        Me.CheckBoxDelivery.Text = "Envio premi"
        Me.CheckBoxDelivery.UseVisualStyleBackColor = True
        '
        'DateTimePickerPicture
        '
        Me.DateTimePickerPicture.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerPicture.Location = New System.Drawing.Point(156, 342)
        Me.DateTimePickerPicture.Name = "DateTimePickerPicture"
        Me.DateTimePickerPicture.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerPicture.TabIndex = 23
        Me.DateTimePickerPicture.Visible = False
        '
        'CheckBoxPicture
        '
        Me.CheckBoxPicture.AutoSize = True
        Me.CheckBoxPicture.Location = New System.Drawing.Point(11, 345)
        Me.CheckBoxPicture.Name = "CheckBoxPicture"
        Me.CheckBoxPicture.Size = New System.Drawing.Size(88, 17)
        Me.CheckBoxPicture.TabIndex = 22
        Me.CheckBoxPicture.Text = "Foto penjada"
        Me.CheckBoxPicture.UseVisualStyleBackColor = True
        '
        'DateTimePickerDistributorReaction
        '
        Me.DateTimePickerDistributorReaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDistributorReaction.Location = New System.Drawing.Point(156, 296)
        Me.DateTimePickerDistributorReaction.Name = "DateTimePickerDistributorReaction"
        Me.DateTimePickerDistributorReaction.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerDistributorReaction.TabIndex = 21
        Me.DateTimePickerDistributorReaction.Visible = False
        '
        'CheckBoxDistributorReaction
        '
        Me.CheckBoxDistributorReaction.AutoSize = True
        Me.CheckBoxDistributorReaction.Location = New System.Drawing.Point(11, 299)
        Me.CheckBoxDistributorReaction.Name = "CheckBoxDistributorReaction"
        Me.CheckBoxDistributorReaction.Size = New System.Drawing.Size(124, 17)
        Me.CheckBoxDistributorReaction.TabIndex = 20
        Me.CheckBoxDistributorReaction.Text = "Resposta distribuidor"
        Me.CheckBoxDistributorReaction.UseVisualStyleBackColor = True
        '
        'DateTimePickerWinnerReaction
        '
        Me.DateTimePickerWinnerReaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerWinnerReaction.Location = New System.Drawing.Point(156, 273)
        Me.DateTimePickerWinnerReaction.Name = "DateTimePickerWinnerReaction"
        Me.DateTimePickerWinnerReaction.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerWinnerReaction.TabIndex = 19
        Me.DateTimePickerWinnerReaction.Visible = False
        '
        'CheckBoxWinnerReaction
        '
        Me.CheckBoxWinnerReaction.AutoSize = True
        Me.CheckBoxWinnerReaction.Location = New System.Drawing.Point(11, 276)
        Me.CheckBoxWinnerReaction.Name = "CheckBoxWinnerReaction"
        Me.CheckBoxWinnerReaction.Size = New System.Drawing.Size(124, 17)
        Me.CheckBoxWinnerReaction.TabIndex = 18
        Me.CheckBoxWinnerReaction.Text = "Resposta guanyador"
        Me.CheckBoxWinnerReaction.UseVisualStyleBackColor = True
        '
        'TextBoxUrlExterna
        '
        Me.TextBoxUrlExterna.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUrlExterna.Location = New System.Drawing.Point(67, 178)
        Me.TextBoxUrlExterna.Name = "TextBoxUrlExterna"
        Me.TextBoxUrlExterna.Size = New System.Drawing.Size(354, 20)
        Me.TextBoxUrlExterna.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 181)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Url Externa"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 156)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Premi"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(67, 152)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(354, 20)
        Me.Xl_LookupProduct1.TabIndex = 14
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(296, 388)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Call to action"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(153, 388)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Banner"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 388)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Featured image"
        '
        'Xl_ImageCallToAction
        '
        Me.Xl_ImageCallToAction.Bitmap = Nothing
        Me.Xl_ImageCallToAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageCallToAction.EmptyImageLabelText = ""
        Me.Xl_ImageCallToAction.IsDirty = False
        Me.Xl_ImageCallToAction.Location = New System.Drawing.Point(299, 404)
        Me.Xl_ImageCallToAction.Name = "Xl_ImageCallToAction"
        Me.Xl_ImageCallToAction.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageCallToAction.TabIndex = 8
        Me.Xl_ImageCallToAction.ZipStream = Nothing
        '
        'Xl_ImageBanner
        '
        Me.Xl_ImageBanner.Bitmap = Nothing
        Me.Xl_ImageBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageBanner.EmptyImageLabelText = ""
        Me.Xl_ImageBanner.IsDirty = False
        Me.Xl_ImageBanner.Location = New System.Drawing.Point(156, 404)
        Me.Xl_ImageBanner.Name = "Xl_ImageBanner"
        Me.Xl_ImageBanner.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageBanner.TabIndex = 7
        Me.Xl_ImageBanner.ZipStream = Nothing
        '
        'Xl_ImageFeatured
        '
        Me.Xl_ImageFeatured.Bitmap = Nothing
        Me.Xl_ImageFeatured.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageFeatured.EmptyImageLabelText = ""
        Me.Xl_ImageFeatured.IsDirty = False
        Me.Xl_ImageFeatured.Location = New System.Drawing.Point(13, 404)
        Me.Xl_ImageFeatured.Name = "Xl_ImageFeatured"
        Me.Xl_ImageFeatured.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageFeatured.TabIndex = 6
        Me.Xl_ImageFeatured.ZipStream = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Final"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(67, 125)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerFchTo.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Inici"
        '
        'TextBoxTitle
        '
        Me.TextBoxTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitle.Location = New System.Drawing.Point(67, 24)
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.TextBoxTitle.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxTitle.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Titol"
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(67, 99)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerFchFrom.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ComboBoxRightAnswer)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.TextBoxAnswers)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.TextBoxQuestion)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(427, 501)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Pregunta"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ComboBoxRightAnswer
        '
        Me.ComboBoxRightAnswer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxRightAnswer.FormattingEnabled = True
        Me.ComboBoxRightAnswer.Location = New System.Drawing.Point(0, 474)
        Me.ComboBoxRightAnswer.Name = "ComboBoxRightAnswer"
        Me.ComboBoxRightAnswer.Size = New System.Drawing.Size(424, 21)
        Me.ComboBoxRightAnswer.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 327)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "respostes (una per linia):"
        '
        'TextBoxAnswers
        '
        Me.TextBoxAnswers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAnswers.Location = New System.Drawing.Point(2, 343)
        Me.TextBoxAnswers.Multiline = True
        Me.TextBoxAnswers.Name = "TextBoxAnswers"
        Me.TextBoxAnswers.Size = New System.Drawing.Size(422, 125)
        Me.TextBoxAnswers.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "enunciat"
        '
        'TextBoxQuestion
        '
        Me.TextBoxQuestion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxQuestion.Location = New System.Drawing.Point(2, 20)
        Me.TextBoxQuestion.Multiline = True
        Me.TextBoxQuestion.Name = "TextBoxQuestion"
        Me.TextBoxQuestion.Size = New System.Drawing.Size(422, 304)
        Me.TextBoxQuestion.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBoxBases)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(427, 501)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Bases"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TextBoxBases
        '
        Me.TextBoxBases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxBases.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxBases.Multiline = True
        Me.TextBoxBases.Name = "TextBoxBases"
        Me.TextBoxBases.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxBases.Size = New System.Drawing.Size(427, 501)
        Me.TextBoxBases.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.ButtonWinner)
        Me.TabPage4.Controls.Add(Me.TextBoxWinner)
        Me.TabPage4.Controls.Add(Me.Xl_ImageWinner)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(427, 501)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Guanyador"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'ButtonWinner
        '
        Me.ButtonWinner.Location = New System.Drawing.Point(388, 16)
        Me.ButtonWinner.Name = "ButtonWinner"
        Me.ButtonWinner.Size = New System.Drawing.Size(36, 20)
        Me.ButtonWinner.TabIndex = 67
        Me.ButtonWinner.Text = "..."
        Me.ButtonWinner.UseVisualStyleBackColor = True
        '
        'TextBoxWinner
        '
        Me.TextBoxWinner.Enabled = False
        Me.TextBoxWinner.Location = New System.Drawing.Point(4, 16)
        Me.TextBoxWinner.Name = "TextBoxWinner"
        Me.TextBoxWinner.Size = New System.Drawing.Size(378, 20)
        Me.TextBoxWinner.TabIndex = 9
        '
        'Xl_ImageWinner
        '
        Me.Xl_ImageWinner.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageWinner.Bitmap = Nothing
        Me.Xl_ImageWinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageWinner.EmptyImageLabelText = ""
        Me.Xl_ImageWinner.IsDirty = False
        Me.Xl_ImageWinner.Location = New System.Drawing.Point(4, 42)
        Me.Xl_ImageWinner.Name = "Xl_ImageWinner"
        Me.Xl_ImageWinner.Size = New System.Drawing.Size(420, 295)
        Me.Xl_ImageWinner.TabIndex = 7
        Me.Xl_ImageWinner.ZipStream = Nothing
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_TextboxSearchParticipants)
        Me.TabPage5.Controls.Add(Me.Xl_RaffleParticipants1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(427, 501)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Participants"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchParticipants
        '
        Me.Xl_TextboxSearchParticipants.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchParticipants.Location = New System.Drawing.Point(274, 6)
        Me.Xl_TextboxSearchParticipants.Name = "Xl_TextboxSearchParticipants"
        Me.Xl_TextboxSearchParticipants.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearchParticipants.TabIndex = 1
        '
        'Xl_RaffleParticipants1
        '
        Me.Xl_RaffleParticipants1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RaffleParticipants1.DisplayObsolets = False
        Me.Xl_RaffleParticipants1.Filter = Nothing
        Me.Xl_RaffleParticipants1.Location = New System.Drawing.Point(3, 32)
        Me.Xl_RaffleParticipants1.MouseIsDown = False
        Me.Xl_RaffleParticipants1.Name = "Xl_RaffleParticipants1"
        Me.Xl_RaffleParticipants1.Size = New System.Drawing.Size(421, 466)
        Me.Xl_RaffleParticipants1.TabIndex = 0
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Xl_RaffleStoreLocator1)
        Me.TabPage6.Controls.Add(Me.Xl_TextboxSearchStoreLocator)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(427, 501)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Distribuidors"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchStoreLocator
        '
        Me.Xl_TextboxSearchStoreLocator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearchStoreLocator.Location = New System.Drawing.Point(269, 6)
        Me.Xl_TextboxSearchStoreLocator.Name = "Xl_TextboxSearchStoreLocator"
        Me.Xl_TextboxSearchStoreLocator.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearchStoreLocator.TabIndex = 2
        '
        'Xl_RaffleStoreLocator1
        '
        Me.Xl_RaffleStoreLocator1.AllowUserToAddRows = False
        Me.Xl_RaffleStoreLocator1.AllowUserToDeleteRows = False
        Me.Xl_RaffleStoreLocator1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RaffleStoreLocator1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RaffleStoreLocator1.DisplayObsolets = False
        Me.Xl_RaffleStoreLocator1.Filter = Nothing
        Me.Xl_RaffleStoreLocator1.Location = New System.Drawing.Point(4, 32)
        Me.Xl_RaffleStoreLocator1.MouseIsDown = False
        Me.Xl_RaffleStoreLocator1.Name = "Xl_RaffleStoreLocator1"
        Me.Xl_RaffleStoreLocator1.ReadOnly = True
        Me.Xl_RaffleStoreLocator1.Size = New System.Drawing.Size(420, 466)
        Me.Xl_RaffleStoreLocator1.TabIndex = 3
        '
        'Frm_Raffle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 581)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Raffle"
        Me.Text = "Sorteig"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.NumericUpDownSuplents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.Xl_RaffleParticipants1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.Xl_RaffleStoreLocator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFchFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxRightAnswer As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAnswers As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxQuestion As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageCallToAction As Xl_Image
    Friend WithEvents Xl_ImageBanner As Xl_Image
    Friend WithEvents Xl_ImageFeatured As Xl_Image
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxBases As System.Windows.Forms.TextBox
    Friend WithEvents Xl_ImageWinner As Xl_Image
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RaffleParticipants1 As Xl_RaffleParticipants2
    Friend WithEvents TextBoxWinner As System.Windows.Forms.TextBox
    Friend WithEvents ButtonWinner As System.Windows.Forms.Button
    Friend WithEvents TextBoxUrlExterna As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerDelivery As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxDelivery As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerPicture As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxPicture As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerDistributorReaction As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxDistributorReaction As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerWinnerReaction As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxWinnerReaction As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCostPubli As Xl_Amt
    Friend WithEvents Xl_AmtCostPrize As Xl_Amt
    Friend WithEvents DateTimePickerHHmmTo As DateTimePicker
    Friend WithEvents NumericUpDownSuplents As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents CheckBoxVisible As CheckBox
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents Label14 As Label
    Friend WithEvents Xl_LookupCountry1 As Xl_LookupCountry
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_TextboxSearchParticipants As Xl_TextboxSearch
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Xl_TextboxSearchStoreLocator As Xl_TextboxSearch
    Friend WithEvents Xl_RaffleStoreLocator1 As Xl_RaffleStoreLocator
End Class
