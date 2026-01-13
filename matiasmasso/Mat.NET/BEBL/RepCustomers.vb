Public Class RepCustomers
    Shared Function All(oUser As DTOUser, Optional oArea As DTOArea = Nothing) As List(Of DTOContact)
        Dim retval As List(Of DTOContact) = RepCustomersLoader.All(oUser, oArea)
        Return retval
    End Function

    Shared Function Atlas(oRepUser As DTOUser) As List(Of DTOAtlas.Country)
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOAtlas.Country)
        BEBL.User.Load(oRepUser)
        Dim oContacts = BEBL.RepCustomers.All(oRepUser)
        Dim oCountries As List(Of DTOCountry) = oContacts.Select(Function(x) x.address.Zip.Location.Zona.Country).Distinct.ToList
        For Each oCountry In oCountries
            Dim itemCountry As New DTOAtlas.Country(oCountry.Guid, oCountry.LangNom.Tradueix(oRepUser.lang))
            retval.Add(itemCountry)
            For Each oZona In oCountry.Zonas
                Dim itemZona As New DTOAtlas.Zona(oZona.Guid, oZona.Nom)
                itemCountry.zonas.Add(itemZona)
                For Each oLocation In oZona.Locations
                    Dim itemLocation As New DTOAtlas.location(oLocation.Guid, oLocation.Nom)
                    itemZona.locations.Add(itemLocation)
                    For Each oContact In oLocation.Contacts
                        Dim itemContact = DTOAtlas.Contact.Factory(oContact)
                        itemLocation.contacts.Add(itemContact)
                    Next
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function AtlasOld(oRepUser As DTOUser) As List(Of Object)
        Dim exs As New List(Of Exception)
        BEBL.User.Load(oRepUser)
        Dim oContacts = BEBL.RepCustomers.All(oRepUser)
        Dim oCountries As List(Of DTOCountry) = oContacts.Select(Function(x) x.address.Zip.Location.Zona.Country).Distinct.ToList

        Dim items As New List(Of Object)
        For Each oCountry In oCountries
            Dim duiCountry As Object = New With {.guid = oCountry.Guid, .nom = oCountry.LangNom.Tradueix(oRepUser.lang), .zonas = New List(Of Object)}
            items.Add(duiCountry)
            For Each oZona As DTOZona In oCountry.Zonas
                Dim duiZona As Object = New With {.guid = oZona.Guid, .nom = oZona.Nom, .locations = New List(Of Object)}
                duiCountry.zonas.add(duiZona)
                For Each oLocation As DTOLocation In oZona.Locations
                    Dim duiLocation As Object = New With {.guid = oLocation.Guid, .nom = oLocation.Nom, .contacts = New List(Of Object)}
                    duiZona.locations.Add(duiLocation)
                    For Each oContact As DTOContact In oLocation.Contacts
                        Dim duiContact As Object = New With {.guid = oContact.Guid, .nom = oContact.FullNom}
                        duiLocation.contacts.Add(duiContact)
                    Next
                Next
            Next
        Next

        Return items
    End Function
End Class
