Public Class SabiasQue
    Shared Function Search(sFriendlyUrlSegment As String) As DTOSabiasQuePost
        Dim retval As DTOSabiasQuePost = SabiasQueLoader.Find(sFriendlyUrlSegment)
        Return retval
    End Function

End Class
