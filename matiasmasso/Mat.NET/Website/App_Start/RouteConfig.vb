Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Private sGuidConstrain As String = "\b[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b"
    Private sBrandsConstrain As String = FEB.ProductBrands.RoutingConstraint(Website.GlobalVariables.Emp)
    Private sLangsConstrain As String = "es|ca|en|pt|esp|cat|eng|por"

    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")


        'routes.MapRoute(
        'name:="Maintenance",
        'url:="{*url}",
        'Defaults:=New With {.controller = "Maintenance", .action = "Index", .id = UrlParameter.Optional}
        ')


        routes.MapRoute(
    name:="SepaCoreMandatoManual",
    url:="SepaCoreMandatoManual",
    defaults:=New With {.controller = "SepaCoreMandatoManual", .action = "Index"}
)


        routes.MapRoute(
    name:="StaffLogs_Logs",
    url:="StaffLogs/{action}",
    defaults:=New With {.controller = "StaffLogs", .action = "Index"}
)

        routes.MapRoute(
            name:="software",
            url:="software",
            defaults:=New With {.controller = "SoftwareDownloads", .action = "Index"}
        )

        routes.MapRoute(
            name:="pdfThumbnail",
            url:="pdf/thumbnail",
            defaults:=New With {.controller = "pdf", .action = "thumbnail"}
        )

        routes.MapRoute(
            name:="JornadaLaboral",
            url:="JornadaLaboral/{action}",
            defaults:=New With {.controller = "JornadaLaboral", .action = "LogStart"}
        )

        routes.MapRoute(
            name:="atlas",
            url:="atlas",
            defaults:=New With {.controller = "Atlas", .action = "Index"}
        )

        routes.MapRoute(
            name:="sitemaps",
            url:="sitemaps/{id}",
            defaults:=New With {.controller = "Sitemaps", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Error",
            url:="Error",
            defaults:=New With {.controller = "Error", .action = "Index"}
        )

        routes.MapRoute(
            name:="WebErrs",
            url:="weberrs/{action}",
            defaults:=New With {.controller = "WebErrs", .action = "Index"}
        )

        routes.MapRoute(
            name:="apple-app-site-association",
            url:="apple-app-site-association",
            defaults:=New With {.controller = "apple-app-site-association", .action = "Index"}
        )



        routes.MapRoute(
            name:="Feed",
            url:="Feed/{action}",
            defaults:=New With {.controller = "Feed", .action = "Index"}
            )

        routes.MapRoute(
            name:="Computer",
            url:="Computer/{guid}",
            defaults:=New With {.controller = "Computer", .action = "Index", .guid = UrlParameter.Optional}
            )

        routes.MapRoute(
            name:="Comments Feed",
            url:="Comments/Feed",
            defaults:=New With {.controller = "Feed", .action = "Comments"}
            )


        routes.MapRoute(
            name:="PostComment",
            url:="PostComment/{id}",
            defaults:=New With {.controller = "PostComment", .action = "Index", .id = UrlParameter.Optional}
            )

        routes.MapRoute(
            name:="MediaResource",
            url:="MediaResource/{action}/{id}",
            defaults:=New With {.controller = "MediaResource", .action = "Index", .id = UrlParameter.Optional}
            )


        routes.MapRoute(
            name:="Catalog",
            url:="Catalog/{action}/{id}",
            defaults:=New With {.controller = "Catalog", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Gen",
            url:="Gen",
            defaults:=New With {.controller = "Gen", .action = "Index"}
        )

        routes.MapRoute(
            name:="Old main Pro",
            url:="Pro",
            defaults:=New With {.controller = "Pro", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Pro",
            url:="Pro/{controller}/{action}/{id}/{cod}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional, .cod = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="Old Pro",
            url:="Pro/{action}",
            defaults:=New With {.controller = "Pro", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="FaceLift",
            url:="FaceLift/{*CatchAll}",
            defaults:=New With {.controller = "Home", .action = "FaceLift", .CatchAll = UrlParameter.Optional}
        )




        routes.MapRoute(
            name:="Default",
            url:="",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        )
        'namespaces:=New String() {"Mat.Mvc"}



        '-------------------------------------------------------------------------
        'robots & sitemaps
        '-------------------------------------------------------------------------
        routes.MapRoute(
            name:="Robots",
            url:="robots.txt",
            defaults:=New With {.controller = "Robots", .action = "Index"}
        )



        '-------------------------------------------------------------------------
        routes.MapRoute(
            name:="SwitchLang",
            url:="{lang}",
            defaults:=New With {.controller = "Lang", .action = "Switch"},
            constraints:=New With {.Lang = sLangsConstrain}
        )

        routes.MapRoute(
            name:="Lang",
            url:="lang",
            defaults:=New With {.controller = "Lang", .action = "Index"}
        )

        routes.MapRoute(
            name:="LangSettings",
            url:="LangSettings",
            defaults:=New With {.controller = "LangSettings", .action = "Index"}
       )

        routes.MapRoute(
            name:="Unsubscribe",
            url:="Unsubscribe/{subscription}/{user}",
            defaults:=New With {.controller = "Unsubscribe", .action = "Index", .subscription = UrlParameter.Optional, .user = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="Descatalogados",
            url:="Descatalogados",
            defaults:=New With {.controller = "Descatalogados", .action = "Index"}
       )

        routes.MapRoute(
            name:="Descatalogados excel",
            url:="Descatalogados/Excel/{fchFrom}",
            defaults:=New With {.controller = "Descatalogados", .action = "Excel", .fchFrom = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="DescatalogadosReload",
            url:="Descatalogados/{action}/{fchFrom}/{pageIdx}",
            defaults:=New With {.controller = "Descatalogados", .action = "Reload", .fchFrom = UrlParameter.Optional, .pageIdx = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="DescatalogadosSubscribe",
            url:="Descatalogados/Subscribe/{value}",
            defaults:=New With {.controller = "Descatalogados", .action = "Subscribe", .value = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="Feedback log",
            url:="feedback",
            defaults:=New With {.controller = "Feedback", .action = "Update", .cod = UrlParameter.Optional, .source = UrlParameter.Optional, .nom = UrlParameter.Optional, .score = UrlParameter.Optional}
            )

        routes.MapRoute(
            name:="Feedback saveComment",
            url:="feedback/{action}",
            defaults:=New With {.controller = "Feedback", .action = "Index"}
            )

        routes.MapRoute(
            name:="ProductRank",
            url:="productRank",
            defaults:=New With {.controller = "ProductRank", .action = "Index"}
       )

        routes.MapRoute(
            name:="Cx3",
            url:="cx3/{action}/{id}",
            defaults:=New With {.controller = "cx3", .action = "Index", .id = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="ProductRank Update",
            url:="productRank/update",
            defaults:=New With {.controller = "ProductRank", .action = "update", .period = UrlParameter.Optional, .Zona = UrlParameter.Optional, .brand = UrlParameter.Optional, .unit = UrlParameter.Optional}
       )


        routes.MapRoute(
            name:="Apps",
            url:="apps/{action}",
            defaults:=New With {.controller = "Apps", .action = "Index"}
       )

        routes.MapRoute(
            name:="Test",
            url:="test/{*action}",
            defaults:=New With {.controller = "Test", .action = "Index"}
       )


        routes.MapRoute(
            name:="Plugin",
            url:="plugin/{action}/{id}",
            defaults:=New With {.controller = "Plugin", .action = "Index", .id = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="Blog Feed",
            url:="Blog/Feed",
            defaults:=New With {.controller = "Feed", .action = "Index"}
            )

        routes.MapRoute(
        name:="LangBlog",
        url:="{lang}/blog/{action}",
        defaults:=New With {.controller = "BlogPost", .action = "Index"},
        constraints:=New With {.lang = sLangsConstrain, .action = "(SignUp|SignedUp|About|Consultas|Registro|LastPostsPartial|Posts|Normasdeuso)"}
        )

        routes.MapRoute(
            name:="LangBlog2",
            url:="{lang}/blog/{*Catchall}",
            defaults:=New With {.controller = "BlogPost", .action = "Catchall"},
            constraints:=New With {.lang = sLangsConstrain}
       )

        routes.MapRoute(
            name:="BlogPost",
            url:="blogPost/{guid}",
            defaults:=New With {.controller = "BlogPost", .action = "Index", .guid = UrlParameter.Optional}
       )

        routes.MapRoute(
            name:="Blog action",
            url:="blog/{action}/{returnPostGuid}",
            defaults:=New With {.controller = "BlogPost", .action = "Index", .returnPostGuid = UrlParameter.Optional},
            constraints:=New With {.action = "(SignUp|SignedUp|About|Consultas|Registro|LastPostsPartial|Posts|Normasdeuso)"}
       )

        routes.MapRoute(
            name:="Blog Consulta",
            url:="blog/consulta",
            defaults:=New With {.controller = "BlogPost", .action = "Consulta"}
       )

        routes.MapRoute(
            name:="Blog",
            url:="blog/{*Catchall}",
            defaults:=New With {.controller = "BlogPost", .action = "Catchall"}
       )

        routes.MapRoute(
            name:="BlogSearch",
            url:="BlogSearch/{action}",
            defaults:=New With {.controller = "BlogSearch", .action = "Index"}
        )


        routes.MapRoute(
                name:="ediversa.readpending",
                url:="ediversa/readpending",
                defaults:=New With {.Controller = "Ediversa", .Action = "Index"}
            )
        routes.MapRoute(
                name:="Recall",
                url:="recall/{action}",
                defaults:=New With {.Controller = "Recall", .Action = "Index"}
            )

        routes.MapRoute(
                name:="Rep Neighbours",
                url:="rep/{action}",
                defaults:=New With {.Controller = "Rep", .Action = "Neighbours"}
            )

        routes.MapRoute(
            name:="Rep FromGeoLocation",
            url:="rep/FromGeoLocation/{latitud}/{longitud}",
            defaults:=New With {.controller = "Rep", .action = "FromGeoLocation", .latitud = UrlParameter.Optional, .longitud = UrlParameter.Optional}
        )

        'WhereToBuyOnline
        routes.MapRoute(
                name:="WhereToBuyOnline",
                url:="WhereToBuyOnline/{action}/{guid}",
                defaults:=New With {.Controller = "WhereToBuyOnline", .Action = "ConversionPixel", .guid = UrlParameter.Optional}
            )


        routes.MapRoute(
                name:="OrdersPreview",
                url:="orders/preview",
                defaults:=New With {.Controller = "Forecast", .Action = "OrdersPreview"}
            )

        routes.MapRoute(
            name:="SearchJQuery",
            url:="search/{Action}/{searchKey}",
            defaults:=New With {.controller = "Search", .action = "SearchRequest", .searchKey = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Search",
            url:="search/{searchKey}",
            defaults:=New With {.controller = "Search", .action = "Index"}
        )
        routes.MapRoute(
            name:="SearchBox",
            url:="search/{action}",
            defaults:=New With {.controller = "Search", .action = "Index"}
        )

        routes.MapRoute(
            name:="El Corte Ingles",
            url:="ECI/{action}/{year}/{units}/{brand}/{category}/{sku}",
            defaults:=New With {.controller = "ECI", .action = "index",
                         .year = UrlParameter.Optional,
                         .units = UrlParameter.Optional,
                         .brand = UrlParameter.Optional,
                         .category = UrlParameter.Optional,
                         .sku = UrlParameter.Optional
           }
        )



        routes.MapRoute(
            name:="Store locator",
            url:="StoreLocator/{product}",
            defaults:=New With {.controller = "StoreLocator", .action = "Index", .product = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Store locator Premium",
            url:="StoreLocator/premium/{premiumline}",
            defaults:=New With {.controller = "StoreLocator", .action = "Premium", .premiumline = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Store locator Fetch",
            url:="StoreLocator/Fetch/{product}",
            defaults:=New With {.controller = "StoreLocator", .action = "Fetch", .product = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Store locator Fetch Premium",
            url:="StoreLocator/FetchPremium/{premiumline}",
            defaults:=New With {.controller = "StoreLocator", .action = "FetchPremium", .premiumline = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Store locator Offline FromGeoLocation",
            url:="Product/FromGeoLocation/{product}/{latitud}/{longitud}",
            defaults:=New With {.controller = "Product", .action = "FromGeoLocation", .product = UrlParameter.Optional, .latitud = UrlParameter.Optional, .longitud = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Store locator Offline FromLocation",
            url:="Product/FromLocation/{product}/{location}",
            defaults:=New With {.controller = "Product", .action = "FromLocation", .product = UrlParameter.Optional, .location = UrlParameter.Optional}
        )

        ' routes.MapRoute(
        ' name:="Mueller",
        ' url:="mueller",
        ' Defaults:=New With {.controller = "Mueller", .action = "Index"}
        ' )

        routes.MapRoute(
            name:="Sepa Core Mandato Manual",
            url:="Sepa/Core/mandato/manual/{action}",
            defaults:=New With {.controller = "SepaCoreMandatoManual", .action = "index"}
        )

        routes.MapRoute(
        name:="Dox",
        url:="Dox/{sBase64Params}",
        defaults:=New With {.controller = "Dox", .action = "index", .sBase64Params = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Tutorials_iMat",
        url:="Matsoft/iMat/Tutorials",
        defaults:=New With {.controller = "Tutorials", .action = "iMat"}
    )

        routes.MapRoute(
        name:="TutorialSubject",
        url:="tutorials/{guid}",
        defaults:=New With {.controller = "Tutorials", .action = "index", .guid = UrlParameter.Optional},
        constraints:=New With {.guid = sGuidConstrain}
    )

        routes.MapRoute(
        name:="Outlet",
        url:="Outlet/{action}/{lines}",
        defaults:=New With {.controller = "Outlet", .action = "index", .lines = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Dept",
        url:="Dept/{guid}",
        defaults:=New With {.controller = "Dept", .action = "index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Depts",
        url:="Depts/{brand}",
        defaults:=New With {.controller = "Depts", .action = "fromBrand", .brand = UrlParameter.Optional}
    )


        routes.MapRoute(
        name:="Rep_Retencions",
        url:="RepRetencions",
        defaults:=New With {.controller = "RepRetencions", .action = "index"}
    )


        routes.MapRoute(
        name:="RepComFollowUps",
        url:="RepComFollowUps",
        defaults:=New With {.controller = "RepComFollowUps", .action = "index", .guid = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="DocsMercantils",
        url:="DocsMercantils",
        defaults:=New With {.controller = "DocsMercantils", .action = "index"}
    )


        routes.MapRoute(
        name:="DocsFinanciers",
        url:="DocsFinanciers",
        defaults:=New With {.controller = "DocsFinanciers", .action = "index"}
    )

        routes.MapRoute(
        name:="mayborn",
        url:="mayborn/{action}",
        defaults:=New With {.controller = "mayborn", .action = "index"}
    )

        routes.MapRoute(
        name:="LlibreMajor",
        url:="LlibreMajor/{action}/{guid}",
        defaults:=New With {.controller = "LlibreMajor", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="LlibreDiari",
        url:="LLibreDiari/{action}/{guid}",
        defaults:=New With {.controller = "LlibreDiari", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Ccd",
        url:="Ccd/{cta}/{contact}/{fch}",
        defaults:=New With {.controller = "Ccd", .action = "Index", .cta = UrlParameter.Optional, .contact = UrlParameter.Optional, .fch = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="Cce",
        url:="Cce/{cta}/{fch}",
        defaults:=New With {.controller = "Cce", .action = "Index", .cta = UrlParameter.Optional, .fch = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Balances",
        url:="Balances/{guid}",
        defaults:=New With {.controller = "Balances", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Impagats",
        url:="Impagats",
        defaults:=New With {.controller = "Impagats", .action = "Index"}
    )

        routes.MapRoute(
        name:="Outdated",
        url:="Outdated",
        defaults:=New With {.controller = "Home", .action = "Outdated"}
    )
        routes.MapRoute(
        name:="JSONServices",
        url:="Json/{op}/{customer}/{user}",
        defaults:=New With {.controller = "Json", .action = "Index", .op = UrlParameter.Optional, .customer = UrlParameter.Optional, .user = UrlParameter.Optional},
        constraints:=New With {.customer = sGuidConstrain, .user = sGuidConstrain}
    )

        routes.MapRoute(
        name:="Raport",
        url:="Raport/{guid}",
        defaults:=New With {.controller = "Raports", .action = "Raport", .guid = UrlParameter.Optional},
        constraints:=New With {.guid = sGuidConstrain}
    )
        routes.MapRoute(
        name:="Raports",
        url:="Raports/{action}/{guid}",
        defaults:=New With {.controller = "Raports", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="NewRaport",
        url:="Raport/forCustomer/{customer}",
        defaults:=New With {.controller = "Raports", .action = "forCustomer", .customer = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="CustomerRaports",
        url:="Customer/raports/{customer}",
        defaults:=New With {.controller = "Raports", .action = "CustomerRaports", .customer = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="CliAperturaFarmacia",
        url:="Apertura/farmacia",
        defaults:=New With {.controller = "CliAperturas", .action = "NovaFarmacia"}
    )
        routes.MapRoute(
        name:="CliAperturaGuarderia",
        url:="Apertura/guarderia",
        defaults:=New With {.controller = "CliAperturas", .action = "NovaGuarderia"}
    )
        routes.MapRoute(
        name:="CliAperturaOnline",
        url:="Apertura/online",
        defaults:=New With {.controller = "CliAperturas", .action = "NouECommerce"}
    )
        routes.MapRoute(
        name:="CliApertura",
        url:="Apertura/{guid}",
        defaults:=New With {.controller = "CliAperturas", .action = "SingleApertura", .guid = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="CliAbertura",
        url:="Abertura/{guid}",
        defaults:=New With {.controller = "CliAperturas", .action = "SingleAbertura", .guid = UrlParameter.Optional}
    )



        routes.MapRoute(
        name:="CliAperturas",
        url:="Aperturas/{action}/{guid}",
        defaults:=New With {.controller = "CliAperturas", .action = "Index", .guid = UrlParameter.Optional}
    )


        routes.MapRoute(
        name:="AeatModels",
        url:="AeatModels/{emp}",
        defaults:=New With {.controller = "AeatModels", .action = "Index", .emp = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="AeatModel",
        url:="AeatModel/{guid}/{emp}",
        defaults:=New With {.controller = "AeatModel", .action = "Index", .guid = UrlParameter.Optional, .emp = UrlParameter.Optional},
        constraints:=New With {.guid = sGuidConstrain}
    )

        routes.MapRoute(
        name:="AeatModel2",
        url:="AeatModel/{Action}",
        defaults:=New With {.controller = "AeatModel", .action = "Index"}
    )


        routes.MapRoute(
        name:="ProductRanking",
        url:="ProductRanking/{action}",
        defaults:=New With {.controller = "ProductRanking", .action = "Index", .id = UrlParameter.Optional}
    )


        routes.MapRoute(
        name:="promos",
        url:="promos/{action}",
        defaults:=New With {.controller = "Incentivos", .action = "Index", .id = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="promo salepoints",
        url:="promo/salepoints/{guid}",
        defaults:=New With {.controller = "Incentivos", .action = "salepoints", .guid = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="promo",
        url:="promo/{guid}",
        defaults:=New With {.controller = "Incentivos", .action = "Promo", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="fullpromo",
        url:="fullpromo/{guid}",
        defaults:=New With {.controller = "Promos", .action = "FullPromo", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Nominas",
        url:="Nominas/{action}",
        defaults:=New With {.controller = "Nominas", .action = "Index", .year = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="CustomerRanking",
        url:="CustomerRanking/{action}/{data}",
        defaults:=New With {.controller = "CustomerRanking", .action = "Index", .data = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="RepBasketForCustomer",
        url:="RepBasketForCustomer/{customer}",
        defaults:=New With {.controller = "RepBasket", .action = "ForCustomer", .customer = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="RepBasket",
        url:="RepBasket/{action}/{contact}/{id}",
        defaults:=New With {.controller = "RepBasket", .action = "Index", .contact = UrlParameter.Optional, .id = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="Sku",
        url:="sku/{guid}",
        defaults:=New With {.controller = "Sku", .action = "Index", .guid = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="Wtbol Landing Pages",
        url:="landingpages/{guid}",
        defaults:=New With {.controller = "Wtbol", .action = "LandingPages", .guid = UrlParameter.Optional}
    )
        'routes.MapRoute(
        'name:="Wtbol ClickThrough log",
        'url:="wtbol/ClickThroughLog/{session}/{landingpage}",
        'Defaults:=New With {.controller = "Wtbol", .action = "ClickThroughLog", .session = UrlParameter.Optional, .landingpage = UrlParameter.Optional}
        ')

        routes.MapRoute(
        name:="Wtbol",
        url:="Wtbol/{action}/{guid}",
        defaults:=New With {.controller = "Wtbol", .action = "Index", .guid = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="Wtbol Landing Page track",
        url:="wtbol/landingpage/{url}",
        defaults:=New With {.controller = "Wtbol", .action = "LandingPage", .url = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="EciTransmTemplate",
        url:="EciTransmTemplate/{action}",
        defaults:=New With {.controller = "EciTransmTemplate", .action = "Index"}
    )

        routes.MapRoute(
        name:="Diari",
        url:="Diari",
        defaults:=New With {.controller = "Diari", .action = "Diari"}
    )
        routes.MapRoute(
        name:="ContactDiari",
        url:="Diari/{contact}",
        defaults:=New With {.controller = "Diari", .action = "ContactDiari", .contact = UrlParameter.Optional}
    )
        routes.MapRoute(
        name:="BaseDiari",
        url:="Diari/{contact}/{year}/{month}/{day}/{level}",
        defaults:=New With {.controller = "Diari", .action = "BaseDiari", .contact = UrlParameter.Optional, .year = UrlParameter.Optional, .month = UrlParameter.Optional, .day = UrlParameter.Optional, .level = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="LogosDistribuidorOficial",
        url:="LogosDistribuidorOficial",
        defaults:=New With {.controller = "LogosDistribuidorOficial", .action = "Logos"}
    )

        routes.MapRoute(
        name:="DistribuidorOficial",
        url:="LogoDistribuidorOficial/{brand}/{customer}",
        defaults:=New With {.controller = "LogosDistribuidorOficial", .action = "Logo", .brand = UrlParameter.Optional, .customer = UrlParameter.Optional},
        constraints:=New With {.brand = sGuidConstrain, .customer = sGuidConstrain}
    )

        routes.MapRoute(
        name:="FairGuest",
        url:="FairGuest/{Action}",
        defaults:=New With {.controller = "FairGuest", .action = "SignUp"}
    )

        routes.MapRoute(
        name:="tarifaByCustomer",
        url:="tarifa/{customer}/{fch}",
        defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByCustomer", .customer = UrlParameter.Optional, .fch = UrlParameter.Optional},
        constraints:=New With {.customer = sGuidConstrain, .fch = "^\d{18}$"}
    )

        routes.MapRoute(
        name:="tarifaByUser",
        url:="tarifa/{fch}",
        defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByUser", .fch = UrlParameter.Optional},
        constraints:=New With {.fch = "^\d{4,6}$"}
    )
        routes.MapRoute(
        name:="Tarifas",
        url:="Tarifas/{customer}/{fch}",
        defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByCustomer", .customer = UrlParameter.Optional, .fch = UrlParameter.Optional},
        constraints:=New With {.customer = sGuidConstrain, .fch = "^\d{18}$"}
    )
        routes.MapRoute(
        name:="Tarifas2",
        url:="Tarifas/{customer}",
        defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByCustomer", .customer = UrlParameter.Optional},
        constraints:=New With {.customer = sGuidConstrain}
    )
        routes.MapRoute(
        name:="TarifasByUser",
        url:="Tarifas",
        defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByUser"}
    )

        routes.MapRoute(
            name:="TarifaPvp",
            url:="Pvp",
            defaults:=New With {.controller = "CustomerTarifa", .action = "tarifaByUser"}
    )


        routes.MapRoute(
        name:="britax storelocator",
        url:="storelocator/britax",
        defaults:=New With {.controller = "Britax", .action = "StoreLocator"}
    )
        routes.MapRoute(
        name:="britax wtbol stocks",
        url:="britax/stocks/{MerchantId}",
        defaults:=New With {.controller = "Britax", .action = "Stocks", .MerchantId = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Britax",
        url:="Britax/{action}",
        defaults:=New With {.controller = "Britax", .action = "Index"},
        constraints:=New With {.action = "Stock|StoreLocator|Forecast"}
    )


        routes.MapRoute(
        name:="inglesina retailers",
        url:="inglesina/retailers",
        defaults:=New With {.controller = "ProductDistributors", .action = "Inglesina"}
    )

        routes.MapRoute(
        name:="4moms",
        url:="4moms/json/{action}",
        defaults:=New With {.controller = "fourmoms", .action = "Index"}
    )

        routes.MapRoute(
        name:="Cca",
        url:="Cca/{guid}",
        defaults:=New With {.controller = "cca", .action = "index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Extracte",
        url:="Extracte/{Action}/{year}/{cta}/{contact}",
        defaults:=New With {.controller = "PgcExtracte", .Action = "Index", .year = UrlParameter.Optional, .cta = UrlParameter.Optional, .contact = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="SumasYSaldos",
        url:="SumasYSaldos/{Action}/{guid}/{year}",
        defaults:=New With {.controller = "SumasYSaldos", .action = "Summary", .guid = UrlParameter.Optional, .year = UrlParameter.Optional}
    )



        routes.MapRoute(
        name:="Menu",
        url:="menu",
        defaults:=New With {.controller = "menu", .action = "MainMenu"}
    )

        routes.MapRoute(
        name:="SubMenu",
        url:="menu/{menu}",
        defaults:=New With {.controller = "menu", .action = "SubMenu", .menu = UrlParameter.Optional}
        )


        routes.MapRoute(
        name:="Area",
        url:="area/{Action}/{area}",
        defaults:=New With {.controller = "area", .action = "AreaCustomers", .area = UrlParameter.Optional}
    )


        routes.MapRoute(
        name:="ProductDistributorsRepList",
        url:="representante/clientes/{rep}",
        defaults:=New With {.controller = "ProductDistributors", .action = "RepList", .rep = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Distribuidores",
        url:="distribuidores",
        defaults:=New With {.controller = "StoreLocator", .action = "Index"}
    )

        routes.MapRoute(
        name:="ProductDistributorsOficialsRepList",
        url:="representante/distribuidoresOficiales/{brandnom}",
        defaults:=New With {.controller = "ProductDistributors", .action = "RepDistribuidoresOficialesList", .brandnom = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="RepLiqs",
        url:="representante/liquidaciones",
        defaults:=New With {.controller = "RepLiqs", .action = "RepLiqs"}
    )

        routes.MapRoute(
         name:="RepLiqs_",
         url:="RepLiqs/{action}",
         defaults:=New With {.controller = "RepLiqs", .action = "Index"}
        )

        routes.MapRoute(
        name:="RepLiq",
        url:="representante/liquidacion/{guid}",
        defaults:=New With {.controller = "RepLiqs", .action = "RepLiq", .guid = UrlParameter.Optional},
        constraints:=New With {.guid = sGuidConstrain}
    )

        routes.MapRoute(
        name:="Contacto",
        url:="contacto/{guid}",
        defaults:=New With {.controller = "Contact", .action = "Index", .guid = UrlParameter.Optional},
        constraints:=New With {.guid = sGuidConstrain}
    )

        routes.MapRoute(
        name:="Escriptures",
        url:="escriptures/{action}",
        defaults:=New With {.controller = "Escriptures", .action = "Index"}
    )
        routes.MapRoute(
        name:="BebesyMamas",
        url:="bebesymamas/{action}",
        defaults:=New With {.controller = "QuizConsumerFair", .action = "SignUp"}
    )


        routes.MapRoute(
        name:="PedidosPendientes",
        url:="Pedidos/Pendientes/{customer}",
        defaults:=New With {.controller = "PurchaseOrder", .action = "CustomerPending", .customer = UrlParameter.Optional}
    )

        routes.MapRoute(
            name:="PedidosFromUser",
            url:="Pedidos",
            defaults:=New With {.controller = "PurchaseOrder", .action = "FromUser"}
        )

        routes.MapRoute(
            name:="Pedidos",
            url:="Pedidos/{customer}",
            defaults:=New With {.controller = "PurchaseOrder", .action = "FromContact", .customer = UrlParameter.Optional}
        )

        routes.MapRoute(
        name:="PurchaseOrder",
        url:="PurchaseOrder/{action}",
        defaults:=New With {.controller = "PurchaseOrder", .action = "Index"}
    )


        routes.MapRoute(
        name:="LineaDePedido",
        url:="LineaDePedido/{guid}/{lin}",
        defaults:=New With {.controller = "PurchaseOrder", .action = "PurchaseOrderItem", .guid = UrlParameter.Optional, .lin = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="RepSortides",
        url:="RepSortides/{guid}",
        defaults:=New With {.controller = "PurchaseOrder", .action = "RepSortides", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="ProductDistributors",
        url:="ProductDistributors/{action}",
        defaults:=New With {.controller = "ProductDistributors", .action = "Index"}
    )



        routes.MapRoute(
        name:="347b",
        url:="AeatMod347/FromCustomer/{guid}",
        defaults:=New With {.controller = "AeatMod347", .action = "FromCustomer", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="347",
        url:="347",
        defaults:=New With {.controller = "AeatMod347", .action = "Index"}
    )

        routes.MapRoute(
        name:="SabiasQue",
        url:="SabiasQue/{*tag}",
        defaults:=New With {.controller = "SabiasQue", .action = "Index", .tag = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Navegador",
        url:="navegador",
        defaults:=New With {.controller = "Home", .action = "Navegador"}
    )

        routes.MapRoute(
        name:="Sorteo",
        url:="Sorteo/{action}/{guid}",
        defaults:=New With {.controller = "Raffle", .action = "Index", .guid = UrlParameter.Optional}
    )


        routes.MapRoute(
        name:="SorteoPlay3",
        url:="{lang}/Sorteo/play/{guid}",
        defaults:=New With {.controller = "Raffle", .action = "play", .guid = UrlParameter.Optional},
        constraints:=New With {.Lang = sLangsConstrain}
    )

        routes.MapRoute(
        name:="SorteoPlay2",
        url:="{lang}/Sorteo/play/{guid}/{user}",
        defaults:=New With {.controller = "Raffle", .action = "play2", .guid = UrlParameter.Optional, .user = UrlParameter.Optional},
        constraints:=New With {.Lang = sLangsConstrain}
    )

        routes.MapRoute(
        name:="SorteoLang",
        url:="{lang}/Sorteo/{action}/{guid}",
        defaults:=New With {.controller = "Raffle", .action = "Index", .guid = UrlParameter.Optional},
        constraints:=New With {.Lang = sLangsConstrain}
    )

        routes.MapRoute(
        name:="Sorteos",
        url:="{Sorteos}/{action}/{guid}",
        defaults:=New With {.controller = "Raffles", .action = "Index", .guid = UrlParameter.Optional},
        constraints:=New With {.Sorteos = "Sorteos|Sortejos|Raffles|Sorteios"}
    )

        routes.MapRoute(
            name:="Sorteos Lang",
            url:="{lang}/{Sorteos}",
            defaults:=New With {.controller = "Raffles", .action = "Index", .guid = UrlParameter.Optional},
            constraints:=New With {.Lang = sLangsConstrain, .Sorteos = "Sorteos|Sortejos|Raffles|Sorteios"}
        )

        routes.MapRoute(
        name:="Raffles",
        url:="Raffles/{action}/{guid}",
        defaults:=New With {.controller = "Raffles", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
        name:="Raffle",
        url:="Raffle/{action}/{guid}",
        defaults:=New With {.controller = "Raffle", .action = "Index", .guid = UrlParameter.Optional}
    )

        routes.MapRoute(
            name:="Incidencias",
            url:="Incidencias/{action}/{id}",
            defaults:=New With {.controller = "Incidencia", .action = "Incidencias", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="IncidenciaSteps",
            url:="Incidencia/gotoStep/{oStep}",
            defaults:=New With {.controller = "Incidencia", .action = "gotoStep", .oStep = UrlParameter.Optional}
        )

        'routes.MapRoute( _
        '    name:="NewIncidencia", _
        '    url:="Incidencia", _
        '    defaults:=New With {.controller = "Incidencia", .action = "NewIncidencia"} _
        ')

        routes.MapRoute(
            name:="SingleIncidencia",
            url:="Incidencia/{guid}",
            defaults:=New With {.controller = "Incidencia", .action = "showIncidencia", .guid = UrlParameter.Optional},
            constraints:=New With {.guid = sGuidConstrain}
        )

        routes.MapRoute(
            name:="Incidencia",
            url:="Incidencia/{action}/{guid}",
            defaults:=New With {.controller = "Incidencia", .action = "NewIncidencia", .guid = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Utilities",
            url:="Utilities/{action}",
            defaults:=New With {.controller = "Utilities", .action = "Index"}
        )


        routes.MapRoute(
            name:="Default2",
            url:="home/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}, namespaces:=New String() {"Mat.Mvc"}
        )

        routes.MapRoute(
            name:="Iban",
            url:="Iban/{action}/{id}",
            defaults:=New With {.controller = "Iban", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
             name:="Politica de Cookies",
             url:="PoliticaDeCookies",
             defaults:=New With {.controller = "Home", .action = "PoliticaDeCookies"}, namespaces:=New String() {"Mat.Mvc"}
         )

        routes.MapRoute(
             name:="Contact message",
             url:="ContactMessage",
             defaults:=New With {.controller = "ContactMessage", .action = "Index"}
         )

        routes.MapRoute(
            name:="Videos",
            url:="Videos",
            defaults:=New With {.controller = "product", .action = "AllVideos"}
        )

        routes.MapRoute(
            "productRef",
            "producto/ref/{id}",
            New With {.controller = "product", .action = "ref"}, New With {.id = "^\d{5}$"}
        )

        routes.MapRoute(
            "productGuid",
            "product/{guid}",
            New With {.controller = "product", .action = "ProductGuid"},
            New With {.guid = sGuidConstrain}
        )

        'guid = at least 6 characters
        routes.MapRoute(
            "renderPdf",
            "img/renderPdf/{maxWidth}/{maxHeight}",
             New With {.controller = "Img", .action = "renderPdf", .maxWidth = UrlParameter.Optional, .maxHeight = UrlParameter.Optional}
        )

        routes.MapRoute(
            "Pdf2Img",
            "img/Pdf2Img",
             New With {.controller = "Img", .action = "Pdf2Img"}
        )

        routes.MapRoute(
            "Images",
            "img/{cod}/{id}",
             New With {.controller = "Img", .action = "Index"}, New With {.cod = "^\d+$", .id = "^.{6,}$"}
        )

        routes.MapRoute(
            "Images with mime",
            "img/{cod}/{id}.jpg",
             New With {.controller = "Img", .action = "Index"}, New With {.cod = "^\d+$", .id = "^.{6,}$"}
        )

        routes.MapRoute(
            "ImagesEmp",
            "img/{cod}/{id}",
             New With {.controller = "Img", .action = "Index"}, New With {.cod = "^\d+$", .id = "^\d+$"}
        )

        routes.MapRoute(
            "DocsExcel",
            "doc/excel/{fileguid}/{filename}",
             New With {.controller = "Doc", .action = "Excel", .fileguid = UrlParameter.Optional, .filename = UrlParameter.Optional}
        )

        routes.MapRoute(
            "Docs",
            "doc/{cod}/{id}/{filename}",
             New With {.controller = "Doc", .action = "Index", .cod = "", .id = "", .filename = UrlParameter.Optional}, New With {.cod = "^\d+$", .id = "^.{6,}$"}
        )

        routes.MapRoute(
            "DocFiles",
            "doc/{cod}",
             New With {.controller = "Doc", .action = "Index", .cod = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="Account",
            url:="Account/{action}/{*returnurl}",
            defaults:=New With {.controller = "Account", .action = "login", .returnurl = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="SignUp",
            url:="SignUp/{action}",
            defaults:=New With {.controller = "Signup", .action = "Index"}
        )

        routes.MapRoute(
            name:="Registro_facebook_4moms",
            url:="Registro/facebook/4moms",
            defaults:=New With {.controller = "Account", .action = "RegistroFacebook4moms", .returnUrl = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Registro_facebook",
            url:="Registro/facebook",
            defaults:=New With {.controller = "Account", .action = "RegistroFacebook", .returnUrl = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Registro",
            url:="Registro",
            defaults:=New With {.controller = "Account", .action = "Registro", .returnUrl = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="mailPassword",
            url:="SignUp/mailPassword/{emailAddress}",
            defaults:=New With {.controller = "SignUp", .action = "mailPassword", .emailAddress = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Sscs",
            url:="Subscripciones",
            defaults:=New With {.controller = "Subscripcions", .action = "Index"}
        )

        routes.MapRoute(
            name:="AccountActionId",
            url:="Account/{action}/{id}",
            defaults:=New With {.controller = "Account", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Guid",
            url:="Guid/{targetUser}/{requestUser}",
            defaults:=New With {.controller = "Pro", .action = "Guid", .targetUser = UrlParameter.Optional, .requestUser = UrlParameter.Optional}
        )



        routes.MapRoute(
            name:="SellOutHW",
            url:="sellout/HelloWorld",
            defaults:=New With {.controller = "Sellout", .action = "HelloWorld"}
        )
        routes.MapRoute(
            name:="SellOutAmazon",
            url:="sellout/Amazon",
            defaults:=New With {.controller = "Sellout", .action = "SellOutAmazon"}
        )

        routes.MapRoute(
            name:="SelloutX",
            url:="Sellout/{action}/{id}",
            defaults:=New With {.controller = "Sellout", .action = "Index", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="SellOut",
            url:="sellout",
            defaults:=New With {.controller = "Sellout", .action = "index"}
        )

        'routes.MapRoute(
        'name:="SellOutCustomer",
        'url:="sellout/{customer}",
        'Defaults:=New With {.controller = "Sellout", .action = "index", .customer = UrlParameter.Optional},
        'constraints:=New With {.customer = sGuidConstrain}
        ')





        routes.MapRoute(
           name:="TpvResponse",
            url:="Tpv/{action}",
            defaults:=New With {.controller = "Tpv", .action = ""},
            constraints:=New With {.action = "(Log|Ok|Ko)"}
        )

        routes.MapRoute(
           name:="TpvRequest",
            url:="Tpv/{Base64Json}",
            defaults:=New With {.controller = "Tpv", .action = "Index", .Base64Json = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="Comments",
            url:="Comments/{action}/{id}",
            defaults:=New With {.controller = "Comments", .action = "Index", .id = UrlParameter.Optional}
            )



        routes.MapRoute(
            name:="Condicions",
            url:="{Condicions}/{guid}",
            defaults:=New With {.controller = "Condicions", .action = "Index", .guid = UrlParameter.Optional},
            constraints:=New With {.Condicions = "Condiciones|Condicions|Conditions|Condições", .guid = sGuidConstrain}
        )

        routes.MapRoute(
            name:="Ifema",
            url:="Ifema/2016",
            defaults:=New With {.controller = "Pro", .action = "IfemaGuestSignUp"}
        )

        routes.MapRoute(
            name:="Factura",
            url:="Factura/{guid}",
            defaults:=New With {.controller = "Invoice", .action = "Index", .guid = UrlParameter.Optional},
            constraints:=New With {.guid = sGuidConstrain}
        )

        routes.MapRoute(
            name:="Facturas",
            url:="Facturas/{customer}",
            defaults:=New With {.controller = "Facturas", .action = "Facturas", .customer = UrlParameter.Optional},
            constraints:=New With {.customer = sGuidConstrain}
        )

        routes.MapRoute(
            name:="Facturas2",
            url:="Facturas/{Action}",
            defaults:=New With {.controller = "Facturas", .action = "Facturas"}
        )

        routes.MapRoute(
            name:="Retenciones",
            url:="Retenciones",
            defaults:=New With {.controller = "Retenciones", .action = "Index"}
        )

        routes.MapRoute(
            name:="Stocks",
            url:="Stocks",
            defaults:=New With {.controller = "SkuStocks", .action = "Index"}
        )


        routes.MapRoute(
            name:="StocksSubscribe",
            url:="Stocks/Subscribe",
            defaults:=New With {.controller = "SkuStocks", .action = "Subscribe", .value = UrlParameter.Optional}
       )


        routes.MapRoute(
            name:="StocksDownload",
            url:="Stocks/download",
            defaults:=New With {.controller = "SkuStocks", .action = "Download"}
        )

        routes.MapRoute(
            name:="Mail",
            url:="Mail/{Action}/{id}/{lang}",
            defaults:=New With {.controller = "Mail", .action = "Index", .id = UrlParameter.Optional, .lang = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="Apertura2",
            url:="Apertura2",
            defaults:=New With {.controller = "Customer", .action = "Apertura"}
        )



        'noticies per iMat
        routes.MapRoute(
            name:="NoticiaForImat",
            url:="news/iMat/{id}",
            defaults:=New With {.controller = "News", .action = "iMat", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="LastNoticiaForMobile",
            url:="{news}/LastNoticiaForMobile",
            defaults:=New With {.controller = "News", .action = "LastNoticiaForMobile"},
            constraints:=New With {.news = "noticias|noticies|news|eventos|esdeveniments|events"}
)

        'noticies per UrlFriendly
        routes.MapRoute(
            name:="partialnoticias",
            url:="{news}/partialnoticias/{lang}",
            defaults:=New With {.controller = "News", .action = "partialnoticias", .lang = UrlParameter.Optional},
            constraints:=New With {.news = "noticias|noticies|news|eventos|esdeveniments|events"}
        )


        'noticies per UrlFriendly
        routes.MapRoute(
            name:="News",
            url:="{news}",
            defaults:=New With {.controller = "News", .action = "Index"},
            constraints:=New With {.news = "noticias|noticies|news"}
        )

        'noticies per UrlFriendly
        routes.MapRoute(
            name:="NewsCatchall",
            url:="{news}/{*Catchall}",
            defaults:=New With {.controller = "News", .action = "Index"},
            constraints:=New With {.news = "noticias|noticies|news"}
        )

        'noticies per UrlFriendly
        routes.MapRoute(
            name:="CanonicalNews",
            url:="{lang}/{news}/{*Catchall}",
            defaults:=New With {.controller = "News", .action = "Index"},
            constraints:=New With {.Lang = sLangsConstrain, .news = "noticias|noticies|news|eventos|esdeveniments|events"}
        )


        'noticies per UrlFriendly
        routes.MapRoute(
            name:="Content",
            url:="content/{id}",
            defaults:=New With {.controller = "Content", .action = "Index", .id = UrlParameter.Optional}
        )

        'noticies 
        routes.MapRoute(
            name:="Last_Noticias",
            url:="{news}/{action}/{id}",
            defaults:=New With {.controller = "News", .action = "Noticias", .id = UrlParameter.Optional},
            constraints:=New With {.news = "noticias|noticies|news"}
        )

        'noticies per categoria
        routes.MapRoute(
            name:="Noticias",
            url:="{news}/{Nom}",
            defaults:=New With {.controller = "News", .action = "Categoria", .Nom = UrlParameter.Optional},
            constraints:=New With {.news = "noticias|noticies|news"}
        )

        routes.MapRoute(
           name:="productosxCnap",
           url:="productos/{cnapKey}",
           defaults:=New With {.controller = "product", .action = "Cnap", .cnapKey = UrlParameter.Optional}
           )





        routes.MapRoute(
           name:="product_distributors",
           url:="product/distributors",
           defaults:=New With {.controller = "product", .action = "Distributors"}
           )

        routes.MapRoute(
           name:="product_jQuery",
           url:="product/{action}",
           defaults:=New With {.controller = "product", .action = "Index"}
           )


        routes.MapRoute(
            name:="Lang products",
            url:="{Lang}/{Brand}/{*Catchall}",
            defaults:=New With {.controller = "product", .action = "Index", .Brand = UrlParameter.Optional},
            constraints:=New With {.Lang = sLangsConstrain, .Brand = sBrandsConstrain}
            )

        routes.MapRoute(
            name:="products",
            url:="{Brand}/{*Catchall}",
            defaults:=New With {.controller = "product", .action = "Index", .Brand = UrlParameter.Optional},
            constraints:=New With {.Brand = sBrandsConstrain}
            )

        routes.MapRoute(
             "descargas",
             "{Src}",
             New With {.controller = "Descargas", .action = "FromSrc", .Src = UrlParameter.Optional},
             New With {.Src = "(Catalogos|Instrucciones|Compatibilidad)"}
         )


        routes.MapRoute(
             "BrandDescargas",
             "{Src}/{Brand}",
             New With {.controller = "Descargas", .action = "FromBrand", .Src = UrlParameter.Optional, .Brand = UrlParameter.Optional},
             New With {.Src = "(Catalogos|Instrucciones|Compatibilidad)", .Brand = sBrandsConstrain}
         )

        'Brand specific after brands

        routes.MapRoute(
name:="4moms Sales Data",
url:="fourmoms/{action}",
defaults:=New With {.controller = "FourMoms", .action = "SalesData", .year = UrlParameter.Optional}
)






        routes.MapRoute(
            name:="EventosOld",
            url:="eventos",
            defaults:=New With {.controller = "Home", .action = "RedirectToOldWebsite"}, namespaces:=New String() {"Mat.Mvc"}
        )

        routes.MapRoute(
            name:="AperturaOld",
            url:="Apertura",
            defaults:=New With {.controller = "Home", .action = "RedirectToOldWebsite"}, namespaces:=New String() {"Mat.Mvc"}
        )

        routes.MapRoute(
           name:="pedido#",
           url:="pedido/{guid}",
           defaults:=New With {.controller = "PurchaseOrder", .action = "SingleOrder"},
           constraints:=New With {.guid = sGuidConstrain}
           )

        routes.MapRoute(
           name:="pedidoFactory",
           url:="pedido/{action}",
           defaults:=New With {.controller = "CustomerBasket", .action = "Index"}
           )


        routes.MapRoute(
           name:="CustomerBasket",
           url:="pedido/{action}/{data}",
           defaults:=New With {.controller = "CustomerBasket", .action = "Index", .data = UrlParameter.Optional}
           )






        routes.MapRoute(
           name:="albaran",
           url:="albaran/{guid}",
           defaults:=New With {.controller = "Deliveries", .action = "SingleDelivery"},
           constraints:=New With {.guid = sGuidConstrain}
        )

        routes.MapRoute(
           name:="deliveryTracking",
           url:="delivery/tracking/{guid}",
           defaults:=New With {.controller = "Deliveries", .action = "Tracking", .guid = UrlParameter.Optional},
           constraints:=New With {.guid = sGuidConstrain}
        )

        routes.MapRoute(
            name:="Albaranes propios",
            url:="Albaranes",
            defaults:=New With {.controller = "Deliveries", .action = "Deliveries"}
        )

        routes.MapRoute(
            name:="Albaranes de un cliente",
            url:="Albaranes/{customer}",
            defaults:=New With {.controller = "Deliveries", .action = "Deliveries", .customer = UrlParameter.Optional},
            constraints:=New With {.customer = sGuidConstrain}
        )


        routes.MapRoute(
            name:="Deliveries",
            url:="Deliveries/{action}",
            defaults:=New With {.controller = "Deliveries", .action = "Deliveries"}
        )


        routes.MapRoute(
            name:="bancs-renovacions",
            url:="Bancs/renovacions",
            defaults:=New With {.controller = "Home", .action = "RedirectToOldWebsite"}, namespaces:=New String() {"Mat.Mvc"}
        )

        routes.MapRoute(
            name:="Profesionales",
            url:="Profesionales",
            defaults:=New With {.controller = "Pro", .action = "Index"}, namespaces:=New String() {"Mat.Mvc"}
        )

        'per tema tpv
        routes.MapRoute(
            name:="PublicOld",
            url:="Public/{id}",
            defaults:=New With {.controller = "Home", .action = "RedirectToOldWebsite"}, namespaces:=New String() {"Mat.Mvc"}
        )

        routes.MapRoute(
            name:="EmailOld",
            url:="email/{id}",
            defaults:=New With {.controller = "Home", .action = "RedirectToOldWebsite"}, namespaces:=New String() {"Mat.Mvc"}
        )



        '---------------------------------------------Quiz ---------------------------------------
        routes.MapRoute(
            name:="Quiz2",
            url:="Quiz/{Action}/{id}",
            defaults:=New With {.controller = "Quiz", .action = "Index", .id = UrlParameter.Optional}
        )


        routes.MapRoute(
            name:="NuevaRedOficialInglesina",
            url:="NuevaRedOficialInglesina",
            defaults:=New With {.controller = "Quiz", .action = "NuevaRedOficialInglesina"}
        )

        'eliminat perque capta els que han de anar per Catchall i dona error
        'routes.MapRoute(
        '    name:="Standard",
        '    url:="{controller}/{action}/{id}",
        '    defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        ')

        routes.MapRoute(
            name:="Catchall",
            url:="{*src}",
            defaults:=New With {.controller = "Home", .action = "Catchall"}, namespaces:=New String() {"Mat.Mvc"}
        )

    End Sub


End Module
