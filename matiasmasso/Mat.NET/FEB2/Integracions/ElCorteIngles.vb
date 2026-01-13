Public Class ElCorteIngles
    Private Shared _platforms As List(Of DTOCustomerPlatform)
    Private Shared _lastContact As DTOContact

    Shared Async Function Orders(exs As List(Of Exception), year As Integer) As Threading.Tasks.Task(Of List(Of DTO.Models.ElCorteInglesOrderModel))
        Return Await Api.Fetch(Of List(Of DTO.Models.ElCorteInglesOrderModel))(exs, "ElCorteIngles/orders", year)
    End Function
    Shared Async Function PlantillaDescatalogats(exs As List(Of Exception)) As Threading.Tasks.Task(Of ExcelHelper.Sheet)
        Return Await Api.Fetch(Of ExcelHelper.Sheet)(exs, "ElCorteIngles/plantillas/descatalogats")
    End Function

    Shared Async Function PlantillaExhaurits(exs As List(Of Exception)) As Threading.Tasks.Task(Of ExcelHelper.Sheet)
        Return Await Api.Fetch(Of ExcelHelper.Sheet)(exs, "ElCorteIngles/plantillas/exhaurits")
    End Function

    Shared Async Function Platforms(oContact As DTOContact) As Task(Of List(Of DTOCustomerPlatform))
        If _platforms Is Nothing Or oContact.UnEquals(_lastContact) Then
            Dim exs As New List(Of Exception)
            _platforms = Await FEB2.CustomerPlatforms.All(oContact, exs)
        End If
        Return _platforms
    End Function

    Shared Function Central() As DTOCustomer
        Static retval As DTOCustomer
        If retval Is Nothing Then
            retval = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.ElCorteIngles)
            ' retval = FEB2.Customer.Find(exs,oGuid) 'implementar
        End If
        Return retval
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

    Shared Function AlbHeaderDoc(ByVal oDeliveries As List(Of DTODelivery)) As DTODoc
        Dim oDoc As New DTODoc(DTODoc.Estilos.Factura, Central.Lang, DTOCur.Eur, False)
        Dim oFirstDelivery As DTODelivery = oDeliveries.First
        Dim tmp As String

        With oDoc
            .Dest.Add(oFirstDelivery.Nom)
            .Dest.Add(oFirstDelivery.Address.Text)
            .Dest.Add(oFirstDelivery.Address.Zip.FullNom(Central.Lang))
            .fch = DateTime.Today

            tmp = ""
            .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Regular))


            tmp = "ALBARAN DE ENTREGA"
            .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Underline))


            tmp = ""
            .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Regular))

            tmp = "PROVEEDOR Nº: " & Central.suProveedorNum
            .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Regular))

            Dim sDepartamento As String = GetDepartamentoFromFirstAlbPdd(oDeliveries)
            If sDepartamento > "" Then
                tmp = "DEPARTAMENTO Nº: " & GetDepartamentoFromFirstAlbPdd(oDeliveries)
                .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Regular))
            End If

            tmp = ""
            .Itms.Add(New DTODocItm(tmp, DTODoc.FontStyles.Regular))

            For Each oDelivery As DTODelivery In oDeliveries
                Dim sb As New System.Text.StringBuilder
                sb.Append("Pedido nº " & ConcepteComanda(oDelivery))
                'sb.Append("PEDIDO Nº " & NumeroDeComanda(oDelivery))
                'sb.Append(" CENTRO Nº: " & oDelivery.Customer.Ref)
                If oDelivery.Id <> 0 Then
                    sb.Append(" Albarán nº: " & oDelivery.Id)
                End If
                .Itms.Add(New DTODocItm(sb.ToString, DTODoc.FontStyles.Regular))
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
            Case DTOPurchaseOrder.Codis.Client
                Dim oOrder As DTOPurchaseOrder = oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
                retval = oOrder.Concept
        End Select
        Return retval
    End Function

    Shared Function NumeroDeComanda(ByVal oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.Client
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

    Shared Async Function PurchaseOrderYears(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of Integer))
        Dim oCentral = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.ElCorteIngles)
        Return Await FEB2.PurchaseOrders.Years(exs, oEmp, DTOPurchaseOrder.Codis.Client, oCentral, True)
    End Function

    Shared Async Function PurchaseOrders(exs As List(Of Exception), oEmp As DTOEmp, iYear As Integer) As Task(Of List(Of DTOPurchaseOrder))
        Dim oCentral = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.ElCorteIngles)
        Return Await FEB2.PurchaseOrders.All(exs, oEmp, DTOPurchaseOrder.Codis.Client, iYear, oCentral, True)
    End Function

    Shared Async Function ComandesDeTransmisions(exs As List(Of Exception), oTransmisions As List(Of DTOTransmisio)) As Task(Of List(Of DTOTransmisio))
        Return Await Api.Execute(Of List(Of DTOTransmisio), List(Of DTOTransmisio))(oTransmisions, exs, "elCorteIngles/ComandesDeTransmisions")
    End Function
    Shared Async Function AlineamientosDeDisponibilidad(exs As List(Of Exception)) As Task(Of List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad))
        Return Await Api.Fetch(Of List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad))(exs, "elcorteingles/AlineamientosDeDisponibilidad")
    End Function
    Shared Async Function AlineamientoDeDisponibilidad(exs As List(Of Exception)) As Task(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Return Await Api.Fetch(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)(exs, "elcorteingles/AlineamientoDeDisponibilidad")
    End Function

    Shared Async Function AlineamientoDeDisponibilidad(exs As List(Of Exception), oGuid As Guid, oUser As DTOUser) As Task(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Dim retval As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad = Nothing
        Dim oCache = Await FEB2.Cache.Fetch(exs, oUser)
        If exs.Count = 0 Then
            retval = Await Api.Fetch(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)(exs, "elcorteingles/AlineamientoDeDisponibilidad", oGuid.ToString())

            retval.Items = New List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
            Dim sb As New Text.StringBuilder
            For Each line In retval.Text.Split(vbCrLf)
                Dim fields = line.Split(",")
                If fields.Count > 9 Then 'prevent lines with just vbLf
                    Dim item As New DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item()
                    With item
                        .Ean = fields(2)
                        .Price = Decimal.Parse(fields(6), System.Globalization.CultureInfo.InvariantCulture)
                        .RefEci = fields(7)
                        .Obsoleto = fields(5) = "S"
                        .Stock = fields(9)
                        .Uneco = fields(10)
                        Dim oSku = oCache.FindSku(DTOEan.Factory(.Ean))
                        If oSku IsNot Nothing Then
                            .BrandNom = oSku.Category.Brand.Nom.Tradueix(oUser.Lang)
                            .CategoryNom = oSku.Category.Nom.Tradueix(oUser.Lang)
                            .SkuNom = oSku.Nom.Tradueix(oUser.Lang)
                        End If
                    End With
                    retval.Items.Add(item)
                End If
            Next
        End If
        Return retval
    End Function

    Shared Async Function ExcelTransmisions(exs As List(Of Exception), oTransms As List(Of DTOTransmisio)) As Task(Of ExcelHelper.Sheet)

        Dim retval As New ExcelHelper.Sheet
        retval.AddRowWithCells("Transmision", "Plataforma", "Pedido", "Centro", "Albaran")

        Dim items = Await FEB2.ElCorteIngles.ComandesDeTransmisions(exs, oTransms)
        For Each item As DTOTransmisio In items
            Dim sPlatformNom As String = ""
            If item.Deliveries.First.Platform IsNot Nothing Then
                sPlatformNom = item.Deliveries.First.Platform.Nom

            End If
            retval.AddRowWithCells(item.Id, sPlatformNom)

            For Each oDelivery As DTODelivery In item.Deliveries
                For Each oPurchaseOrder In oDelivery.PurchaseOrders
                    retval.AddRowWithCells("", "", DTOEci.NumeroDeComanda(oPurchaseOrder), oDelivery.Customer.Ref, oDelivery.Id)
                Next
            Next
        Next
        Return retval
    End Function


End Class
