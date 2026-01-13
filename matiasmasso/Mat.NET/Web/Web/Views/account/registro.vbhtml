@ModelType DTOUser.Sources
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
End Code


    <h1>@ViewBag.Title</h1>

    <div class="SISU"
         data-lang="@Mvc.ContextHelper.Lang().Tag"
         data-email="@DTOUser.GetEmailAddress(oUser)"
         data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
         data-src="@CInt(Model)">
    </div>

    <div id="Thanks" hidden="hidden">
        <p>@lang.Tradueix("Tus datos han sido registrados correctamente.", "Les teves dades han estat registrades correctament.", "Your user details have been successfully signed up")</p>
        <p>@lang.Tradueix("Para acceder a ellos haz clic en el menu 'Perfil'", "Pots accedir-hi fent clic al menu 'Perfil'", "You may access them through your Profile menu")</p>
        <p>@lang.Tradueix("Gracias por seguirnos.", "Gracies per seguir-nos", "Thanks for following-us")</p>

             <!--facebook pixel event -->
             <script>
                 fbq('track', 'CompleteRegistration');
             </script>
         </div>


     @Section Styles
         <link href="~/Media/Css/SISU.css" rel="stylesheet" />
     End Section

     @Section Scripts
         <script src="~/Scripts/SignInOrSignUp.js"></script>
         <script>
             $(document).ready(function () {
                 SISU_SignInOrSignUp($('.SISU'), '');
                 $(document).on('SISU_Authenticated', function (e, argument) {
                     window.location.href = '/account/signedUp'
                 });
             });
         </script>

     End Section


