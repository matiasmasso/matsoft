@ModelType DTORep
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Pedido de Representante", "Comanda de Representant", "Sales Order")
    ViewBag.Title = "M+O | " & sTitle

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim sToday As String = Format(Today, "yyyy-MM-dd")

End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>

    <div class="DataCollection">

        <section class="CustomerProductSelection" >
            <section class="CustomerSelection">
                <div> 
                    <select id="Country" disabled='disabled'>
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar país", "Sel·leccionar país", "Select country")</option>
                    </select>
                </div>
                <div>
                    <select id="Zona" disabled='disabled'>
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar zona", "Sel·leccionar zona", "Select area")</option>
                    </select>
                </div>
                <div>
                    <select id="Location" disabled='disabled'>
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar población", "Sel·leccionar població", "Select location")</option>
                    </select>
                </div>
                <div>
                    <select id="Customer" disabled='disabled'>
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar cliente", "Sel·leccionar client", "Select customer")</option>
                    </select>
                </div>
            </section>

            <section Class="CustomerSelected" hidden="hidden">
                <a href = "#" target="_blank">

                </a>
            </section>

            <a href = "#" Class="AdvancedOptions">@Mvc.ContextHelper.Tradueix("Opciones Avanzadas", "Opcions Avançades", "Advanced Options")</a> 
            <table class="AdvancedOptions" hidden="hidden">
                
                <tr>
                    <td>@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</td>
                    <td align="right"><input type="text" id="Concept" /></td>
                </tr>
                <tr>
                    <td>@Mvc.ContextHelper.Tradueix("Observaciones", "Observacions", "Comments")</td>
                    <td><input type="text" id="Obs" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="checkbox" id="TotJunt" />
                    @Mvc.ContextHelper.Tradueix("Servir todo junto", "Servir tot junt", "Ship all together")</td>
                </tr>
                <tr>
                    <td colspan="2">
                    <table width="100">
                        <tr>
                            <td align="left" wrap="nowrap">
                                <input type="checkbox" id="CheckboxFchMin" />
                                @Mvc.ContextHelper.Tradueix("Fecha Servicio", "Data Servei", "Shipping Date")
                            </td>
                            <td align="right"><input type="date" id="FchMin" min="@sToday" hidden="hidden" value="@sToday" /></td>
                        </tr>
                    </table>
                    
                    </td>
                </tr>
             </table>


            <section class="ProductSelection">
                <div>
                    <select id="Brand" disabled="disabled">
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar marca comercial", "Sel·leccionar marca comercial", "Select brand")</option>
                    </select>
                </div>
                <div>
                    <select id="Category" disabled="disabled">
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar categoría", "Sel·leccionar categoría", "Select category")</option>
                    </select>
                </div>
                <div>
                    <select id="Sku" disabled="disabled">
                        <option value="">@Mvc.ContextHelper.Tradueix("Seleccionar producto", "Sel·leccionar producte", "Select product")</option>
                    </select>
                </div>
            </section>
        </section>

        <section class="QtySelection">
            <div class="SkuThumbnail">
                <a href="#" target="_blank">
                    <img />
                </a>
            </div>

            <div class="SkuDetails">
                <input type="number" id="Qty" value="" />
                <input type="button" id="ButtonAdd" value='@Mvc.ContextHelper.Tradueix("Añadir", "Afegir", "Add")' disabled="disabled" />
            </div>

        </section>

        <section class="Grid">
            <div class="RowHdr">
                <div class="CellNum">@Mvc.ContextHelper.Tradueix("Cant", "Quant", "Quant")</div>
                <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
                <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")</div>
                <div class="CellDto" hidden="hidden">@Mvc.ContextHelper.Tradueix("Dto", "Dte", "Dct")</div>
                <div class="CellIco">&nbsp;</div>
            </div>
        </section>

        <section class="SubmitButtons">
            <span id="CheckBoxMail" hidden="hidden">
                <input type="checkbox" />
                @Mvc.ContextHelper.Tradueix("Enviar email de confirmación", "Enviar email de confirmació", "Send confirmation email")
            </span>
            <input type="button" id="ButtonCancel" value='@Mvc.ContextHelper.Tradueix("Cancelar", "Cancel·lar", "Cancel")' />
            <input type="button" id="ButtonSubmit" value='@Mvc.ContextHelper.Tradueix("Enviar", "Enviar", "Submit", "Aceitar")' disabled="disabled" />
        </section>
    </div>

    <div class="Thanks" hidden="hidden">

    </div>

    <input type="hidden" id="Rep" value="@Model.Guid.ToString" />
    <input type="hidden" id="viewBagCustomer" value="@ViewBag.customer" />
