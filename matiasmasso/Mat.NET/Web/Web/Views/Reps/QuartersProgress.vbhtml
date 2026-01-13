@ModelType List(Of DTO.DTOPeriodProgress)
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = oWebSession.Tradueix("Mis representantes", "Els meus representants", "My reps")
    Layout = "~/Views/shared/_Layout.vbhtml"

End Code


<div class="pagewrapper">

    <div class="Grid">

        <div class="Row">
            <div class="CellNum">progrés</div>
            <div class="CellTxt">Rep/Comercial</div>
            <div class="CellAmt">90-0 dies</div>
            <div class="CellAmt">180-90 dies</div>
        </div>


        @For Each item As DTO.DTOPeriodProgress In Model

            @<div class="Row">
                @Select Case BLL.BLLPeriodProgress.ProgressRating(item)
                    Case BLL.BLLPeriodProgress.rating.Good
                        @<div class="CellNum Good">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                    Case BLL.BLLPeriodProgress.rating.Regular
                        @<div class="CellNum Regular">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                    Case BLL.BLLPeriodProgress.rating.Bad
                        @<div class="CellNum Bad">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                End Select

                 <div class="CellTxt">
                     <a href="@FEBL.Contact.Url(item.Contact.Guid)">
                         @item.Contact.Nom
                     </a>
                 </div>

                 <div class="CellAmt">
                     @DTO.DTOAmt.CurFormatted(item.PeriodCurrent)
                 </div>
                 <div class="CellAmt">
                     @DTO.DTOAmt.CurFormatted(item.PeriodPrevious)
                 </div>

            </div>

        Next

    </div>
</div>

@Section Styles
    <link href="~/Media/Css/Tables.css" rel="stylesheet" />

    <style>
        .pagewrapper {
            max-width:500px;
            margin:auto;
        }
        .Good {
            color: green;
        }

        .Regular {
            color: orange;
        }

        .Bad {
            color: red;
        }

               @@media (max-width:400px) {
        .CellAmt {
            display:none;
        }
    }
    </style>
End Section