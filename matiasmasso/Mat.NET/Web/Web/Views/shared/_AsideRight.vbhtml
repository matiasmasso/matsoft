@ModelType DTOUser

@Select Case ViewBag.RightSideConfig
    Case DTO.Defaults.SideConfigs.LastNews
        @<div>
            @Html.Partial("_LastNoticias", Model)
        </div>

    Case DTO.Defaults.SideConfigs.LastEvents
                            @<div>
                                @Html.Partial("_LastEvents", Model)
                            </div>

    Case Else
                            @<div class="misc">
                                @Html.Partial("_LastRaffle", Model)
                            </div>
                            @<div class="rrss">
                                @Html.Partial("_SocialIcons", Model)
                            </div>
                            @<div>
                                @Html.Partial("_SocialFacebook", Model)
                            </div>
End Select


