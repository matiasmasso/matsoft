@ModelType DTO.DTOCustomer
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oBrands As List(Of DTO.DTOProductBrand) = BLL.BLLProductBrands.All(Model)
End Code

<section id="AdvancedOptions">
    <a href="#" class="AdvancedOptions">
        @oWebSession.Tradueix("mostrar opciones avanzadas...", "mostrar opcions avançades...", "Display advanced options...")
    </a>
    <div class="AdvancedOptions" hidden="hidden">
        Opcions avançades
    </div>
</section>

<section id="ProductSelection">
    @Html.Partial("ProductSelection",oBrands)
</section>


<section id="BasketGrid">
    @Html.Partial("_BasketGrid")
</section>