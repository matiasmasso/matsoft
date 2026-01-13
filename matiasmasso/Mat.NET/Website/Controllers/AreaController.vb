Public Class AreaController
    Inherits _MatController



    Shadows Function LoginOrView(Optional sView As String = "", Optional oModel As Object = Nothing) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.comercial, DTORol.Ids.rep
                    If sView > "" Then
                        If oModel Is Nothing Then
                            retval = View(sView)
                        Else
                            retval = View(sView, oModel)
                        End If
                    Else
                        If oModel Is Nothing Then
                            retval = View()
                        Else
                            retval = View(oModel)
                        End If
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

End Class