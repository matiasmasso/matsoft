Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RaffleParticipantController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RaffleParticipant/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.RaffleParticipant.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RaffleParticipant")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RaffleParticipant/PlayModelFactory/{raffle}/{user}/{lang}")>
    Public Function PlayModelFactory(raffle As Guid, user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle = BEBL.Raffle.Find(raffle)
            Dim oUser As DTOUser = Nothing
            If Not user.Equals(Guid.Empty) Then oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.RaffleParticipant.PlayModelFactory(oRaffle, oUser, oLang)
            'value.Participant.Raffle.Bases = HttpUtility.HtmlEncode(value.Participant.Raffle.Bases)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al crear el model RafflePlayModel")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RaffleParticipant/search/{raffle}/{user}")>
    Public Function Search(raffle As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle As New DTORaffle(raffle)
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.RaffleParticipant.Find(oRaffle, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RaffleParticipant")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RaffleParticipant")>
    Public Function Update(<FromBody> value As DTORaffleParticipant) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RaffleParticipant.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RaffleParticipant")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RaffleParticipant")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RaffleParticipant/delete")>
    Public Function Delete(<FromBody> value As DTORaffleParticipant) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RaffleParticipant.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la RaffleParticipant")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la RaffleParticipant")
        End Try
        Return retval
    End Function

End Class

Public Class RaffleParticipantsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RaffleParticipants/{raffle}")>
    Public Function All(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle As New DTORaffle(raffle)
            Dim values = BEBL.RaffleParticipants.All(oRaffle)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RaffleParticipants")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RaffleParticipants/Compact/{raffle}")>
    Public Function Compact(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle As New DTORaffle(raffle)
            Dim values = BEBL.RaffleParticipants.Compact(oRaffle)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RaffleParticipants")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RaffleParticipants/valids/{raffle}")>
    Public Function Valids(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRaffle As New DTORaffle(raffle)
            Dim values = BEBL.RaffleParticipants.Valids(oRaffle)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RaffleParticipants")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RaffleParticipants/delete")>
    Public Function Delete(<FromBody> values As List(Of DTORaffleParticipant)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RaffleParticipants.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la RaffleParticipant")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la RaffleParticipant")
        End Try
        Return retval
    End Function
End Class

