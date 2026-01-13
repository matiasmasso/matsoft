Public Class ProductSkusExtendedLoader

    Shared Function All(oCategory As DTOProductCategory, oMgz As DTOMgz, Optional BlHideObsolets As Boolean = False) As List(Of DTOProductSkuExtended)
        Dim retval As New List(Of DTOProductSkuExtended)

        ProductCategoryLoader.Load(oCategory)
        Dim oProveidor As DTOProveidor = oCategory.Brand.Proveidor

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.Ord, Art.Myd ")
        sb.AppendLine("Art.LastProduction, ")
        sb.AppendLine("ArtStock.stock, ")
        sb.AppendLine("ArtPnc.pn2, ArtPnc.pn3, ")
        sb.AppendLine("(CASE WHEN ART.IMAGE IS NULL THEN 0 ELSE 1 END) AS IMG, ")
        sb.AppendLine("ArtPnc.pn1, ")
        sb.AppendLine("Previsio.PRV, ")
        sb.AppendLine("Art.Obsoleto AS EX ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("LEFT OUTER JOIN ArtStock ON ART.Guid=ArtStock.ArtGuid AND (ArtStock.MgzGuid='" & oMgz.Guid.ToString & "' OR ArtStock.MgzGuid IS NULL)  ")
        sb.AppendLine("LEFT OUTER JOIN ArtPnc ON Art.Guid=ArtPnc.ArtGuid  ")

        If oProveidor IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN (SELECT ImportPrevisio.Ref, ImportHdr.Proveidor, SUM(Qty) AS Prv FROM ImportPrevisio INNER JOIN ImportHdr ON ImportPrevisio.Importacio=ImportHdr.Guid AND ImportHdr.Arrived<>0 GROUP BY ImportPrevisio.Ref, ImportHdr.Proveidor) AS PREVISIO ON ART.Ref=PREVISIO.Ref AND Previsio.Proveidor='" & oProveidor.Guid.ToString & "' AND Art.Ref>''  ")
        End If
        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")

        ' "LEFT OUTER JOIN (SELECT ArtGuid,SUM(Qty) AS PRV FROM DELIVERY GROUP BY ArtGuid) AS PREVISIO ON ART.Guid=PREVISIO.ArtGuid " _

        If BlHideObsolets Then
            sb.AppendLine("AND (ArtStock.Stock<>0 OR ArtPnc.Pn2<>0 OR ArtPnc.Pn1<>0 OR ART.OBSOLETO=0) ")
        End If

        sb.AppendLine("ORDER BY ART.Obsoleto, ART.Ord, ART.Art")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTOProductSkuExtended(oDrd("Guid"))
            With oItem
                .Id = CInt(oDrd("Art"))
                .NomCurt = oDrd("Ord").ToString
                .NomLlarg = oDrd("Myd")
                .Nom = .NomLlarg
                .LastProduction = CBool(oDrd("LastProduction"))
                If Not IsDBNull(oDrd("stock")) Then
                    .UnitsInStock = oDrd("Stock")
                End If
                If Not IsDBNull(oDrd("Pn2")) Then
                    .UnitsInClients = oDrd("Pn2")
                End If
                If Not IsDBNull(oDrd("Pn3")) Then
                    .UnitsInPot = oDrd("Pn3")
                End If
                .ImageExists = oDrd("Img")

                If Not IsDBNull(oDrd("Pn1")) Then
                    .UnitsInProveidor = oDrd("Pn1")
                End If
                If Not IsDBNull(oDrd("Prv")) Then
                    .UnitsInPrevisio = oDrd("Prv")
                End If
                .Obsoleto = CBool(oDrd("Ex"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
