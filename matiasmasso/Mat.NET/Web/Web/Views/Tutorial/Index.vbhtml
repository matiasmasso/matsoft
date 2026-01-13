@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"
    Dim lang = Mvc.ContextHelper.Lang
End Code



<div id="videos">
    @If Model.Count = 0 Then
        @<div class="novideo">
            @Html.Raw(oWebsession.Tradueix("ningún video disponible en estos momentos", "Cap video disponible en aquests moments", "Sorry no movies available yet"))
        </div>
    Else
        For Each item As DTO.DTOTutorial In Model

            @<div class="TutorialRow">
                <div class="TutorialVideoCell">
                    <iframe width="150" height="150" src='@BLL.BLLTutorial.Url_embed(item)'
                            frameborder="0"
                            allowfullscreen></iframe>
                </div>

                <div class="TutorialTextCell">

                    <div class="TutorialTitle">
                        @item.Title
                    </div>

                    <div class="TutorialExcerpt">
                        @item.Excerpt
                    </div>

                </div>

            </div>


        Next
    End If
</div>

@Section styles
<style>

    .TutorialRow {
        margin-bottom: 10px;
        text-align:left;
    }

    .TutorialVideoCell {
        display: inline-block;
        width: 150px;
        vertical-align: top;
        margin-right: 10px;
        margin-left: 20px;
    }

    .TutorialTextCell {
        display: inline-block;
        max-width: 250px;
        vertical-align: top;
    }

    .TutorialTitle {
        font-weight: bold;
    }

    .TutorialExcerpt {
        margin-top: 10px;
    }
</style>
End Section