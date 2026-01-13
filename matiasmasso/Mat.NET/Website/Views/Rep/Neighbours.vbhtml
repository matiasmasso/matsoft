@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    
    Dim sTitle As String = ContextHelper.Tradueix("Clientes más cercanos", "Clients propers", "Nearest neighbours")
    ViewBag.Title = "M+O | " & sTitle

End Code




<div id="NearestNeighbours" class="StoreList">

</div>

@Section Styles
    <link href="~/Media/Css/Videos.css" rel="stylesheet" />
    <style>
        .RightAlign {
            text-align: right;
        }
        .Turnover {
            text-align: right;
            color:red;
        }

        .StoreList {
            display: inline-block;
            width: 280px;
            margin: 5px;
            vertical-align: top;
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

        .InStock {
            text-align: right;
            color: green;
        }

        a[href^="tel:"]:before {
            content: "\260e";
            margin-right: 0.5em;
        }
    </style>
End Section



@Section Scripts

    <script>

    /* Geolocation ====================================================================== */

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(loadNearestNeighbours, errorHandler, { timeout: 10000 });
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
        var url = '@Url.Action("FromGeoLocation", "Rep")';
        $('#NearestNeighbours').load(
            url,
            {
                latitud: position.coords.latitude,
                longitud: position.coords.longitude
            },
            function () {
                $('.loading').hide();
            }
        );
    }


    function errorHandler(positionError) {
        loadGeoSelector();
        if (window.console) {
            console.log(positionError);
        }
        if (positionError.code === 1) {
            alert('debe autorizar el acceso a su ubicación para poder mostrar los puntos de venta más cercanos');
        }
    }


    </script>
End Section
