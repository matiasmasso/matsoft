

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
    Friend WithEvents MenuItemImatges As System.Windows.Forms.MenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuItemDel As System.Windows.Forms.MenuItem

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItemDel = New System.Windows.Forms.MenuItem()
        Me.MenuItemExit = New System.Windows.Forms.MenuItem()
        Me.MenuItemImatges = New System.Windows.Forms.MenuItem()
        Me.MenuItemTabs = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.TabPageMGZ = New System.Windows.Forms.TabPage()
        Me.TabPageSPV = New System.Windows.Forms.TabPage()
        Me.TabPageBANC = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Banc1 = New Xl_Contact_Banc()
        Me.TabPageMAIL = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Mail1 = New Xl_Contact_Mail()
        Me.TabPageSTAFF = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Staff1 = New Xl_Contact_Staff()
        Me.TabPageCLI = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Cli1 = New Xl_Contact_Cli()
        Me.TabPageREP = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Rep1 = New Xl_Contact_Rep()
        Me.TabPagePRV = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Prv1 = New Xl_Contact_Prv()
        Me.TabPageTrp = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Trp1 = New Xl_Contact_Trp()
        Me.TabPageGRAL = New System.Windows.Forms.TabPage()
        Me.Xl_Contact_Gral1 = New Xl_Contact_Gral()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabPageBANC.SuspendLayout()
        Me.TabPageMAIL.SuspendLayout()
        Me.TabPageSTAFF.SuspendLayout()
        Me.TabPageCLI.SuspendLayout()
        Me.TabPageREP.SuspendLayout()
        Me.TabPagePRV.SuspendLayout()
        Me.TabPageTrp.SuspendLayout()
        Me.TabPageGRAL.SuspendLayout()
        Me.TabControl1.SuspendLayout()
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
        Me.TabPageMGZ.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMGZ.Name = "TabPageMGZ"
        Me.TabPageMGZ.Size = New System.Drawing.Size(488, 495)
        Me.TabPageMGZ.TabIndex = 13
        '
        'TabPageSPV
        '
        Me.TabPageSPV.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSPV.Name = "TabPageSPV"
        Me.TabPageSPV.Size = New System.Drawing.Size(488, 495)
        Me.TabPageSPV.TabIndex = 10
        Me.TabPageSPV.Text = "TALLER"
        '
        'TabPageBANC
        '
        Me.TabPageBANC.Controls.Add(Me.Xl_Contact_Banc1)
        Me.TabPageBANC.Location = New System.Drawing.Point(4, 22)
        Me.TabPageBANC.Name = "TabPageBANC"
        Me.TabPageBANC.Size = New System.Drawing.Size(488, 495)
        Me.TabPageBANC.TabIndex = 8
        Me.TabPageBANC.Text = "BANC"
        '
        'Xl_Contact_Banc1
        '
        Me.Xl_Contact_Banc1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Banc1.Name = "Xl_Contact_Banc1"
        Me.Xl_Contact_Banc1.Size = New System.Drawing.Size(400, 494)
        Me.Xl_Contact_Banc1.TabIndex = 0
        '
        'TabPageMAIL
        '
        Me.TabPageMAIL.Controls.Add(Me.Xl_Contact_Mail1)
        Me.TabPageMAIL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageMAIL.Name = "TabPageMAIL"
        Me.TabPageMAIL.Size = New System.Drawing.Size(488, 495)
        Me.TabPageMAIL.TabIndex = 5
        Me.TabPageMAIL.Text = "CORREO"
        '
        'Xl_Contact_Mail1
        '
        Me.Xl_Contact_Mail1.Address = Nothing
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
        Me.TabPageSTAFF.Size = New System.Drawing.Size(488, 495)
        Me.TabPageSTAFF.TabIndex = 3
        Me.TabPageSTAFF.Text = "PERSONAL"
        '
        'Xl_Contact_Staff1
        '
        Me.Xl_Contact_Staff1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Staff1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Staff1.Name = "Xl_Contact_Staff1"
        Me.Xl_Contact_Staff1.Size = New System.Drawing.Size(488, 495)
        Me.Xl_Contact_Staff1.TabIndex = 0
        '
        'TabPageCLI
        '
        Me.TabPageCLI.Controls.Add(Me.Xl_Contact_Cli1)
        Me.TabPageCLI.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCLI.Name = "TabPageCLI"
        Me.TabPageCLI.Size = New System.Drawing.Size(488, 495)
        Me.TabPageCLI.TabIndex = 1
        Me.TabPageCLI.Text = "CLIENT"
        '
        'Xl_Contact_Cli1
        '
        Me.Xl_Contact_Cli1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Cli1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Cli1.Name = "Xl_Contact_Cli1"
        Me.Xl_Contact_Cli1.Size = New System.Drawing.Size(488, 495)
        Me.Xl_Contact_Cli1.TabIndex = 0
        '
        'TabPageREP
        '
        Me.TabPageREP.Controls.Add(Me.Xl_Contact_Rep1)
        Me.TabPageREP.Location = New System.Drawing.Point(4, 22)
        Me.TabPageREP.Name = "TabPageREP"
        Me.TabPageREP.Size = New System.Drawing.Size(488, 495)
        Me.TabPageREP.TabIndex = 7
        Me.TabPageREP.Text = "REP"
        '
        'Xl_Contact_Rep1
        '
        Me.Xl_Contact_Rep1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Rep1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Rep1.Name = "Xl_Contact_Rep1"
        Me.Xl_Contact_Rep1.Size = New System.Drawing.Size(488, 495)
        Me.Xl_Contact_Rep1.TabIndex = 0
        '
        'TabPagePRV
        '
        Me.TabPagePRV.Controls.Add(Me.Xl_Contact_Prv1)
        Me.TabPagePRV.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePRV.Name = "TabPagePRV"
        Me.TabPagePRV.Size = New System.Drawing.Size(488, 495)
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
        Me.TabPageTrp.Size = New System.Drawing.Size(488, 495)
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
        Me.TabPageGRAL.Controls.Add(Me.MenuStrip1)
        Me.TabPageGRAL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGRAL.Name = "TabPageGRAL"
        Me.TabPageGRAL.Size = New System.Drawing.Size(488, 495)
        Me.TabPageGRAL.TabIndex = 0
        Me.TabPageGRAL.Text = "GENERAL"
        '
        'Xl_Contact_Gral1
        '
        Me.Xl_Contact_Gral1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Gral1.Location = New System.Drawing.Point(0, 24)
        Me.Xl_Contact_Gral1.Name = "Xl_Contact_Gral1"
        Me.Xl_Contact_Gral1.Size = New System.Drawing.Size(488, 471)
        Me.Xl_Contact_Gral1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(488, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
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
        Me.TabControl1.Location = New System.Drawing.Point(5, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(496, 521)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 533)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(504, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
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
        Me.ButtonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
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
        Me.ButtonDel.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
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
        Me.ClientSize = New System.Drawing.Size(504, 564)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.MainMenuStrip = Me.MenuStrip1
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
        Me.TabPageGRAL.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private _Contact As DTOContact

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
    Private mDirtyStaff As Boolean
    Private mDirtyBanc As Boolean
    Private mDirtyTrp As Boolean

    Private _Tabs As List(Of DTOContact.Tabs)
    Private _AllowEvents As Boolean


    Public Sub New(ByVal oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Contact_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Contact.IsLoaded = False
        If FEB.Contact.Load(_Contact, exs) Then
            setTitle()

            If Await LoadTabs(exs) Then
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub setTitle()
        If _Contact.IsNew Then
            Me.Text = "Nou contacte"
            ButtonDel.Enabled = False
        Else
            Me.Text = String.Format("Contacte {0} {1}", _Contact.Id, _Contact.FullNom)
            ButtonDel.Enabled = True
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedGral() Handles Xl_Contact_Gral1.ChangedGral
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyGral = True
        End If
    End Sub

    Private Sub ChangedClx() Handles Xl_Contact_Gral1.ChangedClx, Xl_Contact_Cli1.ChangedClx
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyClx = True
        End If
    End Sub

    Private Sub ChangedCll() Handles Xl_Contact_Gral1.ChangedCll, Xl_Contact_Cli1.ChangedCll
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyCll = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedAdr() Handles Xl_Contact_Gral1.ChangedAdr
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyAdr = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedTels() Handles Xl_Contact_Gral1.ChangedTels
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyTels = True
        End If
    End Sub

    Private Sub Xl_Contact_Gral1_ChangedSubContacts() Handles Xl_Contact_Gral1.ChangedSubContacts
        If _AllowEvents Then
            ButtonsEnable()
            mDirtySubContacts = True
        End If
    End Sub

    Private Sub ButtonsEnable()
        If _Contact.IsNew Then
            ButtonOk.Enabled = True
        Else
            Select Case Current.Session.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                    ButtonOk.Enabled = True
                Case DTORol.Ids.Accounts
                    Select Case _Contact.Rol.Id
                        Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                        Case Else
                            ButtonOk.Enabled = True
                    End Select
                Case DTORol.Ids.Operadora, DTORol.Ids.LogisticManager, DTORol.Ids.SalesManager, DTORol.Ids.Marketing
                    Select Case _Contact.Rol.Id
                        Case DTORol.Ids.Operadora, DTORol.Ids.LogisticManager, DTORol.Ids.SalesManager, DTORol.Ids.Marketing
                            ButtonOk.Enabled = Current.Session.User.Contacts.Any(Function(x) x.Equals(_Contact))
                        Case Else
                            ButtonOk.Enabled = True
                    End Select
                Case Else
            End Select
        End If

    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oContact As DTOContact = Xl_Contact_Gral1.Contact
        If oContact.Address Is Nothing Then
            MsgBox("falta adreça!", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        ElseIf oContact.Address.Zip Is Nothing Then
            MsgBox("falta adreça!", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If

        Dim sErr As String = ""
        'If Not CheckMailEfras(sErr) Then
        'MsgBox(sErr, MsgBoxStyle.Exclamation, "MAT.NET")
        'Exit Sub
        'End If

        UIHelper.ToggleProggressBar(Panel1, True)
        Dim exs As New List(Of Exception)
        If Await UpdateChanges(exs) Then
            CheckDuplicatedAddresses()
            RaiseEvent AfterUpdate(_Contact, New MatEventArgs(_Contact))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            MsgBox("error al grabar el contacte:" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
        End If
    End Sub



#Region "LoadData"

    Private Async Function LoadTabs(exs As List(Of Exception)) As Task(Of Boolean)
        ClearTabs()

        _Tabs = Await FEB.Contact.Tabs(exs, _Contact)
        If exs.Count = 0 Then
            LoadTabMenuItems(_Tabs)
            For Each oTab As DTOContact.Tabs In _Tabs
                Await AddTab(exs, oTab)
            Next
        Else
            UIHelper.WarnError(exs)
        End If

        Return exs.Count = 0
    End Function

    Private Async Function AddTab(exs As List(Of Exception), oTab As DTOContact.Tabs) As Task
        Select Case oTab
            Case DTOContact.Tabs.General
                loadGral(exs)
            Case DTOContact.Tabs.Client
                loadClient(exs)
            Case DTOContact.Tabs.Proveidor
                Await LoadProveidor(exs)
            Case DTOContact.Tabs.Staff
                If FEB.User.IsAllowedToRead(Current.Session.User, _Contact) Then
                    Await loadStaff(exs)
                End If
            Case DTOContact.Tabs.Banc
                loadBanc(exs)
            Case DTOContact.Tabs.Rep
                Await loadRep(exs)
            Case DTOContact.Tabs.Transportista
                loadTrp(exs)
        End Select
    End Function

    Private Sub RemoveTab(oTab As DTOContact.Tabs)
        For i = 1 To TabControl1.TabPages.Count - 1
            If TabControl1.TabPages(i).Tag = oTab Then
                TabControl1.TabPages.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub LoadTabMenuItems(oTabs As List(Of DTOContact.Tabs))
        For Each oTab In [Enum].GetValues(GetType(DTOContact.Tabs))
            If oTab <> DTOContact.Tabs.General Then
                Dim oMenuItem As New MenuItem(oTab.ToString, AddressOf onTablistUpdate)
                oMenuItem.Checked = oTabs.Contains(oTab)
                oMenuItem.Tag = oTab
                MenuItemTabs.MenuItems.Add(oMenuItem)
            End If
        Next
    End Sub

    Private Async Sub onTablistUpdate(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oMenuItem As MenuItem = sender
        Dim oTab = oMenuItem.Tag
        oMenuItem.Checked = Not oMenuItem.Checked
        If oMenuItem.Checked Then
            Await AddTab(exs, oTab)
        Else
            RemoveTab(oTab)
        End If
        If exs.Count = 0 Then
            ButtonsEnable()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ClearTabs()
        For i = TabControl1.TabCount - 1 To 0 Step -1
            'For i = TabControl1.TabCount - 1 To 1 Step -1
            TabControl1.TabPages.RemoveAt(i)
        Next
    End Sub


    Private Sub AddTabPage(oPage As TabPage, oTab As DTOContact.Tabs)
        Dim done As Boolean
        oPage.Tag = oTab
        For i = 0 To TabControl1.TabPages.Count - 1
            If TabControl1.TabPages(i).Tag > oPage.Tag Then
                TabControl1.TabPages.Insert(i, oPage)
                done = True
                Exit For
            End If
        Next
        If Not done Then
            TabControl1.TabPages.Add(oPage)
        End If
    End Sub

    Private Sub loadGral(exs As List(Of Exception))
        AddTabPage(TabPageGRAL, DTOContact.Tabs.General)
        'If FEB.Contact.Load(_Contact, exs, includeLogo:=True) Then
        Xl_Contact_Gral1.Load(exs, _Contact)
    End Sub

    Private Sub loadClient(exs As List(Of Exception))
        AddTabPage(TabPageCLI, DTOContact.Tabs.Client)
        Dim oCustomer As New DTOCustomer(_Contact.Guid)
        If FEB.Customer.Load(oCustomer, exs) Then
            Xl_Contact_Cli1.load(oCustomer)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadProveidor(exs As List(Of Exception)) As Task
        AddTabPage(TabPagePRV, DTOContact.Tabs.Proveidor)
        Dim oProveidor = DTOProveidor.FromContact(_Contact)
        If FEB.Proveidor.Load(oProveidor, exs) Then
            Await Xl_Contact_Prv1.Load(oProveidor)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function loadStaff(exs As List(Of Exception)) As Task
        AddTabPage(TabPageSTAFF, DTOContact.Tabs.Staff)
        Dim oStaff As New DTOStaff(_Contact.Guid)
        If FEB.Staff.Load(exs, oStaff) Then
            Await Xl_Contact_Staff1.Load(oStaff)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function loadRep(exs As List(Of Exception)) As Task
        AddTabPage(TabPageREP, DTOContact.Tabs.Rep)
        Dim oRep = Await FEB.Rep.Find(_Contact.Guid, exs)
        Await Xl_Contact_Rep1.Load(oRep)
    End Function

    Private Sub loadTrp(exs As List(Of Exception))
        AddTabPage(TabPageTrp, DTOContact.Tabs.Transportista)
        Dim oTrp = DTOTransportista.FromContact(_Contact)
        If FEB.Transportista.Load(oTrp, exs) Then
            Xl_Contact_Trp1.Load(oTrp)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub loadBanc(exs As List(Of Exception))
        AddTabPage(TabPageBANC, DTOContact.Tabs.Banc)
        Dim oBanc As New DTOBanc(_Contact.Guid)
        If FEB.Banc.Load(oBanc, exs) Then
            Xl_Contact_Banc1.Load(oBanc)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

#End Region


#Region "SetDirty"

    Private Sub Xl_Contact_Cli1_ChangedClient() Handles Xl_Contact_Cli1.ChangedClient
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyClient = True
        End If
    End Sub

    Private Sub Xl_Contact_Cli1_ChangedAdrEntregas() Handles Xl_Contact_Cli1.ChangedAdrEntregas
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyAdrEntregas = True
        End If
    End Sub


    Private Sub Xl_Contact_Rep1_Changed() Handles Xl_Contact_Rep1.AfterUpdate
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyRep = True
        End If
    End Sub



    Private Sub Xl_Contact_Prv1_AfterUpdate() Handles Xl_Contact_Prv1.AfterUpdate
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyPrv = True
        End If
    End Sub


    Private Sub Xl_Contact_Banc1_AfterUpdate() Handles Xl_Contact_Banc1.AfterUpdate
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyBanc = True
        End If
    End Sub

    Private Sub Xl_Contact_Trp1_AfterUpdate() Handles Xl_Contact_Trp1.AfterUpdate
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyTrp = True
        End If
    End Sub

    Private Sub Xl_Contact_Staff1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact_Staff1.AfterUpdate
        If _AllowEvents Then
            ButtonsEnable()
            mDirtyStaff = True
        End If
    End Sub

#End Region

    Private Async Function UpdateChanges(exs As List(Of Exception)) As Task(Of Boolean)

        Dim retval As Boolean

        Await DeleteRemovedTabs()

        Dim sFullNom As String = _Contact.FullNom

        If mDirtyClx Then
            If mDirtyClient Then
                If TypeOf _Contact IsNot DTOCustomer Then
                    _Contact = DTOCustomer.FromContact(_Contact)
                End If
                Dim oCustomer As DTOCustomer = _Contact
                oCustomer.Ref = Xl_Contact_Cli1.Customer.Ref
            End If
            sFullNom = DTOContact.GenerateFullNom(_Contact)
            mDirtyGral = True
        End If

        If mDirtyGral Then
            _Contact = Xl_Contact_Gral1.Contact
            If mDirtyClx Then _Contact.FullNom = sFullNom
            exs = New List(Of Exception)
            Dim id = Await FEB.Contact.Update(exs, _Contact)
            If exs.Count = 0 Then
                _Contact.Id = id
            Else
                MsgBox("Error al desar la fitxa de client" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
            End If
        End If


        If mDirtyAdrEntregas Then
            Dim oDlvrAdr As DTOAddress = Xl_Contact_Cli1.DeliveryAddress()
            If oDlvrAdr Is Nothing Then
                oDlvrAdr = DTOAddress.Factory(_Contact, DTOAddress.Codis.Entregas)
                Await FEB.Address.Delete(oDlvrAdr, exs)
            ElseIf _Contact.Address.Equals(oDlvrAdr) Or oDlvrAdr Is Nothing Then
                Await FEB.Address.Delete(oDlvrAdr, exs)
            Else
                If oDlvrAdr.Zip Is Nothing Then
                    Await FEB.Address.Delete(oDlvrAdr, exs)
                Else
                    oDlvrAdr.Codi = DTOAddress.Codis.Entregas
                    Await FEB.Address.Update(oDlvrAdr, exs)
                End If
            End If
        End If


        If mDirtyClient Then
            Dim oCustomer = Xl_Contact_Cli1.Customer
            exs = New List(Of Exception)
            If Not Await FEB.Customer.Update(exs, oCustomer) Then
                MsgBox("Error al desar la fitxa de client" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
            End If
        End If

        If mDirtyPrv Then
            Dim oProveidor = Xl_Contact_Prv1.Proveidor
            exs = New List(Of Exception)
            If Not Await FEB.Proveidor.Update(oProveidor, exs) Then
                MsgBox("Error al desar la fitxa de representant" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
            End If
        End If

        If mDirtyRep Then
            Dim oRep = Xl_Contact_Rep1.Rep
            exs = New List(Of Exception)
            If Not Await FEB.Rep.Update(exs, oRep) Then
                MsgBox("Error al desar la fitxa de representant" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
            End If
        End If

        If mDirtyBanc Then
            Dim oBanc = Xl_Contact_Banc1.Banc
            exs = New List(Of Exception)
            If Not Await FEB.Banc.Update(oBanc, exs) Then
                UIHelper.WarnError(exs, "error al desar la fitxa de banc")
            End If
        End If

        If mDirtyTrp Then
            Dim oTrp = Xl_Contact_Trp1.Transportista
            exs = New List(Of Exception)
            If Not Await FEB.Transportista.Update(oTrp, exs) Then
                UIHelper.WarnError(exs, "error al desar la fitxa de Transportista")
            End If
        End If

        If mDirtyStaff Then
            Dim oStaff = Xl_Contact_Staff1.Staff
            exs = New List(Of Exception)
            If Not Await FEB.Staff.Update(exs, oStaff) Then
                MsgBox("error al desar la fitxa de personal" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        End If



        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
            ButtonOk.Enabled = False
        Else
            MsgBox(ExceptionsHelper.ToFlatString(exs))
        End If
        Me.Text = "Contacte " & _Contact.Id '& " " & My.Application.AssemblyInfo.Version.ToString

        retval = (exs.Count = 0)
        Return retval
    End Function

    Private Async Function DeleteRemovedTabs() As Task
        Dim FinalTabs As New List(Of DTOContact.Tabs)
        For i = 1 To TabControl1.TabPages.Count - 1
            FinalTabs.Add(TabControl1.TabPages(i).Tag)
        Next

        Dim exs As New List(Of Exception)
        For Each oTab In _Tabs
            If Not FinalTabs.Contains(oTab) Then
                Await DeleteTab(oTab, exs)
            End If
        Next

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function DeleteTab(oTab As DTOContact.Tabs, exs As List(Of Exception)) As Task
        Select Case oTab
            Case DTOContact.Tabs.Client
                Dim oCustomer As DTOCustomer = Nothing
                If TypeOf _Contact Is DTOCustomer Then
                    oCustomer = _Contact
                Else
                    oCustomer = DTOCustomer.FromContact(_Contact)
                End If
                If Not Await FEB.Customer.Delete(exs, oCustomer) Then
                    UIHelper.WarnError(exs)
                End If
            Case DTOContact.Tabs.Proveidor
                Await FEB.Proveidor.Delete(_Contact, exs)
            Case DTOContact.Tabs.Rep
                Await FEB.Rep.Delete(exs, _Contact)
            Case DTOContact.Tabs.Banc
                If Not Await FEB.Banc.Delete(_Contact, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Case DTOContact.Tabs.Transportista
                If Not Await FEB.Transportista.Delete(_Contact, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Case DTOContact.Tabs.Staff
                If Not Await FEB.Staff.Delete(exs, _Contact) Then
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Function

    Private Sub CheckDuplicatedAddresses()
        ' Dim oBaseAdr As DTOAddress = mContact.GetAddress(DTOAddress.Codis.Fiscal)
        ' Dim oDeliveryAdr As DTOAddress = mContact.GetAddress(DTOAddress.Codis.Entregas)
        'Dim oMailAdr As DTOAddress = mContact.GetAddress(DTOAddress.Codis.Correspondencia)
        Dim exs As New List(Of Exception)

        'If oBaseAdr.Equals(oDeliveryAdr) Then
        'Dim oContact As New DTOContact(mContact.Guid)
        'If Not DTOAddress.Delete(oContact, DTOAddress.Codis.Entregas, exs) Then
        'UIHelper.WarnError(exs)
        'End If
        'End If

        'If oBaseAdr.Equals(oMailAdr) Then
        'Dim oContact As New DTOContact(mContact.Guid)
        'If Not FEB.Address.Delete(oContact, DTOAddress.Codis.Correspondencia, exs) Then
        'UIHelper.WarnError(exs)
        'End If
        'End If

    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la fitxa " & _Contact.Id & " de " & _Contact.FullNom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Contact.Delete(exs, _Contact) Then
                Me.Close()
            Else
                UIHelper.WarnError(exs, "No s'ha pogut eliminar la fitxa " & _Contact.Id & " de " & _Contact.FullNom & ".")
            End If
        End If
    End Sub

End Class
