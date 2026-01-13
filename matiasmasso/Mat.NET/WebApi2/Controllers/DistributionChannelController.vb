Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DistributionChannelController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DistributionChannel/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DistributionChannel.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la DistributionChannel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/DistributionChannel")>
    Public Function Update(<FromBody> value As DTODistributionChannel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DistributionChannel.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la DistributionChannel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la DistributionChannel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/DistributionChannel/delete")>
    Public Function Delete(<FromBody> value As DTODistributionChannel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DistributionChannel.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la DistributionChannel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la DistributionChannel")
        End Try
        Return retval
    End Function

End Class

Public Class DistributionChannelsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DistributionChannels/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.DistributionChannels.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DistributionChannels")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DistributionChannels/AllWithContacts/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.DistributionChannels.AllWithContacts(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DistributionChannels")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DistributionChannels/Headers/{emp}/{lang}")>
    Public Function All(emp As DTOEmp.Ids, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp) ' a implementar
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.DistributionChannels.Headers(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DistributionChannels")
        End Try
        Return retval
    End Function


End Class

