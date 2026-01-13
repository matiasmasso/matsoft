Public Class AeatModelsController
    Inherits _MatController

    Async Function Index(emp As DTOEmp.Ids) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        'TODO: switch user for alternative emps
        Select Case MyBase.Authorize({DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.banc, DTORol.Ids.auditor, DTORol.Ids.accounts})
            Case AuthResults.success
                Dim model = Await FEB.AeatModels.All(exs, ContextHelper.GetUser())
                If exs.Count = 0 Then
                    ViewBag.Title = ContextHelper.Tradueix("Documentación fiscal", "Documentació fiscal", "Tax docs")
                    retval = View("AeatModels", model)
                Else
                    retval = Await ErrorResult(exs)
                End If
            Case AuthResults.login
                retval = MyBase.Login()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

End Class
