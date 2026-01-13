@ModelType DTOProductPageQuery
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim sLaunchment As String = DTOProduct.Launchment(Model.Product, Mvc.ContextHelper.lang())
    Dim oNavbarPills As DTONavbar = FEB2.Product.Pills(Model.Product, Mvc.ContextHelper.lang(), Model.Tab)
End Code

<style scoped>
    .disponibilitat {
        clear: both;
        color: red;
    }
</style>

<script type="text/javascript">
    var productGuid = '@Model.Product.Guid.ToString';
</script>

<div id="pills" class="pills">
    @Html.Raw(oNavbarPills.Html())
    <!--Html.Partial("_Pills")-->
</div>

@If sLaunchment > "" Then
    @<div class="disponibilitat">
        @sLaunchment
    </div>
end if

<div id="pillContent" class="paginated">
    @Select Case Model.Tab
        Case DTOProduct.Tabs.general
            @Select Case DirectCast(Model.Product, DTOProduct).SourceCod
                Case DTOProduct.SourceCods.Brand
                    @Html.Partial("_Categories", Model.Product)
                                            Case DTOProduct.SourceCods.Category
                                                @Html.Partial("_Category", Model.Product)
                                                                        Case DTOProduct.SourceCods.Sku
                                                                            @Html.Partial("_SKU", Model.Product)
                                                                    End Select
                            Case DTOProduct.Tabs.coleccion
                                @Html.Partial("_Collection", Model.Product)
                                                Case DTOProduct.Tabs.accesorios
                                                    @Html.Partial("_Accessories", Model.Product)
                                                                    Case DTOProduct.Tabs.distribuidores
                                                                        @If FEB2.Product.ShowAtlas(Model.Product) Then
                                                                            @Html.Partial("_SalePoints", Model)
                                                                        End If
                                                                                        Case DTOProduct.Tabs.galeria
                                                                                            @Html.Partial("_MediaResources", Model)
                                                                                                            Case DTOProduct.Tabs.descargas
                                                                                                                Dim exs As New List(Of Exception)
                                                                                                                Dim oDownloads As List(Of DTOProductDownload) = FEB2.Downloads.FromProductOrParentSync(exs, Model.Product, False, True, Mvc.ContextHelper.lang())

                                                                                                                @Html.Partial("_Downloads", oDownloads)
                                                                                                                                Case DTOProduct.Tabs.videos
                                                                                                                                    @Html.Partial("_Videos", New With {.productGuid = Model.product.guid.ToString, .pageIndex = 1, .pageItems = 4})
                                                                                                                                Case Else
                                                                                                                                    @Html.Partial("_Categories", Model.Product)
                                                                                                                            End Select
</div>

@Section Scripts
    <script src="~/Media/js/StoreLocator.js"></script>

    <script>
        $(document).on('click', 'div.showmore a', function (e) {
            event.preventDefault();
            var cod = $(this).data("cod");
            $(".showmore[data-cod='" + cod + "']").hide();
            $(".showless[data-cod='" + cod + "']").show();
            $("div.categoryBody[data-cod='" + cod + "']").css('height', 'auto');
        });

        $(document).on('click', 'div.showless a', function (e) {
            event.preventDefault();
            var cod = $(this).data("cod");
            $(".showmore[data-cod='" + cod + "']").show();
            $(".showless[data-cod='" + cod + "']").hide();
            $("div.categoryBody[data-cod='" + cod + "']").css('height', '185px');
        });

    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Videos.css" rel="stylesheet" />
    <style>
        .RightAlign {
            text-align: right;
        }
    </style>
End Section

