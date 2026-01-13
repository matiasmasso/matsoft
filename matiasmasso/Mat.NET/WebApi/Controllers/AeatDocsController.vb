Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AeatDocController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AeatDoc/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AeatDoc.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AeatDoc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AeatDoc/LastFromModel/{emp}/{cod}")>
    Public Function LastFromModel(emp As Integer, cod As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.AeatDoc.LastFromModel(oEmp, cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AeatDoc")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/AeatDoc")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOAeatDoc)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la AeatDoc")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.AeatDocLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.AeatDocLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.AeatDocLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/AeatDoc/delete")>
    Public Function Delete(<FromBody> value As DTOAeatDoc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AeatDoc.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AeatDoc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AeatDoc")
        End Try
        Return retval
    End Function

End Class

Public Class AeatDocsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AeatDocs/{emp}/{model}")>
    Public Function All(emp As Integer, model As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim oModel As New DTOAeatModel(model)
            Dim values = BEBL.AeatDocs.All(oEmp, oModel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les AeatDocs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AeatDocs/exercicis/{emp}")>
    Public Function Exercicis(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.AeatDocs.Exercicis(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les exercicis de Hisenda")
        End Try
        Return retval
    End Function

End Class

