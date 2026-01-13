Imports System.Threading.Tasks
Public Class ProductRankController
    Inherits _MatController

    Async Function index() As Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.CliFull,
                                               DTORol.Ids.CliLite,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.Manufacturer})
            Case AuthResults.success
                Dim Model As DTOProductRank = Await FEB2.ProductRank.Factory(exs, oUser)

                If exs.Count = 0 Then
                    ViewBag.Title = ContextHelper.Lang.Tradueix("Ranking de productos", "Ranquing de productes", "Product ranking")
                    retval = View(Model)
                Else
                    retval = Await ErrorResult(exs)
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function update(period As DTOProductRank.Periods, zona As Guid, brand As Guid, unit As DTOProductRank.Units) As Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim Model As DTOProductRank = Await FEB2.ProductRank.Factory(exs, oUser, period, zona, brand, unit)
        If exs.Count = 0 Then
            Return PartialView("ProductRank_", Model)
        Else
            Return PartialView("_Error")
        End If
    End Function
End Class
