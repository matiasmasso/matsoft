Public Class ImpagatsController
    Inherits _MatController

    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult
        Dim Model As List(Of DTOImpagat) = Nothing
        Dim oUser = ContextHelper.GetUser()
        Select Case oUser.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Accounts, DTORol.Ids.Operadora
                Model = Await FEB.Impagats.All(exs, oUser)
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                Dim oRep = Await FEB.User.GetRep(oUser, exs)
                Model = Await FEB.Impagats.All(exs, oRep)
        End Select
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Tradueix("Impagados", "Impagats", "Unpayments")
            retval = View("Impagats", Model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function
End Class
