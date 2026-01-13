Public Class Defaults

    Public Shared serverName = "10.74.52.10" '"WIN-21VN07SBVSF"
    'Public Shared serverName = "sql.matiasmasso.es" '"WIN-21VN07SBVSF"
    'Public Const URLAPI As String = "https://matiasmasso-api.azurewebsites.net/"
    Public Const URLAPI As String = "https://api.matiasmasso.es/"

    Shared Function ApiUrl(ByVal ParamArray UrlSegments() As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(URLAPI)

        For i As Integer = 0 To UBound(UrlSegments)
            Dim sSegment As String = UrlSegments(i).Trim
            If Not sb.ToString.EndsWith("/") Then sb.Append("/")
            If sSegment.StartsWith("/") Then sSegment = sSegment.Substring(1)
            sb.Append(sSegment)
        Next i

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function UrlFromSegments(AbsoluteUrl As Boolean, ByVal ParamArray UrlSegments() As String) As String
        Dim sb As New System.Text.StringBuilder

        Dim oDomain = DTOWebDomain.Default(AbsoluteUrl)
        sb.Append(oDomain.RootUrl())

        For intLoopIndex As Integer = 0 To UBound(UrlSegments)
            Dim sSegment As String = UrlSegments(intLoopIndex).Trim
            'sSegment = System.Net.WebUtility.UrlEncode(sSegment)
            If Not sb.ToString.EndsWith("/") Then sb.Append("/")
            If sSegment.StartsWith("/") Then sSegment = sSegment.Substring(1)
            sb.Append(sSegment)

        Next intLoopIndex

        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function GetImageUrl(ByVal oType As DTO.Defaults.ImgTypes, ByVal oGuid As Guid, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlFromSegments(AbsoluteUrl, "img", CInt(oType).ToString, oGuid.ToString())
        Return retval
    End Function

    Shared Function GetImageUrl(ByVal oType As DTO.Defaults.ImgTypes, sHash As String, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim sRetval As String = UrlFromSegments(AbsoluteUrl, "img", CInt(oType).ToString, sHash) ' BaseGuid.GetBase64FromGuid(oGuid))
        'Dim sRetval As String = UrlFromSegments(AbsoluteUrl, False, "img", CInt(oType).ToString, sHash) ' BaseGuid.GetBase64FromGuid(oGuid))
        Return sRetval
    End Function


    Shared Function CheckDB() As Boolean
        Dim retval As Boolean
        Dim SQL As String = "SELECT TOP 1 Emp From Emp ORDER BY Emp"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        oDrd.Close()
        retval = True
        Return retval
    End Function

    Shared Function SQLConnectionString() As String
        Dim database As String = "maxi"
        Dim username As String = "sa_cXJSQYte"
        Dim password As String = "CC1SURJQXHyfem27Bc"
        Dim retval = String.Format("Server={0};Database={1};User id={2};Password={3};", servername, database, username, password)
        Return retval
    End Function

    Shared Function NullOrValue(src As String) As Object
        Dim retval As Object = IIf(src = "", System.DBNull.Value, src)
        Return retval
    End Function

    Shared Function NullOrValue(src As Guid) As Object
        Dim retval As Object = IIf(src = Nothing, System.DBNull.Value, src)
        Return retval
    End Function

    Shared Function NullOrValue(src As Date) As Object
        Dim retval As Object = IIf(src = Nothing, System.DBNull.Value, src)
        Return retval
    End Function

    Shared Function NullOrValue(src As DTOBaseGuid) As Object
        Dim retval As Object = Nothing
        If src Is Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src.Guid
        End If
        Return retval
    End Function

    Shared Function NullOrValue(src As Object) As Object
        Dim retval As Object = Nothing
        If src Is Nothing Then
            retval = System.DBNull.Value
        End If
        Return retval
    End Function

    Shared Function StringOrEmpty(src As Object) As String
        Dim retval As String = ""
        If Not IsDBNull(src) Then
            retval = src.ToString
        End If
        Return retval
    End Function

    Shared Function FchOrNothing(src As Object) As Date
        Dim retval As Date = Nothing
        If Not IsDBNull(src) Then
            retval = CDate(src)
        End If
        Return retval
    End Function

    Shared Function IntOrZeroIfNull(src As Object) As Integer
        Dim retval As Integer
        If Not IsDBNull(src) Then
            retval = CInt(src)
        End If
        Return retval
    End Function

    Shared Function DecimalOrZero(src As Object) As Decimal
        Dim retval As Decimal = 0
        If Not IsDBNull(src) Then
            retval = CDec(src)
        End If
        Return retval
    End Function

    Shared Function AmtOrNothing(src As Object) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If Not IsDBNull(src) Then
            retval = DTOAmt.Factory(CDec(src))
        End If
        Return retval
    End Function

End Class
