Public Class Menu_EDiversaException
    Inherits Menu_Base

    Private _EDiversaException As DTOEdiversaException


    Public Sub New(ByVal oEDiversaException As DTOEdiversaException)
        MyBase.New()
        _EDiversaException = oEDiversaException
    End Sub

    Public Shadows Async Function Range() As Task(Of ToolStripMenuItem())
        Dim items As New List(Of ToolStripMenuItem)
        Select Case _EDiversaException.Cod
            Case DTOEdiversaException.Cods.InterlocutorNotFound
                items.Add(MenuItem_Interlocutor_Create())
                items.Add(MenuItem_Interlocutor_Select())
            Case DTOEdiversaException.Cods.PlatformNotFound
                items.Add(Await MenuItem_Platform_Select())
            Case DTOEdiversaException.Cods.PlatformNoValid
                items.Add(Await MenuItem_Platform_Select())
            Case DTOEdiversaException.Cods.SkuNotFound
                items.Add(MenuItem_SkuNotFound_Select)
                items.Add(MenuItem_SkuNotFound_Email)
                items.Add(MenuItem_RefuseLine)
                items.Add(MenuItem_CopyEan)
            Case DTOEdiversaException.Cods.MissingPrice
                items.Add(MenuItem_PriceAccept)
            Case DTOEdiversaException.Cods.WrongPrice
                items.Add(MenuItem_PriceAmend)
                items.Add(MenuItem_PriceAccept)
                items.Add(MenuItem_RefuseLine)
            Case DTOEdiversaException.Cods.ContactCompradorNotFound
                items.Add(MenuItem_ContactLookup)
                items.Add(MenuItem_ContactRegister)
            Case DTOEdiversaException.Cods.WrongDiscount
                items.Add(MenuItem_DtoAccept)
                items.Add(MenuItem_AmendDto)
                items.Add(MenuItem_AmendAllDtos)
                items.Add(MenuItem_RefuseLine)
            Case DTOEdiversaException.Cods.SkuObsolet
                items.Add(MenuItem_EmailWarnObsolet)
                items.Add(MenuItem_RefuseLine)
            Case DTOEdiversaException.Cods.MissingSkuEAN
                items.Add(MenuItem_ShowSku)
            Case DTOEdiversaException.Cods.DuplicatedOrder
                items.Add(MenuItem_ShowPdc)
        End Select
        Dim retval() As ToolStripMenuItem = items.ToArray
        Return retval
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Interlocutor_Create() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Crear nou interlocutor"
        AddHandler oMenuItem.Click, AddressOf Do_ContactRegister
        Return oMenuItem
    End Function

    Private Function MenuItem_Interlocutor_Select() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Seleccionar interlocutor existent"
        AddHandler oMenuItem.Click, AddressOf Do_SelectInterlocutor
        Return oMenuItem
    End Function


    Private Async Function MenuItem_Platform_Select() As Task(Of ToolStripMenuItem)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Seleccionar plataforma"
        If _EDiversaException.Tag Is Nothing Then
            oMenuItem.Enabled = False
        Else
            Dim exs As New List(Of Exception)
            Dim oOrder = Await FEB.EdiversaOrder.Find(_EDiversaException.Tag.Guid, exs)
            Dim oPlatforms As List(Of DTOCustomerPlatform) = Await FEB.ElCorteIngles.Platforms(oOrder.Customer)
            For Each item As DTOCustomerPlatform In oPlatforms
                Dim oMenuPlatf As New ToolStripMenuItem(item.Nom, Nothing, AddressOf Do_ChangePlatform)
                oMenuPlatf.Tag = item
                oMenuItem.DropDownItems.Add(oMenuPlatf)
            Next
        End If

        Return oMenuItem
    End Function

    Private Function MenuItem_SkuNotFound_Select() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sel·leccionar producte"
        AddHandler oMenuItem.Click, AddressOf Do_SkuNotFound_Select
        Return oMenuItem
    End Function

    Private Function MenuItem_ShowSku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mostra la fitxa de producte"
        AddHandler oMenuItem.Click, AddressOf Do_ShowSku
        Return oMenuItem
    End Function

    Private Function MenuItem_ShowPdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mostra la comanda original"
        AddHandler oMenuItem.Click, AddressOf Do_ShowPdc
        Return oMenuItem
    End Function


    Private Function MenuItem_SkuNotFound_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "redactar email avisan-t'ho"
        AddHandler oMenuItem.Click, AddressOf Do_SkuNotFound_Email
        Return oMenuItem
    End Function

    Private Function MenuItem_PriceAccept() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Acceptar el preu de la comanda"
        AddHandler oMenuItem.Click, AddressOf Do_PriceAccept
        Return oMenuItem
    End Function

    Private Function MenuItem_PriceAmend() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Donar-li el preu correcte"
        AddHandler oMenuItem.Click, AddressOf Do_PriceAmend
        Return oMenuItem
    End Function

    Private Function MenuItem_DtoAccept() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Acceptar el descompte de la comanda"
        AddHandler oMenuItem.Click, AddressOf Do_DtoAccept
        Return oMenuItem
    End Function

    Private Function MenuItem_AmendDto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Donar-li el descompte correcte"
        AddHandler oMenuItem.Click, AddressOf Do_AmendDto
        Return oMenuItem
    End Function

    Private Function MenuItem_AmendAllDtos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Asignar els descomptes correctes a totes les linies"
        AddHandler oMenuItem.Click, AddressOf Do_AmendAllDtos
        Return oMenuItem
    End Function

    Private Function MenuItem_RefuseLine() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Descarta aquesta linia"
        AddHandler oMenuItem.Click, AddressOf Do_DescartaLine
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyEan() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Ean"
        AddHandler oMenuItem.Click, AddressOf Do_CopyEan
        Return oMenuItem
    End Function

    '

    Private Function MenuItem_EmailWarnObsolet() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Redactar email de notificació"
        AddHandler oMenuItem.Click, AddressOf Do_EmailWarnObsolet
        Return oMenuItem
    End Function

    Private Function MenuItem_ContactLookup() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Buscar-lo als missatges Edi"
        AddHandler oMenuItem.Click, AddressOf Do_ContactLookup
        Return oMenuItem
    End Function

    Private Function MenuItem_ContactRegister() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Donar-lo d'alta"
        AddHandler oMenuItem.Click, AddressOf Do_ContactRegister
        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_SkuNotFound_Select(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku)
        AddHandler oFrm.onItemSelected, AddressOf On_SkuNotFound_Selected
        oFrm.Show()
    End Sub

    Private Sub Do_ShowSku(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSku As DTOProductSku = Nothing
        If TypeOf _EDiversaException.Tag Is DTOEdiversaOrderItem Then
            oSku = DirectCast(_EDiversaException.Tag, DTOEdiversaOrderItem).Sku
        ElseIf TypeOf _EDiversaException.Tag Is DTOPurchaseOrderItem Then
            oSku = DirectCast(_EDiversaException.Tag, DTOPurchaseOrderItem).Sku
        End If
        Dim oFrm As New Frm_Art(oSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_ShowPdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not TypeOf _EDiversaException.Tag Is DTOPurchaseOrder Then
            _EDiversaException.Tag = Await FEB.PurchaseOrder.Find(_EDiversaException.Tag.Guid, exs)
        End If
        Dim oOrder As DTOPurchaseOrder = _EDiversaException.Tag
        Dim oFrm As New Frm_PurchaseOrder(oOrder)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



    Private Async Sub Do_SkuNotFound_Email(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            Dim oOrder As DTOEdiversaOrder = oOrderItem.Parent
            Dim sSubject As String = "Incidencia pedido " & oOrder.DocNum & ". Producto con referencia incorrecta"
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Producto: " & oOrderItem.RefClient)
            sb.AppendLine("Descripción: " & oOrderItem.Dsc)
            sb.AppendLine("EAN: " & oOrderItem.Ean.Value)

            Dim oMailMessage = DTOMailMessage.Factory()
            With oMailMessage
                .Subject = String.Format("Incidencia pedido {0}. Producto con referencia incorrecta", oOrder.DocNum)
                .Body = sb.ToString
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyEan(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem As DTOEdiversaOrderItem = _EDiversaException.Tag
        UIHelper.CopyToClipboard(DTOEan.eanValue(oOrderItem.Ean))
    End Sub

    Private Async Sub Do_PriceAccept(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            With oOrderItem
                .SkipPreuValidationUser = Current.Session.User
                .SkipPreuValidationFch = DTO.GlobalVariables.Now()
            End With
            MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_PriceAmend(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            Dim oOrder As DTOEdiversaOrder = oOrderItem.Parent
            Dim oSku As DTOProductSku = oOrderItem.Sku
            Dim oPrice As DTOAmt = Await FEB.ProductSku.Price(exs, oSku, oOrder.Customer)
            If exs.Count = 0 Then
                oOrderItem.Preu = oPrice
                MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_DtoAccept(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            With oOrderItem
                .SkipDtoValidationUser = Current.Session.User
                .SkipDtoValidationFch = DTO.GlobalVariables.Now()
            End With

            MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub Do_AmendDto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            Dim oOrder As DTOEdiversaOrder = oOrderItem.Parent
            Dim oCustomer As DTOCustomer = oOrder.Customer
            Dim oSku As DTOProductSku = oOrderItem.Sku
            Dim oCliProductDtos = Await FEB.CliProductDtos.All(oCustomer, exs)
            If exs.Count = 0 Then
                Dim DcDto As Decimal = FEB.PurchaseOrderItem.GetDiscount(oSku, oCustomer, oCliProductDtos)
                oOrderItem.Dto = DcDto
                MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_AmendAllDtos(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            Dim oOrder As DTOEdiversaOrder = oOrderItem.Parent
            Dim oCustomer As DTOCustomer = oOrder.Customer
            Dim oSku As DTOProductSku = oOrderItem.Sku
            Dim oCliProductDtos = Await FEB.CliProductDtos.All(oCustomer, exs)
            If exs.Count = 0 Then
                For Each item As DTOEdiversaOrderItem In oOrder.Items
                    item.Dto = FEB.PurchaseOrderItem.GetDiscount(oSku, oCustomer, oCliProductDtos)
                Next

                MyBase.RefreshRequest(sender, New MatEventArgs(oOrder.Items))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_DescartaLine(sender As Object, e As EventArgs)
        If _EDiversaException.TagCod = DTOEdiversaException.TagCods.EdiversaOrderItem AndAlso _EDiversaException.Tag IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
            If exs.Count = 0 Then
                With oOrderItem
                    .SkipItemUser = Current.Session.User
                    .SkipItemFch = DTO.GlobalVariables.Now()
                End With
                MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError("no implementat per aquest cas")
        End If
    End Sub

    Private Async Sub On_SkuNotFound_Selected(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oSku As DTOProductSku = e.Argument
        If oSku IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If _EDiversaException.Tag Is Nothing Then
                UIHelper.WarnError("missing exception tag")
            Else
                Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
                If exs.Count = 0 Then
                    If oOrderItem Is Nothing Then
                        UIHelper.WarnError("No s'ha trobat la linia de la comanda a modificar")
                    Else
                        oOrderItem.Sku = oSku
                        MyBase.RefreshRequest(sender, New MatEventArgs(oOrderItem))
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
    End Sub



    Private Async Sub Do_ContactLookup(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oOrder As DTOEdiversaOrder = _EDiversaException.Tag
        Dim sSearchKey As String = oOrder.CompradorEAN.Value
        Dim oGenrals = Await FEB.EdiversaGenrals.Search(sSearchKey, exs)
        If oGenrals.Count = 0 Then
            UIHelper.WarnError("No s'ha trobat cap missatge sobre '" & sSearchKey & "'")
        Else
            Dim sb As New System.Text.StringBuilder
            For Each item As DTOEdiversaGenral In oGenrals
                sb.AppendLine("missatge del " & item.Fch.ToShortDateString)
                sb.AppendLine(item.Text)
                sb.AppendLine()
            Next
            Dim oFrm As New Frm_Literal("Recerca Edi per '" & sSearchKey & "'", sb.ToString())
            oFrm.Show()
        End If
    End Sub

    Private Async Sub Do_ContactRegister(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oEdiversaOrderBase As DTOBaseGuid = CType(_EDiversaException.Tag, DTOBaseGuid)
        Dim oEdiversaOrder = Await FEB.EdiversaOrder.Find(oEdiversaOrderBase.Guid, exs)
        If exs.Count = 0 Then
            _EDiversaException.Tag = oEdiversaOrder 'so it is available later on
            Dim oEAN As DTOEan = Nothing
            Select Case _EDiversaException.TagCod
                Case DTOEdiversaException.TagCods.NADBY
                    oEAN = oEdiversaOrder.CompradorEAN
                Case DTOEdiversaException.TagCods.NADIV
                    oEAN = oEdiversaOrder.FacturarAEAN
                Case DTOEdiversaException.TagCods.NADDP
                    oEAN = oEdiversaOrder.ReceptorMercanciaEAN
                Case Else
                    UIHelper.WarnError("codi Interlocutor desconegut")
            End Select

            If oEAN IsNot Nothing Then
                Dim oContact As New DTOContact()
                With oContact
                    .Emp = Current.Session.Emp
                    .Rol = New DTORol(DTORol.Ids.cliLite)
                    .GLN = oEAN
                End With
                Dim oFrm As New Frm_Contact(oContact)
                AddHandler oFrm.AfterUpdate, AddressOf EdiversaOrderRestore
                oFrm.Show()
            End If
        Else
            UIHelper.WarnError(exs)
        End If


    End Sub


    Private Sub Do_SelectInterlocutor(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ContactSearch()
        AddHandler oFrm.itemSelected, AddressOf OnInterlocutorSelected
        oFrm.Show()
    End Sub

    Private Async Sub OnInterlocutorSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = e.Argument
        oContact = Await FEB.Contact.Find(oContact.Guid, exs)
        If exs.Count = 0 Then
            If oContact.GLN IsNot Nothing Then
                UIHelper.WarnError("Aquest interlocutor ja te assignat el GLN " & oContact.GLN.Value)
            Else
                Dim oEdiversaOrderBase As DTOBaseGuid = CType(_EDiversaException.Tag, DTOBaseGuid)
                Dim oEdiversaOrder = Await FEB.EdiversaOrder.Find(oEdiversaOrderBase.Guid, exs)
                _EDiversaException.Tag = oEdiversaOrder 'so it is available later on
                If exs.Count = 0 Then

                    Select Case _EDiversaException.TagCod
                        Case DTOEdiversaException.TagCods.NADBY
                            oContact.GLN = oEdiversaOrder.CompradorEAN
                            If Await FEB.Contact.Update(exs, oContact) Then
                                EdiversaOrderRestore()
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Case DTOEdiversaException.TagCods.NADIV
                            oContact.GLN = oEdiversaOrder.FacturarAEAN
                            If Await FEB.Contact.Update(exs, oContact) Then
                                EdiversaOrderRestore()
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Case DTOEdiversaException.TagCods.NADDP
                            oContact.GLN = oEdiversaOrder.ReceptorMercanciaEAN
                            If Await FEB.Contact.Update(exs, oContact) Then
                                EdiversaOrderRestore()
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Case Else
                            UIHelper.WarnError("codi Interlocutor desconegut")
                    End Select
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub EdiversaOrderRestore()
        Dim exs As New List(Of Exception)
        Dim oOrder As DTOEdiversaOrder = _EDiversaException.Tag
        MyBase.ToggleProgressBarRequest(True)
        Dim oFile = oOrder.EdiversaFile
        If FEB.EdiversaFile.Load(oFile, exs) Then
                If Not Await FEB.EdiversaFileSystem.SaveInboxFile(oFile, exs) Then
                    UIHelper.WarnError(exs)
                End If
            End If
        MyBase.RefreshRequest(Me, New MatEventArgs(oOrder))
        MyBase.ToggleProgressBarRequest(False)

        'If Await FEB.EdiversaOrders.Validate(oOrder, exs) Then
        '    MyBase.ToggleProgressBarRequest(False)
        '    MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
        'Else
        '    MyBase.ToggleProgressBarRequest(False)
        '    UIHelper.WarnError(exs)
        'End If
    End Sub



    Private Async Sub Do_EmailWarnObsolet()
        Dim exs As New List(Of Exception)
        Dim oOrderItem = Await FEB.EdiversaOrderItem.Find(_EDiversaException.Tag.Guid, exs)
        If exs.Count = 0 Then
            Dim oOrder As DTOEdiversaOrder = oOrderItem.Parent
            Dim sSubject As String = "Incidencia pedido " & oOrder.DocNum
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Producto: " & oOrderItem.RefClient)
            sb.AppendLine("Descripción: " & oOrderItem.Dsc)

            Dim oMailMessage As New DTOMailMessage
            With oMailMessage
                .Subject = String.Format("Incidencia pedido {0}", oOrder.DocNum)
                .BodyUrl = sb.ToString
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ChangePlatform(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oPlatform As DTOCustomerPlatform = oMenuItem.Tag
        If _EDiversaException.Tag Is Nothing Then
            UIHelper.WarnError("_EDiversaException.Tag Is Nothing")
        Else
            With DirectCast(_EDiversaException.Tag, DTOEdiversaOrder)
                .ReceptorMercancia = oPlatform
                .ReceptorMercanciaEAN = oPlatform.GLN
            End With
            MyBase.RefreshRequest(sender, New MatEventArgs(_EDiversaException.Tag))
        End If
    End Sub
End Class

