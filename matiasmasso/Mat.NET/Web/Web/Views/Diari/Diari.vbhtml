@ModelType DTODiari
@Code
    Dim exs As New List(Of Exception)
    ViewBag.Title = "Diari"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim years As List(Of DTODiariItem) = FEB2.Diari.Years(Model, exs)
    Dim months As List(Of DTODiariItem) = FEB2.Diari.Months(Model, exs)
    Dim days As List(Of DTODiariItem) = FEB2.Diari.Days(Model, exs)

    Dim BlShow = FEB2.PurchaseOrders.ExistsSync(exs, Model.User, Today.AddYears(-2), Today)
End Code

@If BlShow Then



    @<div Class="Grid">

        <div Class="RowHdr">
            <div Class="CellTxt Epigraf">
                @Model.Lang.Tradueix("Ejercicios", "Exericis", "Exercises")
            </div>
            <div class="CellAmt">
                @Model.Lang.Tradueix("Totales", "Totals", "Totals")
            </div>
            @If Model.DisplayableBrandsCount > 0 Then
                For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                    @<div class="CellAmt">
                        @oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)
                    </div>
                Next
                @<div class="CellAmt">
                    @Model.Lang.Tradueix("Otras", "Altres", "Others")
                </div>
            End If
        </div>

        @For i = 0 To Math.Min(2, years.Count - 1)
            @<div class="Row">
                <div class="CellTxt Epigraf">
                    <a href="@FEB2.Diari.Url(years(i))">
                        @years(i).Text
                    </a>
                </div>
                <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Total))
                </div>

                @If Model.DisplayableBrandsCount > 0 Then
                    For j As Integer = 0 To Model.DisplayableBrandsCount - 1
                        @<div class="CellAmt">
                            @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Values(j)))
                        </div>
                    Next
                    @<div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Resto))
                    </div>
                End If

            </div>
        Next i

        <div class="RowHdr">
            <div class="CellTxt Epigraf">&nbsp;</div>
            <div class="CellAmt">&nbsp;</div>

            @If Model.DisplayableBrandsCount > 0 Then
                For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                    @<div class="CellAmt">&nbsp;</div>
                Next
                @<div class="CellAmt">&nbsp;</div>
            End If
        </div>

        <div class="RowHdr">
            <div class="CellTxt Epigraf">
                @Model.Lang.Tradueix("Meses", "Mesos", "Months")
            </div>
            <div class="CellAmt">
                @Model.Lang.Tradueix("Totales", "Totals", "Totals")
            </div>

            @If Model.DisplayableBrandsCount > 0 Then
                For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                    @<div class="CellAmt">
                        @oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)
                    </div>
                Next
                @<div class="CellAmt">
                    @Model.Lang.Tradueix("Otras", "Altres", "Others")
                </div>
            End If

        </div>

        @For i As Integer = 0 To months.Count - 1
            @<div class='Row'>
                <div class="CellTxt Epigraf">
                    <a href="@FEB2.Diari.Url(months(i))">
                        @months(i).Text
                    </a>
                </div>
                <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Total))
                </div>

                @If Model.DisplayableBrandsCount > 0 Then
                    For j As Integer = 0 To Model.DisplayableBrandsCount - 1
                        @<div class="CellAmt">
                            @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Values(j)))
                        </div>
                    Next
                    @<div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Resto))
                    </div>
                End If

            </div>
        Next

        <div class="RowHdr">
            <div class="CellTxt Epigraf">&nbsp;</div>
            <div class="CellAmt">&nbsp;</div>

            @If Model.DisplayableBrandsCount > 0 Then
                For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                    @<div class="CellAmt">&nbsp;</div>
                Next
                @<div class="CellAmt">&nbsp;</div>
            End If
        </div>


        <div class="RowHdr">
            <div class="CellTxt Epigraf">
                @(Model.Year & " " & Model.Lang.Mes(Model.Month))
            </div>
            <div class="CellAmt">
                @Model.Lang.Tradueix("Totales", "Totals", "Totals")
            </div>

            @If Model.DisplayableBrandsCount > 0 Then
                For Each oBrand As DTOProductBrand In Model.DisplayableBrands
                    @<div class="CellAmt">
                        @oBrand.Nom.Tradueix(Mvc.ContextHelper.Lang)
                    </div>
                Next
                @<div class="CellAmt">
                    @Model.Lang.Tradueix("Otras", "Altres", "Others")
                </div>
            End If
        </div>

        @For i As Integer = 0 To days.Count - 1
            @<div class='Row'>
                <div class="CellTxt Epigraf">
                    <a href="@FEB2.Diari.Url(days(i))">
                        @days(i).Text
                    </a>
                </div>
                <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Total))
                </div>

                @If Model.DisplayableBrandsCount > 0 Then
                    For j As Integer = 0 To Model.DisplayableBrandsCount - 1
                        @<div class="CellAmt">
                            @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Values(j)))
                        </div>
                    Next
                    @<div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Resto))
                    </div>
                End If

            </div>
        Next

    </div>


Else
    @<div class="EmptyDataWarning">
        <img src="~/Media/Img/Ico/warn.gif" />
        <span>@Model.User.Lang.Tradueix("historial en blanco", "historial en blanc", "Missing historic data")</span>
    </div>
End If



@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style>
        .EmptyDataWarning {
            max-width: 320px;
            margin: 20px auto;
            text-align: center;
        }

            .EmptyDataWarning img, .EmptyDataWarning span {
                vertical-align: middle;
            }

        .Epigraf {
            min-width: 100px;
        }
    </style>
End Section
