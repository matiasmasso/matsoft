Public Class DocFileSrc

    Shared Function Load(oDocfileSrc As DTODocFileSrc) As Boolean
        Dim retval As Boolean = DocfileSrcLoader.Load(oDocfileSrc)
        Return retval
    End Function

    Shared Function Update(oDocFileSrc As DTODocFileSrc, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DocfileSrcLoader.Update(oDocFileSrc, exs)
        Return retval
    End Function

    Shared Function Delete(oDocFileSrc As DTODocFileSrc, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DocfileSrcLoader.Delete(oDocFileSrc, exs)
        Return retval
    End Function

End Class



Public Class DocFileSrcs
    Shared Function All(value As DTOBaseGuid) As List(Of DTODocFileSrc)
        Dim retval As List(Of DTODocFileSrc) = DocfileSrcsLoader.All(value)
        Return retval
    End Function
End Class
