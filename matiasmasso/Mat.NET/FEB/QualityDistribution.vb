Public Class QualityDistribution
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProveidor As DTOProveidor, fchFrom As Date) As Task(Of List(Of DTOQualityDistribution))
        Return Await Api.Fetch(Of List(Of DTOQualityDistribution))(exs, "QualityDistribution", oProveidor.Guid.ToString, FormatFch(fchFrom))
    End Function

End Class
