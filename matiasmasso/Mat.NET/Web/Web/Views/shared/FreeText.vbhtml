@ModelType DTOTxt

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

@Html.Raw(Model.ToHtml(lang))

