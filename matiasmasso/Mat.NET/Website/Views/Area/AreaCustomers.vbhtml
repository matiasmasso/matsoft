@ModelType List(Of DTOPeriodProgress)
@Code
    
    Dim oArea As DTOArea = ViewData("Area")
    ViewBag.Title = DTOArea.NomOrDefault(oArea)
    Layout = "~/Views/shared/_Layout.vbhtml"

End Code


    <div class="pagewrapper">

        <div class="PageTitle">@DTOArea.NomOrDefault(oArea)</div>
        <div class="Grid">

            <div class="RowHdr">
                <div class="CellNum Progress">@ContextHelper.Tradueix("Progreso", "Progrés", "Progress")</div>
                <div class="CellTxt">@ContextHelper.Tradueix("Cliente", "Client", "Customer")</div>
                <div class="CellAmt">@ContextHelper.Tradueix("90-0 dias", "90-0 dies", "90-0 days")</div>
                <div class="CellAmt Previous">@ContextHelper.Tradueix("180-90 dias", "180-90 dies", "180-90 days")</div>
            </div>


            @For Each item As DTOPeriodProgress In Model

                @<div class="Row">
                    @Select Case BLL.BLLPeriodProgress.ProgressRating(item)
                        Case BLL.BLLPeriodProgress.rating.Good
                            @<div class="CellNum Progress Good">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                                                Case BLL.BLLPeriodProgress.rating.Regular
                                                    @<div class="CellNum Progress Regular">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                                                                        Case BLL.BLLPeriodProgress.rating.Bad
                                                                            @<div class="CellNum Progress Bad">@BLL.BLLPeriodProgress.ProgressFormatted(item)</div>
                                                                    End Select

                    <div class="CellTxt">
                        <a href="@FEB.Contact.Url(item.Contact)">
                            @DTOCustomer.NomAndNomComercial(item.Contact)
                        </a>
                    </div>

                    <div class="CellAmt">
                        @DTOAmt.CurFormatted(item.PeriodCurrent)
                    </div>
                    <div class="CellAmt Previous">
                        @DTOAmt.CurFormatted(item.PeriodPrevious)
                    </div>

                </div>

            Next

        </div>

        <!--
    <div class="google-maps">
            <img src="@@BLL.BLLGoogleMaps.StaticImageUrl(BLL.BLLPeriodProgress.Contacts(Model))" />
    </div>
        -->
    </div>

@Section Styles
    <link href="~/Media/Css/Tables.css" rel="stylesheet" />

    <style>
        .pagewrapper {
            max-width: 500px;
            margin: auto;
        }

        .PageTitle {
            font-weight:700;
            color:darkgray;
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

            .google-maps img {
        width:100%;
    }

        @@media (max-width:400px) {
            .Progress, .Previous {
                display: none;
            }
        }
    </style>
End Section
