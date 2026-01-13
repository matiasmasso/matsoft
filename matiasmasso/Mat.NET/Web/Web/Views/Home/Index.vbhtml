@ModelType DTO.MvcHomeModel
@Code
    Layout = "~/Views/Shared/_Layout_2.vbhtml"
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div class="Home">

    <div class="AsideLeft">
        @For Each item In Model.Noticias
            @<a href="@item.NavigateTo" title="@item.Title">
                <img src="@item.ImageUrl" alt="@item.Title" width="@DTONoticia.THUMBNAIL_WIDTH" height="@DTONoticia.THUMBNAIL_HEIGHT" />
                <div class="Caption">
                    @item.Title
                </div>
            </a>
        Next
    </div>

    <div class="CenterColumn">
        <div class="BannerRotator">
            <a href="@Model.Banners.First().NavigateTo" title="@Model.Banners.First().Title" class="img-anchor">
                <img src="@Model.Banners.First().ImageUrl" alt="@Model.Banners.First().Title" width="@DTOBanner.BANNERWIDTH" height="@DTOBanner.BANNERHEIGHT" />
            </a>

            <a class="prev" title='@Mvc.ContextHelper.Tradueix("ir al banner anterior", "anar al banner anterior", "go to previous banner")'>&#10094;</a>
            <a class="next" title='@Mvc.ContextHelper.Tradueix("ir al siguiente banner", "anar al següent banner", "go to next banner")'>&#10095;</a>

            <div style="text-align:center" class="dots">
                @For i = 0 To Model.Banners.Count - 1
                    @<a class="dot" data-tag="@i" title="@Model.Banners(i).Title"></a>
                Next
            </div>
        </div>

        <div Class="BrandsV">
            <div Class="BrandV0">
                <a href="@Model.BrandsV(0).NavigateTo" title="@Model.BrandsV(0).Title">
                    <img src="@Model.BrandsV(0).ImageUrl" alt="@Model.BrandsV(0).Title" width="@MvcHomeModel.BRANDSVWIDTH" height="@MvcHomeModel.BRANDSVHEIGHT" />
                </a>
            </div>
            <div class="BrandV1">
                <a href="@Model.BrandsV(1).NavigateTo" title="@Model.BrandsV(1).Title">
                    <img src="@Model.BrandsV(1).ImageUrl" alt="@Model.BrandsV(1).Title" width="@MvcHomeModel.BRANDSVWIDTH" height="@MvcHomeModel.BRANDSVHEIGHT" />
                </a>
            </div>
            <div class="BrandV2">
                <a href="@Model.BrandsV(2).NavigateTo" title="@Model.BrandsV(2).Title">
                    <img src="@Model.BrandsV(2).ImageUrl" alt="@Model.BrandsV(2).Title" width="@MvcHomeModel.BRANDSVWIDTH" height="@MvcHomeModel.BRANDSVHEIGHT" />
                </a>
            </div>
        </div>

        <div class="SinglePosts">
            <div class="SingleBlog">
                <a href="@Model.BlogPosts(0).NavigateTo" title="@Model.BlogPosts(0).Title">
                    <img src="@Model.BlogPosts(0).ImageUrl" alt="@Model.BlogPosts(0).Title" width="@DTOBlogPost.THUMBNAIL_WIDTH" height="@DTOBlogPost.THUMBNAIL_WIDTH" />
                    <div class="Caption">
                        @Model.BlogPosts(0).Title
                    </div>
                </a>
            </div>
            <div class="SingleNoticia">
                <a href="@Model.Noticias(0).NavigateTo" title="@Model.Noticias(0).Title">
                    <img src="@Model.Noticias(0).ImageUrl" alt="@Model.Noticias(0).Title" width="@DTONoticia.THUMBNAIL_WIDTH" height="@DTONoticia.THUMBNAIL_HEIGHT" />
                    <div class="Caption">
                        @Model.Noticias(0).Title
                    </div>
                </a>
            </div>
        </div>

        <div class="BrandsH">
            <div class="BrandH0">
                <a href="@Model.BrandsH(0).NavigateTo" title="@Model.BrandsH(0).Title">
                    <img src="@Model.BrandsH(0).ImageUrl" alt="@Model.BrandsH(0).Title" width="@MvcHomeModel.BRANDSHWIDTH" height="@MvcHomeModel.BRANDSHHEIGHT" />
                </a>
            </div>
            <div class="BrandH1">
                <a href="@Model.BrandsH(1).NavigateTo" title="@Model.BrandsH(1).Title">
                    <img src="@Model.BrandsH(1).ImageUrl" alt="@Model.BrandsH(1).Title" width="@MvcHomeModel.BRANDSHWIDTH" height="@MvcHomeModel.BRANDSHHEIGHT" />
                </a>
            </div>
            <div class="BrandH2">
                <a href="@Model.BrandsH(2).NavigateTo" title="@Model.BrandsH(2).Title">
                    <img src="@Model.BrandsH(2).ImageUrl" alt="@Model.BrandsH(2).Title" width="@MvcHomeModel.BRANDSHWIDTH" height="@MvcHomeModel.BRANDSHHEIGHT" />
                </a>
            </div>
        </div>


        <div class="Raffle">
            <a href="@Model.ActiveRaffle.NavigateTo" title="@Model.ActiveRaffle.Title">
                <img src="@Model.ActiveRaffle.ImageUrl" alt="@Model.ActiveRaffle.Title" width="@DTORaffle.BANNER_WIDTH" height="@DTORaffle.BANNER_HEIGHT" />
            </a>
        </div>

        @If Not Mvc.ContextHelper.IsIOS Then

            @<div Class="AppStoreIOS">
                <a href="https://apple.co/3dDXbCl">
                    <img src="/Media/Img/appstore.svg" width="120" height="40" />
                    <div>
                        <Span>
                            @lang.tradueix("¡Descarga nuestra App", "Descarrega't la nostra App", "Download our App", "Baixe nosso aplicativo")
                        </Span>
                        <br />
                        <Span>
                            @lang.tradueix("para iPhone/iPad!", "per iPhone/iPad!", "for iPhone/iPad!", "para iPhone/iPad!")
                        </Span>
                    </div>
                </a>
            </div>

        End If

    </div>
    <div class="AsideRight">
        @For Each item In Model.BlogPosts
            @<a href="@item.NavigateTo" title="@item.Title">
                <img src="@item.ImageUrl" alt="@item.Title" width="@DTONoticia.THUMBNAIL_WIDTH" height="@DTONoticia.THUMBNAIL_HEIGHT" />
                <div class="Caption">
                    @item.Title
                </div>
            </a>
        Next
    </div>
