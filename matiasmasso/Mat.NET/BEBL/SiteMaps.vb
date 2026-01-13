Public Class SiteMap

    Shared Function product_accessories(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Return SitemapLoader.product_accessories(oEmp, oDomain)
    End Function

    Shared Function product_descargas(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Return SitemapLoader.product_descargas(oEmp, oDomain)
    End Function

    Shared Function product_downloads(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Return SitemapLoader.product_downloads(oEmp, oDomain)
    End Function

    Shared Function product_videos(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Return SitemapLoader.product_videos(oEmp, oDomain)
    End Function

    Shared Function Distributors(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Return SitemapLoader.Distributors(oEmp, oDomain)
    End Function

End Class
