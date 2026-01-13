Public Class Raffle 'BEBL

    Shared Function Find(oGuid As Guid) As DTORaffle
        Dim retval As DTORaffle = RaffleLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function ImgBanner600(oRaffle As DTORaffle) As ImageMime
        Return RaffleLoader.ImgBanner600(oRaffle)
    End Function

    Shared Function ImgCallToAction500(oRaffle As DTORaffle) As ImageMime
        Return RaffleLoader.ImgCallToAction500(oRaffle)
    End Function

    Shared Function ImgFbFeatured116(oRaffle As DTORaffle) As ImageMime
        Return RaffleLoader.ImgFbFeatured116(oRaffle)
    End Function

    Shared Function ImgWinner(oRaffle As DTORaffle) As ImageMime
        Return RaffleLoader.ImgWinner(oRaffle)
    End Function

    Shared Sub Load(oRaffle As DTORaffle)
        RaffleLoader.Load(oRaffle)
    End Sub

    Shared Function Update(oRaffle As DTORaffle, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RaffleLoader.Update(oRaffle, exs)
        Return retval
    End Function

    Shared Function Delete(oRaffle As DTORaffle, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RaffleLoader.Delete(oRaffle, exs)
        Return retval
    End Function

    Shared Function RemoveWinner(oRaffle As DTORaffle, ByRef exs As List(Of Exception)) As Boolean
        RaffleLoader.Load(oRaffle)
        oRaffle.Winner = Nothing
        Dim retval = RaffleLoader.Update(oRaffle, exs)
        Return retval
    End Function

    Shared Function WinnerNom(oRaffle As DTORaffle) As String
        Dim retval As String = ""
        If oRaffle.Winner IsNot Nothing Then
            RaffleParticipantLoader.Load(oRaffle.Winner)
            Dim oWinner As DTOUser = oRaffle.Winner.User
            If oWinner.Nom = "" Then UserLoader.Load(oWinner)
            Dim sb As New System.Text.StringBuilder
            sb.Append(oWinner.Nom)
            If sb.Length > 0 Then sb.Append(" ")
            sb.Append(oWinner.Cognoms)
            retval = sb.ToString
        End If
        Return retval
    End Function

    Shared Function WinnerLocation(oRaffle As DTORaffle) As String
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        If oRaffle.Winner IsNot Nothing Then
            RaffleParticipantLoader.Load(oRaffle.Winner)
            Dim oWinner As DTOUser = oRaffle.Winner.User
            If oWinner.Nom = "" Then BEBL.User.Load(oWinner)
            Dim sb As New System.Text.StringBuilder
            sb.Append(oWinner.LocationNom)
            retval = sb.ToString
        End If
        Return retval
    End Function


    Shared Function SetRandomWinner(ByRef oRaffle As DTORaffle, exs As List(Of Exception)) As Boolean
        RaffleLoader.Load(oRaffle)
        Dim oValidParticipants As List(Of DTORaffleParticipant) = RaffleParticipantsLoader.Valids(oRaffle)
        Dim oWinner As DTORaffleParticipant = GetRandomWinner(oValidParticipants)
        If oWinner IsNot Nothing Then
            oRaffle.Winner = oWinner
            RaffleLoader.Update(oRaffle, exs)
        End If
        Return exs.Count = 0 And oWinner IsNot Nothing
    End Function


    Shared Function GetRandomWinner(oValidParticipants As List(Of DTORaffleParticipant)) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = Nothing
        If oValidParticipants.Count > 0 Then
            Dim iWinner As Integer = BEBL.DiversHelper.RandomNumber(oValidParticipants.Count - 1)
            retval = oValidParticipants(iWinner)
        End If
        Return retval
    End Function


    Shared Function Zonas(oRaffle As DTORaffle) As List(Of DTOGuidNom)
        Return RaffleLoader.Zonas(oRaffle)
    End Function

    Shared Function PlayUrl(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = oRaffle.UrlExterna
        If retval = "" Then
            Dim oDomain = DTOWebDomain.Factory(oRaffle.Lang, AbsoluteUrl)
            retval = oDomain.Url("sorteo", "play", oRaffle.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function ImgBanner600Url(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.SorteoBanner600, oRaffle.Guid, AbsoluteUrl)
    End Function

    Shared Function BrandNom(oRaffle As DTORaffle) As String
        Dim oBrand As DTOProduct = BEBL.Raffle.Brand(oRaffle)
        Dim retval As String = DTOProduct.GetNom(oBrand)
        Return retval
    End Function

    Shared Function Brand(oRaffle As DTORaffle) As DTOProduct
        Dim retval = BEBL.Product.Brand(oRaffle.Product)
        Return retval
    End Function

    Shared Function Distributors(oEmp As DTOEmp, oRaffle As DTORaffle, oArea As DTOArea) As List(Of DTOProductDistributor)
        Dim oBrand As DTOProductBrand = DTOProduct.Brand(oRaffle.Product)

        If oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Gro)) Then
            oBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)
        End If

        Dim oDistributors = StoreLocatorLoader.Distributors(oRaffle.lang, oBrand, oArea)

        Dim DaysDeadline As Integer = oBrand.WebAtlasRafflesDeadline
        If DaysDeadline = 0 Then DaysDeadline = 60
        Dim LatestPdcFchFrom = DTO.GlobalVariables.Today().AddDays(-DaysDeadline)
        Dim retval = oDistributors.Where(Function(x) x.Raffles = True And x.LastFch >= LatestPdcFchFrom).ToList

        Return retval
    End Function

    Shared Function Locations(oEmp As DTOEmp, oRaffle As DTORaffle, oZona As DTOArea) As List(Of DTOArea)
        Dim oDistributors = BEBL.Raffle.Distributors(oEmp, oRaffle, oZona)
        Dim retval = DTOProductDistributor.Locations(oDistributors)
        Return retval
    End Function

    Shared Function Zonas(oEmp As DTOEmp, oRaffle As DTORaffle, oCountry As DTOArea) As List(Of DTOArea)
        Dim oDistributors = BEBL.Raffle.Distributors(oEmp, oRaffle, oCountry)
        Dim retval = DTOProductDistributor.Zonas(oDistributors)
        Return retval
    End Function

End Class

Public Class Raffles 'BEBL

    Shared Function Headers(Optional OnlyVisible As Boolean = False, Optional IncludeSummaries As Boolean = True, Optional oLang As DTOLang = Nothing, Optional year As Integer = 0) As List(Of DTORaffle)
        Dim retval As List(Of DTORaffle) = RafflesLoader.AllRaffleHeaders(OnlyVisible, IncludeSummaries, oLang, year)
        Return retval
    End Function

    Shared Function CompactHeaders(oLang As DTOLang) As List(Of DTORaffle.Compact)
        Return RafflesLoader.CompactHeaders(oLang)
    End Function


    Shared Function CurrentOrNextRaffle(oLang As DTOLang) As DTORaffle
        Dim retval As DTORaffle = Nothing
        'Dim oAllRaffles As List(Of DTORaffle) = RafflesLoader.AllRaffleHeaders(OnlyVisible:=True)
        Dim oTunedLang As DTOLang = DTOLang.PortugueseOrEsp(oLang)
        Dim oAllRaffles As List(Of DTORaffle) = RafflesLoader.Headers(oTunedLang, OnlyVisible:=True)
        Dim oLastRaffles As List(Of DTORaffle) = oAllRaffles.FindAll(Function(x) x.FchFrom <= DTO.GlobalVariables.Today())
        If oLastRaffles.Count > 0 Then
            If DTORaffle.IsActive(oLastRaffles(0)) Then
                retval = oLastRaffles(0)
            End If
        End If

        If retval Is Nothing Then
            Dim oNextRaffles As List(Of DTORaffle) = oAllRaffles.FindAll(Function(x) x.FchFrom > DTO.GlobalVariables.Today())
            If oNextRaffles.Count > 0 Then
                retval = oNextRaffles.Last
            End If
        End If

        Return retval
    End Function

    Shared Function NextFchFrom(oLang As DTOLang) As Date
        Dim oTunedLang As DTOLang = DTOLang.PortugueseOrEsp(oLang)
        Dim retval As Date = RafflesLoader.NextFchFrom(oTunedLang)
        Return retval
    End Function

    Shared Function HeadersModel(oLang As DTOLang, take As Integer, takeFrom As Integer, Optional oUser As DTOUser = Nothing) As DTORaffle.HeadersModel
        Return RafflesLoader.HeadersModel(oLang, take, takeFrom, oUser)
    End Function

    Shared Function SetWinners(exs As List(Of Exception), Optional ByRef oTask As DTOTask = Nothing) As Boolean
        Dim retval As Boolean
        Try
            Dim oWinners As New List(Of DTORaffleParticipant)
            Dim oRaffles = RafflesLoader.DueRaffles()
            If oRaffles.Count = 0 Then
                If oTask IsNot Nothing Then
                    oTask.LastLog.ResultCod = DTOTask.ResultCods.Empty
                    oTask.LastLog.ResultMsg = "Cap sorteig vençut"
                End If
                retval = True
            Else
                For Each oRaffle As DTORaffle In oRaffles
                    If BEBL.Raffle.SetRandomWinner(oRaffle, exs) Then
                        oWinners.Add(oRaffle.Winner)
                    End If
                Next

                If oWinners.Count = 0 Then
                    If oTask IsNot Nothing Then
                        oTask.LastLog.ResultCod = DTOTask.ResultCods.Empty
                        oTask.LastLog.ResultMsg = String.Format("No s'ha trovat guanyador per cap dels sortejos {0} pendents:", oRaffles.Count)
                    End If
                    retval = True
                Else
                    Dim sb As New System.Text.StringBuilder
                    For Each oWinner As DTORaffleParticipant In oWinners
                        Dim oWinnerUser As DTOUser = oWinner.User
                        sb.AppendLine("sorteig " & oWinner.Raffle.Title)
                        If oWinnerUser IsNot Nothing Then
                            sb.AppendLine("guanyador " & oWinnerUser.Nom & " " & oWinnerUser.Cognoms)
                        End If
                    Next
                    If oTask IsNot Nothing Then
                        oTask.LastLog.ResultCod = DTOTask.ResultCods.Success
                        oTask.LastLog.ResultMsg = sb.ToString()
                    End If

                    retval = True
                End If
            End If
        Catch ex As Exception
            exs.Add(New Exception("BEBL.Raffles.SetWinners: Error al assignar guanyador del sorteig"))
            exs.Add(ex)
            If oTask IsNot Nothing Then
                oTask.LastLog.ResultCod = DTOTask.ResultCods.Failed
            End If
        End Try

        Return retval
    End Function

    Shared Function RafflesCount(oLang As DTOLang) As Integer
        Dim oTunedLang As DTOLang = DTOLang.PortugueseOrEsp(oLang)
        Dim retval As Integer = RafflesLoader.RafflesCount(oTunedLang)
        Return retval
    End Function

End Class
