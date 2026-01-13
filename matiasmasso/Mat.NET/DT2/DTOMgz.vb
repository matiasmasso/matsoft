Public Class DTOMgz 'Warehouse
    Inherits DTOContact

    Property Abr As String
    Shadows Property IsLoaded As Boolean

    Public Enum wellknowns
        NotSet
        VivaceLliça
        VivaceLaRoca2
        MatiasMasso
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function wellknown(id As DTOMgz.wellknowns) As DTOMgz
        Dim retval As DTOMgz = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOMgz.wellknowns.VivaceLliça
                sGuid = "41a81aca-1c01-44fc-bf57-2728b03f74d8"
            Case DTOMgz.wellknowns.VivaceLaRoca2
                sGuid = "88A2C2F3-9E14-421A-B727-7647ECD07165"
            Case DTOMgz.wellknowns.MatiasMasso
                sGuid = "4C37D20F-A975-4C63-BB9C-F997A7080DF1"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOMgz(oGuid)
        End If
        Return retval
    End Function

    Shared Function FromContact(oContact As DTOContact) As DTOMgz
        Dim retval As DTOMgz = Nothing
        If oContact Is Nothing Then
            retval = New DTOMgz
        ElseIf TypeOf oContact Is DTOMgz Then
            retval = oContact
        Else
            retval = New DTOMgz(oContact.Guid)
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

    Shared Function AbrOrNom(oMgz As DTOMgz) As String
        Dim retval As String = If(oMgz.Abr, oMgz.Nom)
        Return retval
    End Function
End Class
