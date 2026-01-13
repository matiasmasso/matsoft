Public Class ProductChannelLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOProductChannel
        Dim retval As DTOProductChannel = Nothing
        Dim oProductChannel As New DTOProductChannel(oGuid)
        If Load(oProductChannel) Then
            retval = oProductChannel
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProductChannel As DTOProductChannel) As Boolean
        If Not oProductChannel.IsLoaded And Not oProductChannel.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ProductChannel.Product, ProductChannel.DistributionChannel, ProductChannel.Cod ")
            sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine(", DistributionChannel.NomEsp AS ChannelEsp, DistributionChannel.NomCat AS ChannelCat, DistributionChannel.NomEng AS ChannelEng, DistributionChannel.NomPor AS ChannelPor ")
            sb.AppendLine("FROM ProductChannel ")
            sb.AppendLine("INNER JOIN VwProductNom ON ProductChannel.Product = VwProductNom.Guid ")
            sb.AppendLine("INNER JOIN DistributionChannel ON ProductChannel.DistributionChannel = DistributionChannel.Guid ")
            sb.AppendLine("WHERE ProductChannel.Guid='" & oProductChannel.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oProductChannel
                    .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"))
                    .DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                    .Cod = oDrd("Cod")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oProductChannel.IsLoaded
        Return retval
    End Function

    Shared Function Update(oProductChannel As DTOProductChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProductChannel, oTrans)
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


    Shared Sub Update(oProductChannel As DTOProductChannel, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductChannel ")
        sb.AppendLine("WHERE Guid='" & oProductChannel.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProductChannel.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oProductChannel
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("DistributionChannel") = SQLHelper.NullableBaseGuid(.DistributionChannel)
            oRow("Cod") = .Cod
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProductChannel As DTOProductChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProductChannel, oTrans)
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


    Shared Sub Delete(oProductChannel As DTOProductChannel, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductChannel WHERE Guid='" & oProductChannel.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ProductChannelsLoader

    Shared Function All(oProduct As DTOProduct) As List(Of DTOProductChannel)
        Dim retval As New List(Of DTOProductChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductChannel.Guid, ProductChannel.Product, ProductChannel.DistributionChannel, ProductChannel.Cod ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.Guid AS SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine(", DistributionChannel.Ord AS ChannelOrd, DistributionChannel.NomEsp AS ChannelEsp, DistributionChannel.NomCat AS ChannelCat, DistributionChannel.NomEng AS ChannelEng, DistributionChannel.NomPor AS ChannelPor ")
        sb.AppendLine("FROM ProductChannel ")
        sb.AppendLine("INNER JOIN VwProductNom ON ProductChannel.Product = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON ProductChannel.Product = VwProductParent.Parent AND VwProductParent.Child ='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("INNER JOIN DistributionChannel ON ProductChannel.DistributionChannel = DistributionChannel.Guid ")
        sb.AppendLine("ORDER BY DistributionChannel.Ord, ProductChannel.DistributionChannel, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductChannel(oDrd("Guid"))
            With item
                .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"))
                .DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                .DistributionChannel.LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ChannelEsp", "ChannelCat", "ChannelEng", "ChannelPor")
                .DistributionChannel.Ord = oDrd("ChannelOrd")
                .Cod = oDrd("Cod")
                If oProduct.Equals(.Product) Then
                    'Stop
                Else
                    .Inherited = True
                End If
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oChannel As DTODistributionChannel) As List(Of DTOProductChannel)
        Dim retval As New List(Of DTOProductChannel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductChannel.Guid, ProductChannel.Product, ProductChannel.Cod ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine("FROM ProductChannel ")
        sb.AppendLine("INNER JOIN VwProductNom ON ProductChannel.Product = VwProductNom.Guid ")
        sb.AppendLine("WHERE ProductChannel.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductChannel(oDrd("Guid"))
            With item
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
                .DistributionChannel = oChannel
                .Cod = oDrd("Cod")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

