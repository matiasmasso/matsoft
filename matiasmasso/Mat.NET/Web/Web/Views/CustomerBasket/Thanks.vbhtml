@ModelType DTOPurchaseOrder
@Code

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim sEmailAddress As String = DTOUser.GetEmailAddress(oUser)
End Code

<div>
    <p>
        @Mvc.ContextHelper.Tradueix("Gracias por utilizar nuestro servicio online.", "Gracies per utilitzar el nostre servei online", "Thanks for using our online service")
    </p>
    <p>
        @Mvc.ContextHelper.Tradueix("Acabamos de enviarle una confirmación a " & sEmailAddress & ".", "Acabem de enviar-li un correu de confirmació a " & sEmailAddress & " .", "A confirmation message has just been sent to " & sEmailAddress & " .")
    </p>
    <p>
        @Mvc.ContextHelper.Tradueix("Su pedido ha sido registrado con el número ", "La seva comanda ha estat registrada amb el numero ", "Your order has been successfully registered with number ")
        <b>@Model.Num</b>
    </p>
    <p>
        @Mvc.ContextHelper.Tradueix("Puede consultarlo en cualquier momento en", "Pot consultar-lo en qualsevol moment en", "Browse it at any time on")
        <a href="/pedidos">www.matiasmasso.es/pedidos</a>
    </p>
    <p>
        @Mvc.ContextHelper.Tradueix("En breve nos pondremos en contacto para concertar el envío.", "En breu contactarem per concertar l'enviament.", "You'll be contacted for shipment details.")
    </p>

 

</div>
