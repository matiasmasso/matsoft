Public Class ElCorteInglesDept

    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTO.Integracions.ElCorteIngles.Dept)
        Return Await Api.Fetch(Of DTO.Integracions.ElCorteIngles.Dept)(exs, "ElCorteIngles/dept", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oTemplate As DTO.Integracions.ElCorteIngles.Dept) As Boolean
        If Not oTemplate.IsLoaded And Not oTemplate.IsNew Then
            Dim pTemplate = Api.FetchSync(Of DTO.Integracions.ElCorteIngles.Dept)(exs, "ElCorteIngles/dept", oTemplate.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTO.Integracions.ElCorteIngles.Dept)(pTemplate, oTemplate, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Upload(exs As List(Of Exception), value As DTO.Integracions.ElCorteIngles.Dept) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.PlantillaModSkuDocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.PlantillaModSkuDocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.PlantillaModSkuDocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ElCorteIngles/dept/upload")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oTemplate As DTO.Integracions.ElCorteIngles.Dept) As Task(Of Boolean)
        Return Await Api.Delete(Of DTO.Integracions.ElCorteIngles.Dept)(oTemplate, exs, "ElCorteIngles/dept")
    End Function

End Class

Public Class ElCorteInglesDepts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTO.Integracions.ElCorteIngles.Dept))
        Return Await Api.Fetch(Of List(Of DTO.Integracions.ElCorteIngles.Dept))(exs, "ElCorteIngles/depts")
    End Function


End Class

