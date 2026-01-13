Public Class RepComFollowUpsController
    Inherits _MatController

    Async Function Index(guid As Guid?) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            ViewBag.Title = Mvc.ContextHelper.Tradueix("Seguimiento de Comisiones", "Seguiment de Comisions", "Commission Follow up")
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager
                    If guid = Nothing Then
                        retval = MyBase.UnauthorizedView()
                    Else
                        Dim oRep As New DTORep(guid)
                        Return View("RepComFollowUps", oRep)
                    End If

                Case DTORol.Ids.rep
                    Dim oRep = Await FEB2.User.GetRep(oUser, exs)
                    Return View("RepComFollowUps", oRep)
                Case DTORol.Ids.unregistered
                    retval = LoginOrView("RepComFollowUps")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval

    End Function

End Class
