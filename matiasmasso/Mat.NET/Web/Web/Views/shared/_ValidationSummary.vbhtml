@ModelType Web.Mvc.ModelStateDictionary

@Code
    
End Code

<style>
    .ValidationSummary {
    }
        .ValidationSummary ul {
            list-style-image: url('/Media/Img/Ico/Warn.gif');
            list-style-position: inside;
        }
            .ValidationSummary ul li {
                color: red;
                padding: 0;
                margin: 0;
            }
</style>

@If Not Model.IsValid Then
    @<div class="ValidationSummary">
        @Html.ValidationSummary(Mvc.ContextHelper.Tradueix("Por favor corrija los siguientes errores:", "Si us plau esmeni les següents errades:", "Please correct next errors:"))
    </div>
End If