@ModelType DTOProduct
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
    Dim exs As New List(Of Exception)
    Dim items = FEB2.WtbolLandingPages.AllSync(exs, Model)
    'item.Uri.AbsoluteUri
End Code

<p><b>Comprar ahora @Model.FullNom() online</b></p>
<p>
    Los siguientes comercios online son una selección de distribuidores oficiales de la marca, de absoluta confianza, que disponen de este producto en existencia para entrega inmediata:
</p>

@For Each item In items
    @<div class="logos">
        <a href="#" id="Wtbol" data-landing="@item.Guid.ToString" target="_blank" >
            <img src="@FEB2.WtbolSite.logoUrl(item.Site)" alt="@item.Site.Web" width="150" height="48" />
        </a>
    </div>
Next

<p>
    ¿es Vd. distribuidor oficial y desea participar en esta sección?<br />
    Solicítelo en <a href="mailto:info@matiasmasso.es">info@matiasmasso.es</a>
</p>
@Section Scripts
    <script>
        $(document).on('click', '#Wtbol', function (e) {
            event.preventDefault();
            var landing = $(this).data("landing");
           $.getJSON(
                '/wtbol/landingpage',
                {
                    guid: landing
                },
               function (result) {
                   if (result.success) {
                       window.location.assign(result.url);
                   }
                }
            )
        });
    </script>
End Section
@Section Styles
    <style>
        .logos {
            text-align: center;
        }
    </style>
End Section