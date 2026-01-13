Public Class SepaMe
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOSepaMe)
        Return Await Api.Fetch(Of DTOSepaMe)(exs, "SepaMe", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oSepaMe As DTOSepaMe) As Boolean
        If Not oSepaMe.IsLoaded And Not oSepaMe.IsNew Then
            Dim pSepaMe = Api.FetchSync(Of DTOSepaMe)(exs, "SepaMe", oSepaMe.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSepaMe)(pSepaMe, oSepaMe, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Upload(exs As List(Of Exception), value As DTOSepaMe) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "SepaMe")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oSepaMe As DTOSepaMe) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSepaMe)(oSepaMe, exs, "SepaMe")
    End Function
End Class

Public Class SepaMes
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOSepaMe))
        Return Await Api.Fetch(Of List(Of DTOSepaMe))(exs, "SepaMes")
    End Function

End Class
