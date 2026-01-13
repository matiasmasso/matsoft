@ModelType DTOWebDomain
@Code
    Layout = Nothing
End Code

# robots.txt for @Me.Request.Url.Host

# disallow all
User-agent: *
# Disallow: /
Crawl-delay: 600

# but allow only important bots
User-agent: Googlebot
User-agent: Googlebot-Image
User-agent: Mediapartners-Google
User-agent: Bingbot
User-agent: msnbot
User-agent: msnbot-media
User-agent: Slurp
User-agent: Yahoo-Blogs
User-agent: Yahoo-MMCrawler
User-Agent: DuckDuckBot
User-agent: Baiduspider
User-agent: YandexBot
User-agent: Exabot
User-agent: Facebot
User-agent: ia_archiver

# disallow all files in these directories
Disallow: /account/
Disallow: /condiciones/
Disallow: /doc/*
Disallow: /doc?*
Disallow: /dox/*
Disallow: /documentacion/
Disallow: /downloads/ # privat
Disallow: /home/ # privat
Disallow: /signup/
Disallow: /registro/
Disallow: /Subscripciones/
Disallow: /pro/ # profesionals
Disallow: /tarifas/
Disallow: /pvp/
Disallow: /facturas/
Disallow: /retenciones/
Disallow: /stocks/
Disallow: /apertura/
Disallow: /aperturas/
Disallow: /pedido/
Disallow: /incentivos/
Disallow: /incidencia/
Disallow: /bancs/
Disallow: /email/
Disallow: /product/
Disallow: /comments/
Disallow: /webservices/ # temp
Disallow: /wp/
Disallow: /nuk/
Disallow: /nomad/
Disallow: /Inglesina/

Disallow: /wDoc.aspx?id=*

# disallow all files ending with these extensions
# Disallow: /*.js$
# Disallow: /*.css$
Disallow: /*.aspx$

# allow google image bot to search all images
# User-agent: Googlebot-Image
# Disallow:
# Allow: /*.gif$
# Allow: /*.png$
# Allow: /*.jpeg$
# Allow: /*.jpg$
# Allow: /*.ico$
# Allow: /*.jpg$
# Allow: /images/

Sitemap: @Html.Raw(DTOSiteMap.Url(Model))

