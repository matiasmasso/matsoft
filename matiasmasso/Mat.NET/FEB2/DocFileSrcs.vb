Public Class DocFileSrc

    Shared Function Load(ByRef value As DTODocFileSrc, exs As List(Of Exception)) As Boolean
        Api.ExecuteSync(Of DTODocFileSrc)(value, exs, "DocFileSrc/load")
        Return exs.Count = 0
    End Function

    Shared Async Function Update(value As DTODocFileSrc, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Docfile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.Docfile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.Docfile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "DocFileSrc")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oDocFileSrc As DTODocFileSrc, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODocFileSrc)(oDocFileSrc, exs, "DocFileSrc")
    End Function
End Class

Public Class DocFileSrcs

    Shared Async Function All(value As DTOBaseGuid, exs As List(Of Exception)) As Task(Of List(Of DTODocFileSrc))
        Return Await Api.Fetch(Of List(Of DTODocFileSrc))(exs, "DocFileSrcs", value.Guid.ToString())
    End Function

End Class

