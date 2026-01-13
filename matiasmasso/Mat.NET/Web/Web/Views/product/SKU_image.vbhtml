@ModelType DTOProductPageQuery
@Code
    Layout = "~/Views/Shared/_Layout_Minimal.vbhtml"
    
    Dim ImageUrl As String = FEB2.UrlHelper.Image(DTO.Defaults.ImgTypes.Art, Model.Product.Guid)
    ViewBag.Title = Model.Product.FullNom() & " | " & Mvc.ContextHelper.Tradueix("imagen ampliada", "imatge ampliada", "zoom image")
End Code


    <div>
        <img src="@ImageUrl" alt="@ViewBag.Title"/>
    </div>

@Section Styles
    <style>
        img {
            width:100%;
        }
    </style>
End Section