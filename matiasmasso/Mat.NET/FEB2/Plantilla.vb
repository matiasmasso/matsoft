Public Class Plantilla
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPlantilla)
        Return Await Api.Fetch(Of DTOPlantilla)(exs, "Plantilla", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPlantilla As DTOPlantilla) As Boolean
        If Not oPlantilla.IsLoaded And Not oPlantilla.IsNew Then
            Dim pPlantilla = Api.FetchSync(Of DTOPlantilla)(exs, "Plantilla", oPlantilla.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPlantilla)(pPlantilla, oPlantilla, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Upload(exs As List(Of Exception), value As DTOPlantilla, oEmp As DTOEmp) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Plantilla/upload", oEmp.Id)
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPlantilla As DTOPlantilla) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPlantilla)(oPlantilla, exs, "Plantilla")
    End Function
End Class

Public Class Plantillas
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOPlantilla))
        Return Await Api.Fetch(Of List(Of DTOPlantilla))(exs, "Plantillas", oEmp.Id)
    End Function

End Class
