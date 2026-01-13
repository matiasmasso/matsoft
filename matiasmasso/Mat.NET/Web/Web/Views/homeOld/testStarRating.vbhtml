@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
End Code

<div>
        <table>
            <tr>
                <td>Default stars</td>
                <td><div id="stars-default"><input type=hidden name"rating" /></div></td>
            </tr>
            <tr>
                <td>Green stars with a callback and a preset value</td>
                <td><div id="stars-green" data-rating="3"></div></td>
            </tr>
            <tr>
                <td>Ten hearts!</td>
                <td><div id="stars-herats" data-rating="6"></div></td>
            </tr>
        </table>



</div>

@Section scripts
<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script>
        //the $(document).ready() function is down at the bottom

        (function ($) {

            $.fn.rating = function (method, options) {
                method = method || 'create';
                // This is the easiest way to have default options.
                var settings = $.extend({
                    // These are the defaults.
                    limit: 5,
                    value: 0,
                    glyph: "glyphicon-star",
                    coloroff: "gray",
                    coloron: "gold",
                    size: "2.0em",
                    cursor: "default",
                    onClick: function () { },
                    endofarray: "idontmatter"
                }, options);
                var style = "";
                style = style + "font-size:" + settings.size + "; ";
                style = style + "color:" + settings.coloroff + "; ";
                style = style + "cursor:" + settings.cursor + "; ";



                if (method == 'create') {
                    //this.html('');	//junk whatever was there

                    //initialize the data-rating property
                    this.each(function () {
                        attr = $(this).attr('data-rating');
                        if (attr === undefined || attr === false) { $(this).attr('data-rating', settings.value); }
                    })

                    //bolt in the glyphs
                    for (var i = 0; i < settings.limit; i++) {
                        this.append('<span data-value="' + (i + 1) + '" class="ratingicon glyphicon ' + settings.glyph + '" style="' + style + '" aria-hidden="true"></span>');
                    }

                    //paint
                    this.each(function () { paint($(this)); });

                }
                if (method == 'set') {
                    this.attr('data-rating', options);
                    this.each(function () { paint($(this)); });
                }
                if (method == 'get') {
                    return this.attr('data-rating');
                }
                //register the click events
                this.find("span.ratingicon").click(function () {
                    rating = $(this).attr('data-value')
                    $(this).parent().attr('data-rating', rating);
                    paint($(this).parent());
                    settings.onClick.call($(this).parent());
                })
                function paint(div) {
                    rating = parseInt(div.attr('data-rating'));
                    div.find("input").val(rating);	//if there is an input in the div lets set it's value
                    div.find("span.ratingicon").each(function () {	//now paint the stars

                        var rating = parseInt($(this).parent().attr('data-rating'));
                        var value = parseInt($(this).attr('data-value'));
                        if (value > rating) { $(this).css('color', settings.coloroff); }
                        else { $(this).css('color', settings.coloron); }
                    })
                }

            };

        }(jQuery));

        $(document).ready(function () {

            $("#stars-default").rating();
            $("#stars-green").rating('create', { coloron: 'green', onClick: function () { alert('rating is ' + this.attr('data-rating')); } });
            $("#stars-herats").rating('create', { coloron: 'red', limit: 10, glyph: 'glyphicon-heart' });
        });
</script>
End Section

@Section styles
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" />
End Section