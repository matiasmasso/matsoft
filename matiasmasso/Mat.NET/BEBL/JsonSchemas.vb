Public Class JsonSchema
    Shared Function Find(oGuid As Guid) As DTOJsonSchema
        Dim retval As DTOJsonSchema = JsonSchemaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(id As DTOJsonSchema.Wellknowns) As DTOJsonSchema
        Dim retval = DTOJsonSchema.Wellknown(id)
        Load(retval)
        Return retval
    End Function

    Shared Function Load(ByRef oJsonSchema As DTOJsonSchema) As Boolean
        Dim retval As Boolean = JsonSchemaLoader.Load(oJsonSchema)
        Return retval
    End Function

    Shared Function Update(oJsonSchema As DTOJsonSchema, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = JsonSchemaLoader.Update(oJsonSchema, exs)
        Return retval
    End Function

    Shared Function Delete(oJsonSchema As DTOJsonSchema, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = JsonSchemaLoader.Delete(oJsonSchema, exs)
        Return retval
    End Function

    Shared Function Validate(id As DTOJsonSchema.Wellknowns, sJObject As String, exs As List(Of Exception)) As Boolean
        Dim oSchema = BEBL.JsonSchema.Find(id)
        Dim retval As Boolean = JsonSchemaHelper.Validate(oSchema.Json, sJObject, exs)
        Return retval
    End Function
End Class


Public Class JsonSchemas

    Shared Function All() As List(Of DTOJsonSchema)
        Dim retval As List(Of DTOJsonSchema) = JsonSchemasLoader.All()
        Return retval
    End Function

End Class
