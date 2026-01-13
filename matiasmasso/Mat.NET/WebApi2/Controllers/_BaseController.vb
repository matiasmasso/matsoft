Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Http
Imports Newtonsoft.Json
Imports MatHelperStd

Public Class _BaseController
    Inherits ApiController

    'takes authorisation header as user primary key
    Public Function GetUser(exs As List(Of Exception)) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim ApiKey = Request.Headers.GetValues("ApiKey")

        If ApiKey.Count = 1 Then
            Try
                Dim sApiKeyValues = CType(ApiKey, String())
                If sApiKeyValues.Length > 0 Then
                    Dim sApiKey = sApiKeyValues.First()
                    Dim userGuid = Guid.Parse(sApiKey.ToString())
                    If GlobalVariables.UsersCache.ContainsKey(userGuid) Then
                        retval = GlobalVariables.UsersCache(userGuid)
                    Else
                        retval = BEBL.User.Find(userGuid)
                        If retval Is Nothing Then Throw New Exception("invalid ApiKey")
                        retval.Emp = GetEmp(retval.Emp.Id)
                        GlobalVariables.UsersCache.Add(userGuid, retval)
                    End If
                Else
                    'ApiKey is missing
                    exs.Add(New Exception("missing authorisation key"))
                End If
            Catch ex As Exception
                'ApiKey is not in the right Guid format
                exs.Add(New Exception("wrong or missing authorisation key"))
            End Try
        End If
        Return retval
    End Function

    Public Sub UpdateUsersCache(oUser As DTOUser)
        If GlobalVariables.UsersCache.ContainsKey(oUser.Guid) Then
            GlobalVariables.UsersCache(oUser.Guid) = oUser
        End If
    End Sub

    Shared Function DateTimeFormat(DtFch As Date) As String
        Dim retval As String = DateFormat(DtFch.Date)
        Return retval
    End Function

    Shared Function DateFormat(DtFch As Date) As String
        Dim retval As String = DtFch.Date.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK")
        Return retval
    End Function

    Shared Function ParseDate(sFch As String) As Date
        Dim retval As Date = DTO.GlobalVariables.Today()
        If Microsoft.VisualBasic.IsDate(sFch) Then
            retval = CDate(sFch).Date
        End If
        Return retval
    End Function

    Shared Function ParseDateTime(sFch As String) As Date
        Dim retval As Date = DTO.GlobalVariables.Now()
        If Microsoft.VisualBasic.IsDate(sFch) Then
            retval = CDate(sFch)
        End If
        Return retval
    End Function

    Shared Sub ThrowException(reasonPhrase As String, content As String, Optional statusCode As Net.HttpStatusCode = Net.HttpStatusCode.SeeOther)
        Dim oResponseMessage As New System.Net.Http.HttpResponseMessage(statusCode)
        With oResponseMessage
            .ReasonPhrase = reasonPhrase
            .Content = New System.Net.Http.StringContent(content)
        End With
        Throw New HttpResponseException(oResponseMessage)
    End Sub

    Shared Function HttpErrorResponseMessage(s As String) As System.Net.Http.HttpResponseMessage
        Dim exs As New List(Of Exception)
        exs.Add(New Exception(s))
        Return HttpErrorResponseMessage(exs, s)
    End Function

    Shared Function HttpErrorResponseMessage(ex As Exception, Optional s As String = "") As System.Net.Http.HttpResponseMessage
        Dim exs As New List(Of Exception)
        exs.Add(ex)
        Return HttpErrorResponseMessage(exs, s)
    End Function

    Shared Function HttpErrorResponseMessage(exs As List(Of Exception), Optional s As String = "") As System.Net.Http.HttpResponseMessage
        Dim retval As New System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
        Dim sb As New Text.StringBuilder
        If Not String.IsNullOrEmpty(s) Then exs.Insert(0, New Exception(s))
        For Each ex In exs
            sb.AppendLine(ex.Message)
        Next
        retval.Content = New System.Net.Http.StringContent(sb.ToString())
        Dim reason As String = sb.ToString.Replace(vbCrLf, "-").Replace(vbLf, "-")
        If exs.Count > 0 Then
            retval.ReasonPhrase = Left(reason, 512)
        End If
        Return retval
    End Function

    Protected Function HttpImageResponseMessage(oImageBytes As Byte(), Optional oMime As MimeCods = MimeCods.Jpg) As HttpResponseMessage
        Dim oImageMime As MatHelperStd.ImageMime = Nothing
        If oImageBytes IsNot Nothing Then
            oImageMime = New ImageMime
            oImageMime.ByteArray = oImageBytes
            oImageMime.Mime = oMime
        End If
        Return HttpImageMimeResponseMessage(oImageMime)
    End Function

    Protected Function HttpImageMimeResponseMessage(oImageMime As ImageMime) As HttpResponseMessage
        If oImageMime Is Nothing OrElse oImageMime.ByteArray Is Nothing Then oImageMime = ImageMime.Factory()
        If oImageMime.Mime = MimeCods.NotSet Then oImageMime.Mime = MimeCods.Jpg
        Dim sMediaType As String = MimeHelper.GetMediaType(oImageMime.Mime)
        Dim retval = HttpBinaryResponseMessage(oImageMime.ByteArray)
        retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue(sMediaType)
        Return retval
    End Function

    Protected Function HttpIgnoreNullsResponseMessage(Of T)(value As T) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        Dim json = JsonConvert.SerializeObject(value, Formatting.Indented, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
        Dim oByteArray = New System.Text.UTF8Encoding().GetBytes(json)
        retval.Content = New ByteArrayContent(oByteArray)
        'retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        'retval.Content.Headers.ContentDisposition.FileName = filename
        'retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")

        'retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
        Return retval

    End Function

    Protected Function HttpPdfResponseMessage(oByteArray As Byte(), filename As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
        retval.Content.Headers.ContentDisposition.FileName = filename
        retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")

        'retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
        Return retval
    End Function

    Protected Function HttpExcelResponseMessage(oByteArray As Byte(), Optional filename As String = "") As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        retval = Request.CreateResponse(HttpStatusCode.OK)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
        retval.Content.Headers.ContentDisposition.FileName = filename
        retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        Return retval
    End Function

    Protected Function HttpExcelResponseMessage(oSheet As MatHelper.Excel.Sheet, Optional filename As String = "") As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oByteArray() As Byte = MatHelper.Excel.ClosedXml.Bytes(oSheet)
            If String.IsNullOrEmpty(filename) Then filename = oSheet.Filename
            retval = Request.CreateResponse(HttpStatusCode.OK)
            retval.Content = New ByteArrayContent(oByteArray)
            retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
            retval.Content.Headers.ContentDisposition.FileName = filename
            retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")

        Catch ex As Exception
            retval = HttpErrorResponseMessage(ex, "Error al redactar l'Excel")
        End Try
        Return retval
    End Function

    Protected Function HttpFileStreamResponseMessage(oByteArray As Byte(), filename As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
        retval.Content.Headers.ContentDisposition.FileName = filename

        If filename.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase) Then
            retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")
        Else
            retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
        End If
        'retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
        Return retval
    End Function

    Protected Function HttpFileAttachmentResponseMessage(oByteArray As Byte(), filename As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        retval.Content.Headers.ContentDisposition.FileName = filename

        If filename.EndsWith(".pdf") Then
            retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")
        Else
            retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
        End If
        'retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
        Return retval
    End Function

    Protected Function HttpXmlResponseMessage(Of T)(oSerializableObject As T) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        Dim oXmlFormatter As New System.Net.Http.Formatting.XmlMediaTypeFormatter
        oXmlFormatter.UseXmlSerializer = True
        retval.Content = New ObjectContent(Of T)(oSerializableObject, oXmlFormatter)
        Return retval
    End Function

    Protected Function HttpXmlFileResponseMessage(Text As String, filename As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        Dim oByteArray = New System.Text.UTF8Encoding().GetBytes(Text)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        retval.Content.Headers.ContentDisposition.FileName = filename
        retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")

        'retval.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
        Return retval
    End Function

    Protected Function HttpBinaryResponseMessage(oByteArray As Byte()) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK)
        retval.Content = New ByteArrayContent(oByteArray)
        retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
        Return retval
    End Function

    Protected Function GetEmp(empId As DTOEmp.Ids) As DTOEmp
        Return GlobalVariables.Emps.FirstOrDefault(Function(x) x.Id = empId)
    End Function

    Protected Function GetExercici(empId As DTOEmp.Ids, year As Integer) As DTOExercici
        Dim oEmp = GetEmp(empId)
        Dim retval = DTOExercici.FromYear(oEmp, year)
        Return retval
    End Function

    Protected Function ClientIP() As String
        Dim retval As String = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(retval) Then
            retval = System.Web.HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If
        Return retval
    End Function

End Class
