@ModelType DTOProduct
@Code
    Dim exs As New List(Of Exception)

    FEB.Product.Load(Model, exs)
    Dim sDescription As String = FEB.Product.Description(Model, ContextHelper.lang(), True)
    If sDescription = "" Then sDescription = FEB.Product.Excerpt(Model, ContextHelper.lang())
    Dim sHtmlDescription As String = MatHelperStd.TextHelper.Html(sDescription)
End Code

<div>
    @Html.Raw(sHtmlDescription)
</div>







