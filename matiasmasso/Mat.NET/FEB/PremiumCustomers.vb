Public Class premiumCustomer
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPremiumCustomer)
        Return Await Api.Fetch(Of DTOPremiumCustomer)(exs, "premiumCustomer", oGuid.ToString())
    End Function

    Shared Function Load(ByRef opremiumCustomer As DTOPremiumCustomer, exs As List(Of Exception)) As Boolean
        If Not opremiumCustomer.IsLoaded And Not opremiumCustomer.IsNew Then
            Dim ppremiumCustomer = Api.FetchSync(Of DTOPremiumCustomer)(exs, "premiumCustomer", opremiumCustomer.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPremiumCustomer)(ppremiumCustomer, opremiumCustomer, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOPremiumCustomer) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "PremiumCustomer")
        End If
        Return retval
    End Function


    Shared Async Function Delete(opremiumCustomer As DTOPremiumCustomer, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPremiumCustomer)(opremiumCustomer, exs, "premiumCustomer")
    End Function
End Class

Public Class premiumCustomers
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oPremiumLine As DTOPremiumLine) As Task(Of List(Of DTOPremiumCustomer))
        Return Await Api.Fetch(Of List(Of DTOPremiumCustomer))(exs, "premiumCustomers/fromPremiumLine", oPremiumLine.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTOPremiumCustomer))
        Return Await Api.Fetch(Of List(Of DTOPremiumCustomer))(exs, "premiumCustomers/fromContact", oContact.Guid.ToString())
    End Function

    Shared Function Atlas(oPremiumCustomers As List(Of DTOPremiumCustomer), oLang As DTOLang) As DTOAtlas
        Dim oCustomerCountries = oPremiumCustomers.GroupBy(Function(x) DTOAddress.Country(x.Customer.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Country(z.Customer.Address)).ToList
        Dim oCustomerZonas = oPremiumCustomers.GroupBy(Function(x) DTOAddress.Zona(x.Customer.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Zona(z.Customer.Address)).ToList
        Dim oCustomerLocations = oPremiumCustomers.GroupBy(Function(x) DTOAddress.Location(x.Customer.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Location(z.Customer.Address)).ToList

        Dim oCountries As New List(Of DTOAtlas.Country)
        For Each oCustomerCountry In oCustomerCountries
            Dim oCountry As New DTOAtlas.Country(oCustomerCountry.Guid, oCustomerCountry.LangNom.Tradueix(oLang))
            oCountries.Add(oCountry)
            For Each oCustomerZona In oCustomerZonas.Where(Function(x) x.Country.Guid.Equals(oCountry.guid)).ToList
                Dim oZona As New DTOAtlas.Zona(oCustomerZona.Guid, oCustomerZona.Nom)
                oCountry.zonas.Add(oZona)
                For Each oCustomerLocation In oCustomerLocations.Where(Function(x) x.Zona.Guid.Equals(oZona.guid)).ToList
                    Dim oLocation As New DTOAtlas.location(oCustomerLocation.Guid, oCustomerLocation.Nom)
                    oZona.locations.Add(oLocation)
                    For Each oPremiumCustomer In oPremiumCustomers.Where(Function(x) x.Customer.Address.Zip.Location.Guid.Equals(oLocation.guid)).ToList
                        Dim oContact As New DTOAtlas.Contact(oPremiumCustomer.Customer.Guid, DTOCustomer.NomAndNomComercial(oPremiumCustomer.Customer))
                        oLocation.contacts.Add(oContact)
                    Next
                Next
            Next
        Next
        Dim retval As New DTOAtlas
        If oCountries.Count = 1 Then
            retval.areas = oCountries.First.zonas
        Else
            retval.areas = oCountries
        End If
        Return retval
    End Function

End Class
