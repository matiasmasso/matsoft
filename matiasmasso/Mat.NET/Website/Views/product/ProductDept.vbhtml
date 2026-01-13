@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim wideItems = Model.Items.Count < 9
End Code


@Html.Partial("_ProductBreadcrumbs", Model.Product.Breadcrumbs(lang))

<div class="Contingut">
    @*@Html.Raw(Model.Product.Content.Html(lang))*@
    @Html.Raw(Model.TextBeforeMoreOrDefault())

    <div class='@IIf(wideItems, "CollectionGalleryWide", "CollectionGallery")'>
        @For Each item As DTO.ImageBoxViewModel In Model.Items
            @<div Class="CollectionItem" data-tag="@item.Tag">
                <a href="@item.NavigateTo" title="@item.Title">
                    <img src="@item.ImageUrl"
                         alt="@item.Title"
                         width="410"
                         height="410"
                         onError="this.onerror=null;this.src='/Media/img/misc/noImage410.jpg';" />
                    <div Class="caption">@item.Title</div>
                </a>
            </div>
        Next
    </div>

    @Html.Raw(Model.TextAfterMoreOrDefault())
</div>


<input type="hidden" id="productGuid" value="@Model.Product.Guid.ToString()" />

@Section AdditionalMetaTags
    @If CType(Model.Product, DTODept).obsoleto Then
        @<meta name="robots" content="noindex" />
    Else
        @<meta Property="og:type" content="website" />
        @<meta Property="og:title" content="@ViewBag.Title" />
        If Model.TextHasImages Then
            @<meta property="og:image:url" content="@Model.FacebookImgUrl()" />
        End If
        @<meta Property="og:url" content="@Model.Product.DomainUrl(ContextHelper.Domain)" />
        If Model.Excerpt > "" Then
            @<meta Property="og:description" content="@Html.Raw(Model.Excerpt)" />
        End If
    End If
End Section


@Section Scripts
    <script>
        var filters = @Html.Raw(System.Web.Helpers.Json.Encode(Model.Filters));
        var checkedItems = @Html.Raw(System.Web.Helpers.Json.Encode(Model.CheckedFilterItems));
        var allTargets = AllTargets();

        $(document).ready(function () {
            if (checkedItems.length > 0) {

                $.each(checkedItems, function (i, item) {
                    var itemCheckbox = $('.filterItem [data-tag=' + item.Guid + ']');
                    itemCheckbox.prop('checked', true);

                    var filterDiv = itemCheckbox.parent().parent();
                    filterDiv.children('input[data-tag]').prop('checked', true);
                    filterDiv.children('.filterItem').show();
                });

                ShowActiveTargets();
            }
        });

        $(document).on('change', '.sidenav .filters > div > input[type=checkbox]', function () {
            onFilterCheckedChange($(this));
        });

        $(document).on('change', '.sidenav .filterItem input[type=checkbox]', function () {
            onFilterItemCheckedChange($(this));
        });

        function onFilterCheckedChange(checkbox) {
            checkbox.siblings('.filterItem').toggle('fast');
            if (checkbox.prop('checked') == false) {
                UncheckFilterItems(checkbox);
                ShowActiveTargets();
            }
        }

        function onFilterItemCheckedChange(checkbox) {
            ShowActiveTargets();
        }

        function UncheckFilterItems(checkbox) {
            var itemCheckboxes = checkbox.siblings().children('input[type=checkbox]');
            itemCheckboxes.prop('checked', false);
        }

        function ShowActiveTargets() {
            var targets = ActiveTargets();
            var checkedFilters = CheckedFilters();
            if (targets.length == 0 && checkedFilters == 0) {
                $(".CollectionItem[data-tag]").show();
            } else {
                $(".CollectionItem[data-tag]").hide();
                for (i = 0; i < targets.length; i++) {
                    var target = targets[i];
                    $(".CollectionItem[data-tag='" + target + "']").show();
                }
            }
        }

        function ActiveTargets() {
            var retval = [];
            var i;
            for (i = 0; i < allTargets.length; i++) {
                var target = allTargets[i];
                if (PassesAllFilters(target)){
                    retval.push(target);
                }
            }
            return retval;
        }

        function PassesAllFilters(target) {
            var retval = true;
            var j;
            var checkedFilters = CheckedFilters();
            for (j = 0; j < checkedFilters.length; j++) {
                var filter = checkedFilters[j];
                if (!PassesFilter(filter, target)) {
                    /*si en falla un ho falla tot'*/
                    retval = false;
                    break;
                }
            }
            return retval;
        }

        function PassesFilter(filter, target) {
            var retval = false;
            var k;
            var items = CheckedItems(filter);
            if (items.length == 0) {
                //si no hi ha activada cap opció desactiva el filtre i dona'l per bo
                retval = true;
            } else {
                for (k = 0; k < items.length; k++) {
                    var item = items[k];
                    var targetGuids = item.TargetGuids;
                    if (targetGuids.includes(target)) {
                        //nomes que en passi un ho passa tot
                        retval = true;
                        break;
                    }
                }
            }
            return retval;
        }

        function CheckedFilters() {
            var checkboxes = $('.sidenav .filters > div > input:checked');
            var checkedTags = checkboxes.map(function () { return $(this).data("tag"); }).get();
            var retval = filters.filter((x) => checkedTags.includes(x.Guid));
            return retval;
        }

        function CheckedItems(filter) {
            var items = filter.Items;
            var filterCheckbox = $('.sidenav .filters > div > input[data-tag=' + filter.Guid + ']');
            var itemCheckboxes = filterCheckbox.siblings().children('input:checked');
            var checkedTags = itemCheckboxes.map(function () { return $(this).data("tag"); }).get();
            var retval = items.filter((y) => checkedTags.includes(y.Guid));
            return retval;
        }

        function AllTargets() {
            var retval = [];
            $('.CollectionItem[data-tag]').each(function () {
                var tag = $(this).attr('data-tag');
                retval.push(tag);
            });
            return retval;
        }


    </script>
End Section

@Section Styles

    <link href="~/Media/Css/Product.css" rel="stylesheet" />

    <style>

        .ContentColumn {
            width: 100%;
        }

        .Contingut {
            max-width: 100%;
        }

        .CollectionGalleryWide, .CollectionGallery {
            margin-top:20px;
        }
    </style>
End Section

