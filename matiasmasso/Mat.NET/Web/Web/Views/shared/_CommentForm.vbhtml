@Code
    Dim lang = Mvc.ContextHelper.Lang
End Code


<div class="CommentForm">
    <div>
        <textarea rows="5" id="CommentFormTextArea"></textarea> <!--id for 'label for'-->
    </div>

    <div class="EmailRequest" hidden>
        <span class="Info">
            @lang.Tradueix("Por favor deja un correo para avisarte cuando publiquemos la respuesta", "Si us plau deixa un correu per avisar-te quan publiquem la resposta", "Please leave us an email to notify you as soon as the answer is available")
        </span>

        <div class="SubmitComplexRow">
            <input type="email"
                   class="EmailAddress"
                   placeholder='@lang.Tradueix("dirección email", "adreça email", "email address")' />
            <button class="Submit">@lang.Tradueix("Enviar", "Enviar", "Submit")</button>
        </div>

        <div class="Warning EmailEmpty" hidden>
            @lang.Tradueix("La dirección email no puede estar vacía")
        </div>

    </div>

    <div class="Info DeliveringEmail" hidden>
        @lang.Tradueix("Un momento por favor, le estamos enviando la contraseña por correo...")
    </div>

    <div class="Info EmailDelivered" hidden>
        @lang.Tradueix("Acabamos de enviarte una contraseña, por favor verifica tu correo ý cópiala en la casilla siguiente para verificar tu identidad")
    </div>

    <div class="PasswordRequest" hidden>
        <span class="Info EmailFound">
            @lang.Tradueix("Por favor introduce tu contraseña. Si no la sabes o no la recuerdas, deja la casilla en blanco y dale a Aceptar; te la enviaremos al instante por correo")
        </span>
        <div class="SubmitComplexRow">

            <input type="password"
                   class="Password"
                   placeholder='@lang.Tradueix("contraseña", "clau de pas", "password")'
                   value="" />
            <button class="Submit">@lang.Tradueix("Enviar", "Enviar", "Submit")</button>

        </div>
        <span class="Warning WrongPassword" hidden>
            @lang.Tradueix("Contraseña incorrecta. Si no la sabes o no la recuerdas, deja la casilla en blanco y dale a Aceptar; te la enviaremos al instante por correo.")
        </span>
    </div>


    <div class="NicknameRequest" hidden>
        <span class="info">
            @lang.Tradueix("Por último, indícanos un alias para publicarlo como autor del comentario",
                    "Per últim, indicans un alies per publicar-lo com autor del comentari",
               "As last task, please leave us a nickname to publish your comment")
        </span>
        <div class="SubmitComplexRow">
            <input type="text"
                   class="Nickname"
                   placeholder='@lang.Tradueix("alias")'
                   value="" />
            <button class="Submit">@lang.Tradueix("Enviar", "Enviar", "Submit")</button>
        </div>
    </div>

    <div class="Warning SysError" hidden>
        @lang.Tradueix("Lamentamos comunicarle que se ha producido un error de sistema")
    </div>


    <div class="SubmitRow">
        <button class="Submit">@lang.Tradueix("Enviar", "Enviar", "Submit")</button>
    </div>

    <div class="Info Submitted" hidden>
        @lang.Tradueix("Gracias por tu comentario, en breve recibirás tu respuesta")
    </div>
</div>
