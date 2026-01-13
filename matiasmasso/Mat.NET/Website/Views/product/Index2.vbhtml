@ModelType DTOProductPageQuery
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim exs As New List(Of Exception)
    Dim sLaunchment As String = DTOProduct.Launchment(Model.Product, ContextHelper.lang())
    Dim oNavbarPills As DTONavbar = FEB.Product.Pills(Model.Product, ContextHelper.lang(), Model.Tab)
    Dim sLocationGuid As String = Guid.Empty.ToString
    If Model.Location IsNot Nothing Then sLocationGuid = Model.Location.Guid.ToString

    Dim oDepts As New List(Of DTODept)
    If Model.Tab = DTOProduct.Tabs.general AndAlso DirectCast(Model.Product, DTOProduct).SourceCod = DTOProduct.SourceCods.Brand Then
        oDepts = FEB.Depts.AllSync()
    End If

    Dim oLang = ContextHelper.lang()
    Dim finalText = Model.Product.Content.Tradueix(oLang)
    Dim fbImg = MatHelperStd.TextHelper.FbImg(finalText)
    Dim fbDescription = Model.Product.Excerpt.Tradueix(oLang)

End Code


@Section AdditionalMetaTags
    <meta property="og:title" content="@DTOProduct.GetNom(Model.Product)" />
    @If fbImg > "" Then
        @<meta property="og:image:url" content="@fbImg" />
    End If
    <meta property="og:type" content="website" />
    <meta property="og:url" content="@Model.Product.GetUrl(ContextHelper.Lang)" />
    @If fbDescription > "" Then
        @<meta Property="og:description" content="@Html.Raw(fbDescription)" />
    End If

End Section



<style scoped>
    .disponibilitat {
        clear: both;
        color: red;
    }
</style>

<script type="text/javascript">
    var productGuid = '@Model.Product.Guid.ToString';
    var isStoreLocator = false;
    var geolocationState;
</script>

<div id="pills" class="pills">
    @Html.Raw(oNavbarPills.Html())
    <!--Html.Partial("_Pills")-->
</div>

@If sLaunchment > "" AndAlso Model.Tab = DTOProduct.Tabs.general Then
    @<div class="disponibilitat">
        @sLaunchment
    </div>
End If

<div id="pillContent" class="paginated">
    @Select Case Model.Tab
        Case DTOProduct.Tabs.general
            Select Case DirectCast(Model.Product, DTOProduct).SourceCod
                Case DTOProduct.SourceCods.Brand
                    If oDepts.Where(Function(x) x.Brand.Equals(Model.Product)).Count = 0 Then
                        @Html.Partial("_Categories", Model.Product)
                    Else
                        @Html.Partial("_Categories", Model.Product)
                        'Dim oBrand As DTOProductBrand = Model.Product
                        'Dim oSprite = oBrand.DeptsSprite(oDepts, ContextHelper.lang())
                        '@@Html.Partial("_Depts", oSprite)
                    End If
                Case DTOProduct.SourceCods.Dept
                    @Html.Partial("_DeptCategories", Model.Product)
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
                                                                                                            Dim oDistributor As DTOContact = ContextHelper.GetDistribuidorFromCookie()
                                                                                                            If oDistributor Is Nothing Then
                                                                                                                If FEB.Product.ShowAtlas(Model.Product) Then
                                                                                                                    @<script>
                                                                                                                         isStoreLocator = true;
                                                                                                                    </script>
                                                                                                                    @Html.Partial("_StoreLocator", Model)
                                                                                                                End If
                                                                                                            Else
                                                                                                                @Html.Partial("_StoreLocatorExclusive", oDistributor)
                                                                                                            End If
                                                                                                        Case DTOProduct.Tabs.galeria
                                                                                                            @Html.Partial("_MediaResources", Model.Product)
                                                                                                                            Case DTOProduct.Tabs.descargas
                                                                                                                                Dim oDownloads As List(Of DTOProductDownload) = FEB.Downloads.FromProductOrParentSync(exs, Model.Product, False, True, ContextHelper.lang())
                                                                                                                                @Html.Partial("_Downloads", oDownloads)
                                                                                                                                                Case DTOProduct.Tabs.videos
                                                                                                                                                    @Html.Partial("_Videos", Model.Product) ' New With {.productGuid = Model.Product.Guid.ToString, .pageIndex = 1, .pageItems = 4})
                                                                                                                                                Case Else
                                                                                                                                                    @Html.Partial("_Categories", Model.Product)
                                                                                                                                            End Select
