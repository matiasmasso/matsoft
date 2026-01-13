Public Class LiniaTelefonConsum
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOLiniaTelefon.Consum)
        Return Await Api.Fetch(Of DTOLiniaTelefon.Consum)(exs, "LiniaTelefonConsum", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oLiniaTelefonConsum As DTOLiniaTelefon.Consum, exs As List(Of Exception)) As Boolean
        If Not oLiniaTelefonConsum.IsLoaded And Not oLiniaTelefonConsum.IsNew Then
            Dim pLiniaTelefonConsum = Api.FetchSync(Of DTOLiniaTelefon.Consum)(exs, "LiniaTelefonConsum", oLiniaTelefonConsum.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOLiniaTelefon.Consum)(pLiniaTelefonConsum, oLiniaTelefonConsum, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOLiniaTelefon.Consum, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "LiniaTelefonConsum")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oLiniaTelefonConsum As DTOLiniaTelefon.Consum, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOLiniaTelefon.Consum)(oLiniaTelefonConsum, exs, "LiniaTelefonConsum")
    End Function
End Class

Public Class LiniaTelefonConsums
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oLiniaTelefon As DTOLiniaTelefon = Nothing) As Task(Of List(Of DTOLiniaTelefon.Consum))
        Return Await Api.Fetch(Of List(Of DTOLiniaTelefon.Consum))(exs, "LiniaTelefonConsums", OpcionalGuid(oLiniaTelefon))
    End Function

End Class
