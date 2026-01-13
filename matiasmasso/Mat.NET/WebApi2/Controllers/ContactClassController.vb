Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactClassController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactClass/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ContactClass.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ContactClass")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContactClass")>
    Public Function Update(<FromBody> value As DTOContactClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContactClass.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ContactClass")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ContactClass")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContactClass/delete")>
    Public Function Delete(<FromBody> value As DTOContactClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContactClass.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ContactClass")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ContactClass")
        End Try
        Return retval
    End Function

End Class

Public Class ContactClasssController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactClasses")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ContactClasses.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactClasss")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ContactClasses/AllWithChannel")>
    Public Function AllWithChannel() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ContactClasses.AllWithChannel()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactClasss")
        End Try
        Return retval
    End Function

End Class
