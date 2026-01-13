@ModelType List(Of DTOCompactNode)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim hiddenCountries As New List(Of DTOCompactNode)
    If Model.Count > 10 Then hiddenCountries = Model.Where(Function(x) Not DTOCountry.Collection.Favorites.Any(Function(y) y.Guid.Equals(x.Guid))).ToList()

End Code

<h1>@lang.tradueix("Contactos", "Contactes", "Contacts")</h1>

@If Model.Count = 0 Then
    @<div>
        @lang.Tradueix("No se han encontrado contactos para este usuario", "No s'han trobat contactes per aquest usuari", "No contacts found for current user")
    </div>

Else

    @If hiddenCountries.Count > 0 Then
        @<a href="#" Class="LessCountries">(@lang.tradueix("ver sólo los paises habituales", "nomes els països habituals", "see just usual countries"))</a>
    End If

    @<div>
        @For each country In Model
            @<a href="#" class='Atlas @IIf(hiddenCountries.Contains(country), "HiddenCountry", "Country")' data-guid="@country.Guid.ToString()">@country.Nom</a>
            @For each zona As DTOCompactNode In country.Items
                @<a href="#" class="Atlas Zona" data-guid="@zona.Guid.ToString()" data-parent="@country.Guid.ToString">@zona.Nom</a>
                @For each location As DTOCompactNode In zona.Items
                    @<a href="#" class="Atlas Location" data-guid="@location.Guid.ToString()" data-parent="@zona.Guid.ToString">@location.Nom</a>
                    @For each contact As DTOCompactNode In location.Items
                        @<a href="Contacto/@contact.Guid.ToString()" class="Atlas Contact" data-parent="@location.Guid.ToString">@contact.Nom</a>
                    Next
                Next
            Next
        Next

        @If hiddenCountries.Count > 0 Then
            @<a href="#" Class="MoreCountries">(@lang.tradueix("ver todos los paises", "veure tots els paisos", "see all countries"))</a>
            @<a href="#" Class="LessCountries">(@lang.tradueix("ver sólo los paises habituales", "nomes els països habituals", "see just usual countries"))</a>
        End If
    </div>

End If

@Section Styles
    <style scoped>
        .Atlas {
            padding: 4px 7px 2px 0px;
        }

        .MoreCountries {
            display: block;
            padding: 4px 7px 2px 0px;
        }

        .LessCountries {
            display: none;
            padding: 4px 7px 2px 0px;
        }

        .Country {
            display: block;
        }

        .HiddenCountry {
            display: none;
        }

        .Zona {
            display: none;
            padding-left: 30px;
        }

        .Location {
            display: none;
            padding-left: 60px;
        }

        .Contact {
            display: none;
            padding-left: 90px;
        }

        @@media screen and (max-width:450px) {
            .Atlas {
                padding: 15px 7px 10px 0px;
                border-bottom:1px solid gray;
            }
        }
    </style>

End Section

@Section Scripts
    <script>
        var selectedCountry;
        var selectedZona;
        var selectedLocation;
        var seeMoreCountries = false;

        $(document).ready(function (e) {
            if ($('.Country').length == 1) {
                selectedCountry = $('.Country')[0];
                expand(selectedCountry);
            }
        });

        $(document).on('click', '.Country, .Zona, .Location', function (e) {
            event.preventDefault();
            isSelected($(this)) ? collapse($(this)) : expand($(this));
        })

        $(document).on('click', '.MoreCountries', function (e) {
            event.preventDefault();
            seeMoreCountries = true
            displaycountries();
        })

        $(document).on('click', '.LessCountries', function (e) {
            event.preventDefault();
            seeMoreCountries = false
            displaycountries();
        })


        function isSelected(element) {
            var guid = getGuid(element);
            var retval = (guid == getGuid(selectedCountry) || guid == getGuid(selectedZona) || guid == getGuid(selectedLocation) );
            return retval;
        }

        function expand(element) {
            select(element)
            collapseAll()
            displayHeaders()
            displayChildren(element)
            displaycountries()
        }

        function collapse(element) {
            deselect(element)
            collapseAll()
            displayHeaders()
            displaySiblings(element)
            displaycountries()
        }

        function select(element) {
            if ($(element).hasClass('Country') || $(element).hasClass('HiddenCountry')) {
                selectedCountry = element;
            } else if ($(element).hasClass('Zona')) {
                selectedZona = element;
            } else if ($(element).hasClass('Location')) {
                selectedLocation = element;
            }
        }

        function deselect(element) {
            if ($(element).hasClass('Country') || $(element).hasClass('HiddenCountry')) {
                selectedCountry = null;
                selectedZona = null;
                selectedLocation = null;
            } else if ($(element).hasClass('Zona')) {
                selectedZona = null;
                selectedLocation = null;
            } else if ($(element).hasClass('Location')) {
                selectedLocation = null;
            }
        }

        function displayHeaders() {
            if (selectedCountry != null) {
                $(selectedCountry).css('display', 'block');
            }
            if (selectedZona != null) {
                $(selectedZona).css('display', 'block');
            }
            if (selectedLocation != null) {
                $(selectedLocation).css('display', 'block');
            }
         }



        function getGuid(element) {
            var retval = $(element).data('guid');
            return retval;
        }

        function displayChildren(element) {
            var guid = $(element).data('guid');
            $('[data-parent=' + guid + ']').css('display', 'block');
        }

        function displaySiblings(element) {
            if ($(element).hasClass('Country') || $(element).hasClass('HiddenCountry')) {
                $('.Country').css('display', 'block');
            } else {
                var parent = $(element).data('parent');
                $('[data-parent=' + parent + ']').css('display', 'block');
            }
        }


        function displaycountries() {
            if (selectedCountry) {
                $('.MoreCountries').css('display', 'none')
                $('.LessCountries').css('display', 'none')
            } else {
                if (seeMoreCountries) {
                    $('.MoreCountries').css('display', 'none')
                    $('.LessCountries').css('display', 'block')
                    $('.HiddenCountry').css('display', 'block');
                    $('.Country').css('display', 'none');
                } else {
                    $('.MoreCountries').css('display', 'block')
                    $('.LessCountries').css('display', 'none')
                    $('.HiddenCountry').css('display', 'none');
                    $('.Country').css('display', 'block');
                }
            }
        }


        function collapseAll() {
            $('.Atlas').css('display', 'none');
        }


    </script>
End Section