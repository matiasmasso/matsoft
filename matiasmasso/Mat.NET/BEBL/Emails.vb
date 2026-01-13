Public Class Emails
    Shared Function All(oContact As DTOContact, Optional includeObsoletos As Boolean = False) As List(Of DTOEmail)
        Dim retval As List(Of DTOEmail) = EmailsLoader.All(oContact, includeObsoletos)
        Return retval
    End Function


End Class
