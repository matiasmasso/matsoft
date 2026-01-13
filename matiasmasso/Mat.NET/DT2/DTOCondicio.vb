Public Class DTOCondicio

    Inherits DTOBaseGuid

    Property Title As DTOLangText
    Property Excerpt As DTOLangText
    Property Capitols As List(Of DTOCondicioCapitol)
    Property FchLastEdited As Date

    Public Sub New()
        MyBase.New()
        _Capitols = New List(Of DTOCondicioCapitol)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Capitols = New List(Of DTOCondicioCapitol)
    End Sub

    Public Shadows Function UrlSegment() As String
        Return MyBase.UrlSegment("condicions")
    End Function

    Shared Function TextHtml(oCondicio As DTOCondicio, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCondicio IsNot Nothing Then
            retval = DTOLangText.Replace(oCondicio.Excerpt, vbCrLf, "<br/>").Tradueix(oLang)
        End If
        Return retval
    End Function


End Class

    Public Class DTOCondicioCapitol
    Inherits DTOBaseGuid

    Property Parent As DTOCondicio
    Property Ord As Integer
    Property Caption As DTOLangText
    Property Text As DTOLangText
    Property UsrLastEdited As DTOUser
    Property FchLastEdited As DateTime

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function TextHtml(oCapitol As DTOCondicioCapitol, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCapitol IsNot Nothing Then
            retval = DTOLangText.Replace(oCapitol.Text, vbCrLf, "<br/>").Tradueix(oLang)
        End If
        Return retval
    End Function

    Shared Function CapitolId(src As DTOCondicioCapitol) As DTOLangText
        Dim retval = DTOLangText.RemoveAccents(src.Caption)
        If retval.Esp > "" Then retval.Esp = retval.Esp.Replace(" ", "")
        If retval.Cat > "" Then retval.Cat = retval.Cat.Replace(" ", "")
        If retval.Eng > "" Then retval.Eng = retval.Eng.Replace(" ", "")
        If retval.Por > "" Then retval.Por = retval.Por.Replace(" ", "")
        Return retval
    End Function

    Shared Function LastUsrText(oCondicioCapitol As DTOCondicioCapitol) As String
        Dim retval As String = ""
        If oCondicioCapitol IsNot Nothing Then
            With oCondicioCapitol
                If .IsNew Then
                    retval = String.Format("Nou Capitol Creat per {0}", DTOUser.NicknameOrElse(.UsrLastEdited))
                Else
                    If .UsrLastEdited IsNot Nothing Then
                        retval = String.Format("Modificat per {0} el {1:dd/MM/yy}", DTOUser.NicknameOrElse(.UsrLastEdited), .FchLastEdited)
                    End If
                End If

            End With

        End If
        Return retval
    End Function

End Class
