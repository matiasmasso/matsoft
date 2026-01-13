@ModelType List(Of DTOEscriptura)
@Code
    
    ViewBag.Title = "Escriptures"
    Layout = "~/Views/shared/_Layout.vbhtml"

    Dim pagesize As Integer = 10
End Code


<div class="pagewrapper">
    <h2>Escriptures</h2>

    @If Model.Count > 0 Then
        @<div>
            <div id="Items">
                @Html.Partial("Escriptures_", Model.Take(pagesize).ToList)
            </div>

            <div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="" data-pagesize='@pagesize' data-itemscount='@Model.Count'></div>
        </div>
    End If

</div>

