Imports Newtonsoft.Json
Imports SixLabors.ImageSharp

Public Class MediaResourceController
    Inherits _MatController

    Public Async Function Thumbnail(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim hash = CryptoHelper.FromUrFriendlyBase64(id)
        Dim oMediaResource As New DTOMediaResource(hash)
        Dim value = Await FEB2.MediaResource.Thumbnail(exs, oMediaResource)
        If exs.Count = 0 Then
            If oMediaResource Is Nothing Then
            Else
                Dim oMimeCod As MimeCods = LegacyHelper.ImageHelper.GuessMime(value)
                Dim oImageFormat As System.Drawing.Imaging.ImageFormat = LegacyHelper.ImageHelper.GetImageFormat(oMimeCod)
                Dim sContentType As String = MediaHelper.ContentType(oMimeCod)
                MyBase.HttpContext.Response.Cache.SetMaxAge(New TimeSpan(24 * 360, 0, 0))

                Dim oStream As New System.IO.MemoryStream
                value.SaveAsJpeg(oStream)
                oStream.Position = 0
                retval = New FileStreamResult(oStream, sContentType) ' "image/jpeg")
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Public Async Function Media(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim hash = CryptoHelper.FromUrFriendlyBase64(id)
        Dim oMediaResource = Await FEB2.MediaResource.Find(exs, hash)
        Dim url = String.Format("~/Recursos/{0}", DTOMediaResource.TargetFilename(oMediaResource))
        Dim path As String = Server.MapPath(url)
        Dim oBuffer As Byte() = Nothing
        If MatHelperStd.FileSystemHelper.GetStreamFromFile(path, oBuffer, exs) Then
            Dim filename = DTOMediaResource.FriendlyName(oMediaResource)
            Dim sContentType As String = MediaHelper.ContentType(oMediaResource.Mime)
            retval = New FileContentResult(oBuffer, sContentType)
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" & filename & "")
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Public Async Function Upload() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Try
            Dim json As String = Request("serialized")
            Dim value As DTOMediaResource = JsonConvert.DeserializeObject(json, GetType(DTOMediaResource))
            Dim oFile As HttpPostedFileBase = Request.Files("stream")
            Dim oThumbnail As HttpPostedFileBase = Request.Files("thumbnail")
            value.Thumbnail = SixLabors.ImageSharp.Image.Load(oThumbnail.InputStream)
            If oFile IsNot Nothing Then
                Using binaryReader As New System.IO.BinaryReader(oFile.InputStream)
                    value.Stream = binaryReader.ReadBytes(oFile.ContentLength)
                End Using
                Dim url = String.Format("~/Recursos/{0}", DTOMediaResource.TargetFilename(value))
                Dim path As String = Server.MapPath(url)
                FileSystemHelper.SaveStream(exs, value.Stream, path)
            End If
            If exs.Count = 0 Then
                If Await FEB2.MediaResource.UpdateData(value, exs) Then
                    retval = MyBase.Success
                Else
                    retval = MyBase.Fail(exs)
                End If
            Else
                retval = MyBase.Fail(exs)
            End If
        Catch ex As Exception
            exs.Add(ex)
            retval = MyBase.Fail(exs)
        End Try
        Return retval

    End Function

    Public Async Function MissingFiles() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim files = AllFilenamesFromFileSystem(exs)
        If exs.Count = 0 Then
            ViewBag.Title = "Missing media files"
            Dim model As DTOMediaResource.Collection = Await FEB2.MediaResources.GetMissingResources(exs, files)
            retval = View(model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Public Function AllFilenamesFromFileSystem(exs As List(Of Exception)) As List(Of String)
        Dim retval As New List(Of String)
        Try
            Dim IISfolder = DTOMediaResource.VIRTUALPATH
            Dim folder = Server.MapPath(IISfolder)
            retval = System.IO.Directory.GetFiles(folder).ToList()

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

End Class
