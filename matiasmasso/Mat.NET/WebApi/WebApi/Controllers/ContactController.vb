Public Class ContactController

    Inherits _BaseController

    <HttpPost>
    <Route("api/contacts/search")>
    Public Function search(userSearchkey As DUI.UserSearchKey) As List(Of DUI.Contact)
        Dim retval As New List(Of DUI.Contact)
        Dim oUser As DTOUser = BLLUser.Find(userSearchkey.User.Guid)
        Dim oContacts As List(Of DTOContact) = BLL.BLLContacts.Search(userSearchkey.Searchkey, oUser)
        For Each oContact As DTOContact In oContacts
            Dim item As New DUI.Contact
            With item
                .Guid = oContact.Guid
                .Nom = BLLContact.NomAndNomComercial(oContact)
                .Nif = oContact.Nif
                .Location = BLLAddress.Location(oContact.Address).Nom
                .Address = oContact.Address.Text
                .Location = oContact.Address.Zip.Location.Nom
                If oContact.Address.Coordenadas IsNot Nothing Then
                    .Latitude = oContact.Address.Coordenadas.Latitud
                    .Longitude = oContact.Address.Coordenadas.Longitud
                End If
            End With
            retval.Add(item)
        Next
        Return retval
    End Function


    <HttpPost>
    <Route("api/contacts/classes")>
    Public Function contactClasses(user As DUI.User) As List(Of DUI.Guidnom)
        Dim retval As New List(Of DUI.Guidnom)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oClasses As List(Of DTOContactClass) = BLLContactClasses.All()
        For Each oClass As DTOContactClass In oClasses
            Dim item As New DUI.Guidnom()
            With item
                .Guid = oClass.Guid
                .Nom = oClass.Nom.Tradueix(oUser.Lang)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/class/contacts")>
    Public Function ClassContacts(userClass As DUI.UserGuidNom) As List(Of DUI.Contact)
        Dim retval As New List(Of DUI.Contact)
        Dim oUser As DTOUser = BLLUser.Find(userClass.User.Guid)
        Dim oClass As New DTOContactClass(userClass.GuidNom.Guid)
        Dim oContacts As List(Of DTOContact) = BLL.BLLContacts.All(oClass, oUser)
        For Each oContact As DTOContact In oContacts
            Dim item As New DUI.Contact
            With item
                .Guid = oContact.Guid
                .Nom = BLLContact.NomAndNomComercial(oContact)
                .Nif = oContact.Nif
                .Location = BLLAddress.Location(oContact.Address).Nom
                .Address = oContact.Address.Text
                .Location = BLLAddress.CityZon(oContact.Address.Zip.Location)
                If oContact.Address.Coordenadas IsNot Nothing Then
                    .Latitude = oContact.Address.Coordenadas.Latitud
                    .Longitude = oContact.Address.Coordenadas.Longitud
                End If
            End With
            retval.Add(item)
        Next
        Return retval
    End Function




    <HttpPost>
    <Route("api/NearestNeighbours")>
    Public Function NearestNeighbours(oUserLocation As DUI.UserLocation) As List(Of DUI.Neighbour)
        Dim retval As New List(Of DUI.Neighbour)
        Dim oUser As DTOUser = BLLUser.Find(oUserLocation.User.Guid)
        Dim oCoordenadas As New DTOGeoCoordenadas(oUserLocation.Latitude, oUserLocation.Longitude)
        Dim oNeighbours As List(Of DTONeighbour) = BLLNeighbours.NearestNeighbours(oUser, oCoordenadas)
        For Each oContact As DTONeighbour In oNeighbours
            Dim dui As New DUI.Neighbour
            With dui
                .Guid = oContact.Guid
                .Nom = BLLContact.NomComercialOrDefault(oContact)
                .Nif = oContact.Nif
                .Address = oContact.Address.Text
                .Location = oContact.Address.Zip.Location.Nom
                .Latitude = oContact.Address.Coordenadas.Latitud
                .Longitude = oContact.Address.Coordenadas.Longitud
                .Distance = BLLNeighbour.FormattedDistance(oContact.Distance)
            End With
            retval.Add(dui)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/tels")>
    Public Function tels(contact As DUI.Contact) As List(Of DUI.Tel)
        Dim retval As New List(Of DUI.Tel)
        Dim oContact As New DTOContact(contact.Guid)
        Dim oTels As List(Of DTOContactTel) = BLL.BLLContactTels.All(oContact)
        For Each oTel As DTOContactTel In oTels
            Dim item As New DUI.Tel
            With item
                .Num = BLLContactTel.Formatted(oTel)
                .Obs = IIf(oTel.Cod = DTOContactTel.Cods.fax, "(fax) ", "") & oTel.Obs
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/emails")>
    Public Function emails(contact As DUI.Contact) As List(Of DUI.Email)
        Dim retval As New List(Of DUI.Email)
        Dim oContact As New DTOContact(contact.Guid)
        Dim oemails As List(Of DTOEmail) = BLL.BLLEmails.All(oContact)
        For Each oEmail As DTOEmail In oemails
            Dim item As New DUI.Email
            With item
                .Address = oEmail.EmailAddress
                .Obs = oEmail.Obs
            End With
            retval.Add(item)
        Next
        Return retval
    End Function



End Class
