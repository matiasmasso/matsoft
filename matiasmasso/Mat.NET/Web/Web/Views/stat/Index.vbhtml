@ModelType MaxiSrvr.Stat

@Code
    Layout = "~/Views/Shared/_Layout_Minimal.vbhtml"

    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    
    Dim oStatYears As MaxiSrvr.Stat = MaxiSrvr.StatLoader.Years(oWebSession.Lang)
    Dim oStatMonths As MaxiSrvr.Stat = MaxiSrvr.StatLoader.Months(oStatYears.SelectedKey, oWebSession.Lang)
    Dim oStatDays As MaxiSrvr.Stat = MaxiSrvr.StatLoader.Days(oStatYears.SelectedKey, oStatMonths.SelectedKey, oWebSession.Lang)
End Code

<style scoped>

    .tablewrap {
        width:920px;
        margin:0 auto 20px;
    }

    .row {
        display: inline-block;
    }

    .row div {
        display: inline-block;
        font-size:0.7em;
        width: 80px;
        text-align: right;
    }

    .row div:first-child {
        text-align: left;
    }

    .row.active {
        background-color:navy;
        color:white;
    }

    .row:hover {
        background-color:yellow;
    }


</style>

<div id="statYears" class="tablewrap">
    @Html.Partial("_Table", oStatYears)
</div>

<div id="statMonths" class="tablewrap">
    @Html.Partial("_Table", oStatMonths)
</div>

<div id="statDays" class="tablewrap">
    @Html.Partial("_Table", oStatDays)
</div>

    <script type="text/javascript">
        $('#statYears .row').click(function (event) {
            event.preventDefault();
        $(this).addClass('active');
        $(this).siblings().removeClass('active');
        var year = $(this).data('key');
        var url = '@Url.Action("Months")';
        $('#statMonths').load(url, { 'year': year });
    });

        $('#statMonths a.row').click(function (event) {
            event.preventDefault();
            alert('Hi!');
            /*
            alert(activeYear());
            $(this).addClass('active');
            $(this).siblings().removeClass('active');
            var year = $(this).data('key');
            var url = 'Url.Action("Months")';
            $('#statMonths').load(url, { 'year': year });
            */
        });

        function activeYear() {
            var retval = $('#statYears a.row.active').data('key');
            return retval;
        }

</script>