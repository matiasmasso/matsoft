Public Class Msg

    Shared Function Find(Id As Integer) As DTOMsg
        Dim retval As DTOMsg = MsgLoader.Find(Id)
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOMsg
        Dim retval As DTOMsg = MsgLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oMsg As DTOMsg, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MsgLoader.Update(oMsg, exs)
        Return retval
    End Function



    Shared Async Function MailInfo(oEmp As DTOEmp, oMsg As DTOMsg, exs As List(Of Exception)) As Task(Of Boolean)
        BEBL.User.Load(oMsg.User())
        Dim sSender As String = ""
        Dim oContact As DTOContact = oMsg.User.Contact
        If oContact Is Nothing Then
            sSender = DTOUser.NicknameOrElse(oMsg.User)
        Else
            BEBL.Contact.Load(oContact)
            sSender = oContact.FullNom
        End If

        Dim sSubject As String = String.Format("Missatge {0} via App de {1}", oMsg.Id, sSender)
        Dim retval = Await BEBL.MailMessageHelper.MailInfo(exs, oEmp, sSubject, oMsg.Text)
        Return retval
    End Function
End Class



Public Class Msgs
    Shared Function All() As List(Of DTOMsg)
        Dim retval As List(Of DTOMsg) = MsgsLoader.All()
        Return retval
    End Function
End Class