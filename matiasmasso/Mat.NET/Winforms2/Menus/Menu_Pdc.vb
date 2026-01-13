Public Class Menu_Pdc
    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _PurchaseOrder As DTOPurchaseOrder

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oPurchaseOrders As List(Of DTOPurchaseOrder))
        MyBase.New()
        _PurchaseOrders = oPurchaseOrders
        _PurchaseOrder = oPurchaseOrders(0)
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Confirmacio(),
        MenuItem_Proforma(),
        MenuItem_Document(),
        MenuItem_CopyLink(),
        MenuItem_Tpv(),
        MenuItem_Alb(),
        MenuItem_Del(),
        MenuItem_Avançats(),
        MenuItem_Clon(),
        MenuItem_Excel()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If _PurchaseOrders.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
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
            .Add(MenuItem_ConfirmacioEdiSave)
            .Add(MenuItem_ConfirmacioEdiFtp)
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

    Private Function MenuItem_ConfirmacioEdiSave() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "desar fitxer Edi"
        'oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_ConfirmacioEdiSave
        Return oMenuItem
    End Function

    Private Function MenuItem_ConfirmacioEdiFtp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enviar fitxer Edi per Ftp"
        'oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_ConfirmacioEdiFtp
        Return oMenuItem
    End Function

    Private Function MenuItem_Proforma() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proforma"
        With oMenuItem.DropDownItems
            .Add(MenuItem_ProformaPdf)
            .Add(MenuItem_ProformaPdfExport)
            .Add(MenuItem_ProformaEmail)
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
            .Add(MenuItem_Excel)
            '.Add(MenuItem_ProformaPdfExport)
            '.Add(MenuItem_ProformaEmail)
        End With
        Return oMenuItem
    End Function


    Private Function MenuItem_Tpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a Tpv"
        AddHandler oMenuItem.Click, AddressOf Do_CopyTpv
        Return oMenuItem
    End Function

    Private Function MenuItem_DocumentImportar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar"
        AddHandler oMenuItem.Click, AddressOf Do_DocumentImportar
        Return oMenuItem
    End Function

    Private Function MenuItem_DocumentZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Visualitzar"
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

    Private Function MenuItem_CopyLinkToDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç al document"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToDoc
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
        If _PurchaseOrder.cod <> DTOPurchaseOrder.Codis.proveidor Then oMenuItem.Visible = False
        AddHandler oMenuItem.Click, AddressOf Do_Edi_Queue
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


    Private Function MenuItem_Alb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "nou albarà"
        oMenuItem.Enabled = False
        If _PurchaseOrders.Count = 1 Then
            oMenuItem.Enabled = True
        End If
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Alb
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.UNDO
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function

    Private Function MenuItem_Avançats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats..."
        oMenuItem.DropDownItems.Add(MenuItem_EtiquetesTransport)
        oMenuItem.DropDownItems.Add(MenuItem_RedoPdf)
        oMenuItem.DropDownItems.Add(MenuItem_RecalculaPendents)
        oMenuItem.DropDownItems.Add(MenuItem_MailJustificant)
        oMenuItem.DropDownItems.Add(MenuItem_BlockPendingItems)
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin
                oMenuItem.DropDownItems.Add(MenuItem_GenerateOrderToSuplier)
                'oMenuItem.DropDownItems.Add(MenuItem_RecuperaLiniesDeSortides) HO LIA TOT AL MENYS EN COMANDES DE PROVEIDOR
                oMenuItem.DropDownItems.Add(MenuItem_NoRep)
                If Current.Session.User.Rol.isSuperAdmin Then
                    oMenuItem.DropDownItems.Add(MenuItem_ToCompactCustomerPO)
                End If
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_ToCompactCustomerPO() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar CompactCustomerPO json"
        AddHandler oMenuItem.Click, AddressOf Do_Copy_CompactCustomerPO
        Return oMenuItem
    End Function
    Private Function MenuItem_EtiquetesTransport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Etiquetes transportista"
        AddHandler oMenuItem.Click, AddressOf Do_EtiquetesTransport
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


    Private Function MenuItem_BlockPendingItems() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Bloquejar pendents d'entrega"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_BlockPendingItems
        Return oMenuItem
    End Function

    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "clon"
        oMenuItem.Image = My.Resources.tampon
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_RecalculaPendents() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recalcula pendents"
        AddHandler oMenuItem.Click, AddressOf Do_RecalculaPendents
        Return oMenuItem
    End Function

    Private Function MenuItem_MailJustificant() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email justificant"
        AddHandler oMenuItem.Click, AddressOf Do_MailJustificant
        Return oMenuItem
    End Function


    Private Function MenuItem_RedoPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "redacta document de nou"

        AddHandler oMenuItem.Click, AddressOf Do_RedactaPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_NoRep() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "retirar comissió de rep"
        AddHandler oMenuItem.Click, AddressOf Do_NoRep
        Return oMenuItem
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

    Private Async Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, _PurchaseOrder, GlobalVariables.Emp.Mgz) Then
            Select Case _PurchaseOrder.cod
                Case DTOPurchaseOrder.Codis.client
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _PurchaseOrder.contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(_PurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOPurchaseOrder.Codis.proveidor
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _PurchaseOrder.contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder_Proveidor(_PurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Else
                    MsgBox("nomes implementades les comandes de client o proveidor", MsgBoxStyle.Exclamation)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub




    Private Async Sub Do_PdcConfirmacio(ByVal sender As Object, ByVal e As System.EventArgs)
        Await BrowsePdf(False)
    End Sub

    Private Sub Do_PdcPdfExport(ByVal sender As Object, ByVal e As System.EventArgs)
        SavePdf(False)
    End Sub

    Private Sub Do_ProformaPdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_FreeText("Observacions proforma")
        AddHandler oFrm.AfterUpdate, AddressOf onProforma
        oFrm.Show()
    End Sub

    Private Async Sub onProforma(sender As Object, e As MatEventArgs)
        If e.Argument > "" Then
            Dim obs() As String = e.Argument.Split(vbCrLf)
            Dim oPurchaseOrder As DTOPurchaseOrder = _PurchaseOrders.First
            oPurchaseOrder.proformaObs = New List(Of String)
            For Each ob As String In obs
                If ob <> vbLf Then
                    oPurchaseOrder.proformaObs.Add(ob.Replace(vbLf, ""))
                End If
            Next
        End If
        Await BrowsePdf(True)
    End Sub

    Private Sub Do_ProformaPdfExport(ByVal sender As Object, ByVal e As System.EventArgs)
        SavePdf(True)
    End Sub


    Private Sub Do_PdcHtml(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB.PurchaseOrder.Url(_PurchaseOrder, True)
        Process.Start(sUrl)
    End Sub

    Private Async Sub Do_PdcEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await Email(False, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String
        If _PurchaseOrders.Count = 1 Then
            sUrl = FEB.PurchaseOrder.Url(_PurchaseOrder, True)
        Else
            Dim sb As New Text.StringBuilder
            For Each item In _PurchaseOrders
                sb.AppendLine(item.FullNomAndCaption())
                sb.AppendLine("    " & FEB.PurchaseOrder.Url(item, True))
                sb.AppendLine()
            Next
            sUrl = sb.ToString
        End If

        Clipboard.SetText(sUrl)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Do_CopyLinkToDoc(ByVal sender As Object, ByVal e As System.EventArgs)

        If _PurchaseOrder.docFile IsNot Nothing Then
            Dim sUrl As String = FEB.DocFile.DownloadUrl(_PurchaseOrder.docFile, True)
            Clipboard.SetDataObject(sUrl, True)
        Else
            MsgBox("no hi ha cap document assignat a aquesta comanda", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Async Sub Do_ProformaEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await Email(False, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Do_CopyTpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB.PurchaseOrder.UrlTpv(_PurchaseOrder, True)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = String.Format("Eliminem {0} {1}?", IIf(_PurchaseOrders.Count > 1, "comandes", "comanda"), DTOPurchaseOrder.Nums(_PurchaseOrders))

        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.PurchaseOrders.Delete(exs, _PurchaseOrders) Then
                MsgBox("Comandes eliminades", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, MatEventArgs.Empty)
            Else
                MsgBox("error al eliminar les comandes" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub



    Private Async Sub Do_DocumentImportar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sTitle As String = "Importar document"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            If FEB.PurchaseOrder.Load(exs, _PurchaseOrder) Then
                _PurchaseOrder.docFile = oDocFile
                Dim pOrder = Await FEB.PurchaseOrder.Update(exs, _PurchaseOrder)
                If exs.Count = 0 Then
                    _PurchaseOrder = pOrder
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                Else
                    UIHelper.WarnError(exs, "error al desar el document")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "error al importar el document")
        End If

    End Sub

    Private Async Sub Do_DocumentZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, _PurchaseOrder.docFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Alb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _PurchaseOrder.contact, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oCustomer As DTOCustomer = _PurchaseOrder.customer
            If oCustomer Is Nothing Then oCustomer = DTOCustomer.fromContact(_PurchaseOrder.contact)
            Dim oDelivery = FEB.Delivery.Factory(exs, oCustomer, Current.Session.User, GlobalVariables.Emp.Mgz)
            Dim oFrm As New Frm_AlbNew2(oDelivery)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(_PurchaseOrder, exs) Then
            Dim oClon = DTOPurchaseOrder.clon(_PurchaseOrder)
            oClon.UsrLog = DTOUsrLog.Factory(Current.Session.User)

            Select Case oClon.cod
                Case DTOPurchaseOrder.Codis.proveidor
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oClon.contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder_Proveidor(oClon)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Case DTOPurchaseOrder.Codis.client

                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oClon.contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oClon)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Else
                    MsgBox("no implementat per aquest tipus de comanda")
            End Select
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_Edi_Export(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim src As String = FEB.EdiversaOrder.EdiMessage(Current.Session.Emp, _PurchaseOrder, exs)
        If exs.Count = 0 Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .FileName = String.Format("Edi Order {0} {1:yyMMddHHmm}.txt", _PurchaseOrder.Num, DTO.GlobalVariables.Now())
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If FileSystemHelper.SaveTextToFile(src, .FileName, exs) Then
                        RefreshRequest(Me, MatEventArgs.Empty)
                    Else
                        UIHelper.WarnError(exs, "Error al desar el fitxer")
                    End If
                End If
            End With
        Else
            UIHelper.WarnError(exs, "Error al redactar missatge Edi")
        End If
    End Sub

    Private Async Sub Do_Edi_Queue(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
        For Each oPurchaseOrder In _PurchaseOrders
            Dim exs2 As New List(Of Exception)
            Dim oEdiFile = FEB.EdiversaOrder.EdiFile(Current.Session.Emp, oPurchaseOrder, exs2)
            If exs.Count = 0 Then
                If oEdiFile IsNot Nothing Then
                    If Not Await FEB.EdiversaFile.Update(exs2, oEdiFile) Then
                        'verificar si src es Edi i si no grabar-li
                        'If Not FEB.EdiversaFile.Update(exs, oEdiFile) Then
                    End If
                End If
            End If

            If exs2.Count > 0 Then
                exs.Add(New Exception(String.Format("error al desar el fitxer Edi de la comanda {0}", oPurchaseOrder.formattedId)))
                exs.AddRange(exs2)
            End If
        Next

        RefreshRequest(Me, MatEventArgs.Empty)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_Edi_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, _PurchaseOrder) Then
            Dim src As String = FEB.EdiversaOrder.EdiMessage(Current.Session.Emp, _PurchaseOrder, exs)

            If exs.Count > 0 Then
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("=======================================")
                sb.AppendLine("fitxer no validat (" & exs.Count.ToString & " errors detectats)")
                sb.AppendLine("=======================================")
                For Each oErr As Exception In exs
                    sb.AppendLine(oErr.Message)
                Next
                sb.AppendLine("=======================================")
                sb.AppendLine()
                src = sb.ToString
            End If

            root.ShowLiteral("EDI comanda " & DTOPurchaseOrder.formattedId(_PurchaseOrder) & " " & _PurchaseOrder.contact.nomComercialOrDefault(), src)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_EtiquetesTransport()
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, _PurchaseOrders.First.etiquetesTransport) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_GenerateOrderToSuplier()
        Dim exs As New List(Of Exception)
        For Each oCustomerOrder As DTOPurchaseOrder In _PurchaseOrders
            FEB.PurchaseOrder.Load(exs, oCustomerOrder)
        Next

        Dim oProveidors = _PurchaseOrders.SelectMany(Function(x) x.items).GroupBy(Function(y) DTOProduct.proveidor(y.sku).Guid).Select(Function(z) z.First).Select(Function(q) DTOProduct.proveidor(q.sku)).ToList
        For Each oProveidor In oProveidors
            If FEB.Proveidor.Load(oProveidor, exs) Then
                Dim oSuplierOrder = DTOPurchaseOrder.Factory(GlobalVariables.Emp, oProveidor, Current.Session.User)
                For Each oCustomerOrder In _PurchaseOrders
                    For Each customerItem In oCustomerOrder.items
                        If oProveidor.Equals(DTOProduct.proveidor(customerItem.sku)) Then
                            Dim oSku As DTOProductSku = customerItem.sku
                            Dim suplierItem = oSuplierOrder.items.FirstOrDefault(Function(x) x.sku.Equals(oSku))
                            If suplierItem Is Nothing Then
                                Dim oCostItem = Await FEB.ProductSku.Cost(exs, oSku)
                                Dim oCostPrice As DTOAmt = DTOAmt.Factory(oCostItem.Parent.Cur, oCostItem.Price)
                                suplierItem = DTOPurchaseOrderItem.Factory(oSuplierOrder, oSku, 0, oCostPrice, oCostItem.Parent.Discount_OnInvoice)
                                oSuplierOrder.items.Add(suplierItem)
                            End If
                            suplierItem.qty += customerItem.qty
                            suplierItem.pending = suplierItem.qty
                        End If
                    Next
                Next

                Dim oFrm As New Frm_PurchaseOrder_Proveidor(oSuplierOrder)
                oFrm.Show()
            End If
        Next

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_RecuperaLiniesDeSortides(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim iCount As Integer = Await FEB.PurchaseOrderItems.RecuperaLiniesDeSortides(exs, _PurchaseOrder)
        If exs.Count = 0 Then
            If iCount = 0 Then
                MsgBox("no s'han trobat sortides d'aquesta comanda", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                MsgBox("S'han recuperat " & iCount.ToString & " linies de comanda" & vbCrLf & "revisar preus i descomptes especialment en aquelles linies que hagin sortit en mes de un albará i poguessin tenir preus diferents")
            End If

            RefreshRequest(sender, e)
        Else
            UIHelper.WarnError(exs, "error al recuperar linies de comanda")
        End If
    End Sub


    Private Async Sub Do_NoRep(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, _PurchaseOrder) Then
            For Each item In _PurchaseOrder.items
                item.repCom.Clear()
            Next

            Await FEB.PurchaseOrder.Update(exs, _PurchaseOrder)
            If exs.Count = 0 Then
                MsgBox("retirada la comisió d'aquesta comanda", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_RemovePromo()
        Dim exs As New List(Of Exception)
        If Await FEB.PurchaseOrder.RemovePromo(exs, _PurchaseOrder) Then
            MsgBox("Promo retirada", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailJustificant()
        Dim exs As New List(Of Exception)
        Dim oMailMessage = DTOPurchaseOrder.mailMessageRepConfirmation(_PurchaseOrder)
        If Not Await OutlookHelper.Send(oMailMessage, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BlockPendingItems()
        Dim exs As New List(Of Exception)
        _PurchaseOrder = Await FEB.PurchaseOrder.Find(_PurchaseOrder.Guid, exs)
        If exs.Count = 0 Then
            If Await FEB.PurchaseOrder.BlockPendingItems(exs, _PurchaseOrder) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
            Else
                UIHelper.WarnError(exs, "Error al bloquejar els productes que han quedat pendents")
            End If
        Else
            UIHelper.WarnError(exs, "Erroir al llegir la comanda")
        End If
    End Sub

    Private Sub Do_ConfirmacioEdiSave()
        Dim exs As New List(Of Exception)
        Dim warnMsg As String = ""
        If FEB.PurchaseOrder.Load(_PurchaseOrder, exs) Then
            Dim oEdiOrdrsp = DTOEdiOrdrsp.Factory(_PurchaseOrder, GlobalVariables.Emp.Org, DTO.GlobalVariables.Today(), DTO.GlobalVariables.Today())
            Dim oDlg As New SaveFileDialog
            With oDlg
                .FileName = String.Format("ORDRSP {0} {1:yyMMddHHmm}.txt", _PurchaseOrder.Num, DTO.GlobalVariables.Now())
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If FileSystemHelper.SaveTextToFile(oEdiOrdrsp.Stream(warnMsg), .FileName, exs) Then
                        If (warnMsg.Length > 0) Then MsgBox(warnMsg, MsgBoxStyle.Exclamation)
                        RefreshRequest(Me, MatEventArgs.Empty)
                    Else
                        UIHelper.WarnError(exs, "Error al desar el fitxer")
                    End If
                End If
            End With

        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ConfirmacioEdiFtp()
        Dim exs As New List(Of Exception)
        Dim warnMsg As String = ""
        If FEB.PurchaseOrder.Load(_PurchaseOrder, exs) Then
            Dim oEdiOrdrsp = DTOEdiOrdrsp.Factory(_PurchaseOrder, GlobalVariables.Emp.Org, DTO.GlobalVariables.Today(), DTO.GlobalVariables.Today())
            Dim OrdrSpBytes = oEdiOrdrsp.ByteArray(warnMsg)
            Dim rc As MsgBoxResult = MsgBoxResult.Ok
            If warnMsg.Length > 0 Then rc = MsgBox(warnMsg, MsgBoxStyle.OkCancel)
            If rc = MsgBoxResult.Ok Then
                Dim msg = Await FEB.Ftpserver.Send(exs, _PurchaseOrder.Customer, DTOFtpserver.Path.Cods.Ordrsp, OrdrSpBytes, oEdiOrdrsp.DefaultFileName)
                If exs.Count = 0 Then
                    MsgBox("fitxer pujat satisfactoriament al Ftp" & vbCrLf & msg)
                Else
                    UIHelper.WarnError(exs, msg)
                End If
            Else
                MsgBox("Missatge Edi cancel·lat per l'usuari", MsgBoxStyle.Information)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    '==========================================================================
    '                               AUXILIARS
    '==========================================================================

    Private Async Function Email(ByVal BlProforma As Boolean, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oFirstPurchaseOrder = _PurchaseOrders.First
        Dim oFirstContact = oFirstPurchaseOrder.Contact
        If FEB.Contact.Load(oFirstContact, exs) Then
            Dim sameCustomers As Boolean = _PurchaseOrders.All(Function(x) x.Contact.Equals(oFirstContact))
            If sameCustomers Then
                Dim oEmails = Await FEB.Emails.All(exs, oFirstContact)
                Dim sRecipients = oEmails.Select(Function(x) x.EmailAddress).ToList
                Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
                Dim sAttachmentFilename = GetTmpPdfFile(BlProforma)
                With oMailMessage
                    .subject = IIf(_PurchaseOrders.Count = 1, DTOPurchaseOrder.FullConcepte(_PurchaseOrders.First, oFirstContact.lang), DTOPurchaseOrder.Nums(_PurchaseOrders))
                    .AddAttachment(sAttachmentFilename, DTOPurchaseOrder.PdfFilename(_PurchaseOrders, oFirstContact.Lang))
                End With

                Await OutlookHelper.Send(oMailMessage, exs)
            Else
                exs.Add(New Exception("s'han sel·leccionat comandes de diferents clients"))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function

    Private Async Function BrowsePdf(ByVal BlProforma As Boolean) As Task
        Dim exs As New List(Of Exception)
        If _PurchaseOrders.Count = 1 Then
            Dim oPurchaseOrder As DTOPurchaseOrder = _PurchaseOrders.First
            If FEB.PurchaseOrder.Load(exs, oPurchaseOrder) Then
                Dim sFilename As String = DTOPurchaseOrder.filename(oPurchaseOrder, MimeCods.Pdf)
                Dim BlSigned As Boolean = (oPurchaseOrder.Cod = DTOPurchaseOrder.Codis.proveidor)

                Dim oPurchaseOrders As New List(Of DTOPurchaseOrder)
                oPurchaseOrders.Add(oPurchaseOrder)
                Dim oPdf As New LegacyHelper.PdfAlb2(oPurchaseOrders, BlSigned, BlProforma)
                Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)

                Dim oDoc = FEB.PurchaseOrder.Doc(oPurchaseOrder, BlProforma)
                oPdf.PrintDoc(oDoc)

                Dim oStream = oPdf.Stream(exs, oCert)
                If exs.Count = 0 Then
                    UIHelper.ShowPdf(oStream, sFilename)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If

        End If
        'Dim sFileName As String = GetTmpPdfFile(BlProforma)
        'root.ShowPdf(sFilename)
    End Function

    Private Function GetTmpPdfFile(ByVal BlProforma As Boolean) As String
        Dim sFileName As String = "M+O " & DTOPurchaseOrder.FormattedId(_PurchaseOrder) & ".pdf"

        Dim exs As New List(Of Exception)
        Dim oPdf As New LegacyHelper.PdfAlb2(_PurchaseOrders, False, BlProforma)
        For Each oPurchaseOrder As DTOPurchaseOrder In _PurchaseOrders
            Dim oDoc = FEB.PurchaseOrder.Doc(oPurchaseOrder, BlProforma)
            oPdf.PrintDoc(oDoc)
        Next

        Dim oStream = oPdf.Stream(exs)
        If exs.Count = 0 Then
            If Not FileSystemHelper.SaveStream(oStream, exs, sFileName) Then
                UIHelper.WarnError(exs, "error al desar el document")
            End If
        Else
            UIHelper.WarnError(exs, "error al generar els pdfs")
        End If

        Return sFileName
    End Function

    Private Function SavePdf(ByVal BlProforma As Boolean) As Boolean
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = DTOPurchaseOrder.PdfFilename(_PurchaseOrders)
        Dim sTit As String = IIf(BlProforma, "PROFORMES", "COMANDES")
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR " & sTit
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                Dim exs As New List(Of Exception)
                Dim oPdf As New LegacyHelper.PdfAlb2(_PurchaseOrders, False, BlProforma)

                Dim oStream = oPdf.Stream(exs)
                If Not FileSystemHelper.SaveStream(oStream, exs, .FileName) Then
                    UIHelper.WarnError(exs, "error al desar el document")
                End If
            End If
        End With
        Return True
    End Function

    Private Async Sub Do_RedactaPdf()
        Dim exs As New List(Of Exception)
        For Each oPurchaseOrder In _PurchaseOrders
            If FEB.PurchaseOrder.Load(exs, oPurchaseOrder) Then
                Dim tmp As New List(Of DTOPurchaseOrder)
                tmp.Add(oPurchaseOrder)
                Dim oPdf As New LegacyHelper.PdfAlb2(tmp)
                For Each oOrder In tmp
                    Dim oDoc = FEB.PurchaseOrder.Doc(oOrder, False)
                    oPdf.PrintDoc(oDoc)
                Next
                Dim oByteArray As Byte()
                If oPurchaseOrder.cod = DTOPurchaseOrder.Codis.proveidor Then
                    Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)
                    oByteArray = oPdf.Stream(exs, oCert)
                Else
                    oByteArray = oPdf.Stream(exs)
                End If
                If exs.Count = 0 Then
                    Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, MimeCods.Pdf)
                    If exs.Count = 0 Then
                        oPurchaseOrder.docFile = oDocFile
                        Await FEB.PurchaseOrder.Update(exs, oPurchaseOrder)
                        If exs.Count = 0 Then
                            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                        Else
                            UIHelper.WarnError(exs, "error al redactar el document")
                        End If
                    Else
                        UIHelper.WarnError(exs, "error al redactar el document")
                    End If
                Else
                    UIHelper.WarnError(exs, "error al redactar el document")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Next
    End Sub


    Private Async Sub Do_RecalculaPendents(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim iCount = Await FEB.PurchaseOrder.RecalculaPendents(exs, _PurchaseOrder)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            MsgBox(iCount & " linies actualitzades", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_DeletePendingItems()
        Dim rc As MsgBoxResult
        If _PurchaseOrders.Count = 1 Then
            rc = MsgBox("Eliminem totes les quantitats pendents d'entrega d'aquesta comanda?", MsgBoxStyle.OkCancel)
        Else
            rc = MsgBox("Eliminem totes les quantitats pendents d'entrega d'aquestes " & _PurchaseOrders.Count & " comandes?", MsgBoxStyle.OkCancel)
        End If
        If rc = MsgBoxResult.Ok Then
            Dim oFrm As New Frm_Progress("elimina pendents", "Elimina les quantitats pendents de entrega de " & _PurchaseOrders.Count & " comandes")
            oFrm.SetStart()
            oFrm.Show()

            Dim exs As New List(Of Exception)
            If Await FEB.PurchaseOrders.DeletePendingItems(_PurchaseOrders, AddressOf oFrm.ShowProgress, exs) Then
                oFrm.SetEnd("quantitats pendents eliminades")
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar linies de comanda")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub



    Private Sub Do_Excel()
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(_PurchaseOrder, exs) Then
            If FEB.Contact.Load(_PurchaseOrder.Contact, exs) Then
                Dim oSheet As MatHelper.Excel.Sheet = FEB.PurchaseOrder.Excel(_PurchaseOrder)
                If Not UIHelper.ShowExcel(oSheet, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Copy_CompactCustomerPO()
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, _PurchaseOrder) Then
            Dim oPO = DTOCompactCustomerPO.Factory(_PurchaseOrder)
            Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim sJson As String = jss.Serialize(oPO)

            UIHelper.CopyToClipboard(sJson)
        End If
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