@ModelType DTOContactMessage
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

        <h1 class="title">@ViewBag.Title</h1>


        @Using Html.BeginForm()
            @<div>
                <div class="dataRow">
                    <div>
                        <label>@ContextHelper.Tradueix("dirección de correo electrónico", "adreça email", "email address")</label>
                    </div>
                    <div>
                        @Html.TextBoxFor(Function(m) m.Email)
                    </div>
                </div>

                <div class="dataRow">
                    <div>
                        <label>@ContextHelper.Tradueix("nombre", "nom", "name")</label>
                    </div>
                    <div>
                        @Html.TextBoxFor(Function(m) m.Nom)
                    </div>
                </div>

                <div class="dataRow">
                    <div>
                        <label>@ContextHelper.Tradueix("población", "població", "location")</label>
                    </div>
                    <div>
                        @Html.TextBoxFor(Function(m) m.Location)
                    </div>
                </div>

                <div class="dataRow">
                    <div>
                        <label>@ContextHelper.Tradueix("Su consulta", "La seva consulta", "Your request")</label>
                    </div>
                    <div>
                        @Html.TextAreaFor(Function(m) m.Text)
                    </div>
                </div>


                <div class="dataRow" id="submit">
                    <input type="submit" />
                </div>

            </div>
        End Using





@Section Styles
    <style>
        .ContentColumn {
            max-width: 600px;
        }

            .ContentColumn .title {
                color: gray;
                font-size: 1.5em;
                font-weight: 600;
                padding-top: 15px;
            }

            .ContentColumn label {
                color: gray;
            }

            .ContentColumn input[type=text] {
                width: 100%;
                justify-content: stretch;
            }

            .ContentColumn textarea {
                display: flex;
                width: 100%;
                height: 150px;
            }

            .ContentColumn .dataRow {
                padding-top: 15px;
            }

                .ContentColumn .dataRow div {
                }


        #submit {
            display: flex;
            justify-content: flex-end;
            margin-right: 0;
        }

            #submit input {
                padding: 7px 20px;
                margin-right: 0;
                border: 1px solid cornflowerblue;
                border-radius: 5px;
                background-color: cornflowerblue;
                color: white;
            }

                #submit input:hover {
                    background-color: aqua;
                    color: white;
                }
    </style>
End Section

