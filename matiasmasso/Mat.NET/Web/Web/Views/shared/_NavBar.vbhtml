@ModelType DTO.NavViewModel.TopNavBarViewModel

<div class="topnavbar">
    @For Each item In Model.ProductsMenu
        @<div class="dropdown ProductsMenu">
            @if item.Children.Count = 0 Then
                @<a href="@item.NavigateTo">@item.Title</a>
            Else
                @<span>@item.Title</span>
                @<div Class="dropdown-content">
                    @For Each subitem In item.Children
                        @<div>
                            <a href="@subitem.NavigateTo">@subitem.Title</a>
                        </div>
                    Next
                </div>
            End If
        </div>
    Next

    @For Each item In Model.GlobalMenu
        @<div class="dropdown GlobalMenu">
            @if item.Children.Count = 0 Then
                @<a href="@item.NavigateTo">@item.Title</a>
            Else
                @<span>@item.Title</span>
                @<div Class="dropdown-content">
                    @For Each subitem In item.Children
                        @<div>
                            <a href="@subitem.NavigateTo">@subitem.Title</a>
                        </div>
                    Next
                </div>
            End If
        </div>
    Next

    <a href="#" class="VerticalEllipsis" title='@Mvc.ContextHelper.Tradueix("ver más opciones", "veure mes opcions", "see more options")'>
        &#8942;
    </a>


</div>
