Public Class Raffle
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTORaffle)
        Dim retval = Await Api.Fetch(Of DTORaffle)(exs, "Raffle", oGuid.ToString())
        If retval IsNot Nothing AndAlso retval.Product IsNot Nothing Then
            Product.Load(retval.Product, exs)
        End If
        Return retval
    End Function
    Shared Async Function ImageFbFeatured(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Raffle/ImageFbFeatured", oGuid.ToString())
    End Function
    Shared Async Function ImageBanner600(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Raffle/ImageBanner600", oGuid.ToString())
    End Function
    Shared Async Function ImageCallToAction500(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Raffle/ImageCallToAction500", oGuid.ToString())
    End Function
    Shared Async Function ImageWinner(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Raffle/ImageWinner", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oRaffle As DTORaffle, exs As List(Of Exception)) As Boolean
        If Not oRaffle.IsLoaded And Not oRaffle.IsNew Then
            Dim pRaffle = Api.FetchSync(Of DTORaffle)(exs, "Raffle", oRaffle.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORaffle)(pRaffle, oRaffle, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(exs As List(Of Exception), value As DTORaffle) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("ImageBanner600", value.ImageBanner600)
            oMultipart.AddFileContent("ImageCallToAction500", value.ImageCallToAction500)
            oMultipart.AddFileContent("ImageFbFeatured", value.ImageFbFeatured)
            oMultipart.AddFileContent("ImageWinner", value.ImageWinner)
            retval = Await Api.Upload(oMultipart, exs, "Raffle")
        End If
        Return retval
    End Function


    Shared Async Function Delete(oRaffle As DTORaffle, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORaffle)(oRaffle, exs, "Raffle")
    End Function

    Shared Function WinnerNomSync(exs As List(Of Exception), oRaffle As DTORaffle) As String
        Return Api.FetchSync(Of String)(exs, "raffle/Winner/Nom", oRaffle.Guid.ToString())
    End Function

    Shared Function WinnerLocationSync(exs As List(Of Exception), oRaffle As DTORaffle) As String
        Return Api.FetchSync(Of String)(exs, "raffle/Winner/Location", oRaffle.Guid.ToString())
    End Function

    Shared Async Function RemoveWinner(exs As List(Of Exception), oRaffle As DTORaffle) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Raffle/RemoveWinner", oRaffle.Guid.ToString())
    End Function

    Shared Function ZonasSync(exs As List(Of Exception), oEmp As DTOEmp, oRaffle As DTORaffle) As List(Of DTOArea)
        Return Api.FetchSync(Of List(Of DTOArea))(exs, "raffle/zonas", oEmp.Id, oRaffle.Guid.ToString())
    End Function

    Shared Async Function Zonas(exs As List(Of Exception), oEmp As DTOEmp, oRaffle As DTORaffle) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "raffle/zonas", oEmp.Id, oRaffle.Guid.ToString())
    End Function

    Shared Async Function Locations(exs As List(Of Exception), oEmp As DTOEmp, oRaffle As DTORaffle, oZona As DTOBaseGuid) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "raffle/locations", oEmp.Id, oRaffle.Guid.ToString, oZona.Guid.ToString())
    End Function

    Shared Async Function Distributors(exs As List(Of Exception), oEmp As DTOEmp, oRaffle As DTORaffle, oLocation As DTOBaseGuid) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "raffle/distributors", oEmp.Id, oRaffle.Guid.ToString, oLocation.Guid.ToString())
    End Function

    Shared Async Function SetRandomWinner(oRaffle As DTORaffle, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Return Await Api.Fetch(Of DTOTaskResult)(exs, "raffle", oRaffle.Guid.ToString, "SetRandomWinner")
    End Function

    Shared Function ZoomUrl(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "sorteo", "zoom", oRaffle.Guid.ToString())
    End Function

    Shared Function PlayUrl(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = oRaffle.UrlExterna
        If retval = "" Then
            Dim oDomain = DTOWebDomain.Factory(oRaffle.Lang, AbsoluteUrl)
            retval = oDomain.Url(oDomain.DefaultLang.ISO6391, "sorteo/play", oRaffle.Guid.ToString())
            'retval = oDomain.Url("sorteo/play", oContestBase.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function PlayOrZoomUrl(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If DTO.GlobalVariables.Today() > oRaffle.FchTo Then
            retval = Raffle.ZoomUrl(oRaffle, AbsoluteUrl)
        Else
            retval = Raffle.PlayUrl(oRaffle, AbsoluteUrl)
        End If
        Return retval
    End Function

    Shared Function ThumbnailUrl(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.sorteofbfeatured200, oRaffle.Guid, AbsoluteUrl)
    End Function

    Shared Function ImgBanner600Url(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.SorteoBanner600, oRaffle.Guid, AbsoluteUrl)
    End Function

    Shared Function ImgCallAction500(oRaffle As DTORaffle, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.SorteoCallAction500, oRaffle.Guid, AbsoluteUrl)
    End Function

    Shared Function BasesUrl(oRaffle As DTORaffle, AbsoluteUrl As Boolean) As String
        Return UrlHelper.Factory(AbsoluteUrl, "sorteo", "bases", oRaffle.Guid.ToString())
    End Function

    Shared Function ImgWinnerUrl(oRaffle As DTORaffle) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.SorteoWinner, oRaffle.Guid)
    End Function

    Shared Function MailReminderUrl(oRaffle As DTORaffle) As String
        Return UrlHelper.Factory(True, "mail/RaffleReminder", oRaffle.Guid.ToString())
    End Function


    Shared Function FacebookPage(oRaffle As DTORaffle, oLang As DTOLang) As String
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        Dim oBrand = Product.Brand(exs, oRaffle.Product)
        If (oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Britax)) Or
            oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Romer)) Or
            oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Bob))) Then
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    retval = "https://www.facebook.com/BritaxPT/"
                Case Else
                    retval = "https://www.facebook.com/BritaxES/"
            End Select
        ElseIf oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms)) Then
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    retval = "https://www.facebook.com/4moms.pt/"
                Case Else
                    retval = "https://www.facebook.com/4momsES/"
            End Select
        ElseIf oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.TommeeTippee)) Then
            retval = "https://www.facebook.com/TommeeTippeeES/"
        ElseIf oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.JBimbi)) Then
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    retval = "https://www.facebook.com/JBimbiPT/"
                Case Else
                    retval = "https://www.facebook.com/JBimbiES/"
            End Select
        End If
        Return retval
    End Function

    Shared Function FacebookPageLabel(oRaffle As DTORaffle, oLang As DTOLang) As String
        Dim s As String = FacebookPage(oRaffle, oLang)
        Dim retval As String = s.Replace("https://", "")
        If retval.EndsWith("/") Then retval = retval.Substring(0, retval.Length - 1)
        Return retval
    End Function

