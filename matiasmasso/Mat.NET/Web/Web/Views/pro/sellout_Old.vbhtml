@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oProveidor As New MaxiSrvr.Proveidor(oWebSession.Contact.Guid)
    Dim oStat As MaxiSrvr.Stat = MaxiSrvr.BLL_Stat.SellOut(oProveidor)
    MaxiSrvr.StatLoader.Load(oStat)
End Code

<style>
    .StocksContainer {
        width: 570px;
        margin: 0 auto;
        padding: 0 0 0 0;
        font-size:0.7em;
    }

    .StocksBrandBox {
        border: 1px solid darkgray;
        margin: 5px 0 20px 0;
    }

    .StocksBrandHeader {
        display: block;
        margin: 0 0 0 0;
        font-size: 1.4em;
        font-weight: 900;
        width: 100%;
        padding: 5px;
        color: white;
        background-color: navy;
    }

        .StocksBrandHeader a {
            text-decoration: none;
            color: white;
        }

    .StocksCategoryBox {
        margin: 30px 0px 10px 0;
        width: 100%;
    }

    .StocksCategoryHeader {
        display: block;
        padding: 5px 5px 5px 45px;
        font-weight: 900;
        color: white;
        background-color: darkgray;
        width: 100%;
    }

        .StocksCategoryHeader a {
            text-decoration: none;
            color: white;
        }



    .StocksSku a {
        text-decoration: none;
        color: #223ba7;
    }

        .StocksSku a:hover {
            color: red;
        }

    .statrow {
        position: relative;
        display: block;
        text-align: left;
        left: 0px;
        height: 20px;
        vertical-align: top;
    }

    .rowlevel0 {
        font-weight:bold;
    }

    .rowlevel1 {
        background-color: #cdfbf8;
    }

    .statwrapheader {
        position: relative;
        display: block;
        text-align: left;
        left: 0px;
        height: 20px;
        vertical-align: top;
        font-weight:bold;
        /*background-color: darkgray;*/
    }
        .statwrapheader a {
        color: white;
        }
    .statnom {
        display: inline-block;
        width: 180px;
        padding-left:5px;
    }
    .statmes {
        display: inline-block;
        width: 27px;
        text-align: right;
        border: 1px solid #d5d5d5;
    }
    .statmesheader {
        display: inline-block;
        width: 27px;
        text-align: right;
        border: 1px solid #d5d5d5;
    }

    .pageTitle {
        font-size:2em;
        margin-top:0;
        padding-top:0;
    }

    .filterbox  {
        width:200px;
    }

    .downloadLink  {
        text-align:right;
        font-size:1em;
    }

    .downloadLink a:hover {
        color:red;
    }

</style>



<div class="StocksContainer">

    <h1 class="pageTitle">
        @oWebSession.Tradueix("Pedidos de clientes", "Comandes de clients", "Sell-out data per product")
    </h1>

    <p>
        @Html.Raw(oWebSession.Tradueix( _
       "Las cifras se refieren a unidades vendidas en el mes en que el que el cliente cursó el pedido, tanto si se le ha servido como si no.<br/>Estas cifras pueden verse afectadas por potenciales devoluciones, en cuyo caso se restan del pedido original.", _
       "Les xifres es refereixen a unitas venudes el mes que es va rebre la comanda, tant si es va servir llavors com si no.<br/>Les xifres poden variar en el futur per potencials devolucions, que serien deduides del mes en que van ser demanades.", _
       "Figures refer to units ordered by customers, in the month the order was logged, regardless if delivered or not.<br/>Potential returns may affect them in the future as they are deducted from the original order."))
    </p>


    <div class="filterbox" hidden="hidden">
        <input type="search" id="search-text" />
        <a href="#">
            <img id="search-button" src="~/Media/Img/Ico/magnifying-glass.jpg" />
        </a>
    </div>

    <div class="downloadLink">
        <a href="@MaxiSrvr.BLL_Stat.SellOutUrl">
            @oWebSession.Tradueix("descargar fichero csv", "descarregar fitxer csv", "download csv file")
        </a>
    </div>

    <div class="StocksCategoryBox truncate">

        <span class="statwrapheader">
            <span class="statnom truncate">

            </span>
            <span class="statmesheader">
               total
            </span>
            @For m As Integer = 1 To 12
                @<span class="statmesheader">
                    @oWebSession.Lang.MesAbr(m)
                </span>
            Next
        </span>

        @For Each oItem As MaxiSrvr.StatItem In oStat.Items
            @<span class='statrow @Html.Raw(IIf(oItem.Level = 0, "rowlevel0", IIf(oItem.Level = 1, "rowlevel1", "")))'>
                <span class="statnom truncate" data-level="@oItem.Level">
                    @For i As Integer = 0 To oItem.Level
                        @Html.Raw("&nbsp;&nbsp;&nbsp;&nbsp;")
                    Next
                    @If oItem.HasChildren Then
                        @<a href="#" class="drilldown" data-rowindex="@oItem.Index.ToString">
                            <img src="~/Media/Img/Ico/minus.gif" />
                        </a>
                    End If
                        @oItem.Concept
                </span>

                @If oItem.Level > 0 Then
                    @<span class="statmes">
                            @Html.Raw(Format(oItem.Tot,"#,###"))
                    </span>
                For m As Integer = 0 To 11
                    @<span class="statmes">
                        @If oItem.Values(m) <> 0 Then
                            @Html.Raw(Format(oItem.Values(m), "#,###"))
                        Else
                            @Html.Raw("&nbsp;")
                        End If
                    </span>
                Next
                End if
            </span>

        Next
    </div>


        
    <div class="downloadLink">
        <a href="@MaxiSrvr.BLL_Stat.SellOutUrl">
            @oWebSession.Tradueix("descargar fichero csv", "descarregar fitxer csv", "download csv file")
        </a>
    </div>

</div>

@Section Scripts
<script>
    $("div.filterbox > a").click(function (event) {
        alert('Hit!');
        var searchtext = $("#search-text").val();
        var rows = $(".statrow span.statnom");
        var isChild = false;
        var parentLevel = 0;
        $.each(rows, function (index, value) {
            var level = value.getAttribute('data-level');
            var html = value.innerHTML;
            alert(html + ' ' + level);
            if (isChild == true) {
                //var childLevel = $(this).data('level');
                //alert($(this).text() + ' Childlevel:' + childLevel + ' Parentlevel:' + parentlevel);
                alert(html + ' Childlevel:' + level + ' Parentlevel:' + parentlevel);
                if (level >= parentlevel) {
                    isChild = false;
                }
            }

            if (isChild == false) {
                //var text = $(this).text();
                if (html.indexOf(searchtext) >= 0) {
                    alert(html + ' match');
                    //parentLevel = $(this).data('level');
                    parentLevel = level;
                    isChild = true;
                    alert(html + ' matched details level:' + parentlevel);

                    //for (i = index - 1; i >= 0; i--) {
                    //var childLevel = $(this).data('level');
                    //}

                } else {
                    alert(html + ' NOT match');
                    //value.parent().hide();
                    //$(this).parent().hide();
                    //$(this).parent().style.display = 'none';
                }
            }
        });
    })



    $(".drilldown").click(function (event) {
        var filtro = $("#search-text").val();
        var rowindex = $(this).data('rowindex');
        alert(filtro + ' ' + rowindex);
    })
</script>

End Section
