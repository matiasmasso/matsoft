Public Class Britax
    Shared Function XMLStoreLocator() As DTO.Britax.XML
        Dim retval As New DTO.Britax.XML

        Dim oRomer = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Roemer)
        Dim oDistributors = BEBL.StoreLocator.Distributors(oRomer, DTOLang.ENG, includeItems:=True)
        For Each oDistribuidor As DTOProductDistributor In oDistributors
            Dim oCustomer As New DTO.Britax.XML.Customer
            With oCustomer
                .AccountNum = oDistribuidor.Guid.ToString
                .Name = oDistribuidor.Nom
                .Street = oDistribuidor.Adr
                .ZipCode = oDistribuidor.ZipCod
                .City = oDistribuidor.Location.Nom
                .Country = DirectCast(oDistribuidor.Country, DTOCountry).ISO
                .Region = oDistribuidor.Zona.Nom
                .Latitude = oDistribuidor.Latitud
                .Longitude = oDistribuidor.Longitud
                .Items = New List(Of DTO.Britax.XML.Item)
                For Each src As String In oDistribuidor.Items
                    Dim item As New DTO.Britax.XML.Item
                    item.ItemId = src
                    .Items.Add(item)
                Next
            End With
            retval.Add(oCustomer)
            'If Debugger.IsAttached And retval.Count = 2 Then Exit For 'TO DEPRECATE
        Next

        Return retval
    End Function



End Class
