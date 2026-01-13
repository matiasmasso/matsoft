Imports Newtonsoft.Json.Schema
Imports Newtonsoft.Json.Linq

Public Class JsonSchemaHelper
    Shared Function Validate(jsonSchema As String, sJObject As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim oJObject As JObject = JObject.Parse(sJObject)
            Dim oJSchema As JSchema = JSchema.Parse(jsonSchema)
            Dim errorMessages As IList(Of String) = Nothing
            retval = oJObject.IsValid(oJSchema, errorMessages)
            If errorMessages IsNot Nothing Then
                For Each e As String In errorMessages
                    exs.Add(New Exception(e))
                Next
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function
End Class
