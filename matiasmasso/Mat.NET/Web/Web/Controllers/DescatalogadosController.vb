Public Class DescatalogadosController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.cliFull, DTORol.Ids.cliLite, DTORol.Ids.manufacturer, DTORol.Ids.superUser, DTORol.Ids.marketing})
            Case AuthResults.success
                Dim fchFrom = Now.AddMonths(-12)
                Dim oUser = ContextHelper.GetUser()
                Dim oSheet = Await FEB2.ProductSkus.Obsolets(exs, oUser, ContextHelper.Lang, fchFrom)
                If exs.Count = 0 Then
                    Dim oPagination = New DTOGallery.PaginationClass(oSheet.rows.Count - 1)
                    Dim Model As New DescatalogadosModel
                    With Model
                        .FchFrom = fchFrom
                        .User = oUser
                        .IsSubscribed = Await FEB2.Subscriptor.IsSubscribed(exs, DTOSubscription.Wellknowns.AvisDescatalogats, .User)
                        .Pagination = oPagination
                        .Sheet = oSheet '.Clon(.Pagination.PageFirstItem, .Pagination.PageLastItem)
                    End With

                    If exs.Count = 0 Then
                        retval = View("Descatalogados", Model)
                    Else
                        retval = View("Error")
                    End If
                Else
                    retval = Await MyBase.ErrorResult(exs)
                End If

            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function Reload(fchFrom As Date) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        Dim Model As New DescatalogadosModel
        With Model
            .FchFrom = fchFrom
            .User = oUser
            .Sheet = Await FEB2.ProductSkus.Obsolets(exs, oUser, ContextHelper.Lang, fchFrom)
        End With

        If exs.Count = 0 Then
            retval = PartialView("_Grid", Model)
        Else
            retval = PartialView("Error")
        End If

        Return retval
    End Function

    Async Function Subscribe(value As Boolean) As Threading.Tasks.Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.AvisDescatalogats)
        Dim retval As Boolean
        If value Then
            retval = Await FEB2.Subscription.Subscribe(exs, GlobalVariables.Emp, oSubscription, oUser)
        Else
            retval = Await FEB2.Subscription.UnSubscribe(exs, GlobalVariables.Emp, oSubscription, oUser)
        End If
        Return retval
    End Function

    Async Function Excel(Optional fchFrom As Nullable(Of Date) = Nothing) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If fchFrom Is Nothing Then fchFrom = Today.AddYears(-1)
        Dim Model As New DescatalogadosModel
        With Model
            .FchFrom = fchFrom
            .User = ContextHelper.GetUser()
            .Sheet = Await FEB2.ProductSkus.Obsolets(exs, Model.User, ContextHelper.Lang, Model.FchFrom)
        End With

        If exs.Count = 0 Then
            retval = MyBase.ExcelResult(Model.Sheet)
        Else
            retval = PartialView("Error")
        End If

        Return retval
    End Function

End Class

Public Class DescatalogadosModel
    Property FchFrom As Date
    Property User As DTOUser
    Property IsSubscribed As Boolean
    Property Sheet As ExcelHelper.Sheet
    Property Pagination As DTOGallery.PaginationClass
End Class
