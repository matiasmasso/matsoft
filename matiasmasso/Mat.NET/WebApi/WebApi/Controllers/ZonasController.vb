Public Class ZonasController

    Inherits _BaseController

    <HttpPost>
    <Route("api/rep/zonas2")>
    Public Function FromRep(rep As DTOBaseGuid) As List(Of DUI.Country)
        Dim retval As New List(Of DUI.Country)
        Dim oRep As New DTORep(rep.Guid)
        BLLContact.Load(oRep) 'per lang
        Dim oCountries As List(Of DTOCountry) = BLLRep.Countries(oRep)
        For Each oCountry As DTOCountry In oCountries
            Dim DuiCountry As New DUI.Country
            With DuiCountry
                .Guid = oCountry.Guid
                .Nom = oCountry.Nom.Tradueix(oRep.Lang)
                .Zonas = New List(Of DUI.Zona)
            End With
            retval.Add(DuiCountry)

            For Each oZona As DTOZona In oCountry.Zonas
                Dim duiZona As New DUI.Zona
                With duiZona
                    .Guid = oZona.Guid
                    .Nom = oZona.Nom
                End With
                DuiCountry.Zonas.Add(duiZona)
            Next
        Next
        Return retval
    End Function
End Class
