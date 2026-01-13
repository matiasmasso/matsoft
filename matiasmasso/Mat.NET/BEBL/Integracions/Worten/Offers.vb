Namespace Integracions.Worten


    Public Class Offers

        Public Shared Async Function ShopOffer(exs As List(Of Exception)) As Task(Of DTO.Integracions.Worten.ShopOffer)
            Return Await Api.GetRequest(Of DTO.Integracions.Worten.ShopOffer)(exs, "offers")
        End Function

        Public Shared Async Function UpdateStocks(exs As List(Of Exception), oEmp As DTOEmp, Optional oTaskLog As DTOTaskLog = Nothing) As Task(Of DTOTaskLog)
            Dim retval As DTOTaskLog = If(oTaskLog, New DTOTaskLog)
            Dim oShopOffer = Await ShopOffer(exs)
            Dim oEans = oShopOffer.offers.SelectMany(Function(x) x.product_references).Where(Function(y) y.reference_type = "EAN").Select(Function(z) z.reference).ToList()
            Dim oSkus = BEBL.ProductSkus.FromEanValues(oEans, oEmp.Mgz)
            Dim oNewShopOffer = StocksToUpdate(oShopOffer, oSkus)
            If oNewShopOffer.offers.Count = 0 Then
                retval.SetResult(DTOTask.ResultCods.Empty, String.Format("Worten: sense canvis a l'stock de {0} productes", oShopOffer.offers.Count), exs)
            Else
                Dim result = Await Update(exs, oNewShopOffer)
                retval.SetResult(DTOTask.ResultCods.Success, String.Format("Worten: id:{0} actualitzats els stocks de {1} productes sobre un total de {2} ", result.import_id, oNewShopOffer.offers.Count, oShopOffer.offers.Count), exs)
            End If
            Return retval
        End Function

        Private Shared Function StocksToUpdate(ByRef oShopOffer As DTO.Integracions.Worten.ShopOffer, oSkus As List(Of DTOProductSku)) As DTO.Integracions.Worten.ShopOffer
            Dim retval As New DTO.Integracions.Worten.ShopOffer
            retval.offers = New List(Of DTO.Integracions.Worten.Offer)
            For Each offer In oShopOffer.offers
                Dim sEan = offer.Ean()
                If sEan > "" Then
                    Dim oSku = oSkus.FirstOrDefault(Function(x) x.Ean13.Value = sEan)
                    If oSku.stockAvailable <> offer.quantity Then
                        offer.quantity = oSku.stockAvailable
                        retval.offers.Add(offer)
                    End If
                End If
            Next
            Return retval
        End Function

        Public Shared Async Function Update(exs As List(Of Exception), oShopOffer As DTO.Integracions.Worten.ShopOffer) As Task(Of DTO.Integracions.Worten.ImportResult)
            For Each oOffer In oShopOffer.offers
                oOffer.update_delete = "update"
                oOffer.state_code = "11"
            Next
            Return Await Api.PostRequest(Of DTO.Integracions.Worten.ShopOffer, DTO.Integracions.Worten.ImportResult)(oShopOffer, exs, "offers")
        End Function

    End Class
End Namespace