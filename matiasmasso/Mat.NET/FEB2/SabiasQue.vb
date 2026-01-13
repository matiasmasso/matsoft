Public Class SabiasQue
    Inherits _FeblBase

    Shared Async Function Search(exs As List(Of Exception), sFriendlyUrlSegment As String) As Task(Of DTOSabiasQuePost)
        Return Await Api.Execute(Of String, DTOSabiasQuePost)(sFriendlyUrlSegment, exs, "sabiasQue/search")
    End Function

End Class
