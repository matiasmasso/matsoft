@ModelType DTO.DTOProductPageQuery
@ModelType DTO.DTOProductPageQuery
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oProduct As DTO.DTOProduct = Model.Product
    Dim oHighResImages As MaxiSrvr.HighResImages = MaxiSrvr.HighResImagesLoader.FromProductOrChildren(oProduct)
End Code

<style>
    .page-wrapper {
        clear:both;
    }

    .BoxCollectionItem {
        display: inline-block;
        width: 150px;
        height: 180px;
        border: 1px solid gray;
        background-color: darkgray;
        margin-bottom:5px;
    }

        .BoxCollectionItem:hover {
            background-color: red;
        }

        .BoxCollectionItem a {
            text-decoration: none;
        }


        .BoxCollectionItem img {
            height: 150px;
            width: auto;
            max-width: 100%;
        }

        .BoxCollectionItem div {
            color: white;
            text-align: center;
            padding: 2px 4px;
            margin: 0;
            width: 100%;
            height: 30px;
            font-size: 0.6em;
        }
</style>

<div class="page-wrapper">
    @For Each item As MaxiSrvr.HighResImage In oHighResImages
        @<div class="BoxCollectionItem">
            <a href="@item.Url">
                <img src="@item.ThumbnailUrl" />
                <div class="truncate">
                    @item.Features
                </div>
            </a>
        </div>
    Next
</div>

