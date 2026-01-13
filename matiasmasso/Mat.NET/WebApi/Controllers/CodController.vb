Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CodController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Cod/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Cod.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Codi")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Cod")>
    Public Function Update(<FromBody> value As DTOCod) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cod.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Codi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Codi")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Cod/delete")>
    Public Function Delete(<FromBody> value As DTOCod) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cod.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Codi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Codi")
        End Try
        Return retval
    End Function

End Class

Public Class CodsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Cods/{parent?}")>
    Public Function All(Optional parent As Guid? = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oParent As DTOCod = Nothing
            If parent IsNot Nothing Then oParent = New DTOCod(parent)
            Dim values = BEBL.Cods.All(oParent)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Codis")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Cods/IdNoms/{parent}/{lang}")>
    Public Function All(parent As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oParent = New DTOCod(parent)
            Dim oLang = DTOLang.Factory(lang)
            Dim oCods = BEBL.Cods.All(oParent)
            Dim values = oCods.Select(Function(x) New Models.Base.IdNom(x.Id, x.Nom.Tradueix(oLang))).ToList()

            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Codis")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Cods/Sort")>
    Public Function All(<FromBody> guids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cods.Sort(exs, guids) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al ordenar els codis")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al ordenar els codis")
        End Try
        Return retval
    End Function

End Class
