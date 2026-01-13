@ModelType List(Of DTOAeatMod347Item)
@Code
    ViewBag.Title = "Modelo 347"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

End Code

<div class="pagewrapper">

        <div>
            <div class="PageTitle">@ContextHelper.Tradueix("Modelo", "Model", "Form") 347</div>
             <p>
                 @ContextHelper.Tradueix("No nos constan operaciones declarables devengadas durante este ejercicio.",
                                                       "No ens consten operacions declarables devengades durant aquest exercici.",
                                                       "No commercial operations elligible for this declaration form.")
            </p>
            <p>
                 @ContextHelper.Tradueix("Si cree que se trata de un error, por favor póngase en contacto con nuestras oficinas.",
                                                                                   "Si creu que es tracta de una errada si us plau posis en contacte amb les nostres oficines.",
                                                                                   "If you feel we might be wrong please contact our offices.")
             </p>
        </div>

</div>

@Section Styles
    <link href="~/Media/Css/Aeat.css" rel="stylesheet" />
End Section

