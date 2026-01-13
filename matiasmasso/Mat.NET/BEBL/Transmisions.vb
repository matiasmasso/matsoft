Imports System.Xml

Public Class Transmisio


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTransmisio
        Dim retval As DTOTransmisio = TransmisioLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYea As Integer, iId As Integer) As DTOTransmisio
        If iYea = 0 Then iYea = DTO.GlobalVariables.Today().Year
        Dim retval As DTOTransmisio = TransmisioLoader.FromNum(oEmp, iYea, iId)
        Return retval
    End Function

    Shared Function Update(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Boolean
        Return TransmisioLoader.Update(oTransmisio, exs)
    End Function


    Shared Function Delete(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Boolean
        Return TransmisioLoader.Delete(oTransmisio, exs)
    End Function

#End Region

    Shared Function Send(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim oDeliveries As List(Of DTODelivery) = DeliveriesLoader.PendentsDeTransmetre(oEmp.Mgz)
            If oDeliveries.Count > 0 Then
                Dim oTransmisio = DTOTransmisio.Factory(oEmp, oEmp.Mgz, oDeliveries)
                If BEBL.Transmisio.Update(oTransmisio, exs) Then
                    retval = BEBL.Transmisio.Send(oTransmisio, exs)
                Else
                    exs.Add(New Exception("BEBL.Transmisions.Send(3): Error al generar la Transmisió a VIVACE"))
                End If
            Else
                retval = True
            End If

        Catch ex As Exception
            exs.Add(New Exception("BEBL.Transmisions.Send(1): Error al generar la Transmisió a VIVACE"))
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function Send(oTransmisio As DTOTransmisio, exs As List(Of Exception), Optional sTo As String = "") As Boolean
        Dim retval As Boolean
        Try
            EmpLoader.Load(oTransmisio.Emp)
            Dim oXmlDocument As XmlDocument = Nothing
            If BEBL.Transmisio.DOM(oTransmisio, oXmlDocument, exs) Then
                Dim oMailMsg = MailMsg.Factory(oTransmisio.Emp)
                With oMailMsg
                    If sTo > "" Then
                        For Each recipìent In sTo.Split(";")
                            .Add(MailMsg.Recipients.To, recipìent)
                        Next
                    Else
                        Dim oSubscriptors = Subscriptors(oTransmisio)
                        .AddRange(MailMsg.Recipients.To, oSubscriptors)
                    End If
                    .Subject = Subject(oTransmisio)
                    If .DownloadBody(UrlHelper.FromSegments(True, "mail", "transmisio", oTransmisio.Guid.ToString()), exs) Then
                        Dim sFilename = String.Format("M+O.{0}.{1} dades transmisio.xml", oTransmisio.fch.Year, oTransmisio.id.ToString())
                        If .AttachXmlDocument(oXmlDocument, sFilename, exs) Then
                            retval = .Send(exs)
                        End If
                    End If
                End With
            End If

        Catch ex As Exception
            exs.Add(New Exception("BEBL.Transmisions.Send(2): Error al enviar la transmisio"))
        End Try

        Return retval
    End Function

    Shared Function SendFrom(oEmp As DTOEmp) As System.Net.Mail.MailAddress
        Dim retval = New System.Net.Mail.MailAddress(oEmp.MailboxUsr, "M+O MATIAS MASSO, S.A.")
        Return retval
    End Function

    Shared Function Subscriptors(oTransmisio As DTOTransmisio) As List(Of DTOSubscriptor)
        Dim oOrg = oTransmisio.Emp.Org
        Dim oMgz = oTransmisio.Emp.mgz
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Transmisio)
        Dim oSubscriptors = BEBL.Subscription.Subscriptors(oSsc)
        Dim retval = oSubscriptors.Where(Function(x) (x.Contacts.Any(Function(y) y.Equals(oMgz) Or y.Equals(oOrg))))
        Return retval.ToList
    End Function

    Shared Function Subject(oTransmisio As DTOTransmisio) As String
        Dim retval = String.Format("Transmisió num.{0}", oTransmisio.Id.ToString())
        Return retval
    End Function

    Shared Function XmlFileSource(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As String
        Dim retval As String = ""
        Dim oXmlDoc As New XmlDocument
        If DOM(oTransmisio, oXmlDoc, exs) Then
            retval = oXmlDoc.OuterXml()
        End If
        Return retval
    End Function

    Shared Function DOM(oTransmisio As DTOTransmisio, ByRef oXmlDoc As XmlDocument, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try

            Dim oEmp = oTransmisio.Emp
            EmpLoader.Load(oEmp)
            Dim oOrg = oEmp.Org
            ContactLoader.Load(oOrg)
            Dim oMgz = oEmp.Mgz
            MgzLoader.Load(oMgz)

            Dim Doc As XmlElement
            Dim NodeAlbs As XmlElement
            Dim NodeAlb As XmlElement
            Dim node As XmlElement

            oXmlDoc = New XmlDocument
            Doc = oXmlDoc.CreateElement("DOCUMENT")
            Doc.SetAttribute("TYPE", "SALIDASALMACEN")

            'A cambiar a Versió 2.0=====================================================================
            'Doc.SetAttribute("VERSION", "1.5")
            'Doc.SetAttribute("VERSION", "2.1")
            Doc.SetAttribute("VERSION", "2.2") '21/10/22 afegeix NADBY i NADDP a ALBARAN/DESTINO/NOMBRE
            '===========================================================================================

            Doc.SetAttribute("NUMERO", oTransmisio.Fch.Year.ToString & "." & oTransmisio.Id.ToString())
            Doc.SetAttribute("FECHA", Format(oTransmisio.Fch.BcnDateTime, "dd/MM/yyyy"))
            oXmlDoc.AppendChild(Doc)

            node = oXmlDoc.CreateElement("REMITE")
            node.SetAttribute("NIF", oOrg.PrimaryNifValue())
            node.SetAttribute("NOMBRE", oOrg.Nom)
            Doc.AppendChild(node)

            node = oXmlDoc.CreateElement("DESTINO")
            node.SetAttribute("NIF", oMgz.PrimaryNifValue())
            node.SetAttribute("NOMBRE", oMgz.Nom)
            Doc.AppendChild(node)

            NodeAlbs = oXmlDoc.CreateElement("ALBARANES")
            Doc.AppendChild(NodeAlbs)

            For Each itm As DTODelivery In oTransmisio.Deliveries
                Try
                    DeliveryLoader.Load(itm)

                    NodeAlb = oXmlDoc.CreateElement("ALBARAN")
                    NodeAlb.SetAttribute("NUMERO", itm.Formatted())
                    NodeAlb.SetAttribute("FECHA", Format(itm.Fch, "dd/MM/yyyy"))

                    'Requested on 03/11/2021 by Vivace to generate El Corte Ingles Csv new file request =====================================================================

                    Dim oFirstItem As DTODeliveryItem = itm.Items.FirstOrDefault()
                    If oFirstItem IsNot Nothing Then
                        If oFirstItem.PurchaseOrderItem IsNot Nothing Then
                            If oFirstItem.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                                Dim dtFchEntrega = oFirstItem.PurchaseOrderItem.PurchaseOrder.FchDeliveryMin
                                If dtFchEntrega <> Nothing Then
                                    NodeAlb.SetAttribute("FECHAENTREGA", Format(dtFchEntrega, "dd/MM/yyyy"))
                                End If
                            End If
                        End If
                    End If

                    'A suprimir=====================================================================
                    'NodeAlb.SetAttribute("VALORADO", IIf(itm.Valorado = True, "SI", "NO"))
                    'A suprimir=====================================================================

                    Dim docFrag As XmlDocumentFragment
                    For Each node In BEBL.Delivery.DOM(itm).DocumentElement.ChildNodes
                        docFrag = oXmlDoc.CreateDocumentFragment()
                        docFrag.InnerXml = node.OuterXml
                        NodeAlb.AppendChild(docFrag)
                    Next node

                    NodeAlbs.AppendChild(NodeAlb)

                Catch ex As Exception
                    exs.Add(New Exception(String.Format("Error al generar el document Xml de l'albarà {0} transmisió {1}", itm.Id.ToString, oTransmisio.Id.ToString())))
                    exs.Add(ex)
                End Try
            Next itm

            Dim NodeCustomLabels As XmlElement = oXmlDoc.CreateElement("CustomLabels")

            Dim oCarrefour = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Carrefour)
            If oTransmisio.Deliveries.Any(Function(x) BEBL.Customer.CcxOrMe(x.Customer).Equals(oCarrefour)) Then
                Dim oCustomDeliveries = oTransmisio.Deliveries.Where(Function(x) BEBL.Customer.CcxOrMe(x.Customer).Equals(oCarrefour)).ToList
                For Each item In BEBL.Carrefour.LogisticItems(oCustomDeliveries)
                    Dim NodeCustomLabel As XmlElement = oXmlDoc.CreateElement("CustomLabel")
                    NodeCustomLabel.SetAttribute("Type", "Carrefour")
                    NodeCustomLabels.AppendChild(NodeCustomLabel)

                    NodeCustomLabel.SetAttribute("Albaran", item.Albaran)
                    NodeCustomLabel.SetAttribute("Linea", item.Linea)
                    NodeCustomLabel.SetAttribute("SupplierCode", item.SupplierCode)
                    NodeCustomLabel.SetAttribute("Section", item.Section)
                    NodeCustomLabel.SetAttribute("Implantation", item.Implantation)
                    NodeCustomLabel.SetAttribute("MadeIn", item.MadeIn)
                    NodeCustomLabel.SetAttribute("Reference", item.SkuCustomRef)
                    NodeCustomLabel.SetAttribute("Color", item.SkuColor)
                    NodeCustomLabel.SetAttribute("UnitsPCB", item.UnitsPerInnerBox)
                    NodeCustomLabel.SetAttribute("SPCB", item.UnitsPerMasterBox)
                    NodeCustomLabel.SetAttribute("BoxHeight", item.Height)
                    NodeCustomLabel.SetAttribute("BoxWidth", item.Width)
                    NodeCustomLabel.SetAttribute("BoxLength", item.Length)
                    NodeCustomLabel.SetAttribute("BoxWeight", String.Format("{0} Kg", item.Weight))
                    NodeCustomLabel.SetAttribute("MasterBarcode", item.MasterBarCode)
                    NodeCustomLabel.SetAttribute("SkuDsc", item.SkuDsc)
                Next

            End If

            If NodeCustomLabels.HasChildNodes Then
                Doc.AppendChild(NodeCustomLabels)
            End If

            If exs.Count = 0 Then
                retval = True
            Else
                exs.Add(New Exception(String.Format("Errors detectats al generar el document Xml de la transmisió {0}", oTransmisio.Id.ToString())))
            End If
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al generar el document Xml de la transmisió {0}", oTransmisio.Id.ToString())))
        End Try
        Return retval
    End Function

    Shared Function DeliveriesPdfStream(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As Byte()
        Dim retval As Byte() = Nothing
        Try
            Dim items As List(Of DTODelivery) = oTransmisio.Deliveries
            If items.Count > 0 Then
                Dim oSortedItems As List(Of DTODelivery) = BEBL.ElCorteIngles.ECISortedDeliveries(items)
                Dim oPdf As New BEBL.PdfDelivery(oSortedItems, False)
                oPdf.Print(DTODelivery.CodsValorat.ForceFalse, exs)
                retval = oPdf.Stream(exs)
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function Excel(oTransmisio As DTOTransmisio, exs As List(Of Exception)) As MatHelper.Excel.Book
        Dim filename = String.Format("Transmisió {0}.xlsx", oTransmisio.Id)
        Dim retval As New MatHelper.Excel.Book(filename)
        Dim oDeliveries As List(Of DTODelivery) = BEBL.Deliveries.All(oTransmisio)
        retval.Sheets.Add(ExcelDeliveriesSheet(oDeliveries, exs))
        retval.Sheets.Add(ExcelSkusSheet(oDeliveries, exs))
        Return retval
    End Function

    Shared Function ExcelSkusSheet(oDeliveries As List(Of DTODelivery), exs As List(Of Exception)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("producte")
        With retval
            .AddColumn("unitats", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("ref M+O")
            .AddColumn("marca")
            .AddColumn("categoria")
            .AddColumn("producte")
        End With

        Dim items = oDeliveries.SelectMany(Function(x) x.Items).
        GroupBy(Function(g) New With {Key g.Sku.Guid, g.Sku.Id, g.Sku.brandNom(), g.Sku.categoryNom(), g.Sku.NomCurt()}).
        Select(Function(group) New With {
        .Guid = group.Key.Guid,
        .Qty = group.Sum(Function(y) y.Qty),
        .Id = group.Key.Id,
        .BrandNom = group.Key.brandNom,
        .CategoryNom = group.Key.categoryNom,
        .SkuNom = group.Key.NomCurt
        }).
        OrderBy(Function(a) a.SkuNom).
        OrderBy(Function(b) b.CategoryNom).
        OrderBy(Function(c) c.BrandNom).
        ToList()

        Try
            Dim oRow As MatHelper.Excel.Row

            For Each item In items
                oRow = retval.AddRow
                With item
                    oRow.AddCell(.Qty)
                    oRow.AddCell(.Id)
                    oRow.AddCell(.BrandNom)
                    oRow.AddCell(.CategoryNom)
                    oRow.AddCell(.SkuNom)
                End With
            Next
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval

    End Function
    Shared Function ExcelDeliveriesSheet(oDeliveries As List(Of DTODelivery), exs As List(Of Exception)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("albarans")
        With retval
            .AddColumn("albará", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("destinatari")
            .AddColumn("localitat")
            .AddColumn("import", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
        Try
            Dim oRow As MatHelper.Excel.Row

            For Each item As DTODelivery In oDeliveries
                oRow = retval.AddRow
                With item
                    oRow.AddCell(.Id)
                    oRow.AddCell(.Fch)
                    oRow.AddCell(.Customer.Nom & " " & .Customer.Ref)
                    oRow.AddCell(DTODelivery.deliveryLocationNom(item))
                    oRow.AddCell(DTOAmt.EurOrDefault(.Import))
                End With
            Next
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval

    End Function

End Class

Public Class Transmisions

    Shared Function All(oMgz As DTOMgz) As List(Of DTOTransmisio)
        Dim retval As List(Of DTOTransmisio) = TransmisionsLoader.Headers(oMgz)
        Return retval
    End Function

    Shared Function HoldingHeaders(oHolding As DTOHolding, daysFrom As Integer) As List(Of DTOTransmisio)
        Return TransmisionsLoader.HoldingHeaders(oHolding, daysFrom)
    End Function

    Shared Function Orders(transmGuids As List(Of Guid)) As List(Of DTOPurchaseOrder)
        Return TransmisionsLoader.Orders(transmGuids)
    End Function

End Class