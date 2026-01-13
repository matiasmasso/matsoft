Public Class eCommerceController
    Inherits _MatController

    Private _Site As DTOConsumerBasket.Sites = DTOConsumerBasket.Sites.Thorley


    Function Index() As ActionResult
        Return View("Home")
    End Function

    Function TestPage() As ActionResult
        Return View()
    End Function

    Function Category(guid As String) As ActionResult
        Dim Model As DTOProductCategory = Nothing
        If GuidHelper.IsGuid(guid) Then
            Dim oGuid As New Guid(guid)
            Model = BLL.BLLProductCategory.Find(oGuid)
        End If
        Return View(Model)
    End Function

    Function Basket() As ActionResult
        Dim Model As DTOConsumerBasket = GetBasket()
        Return View(Model)
    End Function

    Function Pay() As ActionResult
        Dim Model As DTOConsumerBasket = GetBasket()
        Return View(Model)
    End Function

    Function ClearBasket() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oBasket As DTOConsumerBasket = GetBasket()
        oBasket.Items.Clear()
        Dim exs As New List(Of Exception)
        If BLL.BLLConsumerBasket.Update(oBasket, exs) Then
            retval = View("Home")
        Else
            retval = View("Error", exs)
        End If
        Return retval
    End Function

    Function Update() As JsonResult
        Dim oBasket As DTOConsumerBasket = GetBasket()
        oBasket.Items.Clear()
        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLL.BLLConsumerBasket.Update(oBasket, exs) Then
            myData = New With {.result = 0}
        Else
            myData = New With {.result = 1}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function Ok() As ActionResult
        Dim retval As ActionResult = View()
        Return retval
    End Function

    Function addItem(sku As Guid, qty As Integer) As PartialViewResult
        Dim retval As PartialViewResult = Nothing
        Dim oBasket As DTOConsumerBasket = GetBasket()
        Dim oSku As DTOProductSku = BLL.BLLProductSku.Find(sku)
        Dim exs As New List(Of Exception)
        If BLL.BLLConsumerBasket.AddItem(oBasket, oSku, qty, exs) Then
            retval = PartialView("Nav_", DTO.Defaults.eComPages.Category)
        Else
            retval = PartialView("Error", exs)
        End If
        Return retval
    End Function

    Private Function GetBasket() As DTOConsumerBasket
        Dim oUser As DTOUser = GetSession.User
        Dim retval As DTOConsumerBasket = BLL.BLLConsumerBasket.FindLastorNew(_Site, oUser)
        Return retval
    End Function

End Class