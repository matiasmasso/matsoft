Imports LegacyHelper

Public Class ElCorteIngles



    Shared Function Central() As DTOCustomer
        Static retval As DTOCustomer
        If retval Is Nothing Then
            retval = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
            BEBL.Customer.Load(retval)
        End If
        Return retval
    End Function

    Shared Sub GetDetailsFromPdc(ByVal oPurchaseOrder As DTOPurchaseOrder, ByRef sPedido As String, ByRef sCentro As String, ByRef sDepartamento As String, Optional ByRef sNumProveedor As String = "")
        '30560101/ctro.0050/dep.053/prov.01-030825
        Dim sPdd() As String = oPurchaseOrder.Concept.Split("/")
        If sPdd.Length = 4 Then
            sPedido = sPdd(0)
            If sPdd(1).Length >= 5 Then
                sCentro = sPdd(1).Substring(5)
            End If
            sDepartamento = sPdd(2).Replace("dep.", "")
            sNumProveedor = sPdd(3).Replace("prov.", "")
        End If

    End Sub

    Shared Function OrdersModel(year As Integer) As List(Of DTO.Models.ElCorteInglesOrderModel)
        Return ElCorteInglesLoader.OrdersModel(year)
    End Function

    Shared Function Belongs(oContact As DTOContact) As Boolean
        Dim retval As Boolean
        If oContact.Nom > "" Then
            retval = oContact.Nom.Contains("CORTE INGLES") Or oContact.Nom.Contains("HIPERCOR")
        ElseIf oContact.FullNom > "" Then
            retval = oContact.FullNom.Contains("CORTE INGLES") Or oContact.FullNom.Contains("HIPERCOR")
        End If
        Return retval
    End Function

    Shared Function Platforms(oContact As DTOContact) As List(Of DTOCustomerPlatform)
        Static oLastContact As DTOContact
        Static retval As List(Of DTOCustomerPlatform)
        If retval Is Nothing Or oContact.UnEquals(oLastContact) Then
            retval = CustomerPlatformsLoader.All(oContact)
        End If
        Return retval
    End Function

    Shared Function GetOrderConceptFromEdiversa(oFile As DTOEdiversaOrder) As String
        Dim sCentro As String = oFile.Centro
        Dim sDepto As String = oFile.Departamento
        Customer.Load(oFile.Customer)
        Dim sNumProveidor As String = DirectCast(oFile.Customer, DTOCustomer).SuProveedorNum
        Dim sComanda As String = oFile.DocNum

        Dim retval As String = String.Format("{0}/ctro.{1}/dep.{2}/prov.{3}", sComanda, sCentro, sDepto, sNumProveidor)
        Return retval
    End Function

    Shared Function Descataloga(exs As List(Of Exception), oGuids As List(Of Guid))
        Return ElCorteInglesLoader.Descataloga(exs, oGuids)
    End Function

    Shared Function Recataloga(exs As List(Of Exception), oGuids As List(Of Guid))
        Return ElCorteInglesLoader.Descataloga(exs, oGuids, reverse:=True)
    End Function

    Shared Function AlbHeaderDoc(ByVal oDeliveries As List(Of DTODelivery)) As DTODoc
        Dim oDoc As New DTODoc(DTODoc.Estilos.factura, Central.Lang, DTOCur.Eur, False)
        Dim oFirstDelivery As DTODelivery = oDeliveries.First
        Dim tmp As String

        With oDoc
            BEBL.Contact.Load(oFirstDelivery.Platform)
            If oFirstDelivery.Platform.Nom > "" Then
                .dest.Add(oFirstDelivery.Platform.Nom)
            End If
            If oFirstDelivery.Platform.NomComercial > "" Then
                .dest.Add(oFirstDelivery.Platform.NomComercial)
            End If
            .dest.Add(oFirstDelivery.Platform.Address.Text)
            .dest.Add(oFirstDelivery.Platform.Address.Zip.FullNom(Central.Lang))
            '.dest.Add(oFirstDelivery.Address.Text)
            '.dest.Add(oFirstDelivery.Address.Zip.FullNom(Central.Lang))
            .fch = DTO.GlobalVariables.Today()

            tmp = ""
            .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.regular))


            tmp = "ALBARAN DE ENTREGA"
            .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.underline))


            tmp = ""
            .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.regular))

            tmp = "PROVEEDOR Nº: " & Central.SuProveedorNum
            .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.regular))

            Dim sDepartamento As String = GetDepartamentoFromFirstAlbPdd(oDeliveries)
            If sDepartamento > "" Then
                tmp = "DEPARTAMENTO Nº: " & GetDepartamentoFromFirstAlbPdd(oDeliveries)
                .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.regular))
            End If

            tmp = ""
            .itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.regular))

            For Each oDelivery As DTODelivery In oDeliveries
                BEBL.Delivery.Load(oDelivery)
                If oDelivery.Items.Count > 0 Then
                    Dim sb As New System.Text.StringBuilder
                    sb.Append("Pedido nº " & ConcepteComanda(oDelivery))
                    'sb.Append("PEDIDO Nº " & NumeroDeComanda(oDelivery))
                    'sb.Append(" CENTRO Nº: " & oDelivery.Customer.Ref)
                    If oDelivery.Id <> 0 Then
                        sb.Append(" Albarán Nº: " & oDelivery.Id)
                    End If
                    .itms.Add(New DTODocItm(sb.ToString, DTODoc.FontStyles.regular))
                End If
            Next

        End With
        Return oDoc
    End Function

    Shared Function GetDepartamentoFromFirstAlbPdd(oDeliveries As List(Of DTODelivery)) As String
        Dim retval As String = ""
        '/dep.053/
        For Each oDelivery As DTODelivery In oDeliveries
            For Each oItem As DTODeliveryItem In oDelivery.Items
                If oItem.PurchaseOrderItem IsNot Nothing Then
                    Dim oOrder As DTOPurchaseOrder = oItem.PurchaseOrderItem.PurchaseOrder
                    Dim src As String = oOrder.Concept
                    Dim iStart As Integer = src.IndexOf("/dep.")
                    If iStart >= 0 Then
                        Dim iEnd As Integer = src.IndexOf("/", iStart + 5)
                        If iEnd > iStart Then
                            Dim sSegment As String = src.Substring(iStart, iEnd - iStart)
                            retval = sSegment.Replace("dep.", "").Replace("/", "")
                            Exit For
                        End If
                    End If
                End If
            Next
            If retval > "" Then Exit For
        Next
        Return retval
    End Function

    Shared Function ConcepteComanda(ByVal oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.client
                Dim oOrder As DTOPurchaseOrder = oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
                retval = oOrder.Concept
        End Select
        Return retval
    End Function

    Shared Function NumeroDeComanda(ByVal oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.client
                Dim oOrder As DTOPurchaseOrder = oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
                retval = NumeroDeComanda(oOrder)
        End Select
        Return retval
    End Function

    Shared Function NumeroDeComanda(ByVal oOrder As DTOPurchaseOrder) As String
        Dim retval As String = ""
        If oOrder.Concept > "" Then
            Dim sPdd As String = oOrder.Concept
            Dim i As Integer
            For i = 0 To sPdd.Length - 1
                If Not "0123456789".Contains(sPdd.Substring(i, 1)) Then Exit For
            Next
            If i > 0 Then retval = sPdd.Substring(0, i)
        End If
        Return retval
    End Function

    Shared Function ECISortedDeliveries(ByVal oSrcDeliveries As List(Of DTODelivery)) As List(Of DTODelivery)
        'posa els de ElCorteIngles al final, ordenats per adreça
        'per intercalar una plana de capçalera al davant

        Dim item As DTODelivery
        Dim retval As New List(Of DTODelivery)
        Dim oEciAlbs As New List(Of DTODelivery)
        Dim oElCorteIngles As DTOCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)

        'posa primer els que no son de ElCorteIngles
        For Each item In oSrcDeliveries
            If BEBL.Customer.CcxOrMe(item.Customer).isElCorteIngles Then
                oEciAlbs.Add(item)
            Else
                retval.Add(item)
            End If
        Next

        'endreça els de ElCorteIngles per destinació i afegeix-los al final
        Dim oSortedEciAlbs As List(Of DTODelivery) = BEBL.Deliveries.SortByAddress(oEciAlbs)
        retval.AddRange(oSortedEciAlbs)
        Return retval
    End Function

    Shared Function ComandesDeTransmisions(oTransmisions As List(Of DTOTransmisio)) As List(Of DTOTransmisio)
        Return ElCorteInglesLoader.ComandesDeTransmisions(oTransmisions)
    End Function

    Shared Function PlantillaDescatalogats() As MatHelper.Excel.Sheet
        Return ElCorteInglesLoader.PlantillaDescatalogats()
    End Function

    Shared Function PlantillaExhaurits() As MatHelper.Excel.Sheet
        Return ElCorteInglesLoader.PlantillaExhaurits()
    End Function

End Class
