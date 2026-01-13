Public Class fourmomsController
    Inherits _MatController

    Async Function distributors() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oProveidor As DTOProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.FourMoms)
        Dim sb As New System.Text.StringBuilder
        Dim items = Await FEB.ProductDistributors.FromManufacturer(exs, oProveidor)
        Dim retval As JsonResult = Json(items, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function SalesData() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()

        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Manufacturer})
            Case AuthResults.success
                If oUser.Rol.id = DTORol.Ids.Manufacturer Then
                    Dim oManufacturer = Await FEB.User.GetProveidor(oUser, exs)
                    If oManufacturer Is Nothing Then
                        retval = MyBase.UnauthorizedView
                    ElseIf oManufacturer.Equals(DTOProveidor.Wellknown(DTOProveidor.Wellknowns.FourMoms)) Then
                        retval = View()
                    Else
                        retval = MyBase.UnauthorizedView
                    End If
                Else
                    retval = View()
                End If
            Case AuthResults.login
                retval = MyBase.Login
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView
        End Select

        Return retval
    End Function

    Function SalesData_OnFchChange(fch As String) As PartialViewResult
        Dim retval As PartialViewResult = Nothing
        Dim DtFch As Date
        If Date.TryParse(fch, DtFch) Then
            retval = PartialView("SalesData_", DtFch)
        End If
        Return retval
    End Function

End Class