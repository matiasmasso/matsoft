@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    ViewBag.Title = "MATIAS MASSO, S.A. - " & ContextHelper.Tradueix("Registro de incidencia postventa", "Registre de incidencia postvenda", "Post sales incidence form", "Registo de Incidência Pós-venda")
    Dim exs As New List(Of Exception)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oCustomers = FEB.User.GetCustomersSync(oUser, exs)
    Dim oActiveCustomers = oCustomers.FindAll(Function(x) x.Obsoleto = False).ToList
End Code






    <h1>@ContextHelper.Tradueix("Registro de Incidencia Postventa", "Registre de Incidencia Postvenda", "Support Incidence Form", "Registo de Incidência Pós-venda")</h1>

    @If oActiveCustomers.Count = 0 Then
        @<div>
            <p>
                @Html.Raw(ContextHelper.Tradueix("El usuario <b>" & oUser.EmailAddress & "</b> no consta vinculado a ninguno de nuestros clientes, por lo que no es posible registrar incidencias de postventa por este medio.",
                                                "L'usuari <b>" & oUser.EmailAddress & "</b> no consta vinculat a cap client, per el que no ens es possible registrar incidecies postvenda per aquest mitjá.",
                                                "User <b>" & oUser.EmailAddress & "</b> is not linked to any of our customers, hence we cannot register a postsales incidence.",
                                                "O usuário <b>" & oUser.EmailAddress & "</b> não está vinculado a nenhumo dos nossos clientes, por este motivo não é possivel registar incidências de pós-venda por este meio."))
            </p><p>
                @Html.Raw(ContextHelper.Tradueix("Si Vd. no es <b>" & oUser.EmailAddress & "</b>, por favor cierre la sesión (arriba a la derecha 'perfil' -> 'desconectar'), seleccione 'acceder' e identifiquese con sus credenciales.",
                                                     "Si vosté no es <b>" & oUser.EmailAddress & "</b>, si us plau tanqui la sessió (a dalt a ma dreta, 'perfil' -> 'desconnectar'), cliqui 'accedir' e identifiqui's amb les seves credencials.",
                                                     "If you are not <b>" & oUser.EmailAddress & "</b>, please close your session (top right menu 'profile' -> 'close') and click on 'login' to identify yourself.",
                                                     "Se você não é o usuário <b>" & oUser.EmailAddress & "</b>, por favor feche a sessão (acima à direita 'perfil' -> 'desconetar'), selecione 'aceder' e identifique-se com a sua contrassenha."))
            </p><p>
                @Html.Raw(ContextHelper.Tradueix("Si Vd. es <b>" & oUser.EmailAddress & "</b> y cree que deberíamos haberle identificado como cliente por favor contacte con nuestras oficinas.",
                                                     "Si vosté es <b>" & oUser.EmailAddress & "</b> y creu que hariem de haver-lo identificat com a client si us plau contacti amb les nostres oficines.",
                                                     "If you are <b>" & oUser.EmailAddress & "</b> and you feel we should have identified you as an active customer please contact our offices.",
                                                     "Se você é o usuário <b>" & oUser.EmailAddress & "</b> e considera que deveríamos tê-lo identificado como cliente por favor contacte com os nossos escritórios."))
            </p>
        </div>
    Else
        @<div>

            <div id="Customer">
                @Html.Partial("_UsuariCustomerSelection")
            </div>

            <div id="Details" @IIf(oActiveCustomers.Count = 1, "", "hidden = 'hidden'") data-customer='@IIf(oActiveCustomers.Count=1,oActiveCustomers(0).Guid.ToString,"")'>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("persona de contacto", "persona de contacte", "contact person", "pessoa de contacto")
                    </div>

                    <div class="control">
                        <input id="contactPerson" type="text" value="@DTOUser.Nom_i_Cognoms(oUser)" maxlength="50" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        email
                    </div>

                    <div class="control">
                        <input id="emailAdr" type="email" value="@oUser.EmailAddress" maxlength="100" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("teléfono de contacto", "telefon de contacte", "contact phone", "telefone de contacto")
                    </div>

                    <div class="control">
                        <input id="tel" type="text" value="@oUser.Tel" maxlength="50" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("su referencia", "la seva referencia", "your reference", “a sua referencia”)
                    </div>

                    <div class="control">
                        <input id="customerRef" type="text" maxlength="50" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("procedencia", "procedencia", "origin", “procedencia”)
                    </div>

                    <div class="control">
                        <select id="procedencia">
                            <option value="1" selected>@ContextHelper.Tradueix("adquirido en mi establecimiento", "adquirit al meu establiment", "purchased on my shop", "adquirido no meu estabelecimento")</option>
                                                                            <option value="2">@ContextHelper.Tradueix("adquirido en otro establecimiento", "adquirit a un altre establiment", "purchased on other shops", "adquirido noutro estabelecimento")</option>
                            <option value="3">@ContextHelper.Tradueix("producto de exposición", "producte d'exposició", "product on display", "produto de exposição")</option>
                        </select>
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("selección de producto", "sel.lecció de producte", "product selection", "seleção de produto")
                    </div>

                    <div class="control">
                        <div id="productBrand">
                            <select>
                                                                                            <option value="" selected="selected">@ContextHelper.Tradueix("(seleccionar marca)", "(sel.leccionar marca)", "(select brand)", "(selecionar marca)")</option>
                                                                                            <!--
                    @@If oActiveCustomers.Count = 1 Then
                        For Each oBrand As DTOGuidNom In FEB.Incidencia.ProductBrandsSync(exs, DTOIncidencia.Procedencias.MyShop, oActiveCustomers(0))
                            @@<option value='@@oBrand.Guid.ToString'>@@oBrand.Nom</option>
                        Next
                    End If
                    -->
                            </select>
                        </div>

                        <div id="productCategory" hidden="hidden">
                            <select>
                                <option value="" selected="selected">@ContextHelper.Tradueix("(seleccionar categoría)", "(sel.leccionar categoría)", "(select category)", “(selecionar categoría)”)</option>
                            </select>
                        </div>

                        <div id="productSku" hidden="hidden">
                            <select>
                                <option value="" selected="selected">@ContextHelper.Tradueix("(seleccionar producto)", "(sel.leccionar producte)", "(select product)", "(selecionar produto)")</option>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("número de serie", "numero de serie", "serial number", "número de serie")
                    </div>

                    <div class="control">
                        <input id="serialNumber" type="text" maxlength="50" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("fecha de fabricación", "data de fabricació", "manufacturing date", “data de fabricação”)
                    </div>

                    <div class="control">
                        <input id="ManufactureDate" type="text" maxlength="20" />
                    </div>
                </div>

                <div class="row">
                    <div class="label">
                        @ContextHelper.Tradueix("descripción de la avería", "descripció de la avería", "malfunctioning description", “descrição da avaria")
                    </div>

                    <div class="control">
                        <textarea id="description" rows="4" placeholder='@ContextHelper.Tradueix("Por favor describa el defecto observado", "Si us plau descrigui el problema amb que s'ha trobat", "Please explain what problem are you facing", “Por favor faça uma breve descrição do que observa”)'></textarea>
                    </div>
                </div>


                <div class="row">
                    <div class="label">
                        <a href="#" class="UploadImgs">
                            @ContextHelper.Tradueix("clic aquí para adjuntar imágenes y/o videos cortos (max 10Mb)", "clic aquí per pujar imatges i/o videos curts (max 10Mb)", "click here to upload images and/or short videos (max 10Mb)", "anexe as suas imagens e/ou videos curtos aqui (max 10Mb)")
                        </a>
                    </div>

                    <input id="fileBoxImgs" type="file" accept="image/*, video/*" multiple />
                    <div id="images" class="control">

                    </div>
                </div>


                <div class="row">
                    <div class="label">
                        <a href="#" class="UploadTicket">
                            @ContextHelper.Tradueix("clic aquí para adjuntar el ticket de compra", "clic aquí per pujar el ticket de compra", "click here to upload purchasing ticket", “anexar talão da compra/fatura aquí”)
                        </a>
                    </div>

                    <input id="fileBoxTicket" type="file" />
                    <div id="ticket" class="control">

                    </div>
                </div>



                <div class="row">
                    <div class="label">
                        <span>
                                            <input type="checkbox" id="acceptTerms" data-warning='@ContextHelper.Tradueix("por favor acepte las condiciones del servicio", "si us plau accepti les condicions de servei", "please accept service terms", “por favor aceite as condições do serviço”)' />
                            @ContextHelper.Tradueix("He leído y acepto las",
                         "He llegit i accepto les",
                         "I've read and I accept",
                         “Li e aceito as”)
                            <a href="https://www.matiasmasso.es/condiciones/sat" target="_blank">
                                @ContextHelper.Tradueix("condiciones del Servicio Técnico",
                        "Condicions del Servei Tècnic",
                        "Post Sales Terms and Conditions",
                        “Condições do Serviço Técnico”)
                            </a>
                        </span>
                    </div>

                    <div id="submit" class="control">

                        <input type="button" class="Submit" value='@ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
                        <div class="Spinner64"></div>
                    </div>
                </div>


            </div>

            <div id="Thanks" hidden="hidden">
                <p>
                    @ContextHelper.Tradueix("Gracias por utilizar nuestro servicio de registro de incidencias online",
                                         "Gràcies per utilitzar el nostre servei de registre de incidències online ",
                                         "Thanks for using our online postsales service",
                                         “Agradecemos tenha registado a sua incidencia através do formulario online”)
                    <br />
                </p>

                <p>
                    @ContextHelper.Tradueix("Nuestro personal de Servicio de Asistencia Técnica se pondrá en contacto con Vd. en breve.",
                           "El nostre personal de Servei d'Assistencia Tecnica es posará en contacte amb vostés en breu",
                           "Our support staff will contact you as soon as possible",
                           "A nossa equipa do Serviço de Assistência Técnica contactará consigo em breve.")
                </p>

                <p>
                    @ContextHelper.Tradueix("Acabamos de enviarle un correo de confirmación.",
                                   "Acabem de enviar-li un missatge de confirmació.",
                                   "We have just sent a confirmation email.",
                                   “Acabamos de enviar-lhe uma confirmação por email”)
                </p>

                <p>
                    @ContextHelper.Tradueix("Si no lo recibe en unos instantes por favor verifique su bandeja de correo no deseado",
                               "Si no el rep en uns instants si us plau revisi la safata de correu no desitjat",
                               "If you don't see it please check your spam box",
                               “Se não a recebe nos próximos minutos, verifique a sua carpeta de entrada de mensagens não desejadas/spam”)
                </p>
            </div>
        </div>
    End If


@Section Styles
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Media/Css/Incidencia.css" rel="stylesheet" />
End Section

@Section Scripts
    <script src="~/Media/js/UsuariCustomerSelection.js"></script>
                                                <script src="~/Media/js/Incidencia.js"></script>
                                                End Section