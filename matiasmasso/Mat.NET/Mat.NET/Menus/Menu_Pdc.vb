
Imports Microsoft.Office.Interop

Public Class Menu_Pdc
    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _PurchaseOrder As DTOPurchaseOrder

    Private mPdcs As Pdcs
    Private mPdc As Pdc
    Private mMenuItem_NoRep As ToolStripMenuItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(ByVal oPurchaseOrder As DTOPurchaseOrder)
        MyBase.New()
        _PurchaseOrder = oPurchaseOrder
        _PurchaseOrders = New List(Of DTOPurchaseOrder)
        _PurchaseOrders.Add(_PurchaseOrder)

        mPdc = New Pdc(_PurchaseOrder.Guid)
        mPdcs = New Pdcs
        mPdcs.Add(mPdc)
    End Sub

    Public Sub New(ByVal oPurchaseOrders As List(Of DTOPurchaseOrder))
        MyBase.New()
        _PurchaseOrders = oPurchaseOrders
        _PurchaseOrder = oPurchaseOrders(0)

        mPdcs = New Pdcs
        For Each item As DTOPurchaseOrder In _PurchaseOrders
            Dim oPdc As New Pdc(item.Guid)
            mPdcs.Add(oPdc)
        Next
    End Sub

    Public Sub New(ByVal oPdc As Pdc)
        MyBase.New()
        _PurchaseOrder = New DTOPurchaseOrder(oPdc.Guid)
        _PurchaseOrders = New List(Of DTOPurchaseOrder)
        _PurchaseOrders.Add(_PurchaseOrder)

        mPdc = oPdc
        mPdcs = New Pdcs
        mPdcs.Add(mPdc)
    End Sub

    Public Sub New(ByVal oPdcs As Pdcs)
        MyBase.New()
        _PurchaseOrders = New List(Of DTOPurchaseOrder)
        For Each item As Pdc In oPdcs
            Dim oOrder As New DTOPurchaseOrder(item.Guid)
            _PurchaseOrders.Add(oOrder)
        Next
        _PurchaseOrder = _PurchaseOrders(0)

        mPdcs = oPdcs
        mPdc = oPdcs(0)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Print(), _
        MenuItem_Confirmacio(), _
        MenuItem_Proforma(), _
        MenuItem_Document(), _
        MenuItem_Tpv(), _
        MenuItem_Alb(), _
        MenuItem_Del(), _
        MenuItem_Avançats(), _
        MenuItem_Clon(), _
        MenuItem_Excel()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If mPdcs.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Print() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imprimir"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_PdcPrint
        Return oMenuItem
    End Function

    Private Function MenuItem_Confirmacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Confirmacio"
        With oMenuItem.DropDownItems
            .Add(MenuItem_ConfirmacioZoom)
            .Add(MenuItem_ConfirmacioExportar)
            .Add(MenuItem_ConfirmacioHtml)
            .Add(MenuItem_ConfirmacioEmail)
        End With
        Return oMenuItem
    End Function


    Private Function MenuItem_ConfirmacioZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Confirmacio"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_PdcConfirmacio
        Return oMenuItem
    End Function

    Private Function MenuItem_ConfirmacioExportar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_PdcPdfExport
        Return oMenuItem
    End Function

    Private Function MenuItem_ConfirmacioHtml() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Html"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_PdcHtml
        Return oMenuItem
    End Function

    Private Function MenuItem_ConfirmacioEmail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_PdcEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_Proforma() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proforma"
        With oMenuItem.DropDownItems
            .Add(MenuItem_ProformaPrint)
            .Add(MenuItem_ProformaPdf)
            .Add(MenuItem_ProformaPdfExport)
            .Add(MenuItem_ProformaEmail)
            .Add(MenuItem_PackingList)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Document() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Document"
        With oMenuItem.DropDownItems
            .Add(MenuItem_DocumentZoom)
            .Add(MenuItem_DocumentImportar)
            .Add(MenuItem_CopyLink)
            .Add(MenuItem_Edi_ExportToDisk)
            .Add(MenuItem_Edi_SaveToQueue)
            .Add(MenuItem_Edi_Browse)
            '.Add(MenuItem_ProformaPdf)
            '.Add(MenuItem_ProformaPdfExport)
            '.Add(MenuItem_ProformaEmail)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Tpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a Tpv"
        'oMenuItem.Image = My.Resources.MailSobreObert
        oMenuItem.Enabled = mPdc.TpvEnabled
        AddHandler oMenuItem.Click, AddressOf Do_Tpv
        Return oMenuItem
    End Function

    Private Function MenuItem_DocumentImportar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar"
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_DocumentImportar
        Return oMenuItem
    End Function

    Private Function MenuItem_DocumentZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Visualitzar"
        oMenuItem.Enabled = mPdc.DocFile IsNot Nothing
        oMenuItem.Image = My.Resources.binoculares
        AddHandler oMenuItem.Click, AddressOf Do_DocumentZoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Edi_ExportToDisk() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "exportar Edi a disc"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_Edi_Export
        Return oMenuItem
    End Function

    Private Function MenuItem_Edi_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Edi vista previa"
        'oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_Edi_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_Edi_SaveToQueue() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Edi a cua proveidor"
        oMenuItem.Image = My.Resources.save_16
        If mPdc.Cod <> DTOPurchaseOrder.Codis.proveidor Then oMenuItem.Visible = False
        AddHandler oMenuItem.Click, AddressOf Do_Edi_Queue
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaPrint() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imprimir"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPrint
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaPdfExport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPdfExport
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaEmail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_ProformaEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_PackingList() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "packing list"
        AddHandler oMenuItem.Click, AddressOf Do_PackingList
        Return oMenuItem
    End Function

    Private Function MenuItem_Alb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "nou albará"
        oMenuItem.Enabled = False
        If mPdcs.Count = 1 Then
            If mPdcs(0).Client IsNot Nothing Then
                If mPdcs(0).Client.ExistPncs Then
                    oMenuItem.Enabled = True
                End If
            End If
        End If
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Alb
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.UNDO
        oMenuItem.Enabled = mPdcs.AllowDelete
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function

    Private Function MenuItem_Avançats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats..."
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
                oMenuItem.DropDownItems.Add(MenuItem_GenerateOrderToSuplier)
                oMenuItem.DropDownItems.Add(MenuItem_RecuperaLiniesDeSortides)
                oMenuItem.DropDownItems.Add(MenuItem_RecuperaAlbPrvRoche)
                oMenuItem.DropDownItems.Add(MenuItem_RecalculaPendents)
                oMenuItem.DropDownItems.Add(MenuItem_RedoPdf)
                oMenuItem.DropDownItems.Add(MenuItem_NoRep)
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_GenerateOrderToSuplier() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "genera comanda a proveidor"
        AddHandler oMenuItem.Click, AddressOf Do_GenerateOrderToSuplier
        Return oMenuItem
    End Function

    Private Function MenuItem_RecuperaLiniesDeSortides() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recupera linies perdudes"
        AddHandler oMenuItem.Click, AddressOf Do_RecuperaLiniesDeSortides
        Return oMenuItem
    End Function

    Private Function MenuItem_RecuperaAlbPrvRoche() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recupera alb prv Roche"
        AddHandler oMenuItem.Click, AddressOf Do_RecuperaAlbPrvRoche
        Return oMenuItem
    End Function

    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "clon"
        oMenuItem.Image = My.Resources.tampon
        'oMenuItem.Enabled = mPdcs.AllowDelete
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_RecalculaPendents() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recalcula pendents"
        AddHandler oMenuItem.Click, AddressOf Do_RecalculaPendents
        Return oMenuItem
    End Function

    Private Function MenuItem_RedoPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "redacta document de nou"
        AddHandler oMenuItem.Click, AddressOf Do_RedactaPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_NoRep() As ToolStripMenuItem
        mMenuItem_NoRep = New ToolStripMenuItem
        With mMenuItem_NoRep
            .Text = "rep deshabilitat"
            .CheckOnClick = True
            .Checked = (mPdc.NoRep = MaxiSrvr.TriState.Verdadero)
            AddHandler .Click, AddressOf Do_NoRep
        End With
        Return mMenuItem_NoRep
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "excel"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLPurchaseOrder.Load(_PurchaseOrder, BLL.BLLApp.Mgz)
        If _PurchaseOrder.Cod = DTOPurchaseOrder.Codis.NotSet Then BLL.BLLPurchaseOrder.Load(_PurchaseOrder, BLL.BLLApp.Mgz)
        Select Case _PurchaseOrder.Cod
            Case DTOPurchaseOrder.Codis.Client
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, _PurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(_PurchaseOrder)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(mPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_Pdc_Proveidor(mPdc)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            Case Else
                MsgBox("nomes implementades les comandes de client o proveidor", MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Private Sub Do_PdcPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintPdcs(mPdcs)
    End Sub

    Private Sub Do_ProformaPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintProforma(mPdcs)
    End Sub

    Private Sub Do_PdcConfirmacio(ByVal sender As Object, ByVal e As System.EventArgs)
        BrowsePdf(False)
        Exit Sub
        '======================================================
        If mPdcs.Count = 1 Then
            'Dim oBigFile As maxisrvr.BigFile = mPdcs(0).BigFile
            'root.ShowBigFile(oBigFile, mPdcs(0).FileName)
        Else
            root.ShowPdf(mPdcs.PdfStream(True))
        End If
    End Sub

    Private Sub Do_PdcPdfExport(ByVal sender As Object, ByVal e As System.EventArgs)
        SavePdf(False)
    End Sub

    Private Sub Do_ProformaPdf(ByVal sender As Object, ByVal e As System.EventArgs)
        BrowsePdf(True)
    End Sub

    Private Sub Do_ProformaPdfExport(ByVal sender As Object, ByVal e As System.EventArgs)
        SavePdf(True)
    End Sub


    Private Sub Do_PdcHtml(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mPdc.Url(True)
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_PdcEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Email(False)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)

        If mPdc.DocFile IsNot Nothing Then
            Dim sUrl As String = BLL.BLLDocFile.DownloadUrl(mPdc.DocFile, True)
            Clipboard.SetDataObject(sUrl, True)
        Else
            MsgBox("no hi ha cap document assignat a aquesta comanda", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Do_ProformaEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Email(True)
    End Sub

    Private Sub Do_PackingList(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = "csv"
            .AddExtension = True
            .FileName = "packing list.csv"
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "guardar packing list"
            If .ShowDialog Then
                Dim oPackingList As PackingList = PackingList.FromPdc(mPdc)

                Dim exs as New List(Of exception)
                If Not oPackingList.Save(.FileName, exs) Then
                    UIHelper.WarnError( exs, "error al desar el document")
                End If

            End If
        End With
    End Sub

    Private Sub Do_Tpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = BLL_Tpv.url(mPdc)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & mPdcs.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mPdcs.Delete( exs) Then
                MsgBox("Comandes eliminades", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, e)
            Else
                MsgBox("error al eliminar les comandes" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub Do_DocumentImportar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        Dim sTitle As String = "Importar document"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            mPdc.DocFile = oDocFile
            If mPdc.Update( exs) Then
                Dim oArgs As New AfterUpdateEventArgs(mPdc, AfterUpdateEventArgs.Modes.NotSet)
                RaiseEvent AfterUpdate(sender, oArgs)
            Else
                MsgBox("error al desar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("error al importar document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub Do_DocumentZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mPdc.Url(True)
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_Alb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(mPdc.Guid)
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.ALB, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oClient As Client = mPdc.Client
            Dim oAlb As Alb = oClient.NewAlb
            Dim oFrm As New Frm_AlbNew2(oAlb)
            oFrm.Show()
        End If



    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPdcClon As Pdc = mPdc.Clon
        Select Case oPdcClon.Cod
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oPdcClon.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_Pdc_Proveidor(oPdcClon)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If

            Case DTOPurchaseOrder.Codis.Client
                Dim oSrc As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(mPdc.Guid)
                Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Clon(oSrc)

                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            Case Else
                MsgBox("no implementat per aquest tipus de comanda")
        End Select
    End Sub

    Private Sub Do_Edi_Export(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oEdiFile As EdiFile_ORDERS_D_96A_UN_EAN008 = Nothing
        Dim exs As New list(Of Exception)
        If mPdc.EDIFile(oEdiFile, exs) Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .FileName = oEdiFile.DefaultFileName
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    oEdiFile.Save(.FileName)
                    RaiseEvent AfterUpdate(sender, e)
                End If
            End With
        Else
            MsgBox("Error al redactar el fitxer EDI:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
        End If



    End Sub

    Private Sub Do_Edi_Queue(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oEdiFile As EdiFile_ORDERS_D_96A_UN_EAN008 = Nothing
        Dim exs As New list(Of Exception)

        Dim blProceed As Boolean
        If mPdc.EDIFile(oEdiFile, exs) Then
            blProceed = True
        Else
            Dim rc As MsgBoxResult = MsgBox("Error al redactar el fitxer EDI:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs) & "ignorar per tirar-ho endavant igualment", MsgBoxStyle.AbortRetryIgnore, "MAT.NET")
            blProceed = (rc = MsgBoxResult.Ignore)
        End If

        If blProceed Then
            If oEdiFile.Update(exs) Then
                If mPdc.Source <> DTOPurchaseorder.Sources.edi Then
                    mPdc.SetItm()
                    mPdc.Source = DTOPurchaseorder.Sources.edi

                    If mPdc.Update(exs) Then
                        RefreshRequest(sender, e)
                    Else
                        MsgBox("error al desar la comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If
                MsgBox(oEdiFile.Filename & " en cua de transmisió." & vbCrLf & oEdiFile.RoutingUrl(True))
            Else
                UIHelper.WarnError(exs, "error al desar el fitxer")
            End If
        End If

    End Sub

    Private Sub Do_Edi_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oEdiFile As EdiFile_ORDERS_D_96A_UN_EAN008 = Nothing
        Dim exs As New list(Of Exception)
        mPdc.EDIFile(oEdiFile, exs)
        Dim sb As New System.Text.StringBuilder

        If exs.Count > 0 Then
            sb.AppendLine("=======================================")
            sb.AppendLine("fitxer no validat (" & exs.Count.ToString & " errors detectats)")
            sb.AppendLine("=======================================")
            For Each oErr As Exception In exs
                sb.AppendLine(oErr.Message)
            Next
            sb.AppendLine("=======================================")
            sb.AppendLine()
        End If

        For Each oSegment As EdiSegment In oEdiFile.Segments
            sb.AppendLine(oSegment.ToString)
        Next
        root.ShowLiteral("EDI comanda " & mPdc.Formatted & " " & mPdc.Client.Nom_o_NomComercial, sb.ToString)
    End Sub

    Private Sub Do_GenerateOrderToSuplier(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNewPdcToSuplier As New Pdc(BLL.BLLApp.Emp, DTOPurchaseOrder.Codis.proveidor)
        Dim oPncSuplier As LineItmPnc = Nothing
        Dim oSuplier As Proveidor = Nothing

        For Each oPdcFromClient As Pdc In mPdcs
            For Each oPncClient As LineItmPnc In oPdcFromClient.Itms
                Dim oArt As Art = oPncClient.Art
                Dim itemFoundInSuplierPdc As Boolean = False
                For Each oPncSuplier In oNewPdcToSuplier.Itms
                    If oPncSuplier.Art.Id = oArt.Id Then
                        With oPncSuplier
                            .Qty += oPncClient.Qty
                            .Pendent += oPncClient.Pendent
                            .Preu = oArt.Cost
                        End With
                        itemFoundInSuplierPdc = True
                        Exit For
                    End If
                Next

                If Not itemFoundInSuplierPdc Then
                    If oSuplier Is Nothing Then oSuplier = oArt.Stp.Tpa.Proveidor
                    oPncSuplier = New LineItmPnc
                    With oPncSuplier
                        .Art = oPncClient.Art
                        .Qty += oPncClient.Qty
                        .Pendent += oPncClient.Pendent
                        .Preu = oArt.Cost
                    End With
                    oNewPdcToSuplier.Itms.Add(oPncSuplier)
                End If
            Next
        Next

        With oNewPdcToSuplier
            .Fch = Today
            .Client = New Client(oSuplier.Guid)
            .Source = DTOPurchaseorder.Sources.eMail
            .Cur = oSuplier.DefaultCur
            .Mgz = BLL.BLLApp.Mgz
        End With


        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oNewPdcToSuplier.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_Pdc_Proveidor(oNewPdcToSuplier)
            oFrm.Show()
        End If

    End Sub

    Private Sub Do_RecuperaLiniesDeSortides(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iRows As Integer = mPdc.RecuperaLiniesDeSortides()
        If iRows = 0 Then
            MsgBox("no s'han trobat sortides d'aquesta comanda", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            MsgBox("S'han recuperat " & iRows.ToString & " linies de comanda" & vbCrLf & "revisar preus i descomptes especialment en aquelles linies que hagin sortit en mes de un albará i poguessin tenir preus diferents")
            RefreshRequest(sender, e)
        End If
    End Sub

    Private Sub Do_NoRep(ByVal sender As Object, ByVal e As System.EventArgs)
        mPdc.SetItm()
        mPdc.NoRep = IIf(mMenuItem_NoRep.Checked, MaxiSrvr.TriState.Verdadero, MaxiSrvr.TriState.Falso)
        Dim exs as New List(Of exception)
        If Not mPdc.Update( exs) Then
            MsgBox("error al desar la comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    '==========================================================================
    '                               AUXILIARS
    '==========================================================================

    Private Sub Email(ByVal BlProforma As Boolean)
        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            If mPdcs.SameClient Then
                If Not mPdcs(0).Client.Email Is Nothing Then
                    .Recipients.Add(mPdcs(0).Client.Email)
                End If
            End If
            .Subject = UCase(mPdcs.ToString)
            .Attachments.Add(GetTmpPdfFile(BlProforma), , , mPdcs.ToString & ".pdf")
            .Display()
        End With
    End Sub

    Private Sub BrowsePdf(ByVal BlProforma As Boolean)
        If mPdcs.Count = 1 Then
            Dim oPdc As Pdc = mPdcs(0)
            Dim sFilename As String = oPdc.ToString & ".pdf"
            Dim BlSigned As Boolean = (oPdc.Cod = DTOPurchaseOrder.Codis.proveidor)
            root.ShowPdf(mPdcs.PdfStream(BlSigned, BlProforma), sFilename)
        Else
            root.ShowPdf(mPdcs.PdfStream(False, BlProforma))
        End If
        'Dim sFileName As String = GetTmpPdfFile(BlProforma)
        'root.ShowPdf(sFilename)
    End Sub

    Private Function GetTmpPdfFile(ByVal BlProforma As Boolean) As String
        Dim sFileName As String = "M+O " & mPdcs.ToString & ".pdf"

        Dim exs as New List(Of exception)
        If Not BLL.FileSystemHelper.SaveStream(mPdcs.PdfStream(False, BlProforma), exs, sFileName) Then
            UIHelper.WarnError( exs, "error al desar el document")
        End If

        Return sFileName
    End Function

    Private Sub SavePdf(ByVal BlProforma As Boolean)
        'Dim BlSigned As Boolean = (mPdcs.Count = 1) And mPdcs(0).Cod = DTOPurchaseOrder.Codis.proveidor 'signa si treus les factures de una en una.
        'root.ShowPdf(mPdcs.Pdf(BlSigned, BlProforma))
        'Exit Sub
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & mPdcs.ToString(BlProforma) & ".pdf"
        Dim sTit As String = IIf(BlProforma, "PROFORMES", "COMANDES")
        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR " & sTit
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                Dim exs as New List(Of exception)
                If Not BLL.FileSystemHelper.SaveStream(mPdcs.PdfStream(False, BlProforma), exs, .FileName) Then
                    UIHelper.WarnError( exs, "error al desar el document")
                End If
            End If
        End With

    End Sub

    Private Sub Do_RedactaPdf()
        Dim exs as New List(Of exception)
        Dim oByteArray As Byte() = mPdc.PdfStream(mPdc.Cod = DTO.DTOPurchaseOrder.Codis.proveidor)

        Dim oDocFile As DTODocFile = Nothing
        If BLL_DocFile.LoadFromStream(oByteArray, oDocFile, exs) Then
            oDocFile.PendingOp = DTODocFile.PendingOps.Update
            mPdc.DocFile = oDocFile
            If mPdc.Update(exs) Then
                RaiseEvent AfterUpdate(Me, EventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al redactar el document")
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar el document")
        End If
    End Sub

    Private Sub Do_RecuperaAlbPrvRoche(sender As Object, e As System.EventArgs)
        eDiversa.RedoPrvAlbFromCliPdc(mPdc)
        MsgBox("Ok")
    End Sub

    Private Sub Do_RecalculaPendents(sender As Object, e As System.EventArgs)
        Dim iCount As Integer = mPdc.RecalculaPendents()
        MsgBox(iCount & " linies actualitzades")
    End Sub

    Private Sub Do_Excel()
        Dim oExcelSheet As New DTOExcelSheet()
        For Each oPdc As Pdc In mPdcs
            Dim oRow As New DTOExcelRow(oExcelSheet)
            oRow.AddCell(oPdc.Id, oPdc.Url(True))
            oRow.AddCell(oPdc.Fch)
            oRow.AddCell(oPdc.BaseImponible)
            oExcelSheet.Rows.Add(oRow)
        Next
        UIHelper.ShowExcel(oExcelSheet)
    End Sub


    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent AfterUpdate(sender, e)
        Catch ex As Exception
        End Try
    End Sub

End Class