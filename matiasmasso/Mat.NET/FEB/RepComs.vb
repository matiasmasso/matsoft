Public Class RepCom

    Shared Async Function GetRepCom(oEmp As DTOEmp, oCustomer As DTOCustomer, oProduct As DTOProductSku, DtFch As Date, Optional oRepProducts As List(Of DTORepProduct) = Nothing, Optional oRepCliComs As List(Of DTORepCliCom) = Nothing, Optional exs As List(Of Exception) = Nothing) As Task(Of DTORepCom)
        Dim retval As DTORepCom = Nothing
        If oCustomer.NoRep Or oProduct Is Nothing Then
            retval = Nothing
        Else
            If oCustomer.ContactClass Is Nothing Or oCustomer.Address Is Nothing Then
                Contact.Load(oCustomer, exs)
            End If
            Dim oChannel As DTODistributionChannel = oCustomer.ContactClass.DistributionChannel
            If oChannel Is Nothing AndAlso exs IsNot Nothing Then
                exs.Add(New Exception("Client no adscrit a cap canal de distribució"))
            Else
                If oRepProducts Is Nothing Then
                    oRepProducts = Await RepProducts.All(exs, oChannel, oCustomer.Address.Zip, oProduct, DtFch)
                Else
                    oRepProducts = DTORepProduct.Match(oRepProducts, oChannel, oCustomer.Address.Zip, oProduct, DtFch)
                End If
                If oRepProducts.Count > 0 Then
                    Dim oRepProduct As DTORepProduct = oRepProducts.First
                    Dim oRepCliCom As DTORepCliCom = Await RepCliCom.Match(exs, oEmp, oRepProduct.Rep, oCustomer, DtFch, oRepCliComs)
                    If oRepCliCom Is Nothing Then
                        retval = New DTORepCom
                        retval.Rep = oRepProduct.Rep
                        retval.Com = oRepProduct.ComStd
                    Else
                        retval = DTORepCliCom.RepCom(oRepCliCom, oRepProduct)
                    End If
                End If
            End If
        End If

        Return retval
    End Function

End Class
