@Select Case ViewBag.RightSideConfig
    Case DTO.Defaults.SideConfigs.LastNews
        @<div>
            @Html.Partial("_LastNoticias")
        </div>
    
    Case DTO.Defaults.SideConfigs.LastEvents
        @<div>
            @Html.Partial("_LastEvents")
        </div>

    Case Else
        @<div class="misc">
            @Html.Partial("_LastRaffle")
        </div>
        @<div class="rrss">
            @Html.Partial("_SocialIcons")
        </div>

        '@<div>
        '@Html.Partial("_SocialTwitterFeed")
        '</div>

        @<div>
            @Html.Partial("_SocialFacebook")
        </div>
End Select


