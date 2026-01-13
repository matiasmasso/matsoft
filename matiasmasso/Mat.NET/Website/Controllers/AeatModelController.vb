Public Class AeatModelController
    Inherits _MatController

    Async Function Index(guid As Guid, emp As DTOEmp.Ids) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oEmp As New DTOEmp(emp)

        Select Case MyBase.Authorize({DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.banc, DTORol.Ids.auditor, DTORol.Ids.accounts})
            Case AuthResults.success
                Dim model As DTOAeatModel = Await FEB.AeatModel.Find(exs, guid, ContextHelper.GetUser)
                If exs.Count = 0 And model IsNot Nothing Then
                    ViewBag.Title = String.Format("{0} {1}", model.Nom, model.Dsc)
                    retval = View("AeatModel", model)
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
