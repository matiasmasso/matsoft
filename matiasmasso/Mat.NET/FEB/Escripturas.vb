Public Class Escriptura

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOEscriptura)
        Return Await Api.Fetch(Of DTOEscriptura)(exs, "Escriptura", oGuid.ToString())
    End Function

    Shared Async Function FromCodi(oEmp As DTOEmp, oCodi As DTOEscriptura.Codis, exs As List(Of Exception)) As Task(Of DTOEscriptura)
        Return Await Api.Fetch(Of DTOEscriptura)(exs, "Escriptura/fromCodi", CInt(oEmp.Id), CInt(oCodi))
    End Function

    Shared Function Load(ByRef oEscriptura As DTOEscriptura, exs As List(Of Exception)) As Boolean
        If Not oEscriptura.IsLoaded And Not oEscriptura.IsNew Then
            Dim pEscriptura = Api.FetchSync(Of DTOEscriptura)(exs, "Escriptura", oEscriptura.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEscriptura)(pEscriptura, oEscriptura, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOEscriptura, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Escriptura")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oEscriptura As DTOEscriptura, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEscriptura)(oEscriptura, exs, "Escriptura")
    End Function



    Shared Function UrlFromCodi(exs As List(Of Exception), oEmp As DTOEmp, oCodi As DTOEscriptura.Codis, oDomain As DTOWebDomain) As String
        Dim retval As String = ""
        If oDomain Is Nothing Then oDomain = DTOWebDomain.Default()
        Dim oEscriptura = Api.FetchSync(Of DTOEscriptura)(exs, "escriptura/fromCodi", CInt(oEmp.Id), CInt(oCodi))
        If oEscriptura IsNot Nothing AndAlso oEscriptura.docFile IsNot Nothing Then
            retval = oEscriptura.docFile.downloadUrl(oDomain)
        End If
        Return retval
    End Function

    Shared Function UrlConstitucio(exs As List(Of Exception), oEmp As DTOEmp, Optional oDomain As DTOWebDomain = Nothing) As String
        Return UrlFromCodi(exs, oEmp, DTOEscriptura.Codis.constitucio, oDomain)
    End Function

    Shared Function UrlAdaptacioEstatuts(exs As List(Of Exception), oEmp As DTOEmp, Optional oDomain As DTOWebDomain = Nothing) As String
        Return UrlFromCodi(exs, oEmp, DTOEscriptura.Codis.adaptacio_Estatuts, oDomain)
    End Function

    Shared Function UrlPoders(exs As List(Of Exception), oEmp As DTOEmp, Optional oDomain As DTOWebDomain = Nothing) As String
        Return UrlFromCodi(exs, oEmp, DTOEscriptura.Codis.nomenament_Administradors, oDomain)
    End Function

    Shared Function UrlTitularitatReal(exs As List(Of Exception), oEmp As DTOEmp, Optional oDomain As DTOWebDomain = Nothing) As String
        Return UrlFromCodi(exs, oEmp, DTOEscriptura.Codis.titularitat_Real, oDomain)
    End Function
End Class

Public Class Escripturas

    Shared Async Function AllAsync(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOEscriptura))
        Return Await Api.Fetch(Of List(Of DTOEscriptura))(exs, "Escripturas", CInt(oEmp.Id))
    End Function

    Shared Function AllSync(oEmp As DTOEmp, exs As List(Of Exception)) As List(Of DTOEscriptura)
        Return Api.FetchSync(Of List(Of DTOEscriptura))(exs, "Escripturas", CInt(oEmp.Id))
    End Function

End Class
