@ModelType DTOMem
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

    Dim sToday As String = Format(Today, "yyyy-MM-dd")
    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oJobDone As List(Of DTOMem) = FEB2.Mems.AllSync(exs, oUser:=oUser, onlyfromLast24H:=True)
    Dim oJobDoneRight As List(Of DTOMem) = oJobDone.Where(Function(x) x.UsrLog.UsrCreated.Guid.Equals(oUser.Guid)).ToList
End Code

<h1>@ViewBag.Title</h1>

<div class="RaportWrapper">

    <div class="asideleft">
        <section class="jobdoneleft">
            @Html.Partial("_RaportJobDone", oJobDone)
        </section>
        <section class="arxiuleft"></section>
    </div>

    @If Model.IsNew Then
        @<section Class="mainform">
            @Html.Partial("_RaportForm")
        </section>
    Else
        @<section Class="mainform">
            @Html.Partial("_RaportDisplay")
        </section>
    End If

    <section class="asideright">
        <section class="jobdoneright">
            @Html.Partial("_RaportJobDone", oJobDoneRight)
        </section>
        <section class="arxiuright"></section>
    </section>
</div>

<input type="hidden" id="viewBagCustomer" value="@ViewBag.customer" />


@Section Styles
    <style>
        textarea {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .ContentColumn {
            max-width: 100%;
            width: 100%;
        }
        .RaportWrapper {
            display: flex;
            flex-direction: row;
            column-gap: 15px;
            max-width: 100%;
            width: 100%;
        }


        .mainform {
            display: inline-block;
            vertical-align: top;
            margin: 0 0;
            min-width: 300px;
            max-width: 400px;
            flex-basis: auto; /* default value */
            flex-grow: 1;
        }

        .asideleft {
            display: inline-block;
            flex: 0 0 1fr;
            vertical-align: top;
            margin: 0 0;
            color: gray;
            padding-right: 10px;
        }

        .asideright {
            display: inline-block;
            flex: 0 0 1fr;
            vertical-align: top;
            margin: 0 0;
            color: gray;
            padding-left: 10px;
        }

        .jobdoneleft {
            margin-bottom:10px;
        }


        #CustomerSelection div select {
            width:100%;
        }
        .RaportWrapper textarea {
            width:100%;
        }
        .RaportWrapper .Fch {
            padding-top:10px;

        }
        .RaportWrapper .Fch input[type=date] {
            float:right;
        }
        .RaportWrapper textarea {
            margin-top:10px;
            height:100px;
        }
        .RaportWrapper .Submit {
            margin-top:10px;
            text-align:right;
        }

        .Focused {
            background-color:lightyellow;
        }

        .ArchiveEpg {
            margin-top:10px;
            text-align:right;
        }
        .ArchiveTxt {
            margin-top:5px;
        }


        @@media screen and (max-width:649px) {
            .RaportWrapper {
                max-width: 320px;
                margin: auto;
            }

            .asideleft {
                display: none;
            }

            .asideright {
                display: none;
            }
        }

        @@media screen and (min-width:650px) and (max-width:975px) {
            .RaportWrapper {
                max-width: 650px;
                margin: auto;
            }

            .asideright {
                display: none;
            }
        }

        @@media screen and (min-width:976px) {
            .RaportWrapper {
                width: 976px;
                margin: auto;
            }

            .jobdoneleft {
                display: none;
            }

            .arxiuright {
                display: none;
            }
        }

    </style>
End Section

