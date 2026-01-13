@code
    
    Dim oProduct As DTO.DTOProduct = FEBL.Session.GetLastProductBrowsed(HttpContext.Current)
    'Dim oTwitterWidget As DTO.DTOrrssWidget = FEBL.Product.TwitterWidget(oWebsession.User, oProduct)
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim exs As New List(Of Exception)
    Dim oTwitterWidget = FEBL.SocialMediaWidget.WidgetSync(exs, oUser, DTO.DTOSocialMediaWidget.Platforms.Twitter, oProduct)
    Dim sTwitterUrl As String = DTO.DTOSocialMediaWidget.Url(oTwitterWidget)
End Code

<p>
    <a class="twitter-timeline"
       width="100%"
       height="150"
       href="@sTwitterUrl"
       data-widget-id="@oTwitterWidget.widgetId">
        Tweets por @@@oTwitterWidget.Titular
    </a>
    <script>
        !function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https';
            if (!d.getElementById(id)) {
                js = d.createElement(s);
                js.id = id;
                js.src = p + "://platform.twitter.com/widgets.js";
                fjs.parentNode.insertBefore(js, fjs);
            }
        }(document, "script", "twitter-wjs");</script>
</p>



