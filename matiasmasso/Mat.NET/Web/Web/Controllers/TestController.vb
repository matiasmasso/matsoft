Public Class TestController
    Inherits _MatController


    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)

        'to deprecate
        Dim oUser = Await FEB2.User.Find(DTOUser.Wellknown(DTOUser.Wellknowns.zabalaHoyos).Guid, exs)
        Dim oCustomers = Await FEB2.User.GetCustomers(oUser, exs)

        Dim model = Await FEB2.CustomerBasket.Model(exs, oUser, oCustomers.First())
        If exs.Count = 0 Then
            retval = View(model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function


End Class