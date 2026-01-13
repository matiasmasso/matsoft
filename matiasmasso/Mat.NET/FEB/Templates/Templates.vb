Public Class Template
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOTemplate)
        Return Await Api.Fetch(Of DTOTemplate)(exs, "Template", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oTemplate As DTOTemplate) As Boolean
        If Not oTemplate.IsLoaded And Not oTemplate.IsNew Then
            Dim pTemplate = Api.FetchSync(Of DTOTemplate)(exs, "Template", oTemplate.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTemplate)(pTemplate, oTemplate, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oTemplate As DTOTemplate) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTemplate)(oTemplate, exs, "Template")
        oTemplate.IsNew = False
    End Function

    Shared Async Function Upload(exs As List(Of Exception), value As DTOTemplate) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Template")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oTemplate As DTOTemplate) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTemplate)(oTemplate, exs, "Template")
    End Function
End Class

Public Class Templates
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOTemplate))
        Return Await Api.Fetch(Of List(Of DTOTemplate))(exs, "Templates")
    End Function

End Class
