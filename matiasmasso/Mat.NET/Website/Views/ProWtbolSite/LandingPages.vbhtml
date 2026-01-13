@ModelType DTOWtbolSite
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

<div>
    <input type="text" class="SearchBox" />
</div>

<div class="Grid" data-contextmenu="LandingPages">
    <div class="Row">
        <div>@lang.Tradueix("Marca Comercial", "Marca Comercial", "Commercial Brand")</div>
        <div>@lang.Tradueix("Categoría", "Categoría", "Category")</div>
        <div>@lang.Tradueix("Producto", "Producte", "Product")</div>
        <div>@lang.Tradueix("Landing page")</div>
        <div>@lang.Tradueix("Fecha", "Data", "Date")</div>
        <div class="Status"></div>
    </div>
    @For Each item In Model.LandingPages
        @<div class="Row" data-guid="@item.Guid.ToString()" data-lowercase="@Html.Raw(item.Product.FullNom(lang).ToLower() & " " & item.Uri.ToString())" data-url="@item.Uri.ToString()">
            <div>@DTOProduct.brandNom(item.Product)</div>
            <div>@DTOProduct.CategoryNom(item.Product)</div>
            <div>@DTOProduct.SkuNom(item.Product)</div>
            <div>@item.Uri.ToString()</div>
            <div>@item.FchCreated.ToString("dd/MM/yy HH:mm")</div>
            <div class="Status @item.Status.ToString()"></div>
        </div>
    Next
</div>

<div class="ContextMenu" data-grid="LandingPages">
    <a href="#" data-url="/pro/proWtbolSite/LandingPage/{guid}" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
    <a href="#" data-action="Navigate">@lang.Tradueix("Navegar", "Navegar", "Navigate")</a>
    <a href="#" data-action="DeleteLandingPage">@lang.Tradueix("Eliminar", "Eliminar", "Delete")</a>
    <a href="#" data-url="/pro/proWtbolSite/LandingPageFactory/@Model.Guid.ToString()" target="_blank">@lang.Tradueix("Añadir nueva", "Afegir nova", "Add new one")</a>
    <hr/>
    <a href="#" data-action="StatusApproved" >@lang.Tradueix("Aprobar", "Aprovar", "Approve")</a>
    <a href="#" data-action="StatusDenied" >@lang.Tradueix("Denegar", "Denegar", "Deny")</a>
    <a href="#" data-action="StatusPending" >@lang.Tradueix("Pendiente de revisión", "Pendent de revisar", "Approval pending")</a>
</div>

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>
        /*---------------------- Search -----------------------------------*/

        $(document).on('keyup', '.MainContent .SearchBox', function (e) {
            var searchKey = $(this).val().toLowerCase();
            if (searchKey == '') {
                $('.Grid div[data-lowercase]').show();
            } else {
                $('.Grid div[data-lowercase]').hide();
                var selector = 'div[data-lowercase*="' + searchKey + '"]';
                var foundLines = $('.Grid').find(selector);
                foundLines.show();
            }
        });

        $(document).on('ContextMenuAppear', function (e, argument) {
            var statusTag = argument.activeTag.find('.Status');
            argument.contextmenu.find('[data-action*="Status"]').removeClass('Disabled');
            if (statusTag.hasClass('Approved'))
                argument.contextmenu.find('[data-action="StatusApproved"]').addClass('Disabled');
            else if (statusTag.hasClass('Denied'))
                argument.contextmenu.find('[data-action="StatusDenied"]').addClass('Disabled');
            else if (statusTag.hasClass('Pending'))
                argument.contextmenu.find('[data-action="StatusPending"]').addClass('Disabled');
        });


        $(document).on('ContextMenuClick', function (e, argument) {
            switch (argument.action) {
                case 'Navigate':
                    var url = argument.activeTag.data('url');
                    window.open(url, '_blank');
                    break;
                case 'DeleteLandingPage':
                    location.href = '/pro/proWtbolSite/DeleteLandingPage/' + argument.guid;
                    break;
                case 'StatusApproved':
                    UpdateStatus(argument.guid, argument.activeTag, 'Approved')
                    break;
                case 'StatusDenied':
                    UpdateStatus(argument.guid, argument.activeTag, 'Denied')
                    break;
                case 'StatusPending':
                    UpdateStatus(argument.guid, argument.activeTag, 'Pending')
                    break;
            }
        });

        function UpdateStatus(landingPageGuid, activeTag, status) {
            var url = '/pro/proWtbolSite/LandingPageStatus/' + landingPageGuid + '/' + status;
            var tag = activeTag.find('.Status');
            tag.removeClass('Approved');
            tag.removeClass('Denied');
            tag.removeClass('Pending');
            tag.addClass('Spinner16');
            activeTag.removeClass('Active');
            $.getJSON(url, function (result) {
                tag.removeClass('Spinner16');
                if (result.success) {
                    tag.addClass(status);
                }
            });

        }

    </script>
End Section

@Section Styles
    <style scoped>

        .Grid {
            grid-template-columns: auto auto auto auto auto 16px;
            font-size: smaller;
        }

                .Grid .Row div {
                    padding: 4px 7px 2px 4px;
                    text-overflow: ellipsis;
                    white-space: nowrap;
                    overflow: hidden;
                }

        .Status {
            width: 16px;
            height: 16px;
            background-repeat: no-repeat;
            background-position: center;
        }
            .Status.Approved {
                background-image: url('/Media/Img/Ico/ok.png');
            }
            .Status.Pending {
                background-image: url('/Media/Img/Ico/Empty.gif');
            }
            .Status.Denied {
                background-image: url('/Media/Img/Ico/aspa.png');
            }

    </style>
End Section
