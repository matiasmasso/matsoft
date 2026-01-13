Public Class ForecastController
    Inherits _MatController

    Async Function OrdersPreview() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.Manufacturer})
            Case AuthResults.success
                Dim oProveidor = Await FEB.User.GetProveidor(oUser, exs)
                'Dim Model As List(Of DTOPurchaseOrder) = BLLForecasts.OrdersPreview(oProveidor, 89)
                'retval = View(Model)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function
End Class