End Class

Public Class Raffles
    Inherits _FeblBase

    Shared Async Function Headers(exs As List(Of Exception), Optional OnlyVisible As Boolean = False, Optional IncludeSummaries As Boolean = True, Optional oLang As DTOLang = Nothing, Optional year As Integer = 0) As Task(Of List(Of DTORaffle))
        If oLang Is Nothing Then oLang = DTOApp.current.lang
        Return Await Api.Fetch(Of List(Of DTORaffle))(exs, "raffles/Headers", OpcionalBool(OnlyVisible), OpcionalBool(IncludeSummaries), oLang.Tag, year)
    End Function

    Shared Async Function Model(exs As List(Of Exception), oLang As DTOLang, take As Integer, takeFrom As Integer, oUser As DTOUser) As Task(Of DTORaffle.HeadersModel)
        Dim retval = Await Api.Fetch(Of DTORaffle.HeadersModel)(exs, "raffles/HeadersModel", oLang.Tag, take, takeFrom, OpcionalGuid(oUser))
        With retval
            .Take = take
            .TakeFrom = takeFrom
        End With
        Return retval
    End Function

    Shared Async Function CurrentOrNext(exs As List(Of Exception), oLang As DTOLang) As Task(Of DTORaffle)
        Return Await Api.Fetch(Of DTORaffle)(exs, "raffles/CurrentOrNext", oLang.Tag)
    End Function

    Shared Function CurrentOrNextSync(exs As List(Of Exception), oLang As DTOLang) As DTORaffle
        Return Api.FetchSync(Of DTORaffle)(exs, "raffles/CurrentOrNext", oLang.Tag)
    End Function

    Shared Function DaysToNextRaffle(exs As List(Of Exception), oLang As DTOLang) As Integer
        Dim retval As Integer = NextFchFrom(exs, oLang).Subtract(DTO.GlobalVariables.Today()).TotalDays
        Return retval
    End Function

    Shared Function NextFchFrom(exs As List(Of Exception), oLang As DTOLang) As Date
        Return Api.FetchSync(Of Date)(exs, "raffles/CurrentOrNext", oLang.Tag)
    End Function

    Shared Async Function RafflesCount(exs As List(Of Exception), oLang As DTOLang) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "raffles/count", oLang.Tag)
    End Function

    Shared Function RafflesCountSync(exs As List(Of Exception), oLang As DTOLang) As Integer
        Return Api.FetchSync(Of Integer)(exs, "raffles/count", oLang.Tag)
    End Function

    Shared Async Function SetWinners(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Return Await Api.Fetch(Of DTOTaskResult)(exs, "raffles/SetWinners", oUser.Guid.ToString())
    End Function

    Shared Function Url(oLang As DTOLang, Optional AbsoluteUrl As Boolean = False) As String
        Dim oDomain = DTOWebDomain.Factory(oLang, AbsoluteUrl)
        If oLang.Equals(DTOLang.POR) Then
            Return oDomain.Url("sorteios")
        Else
            Return oDomain.Url("sorteos")
        End If
    End Function

    Shared Function FirstActiveContest(src As List(Of DTORaffle)) As DTORaffle
        Dim retval As DTORaffle = Nothing
        For Each item In src
            If DTORaffle.IsActive(item) Then
                retval = item
                Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function Excel(oRaffles As List(Of DTORaffle)) As MatHelper.Excel.Sheet

        Dim retval As New MatHelper.Excel.Sheet
        With retval
            .AddColumn("des de", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("fins", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("pais")
            .AddColumn("titol")
            .AddColumn("participants")
            .AddColumn("nous")
            .AddColumn("premi", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("publicitat", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        For Each item As DTORaffle In oRaffles
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            With item
                oRow.AddCell(.FchFrom)
                oRow.AddCell(.FchTo)
                oRow.AddCell(.Lang.Tag)
                oRow.AddCell(.Title, Raffle.ZoomUrl(item, True))
                oRow.AddCell(.ParticipantsCount)
                oRow.AddCell(.NewParticipantsCount)
                oRow.AddCellAmt(.CostPrize)
                oRow.AddCellAmt(.CostPubli)
            End With
        Next

        Return retval
    End Function


End Class
