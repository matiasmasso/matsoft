Public Class ProProductCategoryController
    Inherits _MatController

    Public Async Function Index(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If exs.Count = 0 Then
            If oUser Is Nothing Then
                retval = MyBase.UnauthorizedView()
            Else
                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                        Dim model = Await FEB.ProductCategory.Find(exs, id)
                        ViewBag.Title = model.FullNom(ContextHelper.Lang)
                        ViewBag.SideMenuItems = MyBase.SideMenuItems()
                        If exs.Count = 0 Then
                            If model Is Nothing Then
                                'retval = ErrorNotFoundResult
                            Else
                                retval = View("Properties", model)
                            End If
                        Else
                            retval = Await ErrorResult(exs)
                        End If
                    Case Else
                        retval = MyBase.UnauthorizedView()
                End Select
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

End Class
