Public Class RequestHelper
    Shared Function Host(request As HttpRequest) As String
        'farma.matiasmasso.es
        Dim retval As String = request.Url.Host.ToLower()
        Return retval
    End Function

    Shared Function TopLevelDomain(request As HttpRequest) As String
        Dim sHost As String = Host(request)
        Dim segments As String() = sHost.Split(".")
        Dim retval As String = segments.Last
        Return retval
    End Function

    Shared Function Domain(request As HttpRequest) As String
        Dim retval As String = ""
        Dim sHost As String = Host(request)
        Dim segments As String() = sHost.Split(".")
        If segments.Count > 1 Then
            retval = segments(segments.Count - 2)
        End If
        Return retval
    End Function

    Shared Function SubDomain(request As HttpRequest) As String
        Dim retval As String = ""
        Dim sHost As String = Host(request)
        Dim segments As String() = sHost.Split(".")
        If segments.Count > 2 Then
            retval = segments.First
        End If
        Return retval
    End Function

    Shared Function Lang(request As HttpRequest) As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        Try
            retval = LangFromTopLevelDomain(request)
            If Not retval.Equals(DTOLang.POR) Then
                retval = LangFromUserCulture(request)
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Shared Function LangFromTopLevelDomain(request As HttpRequest) As DTOLang
        Dim retval As DTOLang = Nothing
        Dim tld As String = TopLevelDomain(request)
        Select Case tld
            Case "pt"
                retval = DTOLang.POR
            Case Else
                retval = DTOLang.ESP
        End Select
        Return retval
    End Function

    Shared Function LangFromUserCulture(request As HttpRequest) As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        Dim userLangs As String() = request.UserLanguages
        If userLangs IsNot Nothing Then
            If userLangs.Count > 0 Then
                Dim sFirstLang As String = userLangs.First
                If sFirstLang.Length > 2 Then sFirstLang = sFirstLang.Substring(0, 2)
                Select Case sFirstLang
                    Case "ca"
                        retval = DTOLang.CAT
                    Case "pt"
                        retval = DTOLang.POR
                    Case "es"
                        retval = DTOLang.ESP
                    Case Else
                        retval = DTOLang.ENG
                End Select
            End If
        End If
        Return retval
    End Function
End Class
