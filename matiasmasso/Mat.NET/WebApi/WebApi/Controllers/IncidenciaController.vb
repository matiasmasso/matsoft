Public Class IncidenciaController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contact/incidencias")>
    Public Function ContactIncidencias(customer As DTOBaseGuid) As List(Of DTOIncidencia)
        Dim oCustomer As New DTOCustomer(customer.Guid)
        Dim retval As List(Of DTOIncidencia) = BLLIncidencias.Headers(Nothing, oCustomer)
        Return retval
    End Function

    <HttpPost>
    <Route("api/incidencia/attachments")>
    Public Function Attachments(incidencia As DUI.Guidnom) As List(Of DUI.Guidnom)
        Dim oIncidencia As DTOIncidencia = BLLIncidencia.Find(incidencia.Guid)
        Dim oAttachments As List(Of DTODocFile) = BLLIncidencia.Attachments(oIncidencia)
        Dim retval As New List(Of DUI.Guidnom)
        For Each oAttachment As DTODocFile In oAttachments
            Dim oGuidNom As New DUI.Guidnom
            oGuidNom.Nom = BLLDocFile.DownloadUrl(oAttachment, True)
            retval.Add(oGuidNom)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/incidencia/sprite")>
    Public Function sprite(incidencia As DUI.Guidnom) As System.Net.Http.HttpResponseMessage
        Dim exs As New List(Of Exception)

        Dim oIncidencia As DTOIncidencia = BLLIncidencia.Find(incidencia.Guid)
        Dim oSprite As DTOSprite = BLLSprite.Factory(oIncidencia, 100, exs)

        Dim oBytes() As Byte = BLL.ImageHelper.GetByteArrayFromImg(oSprite.Image)
        Dim MS As New System.IO.MemoryStream(oBytes)
        Dim retval As New System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
        With retval
            .Content = New System.Net.Http.StreamContent(MS)
            .Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg")
        End With
        Return retval
    End Function


End Class
