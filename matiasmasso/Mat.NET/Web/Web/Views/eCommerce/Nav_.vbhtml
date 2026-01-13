@ModelType DTO.Defaults.eComPages

@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oUser As DTO.DTOUser = oWebSession.User
    Dim oBasket As DTO.DTOBasket = BLL.BLLBasket.FindLastorNew(DTO.DTOBasket.Sites.Thorley, oUser)
    Dim sLastSegment As String = Request.Url.Segments.Last
    
End Code

    <ul>
        <li>
            @If oBasket.Items.Count = 0 Then
                @<a>
                    Cesta vacía
                </a>
            ElseIf oBasket.Items.Count = 1 Then
                @<a>@Html.Raw("tiene un producto en la cesta por importe de " & BLL.BLLAmt.CurFormat( BLL.BLLBasket.Total(oBasket)))</a>
            Else
                @<a>@Html.Raw("tiene " & oBasket.Items.Count.ToString & " productos en la cesta por importe de " & BLL.BLLAmt.CurFormat(BLL.BLLBasket.Total(oBasket)))</a>
            End If
        </li>


        @If oBasket.Items.Count > 0 And Model <> DTO.Defaults.eComPages.Home And Model <> DTO.Defaults.eComPages.Pay Then
            @<li>
                 <a href='@Url.Action("Pay")'>
                     pagar
                 </a>
            </li>
        End If


            @If oBasket.Items.Count > 0 And Model <> DTO.Defaults.eComPages.Home And Model <> DTO.Defaults.eComPages.Pay Then
                @<li>
                    <a href='@Url.Action("ClearBasket")'>
                        vaciar la cesta
                    </a>
                </li>
            End If

            @If oBasket.Items.Count > 0 And Model <> DTO.Defaults.eComPages.Basket Then
                @<li>
                    <a href='@Url.Action("Basket")'>
                        ver cesta
                    </a>
                </li>
            End If

            @If Model <> DTO.Defaults.eComPages.Home Then
                 @<li>
                    <a href='@Url.Action("")'>
                        @If Model = DTO.Defaults.eComPages.Basket Or Model = DTO.Defaults.eComPages.Pay Then
                            @Html.Raw("Seguir comprando")
                        Else
                            @Html.Raw("Productos")
                        End if
                      </a>
                 </li>
            End If


        </ul>



