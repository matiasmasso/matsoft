Public Class LLibreMajorController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView("LLibreMajor")
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                    retval = View("LLibreMajor")
                Case DTORol.Ids.Unregistered
                    retval = LoginOrView("LLibreMajor")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval
    End Function

    Async Function FromYear(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB2.Exercici.Find(exs, guid)
        Dim Model As List(Of DTOCcb) = Await FEB2.Ccbs.LlibreMajor(exs, oExercici)
        Return PartialView("LLibreMajor_", Model)
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici = Await FEB2.Exercici.Find(exs, guid)
        Dim items As List(Of DTOCcb) = Await FEB2.Ccbs.LlibreMajor(exs, oExercici)
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
