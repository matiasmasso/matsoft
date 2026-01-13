@ModelType List(Of DTOIncentiu)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang As DTOLang = Mvc.ContextHelper.Lang()
End Code

<h1>@ViewBag.Title</h1>



<div>
    @If Model.Count = 0 Then
        @lang.Tradueix("En estos momentos no hay ninguna promoción activa", "En aquests moments no hi ha cap promoció activa", "No promotions currently available")
    Else
        @For Each oIncentiu As DTOIncentiu In Model

            @<a href="@FEB2.Incentiu.Url(oIncentiu)">

                <div class='Cell'>
                    <div>
                        @oIncentiu.title.Tradueix(lang)
                    </div>
                    <div>
                        @oIncentiu.excerpt.Tradueix(lang)
                    </div>
                </div>
            </a>
        Next
    End If
</div>



@Section Styles

    <style scoped>
        .Cell {
            padding:10px 0;
            border-bottom:1px solid gray;
        }

        .Cell div:first-child {
            font-weight:600;
            color:darkgray;
            padding:10px 0;

        }

    </style>
End Section


