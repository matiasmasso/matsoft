Public Class LeadAreas
    Inherits _FeblBase

    Shared Async Function Pro(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang) As Task(Of DTOLeadAreas)
        Dim retval = Await Api.Fetch(Of DTOLeadAreas)(exs, "LeadAreas/pro", oEmp.Id, oLang.Tag)
        Return retval
    End Function

    Shared Async Function Consumer(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang) As Task(Of DTOLeadAreas)
        Return Await Api.Fetch(Of DTOLeadAreas)(exs, "LeadAreas/consumer", oEmp.Id, oLang.Tag)
    End Function

End Class
