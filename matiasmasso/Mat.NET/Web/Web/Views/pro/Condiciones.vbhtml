@ModelType MaxiSrvr.Faq
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim iOrd As Integer
End Code

<style scoped>
    section {
        max-width:450px;
        margin:auto;
    }
    h1 {
        margin:0 0 2em 0;
        padding-top:0;
        font-size: 1.4em;
        color:#666666;
    }
    h2 {
        margin-top:2.5em;
        font-size: 1.1em;
        color: #666666;
    }
</style>

<section>
<h1>@Html.Raw(Model.Question.GetLangText(oWebSession.Lang))</h1>

@Html.Raw(Model.Answer.GetLangText(oWebSession.Lang).Replace(vbCrLf, "<br />"))

        @For Each oFaq In Model.Children(oWebsession.Rol.Id)
            iOrd += 1

            @<h2>@Html.Raw(iOrd.ToString & ".- ") @Html.Raw(oFaq.Question.GetLangText(oWebsession.Lang)) </h2>
            @<p>
            @Html.Raw(oFaq.AnswerHtml(oWebsession.Lang))
            </p>
        Next
</section>