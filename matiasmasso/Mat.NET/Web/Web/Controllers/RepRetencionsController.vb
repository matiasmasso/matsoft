Public Class RepRetencionsController
    Inherits _MatController

    Async Function index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oGuid As Guid = Nothing
        Dim oUser = ContextHelper.GetUser()
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Retenciones IRPF", "Retencions IRPF", "Taxes")
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager
                    If oGuid = Nothing Then
                        retval = MyBase.UnauthorizedView()
                    Else
                        Dim oRep As New DTORep(oGuid)
                        Dim Model = Await FEB2.RepCertsRetencio.All(exs, oRep)
                        Return View("RepRetencions", Model)
                    End If

                Case DTORol.Ids.rep
                    Dim oRep As DTORep = Await FEB2.User.GetRep(oUser, exs)
                    Dim Model = Await FEB2.RepCertsRetencio.All(exs, oRep)
                    Return View("RepRetencions", Model)

                Case DTORol.Ids.unregistered
                    retval = LoginOrView("RepRetencions")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            Return MyBase.LoginOrView()
        End If

        Return retval
    End Function

End Class