</div>

@Section Scripts
    <script>
        var basket;
        var basketline;
        var atlas;
        var catalogue;

        $(document).ready(function () {
            loadBrands();
            if (viewBagCustomer() == null) {
                loadCountries();
            } else {
                loadCustomer();
            }
        }); 

        $(document).on('change', '#Country', function (event) {
            loadZonas();
        });

        $(document).on('change', '#Zona', function (event) {
            loadLocations();
        });

        $(document).on('change', '#Location', function (event) {
            loadCustomers()
                    });

        $(document).on('change', '#Customer', function (event) {
            loadCustomer();
        });

        $(document).on('change', '#Brand', function (event) {
            loadCategories();
        });

        $(document).on('change', '#Category', function (event) {
            loadSkus();
        });

        $(document).on('change', '#Sku', function (event) {
            loadSku();
        });

        function loadCountries() {
            $('.loading').show();
            var jqxhr = $.getJSON(
                '/RepBasket/Atlas',
                { userguid: '@oUser.Guid.ToString' },
                function (result) {
                    atlas = result;
                })
              .done(function () {
                  $('#Country').children('option:not(:first)').remove();
                  $.each(atlas, function (key, item) {
                      $('#Country').append('<option value=' + item.guid + '>' + item.nom + '</option>');
                  });

                  if (atlas.length == 1) {
                      $('#Country > option:eq(1)').prop('selected', true);
                      loadZonas();
                  } else {
                      $('#Country').addClass("Focused");
                      $('#Country').prop("disabled", false);
                  }
              })
              .fail(function (xhr, status, error) {
                  alert(status + '; ' + error);
              })
              .always(function () {
                  $('.loading').hide();
              });
        }

    function loadZonas() {
        var country = currentCountry();

        $('#Zona').children('option:not(:first)').remove();
        $.each(country.zonas, function (key, item) {
            $('#Zona').append('<option value=' + item.guid + '>' + item.nom + '</option>');
        });

        if (country.zonas.length == 1) {
            $('#Zona > option:eq(1)').prop('selected', true);
            loadLocations()
        } else {
            $('#Country').removeClass("Focused");
            $('#Zona').addClass("Focused");

            $('#Zona').prop("disabled", false);
            $('#Location').prop("disabled", true);
            $('#Customer').prop("disabled", true);
            $('#Brand').prop("disabled", true);
            $('#Category').prop("disabled", true);
            $('#Sku').prop("disabled", true);
        }
    }

    function loadLocations() {
        var zona = currentZona();

        $('#Location').children('option:not(:first)').remove();
        $.each(zona.locations, function (key, item) {
            $('#Location').append('<option value=' + item.guid + '>' + item.nom + '</option>');
        });

        if (zona.locations.length == 1) {
            $('#Location > option:eq(1)').prop('selected', true);
            loadCustomers()
        } else {
            $('#Zona').removeClass("Focused");
            $('#Customer').removeClass("Focused");
            $('#Location').addClass("Focused");

            $('#Location').prop("disabled", false);
            $('#Customer').prop("disabled", true);
            $('#Brand').prop("disabled", true);
            $('#Category').prop("disabled", true);
            $('#Sku').prop("disabled", true);
        }
    }


    function loadCustomers() {
        var location = currentLocation();

        $('#Customer').children('option:not(:first)').remove();
        $.each(location.contacts, function (key, item) {
            $('#Customer').append('<option value=' + item.guid + '>' + item.nom + '</option>');
        });

        $('#Zona').removeClass("Focused");
        $('#Location').removeClass("Focused");
        $('#Customer').addClass("Focused");

        $('#Customer').prop("disabled", false);
        $('#Brand').prop("disabled", true);
        $('#Category').prop("disabled", true);
        $('#Sku').prop("disabled", true);
    }

    function loadCustomer() {
        $('.loading').show();

        var customer = currentCustomer();        
        basket = { customer: customer.guid, lines:   [] };
        var basketData = JSON.stringify(basket);
        $.getJSON('@Url.Action("CustomerSelected")', { basket: basketData }, function (result) {
            $('.loading').hide();
            basket = result.basket;
            $('#Concept').val(basket.concept);
            $('#Customer').removeClass("Focused");
            if (basket.mailConfirmation == true) {
                $('#CheckBoxMail').show();
            } else {
                $('#CheckBoxMail').hide();
            }
            $('#CheckBoxMail input[type=checkbox]').prop('checked', basket.mailConfirmation);
            $('.CustomerSelection').hide();
            $('.CustomerSelected').show();
            $('.CustomerSelected a').attr('href', basket.customerUrl);
            $.each(basket.addressLines, function (key, item) {
                $('.CustomerSelected a').append(item + '<br/>');
            });

            if (catalogue.length == 1) {
                $('#Brand > option:eq(1)').prop('selected', true);
                loadCategories();
                $('#Brand').removeClass("Focused");
                $('#Category').addClass("Focused");
                $('#Sku').removeClass("Focused");
                $('#Brand').prop("disabled", true);
                $('#Category').prop("disabled", false);
                $('#Sku').prop("disabled", true);
            } else {
                $('#Brand').addClass("Focused");
                $('#Brand').prop("disabled", false);
                $('#Category').prop("disabled", true);
                $('#Sku').prop("disabled", true);
            }

        });
    }


    function loadBrands() {
        $('.loading').show();
        var jqxhr = $.getJSON(
            '/RepBasket/Catalogue',
            { userguid: '@oUser.Guid.ToString' },
            function (result) {
                catalogue = result;
            })
          .done(function () {
              $('#Brand').children('option:not(:first)').remove();
              $.each(catalogue, function (key, item) {
                  $('#Brand').append('<option value=' + item.guid + '>' + item.nom + '</option>');
              });

              if (catalogue.length == 1) {
                  $('#Brand > option:eq(1)').prop('selected', true);
                  loadCategories();
                  $('#Brand').removeClass("Focused");
                  $('#Category').addClass("Focused");
                  $('#Sku').removeClass("Focused");
                  $('#Brand').prop("disabled", true);
                  $('#Category').prop("disabled", false);
                  $('#Sku').prop("disabled", true);
              }
          })
          .fail(function (xhr, status, error) {
              alert(status + '; ' + error);
          })
          .always(function () {
              $('.loading').hide();
          });
    }

        function loadCategories() {
            var brand = currentBrand();

            $('#Category').children('option:not(:first)').remove();
            $('#Sku').children('option:not(:first)').remove();
            $.each(brand.Categories, function (key, item) {
                $('#Category').append('<option value=' + item.guid + '>' + item.nom + '</option>');
            });

            if (brand.Categories.length == 1) {
                $('#Category > option:eq(1)').prop('selected', true);
                loadSkus()
            } else {
                $('#Brand').removeClass("Focused");
                $('#Sku').removeClass("Focused");
                $('#Category').addClass("Focused");

                $('#Category').prop("disabled", false);
                $('#Sku').prop("disabled", true);
            }
        }

        function loadSkus() {
            var category = currentCategory();

            $('#Sku').children('option:not(:first)').remove();
            $.each(category.Skus, function (key, item) {
                $('#Sku').append('<option value=' + item.guid + '>' + item.nom + '</option>');
            });

            $('#Category').removeClass("Focused");
            $('#Sku').addClass("Focused");

            $('#Sku').prop("disabled", false);
        }

        function loadSku() {
            $('.loading').show();
            $('.SkuThumbnail a').attr("href", '');
            $('.SkuThumbnail').addClass("Loading");
            var customer = currentCustomer();
            var sku = currentSku();
            basketline = { sku: sku.guid };
            $.getJSON('/RepBasket/Sku', { contact: customer.guid, id: basketline.sku }, function (result) {
                $('.loading').hide();
                basketline.priceFormatted = result.priceFormatted;
                basketline.price = result.price;
                basketline.dto = result.dto;
                basketline.nom = result.nom;
                $('.SkuThumbnail').removeClass("Loading");
                $('.SkuThumbnail a').removeClass("disabled");
                $('.SkuThumbnail a').attr("href", result.url);
                $('.SkuThumbnail a img').attr("src", result.thumbnail);
                $('#ButtonAdd').prop("disabled", false);
                $('#Qty').val(result.moq);
                $('#Qty').addClass("Focused");
                $('#Brand').removeClass("Focused");
                $('#Category').removeClass("Focused");
                $('#Sku').removeClass("Focused");
            });
        }


        function currentCountry() {
            var guid = $('#Country').val();
            var countries = $.grep(atlas, function (e) { return e.guid == guid; });
            var retval = countries[0];
            return retval
        }

        function currentZona() {
            var guid = $('#Zona').val();
            var country = currentCountry();
            var zonas = $.grep(country.zonas, function (e) { return e.guid == guid; });
            var retval = zonas[0];
            return retval
        }

        function currentLocation() {
            var guid = $('#Location').val();
            var zona = currentZona();
            var locations = $.grep(zona.locations, function (e) { return e.guid == guid; });
            var retval = locations[0];
            return retval
        }

        function currentCustomer() {
            var retval;
            if (viewBagCustomer() == null) {
                var guid = $('#Customer').val();
                var location = currentLocation();
                var customers = $.grep(location.contacts, function (e) { return e.guid == guid; });
                retval = customers[0];
            } else {
                retval = viewBagCustomer();
            }

            return retval
        }

        function viewBagCustomer() {
            var guid = $('#viewBagCustomer').val();
            var retval
            if (guid != '') {
                retval = { guid: guid };
            }
            return retval
        }

        function currentBrand() {
            var guid = $('#Brand').val();
            var brands = $.grep(catalogue, function (e) { return e.guid == guid; });
            var retval = brands[0];
            return retval
        }

        function currentCategory() {
            var guid = $('#Category').val();
            var brand = currentBrand();
            var categories = $.grep(brand.Categories, function (e) { return e.guid == guid; });
            var retval = categories[0];
            return retval
        }

        function currentSku() {
            var guid = $('#Sku').val();
            var category = currentCategory();
            var skus = $.grep(category.Skus, function (e) { return e.guid == guid; });
            var retval = skus[0];
            return retval
        }

        $(document).on('click', "a.AdvancedOptions", function (event) {
        $('table.AdvancedOptions').toggle();
    });

    $(document).on('click', '#CheckboxFchMin', function (event) {
        $('#FchMin').toggle();
    });






    $(document).on('click', '#ButtonAdd', function (event) {
        $('.loading').show();
        $(this).prop("disabled", true);
        basketline.qty = $('#Qty').val();
        var basketData = JSON.stringify(basket);
        var basketlineData = JSON.stringify(basketline);

        $.ajax({
            url: '@Url.Action("RequestToAddRow", "RepBasket")',
                data: { basket: basketData, basketline:basketlineData },
                type: 'POST',
                dataType: "json",
                success: function (result) {
                    $('.loading').hide();
                    if (result.errors == 0) {
                        basket = result.basket;
                        redrawBasket();
                        $('#ButtonSubmit').prop("disabled", false);
                        $('.SkuThumbnail a').addClass("disabled");
                        $('.SkuThumbnail a img').attr("src", "");
                        $('#Qty').val('');
                        $('#Qty').removeClass("Focused");
                        $('#Brand').addClass("Focused");
                        $('#Category').addClass("Focused");
                        $('#Sku > option:eq(0)').prop('selected', true);
                        $('#Sku').addClass("Focused");
                    } else {
                        alert(result.errors);
                    }
                }

            })

        });

        $(document).on('click', '.SkuThumbnail a', function (event) {
            if ($('.SkuThumbnail a').hasClass("disabled"))
                event.preventDefault();
        });


        $(document).on('click', '.CellKo', function (event) {
            $('.loading').show();
            var index = $(this).closest(".Row").index() - 1
            basketline.qty = $('#Qty').val();
            var basketData = JSON.stringify(basket);
            var basketlineData = JSON.stringify(basketline);

            $.ajax({
                url: '@Url.Action("RequestToDelRow", "RepBasket")',
                data: { basket: basketData, index: index },
                type: 'POST',
                dataType: "json",
                success: function (result) {
                    $('.loading').hide();
                    if (result.errors == 0) {
                        basket = result.basket;
                        redrawBasket();
                        if (basket.lines.length == 0)
                            $('#ButtonSubmit').prop("disabled", true);
                        else
                            $('#ButtonSubmit').prop("disabled", false);
                    } else {
                        alert(result.errors);
                    }
                }
            })

            /*
            $.getJSON('/RepBasket/RequestToDelRow', { basket: basketData, index: index }, function (result) {
                if (result.errors == 0) {
                    basket = result.basket;
                    redrawBasket();
                    if (basket.lines.length == 0)
                        $('#ButtonSubmit').prop("disabled", true);
                    else
                        $('#ButtonSubmit').prop("disabled", false);
                } else {
                    alert(result.errors);
                }
            });
            */
        });

        function redrawBasket() {
            var discountExists;
            $('section.Grid .Row').remove();
            $.each(basket.lines, function (key, item) {
                $('section.Grid').append(rowHtml(item));
                if (item.dto != 0) discountExists = true;
            });

            if (discountExists)
                $('.CellDto').show();
            else
                $('.CellDto').hide();
        }

        function rowHtml(item) {
            var retval = '<div class="Row">';
            retval += '<div class="CellNum">';
            retval += item.qty;
            retval += '</div>';
            retval += '<div class="CellTxt">';
            retval += item.nom;
            retval += '</div>';
            retval += '<div class="CellAmt">';
            retval += item.priceFormatted;
            retval += '</div>';
            retval += '<div class="CellDto">';
            if (item.dto!=0)  {
                retval += item.dto+'%';
            }
            retval += '</div>';
            retval += '<div class="CellKo">';
            retval += '</div>';
            retval += '</div>';
            return (retval);
        }

        $(document).on('click', '#ButtonSubmit', function (event) {
            $('.loading').show();
            basket.totjunt = $('#TotJunt').is(':checked');
            basket.concept = $('#Concept').val();
            basket.obs = $('#Obs').val();
            if ($('#CheckboxFchMin').is(':checked'))
                basket.fchMin = $('#FchMin').val();
            if ($('#CheckBoxMail input[type=checkbox]').is(':checked'))
                basket.mailConfirmation = true;
            var data = JSON.stringify(basket);

            $.ajax({
                url: '@Url.Action("Update", "RepBasket")',
            data: { data: data },
            type: 'POST',
            dataType: "json",
            success: function (result) {
                $('.loading').hide();
                if (result.id == 0) {
                    alert('error: ' + result.message);
                } else {
                    $('.DataCollection').hide();
                    $('.Thanks').show();
                    $(".Thanks").load('@Url.Action("Thanks")');
                    $.getJSON('MailConfirmation', { orderguid: result.guid });
                }
            }
        })
    });

    $(document).on('click', '#ButtonCancel', function (event) {
        window.location = '@Url.Action("Index")';
    });


    $(document).on('click', '#RequestForNewOrder input[type=button]', function (event) {
        window.location = '@Url.Action("Index")';
    })



    </script>
