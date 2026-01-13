@ModelType List(Of DTOSubscription)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


<h1>@ViewBag.Title</h1>

<fieldset class="checkboxes" id="subscripcions">
    <p>

        @For Each oSsc In Model
            @<div class="item">
                <div Class="itemTitle">
                    <Label for='@("CheckBox_" & oSsc.Guid.ToString())'>

                        <input type="checkbox"
                               id='@("CheckBox_" & oSsc.Guid.ToString())'
                               value="@oSsc.Guid.ToString"
                               @IIf(oSsc.FchSubscribed = Nothing, "", " checked = 'checked' ") />

                        <span>
                            <b>@oSsc.Nom.Tradueix(lang)</b>
                        </span>
                    </Label>


                </div>
                <div class="itemDescription">
                    @oSsc.Dsc.Tradueix(lang)
                </div>
            </div>
        Next

    </p>

    <div id="submit">
        <input id="buttonOk" type="button" class="form-submit" value='@Mvc.ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
    </div>

    <span id="Subscriptions-warn" class="form-warn"></span>

</fieldset>





@Section Styles

    <style scoped>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }

        fieldset {
            border: none;
        }

        .item {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            margin-top: 15px;
        }

        .itemTitle {
            display: flex;
            flex-direction: row;
        }

        .itemDescription {
            padding-left: 25px;
        }

        #submit {
            display: flex;
            justify-content: flex-end;
            margin-right: 0;
        }

            #submit input {
                padding: 7px 20px;
                margin-right: 0;
                border: 1px solid cornflowerblue;
                border-radius: 5px;
                background-color: cornflowerblue;
                color: white;
            }

                #submit input:hover {
                    background-color: aqua;
                    color: white;
                }
    </style>

End Section

@Section Scripts

    <script type="text/javascript">
        $("#buttonOk").click(function () {
            var selectedItems = [];
            $('#subscripcions :checked').each(function () {
                selectedItems.push($(this).val());
            });
            update(selectedItems);
        })

        function update(selectedItems) {
            $.ajax({
                type: "POST",
                url: '@Url.Action("updateSubscripcions")',
                data: { 'subscripcions': selectedItems.join() },
                dataType: "json",
                success: function (result) {
                    onValidation(result);
                }
            });
        }

        function onValidation(result) {
            $('#Subscriptions-warn').html(result.text)
            if (result.value == 1)
                $('#Subscriptions-warn').css('color', 'blue');
            else
                $('#Subscriptions-warn').css('color', 'red');
        }
    </script>

End Section