@ModelType DTODept
@Code
    
    Dim oCategories = FEB2.Dept.CategoriesSync(Model).Where(Function(x) x.HideUntil < Today).ToList
End Code


<style scoped>
    .BoxCategory {
        clear: both;
        position: relative;
        display: block;
        height: 132px;
        border-bottom: 1px solid darkgrey;
        margin-bottom: 5px;
        overflow: hidden;
    }

    .BoxCategory:last-child {
            border-bottom: none;
        }


        .BoxCategory img {
            display: block;
            float: left;
            width: 100px;
            height: 130px;
            margin: 0 25px;
        }

        .BoxCategory div {
            margin-left: 131px;
            height: 100%;
            overflow: hidden;
            margin: 0;
            padding: 5px;
        }

        .BoxCategory a {
            text-decoration: none;
            color: black;
        }

        .BoxCategory span {
            font-weight: 700;
        }

        .BoxCategory p {
            margin-top: 8px;
            font-size: 0.8em;
        }


    .BandaDiagonal {
        position: relative;
        top: 60px;
        right: -120px;
        text-align: right;
        color: red;
        font-weight: bold;
        /*float:right;*/
        /* Safari */
        -webkit-transform: rotate(-45deg);
        /* Firefox */
        -moz-transform: rotate(-45deg);
        /* IE */
        -ms-transform: rotate(-45deg);
        /* Opera */
        -o-transform: rotate(-45deg);
    }
</style>

<div class="CollectionGallery">
    @For Each item As DTOProductCategory In oCategories.FindAll(Function(x) x.EnabledxConsumer = True)
        @<div class="CollectionItem">
            <a href="@item.GetUrl(Mvc.ContextHelper.Lang)">
                <img src="@FEB2.ProductCategory.ThumbnailUrl(item)" />
                <div class="innertube">
                    <span>@item.Nom.Tradueix(Mvc.ContextHelper.Lang())</span>
                    <p>
                        @Html.Raw(DTOProductCategory.ExcerptOrShortDescription(item, Mvc.ContextHelper.Lang()))
                    </p>
                </div>
            </a>

            @If DTOProduct.Launchment(item, Mvc.ContextHelper.Lang()) > "" Then
                @<div class="BandaDiagonal">
                    @DTOProduct.Launchment(item, Mvc.ContextHelper.Lang())
                </div>
            End If
        </div>
    Next
</div>






