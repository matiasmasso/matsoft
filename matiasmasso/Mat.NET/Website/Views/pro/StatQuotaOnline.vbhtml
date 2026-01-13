@ModelType System.Collections.Generic.List(Of DTOStatQuotaOnline)

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    

End Code

<style scoped>
    .PageContainer {
        width:500px;
        margin:auto;
    }
    .PageRow {
    }
    .PageCellYear {
        display:inline-block;
        width:60px;
        text-align:center;
    }
    .PageCellQuarter {
        display:inline-block;
        width:60px;
        text-align:center;
    }
    .PageCellBase {
        display:inline-block;
        width:120px;
        text-align:right;
    }
    .PageCellOnline{
        display:inline-block;
        width:120px;
        text-align:right;
    }
    .PageCellShare{
        display:inline-block;
        width:90px;
        text-align:right;
    }
</style>

<div class="PageContainer">

    <div class="PageRow">
        <div class="PageCellYear">
            @ContextHelper.lang().Tradueix("año", "any", "year")
        </div>
        <div class="PageCellQuarter">
            @ContextHelper.lang().Tradueix("trimestre", "trimestre", "quarter")
        </div>
        <div class="PageCellBase">
            @ContextHelper.lang().Tradueix("base","base","base")
        </div>
        <div class="PageCellOnline">
            @ContextHelper.lang().Tradueix("online", "online", "online")
        </div>
        <div class="PageCellShare">
            @ContextHelper.lang().Tradueix("cuota", "quota", "share")
        </div>
    </div>

    <br />

    @For Each oQuota As DTOStatQuotaOnline In Model
        @<div class="PageRow">
             <div class="PageCellYear">
                 @oQuota.Year
             </div>
             <div class="PageCellQuarter">
                 @oQuota.Quarter
             </div>
             <div class="PageCellBase">
                 @String.Format("{0:c}", oQuota.baseImponible.Eur)
             </div>
             <div class="PageCellOnline">
                 @String.Format("{0:c}", oQuota.Online.Eur)
             </div>
             <div class="PageCellShare">
                 @String.Format("{0:P2}", DTOStatQuotaOnline.Share(oQuota))
             </div>
        </div>
    Next
</div>
