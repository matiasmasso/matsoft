@ModelType DTOSubscriptor
@Code
    ViewBag.Title = Mvc.ContextHelper.Tradueix("Cancelar subscripción", "Cancel·lar subscripció", "Unsubscribe")
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oLang = Mvc.ContextHelper.Lang()
End Code


<h2>@Model.Subscription.Nom.Tradueix(oLang)</h2>

<p>
    @Model.Subscription.Dsc.Tradueix(oLang)
</p>

@Using Html.BeginForm()
    @<div class="buttons">
        <Button id="ButtonKeep" value='@oLang.Tradueix("Seguir suscrito")' onclick="window.location.href = '/';" />
        <Button id="ButtonUnsubscribe" type="submit" value='@oLang.Tradueix("Cancelar subscripción")' />
    </div>
End Using


@Section Styles
    <style>
        .buttons {
            display: flex;
        }

            .buttons button {
                padding: 10px;
            }

        #ButtonKeep {
            background-color: blue;
            color: white;
        }

        #ButtonUnsubscribe {
            background-color: red;
            color: white;
        }
    </style>
End Section
