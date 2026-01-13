Public Class DTOCustomerPlatform
    Inherits DTOContact

    Public Property Customer As DTOContact
    Public Property Destinations As List(Of DTOPlatformDestination)
    Public Property Deliveries As List(Of DTODelivery)
    Public Property BaseImponible As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Destinations = New List(Of DTOPlatformDestination)
    End Sub

    Shared Function FromContact(oContact As DTOContact) As DTOCustomerPlatform
        Dim retval As DTOCustomerPlatform = Nothing
        If oContact Is Nothing Then
            retval = New DTOCustomerPlatform
        ElseIf TypeOf oContact Is DTOCustomerPlatform Then
            retval = oContact
        Else
            retval = New DTOCustomerPlatform(oContact.Guid)
            With retval
                .Emp = oContact.Emp
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .SearchKey = oContact.SearchKey
                .FullNom = oContact.FullNom
                .Nif = oContact.Nif
                .Nif2 = oContact.Nif2
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
                .NomAnterior = oContact.NomAnterior
                .NomNou = oContact.NomNou
                .Website = oContact.Website
                .DisplayWebsite = oContact.DisplayWebsite
                .Botiga = oContact.Botiga
                .Logo = oContact.Logo
                .GLN = oContact.GLN
                .Telefon = oContact.Telefon
                .Tels = oContact.Tels
                .ContactPersons = oContact.ContactPersons
                .Obsoleto = oContact.Obsoleto
            End With
        End If
        Return retval
    End Function
End Class


Public Class DTOPlatformDestination
    Inherits DTOContact

    Public Property Platform As DTOCustomerPlatform


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FromContact(oContact As DTOContact) As DTOPlatformDestination
        Dim retval As New DTOPlatformDestination(oContact.Guid)
        retval.Emp = oContact.Emp
        retval.FullNom = oContact.FullNom
        Return retval
    End Function
End Class



