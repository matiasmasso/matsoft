Imports System.Globalization
Imports System.Runtime.CompilerServices


Module HttpRequestExtensions

    <Extension()>
    Public Function test(ByVal request As HttpRequestBase) As String
        Return "Hello World!"
    End Function

    <Extension()>
    Public Function resource(ByVal request As HttpRequestBase, ByVal stringKey As String) As String
        Dim retval As String = ""

        If Not String.IsNullOrEmpty(stringKey) Then
            Dim cultureInfo As CultureInfo = request.cultureInfo()
            retval = DTO.GlobalStrings.ResourceManager.GetString(stringKey, cultureInfo)
        End If

        Return (If(retval, ""))
    End Function

    <Extension()>
    Public Function Lang(ByVal value As HttpRequestBase) As DTO.DTOLang
        Dim ISO639 As String = value.ISO639()
        Dim retval As DTO.DTOLang = DTO.DTOLang.FromISO639OrDefault(ISO639)
        Return retval
    End Function

    <Extension()>
    Public Function ISO639(ByVal request As HttpRequestBase) As String
        Dim retval As String = request.cultureInfo().TwoLetterISOLanguageName
        Return retval
    End Function

    <Extension()>
    Public Function cultureInfo(ByVal request As HttpRequestBase) As CultureInfo
        Dim userLanguages = request.UserLanguages
        Dim retval As CultureInfo

        If userLanguages.Count() > 0 Then

            Try
                retval = New CultureInfo(userLanguages(0))
            Catch __unusedCultureNotFoundException1__ As CultureNotFoundException
                retval = CultureInfo.InvariantCulture
            End Try
        Else
            retval = CultureInfo.InvariantCulture
        End If

        Return retval
    End Function
End Module

