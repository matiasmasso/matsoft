Public Class MvcHome
    Inherits _FeblBase

    Shared Async Function Model(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang, oUser As DTOUser) As Threading.Tasks.Task(Of MvcHomeModel)
        Dim retval = Await Api.Fetch(Of MvcHomeModel)(exs, "MvcHome/model", oEmp.Id, oLang.Tag, OpcionalGuid(oUser))
        Return retval
    End Function
End Class
