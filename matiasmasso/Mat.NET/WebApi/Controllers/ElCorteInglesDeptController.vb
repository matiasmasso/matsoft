Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ElCorteInglesDeptController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ElCorteIngles/Dept/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ElCorteInglesDept.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Departament")
        End Try
        Return retval
    End Function




    <HttpPost>
    <Route("api/ElCorteIngles/Dept/delete")>
    Public Function Delete(<FromBody> value As DTO.Integracions.ElCorteIngles.Dept) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ElCorteInglesDept.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Departament")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Departament")
        End Try
        Return retval
    End Function


End Class

Public Class ElCorteInglesDeptsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ElCorteIngles/Depts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ElCorteInglesDepts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Departaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ElCorteIngles/AlineamientoDisponibilidad")>
    Public Function AlineamientoDisponibilidad() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ElCorteInglesDepts.AlineamientoDisponibilidad()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al generar alineaiento de disponibilidad")
        End Try
        Return retval
    End Function

End Class

