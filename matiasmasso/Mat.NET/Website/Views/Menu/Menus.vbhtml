@ModelType List(Of DTOWebMenuGroup)
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code

<div class="flex-container">
    @For Each oGroup In Model
        @<div class="flex-item" >
            <a href="@FEB.WebMenuGroup.LangUrl(oGroup).Tradueix(ContextHelper.Lang())" class="menuGroup" data-group="@oGroup.Guid.ToString">
                @DTOWebMenuGroup.Nom(oGroup, ContextHelper.lang())
            </a>

            @For Each item In oGroup.Items
                @<div class="flex-item subitem" data-group="@oGroup.Guid.ToString">
                    <a href="@item.LangUrl.Tradueix(ContextHelper.Lang())">
                        @item.LangText.Tradueix(ContextHelper.lang())
                    </a>
                </div>

            Next
        </div>
    Next
</div>

@Section Scripts
    <script>
        $(document).on('click', '.menuGroup', function (event) {
            var menuGroup = $(this).attr('data-group');
            var subItems = $('.subitem[data-group = ' + menuGroup + ']');
            if (subItems.length > 0) {
                event.preventDefault();
                $('.subitem[data-group = ' + menuGroup + ']').toggle();
            }
        })
    </script>
End Section

@Section Styles
    <style scoped>
        .flex-container {
            display: flex;
            flex-direction: column;
        }

        .flex-item {
            display: inline-block;
            border-top: 1px solid lightgray;
            padding: 10px;
        }

            .flex-item:first-child {
                border-top: none;
            }

        .subitem {
            padding-left: 50px;
            display: none;
        }
    </style>
End Section



