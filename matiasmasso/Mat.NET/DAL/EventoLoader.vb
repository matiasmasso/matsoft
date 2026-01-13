
Public Class EventosLoader

    Shared Function Headers(oUser As DTOUser, Optional OnlyVisible As Boolean = False) As List(Of DTOEvento)
        Dim retval As New List(Of DTOEvento)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment, Noticia.FchFrom, Noticia.FchTo ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("WHERE Noticia.Cod=" & CInt(DTONoticia.Srcs.Eventos) & " ")
        If oUser Is Nothing Then
            sb.AppendLine("AND Professional=0 ")
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("FROM NoticiaChannel ")
                    sb.AppendLine("INNER JOIN ProductChannel ON NoticiaChannel.Channel = ProductChannel.DistributionChannel ")
                    sb.AppendLine("INNER JOIN Tpa ON ProductChannel.Product = Tpa.Guid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid ")
                    sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")
                Case DTORol.Ids.rep
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("     SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("     FROM NoticiaChannel ")
                    sb.AppendLine("     INNER JOIN RepProducts ON NoticiaChannel.Channel = RepProducts.DistributionChannel ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "'")
                    sb.AppendLine("     AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo >= GETDATE())")
                    sb.AppendLine("     GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("     SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("     FROM NoticiaChannel ")
                    sb.AppendLine("     INNER JOIN ContactClass ON NoticiaChannel.Channel = ContactClass.DistributionChannel ")
                    sb.AppendLine("     INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON CliGral.Guid = Email_Clis.ContactGuid ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("     GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")

                Case DTORol.Ids.guest, DTORol.Ids.denied
                    sb.AppendLine("AND Professional=0 ")
            End Select
        End If
        If OnlyVisible Then
            sb.AppendLine("AND Visible=1 ")
        End If
        sb.AppendLine("ORDER BY Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEvento(DirectCast(oDrd("Guid"), Guid))
            With item
                .fch = oDrd("Fch")
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
