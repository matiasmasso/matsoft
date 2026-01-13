Public Class UrlHelper


    Shared Function Image(ByVal oType As DTO.Defaults.ImgTypes, ByVal oGuid As Guid, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = Factory(AbsoluteUrl, "img", CInt(oType).ToString, oGuid.ToString())
        Return retval
    End Function

    Shared Function Doc(ByVal oCod As DTODocFile.Cods, ByVal oGuid As Guid, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = Factory(AbsoluteUrl, "doc", oCod, oGuid.ToString())
        Return retval
    End Function

    Shared Function Dox(AbsoluteUrl As Boolean, doxcod As DTODocFile.Cods, ParamArray oParams() As String) As String
        Dim oParamsDictionary As New Dictionary(Of String, String)
        oParamsDictionary.Add("dox", doxcod)
        For i As Integer = 0 To oParams.Length - 2 Step 2
            oParamsDictionary.Add(oParams(i), oParams(i + 1))
        Next

        Dim sUrlFriendlyBase64Json As String = CryptoHelper.UrlFriendlyBase64Json(oParamsDictionary)
        Dim retval As String = Factory(AbsoluteUrl, "dox", sUrlFriendlyBase64Json)
        Return retval
    End Function

    Shared Function Factory(oCod As DTODocFile.Cods, oParameters As Dictionary(Of String, String), Optional AbsoluteUrl As Boolean = False) As String
        Dim sBase64Json As String = CryptoHelper.UrlFriendlyBase64Json(oParameters)
        Dim retval As String = Factory(AbsoluteUrl, "doc", CInt(oCod), sBase64Json)
        Return retval
    End Function

    Shared Function Factory(AbsoluteUrl As Boolean, ByVal ParamArray UrlSegments() As String) As String
        Dim oDomain As DTOWebDomain = DTOWebDomain.Default(AbsoluteUrl)
        Dim retval = oDomain.Url(UrlSegments)
        Return retval
    End Function

    '
    Shared Sub addQueryStringParam(ByRef url As String, sParam As String, ByVal sValue As String)
        Dim BlFirstParam As Boolean = url.IndexOf("?") = -1
        Dim sb As New System.Text.StringBuilder
        sb.Append(url)
        sb.Append(If(BlFirstParam, "?", "&"))
        sb.Append(sParam)
        sb.Append("=")
        sb.Append(sValue)
        url = sb.ToString
    End Sub

End Class

