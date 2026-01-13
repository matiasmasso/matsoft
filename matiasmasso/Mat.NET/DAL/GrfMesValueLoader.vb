Public Class GrfMesValuesLoader



    Shared Function All(oUser As DTOUser, DtFchFrom As Date, DtFchTo As Date) As List(Of DTOGrfMesValue)
        Dim retval As New List(Of DTOGrfMesValue)
        UserLoader.Load(oUser)

        Dim sb As New System.Text.StringBuilder
        Select Case oUser.Rol.Id
            Case DTORol.Ids.manufacturer
                sb.AppendLine("SELECT Stp.Guid, VwSkuNom.CategoryNomEsp, Stp.Color, CONVERT(VARBINARY(8), color) AS HexColor, Year(Pdc.FchCreated) AS Year, Month(Pdc.FchCreated) AS Mes, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
                sb.AppendLine("From Email_Clis  ")
                sb.AppendLine("INNER JOIN Tpa ON Email_Clis.ContactGuid = Tpa.Proveidor ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.Guid ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.FchCreated BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & " 00:00:00' AND '" & Format(DtFchTo, "yyyyMMdd") & " 23:59:59' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
                sb.AppendLine("AND (Stp.Codi = " & DTOProductCategory.Codis.Standard & " OR Stp.Codi = " & DTOProductCategory.Codis.Accessories & ") ")
                sb.AppendLine("GROUP BY Stp.Guid, VwSkuNom.CategoryNomEsp, Stp.Color, HexColor, year(Pdc.FchCreated), Month(Pdc.FchCreated) ")
                sb.AppendLine("ORDER BY Stp.Guid, year(Pdc.FchCreated), Mes")
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.salesManager
                sb.AppendLine("SELECT Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, CONVERT(VARBINARY(8), color) AS HexColor, Year(Pdc.FchCreated) AS Year, Month(Pdc.FchCreated) AS Mes, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.Guid ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("WHERE VwSkuNom.Emp =" & oUser.Emp.Id & " ")
                sb.AppendLine("AND Pdc.FchCreated BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & " 00:00:00' AND '" & Format(DtFchTo, "yyyyMMdd") & " 23:59:59' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
                sb.AppendLine("GROUP BY Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, year(Pdc.FchCreated), Month(Pdc.FchCreated) ")
                sb.AppendLine("ORDER BY Tpa.Guid, year(Pdc.FchCreated), Mes")
            Case DTORol.Ids.Rep, DTORol.Ids.comercial
                sb.AppendLine("SELECT Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, HexColor, CONVERT(VARBINARY(8), color) AS HexColor, Year(Pdc.FchCreated) AS Year, Month(Pdc.FchCreated) AS Mes, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.Guid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.FchCreated BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & " 00:00:00' AND '" & Format(DtFchTo, "yyyyMMdd") & " 23:59:59' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
                sb.AppendLine("GROUP BY Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, HexColor, year(Pdc.FchCreated), Month(Pdc.FchCreated) ")
                sb.AppendLine("ORDER BY Tpa.Guid, year(Pdc.FchCreated), Mes")
            Case DTORol.Ids.CliFull, DTORol.Ids.cliLite
                sb.AppendLine("SELECT Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, CONVERT(VARBINARY(8), color) AS HexColor, Year(Pdc.FchCreated) AS Year, Month(Pdc.FchCreated) AS Mes, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.Guid ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.FchCreated BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & " 00:00:00' AND '" & Format(DtFchTo, "yyyyMMdd") & " 23:59:59' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
                sb.AppendLine("GROUP BY Tpa.Guid, VwSkuNom.BrandNomEsp, Tpa.Color, HexColor, year(Pdc.FchCreated), Month(Pdc.FchCreated) ")
                sb.AppendLine("ORDER BY Tpa.Guid, year(Pdc.FchCreated), Mes")
            Case Else
                Return retval
                Exit Function
        End Select

        Dim SQL As String = sb.ToString

        Dim item As DTOGrfMesValue = Nothing
        Dim items As New List(Of DTOGrfMesValue)
        Dim oProduct As New DTOProductCategory()
        Dim iYear As Integer
        Dim iMes As Integer
        Dim value As DTOYearMonth
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oProduct.Guid.Equals(oDrd("Guid")) Then
                oProduct = New DTOProductCategory(oDrd("Guid"))
                SQLHelper.LoadLangTextFromDataReader(oProduct.nom, oDrd, "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp")
                Dim oEmptyYearMonths As List(Of DTOYearMonth) = YearMonthsLoader.All(DtFchFrom, DtFchTo)
                item = New DTOGrfMesValue(oProduct, oEmptyYearMonths)
                If Not IsDBNull(oDrd("HexColor")) Then
                    item.Color = System.Drawing.ColorTranslator.FromHtml(oDrd("HexColor").ToString())
                End If
                items.Add(item)

            End If
            iYear = oDrd("Year")
            iMes = CInt(oDrd("Mes"))
            value = item.Mesos.Find(Function(x) x.Year = iYear And x.Month = iMes)
            If value IsNot Nothing Then
                value.Eur = oDrd("Eur")
            End If

        Loop
        oDrd.Close()

        retval = items.OrderByDescending(Function(x) x.Sum).ToList
        Return retval
    End Function


End Class
