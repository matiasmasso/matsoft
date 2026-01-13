@ModelType DTO.DTOUser
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim sTitle As String = "Stocks Advansafix II pendiente de confirmar"
    ViewData("Title") = sTitle & " | M+O"
    Dim items As List(Of DTO.DTOQuizAdvansafix) = BLL.BLLQuizAdvansafixs.PendingQuizList(oWebSession.User)
End Code




<div class="pagewrapper">
    <h2>@sTitle</h2>

    @If items.Count = 0 Then
        @<div class="Warn">
            <p>
                @Html.Raw("No nos constan clientes pendientes de respuesta")
            </p><p>
                @Html.Raw("No queden cliens pendents de resposta")
            </p><p>
                @Html.Raw("No customers left with missing answer")
            </p>
        </div>

    Else
        @<div Class="Grid">

            <div Class="RowHdr">
                <div Class="CellTxt CellZona">
                    @oWebSession.Tradueix("Zona", "Zona", "Area")
                </div>
                <div Class="CellTxt CellCustomer">
                    @oWebSession.Tradueix("Cliente", "Client", "Customer")
                </div>
                <div Class="CellNum">
                    Adv.II
                </div>
                <div Class="CellNum">
                    Adv.II SICT
                </div>
            </div>


            @For Each item As DTO.DTOQuizAdvansafix In items
                @<div class="Row" data-url="@BLL.BLLContact.Url(item.Customer)">
                     <div Class="CellTxt CellZona">
                         @item.Zona.Nom
                     </div>
                     <div Class="CellTxt CellCustomer">
                         @item.Customer.FullNom
                     </div>
                     <div Class="CellNum">
                         @item.NoSICTPurchased
                     </div>
                     <div Class="CellNum">
                         @item.SICTPurchased
                     </div>
                </div>
            Next
        </div>
    End If

</div>



@Section Styles
    <style>
        .pagewrapper {
            min-width:320px;
            max-width:700px;
        }
        .CellZona {
            width:80px;
            min-width:80px;
            max-width:100px;
        }
        .CellZona {
            min-width:200px;
            max-width:400px;
        }
        </style>
End Section