</div>




@Section Styles
    <link href="~/Media/Css/BannerRotator.css" rel="stylesheet" />
    <style>
        .Home {
            display: flex;
            flex-direction: row;
            align-content: flex-start;
            column-gap: 10px;
            width: 100%;
        }

            .Home .Caption {
                font-size: 0.8em;
                padding: 10px 0;
            }

        .AsideLeft, .AsideRight {
            display: flex;
            flex-direction: column;
            min-width: 180px;
            max-width: 265px;
            flex-basis: auto; /* default value */
            flex-grow: 1;
            flex-shrink: 1;
        }

        .CenterColumn {
            display: flex;
            flex-direction: column;
            flex-basis: auto; /* default value */
            flex-grow: 1;
            flex-shrink: 1;
        }

            .CenterColumn img, .AsideLeft img, .AsideRight img {
                width: 100%;
                height: auto;
            }


        .BrandsV {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            column-gap: 10px;
            margin-bottom: 10px;
            width: 100%;
        }

        .BrandV0 {
            flex: 1 1 auto;
        }

        .BrandV1 {
            flex: 1 1 auto;
        }

        .BrandV2 {
            flex: 1 1 auto;
        }

        .BrandsH {
        }

        .BrandH0 {
            margin-bottom: 10px;
        }

        .BrandH1 {
            margin-bottom: 10px;
        }

        .BrandH2 {
            margin-bottom: 10px;
        }

        .SinglePosts {
            display: flex;
            flex-direction: row;
            justify-content: space-evenly;
            column-gap: 10px;
            margin-bottom: 10px;
            width: 100%;
        }

        .SingleBlog {
            flex-basis: 100%;
        }

        .SingleNoticia {
            flex-basis: 100%;
        }

        .Raffle {
            margin-bottom: 10px;
            width: 100%;
        }

        @@media (max-width:1100px) {

            .AsideLeft > a:last-child, .AsideRight > a:last-child {
                display: none;
            }
        }

        @@media (max-width:1000px) {

            .AsideLeft {
                display: none;
            }
        }

        @@media (min-width:601px) {
            .SinglePosts, .BrandsH {
                display: none;
            }
        }

        @@media (max-width:600px) {

            .AsideRight, .BrandsV {
                display: none;
            }

            .BannerRotator .dots {
                display: none;
            }
        }

        @@media (max-width:350px) {

            .SinglePosts {
                flex-direction: column;
            }
        }
    </style>
End Section

@section Scripts
    <script src="~/Media/js/BannerRotator.js"></script>
    <script>
        $(document).ready(function () {
            LoadBannerRotator(@Html.Raw(System.Web.Helpers.Json.Encode(Model.Banners)))
        });
    </script>
End Section




