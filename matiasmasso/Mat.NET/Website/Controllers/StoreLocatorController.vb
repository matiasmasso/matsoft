Public Class StoreLocatorController
    Inherits _MatController


    Function Index(product As Nullable(Of Guid)) As ActionResult
        Dim retval As ActionResult = Redirect("/")
        Return retval
    End Function

    Async Function Index_Old(product As Nullable(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim model As New DTO.ProductModel
        Dim exs As New List(Of Exception)
        If product Is Nothing Then
            Dim oBrands = Await FEB.ProductBrands.All(exs, Website.GlobalVariables.Emp)
            If exs.Count = 0 Then

                For Each oBrand In oBrands.Where(Function(x) x.EnabledxConsumer).ToList()
                    model.Items.Add(oBrand.Nom.Tradueix(ContextHelper.Lang), tag:=oBrand.Guid.ToString)
                Next

                Dim oDefaultBrand As DTOProductBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Romer)
                Dim oDefaultItem = model.Items.FirstOrDefault(Function(x) x.Tag = oDefaultBrand.Guid.ToString)
                If oDefaultItem Is Nothing Then oDefaultItem = model.Items.First
                model.Tag = oDefaultItem.Tag
                model.BrandLogoUrl = oDefaultBrand.LogoUrl()
            Else
                retval = Await MyBase.ErrorResult(exs)
            End If
        Else
            model.Product = Await FEB.Product.Find(exs, product)
            model.Tag = product.ToString
        End If

        ContextHelper.NavViewModel.ResetCustomMenu()
        ViewBag.Title = ContextHelper.Tradueix("Distribuidores Oficiales", "Distribuidors Oficials", "Official Dealers")
        Return View("StoreLocator", model)
    End Function

    Async Function Premium(premiumline As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim model = Await FEB.PremiumLine.Find(premiumline, exs)
        If exs.Count = 0 Then
            ContextHelper.NavViewModel.ResetCustomMenu()
            ViewBag.Title = ContextHelper.Tradueix("Distribuidores Oficiales", "Distribuidors Oficials", "Official Dealers")
            retval = View("StoreLocatorPremium", model)
            'model. = oDefaultBrand.LogoUrl()
        Else
            retval = Await MyBase.ErrorResult(exs)
        End If

        Return retval
    End Function

    Async Function Fetch(product As Guid) As Threading.Tasks.Task(Of JsonResult)
        've de StoreLocator.js
        Dim exs As New List(Of Exception)

        'passa de Baby-Safe iSize 5Z a Dualfix iSize temporalment
        If product.Equals(New Guid("d1449192-b880-4309-a322-946f72686797")) Then
            product = New Guid("7318ff90-5847-4d73-9b5b-4dafc168810b")
        End If
        Dim oProduct As New DTOProduct(product)
        Dim oLang = ContextHelper.Lang
        Dim value = Await FEB.StoreLocator.Fetch(exs, oProduct, oLang)
        Dim retval = Json(value, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function FetchPremium(premiumline As Guid) As Threading.Tasks.Task(Of JsonResult)
        've de StoreLocator.js
        Dim exs As New List(Of Exception)
        Dim oPremiumLine As New DTOPremiumLine(premiumline)
        Dim oLang = ContextHelper.Lang
        Dim value = Await FEB.StoreLocator.FetchPremium(exs, oPremiumLine, oLang)
        Dim retval = Json(value, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function FromGeoLocation(latitud As Decimal, longitud As Decimal, product As Guid) As PartialViewResult
        'Dim oLocation As DTOLocation = BLLWebAtlas.ClosestLocation(oProduct, latitud, longitud)
        Dim Model As New DTOProduct(product)
        Return PartialView("_StoreLocatorOffline", Model)
    End Function


End Class
