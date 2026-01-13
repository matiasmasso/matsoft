<!--@@ModelType DTO.NavViewModel.SideNavViewModel-->
@Code
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim model = ContextHelper.NavViewModel().SideNav(lang)
End Code

<div class="sidenav">
    <div>
        @If Model.Logo.ImageUrl.isNotEmpty() Then
            @<div Class="Logo-container">
                <a href="" title="">
                    <img src="@Model.Logo.ImageUrl"
                         alt="logo @Model.Logo.Title"
                         width="@Model.Logo.ImageWidth"
                         height="@Model.Logo.ImageHeight" />
                </a>
            </div>

        End If


        @If Model.Filters.Count > 0 Then
            @<div class="filtersTitle">@ContextHelper.Tradueix("Filtrar por...", "Filtrar per...", "Filter by...")</div>
            @<div Class="filters">
                @For Each oFilter In Model.Filters
                    @<div>

                        <input type="checkbox" data-tag="@oFilter.Tag" id="@oFilter.Tag" />
                        <label for="@oFilter.Tag">
                            @oFilter.Title
                        </label>
                        @For Each item In oFilter.Children
                            @<div class="filterItem">
                                <input type="checkbox" data-tag="@item.Tag" id="@item.Tag" />
                                <label for="@item.Tag">
                                    @item.Title
                                </label>
                            </div>
                        Next
                    </div>
                Next
            </div>
        End If


        <div class="menuItems">
            @For Each item In Model.CustomMenu
                @<a href="@item.NavigateTo" data-tag="@item.Tag">@item.Title</a>
            Next
        </div>

        @If Model.CustomMenu.Count > 0 Then
            @<div>&nbsp;</div>
        End If


        @For Each item In Model.ProductsMenu
            @<div class="dropdown2 ProductsMenu menuItems">
                @if item.Children.Count = 0 Then
                    @<a href="@item.NavigateTo">@item.Title</a>
                Else
                    @<span>@item.Title</span>
                    @<div class="dropdown-content2">
                        @For Each subitem In item.Children
                            @<div>
                                <a href="@subitem.NavigateTo">@subitem.Title</a>
                            </div>
                        Next
                    </div>
                End If
            </div>
        Next


        @If Model.ProductsMenu.Count > 0 Then
            @<div>&nbsp;</div>
        End If

        <div class="menuItems">
            @For Each item In Model.GlobalMenu
                @<div class="dropdown2 GlobalMenu menuItems">
                    @if item.Children.Count = 0 Then
                        @<a href="@item.NavigateTo">@item.Title</a>
                    Else
                        @<span>@item.Title</span>
                        @<div class="dropdown-content2">
                            @For Each subitem In item.Children
                                @<div>
                                    <a href="@subitem.NavigateTo">@subitem.Title</a>
                                </div>
                            Next
                        </div>
                    End If
                </div>
            Next
        </div>

        <div>&nbsp;</div>

        <div class="menuItems">
            <select Class="LangSelector" onchange="setLangCookie(this);">
                <option value="ESP" @IIf(lang.tag = "ESP", "selected", "")>Español</option>
                <option value="CAT" @IIf(lang.tag = "CAT", "selected", "")>Català</option>
                <option value="ENG" @IIf(lang.tag = "ENG", "selected", "")>English</option>
                <option value="POR" @IIf(lang.tag = "POR", "selected", "")>Português</option>
            </select>

        </div>

    </div>
</div>


