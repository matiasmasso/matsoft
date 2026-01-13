Public Class Frm_ConsumerTicketFactory
    Private _ConsumerTicket As DTOConsumerTicket
    Private _AmazonSellerOrder As AmazonSellerOrder
    Private _ExportCod As DTOInvoice.ExportCods = DTOInvoice.ExportCods.nacional

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOConsumerTicket)
        MyBase.New()
        Me.InitializeComponent()
        _ConsumerTicket = value
    End Sub

    Public Sub New(oAmazonSellerOrder As AmazonSellerOrder)
        MyBase.New
        InitializeComponent()
        _AmazonSellerOrder = oAmazonSellerOrder
        _ConsumerTicket = DTOConsumerTicket.Factory(Current.Session.User, _AmazonSellerOrder.MarketPlace)
    End Sub

    Private Async Sub Frm_ConsumerTicket_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _AmazonSellerOrder Is Nothing Then
            If _ConsumerTicket.IsNew Then
                If _ConsumerTicket.MarketPlace Is Nothing Then
                    Me.Text = "Nou ticket de consumidor"
                Else
                    Me.Text = "Nou ticket de " & _ConsumerTicket.MarketPlace.Nom
                End If
                DateTimePicker1.Value = Today

                Await Xl_PurchaseOrderItems1.Load(_ConsumerTicket.PurchaseOrder, _ConsumerTicket.PurchaseOrder.customer)
            Else
                If FEB2.ConsumerTicket.Load(exs, _ConsumerTicket) Then
                    Await refresca()
                Else
                    UIHelper.WarnError(exs)
                    Me.Close()
                End If
            End If
        Else
            UIHelper.ToggleProggressBar(PanelButtons, True)
            Me.Text = "Nou ticket de Amazon Seller"
            Await DisplayAmazonSellerOrder(_AmazonSellerOrder)
            UIHelper.ToggleProggressBar(PanelButtons, False)
        End If

        _AllowEvents = True


    End Sub

    Private Async Function refresca() As Task
        With _ConsumerTicket
            If .MarketPlace Is Nothing Then
                Me.Text = String.Format("Ticket {0} de consumidor", .Id)
            Else
                Me.Text = String.Format("Ticket {0} de {1}", .Id, .MarketPlace.Nom)
            End If
            DateTimePicker1.Value = .Fch
            TextBoxComanda.Text = .PurchaseOrder.concept
            TextBoxNom.Text = .Nom
            TextBoxCognom1.Text = .Cognom1
            TextBoxCognom2.Text = .Cognom2
            TextBoxAdr.Text = .Address.Text
            Xl_LookupZip1.Load(.Address.Zip)
            TextBoxTel.Text = .Tel
            Await Xl_PurchaseOrderItems1.Load(.PurchaseOrder)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With

    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged,
        TextBoxComanda.TextChanged,
         TextBoxNom.TextChanged,
          TextBoxCognom1.TextChanged,
           TextBoxCognom2.TextChanged,
            TextBoxAdr.TextChanged,
             Xl_LookupZip1.AfterUpdate,
              TextBoxTel.TextChanged,
               Xl_PurchaseOrderItems1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ConsumerTicket
            With .PurchaseOrder
                .emp = GlobalVariables.Emp
                .fch = DateTimePicker1.Value
                .concept = TextBoxComanda.Text
                .items = Xl_PurchaseOrderItems1.Items
                .docFile = Xl_DocFile1.Value
            End With
            .Fch = DateTimePicker1.Value
            .Nom = TextBoxNom.Text
            .Cognom1 = TextBoxCognom1.Text
            .Cognom2 = TextBoxCognom2.Text
            .Address = DTOAddress.Factory(_ConsumerTicket, DTOAddress.Codis.Fiscal)
            .Address.Text = TextBoxAdr.Text
            .Address.Zip = Xl_LookupZip1.Zip
            .Tel = TextBoxTel.Text

            .SetCca()
        End With

        _ConsumerTicket.Delivery = _ConsumerTicket.Deliver(Current.Session.User, DTOCustomer.CashCodes.credit)
        With _ConsumerTicket.Delivery
            .mgz = GlobalVariables.Emp.Mgz
            .portsCod = DTOCustomer.PortsCodes.pagats
            .exportCod = _ExportCod
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.ConsumerTicket.Update(exs, _ConsumerTicket) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ConsumerTicket))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.ConsumerTicket.Delete(exs, _ConsumerTicket) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ConsumerTicket))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            '    or this tells us if it is an Outlook attachment drop
            e.Effect = DragDropEffects.Copy
        Else
            '    or none of the above
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim exs As New List(Of Exception)

        Dim oDocFiles As New List(Of DTODocFile)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            If oDocFiles.Count > 0 Then
                DisplayAmazonSellerOrder(oDocFiles.First())
            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub

    Private Async Sub DisplayAmazonSellerOrder(oDocfile As DTODocFile)
        Dim exs As New List(Of Exception)
        Dim lines = ReadPdf(oDocfile, exs)
        If exs.Count = 0 Then
            Dim oCountries = Await FEB2.Countries.All(DTOLang.ESP, exs)
            If exs.Count = 0 Then
                Dim oAmazonOrder As New AmazonSellerOrder(oDocfile, lines, oCountries)
                Await DisplayAmazonSellerOrder(oAmazonOrder)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "error al llegitr el Pdf")
        End If
    End Sub

    Private Async Function DisplayAmazonSellerOrder(src As AmazonSellerOrder) As Task
        Dim exs As New List(Of Exception)
        Dim warnColor = Color.LightSalmon


        With src
            Await Xl_DocFile1.Load(.Docfile)

            If String.IsNullOrEmpty(.OrderNum) Then
                TextBoxComanda.BackColor = warnColor
            Else
                TextBoxComanda.Text = .OrderNum
            End If

            Dim nomSegments = .Nom.Trim().Split(" ").ToList
            If nomSegments.Count > 0 Then
                TextBoxNom.Text = nomSegments.First
                If nomSegments.Count > 1 Then
                    TextBoxCognom1.Text = nomSegments(1)
                    If nomSegments.Count > 2 Then
                        TextBoxCognom2.Text = src.Nom.Substring(src.Nom.IndexOf(nomSegments(2)))
                    End If
                End If
            End If

            If nomSegments.Count <> 3 Then TextBoxCognom2.BackColor = warnColor

            TextBoxAdr.Text = .Address

            If .Country Is Nothing Then
                Xl_LookupZip1.BackColor = warnColor
            Else
                Dim zipcod = src.Location.Substring(src.Location.LastIndexOf(" ") + 1)
                Dim provinciaIdx = src.Location.IndexOf(",")
                Dim location As String = ""
                If provinciaIdx > 0 Then
                    location = src.Location.Substring(0, provinciaIdx).Trim()
                ElseIf src.Location.LastIndexOf(" ") > 0 Then
                    location = src.Location.Substring(0, src.Location.LastIndexOf(" ")).Trim()
                End If
                'location = If(provinciaIdx > 0, src.Location.Substring(0, provinciaIdx).Trim(), src.Location.Substring(0, src.Location.LastIndexOf(" ")).Trim())
                Dim oZips = Await FEB2.Zips.All(exs, src.Country, zipcod)
                If exs.Count = 0 Then
                    Select Case oZips.Count
                        Case 0
                            Xl_LookupZip1.BackColor = warnColor
                        Case 1
                            Dim oZip = oZips.First()
                            _ExportCod = oZip.ExportCod
                            Xl_LookupZip1.Load(oZip)
                            If location <> oZip.Location.nom Then Xl_LookupZip1.BackColor = warnColor
                        Case Else
                            Dim oZip = oZips.FirstOrDefault(Function(x) x.location.nom.ToLower = location.ToLower)
                            If oZip Is Nothing Then
                                oZip = oZips.First()
                                Xl_LookupZip1.BackColor = warnColor
                            End If
                            _ExportCod = oZip.ExportCod
                            Xl_LookupZip1.Load(oZip)
                    End Select
                Else
                    Xl_LookupZip1.BackColor = warnColor
                End If
            End If

            Dim oAmazon = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.amazon)

            Dim oItems As New List(Of DTOPurchaseOrderItem)
            For Each item In src.Items
                Dim oSkus = Await FEB2.CustomerProducts.FromRef(exs, oAmazon, item.Asin)
                If exs.Count = 0 AndAlso oSkus IsNot Nothing AndAlso oSkus.Count > 0 Then
                    Dim oSku = oSkus.First.sku
                    FEB2.ProductSku.Load(oSku, exs,, GlobalVariables.Emp.Mgz)

                    Dim oPrice As DTOAmt = Nothing
                    If _ExportCod = DTOInvoice.ExportCods.nacional Then
                        Dim dcIvaTipus = DTOTax.closestTipus(DTOTax.Codis.iva_Standard, DateTimePicker1.Value)
                        Dim dcCost = Math.Round(oSku.rrpp.Eur / ((100 + dcIvaTipus) / 100), 2)
                        oPrice = DTOAmt.Factory(dcCost)
                    Else
                        oPrice = oSku.rrpp
                    End If

                    Dim oItem = DTOPurchaseOrderItem.Factory(Nothing, oSku, item.Qty, oPrice, 0)
                    oItems.Add(oItem)
                Else
                    Xl_PurchaseOrderItems1.BackgroundColor = warnColor
                End If
            Next

            If src.Portes <> 0 Then
                Dim oSku = Await FEB2.ProductSku.Find(exs, DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport).Guid)
                If oSku IsNot Nothing Then
                    Dim oPrice = DTOAmt.Factory(src.Portes)
                    If _ExportCod = DTOInvoice.ExportCods.nacional Then
                        Dim dcIvaTipus = DTOTax.closestTipus(DTOTax.Codis.iva_Standard, DateTimePicker1.Value)
                        Dim dcCost = Math.Round(src.Portes / ((100 + dcIvaTipus) / 100), 2)
                        oPrice = DTOAmt.Factory(dcCost)
                    End If
                    Dim oItem = DTOPurchaseOrderItem.Factory(Nothing, oSku, 1, oPrice, 0)
                    oItems.Add(oItem)
                End If
            End If

            _ConsumerTicket.PurchaseOrder.items = oItems
            Await Xl_PurchaseOrderItems1.Load(_ConsumerTicket.PurchaseOrder)

            Dim oBaseImponible As DTOAmt = Xl_PurchaseOrderItems1.GetTotals()
            TextBoxBaseImponible.Text = oBaseImponible.CurFormatted()
            Dim oTotal = oBaseImponible.clone()
            If _ExportCod = DTOInvoice.ExportCods.nacional Then
                Dim dcIvaTipus = DTOTax.closest(DTOTax.Codis.iva_Standard, DateTimePicker1.Value).tipus
                LabelIvaTipus.Text = String.Format("IVA {0}%", dcIvaTipus)
                Dim oIvaValue As DTOAmt = oBaseImponible.times(dcIvaTipus / 100)
                TextBoxIva.Text = oIvaValue.CurFormatted
                oTotal = oBaseImponible.add(oIvaValue)
            End If

            If oTotal Is Nothing Then
                TextBoxTotal.BackColor = If(.Total = 0, Color.LightGreen, warnColor)
            Else
                TextBoxTotal.Text = oTotal.CurFormatted()
                TextBoxTotal.BackColor = If(src.Total = oTotal.Eur, Color.LightGreen, warnColor)
            End If

            ButtonOk.Enabled = True
        End With

    End Function



    Private Function ReadPdf(oDocfile As DTODocFile, exs As List(Of Exception)) As List(Of String)
        Dim retval As New List(Of String)
        Dim src = LegacyHelper.iTextPdfHelper.readText(oDocfile.stream, exs)
        If exs.Count = 0 Then
            retval = src.Split(vbLf).ToList
        End If
        Return retval
    End Function


    Private Sub ImportarPdfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarPdfToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "documents pdf|*.pdf|(tots els fitxers)|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim oDocfile = LegacyHelper.DocfileHelper.Factory(.FileName, exs)
                If exs.Count = 0 Then
                    DisplayAmazonSellerOrder(oDocfile)
                Else
                    UIHelper.WarnError(exs, "error al importar document")
                End If
            End If
        End With
    End Sub


End Class