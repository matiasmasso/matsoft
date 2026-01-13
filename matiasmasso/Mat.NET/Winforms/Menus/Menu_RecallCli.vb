Public Class Menu_RecallCli

    Inherits Menu_Base

    Private _RecallClis As List(Of DTORecallCli)
    Private _RecallCli As DTORecallCli

    Public Sub New(ByVal oRecallClis As List(Of DTORecallCli))
        MyBase.New()
        _RecallClis = oRecallClis
        If _RecallClis IsNot Nothing Then
            If _RecallClis.Count > 0 Then
                _RecallCli = _RecallClis.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oRecallCli As DTORecallCli)
        MyBase.New()
        _RecallCli = oRecallCli
        _RecallClis = New List(Of DTORecallCli)
        If _RecallCli IsNot Nothing Then
            _RecallClis.Add(_RecallCli)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Labels())
        MyBase.AddMenuItem(MenuItem_MailVivace())
        MyBase.AddMenuItem(MenuItem_Pdc())
        MyBase.AddMenuItem(MenuItem_Delivery())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _RecallClis.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Labels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Etiquetes"
        oMenuItem.DropDownItems.Add(MenuItem_LabelsPdf)
        oMenuItem.DropDownItems.Add(MenuItem_LabelsOutlook)
        Return oMenuItem
    End Function

    Private Function MenuItem_MailVivace() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email a Vivace"
        AddHandler oMenuItem.Click, AddressOf Do_MailVivace
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda"
        oMenuItem.Enabled = _RecallClis.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Pdc
        Return oMenuItem
    End Function

    Private Function MenuItem_Delivery() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Albarà"
        oMenuItem.Enabled = _RecallClis.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Delivery
        Return oMenuItem
    End Function

    Private Function MenuItem_LabelsPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        AddHandler oMenuItem.Click, AddressOf Do_LabelsPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_LabelsOutlook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enviar per Outlook"
        AddHandler oMenuItem.Click, AddressOf Do_LabelsOutlook
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RecallCli(_RecallCli)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_LabelsPdf()
        Dim exs As New List(Of Exception)
        Dim oStream As Byte() = LegacyHelper.PdfRecallLabel.Factory(_RecallCli)
        Dim oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oStream)
        If Not Await UIHelper.ShowStreamAsync(exs, oDocfile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_LabelsOutlook()
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            Dim oPdfStream As Byte() = LegacyHelper.PdfRecallLabel.Factory(_RecallCli)
            Dim oMailMessage As DTOMailMessage = Await FEB2.RecallCli.MailMessage(_RecallCli, oPdfStream, exs)
            If exs.Count = 0 Then
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If


    End Sub


    Private Async Sub Do_MailVivace()
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            Dim oPdfStream As Byte() = LegacyHelper.PdfRecallLabel.Factory(_RecallCli)
            Dim oMailMessage = Await FEB2.RecallCli.MailMessageToVivace(_RecallCli, oPdfStream, exs)
            If exs.Count = 0 Then
                If Await OutlookHelper.Send(oMailMessage, exs) Then
                    If FEB2.RecallCli.Load(exs, _RecallCli) Then
                        _RecallCli.FchVivace = Now
                        If Await FEB2.RecallCli.Update(exs, _RecallCli) Then
                            RefreshRequest(Me, MatEventArgs.Empty)
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
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

    Private Async Sub Do_Delivery(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            Dim oDelivery As DTODelivery = Nothing
            If _RecallCli.Delivery IsNot Nothing Then
                oDelivery = FEB2.Delivery.Factory(exs, _RecallCli.PurchaseOrder, GlobalVariables.Emp.Mgz, Today, Current.Session.User)
                With oDelivery
                    .Address = New DTOAddress()
                    .Address.Text = _RecallCli.Address
                    .Address.Zip = Await FEB2.Zip.FromZipCod(_RecallCli.Country, _RecallCli.Zip, exs)
                End With
            Else
                oDelivery = _RecallCli.Delivery
            End If

            Dim oCustomer As DTOCustomer = oDelivery.Contact
            If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                AddHandler oFrm.AfterUpdate, AddressOf on_delivery
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub on_delivery(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            If Await FEB2.RecallCli.Update(exs, _RecallCli) Then
                RefreshRequest(sender, e)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        _RecallCli.Delivery = e.Argument
    End Sub

    Private Async Sub Do_Pdc()
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            If _RecallCli.PurchaseOrder Is Nothing Then
                Dim oSkus = New List(Of DTOProductSku)
                For Each oProduct As DTORecallProduct In _RecallCli.Products
                    Dim oSku As DTOProductSku = oProduct.Sku
                    If Not oSkus.Any(Function(x) x.Equals(oSku)) Then
                        oSkus.Add(oSku)
                    End If
                Next

                Dim oUser As DTOUser = Current.Session.User
                If FEB2.Customer.Load(_RecallCli.Customer, exs) Then
                    Dim oPurchaseOrder = DTOPurchaseOrder.Factory(_RecallCli.Customer, oUser)
                    With oPurchaseOrder
                        .Source = DTOPurchaseOrder.Sources.no_Especificado
                        .Concept = _RecallCli.Recall.Nom & " recall"
                        .Items = New List(Of DTOPurchaseOrderItem)
                        For Each oSku As DTOProductSku In oSkus
                            Dim pSku = oSku
                            Dim item As New DTOPurchaseOrderItem()
                            With item
                                .PurchaseOrder = oPurchaseOrder
                                .Sku = pSku
                                .Qty = _RecallCli.Products.Where(Function(x) x.Sku.Equals(pSku)).Count
                                .Pending = .Qty
                                .ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC
                            End With
                            .Items.Add(item)
                        Next
                    End With

                    Dim pOrder = Await FEB2.PurchaseOrder.Update(exs, oPurchaseOrder)
                    If exs.Count = 0 Then
                        oPurchaseOrder = pOrder
                        If FEB2.RecallCli.Load(exs, _RecallCli) Then
                            _RecallCli.PurchaseOrder = oPurchaseOrder
                            If Await FEB2.RecallCli.Update(exs, _RecallCli) Then
                                RefreshRequest(Me, New MatEventArgs(_RecallCli))
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs, "Error al redactar la comanda:")
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                Dim oFrm As New Frm_PurchaseOrder(_RecallCli.PurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub



    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.RecallCli.Delete(exs, _RecallClis.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

