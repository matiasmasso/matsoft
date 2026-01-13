@ModelType List(Of DTO.DTOBancPool)
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim sTitle As String = oWebSession.Tradueix("Pool Bancario", "Pool Bancari", "Bank Pool")
    ViewData("Title") = sTitle & " | M+O"
    Dim Query = Model.GroupBy(Function(g) New With {Key g.Bank.RaoSocial, Key g.Bank.Guid}).
        Select(Function(group) New With {group.Key.RaoSocial, group.Key.Guid})
End Code

<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>

        @If Query.Count = 0 Then
            @<div>
                @oWebSession.Tradueix("No existen datos", "No existeixen dades", "No data is found")
            </div>
        Else

            @<div Class="Grid">

                <div Class="RowHdr">
                    <div Class="CellTxt">
                        @oWebSession.Tradueix("Entidad", "Entitat", "Entity")
                    </div>
                    <div Class="CellNum">
                        @oWebSession.Tradueix("Crédito puro", "Credit pur", "Pure credit")
                    </div>
                    <div Class="CellNum">
                        @oWebSession.Tradueix("Crédito comercial", "Credit comercial", "Commercial credit")
                    </div>
                    <div Class="CellNum">
                        @oWebSession.Tradueix("Prestamos", "Prestecs", "Loans")
                    </div>
                    <div Class="CellNum">
                        @oWebSession.Tradueix("Avales", "Avals", "Guarantees")
                    </div>
                </div>


                @For Each item In Query
                    @<div class="Row" >
                         <div Class="CellTxt">
                             <a href="@(New DTO.DTOBank(item.Guid)).UrlDetail">
                                 @item.RaoSocial
                             </a>
                         </div>
                         <div Class="CellNum">
                             @DTO.DTOAmt.CurFormatted(DTO.DTOBancPool.Total(Model, item.Guid, 1))
                         </div>
                         <div Class="CellNum">
                             @DTO.DTOAmt.CurFormatted(DTO.DTOBancPool.Total(Model, item.Guid, 2))
                         </div>
                         <div Class="CellNum">
                             @DTO.DTOAmt.CurFormatted(DTO.DTOBancPool.Total(Model, item.Guid, 3))
                         </div>
                         <div Class="CellNum">
                             @DTO.DTOAmt.CurFormatted(DTO.DTOBancPool.Total(Model, item.Guid, 4))
                         </div>
                    </div>
                Next
            </div>

        End If
    </div>


@Section Styles
 <style>
    .pagewrapper {
        max-width: 800px;
        margin: auto;
    }


    .Grid {
        width: 100%;
        margin: auto;
    }

    .CellNum {
        max-width: 130px;
        min-width: 130px;
    }
</style>
End Section