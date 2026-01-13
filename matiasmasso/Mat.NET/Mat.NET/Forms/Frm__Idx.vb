Public Class Frm__Idx
    Inherits Form

    Private _User As DTOUser
    Private _WinMenuItems As List(Of DTOWinMenuItem)
    Private _AllowEvents As Boolean

    Public Sub New()
        MyBase.New()

        Dim exs As List(Of Exception) = Nothing
        If BLL.BLLApp.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.MatNet, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs) Then

            If Not Session.Initialize Then
                Dim oFrm As New Frm_Login
                oFrm.ShowDialog()
            End If

            'TO DEPRECATE ===========================================================================
            MaxiSrvr.BLL_App.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.MatNet, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs)
            BLL.BLLSession.Current = SessionLoader.Find(Session.Guid)
            '========================================================================================

            'GetEmpFromCommandLineArgs()
            InitializeComponent()

        Else
            UIHelper.WarnError(exs, "imposible iniciar la aplicació")
            Application.Exit()
        End If
    End Sub



    Private Sub Frm_Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _User = BLL.BLLSession.Current.User

        Me.Text = "MAT.NET " & version() & "   " & _User.NickName

        DeveloperToolStripMenuItem.Checked = (GetSetting("MATSOFT", "MAT.NET", "Developer") <> "")


        Dim sWidth As String = BLL.BLLApp.GetSetting(DTOSession.Settings.FrmIdx_Width)
        Dim sHeight As String = BLL.BLLApp.GetSetting(DTOSession.Settings.FrmIdx_Height)
        Dim sSplitter As String = BLL.BLLApp.GetSetting(DTOSession.Settings.FrmIdx_Splitter)

        If IsNumeric(sWidth) Then
            Me.Width = CInt(sWidth)
        End If
        If IsNumeric(sHeight) Then
            Me.Height = CInt(sHeight)
        End If
        If IsNumeric(sSplitter) Then
            Me.SplitContainer1.SplitterDistance = CInt(sSplitter)
        End If

        refrescaTreeview()
        _AllowEvents = True
    End Sub

    Private Sub refrescaTreeview()
        _WinMenuItems = BLL.BLLWinMenuItems.All(_User.Rol)
        Xl_WinMenuTree1.Load(_WinMenuItems, LastMenuSelection)
        RefrescaListView(Xl_WinMenuTree1.SelectedNode.Tag)
    End Sub

    Private Sub RefrescaListView(item As DTOWinMenuItem)
        Select Case item.CustomTarget
            Case DTOWinMenuItem.CustomTargets.Bancs
                BLL.BLLWinMenuItem.loadBancs(item)
            Case DTOWinMenuItem.CustomTargets.Staff
                BLL.BLLWinMenuItem.loadStaff(item)
            Case DTOWinMenuItem.CustomTargets.Reps
                BLL.BLLWinMenuItem.loadReps(item)
        End Select
        Xl_WinMenuListView1.Load(item.Children)
    End Sub

    Private Function LastMenuSelection() As DTOWinMenuItem
        Dim retval As DTOWinMenuItem = Nothing
        Dim sLastSelectedMenuitem As String = BLL.BLLApp.GetSetting(DTOSession.Settings.Last_Menu_Selection)
        If GuidHelper.IsGuid(sLastSelectedMenuitem) Then
            retval = New DTOWinMenuItem(New Guid(sLastSelectedMenuitem))
        End If
        Return retval
    End Function

    Private Sub Xl_WinMenuTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WinMenuTree1.RequestToRefresh
        refrescaTreeview()
    End Sub

    Private Sub Xl_WinMenuTree1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_WinMenuTree1.ValueChanged
        Dim item As DTOWinMenuItem = e.Argument
        RefrescaListView(item)
    End Sub

    Private Sub Xl_WinMenuListView1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_WinMenuListView1.onItemSelected
        Dim item As DTOWinMenuItem = e.Argument
        Select Case item.CustomTarget
            Case Else
                CallByName(Me, item.Action, CallType.Method)
        End Select
    End Sub


    Private Sub DesconectarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesconectarToolStripMenuItem.Click
        BLL_Session.LogOff(BLL.BLLSession.Current)

        Me.Close()
    End Sub

    Private Sub Frm_Idx_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        BLL.BLLApp.SaveSetting(DTOSession.Settings.FrmIdx_Width, Me.Width)
        BLL.BLLApp.SaveSetting(DTOSession.Settings.FrmIdx_Height, Me.Height)
        BLL.BLLApp.SaveSetting(DTOSession.Settings.FrmIdx_Splitter, SplitContainer1.Panel1.Width)

        Dim oNode As TreeNode = Xl_WinMenuTree1.SelectedNode
        If oNode IsNot Nothing Then
            Dim oWinMenuItem As DTOWinMenuItem = oNode.Tag
            BLL.BLLApp.SaveSetting(DTOSession.Settings.Last_Menu_Selection, oWinMenuItem.Guid.ToString)
        End If

        BLL_Session.Close(BLL.BLLSession.Current)
    End Sub

    Public Function version() As String
        Dim retval As String = ""
        Try
            If My.Application.IsNetworkDeployed Then
                With My.Application.Deployment.CurrentVersion
                    retval = .Major & "." & .Minor & "." & .Revision
                End With
            Else
                retval = "(versió de desenvolupador)"
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Private Sub Frm_Idx_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        MsgBox(My.Application.Deployment.CurrentVersion.Revision)
    End Sub

