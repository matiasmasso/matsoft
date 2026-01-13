Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class NominaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/nomina/{cca}")>
    Public Function Find(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim value = BEBL.Nomina.Find(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la nomina")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/nomina")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTONomina)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la nomina")
            Else
                If value.Cca.DocFile IsNot Nothing Then
                    value.Cca.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.Cca.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.NominaLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.nominaLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.nominaLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Nomina/delete")>
    Public Function Delete(<FromBody> value As DTONomina) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Nomina.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Nomina")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Nomina")
        End Try
        Return retval
    End Function

End Class

Public Class NominasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Nominas/{staff}")>
    Public Function All(staff As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oStaff As New DTOStaff(staff)
            Dim values = BEBL.Nominas.All(oStaff)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Nomines")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/Nominas/FromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.Nominas.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Nomines")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Nominas/{emp}/{year}")>
    Public Function All(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Nominas.All(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Nomines")
        End Try
        Return retval
    End Function


End Class