End Section

@Section Styles
    <style>
        table {
            border-collapse:collapse;   
        }
        .pagewrapper {
            max-width:650px;
            margin:auto;
        }
        .CustomerProductSelection select {
            width:100%;
        }

        .CustomerProductSelection {
            display:inline-block;
            width:300px;
        }
        .CustomerSelection {
            margin-bottom:20px;
        }
        .CustomerSelected {
            margin-bottom:20px;
        }

        a.AdvancedOptions {
            margin-bottom:10px;
        }
        #Concept {
            width:100%;
        }
        #TotJunt {
            text-wrap:none;
        }
        #FchMin {
        }
        #Obs {
            width:100%;
        }
        .ProductSelection {
            margin-top:20px;
            margin-bottom:20px;
        }
        .Focused {
            background-color:lightyellow;
        }

        .QtySelection {
            display:inline-block;
            width:150px;
            vertical-align:bottom;
            float:right;
            margin-bottom:20px;
        }

        section.Grid {
            width:100%;
            margin-top:20px;
        }

        .SkuThumbnail {
            height:172px;
            border:1px solid gray;
            vertical-align:top;
        }

        .SkuDetails {
            width:150px;
        }
        .SkuDetails div {
            margin-bottom:20px;
        }
        .SkuDetails div span {
            vertical-align:middle;
        }
        #Qty {
            width:60px;
            text-align:right;
            margin-top:5px;
        }
        #ButtonAdd {
            float:right;
            margin-top:5px;
            width:85px;
        }

        .SubmitButtons {
            text-align:right;
            margin-top:20px;
        }

            .SubmitButtons input[type=button] {
                width: 85px;
            }
            .SubmitButtons span {
                text-align: left;
                margin-right: 20px;
            }

        .Loading {
            background-image: url('/Media/Img/animated/ajax-loader_32x32.gif');
            background-repeat: no-repeat;
            background-position: center;
            cursor:wait;
        }

        #RequestForNewOrder {
            text-align:right;
        }

    </style>
End Section