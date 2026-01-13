Public Class ProductRankLoader

    Shared Function Load(oUser As DTOUser, oPeriod As DTOProductRank.Periods, oArea As DTOArea, units As DTOProductRank.Units) As DTOProductRank
        Dim retval As New DTOProductRank(oPeriod)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BrandGuid, BrandNomEsp, CategoryGuid, CategoryNomEsp, SUM(Eur) AS Amt, SUM(Qty) AS Qty ")
        sb.AppendLine("FROM VwSellout2 ")
        sb.AppendLine("WHERE VwSellout2.Emp = " & oUser.Emp.Id & " ")
        sb.AppendLine("AND VwSellout2.IsBundle = 0 ")
        sb.AppendLine("AND VwSellout2.CategoryCodi = 0 ")
        sb.AppendLine("AND ((VwSellout2.Year =" & retval.YearMonthFrom.Year & " AND VwSellout2.Month >=" & retval.YearMonthFrom.Month & ") OR VwSellout2.Year >" & retval.YearMonthFrom.Year & " )")
        If oArea IsNot Nothing Then
            sb.AppendFormat("AND (VwSellout2.CountryGuid='{0}' OR VwSellout2.ZonaGuid = '{0}' OR VwSellout2.LocationGuid = '{0}') ", oArea.Guid.ToString())
        End If
        sb.AppendLine("AND VwSellout2.BrandNomEsp <> 'Varios' ")
        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oProveidorGuid As Guid = Nothing
                Dim oProveidors = UserLoader.GetProveidors(oUser)
                If oProveidors.Count > 0 Then oProveidorGuid = oProveidors.First.Guid
                sb.AppendLine("AND VwSellout2.ProveidorGuid = '" & oProveidorGuid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY BrandGuid, BrandNomEsp, CategoryGuid, CategoryNomEsp ")
        Select Case units
            Case DTOProductRank.Units.Units
                sb.AppendLine("ORDER BY SUM(Qty) DESC ")
            Case DTOProductRank.Units.Eur
                sb.AppendLine("ORDER BY SUM(Eur) DESC ")
        End Select
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductRank.Item
            With item
                .Product = SQLHelper.GetProductFromDataReader(oDrd, , "BrandNomEsp",, "CategoryNomEsp")
                Select Case units
                    Case DTOProductRank.Units.Units
                        .Value = oDrd("Qty")
                    Case DTOProductRank.Units.Eur
                        .Value = oDrd("Amt")
                End Select
            End With
            retval.Items.Add(item)
        Loop

        oDrd.Close()

        retval.Brands = New List(Of DTOGuidNom)

        For Each oBrand In retval.Items.GroupBy(Function(x) x.Product.Brand.Guid).Select(Function(y) y.First.Product.Brand).ToList
            retval.Brands.Add(New DTOGuidNom(oBrand.Guid, oBrand.nom.Esp))
        Next

        Return retval
    End Function
End Class
