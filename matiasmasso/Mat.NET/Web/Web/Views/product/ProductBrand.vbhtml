@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim wideItems = Model.Items.Count < 9
    Dim galleryClass = IIf(wideItems, "CollectionGalleryWide", "CollectionGallery")
    Dim captionClass = "caption"
    If Model.GalleryMode = DTO.ProductModel.GalleryModes.Depts Then
        captionClass = "DeptsCaption"
        galleryClass = "DeptsGallery"
    End If
End Code

<h1>@Html.Raw(Model.Title)</h1>

<div class="Contingut">
    <div class='@galleryClass'>
        @For Each item As DTO.ImageBoxViewModel In Model.Items
            @<div Class="CollectionItem" data-tag="@item.Tag">
                <a href="@item.NavigateTo" title="@item.Title">
                    <img src="@item.ImageUrl" alt="@item.Title" onError="this.onerror=null;this.src='/Media/img/misc/noImage410.jpg';" />
                    <div Class='@captionClass'>@item.Title</div>
                </a>
            </div>
        Next
    </div>
</div>

@Html.Partial("_Persianas")




@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var product = '@Model.Tag';
        var lang = '@Mvc.ContextHelper.Lang.Tag';

        $(document).ready(function () {
            loadStoreLocator(product, lang);
        });

        $(document).on('storeLocatorChanged', function () {
            if (typeof gtag_report_conversion === "function") {
                gtag_report_conversion(window.location.href);
            }
        });
    </script>

End Section

@Section Styles

    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <link href="~/Media/Css/StoreLocator.css" rel="stylesheet" />
    <link href="~/Styles/VideoPlugins.css" rel="stylesheet" />
    <link href="~/Media/Css/PopSideNav.css" rel="stylesheet" />

    <style>

        /*
        .PageWrapper {
            margin: auto;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            max-width: 1200px !important;
        }
            */
        .ContentColumn {
            max-width: 100%;
        }

        .Contingut {
            max-width: 100%;
        }

        /*side menu*/

        .sidenav {
            min-width: 250px;
            max-width: 300px;
            padding: 0;
            margin: 0 20px 0 0;
        }

        .SideMenuCaption {
            display: none;
        }


        /*------------------depts gallery---------------*/

        .DeptsGallery {
            display: grid;
            /*
             grid-template-columns: minmax(0, 1fr) minmax(0, 1fr);
           */
            grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
            grid-auto-flow: row;
            grid-gap: 20px;
        }

            .DeptsGallery .item {
                position: relative;
                max-width: 410px;
            }

                .DeptsGallery .item img {
                    width: 100%;
                }


        .DeptsCaption {
            position: absolute;
            top: 0px;
            left: 0px;
            right: 20px;
            padding: 10px !important;
            background-color: rgba(0,0,0,0.3);
            font-size: 1.7vw !important;
            font-weight: 700;
            color: white;
            flex: 1 1 0;
            box-sizing: border-box;
        }



        @@media (max-width: 700px) {
            .DeptGallery {
                grid-template-columns: minmax(0, 1fr);
            }

            .DeptCaption {
                font-size: 1.5em;
                font-weight: 700;
                color: white;
                flex: 1 1 0;
            }
        }



        /*-----------filters -----------*/

        .filtersTitle {
            color: darkgray;
            font-weight: 600;
            padding: 4px 0 2px 0;
            margin: 30px 0 20px 0;
        }

        .filters > a {
            display: block;
            color: darkgray;
            font-weight: 600;
            padding: 15px 0 2px 0;
        }


        @@media(max-width:700px) {
            .PageWrapperHorizontal {
                flex-direction: column-reverse !important;
                align-items: center;
            }

            .SideMenuCaption {
                display: block;
                padding: 20px 0 15px 0;
            }
        }
    </style>
End Section

