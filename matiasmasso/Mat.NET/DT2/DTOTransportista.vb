Public Class DTOTransportista
    Inherits DTOContact

    Property Abr As String
    Property Cubicaje As Integer
    Property NoCubicarPerSotaDe As Decimal
    Property CompensaPercent As Integer
    Property AllowReembolsos As Boolean
    Property TransportaMobiliari As Boolean
    Property Activat As Boolean

    Public Enum wellknowns
        Souto
        Tnt
        Seur
    End Enum

    Public Enum CodTarifa
        PreuHastaKg
        PreuPerKg
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function wellknown(id As wellknowns) As DTOTransportista
        Dim retval As DTOTransportista = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case wellknowns.Souto
                sGuid = "4D92D321-C045-49D6-BEAD-181169002128"
            Case wellknowns.Tnt
                sGuid = "9EE52C83-BFCE-41C2-9435-8D0516C26424"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOTransportista(oGuid)
        End If
        Return retval
    End Function

    Shared Function FromContact(oContact As DTOContact) As DTOTransportista

        Dim retval As DTOTransportista = Nothing
        If oContact Is Nothing Then
            retval = New DTOTransportista
        Else
            retval = New DTOTransportista(oContact.Guid)
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
