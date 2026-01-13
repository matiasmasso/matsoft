

Public Class Menu_ClientGroup
    Private _Customer As DTOCustomer

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New
        _Customer = oCustomer
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Ccxs(),
        MenuItem_Pncs(),
        MenuItem_SellOut(),
        MenuItem_Margins(),
        MenuItem_LastPdcs(),
        MenuItem_LastAlbs(),
        MenuItem_SortidaPerPlataformes()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================



    Private Function MenuItem_Ccxs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Ccx
        oMenuItem.Text = "centres"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        oMenuItem.Text = "ultimes comandes"
        Return oMenuItem
    End Function

    Private Function MenuItem_Pncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Pncs
        oMenuItem.Text = "pendents d'entrega"
        Return oMenuItem
    End Function

    Private Function MenuItem_SellOut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SellOut
        oMenuItem.Text = "sellout"
        Return oMenuItem
    End Function

    Private Function MenuItem_Margins() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Margins
        oMenuItem.Text = "Marges comercials"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "ultims albarans"
        Return oMenuItem
    End Function

    Private Function MenuItem_SortidaPerPlataformes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SortidaPerPlataformes
        oMenuItem.Text = "sortida per plataformes"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================



    Private Sub Do_Ccx(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_Client_Ccx(_Customer)
        Dim oFrm As New Frm_Ccx(_Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_Pncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Ccx(_Customer, Frm_Ccx.Tabs.Pncs)
        oFrm.Show()
    End Sub

    Private Async Sub Do_SellOut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.Yeas)
        If exs.Count = 0 Then
            Dim oCcx = FEB.Customer.CcxOrMe(exs, _Customer)
            If exs.Count = 0 Then
                FEB.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Customer, {oCcx})
                oSellout.GroupByHolding = True
                Dim oFrm As New Frm_SellOut(oSellout)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Margins()
        Dim oFrm As New Frm_Margins(Models.MarginsModel.Modes.Ccx, _Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCcx = FEB.Customer.CcxOrMe(exs, _Customer)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_PurchaseOrders(oCcx, includeGroupSalePoints:=True)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCcx = FEB.Customer.CcxOrMe(exs, _Customer)
        If exs.Count = 0 Then
            Dim oDeliveries = Await FEB.Deliveries.Headers(exs, Current.Session.Emp, contact:=oCcx, group:=True)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_Deliveries(oPurpose:=Xl_Deliveries.Purposes.MultipleCustomers, oDeliveries:=oDeliveries, sCaption:="Albarans del Grup de " & _Customer.FullNom)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_SortidaPerPlataformes()
        Dim oFrm As New Frm_PlatformsNewAlb(_Customer)
        oFrm.Show()
    End Sub

End Class

