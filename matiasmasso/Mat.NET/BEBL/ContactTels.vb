Public Class ContactTels
    Shared Function All(oContact As DTOContact, Optional oCod As DTOContactTel.Cods = DTOContactTel.Cods.NotSet) As List(Of DTOContactTel)
        Dim retval As List(Of DTOContactTel) = ContactTelsLoader.All(oContact, oCod)
        Return retval
    End Function
End Class
