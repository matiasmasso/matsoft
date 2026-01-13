
Imports Microsoft.Office.Interop

Public Module root

    Public Function Usuari() As MaxiSrvr.Contact
        Return New Contact(BLL.BLLSession.Current.Contact.Guid)
    End Function

    Public Property ServerName() As String
        Get
            Return maxisrvr.ServerName
        End Get
        Set(ByVal value As String)
            maxisrvr.ServerName = value
        End Set
    End Property

    Public Sub AddMenuCsa(ByRef oMenu As ContextMenu, ByVal oCsa As Csa)
        Dim sRoot As String = "remesa " & oCsa.Id
        Dim oItm As MenuItem
        For Each oItm In oMenu.MenuItems
            If oItm.Text = sRoot Then Exit Sub
        Next

        Dim oCsaMenu As New Xl_Csa_Menu
        oCsaMenu.Csa = oCsa
        Dim oRootItm As MenuItem = oMenu.MenuItems.Add(sRoot)

        For Each oItm In oCsaMenu.MenuItems
            oRootItm.MenuItems.Add(oItm.CloneMenu)
        Next
    End Sub

    Public Sub WzCobrament(ByVal oContact As Contact)
        Dim oFrm As New Frm_Cobrament(oContact)
        oFrm.Show()
    End Sub

    Public Sub WzPagament(ByVal oContact As Contact, Optional ByVal DtFch As Date = Nothing, Optional ByVal oDocFile As DTODocFile = Nothing)
        If DtFch = Nothing Then DtFch = Today
        Dim oFrm As New Frm_Pagament
        With oFrm
            .Proveidor = oContact
            .Fch = DtFch
            .DocFile = oDocFile
            .Show()
        End With
        oFrm.Show()
    End Sub

    Public Sub WzGirs(ByVal oCsaType As DTO.DTOCsa.Types, Optional ByVal BlOnlyNoDomiciliats As Boolean = False)
        Dim ofrm As New Frm_Girs2 ' Wz_Girs
        'ofrm.OnlyNoDomiciliats = BlOnlyNoDomiciliats
        'ofrm.CsaType = oCsaType
        ofrm.Show()
    End Sub


    Public Sub ExeFacturacio(Optional ByVal oAlbs As Albs = Nothing)
        Dim oFrm As New Frm_Facturacio
        With oFrm
            .Albs = oAlbs
            .Show()
        End With
    End Sub

    Public Sub ExeMailing()
        Dim oFrm As New Frm_Mailing
        With oFrm
            .Show()
        End With
    End Sub


    Public Function ExportDataTableToTextStream(ByVal oTb As DataTable) As String
        Dim oRow As DataRow
        Dim i As Integer
        Dim Str As String = ""

        For i = 0 To oTb.Columns.Count - 1
            Str = Str & oTb.Columns(i).Caption
            If i = oTb.Columns.Count - 1 Then
                Str = Str & vbCrLf
            Else
                Str = Str & vbTab
            End If
        Next

        For Each oRow In oTb.Rows
            For i = 0 To oTb.Columns.Count - 1
                Str = Str & oRow(i)
                If i = oTb.Columns.Count - 1 Then
                    Str = Str & vbCrLf
                Else
                    Str = Str & vbTab
                End If
            Next
        Next
        Return Str
    End Function



    Public Sub EMailPunts(ByVal oClient As Client)
        Dim DtFch As Date = Today
        Dim IntYea As Integer = Today.Year
        Dim IntMes As Integer = Today.Month
        'App.Current.Emp.EMail_Punts(oClient, IntYea, IntMes)
    End Sub



    Public Sub NewCsa(ByVal oBanc As MaxiSrvr.Banc, ByVal oFileFormat As DTOCsa.FileFormats, ByVal oCsaType As DTOCsa.Types)
        Dim oCsa As New Csa(oBanc, oFileFormat, oCsaType)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Public Sub NewBancAEB19Impag(ByVal oBanc As Banc)
        Dim oFrm As New Frm_Banc_AEB19_Impag
        With oFrm
            .Banc = oBanc
            .Show()
        End With
    End Sub

    Public Sub NewCca()
        Dim oCca As New cca(BLL.BLLApp.emp)
        oCca.fch = Today
        root.ShowCca(oCca)
    End Sub

    Public Sub NewPrvEntrada(ByVal oProveidor As Proveidor)
        Dim oAlb As Alb = oProveidor.NewAlb
        Dim oFrm As New Frm_AlbNew2(oAlb)
        With oFrm
            .Show()
        End With
    End Sub



    Public Sub NewCliAlbNew(ByVal oClient As Client)
        Dim oAlb As Alb = oClient.NewAlb
        Dim oCustomer As New DTOCustomer(oAlb.Client.Guid)
        BLL.BLLContact.Load(oCustomer)

        'check bloqueig
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oMsgBoxStyle As MsgBoxStyle = MsgBoxStyle.OkOnly
            Dim oRol As DTORol = BLL.BLLSession.Current.User.Rol
            If oRol.IsAdmin Then oMsgBoxStyle = MsgBoxStyle.RetryCancel
            Dim rc As MsgBoxResult = MsgBox(exs(0).Message, oMsgBoxStyle, "MAT.NET")
            Select Case rc
                Case MsgBoxResult.Retry
                Case Else
                    Exit Sub
            End Select
        Else
            Dim oFrm As New Frm_AlbNew2(oClient.NewAlb())
            oFrm.Show()
        End If

    End Sub

    Public Sub NewCliPdc(ByVal oClient As Client)
        Dim oPdc As Pdc = oClient.NewPdc(Today, "")
        If oPdc.Client.Id <> oClient.Id Then
            MsgBox("Comanda passada a central", MsgBoxStyle.Information, "MAT.NET")
        End If

        If oPdc IsNot Nothing Then
            Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
            Dim exs As New List(Of Exception)
            If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                UIHelper.WarnError(exs)
            Else
                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                oFrm.Show()
            End If
        End If
    End Sub



    Public Sub NewRepTransfer()
        Dim oFrm As New Frm_Rep_Transfers
        With oFrm
            .NewTransferCod = Frm_Rep_Transfers.NewTransferCods.Reps
            .Show()
        End With
    End Sub

    Public Sub PrintCsas(ByVal oCsas As Csas, Optional ByVal oPrintMode As MaxiSrvr.ReportDocument.PrintModes = MaxiSrvr.ReportDocument.PrintModes.Preview)
        Dim oRpt As New Rpt_Csa
        oRpt.Csas = oCsas
        oRpt.Print(oPrintMode)
    End Sub

    Public Sub PrintPdcs(ByVal oPdcs As Pdcs, Optional ByVal oPrintMode As MaxiSrvr.ReportDocument.PrintModes = MaxiSrvr.ReportDocument.PrintModes.Preview)
        Dim oPdc As Pdc
        Dim oRpt As New maxisrvr.DocRpt(maxisrvr.DocRpt.Estilos.Comanda, maxisrvr.DocRpt.FuentePapel.Copia)
        Dim sNotFound As String = ""
        Dim BlFoundAny As Boolean = False
        For Each oPdc In oPdcs
            If oPdc.Exists Then
                oRpt.Docs.Add(oPdc.Doc)
                BlFoundAny = True
            Else
                If sNotFound > "" Then sNotFound = sNotFound & ", "
                sNotFound = sNotFound & oPdc.Yea.ToString & "/" & oPdc.Id.ToString
            End If
        Next

        If BlFoundAny Then
            Dim oFrm As New Frm_PrintDoc
            With oFrm
                .ShowDialog()
                If Not .Cancel Then
                    If .Preview Then
                        root.PrintDoc_Preview(oRpt)
                    Else
                        If .Copia Then
                            root.PrintDoc(oRpt)
                            'SelectedFras.Print(maxisrvr.DocRpt.FuentePapel.Copia)
                        End If
                        If .Original Then
                            oRpt.Papel = maxisrvr.DocRpt.FuentePapel.Original
                            root.PrintDoc(oRpt)
                            'SelectedFras.Print(maxisrvr.DocRpt.FuentePapel.Original)
                        End If
                    End If
                End If
            End With
        End If

        If sNotFound > "" Then
            Dim s As String = "les següents comandes no s'han pogut imprimir perque no s'han trobat:" & vbCrLf & sNotFound
            MsgBox(s, MsgBoxStyle.Exclamation, "MAT.NET")
        End If

    End Sub

    Public Sub PrintPdcForValencia(ByVal oPdc As Pdc)
        Dim oRpt As New maxisrvr.DocRpt(maxisrvr.DocRpt.Estilos.Comanda, maxisrvr.DocRpt.FuentePapel.Copia)
        oRpt.Docs.Add(oPdc.Doc)
        root.PrintDoc(oRpt, 1)
    End Sub

    Public Sub PrintAlbs_Old(ByVal oAlbs As Albs, Optional ByVal oPrintMode As MaxiSrvr.ReportDocument.PrintModes = MaxiSrvr.ReportDocument.PrintModes.Preview)
        Dim oAlb As Alb
        Dim oRpt As New maxisrvr.DocRpt(maxisrvr.DocRpt.Estilos.Albara, maxisrvr.DocRpt.FuentePapel.Copia)
        For Each oAlb In oAlbs
            oRpt.Docs.Add(oAlb.Doc)
        Next

        Dim oFrm As New Frm_PrintDoc
        With oFrm
            .ShowDialog()
            If Not .Cancel Then
                If .Preview Then
                    root.PrintDoc_Preview(oRpt)
                Else
                    If .Copia Then
                        root.PrintDoc(oRpt)
                        'SelectedFras.Print(maxisrvr.DocRpt.FuentePapel.Copia)
                    End If
                    If .Original Then
                        oRpt.Papel = maxisrvr.DocRpt.FuentePapel.Original
                        root.PrintDoc(oRpt)
                        'SelectedFras.Print(maxisrvr.DocRpt.FuentePapel.Original)
                    End If
                End If
            End If
        End With



    End Sub

    Public Sub ShowFrasToPrint()
        Dim oFrm As New Frm_Print_Fras
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub PrintFras(ByVal oFras As Fras, Optional ByVal SkipDialog As Boolean = False, Optional ByVal BlCopia As Boolean = True, Optional ByVal BlOriginal As Boolean = False, Optional ByVal BlPreview As Boolean = True)
        Dim oRpt As maxisrvr.DocRpt

        If Not SkipDialog Then
            Dim oFrm As New Frm_PrintDoc
            With oFrm
                .ShowDialog()
                If .Cancel Then
                    Exit Sub
                Else
                    BlPreview = .Preview
                    BlCopia = .Copia
                    BlOriginal = .Original
                End If
            End With
        End If

        If BlPreview Then
            oRpt = oFras.DocRpt(maxisrvr.DocRpt.FuentePapel.Original)
            root.PrintDoc_Preview(oRpt)
        Else
            If BlCopia Then
                oRpt = oFras.DocRpt(maxisrvr.DocRpt.FuentePapel.Copia)
                root.PrintDoc(oRpt)
            End If
            If BlOriginal Then
                oRpt = oFras.DocRpt(maxisrvr.DocRpt.FuentePapel.Original)
                root.PrintDoc(oRpt)
                oFras.LogPrint(New Contact(BLL.BLLSession.Current.Contact.Guid))
            End If
        End If

    End Sub

    Public Sub PrintAlbs(ByVal oAlbs As Albs, Optional ByVal SkipDialog As Boolean = False, Optional ByVal BlCopia As Boolean = True, Optional ByVal BlOriginal As Boolean = False, Optional ByVal BlPreview As Boolean = True)
        Dim oRpt As MaxiSrvr.DocRpt

        If Not SkipDialog Then
            Dim oFrm As New Frm_PrintDoc
            With oFrm
                .ShowDialog()
                If .Cancel Then
                    Exit Sub
                Else
                    BlPreview = .Preview
                    BlCopia = .Copia
                    BlOriginal = .Original
                End If
            End With
        End If

        If BlPreview Then
            oRpt = oAlbs.DocRpt(MaxiSrvr.DocRpt.FuentePapel.Original)
            root.PrintDoc_Preview(oRpt)
        Else
            If BlCopia Then
                oRpt = oAlbs.DocRpt(MaxiSrvr.DocRpt.FuentePapel.Copia)
                root.PrintDoc(oRpt)
            End If
            If BlOriginal Then
                oRpt = oAlbs.DocRpt(MaxiSrvr.DocRpt.FuentePapel.Original)
                root.PrintDoc(oRpt)
            End If
        End If
    End Sub

    Public Sub PrintProforma(ByVal oPdcs As Pdcs, Optional ByVal SkipDialog As Boolean = False, Optional ByVal BlCopia As Boolean = True, Optional ByVal BlOriginal As Boolean = False, Optional ByVal BlPreview As Boolean = True)
        Dim oRpt As maxisrvr.DocRpt

        If Not SkipDialog Then
            Dim oFrm As New Frm_PrintDoc
            With oFrm
                .ShowDialog()
                If .Cancel Then
                    Exit Sub
                Else
                    BlPreview = .Preview
                    BlCopia = .Copia
                    BlOriginal = .Original
                End If
            End With
        End If

        If BlPreview Then
            oRpt = oPdcs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Original)
            root.PrintDoc_Preview(oRpt)
        Else
            If BlCopia Then
                oRpt = oPdcs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Copia)
                root.PrintDoc(oRpt)
            End If
            If BlOriginal Then
                oRpt = oPdcs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Original)
                root.PrintDoc(oRpt)
            End If
        End If
    End Sub

    Public Sub PrintProforma(ByVal oAlbs As Albs, Optional ByVal SkipDialog As Boolean = False, Optional ByVal BlCopia As Boolean = True, Optional ByVal BlOriginal As Boolean = False, Optional ByVal BlPreview As Boolean = True)
        Dim oRpt As maxisrvr.DocRpt

        If Not SkipDialog Then
            Dim oFrm As New Frm_PrintDoc
            With oFrm
                .ShowDialog()
                If .Cancel Then
                    Exit Sub
                Else
                    BlPreview = .Preview
                    BlCopia = .Copia
                    BlOriginal = .Original
                End If
            End With
        End If

        If BlPreview Then
            oRpt = oAlbs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Original)
            root.PrintDoc_Preview(oRpt)
        Else
            If BlCopia Then
                oRpt = oAlbs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Copia)
                root.PrintDoc(oRpt)
            End If
            If BlOriginal Then
                oRpt = oAlbs.DocRptProforma(maxisrvr.DocRpt.FuentePapel.Original)
                root.PrintDoc(oRpt)
            End If
        End If
    End Sub

    Public Function SelectZipFromZips(ByVal oZips As MaxiSrvr.Zips, Optional ByVal DefaultZip As MaxiSrvr.Zip = Nothing) As MaxiSrvr.Zip
        Dim oFrm As New Frm_Zips_Select
        With oFrm
            .Zips = oZips
            .ShowDialog()
            Return .Zip
        End With
    End Function

    Public Function SelectArt(ByVal oArts As MaxiSrvr.Arts) As MaxiSrvr.Art
        Dim oFrm As New frm_arts_select
        With oFrm
            .Arts = oArts
            .ShowDialog()
            Return .Art
        End With
    End Function


    Public Function SelectCta(ByRef oCta As PgcCta) As Boolean
        Dim retval As Boolean
        Dim oFrm As New Frm_Ctas_Select_Old
        With oFrm
            .Cta = oCta
            .ShowDialog()
            Dim oNewCta As PgcCta = .Cta
            If oCta Is Nothing Then
                retval = True
            Else
                If oNewCta IsNot Nothing Then
                    retval = (oCta.Id <> oNewCta.Id)
                End If
            End If
            oCta = oNewCta
        End With
        Return retval
    End Function



    Public Function SelectRegioFromPais(ByVal oCountry As Country, Optional ByVal oDefaultRegio As MaxiSrvr.Regio = Nothing) As MaxiSrvr.Regio
        Dim oRegio As Regio = Nothing
        Dim oRegions As MaxiSrvr.Regions = MaxiSrvr.GetRegionsFromPais(oCountry)
        If oRegions.Count = 0 Then
            Dim sPais As String = oCountry.Nom(BLL.BLLApp.Lang)
            Dim sWarn As String = sPais & " no té cap Regio registrada." & vbCrLf & "n'afegim una de nova?"
            Dim rc As MsgBoxResult = MsgBox(sWarn, MsgBoxStyle.YesNo, "M+O")
            If rc = MsgBoxResult.Yes Then
                Dim oNewRegio As New MaxiSrvr.Regio(oCountry)
                root.ShowRegio(oNewRegio)
                oRegio = oNewRegio
            End If
        Else
            Dim oFrm As New Frm_Regions_Select
            With oFrm
                .Regions = oRegions
                .Regio = oDefaultRegio
                .ShowDialog()
                oRegio = .Regio
            End With
        End If
        Return oRegio
    End Function


    Public Function SelectRol(Optional ByRef oRol As DTORol = Nothing) As Boolean
        If oRol Is Nothing Then oRol = DefaultRol()
        Dim oFrm As New Frm_Rol_Select
        With oFrm
            .Rol = oRol
            .ShowDialog()
            Dim oNewRol As DTORol = .Rol
            SelectRol = (oRol.Id <> oNewRol.Id)
            oRol = oNewRol
        End With
    End Function


    Public Function SaveCsaFile(ByVal oCsa As Csa, Optional ByVal oFormat As DTOCsa.FileFormats = DTOCsa.FileFormats.NotSet) As String
        Dim sTit As String = ""
        Dim sPrefixe As String = ""
        If oFormat = DTOCsa.FileFormats.NotSet Then oFormat = oCsa.FileFormat

        Select Case oFormat
            Case DTOCsa.FileFormats.Norma58
                sTit = "GUARDAR REMESA DE ANTICIPS DE CREDIT (Norma 58)"
                sPrefixe = "AEB_58_"
            Case DTOCsa.FileFormats.Norma19
                sTit = "GUARDAR REMESA DE REBUTS AL COBRO (Norma 19)"
                sPrefixe = "AEB_19_"
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                sTit = "GUARDAR REMESA MASIVA DE EXPORTACIO (RME)"
                sPrefixe = "AEB_RME_"
            Case DTOCsa.FileFormats.NormaAndorrana
                sTit = "GUARDAR REMESA REBUTS ANDORRA"
                sPrefixe = "ABA_"
            Case DTOCsa.FileFormats.SepaB2b
                sTit = "GUARDAR REMESA SEPA B2B (norma 19.44)"
                sPrefixe = "AEB_SEPA_B2B_"
        End Select

        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = sTit
            .FileName = sPrefixe & oCsa.formatted
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            .FilterIndex = 1
            If .ShowDialog() = DialogResult.OK Then
                Select Case oFormat
                    Case DTOCsa.FileFormats.SepaB2b
                        oCsa.SaveFileSEPAB2B(.FileName)
                    Case Else
                        oCsa.SaveFile(.FileName)
                End Select
            End If
            Return .FileName
        End With
    End Function

    Public Sub ShowClientFras(ByRef oClient As Client)
        Dim oFrm As New Frm_Client_Fras
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowClientHistorial(ByRef oClient As Client)
        Dim oFrm As New Frm_Client_Historial
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowClientAlb(ByRef oAlb As Alb)
        Dim oFrm As New Frm_AlbNew2(oAlb)
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowClientAlbs(ByRef oClient As Client, Optional ByVal ToBeInvoicedOnly As Boolean = False)
        Dim oFrm As New Frm_Client_Albs
        With oFrm
            .ToBeInvoicedOnly = ToBeInvoicedOnly
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowClientGroupAlbs(ByRef oClient As Client, Optional ByVal OcultaFacturats As Boolean = False)
        Dim oFrm As New Frm_ClientGroup_Albs(oClient, OcultaFacturats)
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowClientCredit(ByVal oClient As Client)
        Dim oFrm As New Frm_Client_Risc(oClient)
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowClientPdcs(ByRef oClient As Client)
        Dim oFrm As New Frm_Pdcs
        With oFrm
            .Cod = DTOPurchaseOrder.Codis.client
            .Contact = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowClientPncs(ByRef oClient As Client)
        Dim oFrm As New Frm_Client_pncs
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowProveidorPncs(ByRef oProveidor As Proveidor)
        Dim oFrm As New Frm_Client_pncs
        With oFrm
            .Proveidor = oProveidor
            .Show()
        End With
    End Sub

    Public Sub ShowClientPncsTree(ByRef oClient As Client)
        Dim oFrm As New Frm_Client_Pncs_Tree
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub


    Public Sub ShowClientStatStps(ByRef oClient As Client)
        Dim oFrm As New Frm_Cli_Stat_StpMes
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowAdmBalSdos()
        Dim oFrm As New Frm_Ctas
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowAlb(ByRef oAlb As MaxiSrvr.Alb)
        Select Case oAlb.Cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.proveidor
                ShowClientAlb(oAlb)
                'Case DTOPurchaseOrder.Codis.proveidor
                'ShowProveidorAlb(oAlb)
            Case DTOPurchaseOrder.Codis.reparacio
                ShowClientAlb(oAlb)
                'ShowAlbSpv(oAlb)
            Case Else
                MsgBox("no implementat per " & oAlb.Cod.ToString, MsgBoxStyle.Exclamation)
        End Select
    End Sub


    Public Sub ShowArt(ByRef oArt As MaxiSrvr.Art)  ', Optional ByVal BlModal As Boolean = False) As Boolean
        Dim oFrm As New Frm_Art(oArt)
        oFrm.Show()
    End Sub

    Public Sub ShowArtPncs(ByVal oArt As Art, ByVal oCod As DTOPurchaseOrder.Codis)
        Dim oFrm As New Frm_Art_Pncs
        With oFrm
            .Cod = oCod
            .Art = oArt
            .Show()
        End With
    End Sub

    Public Sub ShowArtPrevisions(ByVal oArt As Art)
        Dim oFrm As New Frm_Art_Previsions
        With oFrm
            .Art = oArt
            .Show()
        End With
    End Sub

    Public Sub ShowForecast(ByVal oStp As Stp)
        Dim oFrm As New Frm_Forecast
        With oFrm
            .Stp = oStp
            .Show()
        End With
    End Sub

    Public Sub ShowForecast(ByVal oTpa As Tpa)
        Dim oFrm As New Frm_Forecast
        With oFrm
            .Tpa = oTpa
            .Show()
        End With
    End Sub

    Public Sub ShowArtArc(ByVal oArt As Art, Optional oMgz As DTOMgz = Nothing)

        If oArt.MovimentExists Then
            If oMgz Is Nothing Then oMgz = BLL.BLLApp.Mgz
            Dim oFrm As New Frm_Art_Arc(oArt, oMgz)
            oFrm.Show()
        Else
            MsgBox("no s'ha trobat historial d'aquest article", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Public Sub ShowArtxStpRanking(ByVal oStp As Stp)
        Dim oFrm As New Frm_ArtxStp_Ranking(oStp)
        oFrm.Show()
    End Sub

    Public Sub ShowArts()
        Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.Browse)
        oFrm.Show()
    End Sub

    Public Sub ShowStpxTpaRanking(ByVal oTpa As Tpa)
        Dim oFrm As New Frm_ArtxStp_Ranking(oTpa)
        oFrm.Show()
    End Sub

    Public Sub ShowArtxStpRepeticions(ByVal oStp As Stp)
        Dim oFrm As New Frm_ArtXStp_Repeticions(New Product(oStp))
        oFrm.Show()
    End Sub

    Public Sub ShowAtlasContacts()
        Dim oFrm As New Frm_Atlas
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowBancs()
        Dim oFrm As New Frm_Bancs_Old
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowBancVtos(ByVal oBanc As Banc)
        Dim oFrm As New Frm_Banc_Vtos
        With oFrm
            .Banc = oBanc
            .Show()
        End With
    End Sub

    Public Sub ShowBancAutoritzVtos(ByVal oBanc As Banc)
        Dim oFrm As New Frm_Banc_AutorizVtos
        With oFrm
            .Banc = oBanc
            .Show()
        End With
    End Sub

    Public Sub ShowCca(ByVal oCca As MaxiSrvr.Cca)
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
            Case Else
                'protetgir acces
                Dim oCcb As Ccb
                For Each oCcb In oCca.ccbs
                    If Not root.Usuari.AllowContactBrowse(oCcb.Contact) Then
                        BLL.MailHelper.MailAdmin(My.User.Name & " BROWSE ASSENTAMENT SENSIBLE " & oCca.yea & "/" & oCca.Id)
                        MsgBox("Operació no autoritzada", MsgBoxStyle.Exclamation, "MAT.NET")
                        Exit Sub
                    End If
                Next
        End Select

        Dim oFrm As New Frm_Cca(oCca)
        oFrm.Show()
    End Sub

    Public Sub ShowCceCcds(ByVal oCce As MaxiSrvr.Cce, ByVal DtFchFrom As Date, ByVal DtFchTo As Date)
        Dim oFrm As New Frm_CceCcds(oCce, DtFchFrom, DtFchTo)
        oFrm.Show()
    End Sub

    Public Sub ShowContact(ByVal oContact As MaxiSrvr.Contact)
        Dim oFrm As New Frm_Contact(oContact)
        oFrm.Show()
    End Sub

    Public Sub Old_ShowContactCtas(ByVal oContact As MaxiSrvr.Contact)
        If Not root.Usuari.AllowContactBrowse(oContact) Then
            BLL.MailHelper.MailErr(My.User.Name & " BROWSE ACCOUNTS " & oContact.Nom)
            MsgBox("Operació no autoritzada", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If

        Dim oFrm As New Frm_CliCtasOld(oContact)
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowContactMail(ByVal oMail As Mail)
        Dim oFrm As New Frm_Contact_Mail(oMail)
        oFrm.Show()
    End Sub

    Public Sub ShowContactMails(ByVal oContact As MaxiSrvr.Contact)
        Dim oFrm As New Frm_Contact_Correspondencia
        With oFrm
            .Contact = oContact
            .Show()
        End With
    End Sub

    Public Sub ShowContactNewMail(ByVal oContact As MaxiSrvr.Contact)
        Dim oMail As New Mail(BLL.BLLApp.Emp, Today)
        oMail.Contacts.Add(oContact)

        Dim oFrm As New Frm_Contact_Mail(oMail)
        oFrm.Show()
    End Sub

    Public Sub ShowContactMemo(ByVal omem As Mem)
        Dim oFrm As New Frm_Contact_Memo(omem)
        oFrm.Show()
    End Sub

    Public Sub ShowContactMemos(ByVal oContact As MaxiSrvr.Contact)
        Dim oFrm As New Frm_Contact_Correspondencia
        With oFrm
            .Contact = oContact
            .Show()
        End With
    End Sub

    Public Sub ShowContactNewMemo(ByVal oContact As MaxiSrvr.Contact)
        'Dim oMem As New Mem(BLL.BLLApp.Emp)
        'With oMem
        ' .Contact = oContact
        ' .Fch = Today
        ' End With



        'Dim oFrm As New Frm_Contact_Memo(oMem)
        'oFrm.Show()
    End Sub

    Public Sub ShowCsa(ByVal oCsa As MaxiSrvr.Csa)
        Dim oFrm As New Frm_Csa
        With oFrm
            .Csa = oCsa
            .Show()
        End With
    End Sub

    Public Sub ShowCsb(ByVal oCsb As MaxiSrvr.Csb)
        Dim oFrm As New Frm_Csb
        With oFrm
            .Csb = oCsb
            .Show()
        End With
    End Sub

    Public Sub ShowCsas(Optional ByVal oBanc As MaxiSrvr.Banc = Nothing)
        Dim oFrm As New Frm_Csas(oBanc)
        oFrm.Show()
    End Sub

    Public Sub ShowCta(ByVal oCta As MaxiSrvr.PgcCta)
        Dim oFrm As New Frm_PgcCta(oCta)
        oFrm.Show()
    End Sub

    Public Sub ShowDescuadreClients_Old()
        Dim oFrm As New Frm_Descuadre_Clients
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowDocs(ByVal oDocStyle As Frm_Doc_Select.Styles)
        Dim oFrm As New Frm_Doc_Select
        With oFrm
            .Style = oDocStyle
            .Show()
        End With
    End Sub

    Public Sub ShowFra(ByVal oFra As Fra)
        If oFra.Exists Then
            Dim oFrm As New Frm_Fra(oFra)
            With oFrm
                .Show()
            End With
        Else
            MsgBox("No existeix la fra." & oFra.Id & " a l'any " & oFra.Yea & "!", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub




    Public Sub ShowEpg(ByVal oEpg As Epg)
        Dim oFrm As New Frm_Epg
        With oFrm
            .Epg = oEpg
            .Show()
        End With
    End Sub

    Public Sub ShowEpgs(Optional ByVal IntYea As Integer = 0)
        'If IntYea = 0 Then IntYea = Today.Year
        'Dim oFrm As New Frm_Epgs
        'With oFrm
        ' .Yea = IntYea
        ' .Show()
        ' End With
    End Sub

    Public Sub ShowEshopsSort()
        Dim oFrm As New Frm_Eshop_Sort
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowImage(ByVal oImage As Image, Optional ByVal sCaption As String = "")
        Dim oFrm As New Frm_Img
        With oFrm
            .Image = oImage
            .Caption = sCaption
            .Show()
        End With
    End Sub

    Public Sub ShowFiscalIva()
        Dim oFrm As New Frm_Fiscal_IVA
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowFiscalLlibresOficials()
        Dim oFrm As New Frm_Fiscal_LlibresOficials
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastAlbs()
        Dim oFrm As New Frm_Last_Albs
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastCcas()
        Dim oFrm As New Frm_Last_Ccas
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastFras()
        Dim oFrm As New Frm_Last_Fras
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastMails()
        Dim oFrm As New Frm_Last_Mails
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastMems()
        Dim oFrm As New Frm_Last_Memos
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowLastPdcClients()
        Dim oFrm As New Frm_Last_Pdc_Clients
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowMailWord(ByVal oMail As Mail)
        Dim oFrm As New Frm_MailWord
        With oFrm
            .Mail = oMail
            .Show()
        End With
    End Sub


    Public Sub ShowAdmMrts()
        Dim oFrm As New Frm_Adm_Mrts
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowMrt(ByVal oMrt As Mrt)
        Dim oFrm As New Frm_Mrt
        With oFrm
            .Mrt = oMrt
            .Show()
        End With
    End Sub

    Public Sub ShowPurchaseOrder(ByVal oPurchaseOrder As DTOPurchaseOrder)
        BLL.BLLPurchaseOrder.Load(oPurchaseOrder, BLL.BLLApp.Mgz)
        Select Case oPurchaseOrder.Cod
            Case DTOPurchaseOrder.Codis.Client
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    oFrm.Show()
                End If
            Case Else
                MsgBox("nomes implementades les comandes de client", MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Public Sub ShowPdc(ByVal oPdc As Pdc)
        Select Case oPdc.Cod
            Case DTOPurchaseOrder.Codis.Client
                Dim oOrder As DTOPurchaseOrder = oPdc.PurchaseOrder()
                ShowPurchaseOrder(oOrder)
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
                Try
                    oFrm.Show()
                Catch ex As Exception
                End Try
            Case Else
                MsgBox("nomes implementades les comandes de client o proveidor", MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Public Sub ShowPGC()
        Dim oFrm As New Frm_PGC
        oFrm.Show()
    End Sub


    Public Sub ShowPqHdr(ByVal oPqHdr As PqHdr)
        Dim oFrm As New Frm_PqHdr
        With oFrm
            .PqHdr = oPqHdr
            .Show()
        End With
    End Sub

    Public Sub ShowPrevisions(Optional ByVal oProveidor As Proveidor = Nothing, Optional ByVal oPrevisio As Previsio = Nothing)
        Dim oFrm As New Frm_PrvPrevisions
        With oFrm
            If oPrevisio Is Nothing Then
                .Proveidor = oProveidor
            Else
                .Previsio = oPrevisio
            End If
            .Show()
        End With
    End Sub

    Public Sub ShowProveidorPdcs(Optional ByVal oProveidor As Proveidor = Nothing)
        Dim oFrm As New Frm_Pdcs
        With oFrm
            .Cod = DTOPurchaseOrder.Codis.proveidor
            .Contact = oProveidor
            .Show()
        End With
    End Sub

    Public Sub ShowProveidorAlbs(Optional ByVal oProveidor As Proveidor = Nothing)
        Dim oFrm As New Frm_Proveidor_Albs
        With oFrm
            .Proveidor = oProveidor
            .Show()
        End With
    End Sub


    Public Sub ShowRegio(ByRef oRegio As MaxiSrvr.Regio)
        Dim oFrm As New Frm_Regio
        With oFrm
            .Regio = oRegio
            .ShowDialog()
        End With
    End Sub

    Public Sub OldShowRepAddZonas(ByVal oRep As Rep)
        'Dim oFrm As New Frm_RepZona_Add
        'With oFrm
        '.rep = oRep
        '.Show()
        'End With
    End Sub

    Public Sub ShowReps()
        Dim oFrm As New Frm_Reps_Manager
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowStaffs()
        Dim oFrm As New Frm_Staffs
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowStatGeoMes(Optional ByVal oTpa As Tpa = Nothing, Optional ByVal oStp As Stp = Nothing, Optional ByVal oArt As Art = Nothing)
        Dim oFrm As New Frm_Stat_Geo_Mes
        With oFrm
            .Tpa = oTpa
            .Stp = oStp
            .Art = oArt
            .Show()
        End With
    End Sub

    Public Sub ShowStatArtsMes(Optional ByVal oTpaStpCat As Object = Nothing, Optional ByVal intyea As Integer = 0, Optional ByVal oClient As Client = Nothing)
        Dim oFrm As New Frm_Stat_Arts_Mes
        With oFrm
            .Client = oClient
            .Yea = intyea
            If oTpaStpCat IsNot Nothing Then
                If TypeOf (oTpaStpCat) Is Tpa Then
                    .Tpa = oTpaStpCat
                ElseIf TypeOf (oTpaStpCat) Is Stp Then
                    .Stp = oTpaStpCat

                End If
            End If
            .Show()
        End With
    End Sub

    Public Sub ShowStp(ByRef oStp As MaxiSrvr.Stp, Optional ByVal BlModal As Boolean = False)
        Dim oFrm As New Frm_Stp(oStp, BLL.Defaults.SelectionModes.Browse)
        oFrm.Show()
    End Sub

    Public Sub ShowTelSearch()
        Dim oFrm As New Frm_Tel_Search
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowTpa(ByRef oTpa As MaxiSrvr.Tpa)
        Dim oFrm As New Frm_Tpa(oTpa)
        oFrm.Show()
    End Sub

    Public Sub ShowTransmisions(ByVal IntYea As Integer)
        Dim oFrm As New Frm_Transmisions
        With oFrm
            .Yea = IntYea
            .Show()
        End With
    End Sub

    Public Sub NewTransmisio()
        Dim oFrm As New Frm_Transmisio_New(New Mgz(BLL.BLLApp.Mgz.Guid))
        oFrm.Show()
    End Sub

    Public Sub ShowTransmisio(ByVal oTransmisio As Transmisio)
        Dim oFrm As New Frm_Transmisio_New(oTransmisio)
        oFrm.Show()
    End Sub

    Public Sub ShowTransmConfig()
        Dim oTask As New MaxiSrvr.Task(MaxiSrvr.Task.Ids.VivaceTransmisio)
        Dim oFrm As New Frm_Task(oTask)
        oFrm.Show()
    End Sub

    Public Sub EmailStks(ByVal sMailAdr As String, Optional ByVal oLang As DTOLang = Nothing)
        If oLang Is Nothing Then oLang = BLL.BLLApp.Lang
        Dim sURL As String = BLL.BLLWebPage.Url(DTOWebPage.Ids.ProStocks) & "?lang=" & oLang.Id & "&email=true"
        Dim exs as new list(Of Exception)
        If BLL.MailHelper.SendMail(BLL.MailHelper.GetMailToAdrFromString(sMailAdr), BLL.MailHelper.GetMailCcAdrsFromString(sMailAdr), , "STOCKS M+O", sURL, BLL.FileSystemHelper.OutputFormat.URL, , exs) Then
            MsgBox("missatge enviat correctament", MsgBoxStyle.Information, "M+O")
        Else
            MsgBox("Missatje no enviat:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Public Sub PrintBancAutoritacioVtos(ByVal oBanc As Banc, ByVal oPncs As MaxiSrvr.Pnds)

    End Sub

    Public Sub ShowCdMake()
        'Dim oFrm As New Frm_CdMake
        'oFrm.Show()
    End Sub

    Public Sub ShowContactPnd(ByVal oPnd As Pnd)
        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        oFrm.Show()
    End Sub

    Public Sub ShowContactPnds(ByVal oContact As Contact)
        Dim oFrm As New Frm_CliCtasOld(oContact, True)
        oFrm.Show()
        'Dim oFrm As New Frm_Contact_Pnds
        'With oFrm
        '.Contact = oContact
        '.Show()
        'End With
    End Sub

    Public Sub NewMail(ByVal sTo As String)
        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            Try
                .Recipients.Add(sTo)
                .Display()
            Catch ex As Exception
            End Try
        End With
    End Sub

    Public Sub ImportFile()
        Dim oDlg As New Windows.Forms.OpenFileDialog
        With oDlg
            .Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            .FilterIndex = 1
            If .ShowDialog() = DialogResult.OK Then
                Dim oFrm As New Frm_ImportMailbox
                oFrm.FileName = .FileName
                oFrm.Show()
            End If
        End With
    End Sub

    Public Sub ImportMailBoxAttachments()
        Dim oFrm As New Frm_ImportMailbox
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ImportedFileManager(ByVal sFilename As String)
        Dim oFile As New System.IO.FileInfo(sFilename)
        Select Case UCase(oFile.Extension())
            Case ".XML"
                ImportedXMLFileManager(sFilename)
            Case ".RTF"
                'chequea nomina escura
                Dim oFrm As New Frm_NominasNew(sFilename, Today)
                oFrm.Show()
            Case Else
                MsgBox(oFile.Extension & vbCrLf & oFile.FullName)
        End Select
    End Sub



    Public Function ImportedXMLFileManager(ByVal sFilename As String) As Boolean
        Dim oDoc As New Xml.XmlDocument
        oDoc.Load(sFilename)
        Dim RetVal As Boolean = ImportedXMLFileManager(oDoc, sFilename)
        Return RetVal
    End Function

    Public Function ImportedXMLFileManager(ByVal oDoc As Xml.XmlDocument, Optional ByVal sFileName As String = "") As Boolean
        Dim RetVal As Boolean = False
        Dim sTipo As String = oDoc.DocumentElement.GetAttribute("TIPO")
        If sTipo = "" Then sTipo = oDoc.DocumentElement.GetAttribute("TYPE")
        'If sTipo = "" Then sTipo = oDoc.DocumentElement.GetAttribute("TYPE")
        Select Case sTipo
            Case "AVISOCAMION"
                Dim oFrm As New Frm_Importacio(oDoc)
                oFrm.Show()
            Case "TRANSMTRP"
                'RetVal = AlbConfirm.ImportTransmisio(oDoc)
                Dim exs as new list(Of Exception)
                If AlbTrp.ImportTransmisio(oDoc, exs) Then
                    Dim sNum As String = oDoc.DocumentElement.GetAttribute("NUMERO")
                    MsgBox("importades correctament les dades logístiques de la remesa " & sNum)
                Else
                    UIHelper.WarnError(exs, "error al importar el fitxer del magatzem")
                End If
                'Dim oAlbConfirm As New AlbConfirm(mEmp, oDoc)
                'Dim sErr As String = oAlbConfirm.ImportErrors
                'If sErr = "" Then
                'sErr = oAlbConfirm.Update(sErr)
                'End If
                'RetVal = (sErr = "")
                'Case "PEDIDOCLIENTE"
                'Case "SALIDASALMACEN"

            Case ""
                'DOCUMENT DE PROCEDENCIA EXTERNA
                Dim oNodeOsvalma As Xml.XmlNode = oDoc.DocumentElement.SelectSingleNode("/factura/emisor/nombre")
                If oNodeOsvalma IsNot Nothing Then
                    If oNodeOsvalma.InnerText.Contains("OSVALMA") Then
                        Dim oFrm As New Frm_Osvalma
                        With oFrm
                            .XmlDoc = oDoc
                            .Show()
                            Return False
                            Exit Function
                        End With
                    End If
                End If



                MsgBox("Importació XML incorrecte." & vbCrLf & "Document XML no segueix nomenclatura standard" & vbCrLf & sFileName, MsgBoxStyle.Exclamation)
            Case Else

                MsgBox("Importació XML incorrecte." & vbCrLf & "Tipus de document " & sTipo & " no registrat" & vbCrLf & sFileName, MsgBoxStyle.Exclamation)
        End Select
        'Catch ex As Exception
        'Log(Results.Err, "XML", sFileDisplayName & ": " & ex.Message)
        'maxisrvr.MailErr(ex.StackTrace)
        'Return Results.Err
        'End Try
        Return RetVal
    End Function

    Public Sub ShowCcaSearch(Optional ByVal IntYea As Integer = 0)
        Dim oFrm As New Frm_Cca_Search
        With oFrm
            .Yea = IntYea
            .Show()
        End With
    End Sub

    Public Sub ShowBalSumasYSaldos(ByVal DtFch As Date)
        Dim oFrm As New Frm_Ctas
        oFrm.Fch = DtFch
        oFrm.Show()
    End Sub

    Public Sub ShowBalances()
        'Dim oFrm As New Frm_Balance
        Dim oFrm As New Frm_Bal
        oFrm.Show()
    End Sub

    Public Sub ShowClientsQueNoCuadren()
        Dim oFrm As New Frm_AdmDescuadrePendents(Frm_AdmDescuadrePendents.Rols.Clients)
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowProveidorsQueNoCuadren()
        Dim oFrm As New Frm_AdmDescuadrePendents(Frm_AdmDescuadrePendents.Rols.Proveidors)
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub NewYearSdos()
        Dim oFrm As New Frm_Adm_NewYearSdos
        oFrm.Show()
    End Sub

    Public Sub ShowPqHdrs(Optional ByVal oMgz As Mgz = Nothing)
        If oMgz Is Nothing Then oMgz = New Mgz(BLL.BLLApp.Mgz.Guid)
        Dim oFrm As New Frm_PqHdrs
        oFrm.Mgz = oMgz
        oFrm.Show()
    End Sub

    Public Function RepFraEdit(ByVal orep As Rep, ByVal oFra As Fra) As Boolean
        Dim ofrm As New Frm_FraRep
        With ofrm
            .Rep = orep
            .Fra = oFra
            .ShowDialog()
            Return Not .Cancel
        End With
    End Function

    Public Sub ShowCyc()
        Dim oFrm As New Frm_Cyc
        oFrm.Show()
    End Sub

    Public Sub ShowIncentiu(ByVal oItm As Incentiu)
        Dim oFrm As New Frm_IncentiuOld
        With oFrm
            .Incentiu = oItm
            .Show()
        End With
    End Sub




    Public Function Bloqueijat(ByVal oContact As Contact) As MsgBoxResult
        Dim retval As MsgBoxResult
        Dim SQL As String = "SELECT USR,FCH FROM ALB_BLOQUEIG WHERE " _
        & "EMP=" & App.Current.Emp.Id & " AND " _
        & "CLI=" & oContact.Id
        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        If oDrd.Read Then
            retval = MsgBox("cal tancar l'albará ocupat per " & oDrd("USR") & " desde " & CDate(oDrd("FCH")).ToLongTimeString, MsgBoxStyle.AbortRetryIgnore, "MAT.NET")
        End If
        oDrd.Close()
        Return retval
    End Function

    Public Sub ShowSpv(ByVal oSpv As Spv)
        Dim oFrm As New Frm_Spv(oSpv)
        With oFrm
            '.Spv = oSpv
            .Show()
        End With
    End Sub

    Public Sub ShowSelArt()
        Dim oFrm As New Frm_Art_Select
        oFrm.Show()
    End Sub

    Public Sub ShowPncSortides(ByVal oPnc As LineItmPnc)
        Dim oFrm As New Frm_Pnc_Sortides
        With oFrm
            .Pnc = oPnc
            .Show()
        End With
    End Sub

    Public Sub ShowAlbTrps(ByVal oAlb As Alb)
        Dim oFrm As New Frm_AlbTrps
        With oFrm
            .Alb = oAlb
            .Show()
        End With
    End Sub


    Public Sub ShowProveidorsVenciments()
        Dim oFrm As New Frm_Proveidors_Venciments
        oFrm.Show()
    End Sub

    Public Sub ShowTrpTarifas(ByVal oTransportista As Transportista)
        Dim oFrm As New Frm_Trp_Tarifas
        With oFrm
            .Transportista = oTransportista
            .Show()
        End With
    End Sub

    Public Sub ShowTrpTarifa(ByVal oTrpZon As TrpZon)
        Dim oFrm As New Frm_Trp_Tarifa(oTrpZon)
        oFrm.Show()
    End Sub


    Public Sub ShowTrpFra(ByVal oTrpFra As TrpFra)
        'Dim oFrm As New Frm_TrpFra
        'With oFrm
        '.TrpFra = oTrpFra
        '.Show()
        'End With
    End Sub

    Public Sub ShowTrpFras(ByVal oTrp As Transportista)
        Dim oFrm As New Frm_TrpFras
        With oFrm
            .Trp = oTrp
            .Show()
        End With
    End Sub

    Public Sub ShowLastSpvs(Optional ByVal oClient As Client = Nothing)
        Dim oFrm As New Frm_Last_Spvs
        With oFrm
            .Client = oClient
            .Show()
        End With
    End Sub

    Public Sub ShowImportacions(Optional ByVal oProveidor As Proveidor = Nothing)
        Dim oFrm As New Frm_Importacions2(oProveidor)
        oFrm.Show()
    End Sub

    Public Sub ShowBancTransfer(ByVal oBancTransfer As BancTransfer)
        Dim oFrm As New Frm_Rep_Transfers
        With oFrm
            .BancTransfer = oBancTransfer
            .Show()
        End With
    End Sub

    Public Sub ExeCsaDespeses(ByVal oCsa As Csa)
        Dim oFrm As New Frm_Csa_Despeses
        With oFrm
            .Csa = oCsa
            .Show()
        End With
    End Sub

    Public Sub TabControlHideTabLabels(ByVal oXl As TabControl)
        Dim oParent As Control = oXl.Parent
        Dim oPBox As New PictureBox
        oParent.Controls.Add(oPBox)
        oXl.Appearance = TabAppearance.Buttons
        With oPBox
            .Height = 21
            .Width = oXl.Width
            .Top = oXl.Top
            .Left = oXl.Left
            .BringToFront()
        End With
    End Sub

    Public Function ContextMenu_Art(ByVal oArt As Art) As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        oMenu.Items.AddRange(New Menu_Art(oArt).Range)
        Return oMenu
    End Function

    Public Function ContextMenu_Contact(ByVal oContact As Contact) As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        oMenu.Items.AddRange(New Menu_Contact(oContact).Range)
        Return oMenu
    End Function

    Public Function ContextMenu_Csas(ByVal oCsas As Csas) As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        oMenu.Items.AddRange(New Menu_Csa(oCsas).Range)
        Return oMenu
    End Function

    Public Function ContextMenu_Csb(ByVal oCsb As Csb) As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        oMenu.Items.AddRange(New Menu_Csb(oCsb).Range)
        Return oMenu
    End Function

    Public Function ContextMenu_Mail(ByVal oMail As Mail) As ContextMenuStrip
        Dim oMenu As New ContextMenuStrip
        oMenu.Items.AddRange(New Menu_Mail(oMail).Range)
        Return oMenu
    End Function

    Public Sub showartsearch()
        'Dim oFrm As New Frm_Art_Search
        'oFrm.Show()
    End Sub

    Private Sub MailHome()
        For i As Integer = 1003 To 1042
            Dim oMsg As New Msg(BLL.BLLApp.Emp, 2007, i)
            Dim StFrom As String = oMsg.Email ' "info@matiasmasso.es" ' oMsg.adr
            Dim StBody As String
            Dim StSubject As String = "MISSATGE NUM. " & oMsg.Id & " DE CONSUMIDOR"
            StBody = "Remite:" & vbCrLf & oMsg.nom & vbCrLf & oMsg.adr & vbCrLf & oMsg.zip & " " & oMsg.cit
            If oMsg.Country.ISO <> "ES" Then
                StSubject = StSubject & " (" & oMsg.Country.Nom_Cat & ")"
                StBody = StBody & " (" & oMsg.Country.Nom_Cat & ")"
            End If
            If oMsg.tel > "" Then StBody = StBody & vbCrLf & "tels.: " & oMsg.tel
            If oMsg.Email > "" Then StBody = StBody & vbCrLf & "email.: " & oMsg.Email
            StBody = StBody & vbCrLf & vbCrLf & oMsg.Question
            BLL.MailHelper.SendMail("INFO@MATIASMASSO.ES", , , StSubject, StBody)
        Next i

    End Sub


    Public Sub Test()
        Dim oFrm As New Frm__Idx
        oFrm.Show()


        'App.Current.emp.VtosUpdate(exs)

        'Dim oFrm As New Frm_Deliveries()
        'oFrm.Show()

        'Dim exs As New List(Of Exception)
        'Dim iCount, iCountDone As Integer
        'Dim oEdiFiles As EdiFiles = EdiFiles.All(EdiFile.IOcods.Inbox, EdiFile.Tags.ORDERS_D_96A_UN_EAN008.ToString, EdiFiles.Filters.ShowPending)
        'oEdiFiles.Procesa(iCount, iCountDone, exs)

        'Dim qscoll As System.Collections.Specialized.NameValueCollection = System.Web.HttpUtility.ParseQueryString("Ds_Date=04%2f08%2f2015&Ds_Hour=14%3a50&Ds_SecurePayment=1&Ds_Amount=20262&Ds_Currency=978&Ds_Order=284418434752&Ds_MerchantCode=22425573&Ds_Terminal=001&Ds_Signature=1542BF2D06C932A4EE15BBF963F29458E0CFFE8A&Ds_Response=0000&Ds_TransactionType=0&Ds_MerchantData=2%3be2974bff-3f49-4ae4-a613-045a960a2a25%3balb.12712+de+RAMIRO+MAS%2c+M%u00aa+Concepci%u00f3n+%22ORIGINALBABY.ES%22+(TORREJON+DE+VELASCO)&Ds_AuthorisationCode=362553&Ds_ConsumerLanguage=1&Ds_Card_Country=724 ")
        'Dim exs As New List(Of Exception)
        'MaxiSrvr.BLL_Tpv.Procesa(qscoll, exs)



        'Dim oGuid As New Guid("938AA55C-2AD5-451D-A14A-6F5AF1D1A888")
        'Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(oGuid)
        'BLL.BLLContact.Load(oCustomer)
        'Dim oFrm As New Frm_Customer_PropertyGrid(oCustomer)
        'oFrm.Show()

        'Dim oFrm As New Frm_Girs2
        'oFrm.Show()
    End Sub



    Private Function GetRegistreWithFields(ByVal oArray As ArrayList) As String
        Dim i As Integer
        Dim iLastIdx As Integer = oArray.Count - 1
        Dim sb As New System.Text.StringBuilder
        For i = 0 To iLastIdx
            sb.Append(oArray(i))
            If i < iLastIdx Then sb.Append(";")
        Next
        Return sb.ToString
    End Function




    Public Sub Test_DoWork()
    End Sub

    Public Sub Test_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        MsgBox("test end", MsgBoxStyle.Information, "root.test")

    End Sub

    '***************************************************


    Public Function Email_Efra(ByVal oFra As Fra, ByVal DtFchPrinted As Date) As Boolean

        Dim oEmail As Email
        Dim exs as new list(Of Exception)
        Dim BlSuccess As Boolean
        Dim oEmails As Emails = oFra.Client.eFrasMailboxRecipients

        For Each oEmail In oEmails
            Dim oLang As DTOLang = oEmail.Lang
            Dim sTo As String = oEmail.Adr
            Dim sSubject As String = oLang.Tradueix("Factura electrónica", "Factura electrónica", "e-invoice") & " " & oFra.Id.ToString
            Dim sUrl As String = GetRootUrlOld(True) & "eMail/EmailFactura.aspx?guid=" & oFra.Guid.ToString & "&lang=" & CInt(oLang.Id).ToString
            BlSuccess = BLL.MailHelper.SendMail(BLL.MailHelper.GetMailToAdrFromString(sTo), BLL.MailHelper.GetMailCcAdrsFromString(sTo), , sSubject, sUrl, BLL.FileSystemHelper.OutputFormat.URL, , exs)

            If BlSuccess Then
                Dim SQL As String = "UPDATE FRA SET " _
                & "FCHLASTPRINTED='" & Format(DtFchPrinted, "yyyyMMdd HH:mm") & "', " _
                & "USRLASTPRINTED=" & root.Usuari.Id & ", " _
                & "EMAILEDTOGuid='" & oEmail.Guid.ToString & "' WHERE " _
                & "EMP=" & CInt(App.Current.Emp.Id).ToString & " AND " _
                & "YEA=" & oFra.Yea.ToString & " AND " _
                & "FRA=" & oFra.Id.ToString
                maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)
                BlSuccess = True
            End If
        Next

        Dim retval As Boolean = BlSuccess
        If Not BlSuccess Then
            MsgBox("error al enviar factura " & oFra.Id & " a " & oFra.Client.Clx & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
        End If

        Return retval
    End Function

    Public Sub ShowConfig()
        Dim oFrm As New Frm_Config
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowPortsCondicions(ByVal oPortsCond As PortsCondicions)
        Dim oFrm As New Frm_PortsCondicions
        With oFrm
            .PortsCondicions = oPortsCond
            .Show()
        End With
    End Sub

    Public Sub ShowUsrEvts(Optional ByVal oUsr As Contact = Nothing)
        Dim oFrm As New Frm_UsrEvents
        With oFrm
            .Usr = oUsr
            .Show()
        End With
    End Sub

    Public Sub ShowRebut(Optional ByVal oRebut As Rebut = Nothing)
        If oRebut Is Nothing Then
            oRebut = New Rebut()
            With oRebut
                .Lang = BLL.BLLApp.Lang
                .Amt = New maxisrvr.Amt
                .Fch = Today
                .Vto = Today
            End With
        End If
        Dim oFrm As New Frm_Rebut
        With oFrm
            .Rebut = oRebut
            .Show()
        End With
    End Sub

    Public Sub ShowReembolsos(ByVal oTrp As Transportista)
        Dim oFrm As New Frm_Cobrament_Reembolsos
        oFrm.Transportista = oTrp
        oFrm.Show()
    End Sub


    Public Function ContextMenu_Alb(ByVal oAlb As Alb) As ContextMenuStrip
        Dim oContextMenu As New ContextMenuStrip
        With oContextMenu.Items
            .AddRange(New Menu_Alb(oAlb).Range)
        End With
        Return oContextMenu
    End Function

    Public Function ContextMenu_Banc(ByVal oBanc As DTOBanc) As ContextMenuStrip
        Dim pBanc As New Banc(oBanc.Guid)
        Dim oContextMenu As New ContextMenuStrip
        With oContextMenu.Items
            '.Add(MatCommunicator.MenuItem_Truca(oBanc))
            .AddRange(New Menu_Banc(pBanc).Range)
        End With
        Return oContextMenu
    End Function

    Public Function ContextMenu_Banc(ByVal oBanc As Banc) As ContextMenuStrip
        Dim oContextMenu As New ContextMenuStrip
        With oContextMenu.Items
            '.Add(MatCommunicator.MenuItem_Truca(oBanc))
            .AddRange(New Menu_Banc(oBanc).Range)
        End With
        Return oContextMenu
    End Function

    Public Function ContextMenu_Pnd(ByVal oPnd As Pnd) As ContextMenuStrip
        Dim oContextMenu As New ContextMenuStrip
        With oContextMenu.Items
            .AddRange(New Menu_Pnd(oPnd).Range)
        End With
        Return oContextMenu
    End Function

    Public Sub ShowPnds()
        Dim oFrm As New Frm_Pnds
        oFrm.Show()
    End Sub

    Public Sub ShowCcaDescuadres(ByVal iYea As Integer)
        Dim oFrm As New Frm_Cca_Descuadres
        With oFrm
            .Yea = iYea
            .Show()
        End With
    End Sub

    Public Sub ShowBancIngresXecs(ByVal oBanc As Banc)
        MsgBox("sustituit per formulari de presentació de xecs i pagarés")
        'Dim oFrm As New Frm_Banc_IngresXecs
        'With oFrm
        ' .Cod = XecIngres.Cods.Xecs
        ' .Banc = oBanc
        ' .Show()
        ' End With
    End Sub

    Public Sub ShowBancIngresPagares(ByVal oBanc As Banc)
        MsgBox("sustituit per formulari de presentació de xecs i pagarés")
        'Dim oFrm As New Frm_Banc_IngresXecs
        'With oFrm
        ' .Cod = XecIngres.Cods.Pagares
        ' .Banc = oBanc
        ' .Show()
        ' End With
    End Sub


    Public Sub ShowBigFile(ByVal oBigFile As MaxiSrvr.BigFileNew, Optional ByVal sSuggestedFileName As String = "")
        If oBigFile.IsEmpty Then
            MsgBox("document no disponible", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            If oBigFile.Exists Then
                If sSuggestedFileName = "" Then sSuggestedFileName = oBigFile.DefaultFileName()
                Dim BlDeveloper As Boolean = GetSetting("MATSOFT", "MAT.NET", "Developer") = "1"
                If BlDeveloper Then
                    MsgBox("modo developer:" & vbCrLf & "els pdfs es salven a disc i els obre el browser en lloc de fer servir la pagina web de pdf streaming", MsgBoxStyle.Information)
                    Select Case oBigFile.MimeCod
                        Case DTOEnums.MimeCods.Pdf, DTOEnums.MimeCods.Gif, DTOEnums.MimeCods.Jpg
                            Dim exs As New List(Of exception)
                            If BLL.FileSystemHelper.SaveStream(oBigFile.Stream, exs, sSuggestedFileName) Then
                                Process.Start("IExplore.exe", sSuggestedFileName)
                            Else
                                UIHelper.WarnError(exs, "error al redactar el fitxer")
                            End If
                        Case Else
                            MsgBox("visor no implementat per aquest format" & vbCrLf & oBigFile.Features, MsgBoxStyle.Exclamation, "MAT.NET")
                    End Select
                Else
                    Dim sUrl As String = WebPage.GetDocUrl(oBigFile, True)
                    If sSuggestedFileName > "" Then
                        BLL.WebPageHelper.addParam(sUrl, "filename", sSuggestedFileName)
                        'sUrl = sUrl & "&filename=" & sSuggestedFileName
                    End If
                    Process.Start("IExplore.exe", sUrl)
                End If
            Else
                Select Case oBigFile.MimeCod
                    Case DTOEnums.MimeCods.Pdf
                        Dim exs As New List(Of exception)
                        If BLL.FileSystemHelper.SaveStream(oBigFile.Stream, exs, sSuggestedFileName) Then
                            Process.Start("IExplore.exe", sSuggestedFileName)
                        Else
                            UIHelper.WarnError(exs, "error al desar el fitxer")
                        End If
                    Case Else
                        MsgBox("visor no implementat per aquest format" & vbCrLf & oBigFile.Features, MsgBoxStyle.Exclamation, "MAT.NET")
                End Select
            End If

        End If
    End Sub

    Public Sub ShowFileDocument(ByVal oFileDocument As FileDocument, Optional ByVal sSuggestedFileName As String = "")
        If oFileDocument Is Nothing Then
            MsgBox("document no disponible", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            If oFileDocument.Exists Then
                If sSuggestedFileName = "" Then sSuggestedFileName = oFileDocument.DefaultFileName()
                Dim BlDeveloper As Boolean = GetSetting("MATSOFT", "MAT.NET", "Developer") = "1"
                If BlDeveloper Then
                    MsgBox("modo developer:" & vbCrLf & "els pdfs es salven a disc i els obre el browser en lloc de fer servir la pagina web de pdf streaming", MsgBoxStyle.Information)
                    Select Case oFileDocument.MediaObject.Mime
                        Case DTOEnums.MimeCods.Pdf, DTOEnums.MimeCods.Gif, DTOEnums.MimeCods.Jpg
                            Dim exs As New List(Of exception)
                            If BLL.FileSystemHelper.SaveStream(oFileDocument.MediaObject.Stream, exs, sSuggestedFileName) Then
                                Process.Start("IExplore.exe", sSuggestedFileName)
                            Else
                                UIHelper.WarnError(exs, "error al desar el fitxer")
                            End If
                        Case Else
                            MsgBox("visor no implementat per aquest format" & vbCrLf & oFileDocument.MediaObject.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
                    End Select
                Else
                    Dim sUrl As String = WebPage.GetDocUrl(oFileDocument.Guid, True)
                    If sSuggestedFileName > "" Then
                        BLL.WebPageHelper.addParam(sUrl, "filename", sSuggestedFileName)
                        'sUrl = sUrl & "&filename=" & sSuggestedFileName
                    End If
                    Process.Start("IExplore.exe", sUrl)
                End If
            Else
                Select Case oFileDocument.MediaObject.Mime
                    Case DTOEnums.MimeCods.Pdf
                        Dim exs As New List(Of exception)
                        If BLL.FileSystemHelper.SaveStream(oFileDocument.MediaObject.Stream, exs, sSuggestedFileName) Then
                            Process.Start("IExplore.exe", sSuggestedFileName)
                        Else
                            UIHelper.WarnError(exs, "error al desar el fitxer")
                        End If
                    Case Else
                        MsgBox("visor no implementat per aquest format" & vbCrLf & oFileDocument.MediaObject.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
                End Select
            End If

        End If
    End Sub

    Public Sub ShowStream(ByVal oMediaObject As MediaObject, Optional ByVal sSuggestedFileName As String = "")
        If oMediaObject Is Nothing Then
            MsgBox("document no disponible", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            If sSuggestedFileName = "" Then sSuggestedFileName = oMediaObject.FileName
            'Dim BlDeveloper As Boolean = GetSetting("MATSOFT", "MAT.NET", "Developer") = "1"
            'If BlDeveloper Then
            'MsgBox("modo developer:" & vbCrLf & "els pdfs es salven a disc i els obre el browser en lloc de fer servir la pagina web de pdf streaming", MsgBoxStyle.Information)
            Select Case oMediaObject.Mime
                Case DTOEnums.MimeCods.Pdf, DTOEnums.MimeCods.Gif, DTOEnums.MimeCods.Jpg
                    Dim exs As New List(Of exception)
                    If BLL.FileSystemHelper.SaveStream(oMediaObject.Stream, exs, sSuggestedFileName) Then
                        Process.Start("IExplore.exe", sSuggestedFileName)
                    Else
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                Case Else
                    MsgBox("visor no implementat per aquest format" & vbCrLf & oMediaObject.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
            End Select
            'End If
        End If
    End Sub

    Public Sub ShowCsvFromDataset(ByVal oDs As DataSet, Optional ByVal sFileName As String = "", Optional ByVal sTitleRow As String = "")
        Dim sTmpFolder As String = maxisrvr.TmpFolder
        Dim oTmpFolder As System.IO.DirectoryInfo = BLL.FileSystemHelper.CleanOrCreateFolder(sTmpFolder)

        If sFileName > "" Then
            If Right(sFileName, 4) <> ".csv" Then sFileName = sFileName & ".csv"

            'carregat el fitxer si ja existeix
            'si no t'el pots carregar perque está ocupat, dona un nom aleatori
            Try
                Dim oFile As New IO.FileInfo(sTmpFolder & sFileName)
                If oFile.Exists() Then oFile.Delete()
            Catch ex As Exception
                sFileName = System.Guid.NewGuid.ToString & ".csv"
            End Try
        Else
            sFileName = System.Guid.NewGuid.ToString & ".csv"
        End If

        Dim sFullPath As String = oTmpFolder.FullName & sFileName

        Dim sr As New IO.StreamWriter(sFullPath, False, System.Text.Encoding.Default)
        If sTitleRow > "" Then
            sr.WriteLine(sTitleRow)
        End If

        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            Dim BlFirstCell As Boolean = True
            For Each oValue As Object In oRow.ItemArray
                If BlFirstCell Then
                    BlFirstCell = False
                Else
                    sr.Write(";")
                End If
                Dim s As String = ""
                If Not IsDBNull(oValue) Then
                    s = oValue.ToString
                End If
                sr.Write(s)
            Next
            sr.WriteLine()
        Next

        sr.Close()

        Process.Start("IExplore.exe", sFullPath)

    End Sub

    Private Sub CleanOrCreateFolder(ByVal oFolder As IO.DirectoryInfo)
        If oFolder.Exists Then
            Dim oFile As System.IO.FileInfo

            'buida la carpeta de fitxers caducats
            Try
                For Each oFile In oFolder.GetFiles()
                    If DateDiff(DateInterval.Day, oFile.CreationTime, Now) > 1 Then
                        oFile.Delete()
                    End If
                Next
            Catch ex As Exception
            End Try
        Else
            oFolder.Create()
        End If
    End Sub

    Public Sub ShowEtqs()
        Dim oFrm As New Frm_Etqs
        oFrm.Show()
    End Sub

    Public Sub ShowImpagats()
        'Dim oFrm As New Frm_Impagats
        Dim oFrm As New Frm_Impagats2
        oFrm.Show()
    End Sub

    Public Sub ShowImpagatsCYC()
        'Dim oFrm As New Frm_Impagats
        Dim oFrm As New Frm_Impagats_Old
        oFrm.Show()
    End Sub

    Public Sub ShowTrpFrasVivace()
        Dim oFrm As New Frm_Trp_FrasVivace
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowCcbBlock(ByVal oCcd As Ccd)
        Dim oFrm As New Frm_CcbBlock
        With oFrm
            .CcbBlock = New CcbBlock(oCcd)
            .Show()
        End With
    End Sub

    Public Sub ShowIngresosYDespesesMensual(Optional ByVal iYea As Integer = 0, Optional ByVal iMes As Integer = 0)
        If iYea = 0 Or iMes = 0 Then
            Dim DtFch As Date = Today.AddMonths(-1)
            iYea = DtFch.Year
            iMes = DtFch.Month
        End If

        'Dim oFrm As New Frm_Adm_IngresosYDespesesMensual
        'With oFrm
        ' .Yea = iYea
        ' .Mes = iMes
        ' .Show()
        ' End With
    End Sub


    Public Sub ShowPrvLastEntrys()
        Dim oFrm As New Frm_Last_Albs
        With oFrm
            .Cod = DTOPurchaseOrder.Codis.proveidor
            .Show()
        End With
    End Sub

    Public Sub ShowFiscalIrpf()
        Dim oFrm As New Frm_Fiscal_IRPF
        With oFrm
            .Show()
        End With
    End Sub


    Public Function FileSearchSignCertPath() As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "BUSCAR CERTIFICAT PER SIGNATURA ELECTRONICA"
            .Filter = "certificats (.pfx)|*.pfx|tots els arxius|*.*"
            If .ShowDialog() = DialogResult.OK Then
                App.Current.Emp.SetDefault("SignCertPath", .FileName)
                retval = True
            End If
        End With
        Return retval
    End Function

    Public Sub ShowCliTel(ByVal oTel As Tel)
        Dim oFrm As New Frm_CliTel
        With oFrm
            .Tel = oTel
            .Show()
        End With
    End Sub



    Public Sub ShowRepRetencions(ByVal oRep As Rep)
        Dim oFrm As New Frm_RepRetencions
        oFrm.Rep = oRep
        oFrm.Show()
    End Sub

    Public Sub ExeVtosUpdate()
        Dim oFrm As New Frm_Tarea
        With oFrm
            .Caption = "Actualitzan venciments..."
            .Show()
            Dim exs as new list(Of Exception)
            App.Current.Emp.VtosUpdate(exs)
            .Fin("Venciments actualitzats.")
        End With
    End Sub

    Public Sub PncRecalc()
        Dim oFrm As New Frm_Tarea
        With oFrm
            .Caption = "Actualitzan pendents (quantitat demanada - sortits = pendents) ..."
            .Show()
            Application.DoEvents()
            Dim exs As List(Of exception) = App.Current.emp.PncRecalc()
            If exs.Count = 0 Then
                .Fin("pendents actualitzats.")
            Else
                .Fin(BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
    End Sub


    Public Sub ShowCurs()
        Dim oFrm As New Frm_Curs
        oFrm.Show()
    End Sub

    Public Sub ShowNomines()
        Dim oFrm As New Frm_Nominas
        oFrm.Show()
    End Sub

    Public Sub TransferNominas()
        Dim oFrm As New Frm_Rep_Transfers
        With oFrm
            .NewTransferCod = Frm_Rep_Transfers.NewTransferCods.Staff
            .Show()
        End With
    End Sub



    Public Sub ShowEmail(ByVal oEmail As Email)
        Dim oFrm As New Frm_Contact_Email(oEmail)
        oFrm.Show()
    End Sub

    Public Sub ShowRevistas()
        'Dim oFrm As New Frm_PrRevistas
        Dim oFrm As New Frm_PrEditorials
        With oFrm
            .Show()
        End With
    End Sub

    Public Sub ShowECIconfig()
        Dim oFrm As New Frm_ECI
        oFrm.Show()
    End Sub

    Public Sub NewPdcPrv(ByVal oPrv As Proveidor)
        Dim oPdc As Pdc = oPrv.NewPdc(Today, "")
        Dim oFrm As New Frm_Prv_Pdc2
        With oFrm
            .Pdc = oPdc
            .Show()
        End With
    End Sub

    Public Sub ShowVivaceStk()
        Dim oFrm As New Frm_Vivace_Stk
        oFrm.Show()
    End Sub

    Public Sub ShowDiariResumMensual(ByVal iYea As Integer, ByVal iMes As Integer)
        Dim oFrm As New Frm_Diari_ResumMensual
        With oFrm
            .Yea = iYea
            .Mes = iMes
            .Show()
        End With
    End Sub

    Public Sub SowAeatMods()
        Dim oFrm As New Frm_Aeat_Mods
        oFrm.Show()
    End Sub

    Public Sub ShowMovies()
        root.ShowMovie()
    End Sub

    Public Sub ShowFileImport(ByVal oBigFile As MaxiSrvr.BigFileNew, Optional ByVal oContact As Contact = Nothing)
        Dim oFrm As New Frm_FileImport
        'oFrm.MediaObject = oBigFile
        oFrm.Contact = oContact
        oFrm.Show()
    End Sub

    Public Sub ShowFileImport(ByVal oDocFile As DTODocFile, Optional ByVal oContact As Contact = Nothing)
        Dim oFrm As New Frm_FileImport
        oFrm.DocFile = oDocfile
        oFrm.Contact = oContact
        oFrm.Show()
    End Sub

    Public Sub ClearAlbBloqueig()
        Dim oFrm As New Frm_AlbBloqueigs
        oFrm.Show()
    End Sub

    Public Function RepRetencionsSave(ByVal iYea As Integer, ByVal iQuarter As Integer, Optional ByVal oRep As Rep = Nothing) As Integer

        Dim iStartMes As Integer = iQuarter * 3 - 2
        Dim DtFirstDayFirstMonth As New Date(iYea, iStartMes, 1)
        Dim DtLastDayLastMonth As Date = DtFirstDayFirstMonth.AddMonths(3).AddDays(-1)
        Dim sFchStart As String = Format(DtFirstDayFirstMonth, "yyyyMMdd")
        Dim sFchEnd As String = Format(DtLastDayLastMonth, "yyyyMMdd")

        Dim oReps As New Reps
        If oRep Is Nothing Then
            oReps = RepsLoader.WithRetencions(BLL.BLLApp.Emp, DtFirstDayFirstMonth, DtLastDayLastMonth)
        Else
            oReps.Add(oRep)
        End If

        Dim oPdfRepRetencio As PdfRepRetencions = Nothing
        Dim oStream As Byte() = Nothing
        For Each oRep In oReps
            oPdfRepRetencio = New PdfRepRetencions(oRep, iYea, iQuarter)
            oStream = oPdfRepRetencio.Stream
            Dim exs As New List(Of Exception)

            Dim oDocFile As DTODocFile = Nothing
            If BLL_DocFile.LoadFromStream(oStream, oDocFile, exs) Then
                oDocFile = oDocFile
            End If

            Dim oDoc As New CliDoc(oRep)
            With oDoc
                .Ref = iYea.ToString & "." & iQuarter.ToString
                .Fch = DtLastDayLastMonth
                .Type = CliDoc.Types.Retencions
                .DocFile = oDocfile
            End With
            If CliDocLoader.Update(oDoc, exs) Then
                'emailRepRetencions(oDoc)
            End If
        Next
        Return oReps.Count
    End Function

    Public Sub emailRepRetencions(ByVal oDoc As CliDoc)
        Dim sTo As String = oDoc.Contact.Email() & ";matias@matiasmasso.es"
        Dim sSubject As String = "CERTIFICADO TRIMESTRAL DE RETENCIONES"
        Dim sBody As String = "Esto es un mensaje automatico para avisar que ha sido actualizada su página de retenciones de IRPF con un nuevo certificado disponible en el siguiente enlace:" & vbCrLf _
        & "http://www.matiasmasso.es/retenciones"
        BLL.MailHelper.SendMail(BLL.MailHelper.GetMailToAdrFromString(sTo), BLL.MailHelper.GetMailCcAdrsFromString(sTo), , sSubject, sBody)
    End Sub

    Public Sub EmailAvisame()
        Dim exs As New list(Of Exception)
        App.Current.Emp.EmailAvisame(exs)
        MsgBox(BLL.Defaults.ExsToMultiline(exs))
    End Sub

    Public Sub ShowEanEci()
        Dim oFrm As New Frm_Ean_ECI
        oFrm.Show()
    End Sub



    Public Sub ShowInfoJobs()
        Dim oFrm As New Frm_InfoJobs
        oFrm.Show()
    End Sub

    Public Function GetNominasFromEscura(ByVal sFileName As String)
        Dim oEmp As DTO.DTOEmp = BLL.BLLApp.Emp
        Dim oWord As New Word.Application
        Dim oDocument As Word.Document = oWord.Documents.Open(sFileName)
        Dim oDocCopy As Word.Document
        Dim oWhich As Word.WdGoToDirection = Word.WdGoToDirection.wdGoToFirst
        'Dim oPageRange As Word.Range = Nothing
        'Dim oShapeRange As Word.ShapeRange = Nothing
        Dim sPdfFileName As String = ""
        Dim oNomina As Nomina = Nothing
        Dim oNominas As New Nominas
        Dim sLogoFileName As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\logo.gif"
        'My.Resources.Logo_M_O_2cm_300x300.Save(sLogoFileName)
        My.Resources.Logo_M_O_200x200.Save(sLogoFileName)


        'oWord.Visible = True

        For iPageCount As Integer = 1 To oDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument)
            'oPageRange = oWord.Selection.GoTo(What:=Word.WdGoToItem.wdGoToPage, Which:=oWhich)
            'oPageRange.End = oWord.Selection.Bookmarks("\Page").Range.End
            'oPageRange.Copy()
            oWord.Selection.GoTo(What:=Word.WdGoToItem.wdGoToPage, Which:=oWhich)
            oWord.Selection.Extend()
            oWord.Selection.GoTo(What:=Word.WdGoToItem.wdGoToBookmark, Name:="\Page")
            oWord.Selection.Copy()

            sPdfFileName = "C:\tmp\result" & iPageCount & ".pdf"
            'oShapeRange.Select()
            'oWord.Selection.Copy()
            oDocCopy = oWord.Documents.Add
            oDocCopy.Range.Paste()

            'oDocCopy.Shapes(1).Select()
            'oWord.Selection.InlineShapes.AddPicture(FileName:=sLogoFileName)
            oDocCopy.InlineShapes.AddPicture(FileName:=sLogoFileName, LinkToFile:=False, SaveWithDocument:=True)

            oDocCopy.SaveAs(sPdfFileName, Word.WdSaveFormat.wdFormatPDF)
            oDocCopy.Close(Word.WdSaveOptions.wdDoNotSaveChanges)

            'Dim oBigFile As New maxisrvr.BigFileNew()
            'With oBigFile
            ' .MimeCod = DTOEnums.MimeCods.Pdf
            ' End With
            'root.LoadBigFilePdfFile(oBigFile, sPdfFileName)

            With oWord.Selection
                With .Find
                    .Text = "[0-9][0-9]/????????-[0-9][0-9]"
                    .Replacement.Text = ""
                    .Forward = True
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = True
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                    .Execute()
                End With
            End With


            Dim sSegSocialNum As String = oWord.Selection.Range.Text ' GetData(oShapeRange(70), 1, 18).Trim
            'oNomina = New Nomina(oEmp, sSegSocialNum)
            'oNomina.LoadPdf(sPdfFileName)
            oNominas.Add(oNomina)

            oWhich = Word.WdGoToDirection.wdGoToNext
        Next
        Return oNominas
    End Function

    Public Sub ShowWebLog()
        Dim oFrm As New Frm_WebLog
        oFrm.Show()
    End Sub

    Public Sub ShowAuditoria()
        Dim oFrm As New Frm_Auditoria
        oFrm.Show()
    End Sub

    Private Sub RegeneraFactures(ByVal iYea As Integer)
        Dim SQL As String = "SELECT Guid FROM FRA " _
        & "INNER JOIN CCA ON FRA.CCAGUID=CCA.GUID WHERE fra.EMP=1 AND fra.YEA=" & iYea & " AND CCA.FileDocument IS NULL ORDER BY fra.FRA"
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Dim oFra As Fra = Nothing
        Do While oDrd.Read
            Try
                Dim oGuid As Guid = oDrd("Guid")
                oFra = New Fra(oGuid)
                Dim oCca As Cca = oFra.Cca
                Dim oPdf As New PdfAlb(oFra)
                Dim exs As New List(Of Exception)

                Dim oDocFile As DTODocFile = Nothing
                If BLL_DocFile.LoadFromStream(oPdf.Stream(True), oDocFile, exs) Then
                    oCca.DocFile = oDocFile
                    oCca.DocFile.PendingOp = DTODocFile.PendingOps.Update
                    If Not oCca.Update(exs) Then
                        MsgBox("error al desar el document:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                Else
                    UIHelper.WarnError(exs, "error al redactar/firmar la factura " & oFra.Id)
                End If

            Catch ex As Exception
                Dim sId As String = ""
                If oFra IsNot Nothing Then sId = oFra.Id
                BLL.MailHelper.MailAdmin("SIGNFRA ERR " & sId & " " & ex.Message)
            End Try

        Loop
        oDrd.Close()
        MsgBox("pdf de factures " & iYea & " regenerats", MsgBoxStyle.Information, "MAT.NET")

    End Sub

    Public Sub ShowSegSocNewTc1()
        Dim oFrm As New Frm_SegSocNewCca
        oFrm.Show()
    End Sub

    Public Sub ShowDownloadSrc()
        'Dim oFrm As New Frm_DownloadSrc
        'oFrm.Show()
    End Sub

    Public Sub ShowPdcFchCreated()
        Dim oFrm As New Frm_PdcFchCreated
        oFrm.Show()
    End Sub

    Public Sub ShowCanarias()
        Dim oFrm As New Frm_Canarias
        oFrm.Show()
    End Sub

    Public Sub ShowCodisMercancia()
        Dim oFrm As New Frm_CodisMercancia
        oFrm.Show()
    End Sub

    Public Sub ShowPncsXFch()
        Dim oFrm As New Frm_PncsXFch
        oFrm.Show()
    End Sub

    Public Sub ShowFlota()
        Dim oFrm As New Frm_VehicleFlota
        oFrm.Show()
    End Sub

    Public Sub ShowGeoFras()
        Dim oFrm As New Frm_Geo_Fras
        oFrm.Show()
    End Sub

    Public Sub ShowFaqs()
        Dim oFrm As New Frm_Faqs
        oFrm.Show()
    End Sub


    Public Sub ShowContactEmails()
        Dim oFrm As New Frm_ContactEmails
        oFrm.Show()
    End Sub

    Public Sub ShowContracts()
        Dim oFrm As New Frm_Contracts
        oFrm.Show()
    End Sub

    Public Sub ShowAlbConfirms()
        Dim oFrm As New Frm_AlbConfirms
        oFrm.Show()
    End Sub

    Public Sub ShowEscriptures()
        Dim oFrm As New Frm_Escriptures
        oFrm.Show()
    End Sub

    Public Sub ShowAsnef()
        Dim oFrm As New Frm_Asnef
        oFrm.Show()
    End Sub

    Public Sub ShowBancConciliacions()
        Dim oFrm As New Frm_Banc_Conciliacions
        oFrm.Show()
    End Sub

    Public Sub Show_AEAT_Mod349()
        Dim oFrm As New frm_Aeat_Mod349
        oFrm.Show()
    End Sub

    Public Sub ShowQ43()
        Dim oFrm As New Frm_Banc_Conciliacions2
        oFrm.Show()
    End Sub

    Public Sub ShowAlbsTransferenciaPrevia()
        Dim oFrm As New Frm_AlbsTransferenciaPrevia(DTODelivery.RetencioCods.Transferencia)
        oFrm.Show()
    End Sub

    Public Sub ShowAlbsPendentsDeVisa()
        Dim oFrm As New Frm_AlbsTransferenciaPrevia(DTODelivery.RetencioCods.VISA)
        oFrm.Show()
    End Sub

    Public Sub ShowNukPncs()
        Dim oFrm As New Frm_NUK
        oFrm.Show()
    End Sub

    Public Sub ShowPorts()
        Dim oFrm As New Frm_Ports
        oFrm.Show()
    End Sub

    Public Sub ShowTelefons()
        Dim oFrm As New Frm_Telefons
        oFrm.Show()
    End Sub

    Public Sub ShowEdi()
        Dim oFrm As New Frm_Edi
        oFrm.Show()
    End Sub

    Public Sub ShowMod349()
        Dim oFrm As New Frm_fiscal_Mod349
        oFrm.Show()
    End Sub

    Public Sub ShowClientsEnActiu()
        Dim oFrm As New Frm_ClientsEnActiu
        oFrm.Show()
    End Sub

    Public Sub ShowCnaps()
        Dim oFrm As New Frm_Cnaps
        oFrm.Show()
    End Sub



    Public Sub ShowNukDesadvs()
        Dim oFrm As New Frm_Nuk_Desadvs
        oFrm.Show()
    End Sub

    Public Sub ShowNukPdcs()
        Dim oFrm As New Frm_NukPdcs
        oFrm.Show()
    End Sub


    Public Sub ShowEventos()
        Dim oFrm As New Frm_Eventos
        oFrm.Show()
    End Sub

    Public Sub ShowTxts()
        Dim oFrm As New Frm_Txts
        oFrm.Show()
    End Sub

    Public Sub ShowPr()
        Dim oFrm As New Frm_Pr
        oFrm.Show()
    End Sub

    Public Sub ShowTasks()
        Dim oFrm As New Frm_Tasks
        oFrm.Show()
    End Sub

    Public Sub ShowNews()
        Dim oFrm As New Frm_Noticias()
        oFrm.Show()
    End Sub

    Public Sub ShowSubscripcions()
        Dim oFrm As New Frm_Subscripcions
        oFrm.Show()
    End Sub

    Public Sub ShowLiniesTelefon()
        Dim oFrm As New Frm_LiniesTelefon
        oFrm.Show()
    End Sub

    Public Sub ShowTelConsums()
        Dim oFrm As New Frm_LiniaTelConsumsXMes
        oFrm.Show()
    End Sub

    Public Sub ShowTaxes()
        Dim oFrm As New Frm_Taxes
        oFrm.Show()
    End Sub

    Public Sub ShowCliCredits()
        Dim oFrm As New Frm_CliCredits
        oFrm.Show()
    End Sub




    Public Sub ShowInspeccio2008()
        Dim oFrm As New Frm_Inspeccio2008
        oFrm.Show()
    End Sub

    Public Sub ShowCreditsClients()
        Dim oFrm As New Frm_CreditsClients
        oFrm.Show()
    End Sub

    Public Sub ShowEFras()
        Dim oFrm As New Frm_EFras
        oFrm.Show()
    End Sub

    Public Sub ShowSystemUsers()
        Dim oFrm As New Frm_SystemUsers
        oFrm.Show()
    End Sub

    Public Sub ShowBigfiles()
        'Dim oFrm As New Frm_BigFiles
        'oFrm.Show()
    End Sub

    Public Sub ShowProductDownloads()
        'Dim oFrm As New Frm_ProductDownloads
        'oFrm.Show()
    End Sub

    Public Sub ShowYoutubes()
        Dim oFrm As New Frm_Youtubes
        oFrm.Show()
    End Sub

    Public Function LoadBigFileFromDialog(ByRef oBigFile As BigFileSrc) As Boolean
        Dim retval As Boolean = False
        Dim oDlg As New Windows.Forms.OpenFileDialog
        Dim oResult As Windows.Forms.DialogResult
        Dim sFilter As String = "Images(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|*.zip|*.zip|*.pdf|*.pdf|*.xls|*.xls|*.xlsx|*.xlsx|tots els arxius|*.*"
        With oDlg
            Select Case oBigFile.Src
                Case DTODocFile.Cods.Correspondencia
                    .Title = "IMPORTAR DOCUMENT DE CORRESPONDENCIA"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case DTODocFile.Cods.Assentament
                    .Title = "IMPORTAR JUSTIFICANT ASSENTAMENT"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case DTODocFile.Cods.Pdc
                    .Title = "IMPORTAR COPIA COMANDA"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case DTODocFile.Cods.Hisenda
                    .Title = "IMPORTAR DECLARACIO HISENDA"
                    sFilter = "*.pdf|*.pdf|*.xls|*.xls|*.xlsx|*.xlsx|tots els arxius|*.*"
                Case DTODocFile.Cods.CliDoc
                    .Title = "IMPORTAR DOCUMENT DE CONTACTE"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case DTODocFile.Cods.Download
                    .Title = "IMPORTAR FITXER PER DESCARREGA"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case DTODocFile.Cods.Dua
                    .Title = "Importar justificant d'exportació"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                Case Else
                    '.Title = "(importar fitxer d'alta resolució)"
                    .Title = "(importar fitxer generic)"
                    sFilter = "*.pdf|*.pdf|tots els arxius|*.*"
                    .FilterIndex = 2
                    'sFilter = "Images(*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|tots els arxius|*.*"
                    '.FilterIndex = 0
            End Select
            .Filter = sFilter
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    Dim sFilename As String = .FileName
                    Dim exs As New List(Of Exception)
                    oBigFile.BigFile.LoadFromFilename(sFilename, exs)
                    'oBigFile.BigFile = oBigfileNew
                    retval = True
            End Select

        End With
        Return retval
    End Function

    Public Function GetIcoFromMime(ByVal oMimeCod As DTOEnums.MimeCods) As Image
        Dim retVal As Image = Nothing
        Select Case oMimeCod
            Case DTOEnums.MimeCods.Pdf
                retVal = My.Resources.pdf
            Case DTOEnums.MimeCods.Xps
                retVal = My.Resources.xps
            Case DTOEnums.MimeCods.Doc, DTOEnums.MimeCods.Docx
                retVal = My.Resources.word
            Case DTOEnums.MimeCods.Xls, DTOEnums.MimeCods.Xlsx
                retVal = My.Resources.Excel
            Case DTOEnums.MimeCods.Jpg, DTOEnums.MimeCods.Gif, DTOEnums.MimeCods.Bmp, DTOEnums.MimeCods.Png, DTOEnums.MimeCods.Tif, DTOEnums.MimeCods.Tiff, DTOEnums.MimeCods.Ai, DTOEnums.MimeCods.Eps
                retVal = My.Resources.img_16
            Case DTOEnums.MimeCods.Zip
                retVal = My.Resources.zip
            Case Else
                retVal = My.Resources.empty
        End Select
        Return retVal
    End Function


    Public Sub ShowColeccions()
        'Dim oFrm As New Frm_Coleccions
        'oFrm.Show()
    End Sub

    Public Sub ShowWebMenuItems()
        Dim oFrm As New Frm_WebMenuItems(BLL.Defaults.SelectionModes.Browse)
        oFrm.Show()
    End Sub

    Public Function FilesDroppedArray(ByVal e As System.Windows.Forms.DragEventArgs) As ArrayList
        Dim retval As New ArrayList
        If FilesDroppedFromWindowsExplorerExists(e) Then
            retval.AddRange(FilesDroppedFromWindowsExplorerArray(e))
        ElseIf FilesDroppedFromOutlookMessageExists(e) Then
            retval.AddRange(FilesDroppedFromOutlookMessageArray(e))
        End If
        Return retval
    End Function

    Private Function FilesDroppedFromWindowsExplorerExists(ByVal e As System.Windows.Forms.DragEventArgs) As Boolean
        Dim retval As Boolean = e.Data.GetDataPresent(DataFormats.FileDrop, False)
        Return retval
    End Function

    Private Function FilesDroppedFromWindowsExplorerArray(ByVal e As System.Windows.Forms.DragEventArgs) As ArrayList
        Dim retval As New ArrayList
        If FilesDroppedFromWindowsExplorerExists(e) Then
            For Each sFilename As String In e.Data.GetData(DataFormats.FileDrop)
                retval.Add(sFilename)
            Next
        End If
        Return retval
    End Function

    Private Function FilesDroppedFromOutlookMessageExists(ByVal e As System.Windows.Forms.DragEventArgs) As Boolean
        Dim retval As Boolean = e.Data.GetDataPresent("FileGroupDescriptor")
        Return retval
    End Function

    Private Function FilesDroppedFromOutlookMessageArray(ByVal e As System.Windows.Forms.DragEventArgs) As ArrayList
        Dim sTmpPath As String = System.IO.Path.GetTempPath
        Dim retval As New ArrayList
        If FilesDroppedFromOutlookMessageExists(e) Then

            Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
            Dim fileGroupDescriptor(512) As Byte
            theStream.Read(fileGroupDescriptor, 0, 512)

            ' used to build the filename from the FileGroupDescriptor block
            Dim sfilename As String = ""
            For i As Integer = 76 To 512
                If fileGroupDescriptor(i) = 0 Then Exit For
                sfilename = sfilename & Convert.ToChar(fileGroupDescriptor(i))
            Next
            theStream.Close()
            sfilename = sTmpPath & sfilename

            Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
            BLL.FileSystemHelper.SaveStreamToFile(oMemStream, sfilename)
            retval.Add(sfilename)
        End If
        Return retval
    End Function

    Public Sub ShowEnquestes()
        Dim oFrm As New Frm_EnquestaHeaders
        oFrm.Show()
    End Sub

    Public Sub ShowCnapsStat()
        'Dim oFrm As New Frm_CnapsStat
        'oFrm.Show()
    End Sub

    Public Sub ShowGeoPdcs()
        Dim oFrm As New Frm_Geo_Pdcs
        oFrm.Show()
    End Sub

    Public Sub ShowCatalegUpload()
        Dim oFrm As New Frm_Cataleg_Upload
        oFrm.Show()
    End Sub

    Public Sub ShowEciPdcs()
        Dim oFrm As New Frm_ECI_Pdcs
        oFrm.Show()
    End Sub

    Public Sub ShowQuizs()
        Dim oFrm As New Frm_Quizs
        oFrm.Show()
    End Sub

    Public Sub ShowBookFras()
        Dim oFrm As New Frm_BookFras
        oFrm.Show()
    End Sub

    Public Sub ShowWebQuadRelay()
        Dim oFrm As New Frm_WebQuadRelay
        oFrm.Show()
    End Sub

    Public Sub ShowBlog()
        Dim oFrm As New Frm_Blog
        oFrm.Show()
    End Sub

    Public Sub ShowExport()
        Dim oFrm As New Frm_Export_Main
        oFrm.Show()
    End Sub

    Public Sub ShowSepaBancs()
        Dim oFrm As New Frm_Sepa_Bancs
        oFrm.Show()
    End Sub

    Public Function GetBigfileFromDatagridDrop(oGrid As DataGridView, ByVal e As System.Windows.Forms.DragEventArgs, ByRef oBigfile As MaxiSrvr.BigFileNew, ByRef sFilename As String, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oPoint = oGrid.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = oGrid.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = oGrid.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            oGrid.CurrentCell = oclickedCell

            Dim fileNames() As String = Nothing
            Dim oStream As Byte()

            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                fileNames = e.Data.GetData(DataFormats.FileDrop)
                sFilename = fileNames(0)
                ' get the actual raw file into memory
                Dim oFileStream As New System.IO.FileStream(sFilename, IO.FileMode.Open)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oFileStream)
                oStream = oBinaryReader.ReadBytes(oFileStream.Length)
                oBinaryReader.Close()
                oFileStream.Close()

            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                ' the first step here is to get the filename
                ' of the attachment and
                ' build a full-path name so we can store it 
                ' in the temporary folder
                '
                ' set up to obtain the FileGroupDescriptor 
                ' and extract the file name
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sFilename = sFilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()
                '
                ' Second step:  we have the file name.  
                ' Now we need to get the actual raw
                ' data for the attached file .
                '
                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                oStream = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
            Else
                exs.Add(New Exception("format desconegut"))
                Return False
                Exit Function
            End If

            root.LoadBigFile(oBigfile, oStream, sFilename)
            retval = True
        End If

        Return retval
    End Function

    Public Sub Show_Last_albs_enAltres()
        Dim oFrm As New Frm_Last_albs_enAltres
        oFrm.Show()
    End Sub



    Public Sub PrintDoc_Preview(ByVal oDocRpt As maxisrvr.DocRpt)
        Dim oFrm As New Rpt_Docs
        oFrm.DocRpt = oDocRpt
        oFrm.Printpreview()
    End Sub

    Public Sub PrintDoc(ByVal oDocRpt As maxisrvr.DocRpt, Optional ByVal CopiasExtra As Integer = 0)
        Dim oFrm As New Rpt_Docs
        'AddHandler oFrm.AfterPrintDoc, AddressOf RaiseAfterPrintDoc
        oFrm.DocRpt = oDocRpt
        oFrm.print(oDocRpt.Papel, maxisrvr.DocRpt.Ensobrados.Sencillo, CopiasExtra)

        'Select Case oDocRpt.Papel
        'Case oDocRpt.FuentePapel.Original
        'oKind = 1272 'preprinted
        'oKind = Drawing.Printing.PaperSourceKind.Lower
        'oKind = Drawing.Printing.PaperSourceKind.Upper
        'Case Else
        'oKind = Drawing.Printing.PaperSourceKind.Upper
        'oFrm.print(oKind, oDocRpt.Ensobrados.Sencillo, CopiasExtra)
        'End Select
    End Sub


    Public Sub ShowAEB19(Optional ByVal oAEB19 As AEB19 = Nothing)
        Dim oFrm As New Frm_AEB19
        With oFrm
            .Aeb19 = oAEB19
            .Show()
        End With
    End Sub

    Public Sub ShowLiteral(ByVal sCaption As String, ByVal sText As String)
        Dim oFrm As New Frm_Literal(sCaption, sText)
        oFrm.Show()
    End Sub

    Public Sub ShowMiscImg()
        Dim oFrm As New Frm_MiscImg
        With oFrm
            .Show()
        End With
    End Sub





    Public Function LoadBigFile(ByRef oBigFile As BigFileNew, ByVal oStream As Byte(), Optional ByVal sFileName As String = "") As Boolean
        oBigFile.Stream = oStream
        oBigFile.Filename = sFileName
        oBigFile.Size = oStream.Length
        If oBigFile.MimeCod = DTOEnums.MimeCods.NotSet Then
            Dim oExtension As DTOEnums.MimeCods = BLL.FileSystemHelper.GetMimeFromExtension(sFileName)
            oBigFile.MimeCod = oExtension
        End If

        Select Case oBigFile.MimeCod
            Case DTOEnums.MimeCods.Jpg, DTOEnums.MimeCods.Gif
                With oBigFile
                    Dim oBigImg As Image = maxisrvr.GetImgFromByteArray(.Stream)
                    .Width = oBigImg.Width
                    .Height = oBigImg.Height
                    .Hres = oBigImg.HorizontalResolution
                    .Vres = oBigImg.VerticalResolution
                    .Img = maxisrvr.GetThumbnail(oBigImg, maxisrvr.BigFileNew.THUMB_WIDTH, maxisrvr.BigFileNew.THUMB_HEIGHT)
                End With
            Case DTOEnums.MimeCods.Zip
                oBigFile.Img = Nothing
            Case DTOEnums.MimeCods.Pdf
                With oBigFile
                    Dim oPdfRender As New PdfRender(oBigFile.Stream)
                    .Pags = oPdfRender.PageCount
                    .Width = Math.Round(oPdfRender.Width / PdfRender.FACTOR_PDFTOMM, 0)
                    .Height = Math.Round(oPdfRender.Height / PdfRender.FACTOR_PDFTOMM, 0)
                    .Img = oPdfRender.Thumbnail(BigFileNew.THUMB_WIDTH, BigFileNew.THUMB_HEIGHT)
                End With
            Case DTOEnums.MimeCods.Xps
                maxisrvr.BigFileNew.LoadFromXpsStream(oBigFile, oStream)
            Case DTOEnums.MimeCods.Xml
            Case DTOEnums.MimeCods.Xls, DTOEnums.MimeCods.Xlsx
            Case DTOEnums.MimeCods.Rtf, DTOEnums.MimeCods.Txt, DTOEnums.MimeCods.Pla
        End Select
        Return True
    End Function

    Public Sub LoadBigFilePdfFile(ByRef oBigFile As maxisrvr.BigFileNew, ByVal sFileName As String)
        Dim oByteArray As Byte() = Nothing
        Dim exs As New List(Of exception)
        If BLL.FileSystemHelper.GetStreamFromFile(sFileName, oByteArray, exs) Then
            LoadBigFilePdfStream(oBigFile, oByteArray)
        Else
            MsgBox("error al importar el fitxer" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub LoadBigFilePdfStream(ByRef oBigFile As maxisrvr.BigFileNew, ByVal oStream As Byte())
        With oBigFile
            oBigFile.Stream = oStream
            oBigFile.Size = oBigFile.Stream.Length
            .MimeCod = DTOEnums.MimeCods.Pdf
            Dim oPdfRender As New PdfRender(oBigFile.Stream)
            .Pags = oPdfRender.PageCount
            .Width = Math.Round(oPdfRender.Width / PdfRender.FACTOR_PDFTOMM, 0)
            .Height = Math.Round(oPdfRender.Height / PdfRender.FACTOR_PDFTOMM, 0)
            .Img = oPdfRender.Thumbnail(BigFileNew.THUMB_WIDTH, BigFileNew.THUMB_HEIGHT)
        End With
    End Sub

    Public Sub ShowMovie()
        Dim oMovie As New maxisrvr.Movie(New Guid("d380c9c5-3708-43ed-8bf1-1525be264fdc"))
        Dim oFrm As New Frm_Movie
        With oFrm
            .Movie = oMovie
            .Show()
        End With
    End Sub



    Public Sub ShowPdf(ByVal sFileName As String)
        Process.Start(sFileName)
        'Process.Start("IExplore.exe", sFileName)
        'Process.Start("Acrobat.exe", sFileName)
    End Sub

    Public Sub ShowPdf(ByVal oBuffer() As Byte, Optional ByVal sFileName As String = "")
        Dim sTmpFolder As String = maxisrvr.TmpFolder
        Dim oTmpFolder As System.IO.DirectoryInfo = BLL.FileSystemHelper.CleanOrCreateFolder(sTmpFolder)

        If sFileName > "" Then
            If Right(sFileName, 4) <> ".pdf" Then sFileName = sFileName & ".pdf"

            'carregat el fitxer si ja existeix
            'si no t'el pots carregar perque está ocupat, dona un nom aleatori
            Try
                Dim oFile As New IO.FileInfo(sTmpFolder & sFileName)
                If oFile.Exists() Then oFile.Delete()
            Catch ex As Exception
                sFileName = System.Guid.NewGuid.ToString & ".pdf"
            End Try
        Else
            sFileName = System.Guid.NewGuid.ToString & ".pdf"
        End If

        Dim sFullPath As String = oTmpFolder.FullName & sFileName
        Dim oFileStream As New System.IO.FileStream(sFullPath, IO.FileMode.CreateNew)
        oFileStream.Write(oBuffer, 0, oBuffer.Length)
        oFileStream.Close()
        ' oStream.Save(sFileName)
        ShowPdf(sFullPath)
    End Sub

End Module

