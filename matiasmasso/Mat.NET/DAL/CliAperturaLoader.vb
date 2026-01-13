Public Class CliAperturaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCliApertura
        Dim retval As DTOCliApertura = Nothing
        Dim oCliApertura As New DTOCliApertura(oGuid)
        If Load(oCliApertura) Then
            retval = oCliApertura
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCliApertura As DTOCliApertura) As Boolean
        If Not oCliApertura.IsLoaded And Not oCliApertura.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Cli_Aperturas.* ")
            sb.AppendLine(", Brands.Brand AS BrandGuid, Brands.BrandNomEsp ")
            sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
            sb.AppendLine(", ContactClass.Esp AS ContactClassEsp, ContactClass.Cat AS ContactClassCat, ContactClass.Eng AS ContactClassEng, ContactClass.Por AS ContactClassPor ")
            sb.AppendLine(", ContactClass.DistributionChannel ")
            sb.AppendLine(", DistributionChannel.NomEsp AS ChannelNom ")
            sb.AppendLine("FROM Cli_Aperturas ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON Cli_Aperturas.ContactClass = ContactClass.Guid ")
            sb.AppendLine("LEFT OUTER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT Cli_Aperturas_Brands.Apertura, Cli_Aperturas_Brands.Brand, BrandNom.Esp AS BrandNomEsp FROM Cli_Aperturas_Brands INNER JOIN VwLangText BrandNom ON Cli_Aperturas_Brands.Brand = BrandNom.Guid AND BrandNom.Src = 28 ) Brands ON Cli_Aperturas.Guid = Brands.Apertura ")
            sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON CLI_APERTURAS.ZonaGuid = VwAreaNom.Guid ")
            sb.AppendLine("WHERE Cli_Aperturas.Guid='" & oCliApertura.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oCliApertura.IsLoaded Then
                    With oCliApertura
                        .Nom = oDrd("Nom")
                        .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomComercial"))
                        .Nif = SQLHelper.GetStringFromDataReader(oDrd("Nif"))
                        .Adr = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .Zip = SQLHelper.GetStringFromDataReader(oDrd("Zip"))
                        .Cit = SQLHelper.GetStringFromDataReader(oDrd("Cit"))

                        If Not IsDBNull(oDrd("ZonaGuid")) Then
                            .Zona = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZipGuid"), oDrd("ZipCod"))
                        End If
                        .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                        .Email = SQLHelper.GetStringFromDataReader(oDrd("Email"))
                        .Web = SQLHelper.GetStringFromDataReader(oDrd("web"))
                        If Not IsDBNull(oDrd("ContactClass")) Then
                            .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                            .ContactClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ContactClassEsp", "ContactClassCat", "ContactClassEng", "ContactClassPor")
                            If Not IsDBNull(oDrd("DistributionChannel")) Then
                                .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                                .ContactClass.DistributionChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelNom", "ChannelNom", "ChannelNom", "ChannelNom")
                            End If
                        End If
                        .CodSuperficie = DirectCast(oDrd("CodSuperficie"), DTOCliApertura.CodsSuperficie)
                        .CodVolumen = DirectCast(oDrd("CodVolumen"), DTOCliApertura.CodsVolumen)
                        .SharePuericultura = CInt(oDrd("SharePuericultura"))
                        .OtherShares = SQLHelper.GetStringFromDataReader(oDrd("OtherShares"))
                        .CodSalePoint = DirectCast(oDrd("CodSalePoints"), DTOCliApertura.CodsSalePoint)
                        .Associacions = SQLHelper.GetStringFromDataReader(oDrd("Associacions"))
                        .CodAntiguedad = DirectCast(oDrd("CodAntiguedad"), DTOCliApertura.CodsAntiguedad)
                        .FchApertura = SQLHelper.GetFchFromDataReader(oDrd("FchApertura"))
                        .CodExperiencia = DirectCast(oDrd("CodExperiencia"), DTOCliApertura.CodsExperiencia)

                        .Brands = New List(Of DTOGuidNom)
                        .OtherBrands = SQLHelper.GetStringFromDataReader(oDrd("OtherBrands"))
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                        .CodTancament = DirectCast(oDrd("CodTancament"), DTOCliApertura.CodsTancament)
                        .RepObs = SQLHelper.GetStringFromDataReader(oDrd("RepObs")).Trim

                        .Lang = DTOLang.ESP
                        If (SQLHelper.GetStringFromDataReader(oDrd("ISOPais")) = "PT") Then .Lang = DTOLang.POR
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("BrandGuid")) Then
                    Dim oBrand As New DTOGuidNom(oDrd("BrandGuid"), SQLHelper.GetStringFromDataReader(oDrd("BrandNomEsp")))
                    oCliApertura.Brands.Add(oBrand)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oCliApertura.IsLoaded
        Return retval
    End Function


    Shared Function Update(oCliApertura As DTOCliApertura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCliApertura, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        UpdateHeader(oCliApertura, oTrans)
        UpdateBrands(oCliApertura, oTrans)
    End Sub

    Shared Function UpdateStatus(exs As List(Of Exception), oCliApertura As DTOCliApertura, oCodTancament As DTOCliApertura.CodsTancament, sRepObs As String) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Cli_Aperturas ")
        sb.AppendLine("SET CodTancament = " & oCodTancament & " ")
        If String.IsNullOrEmpty(sRepObs) Then
            sb.AppendLine(", RepObs = NULL ")
        Else
            sb.AppendLine(", RepObs = '" & sRepObs & "' ")
        End If
        sb.AppendLine("WHERE Guid='" & oCliApertura.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Shared Sub UpdateHeader(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Cli_Aperturas ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oCliApertura.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCliApertura.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCliApertura

            oRow("Nom") = .Nom
            oRow("RaoSocial") = .RaoSocial
            oRow("NomComercial") = .NomComercial
            oRow("Nif") = .Nif
            oRow("Adr") = .Adr
            oRow("Zip") = .Zip
            oRow("Cit") = .Cit
            oRow("ZonaGuid") = SQLHelper.NullableBaseGuid(.Zona)
            oRow("Tel") = .Tel
            oRow("Email") = .Email
            oRow("Web") = .Web
            oRow("ContactClass") = SQLHelper.NullableBaseGuid(.ContactClass)
            oRow("CodSuperficie") = .CodSuperficie
            oRow("CodVolumen") = .CodVolumen
            oRow("SharePuericultura") = .SharePuericultura
            oRow("OtherShares") = .OtherShares
            oRow("CodSalePoints") = .CodSalePoint
            oRow("Associacions") = .Associacions
            oRow("CodAntiguedad") = .CodAntiguedad
            oRow("FchApertura") = .FchApertura
            oRow("CodExperiencia") = .CodExperiencia
            oRow("OtherBrands") = .OtherBrands
            oRow("Obs") = .Obs
            oRow("FchCreated") = .FchCreated
            oRow("CodTancament") = .CodTancament
            oRow("RepObs") = .RepObs
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateBrands(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        If Not oCliApertura.IsNew Then DeleteBrands(oCliApertura, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Cli_Aperturas_Brands ")
        sb.AppendLine("WHERE Apertura='" & oCliApertura.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oCliApertura.Brands
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Apertura") = oCliApertura.Guid
            oRow("Brand") = item.Guid
        Next
        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCliApertura As DTOCliApertura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCliApertura, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        DeleteBrands(oCliApertura, oTrans)
        DeleteHeader(oCliApertura, oTrans)
    End Sub

    Shared Sub DeleteHeader(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cli_Aperturas WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCliApertura.Guid.ToString())
    End Sub

    Shared Sub DeleteBrands(oCliApertura As DTOCliApertura, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cli_Aperturas_Brands WHERE Apertura=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCliApertura.Guid.ToString())
    End Sub

#End Region

End Class

Public Class CliAperturasLoader

    Shared Function All(oUser As DTOUser) As DTOCliApertura.Collection
        Dim retval As New DTOCliApertura.Collection
        If oUser IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Select C.Guid, C.FchCreated, C.ZonaGuid, Zona.Nom As ZonaNom, C.Cit, C.Nom, C.CodTancament ")
            sb.AppendLine("FROM Cli_Aperturas C ")
            sb.AppendLine("LEFT OUTER JOIN Zona ON C.ZonaGuid = Zona.Guid ")

            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.operadora
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("WHERE C.ZonaGuid IN (")
                    sb.AppendLine("     SELECT VwAreaParent.ChildGuid ")
                    sb.AppendLine("     FROM VwAreaParent ")
                    sb.AppendLine("     INNER JOIN RepProducts ON VwAreaParent.ParentGuid=RepProducts.Area ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid  ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("     AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                    sb.AppendLine("     AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=GETDATE()) ")
                    sb.AppendLine(") ")
                Case Else
                    sb.AppendLine("WHERE C.ISOPAIS = 'XX' ")
            End Select
            sb.AppendLine("GROUP BY C.Guid, C.FchCreated, C.ZonaGuid, Zona.Nom, C.Cit, C.Nom, C.CodTancament ")
            sb.AppendLine("ORDER BY C.FchCreated DESC")


            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOCliApertura(oDrd("Guid"))
                With item
                    .Nom = oDrd("Nom")
                    .FchCreated = oDrd("FchCreated")
                    If Not IsDBNull(oDrd("ZonaGuid")) Then
                        .Zona = New DTOZona(oDrd("ZonaGuid"))
                        .Zona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    End If
                    .Cit = SQLHelper.GetStringFromDataReader(oDrd("Cit"))
                    .CodTancament = oDrd("CodTancament")
                End With
                retval.Add(item)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

End Class
