Public Class EdiversaOrdrSp
    Shared Function Find(oGuid As Guid) As DTOEdiversaOrdrsp
        Dim retval As DTOEdiversaOrdrsp = EdiversaOrdrSpLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaOrdrSp As DTOEdiversaOrdrsp) As Boolean
        Dim retval As Boolean = EdiversaOrdrSpLoader.Load(oEdiversaOrdrSp)
        Return retval
    End Function

    Shared Function Update(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaOrdrSpLoader.Update(oEdiversaOrdrSp, exs)
        Return retval
    End Function

    Shared Function Delete(oEdiversaOrdrSp As DTOEdiversaOrdrsp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaOrdrSpLoader.Delete(oEdiversaOrdrSp, exs)
        Return retval
    End Function

End Class

Public Class EdiversaOrdrSps
    Shared Function Headers() As List(Of DTOEdiversaOrdrsp)
        Dim retval As List(Of DTOEdiversaOrdrsp) = EdiversaOrdrSpsLoader.Headers()
        Return retval
    End Function

End Class
