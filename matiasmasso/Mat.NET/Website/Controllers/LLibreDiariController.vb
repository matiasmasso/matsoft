Public Class LLibreDiariController
    Inherits _MatController

    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                    retval = View("LlibreDiari")
                Case DTORol.Ids.unregistered
                    retval = LoginOrView("LlibreDiari")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval
    End Function

    Async Function FromYear(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB.Exercici.Find(exs, guid)
        Dim Model = Await FEB.Ccas.Headers(oExercici, exs)
        Return PartialView("LlibreDiari_", Model)
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB.Exercici.Find(exs, guid)
        Dim items = Await FEB.LlibreDiari.Headers(exs, oExercici)
        Dim Model As New List(Of DTOCca)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= items.Count Then Exit For
            Model.Add(items(i))
        Next
        Dim retval As PartialViewResult = PartialView("LLibreDiari_", Model)
        Return retval
    End Function
End Class
