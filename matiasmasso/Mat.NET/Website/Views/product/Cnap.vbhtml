@ModelType List(Of DTOProduct)

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)
    
End Code

<style scoped>
        .list-wrapper {
        clear:both;
    }
    
     .box-wrap {
        margin-bottom: 5px;
        display: inline-block;
    }

    .MyBox150 {
    position: relative;
    border: 1px solid darkgray;
    width: 150px;
    height: 200px;
    padding:0px;

}
    .MyBox150 img {
        position: relative;
        top: 0px;
        left: 0px;
        right:148px;
        bottom:auto;
        display: block;
        z-index:-1;
    }

    .MyBoxHeaderGreen {
        display: block;
        width: 100%;
        height: 24px;
        background-color: #72A58B;
        color: #FFFFFF;
        padding: 4px 7px 2px 4px;
        overflow: hidden;
        font-size:1em;
        margin: 0;
        z-index:3;
    }


    .MyBox150 a {
        text-decoration:none;
    }



    .MyBoxFooter {
        display: block;
        position: absolute;
        bottom: 0px;
        left: 0px;
        width:100%;
        height: 35px;
        background-color: #555555;
        color: #DDDDDD;
        z-index: 1;
        padding: 4px 7px 2px 4px;
        overflow:hidden;
        font-size:1em;
        text-align:center;
    }

    .MyBoxFooter:hover {
        background-color: red;
    }

</style>




@For Each item As DTOProduct In Model
    @<div class="MyBox150 box-wrap">
        <a href="@item.GetUrl(ContextHelper.Lang)">
            
            <div class="MyBoxHeaderGreen">
                @DTOProduct.GetNom(item)
            </div>

            <div>
                <img src="@FEB.Product.UrlThumbnail(item)" style="height:100%;" />
            </div>
            
            <div class="MyBoxFooter">
                @FEB.Product.Brand(exs, item).Nom
            </div>
        </a>
    </div>
Next
