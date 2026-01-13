@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oDistributors As List(Of DTO.DTOProductDistributor) = Model
End Code

<style>
    .BoxSalePoint {
        clear:both;
        display: block;
        width: 100%;
        height:120px;
        border: 1px solid darkgray;
        margin:10px 0;
    }
        .BoxSalePoint img {
            display: block;
            float:left;
            width: 150px;
            margin:36px 10px 36px 10px;
        }
        .BoxSalePoint div {
            display: block;
            margin:10px 10px 5px 10px;
            padding: 0px;
            overflow:hidden;
        }
        .BoxSalePoint a {
            text-decoration: none;
            color: black;
        }
        .BoxSalePoint h4 {
            margin:0 0 0 0;
            padding:0px;
            color: black;
        }
</style>

@For Each oDistributor As DTO.DTOProductDistributor In oDistributors

    @<div class="BoxSalePoint">
        <a href='@IIf(oDistributor.Url > "", oDistributor.Url, "#")'>
            @If oDistributor.NoLogo Then
                @<img src="/Media/img/ShopNoLogo.gif" alt="@oDistributor.Nom" />
            Else
                @<img src="@BLL.BLLProductDistributor.LogoUrl(oDistributor)" alt="@oDistributor.Nom">
            End If
            <div>
                <h4>@oDistributor.Nom</h4>
                    @oDistributor.Adr<br />
                    @oDistributor.Location.Nom<br />
                    @oDistributor.Tel
            </div>
        </a>
    </div>

Next
