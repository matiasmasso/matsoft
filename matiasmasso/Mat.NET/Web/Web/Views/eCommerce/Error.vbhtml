@ModelType List(Of Exception)
@Code
    Layout = "~/Views/Shared/_Layout_eCommerce.vbhtml"
End Code

Se ha producido un error
@For Each ex As Exception In Model
    @<p>@ex.Message</p>
Next