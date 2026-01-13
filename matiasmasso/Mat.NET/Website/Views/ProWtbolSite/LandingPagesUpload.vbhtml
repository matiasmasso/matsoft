@ModelType DTOWtbolSite
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
End Code

<div>
    En los localizadores de distribuidores de nuestra página web publicamos un número limitado de comercios online en función del producto que el consumidor está consultando en aquel momento, para facilitar la conversión.
    Para poder aparecer como comercio online en la sección "Comprar ahora en..." es indispensable que tengamos registrada la Landing Page de su comercio para ese producto determinado, que es donde dirigiremos al consumidor si seleciona su enlace.

    Registramos tres niveles de Landing Page:

    <p>
        <b>Landing page de marca</b><br />
        Mostrará el logo de la marca y las imágenes y enlaces a los productos de la marca.<br />
        No se admitiran Landing Pages donde aparezcan referencias, enlaces, logotipos o productos que no sean de la marca.
    </p>
    <p>
        <b>Landing page de categoría</b><br />
        Mostrará el logo de la marca y las imágenes y enlaces dirigidos a adquirir los productos de esta categoría.<br />
        Opcionalmente se pueden mostrar imágenes y enlaces para otras categorías de la marca, siempre que salgan en posición menos destacada.<br />
        No se admitiran Landing Pages donde aparezcan referencias, enlaces, logotipos o productos que no sean de la marca.
    </p>
    <p>
        <b>Landing page de producto</b><br />
        Mostrará la imagen del producto, el precio y el enlace para adquirirlo.<br />
        Opcionalmente puede mostrar productos o colores relacionados, siempre menos destacados.<br />
        No se admitiran Landing Pages donde aparezcan referencias, enlaces, logotipos o productos que no sean de la marca.
    </p>
    <p>
        @If Model.LandingPages.Count = 0 Then
            @Html.Raw("En estos momentos no tiene declarada ninguna Landing Page de su comercio online")@<br />
            @<span>En el siguiente enlace puede descargar un Excel con el catálogo de productos disponibles y una columna para declarar las Landing Pages correspondientes.</span>@<br />
        Else
            @Html.Raw(String.Format("En estos momentos tiene declaradas {0} Landing Pages de su comercio online", Model.LandingPages.Count))@<br />
            @<span>En el siguiente enlace puede descargar un Excel con el catálogo de productos disponibles y las Landing Pages que tiene declaradas.</span>@<br />
        End If
        Complete las celdas con las Landing Page de su comercio relacionadas con la marca, categoría o producto de cada fila, elimine las que ya no esten activas y suba el Excel de nuevo para que actualicemos nuestra base de datos.
    </p>

    <a Class="Boton" href="/pro/proWtbolSite/LandingPagesExcel/@Model.Guid.ToString()">Descargar Excel</a>

    Subir el Excel actualizado:<br />
    <input type="file" />
</div>
                                                            
@Section Styles
    <style>
        .MainContent {
        }

    </style>
End Section