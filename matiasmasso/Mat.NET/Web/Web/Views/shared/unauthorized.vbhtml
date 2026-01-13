@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


<div class="PageWrapperVertical">
    <h1>@ViewBag.Title</h1>
    <p>
    @Mvc.ContextHelper.Tradueix("Sus credenciales no le autorizan a acceder a la página solicitada.",
                                                         "Les seves credencials tenen autoritzat l'acces a la página solicitada.",
                                                         "Your credentials are not authorized to access requested address.")
</p><p>
    @Mvc.ContextHelper.Tradueix("Verifique que la sesión se haya abierto con su perfil.", _
                                             "Verifiqui que la sesió s'hagi obert amb el seu perfil.", _
                                             "Check current session has been logged in with your profile.")
</p><p>
    @Mvc.ContextHelper.Tradueix("Si cree que debería tener acceso por favor contacte con nuestras oficinas", _
                                             "Si té raons per creure lo contrari si us plau contacti amb les nostres oficines", _
                                             "If you feel you should access it, please contact our offices")
</p>
</div>

@Section Styles
    <style>
        .ContentColumn {
            max-width: 400px;
            margin: 0 auto;
        }
    </style>
End Section