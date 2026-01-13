@ModelType Nullable(Of Guid)
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
End Code

@If Model Is Nothing Then
    If oUser Is Nothing Then
        For Each oGroup In FEBL.WebMenuGroups.ForWebSiteSync(Mvc.GlobalVariables.Emp, oWebsession)
            @<div class="MobMenu">
                <a href="@FEBL.WebMenuGroup.Url(oGroup)">
                    @DTO.DTOWebMenuGroup.Nom(oGroup, Mvc.ContextHelper.getlang())
                </a>
            </div>  Next
    Else
        For Each oGroup In FEBL.WebMenuGroups.ForWebSiteSync(Mvc.GlobalVariables.Emp, oWebsession)
            @<div class="MobMenu">
                <a href="@FEBL.WebMenuGroup.Url(oGroup)">
                    @DTO.DTOWebMenuGroup.Nom(oGroup, Mvc.ContextHelper.getlang())
                </a>
            </div>
        Next
    End If

Else
    Dim oParent As DTO.DTOWebMenuGroup = FEBL.WebMenuGroup.FindSync(exs, Model, Mvc.GlobalVariables.Emp, oWebsession.User, Mvc.ContextHelper.getlang())
    For Each item As DTO.DTOWebMenuItem In oParent.Items
        @<div class="MobMenu">
            <a href="@item.Url">
                @DTO.DTOWebMenuItem.Nom(item, Mvc.ContextHelper.getlang())
            </a>
        </div>
    Next
End If


