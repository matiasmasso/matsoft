Public Class DTOProductDistributor
    Inherits DTOBaseGuid

    Property Nom As String
    Property ContactClass As DTOContactClass
    Property Adr As String
    Property ZipCod As String
    Property Location As DTOArea
    Property Zona As DTOArea
    Property Country As DTOArea
    Property Tel As DTOContactTel

    Property Url As String
    Property Promo As Boolean
    Property Sales As Decimal 'dins el periode actiu (60 dies)
    Property SalesHistoric As Decimal 'dels darrers dos anys
    Property SalesCcx As Decimal 'prorratejat entre els diferents punts de venda que suministra una central de compres
    Property Raffles As Boolean
    Property LastFch As Date

    Property Items As List(Of String)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function Contact() As DTOContact
        Return New DTOContact(MyBase.Guid)
    End Function

    Shared Function Countries(src As List(Of DTOProductDistributor)) As List(Of DTOArea)
        Dim oCountries = src.Select(Function(x) x.Country).ToList

        Dim retval As New List(Of DTOArea)
        For Each oCountry In oCountries
            Dim oGuid As Guid = oCountry.Guid
            If Not retval.Any(Function(x) x.Guid = oGuid) Then
                retval.Add(oCountry)
            End If
        Next
        Return retval
    End Function

    Shared Function Zonas(src As List(Of DTOProductDistributor), oCountry As DTOGuidNom) As List(Of DTOArea)
        Dim retval = src.Where(Function(x) x.Country.Equals(oCountry)).Select(Function(x) x.Zona).Distinct.ToList
        Return retval
    End Function

    Shared Function Zonas(src As List(Of DTOProductDistributor)) As List(Of DTOArea)
        Dim retval = src.Select(Function(x) x.Zona).Distinct.ToList
        Return retval
    End Function

    Shared Function Locations(src As List(Of DTOProductDistributor), oZona As DTOGuidNom) As List(Of DTOArea)
        Dim retval As List(Of DTOArea) = src.
            Where(Function(x) x.Zona.Equals(oZona)).
            Select(Function(x) x.Location).Distinct.ToList
        Return retval
    End Function

    Shared Function Locations(src As List(Of DTOProductDistributor)) As List(Of DTOArea)
        Dim retval As List(Of DTOArea) = src.
            Select(Function(x) x.Location).Distinct.ToList
        Return retval
    End Function


    Shared Function PremiumOrSpareDistributors(oDistributors As List(Of DTOProductDistributor), oLocation As DTOArea) As List(Of DTOProductDistributor)
        Dim oAllDistributors = oDistributors.Where(Function(x) x.Location.Equals(oLocation)).ToList
        Dim oPremiumDistributors = oAllDistributors.Where(Function(x) x.Sales > 0).ToList 'those who purchased within reasonable date
        Dim oSpareDistributors = oAllDistributors.Where(Function(x) x.Sales = 0).ToList 'those who have not purchased since long ago. Just display them if no Premium ones are available

        Dim retval As New List(Of DTOProductDistributor)
        If oPremiumDistributors.Count = 0 Then
            Dim oHistory = oSpareDistributors.Where(Function(x) x.SalesHistoric > 0).ToList
            Dim oNoHistory = oSpareDistributors.Where(Function(x) x.SalesHistoric = 0).ToList
            If oHistory.Count = 0 Then
                retval = oNoHistory
            Else
                retval = oHistory
            End If
        Else
            retval = oPremiumDistributors
        End If
        Return retval
    End Function

    Shared Function LocationDistributors(oDistributors As List(Of DTOProductDistributor), oLocation As DTOArea) As List(Of DTOProductDistributor)
        Dim oAllDistributors = oDistributors.Where(Function(x) x.Location.Equals(oLocation))
        Dim oPremiumDistributors = oAllDistributors.Where(Function(x) x.Sales > 0).ToList 'those who purchased within reasonable date
        Dim oSpareDistributors = oAllDistributors.Where(Function(x) x.Sales = 0).ToList 'those who have not purchased since long ago. Just display them if no Premium ones are available

        Dim retval As New List(Of DTOProductDistributor)
        If oPremiumDistributors.Count = 0 Then
            Dim oHistory = oSpareDistributors.Where(Function(x) x.SalesHistoric > 0).ToList
            Dim oNoHistory = oSpareDistributors.Where(Function(x) x.SalesHistoric = 0).ToList
            If oHistory.Count = 0 Then
                retval = oNoHistory
            Else
                retval = oHistory
            End If
        Else
            retval = oPremiumDistributors
        End If
        Return retval
    End Function

    Shared Function All(src As List(Of DTOProductDistributor), oLocation As DTOGuidNom) As List(Of DTOProductDistributor)
        Dim retval As List(Of DTOProductDistributor) = src.
            Where(Function(x) x.Location.Equals(oLocation)).ToList
        Return retval
    End Function

    Shared Function BestZona(oDistributors As List(Of DTOProductDistributor)) As DTOBaseGuid
        Dim retval As DTOBaseGuid = Nothing
        Dim query = oDistributors.GroupBy(Function(g) New With {Key g.Zona}).
            Select(Function(group) New With {.Zona = group.Key.Zona, .groupCount = group.Count()}).
            OrderBy(Function(z) z.groupCount)

        If query.Count > 0 Then retval = query.Last.Zona
        Return retval
    End Function

    Shared Function BestLocation(oDistributors As List(Of DTOProductDistributor)) As DTOBaseGuid
        Dim retval As DTOBaseGuid = Nothing
        Dim query = oDistributors.GroupBy(Function(g) New With {Key g.Location}).
            Select(Function(group) New With {.Location = group.Key.Location, .groupCount = group.Count()}).
            OrderBy(Function(z) z.groupCount)

        If query.Count > 0 Then retval = query.Last.Location
        Return retval
    End Function

    Shared Function Excel(items As List(Of DTOProductDistributor)) As ExcelHelper.Sheet
        Dim sTitle As String = String.Format("M+O {0} {1} {2:00}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
        Dim sFilename As String = String.Format("M+O Store Locator {0:yyyyMMdd}.xlsx", DateTime.Today)
        Dim retval As New ExcelHelper.Sheet(sTitle, sFilename)
        With retval
            .AddColumn("Store Name")
            .AddColumn("Address")
            .AddColumn("City")
            .AddColumn("Postcode")
            .AddColumn("Region")
            .AddColumn("Country")
        End With

        For Each item In items
            Dim oRow As ExcelHelper.Row = retval.AddRow
            oRow.AddCell(item.Nom)
            oRow.AddCell(item.Adr)
            oRow.AddCell(item.Location.nom)
            oRow.AddCell(item.ZipCod)
            oRow.AddCell(item.Zona.nom)
            oRow.AddCell(item.Country.nom)
        Next

        Return retval
    End Function

End Class
