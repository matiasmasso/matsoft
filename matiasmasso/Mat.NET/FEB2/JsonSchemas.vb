Public Class JsonSchema
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOJsonSchema)
        Return Await Api.Fetch(Of DTOJsonSchema)(exs, "JsonSchema", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oJsonSchema As DTOJsonSchema, exs As List(Of Exception)) As Boolean
        If Not oJsonSchema.IsLoaded And Not oJsonSchema.IsNew Then
            Dim pJsonSchema = Api.FetchSync(Of DTOJsonSchema)(exs, "JsonSchema", oJsonSchema.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOJsonSchema)(pJsonSchema, oJsonSchema, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oJsonSchema As DTOJsonSchema, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOJsonSchema)(oJsonSchema, exs, "JsonSchema")
        oJsonSchema.IsNew = False
    End Function

    Shared Async Function Delete(oJsonSchema As DTOJsonSchema, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOJsonSchema)(oJsonSchema, exs, "JsonSchema")
    End Function
End Class

Public Class JsonSchemas
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOJsonSchema))
        Return Await Api.Fetch(Of List(Of DTOJsonSchema))(exs, "JsonSchemas")
    End Function

End Class
