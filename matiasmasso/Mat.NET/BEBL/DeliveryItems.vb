
Public Class DeliveryItems

    Shared Function All(oPurchaseOrderItem As DTOPurchaseOrderItem) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.All(oPurchaseOrderItem)
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct) As Models.SkuInOutModel
        Return DeliveryItemsloader.All(oProduct)
    End Function

    Shared Function All(Optional oCustomer As DTOCustomer = Nothing, Optional oMgz As DTOMgz = Nothing) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.All(oCustomer, oMgz)
        Return retval
    End Function

    Shared Function All(oProveidor As DTOProveidor, oMgz As DTOMgz) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.All(oProveidor, oMgz)
        Return retval
    End Function

    Shared Function All(oOrder As DTOPurchaseOrder) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.All(oOrder)
        Return retval
    End Function

    Shared Function Factory(oContact As DTOContact, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As List(Of DTODeliveryItem)
        Dim retval As List(Of DTODeliveryItem) = DeliveryItemsloader.Factory(oContact, oCod, oMgz)
        Return retval
    End Function

    Shared Function SetIncentius(exs As List(Of Exception), oCcx As DTOCustomer, oLineas As List(Of DTODeliveryItem), oUser As DTOUser) As Boolean
        Dim oItmIncentius As List(Of DTOIncentiu) = Nothing
        If Not oCcx.NoIncentius Then
            Dim oAlbIncentius As New List(Of DTOIncentiu)
            Dim oAlbIncentiu As DTOIncentiu = Nothing
            Dim oItm As DTODeliveryItem
            Dim DcDto As Decimal

            Dim oIncentius = BEBL.Incentius.All(oUser, False)
            oIncentius = oIncentius.FindAll(Function(x) x.OnlyInStk = False).ToList

            'assigna les quantitats de cada oferta
            For Each oItm In oLineas
                'DTOProductSku.Incentius(oItm.Sku, oIncentius)
                oItm.Incentius = Nothing 'New Product(oItm.Art).Incentius(Incentiu.Cods.Dto, DTO.GlobalVariables.Today(), New DTOCustomer(oClient.Guid))
                oItm.Incentius = DTOProductSku.Incentius(oItm.Sku, oIncentius)
                If oItm.Incentius.Count > 0 Then
                    Dim BlFoundInAlbIncentius As Boolean = False
                    For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                        BlFoundInAlbIncentius = False
                        For Each oAlbIncentiu In oAlbIncentius
                            If oItmIncentiu.Equals(oAlbIncentiu) Then
                                BlFoundInAlbIncentius = True
                                Exit For
                            End If
                        Next

                        If Not BlFoundInAlbIncentius Then
                            oAlbIncentiu = oItmIncentiu
                            oAlbIncentius.Add(oAlbIncentiu)
                        End If

                        oAlbIncentiu.Unitats += oItm.Qty
                    Next
                End If
            Next


            For Each oItm In oLineas

                'assigna la quantitat mès alta de les ofertes en que participa cada linia
                Dim iQty As Integer = 0
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    For Each oAlbIncentiu In oAlbIncentius
                        If oAlbIncentiu.Equals(oItmIncentiu) Then
                            If oAlbIncentiu.Unitats > iQty Then
                                iQty = oAlbIncentiu.Unitats
                            End If
                            Exit For
                        End If
                    Next
                Next

                'assigna el descompte de la oferta mes favorable que li toca a cada linea
                Dim DcArcDto As Decimal = oItm.PurchaseOrderItem.Dto
                Dim oAssignedIncentiu As DTOIncentiu = Nothing
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    DcDto = DTOIncentiu.GetDto(oItmIncentiu, iQty)
                    If DcDto > DcArcDto Then
                        DcArcDto = DcDto
                        oAssignedIncentiu = oItmIncentiu
                    End If
                Next
                oItm.Dto = DcArcDto
                oItm.Incentiu = oAssignedIncentiu
            Next

        End If

        Return exs.Count = 0
    End Function

    Shared Function VolumeM3(oItems As List(Of DTODeliveryItem)) As Decimal
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.VolumeM3(x))
        Return retval
    End Function

    Shared Function WeightKg(oItems As List(Of DTODeliveryItem)) As Integer
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.WeightKg(x))
        Return retval
    End Function

    Shared Function BaseImponible(oItems As List(Of DTODeliveryItem)) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item In oItems
            retval.Add(item.Import)
        Next
        Return retval
    End Function

End Class
