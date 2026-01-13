

Public Class Frm_Contact
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTabs As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemExit As System.Windows.Forms.MenuItem
    Friend WithEvents TabPageMGZ As System.Windows.Forms.TabPage
    Friend WithEvents TabPageSPV As System.Windows.Forms.TabPage
    Friend WithEvents TabPageBANC As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Banc1 As Xl_Contact_Banc
    Friend WithEvents TabPageMAIL As System.Windows.Forms.TabPage
    Friend WithEvents TabPageSTAFF As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Staff1 As Xl_Contact_Staff
    Friend WithEvents TabPageCLI As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Cli1 As Xl_Contact_Cli
    Friend WithEvents TabPageREP As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Rep1 As Xl_Contact_Rep
    Friend WithEvents TabPagePRV As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Prv1 As Xl_Contact_Prv
    Friend WithEvents TabPageTrp As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Trp1 As Xl_Contact_Trp
    Friend WithEvents TabPageGRAL As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Gral1 As Xl_Contact_Gral
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Xl_Contact_Mail1 As Xl_Contact_Mail
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabPageEShop As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Eshop1 As Xl_Contact_Eshop
    Friend WithEvents Xl_Contact_Mgz1 As Xl_Contact_Mgz
    Friend WithEvents MenuItemImatges As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemDel As System.Windows.Forms.MenuItem

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItemDel = New System.Windows.Forms.MenuItem
        Me.MenuItemExit = New System.Windows.Forms.MenuItem
        Me.MenuItemImatges = New System.Windows.Forms.MenuItem
        Me.MenuItemTabs = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.TabPageMGZ = New System.Windows.Forms.TabPage
        Me.TabPageSPV = New System.Windows.Forms.TabPage
        Me.TabPageBANC = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Banc1 = New Xl_Contact_Banc
        Me.TabPageMAIL = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Mail1 = New Xl_Contact_Mail
        Me.TabPageSTAFF = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Staff1 = New Xl_Contact_Staff
        Me.TabPageCLI = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Cli1 = New Xl_Contact_Cli
        Me.TabPageREP = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Rep1 = New Xl_Contact_Rep
        Me.TabPagePRV = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Prv1 = New Xl_Contact_Prv
        Me.TabPageTrp = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Trp1 = New Xl_Contact_Trp
        Me.TabPageGRAL = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Gral1 = New Xl_Contact_Gral
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPageEShop = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Eshop1 = New Xl_Contact_Eshop
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.TabPageBANC.SuspendLayout()
        Me.TabPageMAIL.SuspendLayout()
        Me.TabPageSTAFF.SuspendLayout()
        Me.TabPageCLI.SuspendLayout()
        Me.TabPageREP.SuspendLayout()
        Me.TabPagePRV.SuspendLayout()
        Me.TabPageTrp.SuspendLayout()
        Me.TabPageGRAL.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageEShop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItemTabs, Me.MenuItem2})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemDel, Me.MenuItemExit, Me.MenuItemImatges})
        Me.MenuItem1.Text = "menú"
        '
        'MenuItemDel
        '
        Me.MenuItemDel.Index = 0
        Me.MenuItemDel.Text = "eliminar"
        '
        'MenuItemExit
        '
        Me.MenuItemExit.Index = 1
        Me.MenuItemExit.Text = "sortir"
        '
        'MenuItemImatges
        '
        Me.MenuItemImatges.Index = 2
        Me.MenuItemImatges.Text = "imatges"
        '
        'MenuItemTabs
        '
        Me.MenuItemTabs.Index = 1
        Me.MenuItemTabs.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.MenuItemTabs.Text = "pestanyes"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Text = ""
        '
        'TabPageMGZ
        '
        Me.TabPageMGZ.Controls.Add(Me.Xl_Contact_Mgz1)
        Me.TabPageMGZ.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMGZ.Name = "TabPageMGZ"
        Me.TabPageMGZ.Size = New System.Drawing.Size(192, 74)
        Me.TabPageMGZ.TabIndex = 4
        Me.TabPageMGZ.Text = "MAGATZEM"
        '
        'TabPageSPV
        '
        Me.TabPageSPV.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSPV.Name = "TabPageSPV"
        Me.TabPageSPV.Size = New System.Drawing.Size(192, 74)
        Me.TabPageSPV.TabIndex = 10
        Me.TabPageSPV.Text = "TALLER"
        '
        'TabPageBANC
        '
        Me.TabPageBANC.Controls.Add(Me.Xl_Contact_Banc1)
        Me.TabPageBANC.Location = New System.Drawing.Point(4, 22)
        Me.TabPageBANC.Name = "TabPageBANC"
        Me.TabPageBANC.Size = New System.Drawing.Size(192, 74)
        Me.TabPageBANC.TabIndex = 8
        Me.TabPageBANC.Text = "BANC"
        '
        'Xl_Contact_Banc1
        '
        Me.Xl_Contact_Banc1.Classificacio = Nothing
        Me.Xl_Contact_Banc1.Location = New System.Drawing.Point(37, 59)
        Me.Xl_Contact_Banc1.Name = "Xl_Contact_Banc1"
        Me.Xl_Contact_Banc1.Norma58Cedent = ""
        Me.Xl_Contact_Banc1.Norma58Sufixe = ""
        Me.Xl_Contact_Banc1.Size = New System.Drawing.Size(400, 334)
        Me.Xl_Contact_Banc1.TabIndex = 0
        '
        'TabPageMAIL
        '
        Me.TabPageMAIL.Controls.Add(Me.Xl_Contact_Mail1)
        Me.TabPageMAIL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMAIL.Name = "TabPageMAIL"
        Me.TabPageMAIL.Size = New System.Drawing.Size(192, 74)
        Me.TabPageMAIL.TabIndex = 5
        Me.TabPageMAIL.Text = "CORREO"
        '
        'Xl_Contact_Mail1
        '
        Me.Xl_Contact_Mail1.Location = New System.Drawing.Point(64, 42)
        Me.Xl_Contact_Mail1.Name = "Xl_Contact_Mail1"
        Me.Xl_Contact_Mail1.Size = New System.Drawing.Size(356, 94)
        Me.Xl_Contact_Mail1.TabIndex = 0
        '
        'TabPageSTAFF
        '
        Me.TabPageSTAFF.Controls.Add(Me.Xl_Contact_Staff1)
        Me.TabPageSTAFF.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSTAFF.Name = "TabPageSTAFF"
        Me.TabPageSTAFF.Size = New System.Drawing.Size(192, 74)
        Me.TabPageSTAFF.TabIndex = 3
        Me.TabPageSTAFF.Text = "PERSONAL"
        '
        'Xl_Contact_Staff1
        '
        Me.Xl_Contact_Staff1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Staff1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Staff1.Name = "Xl_Contact_Staff1"
        Me.Xl_Contact_Staff1.Size = New System.Drawing.Size(192, 74)
        Me.Xl_Contact_Staff1.TabIndex = 0
        '
        'TabPageCLI
        '
        Me.TabPageCLI.Controls.Add(Me.Xl_Contact_Cli1)
        Me.TabPageCLI.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCLI.Name = "TabPageCLI"
        Me.TabPageCLI.Size = New System.Drawing.Size(488, 494)
        Me.TabPageCLI.TabIndex = 1
        Me.TabPageCLI.Text = "CLIENT"
        '
        'Xl_Contact_Cli1
        '
        Me.Xl_Contact_Cli1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Cli1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Cli1.Name = "Xl_Contact_Cli1"
        Me.Xl_Contact_Cli1.Size = New System.Drawing.Size(488, 494)
        Me.Xl_Contact_Cli1.TabIndex = 0
        '
        'TabPageREP
        '
        Me.TabPageREP.Controls.Add(Me.Xl_Contact_Rep1)
        Me.TabPageREP.Location = New System.Drawing.Point(4, 22)
        Me.TabPageREP.Name = "TabPageREP"
        Me.TabPageREP.Size = New System.Drawing.Size(192, 74)
        Me.TabPageREP.TabIndex = 7
        Me.TabPageREP.Text = "REP"
        '
        'Xl_Contact_Rep1
        '
        Me.Xl_Contact_Rep1.Abr = ""
        Me.Xl_Contact_Rep1.ComisionReducida = 0.0!
        Me.Xl_Contact_Rep1.ComisionStandard = 0.0!
        Me.Xl_Contact_Rep1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Rep1.FchAlta = New Date(2004, 8, 30, 16, 15, 58, 998)
        Me.Xl_Contact_Rep1.FchBaja = New Date(CType(0, Long))
        Me.Xl_Contact_Rep1.IRPF = DTOProveidor.IRPFCods.standard
        Me.Xl_Contact_Rep1.IRPF_custom = 0.0!
        Me.Xl_Contact_Rep1.IVA = MaxiSrvr.Rep.IVA_Cods.exento
        Me.Xl_Contact_Rep1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Rep1.Name = "Xl_Contact_Rep1"
        Me.Xl_Contact_Rep1.Size = New System.Drawing.Size(192, 74)
        Me.Xl_Contact_Rep1.TabIndex = 0
        '
        'TabPagePRV
        '
        Me.TabPagePRV.Controls.Add(Me.Xl_Contact_Prv1)
        Me.TabPagePRV.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePRV.Name = "TabPagePRV"
        Me.TabPagePRV.Size = New System.Drawing.Size(192, 74)
        Me.TabPagePRV.TabIndex = 2
        Me.TabPagePRV.Text = "PROVEIDOR"
        '
        'Xl_Contact_Prv1
        '
        Me.Xl_Contact_Prv1.Location = New System.Drawing.Point(64, 32)
        Me.Xl_Contact_Prv1.Name = "Xl_Contact_Prv1"
        Me.Xl_Contact_Prv1.Size = New System.Drawing.Size(406, 415)
        Me.Xl_Contact_Prv1.TabIndex = 0
        '
        'TabPageTrp
        '
        Me.TabPageTrp.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageTrp.Controls.Add(Me.Xl_Contact_Trp1)
        Me.TabPageTrp.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTrp.Name = "TabPageTrp"
        Me.TabPageTrp.Size = New System.Drawing.Size(192, 74)
        Me.TabPageTrp.TabIndex = 12
        Me.TabPageTrp.Text = "TRANSPORT"
        '
        'Xl_Contact_Trp1
        '
        Me.Xl_Contact_Trp1.Location = New System.Drawing.Point(40, 32)
        Me.Xl_Contact_Trp1.Name = "Xl_Contact_Trp1"
        Me.Xl_Contact_Trp1.Size = New System.Drawing.Size(376, 272)
        Me.Xl_Contact_Trp1.TabIndex = 0
        '
        'TabPageGRAL
        '
        Me.TabPageGRAL.Controls.Add(Me.Xl_Contact_Gral1)
        Me.TabPageGRAL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGRAL.Name = "TabPageGRAL"
        Me.TabPageGRAL.Size = New System.Drawing.Size(488, 460)
        Me.TabPageGRAL.TabIndex = 0
        Me.TabPageGRAL.Text = "GENERAL"
        '
        'Xl_Contact_Gral1
        '
        Me.Xl_Contact_Gral1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Gral1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Gral1.Name = "Xl_Contact_Gral1"
        Me.Xl_Contact_Gral1.Size = New System.Drawing.Size(488, 460)
        Me.Xl_Contact_Gral1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGRAL)
        Me.TabControl1.Controls.Add(Me.TabPageTrp)
        Me.TabControl1.Controls.Add(Me.TabPagePRV)
        Me.TabControl1.Controls.Add(Me.TabPageREP)
        Me.TabControl1.Controls.Add(Me.TabPageCLI)
        Me.TabControl1.Controls.Add(Me.TabPageSTAFF)
        Me.TabControl1.Controls.Add(Me.TabPageMAIL)
        Me.TabControl1.Controls.Add(Me.TabPageBANC)
        Me.TabControl1.Controls.Add(Me.TabPageSPV)
        Me.TabControl1.Controls.Add(Me.TabPageMGZ)
        Me.TabControl1.Controls.Add(Me.TabPageEShop)
        Me.TabControl1.Location = New System.Drawing.Point(5, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        'Me.TabControl1.Size = New System.Drawing.Size(496, 520)
        Me.TabControl1.Size = New System.Drawing.Size(496, 550)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'TabPageEShop
        '
        Me.TabPageEShop.Controls.Add(Me.Xl_Contact_Eshop1)
        Me.TabPageEShop.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEShop.Name = "TabPageEShop"
        Me.TabPageEShop.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEShop.Size = New System.Drawing.Size(192, 74)
        Me.TabPageEShop.TabIndex = 13
        Me.TabPageEShop.Text = "VIRTUAL"
        Me.TabPageEShop.UseVisualStyleBackColor = True
        '
        'Xl_Contact_Eshop1
        '
        Me.Xl_Contact_Eshop1.eShop = Nothing
        Me.Xl_Contact_Eshop1.Location = New System.Drawing.Point(63, 32)
        Me.Xl_Contact_Eshop1.Name = "Xl_Contact_Eshop1"
        Me.Xl_Contact_Eshop1.Size = New System.Drawing.Size(365, 349)
        Me.Xl_Contact_Eshop1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 532)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(504, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(285, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(396, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Frm_Contact
        '
        'Me.ClientSize = New System.Drawing.Size(504, 563)
        Me.ClientSize = New System.Drawing.Size(504, 593)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Menu = Me.MainMenu1
        Me.Name = "Frm_Contact"
        Me.Text = "CONTACTE"
        Me.TabPageBANC.ResumeLayout(False)
        Me.TabPageMAIL.ResumeLayout(False)
        Me.TabPageSTAFF.ResumeLayout(False)
        Me.TabPageCLI.ResumeLayout(False)
        Me.TabPageREP.ResumeLayout(False)
        Me.TabPagePRV.ResumeLayout(False)
        Me.TabPageTrp.ResumeLayout(False)
        Me.TabPageGRAL.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageEShop.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private mContact As Contact

    Private mDirtyGral As Boolean
    Private mDirtyClx As Boolean
    Private mDirtyCll As Boolean
    Private mDirtyAdr As Boolean
    Private mDirtyTels As Boolean
    Private mDirtySubContacts As Boolean
    Private mDirtyRep As Boolean
    Private mDirtyPrv As Boolean
    Private mDirtyClient As Boolean
    Private mDirtyAdrEntregas As Boolean
    Private mDirtyMail As Boolean
    Private mDirtyStaff As Boolean
    Private mDirtyTrp As Boolean
    Private mDirtyEshop As Boolean
    Private mDirtyBanc As Boolean
    Private mDirtyTabs As Boolean

    Private mLoadedRep As Boolean
    Private mLoadedCli As Boolean
    Private mLoadedMail As Boolean
    Private mLoadedPrv As Boolean
    Private mLoadedStaff As Boolean
    Private mLoadedTrp As Boolean
    Private mLoadedMgz As Boolean
    Private mLoadedEshop As Boolean
    Private mLoadedBanc As Boolean

    Private mAllowEvents As Boolean

    Private mArrayTabs As New ArrayList

    Private Enum Tabs
        Gral
        Client
        Rep
        Proveidor
        Staff
        Transport
    End Enum

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
    End Sub

    Private Sub Frm_Contact_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If mContact.Id = 0 Then
            Me.Text = "NOU CONTACTE"
            ButtonDel.Enabled = False
        Else
            Me.Text = "CONTACTE " & mContact.Id '& " " & My.Application.AssemblyInfo.Version.ToString
            ButtonDel.Enabled = True
        End If
        LoadTabsArray()
        DisplayContact()

        'si no te logo agafal de la central
        If mContact.Img48 Is Nothing Then
            Dim oClient As Client = mContact.Client
            If oClient IsNot Nothing Then
                If oClient.CcxOrMe.Img48 IsNot Nothing Then
                    Xl_Contact_Gral1.setImgFromCcx(oClient.CcxOrMe.Img48)
                    mDirtyClx = True
                End If
            End If
        Else
        End If

        mAllowEvents = True
    End Sub

    Public ReadOnly Property Contact() As Contact
        Get
            Return mContact
        End Get
     End Property

    Private Sub LoadTabsArray()
        mArrayTabs = New ArrayList
        Dim i As Integer
        Dim oTab As TabPage
        For i = 0 To TabControl1.TabPages.Count - 1
            oTab = TabControl1.TabPages(i)
            mArrayTabs.Add(oTab)
        Next
    End Sub

    Private Sub DisplayContact()
        SetTabs()
        SetMenuItems()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedGral() Handles Xl_Contact_Gral1.ChangedGral
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyGral = True
        End If
    End Sub

    Private Sub ChangedClx() Handles Xl_Contact_Gral1.ChangedClx, Xl_Contact_Cli1.ChangedClx
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyClx = True
        End If
    End Sub

    Private Sub ChangedCll() Handles Xl_Contact_Gral1.ChangedCll, Xl_Contact_Cli1.ChangedCll
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyCll = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedAdr() Handles Xl_Contact_Gral1.ChangedAdr
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyAdr = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedTels() Handles Xl_Contact_Gral1.ChangedTels
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyTels = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedSubContacts() Handles Xl_Contact_Gral1.ChangedSubContacts
        If mAllowEvents Then
            ButtonsEnable()
            mDirtySubContacts = True
        End If
    End Sub

    Private Sub ButtonsEnable()
        If mContact.IsNew Then
            ButtonOk.Enabled = True
        Else
            Select Case BLL.BLLSession.Current.User.Rol.id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                    ButtonOk.Enabled = True
                Case DTORol.Ids.Accounts
                    Select Case mContact.Rol.Id
                        Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                        Case Else
                            ButtonOk.Enabled = True
                    End Select
                Case DTORol.Ids.Operadora, Rol.Ids.LogisticManager, Rol.Ids.SalesManager, Rol.Ids.Marketing
                    Select Case mContact.Rol.Id
                        Case DTORol.Ids.Operadora, Rol.Ids.LogisticManager, Rol.Ids.SalesManager, Rol.Ids.Marketing
                            ButtonOk.Enabled = (mContact.Id = root.Usuari.Id)
                        Case Else
                            ButtonOk.Enabled = True
                    End Select
                Case Else
            End Select
        End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oAdr As Adr = Xl_Contact_Gral1.Adr
        If oAdr.Zip Is Nothing Then
            MsgBox("falta adreça!", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If
        Dim sErr As String = ""
        If Not CheckMailEfras(sErr) Then
            MsgBox(sErr, MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If

        Dim exs as New List(Of exception)
        If UpdateChanges( exs) Then
            CheckDuplicatedAddresses()
            RaiseEvent AfterUpdate(mContact, New MatEventArgs(mContact))
            Me.Close()
        Else
            MsgBox("error al grabar el contacte:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Function CheckMailEfras(ByRef sErr As String) As Boolean
        Dim retval As Boolean
        Dim BlCliEFrasEnabled As Boolean = CheckCliEfrasEnabled()
        Dim BlEmailXefrasExists As Boolean = CheckEmailXefrasExists()

        If BlCliEFrasEnabled Then
            If BlEmailXefrasExists Then
                retval = True
            Else
                sErr = "No hi ha cap adreça e-mail per enviar les factures electroniques"
            End If
        Else
            If BlEmailXefrasExists Then
                sErr = "Hi ha una adreça e-mail per enviar factures electroniques pero el client no está habilitat per rebre-les"
            Else
                retval = True
            End If
        End If

        Return retval
    End Function

    Public Function CheckCliEfrasEnabled() As Boolean
        Return Xl_Contact_Cli1.EfrasEnabled
    End Function

    Public Function CheckEmailXefrasExists() As Boolean
        Dim retVal As Boolean = False
        For Each oEmail As Email In Xl_Contact_Gral1.Emails
            If oEmail.Efras Then
                retVal = True
                Exit For
            End If
        Next
        Return retVal
    End Function

    Private Function ConfirmTabs(ByVal oNewTabs As ContactTabs) As MsgBoxResult
        Dim rc As MsgBoxResult = MsgBoxResult.Ok
        Dim oTabsToDelete As ContactTabs = TabsDeleted(oNewTabs)
        If oTabsToDelete.Count > 0 Then
            Dim s As String = "Les següents pestanyes i totes les dades a les que fan referència serán esborrades:"
            Dim oTab As ContactTab
            For Each oTab In TabsDeleted(oNewTabs)
                s = s & vbCrLf & oTab.ToString
            Next
            rc = MsgBox(s, MsgBoxStyle.OkCancel, "MAT.NET")
        End If
        Return rc
    End Function

    Private Function TabsDeleted(ByVal oNewTabs As ContactTabs) As ContactTabs
        Dim oTabsDeleted As New ContactTabs
        Dim oOldTab As ContactTab
        Dim oNewTab As ContactTab
        Dim BlNewTabExists As Boolean
        For Each oOldTab In mContact.Tabs
            BlNewTabExists = False
            For Each oNewTab In oNewTabs
                If oNewTab.Cod = oOldTab.Cod Then
                    BlNewTabExists = True
                    Exit For
                End If
            Next
            If Not BlNewTabExists Then
                oTabsDeleted.Add(oOldTab)
            End If
        Next
        Return oTabsDeleted
    End Function



    Private Function GetContactTabs() As List(Of DTO.DTOContactTab.Tabs)
        Dim retval As New List(Of DTO.DTOContactTab.Tabs)
        Dim i As Integer
        For i = 1 To TabControl1.TabCount - 1
            Dim oTab As DTO.DTOContactTab.Tabs = GetContactTabFromTabPage(TabControl1.TabPages(i))
            retval.Add(oTab)
        Next
        Return retval
    End Function

    Private Function GetContactTabFromTabPage(ByVal oTabPage As TabPage) As DTOContactTab.Tabs
        Dim sTab As String = oTabPage.Text
        Dim retval As DTOContactTab.Tabs
        Select Case sTab
            Case TabPageCLI.Text
                retval = DTOContactTab.Tabs.CLI
            Case TabPagePRV.Text
                retval = DTOContactTab.Tabs.PRV
            Case TabPageTrp.Text
                retval = DTOContactTab.Tabs.TRP
            Case TabPageEShop.Text
                retval = DTOContactTab.Tabs.ESHOP
            Case TabPageSTAFF.Text
                retval = DTOContactTab.Tabs.STAFF
            Case TabPageMGZ.Text
                retval = DTOContactTab.Tabs.MGZ
            Case TabPageMAIL.Text
                retval = DTOContactTab.Tabs.MAIL
            Case TabPageBANC.Text
                retval = DTOContactTab.Tabs.BANC
            Case TabPageSPV.Text
                retval = DTOContactTab.Tabs.SPV
            Case TabPageREP.Text
                retval = DTOContactTab.Tabs.REP
        End Select
        Return retval
    End Function

    Private Sub SetTabs()
        Dim i As Integer
        For i = TabControl1.TabCount - 1 To 1 Step -1
            TabControl1.TabPages.RemoveAt(i)
        Next
        SetTabGral()
        Dim oTab As ContactTab
        For Each oTab In mContact.Tabs
            Select Case oTab.Cod
                Case ContactTab.Codis.CLI
                    TabControl1.TabPages.Add(TabPageCLI)
                Case ContactTab.Codis.PRV
                    TabControl1.TabPages.Add(TabPagePRV)
                Case ContactTab.Codis.STAFF
                    TabControl1.TabPages.Add(TabPageSTAFF)
                    If Not root.Usuari.AllowContactBrowse(mContact) Then
                        Xl_Contact_Staff1.Visible = False
                    End If
                Case ContactTab.Codis.MGZ
                    TabControl1.TabPages.Add(TabPageMGZ)
                Case ContactTab.Codis.MAIL
                    TabControl1.TabPages.Add(TabPageMAIL)
                Case ContactTab.Codis.BANC
                    TabControl1.TabPages.Add(TabPageBANC)
                Case ContactTab.Codis.SPV
                    TabControl1.TabPages.Add(TabPageSPV)
                Case ContactTab.Codis.REP
                    TabControl1.TabPages.Add(TabPageREP)
                Case ContactTab.Codis.TRP
                    TabControl1.TabPages.Add(TabPageTrp)
                Case ContactTab.Codis.ESHOP
                    TabControl1.TabPages.Add(TabPageEShop)
            End Select

        Next
    End Sub

    Private Sub SetTabGral()
        With Xl_Contact_Gral1
            .Contact = mContact
        End With
    End Sub

    Private Sub SetTabCli()

        If mContact.Id = 0 Then
            Dim rc As MsgBoxResult = MsgBox("Grabem primer les dades entrades?", MsgBoxStyle.OkCancel)
            If rc = MsgBoxResult.Cancel Then Exit Sub

            Dim exs as New List(Of exception)
            If Not UpdateChanges( exs) Then
                MsgBox("error al grabar el contacte:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
            RaiseEvent AfterUpdate(mContact, New System.EventArgs)
        End If
        With Xl_Contact_Cli1

            If mContact.Client.Exists Then
                .Client = mContact.Client
            Else
                .Client = mContact.DefaultClient
                .SetDirty()
            End If
        End With
    End Sub

    Private Sub GetTabGral()
        With mContact
            .Nom = Xl_Contact_Gral1.Nom
            .NomComercial = Xl_Contact_Gral1.NomComercial
            .NIF = Xl_Contact_Gral1.NIF
            .NomKey = Xl_Contact_Gral1.NomKey
            .Obsoleto = Xl_Contact_Gral1.Obsoleto
            .Botiga = Xl_Contact_Gral1.Botiga
            .Particular = Xl_Contact_Gral1.Particular
            .Lang = Xl_Contact_Gral1.Lang
            .Adr = Xl_Contact_Gral1.Adr
            .Rol = Xl_Contact_Gral1.Rol
            .Web = Xl_Contact_Gral1.Web
            .ShowWeb = Xl_Contact_Gral1.DisplayWeb
            .Gln = Xl_Contact_Gral1.Gln
            .ContactAnterior = Xl_Contact_Gral1.ContactAnterior
            .ContactNou = Xl_Contact_Gral1.ContactNou
            .Tels = Xl_Contact_Gral1.Tels
            .SubContacts = Xl_Contact_Gral1.SubContacts
            .Img48 = Xl_Contact_Gral1.Img48
        End With
    End Sub

    Private Sub GetTabCli()
        With mContact.Client
            .Ccx = Xl_Contact_Cli1.Ccx
            .Referencia = Xl_Contact_Cli1.Referencia
            .OrdersToCentral = Xl_Contact_Cli1.OrdersToCentral
            .FrasIndependents = Xl_Contact_Cli1.FrasIndependents
            .NoSsc = Xl_Contact_Cli1.NoSsc
            .NoRep = Xl_Contact_Cli1.NoRep
            .NoWeb = Xl_Contact_Cli1.NoWeb
            .NoIncentius = Xl_Contact_Cli1.NoIncentius
            .GrandesCuentas = Xl_Contact_Cli1.GrandesCuentas
            .EShopOnly = Xl_Contact_Cli1.EShopOnly
            .QuotaOnline = Xl_Contact_Cli1.QuotaOnline
            .IVA = Xl_Contact_Cli1.IVA
            .REQ = Xl_Contact_Cli1.REQ
            .CopiasFra = Xl_Contact_Cli1.CopiasFra
            .ValorarAlbarans = Xl_Contact_Cli1.ValorarAlbarans
            .Dto = Xl_Contact_Cli1.Dto
            .Dpp = Xl_Contact_Cli1.Dpp
            .Tarifa = Xl_Contact_Cli1.Tarifa
            .PortsCondicions = Xl_Contact_Cli1.PortsCondicions
            .PortsCod = Xl_Contact_Cli1.PortsCod
            .CashCod = Xl_Contact_Cli1.CashCod
            .SuProveedorNum = Xl_Contact_Cli1.SuProveedorNum
            .WarnAlbs = Xl_Contact_Cli1.WarnAlbs
            .ObsComercials = Xl_Contact_Cli1.ObsComercials
            .CodAlbsXFra = Xl_Contact_Cli1.CodAlbsXFra
            .FpgIndependent = Xl_Contact_Cli1.FpgIndependent
            .FormaDePago = Xl_Contact_Cli1.FormaDePago
            .NoAsnef = Xl_Contact_Cli1.NoAsnef
            .DeliveryPlatform = Xl_Contact_Cli1.DeliveryPlatform
            '.CreditLimit = Xl_Contact_Cli1.CreditLimit.Amt
            'Xl_Contact_Cli1.DeliveryAdr.Update(mContact, Adr.Codis.Fiscal)
        End With
    End Sub

    Private Sub GetTabMail()
        'Xl_Contact_Gral1.Adr
    End Sub

    Private Sub SetTabMail()
        Xl_Contact_Mail1.Adr = mContact.MailAdr
    End Sub

    Private Sub SetTabPrv()
        Xl_Contact_Prv1.Proveidor = mContact.Proveidor
    End Sub

    Private Sub GetTabPrv()
        With mContact.Proveidor
            .DefaultCtaCarrec = Xl_Contact_Prv1.Cta
            .DefaultCur = Xl_Contact_Prv1.Cur
            .IRPF_Cod = Xl_Contact_Prv1.IRPF_Cod
            .FormaDePago = Xl_Contact_Prv1.FormaDePago
            .Incoterm = Xl_Contact_Prv1.Incoterm
            .CodiMercancia = Xl_Contact_Prv1.CodiMercancia
        End With
    End Sub

    Private Sub SetTabBanc()
        With Xl_Contact_Banc1
            .Banc = mContact.Banc
            '.Iban = mContact.Banc.Iban
            '.Norma58Cedent = mContact.Banc.Norma58cedent
            '.Norma58Sufixe = mContact.Banc.Norma58sufixe
            '.Classificacio = mContact.Banc.Classificacio
            '.AllowEvents()
        End With
    End Sub

    Private Sub GetTabBanc()
        With mContact.Banc
            .Abr = Xl_Contact_Banc1.Abr
            .Iban = Xl_Contact_Banc1.Iban
            .Norma58cedent = Xl_Contact_Banc1.Norma58Cedent
            .Norma58sufixe = Xl_Contact_Banc1.Norma58Sufixe
            .Classificacio = Xl_Contact_Banc1.Classificacio
            .ModeCcaImpago = Xl_Contact_Banc1.ModeCcaImpago
        End With
    End Sub

    Private Sub SetTabRep()
        Dim oRep As MaxiSrvr.Rep = mContact.Rep
        With Xl_Contact_Rep1
            .Abr = oRep.Abr
            .FchAlta = oRep.FchAlta
            .FchBaja = oRep.FchBaja
            .Rep = oRep
            .ComisionStandard = oRep.ComisionStandard
            .ComisionReducida = oRep.ComisionReducida
            .IVA = oRep.IVA_Cod
            .IRPF = oRep.IRPF_Cod
            .IRPF_custom = oRep.IRPF_Custom
            .Ccx = oRep.Ccx
            .IBAN = oRep.Iban
            .Foto = oRep.Foto
            .AllowEvents()
        End With
    End Sub

    Private Sub GetTabRep()
        Dim oRep As MaxiSrvr.Rep = mContact.Rep
        With oRep
            .Abr = Xl_Contact_Rep1.Abr
            .FchAlta = Xl_Contact_Rep1.FchAlta
            .FchBaja = Xl_Contact_Rep1.FchBaja
            '.RepZonas = Xl_Contact_Rep1.RepZonas
            .ComisionStandard = Xl_Contact_Rep1.ComisionStandard
            .ComisionReducida = Xl_Contact_Rep1.ComisionReducida
            .IVA_Cod = Xl_Contact_Rep1.IVA
            .IRPF_Cod = Xl_Contact_Rep1.IRPF
            .IRPF_Custom = Xl_Contact_Rep1.IRPF_custom

            Dim oContact As New DTOContact(oRep.Guid)
            .Iban = Xl_Contact_Rep1.IBAN
            .Ccx = Xl_Contact_Rep1.Ccx
            .Foto = Xl_Contact_Rep1.Foto
        End With
    End Sub


    Private Sub SetTabStaff()
        Xl_Contact_Staff1.Staff = mContact.Staff
    End Sub

    Private Sub GetTabStaff()
        With mContact.Staff
            .NomAlias = Xl_Contact_Staff1.NomAlias
            .NumSS = Xl_Contact_Staff1.NumSS
            .LaboralCategoria = Xl_Contact_Staff1.laboralCategoria
            .Sex = Xl_Contact_Staff1.Sex
            .Born = Xl_Contact_Staff1.Born
            .Alta = Xl_Contact_Staff1.Alta
            .Baixa = Xl_Contact_Staff1.Baixa
            .Iban = Xl_Contact_Staff1.IBAN
        End With
    End Sub

    Private Sub SetTabTrp()
        Xl_Contact_Trp1.Transportista = mContact.Transportista
    End Sub

    Private Sub GetTabTrp()
        With mContact.Transportista
            .Activat = Xl_Contact_Trp1.Activated
            .Abr = Xl_Contact_Trp1.Nom
            .Cubicaje = Xl_Contact_Trp1.Cubicatje
            .CompensaPercent = Xl_Contact_Trp1.Factor
            .NoCubicarPerSotaDe = Xl_Contact_Trp1.NoCubicarPerSotaDe
            .AllowReembolsos = Xl_Contact_Trp1.AllowReembolsos
            .TransportaMobiliari = Xl_Contact_Trp1.TransportaMobiliari
        End With
    End Sub

    Private Sub SetTabMgz()
        'Xl_Contact_Mgz1.Mgz = MaxiSrvr.Mgz.FromNum(mContact.Emp, mContact.Id)
    End Sub

    Private Sub GetTabMgz()
        'mContact.Mgz = Xl_Contact_mgz1.mgz
    End Sub

    Private Sub SetTabEshop()
        Xl_Contact_Eshop1.eShop = mContact.Eshop
    End Sub

    Private Sub GetTabEshop()
        mContact.Eshop = Xl_Contact_Eshop1.eShop
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If mAllowEvents Then
            Select Case TabControl1.SelectedTab.Text
                Case ""
                Case TabPageCLI.Text
                    If Not mLoadedCli Then
                        mLoadedCli = True
                        SetTabCli()
                    End If

                Case TabPageMAIL.Text
                    If Not mLoadedMail Then
                        mLoadedMail = True
                        SetTabMail()
                    End If

                Case TabPagePRV.Text
                    If Not mLoadedPrv Then
                        mLoadedPrv = True
                        SetTabPrv()
                    End If

                Case TabPageREP.Text
                    If Not mLoadedRep Then
                        mLoadedRep = True
                        SetTabRep()
                    End If

                Case TabPageTrp.Text
                    If Not mLoadedTrp Then
                        mLoadedTrp = True
                        SetTabTrp()
                    End If

                Case TabPageMGZ.Text
                    If Not mLoadedMgz Then
                        mLoadedMgz = True
                        SetTabMgz()
                    End If

                Case TabPageEShop.Text
                    If Not mLoadedEshop Then
                        mLoadedEshop = True
                        SetTabEshop()
                    End If

                Case TabPageBANC.Text
                    If Not mLoadedBanc Then
                        mLoadedBanc = True
                        SetTabBanc()
                    End If

                Case TabPageSTAFF.Text
                    If Not mLoadedStaff Then
                        mLoadedStaff = True
                        Select Case BLL.BLLSession.Current.User.Rol.id
                            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                                SetTabStaff()
                        End Select
                    End If
            End Select

        End If
    End Sub

    Private Sub Xl_Contact_Rep1_Changed() Handles Xl_Contact_Rep1.Changed
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyRep = True
        End If
    End Sub


    Private Sub Xl_Contact_Cli1_ChangedClient() Handles Xl_Contact_Cli1.ChangedClient
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyClient = True
        End If
    End Sub

    Private Sub Xl_Contact_Cli1_ChangedAdrEntregas() Handles Xl_Contact_Cli1.ChangedAdrEntregas
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyAdrEntregas = True
        End If
    End Sub

    Private Sub Xl_Contact_Prv1_AfterUpdate() Handles Xl_Contact_Prv1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyPrv = True
        End If
    End Sub

    Private Sub Xl_Contact_Mail1_AfterUpdate() Handles Xl_Contact_Mail1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyMail = True
        End If
    End Sub

    Private Sub MenuItemTab_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As MenuItem = CType(sender, MenuItem)
        Dim sTabCaption As String = oMenuItem.Text
        Select Case oMenuItem.Checked
            Case True
                If TabRemove(sTabCaption) Then
                    oMenuItem.Checked = False
                End If
            Case False
                If TabAdd(sTabCaption) Then
                    oMenuItem.Checked = True
                End If
        End Select
        mDirtyTabs = True
        ButtonsEnable()
    End Sub

    Private Function TabAdd(ByVal sTabCaption As String) As Boolean
        Dim retval As Boolean
        Dim oTab As TabPage
        Dim i As Integer
        For i = 1 To mArrayTabs.Count - 1
            oTab = mArrayTabs(i)
            If oTab.Text = sTabCaption Then
                TabControl1.TabPages.Add(oTab)
                Select Case sTabCaption
                    Case TabPageCLI.Text
                        mDirtyClient = True
                End Select
                retval = True
            End If
        Next
        Return retval
    End Function

    Private Function TabRemove(ByVal sTabCaption As String) As Boolean
        Dim retval As Boolean
        Dim oTab As TabPage
        Dim i As Integer
        For i = TabControl1.TabCount - 1 To 1 Step -1
            oTab = TabControl1.TabPages(i)
            If oTab.Text = sTabCaption Then
                TabControl1.TabPages.RemoveAt(i)
                retval = True
            End If
        Next
        Return retval
    End Function

    Private Sub SetMenuItems()
        Dim oMenuItem As MenuItem
        MenuItemTabs.MenuItems.Clear()
        Dim oTab As TabPage
        Dim i As Integer
        For Each oTab In mArrayTabs
            oMenuItem = New MenuItem(oTab.Text, New EventHandler(AddressOf MenuItemTab_Click))
            MenuItemTabs.MenuItems.Add(oMenuItem)
            For i = TabControl1.TabCount - 1 To 1 Step -1
                If oTab.Text = TabControl1.TabPages(i).Text Then
                    oMenuItem.Checked = True
                    Exit For
                End If
            Next
        Next
    End Sub


    Private Sub Xl_Contact_Trp1_AfterUpdate() Handles Xl_Contact_Trp1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyTrp = True
        End If
    End Sub

    Private Sub Xl_Contact_Eshop1_AfterUpdate() Handles Xl_Contact_Eshop1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyEshop = True
        End If
    End Sub

    Private Sub Xl_Contact_Banc1_AfterUpdate() Handles Xl_Contact_Banc1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyBanc = True
        End If
    End Sub

    Private Sub Xl_Contact_Staff1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact_Staff1.AfterUpdate
        If mAllowEvents Then
            ButtonsEnable()
            mDirtyStaff = True
        End If
    End Sub

    Private Function UpdateChanges(ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean
        GetTabGral()

        With mContact
            Dim oBaseAdr As Adr = Xl_Contact_Gral1.Adr

            If mDirtyGral Then .UpdateGral( exs)
            If mDirtyAdr Then .UpdateAdr()
            If mDirtyAdrEntregas Then
                Dim oDlvrAdr As Adr = Xl_Contact_Cli1.DeliveryAdr()
                If oBaseAdr.Equals(oDlvrAdr) Then
                    Adr.Delete(mContact, Adr.Codis.Entregas)
                Else
                    If oDlvrAdr.Zip Is Nothing Then
                        Adr.Delete(mContact, Adr.Codis.Entregas)
                    Else
                        oDlvrAdr.Update(mContact, Adr.Codis.Entregas)
                    End If
                End If
            End If

            If mDirtyTels Then
                .UpdateTels()
                .UpdateEmails( exs)
            End If
            If mDirtySubContacts Then
                .UpdateSubContacts( exs)
            End If

            If mDirtyTabs Then
                Dim oTabs As List(Of DTOContactTab.Tabs) = GetContactTabs()
                If BLL.BLLContactTabs.Update(New DTOContact(mContact.Guid), oTabs, exs) Then
                Else
                    UIHelper.WarnError(exs, "error al desar les pestanyes")
                End If
            End If

            If mDirtyClient Then
                GetTabCli()
                exs = New List(Of Exception)
                If Not mContact.Client.Update(exs) Then
                    MsgBox("Error al desar la fitxa de client" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End If

            If mDirtyMail Then
                Dim oMailAdr As Adr = Xl_Contact_Mail1.Adr
                If oBaseAdr.Equals(oMailAdr) Then
                    Adr.Delete(mContact, Adr.Codis.Correspondencia)
                Else
                    oMailAdr.Update(mContact, Adr.Codis.Correspondencia)
                End If
            End If
            If mDirtyPrv Then
                GetTabPrv()
                Dim oEx As Exception = mContact.Proveidor.UpdatePrv
                If Not oEx Is Nothing Then MsgBox(oEx.Message)
            End If
            If mDirtyRep Then
                GetTabRep()
                exs = New List(Of Exception)
                If Not mContact.Rep.Update(exs) Then
                    MsgBox("Error al desar la fitxa de representant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End If
            If mDirtyTrp Then
                GetTabTrp()
                If Not mContact.Transportista.Update(exs) Then
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End If
            If mDirtyEshop Then
                GetTabEshop()
                mContact.Eshop.Update()
            End If
            If mDirtyBanc Then
                GetTabBanc()
                exs = New List(Of Exception)
                If Not mContact.Banc.Update(exs) Then
                    MsgBox("error al desar la fitxa de banc" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
            If mDirtyStaff Then
                GetTabStaff()
                exs = New List(Of Exception)
                If Not mContact.Staff.Update(exs) Then
                    MsgBox("error al desar la fitxa de personal" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If

            If mDirtyClx Then .UpdateClx(exs)
            If mDirtyCll Then .UpdateCll(exs)

        End With

        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(mContact, MatEventArgs.Empty)
            ButtonOk.Enabled = False
        Else
            MsgBox( BLL.Defaults.ExsToMultiline(exs))
        End If
        Me.Text = "CONTACTE " & mContact.Id '& " " & My.Application.AssemblyInfo.Version.ToString

        retval = ( exs.Count = 0)
        Return retval
    End Function

    Private Sub CheckDuplicatedAddresses()
        Dim oBaseAdr As Adr = mContact.GetAdr(Adr.Codis.Fiscal)
        Dim oDeliveryAdr As Adr = mContact.GetAdr(Adr.Codis.Entregas)
        Dim oMailAdr As Adr = mContact.GetAdr(Adr.Codis.Correspondencia)

        If oBaseAdr.Equals(oDeliveryAdr) Then
            MaxiSrvr.Adr.Delete(mContact, Adr.Codis.Entregas)
        End If

        If oBaseAdr.Equals(oMailAdr) Then
            MaxiSrvr.Adr.Delete(mContact, Adr.Codis.Correspondencia)
        End If

    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim RC As MsgBoxResult = MsgBox("ELIMINEM FITXA " & mContact.Id & " DE " & mContact.Clx & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If RC = MsgBoxResult.Ok Then
            Dim sObs As String = ""
            If mContact.AllowDelete(sObs) Then
                Dim exs as New List(Of exception)
                If mContact.Delete( exs) Then
                    mContact = Nothing
                    RaiseEvent AfterUpdate(mContact, New System.EventArgs)
                    Me.Close()
                Else
                    MsgBox("error al eliminar el contacte:" & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox(sObs)
            End If
        End If
    End Sub



    Private Sub MenuItemImatges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImatges.Click
        Dim oFrm As New Frm_CliImgs
        With oFrm
            .Contact = mContact
            .Show()
        End With
    End Sub

 

End Class
