Public Class EdiversaGenral


    Shared Async Function Send(exs As List(Of Exception), oEmp As DTOEmp, oEdiversaFile As DTOEdiversaFile) As Task(Of Boolean)
        Dim retval As Boolean

        If Await MailMessageHelper.MailInfo(exs, oEmp, Subject(oEdiversaFile), Body(oEdiversaFile), DTOMailMessage.MessageBodyFormats.ASCII) Then
            If EdiversaFileLoader.SetResult(oEdiversaFile, DTOEdiversaFile.Results.processed, Nothing, exs) Then
                retval = True
            Else
                exs.Add(New Exception("error al marcar el missatge com a retransmes '" & oEdiversaFile.Guid.ToString & "'"))
            End If
        Else
            exs.Add(New Exception("error al retransmetre el missatge '" & oEdiversaFile.Guid.ToString & "'"))
        End If
        Return retval
    End Function

    Shared Function Subject(oEdiversa As DTOEdiversaFile) As String
        Dim retval As String = "Missatge EDI "
        Dim oGln As DTOEan = oEdiversa.Sender.Ean
        Dim oInterlocutor As DTOEdiversaFile.Interlocutors = DTOEdiversaFile.ReadInterlocutor(oEdiversa.Sender.Ean)
        If oInterlocutor = DTOEdiversaFile.Interlocutors.unknown Then
            retval += "de remitent desconegut"
            If oGln IsNot Nothing Then
                retval += " amb GLN " & oGln.Value
            End If
        Else
            retval += "de " & oInterlocutor.ToString()
        End If
        Return retval
    End Function

    Shared Function Body(oEdiversa As DTOEdiversaFile) As String
        Dim sMsg As String = oEdiversa.Stream
        Dim Lines() As String = sMsg.Split(vbCrLf)
        Dim sb As New System.Text.StringBuilder
        For Each Line As String In Lines

            If Line.Contains("FTX|AAI") Then
                Dim CleanLine As String = Line.Replace("FTX|AAI|", "").Replace("FTX|AAI", "").Replace(vbLf, "")
                'sb.Append(CleanLine & "<br/>")
                sb.AppendLine(CleanLine)
            End If
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function



End Class

Public Class EdiversaGenrals
    Shared Function Search(sSearchKey As String) As List(Of DTOEdiversaGenral)
        Dim retval As List(Of DTOEdiversaGenral) = EdiversaGenralsLoader.All(sSearchKey)
        Return retval
    End Function

End Class