</div>

<div class="accordion_wrapper">

    @For Each item In FEB.Product.Accordion(Model.Product, ContextHelper.lang())

        @<button class="accordion_dropdown rightIcon" tab="@item.Action">
            @item.Caption
        </button>

        @<div Class="accordion_content"></div>

    Next
</div>

@Section Scripts

    <script src="~/Media/js/StoreLocator.js"></script>

    <!-- Event snippet for Clic en dónde comprar conversion page In your html page, add the snippet and call gtag_report_conversion when someone clicks on the chosen link or button. -->
    <script>
        function gtag_report_conversion(url) {
            /*var callback = function () { if (typeof (url) != 'undefined') { window.location = url; } };*/
            /*gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 520.0, 'currency': 'EUR', 'event_callback': callback }); return false;*/
            gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 100.0, 'currency': 'EUR' }); return false;
        }
    </script>

    <script>

            /* Geolocation ====================================================================== */

    if (isStoreLocator) {
        if (geolocationState === 'granted') {
            navigator.geolocation.getCurrentPosition(loadNearestNeighbours, errorHandler, { timeout:  10000 });
        } else {
            loadGeoSelector();
        }
    }

    function geolocationState() {
        if (navigator.permissions) {
            navigator.permissions.query({ name: 'geolocation' })
                                    .then(function (permissionStatus) {
                    return(permissionStatus.state);
                });
        }
    }

    function loadNearestNeighbours(position) {
        $('.loading').show();
        var url = '@Url.Action("FromGeoLocation", "Product")';
        $('#StoreLocatorOfflineNearestNeighbours').load(
            url,
            {
                latitud: position.coords.latitude,
                longitud: position.coords.longitude,
                product:  '@Model.Product.Guid.ToString'
            },
            function () {
                $('#StoreLocatorOffline').hide();
                $('#StoreLocatorOfflineNearestNeighbours').show();
                $('.loading').hide();
            }
        );
    }

    function loadGeoSelector() {
        $('#StoreLocatorOfflineNearestNeighbours').hide();
        $('#StoreLocatorOffline').show();
    }

    function errorHandler(positionError) {
        loadGeoSelector();
        if (window.console) {
            console.log(positionError);
        }
        if (positionError.code == 1) {
            alert('debe autorizar el acceso a su ubicación para poder mostrar los puntos de venta más cercanos');
        }
    }

    $(document).on('click', ".BackToGeoLocator a", function (event) {
        event.preventDefault();
        loadGeoSelector();
    });

    $(document).on('click', ".BackToNearestNeighbours a", function (event) {
        event.preventDefault();
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(loadNearestNeighbours, errorHandler, { timeout: 10000 });
        }
    });

    $(document).on('click', "a.OnlineStore", function (event) {
        event.preventDefault();
        logClickThrough($(this));
    });

        /*-------------- Accordion --------------------------------------------------------------------*/


        var acc = document.getElementsByClassName("accordion_dropdown");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var panel = this.nextElementSibling;
                if (panel.style.maxHeight) {
                    $(panel).css("display", "none");
                    panel.style.maxHeight = null;
                } else {
                    $(panel).css("display", "block");
                    if ($(panel).is(':empty')) {
                        var tab = $(this).attr('tab');
                        loadTab($(panel), tab)
                    } else {
                        panel.style.maxHeight = panel.scrollHeight + "px";
                    }
                }
            });
        }

        function loadTab(panel, tab) {
            var url = '';
            var data = { product: '@Model.Product.Guid.ToString' }
                                if (tab == @CInt(DTOProduct.Tabs.coleccion) ) {
                url = '@Url.Action("Coleccion", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.distribuidores) ) {
                url = '@Url.Action("StoreLocator", "product")';
                data = { product: '@Model.Product.Guid.ToString', location: '@sLocationGuid' };
                var fullUrl = '@Model.Product.GetUrl(ContextHelper.Lang, DTOProduct.Tabs.distribuidores)';
                gtag_report_conversion(fullUrl);
            } else if (tab == @CInt(DTOProduct.Tabs.accesorios) ) {
                url = '@Url.Action("Accesorios", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.descargas) ) {
                url = '@Url.Action("Descargas", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.galeria) ) {
                url = '@Url.Action("Galeria", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.videos) ) {
                url = '@Url.Action("Videos", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.bloggerposts) ) {
                url = '@Url.Action("BloggerPosts", "product")';
            } else if (tab == @CInt(DTOProduct.Tabs.descripcion) ) {
                url = '@Url.Action("Description", "product")';
            }

            if (url != '') {
                $(panel).load(url, data, function (result) {
                    $(this).css("max-height", $(this).prop("scrollHeight"));
                });
            }
        }

        function openPanel(panel) {
            $(panel).style.maxHeight = panel.scrollHeight + "px";
        }

        function loadAccordionNeighbours(position) {
                url = '@Url.Action("FromGeoLocation", "product")';
                data = {
                        product: '@Model.Product.Guid.ToString',
latitud: position.coords.latitude,
                        longitud: position.coords.longitude
            }
            var panel = $('button.accordion_dropdown[tab="+ @CInt(DTOProduct.Tabs.distribuidores) + "]').nextElementSibling;
            $(panel).load(url, data, function (result) {
                $(this).css("max-height", $(this).prop("scrollHeight"));
            });

        }

        /*-------------- End Accordion -----------------------*/


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



        function logClickThrough(thisObj) {
            //var landingpage = $(this).data("landingpage");

            var landingpage = thisObj.data("landingpage");

            var url = '/wtbol/ClickThroughLog';
            var data = {landingpage: landingpage};

            var jqxhr = $.post(url, data, function (result) {
                var destination = result.url;
                location.href = destination;
            })
                .done(function () {
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    //$('.ProductSelection').html('<div class="Error">' + textStatus + '</div>');
                    $(event).html('<div class="Error">' + textStatus + '</div>');
                })
                .always(function () {
                    $('.Spinner.Loading').hide("fast");
                });

/*
        $.ajax({
            url: '/wtbol/ClickThroughLog',
data: {
                session: '@@Session("SessionId")',
landingpage: landingpage,
            },
type: 'POST',
dataType: "json",
            success: function (result) {
                if (result.success == true) {
                    var url = result.url;
                    location.href = url;
                }
            }
        });
        */
    }

    </script>
