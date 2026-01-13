<!--
Per utilitzar-ho cal:
    - importar StoreLocator.Css
    - importar StoreLocator.Js
-->

@Code
    Dim lang = If(ViewBag.Lang IsNot Nothing, ViewBag.Lang, ContextHelper.Lang)
End Code

<div class="storelocator">
    <div class="offline">
        <h3 id="DondeComprar">@lang.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")</h3>
        <div>
            <select id="dropdownCountry">
                <option value="" label='@lang.Tradueix("pais", "pais", "country", "pais")'>
                        @lang.Tradueix("(seleccionar pais)", "(tria un pais)", "(select a country)", "(selecione um pais)")
                </option>
            </select>
        </div>
        <div>
            <select id="dropdownZona">
                <option value="" label='@lang.Tradueix("zona", "zona", "region", "área")'>
                    @lang.Tradueix("(seleccionar zona)", "(tria una zona)", "(select a region)", "(selecione uma área)")
                </option>
            </select>
        </div>
        <div>
            <select id="dropdownLocation">
                <option value="" label='@lang.Tradueix("población", "població", "location", "cidade")'>
                    @lang.Tradueix("(seleccionar población)", "(tria una població)", "(pick a location)", "(selecione uma cidade)")
                </option>
            </select>
        </div>
        <div id="divDistributors">
        </div>
    </div>

    <div class="online">
        <h3>@lang.Tradueix("Comprar ahora en...", "Comprar ara a...", "Buy now at...")</h3>
        <div class="landingPages" />
        <input type="hidden" id="inStock" value='@lang.Tradueix("en stock para entrega inmediata", "en stock per entrega inmediata", "in stock for immediate delivery")' />
    </div>
</div>


