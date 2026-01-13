Public Class DTOTaller
    Inherits DTOContact

    Property Transportista As DTOTransportista

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FromContact(oContact As DTOContact) As DTOTaller
        Dim retval As DTOTaller = Nothing
        If oContact Is Nothing Then
            retval = New DTOTaller
        Else
            retval = New DTOTaller(oContact.Guid)
            With retval
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .FullNom = oContact.FullNom
                .Nif = oContact.Nif
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
            End With
        End If
        Return retval
    End Function
End Class