@Section Scripts
<script>
        var $CS_country;
        var $CS_zona;
        var $CS_location;
        var $CS_customer;
        var CS_model;

        $(document).ready(function () {
            CS_loadCustomerSelection('#CustomerSelection', '/raports/atlas/');
        });

        $(document).on('CS_CustomerSelected', function (argument) {
            loadArxive();
            enableInput();
        });

        function toggleHighlight(select) {
            if (select.prop('selectedIndex') == 0)
                select.addClass("Focused");
            else
                select.removeClass("Focused");
        }

        $(document).on('change', '#CustomerSelection div:nth-child(1) select', function (e) {
            load_CS_Dropdown($CS_zona, CS_zonas());
            toggleHighlight($(this));
        });

        $(document).on('change', '#CustomerSelection div:nth-child(2) select', function (e) {
            load_CS_Dropdown($CS_location, CS_locations());
            toggleHighlight($(this));
        });

        $(document).on('change', '#CustomerSelection div:nth-child(3) select', function (e) {
            load_CS_Dropdown($CS_customer, CS_customers());
            toggleHighlight($(this));
        });

        $(document).on('change', '#CustomerSelection div:nth-child(4) select', function (e) {
            var argument = currentCustomer();
            toggleHighlight($(this));
            $(document).trigger('CS_CustomerSelected', argument);
        });

        $(document).on('click', '#SubmitButton', function (e) {
            $('.loading').show();
            $.ajax({
                url: '/raports/update',
                data: {
                    Customer: currentCustomer(),
                    Text: $('textarea').val(),
                    Fch: $('.RaportWrapper .Fch input').val()
                },
                type: 'POST',
                dataType: "json",
                success: function (result) {
                    $('.loading').hide();
                    if (result.resultcod == 1) {
                        loadJobDone();
                        load_CS_Dropdown($CS_customer, CS_customers());
                        disableInput();
                        $('#viewBagCustomer').val('');
                    }
                    else
                        alert(result.resultmsg);

                }
            });
        });

        function disableInput() {
            $('.Fch input').attr("disabled", "disabled");
            $('textarea').attr("disabled", "disabled");
            $('#SubmitButton').hide();
        }

        function enableInput() {
            $('.Fch input').removeAttr("disabled");
            $('textarea').removeAttr("disabled");
            $('textarea').val("");
            $('#SubmitButton').show();
        }




        function CS_loadCustomerSelection(div, url, guid) {
            $CS_country = $(div + ' div:nth-child(1) select')
            $CS_zona = $(div + ' div:nth-child(2) select')
            $CS_location = $(div + ' div:nth-child(3) select')
            $CS_customer = $(div + ' div:nth-child(4) select')

            load_CS_model(url, guid);
        
        };

        function load_CS_model(url, guid) {
            $.getJSON(url, function (result) {
                CS_model = result;

                //if no customer predefined load selectors
                if (CS_customers().length == 1) {
                    load_CS_Dropdowns();
                };
            });
        };

        function load_CS_Dropdowns() {
            load_CS_Dropdown($CS_country, CS_countries());
            if (CS_countries().length == 1) {
                load_CS_Dropdown($CS_zona, CS_zonas());
                if (CS_zonas().length == 1) {
                    load_CS_Dropdown($CS_location, CS_locations());
                    if (CS_locations().length == 1) {
                        load_CS_Dropdown($CS_customer, CS_customers());
                    };
                };
            };
        };

        function load_CS_Dropdown(select, items) {
            select.find('option:gt(0)').remove();
            $.each(items, function (key, value) {
                $('<option>').val(value.Guid).text(value.nom).appendTo(select);
            });

            if (items.length == 1) {
                select.prop('selectedIndex', 1);
                if (select == $CS_customer) {
                    select.removeClass("Focused");
                    loadArxive();
                    enableInput();
                }
                else if (select == $CS_zona) {
                    select.removeClass("Focused");
                    load_CS_Dropdown($CS_location, CS_locations());
                }
                else if (select == $CS_location) {
                    select.removeClass("Focused");
                    load_CS_Dropdown($CS_customer, CS_customers());
                }
                else
                    select.hide();
            } else {
                select.show();
                select.addClass("Focused");
            }

        };



        function CS_countries() {
            return CS_model;
        };

        function CS_zonas() {
            var country = $CS_country.val();
            var retval;
            $.each(CS_countries(), function (key, value) {
                if (value.Guid == country) {
                    retval = value.Items;
                    return false;
                }
            });
            return retval;
        };

        function CS_locations() {
            var zona = $CS_zona.val();
            var retval;
            $.each(CS_zonas(), function (key, value) {
                if (value.Guid == zona) {
                    retval = value.Items;
                    return false;
                }
            });
            return retval;
        };

        function CS_customers() {
            var location = $CS_location.val();
            var retval;
            $.each(CS_locations(), function (key, value) {
                if (value.Guid == location) {
                    retval = value.Items;
                    return false;
                }
            });
            return retval;
        };


        function loadArxive() {
            var url = "/raports/archive";
            var data = { customer: currentCustomer() };
            $('.arxiuleft, .arxiuright').load(url, data);
        }

        function loadJobDone() {
            var url = "/raports/jobdone";
            $('.jobdoneleft, .jobdoneright').load(url);
        }

        function currentCustomer() {
            return $CS_customer.val();
        }

</script>
End Section