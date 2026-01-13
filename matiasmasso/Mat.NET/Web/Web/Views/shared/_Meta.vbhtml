<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="keywords" content="Britax,Römer,Bob,4moms,silla auto,cochecito,puericultura" />
<meta name='description' content='@Html.Raw(ViewBag.MetaDescription)' />
<meta name="author" content="Matías Massó" />
<meta name="dcterms.rightsHolder" content="MATIAS MASSO, S.A. 2014" />
<meta name="theme-color" content="#FFFFFF" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta property="fb:app_id" content="489736407757151" />
<meta name="p:domain_verify" content="13bcdbb547c8929f79d9ce38c689bbfa" /> <!--verificació Pinterest-->

<link rel="icon" type="image/x-icon" href="/favicon.png" />
<link rel="apple-touch-icon" href="/touch-icon-iphone.png">
<link rel="apple-touch-icon" sizes="76x76" href="/touch-icon-ipad.png">
<link rel="apple-touch-icon" sizes="120x120" href="/touch-icon-iphone-retina.png">
<link rel="apple-touch-icon" sizes="152x152" href="/touch-icon-ipad-retina.png">

<!--<link rel="author" href="https://plus.google.com/112446272600871467182" />-->

@If ViewBag.Canonical IsNot Nothing Then
    If TypeOf ViewBag.Canonical Is DTOUrl Then
        Dim oUrl = CType(ViewBag.Canonical, DTOUrl)
        If oUrl.CanonicalUrl(DTOLang.ESP()) > "" Then
            @<link rel="alternate" href="@oUrl.CanonicalUrl(DTOLang.ESP)" hreflang="@DTOLang.ESP.ISO6391()" />
            @<link rel="alternate" href="@oUrl.CanonicalUrl(DTOLang.CAT)" hreflang="@DTOLang.CAT.ISO6391()" />
            @<link rel="alternate" href="@oUrl.CanonicalUrl(DTOLang.POR)" hreflang="@DTOLang.POR.ISO6391()" />
            @<link rel="alternate" href="@oUrl.AbsoluteUrl(ViewBag.Lang)" hreflang="x-default" />
            @<link rel='canonical' href="@oUrl.CanonicalUrl(ViewBag.Lang)" />
        End If
        'ElseIf TypeOf ViewBag.Canonical Is DTOProductUrl.Collection Then
        'Dim oUrls = CType(ViewBag.Canonical, DTOProductUrl.Collection)
        'If oUrls.Canonical(DTOLang.ESP) > "" Then
        '@<link rel="alternate" href="@oUrls.Canonical(DTOLang.ESP, True)" hreflang="@DTOLang.ESP.ISO6391()" />
        '@<link rel="alternate" href="@oUrls.Canonical(DTOLang.CAT, True)" hreflang="@DTOLang.CAT.ISO6391()" />
        '@<link rel="alternate" href="@oUrls.Canonical(DTOLang.POR, True)" hreflang="@DTOLang.POR.ISO6391()" />
        '@<link rel="alternate" href="@oUrls.Url(ViewBag.Lang, True)" hreflang="x-default" />
        '@<link rel="canonical" href="@oUrls.Url(ViewBag.Lang, True)" />
        'End If
    End If
End If





