Public Class DTORanking
    Property User As DTOUser
    Property Reps As List(Of DTORep)
    Property Rep As DTORep
    Property Proveidor As DTOContact
    Property FchTo As Date = DateTime.Today
    Property FchFrom As Date = New Date(DateTime.Today.Year, 1, 1)
    Property Days As Integer = 90
    Property Product As DTOProduct
    Property Area As DTOArea
    Property ConceptType As ConceptTypes
    Property Format As Formats
    Property Items As List(Of DTORankingItem)

    Property Proveidors As List(Of DTOContact)
    Property Catalog As DTOProductCatalog
    Property Channel As DTODistributionChannel

    Public Enum Formats
        Amounts
        Units
    End Enum

    Public Enum ConceptTypes
        Geo
        Product
    End Enum

    Shared Function Atlas(oRanking As DTORanking) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim oCountries = oRanking.Items.GroupBy(Function(x) x.Location.Zona.Country.Guid).Select(Function(y) y.First).Select(Function(z) z.Location.Zona.Country).ToList
        Dim oZonas = oRanking.Items.GroupBy(Function(x) x.Location.Zona.Guid).Select(Function(y) y.First).Select(Function(z) z.Location.Zona).ToList
        Dim oLocations = oRanking.Items.GroupBy(Function(x) x.Location.Guid).Select(Function(y) y.First).Select(Function(z) z.Location).ToList

        For Each oCountry In oCountries
            retval.Add(oCountry)
            For Each oZona In oZonas.Where(Function(x) x.Country.Guid.Equals(oCountry.Guid))
                oCountry.Zonas.Add(oZona)
                For Each oLocation In oLocations.Where(Function(x) x.Zona.Guid.Equals(oZona.Guid))
                    oZona.Locations.Add(oLocation)
                Next
            Next
        Next
        Return retval
    End Function

    Shared Function CustomerRanking(oUser As DTOUser, Optional oProduct As DTOProduct = Nothing) As DTORanking
        Dim retval As New DTORanking
        With retval
            .User = oUser
            .ConceptType = DTORanking.ConceptTypes.Geo
            .Product = oProduct
        End With
        Return retval
    End Function

    Shared Function ExcelSheet(oRanking As DTORanking) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet()
        For Each item As DTORankingItem In oRanking.Items
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow()
            With item
                oRow.AddCell(item.Location.Zona.Country.ISO)
                oRow.AddCell(item.Location.Zona.nom)
                oRow.AddCell(item.Location.nom)
                oRow.AddCell(item.Customer.nom)
                Select Case oRanking.Format
                    Case DTORanking.Formats.Amounts
                        oRow.AddCell(item.Amt.Eur)
                    Case DTORanking.Formats.Units
                        oRow.AddCell(item.Units)
                End Select
            End With
        Next
        Return retval
    End Function

End Class

Public Class DTORankingItem
    Property Order As Integer
    Property Customer As DTOCustomer
    Property Location As DTOLocation
    Property Product As DTOProduct
    Property Units As Integer
    Property Amt As DTOAmt
End Class
