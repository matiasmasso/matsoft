Imports System.ComponentModel

Public Class Frm_PlatformsNewAlb
    Private _Customer As DTOCustomer
    Private _Holding As DTOHolding
    Private _AllPurchaseOrders As List(Of DTOPurchaseOrder)
    Private _StocksAvailable As List(Of DTOStockAvailable)
    Private _Depts As List(Of String)
    Private _Groups As List(Of DTOECITransmGroup)

    Private _AllowEvents As Boolean


    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _Customer = oCustomer
    End Sub

    Public Sub New(oHolding As DTOHolding)
        MyBase.New()
        Me.InitializeComponent()
        _Holding = oHolding
    End Sub


    Private Async Sub Frm_PlatformsNewAlb_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _AllowEvents = False
        UIHelper.ToggleProggressBar(Panel1, True)

        If _Holding Is Nothing Then
            _AllPurchaseOrders = Await FEB2.PurchaseOrders.PendingForPlatforms(exs, _Customer)
        Else
            _AllPurchaseOrders = Await FEB2.PurchaseOrders.PendingForPlatforms(exs, _Holding)
        End If

        If exs.Count = 0 Then
            _StocksAvailable = Await StocksAvailable(exs)
            If exs.Count = 0 Then
                _Groups = Await FEB2.ECITransmGroups.All(exs)
                UIHelper.ToggleProggressBar(Panel1, False)

                If exs.Count = 0 Then
                    ButtonOk.Enabled = True
                    LoadDepts()
                    LoadDeliveries()
                    refresca()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub


    Public Function CurrentDeptOrders() As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sDept = ComboBoxDept.SelectedItem
        If sDept = "(sense departament)" Then
            retval = _AllPurchaseOrders.FindAll(Function(x) Not x.concept.Contains("/dep."))
        Else
            Dim sRegexPattern = "\/" & sDept & "\/"
            retval = _AllPurchaseOrders.FindAll(Function(x) x.concept.Contains("/" & sDept & "/"))
        End If
        Return retval
    End Function

    Private Function CurrentOrders() As List(Of DTOPurchaseOrder)
        Dim retval = Xl_EciPlatformDeliveries1.CheckedOrders()
        Return retval
    End Function

    Private Function CurrentDeliveries() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        Dim oDeliveries = Xl_PurchaseOrderItemsForPlatforms1.Deliveries.OrderBy(Function(x) x.customer.ref).ToList
        For Each oGroup In _Groups
            For Each oDelivery In oDeliveries
                If DTOECITransmGroup.Belongs(oGroup, oDelivery) Then
                    If Not retval.Any(Function(x) x.Equals(oDelivery)) Then
                        retval.Add(oDelivery)
                    End If
                End If
            Next
        Next

        Dim oMissingDeliveries = oDeliveries.Except(retval)
        retval.AddRange(oMissingDeliveries)
        Return retval
    End Function

    Private Sub refresca()
        _AllowEvents = False
        refrescaOrders()
        refrescaStocks()
        refrescaDeliveries()
        refrescaSumary()
        _AllowEvents = True
    End Sub


    Private Sub refrescaOrders()
        Dim oOrders = CurrentOrders.OrderBy(Function(x) x.concept).OrderBy(Function(y) y.fchDeliveryMin).ToList()
        Dim oUniqueOrders = oOrders.Distinct().ToList()
        Xl_PurchaseOrderItemsForPlatforms1.Load(oUniqueOrders, _StocksAvailable)
    End Sub

    Private Sub refrescaStocks()
        Xl_StocksAvailable1.Load(Xl_PurchaseOrderItemsForPlatforms1.StocksAvailable)
    End Sub

    Private Sub refrescaDeliveries()
        Xl_EciPlatformDeliveries1.Load(Xl_PurchaseOrderItemsForPlatforms1.Deliveries)
    End Sub

    Private Sub refrescaSumary()
        Dim oDeliveries = CurrentDeliveries()
        Dim iDeliveries As Integer = oDeliveries.Count
        Dim oDeptOrders = CurrentDeptOrders()
        Dim items As List(Of DTODeliveryItem) = oDeliveries.SelectMany(Function(y) y.items).ToList
        Dim pncs = oDeptOrders.SelectMany(Function(y) y.items).ToList
        Dim pendent = DTOAmt.Factory(pncs.Sum(Function(x) x.Amount.Eur))
        Dim linies = items.Count
        Dim sortint = DTOAmt.Factory(items.Sum(Function(x) x.Import.Eur))
        LabelSumary.Text = String.Format("Total {0} albarans, {1} linies, {2} sortit de {3} comandes, {4} linies y {5} pendents", iDeliveries, linies, sortint.Formatted, oDeptOrders.Count, pncs.Count, pendent.Formatted)
    End Sub

    Private Sub LoadDepts()
        Dim depts As New List(Of String)
        Dim sRegexPattern = "\/dep.\d*\/"
        Dim sDepts = _AllPurchaseOrders.Select(Function(x) System.Text.RegularExpressions.Regex.Match(x.concept, sRegexPattern)).ToList
        Dim sUniqueDepts = sDepts.GroupBy(Function(x) x.Value).Select(Function(y) y.First.Value).ToList
        For i As Integer = 0 To sUniqueDepts.Count - 1
            If sUniqueDepts(i) = "" Then sUniqueDepts(i) = "(sense departament)" Else sUniqueDepts(i) = sUniqueDepts(i).Replace("/", "")
        Next
        sUniqueDepts = sUniqueDepts.OrderBy(Function(x) x).ToList

        With ComboBoxDept
            .DataSource = sUniqueDepts
        End With
    End Sub

    Private Sub LoadDeliveries()
        Xl_EciPlatformDeliveries1.Load(_Groups, CurrentDeptOrders)
    End Sub

    Private Async Sub ExcelGrupsTransmisióToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("El Corte Ingles")
        With retval
            .AddColumn("Grup")
            .AddColumn("Centre")
            .AddColumn("Comanda")
            .AddColumn("Import", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
        End With
        Dim oDeliveries As List(Of DTODelivery) = Xl_PurchaseOrderItemsForPlatforms1.Deliveries
        Dim oGroups = Await FEB2.ECITransmGroups.All(exs)
        Dim rest As List(Of DTODelivery) = oDeliveries
        For Each oGroup As DTOECITransmGroup In oGroups
            If rest.Count = 0 Then Exit For
            Dim src As List(Of DTODelivery) = rest
            Dim oGroupDeliveries As List(Of DTODelivery) = DTOECITransmGroup.SelectedDeliveries(oGroup, src, rest)
            Dim oSortedGroupDeliveries = oGroupDeliveries.
                OrderBy(Function(x) x.customer.ref).
                OrderBy(Function(x) DTOEci.NumeroDeComanda(x)).
                ToList
            Dim oRow = retval.AddRow()
            oRow.AddCell(oGroup.Nom)
            For Each oDelivery In oSortedGroupDeliveries
                oRow = retval.AddRow()
                oRow.AddCell()
                oRow.AddCell(oDelivery.customer.ref)
                oRow.AddCell(DTOEci.NumeroDeComanda(oDelivery))
                oRow.AddCell(oDelivery.BaseImponible.Eur)
            Next
        Next

        If rest.Count > 0 Then
            Dim oRow = retval.AddRow()
            oRow.AddCell(String.Format("{0} albarans no s'han pogut assignar a cap grup de transmisió:", rest.Count))
            For Each oDelivery In rest
                oRow.AddCell()
                oRow.AddCell(oDelivery.customer.ref)
                oRow.AddCell(DTOEci.NumeroDeComanda(oDelivery))
                oRow.AddCell(oDelivery.BaseImponible.Eur)
            Next
        End If

        If Not UIHelper.ShowExcel(retval, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim CancelRequest As Boolean
        Dim oDeliveries = CurrentDeliveries()

        With Xl_ProgressBar1
            .Dock = DockStyle.Bottom
            .Visible = True
            .ShowStart(String.Format("Preparant per desar {0} albarans", oDeliveries.Count))
        End With

        If Await FEB2.AlbBloqueig.BloqueigAllStart(Current.Session.User, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oUpdatedDeliveries As New List(Of DTODelivery)
            For Each odelivery In oDeliveries
                Dim exs2 As New List(Of Exception)
                Dim id As Integer = Await FEB2.Delivery.Update(exs2, odelivery)
                If exs2.Count = 0 Then
                    odelivery.id = id
                    oUpdatedDeliveries.Add(odelivery)
                    Xl_ProgressBar1.ShowProgress(0, oDeliveries.Count - 1, oDeliveries.IndexOf(odelivery), String.Format("desant albarà {0} per {1}", odelivery.id, odelivery.customer.Ref), CancelRequest)
                    If CancelRequest Then Exit For
                Else
                    Dim sConcept As String = ""
                    Try
                        sConcept = odelivery.items.First.purchaseOrderItem.purchaseOrder.concept
                    Catch ex As Exception
                    End Try

                    Dim s = String.Format("error al desar albarà per {0} comanda {1}", odelivery.id, odelivery.customer.Ref, sConcept)
                    Xl_ProgressBar1.ShowProgress(0, oDeliveries.Count - 1, oDeliveries.IndexOf(odelivery), s, CancelRequest)
                    UIHelper.WarnError(exs2, s)
                    exs.AddRange(exs2)
                End If
            Next

            If exs.Count = 0 Then
                Dim sText As String = ""
                If oDeliveries.Count > 1 Then
                    sText = "registrats albarans " & oDeliveries.First.id & " - " & oDeliveries.Last.id.ToString
                Else
                    sText = "registrat albará " & oDeliveries.First.id.ToString
                End If

                Xl_ProgressBar1.ShowEnd(sText)
                MsgBox(sText, MsgBoxStyle.Information)
                Me.Close()
            Else
                Dim sText = String.Format("registrats correctament {0} de un total de {1} albarans", oUpdatedDeliveries.Count, oDeliveries.Count)
                Xl_ProgressBar1.ShowEnd(sText)
                UIHelper.WarnError(exs, "error al desar els albarans")
                Xl_ProgressBar1.Visible = False

            End If

        Else
            UIHelper.WarnError(exs, "Error al intentar bloquejar els albarans")
        End If

        If Not Await FEB2.AlbBloqueig.BloqueigAllEnd(Current.Session.User, DTOAlbBloqueig.Codis.ALB, exs) Then
            UIHelper.WarnError(exs, "Error al lliberar el bloqueig dels albarans")
        End If

    End Sub

    Private Sub Xl_PurchaseOrderItemsForPlatforms1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItemsForPlatforms1.AfterUpdate
        If _AllowEvents Then
            refrescaStocks()
            refrescaDeliveries()
            refrescaSumary()
        End If
    End Sub

    Private Async Function StocksAvailable(exs As List(Of Exception)) As Task(Of List(Of DTOStockAvailable))
        Dim retval As New List(Of DTOStockAvailable)
        If _Holding Is Nothing Then
            retval = Await FEB2.PurchaseOrders.StocksAvailableForPlatforms(exs, GlobalVariables.Emp, _Customer)
        Else
            retval = Await FEB2.PurchaseOrders.StocksAvailableForPlatforms(exs, GlobalVariables.Emp, _Holding)
        End If
        Return retval
    End Function

    Private Sub AgrupacionsPerTransmisióToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgrupacionsPerTransmisióToolStripMenuItem.Click
        Dim oFrm As New Frm_ECITransmGroups
        oFrm.Show()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub SelectOrder(oOrder As DTOPurchaseOrder)
        Xl_PurchaseOrderItemsForPlatforms1.selectOrder(oOrder)
    End Sub


    Private Sub ComboBoxDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDept.SelectedIndexChanged
        If _AllowEvents Then
            _AllowEvents = False
            LoadDeliveries()
            refresca()
            _AllowEvents = True
        End If
    End Sub

    Private Sub Xl_StocksAvailable1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_StocksAvailable1.ValueChanged
        Dim oStockAvailable As DTOStockAvailable = e.Argument
        Xl_PurchaseOrderItemsForPlatforms1.selectSku(oStockAvailable.Sku)
    End Sub

    Private Async Sub PreviewAlbaranesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviewAlbaranesToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDeliveries = CurrentDeliveries()
        Dim oStream As Byte() = Await FEB2.Deliveries.Pdf(exs, oDeliveries, False)
        If exs.Count = 0 Then
            UIHelper.ShowPdf(oStream)
        End If
    End Sub



    Private Sub Xl_EciPlatformDeliveries1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_EciPlatformDeliveries1.AfterUpdate
        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub Xl_EciPlatformDeliveries1_onOrderSelected(sender As Object, e As MatEventArgs) Handles Xl_EciPlatformDeliveries1.onOrderSelected
        SelectOrder(e.Argument)
    End Sub


End Class