End Section

@Section Styles

    <link href="~/Media/Css/Accordion.css" rel="stylesheet" />
    <link href="~/Media/Css/Videos.css" rel="stylesheet" />

    <style>
        .RightAlign {
            text-align: right;
        }

        .StoreLocatorOnlineOffline {
            clear: both;
            margin-top: 15px;
        }

        .AreaDropdown select {
            width: 100%;
            border-width: 1px;
            border-color: #EAEAEA;
        }

        .StoreList {
            display: inline-block;
        }



        .Stores {
            max-height: 400px;
            overflow: auto;
        }

        .StoreListTitle {
            background-color: #EAEAEA;
            height: 20px;
            font-size: smaller;
            padding: 1px 0 2px 5px;
        }

        .Store {
            width: 100%;
            border-width: 0 1px 1px 1px;
            border-style: solid;
            border-color: #EAEAEA;
            margin: 0;
            padding: 10px;
            font-size: smaller;
        }

        .StoreNom {
            font-weight: 600;
        }

        .Store .Distance {
            margin: 0;
            padding: 0;
            text-align: right;
        }

        .BackToGeoLocator, .BackToNearestNeighbours {
            text-align: right;
            font-size: smaller;
            margin: 10px 5px;
        }

        .InStock {
            text-align: right;
            color: green;
        }

        a[href^="tel:"]:before {
            content: "\260e";
            margin-right: 0.5em;
        }

        @@media screen and (max-width:540px) {
            .BloggerPostsWrapper {
                display: none;
            }

            .StoreList {
                width: 100%;
            }

            .AreaDropdown select {
                height: 42px;
            }
        }

        @@media screen and (min-width:540px) {
            .StoreList {
                width: 280px;
                margin: 5px;
                vertical-align: top;
            }

            .AreaDropdown select {
            }
        }
    </style>
End Section

