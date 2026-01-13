Imports System.Globalization
Imports System.Text.RegularExpressions
Public Class TextHelper

    Shared Function Html(textWithNewlines As String) As String
        Dim retval = ""
        If Not String.IsNullOrEmpty(textWithNewlines) Then
            Dim lines = textWithNewlines.Split(vbCrLf)
            If lines.Count = 1 Then lines = textWithNewlines.Split(vbLf)
            'Dim lines = textWithNewlines.Split(Environment.NewLine)
            For i As Integer = 1 To lines.Length - 1
                    Dim line = lines(i).Replace(vbLf, "")
                Dim skip As Boolean = line.StartsWith("<td") Or line.StartsWith("<tr") Or line.StartsWith("</td") Or line.StartsWith("</tr") Or line.StartsWith("</table") Or line.StartsWith("<more")
                If Not skip Then
                        lines(i - 1) = lines(i - 1) & "<br/>"
                    End If
                Next
                retval = String.Join(Environment.NewLine, lines)
            End If
            Return retval
    End Function

    Shared Function VbFormat(src As Integer, pattern As String) As String
        Dim retval = String.Format("{0:" & pattern & "}", src)
        Return retval
    End Function

    Shared Function VbFormat(src As Decimal, pattern As String) As String
        Dim retval = String.Format("{0:" & pattern & "}", src)
        Return retval
    End Function

    Shared Function VbFormat(dtFch As DateTime, pattern As String) As String
        Dim retval = dtFch.ToString(pattern)
        Return retval
    End Function

    Shared Function VbChoose(idx As Integer, ParamArray values As String()) As String
        Dim retval As String = ""
        If idx < values.Length Then
            retval = values(idx - 1)
        End If
        Return retval
    End Function

    ''' <summary>
    ''' returns de AsCIICode of the first character
    ''' </summary>
    ''' <param name="UTF8String">input string</param>
    ''' <returns></returns>
    Shared Function VbAsc(UTF8String As String) As Integer
        Dim oByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(UTF8String)
        Dim retval As Integer = oByteArray(0)
        Return retval
    End Function

    Shared Function VbChr(src As Integer) As String
        Return Convert.ToChar(src)
    End Function

    Shared Function VbLeft(src As String, len As Integer) As String
        Dim retval As String = src.Trim()
        If len < src.Length Then
            retval = retval.Substring(0, len)
        End If
        Return retval
    End Function

    Shared Function VbRight(src As String, len As Integer) As String
        Dim retval As String = src.Trim
        If len < src.Length Then
            retval = retval.Substring(retval.Length - len, len)
        End If
        Return retval
    End Function

    Shared Function VbMid(src As String, pos As Integer, Optional len As Integer = 0) As String
        Dim retval As String = src.Trim
        If pos < retval.Length Then
            retval = retval.Substring(pos - 1, retval.Length - (pos - 1))
        End If
        If len < retval.Length Then
            retval = retval.Substring(0, len)
        End If
        Return retval
    End Function


    Shared Function VbIsNumeric(src As String) As Boolean
        Dim result As Integer
        Dim retval = Decimal.TryParse(src, result)
        Return retval
    End Function

    Shared Function Match(fullSentence As String, searchString As String) As Boolean
        Dim compareInfo = CultureInfo.InvariantCulture.CompareInfo
        Dim options = CompareOptions.IgnoreCase Or CompareOptions.IgnoreSymbols Or CompareOptions.IgnoreNonSpace

        Dim Index = compareInfo.IndexOf(fullSentence, searchString, options)
        Dim retval = Index >= 0
        Return retval
    End Function

    Shared Function MatchingSegments(oSegments As List(Of String), sPattern As String) As List(Of String)
        Dim retval As List(Of String) = oSegments.Where(Function(x) Regex.Matches(x, sPattern).Count > 0).ToList()
        Return retval
    End Function

    Shared Function splitByLength(src As String, itemLength As Integer) As List(Of String)
        Dim retval = Enumerable.Range(0, src.Length / itemLength).Select(Function(x) src.Substring(x * itemLength, itemLength)).ToList
        Return retval
    End Function

    Shared Function Excerpt(sLongText As String, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        If sLongText > "" Then
            If sLongText.IndexOf("<more/>") >= 0 Then
                sLongText = sLongText.Substring(0, sLongText.IndexOf("<more/>"))
            Else
                If sLongText > "" Then
                    If MaxLen > 0 Then
                        Dim ellipsis As String = If(BlAppendEllipsis, "...", "")
                        If sLongText.Length > MaxLen - ellipsis.Length Then
                            Dim iLastBlank As Integer = sLongText.Substring(0, MaxLen).LastIndexOf(" ")
                            If iLastBlank > 0 Then
                                sLongText = sLongText.Substring(0, iLastBlank) & ellipsis
                            Else
                                sLongText = sLongText.Substring(0, MaxLen - ellipsis.Length) & ellipsis
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return sLongText
    End Function

    Shared Function RegexMatch(src, Pattern) As Boolean
        Dim oMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(src, Pattern)
        Dim retval As Boolean = oMatch.Success
        Return retval
    End Function

    Shared Function RegexValue(src, pattern) As String
        Dim r As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim m1 As MatchCollection = Regex.Matches(src, pattern)
        Dim oMatch As System.Text.RegularExpressions.Match = r.Match(src)
        Dim retval As String = oMatch.Value
        Return retval
    End Function

    Shared Function RegexSuppress(src As String, sRegexPattern As String) As String
        Dim retval As String = ""
        If src > "" Then
            Dim rgx As New Regex(sRegexPattern)
            Dim sReplacement As String = ""
            retval = rgx.Replace(src, sReplacement)
        End If
        Return retval
    End Function

    Shared Function RegexSelectBetween(sStart As String, sEnd As String) As String
        Dim retval As String = "(?<=" & sStart & ")(.*)(?=" & sEnd & ")"
        Return retval
    End Function

    Shared Function LeaveJustNumbericDigits(src As String) As String
        Dim retval As String = Regex.Replace(src, "[^0-9]", String.Empty)
        Return retval
    End Function
    Shared Function LeaveJustNumericDigits(src As String) As String
        Dim retval As String = Regex.Replace(src, "[^0-9]", String.Empty)
        Return retval
    End Function

    Shared Function FbImg(src As String) As String
        Dim retval As String = ""
        Dim elements = HtmlElements(src)
        Dim sFbImgElement As String = elements.FirstOrDefault(Function(x) x.StartsWith("<img ") And x.Contains(" fbimg "))
        If String.IsNullOrEmpty(sFbImgElement) Then
            sFbImgElement = elements.FirstOrDefault(Function(x) x.StartsWith("<img "))
        End If
        If Not String.IsNullOrEmpty(sFbImgElement) Then
            retval = srcFromImgTag(sFbImgElement)
        End If
        Return retval
    End Function

    Shared Function srcFromImgTag(imgElement As String) As String
        Dim retval As String = ""
        Dim srcPos = imgElement.IndexOf("src")
        If srcPos >= 0 Then
            Dim delimiterVal As String
            For i = 0 To imgElement.Length - 1
                Select Case imgElement.Substring(i, 1)
                    Case TextHelper.VbChr(34), TextHelper.VbChr(39), TextHelper.VbChr(44)
                        delimiterVal = imgElement.Substring(i, 1)
                        Dim tmp = imgElement.Substring(i + 1)
                        Dim delimiterPos = tmp.IndexOf(delimiterVal)
                        retval = tmp.Substring(0, delimiterPos)
                        Exit For
                End Select
            Next
        End If

        Return retval
    End Function

    Shared Function HtmlElements(src As String) As List(Of String)
        Dim pattern = "</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[\^'"">\s]+))?)+\s*|\s*)/?>"
        '("<" & tag & "[^>]*id[\s]?=[\s]?['""]" + id & "['""][\s\S]*?</" + tag & ">")
        Dim matches As MatchCollection = Regex.Matches(src, pattern)
        Dim retval As New List(Of String)
        For Each m As Match In matches
            For Each c As Capture In m.Captures
                retval.Add(c.Value.ToLower)
            Next
        Next
        Return retval
    End Function

    Shared Function HtmlTagById(html As String, tag As String, id As String) As List(Of String)
        Dim pattern = "<" & tag & "[^>]*id[\s]?=[\s]?['""]" + id & "['""][\s\S]*?</" + tag & ">"
        Dim matches As MatchCollection = Regex.Matches(html, pattern)
        Dim retval As New List(Of String)
        For Each m As Match In matches
            For Each c As Capture In m.Captures
                retval.Add(c.Value.ToLower)
            Next
        Next
        Return retval
    End Function

    Shared Function InsertStringRepeatedly(ByVal input As String, ByVal separator As String, ByVal length As Int32) As String
        Dim sb As New System.Text.StringBuilder()
        For chr As Integer = 0 To input.Length - 1
            If chr Mod length = 0 And sb.Length > 0 Then
                sb.Append(separator)
            End If
            sb.Append(input(chr))
        Next
        Return sb.ToString()
    End Function


    Shared Function StringListToMultiline(src As List(Of String)) As String
        Dim sb As New System.Text.StringBuilder
        If src IsNot Nothing Then
            For Each line As String In src
                If line.Trim > "" Then
                    sb.AppendLine(line.Trim)
                End If
            Next
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function StringListFromMultiline(src As String) As List(Of String)
        Dim retval As New List(Of String)
        For Each s As String In src.Split(vbCrLf)
            If s.Trim > "" Then retval.Add(s.Trim)
        Next
        Return retval
    End Function

    Shared Function ReadFileToStringList(sFilename As String, exs As List(Of Exception)) As List(Of String)
        Dim fStream As System.IO.FileStream = Nothing
        Dim sReader As System.IO.StreamReader = Nothing
        Dim retval As New List(Of String)

        Try
            fStream = New System.IO.FileStream(sFilename, IO.FileMode.Open)
            sReader = New System.IO.StreamReader(fStream)
            Do While sReader.Peek >= 0
                retval.Add(sReader.ReadLine)
            Loop

        Catch ex As Exception
            exs.Add(ex)
        Finally
            If fStream IsNot Nothing Then fStream.Close()
            If sReader IsNot Nothing Then sReader.Close()

        End Try
        Return retval
    End Function



    Shared Function CleanForUrl(src As String) As String
        Dim retval As String = ""
        If Not String.IsNullOrEmpty(src) Then
            retval = src.ToLower
            retval = retval.Replace(" ", "_")
            retval = retval.Replace("ö", "o")
        End If
        Return retval
    End Function


    Public Shared Function RemoveAccents(src As String) As String
        ' the normalization to FormD splits accented letters in accents+letters
        Dim sSplitted As String = src.Normalize(System.Text.NormalizationForm.FormD)

        ' removes those accents (and other non-spacing characters)
        Dim retval As New String(sSplitted.ToCharArray().Where(Function(c) CharUnicodeInfo.GetUnicodeCategory(c) <> UnicodeCategory.NonSpacingMark).ToArray())
        Return retval
    End Function

    Shared Function RemoveDiacritics(src As String) As String
        ' the normalization to FormD splits accented letters in accents+letters
        Dim normalizedString As String = src.Normalize(System.Text.NormalizationForm.FormD)
        Dim sb As New System.Text.StringBuilder

        For Each c As Char In normalizedString
            Dim unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
            If unicodeCategory <> Globalization.UnicodeCategory.NonSpacingMark Then
                sb.Append(c)
            End If
        Next

        Dim retval As String = sb.ToString().Normalize(System.Text.NormalizationForm.FormC)
        Return retval
    End Function

    Shared Function ShortenText(src As String, iMaxLen As String) As String
        Dim retval As String = src
        If src.Length > iMaxLen Then
            retval = src.Substring(0, iMaxLen - 3)
            Dim ilastBlank As Integer = retval.LastIndexOf(" ")
            If ilastBlank > 0 Then
                retval = src.Substring(0, ilastBlank)
            End If
            retval = retval & "..."
        End If
        Return retval
    End Function



    Shared Function SelectBetween(src As String, sStart As String, sEnd As String) As String
        Dim sRegex As String = "\" & sStart & "(.*?)" & sEnd
        Dim oRegex As Regex = New Regex(sRegex)
        Dim oMatch As Match = oRegex.Match(src)
        Dim retval As String = oMatch.Groups(1).Value.ToString

        Return retval
    End Function

    Shared Function CleanFromHtmlTags(src As String, Optional iMaxLen As Integer = 0) As String
        Dim retval As String = src
        If src > "" Then
            Dim sRegex As String = RegexSelectBetween("<img", "/>")
            retval = RegexSuppress(src, sRegex)
            sRegex = RegexSelectBetween("<a ", ">")
            retval = RegexSuppress(retval, sRegex)

            sRegex = RegexSelectBetween("<div ", ">")
            retval = RegexSuppress(retval, sRegex)

            retval = retval.Replace("<div >", "")
            retval = retval.Replace("<p>", "")
            retval = retval.Replace("</p>", "")
            retval = retval.Replace("<img/>", "")
            retval = retval.Replace("<b>", "")
            retval = retval.Replace("</b>", "")
            retval = retval.Replace("<strong>", "")
            retval = retval.Replace("</strong>", "")

            retval = retval.Trim

            If iMaxLen > 0 Then
                retval = ShortenText(retval, iMaxLen)
            End If
        End If
        Return retval
    End Function

    Shared Sub NomSplit(sRaoSocial As String, ByRef sFirstNom As String, ByRef sCognom As String)
        Dim sLastChar As String = TextHelper.VbRight(sCognom, 1)
        Dim iComa As Integer = sRaoSocial.IndexOf(",")
        If sLastChar.ToLower = sLastChar And iComa > 0 Then
            sCognom = sRaoSocial.Substring(0, iComa)
            sFirstNom = sRaoSocial.Substring(iComa + 1).Trim
        Else
            sCognom = sRaoSocial
        End If
    End Sub

    Shared Function GuessFraNum(src As String) As String
        'retorna la primera paraula (despres de punt o espai) que inclou digits numerics
        Dim retval As String = ""
        Dim iFirstChar As Integer
        Dim iLastChar As Integer = src.Length - 1
        For i As Integer = 0 To iLastChar
            Dim sChar As String = src.Substring(i, 1)
            Select Case sChar
                Case ".", " "
                    iFirstChar = i + 1
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                    If src.IndexOf(" ", iFirstChar) > 0 Then iLastChar = src.IndexOf(" ", iFirstChar) - 1
                    retval = src.Substring(iFirstChar, iLastChar - iFirstChar + 1)
                    Exit For
            End Select
        Next
        Return retval
    End Function

    Shared Function EncodedDate(DtFch As Date) As String
        Dim sWeekDay As String = DtFch.DayOfWeek
        Dim sYear As String = DtFch.Year - 2000
        Dim sDayOfYear As String = DtFch.DayOfYear
        Dim retval As String = String.Format("{0}{1}{2}", sWeekDay, sYear, sDayOfYear)
        Return retval
    End Function

    Shared Function DecodedDate(src As String) As Date
        Dim retval As Date = Nothing
        If TextHelper.VbIsNumeric(src) Then
            If src.Length > 3 Then
                Dim iWeekDay As Integer = src.Substring(0, 1)
                Dim iYear As Integer = 2000 + CInt(src.Substring(1, 2))
                Dim iDayOfYear As Integer = src.Substring(3)
                Dim DtFch As Date = (New Date(iYear, 1, 1).AddDays(iDayOfYear - 1))

                If DtFch.DayOfWeek = iWeekDay Then retval = DtFch
            End If
        End If
        Return retval
    End Function

    Shared Function RandomString(ByVal length As Integer) As String
        Dim random As New Random()
        Dim charOutput As Char() = New Char(length - 1) {}
        For i As Integer = 0 To length - 1
            Dim selector As Integer = random.[Next](65, 101)
            If selector > 90 Then
                selector -= 43
            End If
            charOutput(i) = Convert.ToChar(selector)
        Next
        Dim retval As New String(charOutput)
        Return retval
    End Function


    Shared Function ToSingleLine(src As String, Optional MaxLength As Integer = 0) As String
        Dim retval As String = src.Replace(vbCrLf, " / ")
        If MaxLength > 0 Then
            retval = TextHelper.VbLeft(retval, MaxLength)
        End If
        Return retval
    End Function

    Shared Function CleanNonAscii(src As String) As String
        Dim pattern As String = "[^ -~]+" 'selecciona tots els caracters entre l'espai i la tilde (ASCII 32 - ASCII 126)
        Dim reg_exp As New Regex(pattern)
        Dim retval As String = reg_exp.Replace(src, " ")
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
        'https://www.google.com
        'mailto: somebody@Google.com 
        'somebody@Google.com
        'www.url-with-querystring.com/?url=has-querystring
        Dim retval As String = "/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/"
        Return retval
    End Function


    Shared Function GetSplitCharSeparatedStringFromArrayList(ByVal oArray As List(Of String), Optional ByVal SplitChar As String = ",") As String
        Dim t As String = ""
        For Each s In oArray
            t = t & SplitChar & s
        Next
        Dim retval As String = ""
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


End Class