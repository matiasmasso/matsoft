Public Class SitemapLoader

    Shared Function product_accessories(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Accessories)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwProductNom.FullNom AS ProductNom")
        sb.AppendLine("FROM Artspare ")
        sb.AppendLine("INNER JOIN VwProductNom on Artspare.Targetguid=Product.Guid ")
        sb.AppendLine("INNER JOIN Tpa on VwProductNom.BrandGuid=Tpa.Guid ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND Artspare.cod = 1 ")
        sb.AppendLine("GROUP BY VwProductNom.FullNom ")
        sb.AppendLine("ORDER BY VwProductNom.FullNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sNom As String = UrlHelper.EncodedUrlSegment(oDrd("ProductNom").ToString())
            Dim sUrl As String = oDomain.Url(sNom, "accesorios")
            Dim DtFch As Date = New Date(DTO.GlobalVariables.Today().Year, DTO.GlobalVariables.Today().Month, 1)
            retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.monthly, 0.5)
        Loop
        Return retval
    End Function

    Shared Function product_descargas(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Descargas)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT MAX(BF.Fch) AS Fch, ProductDownload.Src, VwSkuNom.BrandNomEsp AS BrandNom ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN VwProductParent ON ProductDownload.Product = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = TPA.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON TPA.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN DocFile AS BF ON ProductDownload.Hash = BF.Hash COLLATE SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND (ProductDownload.Obsoleto = 0) AND (ProductDownload.PublicarAlConsumidor <> 0) AND (TPA.OBSOLETO = 0) ")
        sb.AppendLine("GROUP BY ProductDownload.Src, VwSkuNom.BrandNomEsp, Tpa.Ord ")
        sb.AppendLine("ORDER BY ProductDownload.Src, Tpa.Ord ")
        Dim SQL As String = sb.ToString

        Dim oLastSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.notSet
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSrc As DTOProductDownload.Srcs = oDrd("Src")
            Select Case oSrc
                Case DTOProductDownload.Srcs.catalogos, DTOProductDownload.Srcs.compatibilidad, DTOProductDownload.Srcs.instrucciones
                    If oLastSrc <> oSrc Then
                        oLastSrc = oSrc
                        Dim sUrlSrc As String = oDomain.Url(oSrc.ToString())
                        Dim DtFchSrc As Date = oDrd("Fch")
                        retval.AddItem(sUrlSrc, DtFchSrc, DTOSiteMap.ChangeFreqs.monthly, 0.8)
                    End If
                    Dim sNom = UrlHelper.EncodedUrlSegment(oDrd("BrandNom").ToString())
                    Dim sUrl As String = oDomain.Url(oSrc.ToString, sNom)
                    Dim DtFch As Date = oDrd("Fch")
                    retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.monthly, 0.7)
            End Select
        Loop
        Return retval
    End Function

    Shared Function product_downloads(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Downloads)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Max(BF.Fch) AS Fch, VwProductNom.FullNom AS ProductNom ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN VwProductNom ON ProductDownload.Product=VwProductNom.guid ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid=Tpa.guid ")
        sb.AppendLine("INNER JOIN DocFile BF ON ProductDownload.Hash=BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND ProductDownload.Obsoleto = 0 And Tpa.Obsoleto = 0 And ProductDownload.PublicarAlConsumidor <> 0 ")
        sb.AppendLine("GROUP BY VwProductNom.FullNom ")
        sb.AppendLine("ORDER BY VwProductNom.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sNom As String = UrlHelper.EncodedUrlSegment(oDrd("ProductNom").ToString())
            Dim sUrl As String = oDomain.Url(sNom, "descargas")
            Dim DtFch As Date = oDrd("Fch")
            retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.monthly, 0.7)
        Loop
        oDrd.Close()

        sb = New Text.StringBuilder
        sb.AppendLine("SELECT Max(BF.Fch) AS Fch, VwProductNom.FullNom AS ProductNom ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN VwProductNom ON ProductDownload.Product=VwProductNom.guid ")
        sb.AppendLine("INNER JOIN Stp ON ProductDownload.Product=Stp.guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.guid ")
        sb.AppendLine("INNER JOIN DocFile BF ON ProductDownload.Hash=BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND ProductDownload.Obsoleto = 0 AND Tpa.Obsoleto = 0 AND Stp.Obsoleto = 0 AND Stp.Web_Enabled_Consumer = 1 ")
        sb.AppendLine("And ProductDownload.PublicarAlConsumidor <> 0 ")
        sb.AppendLine("GROUP BY VwProductNom.FullNom ")
        sb.AppendLine("ORDER BY VwProductNom.FullNom ")

        SQL = sb.ToString
        oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sNom As String = UrlHelper.EncodedUrlSegment(oDrd("ProductNom").ToString())
            Dim sUrl As String = oDomain.Url(sNom, "descargas")
            Dim DtFch As Date = oDrd("Fch")
            retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.monthly, 0.4)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function product_videos(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Videos)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("select VwProductNom.FullNom AS ProductNom, max(YouTube.fchCreated) as Fch ")
        sb.AppendLine("FROM VwProductNom ")
        sb.AppendLine("INNER JOIN Stp ON VwProductNom.Guid=Stp.guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.guid ")
        sb.AppendLine("INNER JOIN youtubeproducts on youtubeproducts.productguid=VwProductNom.guid ")
        sb.AppendLine("INNER JOIN YouTube on YouTube.Guid=YouTubeProducts.YouTubeGuid ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND Tpa.Obsoleto = 0 And Stp.Obsoleto =0 And Stp.Web_Enabled_Consumer = 1 ")
        sb.AppendLine("GROUP BY VwProductNom.FullNom ")
        sb.AppendLine("ORDER BY VwProductNom.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sNom As String = UrlHelper.EncodedUrlSegment(oDrd("ProductNom").ToString())
            Dim sUrl As String = oDomain.Url(sNom, "Videos")
            Dim DtFch As Date = oDrd("Fch")
            retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.monthly, 0.9)
        Loop
        Return retval
    End Function

    Shared Function Distributors(oEmp As DTOEmp, oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Distributors)
        Dim oTask As DTOTask = TaskLoader.Find(DTOTask.Cods.WebAtlasUpdate)
        Dim DtFch As DateTimeOffset = oTask.lastLog.fch
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwProductNom.BrandNom, Web.Cit ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN VwProductNom ON Web.Brand = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Web.Brand = Tpa.Guid ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " And Tpa.WEB_ENABLED_CONSUMER = 1 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwProductNom.BrandNom, WEB.Cit,TPA.ORD ")
        sb.AppendLine("ORDER BY Tpa.Ord, VwProductNom.BrandNom, Web.Cit ")

        Dim SQL = sb.ToString

        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sTpa As String = UrlHelper.EncodedUrlSegment(oDrd("BrandNom").ToString())
            Dim sCit As String = UrlHelper.EncodedUrlSegment(oDrd("Cit").ToString())
            sCit = sCit.Replace(" ", "_")
            Dim sUrl As String = oDomain.Url(sTpa, sCit)
            Dim oSitemapItem As DTOSiteMapItem = retval.AddItem(sUrl, DtFch, DTOSiteMap.ChangeFreqs.daily, 0.3)
        Loop
        oDrd.Close()

        Return retval
    End Function



End Class
