Public Class ProCredencialsController
    Inherits _MatController
    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        Dim model = Await FEB.Credencials.All(oUser, exs)
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Lang.Tradueix("Credenciales", "Credencials", "Credentials")
            ViewBag.SideMenuItems = MyBase.SideMenuItems()
            Return View("Credencials", model)
        Else
            Return Await ErrorResult(exs)
        End If
    End Function
End Class
