@ModelType DTO.DTORep
@Code
    ViewData("Title") = "err"
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oLang As DTO.DTOLang = oUser.Lang
    Dim items As List(Of DTO.DTORepObj) = BLL.BLLRepObjs.All(Model)
End Code

<div class="widepagewrapper">
    <div class="TableRow">
        <div class="TableCellConcept">@oLang.Tradueix("Concepto", "Concepte", "Concept")</div>
        @For iMonth = 1 To 12
            @<div class="TableCell">
                @oLang.MesAbr(iMonth)
            </div>
        Next
    </div>

    @For year As Integer = Today.Year To 2017 Step -1

        @<div Class="YearRow">
            @year
        </div>

        @<div Class="TableRow">
            <div Class="TableCellConcept">@oLang.Tradueix("Volumen", "Volum", "Volume")</div>
            @For iMonth = 1 To 12
                @<div class="TableCell">
                    @BLL.BLLRepObjs.Volum(items, year, iMonth).Formatted
                </div>
            Next
        </div>


        @For Each oCluster As DTO.DTOCustomer.Clusters In BLL.BLLRepObjs.Clusters(items)
            @<div Class="TableRow">
                <div Class="TableCellConcept">Cluster @BLL.BLLRepObjs.ClusterString(oCluster)</div>
                @For iMonth = 1 To 12
                    @<div class="TableCell">
                        @BLL.BLLRepObjs.ActiveOutlets(items, year, iMonth, oCluster)
                    </div>
                Next
            </div>
        Next
    Next



</div>



@Section Styles
    <style>
        .widepagewrapper {
            font-size: 12px;
        } 
        .YearRow {
            margin-top: 20px;
            font-weight:700;
        } 
        .TableCellConcept {
            width: 100px;
            display: inline-block;
        }
        .TableCell {
            width:60px;
            display:inline-block;
            text-align:right;
        }

    </style>
End Section

