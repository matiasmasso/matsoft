<div class="sidenav">
    @foreach (DTO.DTOBox deptBox in Model.SideMenu)
    {
    <div class='@(Model.SideMenuSingleLevel() ? "SingleLevel" : "title")'>
        <a href="@deptBox.NavigateTo">@deptBox.Title</a>
    </div>

    <ul>
        @foreach (DTO.DTOBox filterBox in deptBox.Children)
        {
        <li>
            <span class="subtitle">@filterBox.Title</span>
            <ul>
                @foreach (DTO.DTOBox itemBox in filterBox.Children)
                {
                <li class="unchecked-box" data-tag="@itemBox.Tag">
                    @itemBox.Title
                </li>
                }
            </ul>
        </li>
        }
    </ul>

    }
</div>

