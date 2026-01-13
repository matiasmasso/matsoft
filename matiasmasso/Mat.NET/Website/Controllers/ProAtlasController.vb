Public Class ProAtlasController
    Inherits _MatController

    Public Function Index() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If MyBase.User Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case MyBase.User.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    ViewBag.Title = ContextHelper.Lang.Tradueix("Contactos", "Contactes", "Contacts")
                    ViewBag.SideMenuItems = MyBase.SideMenuItems()
                    retval = View("Atlas")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function Contact(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If MyBase.User Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case MyBase.User.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim model = Await FEB.Customer.Find(exs, id)
                    FEB.Contact.Load(model, exs)
                    If exs.Count = 0 Then
                        If model Is Nothing Then
                            retval = Await ErrorResult("Contact " & id.ToString & " not found")
                        Else
                            ViewBag.Title = model.FullNom
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            retval = View("Contact", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function searchPartial(searchKey As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim model As New List(Of DTOContact)
        If searchKey > " " Then
            model = Await FEB.Contact.Search(exs, ContextHelper.GetUser, searchKey, DTOContact.SearchBy.notset)
        End If

        If exs.Count = 0 Then
            retval = PartialView("_ContactList", model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

End Class