#Region "Procedures"

    Public Sub ExeFacturacio()
        root.ExeFacturacio()
    End Sub

    Public Sub ExeMailing()
        root.ExeMailing()
    End Sub

    Public Sub ExeVtosUpdate()
        root.ExeVtosUpdate()
    End Sub

    Public Sub ExePncRecalc()
        root.PncRecalc()
    End Sub

    Public Sub ExeWebAtlasUpdate()
        Dim oFrm As New Frm_Tarea
        With oFrm
            .Show()
            .Caption = "Actualitzan atlas web..."
            Dim exs As New List(Of Exception)
            If ProductAtlasLoader.UpdateWebAtlas(exs) Then
                .Fin("Atlas web actualitzat.")
            Else
                UIHelper.WarnError(exs, "error al actualitzar punts de venda")
                .Close()
            End If
        End With
    End Sub


    Public Sub SelPdc()
        root.ShowDocs(Frm_Doc_Select.Styles.Comanda)
    End Sub

    Public Sub SelIncidencia()
        root.ShowDocs(Frm_Doc_Select.Styles.Incidencia)
    End Sub

    Public Sub SelAlb()
        root.ShowDocs(Frm_Doc_Select.Styles.Albara)
    End Sub

    Public Sub SelFra()
        root.ShowDocs(Frm_Doc_Select.Styles.Factura)
    End Sub

    Public Sub SelCca()
        root.ShowDocs(Frm_Doc_Select.Styles.Assentament)
    End Sub

    Public Sub SelMail()
        'root.ShowDocs(Frm_Doc_Select.Styles.mail)
    End Sub

    Public Sub SelSQL()
        Dim oFrm As New Frm_SQLError
        oFrm.Show()
        Me.Close()
    End Sub

    Public Sub ShowPaisos()
        Dim oFrm As New Frm_Geo(BLL.BLLApp.Org.Address.Zip.Location)
        oFrm.Show()
    End Sub


    Public Sub ShowArts()
        root.ShowArts()
    End Sub

    Public Sub ShowArts2()
        'Dim oFrm As New Frm_Arts2
        'oFrm.Show()
    End Sub

    Public Sub ShowBancs()
        'nostres
        root.ShowBancs()
    End Sub

    Public Sub ShowTelSearch()
        root.ShowTelSearch()
    End Sub

    Public Sub ShowCsas()
        root.ShowCsas()
    End Sub

    Public Sub ShowCurs()
        root.ShowCurs()
    End Sub

    Public Sub ShowDescuadreClients()
        'root.ShowDescuadreClients_Old()
        root.ShowClientsQueNoCuadren()
    End Sub


    Public Sub ShowStaffs()
        root.ShowStaffs()
    End Sub

    Public Sub ShowLastAlbs()
        root.ShowLastAlbs()
    End Sub

    Public Sub ShowLastCcas()
        root.ShowLastCcas()
    End Sub

    Public Sub ShowLastFras()
        root.ShowLastFras()
    End Sub

    Public Sub ShowLastMails()
        root.ShowLastMails()
    End Sub

    Public Sub ShowLastMems()
        root.ShowLastMems()
    End Sub

    Public Sub ShowLastPdcClients()
        root.ShowLastPdcClients()
    End Sub

    Public Sub ShowStatGeoMes()
        root.ShowStatGeoMes()
    End Sub

    Public Sub ShowMiscImg()
        root.ShowMiscImg()
    End Sub

    Public Sub WzPagament()
        '
        'root.WzPagament()
    End Sub

    Public Sub WzGirsNoDomiciliats()
        root.WzGirs(DTOCsa.Types.AlDescompte, True)
    End Sub

    Public Sub WzGirs()
        root.WzGirs(DTOCsa.Types.AlDescompte)
    End Sub

    Public Sub WzGirsAlCobro()
        root.WzGirs(DTO.DTOCsa.Types.AlCobro)
    End Sub

    Public Sub UpdateContacts()
        'root.MatOutlook.UpdateContacts()
    End Sub

    Public Sub ShowPrevisions()
        root.ShowPrevisions()
    End Sub

    Public Sub ShowMgzStks()
        Dim oMgz As DTOMgz = BLL.BLLApp.Mgz
        Dim oFrm As New Frm_Mgz_Stks(oMgz)
        oFrm.Show()
    End Sub


    Public Sub ShowAdmBalSdos()
        root.ShowAdmBalSdos()
    End Sub


    Public Sub ShowAEB19()
        root.ShowAEB19()
    End Sub

    Public Sub ShowCdMake()
        root.ShowCdMake()
    End Sub

    Public Sub ShowTransmisions()
        root.ShowTransmisions(Today.Year)
    End Sub

    Public Sub NewTransmisio()
        root.NewTransmisio()
    End Sub

    Public Sub NewClientPdc()
        'root.NewCliPdc()
    End Sub

    Public Sub ShowTransmConfig()
        root.ShowTransmConfig()
    End Sub

    Public Sub OldShowFrasBook()
        'root.ShowFrasBook()
    End Sub

    Public Sub ShowAdmMrts()
        root.ShowAdmMrts()
    End Sub

    Public Sub ImportMailBoxAttachments()
        root.ImportMailBoxAttachments()
    End Sub

    Public Sub ShowCcaSearch()
        root.ShowCcaSearch()
    End Sub

    Public Sub NewCca()
        root.NewCca()
    End Sub

    Public Sub ShowBalSumasYSaldos()
        root.ShowBalSumasYSaldos(Today)
    End Sub

    Public Sub ShowProveidorsQueNoCuadren()
        root.ShowProveidorsQueNoCuadren()
    End Sub

    Public Sub ShowClientsQueNoCuadren()
        root.ShowClientsQueNoCuadren()
    End Sub

    Public Sub NewYearSdos()
        root.NewYearSdos()
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

    Public Sub ShowCyc()
        root.ShowCyc()
    End Sub

    Public Sub ShowSelArt()
        root.ShowSelArt()
    End Sub

    Public Sub ShowProveidorsVenciments()
        root.ShowProveidorsVenciments()
    End Sub

    Public Sub test()
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
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            Dim sFileName As String
            For Each sFileName In files
                Try
                    root.ImportedFileManager(sFileName)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return
                End Try

            Next
        End If
    End Sub


    Public Sub ShowAtlasContacts()
        root.ShowAtlasContacts()
    End Sub

    Public Sub ShowLastSpvs()
        root.ShowLastSpvs()
    End Sub


    Public Sub ShowConfig()
        root.ShowConfig()
    End Sub

    Public Sub ShowUsrEvts()
        root.ShowUsrEvts()
    End Sub

    Public Sub ShowReembolsos()
        'root.ShowReembolsos()
    End Sub

    Public Sub ShowPnds()
        root.ShowPnds()
    End Sub

    Public Sub ShowBalance()
        root.ShowBalances()
    End Sub

    Private Sub ServidorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServidorToolStripMenuItem.Click
        Dim oFrm As New Frm_SQLError
        oFrm.Show()
        Me.Close()
    End Sub



    Private Sub EmpresaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmpresaToolStripMenuItem.Click
        Dim oFrm As New Frm_Emps(BLL.BLLApp.Emp)
        AddHandler oFrm.SelectedItemChanged, AddressOf onEmpChanged
        oFrm.Show()
    End Sub

    Private Sub onEmpChanged(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oEmp As DTOEmp = e.Argument
        BLL.BLLApp.Emp = oEmp
        App.Current.emp = New Emp(oEmp.Id)
        MaxiSrvr.Current.SetEmp(oEmp)
        'Xl_Contact1.Emp = oEmp
        Me.Text = App.Current.emp.Org.Nom_o_NomComercial
    End Sub

    Public Sub ShowMod347()
        Dim oFrm As New Frm_AeatMod347
        oFrm.Show()
    End Sub

    Public Sub ShowFiscalIva()
        root.ShowFiscalIva()
    End Sub

    Public Sub ShowFiscalLlibresOficials()
        root.ShowFiscalLlibresOficials()
    End Sub

    Public Sub ShowEtqs()
        root.ShowEtqs()
    End Sub

    Public Sub ShowImpagats()
        root.ShowImpagats()
    End Sub

    Public Sub ShowImpagatsCYC()
        root.ShowImpagatsCYC()
    End Sub

    Public Sub ShowRebut()
        root.ShowRebut()
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
        root.ShowFiscalIrpf()
    End Sub


    Public Sub ShowBanks()
        Dim oFrm As New Frm_Banks()
        oFrm.Show()
    End Sub

    Public Sub TransferNominas()
        root.TransferNominas()
    End Sub

    Public Sub ShowFrasToPrint()
        root.ShowFrasToPrint()
    End Sub

    Public Sub ShowRevistas()
        root.ShowRevistas()
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR FITXER"
            .Filter = "arxius XML (*.xml)|*.xml|tots els arxius|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Select Case .ShowDialog
                Case Windows.Forms.DialogResult.OK
                    root.ImportedFileManager(.FileName)
            End Select
        End With
    End Sub

    Public Sub ShowECIconfig()
        root.ShowECIconfig()
    End Sub

    Public Sub NewPdcPrv()
        Dim oProveidor As Proveidor = MaxiSrvr.Proveidor.FromNum(BLL.BLLApp.Emp, 6009)
        root.NewPdcPrv(oProveidor)
    End Sub

    Public Sub SowAeatMods()
        root.SowAeatMods()
    End Sub

    Public Sub ShowMovies()
        root.ShowMovies()
    End Sub

    Public Sub ShowVivaceStk()
        root.ShowVivaceStk()
    End Sub

    Public Sub ShowImportacions()
        root.ShowImportacions()
    End Sub



    Private Sub DeveloperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeveloperToolStripMenuItem.Click
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        oMenuItem.Checked = Not oMenuItem.Checked
        If oMenuItem.Checked Then
            SaveSetting("MATSOFT", "MAT.NET", "Developer", "1")
        Else
            SaveSetting("MATSOFT", "MAT.NET", "Developer", "")
        End If
    End Sub

    Private Sub Xl_DropFile1_AfterFileDropped(ByVal sender As System.Object, ByVal e As DropEventArgs) Handles Xl_DropFile1.onFileDropped
        Dim exs As New List(Of Exception)
        Xl_DropFile1.DisplayStatus(Xl_DropFile.Status.Wait)
        Cursor = Cursors.WaitCursor
        For Each oDocFile As DTODocFile In e.DocFiles
            Select Case oDocFile.Mime
                Case DTOEnums.MimeCods.Xml
                    Dim oDoc As Xml.XmlDocument = BLL.FileSystemHelper.GetXMLfromByteStream(oDocFile.Stream)
                    root.ImportedXMLFileManager(oDoc)

                Case DTOEnums.MimeCods.Rtf

                    Dim sFilename As String = ""
                    If My.Computer.FileSystem.FileExists(BLL_DocFile.FileNameOrDefault(oDocFile)) Then
                        sFilename = BLL_DocFile.FileNameOrDefault(oDocFile)
                    Else
                        'si ve de un correu attachment cal desar-lo al disc perque l'obri MS Word Interop
                        If Not BLL.FileSystemHelper.SaveStream(oDocFile.Stream, exs, sFilename) Then
                            UIHelper.WarnError(exs, "error al desar el fitxer Rtf al disc")
                            Exit Sub
                        End If
                    End If

                    Dim oFrm_NominasNew As New Frm_NominasNew(sFilename, Today)
                    oFrm_NominasNew.Show()
                    Exit For


                Case DTOEnums.MimeCods.Csv
                    If Amazon.IsItemListCsv(oDocFile) Then
                        Dim oMemStream As New System.IO.MemoryStream(oDocFile.Stream)
                        Dim oItems As New Amazon_ItemListCsvs(oMemStream)
                        Dim oFrm As New Frm_Amazon_ItemListCsv(oItems)
                        oFrm.Show()
                    End If
                Case DTOEnums.MimeCods.NotSet
                    CheckAEAT(oDocFile)
                Case DTOEnums.MimeCods.Txt, DTOEnums.MimeCods.Pla
                    Dim oFlatFile As DTOFlatFile = BLL.BLLFlatFile.FromDocFile(oDocFile, exs)
                    If exs.Count > 0 Then
                        UIHelper.WarnError(exs)
                    Else
                        If oFlatFile Is Nothing Then
                            Select Case System.IO.Path.GetFileName(BLL_DocFile.FileNameOrDefault(oDocFile))
                                Case "nrc.txt"
                                    Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.Nrc)
                                    If Xl_DropFile1.DocFiles.Count > 0 Then
                                        oFile.Load(BLL_DocFile.FileNameOrDefault(oDocFile))
                                        Dim oFrm As New Frm_FlatFileFixLen(oFile)
                                        oFrm.Show()
                                    End If
                                Case Else
                                    If Not CheckAEAT(oDocFile) Then

                                    End If
                                    Dim oMemStream As New IO.MemoryStream(oDocFile.Stream)
                                    Dim sr As IO.StreamReader = New IO.StreamReader(oMemStream)
                                    Dim line As String = sr.ReadLine
                                    sr.Close()

                                    Dim sFilename As String = BLL_DocFile.FileNameOrDefault(Xl_DropFile1.DocFiles.First)
                                    Dim iLineLength As Integer = line.Length
                                    Dim s As String = line.Substring(0, 3)
                                    If iLineLength = 348 And line.Substring(0, 2) = "10" Then
                                        Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.RMELaCaixa)
                                        If Xl_DropFile1.DocFiles.Count > 0 Then
                                            oFile.Load(sFilename)
                                            Dim oFrm As New Frm_FlatFileFixLen(oFile)
                                            oFrm.Show()
                                        End If
                                    ElseIf iLineLength = 600 And line.Substring(0, 7) = "0119445" Then
                                        Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.SEPAB2B_Adeudos)
                                        If Xl_DropFile1.DocFiles.Count > 0 Then
                                            oFile.Load(sFilename)
                                            Dim oFrm As New Frm_FlatFileFixLen(oFile)
                                            oFrm.Show()
                                        End If
                                    ElseIf iLineLength = 600 And line.Substring(0, 7) = "1119445" Then
                                        Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.SEPAB2B_Rechazos)
                                        If Xl_DropFile1.DocFiles.Count > 0 Then
                                            oFile.Load(sFilename)
                                            Dim oFrm As New Frm_FlatFileFixLen(oFile)
                                            oFrm.Show()
                                        End If
                                    ElseIf iLineLength = 600 And line.Substring(2, 5) = "2119445" Then
                                        Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.SEPAB2B_Devoluciones)
                                        If Xl_DropFile1.DocFiles.Count > 0 Then
                                            oFile.Load(sFilename)
                                            Dim oFrm As New Frm_FlatFileFixLen(oFile)
                                            oFrm.Show()
                                        End If

                                    End If
                            End Select
                        Else
                            Dim oFrm As New Frm_FlatFile(oFlatFile)
                            oFrm.Show()
                        End If
                    End If

                Case Else
                    root.ShowFileImport(oDocFile)
            End Select
        Next
        Xl_DropFile1.DisplayStatus(Xl_DropFile.Status.Ready)
        Cursor = Cursors.Default
    End Sub



    Private Function CheckAEAT(oDocFile As DTODocFile) As Boolean
        Dim retval As Boolean = False
        Dim line As String '= GetFirstLineFromFile(oDocFile)
        Exit Function
        Dim sFilename As String = BLL_DocFile.FileNameOrDefault(Xl_DropFile1.DocFiles.First)
        Try
            Dim iLineLength As Integer = line.Length
            Dim s As String = line.Substring(0, 3)
            If iLineLength = 984 And s = "111" Then
                Dim oFile As New MaxiSrvr.MatFileAEAT111(oDocFile.Stream)
                Dim oFrm As New Frm_FileAEAT111
                oFrm.File = oFile
                oFrm.Show()
                retval = True
            ElseIf iLineLength = 1050 And s = "115" Then
                Dim oFile As New MaxiSrvr.MatFileAEAT115(oDocFile.Stream)
                Dim oFrm As New Frm_FileAEAT115
                oFrm.File = oFile
                oFrm.Show()
                retval = True
            ElseIf iLineLength = 1050 And s = "303" Then
                Dim oFile As New MaxiSrvr.MatFileAEAT303(oDocFile.Stream)
                Dim oFrm As New Frm_FileAEAT303
                oFrm.File = oFile
                oFrm.Show()
                retval = True
            ElseIf iLineLength = 500 And line.Substring(1, 3) = "347" Then
                Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.AEAT347_2012)
                If Xl_DropFile1.DocFiles.Count > 0 Then
                    oFile.Load(sFilename)
                    Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    oFrm.Show()
                End If
            ElseIf iLineLength = 348 And line.Substring(0, 2) = "10" Then
                Dim oFile As New MaxiSrvr.FlatFileFixLen(DTO.DTOFlatFile.Ids.RMELaCaixa)
                If Xl_DropFile1.DocFiles.Count > 0 Then
                    oFile.Load(sFilename)
                    Dim oFrm As New Frm_FlatFileFixLen(oFile)
                    oFrm.Show()
                    retval = True
                End If
            End If
        Catch ex As Exception
            root.ShowFileImport(oDocFile)
        End Try
        Return retval
    End Function

    Public Sub ClearAlbBloqueig()
        root.ClearAlbBloqueig()
    End Sub

    Public Sub EmailAvisame()
        root.EmailAvisame()
    End Sub

    Public Sub ShowEanEci()
        root.ShowEanEci()
    End Sub



    Public Sub ShowInfoJobs()
        root.ShowInfoJobs()
    End Sub

    Public Sub ShowWebLog()
        root.ShowWebLog()
    End Sub

    Public Sub ShowAuditoria()
        root.ShowAuditoria()
    End Sub


    Public Sub ShowPGC()
        root.ShowPGC()
    End Sub

    Public Sub ShowSegSocNewTc1()
        root.ShowSegSocNewTc1()
    End Sub

    Public Sub OldShowLangs()
        'Dim oFrm As New Frm_Langs
        'oFrm.Show()
    End Sub

    Public Sub ShowLangResources()
        Dim oFrm As New Frm_LangResourcess
        oFrm.Show()
    End Sub

    Public Sub ShowDownloadSrc()
        root.ShowDownloadSrc()
    End Sub

    Public Sub ShowIncidencies()
        Dim oQuery As DTOIncidenciaQuery = BLL_Incidencies.DefaultQuery(BLL.BLLSession.Current.User)
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Public Sub Intrastats()
        Dim oFrm As New Frm_Intrastats
        oFrm.Show()
    End Sub


    Public Sub ShowPdcFchCreated()
        root.ShowPdcFchCreated()
    End Sub

    Public Sub ShowCanarias()
        root.ShowCanarias()
    End Sub

    Public Sub ShowCodisMercancia()
        root.ShowCodisMercancia()
    End Sub

    Public Sub ShowPncsXFch()
        root.ShowPncsXFch()
    End Sub

    Public Sub ShowFlota()
        root.ShowFlota()
    End Sub

    Public Sub ShowGeoFras()
        root.ShowGeoFras()
    End Sub

    Public Sub ShowFaqs()
        root.ShowFaqs()
    End Sub


    Public Sub ShowContactEmails()
        root.ShowContactEmails()
    End Sub

    Public Sub ShowContracts()
        root.ShowContracts()
    End Sub

    Public Sub ShowAlbConfirms()
        root.ShowAlbConfirms()
    End Sub

    Public Sub ShowEscriptures()
        root.ShowEscriptures()
    End Sub

    Public Sub ShowAsnef()
        root.ShowAsnef()
    End Sub

    Public Sub ShowBancConciliacions()
        root.ShowBancConciliacions()
    End Sub

    Public Sub Show_AEAT_Mod349()
        root.Show_AEAT_Mod349()
    End Sub

    Public Sub ShowQ43()
        root.ShowQ43()
    End Sub

    Public Sub ShowAlbsTransferenciaPrevia()
        root.ShowAlbsTransferenciaPrevia()
    End Sub

    Public Sub ShowAlbsPendentsDeVisa()
        root.ShowAlbsPendentsDeVisa()
    End Sub

    Public Sub ShowNukPncs()
        root.ShowNukPncs()
    End Sub

    Public Sub ShowPorts()
        root.ShowPorts()
    End Sub

    Public Sub ShowEdi()
        root.ShowEdi()
    End Sub

    Public Sub ShowMod349()
        root.ShowMod349()
    End Sub

    Public Sub ShowClientsEnActiu()
        root.ShowClientsEnActiu()
    End Sub

    Public Sub ShowCnaps()
        root.ShowCnaps()
    End Sub



    Public Sub ShowNukDesadvs()
        root.ShowNukDesadvs()
    End Sub

    Public Sub ShowNukPdcs()
        root.ShowNukPdcs()
    End Sub

    Public Sub ShowEventos()
        root.ShowEventos()
    End Sub

    Public Sub ShowTxts()
        root.ShowTxts()
    End Sub

    Public Sub ShowPr()
        root.ShowPr()
    End Sub

    Public Sub ShowTasks()
        root.ShowTasks()
    End Sub

    Public Sub ShowNews()
        root.ShowNews()
    End Sub

    Public Sub ShowSubscripcions()
        root.ShowSubscripcions()
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

    Public Sub ShowTelefons()
        root.ShowTelefons()
    End Sub

    Public Sub ShowInspeccio2008()
        root.ShowInspeccio2008()
    End Sub

    Public Sub ShowFileDocuments()
        Dim oFrm As New Frm_FileDocuments
        oFrm.Show()
    End Sub

    Public Sub ShowCreditsClients()
        root.ShowCreditsClients()
    End Sub

    Public Sub ShowEFras()
        root.ShowEFras()
    End Sub

    Public Sub ShowSystemUsers()
        root.ShowSystemUsers()
    End Sub

    Public Sub ShowBigfiles()
        root.ShowBigfiles()
    End Sub

    Public Sub ShowProductDownloads()
        root.ShowProductDownloads()
    End Sub

    Public Sub ShowYoutubes()
        root.ShowYoutubes()
    End Sub

    Public Sub ShowCliCredits()
        root.ShowCliCredits()
    End Sub

    Public Sub ShowColeccions()
        root.ShowColeccions()
    End Sub

    Public Sub ShowFlatFileFixLens()
        Dim oFrm As New Frm_FlatFileFixLens
        oFrm.Show()
    End Sub

    Public Sub ShowWebMenuItems()
        root.ShowWebMenuItems()
    End Sub

    Public Sub ShowEnquestes()
        root.ShowEnquestes()
    End Sub

    Public Sub ShowCnapsStat()
        root.ShowCnapsStat()
    End Sub

    Public Sub ShowGeoPdcs()
        root.ShowGeoPdcs()
    End Sub

    Public Sub ShowCatalegUpload()
        root.ShowCatalegUpload()
    End Sub

    Public Sub ShowEciPdcs()
        root.ShowEciPdcs()
    End Sub

    Public Sub ShowQuizs()
        root.ShowQuizs()
    End Sub

    Public Sub ShowBookFras()
        root.ShowBookFras()
    End Sub

    Public Sub ShowWebQuadRelay()
        root.ShowWebQuadRelay()
    End Sub

    Public Sub ShowBlog()
        root.ShowBlog()
    End Sub

    Public Sub ShowExport()
        root.ShowExport()
    End Sub

    Public Sub ShowSepaBancs()
        root.ShowSepaBancs()
    End Sub

    Public Sub Show_Last_albs_enAltres()
        Dim oFrm As New Frm_Last_albs_enAltres
        oFrm.Show()
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

    Public Sub ShowRtf()
        Dim oFrm As New Frm_Rtf
        oFrm.Show()
    End Sub

    Public Sub ShowMailings()
        Dim oFrm As New Frm_Mailings
        oFrm.Show()
    End Sub

    Public Sub ShowEdiRemadvs()
        Dim oFrm As New Frm_EdiRemadvs
        oFrm.Show()
    End Sub

    Public Sub ShowBriqExams()
        Dim oFrm As New Frm_BriqExams
        oFrm.Show()
    End Sub

    Public Sub ShowHighResImagesManager()
        Dim oFrm As New Frm_HighResImagesManager
        oFrm.Show()
    End Sub

    Public Sub ShowBanners()
        Dim oFrm As New Frm_Banners
        oFrm.Show()
    End Sub

    Public Sub ShowAwards()
        Dim oFrm As New Frm_Awards
        oFrm.Show()
    End Sub

    Public Sub ShowTrainingSessions()
        Dim oFrm As New Frm_TrainingSessions
        oFrm.Show()
    End Sub

    Public Sub ShowStats()
        Dim oFrm As New Frm_Stats
        oFrm.Show()
    End Sub

    Public Sub ShowCustomerRanking()
        Dim oRanking As Ranking = BLL_Ranking.CustomerRanking(BLL.BLLSession.Current.User)
        Dim oFrm As New Frm_Ranking(oRanking)
        oFrm.Show()
    End Sub

    Public Sub ShowDefaultImage()
        Dim oFrm As New Frm_DefaultImage
        oFrm.Show()
    End Sub

    Public Sub ShowCliProductWebBlocks()
        Dim oFrm As New Frm_CliProductWebBlocks
        oFrm.Show()
    End Sub

    Public Sub ShowDiari()
        Dim oFrm As New Frm_Diari
        oFrm.Show()
    End Sub

    Public Sub ShowEvents()
        Dim oFrm As New Frm_Events
        oFrm.Show()
    End Sub

    Public Sub EciPlatformsNewAlbs()
        Dim oContact As Contact = Contact.FromNum(BLL.BLLApp.Emp, ECI.Id)
        Dim oCustomer As New DTOCustomer(oContact.Guid)

        Dim oFrm As New Frm_PlatformsNewAlb(oCustomer)
        oFrm.Show()

    End Sub

    Public Sub ShowRaffles()
        Dim oFrm As New Frm_Raffles
        oFrm.Show()
    End Sub

    Public Sub ShowBloggers()
        Dim oFrm As New Frm_Bloggers(BLL.Defaults.SelectionModes.Browse)
        oFrm.Show()
    End Sub

    Public Sub ShowIbans()
        Dim oFrm As New Frm_Ibans
        oFrm.Show()
    End Sub

    Public Sub ShowMeetings()
        Dim oFrm As New Frm_Meetings
        oFrm.Show()
    End Sub

    Public Sub ShowPurchaseOrders()
        Dim oFrm As New Frm_PurchaseOrders
        oFrm.Show()
    End Sub

    Public Sub ShowBancTerms()
        Dim oFrm As New Frm_BancTerms
        oFrm.Show()
    End Sub

    Public Sub ShowAreas()
        Dim oFrm As New Frm_Areas
        oFrm.Show()
    End Sub

    Public Sub ShowProducts()
        Dim oFrm As New Frm_Products
        oFrm.Show()
    End Sub

    Public Sub ShowMvcMenus()
        Dim oFrm As New Frm_MvcMenus
        oFrm.Show()
    End Sub

    Public Sub ShowVivaceStocks()
        Dim oFrm As New Frm_Vivace_Stocks
        oFrm.Show()
    End Sub

    Public Sub ShowPromos()
        Dim oFrm As New Frm_Promos
        oFrm.Show()
    End Sub

    Public Sub ShowFairGuests()
        Dim oFrm As New Frm_FairGuests
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

#End Region


End Class
