

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
    Friend WithEvents Xl_Langs1 As Xl_Langs_Old
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxBotiga As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxParticular As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents Xl_SubContacts1 As Xl_SubContacts
    Friend WithEvents CheckBoxNomAnterior As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNomNou As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactNomNou As Xl_Contact
    Friend WithEvents Xl_ContactNomAnterior As Xl_Contact
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Xl_Rol1 As Xl_Rol
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxWeb As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxWebBrowse As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBoxDisplayWeb As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_NIF1 As Xl_NIF
    Friend WithEvents Xl_EANGLN As Xl_EAN
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_Adr31 As Xl_Adr3
    Friend WithEvents Xl_Tels1 As Mat.Net.Xl_Tels
    Friend WithEvents ReportDocument1 As MaxiSrvr.ReportDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_Langs1 = New Mat.Net.Xl_Langs_Old()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBotiga = New System.Windows.Forms.CheckBox()
        Me.CheckBoxParticular = New System.Windows.Forms.CheckBox()
        Me.TextBoxSearchKey = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_SubContacts1 = New Mat.Net.Xl_SubContacts()
        Me.Xl_ContactNomNou = New Mat.Net.Xl_Contact()
        Me.Xl_ContactNomAnterior = New Mat.Net.Xl_Contact()
        Me.CheckBoxNomAnterior = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNomNou = New System.Windows.Forms.CheckBox()
        Me.Xl_Image1 = New Mat.Net.Xl_Image()
        Me.TextBoxWeb = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Xl_Rol1 = New Mat.Net.Xl_Rol()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PictureBoxWebBrowse = New System.Windows.Forms.PictureBox()
        Me.CheckBoxDisplayWeb = New System.Windows.Forms.CheckBox()
        Me.Xl_NIF1 = New Mat.Net.Xl_NIF()
        Me.ReportDocument1 = New MaxiSrvr.ReportDocument()
        Me.Xl_EANGLN = New Mat.Net.Xl_EAN()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_Adr31 = New Mat.Net.Xl_Adr3()
        Me.Xl_Tels1 = New Mat.Net.Xl_Tels()
        CType(Me.PictureBoxWebBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TextBoxCom.Location = New System.Drawing.Point(96, 19)
        Me.TextBoxCom.Name = "TextBoxCom"
        Me.TextBoxCom.Size = New System.Drawing.Size(272, 20)
        Me.TextBoxCom.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "NIF:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(376, 153)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxObsoleto.TabIndex = 10
        Me.CheckBoxObsoleto.TabStop = False
        Me.CheckBoxObsoleto.Text = "Obsoleto"
        '
        'CheckBoxBotiga
        '
        Me.CheckBoxBotiga.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxBotiga.Location = New System.Drawing.Point(376, 169)
        Me.CheckBoxBotiga.Name = "CheckBoxBotiga"
        Me.CheckBoxBotiga.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxBotiga.TabIndex = 11
        Me.CheckBoxBotiga.TabStop = False
        Me.CheckBoxBotiga.Text = "Botiga"
        '
        'CheckBoxParticular
        '
        Me.CheckBoxParticular.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxParticular.Location = New System.Drawing.Point(376, 184)
        Me.CheckBoxParticular.Name = "CheckBoxParticular"
        Me.CheckBoxParticular.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxParticular.TabIndex = 12
        Me.CheckBoxParticular.TabStop = False
        Me.CheckBoxParticular.Text = "Particular"
        '
        'TextBoxSearchKey
        '
        Me.TextBoxSearchKey.Location = New System.Drawing.Point(96, 62)
        Me.TextBoxSearchKey.Name = "TextBoxSearchKey"
        Me.TextBoxSearchKey.Size = New System.Drawing.Size(152, 20)
        Me.TextBoxSearchKey.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Clau de búsqueda:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 83)
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
        Me.Xl_SubContacts1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SubContacts1.Location = New System.Drawing.Point(232, 254)
        Me.Xl_SubContacts1.Name = "Xl_SubContacts1"
        Me.Xl_SubContacts1.Size = New System.Drawing.Size(216, 133)
        Me.Xl_SubContacts1.TabIndex = 18
        '
        'Xl_ContactNomNou
        '
        Me.Xl_ContactNomNou.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactNomNou.Location = New System.Drawing.Point(232, 234)
        Me.Xl_ContactNomNou.Name = "Xl_ContactNomNou"
        Me.Xl_ContactNomNou.Size = New System.Drawing.Size(216, 20)
        Me.Xl_ContactNomNou.TabIndex = 17
        Me.Xl_ContactNomNou.Visible = False
        '
        'Xl_ContactNomAnterior
        '
        Me.Xl_ContactNomAnterior.Location = New System.Drawing.Point(0, 234)
        Me.Xl_ContactNomAnterior.Name = "Xl_ContactNomAnterior"
        Me.Xl_ContactNomAnterior.Size = New System.Drawing.Size(224, 20)
        Me.Xl_ContactNomAnterior.TabIndex = 14
        Me.Xl_ContactNomAnterior.Visible = False
        '
        'CheckBoxNomAnterior
        '
        Me.CheckBoxNomAnterior.Location = New System.Drawing.Point(0, 217)
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
        Me.CheckBoxNomNou.Location = New System.Drawing.Point(232, 217)
        Me.CheckBoxNomNou.Name = "CheckBoxNomNou"
        Me.CheckBoxNomNou.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxNomNou.TabIndex = 16
        Me.CheckBoxNomNou.TabStop = False
        Me.CheckBoxNomNou.Text = "Nom Nou:"
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.Location = New System.Drawing.Point(298, 44)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_Image1.TabIndex = 22
        Me.Xl_Image1.ZipStream = Nothing
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Location = New System.Drawing.Point(96, 153)
        Me.TextBoxWeb.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(216, 20)
        Me.TextBoxWeb.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 153)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 16)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "web:"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 172)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 16)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "punt operacional:"
        '
        'Xl_Rol1
        '
        Me.Xl_Rol1.Location = New System.Drawing.Point(96, 193)
        Me.Xl_Rol1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.Xl_Rol1.Name = "Xl_Rol1"
        Me.Xl_Rol1.Rol = Nothing
        Me.Xl_Rol1.Size = New System.Drawing.Size(128, 20)
        Me.Xl_Rol1.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(0, 193)
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
        Me.PictureBoxWebBrowse.Image = Global.Mat.Net.My.Resources.Resources.iExplorer
        Me.PictureBoxWebBrowse.Location = New System.Drawing.Point(338, 153)
        Me.PictureBoxWebBrowse.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.PictureBoxWebBrowse.Name = "PictureBoxWebBrowse"
        Me.PictureBoxWebBrowse.Size = New System.Drawing.Size(24, 20)
        Me.PictureBoxWebBrowse.TabIndex = 29
        Me.PictureBoxWebBrowse.TabStop = False
        '
        'CheckBoxDisplayWeb
        '
        Me.CheckBoxDisplayWeb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDisplayWeb.Location = New System.Drawing.Point(318, 156)
        Me.CheckBoxDisplayWeb.Name = "CheckBoxDisplayWeb"
        Me.CheckBoxDisplayWeb.Size = New System.Drawing.Size(18, 16)
        Me.CheckBoxDisplayWeb.TabIndex = 7
        '
        'Xl_NIF1
        '
        Me.Xl_NIF1.Location = New System.Drawing.Point(96, 42)
        Me.Xl_NIF1.Name = "Xl_NIF1"
        Me.Xl_NIF1.Size = New System.Drawing.Size(173, 20)
        Me.Xl_NIF1.TabIndex = 3
        '
        'ReportDocument1
        '
        Me.ReportDocument1.AutoDiscover = False
        Me.ReportDocument1.ColArrastreText = Nothing
        Me.ReportDocument1.DataMember = Nothing
        Me.ReportDocument1.DataSource = Nothing
        Me.ReportDocument1.Font = New System.Drawing.Font("Courier New", 10.0!)
        Me.ReportDocument1.FooterLeft = Nothing
        Me.ReportDocument1.FooterLines = 2
        Me.ReportDocument1.FooterRight = Nothing
        Me.ReportDocument1.GridVisible = False
        Me.ReportDocument1.SubTitleLeft = Nothing
        Me.ReportDocument1.SubTitleRight = Nothing
        Me.ReportDocument1.SupressDefaultFooter = False
        Me.ReportDocument1.SupressDefaultHeader = False
        Me.ReportDocument1.Title = Nothing
        '
        'Xl_EANGLN
        '
        Me.Xl_EANGLN.Location = New System.Drawing.Point(96, 172)
        Me.Xl_EANGLN.Name = "Xl_EANGLN"
        Me.Xl_EANGLN.Size = New System.Drawing.Size(117, 20)
        Me.Xl_EANGLN.TabIndex = 59
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(219, 176)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 16)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "(GLN)"
        '
        'Xl_Adr31
        '
        Me.Xl_Adr31.Adr = Nothing
        Me.Xl_Adr31.Location = New System.Drawing.Point(96, 83)
        Me.Xl_Adr31.Name = "Xl_Adr31"
        Me.Xl_Adr31.Size = New System.Drawing.Size(352, 67)
        Me.Xl_Adr31.TabIndex = 61
        '
        'Xl_Tels1
        '
        Me.Xl_Tels1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Tels1.Location = New System.Drawing.Point(0, 254)
        Me.Xl_Tels1.Name = "Xl_Tels1"
        Me.Xl_Tels1.Size = New System.Drawing.Size(224, 133)
        Me.Xl_Tels1.TabIndex = 62
        '
        'Xl_Contact_Gral
        '
        Me.Controls.Add(Me.Xl_Tels1)
        Me.Controls.Add(Me.Xl_Adr31)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Xl_EANGLN)
        Me.Controls.Add(Me.Xl_NIF1)
        Me.Controls.Add(Me.CheckBoxDisplayWeb)
        Me.Controls.Add(Me.PictureBoxWebBrowse)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Xl_Rol1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxWeb)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.CheckBoxNomNou)
        Me.Controls.Add(Me.CheckBoxNomAnterior)
        Me.Controls.Add(Me.Xl_ContactNomAnterior)
        Me.Controls.Add(Me.Xl_ContactNomNou)
        Me.Controls.Add(Me.Xl_SubContacts1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxSearchKey)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CheckBoxParticular)
        Me.Controls.Add(Me.CheckBoxBotiga)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Name = "Xl_Contact_Gral"
        Me.Size = New System.Drawing.Size(456, 387)
        CType(Me.PictureBoxWebBrowse, System.ComponentModel.ISupportInitialize).EndInit()
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

    Private mContact As MaxiSrvr.Contact
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            If value IsNot Nothing Then
                mContact = value
                With mContact
                    TextBoxNom.Text = .Nom
                    TextBoxCom.Text = .NomComercial
                    Xl_NIF1.Text = .NIF
                    TextBoxSearchKey.Text = .NomKey
                    CheckBoxObsoleto.Checked = .Obsoleto
                    CheckBoxBotiga.Checked = .Botiga
                    CheckBoxParticular.Checked = .Particular
                    Xl_Langs1.Lang = .Lang
                    Xl_Adr31.Adr = .Adr

                    Xl_Tels1.Contact = mContact

                    If Not .Rol Is Nothing Then
                        Xl_Rol1.Rol = .Rol
                        'If Not root.usuari.AllowContactBrowse(mContact) Then
                        'TextBoxPwd.ReadOnly = True
                        'End If
                    End If

                    TextBoxWeb.Text = .Web
                    CheckBoxDisplayWeb.Checked = .ShowWeb
                    PictureBoxWebBrowse.Visible = TextBoxWeb.Text > ""

                    Xl_EANGLN.Ean13 = .Gln

                    Xl_ContactNomAnterior.Contact = .ContactAnterior
                    CheckBoxNomAnterior.Checked = (.ContactAnterior IsNot Nothing)
                    Xl_ContactNomAnterior.Visible = CheckBoxNomAnterior.Checked

                    Xl_ContactNomNou.Contact = .ContactNou
                    If .ContactNou IsNot Nothing Then
                        CheckBoxNomNou.Checked = (.ContactNou.Id > 0)
                        Xl_ContactNomNou.Visible = CheckBoxNomNou.Checked
                        Xl_ContactNomNou.Contact = .ContactNou
                    End If

                    Xl_SubContacts1.Subcontacts = .SubContacts

                    Xl_Image1.Bitmap = .Img48
                    mAllowEvents = True

                End With
            End If
        End Set
    End Property


    Public Sub setImgFromCcx(ByVal oImage As Bitmap)
        Xl_Image1.Bitmap = oImage
        Xl_Image1.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
    End Sub

    Public ReadOnly Property Nom() As String
        Get
            Return TextBoxNom.Text
        End Get
    End Property

    Public ReadOnly Property NomComercial() As String
        Get
            Return TextBoxCom.Text
        End Get
    End Property

    Public ReadOnly Property NIF() As String
        Get
            Return Xl_NIF1.Text
        End Get
    End Property

    Public ReadOnly Property NomKey() As String
        Get
            Return TextBoxSearchKey.Text
        End Get
    End Property

    Public ReadOnly Property Botiga() As Boolean
        Get
            Return CheckBoxBotiga.Checked
        End Get
    End Property

    Public ReadOnly Property Obsoleto() As Boolean
        Get
            Return CheckBoxObsoleto.Checked
        End Get
    End Property

    Public ReadOnly Property Particular() As Boolean
        Get
            Return CheckBoxParticular.Checked
        End Get
    End Property

    Public ReadOnly Property Lang() As DTOLang
        Get
            Return Xl_Langs1.Lang
        End Get
    End Property

    Public ReadOnly Property Adr() As Adr
        Get
            Return Xl_Adr31.Adr
        End Get
    End Property

    Public ReadOnly Property Rol() As DTORol
        Get
            Return Xl_Rol1.Rol
        End Get
    End Property

    Public ReadOnly Property Web() As String
        Get
            Return TextBoxWeb.Text
        End Get
    End Property

    Public ReadOnly Property DisplayWeb() As Boolean
        Get
            Return CheckBoxDisplayWeb.Checked
        End Get
    End Property

    Public ReadOnly Property Gln() As MaxiSrvr.Ean13
        Get
            Return Xl_EANGLN.Ean13
        End Get
    End Property

    Public ReadOnly Property ContactAnterior() As Contact
        Get
            If CheckBoxNomAnterior.Checked Then
                Return Xl_ContactNomAnterior.Contact
            Else
                Return Nothing
            End If

        End Get
    End Property

    Public ReadOnly Property ContactNou() As Contact
        Get
            If CheckBoxNomNou.Checked Then
                Return Xl_ContactNomNou.Contact
            Else
                Return Nothing
            End If

        End Get

    End Property

    Public ReadOnly Property Tels() As tels
        Get
            Return Xl_Tels1.Tels
        End Get
    End Property

    Public ReadOnly Property Emails() As Emails
        Get
            Return Xl_Tels1.Emails
        End Get
    End Property

    Public ReadOnly Property SubContacts() As SubContacts
        Get
            Return Xl_SubContacts1.Subcontacts
        End Get

    End Property

    Public ReadOnly Property Img48() As Bitmap
        Get
            Return Xl_Image1.Bitmap
        End Get
    End Property

    Private Sub DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles CheckBoxBotiga.CheckedChanged, CheckBoxParticular.CheckedChanged, Xl_Langs1.AfterUpdate, CheckBoxDisplayWeb.CheckedChanged, Xl_Rol1.Changed, CheckBoxNomAnterior.CheckedChanged, CheckBoxNomNou.CheckedChanged, Xl_ContactNomAnterior.AfterUpdate, Xl_ContactNomNou.AfterUpdate

        If mAllowEvents Then RaiseEvent ChangedGral()
    End Sub

    Private Sub CheckBoxNomAnterior_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNomAnterior.CheckedChanged
        Xl_ContactNomAnterior.Visible = CheckBoxNomAnterior.Checked
    End Sub

    Private Sub CheckBoxNomNou_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNomNou.CheckedChanged
        Xl_ContactNomNou.Visible = CheckBoxNomNou.Checked
    End Sub

    Private Sub Xl_Tels1_Changed()
        If mAllowEvents Then RaiseEvent ChangedTels()
    End Sub

    Private Sub Xl_SubContacts1_Changed() Handles Xl_SubContacts1.Changed
        If mAllowEvents Then RaiseEvent ChangedSubContacts()
    End Sub

    Private Sub Xl_Adr31_ChangedCit() Handles Xl_Adr31.AfterUpdateCit
        If mAllowEvents Then
            RaiseEvent ChangedCll()
            RaiseEvent ChangedClx()
            RaiseEvent ChangedAdr()
        End If
    End Sub

    Private Sub Xl_Adr31_ChangedAdr(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Adr31.AfterUpdateAdr
        If mAllowEvents Then
            RaiseEvent ChangedClx()
            RaiseEvent ChangedAdr()
        End If
    End Sub

    Private Sub CheckBoxObsoleto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxObsoleto.CheckedChanged, CheckBoxDisplayWeb.CheckedChanged
        If mAllowEvents Then
            RaiseEvent ChangedGral()
            RaiseEvent ChangedClx()
        End If
    End Sub

    Private Sub NomChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged, TextBoxCom.TextChanged
        If mAllowEvents Then
            RaiseEvent ChangedGral()
            RaiseEvent ChangedCll()
            RaiseEvent ChangedClx()
        End If
    End Sub

    Private Sub TextBoxPwd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim BlAllow As Boolean = mContact.UsrAllowed

        'If BlAllow Then
        'MsgBox(TextBoxPwd.Text, MsgBoxStyle.Information, "PASSWORD")
        'Else
        'MsgBox("No está autoritzat per aquesta operació", MsgBoxStyle.Exclamation, "M+O")
        'End If
    End Sub



    Private Sub Xl_Image1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        If mAllowEvents Then
            'mContact.Img48 = oBitmap
            RaiseEvent ChangedClx()
        End If
    End Sub

    Private Sub TextBoxSearchKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSearchKey.TextChanged
        RaiseEvent ChangedCll()
        RaiseEvent ChangedGral()
    End Sub

    Private Sub PictureBoxWebBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxWebBrowse.Click
        Dim sUrl As String = mContact.WebSite(TextBoxWeb.Text)
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxWeb.TextChanged, Xl_EANGLN.Changed
        PictureBoxWebBrowse.Visible = TextBoxWeb.Text > ""
        If mAllowEvents Then RaiseEvent ChangedGral()
    End Sub

    Private Sub Xl_NIF1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_NIF1.Changed
        RaiseEvent ChangedGral()
    End Sub
End Class
