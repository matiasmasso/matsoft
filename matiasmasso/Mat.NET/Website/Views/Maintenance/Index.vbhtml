@Code
    ViewData("Title") = "MATIAS MASSO, S.A."
    Layout = "~/Views/shared/_LayoutMaintenance.vbhtml"
    Dim lang = ContextHelper.GetLang()
End Code

<div class="jumbotron">
    <h1>@lang.Tradueix("Página en mantenimiento", "Pàgina en mantenimient", "Website under maintenance", "Página em manutenção")</h1>
    <p>
        @lang.Tradueix(
                              "En estos momentos estamos realizando trabajos de mantenimiento, por favor acepte nuestras discupas",
                              "En aquests moments estem portan a terme treballs de manteniment, si us plau accepti les nostres disculpes",
                              "We are currently working on the website maintenance, please accept our apologies",
                              "Aceite as nossas desculpas pelo inconveniente, neste momento estamos a realizar trabalhos de manutenção, para melhorar a experiência com o nosso site."
                              )
    </p>
 </div>
