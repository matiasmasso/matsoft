@ModelType List(Of DTODelivery)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    Dim exs As New List(Of Exception)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oCentros = FEB.Deliveries.CentrosSync(exs, oUser)
End Code



<h1>@ViewBag.Title</h1>

<div class="Store">
    <select>
        <option value="@System.Guid.Empty.ToString">@ContextHelper.Tradueix("Todos los centros", "Tots els centres", "All stores")</option>
        @For Each item As DTOCustomer In oCentros
            @<option value="@item.Guid.ToString">@item.Nom</option>
        Next
    </select>
</div>

@If Model.Count = 0 Then
    @<div>
        @ContextHelper.Tradueix("No nos constan albaranes registrados", "No ens consten albarans registrats", "No deliveries have been logged")
    </div>
Else
    @<div class="StoreContainer">
        @Html.Partial("Deliveries_", Model)
    </div>
End If


@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 600px;
        }

        .Store {
            text-align: right;
        }

        .Tracking {
            display: inline-block;
            border: 1px solid gray;
            padding: 4px 7px 2px 4px;
            border-radius: 4px;
            background: #73AD21;
            color: white;
        }

        .Row.Closed {
            display: none;
        }

        #ShowMore {
            text-align: right;
        }
    </style>
End Section

@Section Scripts
    <script>
        $(document).on('change', '.Store select', function (event) {
            $('.StoreContainer').load('/Deliveries/StoreChanged', { guid: $(this).val() });
        });

        $(document).on('click', '#ShowMore a', function (event) {
            event.preventDefault();
            $('.Row').removeClass('Closed');
        });

    </script>
End Section





