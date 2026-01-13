Public Class ProRafflesController
    Inherits _MatController


    Public Function Index() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = Session("User")
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    ViewBag.Title = Mvc.ContextHelper.Lang.Tradueix("Sorteos", "Sortejos", "Raffles", "Sorteios")
                    ViewBag.SideMenuItems = MyBase.SideMenuItems()
                    retval = View("Raffles")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function RafflesPartial(year As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim model = Await FEB2.Raffles.Headers(exs, False, True, , year)
        If exs.Count = 0 Then
            Return PartialView("_Raffles", model)
        Else
            Return Await ErrorResult(exs)
        End If
    End Function
End Class

Public Class ProRaffleController
    Inherits _MatController

    Public Async Function Index(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = Session("User")
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim model = Await FEB2.Raffle.Find(id, exs)
                    If exs.Count = 0 Then
                        ViewBag.Title = Mvc.ContextHelper.Lang.Tradueix("Sorteo", "Sorteig", "Raffle", "Sorteio")
                        ViewBag.SideMenuItems = MyBase.SideMenuItems()
                        Dim oCountries = Await FEB2.Countries.All(ContextHelper.Lang, exs)
                        ViewBag.Countries = oCountries.Where(Function(x) x.ISO = "ES" Or x.ISO = "PT").ToList()
                        retval = View("Raffle", model)
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

End Class
