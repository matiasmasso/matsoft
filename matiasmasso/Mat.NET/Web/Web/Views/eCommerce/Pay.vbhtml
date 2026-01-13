@ModelType DTO.DTOBasket
@Code
    Layout = "~/Views/Shared/_Layout_eCommerce.vbhtml"
    ViewData("eComPage") = DTO.Defaults.eComPages.Pay
End Code

<div class="pagewrapper">

    <div class="Epigraf"></div>

    <div class="Grid">
        <div class="Row" >
            <div class="CellEpigraf">mis datos personales:</div>
        </div>
        <div class="Row" id="Email">
            <div class="CellLabel">Email</div>
            <div class="CellForm">
                <input type="email" />
                <div class="Bubble" hidden="hidden">
                    Verifique su buzón, acabamos de enviarle la contraseña<br />
                    Si no lo encuentra verifique la bandeja de correo no deseado.
                </div>
            </div>
        </div>

            <div class="Row" hidden="hidden" id="Nom">
                <div class="CellLabel">Nombre</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>

            <div class="Row" hidden="hidden" id="CogNoms">
                <div class="CellLabel">Apellidos</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Tel">
                <div class="CellLabel">Teléfono</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden">
                <div class="CellEpigraf">datos de envío:</div>
            </div>
            <div class="Row" hidden="hidden" id="Destinatario">
                <div class="CellLabel">Destinatario</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Address">
                <div class="CellLabel">Direccion</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Pais">
                <div class="CellLabel">Pais</div>
                <div class="CellForm">
                    <select style="width:104px; height:21px;">
                        <option value="ES" selected>España</option>
                        <option value="PT">Portugal</option>
                        <option value="AD">Andorra</option>
                    </select>
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Zip">
                <div class="CellLabel">Código postal</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Location">
                <div class="CellLabel">Población</div>
                <div class="CellForm">
                    <input type="text" />
                </div>
            </div>
            <div class="Row" hidden="hidden">
                <div class="CellEpigraf">datos de facturación:</div>
            </div>
            <div class="Row" hidden="hidden" id="NIF">
                <div class="CellLabel">NIF</div>
                <div class="CellForm">
                    <input type="text" placeholder=" (opcional)" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="FactNom">
                <div class="CellLabel">Razón Social</div>
                <div class="CellForm">
                    <input type="text" placeholder=" (sólo si no va a mi nombre)" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="FactAddress">
                <div class="CellLabel">Direccion</div>
                <div class="CellForm">
                    <input type="text" placeholder=" (sólo si es distinta de la de envío)" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="FactLocation">
                <div class="CellLabel">Población</div>
                <div class="CellForm">
                    <input type="text" placeholder=" (sólo si es distinta de la de envío)" />
                </div>
            </div>
            <div class="Row" hidden="hidden" id="Submit">
                <div class="CellLabel"></div>
                <div class="CellSubmit">
                    <a href="" class="Button">Aceptar</a>
                </div>
            </div>
    </div>


</div>

@Section Scripts
    <script>
        $('.CellSubmit .Button').click(function () {
            var url = '@Url.Action("Update")'
            $.post(url, function (result) {
                url = '@Url.Action("Ok")'
                window.location.href = url;
            });
        });
    </script>
    
End Section

@Section Styles
    <style>
        .Grid {
    margin:5px auto;
}
    .CellLabel {
        display:table-cell;
        width:100px;
        padding:0 5px 0 10px;
    }
    .CellForm {
        position:relative;
        display:table-cell;
        min-width:0;
        width:100%;
        padding-right:10px;
    }
    .CellForm input[type=email], .CellForm input[type=text] {
        width:100%;
    }
    #Tel input[type=text], #Zip input[type=text], #NIF input[type=text]  {
        max-width:100px;
    }

    .CellAux {
        display:table-cell;
        min-width:0;
        padding:0 10px 0 10px;
    }
    .CellSubmit {
        display:table-cell;
        min-width:0;
        width:100%;
        text-align:right;
        padding-top:10px;
    }
    .CellEpigraf {
        padding:20px 5px 5px 10px;
        max-width:100px;
        color:darkgray;
        white-space:nowrap;
        overflow:visible;
    }
    .Button {
        display:inline-block;
        height:21px;
        width:70px;
        border: 1px solid #fff;
        text-decoration:none;
        color: #616161;

    }
    .Button:hover {
        color:blue;
    }



    /*------------- Speech Bubble -------------------*/


    .Bubble {
      position: absolute; 
      left:15px;
      bottom:23px;
      border: 1px solid darkgray;
      padding: .5em 1em;
      width: auto;
      height: auto;
      background-color: #fcf4af;
      -moz-border-radius:10px;
      -webkit-border-radius:10px;
      border-radius:5px;    
      font-size: .86em;
    }

    .Bubble:after 
        {
        content: '';
        position: absolute;
        border-style: solid;
        border-width: 5px 10px 0; /*arrow height width 0 */
        border-color: #fcf4af transparent;
        display: block;
        width: 0;
        z-index: 1;
        bottom: -5px; /* -arrow height*/
        left: 37px; /* arrow horizontal position */
    }

    .Bubble:before 
        {
        content: '';
        position: absolute;
        border-style: solid;
        border-width: 6px 11px 0; /*Bubble:after border-width +=1 */
        border-color: #7F7F7F transparent;
        display: block;
        width: 0;
        z-index: 0;
        bottom: -6px; /*Bubble:after bottom -=1 */
        left: 36px; /*Bubble:after left -=1 */
        }

    </style>
End Section
