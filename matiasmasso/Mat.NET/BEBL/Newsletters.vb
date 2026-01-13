Public Class Newsletter

    Shared Function Find(oGuid As Guid) As DTONewsletter
        Dim retval As DTONewsletter = NewsletterLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oNewsletter As DTONewsletter, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NewsletterLoader.Update(oNewsletter, exs)
        Return retval
    End Function

    Shared Function Delete(oNewsletter As DTONewsletter, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NewsletterLoader.Delete(oNewsletter, exs)
        Return retval
    End Function

End Class



Public Class Newsletters
    Shared Function Headers() As List(Of DTONewsletter)
        Dim retval As List(Of DTONewsletter) = NewslettersLoader.Headers()
        Return retval
    End Function
End Class

