@ModelType DTOProduct
@Code
    Dim exs As New List(Of Exception)
    
    Dim oCategory As DTOProductCategory = Model
    FEB2.ProductCategory.Load(oCategory, exs)
    Dim oSkus = FEB2.ProductSkus.AllSync(exs, oCategory, Mvc.GlobalVariables.Emp.Mgz, False)
    oSkus = oSkus.FindAll(Function(x) x.NoWeb = False And DTOProductSku.IsHidden(x) = False).ToList

End Code

<style>
    .hscroll {
        clear:both;
        overflow: hidden;
        white-space: nowrap;
        overflow-x: scroll;
    }

        .hscroll::-webkit-scrollbar {
            /*display: none;*/
        }

    .thumbnail {
        text-decoration: none;
        display: inline-block;
        /*
        background-image: url(http://localhost:53256/Content/test.png);
        background-repeat: no-repeat;
            */
        border-width: 1px;
        border-color: black;
        border-style: solid;
        margin: 5px;
    }
</style>

<div class="hscroll">
    @For Each item As DTOProductSku In oSkus
        @<a class="thumbnail" href="@item.GetUrl(Mvc.ContextHelper.Lang)" title="@item.Nom.Tradueix(Mvc.ContextHelper.Lang())">
            <img src="@item.ThumbnailUrl()" alt="@item.nom.Tradueix(Mvc.ContextHelper.lang())" width="100" />
        </a>
    Next
</div>




