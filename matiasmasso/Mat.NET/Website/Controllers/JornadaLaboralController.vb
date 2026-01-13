Public Class JornadaLaboralController
    Inherits _MatController

    <HttpGet>
    Async Function Log() As Threading.Tasks.Task(Of ActionResult)
        '----------------------------https://bit.ly/3ry6WqX
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        ElseIf oUser.Rol.isStaff Then
            Dim oStatus = Await FEB.JornadaLaboral.Log(exs, oUser)
            If exs.Count = 0 Then
                retval = RedirectToAction("Logs", "JornadaLaboral")
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = MyBase.UnauthorizedView()
        End If
        Return retval
    End Function

    <HttpGet>
    Async Function Logs() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        ElseIf oUser.Rol.isStaff Then
            Dim oLogs = Await FEB.JornadesLaborals.FromUser(exs, oUser)
            If exs.Count = 0 Then
                retval = View()
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = MyBase.UnauthorizedView()
        End If
        Return retval
    End Function

    Async Function RemoveLast() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        ElseIf oUser.Rol.isStaff Then
            Dim oLogs = Await FEB.JornadesLaborals.RemoveLast(exs, oUser)
            If exs.Count = 0 Then
                ViewBag.Title = ContextHelper.Tradueix("Registro de Jornada Laboral", "Registre de Jornada Laboral", "Work day logs")
                retval = View("Logs", oLogs)
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = MyBase.UnauthorizedView()
        End If
        Return retval
    End Function

End Class
