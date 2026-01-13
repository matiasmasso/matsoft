Public Class IncentivosController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()

        If oUser Is Nothing Then
            retval = LoginOrView("Incentivos")
        Else
            ViewBag.Title = ContextHelper.Tradueix("Promociones vigentes a ", "Promocions vigents a ", "Active promotions on ") & String.Format(Today, "dd/MM/yy")
            Select Case oUser.Rol.Id
                Case DTORol.Ids.Unregistered
                    retval = LoginOrView()
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora, DTORol.Ids.Marketing, DTORol.Ids.Accounts, DTORol.Ids.Manufacturer, DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    Dim Model = Await FEB.Incentius.Headers(exs, oUser)
                    retval = View("Incentivos", Model)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function


    Async Function Promo(Guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()

        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            Select Case oUser.Rol.Id
                Case DTORol.Ids.Unregistered
                    retval = LoginOrView()
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora, DTORol.Ids.Marketing, DTORol.Ids.Accounts, DTORol.Ids.Manufacturer, DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    Dim oIncentiu = Await FEB.Incentiu.Find(exs, Guid)
                    retval = View(oIncentiu)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval

    End Function

    Async Function SalePoints(Guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oIncentiu = Await FEB.Incentiu.Find(exs, Guid)
        Dim retval As ActionResult = View(oIncentiu)
        Return retval
    End Function
End Class
