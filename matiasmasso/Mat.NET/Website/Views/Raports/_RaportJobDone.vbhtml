@ModelType List(Of DTOMem)

@Code
    
End Code

@If Model.Count > 0 Then

    @<span>@ContextHelper.Tradueix("raports de hoy:", "raports d'avui:", "today raports:")</span>

    @<p>
        @For Each oMem As DTOMem In Model
            @<div class="truncate">
                <a href="@oMem.url()">
                    @Format(oMem.UsrLog.FchCreated, "HH:mm")
                </a>
                <span>
                    @oMem.Contact.Nom
                </span>
            </div>
        Next
    </p>
Else
    @<div>
        @ContextHelper.Tradueix("Los informes de visita son importantes para conocer el estado del mercado.", "Els raports de visita son importants per coneixer l'estat del mercat.", "Visit raports are important to know about market status.")
    <br/>    
    @ContextHelper.Tradueix("Por favor no olvideis mencionar en ellos:", "Si us plau no oblideu mencionar-hi:", "Please don't forget to mention:")
        <ul>
            <li>
                @ContextHelper.Tradueix("El nombre de la persona o personas que nos han atendido.", "El nom de les persones que us han atés.", "The name of the person/s who have attended you.")
            </li>
            <li>
                @ContextHelper.Tradueix("Si la visita tenía algún objetivo especial (implantación o formación de nuevo producto, promociones, gestión de impagos...)", "Si la visita tenía algun objectiu especial (implantació o formació de nou producte, promocions, gestió de impagaments...)", "If the visit had any especial objective (training, new products, promotions, payment recoveries...)")
            </li>
            <li>
                @ContextHelper.Tradueix("Exposición de nuestras marcas respecto a la competencia. Qué marcas trabaja mejor que las nuestras.", "Exposició de les nostres marques respecte a la competencia. Quines marques treballa millor que les nostres.", "Exposition. What brands do they exhibit better than ours.")
            </li>
            <li>
                @ContextHelper.Tradueix("Si mantiene expositores nuestros o si ha retirado alguno.", "Si manté expositors nostres o si n'ha retirat algun", "If they keep our POS materials or they have removed any")
            </li>
            <li>
                @ContextHelper.Tradueix("Cualquier otro comentario relevante", "Qualsevol altre comentari rellevant", "Any other relevant comments")
            </li>
        </ul>
    </div>
End If

