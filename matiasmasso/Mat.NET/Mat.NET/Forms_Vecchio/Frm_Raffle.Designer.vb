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
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_ImageCallToAction = New Mat.NET.Xl_Image()
        Me.Xl_ImageBanner = New Mat.NET.Xl_Image()
        Me.Xl_ImageFeatured = New Mat.NET.Xl_Image()
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
        Me.Xl_ImageWinner = New Mat.NET.Xl_Image()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Grf1 = New Mat.NET.Xl_Grf()
        Me.Xl_RaffleParticipants1 = New Mat.NET.Xl_RaffleParticipants()
        Me.TabPageJutges = New System.Windows.Forms.TabPage()
        Me.Xl_Jutges = New Mat.NET.Xl_Usuaris()
        Me.ButtonAddJutge = New System.Windows.Forms.Button()
        Me.Xl_Lookup_Jutge = New Mat.NET.Xl_Lookup_Usuari()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabPageJutges.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 534)
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
        Me.TabControl1.Controls.Add(Me.TabPageJutges)
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(435, 511)
        Me.TabControl1.TabIndex = 43
        '
        'TabPage1
        '
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
        Me.TabPage1.Size = New System.Drawing.Size(427, 485)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 189)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 13)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "Inversió publicitat"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 170)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Valor del premi"
        '
        'Xl_AmtCostPubli
        '
        Me.Xl_AmtCostPubli.Value = Nothing
        Me.Xl_AmtCostPubli.Location = New System.Drawing.Point(156, 186)
        Me.Xl_AmtCostPubli.Name = "Xl_AmtCostPubli"
        Me.Xl_AmtCostPubli.Size = New System.Drawing.Size(106, 20)
        Me.Xl_AmtCostPubli.TabIndex = 27
        '
        'Xl_AmtCostPrize
        '
        Me.Xl_AmtCostPrize.Value = Nothing
        Me.Xl_AmtCostPrize.Location = New System.Drawing.Point(156, 165)
        Me.Xl_AmtCostPrize.Name = "Xl_AmtCostPrize"
        Me.Xl_AmtCostPrize.Size = New System.Drawing.Size(106, 20)
        Me.Xl_AmtCostPrize.TabIndex = 26
        '
        'DateTimePickerDelivery
        '
        Me.DateTimePickerDelivery.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDelivery.Location = New System.Drawing.Point(156, 270)
        Me.DateTimePickerDelivery.Name = "DateTimePickerDelivery"
        Me.DateTimePickerDelivery.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerDelivery.TabIndex = 25
        Me.DateTimePickerDelivery.Visible = False
        '
        'CheckBoxDelivery
        '
        Me.CheckBoxDelivery.AutoSize = True
        Me.CheckBoxDelivery.Location = New System.Drawing.Point(11, 273)
        Me.CheckBoxDelivery.Name = "CheckBoxDelivery"
        Me.CheckBoxDelivery.Size = New System.Drawing.Size(81, 17)
        Me.CheckBoxDelivery.TabIndex = 24
        Me.CheckBoxDelivery.Text = "Envio premi"
        Me.CheckBoxDelivery.UseVisualStyleBackColor = True
        '
        'DateTimePickerPicture
        '
        Me.DateTimePickerPicture.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerPicture.Location = New System.Drawing.Point(156, 293)
        Me.DateTimePickerPicture.Name = "DateTimePickerPicture"
        Me.DateTimePickerPicture.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerPicture.TabIndex = 23
        Me.DateTimePickerPicture.Visible = False
        '
        'CheckBoxPicture
        '
        Me.CheckBoxPicture.AutoSize = True
        Me.CheckBoxPicture.Location = New System.Drawing.Point(11, 296)
        Me.CheckBoxPicture.Name = "CheckBoxPicture"
        Me.CheckBoxPicture.Size = New System.Drawing.Size(88, 17)
        Me.CheckBoxPicture.TabIndex = 22
        Me.CheckBoxPicture.Text = "Foto penjada"
        Me.CheckBoxPicture.UseVisualStyleBackColor = True
        '
        'DateTimePickerDistributorReaction
        '
        Me.DateTimePickerDistributorReaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDistributorReaction.Location = New System.Drawing.Point(156, 247)
        Me.DateTimePickerDistributorReaction.Name = "DateTimePickerDistributorReaction"
        Me.DateTimePickerDistributorReaction.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerDistributorReaction.TabIndex = 21
        Me.DateTimePickerDistributorReaction.Visible = False
        '
        'CheckBoxDistributorReaction
        '
        Me.CheckBoxDistributorReaction.AutoSize = True
        Me.CheckBoxDistributorReaction.Location = New System.Drawing.Point(11, 250)
        Me.CheckBoxDistributorReaction.Name = "CheckBoxDistributorReaction"
        Me.CheckBoxDistributorReaction.Size = New System.Drawing.Size(124, 17)
        Me.CheckBoxDistributorReaction.TabIndex = 20
        Me.CheckBoxDistributorReaction.Text = "Resposta distribuidor"
        Me.CheckBoxDistributorReaction.UseVisualStyleBackColor = True
        '
        'DateTimePickerWinnerReaction
        '
        Me.DateTimePickerWinnerReaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerWinnerReaction.Location = New System.Drawing.Point(156, 224)
        Me.DateTimePickerWinnerReaction.Name = "DateTimePickerWinnerReaction"
        Me.DateTimePickerWinnerReaction.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerWinnerReaction.TabIndex = 19
        Me.DateTimePickerWinnerReaction.Visible = False
        '
        'CheckBoxWinnerReaction
        '
        Me.CheckBoxWinnerReaction.AutoSize = True
        Me.CheckBoxWinnerReaction.Location = New System.Drawing.Point(11, 227)
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
        Me.TextBoxUrlExterna.Location = New System.Drawing.Point(67, 129)
        Me.TextBoxUrlExterna.Name = "TextBoxUrlExterna"
        Me.TextBoxUrlExterna.Size = New System.Drawing.Size(354, 20)
        Me.TextBoxUrlExterna.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 132)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Url Externa"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Premi"
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(67, 103)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(354, 20)
        Me.Xl_LookupProduct1.TabIndex = 14
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(296, 339)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Call to action (500px)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(153, 339)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Banner (600px)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 339)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Featured image (178x125)"
        '
        'Xl_ImageCallToAction
        '
        Me.Xl_ImageCallToAction.Bitmap = Nothing
        Me.Xl_ImageCallToAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageCallToAction.EmptyImageLabelText = ""
        Me.Xl_ImageCallToAction.IsDirty = False
        Me.Xl_ImageCallToAction.Location = New System.Drawing.Point(299, 355)
        Me.Xl_ImageCallToAction.MaxHeight = 0
        Me.Xl_ImageCallToAction.MaxWidth = 0
        Me.Xl_ImageCallToAction.Name = "Xl_ImageCallToAction"
        Me.Xl_ImageCallToAction.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageCallToAction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_ImageCallToAction.TabIndex = 8
        Me.Xl_ImageCallToAction.ZipStream = Nothing
        '
        'Xl_ImageBanner
        '
        Me.Xl_ImageBanner.Bitmap = Nothing
        Me.Xl_ImageBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageBanner.EmptyImageLabelText = ""
        Me.Xl_ImageBanner.IsDirty = False
        Me.Xl_ImageBanner.Location = New System.Drawing.Point(156, 355)
        Me.Xl_ImageBanner.MaxHeight = 0
        Me.Xl_ImageBanner.MaxWidth = 0
        Me.Xl_ImageBanner.Name = "Xl_ImageBanner"
        Me.Xl_ImageBanner.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_ImageBanner.TabIndex = 7
        Me.Xl_ImageBanner.ZipStream = Nothing
        '
        'Xl_ImageFeatured
        '
        Me.Xl_ImageFeatured.Bitmap = Nothing
        Me.Xl_ImageFeatured.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_ImageFeatured.EmptyImageLabelText = ""
        Me.Xl_ImageFeatured.IsDirty = False
        Me.Xl_ImageFeatured.Location = New System.Drawing.Point(13, 355)
        Me.Xl_ImageFeatured.MaxHeight = 0
        Me.Xl_ImageFeatured.MaxWidth = 0
        Me.Xl_ImageFeatured.Name = "Xl_ImageFeatured"
        Me.Xl_ImageFeatured.Size = New System.Drawing.Size(120, 120)
        Me.Xl_ImageFeatured.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_ImageFeatured.TabIndex = 6
        Me.Xl_ImageFeatured.ZipStream = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Final"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(67, 76)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(106, 20)
        Me.DateTimePickerFchTo.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 54)
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
        Me.TextBoxTitle.Size = New System.Drawing.Size(354, 20)
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
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(67, 50)
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
        Me.TabPage2.Size = New System.Drawing.Size(427, 485)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Pregunta"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ComboBoxRightAnswer
        '
        Me.ComboBoxRightAnswer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxRightAnswer.FormattingEnabled = True
        Me.ComboBoxRightAnswer.Location = New System.Drawing.Point(3, 352)
        Me.ComboBoxRightAnswer.Name = "ComboBoxRightAnswer"
        Me.ComboBoxRightAnswer.Size = New System.Drawing.Size(421, 21)
        Me.ComboBoxRightAnswer.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 244)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "respostes (una per linia):"
        '
        'TextBoxAnswers
        '
        Me.TextBoxAnswers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAnswers.Location = New System.Drawing.Point(2, 260)
        Me.TextBoxAnswers.Multiline = True
        Me.TextBoxAnswers.Name = "TextBoxAnswers"
        Me.TextBoxAnswers.Size = New System.Drawing.Size(422, 95)
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
        Me.TextBoxQuestion.Size = New System.Drawing.Size(422, 216)
        Me.TextBoxQuestion.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBoxBases)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(427, 485)
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
        Me.TextBoxBases.Size = New System.Drawing.Size(427, 485)
        Me.TextBoxBases.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.ButtonWinner)
        Me.TabPage4.Controls.Add(Me.TextBoxWinner)
        Me.TabPage4.Controls.Add(Me.Xl_ImageWinner)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(427, 485)
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
        Me.Xl_ImageWinner.Location = New System.Drawing.Point(3, 42)
        Me.Xl_ImageWinner.MaxHeight = 0
        Me.Xl_ImageWinner.MaxWidth = 0
        Me.Xl_ImageWinner.Name = "Xl_ImageWinner"
        Me.Xl_ImageWinner.Size = New System.Drawing.Size(421, 331)
        Me.Xl_ImageWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_ImageWinner.TabIndex = 7
        Me.Xl_ImageWinner.ZipStream = Nothing
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.SplitContainer1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(427, 485)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Participants"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Grf1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_RaffleParticipants1)
        Me.SplitContainer1.Size = New System.Drawing.Size(421, 479)
        Me.SplitContainer1.SplitterDistance = 123
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_Grf1
        '
        Me.Xl_Grf1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Grf1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Grf1.Name = "Xl_Grf1"
        Me.Xl_Grf1.Size = New System.Drawing.Size(421, 123)
        Me.Xl_Grf1.TabIndex = 1
        '
        'Xl_RaffleParticipants1
        '
        Me.Xl_RaffleParticipants1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RaffleParticipants1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RaffleParticipants1.Name = "Xl_RaffleParticipants1"
        Me.Xl_RaffleParticipants1.Size = New System.Drawing.Size(421, 352)
        Me.Xl_RaffleParticipants1.TabIndex = 0
        '
        'TabPageJutges
        '
        Me.TabPageJutges.Controls.Add(Me.Xl_Jutges)
        Me.TabPageJutges.Controls.Add(Me.ButtonAddJutge)
        Me.TabPageJutges.Controls.Add(Me.Xl_Lookup_Jutge)
        Me.TabPageJutges.Location = New System.Drawing.Point(4, 22)
        Me.TabPageJutges.Name = "TabPageJutges"
        Me.TabPageJutges.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageJutges.Size = New System.Drawing.Size(427, 485)
        Me.TabPageJutges.TabIndex = 5
        Me.TabPageJutges.Text = "Jutges"
        Me.TabPageJutges.UseVisualStyleBackColor = True
        '
        'Xl_Jutges
        '
        Me.Xl_Jutges.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Jutges.Location = New System.Drawing.Point(9, 51)
        Me.Xl_Jutges.Name = "Xl_Jutges"
        Me.Xl_Jutges.Size = New System.Drawing.Size(410, 322)
        Me.Xl_Jutges.TabIndex = 2
        '
        'ButtonAddJutge
        '
        Me.ButtonAddJutge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddJutge.Enabled = False
        Me.ButtonAddJutge.Location = New System.Drawing.Point(364, 24)
        Me.ButtonAddJutge.Name = "ButtonAddJutge"
        Me.ButtonAddJutge.Size = New System.Drawing.Size(55, 20)
        Me.ButtonAddJutge.TabIndex = 1
        Me.ButtonAddJutge.Text = "afegir"
        Me.ButtonAddJutge.UseVisualStyleBackColor = True
        '
        'Xl_Lookup_Jutge
        '
        Me.Xl_Lookup_Jutge.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Lookup_Jutge.Location = New System.Drawing.Point(9, 24)
        Me.Xl_Lookup_Jutge.Name = "Xl_Lookup_Jutge"
        Me.Xl_Lookup_Jutge.Size = New System.Drawing.Size(349, 20)
        Me.Xl_Lookup_Jutge.TabIndex = 0
        Me.Xl_Lookup_Jutge.User = Nothing
        '
        'Frm_Raffle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 565)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Raffle"
        Me.Text = "Sorteig"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabPageJutges.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
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
    Friend WithEvents Xl_ImageCallToAction As Mat.NET.Xl_Image
    Friend WithEvents Xl_ImageBanner As Mat.NET.Xl_Image
    Friend WithEvents Xl_ImageFeatured As Mat.NET.Xl_Image
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxBases As System.Windows.Forms.TextBox
    Friend WithEvents Xl_ImageWinner As Mat.NET.Xl_Image
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_RaffleParticipants1 As Mat.NET.Xl_RaffleParticipants
    Friend WithEvents TextBoxWinner As System.Windows.Forms.TextBox
    Friend WithEvents ButtonWinner As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Grf1 As Mat.NET.Xl_Grf
    Friend WithEvents TabPageJutges As System.Windows.Forms.TabPage
    Friend WithEvents ButtonAddJutge As System.Windows.Forms.Button
    Friend WithEvents Xl_Lookup_Jutge As Mat.NET.Xl_Lookup_Usuari
    Friend WithEvents Xl_Jutges As Mat.NET.Xl_Usuaris
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
    Friend WithEvents Xl_AmtCostPubli As Mat.NET.Xl_Amt
    Friend WithEvents Xl_AmtCostPrize As Mat.NET.Xl_Amt
End Class
