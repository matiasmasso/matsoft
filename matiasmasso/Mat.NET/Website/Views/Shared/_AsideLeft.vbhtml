@ModelType DTOUser
@Code

    Dim exs As New List(Of Exception)
    Dim oNextEvento = FEB.Eventos.NextEventoSync(exs, Model)
    Dim oPostOfTheDay = FEB.BloggerPosts.PostOfTheDaySync(Website.GlobalVariables.Emp, ContextHelper.Lang(), exs)
End Code

<div>
    @Select Case ViewBag.leftSideConfig
        Case DTO.Defaults.SideConfigs.Social
        @<div class="rrss">
            @Html.Partial("_SocialIcons")
        </div>
            '@<div>
            '@Html.Partial("_SocialTwitterFeed")
            '</div>
        @<div>
            @Html.Partial("_SocialFacebook", Model)
        </div>

        Case Else
        @<div class="misc">
    <!--@@Html.Partial("_LastNoticia", Model)-->
</div>



        @If oNextEvento IsNot Nothing Then
            @<div class="misc">
                @Html.Partial("_NextEvento", oNextEvento)
            </div>
            End If
        @<div class="misc">
            @Html.Partial("_LastVideo", Model)
        </div>

            If oPostOfTheDay IsNot Nothing Then
                @Html.Partial("_BloggerPostOfTheDay", oPostOfTheDay)
            End If
    End Select
</div>


