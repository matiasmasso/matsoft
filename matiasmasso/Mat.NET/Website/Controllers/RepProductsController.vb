Public Class RepProductsController
    Inherits _MatController

    '
    ' GET: /RepProducts

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.Unregistered
                    retval = MyBase.LoginOrView("RepProducts")
                Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                    Dim oRep = Await FEB.User.GetRep(oUser, exs)
                    retval = View("RepProducts", oRep)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            retval = MyBase.LoginOrView()
        End If
        Return retval
    End Function

    Function FromRep(guid As Guid) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.unregistered
                    retval = MyBase.LoginOrView("RepProducts")
                Case DTORol.Ids.salesManager, DTORol.Ids.admin, DTORol.Ids.superUser
                    Dim oRep As New DTORep(guid)
                    retval = View("RepProducts", oRep)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            retval = MyBase.LoginOrView
        End If
        Return retval
    End Function

End Class