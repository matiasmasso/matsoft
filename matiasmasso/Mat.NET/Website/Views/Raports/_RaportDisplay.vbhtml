@ModelType DTOMem
@Code
    
End Code

<div id="Customer">
    @ContextHelper.Tradueix("cliente", "client", "customer")
    <div>@Model.Contact.Nom</div>
</div>
<br/>
<div id="User">
    @ContextHelper.Tradueix("redactado por", "redactat per", "written by")
    <div>
        @Model.UsrLog.UsrCreated.Nom
    </div>
</div>
<br/>
<div class="Fch">
    @ContextHelper.Tradueix("fecha de visita", "data de visita", "visit date")
    <div>
        @DTO.GlobalVariables.Today().ToString("yyyy-MM-dd")
    </div>
</div>
<br/>
<div>
    <textarea disabled="disabled">
        @Model.Text
    </textarea>
</div>


