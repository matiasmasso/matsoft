@ModelType DTOMem
@Code
    
End Code

<div id="Customer">
    @Mvc.ContextHelper.Tradueix("cliente", "client", "customer")
    <div>@Model.Contact.Nom</div>
</div>
<br/>
<div id="User">
    @Mvc.ContextHelper.Tradueix("redactado por", "redactat per", "written by")
    <div>
        @Model.UsrLog.UsrCreated.Nom
    </div>
</div>
<br/>
<div class="Fch">
    @Mvc.ContextHelper.Tradueix("fecha de visita", "data de visita", "visit date")
    <div>
        @DateTime.Today.ToString("yyyy-MM-dd")
    </div>
</div>
<br/>
<div>
    <textarea disabled="disabled">
        @Model.Text
    </textarea>
</div>


