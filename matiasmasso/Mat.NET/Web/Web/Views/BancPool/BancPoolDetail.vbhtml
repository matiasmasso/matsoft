@ModelType DTO.DTOBank
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim exs As New List(Of Exception)
    FEBL.Bank.Load(Model, exs)
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim sTitle As String = oWebSession.Tradueix("Pool Bancario", "Pool Bancari", "Bank Pool") & " " & Model.RaoSocial
    ViewData("Title") = sTitle & " | M+O"
    Dim items As List(Of DTO.DTOBancPool) = BLL.BLLBancPools.All(Model)

End Code

<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>

    @If items.Count = 0 Then
        @<div>
            @oWebSession.Tradueix("No existen datos", "No existeixen dades", "No data is found")
        </div>
    Else

        @<div Class="Grid">

            <div Class="RowHdr">
                <div Class="CellTxt">
                    @oWebSession.Tradueix("Fecha", "Data", "Date")
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


            @For Each item In items
                @<div class="Row">
                    <div Class="CellTxt">
                        @item.Fch.ToShortDateString
                    </div>
                    <div Class="CellNum">
                        @if item.ProductCategory = DTO.DTOBancPool.ProductCategories.Credit_Pur Then
                            @<span>@DTO.DTOAmt.CurFormatted(item.Amt)</span>
                        Else
                            @<span>&nbsp;</span>
                        End If
                    </div>
                    <div Class="CellNum">
                        @if item.ProductCategory = DTO.DTOBancPool.ProductCategories.Credit_Comercial Then
                            @<span>@DTO.DTOAmt.CurFormatted(item.Amt)</span>
                        Else
                            @<span>&nbsp;</span>
                        End If
                    </div>
                    <div Class="CellNum">
                        @if item.ProductCategory = DTO.DTOBancPool.ProductCategories.Prestecs Then
                            @<span>@DTO.DTOAmt.CurFormatted(item.Amt)</span>
                        Else
                            @<span>&nbsp;</span>
                        End If
                    </div>
                    <div Class="CellNum">
                        @if item.ProductCategory = DTO.DTOBancPool.ProductCategories.Avals Then
                            @<span>@DTO.DTOAmt.CurFormatted(item.Amt)</span>
                        Else
                            @<span>&nbsp;</span>
                        End If
                    </div>
                </div>
            Next
        </div>

    End If

    <div class="BackLink">
        <a href="/BancPool">
            @oWebSession.Tradueix("(volver al pool bancario global)", "(tornar al pool bancari global)", "(back to global bank pool)")
        </a>
    </div>
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

        .BackLink {
            margin-top:15px;
        }
            
    </style>
End Section