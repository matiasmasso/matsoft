@ModelType DTOUser.Sources
@Code
    Layout = "~/Views/shared/_Layout_Minimal.vbhtml"
    Dim sEmail As String = Request.QueryString("email")
    
End Code


<div >
    <h1>&nbsp;</h1>

    <div class="SISU"
         data-lang="ESP"
         data-email="@sEmail"
         data-isauthenticated="false"
         data-src="@CInt(Model)">
    </div>
    <div id="Thanks" hidden="hidden">
        <p>Tus datos han sido registrados correctamente.</p>
        <p>Gracias por seguirnos.</p>
    </div>
    <p>&nbsp;</p>
</div>

@Section Scripts
    <script src="~/Media/js/iFrameResizer.ContentWindow.min.js"></script>
    <script>
        $(document).ready(function () {
            SISU_SignInOrSignUp($('.SISU'), '');
            $(document).on('SISU_Authenticated', function (e, argument) {
                $('.SISU').hide();
                $('#Thanks').show();
            });
        });
    </script>
End Section

