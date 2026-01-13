Imports Newtonsoft.Json.Linq

Public Class Pdf2Jpg

    Public Shared Async Function Thumbnail(exs As List(Of Exception), Pdf As Byte()) As Threading.Tasks.Task(Of System.Drawing.Image)
        Dim oMultipart As New ApiHelper.MultipartHelper()
        oMultipart.AddFileContent("file", Pdf)
        Dim retval = Await Api.Upload(Of System.Drawing.Image)(oMultipart, exs, "Pdf2Jpg")
        Return retval
    End Function

End Class
