Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class XecController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Xec/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Xec.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Xec")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Xec")>
    Public Function Update(<FromBody> value As DTOXec) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Xec.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Xec")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Xec")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Xec/delete")>
    Public Function Delete(<FromBody> value As DTOXec) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Xec.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Xec")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Xec")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Xec/UpdateXecRebut")>
    Public Function Cobrament(<FromBody> oXec As DTOXec) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Xec.UpdateXecRebut(oXec, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al cobrar el Xec")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cobrar el Xec")
        End Try
        Return retval
    End Function

End Class

Public Class XecsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Xecs/{cca}")>
    Public Function All(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim values = BEBL.Xecs.All(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Xecs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Xecs/{emp}/{status}/{codPresentacio}")>
    Public Function All(emp As DTOEmp.Ids, status As DTOXec.StatusCods, codPresentacio As DTOXec.ModalitatsPresentacio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Xecs.All(oEmp, status, codPresentacio)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Xecs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Xecs/Headers/{lliurador}")>
    Public Function Headers(lliurador As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLliurador As DTOContact = Nothing
            If lliurador <> Nothing Then oLliurador = New DTOContact(lliurador)
            Dim values = BEBL.Xecs.Headers(oLliurador)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Xecs")
        End Try
        Return retval
    End Function

End Class
