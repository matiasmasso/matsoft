@ModelType DTOProductCategory
@Code
    Dim exs As New List(Of Exception)
    
    FEB2.ProductCategory.Load(Model, exs)
    Dim oFeaturedImages = FEB2.ProductFeatureImages.AllSync(exs, Model)
    Dim oPosts = FEB2.BloggerPosts.FromProductOrParentSync(Model, exs)
End Code


<style scoped>
    .wrapper {
        padding:0 15px 0 15px;
    }

    .pill-content-wrapper {
        clear:both;
    }

    .product-description-text {
        float: left;
        margin: 0;
        padding: 4px 7px 2px 0;
        max-width: 60%;
        font-size: 1em;
        margin-top: 15px;
    }

    .product-description-text-fullWidth {
        margin: 0;
        padding: 4px 7px 2px 0;
        font-size: 1em;
        margin-top: 15px;
    }

    .product-description-feature-images {
        max-width: 40%;
        float:right;
    }

    .product-description-feature-images:after {
        clear:both;
    }

    .product-description-feature-images img {
        width:100%;
        margin:0 0 10px 0;
    }

    .bloggerPosts {
    }

    .authorship {
        color:darkgray;
        font-size:0.8em;
    }

</style>

<div class="wrapper">
    <div class="pill-content-wrapper">
        <div class=@IIf(oFeaturedImages.Count > 0, "product-description-text", "product-description-text-fullWidth")>
            @Html.Raw(FEB2.Product.Html(Model, Mvc.ContextHelper.lang()))
        </div>

        @If oFeaturedImages.Count > 0 Then

            @<div Class="product-description-feature-images">
                <a href="@Model.GetUrl(Mvc.ContextHelper.Lang)" title='@Mvc.ContextHelper.Tradueix("colección disponible", "col.lecció disponible", "available range")'>
                    @For Each oFeature In FEB2.ProductFeatureImages.AllSync(exs, Model)
                        @<img src="@FEB2.ProductFeatureImage.Url(oFeature)" />
                    Next
                </a>
            </div>
        End If


    </div>

    <div class="BloggerPostsWrapper">
        <hr />
        @Mvc.ContextHelper.Tradueix("Artículos relacionados:", "Articles relacionats:", "Related articles:")
        @Html.Partial("_BloggerPosts", Model)
    </div>

    <!--
    @@If oPosts.Count > 0 Then
        @@<div class="bloggerPosts">
            <hr />
            @@Mvc.ContextHelper.Tradueix("Artículos relacionados:", "Articles relacionats:", "Related articles:")
            @@For Each oPost As DTOBloggerPost In oPosts
                @@<p>
                    <a href="@@oPost.Url" target="_blank" title="@@oPost.Title">@@oPost.Title</a>
                    <br />
                    <span class="authorship">
                        @@(Mvc.ContextHelper.Tradueix("por", "per", "by") & " " & oPost.Blogger.Title & ", " & oPost.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
                    </span>
                </p>
            Next
        </div>
    End If
        -->
</div>