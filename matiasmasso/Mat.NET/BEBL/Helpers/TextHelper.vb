Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class TextHelper

    Shared Function RegexMatch(src, Pattern) As Boolean
        Dim retval As Boolean = DAL.TextHelper.RegexMatch(src, Pattern)
        Return retval
    End Function

    Shared Function RegexValue(src, pattern) As String
        Dim retval As String = DAL.TextHelper.RegexValue(src, pattern)
        Return retval
    End Function

    Public Shared Function RemoveAccents(input As String) As String
        ' the normalization to FormD splits accented letters in accents+letters
        Dim retval As String = DAL.TextHelper.RemoveAccents(input)
        Return retval
    End Function

    Shared Function RegexSuppress(src As String, sRegexPattern As String) As String
        Dim retval As String = DAL.TextHelper.RegexSuppress(src, sRegexPattern)
        Return retval
    End Function

    Shared Function RegexSelectBetween(sStart As String, sEnd As String) As String
        Dim retval As String = DAL.TextHelper.RegexSelectBetween(sStart, sEnd)
        Return retval
    End Function

    Shared Function InsertStringRepeatedly(ByVal input As String, ByVal separator As String, ByVal length As Int32) As String
        Dim retval As String = DAL.TextHelper.InsertStringRepeatedly(input, separator, length)
        Return retval
    End Function

    Shared Function Excerpt(sLongText As String, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        Dim retval As String = DAL.TextHelper.Excerpt(sLongText, MaxLen, BlAppendEllipsis)
        Return retval
    End Function

    Shared Function StringListFromMultiline(src As String) As List(Of String)
        Dim retval As List(Of String) = DAL.TextHelper.StringListFromMultiline(src)
        Return retval
    End Function

    Shared Function ReadFileToStringList(sFilename As String, exs As List(Of Exception)) As List(Of String)
        Dim retval As List(Of String) = DAL.TextHelper.ReadFileToStringList(sFilename, exs)
        Return retval
    End Function

    Shared Function ToHtml(src As String) As String
        Dim retval As String = DAL.TextHelper.ToHtml(src)
        Return retval
    End Function

    Shared Function LeaveJustNumbericDigits(src As String) As String
        Dim retval As String = Regex.Replace(src, "[^0-9]", String.Empty)
        Return retval
    End Function
    Shared Function CleanForUrl(src As String) As String
        Dim retval As String = DAL.TextHelper.CleanForUrl(src)
        Return retval
    End Function

    Shared Function RemoveDiacritics(src As String) As String
        Dim retval As String = DAL.TextHelper.RemoveDiacritics(src)
        Return retval
    End Function

    Shared Function ShortenText(src As String, iMaxLen As String) As String
        Dim retval As String = DAL.TextHelper.ShortenText(src, iMaxLen)
        Return retval
    End Function

    Shared Function SelectBetween(src As String, sStart As String, sEnd As String) As String
        Dim retval As String = DAL.TextHelper.SelectBetween(src, sStart, sEnd)
        Return retval
    End Function

    Shared Function CleanFromHtmlTags(src As String, Optional iMaxLen As Integer = 0) As String
        Dim retval As String = DAL.TextHelper.CleanFromHtmlTags(src, iMaxLen)
        Return retval
    End Function

    Shared Function CleanNonAscii(src As String) As String
        Dim pattern As String = "[^ -~]+" 'selecciona tots els caracters entre l'espai i la tilde (ASCII 32 - ASCII 126)
        Dim reg_exp As New Regex(pattern)
        Dim retval As String = reg_exp.Replace(src, " ")
        Return retval
    End Function

    Shared Sub NomSplit(sRaoSocial As String, ByRef sFirstNom As String, ByRef sCognom As String)
        DAL.TextHelper.NomSplit(sRaoSocial, sFirstNom, sCognom)
    End Sub

    Shared Function GuessFraNum(src As String) As String
        'retorna la primera paraula (despres de punt o espai) que inclou digits numerics
        Dim retval As String = DAL.TextHelper.GuessFraNum(src)
        Return retval
    End Function

    Shared Function EncodedDate(DtFch As Date) As String
        Dim retval As String = DAL.TextHelper.EncodedDate(DtFch)
        Return retval
    End Function

    Shared Function DecodedDate(src As String) As Date
        Dim retval As Date = DAL.TextHelper.DecodedDate(src)
        Return retval
    End Function

    Shared Function StringListToMultiline(src As List(Of String)) As String
        Dim retval As String = DAL.TextHelper.StringListToMultiline(src)
        Return retval
    End Function

    Shared Function RandomString(ByVal length As Integer) As String
        Dim retval As String = DAL.TextHelper.RandomString(length)
        Return retval
    End Function

    Shared Function ToSingleLine(src As String, Optional MaxLength As Integer = 0) As String
        Dim retval As String = src.Replace(vbCrLf, " / ")
        If MaxLength > 0 Then
            retval = Left(retval, MaxLength)
        End If
        Return retval
    End Function

    Shared Function GetSplitCharSeparatedStringFromArrayList(ByVal oArray As List(Of String), Optional ByVal SplitChar As String = ",") As String
        Dim retval As String = DAL.TextHelper.GetSplitCharSeparatedStringFromArrayList(oArray, SplitChar)
        Dim s As String
        Dim t As String = ""
        For Each s In oArray
            t = t & SplitChar & s
        Next
        If t.Length > 0 Then
            retval = t.Substring(1)
        End If
        Return retval
    End Function

    Shared Function GetArrayListFromSplitCharSeparatedString(ByVal Src As String, Optional ByVal SplitChar As String = ",") As List(Of String)
        Dim retval As New List(Of String)

        Dim srcs() As String = Src.ToString.Split(SplitChar)
        retval = New List(Of String)
        For Each s As String In srcs
            Dim sTrimmed As String = s.Trim
            If sTrimmed > "" Then
                retval.Add(sTrimmed)
            End If
        Next
        Return retval
    End Function

    Shared Function CleanAsciiString(ByVal source As String) As String
        If source Is Nothing Then Return ""
        source = source.Replace("ñ", "n")
        source = source.Replace("Ñ", "N")
        source = source.Replace("Á", "A")
        source = source.Replace("À", "A")
        source = source.Replace("É", "E")
        source = source.Replace("Í", "I")
        source = source.Replace("Ó", "O")
        source = source.Replace("Ú", "U")
        source = source.Replace("Ü", "U")
        source = source.Replace("á", "a")
        source = source.Replace("à", "a")
        source = source.Replace("é", "e")
        source = source.Replace("í", "i")
        source = source.Replace("ó", "o")
        source = source.Replace("ú", "u")
        source = source.Replace("ç", "c")
        source = source.Replace("Ç", "C")
        source = source.Replace("ä", "a")
        source = source.Replace("ë", "e")
        source = source.Replace("ï", "i")
        source = source.Replace("ö", "o")
        source = source.Replace("ü", "u")
        source = source.Replace("ü", "u")
        source = source.Replace("ª", "a")
        source = source.Replace("º", "o")
        source = source.Replace("à", "a")
        source = source.Replace("è", "e")
        source = source.Replace("ì", "i")
        source = source.Replace("ò", "o")
        source = source.Replace("ù", "u")
        source = source.Replace("`", "'")
        source = source.Replace("´", "'")
        source = source.Replace("&", "-")

        Dim oAscii As New System.Text.ASCIIEncoding()
        Dim EncodedBytes As Byte() = oAscii.GetBytes(source)
        Dim sTxt As String = oAscii.GetString(EncodedBytes)

        Return sTxt
    End Function

    Shared Function RegexEmail() As String
        'Dim retval As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9_\-]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" (no permet que comenci per numero)
        Dim retval As String = "^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9_\-]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Return retval
    End Function

    Shared Function IsValidUrl(ByVal src As String) As Boolean
        Dim pattern As String = RegexUrl()
        Dim oMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(src, pattern)
        Dim retval As Boolean = oMatch.Success
        Return retval
    End Function

    Shared Function RegexUrl() As String

        'validates as follows:
        '=========================================================================
        'TODO: Evita validar emails en aquesta expresió
        '=========================================================================
        'www.google.com
        'http://www.google.com
        'mailto: somebody@Google.com 
        'somebody@Google.com
        'www.url-with-querystring.com/?url=has-querystring
        Dim retval As String = "/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/"
        Return retval
    End Function
End Class
