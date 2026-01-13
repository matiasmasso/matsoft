Public Class MaybornController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.Manufacturer})
            Case AuthResults.success
                Dim oProveidor = Await FEB2.User.GetProveidor(oUser, exs)
                If oProveidor.Equals(DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)) Then
                    retval = View()
                Else
                    retval = MyBase.UnauthorizedView()
                End If
            Case AuthResults.login
                retval = LoginOrView("Mayborn")
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function
End Class
