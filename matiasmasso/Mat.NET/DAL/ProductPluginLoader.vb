Imports System.IO

Public Class ProductPluginLoader
    Shared Function Find(oGuid As Guid) As DTOProductPlugin
        Dim retval As DTOProductPlugin = Nothing
        Dim oProductPlugin As New DTOProductPlugin(oGuid)
        If Load(oProductPlugin) Then
            retval = oProductPlugin
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProductPlugin As DTOProductPlugin) As Boolean
        If Not oProductPlugin.IsLoaded And Not oProductPlugin.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ProductPlugin.* ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
            sb.AppendLine(", UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", UsrLastEdited.Adr AS UsrLastEditedEmailAddress, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine("FROM ProductPlugin ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrCreated ON ProductPlugin.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrLastEdited ON ProductPlugin.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ProductPlugin.Product = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON ProductPlugin.Product = Url.Guid ")
            sb.AppendLine("WHERE ProductPlugin.Guid = '" & oProductPlugin.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oProductPlugin
                    .Nom = oDrd("Nom")
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    .Product.UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()

            oProductPlugin.Items = ProductPluginItemsLoader.All(oProductPlugin)
        End If


        Dim retval As Boolean = oProductPlugin.IsLoaded
        Return retval
    End Function

    Shared Function Sprite(oGuid As Guid) As List(Of Byte())

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductPluginItem.product ")
        sb.AppendLine(", (CASE WHEN Stp.Thumbnail IS NULL THEN Art.Thumbnail ELSE Stp.Thumbnail END) AS Img ")
        sb.AppendLine("FROM ProductPluginItem ")
        sb.AppendLine("LEFT OUTER JOIN Art ON ProductPluginItem.product = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Stp ON ProductPluginItem.product = Stp.Guid ")
        sb.AppendLine("WHERE ProductPluginItem.Plugin = '" & oGuid.ToString & "' ")
        sb.AppendLine("ORDER BY ProductPluginItem.Lin")

        Dim retval As New List(Of Byte())
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImg As Byte()
            If IsDBNull(oDrd("Img")) Then
                Dim img As New Bitmap(DTOProductPlugin.ItemWidth, DTOProductPlugin.ItemHeight)
                Using ms = New MemoryStream()
                    img.Save(ms, Imaging.ImageFormat.Jpeg)
                    oImg = ms.ToArray()
                End Using
            Else
                oImg = oDrd("Img")
            End If
            retval.Add(oImg)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oProductPlugin As DTOProductPlugin, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProductPlugin, oTrans)
            oTrans.Commit()
            oProductPlugin.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        UpdateHeader(oProductPlugin, oTrans)
        DeleteItems(oProductPlugin, oTrans)
        UpdateItems(oProductPlugin, oTrans)
    End Sub

    Shared Sub UpdateHeader(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductPlugin ")
        sb.AppendLine("WHERE Guid='" & oProductPlugin.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProductPlugin.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oProductPlugin
            oRow("Nom") = .Nom
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductPluginItem ")
        sb.AppendLine("WHERE Plugin ='" & oProductPlugin.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item In oProductPlugin.Items
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = item.Guid
            oRow("Plugin") = oProductPlugin.Guid
            oRow("Lin") = oTb.Rows.Count
            oRow("Product") = item.product.Guid
            SQLHelper.SetNullableLangText(item.LangNom, oRow, "NomEsp", "NomCat", "NomEng", "NomPor")
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProductPlugin As DTOProductPlugin, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProductPlugin, oTrans)
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


    Shared Sub Delete(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        DeleteItems(oProductPlugin, oTrans)
        DeleteHeader(oProductPlugin, oTrans)
    End Sub

    Shared Sub DeleteHeader(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductPlugin WHERE Guid='" & oProductPlugin.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oProductPlugin As DTOProductPlugin, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductPluginItem WHERE Plugin='" & oProductPlugin.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class

Public Class ProductPluginsLoader

    Shared Function All() As List(Of Dictionary(Of String, Object))
        Dim oPlugins As New List(Of DTOProductPlugin)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductPlugin.Guid as PluginGuid ")
        sb.AppendLine(", ProductPluginItem.* ")
        sb.AppendLine(", VwProductGuid.Obsoleto ")
        sb.AppendLine("FROM ProductPlugin ")
        sb.AppendLine("INNER JOIN ProductPluginItem ON ProductPlugin.Guid = ProductPluginItem.Plugin")
        sb.AppendLine("INNER JOIN VwProductGuid ON ProductPluginItem.Product = VwProductGuid.Guid")
        sb.AppendLine("ORDER BY ProductPlugin.Guid, ProductPluginItem.Lin")
        Dim oPlugin As New DTOProductPlugin()
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPlugin.Guid.Equals(oDrd("PluginGuid")) Then
                oPlugin = New DTOProductPlugin(oDrd("PluginGuid"))
                oPlugins.Add(oPlugin)
            End If
            Dim item As New DTOProductPlugin.Item(oDrd("Guid"))
            item.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
            item.Product = New DTOProduct(oDrd("Product"))
            item.Product.obsoleto = oDrd("Obsoleto")
            oPlugin.Items.Add(item)
        Loop
        oDrd.Close()

        Dim a = oPlugins.SelectMany(Function(x) x.Items).Where(Function(x) x.Product IsNot Nothing AndAlso x.Product.obsoleto).Count

        Dim retval As New List(Of Dictionary(Of String, Object))
        For Each oPlugin In oPlugins
            retval.Add(oPlugin.Minified())
        Next
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct) As List(Of DTOProductPlugin)
        Dim retval As New List(Of DTOProductPlugin)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductPlugin.Guid as PluginGuid, ProductPlugin.* ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine("FROM ProductPlugin ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ProductPlugin.Product = VwProductNom.Guid")
        sb.AppendLine("WHERE ProductPlugin.Product = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ProductPlugin.FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductPlugin(oDrd("PluginGuid"))
            With item
                .Nom = oDrd("Nom")
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class

