

Public Class Menu_Client
    Private _Customer As DTOCustomer
    Private _Customers As List(Of DTOCustomer)


    Public Sub New(ByVal oCustomer As DTOCustomer)
        MyBase.New()
        _Customer = oCustomer
        _Customers = New List(Of DTOCustomer)
        _Customers.Add(_Customer)
    End Sub

    Public Sub New(ByVal oCustomers As List(Of DTOCustomer))
        MyBase.New()
        _Customers = oCustomers
        _Customer = oCustomers.First
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Group(),
        MenuItem_NewPdc(),
        MenuItem_NewAlb(),
        MenuItem_Pncs(),
        MenuItem_Hist(),
        MenuItem_LastPdcs(),
        MenuItem_LastAlbs(),
        MenuItem_LastFras(),
        MenuItem_Spvs(),
        MenuItem_Credit(),
        MenuItem_Sellout(),
        MenuItem_CitClis(),
        MenuItem_Tarifas(),
        MenuItem_Margins(),
        MenuItem_CustomCataleg(),
        MenuItem_Notifications()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Group() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Grup..."
        Dim exs As New List(Of Exception)
        If FEB.Customer.IsGroupSync(exs, _Customer) Then
            oMenuItem.Image = My.Resources.People_Blue
            oMenuItem.DropDownItems.AddRange(New Menu_ClientGroup(_Customer).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_NewPdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewPdc
        oMenuItem.Text = "nova comanda"

        oMenuItem.ShortcutKeys = Shortcut.CtrlP ' Keys.Control | Keys.P 'root.ShortCutControlKey("P")
        Return oMenuItem
    End Function


    Private Function MenuItem_NewAlb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewAlb
        oMenuItem.Text = "nou albarà"
        oMenuItem.ShortcutKeys = Shortcut.CtrlA
        Return oMenuItem
    End Function


    Private Function MenuItem_Pncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Pncs
        oMenuItem.Text = "pendents d'entrega"
        Return oMenuItem
    End Function

    Private Function MenuItem_Hist() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Hist
        oMenuItem.Text = "historial"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        oMenuItem.Text = "ultimes comandes"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "ultims albarans"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastFras() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastFras
        oMenuItem.Text = "ultimes factures"
        Return oMenuItem
    End Function

    Private Function MenuItem_Spvs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "reparacions"
        oMenuItem.DropDownItems.Add(MenuItem_LastSpvs)
        oMenuItem.DropDownItems.Add(MenuItem_NewSpv)
        Return oMenuItem
    End Function

    Private Function MenuItem_LastSpvs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastSpvs
        oMenuItem.Text = "reparacions"
        oMenuItem.Image = My.Resources.Spv
        Return oMenuItem
    End Function

    Private Function MenuItem_NewSpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewSpv
        oMenuItem.ShortcutKeys = Shortcut.CtrlR
        oMenuItem.Text = "nova reparacio"
        oMenuItem.Image = My.Resources.clip
        Return oMenuItem
    End Function

    Private Function MenuItem_Credit() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Credit
        oMenuItem.Text = "crèdit"
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        oMenuItem.Text = "sellout"
        Return oMenuItem
    End Function

    Private Function MenuItem_CitClis() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CitClis
        oMenuItem.Text = "altres clients a la poblacio"
        Return oMenuItem
    End Function

    Private Function MenuItem_Margins() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Margins
        oMenuItem.Text = "marges comercials"
        Return oMenuItem
    End Function


    Private Function MenuItem_CustomCataleg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "cataleg personalitzat"
        oMenuItem.DropDownItems.Add(MenuItem_ArtCustRefs)
        oMenuItem.DropDownItems.Add(MenuItem_ExcelStocks)
        Return oMenuItem
    End Function

    Private Function MenuItem_ArtCustRefs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ArtCustRefs
        oMenuItem.Text = "referències"
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelStocks() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ExcelStocks
        oMenuItem.Text = "Stocks en Excel"
        Return oMenuItem
    End Function


    Private Function MenuItem_Tarifas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Tarifas
        oMenuItem.Text = "tarifas"
        Return oMenuItem
    End Function

    Private Function MenuItem_Notifications() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If Current.Session.User.Rol.isSuperAdmin Then
            oMenuItem.Text = "notificacions"
            oMenuItem.DropDownItems.Add(SubMenuItem_BankTransferReminder)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_BankTransferReminder() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_BankTransferReminder
        oMenuItem.Text = "proper venciment de transferències"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Async Sub Do_NewPdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Customer.Load(_Customer, exs) Then
            Dim oOrder = DTOPurchaseOrder.Factory(_Customer, Current.Session.User)
            If oOrder.Contact.UnEquals(_Customer) Then
                MsgBox("Client canviat a central:" & vbCrLf & _Customer.FullNom)
            End If

            If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                Dim oFrm As New Frm_PurchaseOrder(oOrder)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub



    Private Sub Do_NewAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewCliAlbNew(_Customer)
    End Sub


    Private Sub Do_Pncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Pncs(_Customer, DTOPurchaseOrder.Codis.Client)
        oFrm.Show()
    End Sub

    Private Sub Do_Hist(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ContactPurchaseOrderItems(_Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PurchaseOrders(_Customer)
        oFrm.Show()
    End Sub

    Private Async Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB.Deliveries.Headers(exs, _Customer)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Deliveries(Xl_Deliveries.Purposes.SingleCustomer, oDeliveries, "Albarans de " & _Customer.FullNom)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_LastFras(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Customer_Invoices(_Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_LastSpvs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Spvs(_Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_Tarifas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CustomerTarifa(_Customer)
        oFrm.Show()
    End Sub

    Private Async Sub Do_NewSpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSpv As DTOSpv = DTOSpv.Factory(Current.Session.User)
        If FEB.Customer.Load(_Customer, exs) Then
            With oSpv
                .Customer = _Customer
                .Nom = _Customer.NomComercialOrDefault
                .Address = _Customer.Address
                .Tel = Await FEB.Contact.Tel(exs, _Customer)
            End With
            Dim oFrm As New Frm_Spv(oSpv)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Credit(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Client_Risc(_Customer)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User,, DTOSellOut.ConceptTypes.Product)
        If exs.Count = 0 Then
            With oSellout
                Dim oFilter = .Filters.First(Function(x) x.Cod = DTOSellOut.Filter.Cods.Customer)
                oFilter.Values.AddRange(_Customers)
                '.Customers = _Customers
                .Format = DTOSellOut.Formats.Amounts
            End With
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CitClis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Contact.Load(_Customer, exs) Then
            Dim oFrm As New Frm_Location_Clis(_Customer.Address.Zip.Location)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Margins(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Margins(Models.MarginsModel.Modes.Customer, _Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_ArtCustRefs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCustomer As DTOCustomer = FEB.Customer.CcxOrMe(exs, _Customer)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_CustomerProducts(oCustomer)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_ExcelStocks()
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.ProductStocks.Excel(exs, Current.Session.Emp, _Customer)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BankTransferReminder()
        Dim exs As New List(Of Exception)
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.BankTransferReminder)
        Dim oSubscriptors = Await FEB.Subscriptors.All(exs, oSubscription, _Customer)
        Dim sRecipients As New List(Of String)
        If oSubscriptors.Count > 0 Then
            sRecipients = oSubscriptors.Select(Function(x) x.emailAddress).ToList
        End If
        If _Customer.lang Is Nothing Then FEB.Customer.Load(_Customer, exs)
        Dim sSubject = _Customer.lang.Tradueix("Recordatorio de transferencia", "Recordatori de transferencia", "Bank transfer reminder", "Lembrete de transferência bancária")
        Dim oMailMessage = DTOMailMessage.Factory(sRecipients, sSubject)
        oMailMessage.bodyUrl = MmoUrl.Factory(True, "mail/BankTransferReminder", _Customer.Guid.ToString)
        If Not Await OutlookHelper.Send(oMailMessage, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

End Class
