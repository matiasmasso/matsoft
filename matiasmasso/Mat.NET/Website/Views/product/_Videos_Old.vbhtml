@ModelType          If Model.Count
@Code
    Dim exs As New List(Of Exception)
    
    Dim oMovies As List(Of DTOYouTubeMovie) = FEB.YouTubeMovies.FromProductGuidSync(Model.Guid, ContextHelper.lang(), exs).Where(Function(x) x.Obsoleto = False).ToList
    Dim oPagination As New DTOPagination(oMovies.Count, 4, 1)
    Dim pagesize As Integer = 4
End Code

<div class="video-list-Wrapper">
    <div id="Items">
        @Html.Partial("_VideosPaginated", oMovies.Take(oPagination.PageSize).ToList)
    </div>

    <div id='Pagination' data-paginationurl='@Url.Action("videospageindexchanged")' data-pagesize='@oPagination.PageSize' data-itemscount='@oPagination.ItemsCount' data-guid='@Model.Guid.ToString'></div>
</div>







