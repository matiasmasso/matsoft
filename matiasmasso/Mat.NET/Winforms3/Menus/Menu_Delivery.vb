Imports DTO.Integracions
Imports FEB
'Imports FEB2

Public Class Menu_Delivery
    Inherits Menu_Base

    Private _Deliveries As List(Of DTODelivery)
    Private Event RequestToRemoveFromTransmisio(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oDeliveries As IEnumerable(Of DTODelivery))
        MyBase.New()
        _Deliveries = oDeliveries
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Pdf(),
        MenuItem_CopyLink(),
        MenuItem_Email(),
        MenuItem_Confirmation(),
        MenuItem_EmailConfirmationRequest(),
        MenuItem_EmailSubscriptors(),
        MenuItem_Proforma(),
        MenuItem_Excel(),
        MenuItem_Tpv(),
        MenuItem_SeguimentTransportista(),
        MenuItem_ShowRepComs(),
        MenuItem_Facturar(),
        MenuItem_Justificante(),
        MenuItem_Cobrar(),
        MenuItem_ConsumerTicket(),
        MenuItem_Recollida(),
        MenuItem_Avançats(),
        MenuItem_Del()})
    End Function
    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If _Deliveries.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.DropDownItems.Add("Valoració segons fitxa client", Nothing, AddressOf Do_PdfDefault)
        oMenuItem.DropDownItems.Add("Valorat", Nothing, AddressOf Do_PdfValorat)
        oMenuItem.DropDownItems.Add("Sense valorar", Nothing, AddressOf Do_PdfNoValorat)
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Enabled = _Deliveries.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email..."
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_AlbEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_Confirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Confirmacions"
        With oMenuItem.DropDownItems
            .Add(MenuItem_EmailConfirmationRequest)
            .Add(MenuItem_Ordrsp_Ftp)
            .Add(MenuItem_Ordrsp_Save)
            .Add(MenuItem_Desadv_Ftp)
            .Add(MenuItem_Desadv_Save)
            .Add(MenuItem_Ediversa_Save)
            .Add(MenuItem_Desadv_Ediversa_Send)
        End With
        Return oMenuItem
    End Function


    Private Function MenuItem_EmailConfirmationRequest() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email confirmació enviament"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_EmailConfirmationRequest
        Return oMenuItem
    End Function

    Private Function MenuItem_Ordrsp_Ftp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Confirmació comandes per Ftp"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_Ordrsp_Ftp
        Return oMenuItem
    End Function

    Private Function MenuItem_Ordrsp_Save() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar Confirmació comandes Edi"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Ordrsp_Save
        Return oMenuItem
    End Function

    Private Function MenuItem_Desadv_Ftp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar missatge Desadv per Ftp"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_Desadv_Ftp
        Return oMenuItem
    End Function

    Private Function MenuItem_Desadv_Save() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar missatge Desadv (Edi)"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Desadv_Save
        Return oMenuItem
    End Function

    Private Function MenuItem_Ediversa_Save() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar missatge Desadv (Ediversa)"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf DesadvEdiversa_Save
        Return oMenuItem
    End Function

    Private Function MenuItem_Desadv_Ediversa_Send() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar missatge Desadv (Ediversa)"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_DesadvEdiversa_Send
        Return oMenuItem
    End Function


    Private Function MenuItem_EmailSubscriptors() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email a subscriptors"
        oMenuItem.Enabled = False
        Try
            If _Deliveries.Count = 1 Then
                oMenuItem.Enabled = True
            End If
        Catch ex As Exception
        End Try

        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_AlbEmailSubscriptors
        Return oMenuItem
    End Function

    Private Function MenuItem_Proforma() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proforma"
        With oMenuItem.DropDownItems
            .Add(MenuItem_ProformaPdf)
            .Add(MenuItem_ProformaEmail)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function


    Private Function MenuItem_ProformaPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaEmail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_ProformaEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_Tpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a Tpv"
        'oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_CopyTpv
        Return oMenuItem
    End Function


    Private Function MenuItem_SeguimentTransportista() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "seguiment transportista"
        AddHandler oMenuItem.Click, AddressOf Do_SeguimentTransportista
        Return oMenuItem
    End Function


    Private Function MenuItem_ShowRepComs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "comisions"
        oMenuItem.Visible = Current.Session.User.Rol.isMainboard
        AddHandler oMenuItem.Click, AddressOf Do_ShowRepComs
        Return oMenuItem
    End Function

    Private Function MenuItem_Facturar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Facturar"
        oMenuItem.Image = My.Resources.Gears
        oMenuItem.Enabled = _Deliveries.All(Function(x) x.Invoice Is Nothing)
        AddHandler oMenuItem.Click, AddressOf Do_Factura
        Return oMenuItem
    End Function


    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.DelText
        oMenuItem.Enabled = _Deliveries.All(Function(x) x.Invoice Is Nothing And x.Transmisio Is Nothing)
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Justificante() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "justificante"
        With oMenuItem.DropDownItems
            .Add(MenuItem_JustificanteNone)
            .Add(MenuItem_JustificanteSolicitado)
            .Add(MenuItem_JustificanteRecibido)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Cobrar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case _Deliveries.First.CashCod
            Case DTOCustomer.CashCodes.transferenciaPrevia, DTOCustomer.CashCodes.visa
                oMenuItem.Text = "cobrar transf.previa"
                oMenuItem.Image = My.Resources.DollarOrange2
                oMenuItem.Enabled = _Deliveries.Count = 1
                AddHandler oMenuItem.Click, AddressOf Do_TransferenciaPrevia
            Case Else
                oMenuItem.Visible = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_ConsumerTicket() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "genera ticket consumidor"
        AddHandler oMenuItem.Click, AddressOf Do_ConsumerTicket
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteNone() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "no disponible"
        oMenuItem.Checked = _Deliveries.First.Justificante = DTODelivery.JustificanteCodes.none
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteNone
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteSolicitado() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "sol.licitat"
        oMenuItem.Checked = _Deliveries.First.Justificante = DTODelivery.JustificanteCodes.solicitado
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteSolicitado
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteRecibido() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "rebut"
        oMenuItem.Checked = _Deliveries.First.Justificante = DTODelivery.JustificanteCodes.recibido
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteRecibido
        Return oMenuItem
    End Function

    Private Function MenuItem_Recollida() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If _Deliveries.First.Cod = DTOPurchaseOrder.Codis.client Then
            oMenuItem.Text = "recollida"
            oMenuItem.Enabled = _Deliveries.Count = 1
            AddHandler oMenuItem.Click, AddressOf Do_Recollida
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Avançats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats..."
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.superUser
                oMenuItem.DropDownItems.Add(MenuItem_Abona)
                oMenuItem.DropDownItems.Add(MenuItem_RetrocedePedido)
                oMenuItem.DropDownItems.Add(MenuItem_RemoveFromTransmission)
                oMenuItem.DropDownItems.Add(MenuItem_CurExchange)
                oMenuItem.DropDownItems.Add(MenuItem_Edi)
                oMenuItem.DropDownItems.Add("Copiar link Tpv", Nothing, AddressOf Do_CopyLinkTpv)
            Case DTORol.Ids.admin
                oMenuItem.DropDownItems.Add(MenuItem_RetrocedePedido)
                oMenuItem.DropDownItems.Add(MenuItem_RemoveFromTransmission)
                oMenuItem.DropDownItems.Add(MenuItem_CurExchange)
            Case DTORol.Ids.admin, DTORol.Ids.accounts
                oMenuItem.DropDownItems.Add(MenuItem_RetrocedePedido)
                oMenuItem.DropDownItems.Add(MenuItem_CurExchange)
        End Select
        oMenuItem.DropDownItems.Add(MenuItem_ExcelSkuSummary)
        oMenuItem.DropDownItems.Add(MenuItem_SonaeLabels)
        oMenuItem.DropDownItems.Add(MenuItem_EroskiLabels)
        oMenuItem.DropDownItems.Add(MenuItem_CarrefourLabels)
        Return oMenuItem
    End Function

    Private Function MenuItem_Edi() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Edi Desadv natiu"
        AddHandler oMenuItem.Click, AddressOf Do_Desadv
        Return oMenuItem
    End Function

    Private Function MenuItem_RetrocedePedido() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "retrocedeix comandes"
        AddHandler oMenuItem.Click, AddressOf Do_retrocedePedidoPrepare
        Return oMenuItem
    End Function

    Private Function MenuItem_RemoveFromTransmission() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If _Deliveries.First.Transmisio Is Nothing Then
            oMenuItem.Text = Current.Session.Tradueix("asignar a una transmisión", "assignar a una transmisió", "assing to a transmission")
            AddHandler oMenuItem.Click, AddressOf Do_AssignToATransmisio
        Else
            Dim id As String = ""
            id = _Deliveries.First.Transmisio.Id
            oMenuItem.Text = Current.Session.Tradueix("retira de la transmisión ", "retira de la transmisió ", "remove from transmission") & id
            AddHandler oMenuItem.Click, AddressOf Do_RemoveFromTransmission
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Abona() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "abona"
        oMenuItem.Enabled = _Deliveries.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Abona
        Return oMenuItem
    End Function

    Private Function MenuItem_CurExchange() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "aplica canvi de divisa"
        oMenuItem.Enabled = _Deliveries.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CurExchange
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelSkuSummary() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel resum de productes i quantitats"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelSkuSummary
        Return oMenuItem

    End Function
    Private Function MenuItem_SonaeLabels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "etiquetes Sonae"
        oMenuItem.Enabled = _Deliveries.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_PdfLogisticLabelSonae
        Return oMenuItem
    End Function
    Private Function MenuItem_EroskiLabels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "etiquetes Eroski"
        oMenuItem.Enabled = _Deliveries.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_PdfLogisticLabelEroski
        Return oMenuItem
    End Function

    '

    Private Function MenuItem_CarrefourLabels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "etiquetes Carrefour"
        AddHandler oMenuItem.Click, AddressOf Do_PdfLogisticLabelCarrefour
        Return oMenuItem
    End Function

    Private Sub Do_CurExchange()
        Dim oFrm As New Frm_CurExchange(_Deliveries.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Abona(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oAbono = FEB.Delivery.Abonar(exs, _Deliveries.First, Current.Session.User, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Dim id As Integer = Await FEB.Delivery.Update(exs, oAbono)
            If exs.Count = 0 Then
                oAbono.Id = id
                MsgBox(String.Format("Abonament registrat amb el num.", oAbono.Id))
                MyBase.RefreshRequest(Me, New MatEventArgs(oAbono))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ExcelSkuSummary()
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim items As New List(Of DTODeliveryItem)
        Dim pDeliveries As New List(Of DTODelivery)
        For Each oDelivery In _Deliveries
            Dim pDelivery = Await FEB.Delivery.Find(oDelivery.Guid, exs)
            pDeliveries.Add(pDelivery)
            If exs.Count = 0 Then
                items.AddRange(pDelivery.Items)
            Else
                MyBase.ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        Next

        Dim query = items.GroupBy(Function(g) New With {Key g.Sku.Id, Key g.Sku.RefYNomLlarg.Esp}).
            Select(Function(group) New With {.Id = group.Key.Id, .Nom = group.Key.Esp, .Qty = group.Sum(Function(x) x.Qty)})

        Dim oBook As New MatHelper.Excel.Book("Resum albarans")
        Dim oSheet As New MatHelper.Excel.Sheet("Quantitats de producte")
        oBook.Sheets.Add(oSheet)
        oSheet.AddColumn("Unitats", MatHelper.Excel.Cell.NumberFormats.Integer)
        oSheet.AddColumn("Referencia", MatHelper.Excel.Cell.NumberFormats.Integer)
        oSheet.AddColumn("Descripcio", MatHelper.Excel.Cell.NumberFormats.PlainText)
        For Each o In query
            Dim oRow = oSheet.AddRow()
            oRow.AddCell(o.Qty)
            oRow.AddCell(o.Id)
            oRow.AddCell(o.Nom)
        Next

        oSheet = New MatHelper.Excel.Sheet("albarans")
        oSheet.AddColumn("Albarà", MatHelper.Excel.Cell.NumberFormats.Integer)
        oSheet.AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
        oSheet.AddColumn("Destinacio", MatHelper.Excel.Cell.NumberFormats.PlainText)
        oSheet.AddColumn("Import", MatHelper.Excel.Cell.NumberFormats.Euro)
        For Each alb In pDeliveries
            Dim oRow = oSheet.AddRow()
            oRow.AddCell(alb.Id)
            oRow.AddCell(alb.Fch)
            oRow.AddCell(alb.Customer.FullNom)
            oRow.AddCell(alb.Import.Eur)
        Next
        oBook.Sheets.Add(oSheet)
        If UIHelper.ShowExcel(oBook, exs) Then
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_PdfLogisticLabelEroski()
        Dim exs As New List(Of Exception)
        If FEB.Delivery.Load(_Deliveries.First, exs) Then
            If exs.Count = 0 Then
                FEB.Delivery.LoadCustSkuRefs(_Deliveries.First, exs)
                If exs.Count = 0 Then
                    Dim oPdf As New LegacyHelper.PdfLogisticLabelEroski(_Deliveries.First)
                    Dim oByteArray() As Byte = oPdf.Stream
                    Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray)
                    If oPdf.exs.Count = 0 Then
                        If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(oPdf.exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "No s'han pogut carregar les dades de l'albarà")
        End If
    End Sub

    Private Async Sub Do_PdfLogisticLabelSonae()
        Dim exs As New List(Of Exception)
        If FEB.Delivery.Load(_Deliveries.First, exs) Then
            Dim oAllCountries = Await FEB.Countries.All(Current.Session.Lang, exs)
            If exs.Count = 0 Then
                Dim oPncGuids = _Deliveries.First.Items.Where(Function(x) x.PurchaseOrderItem IsNot Nothing).Select(Function(x) x.PurchaseOrderItem.Guid).ToList()
                Dim oEdiSkus As List(Of DTOEdiSku) = Await FEB.Delivery.LoadEdiSkus(exs, oPncGuids)
                If exs.Count = 0 Then

                    For Each item As DTODeliveryItem In _Deliveries.First.Items.Where(Function(x) x.PurchaseOrderItem IsNot Nothing).ToList()
                        Dim oEdiSku = oEdiSkus.FirstOrDefault(Function(x) x.PncGuid = item.PurchaseOrderItem.Guid)
                        If oEdiSku Is Nothing Then
                            exs.Add(New Exception(String.Format("No s'ha trobat la linia de {0} linia {1} al fitxer Edi de la comanda", item.Sku.NomLlarg, item.Lin)))
                        ElseIf String.IsNullOrEmpty(oEdiSku.Ref) Then
                            exs.Add(New Exception(String.Format("No s'ha trobat la ref. de {0} linia {1} al fitxer Edi de la comanda", item.Sku.NomLlarg, item.Lin)))
                        ElseIf String.IsNullOrEmpty(oEdiSku.Ean) Then
                            exs.Add(New Exception(String.Format("No s'ha trobat la el codi Ean de {0} linia {1} al fitxer Edi de la comanda", item.Sku.NomLlarg, item.Lin)))
                        Else
                            item.Sku.RefCustomer = oEdiSku.Ref
                            item.Sku.Ean13 = New DTOEan(oEdiSku.Ean)
                        End If
                    Next

                    Dim oPdf As New LegacyHelper.PdfLogisticLabelSonae(_Deliveries.First, oAllCountries)
                    Dim oByteArray() As Byte = oPdf.Stream
                    Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray)
                    If oPdf.exs.Count = 0 Then
                        If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(oPdf.exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "No s'han pogut carregar les dades de l'albarà")
        End If
    End Sub

    Private Async Sub Do_PdfLogisticLabelCarrefour()
        Dim exs As New List(Of Exception)
        Dim oCountries = Await FEB.Countries.All(Current.Session.Lang, exs)
        Dim items As List(Of DTOCarrefourItem) = FEB.Carrefour.LogisticItems(_Deliveries, oCountries)
        Dim oFrm As New Frm_CarrefourLogisticLabels(items)
        oFrm.Show()
    End Sub

    Private Async Sub Do_JustificanteNone()
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Deliveries.First
        If Await FEB.Delivery.UpdateJustificante(exs, oDelivery, DTODelivery.JustificanteCodes.none) Then
            Me.RefreshRequest(Me, New MatEventArgs(oDelivery))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_JustificanteSolicitado()
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Deliveries.First
        If Await FEB.Delivery.UpdateJustificante(exs, oDelivery, DTODelivery.JustificanteCodes.solicitado) Then
            Me.RefreshRequest(Me, New MatEventArgs(oDelivery))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_JustificanteRecibido()
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Deliveries.First
        If Await FEB.Delivery.UpdateJustificante(exs, oDelivery, DTODelivery.JustificanteCodes.recibido) Then
            Me.RefreshRequest(Me, New MatEventArgs(oDelivery))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_TransferenciaPrevia()
        Dim oDelivery As DTODelivery = _Deliveries.First
        Dim oFrm As New Frm_Cobrament_TransferenciaPrevia(oDelivery)
        AddHandler oFrm.AfterUpdate, AddressOf AfterCobroTransferencia
        oFrm.Show()
    End Sub

    Private Sub Do_ConsumerTicket()
        Dim exs As New List(Of Exception)
        FEB.Delivery.Load(_Deliveries.First, exs)
        If exs.Count = 0 Then
            Dim oConsumerTicket = DTOConsumerTicket.Factory(Current.Session.User)
            With oConsumerTicket
                .Delivery = _Deliveries.First
                Select Case .Delivery.Cod
                    Case DTOPurchaseOrder.Codis.reparacio
                        exs.Add(New Exception("No implementat encara per reparacions"))
                    Case DTOPurchaseOrder.Codis.client
                        Dim oPurchaseOrders = .Delivery.getPurchaseOrders()
                        If oPurchaseOrders.Count = 1 Then
                            .PurchaseOrder = oPurchaseOrders.First
                            FEB.PurchaseOrder.Load(.PurchaseOrder, exs)
                            If exs.Count = 0 Then
                                Dim oFrm As New Frm_ConsumerTicketFactory(oConsumerTicket)
                                oFrm.Show()
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Else
                            exs.Add(New Exception("L'albarà nomes pot tenir una comanda"))
                        End If
                    Case Else
                        exs.Add(New Exception("No implementat encara per aquest tipus d'albarà"))
                End Select
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub AfterCobroTransferencia(ByVal sender As Object, ByVal e As MatEventArgs)
        'Dim oFrm As Frm_Cobrament_TransferenciaPrevia = sender
        Dim oCca As DTOCca = e.Argument
        Dim sLogText As String = oCca.UsrLog.text

        Dim exs As New List(Of Exception)
        Dim oMailMessage = DTOMailMessage.Factory(GlobalVariables.Emp.MailboxUsr, Subject:=oCca.Concept, Body:=sLogText & vbCrLf & "Missatge automatic enviat quan es registra el pagament de un albará per transferencia previa")
        If Await FEB.MailMessage.Send(exs, Current.Session.User, oMailMessage) Then
            RefreshRequest(Me, New MatEventArgs(_Deliveries.First))
            MsgBox(String.Format("albarà cobrat (registre {0})", oCca.Id))
        Else
            RefreshRequest(Me, New MatEventArgs(_Deliveries.First))
            UIHelper.WarnError(exs, "Cobrament registrat correctament pero no s'ha pogut avisar a info")
        End If
    End Sub

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            Dim oContact As DTOContact = oDelivery.Contact

            If FEB.Contact.Load(oContact, exs) Then
                'Dim oCustomer As DTOCustomer = oDelivery.Contact
                If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oDelivery.Contact, DTOAlbBloqueig.Codis.ALB, exs) Then
                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub Do_PdfDefault(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oByteArray As Byte() = Await FEB.Deliveries.Pdf(exs, _Deliveries, False, DTODelivery.CodsValorat.Inherit)
        If exs.Count = 0 Then
            Dim sFilename = DTODelivery.FileName(_Deliveries)
            UIHelper.ShowPdf(oByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_PdfValorat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oByteArray As Byte() = Await FEB.Deliveries.Pdf(exs, _Deliveries, False, DTODelivery.CodsValorat.ForceTrue)
        If exs.Count = 0 Then
            Dim sFilename = DTODelivery.FileName(_Deliveries)
            UIHelper.ShowPdf(oByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_PdfNoValorat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oByteArray As Byte() = Await FEB.Deliveries.Pdf(exs, _Deliveries, False, DTODelivery.CodsValorat.ForceFalse)
        If exs.Count = 0 Then
            Dim sFilename = DTODelivery.FileName(_Deliveries)
            UIHelper.ShowPdf(oByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim oBook = FEB.Delivery.Excel(exs, _Deliveries.First)
        If exs.Count = 0 Then
            If UIHelper.ShowExcel(oBook, exs) Then
                MyBase.ToggleProgressBarRequest(False)
            Else
                MyBase.ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs)
            End If
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_ProformaPdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oByteArray As Byte() = Await FEB.Deliveries.Pdf(exs, _Deliveries, True)
        If exs.Count = 0 Then
            Dim sFilename = DTODelivery.FileName(_Deliveries.First)
            UIHelper.ShowPdf(oByteArray, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_AlbEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Await Email(Current.Session.Emp, False)
    End Sub

    Private Async Sub Do_EmailConfirmationRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Deliveries.First
        If FEB.Customer.Load(oDelivery.Customer, exs) Then
            Dim sSubject As String = ""
            If DTOContactClass.Wellknown(DTOContactClass.Wellknowns.farmacia).Equals(oDelivery.Customer.ContactClass) Then
                sSubject = oDelivery.Customer.Lang.Tradueix("Aviso de envío #" & oDelivery.Id, "Avis d'enviament #" & oDelivery.Id, "Shipment advice #" & oDelivery.Id)
            Else
                sSubject = oDelivery.Customer.Lang.Tradueix("Solicitud de confirmación de envío #" & oDelivery.Id & " (requiere respuesta)", "Sol•licitut de confirmació d'enviament #" & oDelivery.Id & " (requereix resposta)", "Shipment confirmation request #" & oDelivery.Id & " (answer required)")
            End If
            Dim sRecipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.ConfirmacioEnviament, oDelivery.Customer)
            If sRecipients.Count = 0 Then
                Dim oUser As DTOUser = Await FEB.Contact.DefaultUser(oDelivery.Customer, exs)
                If oUser IsNot Nothing Then sRecipients.Add(oUser.EmailAddress)
            End If
            Dim url = FEB.Delivery.EmailConfirmationRequestUrl(oDelivery)

            Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
            With oMailMessage
                .Subject = sSubject
                .BodyUrl = url
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                Dim data_object As New DataObject
                data_object.SetData(DataFormats.Text, True, url)
                Clipboard.SetDataObject(data_object, True)
                UIHelper.WarnError(exs, "error al redactar missatge. Verificar " & url)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_AlbEmailSubscriptors(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB.Delivery.MailToSubscriptors(GlobalVariables.Emp, _Deliveries.First, exs) Then
            MsgBox("albarà enviat correctament als subscriptors")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ProformaEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Await Email(Current.Session.Emp, True)
    End Sub


    Private Sub Do_CopyTpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB.Delivery.UrlTpv(_Deliveries(0), True)
        Clipboard.SetDataObject(url, True)
    End Sub



    Private Sub Do_SeguimentTransportista(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_VivaceTrace(_Deliveries.First())
        oFrm.Show()
    End Sub

    Private Sub Do_ShowRepComs()
        Dim oFrm As New Frm_AlbRepComs(_Deliveries.First)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Factura(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDeliveries = _Deliveries.Where(Function(x) x.Facturable = True).ToList()
        Dim oInvoice = Await FEB.Invoice.Factory(exs, Current.Session.Emp, oDeliveries)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Invoice(oInvoice)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        oMenuItem.Enabled = False
        Application.DoEvents()

        Dim rc As MsgBoxResult
        Select Case _Deliveries.Count
            Case 1
                Dim oDelivery As DTODelivery = _Deliveries.First
                Dim s As String = String.Format("Eliminem l'albará {0}", oDelivery.Id)
                If oDelivery.Customer IsNot Nothing Then
                    s = String.Format("{0} de {1}", s, oDelivery.Contact.NomComercialOrDefault)
                Else
                End If
                rc = MsgBox(s & "?", MsgBoxStyle.OkCancel, "M+O")
            Case Else
                rc = MsgBox("Eliminem aquests " & _Deliveries.Count & " albarans?", MsgBoxStyle.OkCancel, "M+O")
        End Select

        If rc = MsgBoxResult.Ok Then
            MyBase.ToggleProgressBarRequest(True)
            Dim exs As New List(Of Exception)
            Dim sMsg As String = ""
            If Await FEB.Deliveries.Delete(exs, _Deliveries) Then
                Select Case _Deliveries.Count
                    Case 1
                        sMsg = String.Format("Albarà {0} eliminat", _Deliveries.First.Id)
                    Case Else
                        sMsg = "Albarans eliminats"
                End Select

                MsgBox(sMsg, MsgBoxStyle.Information, "M+O")
                oMenuItem.Enabled = True
                Application.DoEvents()

                RefreshRequest(Me, MatEventArgs.Empty)
                MyBase.ToggleProgressBarRequest(False)
            Else
                MyBase.ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs)
                oMenuItem.Enabled = True
                Application.DoEvents()
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If

    End Sub


    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery As DTODelivery = _Deliveries.First
        Dim sUrl As String = FEB.Delivery.Url(oDelivery, True)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("Copiat enllaç al portapapers:" & vbCrLf & "albará " & oDelivery.Id & " de " & oDelivery.Nom)
    End Sub

    Private Sub Do_CopyLinkTpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery As DTODelivery = _Deliveries.First
        Dim sUrl As String = FEB.Delivery.UrlTpv(oDelivery, True)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("Copiat enllaç al portapapers:" & vbCrLf & "albará " & oDelivery.Id & " de " & oDelivery.Customer.FullNom)
    End Sub



    Private Sub Do_Recollida(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            If FEB.Contact.Load(oDelivery.Mgz, exs) Then
                Dim oRecollida = DTORecollida.Factory(_Deliveries.First)
                Dim oFrm As New Frm_Alb_Recollida(oRecollida)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_retrocedePedidoPrepare()

        Dim oFrm As New Frm_Progress("Retroces de comandes", "Selecciona les comandes de cada albarà i hi afegeix en negatiu les linies de comanda que hi trova")
        AddHandler oFrm.requestToProceed, AddressOf Do_retrocedePedido
        oFrm.Show()

    End Sub

    Private Sub Do_RemoveFromTransmission()
        RaiseEvent RequestToRemoveFromTransmisio(Me, MatEventArgs.Empty)
    End Sub


    Private Async Sub Menu_Delivery_RequestToRemoveFromTransmisio(sender As Object, e As MatEventArgs) Handles Me.RequestToRemoveFromTransmisio
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)

        Dim tmp = Await FEB.Delivery.Find(_Deliveries.First.Guid, exs)
        If exs.Count = 0 Then
            tmp.Transmisio = Nothing
            tmp.UsrLog.UsrLastEdited = Current.Session.User.ToGuidNom()
            Await FEB.Delivery.Update(exs, tmp)
            If exs.Count = 0 Then
                _Deliveries.RemoveAt(0)
                _Deliveries.Insert(0, tmp)
                RefreshRequest(Me, New MatEventArgs(_Deliveries.First))
            End If
        End If

        If exs.Count > 0 Then
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AssignToATransmisio()
        Dim oFrm As New Frm_Transmisions(, DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.onItemSelected, AddressOf RequestToAssignToATransmisio
        oFrm.Show()
    End Sub

    Private Async Sub RequestToAssignToATransmisio(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oTransmisio As DTOTransmisio = e.Argument
        MyBase.ToggleProgressBarRequest(True)

        Dim tmp = Await FEB.Delivery.Find(_Deliveries.First.Guid, exs)
        If exs.Count = 0 Then
            tmp.Transmisio = oTransmisio
            tmp.UsrLog.UsrLastEdited = Current.Session.User.ToGuidNom()
            Await FEB.Delivery.Update(exs, tmp)
            If exs.Count = 0 Then
                _Deliveries.RemoveAt(0)
                _Deliveries.Insert(0, tmp)
                RefreshRequest(Me, New MatEventArgs(_Deliveries.First))
            End If
        End If

        If exs.Count > 0 Then
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_retrocedePedido(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim idx As Integer
        Dim BlCancel As Boolean
        For Each oDelivery As DTODelivery In _Deliveries
            FEB.Delivery.Load(oDelivery, exs)
            Dim oPurchaseOrders As List(Of DTOPurchaseOrder) = oDelivery.Items.Select(Function(x) x.PurchaseOrderItem.PurchaseOrder).Distinct.ToList
            For Each oPurchaseOrder As DTOPurchaseOrder In oPurchaseOrders
                If FEB.PurchaseOrder.Load(exs, oPurchaseOrder) Then
                    Dim oItemsToAdd As New List(Of DTOPurchaseOrderItem)
                    For Each item As DTOPurchaseOrderItem In oPurchaseOrder.Items
                        Dim itemToAdd As New DTOPurchaseOrderItem()
                        With itemToAdd
                            .Qty = -item.Qty
                            .Pending = -item.Qty
                            .Price = item.Price
                            .Dto = item.Dto
                            .ChargeCod = item.ChargeCod
                            .PurchaseOrder = item.PurchaseOrder
                            .RepCom = item.RepCom
                            .Sku = item.Sku
                            .Incentius = item.Incentius
                        End With
                        oItemsToAdd.Add(itemToAdd)
                    Next
                    oPurchaseOrder.Items.AddRange(oItemsToAdd)
                    Dim pOrder = FEB.PurchaseOrder.Update(exs, oPurchaseOrder)
                    idx += 1
                    sender.ShowProgress(1, _Deliveries.Count, idx, DTODelivery.FullNom(oDelivery), BlCancel)
                    If BlCancel Then Exit For
                End If
            Next
        Next

        If exs.Count > 0 Then UIHelper.WarnError(exs)

    End Sub

    Private Async Sub Do_Ordrsp_Ftp()
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        Dim warnMsg As String = ""
        Dim oEdiOrdrSps = Await FEB.Delivery.OrdrSps(exs, oDelivery, GlobalVariables.Emp.Org)
        Dim sb As New Text.StringBuilder

        Dim count As Integer = 0
        For Each oEdiOrdrsp As DTOEdiOrdrsp In oEdiOrdrSps
            Dim OrdrSpBytes = oEdiOrdrsp.ByteArray(warnMsg)
            Dim rc As MsgBoxResult = MsgBoxResult.Ok
            If warnMsg.Length > 0 Then rc = MsgBox(warnMsg, MsgBoxStyle.OkCancel)
            If rc = MsgBoxResult.Ok Then
                Dim sMsg = Await FEB.Ftpserver.Send(exs, oDelivery.Customer, DTOFtpserver.Path.Cods.Ordrsp, OrdrSpBytes, oEdiOrdrsp.DefaultFileName)
                sb.AppendLine(sMsg)
                count += 1
            Else
                MsgBox("Confirmació de comanda Edi cancel·lada per l'usuari", MsgBoxStyle.Information)
            End If

        Next

        If exs.Count = 0 Then
            MsgBox(String.Format("{0} de {1} fitxers pujats satisfactoriament al Ftp" & vbCrLf & sb.ToString, count, oEdiOrdrSps.Count))
        Else
            UIHelper.WarnError(exs, sb.ToString)
        End If
    End Sub



    Private Async Sub Ordrsp_Save()
        Dim exs As New List(Of Exception)
        Dim warnMsg As String = ""
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            Dim oDlg As New FolderBrowserDialog
            With oDlg
                .Description = "seleccionar carpeta on desar els fitxers"
                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    Dim path = .SelectedPath()
                    Dim oEdiOrdrSps = Await FEB.Delivery.OrdrSps(exs, oDelivery, GlobalVariables.Emp.Org)
                    For Each oEdiOrdrSp In oEdiOrdrSps
                        If exs.Count = 0 Then
                            If FileSystemHelper.SaveTextToFile(oEdiOrdrSp.Stream(warnMsg), .SelectedPath & "\" & oEdiOrdrSp.DefaultFileName, exs) Then
                                If (warnMsg.Length > 0) Then MsgBox(warnMsg, MsgBoxStyle.Exclamation)
                                'RefreshRequest(Me, MatEventArgs.Empty)
                            Else
                                exs.Add(New Exception("Error al desar el fitxer " & oEdiOrdrSp.DefaultFileName()))
                            End If
                        End If
                    Next
                End If
            End With

        End If
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Async Sub Do_Desadv_Ftp()
        Dim exs As New List(Of Exception)
        Dim sb As New Text.StringBuilder
        For Each oDelivery In _Deliveries
            If FEB.Delivery.Load(oDelivery, exs) Then
                oDelivery.Emp = GlobalVariables.Emp 'per dades GLN Org
                Dim oDesadv = oDelivery.Desadv(exs)
                Dim oByteArray = System.Text.Encoding.UTF8.GetBytes(oDesadv.EdiMessage())
                Dim status As String = Await FEB.Ftpserver.Send(exs, oDelivery.Customer, DTOFtpserver.Path.Cods.Desadv, oByteArray, oDesadv.DefaultFilename)
                sb.AppendLine(status)
            End If
        Next

        If exs.Count = 0 Then
            MsgBox(String.Format("{0} fitxers DESADV pujats satisfactoriament al Ftp" & vbCrLf & sb.ToString, _Deliveries.Count))
        Else
            UIHelper.WarnError(exs, sb.ToString)
        End If
    End Sub



    Private Sub Desadv_Save()
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            oDelivery.Emp = GlobalVariables.Emp 'per dades GLN Org
            Dim oDesadv = oDelivery.Desadv(exs)
            Dim oDlg As New SaveFileDialog
            With oDlg
                .Title = "Desar fitxer Edi DESADV"
                .FileName = oDesadv.DefaultFilename()
                .Filter = "fitxers Txt|*.txt|tots els arxius|*.*"
                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                    If FileSystemHelper.SaveTextToFile(oDesadv.EdiMessage, .FileName, exs) Then
                    Else
                        exs.Add(New Exception("Error al desar el fitxer " & .FileName))
                    End If
                End If
            End With

        End If
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub DesadvEdiversa_Save()
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            oDelivery.Emp = GlobalVariables.Emp 'per dades GLN Org
            Dim oDesadv = oDelivery.Desadv(exs)
            If exs.Count = 0 Then
                Dim oDlg As New SaveFileDialog
                With oDlg
                    .Title = "Desar fitxer Edi DESADV (format Ediversa)"
                    .FileName = "Ediversa " & oDesadv.DefaultFilename()
                    .Filter = "fitxers Txt|*.txt|tots els arxius|*.*"
                    If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        If FileSystemHelper.SaveTextToFile(oDesadv.EdiversaMessage, .FileName, exs) Then
                        Else
                            exs.Add(New Exception("Error al desar el fitxer " & .FileName))
                        End If
                    End If
                End With
            End If
        End If

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_DesadvEdiversa_Send()
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            oDelivery.Emp = GlobalVariables.Emp 'per dades GLN Org
            Dim oDesadv = oDelivery.Desadv(exs)

            Dim oSender As New DTOEdiversaContact
            With oSender
                .Contact = GlobalVariables.Emp.Org
                .Ean = .Contact.GLN
            End With

            Dim oReceiver As New DTOEdiversaContact
            With oReceiver
                .Contact = oDelivery.Customer
                .Ean = .Contact.GLN
            End With

            Dim oEdiversaFile = New DTOEdiversaFile
            With oEdiversaFile
                .Tag = DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN005.ToString
                .Fch = oDelivery.Fch
                .Sender = oSender
                .Receiver = oReceiver
                .Docnum = oDelivery.Id
                .Amount = oDelivery.totalCash
                .Result = DTOEdiversaFile.Results.pending
                .ResultBaseGuid = oDelivery
                .Stream = oDesadv.EdiversaMessage()
                .IOCod = DTOEdiversaFile.IOcods.outbox
            End With


            If Await FEB.EdiversaFile.Update(exs, oEdiversaFile) Then
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                'verificar si src es Edi i si no grabar-li
                'If Not FEB.EdiversaFile.Update(exs, oEdiFile) Then
                'exs.Add(New Exception("error al enrutar factura " & oInvoice.Num))
                UIHelper.WarnError("error al desar el fitxer Edi")
            End If

        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    '==========================================================================
    '                               AUXILIARS
    '==========================================================================

    Private Async Function Email(oEmp As DTOEmp, ByVal BlProforma As Boolean) As Task(Of Task)
        Dim exs As New List(Of Exception)
        If DTODelivery.sameCustomer(_Deliveries) Then 'si tots els albarans son del mateix client
            Dim oFirstAlb = _Deliveries.First
            Dim oCustomer As DTOCustomer = oFirstAlb.Customer
            Dim sRecipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.ConfirmacioEnviament, oCustomer)
            If exs.Count = 0 Then
                If sRecipients.Count = 0 Then
                    Dim oUsers = Await FEB.Users.All(exs, oCustomer)
                    sRecipients = oUsers.Select(Function(x) x.EmailAddress).ToList
                End If

                Dim sSubject As String = DTODelivery.labelAndNumsText(_Deliveries, BlProforma)

                Dim oMailMessage = DTOMailMessage.Factory(sRecipients, sSubject)

                For Each oDelivery In _Deliveries
                    Dim sFriendlyname As String = DTODelivery.FileName(oDelivery, BlProforma)
                    Dim sFullPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) & "\" & sFriendlyname
                    Dim oPdfStream = Await FEB.Delivery.Pdf(oDelivery, BlProforma, exs)
                    System.IO.File.WriteAllBytes(sFullPath, oPdfStream)
                    oMailMessage.AddAttachment(sFullPath, sFriendlyname)
                Next

                If exs.Count = 0 Then
                    If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                exs.Add(New Exception("cal triar albarans del mateix client per enviar-los per correu"))
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Do_Desadv()
        Dim exs As New List(Of Exception)
        Dim oDelivery = _Deliveries.First
        If FEB.Delivery.Load(oDelivery, exs) Then
            Dim oDesadv As EdiHelperStd.Desadv = oDelivery.Desadv2(GlobalVariables.Emp.Org)
            Dim sMessage = oDesadv.Message(exs, EdiHelperStd.EdiFile.Formats.Native)
            If exs.Count = 0 Then
                Dim oDlg As New SaveFileDialog()
                With oDlg
                    .FileName = String.Format("Desadv {0}.txt", oDelivery.Formatted)
                    .Filter = "fitxers de text (*.txt)|*.txt|tots els fitxers (*.*)|*.*"
                    If .ShowDialog Then
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter(.FileName, False)
                        file.WriteLine(sMessage)
                        file.Close()
                    End If
                End With
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class

