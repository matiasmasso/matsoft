Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProveidorController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Proveidor/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Proveidor.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Proveidor")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Proveidor")>
    Public Function Update(<FromBody> value As DTOProveidor) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Proveidor.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Proveidor")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Proveidor")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Proveidor/delete")>
    Public Function Delete(<FromBody> value As DTOProveidor) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Proveidor.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Proveidor")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Proveidor")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Proveidor/CheckFacturaAlreadyExists/{proveidor}/{emp}/{year}")>
    Public Function CheckFacturaAlreadyExists(proveidor As Guid, emp As Integer, year As Integer, <FromBody> franum As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.Proveidor.CheckFacturaAlreadyExists(oProveidor, oExercici, franum)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Proveidor")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Proveidor/SaveFactura")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json_Cca As String = oHelper.GetValue("serialized_Cca")
            Dim json_Pnds As String = oHelper.GetValue("Serialized_Pnds")
            Dim json_Importacio As String = oHelper.GetValue("Serialized_Importacio")
            Dim oCca = ApiHelper.Client.DeSerialize(Of DTOCca)(json_Cca, exs)
            Dim oPnds = ApiHelper.Client.DeSerialize(Of IEnumerable(Of DTOPnd))(json_Pnds, exs)
            Dim oImportacio = ApiHelper.Client.DeSerialize(Of DTOImportacio)(json_Importacio, exs)

            If oCca Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la factura")
            Else
                If oCca.DocFile IsNot Nothing Then
                    oCca.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    oCca.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If BEBL.Proveidor.SaveFactura(exs, oCca, oPnds, oImportacio) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, oCca)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al desar la factura")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al desar la factura")
        End Try

        Return result
    End Function
End Class
