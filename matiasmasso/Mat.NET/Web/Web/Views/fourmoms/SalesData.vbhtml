@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="pagewrapper">
    <div class="Fch">
        <input type="date" value='@Format(Today, "yyyy-MM-dd")' />
    </div>

    <div class="Grid">
        @Html.Partial("SalesData_", Today)
    </div>
</div>

@Section Styles
    <style>
        .Fch {
            text-align: right;
            padding: 0 0 20px 0;
        }
    </style>
End Section

@Section Scripts
    <script>
        $(document).on('change', '.Fch input', function (event) {
            var fch = $(this).val();
            $('.loading').show()
            $(".Grid").load('@Url.Action("SalesData_OnFchChange")', { fch: fch }, function (e) {
                $('.loading').hide()
            });
        });
    </script>
End Section

