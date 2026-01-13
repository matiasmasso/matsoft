Imports System.ComponentModel

Public Class Frm__Idx
    Inherits Form

    Private _WinMenuFolders As List(Of DTOWinMenuItem)
    Private _WinMenuItems As List(Of DTOWinMenuItem)
    'Private _WinMenuItemsSprite As DTOSprite

    Private _requestToExit As Boolean
    Private _DirtyBancs As Boolean
    Private _DirtyStaffs As Boolean
    Private _AllowEvents As Boolean

    Private _iconWidth = 48
    Private _iconHeight = 48

    Public Sub New()
        MyBase.New()

        If CheckNetworkAvailability() Then
            AddHandler System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged, AddressOf CheckNetworkAvailabilityOnceLoaded
            InitializeComponent()
        End If
        MyBase.Opacity = 0
    End Sub


    Private Async Sub Frm__Idx_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim args As String() = Environment.GetCommandLineArgs()

        ToggleSplashScreen(True)

        Dim exs As New List(Of Exception)
        If Await Initialize(exs) Then

        Else
            _requestToExit = True
            UIHelper.WarnError(exs, "imposible iniciar la aplicació")
            Me.Close()
            Application.Exit()
        End If


    End Sub

    Private Function CheckNetworkAvailability() As Boolean
        Do Until System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()
            Dim rc As MsgBoxResult = MsgBox("no hi ha connexió a Internet", MsgBoxStyle.RetryCancel)
            If rc = MsgBoxResult.Cancel Then
                Return False
            End If
        Loop
        Return True
    End Function

    Private Async Sub ToggleSplashScreen(visible As Boolean)
        Me.Opacity = 0
        If visible Then
            MenuStrip1.Visible = False
            Xl_DropFile1.Visible = False
            Me.Width = 400
            Me.Height = 150
            Me.FormBorderStyle = FormBorderStyle.FixedSingle

            With PanelSplashScreen
                .Visible = True
                .Dock = DockStyle.Fill
                LabelVersion.Text = String.Format("Mat.Net versió: {0}", SessionHelper.AppVersion())
            End With
        Else
            PanelSplashScreen.Visible = False
            Me.FormBorderStyle = FormBorderStyle.Sizable
            MenuStrip1.Visible = True
            Xl_DropFile1.Visible = True

            SetFormSize()
        End If
        Await UIHelper.FadeIn(Me, 40)
    End Sub

    Private Sub SetFormSize()
        Dim sWidth As String = UIHelper.GetSetting(DTOSession.Settings.FrmIdx_Width)
        Dim sHeight As String = UIHelper.GetSetting(DTOSession.Settings.FrmIdx_Height)
        Dim sSplitter As String = UIHelper.GetSetting(DTOSession.Settings.FrmIdx_Splitter)

        If IsNumeric(sWidth) Then
            Me.Width = CInt(sWidth)
        Else
            Me.Width = 600
        End If
        If IsNumeric(sHeight) Then
            Me.Height = CInt(sHeight)
        Else
            Me.Height = 300
        End If

        If IsNumeric(sSplitter) Then
            Me.SplitContainer1.SplitterDistance = CInt(sSplitter)
        End If
    End Sub

    Private Sub CheckNetworkAvailabilityOnceLoaded()
        If Not CheckNetworkAvailability() Then
            Me.Close()
            Application.Exit()
            Exit Sub
        End If
    End Sub


    Private Shadows Async Function Initialize(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Try
            LabelLaunchStatus.Text = "Inicialitzant aplicació..."
            Application.DoEvents()
            DTOApp.Current = Await FEB2.App.InitializeAsync(exs, DTOApp.AppTypes.matNet, "https://matiasmasso-api.azurewebsites.net", 55836,,, 44373)
            GlobalVariables.Emp = Await FEB2.Emp.FindAsync(DTOEmp.Ids.MatiasMasso, exs)
            With GlobalVariables.Emp
                LabelLaunchStatus.Text = "Llegint dades organització..."
                Application.DoEvents()
                .Org = Await FEB2.Contact.Find(.Org.Guid, exs)
                LabelLaunchStatus.Text = "Llegint dades magatzem..."
                Application.DoEvents()
                .Mgz = DTOMgz.FromContact(Await FEB2.Contact.Find(.Mgz.Guid, exs))
            End With

            LabelLaunchStatus.Text = "Obrint sessió..."
            Application.DoEvents()
            If Await SessionHelper.AddSession(GlobalVariables.Emp, DTOApp.Current.Id, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs) Then
                Dim oLang = Current.Session.User.Lang
                LabelVersion.Text = String.Format("Mat.Net {0}: {1}", oLang.Tradueix("versión", "versió", "version", "versão"), SessionHelper.AppVersion())
                LabelLaunchStatus.Text = oLang.Tradueix("Abriendo sesión...", "Obrint sessió...", "Opening session...", "sessão aberta...")
                LabelUser.Text = oLang.Tradueix("Usuario: ", "Usuari: ", "User: ", "usuário: ") & Current.Session.User.EmailAddress
                With Current.Session
                    .Lang = oLang
                    .Emps = Await FEB2.Emps.All(exs, Current.Session.User)
                    .Emp = GlobalVariables.Emp '.Emps.FirstOrDefault(Function(x) x.Id = GlobalVariables.Emp.Id)
                    .User.Emp = .Emp
                    .Cur = DTOCur.Factory(DTOCur.Ids.EUR.ToString())
                    .Rol = .User.Rol
                End With

                'TO DEPRECATE ==========================================================================================
                'BLLApp.Initialize(GlobalVariables.Emp.Mgz) 'TO DEPRECATE
                '=======================================================================================================

                UseLocalApiToolStripMenuItem.Visible = Debugger.IsAttached
                LabelLaunchStatus.Text = oLang.Tradueix("Leyendo menú...", "Llegint menú...", "Reading the menu...", "lendo o cardápio...")
                Application.DoEvents()
                'If Await LoadMenuItems(exs) Then
                'LabelLaunchStatus.Text = "Maquetant la pàgina..."
                'Application.DoEvents()
                If Await Refresca(exs) Then
                    ToggleSplashScreen(False)
                    _AllowEvents = True
                Else
                    UIHelper.WarnError(exs)
                End If

                'Else
                'UIHelper.WarnError(exs)
                'End If

                retval = True
            End If
        Catch ex As Exception
            If Debugger.IsAttached Then MsgBox(ex.Message)
        End Try

        Return retval
    End Function

    Public Async Function Refresca(exs As List(Of Exception)) As Task(Of Boolean)

        Dim sBackupGuid As String = GetSetting(DTOSession.CookieSessionNameBackup)
        If GuidHelper.IsGuid(sBackupGuid) Then
            CancellarDemoToolStripMenuItem.Visible = True
        End If
        SetCaption()
        SetLangMenu()
        If Await LoadMenuItems(exs) Then
            Await RefrescaTreeview(exs)
        End If
        Dim retval = exs.Count = 0
        Return retval
    End Function

    Public Sub SetCaption()
        Dim sUserName = Current.Session.User.NicknameOrElse
        Me.Text = String.Format("Mat.Net {0}     {1}", SessionHelper.AppVersion(), sUserName)
    End Sub

    Private Sub SetLangMenu()
        Dim oLang = Current.Session.Lang
        MenuToolStripMenuItem.Text = oLang.Tradueix("Menú", "Menú", "Menu", "Cardápio")
        PerfilToolStripMenuItem.Text = oLang.Tradueix("Perfil", "Perfil", "Profile", "Perfil")
        HelpToolStripMenuItem.Text = oLang.Tradueix("Ayuda", "Ajuda", "Help", "Ajuda")
        AboutToolStripMenuItem.Text = oLang.Tradueix("Acerca de", "Perfil", "Profile", "Perfil")
        LangToolStripMenuItem.Text = oLang.Tradueix("idioma", "llenguatge", "language", "lenguage")
    End Sub


    Private Async Sub onLangUpdate(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oLang As DTOLang = e.Argument
        Dim oUser = Current.Session.User
        If FEB2.User.Load(exs, oUser) Then
            oUser.Lang = oLang
            Current.Session.Lang = oLang
            If Await FEB2.User.Update(exs, oUser) Then
                SetLangMenu()
                If Not Await RefrescaTreeview(exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadMenuItems(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim allItems = Await FEB2.WinMenuItems.All(Current.Session.User, exs) ' ApiGet.WinMenuItems()
        If exs.Count = 0 Then
            _WinMenuFolders = allItems.Where(Function(x) x.cod = DTOWinMenuItem.Cods.folder).ToList
            _WinMenuItems = allItems.Where(Function(x) x.cod = DTOWinMenuItem.Cods.item).ToList
            retval = True
        End If
        Return retval
    End Function


    Private Async Function RefrescaTreeview(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        'Dim oTree As List(Of DTOWinMenuItem) = BLLWinMenuItems.Tree(_User.Rol, DTOWinMenuItem.Cods.Folder)
        'Dim items As List(Of DTOWinMenuItem) = ApiGet.WinMenuItems()
        If _WinMenuFolders.Count = 0 Then
            exs.Add(New Exception("El rol no te cap menu autoritzat"))
        Else
            Xl_WinMenuTree1.Load(_WinMenuFolders, LastMenuSelection)
            Dim oSelectedFolder As DTOWinMenuItem = Xl_WinMenuTree1.SelectedNode.Tag
            retval = Await RefrescaListView(oSelectedFolder, exs)
        End If
        Return retval
    End Function

    Private Async Function RefrescaListView(oParent As DTOWinMenuItem, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Select Case oParent.customTarget
            Case DTOWinMenuItem.CustomTargets.bancs
                If _DirtyBancs Or oParent.children.Count = 0 Then
                    oParent.children.Clear()
                    Dim oBancs = Await FEB2.Bancs.AllActiveWithIcons(Current.Session.Emp, exs)
                    DTOWinMenuItem.loadBancs(oBancs, oParent)
                    _DirtyBancs = False
                End If
                ShowMenuItems(oParent.children)
            Case DTOWinMenuItem.CustomTargets.staff
                If _DirtyStaffs Or oParent.children.Count = 0 Then
                    oParent.children.Clear()
                    Dim oStaffs = Await FEB2.Staffs.AllActiveWithIcons(exs, GlobalVariables.Emp, _iconWidth, _iconHeight)
                    DTOWinMenuItem.loadStaffs(oStaffs, oParent)
                    _DirtyStaffs = False
                End If
                ShowMenuItems(oParent.children)
            Case DTOWinMenuItem.CustomTargets.reps
                oParent.children.Clear()
                Dim oReps = Await FEB2.Reps.AllActiveWithIcons(Current.Session.User, exs)
                DTOWinMenuItem.loadReps(oReps, oParent)
                ShowMenuItems(oParent.children)
            Case DTOWinMenuItem.CustomTargets.userTasks
                Await ShowUserTaskLogs()
            Case Else
                Dim oChildren = _WinMenuItems.Where(Function(x) x.parent IsNot Nothing AndAlso x.parent.Guid.Equals(oParent.Guid)).ToList
                ShowMenuItems(oChildren)
        End Select

        retval = exs.Count = 0
        Return retval
    End Function

    Private Async Sub ShowUserTaskLogs(sender As Object, e As MatEventArgs)
        Await ShowUserTaskLogs()
    End Sub



    Private Async Function ShowUserTaskLogs() As Task
        Dim exs As New List(Of Exception)
        Dim oRightPanel As Panel = SplitContainer1.Panel2
        Dim oControl As Control = oRightPanel.Controls(0)
        Dim Xl As Xl_UserTaskLogs = Nothing
        If TypeOf oControl Is Xl_UserTaskLogs Then
            Xl = oControl
        Else
            Xl = New Xl_UserTaskLogs()
            Xl.Dock = DockStyle.Fill
            AddHandler Xl.RequestToRefresh, AddressOf ShowUserTaskLogs
            oRightPanel.Controls.RemoveAt(0)
            oRightPanel.Controls.Add(Xl)
        End If

        Dim oUserTaskLogs = Await FEB2.UserTaskLogs.All(exs, Current.Session.User, Xl.DisplayObsolets)
        If exs.Count = 0 Then
            Xl.Load(oUserTaskLogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Private Sub ShowMenuItems(items As List(Of DTOWinMenuItem))
        Dim oRightPanel As Panel = SplitContainer1.Panel2
        Dim oControl As Control = oRightPanel.Controls(0)
        Dim Xl As Xl_WinMenuListView = Nothing
        If TypeOf oControl Is Xl_WinMenuListView Then
            Xl = oControl
            'Xl.Clear()
        Else
            Xl = New Xl_WinMenuListView()
            Xl.Dock = DockStyle.Fill
            AddHandler Xl.AfterUpdate, AddressOf Xl_WinMenuListView1_AfterUpdate
            AddHandler Xl.onItemSelected, AddressOf Xl_WinMenuListView1_onItemSelected
            AddHandler Xl.RequestToRefresh, AddressOf Xl_WinMenuListView1_RequestToRefresh
            oRightPanel.Controls.RemoveAt(0)
            oRightPanel.Controls.Add(Xl)
        End If

        Xl.Load(items)
    End Sub


    Private Async Sub Xl_WinMenuTree1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_WinMenuTree1.ValueChanged
        Dim exs As New List(Of Exception)
        Dim oParent As DTOWinMenuItem = e.Argument
        If Await RefrescaListView(oParent, exs) Then
            UIHelper.SaveSetting(DTOSession.Settings.Last_Menu_Selection, oParent.Guid.ToString())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_WinMenuTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WinMenuTree1.RequestToRefresh
        Await ReloadMenuItems()
    End Sub

    Private Async Function ReloadMenuItems() As Task
        Dim exs As New List(Of Exception)
        If Await LoadMenuItems(exs) Then
            If Not Await RefrescaTreeview(exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub Xl_WinMenuListView1_AfterUpdate(sender As Object, e As MatEventArgs)
        Await ReloadMenuItems()
        'Dim oParent As DTOWinMenuItem = Xl_WinMenuTree1.SelectedNode.Tag
        'Dim exs As New List(Of Exception)
        'If Not Await RefrescaListView(oParent, exs) Then
        ' UIHelper.WarnError(exs)
        ' End If
    End Sub


    Private Sub Xl_WinMenuListView1_onItemSelected(sender As Object, e As MatEventArgs)
        Dim item As DTOWinMenuItem = e.Argument
        Select Case item.customTarget
            Case DTOWinMenuItem.CustomTargets.reps, DTOWinMenuItem.CustomTargets.bancs, DTOWinMenuItem.CustomTargets.staff
                Dim oFrm As New Frm_Contact(item.tag)
                oFrm.Show()
            Case Else
                Try
                    CallByName(Me, item.action, CallType.Method)
                Catch ex As Exception
                    UIHelper.WarnError(ex)
                End Try
        End Select
    End Sub

    Private Function LastMenuSelection() As DTOBaseGuid
        Dim retval As DTOBaseGuid = Nothing
        Dim sLastSelectedMenuitem As String = UIHelper.GetSetting(DTOSession.Settings.Last_Menu_Selection)
        If GuidHelper.IsGuid(sLastSelectedMenuitem) Then
            Dim oGuid As New Guid(sLastSelectedMenuitem)
            retval = _WinMenuFolders.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
            If retval Is Nothing Then
                Dim oWinMenuItem = _WinMenuItems.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
                If oWinMenuItem IsNot Nothing Then
                    retval = oWinMenuItem.parent
                End If
            End If
        End If
        Return retval
    End Function
    Private Sub LangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LangToolStripMenuItem.Click
        Dim oFrm As New Frm_LangSelection
        AddHandler oFrm.AfterUpdate, AddressOf onLangUpdate
        oFrm.Show()
    End Sub

    Private Sub CredencialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CredencialsToolStripMenuItem.Click
        Dim oFrm As New Frm_Credencials
        oFrm.Show()
    End Sub

    Private Async Sub DesconectarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesconectarToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.Session.Close(exs, Current.Session) Then
            SaveSettingString(DTOSession.CookiePersistName, "0")
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Frm_Idx_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        Try
            If Not _requestToExit Then
                UIHelper.SaveSetting(DTOSession.Settings.FrmIdx_Width, Me.Width)
                UIHelper.SaveSetting(DTOSession.Settings.FrmIdx_Height, Me.Height)
                UIHelper.SaveSetting(DTOSession.Settings.FrmIdx_Splitter, SplitContainer1.Panel1.Width)
                If Await FEB2.Session.Close(exs, Current.Session) Then
                    LegacyHelper.ExcelAppHelper.Quit()
                Else
                    e.Cancel = True
                    UIHelper.WarnError(exs)
                End If
            End If
            'Stop '=========================================================
        Catch ex As Exception
            'Stop '=========================================================
        End Try
    End Sub


    Private Sub Frm_Idx_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        MsgBox(SessionHelper.AppVersion())
    End Sub

    Private Async Sub Xl_WinMenuListView1_RequestToRefresh(sender As Object, e As MatEventArgs)
        Dim item As DTOWinMenuItem = Xl_WinMenuTree1.SelectedNode.Tag
        Dim exs As New List(Of Exception)
        If Not Await RefrescaListView(item, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

#Region "Procedures"

    Public Sub ShowMarketPlaces()
        Dim oFrm As New Frm_MarketPlaces
        oFrm.Show()
    End Sub

    Public Sub ShowFeedback()
        'Dim oFrm As New Frm_FeedbackSources
        'oFrm.Show()
        MsgBox("No implementat encara, sorry...")
    End Sub

    Public Sub ShowRRHH()
        Dim oFrm As New Frm_RRHH
        oFrm.Show()
    End Sub

    Public Sub CheckIban()
        Dim oFrm As New Frm_IbanCheck("")
        oFrm.Show()
    End Sub

    Public Sub ShowPremiumLines()
        Dim oFrm As New Frm_PremiumLines
        oFrm.Show()
    End Sub

    Public Sub ShowCliReturns()
        Dim oFrm As New Frm_CliReturns
        oFrm.Show()
    End Sub

    Public Sub ExeFacturacio()
        Dim oFrm As New Frm_Facturacio
        oFrm.Show()
    End Sub

    Public Sub ShowCods()
        Dim oFrm As New Frm_Cods
        oFrm.Show()
    End Sub

    Public Async Sub ExeWebAtlasUpdate()
        Dim exs As New List(Of Exception)
        Dim oFrm As New Frm_Tarea("Actualitzant atlas web...")
        With oFrm
            .Show()
            Try
                Dim oTaskResult = Await FEB2.WebAtlas.Update(Current.Session.Emp, exs)
                If oTaskResult.Success Then
                    .Fin("Atlas web actualitzat.")
                Else
                    Dim sb As New Text.StringBuilder
                    sb.AppendLine("error al actualitzar punts de venda")
                    For Each ex In oTaskResult.Exceptions
                        sb.AppendLine(ex.Message)
                    Next
                    Dim sErr = sb.ToString
                    .Fin(Frm_Tarea.results.fail, sErr)
                    UIHelper.WarnError(sErr)
                    .Close()
                End If

            Catch ex As Exception
                Dim sb As New Text.StringBuilder
                sb.AppendLine("error al actualitzar punts de venda")
                sb.AppendLine(ex.Message)
                Dim sErr = sb.ToString
                .Fin(Frm_Tarea.results.fail, sErr)
                UIHelper.WarnError(sErr)
            End Try
        End With
    End Sub


    Public Sub SelPdc()
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = Frm_Doc_Select.Styles.Comanda
            .Show()
        End With
    End Sub

    Public Sub SelIncidencia()
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = Frm_Doc_Select.Styles.Incidencia
            .Show()
        End With
    End Sub

    Public Sub readPdf()
        Dim oFrm As New Frm_PdfRead
        oFrm.Show()
    End Sub

    Public Sub SelAlb()
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = Frm_Doc_Select.Styles.Albara
            .Show()
        End With
    End Sub

    Public Sub SelFra()
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = Frm_Doc_Select.Styles.Factura
            .Show()
        End With
    End Sub

    Public Sub SelCca()
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = Frm_Doc_Select.Styles.Assentament
            .Show()
        End With
    End Sub

    Public Sub SelMail()
        'root.ShowDocs(Frm_Doc_Select.Styles.mail)
    End Sub

    Public Sub WzGirs()
        Dim oFrm As New Wz_Girs
        oFrm.show
    End Sub

    Public Sub ShowPaisos()
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.Browse, Current.Session.Emp.Org.Address.Zip.Location)
        oFrm.Show()
    End Sub


    Public Sub ShowArts()
        Dim oFrm As New Frm_Cataleg
        oFrm.Show()
    End Sub


    Public Sub ShowApps()
        Dim oFrm As New Frm_Apps
        oFrm.Show()
    End Sub

    Public Sub ShowCsas()
        Dim oFrm As New Frm_Csas()
        oFrm.Show()
    End Sub

    Public Sub ShowCurs()
        'Dim oFrm As New Frm_Curs
        'oFrm.show
    End Sub

    Public Sub ShowDescuadreClients()
        Dim oFrm As New Frm_PndDescuadres
        oFrm.Show()
        'root.ShowDescuadreClients_Old()
        'root.ShowClientsQueNoCuadren()
    End Sub

    Public Sub ShowLastAlbs()
        Dim oFrm As New Frm_Deliveries
        oFrm.Show()
    End Sub

    Public Sub ShowLastCcas()
        Dim oFrm As New Frm_LastCcas
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastFras()
        Dim oFrm As New Frm_Invoices
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastMails()
        MsgBox("sorry, tasca pendent")
    End Sub

    Public Sub ShowLastMems()
        Dim oFrm As New Frm_Mems
        oFrm.Show()
    End Sub


    Public Sub WzPagament()
        '
        'root.WzPagament()
    End Sub



    Public Sub UpdateContacts()
        'root.MatOutlook.UpdateContacts()
    End Sub

    Public Sub ShowMgzStks()
        Dim oFrm As New Frm_Inventory()
        oFrm.Show()
    End Sub


    Public Sub ShowAdmBalSdos()
        Dim oFrm As New Frm_Ctas
        oFrm.Show()
    End Sub

    Public Sub ShowNominas()
        Dim oFrm_NominasNew As New Frm_NominasFactory()
        oFrm_NominasNew.Show()
    End Sub


    Public Sub ShowTransmisions()
        Dim oFrm As New Frm_Transmisions
        oFrm.Show()
    End Sub

    Public Sub NewTransmisio()
        Dim oFrm As New Frm_TransmisioNew()
        oFrm.Show()
    End Sub

    Public Sub NewClientPdc()
        'root.NewCliPdc()
    End Sub

    Public Sub ShowTransmConfig()
        MsgBox("Outdated")
    End Sub

    Public Sub ShowBookFras()
        Dim oFrm As New Frm_BookFras
        oFrm.Show()
    End Sub

    Public Sub ShowAmortitzacions()
        Dim oFrm As New Frm_Amortitzations
        oFrm.Show()
    End Sub

    Public Sub ImportMailBoxAttachments()
        root.ImportMailBoxAttachments()
    End Sub

    Public Sub ShowBalSumasYSaldos()
        Dim oFrm As New Frm_Ctas
        oFrm.Show()
    End Sub

    Public Sub ShowBalSumasYSaldosMarketing()
        Dim oFrm As New Frm_Ctas({"627"})
        oFrm.Show()
    End Sub

    Public Sub ShowUserSearch()
        Dim oFrm As New Frm_UserSearch
        oFrm.Show()
    End Sub

    Public Sub ShowExerciciApertura()
        Dim oFrm As New Frm_ExerciciApertura
        oFrm.Show()
    End Sub

    Public Sub ImportFile()
        root.ImportFile()
    End Sub

    Public Sub ShowPqHdrs()
        root.ShowPqHdrs()
    End Sub

    Public Sub Old_ImgUpload()
        'root.ImgUpload()
    End Sub

    Public Sub ShowSelArt()
        root.ShowSelArt()
    End Sub

    Public Sub ShowProveidorsVenciments()
        Dim oFrm As New Frm_ProveidorVtos
        oFrm.Show()
    End Sub

    Public Sub Test()
        root.Test()
    End Sub


    Private Sub MenuStrip1_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles MenuStrip1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub MenuStrip1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles MenuStrip1.DragDrop
        ' Handle FileDrop data.
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            ' Assign the file names to a string array, in 
            ' case the user has selected multiple files.
            Dim files As String() = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
            Dim sFileName As String
            For Each sFileName In files
                Try
                    'root.ImportedFileManager(sFileName)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return
                End Try

            Next
        End If
    End Sub



    Public Sub ShowLastSpvs()
        Dim oFrm As New Frm_Spvs
        oFrm.Show()
    End Sub


    Public Sub ShowConfig()
        Dim oFrm As New Frm_Config
        oFrm.Show()
    End Sub

    Public Sub ShowReembolsos()
        'root.ShowReembolsos()
    End Sub

    Public Sub ShowPnds()
        Dim oFrm As New Frm_CfpPnds
        oFrm.Show()
    End Sub

    Public Sub ShowBalance()
        Dim oFrm As New Frm_Balances
        oFrm.Show()
    End Sub



    Private Sub EmpresaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmpresaToolStripMenuItem.Click
        Dim oFrm As New Frm_Emps()
        AddHandler oFrm.SelectedItemChanged, AddressOf OnEmpChanged
        oFrm.Show()
    End Sub

    Private Async Sub OnEmpChanged(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim sEmailAddress As String = Current.Session.User.EmailAddress
        Dim oEmp = Await FEB2.Emp.Find(CType(e.Argument, DTOEmp).Id, exs)
        Dim allowedFormsCount As Integer = 2 'la principal i el selector d'empreses que encara no està tancat
        If My.Application.OpenForms.Count <= allowedFormsCount Then

            _DirtyBancs = True
            GlobalVariables.Emp = oEmp
            With Current.Session
                .Emp = oEmp
                If .Emp.Org IsNot Nothing Then
                    .Emp.Org = Await FEB2.Contact.Find(.Emp.Org.Guid, exs)
                End If
                If .Emp.Mgz IsNot Nothing Then
                    Dim oMgzContact = Await FEB2.Contact.Find(.Emp.Mgz.Guid, exs)
                    .Emp.Mgz = DTOMgz.FromContact(oMgzContact)
                End If
                .User = Await FEB2.User.FromEmail(exs, oEmp, sEmailAddress)
                .Rol = .User.Rol
            End With

            Me.Text = Current.Session.Emp.Org.nomComercialOrDefault()
            Xl_Contact1.Contact = Nothing
            Xl_Contact1.Emp = oEmp
            If Not Await RefrescaTreeview(exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            Dim msg As String = String.Format("Cal tancar {0} finestres abans de canviar d'empresa", My.Application.OpenForms.Count - allowedFormsCount)
            UIHelper.WarnError(msg)
        End If
    End Sub

    Public Sub ShowMod347()
        Dim oFrm As New Frm_AeatMod347
        oFrm.Show()
    End Sub


    Public Sub ShowFiscalIva2()
        Dim oFrm As New Frm_Iva
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowEtqs()
        Dim oFrm As New Frm_Etqs
        oFrm.Show()
    End Sub

    Public Sub ShowImpagats()
        Dim oFrm As New Frm_Impagats
        oFrm.Show()
    End Sub

    Public Sub ShowImportacions()
        Dim oFrm As New Frm_Importacions()
        oFrm.Show()
    End Sub

    Public Sub ShowEstates()
        Dim oFrm As New Frm_Estates
        oFrm.Show()
    End Sub

    Public Sub ShowRebut()
        Dim oRebut As New DTORebut()
        Dim oFrm As New Frm_Rebut(oRebut)
        oFrm.Show()
    End Sub

    Public Sub ShowTrpFrasVivace()
        root.ShowTrpFrasVivace()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim oFrm As New Frm_About
        oFrm.Show()
    End Sub

    Public Sub ShowIngresosYDespesesMensual()
        root.ShowIngresosYDespesesMensual()
    End Sub

    Public Sub ShowPrvLastEntrys()
        root.ShowPrvLastEntrys()
    End Sub

    Public Sub ShowFiscalIrpf()
        Dim oFrm As New Frm_Fiscal_Irpf2
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowBanks()
        Dim oFrm As New Frm_Banks()
        oFrm.Show()
    End Sub

    Public Sub ShowFrasToPrint()
        'Dim oFrm As New Frm_Print_Fras
        Dim oFrm As New Frm_InvoicesToPrint
        oFrm.Show()
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR FITXER"
            .Filter = "arxius XML (*.xml)|*.xml|tots els arxius|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Select Case .ShowDialog
                Case System.Windows.Forms.DialogResult.OK
                    'root.ImportedFileManager(.FileName)
            End Select
        End With
    End Sub


    Public Sub SowAeatMods()
        root.SowAeatMods()
    End Sub

    Private Async Sub Xl_DropFile1_AfterFileDropped(ByVal sender As System.Object, ByVal e As DropEventArgs) Handles Xl_DropFile1.onFileDropped
        Dim exs As New List(Of Exception)
        ProgressBar2.Visible = True
        Xl_DropFile1.DisplayStatus(Xl_DropFile.Status.Wait)
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        For Each oDocFile As DTODocFile In e.DocFiles
            Select Case oDocFile.Mime
                Case MimeCods.Xml
                    Dim oDoc As Xml.XmlDocument = FileSystemHelper.GetXMLfromByteStream(oDocFile.Stream)
                    Dim sTipo As String = oDoc.DocumentElement.GetAttribute("TIPO")
                    If sTipo = "" Then sTipo = oDoc.DocumentElement.GetAttribute("TYPE")
                    Select Case sTipo
                        Case "AVISOCAMION"
                            Dim oImportacio = Await FEB2.Importacio.FromAvisCamio(exs, oDoc)
                            If exs.Count = 0 Then
                                oImportacio.AvisCamio = oDoc
                                Dim oFrm As New Frm_Importacio(oImportacio)
                                oFrm.Show()
                            End If
                        Case ""
                            exs.Add(New Exception("Importació XML incorrecte." & vbCrLf & "Document XML no segueix nomenclatura standard" & vbCrLf & oDocFile.filename))
                        Case Else

                            exs.Add(New Exception("Importació XML incorrecte." & vbCrLf & "Tipus de document " & sTipo & " no registrat" & vbCrLf & oDocFile.filename))
                    End Select

                Case MimeCods.Pdf
                    Dim src = LegacyHelper.iTextPdfHelper.readText(oDocFile.Stream, exs)
                    If exs.Count = 0 Then
                        Dim segments = src.Split(vbLf).ToList
                        If NominaEscuraHelper.isNominaEscura(segments) Then
                            If oDocFile.Stream IsNot Nothing AndAlso Not IO.Directory.Exists(oDocFile.filename) Then
                                FileSystemHelper.SaveStream(oDocFile.Stream, exs, oDocFile.filename)
                                If exs.Count = 0 Then
                                    Dim oFrm As New Frm_NominasFactory(oDocFile.filename)
                                    oFrm.Show()
                                End If
                            End If
                        ElseIf Bebitus.isPurchaseOrder(segments) Then
                            Dim oTarifa = Await FEB2.CustomerBasket.Tarifa(exs, Current.Session.User, Bebitus.customer())
                            If exs.Count = 0 Then
                                Dim oPurchaseOrder = Bebitus.PurchaseOrder(exs, Current.Session.User, segments, oTarifa)
                                If exs.Count = 0 Then
                                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                    oFrm.Show()
                                End If
                            End If
                        Else
                            Dim lines = src.Split(vbLf).ToList
                            If AmazonSellerOrder.MatchSource(lines) Then
                                Dim oCountries = Await FEB2.Countries.All(DTOLang.ESP, exs)
                                If exs.Count = 0 Then
                                    Dim value As New DTO.AmazonSellerOrder(oDocFile, lines, oCountries)
                                    Dim oFrm As New Frm_ConsumerTicketFactory(value)
                                    oFrm.Show()
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                            Else
                                root.ShowFileImport(oDocFile)
                            End If
                        End If
                    End If

                Case MimeCods.Txt
                    Dim src As String = System.Text.Encoding.ASCII.GetString(oDocFile.stream)
                    If EdiHelper.IsEdiFile(src) Then
                        Select Case EdiHelper.EdiFileType(src)
                            Case EdiHelper.EdiFile.FileTypes.Orders
                                Dim oPurchaseOrder = Await FEB2.PurchaseOrder.FromEdi(exs, src, GlobalVariables.Emp)
                                If exs.Count = 0 Then
                                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                    oFrm.Show()
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                        End Select
                    End If
                    Dim EdiPattern = "ORDERS:D:96A|ORDRSP:D:96A|DESADV:D:96A|INVOIC:D:96A"
                    If TextHelper.RegexMatch(src, EdiPattern) Then
                        If TextHelper.RegexMatch(src, "ORDERS:D:96A") Then
                            'Dim oPurchaseOrder = Await FEB2.PurchaseOrder.FromEdi(exs, src, GlobalVariables.Emp)
                            'Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                            'oFrm.Show()
                        Else
                            exs.Add(New Exception("falta per implementar proces per aquest missatge Edi"))
                        End If
                    End If
                Case MimeCods.Xls
                    exs.Add(New Exception("El fitxer està en fomat Excel obsolet (.xls, 1997-2003)\nCal obrirlo i desar-lo en format .xlsx abans de pujar-lo"))
                Case MimeCods.Xlsx, MimeCods.Xlsm
                    Dim oSheet = LegacyHelper.ExcelEPPlusHelper.Read(exs, oDocFile.Stream, oDocFile.filename)
                    If exs.Count = 0 Then
                        If oSheet.MatchHeaderCaptions("Expedicion", "Pedido", "Consignatario", "FechaEnvio", "Bultos", "CosteEmb", "Manipulacion", "CodTransp", "CIFTransportista", "Transportista", "Peso", "PesoAgrupado", "CostePeso") Then
                            'Costos Vivace

                            If Await FEB2.VivaceCosts.Upload(exs, GlobalVariables.Emp, oSheet) Then
                                MsgBox("fitxer de costos Vivace importat correctament", MsgBoxStyle.Information)
                            End If
                        ElseIf oSheet.MatchHeaderCaptions("Customer", "Delivery Sequence", "Name", "Customer Item Ref", "Item", "Description", "Order Number", "Customer Order Ref", "Order Line", "Quantity Outstanding", "Date Delivery Required", "Order Value", "Allocated?", "Delivery Address", "Despatch Date", "Quantity Allocated", "Date", "Validation") Then
                            'Comandes Mayborn distribuidor farmaceutic validades per processar
                            oSheet.TrimCols(18) 'per si han afegit columnes extra en alguna fila
                            Dim oPurchaseOrders = Await FEB2.Mayborn.CustomerOrdersFactory(exs, oSheet, Current.Session.User, GlobalVariables.Emp.Mgz)
                            If exs.Count = 0 Then
                                For Each oPurchaseOrder In oPurchaseOrders
                                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                    oFrm.Show()
                                Next
                            End If
                        ElseIf oSheet.MatchHeaderCaptions("Customer", "Delivery Sequence", "Name", "Customer Item Ref", "Item", "Description", "Order Number", "Customer Order Ref", "Order Line", "Quantity Outstanding", "Date Delivery Required", "Order Value", "Allocated?", "Delivery Address", "Despatch Date", "Quantity Allocated", "Date") Then
                                'Comandes Mayborn distribuidor farmaceutic per validar
                                oSheet.TrimCols(18) 'per si han afegit columnes extra en alguna fila
                                Dim oValidationSheet = Await FEB2.Mayborn.ValidateCustomerOrder(exs, oSheet, GlobalVariables.Emp)
                                If exs.Count = 0 Then
                                    If oValidationSheet Is Nothing Then
                                        exs.Add(New Exception("Error al intentar validar l'Excel"))
                                    Else
                                        UIHelper.ShowExcel(oValidationSheet, exs)
                                    End If
                                End If

                            ElseIf oSheet.MatchHeaderCaptions("Líneas/Descripción", "Líneas/Cantidad", "Producto/EAN 13") Or oSheet.MatchHeaderCaptions("LÃ­neas/DescripciÃ³n", "LÃ­neas/Cantidad", "Producto/EAN 13") Then
                                'Comandes Mi Farma
                                oSheet.TrimCols(5) 'per si han afegit columnes extra en alguna fila
                                oSheet.CultureInfo = Globalization.CultureInfo.GetCultureInfo("en-US")
                                Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.MiFarma)
                                Dim oCustomerSkus = Await FEB2.ProductSkus.All(exs, oCustomer, GlobalVariables.Emp.Mgz, True)
                                Dim oTarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, oCustomer)
                                Dim oCliProductDtos = Await FEB2.CliProductDtos.All(oCustomer, exs)

                                'Dim oInventari = Await FEB2.Mgz.Skus(GlobalVariables.Emp.Mgz, Today, exs)
                                Dim oPurchaseOrder = DTOExcelPOMiFarma.PurchaseOrder(oSheet, oCustomerSkus, oTarifaDtos, oCliProductDtos, Current.Session.User)
                                If oPurchaseOrder IsNot Nothing Then
                                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                    oFrm.Show()
                                End If

                            Else
                                exs.Add(New Exception("Plantilla Excel no reconeguda"))
                            End If
                        End If
                Case MimeCods.Csv
                    Dim oCsvFile = CsvHelper.CsvFile.Factory(oDocFile.Stream, oDocFile.Filename)
                    If DTOCsvPOBebitus.IsPurchaseOrder(oCsvFile) Then
                        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Bebitus)
                        Dim oTarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, oCustomer)
                        Dim oCustomCosts = Await FEB2.PriceListItemsCustomer.Active(exs, oCustomer)
                        Dim oCustomerSkus = Await FEB2.ProductSkus.All(exs, oCustomer, GlobalVariables.Emp.Mgz, True)
                        Dim oCliProductsBlocked = Await FEB2.CliProductsBlocked.All(exs, oCustomer)
                        Dim oCliProductDtos = Await FEB2.CliProductDtos.All(oCustomer, exs)
                        Dim oPurchaseOrder = DTOCsvPOBebitus.PurchaseOrder(exs, Current.Session.User, oCsvFile, oCustomer, oTarifaDtos, oCustomCosts, oCustomerSkus, oCliProductsBlocked, oCliProductDtos)
                        If exs.Count = 0 Then
                            If oPurchaseOrder IsNot Nothing Then
                                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                oFrm.Show()
                            End If
                        End If
                    Else
                        exs.Add(New Exception("Plantilla Csv no reconeguda"))
                    End If
                    'Número de línea;Código de artículo;Externo;Nombre del producto;Texto;Cantidad;Importe neto;Fecha de entrega

                    'If Amazon.IsItemListCsv(oDocFile) Then
                    'Dim oMemStream As New System.IO.MemoryStream(oDocFile.Stream)
                    'Dim oItems As New Amazon_ItemListCsvs(oMemStream)
                    'Dim oFrm As New Frm_Amazon_ItemListCsv(oItems)
                    'oFrm.Show()
                    'End If
                Case MimeCods.NotSet
                    CheckAEAT(oDocFile)
                Case MimeCods.Txt, MimeCods.Pla
                    ' Dim oFlatFile As DTOFlatFile = BLLFlatFile.FromDocFile(oDocFile, exs)
                    ' If exs.Count > 0 Then
                    ' UIHelper.WarnError(exs)
                    ' Else
                    ' If oFlatFile Is Nothing Then
                    ' Select Case System.IO.Path.GetFileName(DTODocFile.FileNameOrDefault(oDocFile))
                    ' Case "nrc.txt"
                    '     Dim oFile As New MaxiSrvr.FlatFileFixLen(DTOFlatFile.Ids.Nrc)
                    '     If Xl_DropFile1.DocFiles.Count > 0 Then
                    '     oFile.Load(DTODocFile.FileNameOrDefault(oDocFile))
                    '     Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    '     oFrm.Show()
                    '     End If
                    ' Case Else
                    '     If Not CheckAEAT(oDocFile) Then
                    '
                    '                    End If
                    '                    Dim oMemStream As New IO.MemoryStream(oDocFile.Stream)
                    '                   Dim sr As IO.StreamReader = New IO.StreamReader(oMemStream)
                    '                    Dim line As String = sr.ReadLine
                    '                    SR.Close()
                    '
                    '                    Dim sFilename As String = DTODocFile.FileNameOrDefault(Xl_DropFile1.DocFiles.First)
                    '                    Dim iLineLength As Integer = line.Length
                    '                    Dim s As String = line.Substring(0, 3)
                    '                    If iLineLength = 348 And line.Substring(0, 2) = "10" Then
                    '                    Dim oFile As New MaxiSrvr.FlatFileFixLen(DTOFlatFile.Ids.RMELaCaixa)
                    '                    If Xl_DropFile1.DocFiles.Count > 0 Then
                    '                    oFile.Load(sFilename)
                    '                    Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    '                    oFrm.Show()
                    '                    End If
                    '                    ElseIf iLineLength = 600 And line.Substring(0, 7) = "0119445" Then
                    '                    Dim oFile As New MaxiSrvr.FlatFileFixLen(DTOFlatFile.Ids.SEPAB2B_Adeudos)
                    '                    If Xl_DropFile1.DocFiles.Count > 0 Then
                    '                    oFile.Load(sFilename)
                    '                    Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    '                    oFrm.Show()
                    '                    End If
                    'ElseIf iLineLength = 600 And line.Substring(0, 7) = "1119445" Then
                    ' Dim oFile As New MaxiSrvr.FlatFileFixLen(DTOFlatFile.Ids.SEPAB2B_Rechazos)
                    ' If Xl_DropFile1.DocFiles.Count > 0 Then
                    ' oFile.Load(sFilename)
                    'Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    'oFrm.Show()
                    ' End If
                    'ElseIf iLineLength = 600 And line.Substring(2, 5) = "2119445" Then
                    ' Dim oFile As New MaxiSrvr.FlatFileFixLen(DTOFlatFile.Ids.SEPAB2B_Devoluciones)
                    'If Xl_DropFile1.DocFiles.Count > 0 Then
                    'oFile.Load(sFilename)
                    'Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    'oFrm.Show()
                    'End If
                    '
                    'End If
                    'End Select
                    'Else
                    'Dim oFrm As New Frm_FlatFile(oFlatFile)
                    'oFrm.Show()
                    'End If
                    'End If
                Case Else
                    root.ShowFileImport(oDocFile)
            End Select
        Next
        Xl_DropFile1.DisplayStatus(Xl_DropFile.Status.Ready)
        ProgressBar2.Visible = False
        Cursor = Cursors.Default

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Function CheckAEAT(oDocFile As DTODocFile) As Boolean
        Dim retval As Boolean = False
        Dim line As String '= GetFirstLineFromFile(oDocFile)
        Return retval
        Exit Function

        Dim sFilename As String = FEB2.DocFile.FileNameOrDefault(Xl_DropFile1.DocFiles.First)
        Try
            Dim iLineLength As Integer = line.Length
            Dim s As String = line.Substring(0, 3)
            If iLineLength = 984 And s = "111" Then
                'Dim oFile As New MaxiSrvr.MatFileAEAT111(oDocFile.Stream)
                'Dim oFrm As New Frm_FileAEAT111
                'oFrm.File = oFile
                'oFrm.Show()
                'retval = True
            ElseIf iLineLength = 1050 And s = "115" Then
                'Dim oFile As New MaxiSrvr.MatFileAEAT115(oDocFile.Stream)
                'Dim oFrm As New Frm_FileAEAT115
                'oFrm.File = oFile
                'oFrm.Show()
                'retval = True
            End If
        Catch ex As Exception
            root.ShowFileImport(oDocFile)
        End Try
        Return retval
    End Function

    Public Sub ClearAlbBloqueig()
        root.ClearAlbBloqueig()
    End Sub


    Public Sub ShowEanEci()
        root.ShowEanEci()
    End Sub


    Public Sub ShowWebLog()
        root.ShowWebLog()
    End Sub

    Public Sub ShowAuditoria()
        Dim oFrm As New Frm_Auditoria
        oFrm.Show()
    End Sub


    Public Sub ShowIncidencies()
        Dim oQuery = DTOIncidenciaQuery.Factory(Current.Session.User)
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Public Sub Intrastats()
        Dim oFrm As New Frm_Intrastats
        oFrm.Show()
    End Sub


    Public Sub ShowPdcFchs()
        Dim oFrm As New Frm_PdcFchs
        oFrm.Show()
    End Sub

    Public Sub ShowCodisMercancia()
        root.ShowCodisMercancia()
    End Sub

    Public Sub ShowPncsXFch()
        Dim oFrm As New Frm_Pncs
        oFrm.Show()
    End Sub

    Public Sub ShowFlota()
        Dim oFrm As New Frm_Vehicles
        oFrm.Show()
    End Sub


    Public Sub ShowContracts()
        Dim oFrm As New Frm_Contracts()
        oFrm.Show()
    End Sub

    Public Sub ShowEscriptures()
        Dim oFrm As New Frm_Escriptures
        oFrm.Show()
    End Sub



    Public Sub ShowAlbsTransferenciaPrevia()
        Dim oFrm As New Frm_AlbsPendentsDeCobro(DTODelivery.RetencioCods.Transferencia)
        oFrm.Show()
    End Sub

    Public Sub ShowAlbsPendentsDeVisa()
        Dim oFrm As New Frm_AlbsPendentsDeCobro(DTODelivery.RetencioCods.VISA)
        oFrm.Show()
    End Sub



    Public Sub ShowEdi()
        Dim oFrm As New Frm_EdiversaFiles
        oFrm.Show()
    End Sub

    Public Sub ShowCnaps()
        root.ShowCnaps()
    End Sub


    Public Sub ShowTxts()
        Dim oFrm As New Frm_Txts
        oFrm.Show()
    End Sub


    Public Sub ShowTasks()
        Dim oFrm As New Frm_Tasks
        oFrm.Show()
    End Sub

    Public Sub ShowContents()
        Dim oFrm As New Frm_Contents()
        oFrm.Show()
    End Sub

    Public Sub ShowSubscripcions()
        Dim oFrm As New Frm_Subscripcions
        oFrm.Show()
    End Sub

    Public Sub ShowLiniesTelefon()
        root.ShowLiniesTelefon()
    End Sub

    Public Sub ShowTelConsums()
        root.ShowTelConsums()
    End Sub

    Public Sub ShowTaxes()
        root.ShowTaxes()
    End Sub

    Public Sub ShowYoutubes()
        Dim oFrm As New Frm_YouTubeMovies
        oFrm.Show()
    End Sub

    Public Sub ShowCliCredits()
        root.ShowCliCredits()
    End Sub



    Public Sub ShowFlatFileFixLens()
        'Dim oFrm As New Frm_FlatFileFixLens
        'oFrm.Show()
    End Sub

    Public Sub ShowWebMenuItems()
        Dim oFrm As New Frm_WebMenuItems(DTO.Defaults.SelectionModes.Browse)
        oFrm.Show()
    End Sub

    Public Sub ShowCnapsStat()
        root.ShowCnapsStat()
    End Sub


    Public Sub ShowEciPdcs()
        root.ShowEciPdcs()
    End Sub

    Public Sub ShowBlog()
        root.ShowBlog()
    End Sub

    Public Async Sub Show_Last_albs_enAltres()
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB2.Deliveries.Headers(exs, Current.Session.Emp, altresPorts:=True)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Deliveries(Xl_Deliveries.Purposes.PortsAltres, oDeliveries, "Albarans en altres")
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Public Sub ShowPriceLists()
        Dim oFrm As New Frm_PriceLists_Customers
        oFrm.Show()
    End Sub

    Public Sub ShowPostComments()
        Dim oFrm As New Frm_PostComments
        oFrm.Show()
    End Sub

    Public Sub ShowGallery()
        Dim oFrm As New Frm_Gallery
        oFrm.Show()
    End Sub

    Public Sub ShowReps()
        Dim oFrm As New Frm_Reps_Manager
        oFrm.Show()
    End Sub



    Public Sub ShowDocFiles()
        Dim oFrm As New Frm_DocFiles
        oFrm.Show()
    End Sub



    Public Sub ShowEdiRemadvs()
        Dim oFrm As New Frm_EdiversaRemadvs
        oFrm.Show()
    End Sub

    Public Sub ShowBanners()
        Dim oFrm As New Frm_Banners
        oFrm.Show()
    End Sub

    Public Sub ShowCustomerRanking()
        Dim exs As New List(Of Exception)
        Dim oFrm As New Frm_Ranking()
        oFrm.Show()
    End Sub

    Public Sub ShowDefaultImage()
        Dim oFrm As New Frm_DefaultImage
        oFrm.Show()
    End Sub


    Public Sub ShowDiari()
        Dim oFrm As New Frm_Diari
        oFrm.Show()
    End Sub

    Public Sub ShowEvents()
        Dim oFrm As New Frm_Events(Nothing)
        oFrm.Show()
    End Sub

    Public Sub EciPlatformsNewAlbs()
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.ElCorteIngles)
        Dim oFrm As New Frm_PlatformsNewAlb(oHolding)
        oFrm.Show()
    End Sub

    Public Sub ShowRaffles()
        Dim oFrm As New Frm_Raffles
        oFrm.Show()
    End Sub

    Public Sub ShowBloggers()
        Dim oFrm As New Frm_Bloggers(DTO.Defaults.SelectionModes.Browse)
        oFrm.Show()
    End Sub

    Public Sub ShowIbans()
        Dim oFrm As New Frm_Ibans
        oFrm.Show()
    End Sub


    Public Sub ShowPurchaseOrders()
        Dim oFrm As New Frm_PurchaseOrders()
        oFrm.Show()
    End Sub

    Public Sub ShowBancTerms()
        Dim oFrm As New Frm_BancTerms
        oFrm.Show()
    End Sub

    Public Sub ShowAreas()
        Dim oFrm As New Frm_Geo
        oFrm.Show()
    End Sub

    Public Sub ShowProducts()
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny)
        oFrm.Show()
    End Sub


    Public Sub ShowVivaceStocks()
        Dim oFrm As New Frm_Vivace_Stocks
        oFrm.Show()
    End Sub



    Public Sub MailingConsumers()
        Dim oFrm As New Frm_MailingConsumers
        oFrm.Show()
    End Sub

    Public Sub ShowVisaOrgs()
        Dim oFrm As New Frm_VisaOrgs
        oFrm.Show()
    End Sub

    Public Sub ShowVisaCards()
        Dim oFrm As New Frm_VisaCards
        oFrm.Show()
    End Sub

    Public Sub ShowNewsletters()
        Dim oFrm As New Frm_Newsletters
        oFrm.Show()
    End Sub

    Public Sub ShowConsumerTickets()
        Dim oFrm As New Frm_ConsumerTickets
        oFrm.Show()
    End Sub

    Public Sub ShowCliAperturas()
        Dim oFrm As New Frm_CliAperturas
        oFrm.Show()
    End Sub

    Public Sub ShowEdiversaOrders()
        Dim oFrm As New Frm_EDiversaOrders
        oFrm.Show()
    End Sub

    Public Sub ShowPgcClassesCtas()
        Dim oFrm As New Frm_PgcClassesCtas
        oFrm.Show()
    End Sub

    Public Sub ShowLeadAreas()
        Dim oFrm As New Frm_Leads2
        oFrm.Show()
    End Sub


    Public Sub ShowXecs()
        Dim oFrm As New Frm_Xecs
        oFrm.Show()
    End Sub
    Public Sub ShowEmpDefaults()
        Dim oFrm As New Frm_EmpDefaults
        oFrm.Show()
    End Sub


    Public Sub ShowBancTransfers()
        Dim oFrm As New Frm_BancTransfers
        oFrm.Show()
    End Sub

    Public Sub ShowBancTraspas()
        Dim oFrm As New Frm_BancTraspas
        oFrm.Show()
    End Sub

    Public Sub ShowCurExchangeRates()
        Dim oFrm As New Frm_Curs
        oFrm.Show()
    End Sub

    Public Sub ShowTpv()
        Dim oFrm As New Frm_TpvRedsys
        oFrm.Show()
    End Sub

    Public Sub ShowDistribuidorsOficialsRankingLastPdc()
        Dim oFrm As New Frm_DistribuidorsOficialsRankingLastPdc
        oFrm.Show()
    End Sub

    Public Sub ShowAuditStocks()
        Dim oFrm As New Frm_AuditSocks
        oFrm.Show()
    End Sub

    Public Sub ShowMailingPros()
        Dim oFrm As New Frm_MailingPros
        oFrm.Show()
    End Sub

    Public Sub ShowDeliveryTraspassos()
        Dim oFrm As New Frm_DeliveryTraspassos
        oFrm.Show()
    End Sub

    Public Sub ShowMediaResources()
        Dim oFrm As New Frm_MediaResources
        oFrm.Show()
    End Sub

    Public Sub ShowSpvIns()
        Dim oFrm As New Frm_SpvIns
        oFrm.Show()
    End Sub

    Public Sub ShowEciSalesReport()
        Dim oSalesReport = DTOSalesReport.Factory(DTOExercici.Current(GlobalVariables.Emp), DTOHolding.Wellknowns.ElCorteIngles)
        Dim oFrm As New Frm_SalesReport(oSalesReport)
        oFrm.Show()
    End Sub

    Public Sub ShowEdiversaSalesReports()
        Dim oFrm As New Frm_EdiversaSalesReports()
        oFrm.Show()
    End Sub




    Public Sub ShowCondicions()
        Dim oFrm As New Frm_Condicions
        oFrm.Show()
    End Sub

    Public Sub ShowStaffPoss()
        Dim oFrm As New Frm_StaffPoss
        oFrm.Show()
    End Sub


    Public Sub ShowPrintedInvoices()
        Dim oFrm As New Frm_PrintedInvoices
        oFrm.Show()
    End Sub

    Public Sub ShowMargins()
        Dim oFrm As New Frm_Margins
        oFrm.Show()
    End Sub

    Public Sub ShowContacts()
        Dim oFrm As New Frm_Contacts
        oFrm.Show()
    End Sub

    Public Sub ShowInventariDelta()
        Dim oFrm As New Frm_InventoryDelta
        oFrm.Show()
    End Sub

    Public Sub ShowWebPageAlias()
        Dim oFrm As New Frm_WebPagesAlias
        oFrm.Show()
    End Sub

    Public Sub ShowIncentius()
        Dim oFrm As New Frm_Incentius
        oFrm.Show()
    End Sub

    Public Sub ShowSiiLogs()
        Dim oFrm As New Frm_SiiLogs
        oFrm.Show()
    End Sub

    Public Sub ShowLangTexts()
        Dim oFrm As New Frm_LangResources
        oFrm.Show()
    End Sub
    Public Sub ShowSepaTexts()
        Dim oFrm As New Frm_SepaTexts
        oFrm.Show()
    End Sub

    Public Sub ShowSocialMediaWidgets()
        Dim oFrm As New Frm_SocialMediaWidgets
        oFrm.Show()
    End Sub

    Public Sub ShowProjectes()
        Dim oFrm As New Frm_Projectes
        oFrm.Show()
    End Sub

    Public Sub ShowRatios()
        Dim oFrm As New Frm_Ratios
        oFrm.Show()
    End Sub

    Public Sub ShowImmobles()
        Dim oFrm As New Frm_Immobles
        oFrm.Show()
    End Sub

    Public Sub ShowDesadvs()
        Dim oFrm As New Frm_EdiversaDesadvs
        oFrm.Show()
    End Sub

    Shared Sub ShowBalanceKpis()
        Dim oFrm As New Frm_Kpis
        oFrm.Show()
    End Sub

    Private Async Sub CancellarDemoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancellarDemoToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim sBackupGuid As String = GetSetting(DTOSession.CookieSessionNameBackup)
        Dim oUser = Await FEB2.User.Find(New Guid(sBackupGuid), exs)
        If exs.Count = 0 Then
            With Current.Session
                .User = oUser
                .Rol = .User.Rol
            End With

            SaveSettingString(DTOSession.CookieSessionNameBackup, "")
            SaveSettingString(DTOSession.CookieSessionName, oUser.Guid.ToString())
            SaveSettingString(DTOSession.CookiePersistName, "1")

            Dim oForms As FormCollection = Application.OpenForms
            For i As Integer = oForms.Count - 1 To 0 Step -1
                If TypeOf oForms(i) Is Frm__Idx Then
                    If Not Await DirectCast(oForms(i), Frm__Idx).Refresca(exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    oForms(i).Close()
                End If
            Next
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Public Sub ShowLocalizedStrings()
        Dim oFrm As New Frm_LocalizedStrings
        oFrm.Show()
    End Sub

    Public Sub ShowUserTaskIds()
        Dim oFrm As New Frm_UserTaskIds
        oFrm.Show()
    End Sub

    Public Sub ShowInemCompresProductRanking()
        Dim oFrm As New Frm_Inem_CompresProductRanking
        oFrm.Show()
    End Sub



    Public Sub NewCca()
        Dim oCca As DTOCca = DTOCca.Factory(Today, Current.Session.User, DTOCca.CcdEnum.Manual)
        Dim oFrm As New Frm_Cca(oCca)
        oFrm.Show()
    End Sub


    Public Sub ShowBudgets()
        Dim oFrm As New Frm_Budgets
        oFrm.Show()
    End Sub

    Public Sub ShowDistributionChannels()
        Dim oFrm As New Frm_DistributionChannels
        oFrm.Show()
    End Sub



    Public Sub ShowSellout()
        Dim oFrm As New Frm_SellOut
        oFrm.Show()
    End Sub

    Public Sub ShowBritaxTarget()
        Dim oFrm As New Frm_BritaxTarget
        oFrm.Show()
    End Sub

    Public Sub ShowRecalls()
        Dim oFrm As New Frm_Recalls
        oFrm.Show()
    End Sub

    Public Sub ShowWtbolSites()
        Dim oFrm As New Frm_WtbolSites
        oFrm.Show()
    End Sub
    Public Sub ShowPncDescuadres()
        Dim oFrm As New Frm_PncDescuadres()
        oFrm.Show()
    End Sub
    Public Sub ShowInsolvencies()
        Dim oFrm As New Frm_Insolvencies
        oFrm.Show()
    End Sub

    Public Sub ShowMadeIns()
        Dim oFrm As New Frm_MadeIns
        oFrm.Show()
    End Sub

    Public Sub ShowJsonSchemas()
        Dim oFrm As New Frm_JsonSchemas
        oFrm.Show()
    End Sub

    Public Sub ShowIncidenciesRatios()
        Dim oFrm As New Frm_IncidenciesRatios
        oFrm.Show()
    End Sub

    Public Sub ShowWinMenus()
        Dim oFrm As New Frm_WinMenus
        oFrm.Show()
    End Sub

    Public Sub ShowJsonLogs()
        Dim oFrm As New frm_JsonLogs
        oFrm.show
    End Sub

#End Region


    Private Sub IncidenciesInformatiquesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncidenciesInformatiquesToolStripMenuItem.Click
        Dim value As New DTOMatsoftSupportCase
        With value
            .User = Current.Session.User
            .FchOpen = Now
        End With
        Dim oFrm As New Frm_MatsoftSupportCase(value, True)
        oFrm.Show()
    End Sub

    Private Sub AssistenciaRemotaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssistenciaRemotaToolStripMenuItem.Click
        Dim sDownloadsFolder As String = Syroot.Windows.IO.KnownFolders.Downloads.Path
        Dim sFilename As String = String.Format("{0}\supremo.exe", sDownloadsFolder)
        Try
            Process.Start(sFilename)
        Catch ex As Exception
            MsgBox("a continuació es baixarà el fitxer Supremo.exe, que hauras de executar")
            Dim sUrl As String = "http://matsoft.matiasmasso.es/supremo.exe"
            Process.Start(sFilename)
        End Try
    End Sub

    Public Sub IntegrationValidations()
        Dim oFrm As New Frm_IntegrationValidations
        oFrm.Show()
    End Sub

    Public Sub ShowSatRecalls()
        Dim oFrm As New Frm_SatRecalls
        oFrm.Show()
    End Sub

    Public Sub ShowHoldings()
        Dim oFrm As New Frm_Holdings
        oFrm.Show()
    End Sub

    Public Sub ShowDepts()
        Dim oFrm As New Frm_Depts
        oFrm.Show()
    End Sub

    Public Sub ShowCsbResults()
        Dim oFrm As New Frm_CsbResults
        oFrm.Show()
    End Sub

    Public Sub ShowFilters()
        Dim oFrm As New Frm_Filters
        oFrm.Show()
    End Sub

    Public Sub ShowFtpservers()
        Dim oFrm As New Frm_Ftpservers
        oFrm.Show()
    End Sub

    Public Sub ShowWebErrs()
        Dim oFrm As New Frm_WebErrs
        oFrm.Show()
    End Sub

    Private Async Sub RefrescaMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaMenuToolStripMenuItem.Click
        Await ReloadMenuItems()
    End Sub


    Private Sub UseLocalApiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseLocalApiToolStripMenuItem.Click
        FEB2.Api.UseLocalApi(UseLocalApiToolStripMenuItem.Checked)
    End Sub


End Class
