Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TransmisioController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Transmisio/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Transmisio.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Transmisio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Transmisio/XmlFile/{guid}")>
    Public Function XmlFile(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTransmisio = BEBL.Transmisio.Find(guid)
            Dim exs As New List(Of Exception)
            Dim xmlFileSource = BEBL.Transmisio.XmlFileSource(oTransmisio, exs)
            If exs.Count = 0 Then
                Dim value = New System.Text.UTF8Encoding().GetBytes(xmlFileSource)
                retval = MyBase.HttpFileAttachmentResponseMessage(value, oTransmisio.FileNameDades())
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al crear el fitxer de dades de la Transmisio")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Transmisio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Transmisio/PdfDeliveries/{guid}")>
    Public Function PdfDeliveries(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTransmisio = BEBL.Transmisio.Find(guid)
            If oTransmisio Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("No s'ha trobat la Transmisio")
            Else
                Dim value As Byte() = BEBL.Transmisio.DeliveriesPdfStream(oTransmisio, exs)
                If exs.Count = 0 Then
                    retval = MyBase.HttpFileAttachmentResponseMessage(value, oTransmisio.FileNameDocs)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al generar els albarans de la Transmisio")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al generar els albarans de la Transmisio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/transmisio/{emp}/{year}/{id}")>
    Public Function FromNum(emp As Integer, year As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Transmisio.FromNum(oEmp, year, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Transmisio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/transmisio")>
    Public Function Update(<FromBody> value As DTOTransmisio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Transmisio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Transmisio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Transmisio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Transmisio/delete")>
    Public Function Delete(<FromBody> value As DTOTransmisio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Transmisio.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Transmisio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Transmisio")
        End Try
        Return retval
    End Function





    <HttpGet>
    <Route("api/transmisio/send/{guid}")>
    Public Function Send(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTransmisio = BEBL.Transmisio.Find(guid)
            If BEBL.Transmisio.Send(oTransmisio, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la Transmisio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la Transmisio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/transmisio/send/{guid}")>
    Public Function SendTo(guid As Guid, <FromBody> sTo As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTransmisio = BEBL.Transmisio.Find(guid)
            If BEBL.Transmisio.Send(oTransmisio, exs, sTo) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la Transmisio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la Transmisio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Transmisio/excel/{transmisio}")>
    Public Function ExcelAlbarans(transmisio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTransmisio = BEBL.Transmisio.Find(transmisio)
            Dim value = BEBL.Transmisio.Excel(oTransmisio, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al redactar l'Excel dels albarans de la transmisió")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al redactar l'Excel dels albarans de la transmisió")
        End Try
        Return retval
    End Function
End Class


Public Class TransmisionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Transmisions/{mgz}")>
    Public Function All(mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.Transmisions.All(oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Transmisions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Transmisions/HoldingHeaders/{holding}/{daysFrom}")>
    Public Function All(holding As Guid, daysfrom As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oHolding As New DTOHolding(holding)
            Dim values = BEBL.Transmisions.HoldingHeaders(oHolding, daysfrom)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Transmisions")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Transmisions/Orders")>
    Public Function Orders(<FromBody> guids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Transmisions.Orders(guids)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les comandes de les Transmisions")
        End Try
        Return retval
    End Function


End Class