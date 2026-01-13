Public Class RepLiqsController
    Inherits _MatController

    Function RepLiqs() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.rep, DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts
                    ViewBag.Title = Mvc.ContextHelper.Tradueix("Mis liquidaciones", "Les meves liquidacions", "My commission statements")
                    retval = View("RepLiqs")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            retval = LoginOrView()
        End If
        Return retval
    End Function

End Class