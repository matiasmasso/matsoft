Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RaffleController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Raffle/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Raffle.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Raffle")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/ImageFbFeatured/{guid}")>
    Public Function ImageFbFeatured(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.sorteofbfeatured200)
            If oImageMime Is Nothing Then
                Dim oRaffle As New DTORaffle(guid)
                oImageMime = BEBL.Raffle.ImgFbFeatured116(oRaffle)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.sorteofbfeatured200, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la imatge del sorteig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/ImageBanner600/{guid}")>
    Public Function ImageBanner600(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.sorteobanner600)
            If oImageMime Is Nothing Then
                Dim oRaffle As New DTORaffle(guid)
                oImageMime = BEBL.Raffle.ImgBanner600(oRaffle)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.sorteobanner600, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el banner del sorteig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/ImageCallToAction500/{guid}")>
    Public Function ImageCallToAction500(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.sorteocallaction500)
            If oImageMime Is Nothing Then
                Dim oRaffle As New DTORaffle(guid)
                oImageMime = BEBL.Raffle.ImgCallToAction500(oRaffle)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.sorteocallaction500, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el banner Call To Action del sorteig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/ImageWinner/{guid}")>
    Public Function ImageWinner(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.sorteowinner)
            If oImageMime Is Nothing Then
                Dim oRaffle As New DTORaffle(guid)
                oImageMime = BEBL.Raffle.ImgWinner(oRaffle)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.sorteowinner, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Raffle")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Raffle")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTORaffle)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Raffle")
            Else
                value.ImageBanner600 = oHelper.GetImage("ImageBanner600")
                value.ImageCallToAction500 = oHelper.GetImage("ImageCallToAction500")
                value.ImageFbFeatured = oHelper.GetImage("ImageFbFeatured")
                value.ImageWinner = oHelper.GetImage("ImageWinner")

                If DAL.RaffleLoader.Update(value, exs) Then
                    GlobalVariables.CachedImages.Reset(value.Guid)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.RaffleLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.RaffleLoader")
        End Try

        Return result
    End Function

    <HttpPost>
    <Route("api/Raffle/delete")>
    Public Function Delete(<FromBody> value As DTORaffle) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Raffle.Delete(value, exs) Then
                GlobalVariables.CachedImages.Reset(value.Guid)
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Raffle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Raffle")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/Winner/Nom/{raffle}")>
    Public Function WinnerNom(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle = BEBL.Raffle.Find(raffle)
            Dim value = BEBL.Raffle.WinnerNom(oRaffle)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el nom del guanyador")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/Winner/Location/{raffle}")>
    Public Function WinnerLocation(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle = BEBL.Raffle.Find(raffle)
            Dim value = BEBL.Raffle.WinnerLocation(oRaffle)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població del guanyador")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Raffle/RemoveWinner/{raffle}")>
    Public Function RemoveWinner(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oRaffle As New DTORaffle(raffle)
            If BEBL.Raffle.RemoveWinner(oRaffle, exs) Then
                GlobalVariables.CachedImages.Reset(oRaffle.Guid)
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el guanyador")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el guanyador")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffle/zonas/{emp}/{raffle}")>
    Public Function Zonas(emp As DTOEmp.Ids, raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRaffle = BEBL.Raffle.Find(raffle)

            Dim values = BEBL.Raffle.Zonas(oEmp, oRaffle, oRaffle.Country)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les zones del sorteig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffle/locations/{emp}/{raffle}/{provinciaOrZona}")>
    Public Function Locations(emp As DTOEmp.Ids, raffle As Guid, provinciaOrZona As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRaffle = BEBL.Raffle.Find(raffle)
            Dim oProvinciaOrZona As New DTOArea(provinciaOrZona)

            Dim values = BEBL.Raffle.Locations(oEmp, oRaffle, oProvinciaOrZona)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les poblacions del sorteig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffle/distributors/{emp}/{raffle}/{location}")>
    Public Function Distributors(emp As DTOEmp.Ids, raffle As Guid, location As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRaffle = BEBL.Raffle.Find(raffle)
            Dim oLocation As New DTOArea(location)

            Dim values = BEBL.Raffle.Distributors(oEmp, oRaffle, oLocation)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors del sorteig")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/raffle/{raffle}/SetRandomWinner")>
    Public Function SetWinner(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oRaffle = DAL.RaffleLoader.Find(raffle)
            If BEBL.Raffle.SetRandomWinner(oRaffle, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al triar guanyador")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al triar guanyador")
        End Try
        Return retval
    End Function
End Class



Public Class RafflesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/raffles/{lang}")> 'for iMat 3.0
    Public Function CompactHeaders(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            oLang = DTOWebDomain.Factory(oLang).DefaultLang
            Dim values = BEBL.Raffles.CompactHeaders(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el sorteig vigent")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffles/headers/{OnlyVisible}/{IncludeSummaries}/{lang}/{year}")>
    Public Function Headers(OnlyVisible As Integer, IncludeSummaries As Integer, lang As String, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Raffles.Headers(OnlyVisible, IncludeSummaries, oLang, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el sorteig vigent")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffles/currentOrNext/{lang}")>
    Public Function currentOrNext(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Raffles.CurrentOrNextRaffle(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el sorteig vigent")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffles/NextFchFrom/{lang}")>
    Public Function NextFchFrom(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Raffles.NextFchFrom(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el sorteig vigent")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/raffles/HeadersModel/{lang}/{take}/{takeFrom}/{user}")>
    Public Function HeadersModel(lang As String, take As Integer, takeFrom As Integer, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then oUser = New DTOUser(user)
            Dim value = BEBL.Raffles.HeadersModel(oLang, take, takeFrom, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sortejos")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/raffles/count/{lang}")>
    Public Function RafflesCount(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Raffles.RafflesCount(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el numero de sortejos")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/raffles/SetWinners/{user}")>
    Public Async Function SetWinners(user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(exs, "usuari desconegut")
            Else
                Dim oTask = BEBL.Task.Find(DTOTask.Cods.SorteoSetWinners)
                oTask = Await BEBL.Task.Execute(exs, oTask, oUser)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al triar guanyadors")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al triar guanyadors")
        End Try
        Return retval
    End Function

End Class