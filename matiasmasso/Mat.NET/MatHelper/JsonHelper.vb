Imports Newtonsoft.Json
Public Class JsonHelper

    Shared Function Stringify(src As Object) As String
        Dim retval As String = Newtonsoft.Json.JsonConvert.SerializeObject(src)
        Return retval
    End Function

    Shared Function Deserialize(jsonString As String) As Object
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim retval As Object = jss.Deserialize(Of Object)(jsonString)
        Return retval
    End Function

    Shared Function DeSerialize(Of T)(json As String, exs As List(Of Exception)) As T
        Return JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))
    End Function

    Shared Function Serialize(o As Object, exs As List(Of Exception)) As String
        Return Newtonsoft.Json.JsonConvert.SerializeObject(o, JsonSettings(exs))
    End Function

    Shared Function DictionaryFromJsonString(Json As String) As Dictionary(Of String, String)
        Dim o As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Json)
        Dim retval As Dictionary(Of String, String) = o.ToObject(Of Dictionary(Of String, String))
        Return retval
    End Function

    Shared Function JsonSettings(exs As List(Of Exception)) As JsonSerializerSettings
        Dim retval As New JsonSerializerSettings()
        With retval
            .ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            .NullValueHandling = NullValueHandling.Ignore
            .TypeNameHandling = TypeNameHandling.Auto
            .Formatting = Formatting.Indented
            .[Error] = Sub(sender, args)
                           If System.Diagnostics.Debugger.IsAttached Then
                               System.Diagnostics.Debugger.Break()
                           End If
                           exs.Add(New Exception("Error de serialització"))
                           exs.Add(New Exception(args.ErrorContext.Path))
                       End Sub
        End With
        Return retval
    End Function

End Class
