@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oUser = ContextHelper.FindUserSync()
End Code

<div class="pagewrapper">
    <div class="PageTitle">Atención:</div>
    <p>
        Esta página está en mantenimiento, esperamos volver a publicarla en breve
    </p>
    <p>
        Hemos tomado nota de avisarle en cuanto esté disponible al siguiente correo:
    </p>
    <p class="myemail">
        @DTOUser.GetEmailAddress(oUser)
    </p>
    <p>
        Si está intentando cursar un pedido, puede hacerlo por email a info@matiasmasso.es o via telefónica (al tel. 932541522 de lunes a jueves de 09:00 a 18:00 y viernes de 09:00 a 14:00)
    </p>
    <p>
        Rogamos disculpe las molestias
    </p>
</div>

@Section Styles
    <style>
        .myemail {
            margin:20px;
            font-weight:700;
            color:red;
        }
    </style>
End Section
