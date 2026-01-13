<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConsumerTicket
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
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCognom1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCognom2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxTel = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxId = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_DocFileTicket = New Winforms.Xl_DocFile()
        Me.Xl_Address1 = New Winforms.Xl_Address()
        Me.TabPageMarketPlace = New System.Windows.Forms.TabPage()
        Me.TextBoxUsrReviewRequest = New System.Windows.Forms.TextBox()
        Me.CheckBoxReviewRequest = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerDelivered = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxUsrDelivered = New System.Windows.Forms.TextBox()
        Me.CheckBoxDelivered = New System.Windows.Forms.CheckBox()
        Me.TextBoxUsrTrackingNotified = New System.Windows.Forms.TextBox()
        Me.CheckBoxUsrTrackingNotified = New System.Windows.Forms.CheckBox()
        Me.TextBoxTracking = New System.Windows.Forms.TextBox()
        Me.TextBoxTrpNom = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_DocFileFra = New Winforms.Xl_DocFile()
        Me.TextBoxFra = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchFra = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxNif = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxFraNom = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Xl_AddressFra = New Winforms.Xl_Address()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedactaDeNouLaFacturaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_UsrLog1 = New Winforms.Xl_UsrLog()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PanelButtons.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPageMarketPlace.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 505)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(792, 31)
        Me.PanelButtons.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(573, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(684, 4)
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
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(99, 55)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 59
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(99, 81)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(328, 20)
        Me.TextBoxNom.TabIndex = 58
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Nom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Data"
        '
        'TextBoxCognom1
        '
        Me.TextBoxCognom1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCognom1.Location = New System.Drawing.Point(99, 107)
        Me.TextBoxCognom1.Name = "TextBoxCognom1"
        Me.TextBoxCognom1.Size = New System.Drawing.Size(328, 20)
        Me.TextBoxCognom1.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Primer cognom"
        '
        'TextBoxCognom2
        '
        Me.TextBoxCognom2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCognom2.Location = New System.Drawing.Point(99, 133)
        Me.TextBoxCognom2.Name = "TextBoxCognom2"
        Me.TextBoxCognom2.Size = New System.Drawing.Size(328, 20)
        Me.TextBoxCognom2.TabIndex = 64
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Segon cognom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 163)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "Adreça"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(99, 235)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(328, 20)
        Me.TextBoxEmail.TabIndex = 68
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 238)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "Email"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.Location = New System.Drawing.Point(99, 261)
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(124, 20)
        Me.TextBoxTel.TabIndex = 70
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "Telèfon"
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(99, 29)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.ReadOnly = True
        Me.TextBoxId.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxId.TabIndex = 74
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "Ticket"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPageMarketPlace)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(791, 447)
        Me.TabControl1.TabIndex = 75
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_DocFileTicket)
        Me.TabPage1.Controls.Add(Me.TextBoxId)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.DateTimePicker1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxTel)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxCognom1)
        Me.TabPage1.Controls.Add(Me.TextBoxEmail)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxCognom2)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Xl_Address1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(783, 421)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_DocFileTicket
        '
        Me.Xl_DocFileTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFileTicket.IsDirty = False
        Me.Xl_DocFileTicket.IsInedit = False
        Me.Xl_DocFileTicket.Location = New System.Drawing.Point(433, 0)
        Me.Xl_DocFileTicket.Name = "Xl_DocFileTicket"
        Me.Xl_DocFileTicket.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFileTicket.TabIndex = 75
        '
        'Xl_Address1
        '
        Me.Xl_Address1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Address1.Location = New System.Drawing.Point(99, 160)
        Me.Xl_Address1.Name = "Xl_Address1"
        Me.Xl_Address1.ReadOnly = True
        Me.Xl_Address1.Size = New System.Drawing.Size(328, 69)
        Me.Xl_Address1.TabIndex = 65
        Me.Xl_Address1.Text = ""
        '
        'TabPageMarketPlace
        '
        Me.TabPageMarketPlace.Controls.Add(Me.TextBoxUsrReviewRequest)
        Me.TabPageMarketPlace.Controls.Add(Me.CheckBoxReviewRequest)
        Me.TabPageMarketPlace.Controls.Add(Me.DateTimePickerDelivered)
        Me.TabPageMarketPlace.Controls.Add(Me.TextBoxUsrDelivered)
        Me.TabPageMarketPlace.Controls.Add(Me.CheckBoxDelivered)
        Me.TabPageMarketPlace.Controls.Add(Me.TextBoxUsrTrackingNotified)
        Me.TabPageMarketPlace.Controls.Add(Me.CheckBoxUsrTrackingNotified)
        Me.TabPageMarketPlace.Controls.Add(Me.TextBoxTracking)
        Me.TabPageMarketPlace.Controls.Add(Me.TextBoxTrpNom)
        Me.TabPageMarketPlace.Controls.Add(Me.Label15)
        Me.TabPageMarketPlace.Controls.Add(Me.Label8)
        Me.TabPageMarketPlace.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMarketPlace.Name = "TabPageMarketPlace"
        Me.TabPageMarketPlace.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMarketPlace.Size = New System.Drawing.Size(783, 421)
        Me.TabPageMarketPlace.TabIndex = 2
        Me.TabPageMarketPlace.Text = "Market place"
        Me.TabPageMarketPlace.UseVisualStyleBackColor = True
        '
        'TextBoxUsrReviewRequest
        '
        Me.TextBoxUsrReviewRequest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsrReviewRequest.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxUsrReviewRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxUsrReviewRequest.Location = New System.Drawing.Point(170, 199)
        Me.TextBoxUsrReviewRequest.Name = "TextBoxUsrReviewRequest"
        Me.TextBoxUsrReviewRequest.ReadOnly = True
        Me.TextBoxUsrReviewRequest.Size = New System.Drawing.Size(606, 20)
        Me.TextBoxUsrReviewRequest.TabIndex = 10
        Me.TextBoxUsrReviewRequest.Visible = False
        '
        'CheckBoxReviewRequest
        '
        Me.CheckBoxReviewRequest.AutoSize = True
        Me.CheckBoxReviewRequest.Location = New System.Drawing.Point(47, 202)
        Me.CheckBoxReviewRequest.Name = "CheckBoxReviewRequest"
        Me.CheckBoxReviewRequest.Size = New System.Drawing.Size(122, 17)
        Me.CheckBoxReviewRequest.TabIndex = 9
        Me.CheckBoxReviewRequest.Text = "Sol·licitada ressenya"
        Me.CheckBoxReviewRequest.UseVisualStyleBackColor = True
        '
        'DateTimePickerDelivered
        '
        Me.DateTimePickerDelivered.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDelivered.Location = New System.Drawing.Point(170, 166)
        Me.DateTimePickerDelivered.Name = "DateTimePickerDelivered"
        Me.DateTimePickerDelivered.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePickerDelivered.TabIndex = 8
        Me.DateTimePickerDelivered.Visible = False
        '
        'TextBoxUsrDelivered
        '
        Me.TextBoxUsrDelivered.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsrDelivered.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxUsrDelivered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxUsrDelivered.Location = New System.Drawing.Point(276, 166)
        Me.TextBoxUsrDelivered.Name = "TextBoxUsrDelivered"
        Me.TextBoxUsrDelivered.ReadOnly = True
        Me.TextBoxUsrDelivered.Size = New System.Drawing.Size(500, 20)
        Me.TextBoxUsrDelivered.TabIndex = 7
        Me.TextBoxUsrDelivered.Visible = False
        '
        'CheckBoxDelivered
        '
        Me.CheckBoxDelivered.AutoSize = True
        Me.CheckBoxDelivered.Location = New System.Drawing.Point(47, 167)
        Me.CheckBoxDelivered.Name = "CheckBoxDelivered"
        Me.CheckBoxDelivered.Size = New System.Drawing.Size(66, 17)
        Me.CheckBoxDelivered.TabIndex = 6
        Me.CheckBoxDelivered.Text = "Entregat"
        Me.CheckBoxDelivered.UseVisualStyleBackColor = True
        '
        'TextBoxUsrTrackingNotified
        '
        Me.TextBoxUsrTrackingNotified.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsrTrackingNotified.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxUsrTrackingNotified.Location = New System.Drawing.Point(132, 88)
        Me.TextBoxUsrTrackingNotified.Name = "TextBoxUsrTrackingNotified"
        Me.TextBoxUsrTrackingNotified.ReadOnly = True
        Me.TextBoxUsrTrackingNotified.Size = New System.Drawing.Size(644, 20)
        Me.TextBoxUsrTrackingNotified.TabIndex = 5
        Me.TextBoxUsrTrackingNotified.Visible = False
        '
        'CheckBoxUsrTrackingNotified
        '
        Me.CheckBoxUsrTrackingNotified.AutoSize = True
        Me.CheckBoxUsrTrackingNotified.Location = New System.Drawing.Point(47, 91)
        Me.CheckBoxUsrTrackingNotified.Name = "CheckBoxUsrTrackingNotified"
        Me.CheckBoxUsrTrackingNotified.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxUsrTrackingNotified.TabIndex = 4
        Me.CheckBoxUsrTrackingNotified.Text = "Notificat"
        Me.CheckBoxUsrTrackingNotified.UseVisualStyleBackColor = True
        '
        'TextBoxTracking
        '
        Me.TextBoxTracking.Location = New System.Drawing.Point(132, 62)
        Me.TextBoxTracking.Name = "TextBoxTracking"
        Me.TextBoxTracking.ReadOnly = True
        Me.TextBoxTracking.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxTracking.TabIndex = 3
        '
        'TextBoxTrpNom
        '
        Me.TextBoxTrpNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTrpNom.Location = New System.Drawing.Point(132, 36)
        Me.TextBoxTrpNom.Name = "TextBoxTrpNom"
        Me.TextBoxTrpNom.ReadOnly = True
        Me.TextBoxTrpNom.Size = New System.Drawing.Size(644, 20)
        Me.TextBoxTrpNom.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(44, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 13)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Transportista"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(44, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Tracking"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_DocFileFra)
        Me.TabPage2.Controls.Add(Me.TextBoxFra)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.DateTimePickerFchFra)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.TextBoxNif)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.TextBoxFraNom)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.Xl_AddressFra)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(783, 421)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Factura"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_DocFileFra
        '
        Me.Xl_DocFileFra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFileFra.IsDirty = False
        Me.Xl_DocFileFra.IsInedit = False
        Me.Xl_DocFileFra.Location = New System.Drawing.Point(433, 1)
        Me.Xl_DocFileFra.Name = "Xl_DocFileFra"
        Me.Xl_DocFileFra.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFileFra.TabIndex = 87
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Location = New System.Drawing.Point(98, 32)
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.ReadOnly = True
        Me.TextBoxFra.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxFra.TabIndex = 86
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 13)
        Me.Label11.TabIndex = 85
        Me.Label11.Text = "Factura"
        '
        'DateTimePickerFchFra
        '
        Me.DateTimePickerFchFra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFra.Location = New System.Drawing.Point(98, 58)
        Me.DateTimePickerFchFra.Name = "DateTimePickerFchFra"
        Me.DateTimePickerFchFra.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchFra.TabIndex = 83
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 60)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 13)
        Me.Label13.TabIndex = 84
        Me.Label13.Text = "Data"
        '
        'TextBoxNif
        '
        Me.TextBoxNif.Location = New System.Drawing.Point(98, 188)
        Me.TextBoxNif.Name = "TextBoxNif"
        Me.TextBoxNif.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxNif.TabIndex = 82
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 191)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 13)
        Me.Label10.TabIndex = 81
        Me.Label10.Text = "Nif"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 89)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "Raó social"
        '
        'TextBoxFraNom
        '
        Me.TextBoxFraNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFraNom.Location = New System.Drawing.Point(98, 86)
        Me.TextBoxFraNom.Name = "TextBoxFraNom"
        Me.TextBoxFraNom.Size = New System.Drawing.Size(329, 20)
        Me.TextBoxFraNom.TabIndex = 74
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(15, 116)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "Adreça"
        '
        'Xl_AddressFra
        '
        Me.Xl_AddressFra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AddressFra.Location = New System.Drawing.Point(98, 113)
        Me.Xl_AddressFra.Name = "Xl_AddressFra"
        Me.Xl_AddressFra.ReadOnly = True
        Me.Xl_AddressFra.Size = New System.Drawing.Size(329, 69)
        Me.Xl_AddressFra.TabIndex = 75
        Me.Xl_AddressFra.Text = ""
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip1.TabIndex = 76
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RedactaDeNouLaFacturaToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RedactaDeNouLaFacturaToolStripMenuItem
        '
        Me.RedactaDeNouLaFacturaToolStripMenuItem.Name = "RedactaDeNouLaFacturaToolStripMenuItem"
        Me.RedactaDeNouLaFacturaToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.RedactaDeNouLaFacturaToolStripMenuItem.Text = "redacta de nou la factura"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(0, 485)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(792, 20)
        Me.Xl_UsrLog1.TabIndex = 75
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(783, 421)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Devolucions"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Frm_ConsumerTicket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 536)
        Me.Controls.Add(Me.Xl_UsrLog1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_ConsumerTicket"
        Me.Text = "Ticket de Consumidor"
        Me.PanelButtons.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPageMarketPlace.ResumeLayout(False)
        Me.TabPageMarketPlace.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxCognom1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxCognom2 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_Address1 As Xl_Address
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxTel As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxId As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxFra As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents DateTimePickerFchFra As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents TextBoxNif As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBoxFraNom As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Xl_AddressFra As Xl_Address
    Friend WithEvents Xl_DocFileTicket As Xl_DocFile
    Friend WithEvents Xl_DocFileFra As Xl_DocFile
    Friend WithEvents RedactaDeNouLaFacturaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPageMarketPlace As TabPage
    Friend WithEvents DateTimePickerDelivered As DateTimePicker
    Friend WithEvents TextBoxUsrDelivered As TextBox
    Friend WithEvents CheckBoxDelivered As CheckBox
    Friend WithEvents TextBoxUsrTrackingNotified As TextBox
    Friend WithEvents CheckBoxUsrTrackingNotified As CheckBox
    Friend WithEvents TextBoxTracking As TextBox
    Friend WithEvents TextBoxTrpNom As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxUsrReviewRequest As TextBox
    Friend WithEvents CheckBoxReviewRequest As CheckBox
    Friend WithEvents TabPage3 As TabPage
End Class
