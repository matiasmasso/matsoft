Public Class ProductPluginItemLoader
    Shared Function Find(oGuid As Guid) As DTOProductPlugin.Item
        Dim retval As DTOProductPlugin.Item = Nothing
        Dim oProductPluginItem As New DTOProductPlugin.Item(oGuid)
        If Load(oProductPluginItem) Then
            retval = oProductPluginItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProductPluginItem As DTOProductPlugin.Item) As Boolean
        If Not oProductPluginItem.IsLoaded And Not oProductPluginItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ProductPlugin.Guid AS PluginGuid, ProductPlugin.Nom AS PluginNom ")
            sb.AppendLine(", ProductPluginItem.* ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
            sb.AppendLine("FROM ProductPluginItem ")
            sb.AppendLine("INNER JOIN ProductPlugin ON ProductPluginItem.Plugin = ProductPlugin.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ProductPluginItem.Product = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON ProductPluginItem.Product = Url.Guid ")
            sb.AppendLine("WHERE ProductPluginItem.Guid='" & oProductPluginItem.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oProductPluginItem
                    .Plugin = New DTOProductPlugin(oDrd("PluginGuid"))
                    .Plugin.nom = SQLHelper.GetStringFromDataReader(oDrd("PluginNom"))
                    .Lin = oDrd("Lin")
                    .product = SQLHelper.GetProductFromDataReader(oDrd)
                    .product.UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oProductPluginItem.IsLoaded
        Return retval
    End Function

    Shared Function Update(oProductPluginItem As DTOProductPlugin.Item, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProductPluginItem, oTrans)
            oTrans.Commit()
            oProductPluginItem.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oProductPluginItem As DTOProductPlugin.Item, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductPluginItem ")
        sb.AppendLine("WHERE Guid='" & oProductPluginItem.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProductPluginItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oProductPluginItem
            oRow("Plugin") = .Plugin.Guid
            oRow("Product") = .product.Guid
            oRow("Lin") = .Lin
            SQLHelper.SetNullableLangText(.LangNom, oRow, "NomEsp", "NomCat", "NomEng", "NomPor")
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProductPluginItem As DTOProductPlugin.Item, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProductPluginItem, oTrans)
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


    Shared Sub Delete(oProductPluginItem As DTOProductPlugin.Item, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductPluginItem WHERE Guid='" & oProductPluginItem.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class ProductPluginItemsLoader

    Shared Function All(oProductPlugin As DTOProductPlugin) As List(Of DTOProductPlugin.Item)
        Dim retval As New List(Of DTOProductPlugin.Item)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductPluginItem.* ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
        sb.AppendLine("FROM ProductPluginItem ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ProductPluginItem.Product = VwProductNom.guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON ProductPluginItem.Product = Url.Guid ")
        sb.AppendLine("WHERE ProductPluginItem.Plugin = '" & oProductPlugin.Guid.ToString & "' ")
        'sb.AppendLine("AND VwSkuNom.SkuNoWeb = 0 ")
        sb.AppendLine("ORDER BY ProductPluginItem.Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim item As New DTOProductPlugin.Item(oDrd("Guid"))
            With item
                '.Plugin = oProductPlugin
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
                .Product.obsoleto = oDrd("Obsoleto")
                .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                .product.UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                .Lin = retval.Count + 1
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
