Public Class RetencionesController
    Inherits _MatController

    Async Function index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oGuid As Guid = Nothing
        Dim oUser = ContextHelper.GetUser()
        ViewBag.Title = ContextHelper.Tradueix("Retenciones IRPF", "Retencions IRPF", "Taxes")
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            If oUser.Rol.isStaff Or oUser.Rol.id = DTORol.Ids.rep Then
                Dim Model = Await FEB.CertificatIrpfs.All(exs, MyBase.User)
                If exs.Count = 0 Then
                    retval = View("Retenciones", Model)
                Else
                    retval = Await MyBase.ErrorResult(exs)
                End If
            Else
                retval = MyBase.UnauthorizedView()
            End If
        Else
            retval = MyBase.LoginOrView()
        End If

        Return retval
    End Function


End Class
