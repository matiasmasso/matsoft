Public Class SabiasQueLoader


    Shared Function Find(sUrlSegment) As DTOSabiasQuePost
        Dim retval As DTOSabiasQuePost = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine(", LangText.Esp AS TextEsp, LangText.Cat AS TextCat, LangText.Eng AS TextEng, LangText.Por AS TextPor  ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangText ON Noticia.Guid = LangText.Guid AND LangText.Src = " & DTOLangText.Srcs.ContentText & " ")
        sb.AppendLine("WHERE UrlFriendlySegment = '" & sUrlSegment & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSabiasQuePost
            With retval
                .Guid = oDrd("Guid")
                .FriendlyUrl = sUrlSegment
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                SQLHelper.LoadLangTextFromDataReader(.text, oDrd, "TextEsp", "TextCat", "TextEng", "TextPor")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function
End Class
