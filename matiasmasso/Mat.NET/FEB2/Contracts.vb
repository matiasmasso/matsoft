Public Class Contract


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOContract)
        Return Await Api.Fetch(Of DTOContract)(exs, "Contract", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oContract As DTOContract, exs As List(Of Exception)) As Boolean
        If Not oContract.IsLoaded And Not oContract.IsNew Then
            Dim pContract = Api.FetchSync(Of DTOContract)(exs, "Contract", oContract.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContract)(pContract, oContract, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOContract, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Contract")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oContract As DTOContract, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContract)(oContract, exs, "Contract")
    End Function
End Class

Public Class Contracts

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, Optional oCodi As DTOContractCodi = Nothing, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOContract))
        Dim oContactGuid As Guid = Guid.Empty
        If oContact IsNot Nothing Then
            oContactGuid = oContact.Guid
        End If

        Dim oCodiGuid = Guid.Empty
        If oCodi IsNot Nothing Then
            oCodiGuid = oCodi.Guid
        End If

        Return Await Api.Fetch(Of List(Of DTOContract))(exs, "Contracts", oUser.Guid.ToString, oCodiGuid.ToString, oContactGuid.ToString())
    End Function

    Shared Function Excel(oContracts As List(Of DTOContract)) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("contractes", "M+O contractes")
        With retval
            .AddColumn("tipus", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("numero", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("caducitat", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("concepte", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("contractant", ExcelHelper.Sheet.NumberFormats.W50)
        End With

        For Each oContract In oContracts
            Dim oRow As ExcelHelper.Row = retval.AddRow()
            With oContract
                oRow.AddCell(.Codi.Nom)
                oRow.AddCell(.Num)
                oRow.AddCell(.fchFrom)
                oRow.AddCell(.fchTo)
                oRow.AddCell(.Nom, FEB2.DocFile.DownloadUrl(.DocFile, True))
                oRow.AddCell(.Contact.FullNom)
            End With
        Next

        Return retval
    End Function

End Class
