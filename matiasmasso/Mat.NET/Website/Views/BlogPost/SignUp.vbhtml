@ModelType LoginViewModel
@Code
    Layout = "~/Views/Shared/_Layout_Blog.vbhtml"
    Dim oUser = ContextHelper.GetUser()
End Code


<div class="PageWrapperVertical">
    <h1>@ViewBag.Title</h1>

    <div class="SISU"
         data-lang="@ContextHelper.Lang().Tag"
         data-email="@DTOUser.GetEmailAddress(oUser)"
         data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
         data-src="@CInt(Model.Src)">
    </div>

    <div id="SignUp_Thanks"></div>

</div>

@Section Styles
    <link href="~/Media/Css/SISU.css" rel="stylesheet" />

    <style>
        .PageWrapperVertical {
            max-width:450px !important;
        }
    </style>
End Section

@Section Scripts
    <script src="~/Scripts/SignInOrSignUp.js"></script>
    <script>
        var returnUrl = '@Model.ReturnUrl';

        $(document).ready(function () {
            SISU_SignInOrSignUp($('.SISU'), '');
            $(document).on('SISU_Authenticated', function (e, argument) {
                $('#SISU_RowEmail').hide();
                var div = $('#SignUp_Thanks');
                var url = '/blog/signedUp';
                var data = { 'returnUrl': returnUrl };
                div.append(spinner);
                $('#SignUp_Thanks').load(url, data, function () {
                    spinner.remove();
                })
            });
        });
    </script>

End Section


