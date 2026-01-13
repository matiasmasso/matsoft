Public Class RepController
    Inherits _BaseController

    <HttpPost>
    <Route("api/reps")>
    Public Function reps(user As DTOGuidNom) As List(Of DUI.Rep)
        Dim retval As New List(Of DUI.Rep)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oReps As List(Of DTORep) = BLL.BLLReps.All(True)
        For Each oRep As DTORep In oReps
            Dim item As New DUI.Rep
            With item
                .Guid = oRep.Guid.ToString
                .Nom = BLLRep.NicknameOrNom(oRep)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpGet>
    <Route("api/rep/atlas2/{userguid}")>
    Public Function Atlas2(userguid As Guid) As List(Of DUI.Country)
        Dim retval As New List(Of DUI.Country)
        Dim oCountry As DTOCountry = BLLCountry.WellKnown(DTOCountry.WellKnown.Spain)
        Dim duiCountry As New DUI.Country
        duiCountry.Guid = oCountry.Guid
        duiCountry.Nom = "Esp"
        duiCountry.Zonas = New List(Of DUI.Zona)
        retval.Add(duiCountry)

        Return retval
    End Function

    <HttpGet>
    <Route("api/rep/atlas/{userguid}")>
    Public Function Atlas(userguid As Guid) As List(Of DUI.Country)
        'prova per web rep basket fallida perque jsonp no va
        Dim oUser As DTOUser = BLLUser.Find(userguid)
        Dim oRep As DTORep = BLLUser.GetRep(oUser)
        Dim oCountries As List(Of DTO.DTOCountry) = BLLContacts.Countries(oUser)

        Dim retval As New List(Of DUI.Country)
        For Each oCountry In oCountries
            Dim duiCountry As New DUI.Country
            With duiCountry
                .Guid = oCountry.Guid
                .Nom = oCountry.Nom.Tradueix(oUser.Lang)
                .Zonas = New List(Of DUI.Zona)
            End With
            retval.Add(duiCountry)
            For Each oZona As DTOZona In oCountry.Zonas
                Dim duiZona As New DUI.Zona
                With duiZona
                    .Guid = oZona.Guid
                    .Nom = oZona.Nom
                    .Locations = New List(Of DUI.Location)
                End With
                duiCountry.Zonas.Add(duiZona)
                For Each oLocation As DTOLocation In oZona.Locations
                    Dim duiLocation As New DUI.Location
                    With duiLocation
                        .Guid = oLocation.Guid
                        .Nom = oLocation.Nom
                        .Contacts = New List(Of DUI.Guidnom)
                    End With
                    duiZona.Locations.Add(duiLocation)
                    For Each oContact As DTOContact In oLocation.Contacts
                        Dim duiContact As New DUI.Guidnom
                        With duiContact
                            .Guid = oContact.Guid
                            .Nom = oContact.FullNom
                        End With
                        duiLocation.Contacts.Add(duiContact)
                    Next
                Next
            Next
        Next

        Return retval
    End Function

    <HttpPost>
    <Route("api/rep/zonas")>
    Public Function zonas(rep As DUI.Rep) As List(Of DUI.Zona)
        Dim retval As New List(Of DUI.Zona)
        Dim oRep As New DTORep(New Guid(rep.Guid))
        Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(BLLApp.Emp, oRep)
        For Each item As DTORepProduct In oRepProducts
            If Not retval.Exists(Function(x) x.Guid.Equals(item.Area.Guid)) Then
                Dim duiZona As New DUI.Zona
                With duiZona
                    .Guid = item.Area.Guid
                    .Nom = BLLArea.Nom(item.Area)
                End With
                retval.Add(duiZona)
            End If
        Next

        Return retval
    End Function

    <HttpPost>
    <Route("api/rep/repliqs")>
    Public Function repliqs(rep As DUI.Rep) As List(Of DUI.Repliq)
        Dim retval As New List(Of DUI.RepLiq)
        Dim oRep As New DTORep(New Guid(rep.Guid))
        Dim oRepLiqs As List(Of DTORepLiq) = BLL.BLLRepLiqs.All(oRep)
        For Each item As DTORepLiq In oRepLiqs
            Dim duiRepliq As New DUI.RepLiq
            With duiRepliq
                .Devengat = item.BaseImponible.Eur
                .Liquid = BLLRepLiq.GetLiquid(item).Eur
                .Fch = item.Fch
                If item.Cca IsNot Nothing Then
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(item.Cca.DocFile, True)
                    .FileUrl = BLLDocFile.DownloadUrl(item.Cca.DocFile, True)
                End If
            End With
            retval.Add(duiRepliq)
        Next

        Return retval
    End Function

End Class
