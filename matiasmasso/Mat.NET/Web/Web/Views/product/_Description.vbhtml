@ModelType DTOProduct
@Code
    Dim exs As New List(Of Exception)

    FEB2.Product.Load(Model, exs)
    Dim sDescription As String = FEB2.Product.Description(Model, Mvc.ContextHelper.lang(), True)
    If sDescription = "" Then sDescription = FEB2.Product.Excerpt(Model, Mvc.ContextHelper.lang())
    Dim sHtmlDescription As String = MatHelperStd.TextHelper.Html(sDescription)
End Code

<div>
    @Html.Raw(sHtmlDescription)
</div>







