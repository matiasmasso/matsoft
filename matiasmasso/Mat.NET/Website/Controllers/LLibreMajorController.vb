Public Class LLibreMajorController
    Inherits _MatController

    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView("LLibreMajor")
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                    retval = View("LLibreMajor")
                Case DTORol.Ids.unregistered
                    retval = LoginOrView("LLibreMajor")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval
    End Function

    Async Function FromYear(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB.Exercici.Find(exs, guid)
        Dim Model As List(Of DTOCcb) = Await FEB.Ccbs.LlibreMajor(exs, oExercici)
        Return PartialView("LLibreMajor_", Model)
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB.Exercici.Find(exs, guid)
        Dim items As List(Of DTOCcb) = Await FEB.Ccbs.LlibreMajor(exs, oExercici)
        Dim Model As New List(Of DTOCcb)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= items.Count Then Exit For
            Model.Add(items(i))
        Next
        Dim retval As PartialViewResult = PartialView("LLibreMajor_", Model)
        Return retval
    End Function
End Class
