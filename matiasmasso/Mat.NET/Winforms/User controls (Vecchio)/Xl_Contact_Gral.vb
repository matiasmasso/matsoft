

Public Class Xl_Contact_Gral
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
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxBotiga As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents Xl_SubContacts1 As Xl_StringList
    Friend WithEvents CheckBoxNomAnterior As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNomNou As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactNomNou As Xl_Contact2
    Friend WithEvents Xl_ContactNomAnterior As Xl_Contact2
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_Rol1 As Xl_Rol
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxWeb As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWebBrowse As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxDisplayWeb As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_EANGLN As Xl_EANOld
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_Adr31 As Xl_Address
    Friend WithEvents Xl_LookupContactClass1 As Xl_LookupContactClass
    Friend WithEvents Label6 As Label
    Friend WithEvents Xl_Tels2 As Xl_Tels
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Xl_LookupNif1 As Xl_LookupNif
    Friend WithEvents Xl_LookupNif2 As Xl_LookupNif
    Friend WithEvents Label13 As Label
    'Friend WithEvents ReportDocument1 As MaxiSrvr.ReportDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DtoEan1 As DTO.DTOEan = New DTO.DTOEan()
        Me.Xl_Langs1 = New Winforms.Xl_Langs()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBotiga = New System.Windows.Forms.CheckBox()
        Me.TextBoxSearchKey = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_SubContacts1 = New Winforms.Xl_StringList()
        Me.Xl_ContactNomNou = New Winforms.Xl_Contact2()
        Me.Xl_ContactNomAnterior = New Winforms.Xl_Contact2()
        Me.CheckBoxNomAnterior = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNomNou = New System.Windows.Forms.CheckBox()
        Me.TextBoxWeb = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_Rol1 = New Winforms.Xl_Rol()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PictureBoxWebBrowse = New System.Windows.Forms.PictureBox()
        Me.CheckBoxDisplayWeb = New System.Windows.Forms.CheckBox()
        Me.Xl_EANGLN = New Winforms.Xl_EANOld()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_Adr31 = New Winforms.Xl_Address()
        Me.Xl_LookupContactClass1 = New Winforms.Xl_LookupContactClass()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_Tels2 = New Winforms.Xl_Tels()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Xl_LookupNif1 = New Winforms.Xl_LookupNif()
        Me.Xl_LookupNif2 = New Winforms.Xl_LookupNif()
        CType(Me.Xl_SubContacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxWebBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_Tels2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Langs1.Location = New System.Drawing.Point(400, 19)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(48, 21)
        Me.Xl_Langs1.TabIndex = 2
        Me.Xl_Langs1.TabStop = False
        Me.Xl_Langs1.Tag = "Idioma"
        Me.Xl_Langs1.Value = Nothing
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(96, 0)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(272, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nom:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 19)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Nom Comercial:"
        '
        'TextBoxCom
        '
        Me.TextBoxCom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCom.Location = New System.Drawing.Point(96, 21)
        Me.TextBoxCom.Name = "TextBoxCom"
        Me.TextBoxCom.Size = New System.Drawing.Size(272, 20)
        Me.TextBoxCom.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "NIF:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(376, 197)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxObsoleto.TabIndex = 10
        Me.CheckBoxObsoleto.TabStop = False
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        '
        'CheckBoxBotiga
        '
        Me.CheckBoxBotiga.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxBotiga.Location = New System.Drawing.Point(376, 213)
        Me.CheckBoxBotiga.Name = "CheckBoxBotiga"
        Me.CheckBoxBotiga.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxBotiga.TabIndex = 11
        Me.CheckBoxBotiga.TabStop = False
        Me.CheckBoxBotiga.Text = "Botiga"
        '
        'TextBoxSearchKey
        '
        Me.TextBoxSearchKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearchKey.Location = New System.Drawing.Point(96, 105)
        Me.TextBoxSearchKey.Name = "TextBoxSearchKey"
        Me.TextBoxSearchKey.Size = New System.Drawing.Size(163, 20)
        Me.TextBoxSearchKey.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Clau de búsqueda:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "adreça"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.Location = New System.Drawing.Point(400, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "llengua:"
        '
        'Xl_SubContacts1
        '
        Me.Xl_SubContacts1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SubContacts1.Location = New System.Drawing.Point(232, 304)
        Me.Xl_SubContacts1.Name = "Xl_SubContacts1"
        Me.Xl_SubContacts1.Size = New System.Drawing.Size(216, 117)
        Me.Xl_SubContacts1.TabIndex = 18
        '
        'Xl_ContactNomNou
        '
        Me.Xl_ContactNomNou.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactNomNou.Contact = Nothing
        Me.Xl_ContactNomNou.Emp = Nothing
        Me.Xl_ContactNomNou.Location = New System.Drawing.Point(232, 284)
        Me.Xl_ContactNomNou.Name = "Xl_ContactNomNou"
        Me.Xl_ContactNomNou.ReadOnly = False
        Me.Xl_ContactNomNou.Size = New System.Drawing.Size(216, 20)
        Me.Xl_ContactNomNou.TabIndex = 17
        Me.Xl_ContactNomNou.Visible = False
        '
        'Xl_ContactNomAnterior
        '
        Me.Xl_ContactNomAnterior.Contact = Nothing
        Me.Xl_ContactNomAnterior.Emp = Nothing
        Me.Xl_ContactNomAnterior.Location = New System.Drawing.Point(0, 284)
        Me.Xl_ContactNomAnterior.Name = "Xl_ContactNomAnterior"
        Me.Xl_ContactNomAnterior.ReadOnly = False
        Me.Xl_ContactNomAnterior.Size = New System.Drawing.Size(224, 20)
        Me.Xl_ContactNomAnterior.TabIndex = 14
        Me.Xl_ContactNomAnterior.Visible = False
        '
        'CheckBoxNomAnterior
        '
        Me.CheckBoxNomAnterior.Location = New System.Drawing.Point(0, 267)
        Me.CheckBoxNomAnterior.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.CheckBoxNomAnterior.Name = "CheckBoxNomAnterior"
        Me.CheckBoxNomAnterior.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxNomAnterior.TabIndex = 13
        Me.CheckBoxNomAnterior.TabStop = False
        Me.CheckBoxNomAnterior.Text = "Nom Anterior:"
        '
        'CheckBoxNomNou
        '
        Me.CheckBoxNomNou.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxNomNou.Location = New System.Drawing.Point(232, 267)
        Me.CheckBoxNomNou.Name = "CheckBoxNomNou"
        Me.CheckBoxNomNou.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxNomNou.TabIndex = 16
        Me.CheckBoxNomNou.TabStop = False
        Me.CheckBoxNomNou.Text = "Nom Nou:"
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Location = New System.Drawing.Point(96, 197)
        Me.TextBoxWeb.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(216, 20)
        Me.TextBoxWeb.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 197)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 16)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "web:"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 216)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 16)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "punt operacional:"
        '
        'Xl_Rol1
        '
        Me.Xl_Rol1.Location = New System.Drawing.Point(96, 237)
        Me.Xl_Rol1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_Rol1.Name = "Xl_Rol1"
        Me.Xl_Rol1.Rol = Nothing
        Me.Xl_Rol1.Size = New System.Drawing.Size(128, 20)
        Me.Xl_Rol1.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(0, 237)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 16)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "rol:"
        '
        'PictureBoxWebBrowse
        '
        Me.PictureBoxWebBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxWebBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxWebBrowse.Image = Global.Winforms.My.Resources.Resources.iExplorer
        Me.PictureBoxWebBrowse.Location = New System.Drawing.Point(338, 197)
        Me.PictureBoxWebBrowse.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.PictureBoxWebBrowse.Name = "PictureBoxWebBrowse"
        Me.PictureBoxWebBrowse.Size = New System.Drawing.Size(24, 20)
        Me.PictureBoxWebBrowse.TabIndex = 29
        Me.PictureBoxWebBrowse.TabStop = False
        '
        'CheckBoxDisplayWeb
        '
        Me.CheckBoxDisplayWeb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDisplayWeb.Location = New System.Drawing.Point(318, 200)
        Me.CheckBoxDisplayWeb.Name = "CheckBoxDisplayWeb"
        Me.CheckBoxDisplayWeb.Size = New System.Drawing.Size(18, 16)
        Me.CheckBoxDisplayWeb.TabIndex = 7
        '
        'Xl_EANGLN
        '
        DtoEan1.Value = ""
        Me.Xl_EANGLN.Ean13 = DtoEan1
        Me.Xl_EANGLN.Location = New System.Drawing.Point(96, 216)
        Me.Xl_EANGLN.Name = "Xl_EANGLN"
        Me.Xl_EANGLN.Size = New System.Drawing.Size(128, 20)
        Me.Xl_EANGLN.TabIndex = 59
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(219, 220)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 16)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "(GLN)"
        '
        'Xl_Adr31
        '
        Me.Xl_Adr31.Location = New System.Drawing.Point(96, 130)
        Me.Xl_Adr31.Name = "Xl_Adr31"
        Me.Xl_Adr31.ReadOnly = True
        Me.Xl_Adr31.Size = New System.Drawing.Size(352, 67)
        Me.Xl_Adr31.TabIndex = 61
        Me.Xl_Adr31.Text = ""
        '
        'Xl_LookupContactClass1
        '
        Me.Xl_LookupContactClass1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupContactClass1.ContactClass = Nothing
        Me.Xl_LookupContactClass1.IsDirty = False
        Me.Xl_LookupContactClass1.Location = New System.Drawing.Point(96, 84)
        Me.Xl_LookupContactClass1.Name = "Xl_LookupContactClass1"
        Me.Xl_LookupContactClass1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupContactClass1.ReadOnlyLookup = False
        Me.Xl_LookupContactClass1.Size = New System.Drawing.Size(199, 20)
        Me.Xl_LookupContactClass1.TabIndex = 63
        Me.Xl_LookupContactClass1.Value = Nothing
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(0, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 16)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Classificació"
        '
        'Xl_Tels2
        '
        Me.Xl_Tels2.AllowUserToAddRows = False
        Me.Xl_Tels2.AllowUserToDeleteRows = False
        Me.Xl_Tels2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Tels2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Tels2.DisplayObsolets = False
        Me.Xl_Tels2.Location = New System.Drawing.Point(0, 304)
        Me.Xl_Tels2.MouseIsDown = False
        Me.Xl_Tels2.Name = "Xl_Tels2"
        Me.Xl_Tels2.ReadOnly = True
        Me.Xl_Tels2.Size = New System.Drawing.Size(226, 117)
        Me.Xl_Tels2.TabIndex = 66
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(0, 427)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 16)
        Me.Label12.TabIndex = 81
        Me.Label12.Text = "Observacions:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(0, 446)
        Me.TextBoxObs.MinimumSize = New System.Drawing.Size(0, 24)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(448, 78)
        Me.TextBoxObs.TabIndex = 80
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(0, 65)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 16)
        Me.Label13.TabIndex = 82
        Me.Label13.Text = "NIF2:"
        '
        'Xl_LookupNif1
        '
        Me.Xl_LookupNif1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif1.IsDirty = False
        Me.Xl_LookupNif1.Location = New System.Drawing.Point(96, 42)
        Me.Xl_LookupNif1.Name = "Xl_LookupNif1"
        Me.Xl_LookupNif1.Nif = Nothing
        Me.Xl_LookupNif1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif1.ReadOnlyLookup = False
        Me.Xl_LookupNif1.Size = New System.Drawing.Size(199, 20)
        Me.Xl_LookupNif1.TabIndex = 83
        Me.Xl_LookupNif1.Value = Nothing
        '
        'Xl_LookupNif2
        '
        Me.Xl_LookupNif2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupNif2.IsDirty = False
        Me.Xl_LookupNif2.Location = New System.Drawing.Point(96, 63)
        Me.Xl_LookupNif2.Name = "Xl_LookupNif2"
        Me.Xl_LookupNif2.Nif = Nothing
        Me.Xl_LookupNif2.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupNif2.ReadOnlyLookup = False
        Me.Xl_LookupNif2.Size = New System.Drawing.Size(199, 20)
        Me.Xl_LookupNif2.TabIndex = 84
        Me.Xl_LookupNif2.Value = Nothing
        '
        'Xl_Contact_Gral
        '
        Me.Controls.Add(Me.Xl_LookupNif2)
        Me.Controls.Add(Me.Xl_LookupNif1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.TextBoxWeb)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Xl_Tels2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_LookupContactClass1)
        Me.Controls.Add(Me.Xl_Adr31)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Xl_EANGLN)
        Me.Controls.Add(Me.CheckBoxDisplayWeb)
        Me.Controls.Add(Me.PictureBoxWebBrowse)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_Rol1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CheckBoxNomNou)
        Me.Controls.Add(Me.CheckBoxNomAnterior)
        Me.Controls.Add(Me.Xl_ContactNomAnterior)
        Me.Controls.Add(Me.Xl_ContactNomNou)
        Me.Controls.Add(Me.Xl_SubContacts1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxSearchKey)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CheckBoxBotiga)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Name = "Xl_Contact_Gral"
        Me.Size = New System.Drawing.Size(456, 527)
        CType(Me.Xl_SubContacts1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxWebBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_Tels2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event ChangedGral()
    Public Event ChangedClx()
    Public Event ChangedCll()
    Public Event ChangedAdr()
    Public Event ChangedTels()
    Public Event ChangedSubContacts()

    Private _Contact As DTOContact
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(exs As List(Of Exception), oContact As DTOContact)
        _Contact = oContact
        With _Contact
            TextBoxNom.Text = .Nom
            TextBoxCom.Text = .NomComercial
            Xl_LookupNif1.Load(.Nifs.PrimaryNif)
            Xl_LookupNif2.Load(.Nifs.AlternateNif)
            Xl_LookupContactClass1.ContactClass = .ContactClass
            TextBoxSearchKey.Text = .SearchKey
            CheckBoxObsoleto.Checked = .Obsoleto
            CheckBoxBotiga.Checked = .Botiga
            If .Lang Is Nothing Then .Lang = DTOApp.Current.Lang
            Xl_Langs1.Value = .Lang
            Xl_Adr31.Load(.Address)
            Xl_Rol1.Rol = .Rol
            TextBoxWeb.Text = .Website
            CheckBoxDisplayWeb.Checked = .DisplayWebsite
            PictureBoxWebBrowse.Visible = TextBoxWeb.Text > ""

            Xl_EANGLN.Ean13 = .GLN

            If .NomAnterior IsNot Nothing Then
                CheckBoxNomAnterior.Checked = True
                Xl_ContactNomAnterior.Visible = CheckBoxNomAnterior.Checked
                Xl_ContactNomAnterior.Contact = .NomAnterior
            End If

            If .NomNou IsNot Nothing Then
                CheckBoxNomNou.Checked = True
                Xl_ContactNomNou.Visible = CheckBoxNomNou.Checked
                Xl_ContactNomNou.Contact = .NomNou
            End If

            Xl_SubContacts1.Load(.ContactPersons)
            Xl_Tels2.Load(_Contact)
            TextBoxObs.Text = .Obs
        End With

        _AllowEvents = True
    End Sub

    Public ReadOnly Property Contact As DTOContact
        Get
            With _Contact
                .Nom = TextBoxNom.Text
                .NomComercial = TextBoxCom.Text
                Xl_LookupNif1.Load(.Nifs.PrimaryNif)
                Xl_LookupNif2.Load(.Nifs.AlternateNif)
                .ContactClass = Xl_LookupContactClass1.ContactClass
                .SearchKey = TextBoxSearchKey.Text
                .Obsoleto = CheckBoxObsoleto.Checked
                .Botiga = CheckBoxBotiga.Checked
                .Lang = Xl_Langs1.Value
                .Address = Xl_Adr31.Address
                .Rol = Xl_Rol1.Rol
                .Website = TextBoxWeb.Text
                .DisplayWebsite = CheckBoxDisplayWeb.Checked
                .GLN = Xl_EANGLN.Ean13
                .NomAnterior = IIf(CheckBoxNomAnterior.Checked, Xl_ContactNomAnterior.Contact, Nothing)
                .NomNou = IIf(CheckBoxNomNou.Checked, Xl_ContactNomNou.Contact, Nothing)
                .ContactPersons = Xl_SubContacts1.values
                .Tels = Xl_Tels2.Tels
                .Emails = Xl_Tels2.Emails
                .Obs = TextBoxObs.Text

            End With
            Return _Contact
        End Get
    End Property



    Private Sub DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            TextBoxSearchKey.TextChanged,
            Xl_LookupNif1.AfterUpdate,
            Xl_LookupNif2.AfterUpdate,
            CheckBoxBotiga.CheckedChanged,
            Xl_Langs1.AfterUpdate,
            Xl_Rol1.AfterUpdate,
            Xl_EANGLN.Changed,
            CheckBoxDisplayWeb.CheckedChanged,
            CheckBoxNomAnterior.CheckedChanged,
            CheckBoxNomNou.CheckedChanged,
            Xl_ContactNomAnterior.AfterUpdate,
            Xl_ContactNomNou.AfterUpdate,
            CheckBoxObsoleto.CheckedChanged,
              Xl_LookupContactClass1.AfterUpdate,
              Xl_SubContacts1.AfterUpdate,
               TextBoxObs.TextChanged

        If _AllowEvents Then
            RaiseEvent ChangedGral()
        End If
    End Sub

    Private Sub Clx_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxCom.TextChanged

        If _AllowEvents Then
            RaiseEvent ChangedClx()
            RaiseEvent ChangedGral()
        End If
    End Sub

    Private Sub Xl_Adr31_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Adr31.AfterUpdate
        If _AllowEvents Then
            RaiseEvent ChangedAdr()
            RaiseEvent ChangedClx()
            RaiseEvent ChangedGral()
        End If
    End Sub

    Private Sub CheckBoxNomAnterior_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNomAnterior.CheckedChanged
        Xl_ContactNomAnterior.Visible = CheckBoxNomAnterior.Checked
    End Sub

    Private Sub CheckBoxNomNou_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNomNou.CheckedChanged
        Xl_ContactNomNou.Visible = CheckBoxNomNou.Checked
    End Sub

    Private Sub Xl_SubContacts1_Changed() Handles Xl_SubContacts1.AfterUpdate
        If _AllowEvents Then RaiseEvent ChangedSubContacts()
    End Sub


    Private Sub PictureBoxWebBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxWebBrowse.Click
        Dim sUrl As String = TextBoxWeb.Text
        Process.Start(sUrl)
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxWeb.TextChanged

        PictureBoxWebBrowse.Visible = TextBoxWeb.Text > ""
        If _AllowEvents Then RaiseEvent ChangedGral()
    End Sub

    Private Sub Xl_Tels2_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Tels2.AfterUpdate, Xl_Tels2.RequestToRefresh
        If _AllowEvents Then
            'Xl_Tels2.Load(_Contact)
            RaiseEvent ChangedGral()
        End If
    End Sub


End Class
