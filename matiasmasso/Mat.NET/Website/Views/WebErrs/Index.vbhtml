@ModelType List(Of DTOWebErr)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<div>
    <label for="HideBots">oculta bots</label>
    <input type="checkbox" id="HideBots" checked="checked" onclick="ToggleBots();" />
</div>

<div>

    @If Model.Count = 0 Then
        @<span>No s'ha detectat cap link trencat</span>
    Else
        @<a href="#" Class="Button Reset">reset</a>
        @<span>@Model.Where(Function(x) x.IsBot = False).Count links trencats des de @Model.First.Fch.ToString() </span>
        @<span>(mes @Model.Where(Function(x) x.IsBot = True).Count de bots) </span>
    End If

</div>

@For Each item As DTOWebErr In Model
    @<div class='Item @IIf(item.IsBot, "Bot", "")' data-guid='@item.Guid.ToString()' @Html.Raw(IIf(item.IsBot, "hidden", ""))>
        <div>@item.Fch.ToString("dd/MM/yy HH:mm")</div>
        <div class="Truncate">
            <a href="@item.Url" target="_blank"> @item.Url</a>
        </div>
        <div class="Truncate">@item.Msg</div>
        @If Not String.IsNullOrEmpty(item.Referrer) Then
            @<div class="Truncate">referrer:<a href="@item.Referrer" target="_blank"> @item.Referrer </a></div>
        End If
        <div class="Truncate">@item.FormattedBrowser()</div>
        @If item.User IsNot Nothing Then
            @<div class="Truncate">
                user: @item.User.NicknameOrElse
            </div>
        End If
        <div>
            <a href="#" class="Button Delete">eliminar</a>
        </div>
    </div>
Next

@Section Scripts
    <script >
        function ToggleBots() {
            $(".Bot").toggle();
        }
        $(document).on('click', '.Button.Delete', function () {
            var guid = $(this).closest('[data-guid]').data('guid');
            var url = '/weberrs/delete';
            var data = { 'guid': guid };
            var anchor = event.target;
            $(anchor).append(spinner16);
            $.post(url, data, function (result) {
                if (result.success == true) {
                    location.reload();
                }
                else {
                    spinner16.remove();
                    $(this).after('<span style="color:red;">' + exs[0].message + '</span>');
                }
            })
        })

        $(document).on('click', '.Button.Reset', function () {
            var url = '/weberrs/reset';
            var anchor = event.target;
            $(anchor).append(spinner16);
            $.post(url, function (result) {
                if (result.success == true) {
                    location.reload();
                }
                else {
                    spinner16.remove();
                    $(this).after('<span style="color:red;">' + exs[0].message + '</span>');
                }
            })
        })
    </script>
End Section
@Section Styles
    <style scoped>
        .Item {
            margin-top: 20px;
            border-bottom: 1px solid gray;
        }
        .Button {
            display:inline-block;
            margin: 3px 0;
            padding: 3px 7px 2px 4px;
            border: 1px solid gray;
            border-radius: 3px 4px;
        }
    </style>
End Section