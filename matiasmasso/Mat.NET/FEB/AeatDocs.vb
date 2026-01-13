Public Class AeatDoc

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAeatDoc)
        Return Await Api.Fetch(Of DTOAeatDoc)(exs, "AeatDoc", oGuid.ToString())
    End Function
    Shared Function LastFromModelSync(oEmp As DTOEmp, oCod As DTOAeatModel.Cods, exs As List(Of Exception)) As DTOAeatDoc
        Return Api.FetchSync(Of DTOAeatDoc)(exs, "AeatDoc/LastFromModel", CInt(oEmp.Id), CInt(oCod))
    End Function

    Shared Function Load(ByRef oAeatDoc As DTOAeatDoc, exs As List(Of Exception)) As Boolean
        If Not oAeatDoc.IsLoaded And Not oAeatDoc.IsNew Then
            Dim pAeatDoc = Api.FetchSync(Of DTOAeatDoc)(exs, "AeatDoc", oAeatDoc.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAeatDoc)(pAeatDoc, oAeatDoc, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOAeatDoc, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "AeatDoc")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oAeatDoc As DTOAeatDoc, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAeatDoc)(oAeatDoc, exs, "AeatDoc")
    End Function

    Shared Async Function CreateNext(oEmp As DTOEmp, oModel As DTOAeatModel, exs As List(Of Exception)) As Task(Of DTOAeatDoc)
        Dim retval As New DTOAeatDoc
        With retval
            .Model = oModel
        End With

        Dim allDocs = Await AeatDocs.All(oEmp, oModel, exs)
        If allDocs.Count > 0 Then
            Dim oLastDoc As DTOAeatDoc = allDocs.First
            Select Case oModel.PeriodType
                Case DTOAeatModel.PeriodTypes.Mensual
                    If oLastDoc.Period = 12 Then
                        retval.Period = 1
                    Else
                        retval.Period = oLastDoc.Period + 1
                    End If
                    retval.Fch = TimeHelper.LastDayOfMonth(oLastDoc.Fch.AddMonths(1))
                Case DTOAeatModel.PeriodTypes.Anual
                    retval.Period = 1
                    retval.Fch = TimeHelper.LastDayOfYear(oLastDoc.Fch.AddYears(1))
            End Select
        End If
        Return retval
    End Function

End Class

Public Class AeatDocs

    Shared Async Function All(oEmp As DTOEmp, oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Task(Of DTOAeatDoc.Collection)
        Return Await Api.Fetch(Of DTOAeatDoc.Collection)(exs, "AeatDocs", CInt(oEmp.Id), oAeatModel.Guid.ToString())
    End Function

    Shared Async Function Exercicis(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOExercici))
        Return Await Api.Fetch(Of List(Of DTOExercici))(exs, "AeatDocs/exercicis", CInt(oEmp.Id))
    End Function

End Class

