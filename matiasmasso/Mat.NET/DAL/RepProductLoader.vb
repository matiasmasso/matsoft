Public Class RepProductLoader

#Region "Crud"

    Shared Function Find(oGuid As Guid) As DTORepProduct
        Dim retval As DTORepProduct = Nothing
        Dim oRepProduct As New DTORepProduct(oGuid)
        If Load(oRepProduct) Then
            retval = oRepProduct
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepProduct As DTORepProduct) As Boolean
        If Not oRepProduct.IsLoaded And Not oRepProduct.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT RepProducts.*, CliRep.Abr ")
            sb.AppendLine(", DistributionChannel.NomEsp AS ChannelEsp, DistributionChannel.NomCat AS ChannelCat, DistributionChannel.NomEng AS ChannelEng, DistributionChannel.NomPor AS ChannelPor ")
            sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine(", VwAreaNom.* ")
            sb.AppendLine("FROM RepProducts ")
            sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
            sb.AppendLine("INNER JOIN VwAreaNom ON RepProducts.Area=VwAreaNom.Guid ")
            sb.AppendLine("INNER JOIN VwProductNom ON RepProducts.Product=VwProductNom.Guid ")
            sb.AppendLine("INNER JOIN DistributionChannel ON RepProducts.DistributionChannel=DistributionChannel.Guid ")
            sb.AppendLine("WHERE RepProducts.Guid='" & oRepProduct.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRepProduct
                    .Rep = New DTORep(oDrd("Rep"))
                    .Rep.NickName = oDrd("Abr")

                    .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"))
                    '.Product = ProductLoader.NewProduct(DirectCast(oDrd("Product"), Guid), DirectCast(oDrd("ProductCod"), DTOProduct.SourceCods), oDrd("ProductNom").ToString())
                    '.Area = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"))
                    '.Area = AreaLoader.NewArea(oDrd("Area"), oDrd("AreaCod"))
                    .Area = SQLHelper.GetAreaFromDataReader(oDrd)
                    .Cod = oDrd("Cod")
                    .FchFrom = CDate(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .ComStd = SQLHelper.GetDecimalFromDataReader(oDrd("ComStd"))
                    .ComRed = SQLHelper.GetDecimalFromDataReader(oDrd("ComRed"))
                    .DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                    .DistributionChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelEsp", "ChannelCat", "ChannelEng", "ChannelPor")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepProduct.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepProduct As DTORepProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepProduct, oTrans)
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

    Shared Sub Update(oRepProduct As DTORepProduct, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM RepProducts WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oRepProduct.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRepProduct.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepProduct
            oRow("Rep") = .Rep.Guid
            oRow("Product") = .Product.Guid
            oRow("Area") = .Area.Guid
            oRow("DistributionChannel") = .DistributionChannel.Guid
            oRow("Cod") = .Cod
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("ComStd") = SQLHelper.NullableDecimal(.ComStd)
            oRow("ComRed") = SQLHelper.NullableDecimal(.ComRed)
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oRepProduct As DTORepProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepProduct, oTrans)
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


    Shared Sub Delete(oRepProduct As DTORepProduct, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepProducts WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oRepProduct.Guid.ToString())
    End Sub

#End Region



    Shared Function GetRepProduct(oArea As DTOArea, oProduct As DTOProduct, oChannel As DTODistributionChannel, DtFch As Date) As DTORepProduct
        Dim retval As DTORepProduct = Nothing
        Dim sFch As String = Format(DtFch, "yyyyMMdd")

        Dim SQL As String = "SELECT RepProducts.Guid " _
        & "FROM VwProductParent INNER JOIN " _
        & "RepProducts ON VwProductParent.Parent = RepProducts.Product INNER JOIN " _
        & "VwAreaParent ON VwAreaParent.ParentGuid = RepProducts.Area " _
        & "WHERE VwProductParent.Child = '" & oProduct.Guid.ToString & "' " _
        & "AND VwAreaParent.ChildGuid = '" & oArea.Guid.ToString & "' " _
        & "AND RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' " _
        & "AND RepProducts.FchFrom <= '" & sFch & "' AND (RepProducts.FchTo IS NULL OR RepProducts.fchTo > '" & sFch & "') " _
        & "AND RepProducts.Cod = " & DTORepProduct.Cods.included & " " _
        & "AND RepProducts.Rep NOT IN " _
        & "(SELECT        RepProducts.Rep " _
        & "FROM            VwProductParent " _
        & "INNER JOIN RepProducts ON VwProductParent.Parent = RepProducts.Product " _
        & "INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = RepProducts.Area " _
        & "WHERE VwProductParent.Child = '" & oProduct.Guid.ToString & "' " _
        & "AND VwAreaParent.ChildGuid = '" & oArea.Guid.ToString & "' " _
        & "AND RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' " _
        & "AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom <= GETDATE())  " _
        & "AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) " _
        & "AND RepProducts.Cod = " & DTORepProduct.Cods.excluded & " )"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, oArea.Guid.ToString())
        If oDrd.Read Then
            Dim oGuid As Guid = oDrd("Guid")
            retval = Find(oGuid)
        End If
        oDrd.Close()

        Return retval
    End Function
End Class

Public Class RepProductsLoader

    Shared Function SQLRepChannelAreas(oRep As DTOBaseGuid) As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT RepProducts.DistributionChannel, VwAreaParent.ChildGuid ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN VwAreaParent ON RepProducts.Area = VwAreaParent.ParentGuid ")
        If TypeOf oRep Is DTOUser Then
            sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
            sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oRep.Guid.ToString & "' ")
        ElseIf TypeOf oRep Is DTORep Then
            sb.AppendLine("WHERE RepProducts.Rep = '" & oRep.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND RepProducts.Cod = 1 ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        sb.AppendLine("GROUP BY RepProducts.DistributionChannel, VwAreaParent.ChildGuid")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function All(Optional oEmp As DTOEmp = Nothing, Optional oRep As DTORep = Nothing, Optional IncludeObsolets As Boolean = False) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepProducts.Guid AS RepProductGuid, RepProducts.Cod ")
        sb.AppendLine(", RepProducts.Product, VwProductNom.FullNom as ProductNom, VwProductNom.Cod as ProductCod ")
        sb.AppendLine(", RepProducts.Area, VwAreaNom.* ")
        sb.AppendLine(", RepProducts.DistributionChannel, DistributionChannel.NomEsp AS ChannelEsp, DistributionChannel.NomCat AS ChannelCat, DistributionChannel.NomEng AS ChannelEng, DistributionChannel.NomPor AS ChannelPor ")
        sb.AppendLine(", RepProducts.FchFrom, RepProducts.FchTo, RepProducts.ComStd, RepProducts.ComRed ")
        If oRep Is Nothing Then
            sb.AppendLine(", RepProducts.Rep, CliRep.Abr ")
        End If

        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN VwProductNom ON RepProducts.Product=VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwAreaNom ON RepProducts.Area=VwAreaNom.Guid ")
        sb.AppendLine("INNER JOIN DistributionChannel ON RepProducts.DistributionChannel=DistributionChannel.Guid ")
        If oRep Is Nothing Then
            sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        End If

        If oRep Is Nothing Then
            sb.AppendLine("WHERE 1=1 ")
        Else
            sb.AppendLine("WHERE RepProducts.Rep='" & oRep.Guid.ToString & "' ")
        End If

        If Not IncludeObsolets Then
            sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
            sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        End If

        sb.AppendLine("ORDER BY DistributionChannel.NomEsp, VwAreaNom.CountryISO, VwAreaNom.ZonaNom, VwAreaNom.LocationNom, ProductNom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As DTOProduct = ProductLoader.NewProduct(oDrd("Product"), oDrd("ProductCod"), oDrd("ProductNom"))
            Dim oChannel As New DTODistributionChannel(oDrd("DistributionChannel"))
            oChannel.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelEsp", "ChannelCat", "ChannelEng", "ChannelPor")
            Dim oItem As New DTORepProduct(oDrd("RepProductGuid"))
            With oItem
                .cod = oDrd("Cod")
                .product = oProduct
                .area = SQLHelper.GetAreaFromDataReader(oDrd) 'DTOArea.Factory(oDrd("Area"), oDrd("AreaCod"), oDrd("AreaNom"))
                .distributionChannel = oChannel

                If oRep Is Nothing Then
                    .rep = New DTORep(oDrd("Rep"))
                    With .rep
                        .nom = oDrd("Abr")
                    End With
                Else
                    .rep = oRep
                End If

                .fchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .fchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .comStd = SQLHelper.GetDecimalFromDataReader(oDrd("ComStd"))
                .comRed = SQLHelper.GetDecimalFromDataReader(oDrd("ComRed"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct, BlIncludeObsolets As Boolean) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)
        Dim SQL As String = "SELECT RepProducts.Guid, RepProducts.Cod, " _
        & "RepProducts.Rep, CliRep.Abr, " _
        & "RepProducts.Area, VwArea.Nom as AreaNom, VwArea.Cod AS AreaCod, " _
        & "RepProducts.DistributionChannel, DistributionChannel.NomEsp, DistributionChannel.NomCat, DistributionChannel.NomEng, DistributionChannel.NomPor, " _
        & "RepProducts.FchFrom, RepProducts.FchTo, RepProducts.ComStd, RepProducts.ComRed " _
        & "FROM RepProducts " _
        & "INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid " _
        & "INNER JOIN VwArea ON RepProducts.Area=VwArea.Guid " _
        & "INNER JOIN VwProductParent ON RepProducts.Product = VwProductParent.Child " _
        & "INNER JOIN DistributionChannel ON RepProducts.DistributionChannel = DistributionChannel.Guid " _
        & "WHERE RepProducts.Product='" & oProduct.Guid.ToString() & "' "

        If Not BlIncludeObsolets Then
            SQL = SQL & "AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE())  " _
            & "AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) "
        End If
        SQL = SQL & "ORDER BY CliRep.Abr, AreaNom, DistributionChannel.Ord "

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Rep"))
            oRep.NickName = oDrd("Abr")

            Dim oArea As DTOArea = AreaLoader.NewArea(oDrd("Area"), oDrd("AreaCod"), oDrd("AreaNom"))
            'If oArea.Cod = 1 Then Stop
            Dim oItem As New DTORepProduct(oDrd("Guid"))
            With oItem
                .Cod = oDrd("Cod")
                .Rep = oRep
                .Product = oProduct
                .area = oArea
                .distributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                .distributionChannel.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                If Not IsDBNull(oDrd("FchFrom")) Then
                    .FchFrom = oDrd("FchFrom")
                End If
                If Not IsDBNull(oDrd("FchTo")) Then
                    .FchTo = oDrd("FchTo")
                End If
                If Not IsDBNull(oDrd("ComStd")) Then
                    .ComStd = oDrd("ComStd")
                End If
                If Not IsDBNull(oDrd("ComRed")) Then
                    .ComRed = oDrd("ComRed")
                End If
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oChannel As DTODistributionChannel, oArea As DTOArea, oProduct As DTOProduct, DtFch As Date) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        Dim sFch As String = Format(DtFch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT RepProducts.Guid, RepProducts.Cod ")
        sb.AppendLine(", RepProducts.Rep, CliRep.Abr ")
        sb.AppendLine(", RepProducts.Area, VwArea.Nom as AreaNom, VwArea.Cod AS AreaCod ")
        sb.AppendLine(", RepProducts.FchFrom, RepProducts.FchTo, RepProducts.ComStd, RepProducts.ComRed ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN VwArea ON RepProducts.Area=VwArea.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom ON RepProducts.Product=VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON VwProductParent.Parent = RepProducts.Product ")
        sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = RepProducts.Area ")

        'Inclosos
        If oChannel Is Nothing Then
            sb.AppendLine("WHERE RepProducts.Guid IS NOT NULL ") 'PER POSAR ALGO
        Else
            sb.AppendLine("WHERE RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND VwProductParent.Child = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND VwAreaParent.ChildGuid = '" & oArea.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom <= '" & sFch & "' ) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo >= '" & sFch & "' ) ")
        sb.AppendLine("AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
        sb.AppendLine("AND RepProducts.Rep NOT IN ")

        sb.AppendLine("( ")
        sb.AppendLine("SELECT RepProducts.Rep ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN VwProductParent ON VwProductParent.Parent = RepProducts.Product ")
        sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = RepProducts.Area ")

        'exclosos
        If oChannel Is Nothing Then
            sb.AppendLine("WHERE RepProducts.Guid IS NOT NULL ") 'PER POSAR ALGO
        Else
            sb.AppendLine("WHERE RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND VwProductParent.Child = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND VwAreaParent.ChildGuid = '" & oArea.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom <= '" & sFch & "' ) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo >= '" & sFch & "' ) ")
        sb.AppendLine("AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Excluded) & " ")
        sb.AppendLine(") ")

        sb.AppendLine("ORDER BY RepProducts.Cod ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Rep"))
            oRep.nickName = oDrd("Abr")

            Dim oItem As New DTORepProduct(oDrd("Guid"))
            With oItem
                .Cod = oDrd("Cod")
                .Rep = oRep
                .DistributionChannel = oChannel
                .product = SQLHelper.GetProductFromDataReader(oDrd)

                .Area = oArea
                If Not IsDBNull(oDrd("FchFrom")) Then
                    .FchFrom = oDrd("FchFrom")
                End If
                If Not IsDBNull(oDrd("FchTo")) Then
                    .FchTo = oDrd("FchTo")
                End If
                If Not IsDBNull(oDrd("ComStd")) Then
                    .ComStd = oDrd("ComStd")
                End If
                If Not IsDBNull(oDrd("ComRed")) Then
                    .ComRed = oDrd("ComRed")
                End If
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()


        Return retval
    End Function

    Shared Function All(oArea As DTOArea, BlIncludeObsolets As Boolean) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepProducts.Guid, RepProducts.Cod, ")
        sb.AppendLine("RepProducts.Rep, CliRep.Abr, ")
        sb.AppendLine("RepProducts.Product, VwProductNom.FullNom as ProductNom, VwProductNom.Cod as ProductCod, ")
        sb.AppendLine("RepProducts.FchFrom, RepProducts.FchTo, RepProducts.ComStd, RepProducts.ComRed ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN VwProductNom ON RepProducts.Product=VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON RepProducts.Area=VwAreaParent.ChildGuid ")
        sb.AppendLine("INNER JOIN VwProductParent ON RepProducts.Product = VwProductParent.Child ")
        sb.AppendLine("WHERE VwAreaParent.ParentGuid=@AreaParent ")

        If Not BlIncludeObsolets Then
            sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE())  ")
            sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        End If
        sb.AppendLine("ORDER BY CliRep.Abr, ProductNom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@AreaParent", oArea.Guid.ToString())
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Rep"))
            oRep.NickName = oDrd("Abr")

            Dim oProduct As DTOProduct = ProductLoader.NewProduct(oDrd("Product"), oDrd("ProductCod"), oDrd("ProductNom"))

            Dim oItem As New DTORepProduct(oDrd("Guid"))
            With oItem
                .Cod = oDrd("Cod")
                .Rep = oRep
                .Product = oProduct
                .Area = oArea
                If Not IsDBNull(oDrd("FchFrom")) Then
                    .FchFrom = oDrd("FchFrom")
                End If
                If Not IsDBNull(oDrd("FchTo")) Then
                    .FchTo = oDrd("FchTo")
                End If
                If Not IsDBNull(oDrd("ComStd")) Then
                    .ComStd = oDrd("ComStd")
                End If
                If Not IsDBNull(oDrd("ComRed")) Then
                    .ComRed = oDrd("ComRed")
                End If
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function RepsxAreaWithMobiles(Optional BlIncludeObsolets As Boolean = False) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepProducts.Rep, CliRep.Abr, Tel.Mobile ")
        sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO ")
        sb.AppendLine(", VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.CountryNomPor ")
        sb.AppendLine(", VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN VwAreaNom ON RepProducts.Area=VwAreaNom.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON RepProducts.Area=VwAreaParent.ChildGuid ")
        sb.AppendLine("LEFT OUTER JOIN (Select CliGuid, Num AS Mobile FROM CliTel WHERE Cod=3 And Privat=0) Tel ON RepProducts.Rep=Tel.CliGuid ")
        sb.AppendLine("WHERE RepProducts.Cod=" & CInt(DTORepProduct.Cods.included) & "  ")

        If Not BlIncludeObsolets Then
            sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE())  ")
            sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        End If
        sb.AppendLine("GROUP BY RepProducts.Rep, CliRep.Abr, Tel.Mobile ")
        sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("ORDER BY VwAreaNom.ZonaNom, CliRep.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Rep"))
            With oRep
                .NickName = oDrd("Abr")
                .Telefon = oDrd("mobile")
            End With

            Dim oItem As New DTORepProduct(System.Guid.Empty)
            With oItem
                '.Cod = oDrd("Cod")
                .Rep = oRep
                .Area = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"))
                'If Not IsDBNull(oDrd("FchFrom")) Then
                '.FchFrom = oDrd("FchFrom")
                'End If
                'If Not IsDBNull(oDrd("FchTo")) Then
                '.FchTo = oDrd("FchTo")
                'End If
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Customers(oRepUser As DTOUser, Optional OrderByNomComercial As Boolean = False) As List(Of DTOGuidNode)
        Dim retval As New List(Of DTOGuidNode)
        Dim oLang As DTOLang = oRepUser.Lang
        Dim sNomField As String = oLang.Tradueix("CountryEsp", "CountryCat", "CountryEng", "CountryPor")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAddress.CountryGuid, VwAddress." & sNomField & " AS CountryNom ")
        sb.AppendLine(", VwAddress.ZonaGuid, VwAddress.ZonaNom ")
        sb.AppendLine(", VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwRepCustomers.Customer ")
        If OrderByNomComercial Then
            sb.AppendLine(", (CASE WHEN CliGral.NomCom='' THEN CliGral.RaoSocial ELSE CliGral.NomCom END)+(CASE WHEN CliClient.Ref is null THEN '' ELSE ' '+CliClient.Ref END) AS CustomerNom ")
        Else
            sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial END)+(CASE WHEN CliClient.Ref is null THEN '' ELSE ' '+CliClient.Ref END) AS CustomerNom ")
        End If

        sb.AppendLine("FROM VwRepCustomers ")
        sb.AppendLine("INNER JOIN CliGral ON VwRepCustomers.Customer = CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON VwRepCustomers.Customer=VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oRepUser.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CountryNom, VwAddress.ZonaNom, VwAddress.LocationNom ")
        If OrderByNomComercial Then
            sb.AppendLine(", (CASE WHEN CliGral.NomCom='' THEN CliGral.RaoSocial ELSE CliGral.NomCom END),(CASE WHEN CliClient.Ref is null THEN '' ELSE ' '+CliClient.Ref END)")
        Else
            sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial END),(CASE WHEN CliClient.Ref is null THEN '' ELSE ' '+CliClient.Ref END)")
        End If
        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOGuidNode(Guid.NewGuid)
        Dim oZona As New DTOGuidNode(Guid.NewGuid)
        Dim oLocation As New DTOGuidNode(Guid.NewGuid)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOGuidNode(oDrd("CountryGuid"), oDrd("CountryNom"))
                retval.Add(oCountry)
            End If
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                oZona = New DTOGuidNode(oDrd("ZonaGuid"), oDrd("ZonaNom"))
                oCountry.Items.Add(oZona)
            End If
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                oLocation = New DTOGuidNode(oDrd("LocationGuid"), oDrd("LocationNom"))
                oZona.Items.Add(oLocation)
            End If

            Dim sCliNom As String = oDrd("CustomerNom")
            Dim oCustomer As New DTOGuidNode(oDrd("Customer"), sCliNom)
            oLocation.Items.Add(oCustomer)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function Catalogue(oRep As DTORep) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid AS SkuGuid, SkuNom.Esp AS SkuNomEsp, Art.LastProduction ")
        sb.AppendLine(", Stp.Guid AS CategoryGuid, CategoryNom.Esp AS CategoryNomEsp ")
        sb.AppendLine(", Tpa.Guid AS BrandGuid, BrandNom.Esp AS BrandNom ")

        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")

        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa On Stp.Brand = Tpa.Guid ")

        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")

        sb.AppendLine("INNER JOIN VwProductParent ON Art.Guid = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN RepProducts ON VwProductParent.Parent = RepProducts.Product ")

        sb.AppendLine("WHERE RepProducts.Rep='" & oRep.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.Cod = 1) ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        sb.AppendLine("AND Tpa.Obsoleto = 0 ")
        sb.AppendLine("AND Stp.Obsoleto = 0 ")
        sb.AppendLine("AND (Stp.Codi = " & DTOProductCategory.Codis.Standard & " OR Stp.Codi = " & DTOProductCategory.Codis.Accessories & ") ")
        sb.AppendLine("AND Stp.Web_Enabled_Pro = 1 ")
        sb.AppendLine("AND ART.NoPro = 0 ")
        sb.AppendLine("AND ART.Obsoleto = 0 ")

        sb.AppendLine("GROUP BY Art.Guid, SkuNom.Esp, Art.LastProduction ")
        sb.AppendLine(", Stp.Guid, Stp.Ord ")
        sb.AppendLine(", Tpa.Guid, BrandNom.Esp, Tpa.Ord ")
        sb.AppendLine(", CategoryNom.Esp, CategoryNom.Cat, CategoryNom.Eng, CategoryNom.Por ")
        sb.AppendLine(", SkuNom.Esp, SkuNom.Cat, SkuNom.Eng, SkuNom.Por ")
        sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Tpa.Guid, Stp.Ord, CategoryNom.Esp, Stp.Guid, SkuNom.Esp")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                With oBrand
                    SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    .categories = New List(Of DTOProductCategory)
                End With
            End If
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                With oCategory
                    .Brand = oBrand
                    SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    .skus = New List(Of DTOProductSku)
                End With
                oBrand.Categories.Add(oCategory)
            End If
            Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
            With oSku
                .Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                .lastProduction = oDrd("LastProduction")
            End With
            oCategory.Skus.Add(oSku)
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oRepProducts As List(Of DTORepProduct), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            RepProductsLoader.Update(oRepProducts, oTrans)
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

    Shared Sub Update(oRepProducts As List(Of DTORepProduct), oTrans As SqlTransaction)
        For Each item In oRepProducts
            RepProductLoader.Update(item, oTrans)
        Next
    End Sub


    Shared Function Delete(oRepProducts As List(Of DTORepProduct), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepProducts, oTrans)
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

    Shared Sub Delete(oRepProducts As List(Of DTORepProduct), oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE RepProducts WHERE (")
        For Each item In oRepProducts
            If item.UnEquals(oRepProducts.First) Then
                sb.AppendLine("OR ")
            End If
            sb.AppendLine("Guid = '" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim sql As String = sb.ToString
        Dim retval As Integer = SQLHelper.ExecuteNonQuery(sql, oTrans)
    End Sub


End Class
