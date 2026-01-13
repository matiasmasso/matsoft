@Code 
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div class="info">
    @Mvc.ContextHelper.Tradueix("Gracias por tu consulta. Ha quedado pendiente de moderación y recibirás un correo tan pronto lo respondamos",
                                                         "Gracies per la teva consulta. Ha quedat pendent de moderació i rebràs un correu tant punt la contestem",
                                                         "Thanks for your query. You'll get an email as soon as an answer is available")
</div>

