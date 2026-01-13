Public Class Correspondencia
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCorrespondencia)
        Return Await Api.Fetch(Of DTOCorrespondencia)(exs, "correspondencia", oGuid.ToString())
    End Function

    Shared Function Load(ByRef value As DTOCorrespondencia, exs As List(Of Exception)) As Boolean
        If Not value.IsLoaded And Not value.IsNew Then
            Dim tmp = Api.FetchSync(Of DTOCorrespondencia)(exs, "correspondencia", value.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCorrespondencia)(tmp, value, exs)
            End If
        End If
        Return (exs.Count = 0)
    End Function

    Shared Async Function Update(value As DTOCorrespondencia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCorrespondencia)(value, exs, "correspondencia")
    End Function

    Shared Async Function Upload(value As DTOCorrespondencia, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Correspondencia")
        End If
        Return retval
    End Function


    Shared Async Function Delete(value As DTOCorrespondencia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCorrespondencia)(value, exs, "correspondencia")
    End Function
End Class

Public Class Correspondencias
    Shared Async Function All(oContact As DTOContact, exs As List(Of Exception)) As Task(Of List(Of DTOCorrespondencia))
        Return Await Api.Fetch(Of List(Of DTOCorrespondencia))(exs, "correspondencies/fromContact", oContact.Guid.ToString())
    End Function

    Shared Function Excel(items As List(Of DTOCorrespondencia)) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("Correspoondencia")
        With retval
            .AddColumn("Id")
            .AddColumn("Data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Assumpte")
        End With
        For Each item In items
            Dim oRow As ExcelHelper.Row = retval.AddRow()
            oRow.AddCell(item.Formatted())
            oRow.AddCell(item.Fch)
            oRow.AddCell(item.Subject, FEB2.DocFile.DownloadUrl(item.DocFile, True))
        Next
        Return retval
    End Function
End Class
