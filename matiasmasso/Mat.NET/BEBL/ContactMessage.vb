Public Class ContactMessage

    Shared Function Find(oGuid As Guid) As DTOContactMessage
        Return ContactMessageLoader.Find(oGuid)
    End Function

    Shared Function Update(oContactMessage As DTOContactMessage, exs As List(Of Exception)) As Boolean
        Return ContactMessageLoader.Update(oContactMessage, exs)
    End Function

    Shared Function Delete(oContactMessage As DTOContactMessage, exs As List(Of Exception)) As Boolean
        Return ContactMessageLoader.Delete(oContactMessage, exs)
    End Function

End Class



Public Class ContactMessages
    Shared Function All() As List(Of DTOContactMessage)
        Dim retval As List(Of DTOContactMessage) = ContactMessagesLoader.All()
        Return retval
    End Function
End Class
