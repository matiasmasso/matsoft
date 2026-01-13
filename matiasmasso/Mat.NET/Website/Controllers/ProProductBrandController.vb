
Public Class ProProductBrandController
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
                        Dim model = Await FEB.ProductBrand.Find(exs, id)
                        ViewBag.Title = model.nom
                        If exs.Count = 0 Then
                            If model Is Nothing Then
                                'retval = ErrorNotFoundResult
                            Else
                                ViewBag.Title = model.nom
                                ViewBag.SideMenuItems = MyBase.SideMenuItems()
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
