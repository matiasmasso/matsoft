@ModelType Date

@Code
    Dim exs As New List(Of Exception)
    Dim oSales As List(Of DTOProductAreaQty) = FEB.FourMoms.SalesSync(Model, exs)
    Dim oSalePoints As List(Of DTOProductAreaQty) = FEB.FourMoms.SalePointsSync(Model, exs)
    Dim oStocks As List(Of DTOProductAreaQty) = FEB.FourMoms.StocksSync(Model, exs)

    Dim oCategories As New List(Of DTOProduct)
    For Each item As DTOProductAreaQty In oSalePoints
        If Not oCategories.Exists(Function(x) x.Equals(item.Product)) Then
            oCategories.Add(item.Product)
        End If
    Next

    Dim oLang As DTOLang = DTOLang.ENG

End Code


Sales during last 30 days

<div class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">Product</div>
        <div class="CellNum">Total</div>
        <div class="CellNum">Spain</div>
        <div class="CellNum">Portugal</div>
        <div class="CellNum">Andorra</div>
    </div>

    @For Each item In oCategories
        @Code
            Dim iEs, iPt, iAd As Integer
            Dim oEs As DTOProductAreaQty = oSales.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain).Guid) And x.Product.Equals(item))
            Dim oDe As DTOProductAreaQty = oSales.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Germany).Guid) And x.Product.Equals(item))
            Dim oPt As DTOProductAreaQty = oSales.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal).Guid) And x.Product.Equals(item))
            Dim oAd As DTOProductAreaQty = oSales.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra).Guid) And x.Product.Equals(item))
            If oEs IsNot Nothing Then iEs = oEs.Qty + oDe.Qty
            If oPt IsNot Nothing Then iPt = oPt.Qty
            If oAd IsNot Nothing Then iAd = oAd.Qty
        End Code

        @<div class="Row">
            <div class="CellTxt">@item.Nom</div>
            <div class="CellNum">@Format(iEs + iPt + iAd, "#,###")</div>
            <div class="CellNum">@Format(iEs, "#,###")</div>
            <div class="CellNum">@Format(iPt, "#,###")</div>
            <div class="CellNum">@Format(iAd, "#,###")</div>
        </div>
            Next



</div>



Active Sale Points during last 6 months

<div class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">Product</div>
        <div class="CellNum">Total</div>
        <div class="CellNum">Spain</div>
        <div class="CellNum">Portugal</div>
        <div class="CellNum">Andorra</div>
    </div>
    @For Each item In oCategories
        @Code
            Dim iEs, iPt, iAd As Integer
            Dim oEs As DTOProductAreaQty = oSalePoints.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain).Guid) And x.Product.Equals(item))
            Dim oDe As DTOProductAreaQty = oSalePoints.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Germany).Guid) And x.Product.Equals(item))
            Dim oPt As DTOProductAreaQty = oSalePoints.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal).Guid) And x.Product.Equals(item))
            Dim oAd As DTOProductAreaQty = oSalePoints.Find(Function(x) x.Area.Guid.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra).Guid) And x.Product.Equals(item))
            If oEs IsNot Nothing Then iEs = oEs.Qty + oDe.Qty
            If oPt IsNot Nothing Then iPt = oPt.Qty
            If oAd IsNot Nothing Then iAd = oAd.Qty
        End Code

        @<div class="Row">
            <div class="CellTxt">@item.Nom</div>
             <div class="CellNum">@Format(iEs + iPt + iAd, "#,###")</div>
             <div class="CellNum">@Format(iEs, "#,###")</div>
             <div class="CellNum">@Format(iPt, "#,###")</div>
             <div class="CellNum">@Format(iAd, "#,###")</div>
        </div>
            Next


</div>

Stocks on @Model.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))

<div class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">Product</div>
        <div class="CellNum">Stock</div>
    </div>

    @For Each item In oCategories
        @Code
            Dim iQty As Integer = 0
            Dim oItem As DTOProductAreaQty = oStocks.Find(Function(x) x.Product.Equals(item))
            If oItem IsNot Nothing Then
                iQty = oItem.Qty
            End If
        End Code

        @<div class="Row">
            <div class="CellTxt">@item.Nom</div>
            <div class="CellNum">@Format(iQty, "#,###")</div>
        </div>
            Next
</div>


