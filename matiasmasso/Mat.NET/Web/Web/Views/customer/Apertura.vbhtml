@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
End Code

<style>

</style>

<fieldset class="mat_form" id="mat_form">
    <legend>@Mvc.ContextHelper.Tradueix("Solicitud Apertura Punto de Venta", "Sol.licitut Apertura Punt de Venda", "New Sale Point Request")</legend>

    <ul>
        <li>
            <label for="name">Persona de Contacto:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Razón Social:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Nombre comercial:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">NIF:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Dirección:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Zona:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Código postal:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Población:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Teléfonos de contacto:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">email:</label>
            <input type="email" name="email" placeholder="info@developerji.com" required />
            <span class="form_hint">Formato correcto: "name@something.com"</span>
        </li>
        <li>
            <label for="website">página web:</label>
            <input type="url" name="website" placeholder="http://developerji.com" required pattern="(http|https)://.+" />
            <span class="form_hint">Formato correcto: "http://developerji.com"</span>
        <li>
            <label for="name">superficie aproximada de exposición de productos de puericultura:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">facturación anual prevista en productos de puericultura:</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">peso aproximado de la puericultura en la cifra de su negocio (de cero a cien) :</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Vende algo más que puericultura en su establecimiento? (textil, mobiliario, jugueteria...):</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Cuantos puntos de venta de puericultura gestiona o piensa abrir en breve?</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Pertenece a alguna franquicia, grupo, asociación de comerciantes, etc.?</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Qué antigüedad tiene su negocio?</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">fecha de apertura prevista</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Tiene alguna experiencia previa?</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Por favor seleccione las marcas que está Vd. interesado/a en distribuir</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">¿Comercializa ya o piensa comercializar otras marcas de puericultura? Por favor cite cuales</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="name">Por favor seleccione las marcas que está Vd. interesado/a en distribuir</label>
            <input type="text" placeholder="John Doe" required />
        </li>
        <li>
            <label for="message">Detalle cualquier otra información que considere relevante de su negocio:</label>
            <textarea name="message" cols="40" rows="6" required></textarea>
        </li>
        <li>
            <button class="submit" type="submit">@Mvc.ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit")</button>
        </li>
    </ul>


    <span id="Subscriptions-warn" class="form-warn"></span>

</fieldset>