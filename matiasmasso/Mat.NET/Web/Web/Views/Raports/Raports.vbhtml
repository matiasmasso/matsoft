@ModelType List(Of DTOMem)
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"

    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Informes de visitas comerciales", "Raports de visites comercials", "Sales visits reports")
    ViewBag.Title = "M+O | " & sTitle
    Dim sToday As String = Format(Today, "yyyy-MM-dd")

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim pagesize As Integer = ViewData("PageSize")
    Dim recordsCount As Integer = ViewData("RecordsCount")
End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>
    @If Model.Count = 0 Then
        @<div>
            @Mvc.ContextHelper.Tradueix("No nos constan visitas registrados", "No ens consten visites registrades", "No visits have been logged")
        </div>
    Else
        @<div id="Items">
                @Html.Partial("Raports_", Model.Take(pagesize))
            </div>
            @<div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="@oUser.Guid.ToString" data-pagesize='@pagesize' data-itemscount='@recordsCount'></div>

    End If
</div>

@Section scripts
<script src="~/Media/js/Pagination.js"></script>
End Section

@Section styles
    <style>
        .UserNickname {
            color:navy;
        }
    </style>
End Section