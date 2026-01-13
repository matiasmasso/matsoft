@ModelType DTOWebErr
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>@ViewBag.Title</h1>

@lang.Tradueix("Lamentamos informarle que se ha producido un error al intentar mostrar esta página.",
                                                                                 "Lamentem informar que s'ha produit un error al intentar mostrar aquesta pàgina.",
                                                                                 "We regret to report an error on trying to display requested page",
                                                                                 "Lamentamos informar que ocorreu um erro ao mostrar a página.")
<br />
@lang.Tradueix("Si el problema persiste al volver a intentarlo, por favor díganos en un email a info@matiasmasso.es qué estaba intentando consultar e intentaremos solventar el problema a la mayor brevedad",
                                                                                 "Si el problema persisteix al tornar a intentar-ho, si us plau diguins en un email a info@matiasmasso.es què estava intentant consultar i procurarem solventar el problema quan abans",
                                                                                 "If the issue remains after checking it again, please report it to info@matiasmasso.es and we'll try to fix it at soonest",
                                                                                 "Volte a tentar, e se o problema persiste, por favor informe-nos a info@matiasmasso.pt que consulta deseja realizar e trabalharemos para resolver o problema o mais rapidamente possível.")
<br />
@lang.Tradueix("Rogamos disculpe las molestias", "Disculpi les molesties", "Sorry for any inconveniences", "Rogamos aceite as nossas desculpas pelo inconveniente.")

