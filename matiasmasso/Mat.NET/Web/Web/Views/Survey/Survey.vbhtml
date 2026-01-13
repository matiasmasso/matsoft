@Modeltype DTO.DTOSurveyParticipant
@Code
    Layout = "~/Views/Shared/_Layout_fullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    BLL.BLLSurvey.Load(Model.Survey)
End Code


<div class="pagewrapper">
    <div class="Title">
        @Model.Survey.Title
    </div>

    <div class="DataCollection">

        <div class="Excerpt">
            @Model.Survey.Text
        </div>

        @For Each oStep As DTO.DTOSurveyStep In Model.Survey.Steps
            @<div Class="Step">
                @oStep.Title
            </div>
            @For Each oQuestion As DTO.DTOSurveyQuestion In oStep.Questions
                @<div Class="Question">
                    @oQuestion.Text
                </div>

                @For Each oAnswer As DTO.DTOSurveyAnswer In oQuestion.Answers
                    @<div Class="Answer">
                        <label>
                            @if BLL.BLLSurveyParticipant.IsAnswered(Model, oAnswer) Then
                                @<input type = "radio" value="@oAnswer.Guid.ToString" name="@oQuestion.Guid.ToString" checked="checked" />
                            Else
                                @<input type="radio" value="@oAnswer.Guid.ToString" name="@oQuestion.Guid.ToString"  />
                            End If
                            @oAnswer.Text
                        </label>
                    </div>

                Next

            Next

        Next

        <input type="hidden" id="Participant" value="@Model.Guid.ToString" />
        <input type="hidden" id="Survey" value="@Model.Survey.Guid.ToString" />
        <input type="hidden" id="User" value="@Model.User.Guid.ToString" />

        <div class="TextareaObs">
            Comentarios opcionales:
            <textarea id="TextareaObs" cols="40" rows="3">@Model.Obs</textarea>
        </div>

        <div Class="SurveySubmit">
            <input type="button" value="enviar" />
        </div>
    </div>


    <div id="Thanks" hidden="hidden">
        Gracias por su colaboración
    </div>

    <div id="Error" hidden="hidden">
        Lamentamos informarle que ha ocurrido un error al registrar sus respuestas.
        <br/>
        <span></span>
    </div>
</div>


@Section Styles
    <style scoped>
        .pagewrapper {
            padding: 20px 7px 0 10px;
            max-width: 500px;
            margin: auto;
            text-align: left;
        }

        .Thanks {
            padding: 20px 7px 10px 10px;
            max-width: 500px;
            min-height: 400px;
            margin: auto;
        }

        .Title {
            color:gray;
            font-weight:900;
            font-size: 1.2em;
        }

        .Excerpt {
            padding:20px 0 0 0;
        }

        .Step {
            padding:20px 0 0 0;
            font-weight: 700
        }

        .Question {
            padding:10px 0 5px 20px;
        }

        .Answer {
            padding:0 0 0 50px;
        }

        .Answer input, .Answer label {
            vertical-align:middle;
        }

        .SurveySubmit {
            text-align: center;
        }

        .SurveySubmit input {
            margin-top: 30px;
            width:320px;
            height:44px;
        }

        .TextareaObs {
            padding:20px 0 0 0;
        }
        .TextareaObs textarea {
            margin:5px 0 0 0;
            width:100%;
            font-family: inherit;
            font-size: inherit;
        }
    </style>
End Section


@Section Scripts
    <script>
        $(document).on("click", ".SurveySubmit input", function () {
            var filteredObs = $("#TextareaObs").val().replace(/<.*?>/g, '');

            var json = {
                guid: $("#Participant").val(),
                survey: { guid: $("#Survey").val() },
                user: { guid: $("#User").val() },
                obs: filteredObs,
                answers: checkedAnswers()
            };

            var url = '@Url.Action("/Update")'
            var data = JSON.stringify(json);

            $('.loading').show();
            $.post(url,
                { data: data },
                function (result) {
                    $('.loading').hide();
                    $('.DataCollection').hide();
                    if (result.success) {
                        $('#Thanks').show();
                    }
                    else {
                        $('#Error').show();
                        $('#Error span').html(result.message);
                    }
                });
        });

        function checkedAnswers() {
            var retval = [];
            $(".Answer label input[type=radio]:checked").each(function (index) {
                var guid = $(this).val();
                var answer = {guid: guid}
                retval.push(answer);
            });
            return retval;
        }
    </script>
End Section