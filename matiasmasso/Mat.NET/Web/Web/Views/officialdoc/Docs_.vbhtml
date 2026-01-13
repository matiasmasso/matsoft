@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oOfficialDocModel As New MaxiSrvr.OfficialDocModel(CType(ViewBag.guid, Guid))
    Dim oDocs As MaxiSrvr.OfficialDocs = MaxiSrvr.BLL_OfficialDoc.DocsByModel(oOfficialDocModel, MaxiSrvr.App.Current.Emp)
    Dim oPagination = New Mvc.Pagination(oDocs.Count, 8, ViewBag.PageIndex)

End Code



@For i As Integer = oPagination.CurrentPageFirstItem To oPagination.CurrentPageLastItem
    Dim item As MaxiSrvr.OfficialDoc = oDocs(i)
        @<div class="DocsGridItem">
            <a href="@BLL.BLLDocFile.DownloadUrl(item.DocFile, False)">
                <img src="@BLL.BLLDocFile.ThumbnailUrl(item.DocFile)" />
                <div class="truncate">
                    @MaxiSrvr.BLL_OfficialDoc.Ref(item)
                </div>
            </a>
        </div>
    Next


    <div class="pagination">
        @If oPagination.IsDisplayable Then
            @Html.Raw(oPagination.Html)
        End If
    </div>


<script type="text/javascript">
    $(".pagination a[data-pageindex]").click(function (event) {
        event.preventDefault();

        var pageindex = $(this).data('pageindex');
        $('.pagination li.active').removeClass('active');
        $(this).parent().addClass('active');

        var url = '/officialdoc/pageindexchanged'
        var viewname = '@Path.GetFileNameWithoutExtension(Server.MapPath(VirtualPath))';
        $(".paginated").load(url, { returnurl: viewname, guid: '@ViewBag.Guid', pageindex: pageindex });
        
    })
</script>



