Public Class UrlHelper
    Shared Sub addParam(ByRef sSrc As String, ByVal sParam As String, ByVal sValue As String)
        Dim BlFirstParam As Boolean = sSrc.IndexOf("?") = -1
        Dim sb As New System.Text.StringBuilder
        sb.Append(sSrc)
        sb.Append(If(BlFirstParam, "?", "&"))
        sb.Append(sParam)
        sb.Append("=")
        sb.Append(sValue)
        sSrc = sb.ToString
    End Sub


    Shared Function EncodedUrlSegment(nom As String) As String
        Dim retval As String = ""
        If Not String.IsNullOrEmpty(nom) Then
            retval = nom.ToLower
            retval = retval.Replace(" ", "_")
            retval = retval.Replace("&", "|")
            retval = retval.Replace("á", "a")
            retval = retval.Replace("é", "e")
            retval = retval.Replace("í", "i")
            retval = retval.Replace("ó", "o")
            retval = retval.Replace("ö", "o")
            retval = retval.Replace("ú", "u")
            retval = retval.Replace("ü", "u")
        End If
        Return retval
    End Function

    Shared Function DecodedUrlSegment(encodedNom As String) As String
        Dim retval As String = encodedNom
        retval = retval.Replace("_", " ")
        retval = retval.Replace("|", "&")
        Return retval
    End Function

    Shared Function IsCrawler(UserAgent As String) As Boolean
        Dim retval As Boolean = System.Text.RegularExpressions.Regex.IsMatch(UserAgent, "bot|crawler|baiduspider|80legs|ia_archiver|voyager|curl|wget|yahoo! slurp|mediapartners-google|facebookexternalhit", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Return retval
    End Function


    Shared Function IsOurOwnIp(Ip As String) As Boolean
        Dim retval As Boolean
        If Ip = "80.28.148.178" Then 'Diagonal
            retval = True
        ElseIf Ip = "83.58.80.255" Then 'Modolell
            retval = True
        ElseIf Ip = "::1" Then 'Developer from Visual Studio
            retval = True
        End If
        Return retval
    End Function

    Shared Function Title(sTitle As String) As String
        Dim retval As String = sTitle & " | M+O"
        Return retval
    End Function

End Class
