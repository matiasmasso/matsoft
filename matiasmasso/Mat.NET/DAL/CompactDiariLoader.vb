Public Class CompactDiariLoader

    Shared Function Items(exs As List(Of Exception), ByRef value As DTOSalesQuery) As List(Of DTOSalesQuery.Item)
        Dim retval As New List(Of DTOSalesQuery.Item)
        Try
            Dim sSQL As String = Sql(value)
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(sSQL)
            Do While oDrd.Read
                Dim caption As String = ""
                Dim tag As String = ""
                Select Case value.level
                    Case DTOSalesQuery.Levels.months
                        Dim iMonth As Integer = oDrd("Caption")
                        caption = value.user.lang.Mes(iMonth)
                        tag = iMonth
                    Case DTOSalesQuery.Levels.days
                        Dim iDay As Integer = oDrd("Caption")
                        Dim DtFch As New Date(value.year, value.month, iDay)
                        caption = String.Format("{0:00} {1}", iDay, value.user.lang.WeekDay(DtFch))
                        tag = iDay
                    Case DTOSalesQuery.Levels.Orders
                        Dim oGuid As Guid = oDrd("Guid")
                        caption = oDrd("Caption")
                        tag = oGuid.ToString
                    Case DTOSalesQuery.Levels.Search
                        Dim oGuid As Guid = oDrd("Guid")
                        caption = oDrd("Caption")
                        tag = oGuid.ToString
                    Case Else
                        caption = oDrd("Caption")
                        tag = caption
                End Select

                Dim item = DTOSalesQuery.Item.Factory(caption, oDrd("Value"), tag)
                retval.Add(item)
                If retval.Count > 1000 AndAlso value.level = DTOSalesQuery.Levels.Search Then Exit Do
            Loop
            oDrd.Close()

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function Sql(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(SqlSelect(value))
        sb.AppendLine(SqlFrom(value))
        sb.AppendLine(SqlWhere(value))
        sb.AppendLine(SqlGroup(value))
        sb.AppendLine(SqlOrder(value))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlSelect(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        Select Case value.level
            Case DTOSalesQuery.Levels.years
                sb.AppendLine("SELECT YEAR(Pdc.FchCreated) AS Caption ")
            Case DTOSalesQuery.Levels.months
                sb.AppendLine("SELECT MONTH(Pdc.FchCreated) AS Caption ")
            Case DTOSalesQuery.Levels.days
                sb.AppendLine("SELECT DAY(Pdc.FchCreated) AS Caption ")
            Case DTOSalesQuery.Levels.Orders
                sb.AppendLine("SELECT Pdc.Guid, SUBSTRING(CONVERT(varchar,Pdc.FchCreated,108),1,5) + '|' + CliGral.FullNom + '|' + Pdc.Pdd AS Caption ")
            Case DTOSalesQuery.Levels.Search
                sb.AppendLine("SELECT Pdc.Guid, CONVERT(varchar,Pdc.FchCreated,103) + '|' + CliGral.FullNom + '|' + Pdc.Pdd AS Caption ")
        End Select
        sb.AppendLine(", ROUND(SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100),2) AS Value ")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlFrom(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")

        Select Case value.user.rol.id
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & value.user.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwRepCustomers ON Pdc.CliGuid = VwRepCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & value.user.Guid.ToString & "' ")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwSkuNom Manufacturer ON Pnc.ArtGuid = Manufacturer.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Manufacturer.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & value.user.Guid.ToString & "' ")
        End Select

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.customer) Then
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.customer)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("Pdc.CliGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.ccx) Then
            sb.AppendLine("INNER JOIN CliClient ON (Pdc.CliGuid = CliClient.Guid OR Pdc.CliGuid = CliClient.CcxGuid) ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.ccx)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("CliClient.CcxGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.holding) Then
            sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
            sb.AppendLine("INNER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.holding)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("Ccx.Holding = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.channel) Then
            sb.AppendLine("INNER JOIN VwContactChannel ON Pdc.CliGuid = VwContactChannel.Contact ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.channel)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwContactChannel.Channel = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.rep) Then
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.rep)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("Pnc.RepGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.proveidor) Then
            sb.AppendLine("INNER JOIN VwSkuNom VwSkuNomProveidor ON Pnc.ArtGuid = VwSkuNomProveidor.SkuGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.proveidor)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwSkuNomProveidor.Proveidor = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.brand) Then
            sb.AppendLine("INNER JOIN VwSkuNom VwSkuNomBrand ON Pnc.ArtGuid = VwSkuNomBrand.SkuGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.brand)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwSkuNomBrand.BrandGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.category) Then
            sb.AppendLine("INNER JOIN VwSkuNom VwSkuNomCategory ON Pnc.ArtGuid = VwSkuNomCategory.SkuGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.category)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwSkuNomCategory.CategoryGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.sku) Then
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.sku)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("Pnc.ArtGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.Country) Then
            sb.AppendLine("INNER JOIN VwAddress VwAddressCountry ON Pdc.CliGuid = VwAddressCountry.SrcGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.Country)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwAddressCountry.CountryGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.Zona) Then
            sb.AppendLine("INNER JOIN VwAddress VwAddressZona ON Pdc.CliGuid = VwAddressZona.SrcGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.Zona)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwAddressZona.ZonaGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If value.isFilteredBy(DTOSalesQuery.Filter.Cods.location) Then
            sb.AppendLine("INNER JOIN VwAddress VwAddressLocation ON Pdc.CliGuid = VwAddressLocation.SrcGuid ")
            Dim oFilters = value.filters.Where(Function(x) x.cod = DTOSalesQuery.Filter.Cods.location)
            sb.AppendLine("AND ( ")
            For Each oFilter In oFilters
                If Not oFilter.Equals(oFilters.First) Then sb.Append("OR ")
                sb.AppendLine("VwAddressLocation.LocationGuid = '" & oFilter.guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhere(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("WHERE CliGral.Emp = " & value.User.Emp.Id & " ")
        sb.AppendLine("AND Pdc.Cod = " & DTOPurchaseOrder.Codis.client & " ")
        Select Case value.level
            Case DTOSalesQuery.Levels.years
            Case DTOSalesQuery.Levels.months
                sb.AppendLine("AND YEAR(Pdc.FchCreated) = " & value.year & " ")
            Case DTOSalesQuery.Levels.days
                sb.AppendLine("AND YEAR(Pdc.FchCreated) = " & value.year & " ")
                sb.AppendLine("AND MONTH(Pdc.FchCreated) = " & value.month & " ")
            Case DTOSalesQuery.Levels.Orders
                sb.AppendLine("AND YEAR(Pdc.FchCreated) = " & value.Year & " ")
                sb.AppendLine("AND MONTH(Pdc.FchCreated) = " & value.Month & " ")
                sb.AppendLine("AND DAY(Pdc.FchCreated) = " & value.Day & " ")
            Case DTOSalesQuery.Levels.Search
                sb.AppendLine("AND (")
                sb.AppendLine("     CliGral.FullNom LIKE '%" & value.SearchTerm & "%' OR ")
                sb.AppendLine("     Pdc.Pdd LIKE '%" & value.SearchTerm & "%' ")
                sb.AppendLine(") ")
        End Select

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlGroup(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        Select Case value.level
            Case DTOSalesQuery.Levels.years
                sb.AppendLine("GROUP BY YEAR(Pdc.FchCreated) ")
            Case DTOSalesQuery.Levels.months
                sb.AppendLine("GROUP BY MONTH(Pdc.FchCreated) ")
            Case DTOSalesQuery.Levels.days
                sb.AppendLine("GROUP BY DAY(Pdc.FchCreated) ")
            Case DTOSalesQuery.Levels.Orders, DTOSalesQuery.Levels.Search
                sb.AppendLine("GROUP BY Pdc.Guid, Pdc.FchCreated, CliGral.FullNom, Pdc.Pdd ")
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlOrder(value As DTOSalesQuery) As String
        Dim sb As New System.Text.StringBuilder
        Select Case value.level
            Case DTOSalesQuery.Levels.years
                sb.AppendLine("ORDER BY YEAR(Pdc.FchCreated) DESC ")
            Case DTOSalesQuery.Levels.months
                sb.AppendLine("ORDER BY MONTH(Pdc.FchCreated) DESC ")
            Case DTOSalesQuery.Levels.days
                sb.AppendLine("ORDER BY DAY(Pdc.FchCreated) DESC ")
            Case DTOSalesQuery.Levels.Orders, DTOSalesQuery.Levels.Search
                sb.AppendLine("ORDER BY Pdc.FchCreated DESC ")
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function



End Class
