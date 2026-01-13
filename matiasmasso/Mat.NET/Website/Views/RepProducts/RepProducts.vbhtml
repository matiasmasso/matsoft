@ModelType DTORep
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)
    Dim items = FEB.RepProducts.AllSync(exs, Website.GlobalVariables.Emp, Model)
    Dim oProducts As List(Of DTOProduct) = items.Select(Function(x) x.Product).Distinct
End Code

<div class='Expanded truncate'>
    <a href="#" class="PlusMinus">&nbsp;</a>
    Marcas Comerciales


    @For Each oProduct As DTOProduct In oProducts
        @<div class='Collapsed truncate'>
            <a href="#" class="PlusMinus">&nbsp;</a>
            @DTOProduct.GetNom(oProduct)
            @For Each item As DTORepProduct In items.FindAll(Function(x) x.Product.Equals(oProduct))
                @DTOArea.FullNom(item.Area)
            Next
        </div>
    Next
</div>